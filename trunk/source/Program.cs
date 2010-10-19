using System;
using System.Windows.Forms;

namespace ChessCalendar
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //Console.WriteLine(VERSION + DateTime.Now.ToShortTimeString());

            Application.Run(new SysTrayApp());
        } 
    }
}