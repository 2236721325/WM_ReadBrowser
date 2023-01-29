using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM_ReadBrowser.Models;

namespace WM_ReadBrowser.Databases
{
    public class MyDatabase
    {
        public SQLiteAsyncConnection Database;

        public MyDatabase()
        {
            Task.Run(() => this.Init()).Wait();
        }

        async Task Init()
        {
            if (Database is not null)
                return;
            Database = new SQLiteAsyncConnection(databasePath:Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<WebCollection>();
        }

    }
}
