using KordellGiffordMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace KordellGiffordMobileApp.DataAccess
{
    public class DataAccess
    {
        public List<TermModel> GetAllTerms()
        {
            List<TermModel> Terms = new List<TermModel>();
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                //var results = conn.Query();

                return Terms;
            }
        }
    }
}
