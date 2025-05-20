using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Infrastructure.Models
{
    public class ElectricModel : GuitarModel
    {
        public int PickupCount { get; set; }
        public VibratoSystem VibratoSystem { get; set; }
        

        
    }
    public enum VibratoSystem { None, FloatingBridge, LockedBridge, Bigsby }
}
