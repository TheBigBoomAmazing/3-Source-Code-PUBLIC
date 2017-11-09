using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eContract.Common.Common
{
    [Serializable]
    public class GetWechatAccessToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Access_token { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public string Expires_in { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string Refresh_token { get; set; }

        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        [JsonProperty(PropertyName = "unionid")]
        public string Unionid { get; set; }
    }
}
