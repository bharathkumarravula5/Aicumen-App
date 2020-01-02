using System;
using System.Collections.Generic;
using System.Text;

namespace AicumenApp.Config
{
   public class ConfigUtil
    {
        private static string BASE_URL = "http://142.93.251.210:3001/api";
        public static string SHOW_PROFILES_API = BASE_URL + "/users";
        public static string EDIT_PROFILES_API = BASE_URL + "/user";
    }
}
