using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUMPHomework3.Classes
{
    public class Call
    {
        public DateTime DateOfCall { get; set; }
        public string StatusOfCall { get; set; } = "";

        public Call(string statusOfCall)
        {
            DateOfCall = DateTime.Now;
            StatusOfCall = statusOfCall;
        }
        static void TimerCallback()
        {
            var timerFinished = true;
        }
    }
}
