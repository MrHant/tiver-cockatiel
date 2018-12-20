using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using LumenWorks.Framework.IO.Csv;
using NUnit.Framework;

namespace tiver_cockatiel
{
    [TestFixture]
    public class RequestTests
    {
        [Test]
        [TestCaseSource(nameof(ReadTestFiles))]
        public void SendRequestAndCompareResponse(string url, string requestBody, string responseBody)
        {
            var client = Context.HttpClient;
            var response = client.PostAsync(new Uri(url), new StringContent(requestBody, Encoding.UTF8, "application/json")).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(responseBody, response.Content.ReadAsStringAsync().Result);
        }

        private static IEnumerable<TestCaseData> ReadTestFiles()
        {
            var files = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"./input"));
            foreach (var file in files)
            {
                var csv = new CsvReader(new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")), true);

                while (csv.ReadNextRecord())
                {
                    var testcase = new TestCaseData(csv["post_url"], csv["request_body"], csv["response_body"]);
                    testcase.SetName($"{Path.GetFileName(file)} - {csv["test_details"]}");
                    yield return testcase;
                }
            }
        }
    }
}