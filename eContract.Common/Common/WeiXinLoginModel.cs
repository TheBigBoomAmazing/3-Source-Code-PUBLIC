using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eContract.Common.Common
{
    [Serializable]
    public class GetOpenIdResponseModel
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
    }

    [Serializable]
    public class GetUserNameResponseModel
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; }
    }

    [Serializable]
    public class GetWeiXinInfoResponseModel
    {
        [JsonProperty(PropertyName = "subscribe")]
        public string Subscribe { get; set; }

        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }

        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }

        [JsonProperty(PropertyName = "sex")]
        public string Sex { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "headimgurl")]
        public string Headimgurl { get; set; }

        [JsonProperty(PropertyName = "subscribe_time")]
        public string Subscribe_time { get; set; }

        [JsonProperty(PropertyName = "unionid")]
        public string Unionid { get; set; }

        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }

        [JsonProperty(PropertyName = "groupid")]
        public string Groupid { get; set; }

        [JsonProperty(PropertyName = "tagid_list")]
        public string Tagid_list { get; set; }
    }

    [Serializable]
    public class GetWechatTokenResponseModel
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "expiresIn")]
        public string ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; }

    }

    [Serializable]
    public class SendWechatMassageModel
    {
        [JsonProperty(PropertyName = "serviceAccount")]
        public string ServiceAccount { get; set; }

        [JsonProperty(PropertyName = "msgType")]
        public string MsgType { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

    }
}
