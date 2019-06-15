using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGDB;
using IGDB.Models;
using Newtonsoft.Json;

namespace GameRoullete
{
    static class Program
    {
      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DatabasAPItest test = new DatabasAPItest();
            PullFromDatabase queryDB = new PullFromDatabase();
            queryDB.PullSpecificGameAsync("Halo").Wait();

            
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainMenu());
           
        }
    }
}
