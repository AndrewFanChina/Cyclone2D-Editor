using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Cyclone.Cyclone.alg.util
{
    class XMLOpt
    {
        public static XmlNode CreateNode(XmlDocument doc, XmlNode parent, String nodeName, String innerText)
        {
            if (doc == null || nodeName == null || nodeName.Length==0)
            {
                return null;
            }
            XmlNode node = doc.CreateNode(XmlNodeType.Element, nodeName, null);
            if (node != null)
            {
                if (innerText != null)
                {
                    node.InnerText = innerText;
                }
                if (parent != null)
                {
                    parent.AppendChild(node);
                }
            }
            return node;
        }

        public static XmlNode CreatePlist(XmlDocument doc)
        {
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(xmldecl);
            XmlDocumentType docType = doc.CreateDocumentType("plist", "-//Apple Computer//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
            doc.AppendChild(docType);

            XmlNode xml_plist = XMLOpt.CreateNode(doc, doc, "plist", null);
            XmlAttribute plistVersion = doc.CreateAttribute("version");
            plistVersion.Value = "1.0";
            xml_plist.Attributes.Append(plistVersion);
            return xml_plist;
        }
    }
}
