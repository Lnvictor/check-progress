using DatabaseUtils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUtils.Utils
{
    public  class FileReader
    {
        private const string FilesBasePath = "C:\\Check_Progress";
        public static ConstancyLog ReadDayFromFile(DateTime dateTime)
        {
            string fullPath = $"{FilesBasePath}\\{dateTime.Year}{dateTime.Month}{dateTime.Day}.txt";
            if (!File.Exists(fullPath))
            {
                return new ConstancyLog(false, false, false);
            }

            using (StreamReader reader = new StreamReader(fullPath))
            {
                string[] lines = reader.ReadToEnd().Split("\n");
                bool exercise = Boolean.Parse(lines[0]);
                bool study = Boolean.Parse(lines[1]);
                bool workFocused = Boolean.Parse(lines[2]);

                return new ConstancyLog(study, exercise, workFocused);
            }
        }
    }
}
