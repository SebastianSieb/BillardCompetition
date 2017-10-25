using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillardCompetition.Model
{
    public class Competition
    {
        public List<Match> Matches { get; set; }
        public List<Player> Players { get; set; }

        public Competition(List<string> player)
        {
            Matches = new List<Match>();
            Players = new List<Player>();
            generateMatches(player);
        }
        
        private void generateMatches(List<string> playerNames)
        {
            foreach (string name in playerNames)
            {
                Players.Add(new Player(name));
            }
            Player[] player = Players.ToArray();

            for(int i = 0; i < player.Length; i++)
            {
                for(int j = i+1; j < player.Length; j++)
                {
                    Matches.Add(new Match(player[i], player[j]));
                }
            }
        }
    }
}
