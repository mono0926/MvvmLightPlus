using Mono.Api.AmazonClient.AmazonProxyService;
using Mono.Api.AmazonClient.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonSalesRank.Test
{
    class FakeAmazonDataService : IAmazonDataService
    {
        public Task<IEnumerable<SearchIndexType>> AvailableTypesAsync()
        {
            return Task.Factory.StartNew(() => FakeAvailableTypes);
        }

        public Task<IEnumerable<CountryType>> AvailableCountriesAsync()
        {
            return null;
            //throw Task.Factory.StartNew(() => FakeAvailableCountries);
        }

        public Task<Mono.Api.AmazonClient.Model.Ranking> GetRanking(Mono.Api.AmazonClient.AmazonProxyService.SearchIndexType indexType)
        {
            throw new NotImplementedException();
        }

        public Task<Mono.Api.AmazonClient.Model.ItemsResult> GetMoreItems(Mono.Api.AmazonClient.AmazonProxyService.SearchIndexType indexType, int itemPage)
        {
            throw new NotImplementedException();
        }

        public Mono.Api.AmazonClient.AmazonProxyService.CountryType CountryType { get; set; }

        public IEnumerable<SearchIndexType> FakeAvailableTypes { get; set; }
        public IEnumerable<CountryType> FakeAvailableCountries { get; set; }


    }
}
