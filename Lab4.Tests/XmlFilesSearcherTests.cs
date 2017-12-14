using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lab4.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab4.Tests
{
    [TestClass]
    public class XmlFilesSearcherTests
    {
        [TestMethod]
        public void SearchElementTests()
        {
            var expectedResult = new Dictionary<string, int>
            {
                {String.Empty, 2},
                {"34387618", 1}
            };

            var xmls = GetStreams();
            var searcher = new XmlFilesSearcher(1);
            // ReSharper disable once CoVariantArrayConversion
            var result = searcher.Search(xmls, "docID");

            Assert.AreEqual(expectedResult.ToAssertableString(), result.ToAssertableString());
        }

        [TestMethod]
        public void SearchAttributeTests()
        {
            var expectedResult = new Dictionary<string, int>
            {
                {"score", 1},
                {"date", 2}
            };

            var xmls = GetStreams();
            var searcher = new XmlFilesSearcher(1);
            // ReSharper disable once CoVariantArrayConversion
            var result = searcher.Search(xmls, "sort/@s_c");

            Assert.AreEqual(expectedResult.ToAssertableString(), result.ToAssertableString());
        }


        private static MemoryStream[] GetStreams()
        {
            return new[]
            {
                @"<?xml version=""1.0"" encoding=""utf-8""?><docSearch_dsReq_0_0 userID=""798955"" companyID=""222"" queryType=""normal"" dateRange=""allDates"" infoLevel=""default"" tkrEncoding=""prtID"" calcPrice=""1"" ppv=""both"" acceptLanguage=""en"" synCharsRequired=""0"" exclude3rdParty=""0"" maxRows=""1"" xmlns=""x-schema:mxschema://docsearch""><docID>34387618</docID><sort s_c=""score"" s_d=""desc"" /><excludeCtbs>0</excludeCtbs></docSearch_dsReq_0_0>",
                @"<?xml version=""1.0"" encoding=""utf-8""?><docSearch_dsReq_0_0 userID=""3118158"" companyID=""25256"" queryType=""normal"" dateRange=""last90Days"" tkrEncoding=""prtID"" tkrPrimary=""1"" calcPrice=""0"" acceptLanguage=""en"" synCharsRequired=""0"" exclude3rdParty=""1"" maxRows=""3"" xmlns=""x-schema:mxschema://docsearch""><sort s_c=""date"" s_d=""desc"" /><excludeCtbs>0</excludeCtbs><ctbs>54,767,2,5179,3202,3414,16006,360,25256</ctbs><ticker>100204446</ticker><analystSet>MX#001</analystSet><industrySet>MX#001</industrySet><subjectSet>MX#001</subjectSet><regionSet>MX#001</regionSet><categorySet>MX#001</categorySet><langID>en</langID><matchStr strSrc=""hdln"">""model""</matchStr></docSearch_dsReq_0_0>",
                @"<?xml version=""1.0""?><docSearch_dsReq_0_0 xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" calcPrice=""1"" dateRange=""allDates"" companyID=""184"" userID=""2112"" xmlns=""http://www.schemas.multex.com/docsearch""><sort s_c=""date"" /><excludeCtbs>false</excludeCtbs><ctbs>17846</ctbs><industrySet>MG#10338</industrySet><industry>0606</industry><industry>0609</industry><industry>0612</industry><industry>0133</industry><industry>0909</industry><industry>1209</industry><subjectSet>MX#001</subjectSet><subject>OVER</subject><subject>NOV</subject></docSearch_dsReq_0_0>"
            }.Select(xml => new MemoryStream(Encoding.UTF8.GetBytes(xml))).ToArray();
        }
    }
}
