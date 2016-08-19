using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.Utilities.Extensions
{
    public static class ResponseTypeExtensions
    {
        /// <summary>
        /// Converts a string to an acceptable <see cref="ResponseType"/>.
        /// </summary>
        /// <param name="responseType">The string to convert.</param>
        /// <returns>A valid <see cref="ResponseType"/> value.</returns>
        /// <remarks>
        /// Defaults to <see cref="ResponseType.Json"/> if the string cannot be converted.
        /// </remarks>
        public static ResponseType FromString(string responseType)
        {
            responseType = responseType?.ToLower();

            switch (responseType)
            {
                case "json":
                    return ResponseType.Json;
                case "xml":
                    return ResponseType.Xml;
                case "tsv":
                    return ResponseType.Tsv;
                case "psv":
                    return ResponseType.Psv;
                case "csv":
                    return ResponseType.Csv;
            }

            return ResponseType.Json;
        }
    }
}
