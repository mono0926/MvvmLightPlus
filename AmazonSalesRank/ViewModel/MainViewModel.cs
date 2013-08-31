using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mono.MvvmLightPlus.ViewModel;
using Mono.App.AmazonSalesRank.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Mono.App.AmazonSalesRank.ViewModel;
using Mono.App.AmazonSalesRank.Model;
using Mono.App.AmazonSalesRank.View;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Core;
using Mono.MvvmLightPlus.Extensions;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Mono.Api.AmazonClient.Model;
using Mono.Api.AmazonClient.AmazonProxyService;

namespace Mono.App.AmazonSalesRank.ViewModel
{
    [Export, Shared]
    public class MainViewModel : ViewModelBasePlus
    {
        public ISettingService SettingService { get; set; }
        // TODO
        public override string PageTitle { get { return "Amazon Sales Rank"; } }
        [Import]
        public RankingViewModel RankingViewModel { get; set; }
        [Import]
        public ItemViewModel ItemViewModel { get; set; }
        public ObservableCollection<Ranking> Rankings { get; set; }

        private DateTime _lastUpdated;
        private readonly DispatcherTimer _timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(300) };
        private CountryType _currentCountryType;

        [ImportingConstructor]
        public MainViewModel(ISettingService settingService)
        {
            this.SettingService = settingService;
            _currentCountryType = SettingService.CountryType;
            //Rankings = (new RankingSource()).Rankings;

            Rankings = new ObservableCollection<Ranking>();
            var settingView = new IndexTypeSettingView();
            AddSettingPage("indexTypeSetting", "Select Item Types", settingView, width: 540, onClosed: async(_, __) =>
                {
                    var vm = settingView.ViewModel;
                    SettingService.IndexTypeSettings = vm.IndexTypeSettings;
                    bool changed = await SettingService.SaveIndexTypeSettings();
                    changed = changed || _currentCountryType != SettingService.CountryType;
                    _currentCountryType = SettingService.CountryType;
                    Load(changed);
                });
            var lisencePolicyView = new LicensePolicyView();
            AddSettingPage("LicensePolicy", "License Policy", lisencePolicyView, width: 480, onClosed: (_, __) =>
            {           
            });
            _timer.Tick += (_, __) =>
            {
                var rootFrame = Window.Current.Content as Frame;
                if (rootFrame.Content.GetType() != typeof(MainView))
                {
                    Load();
                }
            };
            _timer.Start();


            Load(true);
        }

        private async void Load(bool force = false)
        {
            if (!force)
            {
                var diff = DateTime.Now - _lastUpdated;
                if (diff.CompareTo(TimeSpan.FromMinutes(30)) < 0)
                {
                    return;
                }
            }

            Rankings.Clear();
            //Parallel.ForEach(SettingService.IndexTypeSettings
            //    .Take(Consts.MaxItemNum)
            //    .Where(x => x.On)
            //    .Select(x => x.IndexType), async type =>
            //    {
            //        await Task.Delay(3000);
            //        var r = await SettingService.LoadRanking(type);
            //        Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            //        {
            //            foreach (var i in r.Items)
            //            {
            //                i.Ranking = r;
            //            }
            //            lock (Rankings)
            //            {
            //            }
            //            Rankings.Add(r);
            //        });
            //    });
            await SettingService.RefleshAvailableTypes();
            await SettingService.RestoreIndexTypeSettingFromFileOrFull();
            var xx = SettingService.IndexTypeSettings
                        .Take(Consts.MaxItemNum)
                        .Where(x => x.On)
                        .Select(x => x.IndexType);
            foreach (var type in xx)
            {
                try
                {
                    var r = await SettingService.LoadRanking(type);
                    if (r == null)
                    {
                        continue;
                    }
                    foreach (var i in r.Items)
                    {
                        i.Ranking = r;
                    }
                    Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                    {
                        Rankings.Add(r);
                    });
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            _lastUpdated = DateTime.Now;
        }

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
        private ICommand _genreCommand;

        public ICommand GenreCommand
        {
            get
            {
                return _genreCommand ?? (_genreCommand = new RelayCommand<Ranking>(ranking =>
                {
                    RankingViewModel.Ranking = ranking;
                    NavigationService.Navigate(typeof(RankingView));
                }));
            }
        }

        private ICommand _refreshCommand;

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand(() =>
                    {
                        Load();
                    }));
            }
        }

    }
}
