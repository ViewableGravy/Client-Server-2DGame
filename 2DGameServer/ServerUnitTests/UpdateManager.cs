using System;
using System.Diagnostics;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace _2DGameServer {

    namespace ServerUnitTests
    {
        [TestClass]
        public class UpdateManagerTest
        {
            [TestMethod]
            public void PrepareClientUpdate()
            {
                World world = new World();

                Player temp = new Player(new SessionCredentials("7c9e6679-7425-40de-944b-e07fc1f90ae7",
                                                                    "ViewableGravy",
                                                                    "Test"), Dns.GetHostByName(Dns.GetHostName()).AddressList[0], world, 0);
                UpdateManager UM = new UpdateManager(temp, world);

                var stopwatch = Stopwatch.StartNew();

                UM.PrepareClientUpdate(world);

                stopwatch.Stop();

                var elapsed = stopwatch.Elapsed;
                Console.WriteLine(elapsed);


            }

            [TestMethod]
            public void test()
            {
                Assert.IsTrue(true);
            }
        }
    }
}
