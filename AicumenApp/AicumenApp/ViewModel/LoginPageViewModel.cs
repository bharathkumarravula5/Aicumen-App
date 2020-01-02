using AicumenApp.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using AicumenApp.View;
using AicumenApp.Config;
using System.Net.Http;
using System.Linq;
using System.Xml.Linq;

namespace AicumenApp.ViewModel
{
    public class LoginPageViewModel
    {
        private string _entmobilenumber;
        public string entmobilenumber { set; get; }

        public ICommand SendOTPOperation { set; get; }
        public ICommand VerifyOTPOperation { set; get; }

        private int mOtpNumber;

        public LoginPageViewModel()
        {
            SendOTPOperation = new Command(CheckMobileNumber);
            VerifyOTPOperation = new Command(verifyOTPNumber);
        }


        private string _MobileNumber;
        public string MobileNumber
        {
            set
            {
                _MobileNumber = value;
            }
            get
            {
                return _MobileNumber;
            }
        }


        private string _OTPNumber;
        public string OTPNumber
        {
            set
            {
                _OTPNumber = value;
            }
            get
            {
                return _OTPNumber;
            }
        }

        private async void CheckMobileNumber()
        {
            HttpClient objHttpClient = new HttpClient();
            var profiles = await objHttpClient.GetStringAsync(ConfigUtil.SHOW_PROFILES_API);
            List<Profile> profilesListData = JsonConvert.DeserializeObject<List<Profile>>(profiles.ToString());
            var mobileNumberExists = (from a in profilesListData where a.stq_score == _MobileNumber select a).ToList();
            if (mobileNumberExists.Count > 0)
            {
                sendOTP();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("SendOTP", "Whatever you provided Mobile number is not there in ProfilesList", "ok");
            }
        }

       private async void verifyOTPNumber()
        {
            if(mOtpNumber == Convert.ToInt32(_OTPNumber))
            {
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Verify OTP", "OTP is wrong, Please verify otp number", "ok");

            }
        }

        private async void sendOTP()
        {
            string countryCode = "91";

            OTPModel model = new OTPModel();
            model.sender = "SOCKET";
            model.route = "4";
            model.country = "countryCode";
            mOtpNumber = GenerateOTP();
            string message = $"<#> Your OTP is {mOtpNumber}  89XB%2BseFjklo";
            List<string> numbers = new List<string> { _MobileNumber };
            model.sms.Add(new Sms { message = message, to = numbers });

             var client = new RestClient($"https://api.msg91.com/api/v2/sendsms?country={countryCode}");

            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authkey", "308519AMtXuYrLKJ5df72198");
            string jsonData = JsonConvert.SerializeObject(model);
            request.AddParameter("application/json", jsonData, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                OTPResponse resp = JsonConvert.DeserializeObject<OTPResponse>(response.Content);
                if (resp.type == "success")
                {
                  await Application.Current.MainPage.DisplayAlert("Message", $"An OTP send to {numbers[0]}", "GotIt", "Cancel");
                   
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Message", resp.message, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Message", response.ErrorMessage, "ok");
            }
        }

        private int GenerateOTP()
        {
            return new Random().Next(1000, 9999);
        }
    }

}

