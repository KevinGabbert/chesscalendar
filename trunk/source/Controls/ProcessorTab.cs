﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using ChessCalendar.Interfaces;

namespace ChessCalendar.Controls
{
    public class ProcessorTab: TabPage, IError
    {
        #region Properties
            public GameProcessor Processor { get; set; }
            private MessageList MessageList { get; set; }

            private string _error;
            public string Error 
            {
                get { return _error; }
            }
            public override sealed string Text
            {
                get { return base.Text; }
                set { base.Text = value; }
            }
            public bool UseCalendar { get; set; }
        #endregion

        private readonly DataGridView _grid;
        CheckBox _chkLogToCalendar;

        private int _fail = 0;

        public ProcessorTab(string chessDotComName, string tabName, Uri uriToWatch, string userName, string password, Uri logToCalendar, bool useCalendar)
        {
            this.Name = tabName;
            this.Text = chessDotComName;
            this.Processor = new GameProcessor(chessDotComName, uriToWatch, userName, password, logToCalendar, useCalendar);
            this.MessageList = new MessageList();
            this.UseCalendar = useCalendar;

            _grid = (DataGridView)this.Controls[this.Name + "_dgvAvailableMoves"];

            var messages = new TextBox();
            messages.Name = Constants.MESSAGES;

            _grid = new DataGridView();
            _grid.Name = Constants.GRID;
            _grid.CellClick += this.grid_CellClick;

            _chkLogToCalendar = new CheckBox();
            _chkLogToCalendar.Name = Constants.RECORD_IN_CALENDAR;
            _chkLogToCalendar.Checked = this.UseCalendar;
            _chkLogToCalendar.CheckedChanged += this._chkLogToCalendar_CheckedChanged;

            this.Controls.Add(_chkLogToCalendar);
            this.Controls.Add(_grid);
            this.Controls.Add(messages);

            _grid.Parent.Text = Processor.UserLogged;

            ProcessorTab.FormatDataGrid(this._grid);
            this.ResetControls();
        }

        #region Events
        private void _chkLogToCalendar_CheckedChanged(object sender, EventArgs e)
        {
            this.UseCalendar = _chkLogToCalendar.Checked;
        }

        #endregion

        public static void FormatDataGrid(DataGridView dataGrid)
        {
            dataGrid.RowHeadersVisible = false;

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

        public void RefreshTab()
        {
            this.Processor.Refresh(out _error);
            this.Update_GridView(); //Tells Processor to go read its associated RSS Feed

            Application.DoEvents();
        }

        public void ResetControls()
        {
            if (this.Parent != null)
            {
                if (this.Parent.Parent != null)
                {
                    this.Controls[Constants.GRID].Width = this.Parent.Parent.Width - 20;
                    this.Controls[Constants.GRID].Height = this.Parent.Parent.Height - 130;

                    this.Controls[Constants.RECORD_IN_CALENDAR].Top = this.Height - 85;
                }
            }

            Application.DoEvents();
        }

        private void Update_GridView()
        {
            if ((DateTime.Now > this.Processor.NextCheck) || this.Processor.NewMessage)
            {
                this.UpdateDataGridView();
            }
            else
            {
                this.MessageList = new MessageList();
            }
        }
        private void UpdateDataGridView()
        {
            if (this.Processor != null)
            {
                if (this.Processor.Output != null)
                {
                    if (this.Processor.Output.NewMoves != null)
                    {
                        //TODO Get this IF working
                        if (this.Processor.Output.NewMoves.Updated)
                        {
                            if (this.Processor.ClearList)
                            {
                                this.MessageList.Clear();
                                this.Processor.ClearList = false;
                            }

                            for (int i = this.Processor.Output.NewMoves.Count; i > 0; i--)
                            {
                                this.MessageList.Add(this.Processor.Output.NewMoves.Dequeue());
                            }

                            this.SetMovesDataSource(this.MessageList);

                            this.Processor.NewMessage = true;
                        }
                        else
                        {
                            if (this.Processor.ChessFeed.Count > 0)
                            {
                                this.MessageList.Clear();
                                this.SetMovesDataSource(this.MessageList);
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
            GC.Collect();
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

        private delegate void MovesDelegate(BindingList<IChessItem> dataSource);
        public void SetMovesDataSource(BindingList<IChessItem> dataSource)
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

        //private void dgvAvailableMoves_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (this.dgvAvailableMoves.Columns[e.ColumnIndex].DataPropertyName == "NewMove")
        //    {
        //        if (this.dgvAvailableMoves.DataSource != null)
        //        {
        //            IChessItem item = ((BindingList<IChessItem>)this.dgvAvailableMoves.DataSource)[e.RowIndex];

        //            e.CellStyle.BackColor = item.NewMove == true ? Color.Green : Color.DimGray;
        //        }
        //    }
        //}
    }
}