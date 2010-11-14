using System.Collections.Generic;
using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    public class OutputClass
    {
        #region Properties

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
            //case OutputMode.Balloon:
            //    this.NotifyIcon.BalloonTipText = outputMessage;
            //    this.NotifyIcon.ShowBalloonTip(2000);

            //    //Now Write message to form as well.
            //    break;

           this.Messages.Enqueue(outputMessage);
        }

        /// <summary>
        /// To 'Post' is to put an IChessItem into the Queue.
        /// </summary>
        /// <param name="game"></param>
        public void Post(IChessItem game)
        {
            this.NewMoves.Updated = true;
            this.NewMoves.Enqueue(game);
        }
        //public void Post(string title, string outputMessage)
        //{
        //    this.Post(title, outputMessage);
        //}
    }
}
