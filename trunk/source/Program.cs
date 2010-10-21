using System;
using System.Windows.Forms;
using ChessCalendar.Forms;

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