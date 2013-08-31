using GalaSoft.MvvmLight.Command;
using Mono.MvvmLightPlus.ViewModel;
using Mono.App.AmazonSalesRank.View;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Mono.Api.AmazonClient.Model;

namespace Mono.App.AmazonSalesRank.ViewModel
{
    [Export, Shared]
    public class RankingViewModel : ViewModelBasePlus
    {
        [Import]
        public ItemViewModel ItemViewModel { get; set; }
        public override string PageTitle { get { return this.Ranking.Title; } }
        public Ranking Ranking {get;set;}

        public ObservableCollection<Item> RankingItems { get { return Ranking.AllItemsWithoutFirst; } }

        public Item FirstItem { get { return Ranking.Items.First(); } }

        private RelayCommand<Item> _itemCommand;

        public RelayCommand<Item> ItemCommand
        {
            get
            {
                return _itemCommand ?? (_itemCommand = new RelayCommand<Item>(item =>
                {
                    ItemViewModel.Item = item;
                    NavigationService.Navigate(typeof(ItemView));
                }));
            }
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            Ranking.LoadAllItems();

        }
    }
}
