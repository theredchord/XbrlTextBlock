using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;

namespace XbrlTextBlock
{
    [DelimitedRecord(",")]
    public class XbrlUrl
    {
        public string Id;
        public string XmlUrl;
    }
}
