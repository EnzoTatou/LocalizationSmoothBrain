using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {

        public List<string> ImportCSV(string _path)
        {
            List<string> content = new();

            using (var reader = new StreamReader(_path))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (line != null)
                    {
                        var values = line.Split(';');

                        foreach (var v in values)
                        {
                            content.Add(v);
                        }
                    }
                }
            }

            return content;
        }
    }
}
