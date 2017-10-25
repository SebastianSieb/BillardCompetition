using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillardCompetition.ViewModel;

using Xamarin.Forms;

namespace BillardCompetition.View
{
    public partial class CompetitionPage : ContentPage
    {
        public CompetitionPage(List<string> players)
        {
            InitializeComponent();
            BindingContext = new CompetitionPageViewModel(players, CompetitionGrid, MatchPicker);
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
