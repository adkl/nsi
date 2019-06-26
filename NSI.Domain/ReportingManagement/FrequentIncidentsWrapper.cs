using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.ReportingManagement
{
    public class FrequentIncidentsWrapper
    {
        public int IncidentTypeId { get; set; }
        public string IncidentTypeName { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public int NumberOfIncidents { get; set; }
    }
}
