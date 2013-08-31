using Mono.Api.AmazonClient.AmazonProxyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.App.AmazonSalesRank.Model
{
    public class IndexTypeSetting
    {
        public SearchIndexType IndexType { get; set; }
        public bool On { get; set; }
    }
}
