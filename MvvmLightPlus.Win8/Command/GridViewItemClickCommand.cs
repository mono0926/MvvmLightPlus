﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mono.MvvmLightPlus.Command
{
    public class GridViewItemClickCommand
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(GridViewItemClickCommand), new PropertyMetadata(null, CommandPropertyChanged));


        public static void SetCommand(DependencyObject attached, ICommand value)
        {
            attached.SetValue(CommandProperty, value);
        }


        public static ICommand GetCommand(DependencyObject attached)
        {
            return (ICommand)attached.GetValue(CommandProperty);
        }


        private static void CommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Attach click handler
            (d as ListViewBase).ItemClick += gridView_ItemClick;
        } 


        private static void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Get GridView
            var gridView = (sender as ListViewBase);


            // Get command
            ICommand command = GetCommand(gridView);


            // Execute command
            command.Execute(e.ClickedItem);
        }
    }
}
