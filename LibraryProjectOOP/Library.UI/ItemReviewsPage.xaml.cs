using Library.Model;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Library.UI
{
    /// <summary>
    /// ItemReviewsPage allows the user to see all back Reviews <see cref="Review"/>
    /// </summary>
    public sealed partial class ItemReviewsPage : Page
    {
        // Fields
        /// <summary>
        /// The selected Item
        /// </summary>
        LibraryItem _selectedItem;

        // Ctor
        public ItemReviewsPage() => this.InitializeComponent();
        
        protected override void OnNavigatedTo(NavigationEventArgs e) // Navigation method initializer (must to pass parameters)
        {
            _selectedItem = (LibraryItem)e.Parameter;
            Init_PageUIelements();
            base.OnNavigatedTo(e);
        }
        void Init_PageUIelements() // Initialize the page's UI
        {
            PaneNV.Header = LogInPage.WebSurfer.Name;
            ListView listView = new ListView() { FontSize = 25, ItemsSource = _selectedItem.Reviews };
            listView.SelectionChanged += ListView_SelectionChanged;
            AllReviewsLV.Children.Add(listView);
        }
        async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) // Opens the selected review
        {
            ListView l1 = sender as ListView;
            await new MessageDialog(l1.SelectedItem.ToString()).ShowAsync();
        }
        void AddReviewBtn_Click(object sender, RoutedEventArgs e) // Add a review to the current LibraryItem
        {
            if (AddReviewSP.Visibility == Visibility.Collapsed)
                AddReviewSP.Visibility = Visibility.Visible;
            else
                AddReviewSP.Visibility = Visibility.Collapsed;
        }
        void SubmitReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTB.Text;
            int rate = (int)Rating.Value;
            string comment = CommentTB.Text;
            _selectedItem.AddReview(new Review(name,rate,DateTime.Now,comment));
            Frame.Navigate(typeof(ItemReviewsPage), _selectedItem);
        }
        void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => Frame.Navigate(typeof(ShowMoreDetailsPage), _selectedItem);
        void Exit_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) => Application.Current.Exit();
    }
}