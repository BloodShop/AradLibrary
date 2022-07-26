using Library.Model;
using People.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Library.UI
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LibraryItemTemplate { get; set; }
        public DataTemplate PersonTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is LibraryItem)
                return LibraryItemTemplate;
            if (item is Person)
                return PersonTemplate;

            return base.SelectTemplateCore(item);
        }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) => SelectTemplateCore(item);
    }
}
