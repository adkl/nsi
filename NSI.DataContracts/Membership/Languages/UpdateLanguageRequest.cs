using NSI.DataContracts.Base;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.Languages
{
    public class UpdateLanguageRequest : BaseRequest
    {
        /// <summary>
        /// Language model
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
