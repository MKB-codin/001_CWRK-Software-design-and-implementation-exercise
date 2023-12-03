using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SED_Coursework
{
    class XMLReaderz
    {
        public void XMLReaderx()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("data.xml");
            foreach(XmlNode node in xmlDoc.DocumentElement)
            {
                int id = int.Parse(node["id"].InnerText);
                Console.WriteLine(id);
            }
            
        }
        
    }
}
