using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Volga_IT.Parser
{
    class ParseHtml
    {
        public Stream GetFileStream()
        {
            var dialogFile = new OpenFileDialog
            {
                //Filter = "Html files(*.html)|.html",
                Title = "Выберите html файл"
            };

            if(dialogFile.ShowDialog() == DialogResult.Cancel)
                return null;

            return dialogFile.OpenFile();
        }

        public string[] ParseHtmlOfStream(Stream file)
        {
            using (StreamReader reader = new StreamReader(file))
            {
                string[] fileSeparators = new string[] { "<head>", "</head>" };
                string[] parsedHtml = reader.ReadToEnd()
                    .Split(new string[] { "<div", "\n", "\t", "\r", " ", "\"", "</div>", "</", "«", "<", ">", "script" }, StringSplitOptions.RemoveEmptyEntries);
                return parsedHtml;
            }
        }
    }
}