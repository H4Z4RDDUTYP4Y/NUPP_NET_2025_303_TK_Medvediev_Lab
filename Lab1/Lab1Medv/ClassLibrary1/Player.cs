using Guitar.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar.Common
{
    public class Player : IEntity
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public int YearsExperience { get; set; }

        //delegate used here
        public delegate void PlayerHandler(Player player);

        public event PlayerHandler PlayerIntro;

        public Player(string name, int age, int yearsexperience)
        {
            Name = name; 
            Age = age;   
            YearsExperience = yearsexperience; 
        }
        public void PlayPlayerIntro()
        {
            Console.WriteLine($"Hi!, my name is {Name}, and im going to play music.");
            PlayerIntro?.Invoke(this);
        }
    }
}
