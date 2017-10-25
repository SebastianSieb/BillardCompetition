using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillardCompetition.View;

using Xamarin.Forms;

namespace BillardCompetition
{
    public class App : Application
    {
        private static App Instance;
        public App()
        {
            Instance = this;
            MainPage = new NavigationPage(new StartPage());
        }

        public static void navigateTo(ContentPage page)
        {
            //the if is needed to provide a back button in the windows app
            if (Device.OS == TargetPlatform.Windows)
            {
                Instance?.MainPage.Navigation.PushAsync(page);
            }
            else
            {
                Instance?.MainPage.Navigation.PushModalAsync(page);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
