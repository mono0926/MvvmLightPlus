using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using System.Composition;
using System.Composition.Convention;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;
using Mono.App.AmazonSalesRank.ViewModel;
using Mono.App.AmazonSalesRank.View;
using Mono.MvvmLightPlus;

namespace Mono.App.AmazonSalesRank
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                Bootstrapper.Configure(typeof(App));

                if (!rootFrame.Navigate(typeof(MainView), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            Window.Current.Activate();
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
