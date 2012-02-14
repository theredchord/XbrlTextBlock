using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using FileHelpers;

namespace XbrlTextBlock
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creates new FileHelperEngine to parse csv file
            FileHelperEngine engine = new FileHelperEngine(typeof(XbrlUrl));
            XbrlUrl[] xbrlUrls = engine.ReadFile("XbrlInstanceDocs.csv") as XbrlUrl[];

            Reader reader = new Reader();
            string fileId = null;

            //Iterate through each xml url, strip out enclosing quotations and read xml file contents for tags
            //and writes contents to new xml file.
            foreach (var url in xbrlUrls)
            {
                fileId = url.Id.Replace("\"", "").Replace("\\", "");
                var bareUrl = url.XmlUrl.Replace("\"", "");
                reader.ReadXml(bareUrl, fileId);
                break;
            }

            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}
