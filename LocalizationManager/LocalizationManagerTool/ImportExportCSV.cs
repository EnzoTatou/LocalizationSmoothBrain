using System.IO;

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

            // put

            return content;
        }

        public void ExportCSV(List<string> content, string _path)
        {
            using (var writer = new StreamWriter(_path))
            {
                foreach (var item in content)
                {
                    writer.WriteLine(item);
                }
            }
        }
    }
}
