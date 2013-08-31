using Mono.MvvmLightPlus.Navigation;
using Mono.MvvmLightPlus.ViewModel;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Mono.MvvmLightPlus.Extensions;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.ViewManagement;

namespace Mono.MvvmLightPlus.View
{
    [WebHostHidden]
    public abstract class ViewBase<T> : Page where T : ViewModelBasePlus
    {
        private T _viewModel;
        private List<Control> _layoutAwareControls;

        [Import]
        public T ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                _viewModel.Dispatcher = this.Dispatcher;
                _viewModel.NavigationService = NavigationService.GetInstance();
                this.DataContext = _viewModel;
            }
        }

        protected ViewBase()
        {
            if (DesignModeEnabled) return;
            Bootstrapper.Instance.CompositionHost.SatisfyImports(this);

            SettingsPane.GetForCurrentView().CommandsRequested += ViewBase_CommandsRequested;



            this.Loaded += (sender, e) =>
                {
                    this.StartLayoutUpdates(sender, e);

                    //var bounds = Window.Current.Bounds;
                    //if (ActualHeight == bounds.Height && ActualWidth == bounds.Width)
                    //{
                    //    // Listen to the window directly so focus isn't required
                    //    Dispatcher.AcceleratorKeyActivated += CoreDispatcher_AcceleratorKeyActivated;
                    //    PointerPressed += CoreWindow_PointerPressed;
                    //}
                };
        }

        void ViewBase_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            ViewModel.OnSettingCommandsRequested(sender, args);
        }


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            TidyHandlers();
            base.OnNavigatingFrom(e);
        }
        private void TidyHandlers()
        {
            SettingsPane.GetForCurrentView().CommandsRequested -= ViewBase_CommandsRequested;
        }

        private void StartLayoutUpdates(object sender, RoutedEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;
            if (_layoutAwareControls == null)
            {
                SizeChanged += WindowSizeChanged;
                _layoutAwareControls = new List<Control>();
            }
            _layoutAwareControls.Add(control);

            // Set the initial visual state of the control
            string visualState = ApplicationView.Value.ToString();
            VisualStateManager.GoToState(control, visualState, false);
       
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this._layoutAwareControls != null)
            {
                string visualState = ApplicationView.Value.ToString();
                foreach (var layoutAwareControl in this._layoutAwareControls)
                {
                    VisualStateManager.GoToState(layoutAwareControl, visualState, false);
                }
            }
        }

        protected bool DesignModeEnabled { get { return DesignMode.DesignModeEnabled; } }

        internal void RaiseOnNavigatedTo(NavigationEventArgs e)
        {
            OnNavigatedTo(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e != null)
            {
                base.OnNavigatedTo(e);
            }
            ViewModel.OnNavigatedTo(e);
        }
    }
}
