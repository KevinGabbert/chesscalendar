using System;
using System.Windows.Forms;

namespace ChessCalendar
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new SysTrayApp());
        } 
    }
}