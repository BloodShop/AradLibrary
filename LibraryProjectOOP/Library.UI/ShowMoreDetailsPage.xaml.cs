using Library.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Library.UI
{
    /// <summary>
    /// ShowMoreDetailsPage about the user's item selection
    /// </summary>
    public sealed partial class ShowMoreDetailsPage : Page
    {
        // Fields
        /// <summary>
        /// The selected Item we would like to share more information
        /// </summary>
        LibraryItem _selectedItem;

        // Ctor
        public ShowMoreDetailsPage() => this.InitializeComponent();
        protected override void OnNavigatedTo(NavigationEventArgs e) // Navigation method initializer (must to pass parameters)
        {
            PaneNV.Header = LogInPage.WebSurfer.Name;
            _selectedItem = (LibraryItem)e.Parameter;
            ItemImage.Source = new BitmapImage(_selectedItem.ImageSource);
            
            if (_selectedItem is Book) Init_PageUIelementsForBook();
            else if (_selectedItem is Journal) Init_PageUIelementsForJournal();
            base.OnNavigatedTo(e);
        }
        void Init_PageUIelementsForJournal() // Initialize the page espacially for JOURNAL
        {
            JournalDetailsSP.Visibility = Visibility.Visible;
            Journal selection = (Journal)_selectedItem;

            ContributorsTB.Text += "\t" + String.Join("\t", selection.Contributors);
            JournalGenresTB.Text += "\t" + String.Join("\t", selection.Genres);
            EditorsTB.Text += "\t" + String.Join("\t", selection.Editors);
            FrequencyTB.Text += "\t" + selection.Frequency.ToString();
        }
        void Init_PageUIelementsForBook() // Initialize the page espacially for BOOK
        {
            BookDetailsSP.Visibility = Visibility.Visible;
            Book selection = (Book)_selectedItem;

            AuthorsTB.Text += "\t" + String.Join("\t",selection.Authors);
            GenresTB.Text += "\t" + String.Join("\t", selection.Genres);
            PublisherTB.Text += "\t" + selection.Publisher;
            RevisionTB.Text += "\t" + selection.Revision;
            SynopsisTB.Text += "\n" + selection.Synopsis;
            ISBNTB.Text += "\t" + selection.ISBN;
        }
        void ShowReviews_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(ItemReviewsPage), _selectedItem); // Represent All item's reviews
        void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => Frame.Navigate(typeof(ItemReportPage));
        void Exit_Click(object sender, TappedRoutedEventArgs e) => Application.Current.Exit();
    }
}