using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace SQLiteTest
{
    class clsCon
    {
        public static SQLiteConnection con = new SQLiteConnection("Data Source=Data/Test.s3db");

        public clsCon()
        {
        }
    }
}
