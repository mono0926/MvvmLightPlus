using Mono.App.AmazonSalesRank.ViewModel;
using Mono.MvvmLightPlus.View;
using System;
using System.Collections.Generic;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Mono.App.AmazonSalesRank.View
{
    public sealed partial class IndexTypeSettingView : IndexTypeSettingViewBase
    {
        public IndexTypeSettingView()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.InitialStoryboard.Begin();
            base.OnNavigatedTo(e);
        }
    }
    public class IndexTypeSettingViewBase : ViewBase<IndexTypeSettingViewModel> { }
}
