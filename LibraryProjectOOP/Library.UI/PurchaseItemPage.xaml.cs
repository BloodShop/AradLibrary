using Library.DAL;
using Library.Model;
using People.Model;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Library.UI
{
    /// <summary>
    /// PurchaseItemPage after the user selected what item he wants to purchase
    /// </summary>
    public sealed partial class PurchaseItemPage : Page
    {
        // Fields
        /// <summary>
        /// Our Repository which we are looking at ILoanable <see cref="ILoanable{T}"/>
        /// </summary>
        ILoanable<LibraryItem> _repo = new LibraryRepository();
        /// <summary>
        /// The item that the user selected
        /// </summary>
        LibraryItem _item;
        /// <summary>
        /// The person who is Purchasing [ Manager(admin) , Guest , Customer ]
        /// </summary>
        readonly Person _person = LogInPage.WebSurfer;

        // Ctor
        public PurchaseItemPage() => this.InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _item = e.Parameter as LibraryItem;
            if (LogInPage.WebSurfer is Guest) BorrowBtn.Content = "Purchase";
            PaneNV.Header = LogInPage.WebSurfer.Name;
            Init_PurchaseSP();
            base.OnNavigatedTo(e);
        }
        void Init_PurchaseSP() // Initialize the purchase stackPanel on XAML UI
        {
            PurchaseSP.Visibility = Visibility.Visible;
            CheckPrioprityPurchase();
            if (!(_person is Guest)) AddressTB.Text += "\n" + _person?.Address;
        }  
        void CheckPrioprityPurchase()
        {
            // just an example 
            if (_person is Manager) PriceTB.Text += $"{_item.Price:C}";
            else if (_person is Customer) PriceTB.Text += $"{_item.Price:C}";
            else if (_person is Guest)
            {
                GuestAddressSP.Visibility = Visibility.Visible;
                PriceTB.Text += $"{_item:NoSale}";
            }
        }
        async void BorrowBtn_Click(object sender, RoutedEventArgs e) // Submit button to complete the purchase. Add the book to the person 'bookStorage'
        {
            if (_person is Guest && AddressCondition())
                await new MessageDialog($"{_item.Title} - will arrive {_person.Address} in 1 hour").ShowAsync();
            else
            {
                DateTime expDate = DateTime.Now.AddDays(new Random().Next(1, 10));
                _repo.AddLoan(_item, LogInPage.WebSurfer, expDate);
                await new MessageDialog($"{_item.Title} - will arrive {_person.Address} in 1 hour :)\nYou Shall return the {_item.GetType().Name} until {expDate:f}", "Payment Completed").ShowAsync();
            }
            Frame.Navigate(typeof(MainPage));
        }
        bool AddressCondition() // Checks address validation
        {
            string city = CityBox.Text;
            string street = StreetBox.Text;
            if (city != string.Empty && street != string.Empty && int.TryParse(HouseNumberBox.Text, out int num)
                            && char.TryParse(EntryHouseBox.Text, out char c) && int.TryParse(PostalCodeBox.Text, out int zip))
            {
                ((Guest)_person).SetAddress(city, street, num, c, zip);
                return true;
            }
            return false;
        }
        void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => Frame.Navigate(typeof(ItemReportPage));
        void Exit_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) => Application.Current.Exit();
    }
}