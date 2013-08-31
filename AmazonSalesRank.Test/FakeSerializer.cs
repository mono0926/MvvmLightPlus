using Mono.Framework.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmazonSalesRank.Test
{
    class FakeSerializer : ISerializer
    {
        public Task DeleteFile(string foldername, string filename, bool roaming = false)
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> LoadXml(string filename, bool roaming = false)
        {
            return Task.Factory.StartNew <XDocument>(() =>
                {
                    return null;
                });
        }

        public Task<string> ReadFile(string foldername, string filename, bool roaming = false)
        {
            throw new NotImplementedException();
        }

        public Task SaveXml(string filename, System.Xml.Linq.XElement elem, bool roaming = false)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(string foldername, string filename, string contents, bool roaming = false)
        {
            throw new NotImplementedException();
        }
    }
}
