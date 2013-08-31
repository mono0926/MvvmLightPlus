using GalaSoft.MvvmLight;
using Mono.MvvmLightPlus.View;
using Mono.App.AmazonSalesRank.ViewModel;
using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ApplicationSettings;
using Windows.System;
using Windows.UI.Popups;
using Mono.App.AmazonSalesRank.View;

namespace Mono.App.AmazonSalesRank.View
{
    public sealed partial class MainView : MainViewBase
    {
        public MainView()
        {
            this.InitializeComponent();
        }
    }
    public class MainViewBase : ViewBase<MainViewModel> { }
}
