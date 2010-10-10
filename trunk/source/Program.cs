using System;

namespace ChessCalendar
{
    class Program
    {
        public const string VERSION = "Game Calendar v10.09.10 ";
        public const string CONFIG_FILE_PATH = @"..\..\GamesToLog.xml";

        static void Main(string[] args)
        {
            Console.WriteLine(VERSION + DateTime.Now.ToShortTimeString());

            //var userInfoForm = new Login_Form();
            //userInfoForm.ShowDialog();  
            
            //*** Code Execution will stop at this point and wait until user has dismissed the Login form. ***//

            Log.LogVersion = VERSION;
            Log.Log_All_Games(new Uri("http://www.chess.com/rss/echess/KevinGabbert"));

            //Log.Log_Watched_Games(Config.GetWatchList(CONFIG_FILE_PATH), userInfoForm.User, userInfoForm.Password, userInfoForm.PostURI);
        } 
    }
}