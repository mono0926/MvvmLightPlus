using GalaSoft.MvvmLight.Command;
using Mono.App.AmazonSalesRank.Model;
using Mono.MvvmLightPlus.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.ApplicationSettings;
using Mono.MvvmLightPlus.Extensions;
using Mono.Api.AmazonClient.AmazonProxyService;

namespace Mono.App.AmazonSalesRank.ViewModel
{
    [Export, Shared]
    public class IndexTypeSettingViewModel : ViewModelBasePlus
    {
        public ISettingService SettingService { get; set; }

        public override string PageTitle { get { return "Item Type"; } }

        [ImportingConstructor]
        public IndexTypeSettingViewModel(ISettingService settingService)
        {
            this.SettingService = settingService;
            _selectedCountryType = SettingService.CountryType;
            IndexTypeSettings = new ObservableCollection<IndexTypeSetting>();
            CountryTypes = new ObservableCollection<CountryType>();
        }

        public override async void  OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (CountryTypes.Count != 0)
            {
                return;
            }
            (await SettingService.AvailableCountries()).ForEach(x =>
                {
                    CountryTypes.Add(x);
                });
            RaisePropertyChanged(() => SelectedCountryType);
            Reload(_selectedCountryType);
        }

        ICommand _backCommand;
        public override ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand(SettingsPane.Show));
            }
        }

        public override bool CanGoBack { get { return true; } }

        private ObservableCollection<IndexTypeSetting> _indexTypeSettings;

        public ObservableCollection<IndexTypeSetting> IndexTypeSettings
        {
            get { return _indexTypeSettings; }
            set { Set(() => IndexTypeSettings, ref _indexTypeSettings, value); }
        }

        public ObservableCollection<CountryType> CountryTypes { get; private set; }

        private CountryType _selectedCountryType;

        public CountryType SelectedCountryType
        {
            get { return _selectedCountryType; }
            set
            {
                Set(() => SelectedCountryType, ref _selectedCountryType, value);
                Reload(_selectedCountryType);
            }
        }

        private async Task Reload(CountryType countryType)
        {
            SettingService.CountryType = countryType;
            await SettingService.RefleshAvailableTypes();
            await SettingService.RestoreIndexTypeSettingFromFileOrFull();
            IndexTypeSettings.Clear();
            SettingService.IndexTypeSettings.ForEach(x => IndexTypeSettings.Add(x));
        }

    }
}
