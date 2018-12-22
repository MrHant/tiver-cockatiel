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
    }
}