using Mono.Api.AmazonClient.AmazonProxyService;
using Mono.Api.AmazonClient.Model;
using Mono.App.AmazonSalesRank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Mono.Api.AmazonClient.DataService;
using Mono.Framework.Common.IO;
using System.Composition;

namespace Mono.App.AmazonSalesRank
{
    [Export(typeof(ISettingService)), Shared]
    public class SettingService : ISettingService
    {
        private readonly IAmazonDataService _amazonDataService;
        private readonly ISettingStore _settingStore;
        private readonly ISerializer _serializer;
        private const string CountryTypeKey = "countryType";

        public IEnumerable<SearchIndexType> AvailableTypes { get; set; }

        public IEnumerable<IndexTypeSetting> IndexTypeSettings { get; set; }

        public CountryType CountryType
        {
            get { return _amazonDataService.CountryType; }
            set
            {
                _amazonDataService.CountryType = value;
                _settingStore.SaveAsEnum(CountryTypeKey, CountryType, roaming: true);
            }
        }

        public SettingService(IAmazonDataService amazonDataService, ISettingStore settingStore, ISerializer serializer)
        {
            _amazonDataService = amazonDataService;
            _settingStore = settingStore;
            _serializer = serializer;
            bool found;
            var c = _settingStore.LoadAsEnum<CountryType>(CountryTypeKey, out found, roaming: true);
            if (found)
            {
                _amazonDataService.CountryType = c;
            }
            else
            {
                var locale = new Windows.Globalization.GeographicRegion().CodeTwoLetter;
                var type = CountryType.US;
                switch (locale)
                {
                    case "JP":
                        type = CountryType.Japan;
                        break;
                    case "CA":
                        type = CountryType.Canada;
                        break;
                    case "FR":
                        type = CountryType.France;
                        break;
                    case "DE":
                        type = CountryType.Germany;
                        break;
                    case "UK":
                        type = CountryType.UK;
                        break;
                }
                _amazonDataService.CountryType = type;
            }
        }

        public SettingService()
            : this(new AmazonDataService(Consts.DefautlCountryType, new SettingStore(), new Serializer()), new SettingStore(), new Serializer())
        {
        }

        public async Task<Ranking> LoadRanking(SearchIndexType type)
        {
            return await _amazonDataService.GetRanking(type);
        }

        /// <summary>
        /// Save type settings if changed.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>return true if type setting is changed, otherwise return false</returns>
        public async Task<bool> SaveIndexTypeSettings()
        {
            var elem = new XElement("TypeSettings");
            foreach (var s in IndexTypeSettings)
            {
                var e = new XElement(s.IndexType.ToString());
                e.SetAttributeValue("on", s.On);
                elem.Add(e);
            }
            var filename = string.Format("{0}.xml", CountryType.ToString());

            var old = await _serializer.LoadXml(filename, roaming: true);
            if (old != null)
            {
                var xElementEqulality = new XNodeEqualityComparer();
                if (xElementEqulality.Equals(old.Element("TypeSettings"), elem))
                {
                    return false;
                }
            }

            await _serializer.SaveXml(filename, elem, roaming: true);
            return true;
        }

        public async Task RefleshAvailableTypes()
        {
            AvailableTypes = await _amazonDataService.AvailableTypesAsync();
        }

        public async Task RestoreIndexTypeSettingFromFileOrFull()
        {
            var doc = await _serializer.LoadXml(string.Format("{0}.xml", CountryType.ToString()), roaming: true);
            if (doc == null)
            {
                IndexTypeSettings = AvailableTypes
                                        .Select(x => new IndexTypeSetting { IndexType = x, On = true });
                return;
            }

            var a = doc.Element("TypeSettings").Elements().Select(x => x.Name.LocalName).ToArray();

            var elem = doc.Element("TypeSettings");

            IndexTypeSettings = AvailableTypes
                                    .Select(x => new IndexTypeSetting
                                    {
                                        IndexType = x,
                                        On = GetOnOff(elem, x),
                                    });
        }

        private static bool GetOnOff(XElement elem, SearchIndexType x)
        {
            var e = elem.Element(x.ToString());
            if (e == null)
            {
                return true;
            }
            return Convert.ToBoolean(e.Attribute("on").Value);
        }


        public async Task<IEnumerable<CountryType>> AvailableCountries()
        {
            return await _amazonDataService.AvailableCountriesAsync();
        }
    }
}
