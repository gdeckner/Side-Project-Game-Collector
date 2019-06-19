using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game_Collector;
using IGDB;
using IGDB.Models;
using Newtonsoft.Json;

namespace Game_Collectoror
{
    static class Program
    {
      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            PullFromDatabase queryDB = new PullFromDatabase();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainMenu());
            queryDB.PullGenreIdAsync().Wait();
            queryDB.GetPlatformID();
           
            queryDB.PullSpecificGameAsync("Halo").Wait();


        }
       
    }
}
