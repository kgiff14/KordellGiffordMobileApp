using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace KordellGiffordMobileApp.Models
{
    public class NotificationModel
    {
        [PrimaryKey, AutoIncrement]
        public int NotifyId { get; set; }
        public bool NotifyOn { get; set; }
        public int NotifyDay { get; set; }
    }
}
