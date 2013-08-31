using System.Collections.Generic;
using System.Threading.Tasks;
using Mono.Api.AmazonClient.AmazonProxyService;
using Mono.Api.AmazonClient.Model;
using Mono.App.AmazonSalesRank.Model;

namespace Mono.App.AmazonSalesRank
{
    public interface ISettingService
    {
        /// <summary>
        /// Amazon country setting
        /// </summary>
        CountryType CountryType { get; set; }
        /// <summary>
        /// available countries
        /// </summary>
        Task<IEnumerable<CountryType>> AvailableCountries();
        /// <summary>
        /// available item types(genre)
        /// </summary>
        IEnumerable<SearchIndexType> AvailableTypes { get; }
        /// <summary>
        /// available item types (specified by setting menu).
        /// </summary>
        IEnumerable<IndexTypeSetting> IndexTypeSettings { get; set; }

        Task RefleshAvailableTypes();

        Task RestoreIndexTypeSettingFromFileOrFull();

        Task<Ranking> LoadRanking(SearchIndexType type);

        Task<bool> SaveIndexTypeSettings();

    }
}