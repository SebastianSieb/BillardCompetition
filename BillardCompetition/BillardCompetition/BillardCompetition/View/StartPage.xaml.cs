using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillardCompetition.ViewModel;

using Xamarin.Forms;

namespace BillardCompetition.View
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            BindingContext = new StartPageViewModel(playerGrid);
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
