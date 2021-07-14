using System;
using System.Collections.Generic;
using System.Text;

namespace EasyWebApp.Data.ViewModels
{
    public class DbInfoViewModel
    {
        public int Id { get; set; }
        public string DbType { get; set; }
        public string DatabaseName { get; set; }
        public string BussinessName { get; set; }
        public string ApiDownloadLink { get; set; }
        public string ClientAppDownloadLink { get; set; }
        public string Status { get; set; }
    }
}
