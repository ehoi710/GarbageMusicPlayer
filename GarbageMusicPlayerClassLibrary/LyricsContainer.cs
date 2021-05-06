using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GarbageMusicPlayerClassLibrary
{
    public class LyricsContainer
    {
        public static Data DataFromString(string str)
        {
            string time = str.Substring(1, 8);
            string unselectedColorString = str.Substring(11, 8);
            string selectedColorString = str.Substring(20, 8);

            string lyrics;
            if (str.Length < 29)
                lyrics = " ";
            else
                lyrics = str.Substring(29);


            int unselectedColorCode = 0;
            int selectedColorCode = 0;

            foreach(char x in unselectedColorString)
            {
                unselectedColorCode *= 0x10;
                if ('0' <= x && x <= '9') unselectedColorCode += x - '0';
                else unselectedColorCode += (x - 'A' + 10);
            }
            foreach (char x in selectedColorString)
            {
                selectedColorCode *= 0x10;
                if ('0' <= x && x <= '9') selectedColorCode += x - '0';
                else selectedColorCode += x - 'A' + 10;
            }

            return new Data
            {
                time = TimeSpan.Parse(time),
                unselectedColor = Color.FromArgb(unselectedColorCode),
                selectedColor = Color.FromArgb(selectedColorCode),
                str = lyrics
            };
        }

        public struct Data
        {
            public Color unselectedColor;
            public Color selectedColor;
            public TimeSpan time;
            public string str;
        };

        public bool isSync { get; set; }
        public List<Data> data;

        public LyricsContainer(string LyricsString)
        {
            if (LyricsString == null)
            {
                this.isSync = false;
                this.data = null;

                return;
            }

            string[] str = LyricsString.Split('\n');
            if (str.Length == 0)
            {
                this.isSync = false;
                this.data = null;

                return;
            }

            if (str[0].Length >= 7 && str[0].Substring(0, 7) == "GMPSYNC")
            {
                this.isSync = true;

                string[] syncStr = new string[str.Length - 1];

                for (int i = 1; i < str.Length; i++)
                {
                    syncStr[i - 1] = str[i];
                }

                this.data = new List<Data>();

                for(int i = 0; i < syncStr.Length; i++)
                {
                    if (syncStr[i].Length == 0) continue;
                    this.data.Add(DataFromString(syncStr[i]));
                }
            }
            else
            {
                this.isSync = false;
                this.data = new List<Data>();

                for (int i = 0; i < str.Length; i++)
                {
                    LyricsContainer.Data tmp = new Data
                    {
                        unselectedColor = Color.White,
                        selectedColor = Color.Gray,
                        time = TimeSpan.FromSeconds(0),
                        str = str[i]
                    };

                    this.data.Add(tmp);
                }
            }
        }
        public override string ToString()
        {
            string str = null;

            if(this.isSync)
            {
                str += "GMPSYNC\n";
                foreach (Data line in data)
                {
                    str +=
                         "[" + line.time.ToString() + "] " +
                        line.unselectedColor.ToArgb().ToString("X") + " " +
                        line.selectedColor.ToArgb().ToString("X") + " " +
                        line.str + "\n";
                }
            }

            return str;
        }
    }
}
