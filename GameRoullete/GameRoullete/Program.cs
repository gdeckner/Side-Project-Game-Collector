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
            string[] testArray = new string[] { "Playstation 4", "Xbox 360" };
            DatabasAPItest test = new DatabasAPItest();
            PullFromDatabase queryDB = new PullFromDatabase();
            queryDB.GetPlatformID();
            //queryDB.PullSpecificGameAsync("Witcher 3").Wait();
            //queryDB.PullFranchiseAsync("Halo").Wait();
            //queryDB.PullTopRatedGamesAsync(testArray).Wait();            
            queryDB.PullGenreIdAsync().Wait();


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainMenu());

        }
       
    }
}
