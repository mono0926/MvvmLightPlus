using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mono.MvvmLightPlus.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;
using Mono.MvvmLightPlus.Extensions;
using Mono.MvvmLightPlus.View;

namespace Mono.MvvmLightPlus.ViewModel
{
    public abstract class ViewModelBasePlus : ViewModelBase
    {

        public ViewModelBasePlus()
        {
            SettingsCommands = new List<SettingsCommand>();
        }

        public INavigationService NavigationService { get; set; }

        private ICommand _backCommand;

        public virtual ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand(() =>
                    {
                        if (CanGoBack)
                        {
                            NavigationService.GoBack();
                        }
                    }));
            }
        }

        public virtual bool CanGoBack { get { return NavigationService.CanGoBack; } }

        public abstract string PageTitle { get; }

        public virtual void OnNavigatedTo(NavigationEventArgs e) { }

        public CoreDispatcher Dispatcher { get; set; }

        public List<SettingsCommand> SettingsCommands { get; set; }

        protected void AddSettingPage<T>(string id, string label, ViewBase<T> element, int width = 480, EventHandler<object> onClosed = null) where T : ViewModelBasePlus
        {
            var height = Window.Current.Bounds.Height;
            element.Width = width;
            element.Height = height;
            var popup = new Popup
            {
                Child = element,
                Width = width,
                Height = height,
                IsLightDismissEnabled = true,
            };
            popup.SetValue(Canvas.LeftProperty, Window.Current.Bounds.Width - width);
            popup.SetValue(Canvas.TopProperty, 0);
            if (onClosed != null)
            {
                popup.Closed += onClosed;
            }
            SettingsCommands.Add(new SettingsCommand(id, label, command =>
            {
                popup.IsOpen = true;
                element.RaiseOnNavigatedTo(null);

            }));
        }

        public virtual void OnSettingCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommands.ForEach(x => args.Request.ApplicationCommands.Add(x));
        }
    }
}
