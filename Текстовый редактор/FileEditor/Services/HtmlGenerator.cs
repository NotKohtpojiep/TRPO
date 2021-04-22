using System;
using System.Linq;

namespace FileEditor.Services
{
    public class HtmlGenerator
    {
        public const string HtmlStart = "<html>";
        public const string HtmlEnd = "</html>";
        public const string HeaderStart = "<header>";
        public const string HeaderEnd = "</header>";
        public const string BodyStart = "<body>";
        public const string BodyEnd = "</body>";
        public const string TableStart = "<table style=\"border-spacing: 60px 3px;\">";
        public const string TableEnd = "</table>";
        public const string TrStart = "<tr>";
        public const string TrEnd = "</tr>";
        public const string TdStart = "<td>";
        public const string TdEnd = "</td>";

        public HtmlGenerator()
        {
        }

        public string ConvertToHtml(string content)
        {
            if (content != null)
            {
                content.Replace("\r", "");
                content = string.Join("",
                    content.Split("\t", StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => $"{TdStart} {x} {TdEnd}").ToArray());
                content = string.Join("", content.Split("\n")
                        .Select(x => $"{TdEnd}\n{TrStart}\n{TdStart} {x} {TrEnd}").ToArray());
                content = $"{TableStart}\n{content.Remove(0, 15)}\n{TableEnd}";
            }
            string result = string.Join('\n', new string[]
                { HtmlStart, HeaderStart, HeaderEnd, BodyStart, content, BodyEnd, HtmlEnd });
            return result;
        }
    }
}
