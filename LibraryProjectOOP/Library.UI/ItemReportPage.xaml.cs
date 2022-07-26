using Library.Model;
using People.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Library.UI
{
    /// <summary>
    /// ItemReportPage shows the user a report and all the details of the chosen item.
    /// </summary>
    public sealed partial class ItemReportPage : Page
    {
        /// <summary>
        /// The selected item
        /// </summary>
        LibraryItem _selectedItem;
        public ItemReportPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) // Navigation method initializer (must to pass parameters)
        {
            if (e.Parameter is LibraryItem)
            {
                _selectedItem = (LibraryItem)e.Parameter;
                Init_PageUIelements();
            }
            base.OnNavigatedTo(e);
        }
        void Init_PageUIelements() // Initialize page's UI
        {
            TitleTB.Text = $"Title: \t{_selectedItem.Title}";
            if(LogInPage.WebSurfer is Guest) PriceTB.Text = $"Price: \t{_selectedItem:NoSale}";
            else PriceTB.Text = $"Price: \t{_selectedItem:$$}";
            PublishDateTB.Text = $"Publish Date: \t{_selectedItem.PublicationDate:D}";
            PaneNV.Header = LogInPage.WebSurfer.Name;
        }
        void PaymentBtn_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(PurchaseItemPage), _selectedItem);
        void GuidCheckBox_Unchecked(object sender, RoutedEventArgs e) => GuidTB.Text = "Guid: ";
        void GuidCheckBox_Checked(object sender, RoutedEventArgs e) => GuidTB.Text += _selectedItem.Id;
        void ShowMoreDetailsBtn_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(ShowMoreDetailsPage), _selectedItem);
        void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => Frame.Navigate(typeof(SearchItemPage));
        void Exit_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) => Application.Current.Exit();
    }
}