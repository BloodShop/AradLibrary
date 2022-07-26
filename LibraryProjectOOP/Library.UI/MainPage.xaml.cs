using Library.DAL;
using Library.Model;
using System;
using System.Diagnostics;
using System.Text.Json;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Library.UI
{
    /// <summary>
    /// MainPage congrats the users with the first page
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Fields
        /// <summary>
        /// Our Repository which we are looking at IRepository <see cref="IRepository{T}"/>
        /// </summary>
        readonly IRepository<LibraryItem> _repo = new LibraryRepository();
        readonly string fileName = "exposure.json";
       
        // Ctor
        public MainPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            LoadDataBaseJson();
        }
        void Continue_Click(object sender, RoutedEventArgs e) => this.Frame.Navigate(typeof(LogInPage), _repo);
        void AboutIcon_Click(object sender, RoutedEventArgs e)
        {
            if (AboutUsSP.Visibility is Visibility.Visible) 
                AboutUsSP.Visibility = Visibility.Collapsed;
            else AboutUsSP.Visibility = Visibility.Visible;
        }
        async void LoadDataBaseJson()
        {
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile sampleFile = await storageFolder.GetFileAsync(fileName);
                string jsonFromFile = await FileIO.ReadTextAsync(sampleFile);
                DataBase db = JsonSerializer.Deserialize<DataBase>(jsonFromFile);
            }
            catch (NotSupportedException ex) when (ex.Message.Equals("Deserialization of types without a parameterless constructor, a singular parameterized constructor, or a parameterized constructor annotated with 'JsonConstructorAttribute' is not supported. Type 'Library.DAL.DataBase'. Path: $ | LineNumber: 0 | BytePositionInLine: 1.")) { }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
        void Exit_Click(object sender, TappedRoutedEventArgs e)
        {
            Save_Click(this, e);
            Application.Current.Exit();
        }
        async void Save_Click(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                var appFolder = ApplicationData.Current.LocalFolder;
                var file = await appFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                string jsonString = JsonSerializer.Serialize<DataBase>(DataBase.Instance, options);
                await FileIO.WriteTextAsync(file, jsonString);

                Debug.WriteLine(String.Format($"File is located at {file.Path}"));
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
    }
}