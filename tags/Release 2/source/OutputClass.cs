using System.Collections.Generic;
using ChessCalendar.Enums;

namespace ChessCalendar
{
    public class OutputClass
    {
        #region Properties
            public OutputMode OutputMode { get; set; }
            public Queue<string> Messages { get; set; }
            public System.Windows.Forms.NotifyIcon NotifyIcon { get; set; }
        #endregion

        public void Output(string title, string outputMessage)
        {
            switch (this.OutputMode)
            {
                case OutputMode.Balloon:
                    this.NotifyIcon.BalloonTipText = outputMessage;
                    this.NotifyIcon.ShowBalloonTip(2000);

                    //Now Write message to form as well.
                    break;

                case OutputMode.Form:
                    this.Messages.Enqueue(outputMessage);
                    break;

                default:
                    break;
            }
        }
        public void Output(string title, string outputMessage, OutputMode outputMode)
        {
            this.OutputMode = outputMode;
            this.Output(title, outputMessage);
        }
    }
}
