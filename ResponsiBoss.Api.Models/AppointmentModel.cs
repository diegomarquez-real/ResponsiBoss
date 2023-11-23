using GeoJSON.Net.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Models
{
    public class AppointmentModel
    {
        public Guid AppointmentId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public Feature? Feature { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}