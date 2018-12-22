using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using HandlebarsDotNet;
using Newtonsoft.Json.Linq;

namespace tiver_cockatiel
{
    public class Report
    {
        public static string Build()
        {
            var template = Handlebars.Compile(ReadTemplateFile("./Templates/simple.hbs"));

            var logFilepath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "",
                Context.LogFilepath);
            try
            {
                var log = ReadLogFile(logFilepath);
                var temp = log.GroupBy(l => $"{l["Properties"]["TestId"].Value<string>()} - {l["Properties"]["TestName"].Value<string>()}");
                var data = temp.ToDictionary(l => l.Key);

                return template(new Dictionary<string,object>()
                {
                    {"tests", data}
                });
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string ReadTemplateFile(string templateFilepath)
        {
            return File.ReadAllText(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "",
                    templateFilepath));
        }
        
        protected static List<JObject> ReadLogFile(string logFileFilepath)
        {
            var lines = new List<JObject>();
            using (var fileStream = new FileStream(logFileFilepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(fileStream, Encoding.Default))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines.Add(JObject.Parse(line));
                }
            }

            return lines;
        }
    }
}