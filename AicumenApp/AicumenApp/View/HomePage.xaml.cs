using AicumenApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AicumenApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = new HomePageViewModel();

            Detail = new NavigationPage(new ShowProfilesPage());

            MessagingCenter.Subscribe<ShowProfilesPageViewModel, string>(this, "Profile", (sender, arg) =>
              {

                  switch (arg)
                  {
                      case "edit":
                          {
                              Detail = new NavigationPage(new EditProfilePage());
                              IsPresented = false;
                              break;
                          }
                  }
                 
              });

            MessagingCenter.Subscribe<HomePageViewModel, string>(this, "Profile", (sender, arg) =>
            {

                switch (arg)
                {
                    case "show":
                        {
                            Detail = new NavigationPage(new ShowProfilesPage());
                            IsPresented = false;

                            break;
                        }
                    case "edit":
                        {
                            Detail = new NavigationPage(new EditProfilePage());
                            IsPresented = false;
                            break;
                        }
                }

            });
        }
    }
}