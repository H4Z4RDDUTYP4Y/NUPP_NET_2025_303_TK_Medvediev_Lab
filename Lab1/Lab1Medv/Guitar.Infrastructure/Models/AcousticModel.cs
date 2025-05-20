using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Infrastructure.Models
{
    public class AcousticModel : GuitarModel
    {

        public bool HasPiezo { get; set; }
        public StringType StringType { get; set; }
    }

    public enum StringType { Steel, Nylon }
}
