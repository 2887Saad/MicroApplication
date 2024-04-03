using Micro.Web.Utility;
using System.Net.Mime;
using static Micro.Web.Utility.SD;

namespace Micro.Web.Models.Dto
{
    public class RequestDto
    {
        public ApiType ApiType = ApiType.GET;
        public string Url { get; set;}
        public object Data {  get; set;} 
        public string AccessToken { get; set;}
        /*public ContentType ContentType { get; set;} = ContentType.Json;*/
    }
}
