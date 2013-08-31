using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mono.Api.AmazonClient.AmazonProxyService;
using Mono.Api.AmazonClient.DataService;
using Mono.App.AmazonSalesRank;

namespace AmazonSalesRank.Test
{
    [TestClass]
    public class SettingServiceTest
    {
        [TestMethod]
        public void CountryType_Default()
        {
            var fakeAmazonDataService = new FakeAmazonDataService();
            fakeAmazonDataService.CountryType = CountryType.Japan;
            var fakeSettingStore = new FakeSettingStore();
            var settingService = new SettingService(fakeAmazonDataService, fakeSettingStore, null);
            Assert.AreEqual(CountryType.Japan, settingService.CountryType);
            Assert.IsNull(settingService.AvailableTypes);
            Assert.IsNull(settingService.IndexTypeSettings);
        }

        [TestMethod]
        public void CountryType_Saved()
        {
            var fakeAmazonDataService = new FakeAmazonDataService();
            var fakeSettingStore = new FakeSettingStore();
            fakeSettingStore.SavedCountryType = CountryType.Germany;
            fakeSettingStore.countryTypeSaved = true;

            var settingService = new SettingService(fakeAmazonDataService, fakeSettingStore, null);
            Assert.AreEqual(CountryType.Germany, settingService.CountryType);
        }


        /*
        [TestMethod]
        public void AvailableCountries()
        {
            var fakeSettingStore = new FakeSettingStore();
            var settingService = new SettingService(null, fakeSettingStore, null);
            var availableContries = settingService.AvailableCountries;
            var countryTypes = availableContries as CountryType[] ?? availableContries.ToArray();
            Assert.AreEqual(6, countryTypes.Count());
            Assert.IsTrue(countryTypes.Any(x => x == CountryType.Japan));
            Assert.IsTrue(countryTypes.Any(x => x == CountryType.US));
            Assert.IsTrue(countryTypes.Any(x => x == CountryType.Canada));
            Assert.IsTrue(countryTypes.Any(x => x == CountryType.France));
            Assert.IsTrue(countryTypes.Any(x => x == CountryType.Germany));
            Assert.IsTrue(countryTypes.Any(x => x == CountryType.UK));
        }
         */

        [TestMethod]
        public async Task RefleshAvailableTypes()
        {
            var fakeAmazonDataService = new FakeAmazonDataService();
            fakeAmazonDataService.FakeAvailableTypes = new SearchIndexType[]
                {
                    SearchIndexType.Apparel, 
                    SearchIndexType.Appliances, 
                };
            var fakeSettingStore = new FakeSettingStore();
            var fakeSerializer = new FakeSerializer();
            var settingService = new SettingService(fakeAmazonDataService, fakeSettingStore, fakeSerializer);
            await settingService.RefleshAvailableTypes();
            Assert.IsNotNull(settingService.AvailableTypes);
            Assert.IsTrue(settingService.AvailableTypes.Any(x => x == SearchIndexType.Apparel));
            Assert.IsTrue(settingService.AvailableTypes.Any(x => x == SearchIndexType.Appliances));
        }


        [TestMethod]
        public async Task RestoreIndexTypeSettingFromFileOrFull()
        {
            var fakeAmazonDataService = new FakeAmazonDataService();
            fakeAmazonDataService.FakeAvailableTypes = new SearchIndexType[]
                {
                    SearchIndexType.Apparel, 
                    SearchIndexType.Appliances, 
                };
            var fakeSettingStore = new FakeSettingStore();
            var fakeSerializer = new FakeSerializer();
            var settingService = new SettingService(fakeAmazonDataService, fakeSettingStore, fakeSerializer);

            await settingService.RefleshAvailableTypes();
            await settingService.RestoreIndexTypeSettingFromFileOrFull();

            Assert.IsNotNull(settingService.IndexTypeSettings);
            Assert.IsTrue(settingService.IndexTypeSettings.Any(x => x.IndexType == SearchIndexType.Apparel));
            Assert.IsTrue(settingService.IndexTypeSettings.Any(x => x.IndexType == SearchIndexType.Appliances));
            Assert.AreEqual(2, settingService.IndexTypeSettings.Count());
            Assert.AreEqual(2, settingService.IndexTypeSettings.Count(x => x.On));     
        }

        [TestMethod]
        public async Task RestoreIndexTypeSettingFromFileOrFull_saved()
        {
            var fakeAmazonDataService = new FakeAmazonDataService();
            fakeAmazonDataService.FakeAvailableTypes = new SearchIndexType[]
                {
                    SearchIndexType.Apparel, 
                    SearchIndexType.Appliances, 
                };
            var fakeSettingStore = new FakeSettingStore();
            var fakeSerializer = new FakeSerializer();
            var settingService = new SettingService(fakeAmazonDataService, fakeSettingStore, fakeSerializer);

            await settingService.RefleshAvailableTypes();
            await settingService.RestoreIndexTypeSettingFromFileOrFull();

            Assert.IsNotNull(settingService.IndexTypeSettings);
            Assert.IsTrue(settingService.IndexTypeSettings.Any(x => x.IndexType == SearchIndexType.Apparel));
            Assert.IsTrue(settingService.IndexTypeSettings.Any(x => x.IndexType == SearchIndexType.Appliances));
            Assert.AreEqual(2, settingService.IndexTypeSettings.Count());
            Assert.AreEqual(2, settingService.IndexTypeSettings.Count(x => x.On));
        }
    }
}
