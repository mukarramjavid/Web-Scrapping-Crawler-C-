using System;
using System.Collections.Generic;
using System.Text;

namespace WebScrapping
{
    public class User
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_pwd { get; set; }
        public int user_phone { get; set; }
        public int user_age { get; set; }
        public int addID { set; get; }
        public string cityName { set; get; }
        public string ImagePathUvm { get; set; }
        public string CoverPic { get; set; }
    }
}
