using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socketClientServer.Models
{
    public class Car
    {
        public string Mark { get; set; }
        public int Year { get; set; }
        public float EngineVolume { get; set; }
        public int? NumberOfDoors { get; set; }
    }
}
