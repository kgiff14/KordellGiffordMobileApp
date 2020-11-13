using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KordellGiffordMobileApp.Models
{
    public class TermModel
    {
        [PrimaryKey, AutoIncrement]
        public int TermId { get; set; }
        public string TermName { get; set; }
        public DateTime TermStartDate { get; set; }
        public DateTime TermEndDate { get; set; }
    }
}
