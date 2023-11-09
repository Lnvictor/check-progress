using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUtils.Models
{
    public class ConstancyLog
    {
        public readonly Boolean Study;
        public readonly Boolean Exercise;
        public readonly Boolean WorkFocused;

        public ConstancyLog(Boolean study, Boolean exercise, Boolean workFocused) {
            Study = study;
            Exercise = exercise;
            WorkFocused = workFocused;
        }
    }
}
