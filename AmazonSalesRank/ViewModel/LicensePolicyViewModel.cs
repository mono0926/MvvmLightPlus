using GalaSoft.MvvmLight.Command;
using Mono.MvvmLightPlus.ViewModel;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.ApplicationSettings;

namespace Mono.App.AmazonSalesRank.ViewModel
{
    [Export]
    public class LicensePolicyViewModel : ViewModelBasePlus
    {
        public override string PageTitle
        {
            get { return "License Policy"; }
        }
        ICommand _backCommand;
        public override ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand(() =>
                {
                    SettingsPane.Show();
                }));
            }
        }

        public override bool CanGoBack { get { return true; } }

        public string SubTitle
        {
            get
            {
                return "Privacy Policy";
            }
        }

        public string Message
        {
            get
            {
                return @"Amazon Sales Rank does not collect or publish any personal information. 
Our moderators will refuse any published creations containing phone numbers, addresses or other personal information.
If you would like to report any violations of this policy, please contact us at mono0926@gmail.com";
            }
        }
    }
}
