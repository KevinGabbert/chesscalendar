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
            public MessageQueue NewMoves { get; set; }
            public System.Windows.Forms.NotifyIcon NotifyIcon { get; set; }

        #endregion

        public OutputClass()
        {
            this.NewMoves = new MessageQueue();
            this.Messages = new Queue<string>();
        }

        protected void Post(string title, string outputMessage)
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

        /// <summary>
        /// To 'Post' is to put an IChessItem into the Queue.
        /// </summary>
        /// <param name="game"></param>
        public void Post(IChessItem game)
        {
            switch (this.OutputMode)
            {
                case OutputMode.Form:
                    this.NewMoves.Updated = true;
                    this.NewMoves.Enqueue(game);
                    break;

                default:
                    break;
            }
        }
        public void Post(string title, string outputMessage, OutputMode outputMode)
        {
            this.OutputMode = outputMode;
            this.Post(title, outputMessage);
        }
    }
}
