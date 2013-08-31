using Mono.Api.AmazonClient.AmazonProxyService;
using Mono.Api.AmazonClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace Mono.App.AmazonSalesRank.SampleData
{
    public sealed class RankingSource
    {
        private string imagePath = "ms-appx:///SampleData/Images/win8.jpg";
        public IEnumerable<Ranking> Rankings { get; set; }
        private Ranking _ranking;
        public IEnumerable<Item> RankingItems { get { return _ranking.Items; } }

        public RankingSource()
        {
            _ranking = new Ranking(null)
                {
                    IndexType = SearchIndexType.Books,
                    Items = new[]
                    {
                        new Item
                        {
                            Attributes = new ItemAttributes { Title = "輩は猫である" , Author = "夏目漱石" , FormattedPrice = "350円" },
                            LargeImageURL = imagePath,
                            
                        },
                        new Item
                        {
                            Attributes = new ItemAttributes { Title = "輩は猫である" , Author = "夏目漱石" , FormattedPrice = "350円" },
                            LargeImageURL = imagePath,
                        },
                        new Item
                        {
                            Attributes = new ItemAttributes { Title = "輩は猫である" , Author = "夏目漱石" , FormattedPrice = "350円" },
                            LargeImageURL = imagePath,
                        },
                    },
                };
            Rankings = new[]
            {
                _ranking,
                new Ranking(null)
                {
                    IndexType = SearchIndexType.Apparel,
                    Items = new[]
                    {
                        new Item
                        {
                            Attributes = new ItemAttributes { Title = "輩は猫である" , Author = "夏目漱石" , FormattedPrice = "350円" },
                            LargeImageURL = imagePath,
                        },
                        new Item
                        {
                            Attributes = new ItemAttributes { Title = "輩は猫である" , Author = "夏目漱石" , FormattedPrice = "350円" },
                            LargeImageURL = imagePath,
                        },
                        new Item
                        {
                            Attributes = new ItemAttributes { Title = "輩は猫である" , Author = "夏目漱石" , FormattedPrice = "350円" },
                            LargeImageURL = imagePath,
                        },
                    },
                },
                                new Ranking(null)
                {
                    IndexType = SearchIndexType.Electronics,
                    Items = new[]
                    {
                        new Item
                        {
                            Attributes = new ItemAttributes { Title = "輩は猫である" , Author = "夏目漱石" , FormattedPrice = "350円" },
                            LargeImageURL = imagePath,
                        },
                        new Item
                        {
                            Attributes = new ItemAttributes { Title = "輩は猫である" , Author = "夏目漱石" , FormattedPrice = "350円" },
                            LargeImageURL = imagePath,
                        },
                        new Item
                        {
                            Attributes = new ItemAttributes { Title = "輩は猫である" , Author = "夏目漱石" , FormattedPrice = "350円" },
                            LargeImageURL = imagePath,
                        },
                    },
                }
            };
            foreach (var r in Rankings)
            {
                foreach (var i in r.Items)
                {
                    i.Ranking = r;
                }
            }
        }
    }
}
