using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Common
{
    public class Player
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public int YearsExperience { get; set; }
        public Player(string name, int age, int yearsexperience)
        {
            Name = name; 
            Age = age;   
            YearsExperience = yearsexperience; 
        }
    }
}
