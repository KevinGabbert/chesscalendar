using System;

namespace ChessCalendar
{
    class Program
    {
        public const string VERSION = "Chess Calendar v10.16.10 ";
        //public const string CONFIG_FILE_PATH = @"..\..\GamesToLog.xml"; //Not used.. yet

        static void Main(string[] args)
        {
            Console.WriteLine(VERSION + DateTime.Now.ToShortTimeString());

            var userInfoForm = new Login_Form(VERSION);
            userInfoForm.ShowDialog();  
            
            //*** Code Execution will stop at this point and wait until user has dismissed the Login form. ***//

            Log.LogVersion = VERSION;
            Log.DebugMode = userInfoForm.DebugMode;
            Log.Beep_On_New_Move = userInfoForm.Beep_On_New_Move;
            Log.Log_All_Games(new Uri("http://www.chess.com/rss/echess/" + userInfoForm.ChessDotComName), userInfoForm.User, userInfoForm.Password, userInfoForm.PostURI);
        } 
    }
}