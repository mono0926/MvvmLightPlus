using Mono.Api.AmazonClient.AmazonProxyService;
using Mono.Framework.Common.IO;
using System;

namespace AmazonSalesRank.Test
{
    class FakeSettingStore : ISettingStore
    {
        public T Load<T>(string key, bool roaming = false)
        {
            throw new NotImplementedException();
        }

        public T Load<T>(string key, out bool found, bool roaming = false)
        {
            throw new NotImplementedException();
        }

        public T LoadAsEnum<T>(string key, out bool found, bool roaming = false)
        {
            if (this.countryTypeSaved)
            {
                found = true;
                return (T)(object)SavedCountryType;
            }
            found = false;
            return default(T);
        }

        public void Save(string key, object value, bool roaming = false)
        {
            throw new NotImplementedException();
        }

        public void SaveAsEnum(string key, object value, bool roaming = false)
        {

        }

        public CountryType SavedCountryType { get; set; }
        public bool countryTypeSaved { get; set; }
    }
}
