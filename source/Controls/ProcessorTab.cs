using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChessCalendar.Interfaces;

namespace ChessCalendar.Controls
{
    public class ProcessorTab: TabPage
    {
        public CalendarProcessor Processor { get; set; }
        private MessageList MessageList { get; set; }

        private DataGridView _grid;
        //private ProgressBar _progressBar;
        //private TextBox _nextCheck;

        private int _fail = 0;
        //private bool _pause = false;

        private bool _progressBarFlash;

        public ProcessorTab(string tabName, Uri uriToWatch, string userName, string password, Uri logToCalendar)
        {
            this.Name = tabName;
            this.Processor = new CalendarProcessor(uriToWatch, userName, password, logToCalendar, true);

            _grid = (DataGridView)this.Controls[this.Name + "_dgvAvailableMoves"];
            //_progressBar = (ProgressBar)this.Controls[this.Name + "_pbTimeTillNextUpdate"];
            //_nextCheck = (TextBox)this.Controls[this.Name + "_nextCheck"];

            var messages = new TextBox();
            messages.Name = "TabPage_txtMessages";

            var grid = new DataGridView();
            grid.Name = "TabPage_Grid";
            grid.CellClick += this.grid_CellClick;

            var chkLogToCalendar = new CheckBox();
            chkLogToCalendar.Name = "TabPageName_chkLogToCalendar";

            this.Controls.Add(chkLogToCalendar);
            this.Controls.Add(grid);
            this.Controls.Add(messages);
        }

        public void RefreshTab()
        {
            //this.Update_NextCheck();
            //this.Update_ProgressBar();
            this.Processor.RefreshRSS();
            this.Update_GridView(); //Tells Processor to go read its associated RSS Feed

            Application.DoEvents();
        }

        private void Update_GridView()
        {
            if ((DateTime.Now > this.Processor.NextCheck) || this.Processor.NewMessage)
            {
                //this.UpdateText();
                this.UpdateDataGridView();
            }
            else
            {
                this.MessageList = new MessageList();
            }
        }
        //private void Update_ProgressBar()
        //{
        //    if (this.Processor.NextCheck == new DateTime())
        //    {
        //        this._progressBarFlash = !this._progressBarFlash;

        //        this._progressBar.Value = 100;
        //        this._progressBar.ForeColor = Color.Green;

        //        if (this._progressBarFlash)
        //        {
        //            this._progressBar.Show();
        //            Application.DoEvents();
        //            Thread.Sleep(100);
        //        }
        //        else
        //        {
        //            this._progressBar.Hide();
        //            Application.DoEvents();
        //            Thread.Sleep(100);
        //        }
        //    }
        //    else
        //    {
        //        if (this.Processor.WaitProgress > 100)
        //        {
        //            this._progressBar.ForeColor = Color.Red;
        //            this._progressBar.Value = 100;
        //        }
        //        else
        //        {
        //            this._progressBar.ForeColor = Color.Blue;
        //            this._progressBar.Value = this.Processor.WaitProgress;
        //        }

        //        this._progressBar.Show();
        //    }
        //}
        //private void Update_NextCheck()
        //{
        //    //if (this.Processor.NextCheck == new DateTime())
        //    //{
        //    //    this._nextCheck.Text = "Unknown";
        //    //}
        //    //else
        //    //{
        //    //    this._nextCheck.Text = "Next check will be at: " + this.Processor.NextCheck.ToShortTimeString();
        //    //}
        //}
        private void UpdateDataGridView()
        {
            if (this.Processor != null)
            {
                if (this.Processor.Output != null)
                {
                    if (this.Processor.Output.NewMoves != null)
                    {
                        if (this.Processor.Output.NewMoves.Updated)
                        {
                            if (this.Processor.ClearList)
                            {
                                this.MessageList.Clear();
                            }

                            for (int i = this.Processor.Output.NewMoves.Count; i > 0; i--)
                            {
                                this.MessageList.Add(this.Processor.Output.NewMoves.Dequeue());
                            }

                            GC.Collect();
                            this.SetMovesDataSource(this.MessageList);
                            GC.Collect();

                            this.Processor.NewMessage = true;
                        }
                        else
                        {
                            if (this.Processor.ChessFeed.Count > 0)
                            {
                                if (this.Processor.ChessFeed.Count < 1)
                                {
                                    this.MessageList.Clear();
                                    this.SetMovesDataSource(this.MessageList);
                                }
                            }
                        }
                    }
                    else
                    {
                        _fail += 1;
                        //this.txtLog.Text = "Log is Null: " + _fail + Environment.NewLine + this.txtLog.Text;

                        //if (this.txtLog.Text.Length > 5001)
                        //{
                        //    this.txtLog.Text.Remove(5000);
                        //}
                    }
                }
            }
        }
        //private void UpdateText()
        //{
        //    if (this.Processor != null)
        //    {
        //        if (this.Processor.Messages != null)
        //        {
        //            if (this.Processor.Messages.Count > 0)
        //            {
        //                this.txtLog.Text = (this.Processor.Messages.Dequeue() + Environment.NewLine + this.txtLog.Text);

        //                if (this.txtLog.Text.Length > 5001)
        //                {
        //                    this.txtLog.Text.Remove(5000);
        //                }

        //                this.Processor.NewMessage = true;
        //            }
        //        }
        //        else
        //        {
        //            this.txtLog.Text += "Log is Null" + Environment.NewLine;
        //        }
        //    }
        //}

        private static void FormatDataGrid(DataGridView dataGrid)
        {
            dataGrid.Columns.Clear();
            dataGrid.AutoGenerateColumns = false;

            DataGridViewLinkColumn newMoveColumn = new DataGridViewLinkColumn();
            newMoveColumn.DataPropertyName = "NewMove";
            newMoveColumn.HeaderText = "X";
            newMoveColumn.Width = 20;
            newMoveColumn.Visible = false;

            DataGridViewTextBoxColumn pubDateColumn = new DataGridViewTextBoxColumn();
            pubDateColumn.DataPropertyName = "GetPubDate";
            pubDateColumn.HeaderText = "Pub Date";
            pubDateColumn.Width = 125;

            DataGridViewLinkColumn titleColumn = new DataGridViewLinkColumn();
            titleColumn.DataPropertyName = "GameTitle"; //GameTitle
            titleColumn.HeaderText = "Title";
            titleColumn.Width = 220;
            titleColumn.LinkBehavior = LinkBehavior.SystemDefault;

            DataGridViewTextBoxColumn opponentColumn = new DataGridViewTextBoxColumn();
            opponentColumn.DataPropertyName = "Opponent";
            opponentColumn.HeaderText = "Opponent";
            opponentColumn.Width = 100;

            DataGridViewTextBoxColumn ratingColumn = new DataGridViewTextBoxColumn();
            ratingColumn.DataPropertyName = "RatingRaw";
            ratingColumn.HeaderText = "Rating";
            ratingColumn.Width = 80;

            DataGridViewTextBoxColumn timeLeftColumn = new DataGridViewTextBoxColumn();
            timeLeftColumn.DataPropertyName = "TimeLeftRaw";
            timeLeftColumn.HeaderText = "Time Left";
            timeLeftColumn.Width = 150;

            DataGridViewTextBoxColumn moveColumn = new DataGridViewTextBoxColumn();
            moveColumn.DataPropertyName = "MoveRaw";
            moveColumn.HeaderText = "Move #";
            moveColumn.Width = 80;

            //Do I *really* have to make a hidden column?
            DataGridViewLinkColumn gameIDColumn = new DataGridViewLinkColumn();
            gameIDColumn.DataPropertyName = "GameLink"; //GameID
            gameIDColumn.HeaderText = "Game";
            gameIDColumn.Width = 350;
            gameIDColumn.Visible = false;

            dataGrid.Columns.Add(newMoveColumn);
            dataGrid.Columns.Add(pubDateColumn);
            dataGrid.Columns.Add(titleColumn);
            dataGrid.Columns.Add(opponentColumn);
            dataGrid.Columns.Add(ratingColumn);
            dataGrid.Columns.Add(timeLeftColumn);
            dataGrid.Columns.Add(moveColumn);

            dataGrid.Columns.Add(gameIDColumn);
        }

        private delegate void MovesDelegate(BindingList<IChessItem> dataSource);
        private void SetMovesDataSource(BindingList<IChessItem> dataSource)
        {
            if (_grid.InvokeRequired)
            {
                _grid.Invoke(new MovesDelegate(this.SetMovesDataSource), dataSource);
            }
            else
            {
                _grid.DataSource = dataSource;
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 2:
                    //Do we really have to use a hidden column? is this the only way?
                    Process.Start(_grid[7, e.RowIndex].Value.ToString());
                    break;
            }
        }

        //private Control FindControlByName(string name)
        //{
        //    return this.Controls.Cast<Control>().FirstOrDefault(c => c.Name == name);
        //}
    }
}
