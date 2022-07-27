using Library.DAL;
using People.Model;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Library.UI
{
    /// <summary>
    /// AdminPage let him decide what operation he wants
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        // Fields
        /// <summary>
        /// Our Repository which we are looking at IHumanable <see cref="IHumanable{T}"/>
        /// </summary>
        IHumanable<Person> _repo = new PeopleRepository();

        // Ctor
        public AdminPage()
        {
            this.InitializeComponent();
            PaneNV.Header = LogInPage.WebSurfer.GetType().Name;
        }

        void AddItemBtn_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(CreateNewItemPage));
        void FindItemBtn_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(SearchItemPage));
        void ViewLoanedItemsBtn_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(ViewLoadnedItemsPage));
        async void SetSaleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LogInPage.WebSurfer is Manager)
            {
                _repo.SetSale();
                await new MessageDialog("SALE TIME!!", "Sale Season Ended").ShowAsync();
            }
        }
        async void EndSaleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LogInPage.WebSurfer is Manager)
            {
                _repo.EndSale();
                await new MessageDialog("End of sale :(!!", "Sale Season Began").ShowAsync();
            }
        }
        void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => Frame.Navigate(typeof(LogInPage));
        async void SubmitNewPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LogInPage.WebSurfer.Password.Equals(OldPasswordPS.Password) && NewPasswordPS.Password != string.Empty)
            {
                LogInPage.WebSurfer.SetNewPassword(NewPasswordPS.Password);
                await new MessageDialog("Password is set up", "Copmpleted").ShowAsync();
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
    }
}