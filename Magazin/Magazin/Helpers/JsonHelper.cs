using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Magazin.Helpers
{
    public class JsonNetResult : ActionResult
    {
        public const string DATE_TIME_FORMATE = "dd.MM.yyyy HH:mm:ss";
        public const string DATE_FORMATE = "dd.MM.yyyy";
        public const string MONTH_FORMATE = "MMMM yyyy";
        public const string YEAR_FORMATE = "yyyy";

        public const string BASE_COLLECTION_SUFFIX = "_bo_collection_";
        public const string EASY_COLLECTION_SUFFIX = "_easy_collection_";

        private readonly object _jObject;
        private readonly IContractResolver _contractResolver;
        private readonly JsonConverter[] _converters;

        public JsonNetResult(object jObject)
        {
            _jObject = jObject;
        }

        public JsonNetResult(object jObject, IContractResolver contractResolver)
        {
            _jObject = jObject;

            _contractResolver = contractResolver;
        }

        public JsonNetResult(object jObject, params JsonConverter[] converters)
        {
            _jObject = jObject;

            _converters = converters;
        }

        public JsonNetResult(object jObject, IContractResolver contractResolver, params JsonConverter[] converters)
        {
            _jObject = jObject;

            _contractResolver = contractResolver;

            _converters = converters;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";


            var converters = (_converters?.ToList() ?? new List<JsonConverter>());
            
            var responseString = JsonConvert.SerializeObject(_jObject, Formatting.None,
                new JsonSerializerSettings
                {
                    DateFormatString = DATE_TIME_FORMATE,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Converters = converters,
                    ContractResolver = _contractResolver,
                    StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                });

            response.Write(responseString);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(_jObject, Formatting.None,
                   new JsonSerializerSettings
                   {
                       DateFormatString = DATE_TIME_FORMATE,
                       ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                       Converters = _converters?.ToList(),
                       ContractResolver = _contractResolver,
                       StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                   });
        }
    }
}