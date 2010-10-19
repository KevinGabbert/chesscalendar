﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessCalendar
{
    public class SysTrayApp : Form
    {
        public NotifyIcon  _trayIcon;
        private ContextMenu _trayMenu;

        public const string VERSION = "Chess Calendar v10.18.10 SysTray";
        //public const string CONFIG_FILE_PATH = @"..\..\GamesToLog.xml"; //Not used.. yet

        public SysTrayApp()
        {
            _trayMenu = new ContextMenu();
            _trayMenu.MenuItems.Add("Start", Login);//todo: animated gif with logo phasing in and out.
            //_trayMenu.MenuItems.Add("Stop", Login); //todo: this should change Icon to icon with red circle and line through it.
            _trayMenu.MenuItems.Add("Options", Options); //Todo: all console notifications will go here..
            _trayMenu.MenuItems.Add("Exit", OnExit);

            _trayIcon = new NotifyIcon();
            _trayIcon.Text = "Chess Calendar";
            _trayIcon.Icon = new Icon(@"..\..\Images\Logo.ico", 50, 50);

            // Add menu to tray icon and show it.
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Visible = false; // Hide form window.
            this.ShowInTaskbar = false; // Remove from taskbar.
            base.OnLoad(e);
        }

        private static void Login(object sender, EventArgs e)
        {

            var userInfoForm = new Login_Form(VERSION);
            userInfoForm.ShowDialog();

            //*** Code Execution will stop at this point and wait until user has dismissed the Login form. ***//

            if (userInfoForm.ValidatedForm)
            {
                Log.LogVersion = VERSION;
                Log.DebugMode = userInfoForm.DebugMode;
                Log.Beep_On_New_Move = userInfoForm.Beep_On_New_Move;
                Log.Log_All_Games(new Uri("http://www.chess.com/rss/echess/" + userInfoForm.ChessDotComName),
                                  userInfoForm.User, userInfoForm.Password, userInfoForm.PostURI);
            }
            else
            {
                MessageBox.Show("Some of the Login information wasn't right, try again.", "ummmmmmm...");
            }
        }
        private static void Options(object sender, EventArgs e)
        {
            var optionsForm = new Options();
            optionsForm.ShowDialog();
        }
        private static void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
 
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
               // Release the icon resource.
               _trayIcon.Dispose();
            }
           base.Dispose(isDisposing);
        }
    }
}