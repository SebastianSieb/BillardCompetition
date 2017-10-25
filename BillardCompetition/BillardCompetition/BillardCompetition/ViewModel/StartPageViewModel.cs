using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BillardCompetition.ViewModel
{
    public class StartPageViewModel : BaseViewModel
    {
        private string playerToAdd;
        public string PlayerToAdd
        {
            get
            {
                return playerToAdd;
            }
            set
            {
                if (value != playerToAdd)
                {
                    playerToAdd = value;
                    OnPropertyChanged("PlayerToAdd");
                }
            }
        }

        private Grid playerGrid;
        private List<string> players;

        public ICommand Add { get; set; }
        public ICommand Start { get; set; }

        public StartPageViewModel(Grid playerGrid)
        {
            playerToAdd = "Player Name";
            players = new List<string>();
            this.playerGrid = playerGrid;
            Add = new Command(o => addPlayer());
            Start = new Command(o => App.navigateTo(new View.CompetitionPage(players)));
        }
        
        private void addPlayer()
        {
            players.Add(PlayerToAdd);
            playerGrid.Children.Add(new Label
                {
                    Text = PlayerToAdd,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    FontAttributes = FontAttributes.Bold
                }, 0, playerGrid.RowDefinitions.Count);
            playerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            PlayerToAdd = "";
        }
    }
}
