using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BillardCompetition.Model;

namespace BillardCompetition.ViewModel
{
    public class CompetitionPageViewModel : BaseViewModel
    {
        private string playerOne;
        public string PlayerOne
        {
            get
            {
                return playerOne;
            }
            set
            {
                if (value != playerOne)
                {
                    playerOne = value;
                    OnPropertyChanged("PlayerOne");
                }
            }
        }

        private string playerTwo;
        public string PlayerTwo
        {
            get
            {
                return playerTwo;
            }
            set
            {
                if (value != playerTwo)
                {
                    playerTwo = value;
                    OnPropertyChanged("PlayerTwo");
                }
            }
        }

        public ICommand P1Win { get; set; }
        public ICommand P2Win { get; set; }

        private Grid competitionGrid;
        private Competition competition;

        private int matchNumber;
        public int MatchNumber
        {
            get
            {
                return matchNumber;
            }
            set
            {
                if (value != matchNumber)
                {
                    matchNumber = value;
                    updateCurrentMatch();
                    OnPropertyChanged("MatchNumber");
                }
            }
        }
        private Picker matchPicker;
        private Match selectedMatch;


        public CompetitionPageViewModel(List<string> players, Grid competitionGrid, Picker matchPicker)
        {
            this.competitionGrid = competitionGrid;
            competition = new Competition(players);
            updateCompetitionGrid();
            this.matchPicker = matchPicker;
            updatePicker();
            updateCurrentMatch();
            P1Win = new Command(o => p1Win());
            P2Win = new Command(o => p2Win());
        }

        private void p1Win()
        {
            if (selectedMatch == null) return;
            selectedMatch.Winner = selectedMatch.Player1;
            updatePicker();
            updateCompetitionGrid();
        }

        private void p2Win()
        {
            if (selectedMatch == null) return;
            selectedMatch.Winner = selectedMatch.Player2;
            updatePicker();
            updateCompetitionGrid();
        }


        private void updateCurrentMatch()
        {
            selectedMatch = getSelectedMatch();
            if (selectedMatch == null)
            {
                PlayerOne = "-";
                PlayerTwo = "-";
            } else
            {
                PlayerOne = selectedMatch.Player1.Name;
                PlayerTwo = selectedMatch.Player2.Name;
            }
        }

        private void updatePicker()
        {
            MatchNumber = -1;
            matchPicker.Items.Clear();
            foreach(Match match in competition.Matches)
            {
                if(match.Winner == null)
                {
                    matchPicker.Items.Add(match.toString());
                }
            }
        }

        private Match getSelectedMatch()
        {
            string selectedString = "";
            if(MatchNumber != -1)
            {
                selectedString = matchPicker.Items.ToArray()[MatchNumber];
            }
            foreach(Match mtch in competition.Matches)
            {
                if (mtch.toString().Equals(selectedString)) {
                    return mtch;
                }
            }
            return null;
        }

        private void updateCompetitionGrid()
        {
            clearGrid(competitionGrid);

            competitionGrid.Children.Add(new Label
            {
                Text = "Match",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold
            }, 1, 0);

            competitionGrid.Children.Add(new Label
            {
                Text = "Winner",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold
            }, 2, 0);

            for(int i = 0; i < competition.Matches.Count; i++)
            {
                Match mtch = competition.Matches.ToArray()[i];
                competitionGrid.Children.Add(new Label
                {
                    Text = mtch.toString(),
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontAttributes = FontAttributes.Bold
                }, 1, i+1);

                string tmp = (mtch.Winner == null) ? "-" : mtch.Winner.Name; 
                competitionGrid.Children.Add(new Label
                {
                    Text = tmp,
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontAttributes = FontAttributes.Bold
                }, 2, i+1);
                competitionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
        }

        private void clearGrid(Grid grid)
        {
            List<Xamarin.Forms.View> templist = grid.Children.ToList();
            IEnumerator<Xamarin.Forms.View> e = templist.GetEnumerator();
            int numberOfColums = grid.ColumnDefinitions.Count;
            for (int i = 0; i <= numberOfColums; i++)
            {
                e.MoveNext();
            }
            do
            {
                if (e.Current != null)
                {
                    grid.Children.Remove(e.Current);
                }
            } while (e.MoveNext());

            List<RowDefinition> tempRows = grid.RowDefinitions.ToList();
            IEnumerator<RowDefinition> rowE = tempRows.GetEnumerator();
            rowE.MoveNext();

            do
            {
                if (rowE.Current != null)
                {
                    grid.RowDefinitions.Remove(rowE.Current);
                }
            } while (rowE.MoveNext());
        }
    }
}
