using Library.DAL;
using Library.Model;
using People.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Library.UI
{
    /// <summary>
    /// LogInPage decide as what user you want to browse the Library
    /// </summary>
    public sealed partial class LogInPage : Page
    {
        // Property
        /// <summary>
        /// The browser which connected to the Library
        /// </summary>
        public static Person WebSurfer { get; private set; }

        // Fields
        /// <summary>
        /// List of exicting people
        /// </summary>
        List<Person> _people;
        /// <summary>
        /// Our Repository which we are looking at IRepository <see cref="IRepository{T}"/>
        /// </summary>
        IRepository<LibraryItem> _repo = new LibraryRepository();

        IHumanable<Person> _repoPeople = new PeopleRepository();

        // Ctor
        public LogInPage()
        {
            this.InitializeComponent();
            _people = _repoPeople.Get().ToList();
        }
        void GuestTB_Click(object sender, RoutedEventArgs e) // Selected Guest
        {
            GuestBtn.IsEnabled = false;
            CustomerBtn.IsEnabled = true;
            AdminBtn.IsEnabled = true;
            LogInSP.Visibility = Visibility.Collapsed;
        }
        void CustomerTB_Click(object sender, RoutedEventArgs e) // Selected Customer
        {
            CustomerBtn.IsEnabled = false;
            GuestBtn.IsEnabled = true;
            AdminBtn.IsEnabled = true;
            LogInSP.Visibility = Visibility.Visible;
        }
        void AdminTB_Click(object sender, RoutedEventArgs e) // Selected Admin
        {
            AdminBtn.IsEnabled = false;
            GuestBtn.IsEnabled = true;
            CustomerBtn.IsEnabled = true;
            LogInSP.Visibility = Visibility.Visible;
        }
        async void ContinueBtn_Click(object sender, RoutedEventArgs e) // Continue browsing as selected 'Person'
        {
            if (!GuestBtn.IsEnabled)
            {
                WebSurfer = new Guest();
                Frame.Navigate(typeof(SearchItemPage), _repo);
                return;
            }
            else if (!CustomerBtn.IsEnabled)
            {
                foreach (var person in _people)
                    if (NameAndPasswordCheckValidation(person))
                    {
                        WebSurfer = person;
                        Frame.Navigate(typeof(SearchItemPage), _repo);
                        return;
                    }
            }
            else if (!AdminBtn.IsEnabled)
                foreach (var person in _people)
                    if (NameAndPasswordCheckValidation(person) && (person is Manager || person is Employee))
                    {
                        WebSurfer = person;
                        Frame.Navigate(typeof(AdminPage));
                        return;
                    }
            await new MessageDialog("UserName / Password are inCorrect", "Error LogIn").ShowAsync(); // if username or passwords are incorrect show error message
        }
        bool NameAndPasswordCheckValidation(Person person) => person.Password == PasswordBox.Password && person.Name == UserNameTB.Text;
        void Return_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => Frame.Navigate(typeof(MainPage));
        void Exit_Click(object sender, TappedRoutedEventArgs e) => Application.Current.Exit();
        void Registrate_Click(object sender, TappedRoutedEventArgs e) => Frame.Navigate(typeof(RegistrationPage));
    }
}