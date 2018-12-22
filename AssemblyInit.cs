using System.IO;
using System.Reflection;
using NUnit.Framework;
using tiver_cockatiel.Logging;

namespace tiver_cockatiel
{
    [SetUpFixture]
    public class AssemblyInit
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Logger.Configure();
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            var reportContent = Report.Build();
            File.WriteAllText(Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "",
                $"./output/{Context.LogDatetime}_report.html"), reportContent);
        }
    }
}