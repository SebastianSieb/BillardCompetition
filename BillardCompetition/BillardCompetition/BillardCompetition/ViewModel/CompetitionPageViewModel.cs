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
        private string _playerOne;
        public string PlayerOne
        {
            get
            {
                return _playerOne;
            }
            set
            {
                if (value != _playerOne)
                {
                    _playerOne = value;
                    OnPropertyChanged("PlayerOne");
                }
            }
        }

        private string _playerTwo;
        public string PlayerTwo
        {
            get
            {
                return _playerTwo;
            }
            set
            {
                if (value != _playerTwo)
                {
                    _playerTwo = value;
                    OnPropertyChanged("PlayerTwo");
                }
            }
        }

        public ICommand P1Win { get; set; }
        public ICommand P2Win { get; set; }

        private readonly Grid _competitionGrid;
        private readonly Competition _competition;

        private int _matchNumber;
        public int MatchNumber
        {
            get
            {
                return _matchNumber;
            }
            set
            {
                if (value != _matchNumber)
                {
                    _matchNumber = value;
                    updateCurrentMatch();
                    OnPropertyChanged("MatchNumber");
                }
            }
        }
        private readonly Picker _matchPicker;
        private Match _selectedMatch;


        public CompetitionPageViewModel(List<string> players, Grid competitionGrid, Picker matchPicker)
        {
            this._competitionGrid = competitionGrid;
            _competition = new Competition(players);
            updateCompetitionGrid();
            this._matchPicker = matchPicker;
            updatePicker();
            updateCurrentMatch();
            P1Win = new Command(o => p1Win());
            P2Win = new Command(o => p2Win());
        }

        private void p1Win()
        {
            if (_selectedMatch == null) return;
            _selectedMatch.Winner = _selectedMatch.Player1;
            updatePicker();
            updateCompetitionGrid();
        }

        private void p2Win()
        {
            if (_selectedMatch == null) return;
            _selectedMatch.Winner = _selectedMatch.Player2;
            updatePicker();
            updateCompetitionGrid();
        }


        private void updateCurrentMatch()
        {
            _selectedMatch = getSelectedMatch();
            if (_selectedMatch == null)
            {
                PlayerOne = "-";
                PlayerTwo = "-";
            } else
            {
                PlayerOne = _selectedMatch.Player1.Name;
                PlayerTwo = _selectedMatch.Player2.Name;
            }
        }

        private void updatePicker()
        {
            MatchNumber = -1;
            _matchPicker.Items.Clear();
            foreach(Match match in _competition.Matches)
            {
                if(match.Winner == null)
                {
                    _matchPicker.Items.Add(match.toString());
                }
            }
        }

        private Match getSelectedMatch()
        {
            string selectedString = "";
            if(MatchNumber != -1)
            {
                selectedString = _matchPicker.Items.ToArray()[MatchNumber];
            }
            foreach(Match mtch in _competition.Matches)
            {
                if (mtch.toString().Equals(selectedString)) {
                    return mtch;
                }
            }
            return null;
        }

        private void updateCompetitionGrid()
        {
            clearGrid(_competitionGrid);

            _competitionGrid.Children.Add(new Label
            {
                Text = "Match",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold
            }, 1, 0);

            _competitionGrid.Children.Add(new Label
            {
                Text = "Winner",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold
            }, 2, 0);

            for(int i = 0; i < _competition.Matches.Count; i++)
            {
                Match mtch = _competition.Matches.ToArray()[i];
                _competitionGrid.Children.Add(new Label
                {
                    Text = mtch.toString(),
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontAttributes = FontAttributes.Bold
                }, 1, i+1);

                string tmp = (mtch.Winner == null) ? "-" : mtch.Winner.Name; 
                _competitionGrid.Children.Add(new Label
                {
                    Text = tmp,
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontAttributes = FontAttributes.Bold
                }, 2, i+1);
                _competitionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
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
