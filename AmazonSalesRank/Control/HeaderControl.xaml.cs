using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
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

namespace Mono.App.AmazonSalesRank.Control
{
    public sealed partial class HeaderControl : UserControl
    {
        public HeaderControl()
        {
            this.InitializeComponent();
        }



        public bool IsSnapped
        {
            get { return (bool)GetValue(IsSnappedProperty); }
            set { SetValue(IsSnappedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSnapped.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSnappedProperty =
            DependencyProperty.Register("IsSnapped", typeof(bool), typeof(HeaderControl), new PropertyMetadata(false, (sender, args) =>
                {
                    if (DesignMode.DesignModeEnabled)
                    {
                        return;
                    }
                    var that = sender as HeaderControl;
                    if (sender == null)
                    {
                        return;
                    }
                    if ((bool)args.NewValue)
                    {
                        that.backButton.Style = Application.Current.Resources["SnappedBackButtonStyle"] as Style;
                        that.pageTitle.Style = Application.Current.Resources["SnappedPageHeaderTextStyle"] as Style;
                    }
                    else 
                    {
                        that.backButton.Style = Application.Current.Resources["BackButtonStyle"] as Style;
                        that.pageTitle.Style = Application.Current.Resources["PageHeaderTextStyle"] as Style;
                    }
                }));


    }
}
