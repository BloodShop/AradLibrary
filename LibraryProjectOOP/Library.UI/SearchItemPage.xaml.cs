using Library.DAL;
using Library.Model;
using People.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Library.UI
{
    /// <summary>
    /// SearchItemPage Which allows the user select an item and purchase it. Or the Admin can Edit / Delete the item
    /// </summary>
    public sealed partial class SearchItemPage : Page
    {
        // Fields
        /// <summary>
        /// The exicting derived types from LibraryItem 
        /// </summary>
        Type[] _typesOfLibraryItems;
        /// <summary>
        /// Our Repository which we are looking at IRepository <see cref="IRepository{T}"/>
        /// </summary>
        IRepository<LibraryItem> _repo = new LibraryRepository();
        /// <summary>
        /// Specific IComparer to sort the results
        /// </summary>
        IComparer<LibraryItem> _chosenComparer;
        /// <summary>
        /// List of the result Items
        /// </summary>
        List<LibraryItem> _resultItems;

        // Ctor
        public SearchItemPage()
        {
            this.InitializeComponent();
            Init_PageUIelements();
        }

        void Init_PageUIelements() // Initializing the two comoboBoxes which have sorting criterias and libraryItems types (Book, Journal etc..)
        {
            PaneNV.Header = LogInPage.WebSurfer.Name;
            SearchComaparer.ItemsSource = DataBase.SortLibraryItem; // Init comboBox by all sorting Criterias
            Init_ItemTypeComboBox();
        }
        void Init_ItemTypeComboBox() // Initialize comboBox by LibraryItem derived classes
        {
            _typesOfLibraryItems = (
                from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in domainAssembly.GetTypes()
                where typeof(LibraryItem).IsAssignableFrom(type)
                where type != typeof(LibraryItem)
                select type).ToArray();

            ItemTypeCB.ItemsSource = _typesOfLibraryItems.Select(t => t.Name).ToList();
        }
        void Init_ListViewResultItems(IComparer<LibraryItem> comparer = null, string title = null, Guid guid = default(Guid), ISBN isbn = null) // Initialize ListView Results by all sorting IComparers
        {
            ResultLV.ItemsSource = null;
            _resultItems = new List<LibraryItem>();

            if (isbn != null)
                try { _resultItems.Add(((LibraryRepository)_repo)[isbn]); }
                catch (SearchException ex) { CreateErrorMassage(ex.Message); }

            else if (guid != default(Guid))
                try { _resultItems.Add(((LibraryRepository)_repo)[guid]); }
                catch (SearchException ex) { CreateErrorMassage(ex.Message); }

            else _resultItems = CreateResultsBySelectedCriterias(comparer, title, _resultItems);

            ResultLV.ItemsSource = _resultItems;
        }
        void ItemTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e) // If type ComboBox Selection being changed
        {
            if (ItemTypeCB.SelectedItem?.ToString() == typeof(Book).Name) ISBNSP.Visibility = Visibility.Visible;
            else ISBNSP.Visibility = Visibility.Collapsed;
            ResultLV.ItemsSource = null;
        }
        /// <summary>
        /// Function to create result of the excisting items - by the specific criterias
        /// </summary>
        /// <param name="comparer">The sorting comparer which you want to sort the result. Set to null if not chosen criteria</param>
        /// <param name="title">The item's title you are searching for the item's name. Set to null if not chosen criteria</param>
        /// <param name="guid">The item's unique guid. Set to default if not chosen criteria. <see cref="Guid"/></param>
        List<LibraryItem> CreateResultsBySelectedCriterias(IComparer<LibraryItem> comparer, string title, List<LibraryItem> allItems) // Returns the List<LibraryItems> to "Init_ComboBoxItems"
        {
            Type typeSelected = null;
            if (ItemTypeCB.SelectedIndex >= 0)
                typeSelected = Type.GetType($"{typeof(LibraryItem).Namespace}.{ItemTypeCB.SelectedItem}, {typeof(LibraryItem).Assembly}"); // Format GetType
            if (typeSelected != null)
            {
                if (typeSelected.Name == typeof(Book).Name)
                    allItems = ((LibraryRepository)_repo).FindAllByType(typeof(Book));
                else if (typeSelected.Name == typeof(Journal).Name)
                    allItems = ((LibraryRepository)_repo).FindAllByType(typeof(Journal));
            }
            else allItems = ((LibraryRepository)_repo).FindAllByType(typeof(LibraryItem));

            if (comparer != null) allItems.Sort(comparer);

            if (title != null)
                try { allItems = ((LibraryRepository)_repo)[allItems, title]; }
                catch (SearchException ex) { CreateErrorMassage(ex.Message); }

            return allItems;
        }
        void SortingComaparerCB_SelectionChanged(object sender, SelectionChangedEventArgs e) // If sorting ComboBox Selection being changed
        {
            int index = SearchComaparer.SelectedIndex;
            if (index >= 0) _chosenComparer = DataBase.SortLibraryItem[index];
            ResultLV.ItemsSource = null;
        }
        async void ResultLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdminSP.Visibility = Visibility.Collapsed;
            if (ResultLV.SelectedIndex >= 0)
            {
                DataPackage dp = new DataPackage(); // Copy To clipboard the guid of the selected item
                dp.SetText($"{{{_resultItems[ResultLV.SelectedIndex].Id}}}");
                Clipboard.SetContent(dp);
                await new MessageDialog($"{_resultItems[ResultLV.SelectedIndex].Title} - guid was copied", "Copy GUID").ShowAsync();

                ContinueBtn.Visibility = Visibility.Visible;
                if (LogInPage.WebSurfer is Manager)
                    AdminSP.Visibility = Visibility.Visible;
            }
            else
            {
                ContinueBtn.Visibility = Visibility.Collapsed;
                if (LogInPage.WebSurfer is Manager)
                    AdminSP.Visibility = Visibility.Collapsed;
            }
        }
        void SearchByNameTB_TextChanged(object sender, TextChangedEventArgs e) => ResultLV.ItemsSource = null; // If the search by name TextBox text is BEING changed then clear the result
        void SearchByGuidTB_TextChanged(object sender, TextChangedEventArgs e)// If the search by GUID TextBox text is bieng changed then other options aren't allowed
        {
            if (SearchByGUID.Text != string.Empty)
            {
                SearchByName.IsEnabled = false;
                ItemTypeCB.IsEnabled = false;
                SearchComaparer.IsEnabled = false;
                ISBNSP.Visibility = Visibility.Collapsed;
            }
            else
            {
                SearchByName.IsEnabled = true;
                ItemTypeCB.IsEnabled = true;
                SearchComaparer.IsEnabled = true;
                ISBNSP.Visibility = Visibility.Visible;
            }
        }
        void ClearBtn_Click(object sender, RoutedEventArgs e) // Clears all selections
        {
            ItemTypeCB.SelectedIndex = -1;
            SearchComaparer.SelectedIndex = -1;
            SearchByGUID.Text = string.Empty;
            SearchByName.Text = string.Empty;
            ErrorMessage.Visibility = Visibility.Collapsed;
            ResultLV.ItemsSource = null;
            ClearISBN();
        }
        void ClearISBN() // Clear ISBN fields
        {
            foreach (var tbi in ISBNsmallSP.Children)
                if (tbi is TextBox && ((TextBox)tbi).IsEnabled != false)
                    ((TextBox)tbi).Text = string.Empty;
        }
        void SearchBtn_Click(object sender, RoutedEventArgs e) // Search button press - search all the items by selected criterias
        {
            ErrorMessage.Visibility = Visibility.Collapsed;
            if (Guid.TryParse(SearchByGUID.Text, out Guid guid)) // If GUID Field is ok - Search bY guid
                Init_ListViewResultItems(guid: guid);

            else if (ISBNCondition(out int country, out int publisher, out int serNum, out int control)) // If ISBN FieldS ARE ok - Search bY ISBN
                SearchByIsbn(country, publisher, serNum);

            else
            {
                if (_chosenComparer != null && SearchByName.Text != string.Empty) Init_ListViewResultItems(_chosenComparer, SearchByName.Text);

                else if (_chosenComparer != null) Init_ListViewResultItems(_chosenComparer);

                else if (SearchByName.Text != string.Empty) Init_ListViewResultItems(title: SearchByName.Text);

                else Init_ListViewResultItems();
            }
        }
        void SearchByIsbn(int country, int publisher, int serNum) // Search by ISBN
        {
            try
            {
                ISBN isbn = new ISBN() { Publisher = ISBN.Publishers.First(x => x.Value == publisher).Key, Country = ISBN.Countries.First(x => x.Value == country).Key, SerialNumber = serNum };
                Init_ListViewResultItems(isbn: isbn);
            }
            catch (IsbnException exIsbn) { CreateErrorMassage(exIsbn.Message); }
            catch (Exception) { CreateErrorMassage("Not valid ISBN"); }
        }
        bool ISBNCondition(out int country, out int publisher, out int serNum, out int control)
        {
            country = 0;
            publisher = 0;
            serNum = 0;
            control = 0;
            if (int.TryParse(Prefix.Text, out int pref) && pref == 978 && int.TryParse(CountryNum.Text, out country) && country > 0 && country < 1000
                && int.TryParse(PublisherTB.Text, out publisher) && publisher < 1000 && publisher > 0
                 && int.TryParse(SerialNumberTB.Text, out serNum) && serNum >= 0 && serNum < 1000 && int.TryParse(ControlTB.Text, out control) && control % 10 < 10)
                return true;
            return false;
        }
        void ContinueBtn_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(ItemReportPage), _resultItems[ResultLV.SelectedIndex]); //  After selecting an item --> continues to the report      
        void UpdateItem_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(CreateNewItemPage), _resultItems[ResultLV.SelectedIndex]); // Updates the chosen Item
        async void DeleteItem_Click(object sender, RoutedEventArgs e) // Deletes the chosen Item
        {
            IUICommand resultDialog = await VerifyDelete();
            if (resultDialog.Label == "Yes")
            {
                var temp = _repo.Delete(_resultItems[ResultLV.SelectedIndex]);
                ResultLV.ItemsSource = null;
                await new MessageDialog($"{temp.Title} [{temp.GetType().Name}] has been deleted from the repository", "Delete message").ShowAsync();
            }
        }
        async Task<IUICommand> VerifyDelete() // MessageDialog Yes/No verification before deleteing an Item
        {
            MessageDialog dialog = new MessageDialog("Are you sure?", "Delete Item");
            dialog.Commands.Add(new UICommand("Yes"));
            dialog.Commands.Add(new UICommand("No", delegate (IUICommand command) { return; }));
            return await dialog.ShowAsync();
        }
        void CreateErrorMassage(string message) // Creates an Error message if an exeption was thrown
        {
            ErrorMessage.Visibility = Visibility.Visible;
            ErrorMessage.Text = message;
        }
        void Prefix_TextChanged(object sender, TextChangedEventArgs e) // Prefix ISBN checker if legal
        {
            if (Prefix.Text == ISBN.Prefix.ToString())
            {
                SearchByName.IsEnabled = false;
                ItemTypeCB.IsEnabled = false;
                SearchComaparer.IsEnabled = false;
                SearchByGUID.IsEnabled = false;
            }
            else
            {
                SearchByName.IsEnabled = true;
                ItemTypeCB.IsEnabled = true;
                SearchComaparer.IsEnabled = true;
                SearchByGUID.IsEnabled = true;
            }
        }
        async void SubmitNewPasswordBtn_Click(object sender, RoutedEventArgs e) // Set up new password for the current browser
        {
            if (LogInPage.WebSurfer.Password.Equals(OldPasswordPS.Password) && NewPasswordPS.Password != string.Empty)
            {
                LogInPage.WebSurfer.SetNewPassword(NewPasswordPS.Password);
                await new MessageDialog("Password is set up", "Password message").ShowAsync();
                ChangePasswordSP.Visibility = Visibility.Collapsed;
            }
        }
        void ChangePassword_Click(object sender, TappedRoutedEventArgs e)
        {
            if (!(LogInPage.WebSurfer is Guest) && ChangePasswordSP.Visibility == Visibility.Collapsed)
                ChangePasswordSP.Visibility = Visibility.Visible;
            else if (!(LogInPage.WebSurfer is Guest) && ChangePasswordSP.Visibility == Visibility.Visible)
                ChangePasswordSP.Visibility = Visibility.Collapsed;
        }
        void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => Frame.Navigate(typeof(LogInPage));
        void Exit_Click(object sender, TappedRoutedEventArgs e) => Application.Current.Exit();
    }
}