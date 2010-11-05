using System.Collections.Generic;
using ChessCalendar.Enums;
using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    public class OutputClass
    {
        #region Properties

            public OutputMode OutputMode { get; set; }
            public Queue<string> Messages { get; set; }
            public Queue<IChessItem> NewMoves { get; set; }
            public System.Windows.Forms.NotifyIcon NotifyIcon { get; set; }

        #endregion

        protected OutputClass()
        {
            this.NewMoves = new Queue<IChessItem>();
            this.Messages = new Queue<string>();
        }

        protected void Output(string title, string outputMessage)
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

        protected void Output(IChessItem game)
        {
            switch (this.OutputMode)
            {
                case OutputMode.Form:
                    this.NewMoves.Enqueue(game);
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
