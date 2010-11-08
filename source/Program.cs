using System;

namespace ChessCalendar
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var x = new ApplicationManager();
            x.Start();
        } 
    }
}