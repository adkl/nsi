﻿using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.Languages
{
    public class GetLanguageRequest: BaseRequest
    {
        /// <summary>
        /// Language ID for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
