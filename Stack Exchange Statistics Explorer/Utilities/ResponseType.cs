using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.Utilities
{
    /// <summary>
    /// Indicates what format the API response should be in.
    /// </summary>
    public enum ResponseType
    {
        /// <summary>
        /// The API response is returned in JavaScript Object Notation format.
        /// </summary>
        Json,
        /// <summary>
        /// The API response is returned in Extensible Markup Languge format.
        /// </summary>
        Xml,
        /// <summary>
        /// The API response is returned in a Tab Separated Values format with line breaks on each row.
        /// </summary>
        Tsv,
        /// <summary>
        /// The API response is returned in a Pipe Separated Values format with line breaks on each row.
        /// </summary>
        Psv,
        /// <summary>
        /// The API response is returned in a Comma Separated Values format with line breaks on each row.
        /// </summary>
        Csv,
    }
}
