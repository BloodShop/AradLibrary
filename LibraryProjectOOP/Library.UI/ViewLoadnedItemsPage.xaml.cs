using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DAL;
using Library.Model;
using People.Model;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Library.UI
{
    /// <summary>
    /// ViewLoadnedItemsPage allow the worker see who loaned an item and Retrieve the item to the stock
    /// </summary>
    public sealed partial class ViewLoadnedItemsPage : Page
    {
        // Fields
        /// <summary>
        /// Our library Repository which we are looking at as ILoanable <see cref="ILoanable{T}"/>
        /// </summary>
        ILoanable<LibraryItem> _repoLibrary = new LibraryRepository();
        /// <summary>
        /// Our users Repository which we are looking at as IHumanable <see cref="IHumanable{T}"/>
        /// </summary>
        IHumanable<Person> _repoPeople = new PeopleRepository();
        /// <summary>
        /// Get loaned items of all time
        /// </summary>
        Dictionary<LibraryItem, Person> _dicLoaned;

        // Ctor
        public ViewLoadnedItemsPage()
        {
            this.InitializeComponent();
            PaneNV.Header = LogInPage.WebSurfer.Name;
            _dicLoaned = _repoLibrary.GetLoaned();
        }

        async void ListViewLoanedItems_SelectionChanged(object sender, SelectionChangedEventArgs e) // As manager or Employee - can retrieve an item to stock
        {
            LibraryItem temp = _repoLibrary.RetrieveItem(ListViewLoanedItems.SelectedIndex);
            await new MessageDialog($"{temp.Title} is back in stock!").ShowAsync();
            Frame.Navigate(typeof(ViewLoadnedItemsPage));
        }
        void UsersInfo_Click(object sender, TappedRoutedEventArgs e) // All Users - accessible ONLY by manager
        {
            if(LogInPage.WebSurfer is Manager)
            {
                PeopleSP.Visibility = Visibility.Visible;
                PeopleCB.ItemsSource = _repoPeople.Get();
            }
        }
        async void DeleteBtn_Click(object sender, RoutedEventArgs e) // Deletes the account and also retrieve the items the person has on loan
        {
            IUICommand resultDialog = await VerifyDelete();
            if (resultDialog.Label == "Yes")
            {
                List<Person> availablePeople = _repoPeople.Get().ToList();
                var person = _repoPeople.Delete(availablePeople[PeopleCB.SelectedIndex]);
                await RetrieveItems(person);
                if (person == LogInPage.WebSurfer)
                    Frame.Navigate(typeof(LogInPage));
                await new MessageDialog($"{person.Name} was removed out of the system").ShowAsync();
            }
        }
        async void RetrieveAllBtn_Click(object sender, RoutedEventArgs e) // Retrieve all items from the selected person
        {
            List<Person> availablePeople = _repoPeople.Get().ToList();
            var person = availablePeople[PeopleCB.SelectedIndex];
            await RetrieveItems(person);
        }
        async Task<IUICommand> VerifyDelete() // MessageDialog Yes/No verification before deleteing an account
        {
            MessageDialog dialog = new MessageDialog("Are you sure?", "Delete Acount");
            dialog.Commands.Add(new UICommand("Yes"));
            dialog.Commands.Add(new UICommand("No", delegate (IUICommand command) { return; }));
            return await dialog.ShowAsync();
        }
        async Task RetrieveItems(Person person) // Refresh the page after retrieving all items to stock
        {
            if (person.LoanedItems.Count > 0)
                foreach (var item in person.LoanedItems.Keys)
                {
                    _repoLibrary.RetrieveItem(item);
                    await new MessageDialog($"{item} was added back to stock").ShowAsync();
                }
            Frame.Navigate(typeof(ViewLoadnedItemsPage));
        }
        void People_SelectionChanged(object sender, SelectionChangedEventArgs e) // If the administrator selected a person, buttons will be visible
        {
            if (PeopleCB.SelectedIndex >= 0) PeopleButtonsSP.Visibility = Visibility.Visible;
            else PeopleButtonsSP.Visibility = Visibility.Collapsed;
        }
        void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => Frame.Navigate(typeof(AdminPage));
        void Exit_Click(object sender, TappedRoutedEventArgs e) => Application.Current.Exit();
    }
}