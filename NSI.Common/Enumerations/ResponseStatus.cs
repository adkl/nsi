using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Common.Enumerations
{
    /// <summary>
    /// Used to indicate whether request was successfull or not 
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// Request could not be processed, an error has accured
        /// </summary>
        Failed = 0,
        /// <summary>
        /// Request was successfully processed
        /// </summary>
        Succeeded = 1
    }
}
