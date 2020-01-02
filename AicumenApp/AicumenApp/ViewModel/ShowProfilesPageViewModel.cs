using AicumenApp.Config;
using AicumenApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace AicumenApp.ViewModel
{
   public class ShowProfilesPageViewModel : BaseViewModel
    {
        public ShowProfilesPageViewModel()
        {
            getProfilesList();
        }

        private List<Profile> _ShowProfiles;
        public List<Profile> ShowProfiles
        {
            set
            {
                _ShowProfiles = value;
            }
            get
            {
                return _ShowProfiles;
            }
        }

        private Profile _SelectProfile;
        public Profile SelectProfile
        {
            set
            {
                _SelectProfile = value;

                MessagingCenter.Send(this, "Profile", "edit");
                MessagingCenter.Send(this, "Select_Profile", _SelectProfile);

            }
            get
            {
                return _SelectProfile;
            }
        }

        private async void getProfilesList()
        {
            HttpClient objHttpClient = new HttpClient();
            var profiles =await objHttpClient.GetStringAsync(ConfigUtil.SHOW_PROFILES_API);
            var profilesList = JsonConvert.DeserializeObject<List<Profile>>(profiles.ToString());
            _ShowProfiles = profilesList;
            raisePropertyChange("ShowProfiles");
        }
    }
}
