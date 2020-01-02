using AicumenApp.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AicumenApp.ViewModel
{
   public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            ShowUserProfile = new Command(showUserProfiles);
            EditUserProfile = new Command(editUserProfiles);
        }

        private void showUserProfiles()
        {
            MessagingCenter.Send(this, "Profile", "show");
        }

        private void editUserProfiles()
        {
            MessagingCenter.Send(this, "Profile", "edit");
        }

        public ICommand ShowUserProfile { set; get; }
        public ICommand EditUserProfile { set; get; }
    }
}
