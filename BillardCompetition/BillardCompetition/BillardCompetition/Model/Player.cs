using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillardCompetition.Model
{
    public class Player
    {
        public string Name { get; set; }
        public int Wins { get; set; }

        public Player (string name)
        {
            Name = name;
            Wins = 0;
        }
    }
}
