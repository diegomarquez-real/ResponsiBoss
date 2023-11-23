using GeoJSON.Net.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Models.Create
{
    public class CreateAppointmentModel
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public Feature? Location { get; set; }
    }
}