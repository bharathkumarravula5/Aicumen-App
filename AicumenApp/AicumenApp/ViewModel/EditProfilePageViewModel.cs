using AicumenApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AicumenApp.ViewModel
{
   public  class EditProfilePageViewModel : BaseViewModel
    {
        private Profile selectedProfile; 

        public EditProfilePageViewModel()
        {
            UpdateProfile = new Command(editProfile);
            MessagingCenter.Subscribe<ShowProfilesPageViewModel, Profile>(this, "Select_Profile", (sender, arg) =>
            {
                selectedProfile = arg;
                _fb_name = arg.fb_name;
                _fb_id = arg.fb_id;
                _fb_gender = arg.fb_gender;
                _fb_email = arg.fb_email;
                _qrm_wallet = arg.qrm_wallet;

                _stq_score = arg.stq_score;

                raisePropertyChange("fb_name");
                raisePropertyChange("fb_id");
                raisePropertyChange("fb_gender");
                raisePropertyChange("fb_email");
                raisePropertyChange("qrm_wallet");
                raisePropertyChange("stq_score");

            });
        }

        public ICommand UpdateProfile { set; get; }

        private void editProfile()
        {
            Profile profile = new Profile();
            profile._id = selectedProfile._id;
            profile.user_os = selectedProfile.user_os;
            profile.fb_gender = selectedProfile.fb_gender;
            profile.fb_name = _fb_name;
            profile.fb_id = _fb_id;
            profile.fb_gender = _fb_gender;
            profile.fb_email = _fb_email;
            profile.qrm_wallet = _qrm_wallet;
            profile.stq_score= _stq_score;

            updateProfile(profile);
        }

        private async void updateProfile(Profile profile)
        {
            try
            {
                HttpClient client = new HttpClient();
                string sContentType = "application/json";
                var content = JsonConvert.SerializeObject(profile);
                var response = await client.PutAsync(Config.ConfigUtil.EDIT_PROFILES_API, new StringContent(content, Encoding.UTF8, sContentType));
                if (response.IsSuccessStatusCode)
                {
                   await Application.Current.MainPage.DisplayAlert("Profile", "Profile updated successfully", "Ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Profile", "Something went wrong please try again...", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Profile", "Something went wrong please try again...", "Ok");
            }
        }

    private string _fb_name;
        public string fb_name
        {
            set
            {
                _fb_name = value;
            }
            get
            {
                return _fb_name;
            }
        }

        private string _stq_score;
        public string stq_score
        {
            set
            {
                _stq_score = value;
            }
            get
            {
                return _stq_score;
            }
        }


        private string _fb_id;
        public string fb_id
        {
            set
            {
                _fb_id = value;
            }
            get
            {
                return _fb_id;
            }
        }



        private string _fb_gender;
        public string fb_gender
        {
            set
            {
                _fb_gender = value;
            }
            get
            {
                return _fb_gender;
            }
        }



        private string _fb_email;
        public string fb_email
        {
            set
            {
                _fb_email = value;
            }
            get
            {
                return _fb_email;
            }
        }


        private string _qrm_wallet;
        public string qrm_wallet
        {
            set
            {
                _qrm_wallet = value;
            }
            get
            {
                return _qrm_wallet;
            }
        }


    }
}
