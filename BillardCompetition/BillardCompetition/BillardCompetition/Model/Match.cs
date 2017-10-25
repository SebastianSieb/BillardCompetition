using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillardCompetition.Model
{
    public class Match
    {
        public Player Player1 { get; }
        public Player Player2 { get; }

        public Player Winner { get; set; }
        public TimeSpan Durration { get; set; }

        public Match (Player p1, Player p2)
        {
            Player1 = p1;
            Player2 = p2;
            Durration = new TimeSpan(0);

        }
    }
}
