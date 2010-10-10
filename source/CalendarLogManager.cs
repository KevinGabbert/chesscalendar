using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ChessCalendar
{
    public class CalendarLogManager: List<RecordedGame>
    {
        public void Add(RecordedGame process, bool stillRunning)
        {
            base.Add(process);  
        }

        public void AddOrUpdate(Process process, bool stillRunning)
        {
            try
            {
                int i = 0;
                foreach (RecordedGame entry in this.Where(entry => entry.Id == process.Id))
                {
                    i++;
                    entry.StartTime = process.StartTime;
                    entry.StillRunning = stillRunning;
                    break;
                }

                if (i == 0)
                {
                    var item = new RecordedGame();

                    item.Id = process.Id;
                    item.GameName = process.ProcessName;
                    item.StartTime = process.StartTime;
                    item.StillRunning = stillRunning;

                    item.MainWindowTitle = process.MainWindowTitle.ToString();

                    base.Add(item);
                }

            }
            catch (InvalidOperationException ix)
            {
                
            }
        }
    }
}
