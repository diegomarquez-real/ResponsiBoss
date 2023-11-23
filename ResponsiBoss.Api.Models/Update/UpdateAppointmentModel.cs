using GeoJSON.Net.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Models.Update
{
    public class UpdateAppointmentModel
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public Feature? Location { get; set; }
    }
}