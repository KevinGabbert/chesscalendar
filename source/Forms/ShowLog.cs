﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ChessCalendar.Interfaces;
using System.Threading;

namespace ChessCalendar.Forms
{
    public partial class ShowLog : Form
    {
        private int _fail = 0;
        private bool _pause = false;
        private bool _progressBarFlash;

        #region Properties

            public Log Log { get; set; }
            private MessageList MessageList { get; set; }
            public bool DebugMode { get; set; }
            
        #endregion

        public ShowLog()
        {
            InitializeComponent();

            this.pbTimeTillNextUpdate.Maximum = 100;
            this.pbTimeTillNextUpdate.Minimum = 0;
            this.pbTimeTillNextUpdate.Increment(1);

            this.MessageList = new MessageList();
            FormatDataGrid(this.dgvAvailableMoves);

            //TODO:  make this into a popup.
            this.txtNextCheck.Text = "Querying RSS Feed and Google Calendar....";
        }

        #region Events

        private void dgvAvailableMoves_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvAvailableMoves.Columns[e.ColumnIndex].DataPropertyName == "NewMove")
            {
                if (this.dgvAvailableMoves.DataSource != null)
                {
                    IChessItem item = ((BindingList<IChessItem>) this.dgvAvailableMoves.DataSource)[e.RowIndex];

                    e.CellStyle.BackColor = item.NewMove == true ? Color.Green : Color.DimGray;
                }
            }
        }

        private void ShowLog_Shown(object sender, System.EventArgs e)
        {
            this.RunLoop();
        }
        private void ShowLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
        private void ShowLog_Resize(object sender, EventArgs e)
        {
            this.txtLog.Width = this.Width - 8;

            //TabControl
            this.tabs.Width = this.Width - 10;
            this.tabs.Height = this.Height - 105;

            //Grid
            this.dgvAvailableMoves.Width = this.Width - 20;
            this.dgvAvailableMoves.Height = this.Height - 110;
     
            //button
            this.btnPause.Top = this.Height - 95;
            this.btnRefresh.Top = this.Height - 95;

            //TextBox
            this.txtNextCheck.Top = this.Height - 67;
            this.txtNextCheck.Width = this.Width - 8;

            //ProgressBar
            this.pbTimeTillNextUpdate.Top = this.Height - 45;
            this.pbTimeTillNextUpdate.Width = this.Width - 8;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            this._pause = !_pause;

            this.btnPause.Text = this._pause ? "Resume Reading RSS" : "Pause Reading RSS";

            if (this._pause)
            {
                this.Log.Stop();
            }
            else
            {
                this.Log.Go();
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Log.ResetWait = true;
        }

        #endregion

        private void RunLoop()
        {
            while (true)
            {
                this.Update_NextCheck();
                this.Update_ProgressBar();
                this.Update_GridView();

                Application.DoEvents();
            }
        }

        private void Update_GridView()
        {
            if((DateTime.Now > this.Log.NextCheck) || this.Log.NewMessage)
            {
                this.UpdateText();
                this.UpdateDataGridView();    
            }
            else
            {
                this.MessageList = new MessageList();
            }
        }
        private void Update_ProgressBar()
        {
            if (this.Log.NextCheck == new DateTime())
            {
                this._progressBarFlash = !this._progressBarFlash;

                this.pbTimeTillNextUpdate.Value = 100;
                this.pbTimeTillNextUpdate.ForeColor = Color.Green;
                
                if(this._progressBarFlash)
                {
                    this.pbTimeTillNextUpdate.Show();
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
                else
                {
                    this.pbTimeTillNextUpdate.Hide();
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
            }
            else
            {
                if (this.Log.WaitProgress > 100)
                {
                    this.pbTimeTillNextUpdate.ForeColor = Color.Red;
                    this.pbTimeTillNextUpdate.Value = 100;
                }
                else
                {
                    this.pbTimeTillNextUpdate.ForeColor = Color.Blue;
                    this.pbTimeTillNextUpdate.Value = this.Log.WaitProgress;
                }

                this.pbTimeTillNextUpdate.Show();
            } 
        }
        private void Update_NextCheck()
        {
            if(this.Log.NextCheck == new DateTime())
            {
                this.txtNextCheck.Text = "Unknown";
            }
            else
            {
                this.txtNextCheck.Text = "Next check will be at: " + this.Log.NextCheck.ToShortTimeString();
            }
        }
        private void UpdateDataGridView()
        {
            if (this.Log != null)
            {
                if (this.Log.NewMoves != null)
                {
                    if (this.Log.NewMoves.Count > 0)
                    {
                        if (this.Log.ClearList)
                        {
                            this.MessageList.Clear();
                        }

                        for (int i = this.Log.NewMoves.Count; i > 0; i--)
                        {
                            this.MessageList.Add(this.Log.NewMoves.Dequeue()); 
                        }

                        GC.Collect();
                        this.SetMovesDataSource(this.MessageList);
                        GC.Collect();

                        this.Log.NewMessage = true;
                    }
                }
                else
                {
                    _fail += 1;
                    this.txtLog.Text = "Log is Null: " + _fail + Environment.NewLine + this.txtLog.Text;

                    if (this.txtLog.Text.Length > 5001)
                    {
                        this.txtLog.Text.Remove(5000);
                    }
                }
            }
        }
        private void UpdateText()
        {
            if (this.Log != null)
            {
                if (this.Log.Messages != null)
                {
                    if (this.Log.Messages.Count > 0)
                    {
                        this.txtLog.Text = (this.Log.Messages.Dequeue() + Environment.NewLine + this.txtLog.Text);

                        if (this.txtLog.Text.Length > 5001)
                        {
                            this.txtLog.Text.Remove(5000);
                        }

                        this.Log.NewMessage = true;
                    }
                }
                else
                {
                    this.txtLog.Text += "Log is Null" + Environment.NewLine;
                }
            }
        }

        private void FormatDataGrid(DataGridView dataGrid)
        {
            dataGrid.Columns.Clear();
            dataGrid.AutoGenerateColumns = false;

            DataGridViewLinkColumn newMoveColumn = new DataGridViewLinkColumn();
            newMoveColumn.DataPropertyName = "NewMove";
            newMoveColumn.HeaderText = "X";
            newMoveColumn.Width = 20;

            DataGridViewTextBoxColumn pubDateColumn = new DataGridViewTextBoxColumn();
            pubDateColumn.DataPropertyName = "PubDate";
            pubDateColumn.HeaderText = "Pub Date";
            pubDateColumn.Width = 150;

            DataGridViewLinkColumn titleColumn = new DataGridViewLinkColumn();
            titleColumn.DataPropertyName = "GameTitle"; //GameTitle
            titleColumn.HeaderText = "Title";
            titleColumn.Width = 170;
            
            //titleColumn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(x);
            //titleColumn.UseColumnTextForLinkValue = true;
            titleColumn.LinkBehavior = LinkBehavior.SystemDefault;
           

            DataGridViewLinkColumn opponentColumn = new DataGridViewLinkColumn();
            opponentColumn.DataPropertyName = "Opponent";
            opponentColumn.HeaderText = "Opponent";
            opponentColumn.Width = 70;

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

            DataGridViewLinkColumn gameIDColumn = new DataGridViewLinkColumn();
            gameIDColumn.DataPropertyName = "GameLink"; //GameID
            gameIDColumn.HeaderText = "Game";
            gameIDColumn.Width = 250;

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
            if (this.dgvAvailableMoves.InvokeRequired)
            {
                this.dgvAvailableMoves.Invoke(new MovesDelegate(this.SetMovesDataSource), dataSource);
            }
            else
            {
                this.dgvAvailableMoves.DataSource = dataSource;              
            }  
        }

        private void dgvAvailableMoves_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch(e.ColumnIndex)
            {
                case 2:
                    Process.Start(dgvAvailableMoves[e.ColumnIndex, e.RowIndex].Value.ToString());
                    break;

                case 7: //Game Link

                // access cell value, you could also access other cell values
                //Console.WriteLine(dgvAvailableMoves[e.ColumnIndex, e.RowIndex].Value);

                // or if you want access to the bound object
                // yourDataGridView.Rows[e.RowIndex].DataBoundItem

                // do something
                    Process.Start(dgvAvailableMoves[e.ColumnIndex, e.RowIndex].Value.ToString());
                    break;
            }
        }

        private void x(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //if (e.ColumnIndex == 0 && e.RowIndex >= 0 && e.RowIndex < 2)
            //{

            //    //(dgvAvailableMoves.Rows(e.RowIndex).Cells(e.ColumnIndex).Value);
            //}
        }
    }
}
