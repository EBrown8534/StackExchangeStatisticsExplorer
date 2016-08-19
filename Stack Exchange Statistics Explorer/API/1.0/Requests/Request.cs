using Evbpc.Framework.Utilities.Serialization;
using Evbpc.Framework.Utilities.Serialization.DelimitedSerialization;
using Stack_Exchange_Statistics_Explorer.API._1._0.Models;
using Stack_Exchange_Statistics_Explorer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Stack_Exchange_Statistics_Explorer.API._1._0.Requests
{
    public abstract class Request<T>
        where T : IBaseModel
    {
        private ResponseType _responseType;

        protected Request(HttpContext context)
        {
            var responseTypeString = context.Request.QueryString["FileType"] ?? string.Empty;
            _responseType = Utilities.Extensions.ResponseTypeExtensions.FromString(responseTypeString);
        }

        public string ProcessRequest()
        {
            try
            {
                var response = DoWork();
                var responseWrapped = BuildWrapper(response);
                var responseString = "";

                switch (_responseType)
                {
                    // For the Delimited Serializer types, serialize ONLY the Items. The Delimited Serializer doesn't support serializing graph objects like JSON and XML do.
                    case ResponseType.Csv:
                        responseString = DelimitedSerializer.CsvSerializer.Serialize(responseWrapped.Items);
                        break;
                    case ResponseType.Psv:
                        responseString = DelimitedSerializer.PsvSerializer.Serialize(responseWrapped.Items);
                        break;
                    case ResponseType.Tsv:
                        responseString = DelimitedSerializer.TsvSerializer.Serialize(responseWrapped.Items);
                        break;
                    // For the JSON and XML types, serailize the entire response.
                    case ResponseType.Json:
                        JsonSerialization.Serialize(responseWrapped, ref responseString);
                        break;
                    case ResponseType.Xml:
                        XmlSerialization.Serialize(responseWrapped, ref responseString);
                        break;
                }

                return responseString;
            }
            catch (Exception e)
            {
                return FormatException(e);
            }
        }

        private static string FormatException(Exception exception)
        {
            if (exception is ArgumentException)
            {
                return SerializeWrapper(BuildErrorWrapper(new ArgumentExceptionResponse((ArgumentException)exception)));
            }

            return SerializeWrapper(BuildErrorWrapper(new ExceptionResponse(exception)));
        }

        private static string SerializeWrapper<TItem>(ApiResponseWrapper<TItem> wrapper)
            where TItem : IBaseModel
        {
            string response = "";
            JsonSerialization.Serialize(wrapper, ref response);
            return response;
        }

        private static ApiResponseWrapper<TItem> BuildErrorWrapper<TItem>(TItem item)
            where TItem : ExceptionResponse
        {
            var wrapper = new ApiResponseWrapper<TItem>();
            wrapper.Items.Add(item);
            wrapper.IsError = true;

            AddRateLimits(wrapper);

            return wrapper;
        }

        private static ApiResponseWrapper<TItem> BuildWrapper<TItem>(IEnumerable<TItem> items)
            where TItem : IBaseModel
        {
            var wrapper = new ApiResponseWrapper<TItem>();
            wrapper.Items.AddRange(items);
            wrapper.IsError = false;

            AddRateLimits(wrapper);

            return wrapper;
        }

        private static void AddRateLimits<TItem>(ApiResponseWrapper<TItem> wrapper)
            where TItem : IBaseModel
        {
            wrapper.QuotaMax = int.MaxValue;
            wrapper.QuotaRemaining = int.MaxValue;
            wrapper.Backoff = null;
        }

        protected abstract IEnumerable<T> DoWork();
    }
}
