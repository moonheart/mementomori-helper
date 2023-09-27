using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori
{
    public class TimeManager
    {
        public TimeSpan DiffFromUtc => TimeSpan.Parse(_timeServerMb.DifferenceDateTimeFromUtc);

        private TimeServerMB _timeServerMb;

        public void SetTimeServerMb(TimeServerMB timeServerMb)
        {
            _timeServerMb = timeServerMb;
        }
    }
}
