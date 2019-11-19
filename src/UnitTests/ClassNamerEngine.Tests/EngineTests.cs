using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ClassNamerEngine.Tests
{
    [TestFixture]
    public class EngineTests
    {
        [Test]
        public void EngineInitializesAndExecutesHosting()
        {
            Assert.DoesNotThrow(() => Engine.InitializeContainer());

            Task hostingTask = Task.Run(() => Engine.StartHosting());

            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                while (true)
                {
                    if (stopwatch.Elapsed == TimeSpan.FromMinutes(1))
                    {
                        Assert.Fail("Start timeout");
                    }

                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            Assert.IsEmpty(client.DownloadString("http://127.0.0.1:5000/test"));
                            break;
                        }
                        catch (WebException)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        }
                    }
                }
            }
            finally
            {
                Engine.StopHosting();
                hostingTask.Wait();
            }
        }
    }
}