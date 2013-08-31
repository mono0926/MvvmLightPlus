using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Mono.MvvmLightPlus.Navigation
{
    internal class NavigationService : INavigationService
    {
        private static INavigationService _service = new NavigationService();
        private NavigationService() { }

        public static INavigationService GetInstance() { return _service; }

        public Frame Frame { get { return (Frame)Window.Current.Content; } }

        public void Navigate(Type sourcePageType)
        {
           Frame.Navigate(sourcePageType);
        }
        public void Navigate(Type sourcePageType, object parameter)
        {
            Frame.Navigate(sourcePageType, parameter);
        }
        public void GoBack()
        {
            Frame.GoBack();
        }

        public bool CanGoBack { get { return Frame.CanGoBack; } }
    }
}
