using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Infrastructure.Models
{
    public class PlayerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int YearsExperience { get; set; }

        // Зв’язок один-до-багатьох: один Player -> багато GuitarModel
        public ICollection<GuitarModel> Guitars { get; set; } = new List<GuitarModel>();
    }
}
