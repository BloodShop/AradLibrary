using Library.DAL;
using People.Model;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Library.UI
{
    /// <summary>
    /// RegistrationPage A customer to the System.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {
        /// <summary>
        ///  Our Repository which we are looking at as IHumanable <see cref="IHumanable{T}"/>
        /// </summary>
        IHumanable<Person> _repo = new PeopleRepository();

        // Ctor
        public RegistrationPage() => this.InitializeComponent();
        bool RegistrationCondition(out int houseNum, out char houseEntry, out int zip)
        {
            houseEntry = 'a';
            zip = 0;
            return int.TryParse(HouseNumberTB1.Text, out houseNum) && char.TryParse(HouseEntryTB1.Text, out houseEntry) 
                && int.TryParse(PostalCodeTB1.Text, out zip) && PasswordTB1.Password.Length > 0;
        }
        private async void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if(RegistrationCondition(out int houseNum, out char houseEntry, out int zip))
            {
                Address address = new Address(CityTB1.Text, StreetTB1.Text, houseNum, houseEntry, zip);
                _repo.Add(new Customer(NameTB1.Text, PasswordTB1.Password, address));
                await new MessageDialog("Customer was added successfully!").ShowAsync();
                Frame.Navigate(typeof(LogInPage));
            }
        }
        void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => Frame.GoBack();
        void Exit_Click(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) => Application.Current.Exit();
    }
}