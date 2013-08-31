﻿using Mono.MvvmLightPlus.View;
using Mono.App.AmazonSalesRank.ViewModel;
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

namespace Mono.App.AmazonSalesRank.View
{
    public sealed partial class RankingView : RankingViewBase
    {
        public RankingView()
        {
            this.InitializeComponent();
            //GC.Collect();
        }
    }
    public class RankingViewBase : ViewBase<RankingViewModel> { }
}
