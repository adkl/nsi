﻿using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Document
{
   public class DocumentTypeDomain : BaseDomain
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Encoding { get; set; }
        public string Version { get; set; }
        public string Extension
        {
            get
            {
                return "." + Name;
            }
        }
    }
}
