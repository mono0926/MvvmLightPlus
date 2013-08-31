using GalaSoft.MvvmLight.Command;
using Mono.Api.AmazonClient.Model;
using Mono.MvvmLightPlus.ViewModel;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Navigation;

namespace Mono.App.AmazonSalesRank.ViewModel
{
    [Export, Shared]
    public class ItemViewModel : ViewModelBasePlus
    {
        public override string PageTitle { get { return "Item Detail"; } }

        public Item Item { get; set; }

        public IEnumerable<Item> RankingItems { get { return Item.Ranking.AllItems; } }

        private ICommand _browserCommand;

        public ICommand BrowserCommand
        {
            get
            {
                return _browserCommand ?? (_browserCommand = new RelayCommand( async() =>
                {
                    var success = await Launcher.LaunchUriAsync(new Uri(Item.DetailPageURL));

                    if (success)
                    {
                        // 起動に成功した場合の処理。
                        // ブラウザは起動するがアプリも裏で動く
                    }
                    else
                    {

                    }
                    }));
            }
        }
        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            Item.Ranking.LoadAllItems();

        }

    }
}
