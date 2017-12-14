using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Lab4.BL
{
    public class XmlFilesSearcher
    {
        private readonly int _maxThreadsCount;


        public XmlFilesSearcher(int maxThreadsCount)
        {
            _maxThreadsCount = maxThreadsCount;
        }


        public IDictionary<string, int> Search(Stream[] xmlStreams, string xPathString)
        {
            var xPath = XPath.ParseFrom(xPathString);

            var data = new Dictionary<string, int>();
            Parallel.For(
                0,
                xmlStreams.Length,
                new ParallelOptions { MaxDegreeOfParallelism = _maxThreadsCount },
                i =>
                {
                    var result = SearchOne(xmlStreams[i], xPath);
                    lock (data)
                    {
                        if (data.ContainsKey(result))
                        {
                            data[result]++;
                        }
                        else
                        {
                            data.Add(result, 1);
                        }
                    }
                });

            return data;
        }


        private static string SearchOne(Stream xmlStream, XPath xPath)
        {
            var xmlReader = XmlReader.Create(xmlStream);
            var xPathDocument = new XPathDocument(xmlReader);
            var navigator = xPathDocument.CreateNavigator();

            navigator.MoveToFirstChild();

            if (xPath.Tags.Any(tag => !navigator.MoveToChild(tag, navigator.NamespaceURI)))
            {
                return String.Empty;
            }

            if (!String.IsNullOrEmpty(xPath.Attribute)
                && !navigator.MoveToAttribute(xPath.Attribute, String.Empty))
            {
                return String.Empty;
            }

            return navigator.Value;
        }



        private class XPath
        {
            private readonly List<string> _tags;


            public IEnumerable<string> Tags => _tags;

            public string Attribute { get; }


            private XPath(IEnumerable<string> tags, string attribute)
            {
                _tags = tags.ToList();
                Attribute = attribute;
            }


            public static XPath ParseFrom(string xPath)
            {
                return new XPath(
                    xPath.Split('/').Select(t => t.Split('@').ElementAtOrDefault(0)).Where(t => !String.IsNullOrEmpty(t)),
                    xPath.Split('@').ElementAtOrDefault(1));
            }
        }
    }
}