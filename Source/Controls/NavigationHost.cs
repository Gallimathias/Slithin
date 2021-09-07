using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Slithin.Controls
{
    public class NavigationHost : TemplatedControl
    {
        public static StyledProperty<ObservableCollection<Control>> PagesProperty = AvaloniaProperty.Register<NavigationHost, ObservableCollection<Control>>("Pages");
        public static StyledProperty<int> SelectedIndexProperty = AvaloniaProperty.Register<NavigationHost, int>("SelectedIndex");

        private static NavigationHost _host;

        public NavigationHost()
        {
            Pages = new();
        }

        public ObservableCollection<Control> Pages
        {
            get { return GetValue(PagesProperty); }
            set { SetValue(PagesProperty, value); }
        }

        public int SelectedIndex
        {
            get { return GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static bool GetIsHost(NavigationHost target)
        {
            return _host == target;
        }

        public static void Navigate(Control page)
        {
            _host.Pages.Add(page);
        }

        public static void SetIsHost(NavigationHost target, bool value)
        {
            _host = target;
        }
    }
}
