using Library.DAL;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Library.UI
{
    /// <summary>
    /// Create / Update an Item page, accessed by admin only.
    /// </summary>
    public sealed partial class CreateNewItemPage : Page
    {
        // Fields
        /// <summary>
        /// Our Repository which we are looking at IRepository <see cref="IRepository{T}"/>
        /// </summary>
        IRepository<LibraryItem> _repo = new LibraryRepository();
        /// <summary>
        /// The item which admin wants to edit / update
        /// </summary>
        LibraryItem _editItem;
        /// <summary>
        /// Ignored character for reading seprate wrods and adding them to a list
        /// </summary>
        readonly char[] _delimiterChars = { ' ', ',', '.', ':', '\t' };

        // The parameters which need to be initialized for the new Item / Update
        DateTime _publicationDate;
        string _title;
        double _price;
        int _numOfPages;
        string _country = "Israel";
        int _sale = 0;
        int _serialNumber = 0;
        int _revision = new Random().Next(10);

        // Ctor
        public CreateNewItemPage() => this.InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Init_BookGenresAndCountriesComboBox();
            Init_JournalGenresAndFrequencyComboBox();
            PaneNV.Header = LogInPage.WebSurfer.Name;
            if (e.Parameter is LibraryItem)
            {
                _editItem = (LibraryItem)e.Parameter;
                AddItemBtn.Visibility = Visibility.Collapsed;
                EditItemBtn.Visibility = Visibility.Visible;
                Init_ItemTypeComboBox(_editItem.GetType());
                if (_editItem is Book)
                    Init_Book_TextBoxesWithKnownProperties();
                else if (_editItem is Journal)
                    Init_Journal_TextBoxesWithKnownProperties();
            }
            else
                Init_ItemTypeComboBox();
            
            base.OnNavigatedTo(e);
        }
        void Init_MustPropertiesItem() // Initizalize the common parameters for the edit Item
        {
            TitleTB1.Text = _editItem.Title;
            PriceTB1.Text = $"{_editItem.Price}";
            NumOfPagesTB1.Text = $"{_editItem.NumOfPages}";
            SaleTB1.Text = $"{_editItem.Sale}";
            PublicationDate.Date = _publicationDate = _editItem.PublicationDate;
        }
        void Init_Journal_TextBoxesWithKnownProperties() // Initialize the Journal's fields with his parameters
        {
            JournalSP.Visibility = Visibility.Visible;
            Init_MustPropertiesItem();

            var item = _editItem as Journal;
            foreach (var str in item.Contributors) ContributorsTB1.Text += $"{str.Replace(" ", "-")}\t";
            foreach (var editor in item.Editors) EditorsTB1.Text += $"{editor.Replace(" ", "-")}\t";
            JournalGenresCB.SelectedItem = item.Genres.FirstOrDefault();
            FrequencyCB.SelectedItem = item.Frequency.ToString();
        }
        void Init_JournalGenresAndFrequencyComboBox() // Initializing the ComboBoxes with the constant options
        {
            JournalGenresCB.ItemsSource = Journal.JournalGenres;
            FrequencyCB.ItemsSource = Enum.GetNames(typeof(JournalFrequency));
        }
        void Init_Book_TextBoxesWithKnownProperties() // Initialize the Book's fields with his parameters
        {
            BookSP.Visibility = Visibility.Visible;
            Init_MustPropertiesItem();

            var item = _editItem as Book;
            foreach (var author in item.Authors) AuthorsTB1.Text += $"{author.Replace(" ", "-")}\t";
            PublisherTB1.Text = item.Publisher;
            BookGenresCB.SelectedItem = item.Genres.FirstOrDefault();
            CountryCB.SelectedItem = item.ISBN.Country;
            RevisionTB1.Text = item.Revision.ToString();
            SerialNumberTB1.Text = item.ISBN.SerialNumber.ToString();
            SynopsisTB1.Text = item.Synopsis;
        }
        void Init_BookGenresAndCountriesComboBox() // Initializing a ComboBox by Book's Genres and Countries
        {
            BookGenresCB.ItemsSource = Book.BookGenres;
            CountryCB.ItemsSource = ISBN.Countries.Keys;
        }
        void Init_ItemTypeComboBox(Type itemType = null) // Initialize comboBox by LibraryItem derived classed 
        {
            List<Type> listOfLibraryItems;
            if (itemType == null)
            {
                listOfLibraryItems = (
                    from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                    from type in domainAssembly.GetTypes()
                    where typeof(LibraryItem).IsAssignableFrom(type)
                    where type != typeof(LibraryItem)
                    select type).ToList();
            }
            else listOfLibraryItems = new List<Type>() { itemType };

            ItemTypeCB.ItemsSource = listOfLibraryItems.Select(t => t.Name).ToList();
            if (itemType != null) ItemTypeCB.SelectedItem = itemType.Name;
            ItemTypeCB.SelectionChanged += ItemTypeCB_SelectionChanged;
        }
        void ItemTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e) // When ever the user changes the Type (Book / Journal etc..) their specific fields comes to board
        {
            var selection = ItemTypeCB.SelectedItem.ToString();
            if (selection == typeof(Book).Name)
            {
                BookSP.Visibility = Visibility.Visible;
                JournalSP.Visibility = Visibility.Collapsed;
            }
            else if (selection == typeof(Journal).Name)
            {
                JournalSP.Visibility = Visibility.Visible;
                BookSP.Visibility = Visibility.Collapsed;
            }
        }
        async void AddItemBtn_Click(object sender, RoutedEventArgs e) // Adding the item to the library / editing the selected item
        {
            if (!AddItemMustCondition()) return;

            string content = ItemTypeCB.SelectionBoxItem.ToString();

            if (content == typeof(Book).Name) CreateBook();
            else if (content == typeof(Journal).Name) CreateJournal();

            await new MessageDialog("Item was added successfully!", "Item added").ShowAsync();

            if (_editItem != null)
                Frame.Navigate(typeof(SearchItemPage));
            else Frame.Navigate(typeof(AdminPage));
        }
        void CreateJournal() // Creates the Journal, Function used in "AddItemBtn_Click"
        {
            var tempJournal = new Journal(_title, _publicationDate, _price, _numOfPages, _sale);

            if (JournalGenresCB.SelectedIndex >= 0) tempJournal.Genres.Add(JournalGenresCB.SelectionBoxItem.ToString());

            if (Enum.TryParse(FrequencyCB?.SelectedItem?.ToString(), out JournalFrequency freq)) 
                tempJournal.Frequency = freq;

            if (ContributorsTB1.Text != string.Empty) tempJournal.Contributors.AddRange(ContributorsTB1.Text.Split(_delimiterChars));
            
            if (EditorsTB1.Text != string.Empty) tempJournal.Editors.AddRange(EditorsTB1.Text.Split(_delimiterChars));
            
            if(_editItem != null) _repo.Update(_editItem, tempJournal); // In case there is an updateItem we want to update
            else _repo.Add(tempJournal); // In case we are adding a new item
        }
        void CreateBook() // Creates the Book, Function used in "AddItemBtn_Click"
        {
            if (int.TryParse(SerialNumberTB1.Text, out int serialNumber)) _serialNumber = serialNumber;
            if (int.TryParse(RevisionTB1.Text, out int revision)) _revision = revision;

            if(CountryCB.SelectedIndex >= 0) _country = CountryCB.SelectionBoxItem.ToString();

            var tempBook = new Book(_title, _publicationDate, _price, _numOfPages, _serialNumber, _country, _sale) { Synopsis = SynopsisTB1.Text, Revision = _revision };

            if (BookGenresCB.SelectedIndex >= 0) tempBook.Genres.Add(BookGenresCB.SelectionBoxItem.ToString());
            else tempBook.Genres.Add("Other");

            if (PublisherTB1.Text != string.Empty)
            {
                if (!ISBN.Publishers.ContainsKey(PublisherTB1.Text))
                    ISBN.Publishers.Add(PublisherTB1.Text, new Random().Next());
                tempBook.Publisher = PublisherTB1.Text;
            }
            else tempBook.Publisher = "Anonymus";

            if (AuthorsTB1.Text != string.Empty) tempBook.Authors.AddRange(AuthorsTB1.Text.Split(_delimiterChars));
            else tempBook.Authors.Add("Anonymus");

            if (_editItem != null) _repo.Update(_editItem, tempBook); // In case there is an updateItem we want to update
            else _repo.Add(tempBook); // In case we are adding a new item
        }
        bool AddItemMustCondition() // A condition any item has before being created, if return false --> the 'must' parameters aren't filled as well
        {
            if (TitleTB1.Text != string.Empty && double.TryParse(PriceTB1.Text, out double price) && ItemTypeCB.SelectedIndex >= 0
                            && int.TryParse(NumOfPagesTB1.Text, out int pages) && _publicationDate != DateTime.MinValue)
            {
                _title = TitleTB1.Text;
                _price = price;
                _numOfPages = pages;
                if (int.TryParse(SaleTB1.Text, out int percentageSale)) _sale = percentageSale;
                return true;
            }
            return false;
        }
        void ClearDateButton_Click(object sender, RoutedEventArgs e) // clear date selection
        {
            PublicationDate.SelectedDate = null;
            _publicationDate = DateTime.MinValue;
        }
        void PublicationDatePicker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            if (PublicationDate.SelectedDate != null)
                _publicationDate = new DateTime(args.NewDate.Value.Year, args.NewDate.Value.Month, args.NewDate.Value.Day);
        }
        void EditItemBtn_Click(object sender, RoutedEventArgs e) => AddItemBtn_Click(this, e);
        void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) // // Return Button depends where the admin came fromv
        {
            if (_editItem != null) Frame.Navigate(typeof(SearchItemPage));
            else Frame.Navigate(typeof(AdminPage));
        }
        void Exit_Click(object sender, TappedRoutedEventArgs e) => Application.Current.Exit();
    }
}