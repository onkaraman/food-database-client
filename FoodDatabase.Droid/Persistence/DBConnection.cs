using System;
using FoodDatabase.Core.Persistence;
using SQLite;

namespace FoodDatabase.Droid.Persistence
{
    public class DBConnection : DBConnectionCore
    {
        public DBConnection () : base(){}

        protected override void setup()
        {
            dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dbPath = System.IO.Path.Combine(dbPath, dbName);

            Connection = new SQLiteConnection(dbPath);
        }
    }
}

