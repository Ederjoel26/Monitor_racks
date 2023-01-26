using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor_racks.Models
{
    public class clsRackModel
    {
        public double Temperatura { get; set; }
        public double Humedad { get; set; }
        public bool Puertas { get; set; }
        public bool Energia { get; set; }
        public bool Luz { get; set; }
    }
}
