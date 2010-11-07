using System;
using NUnit.Framework;

namespace ChessCalendar.TestHarness
{
    [TestFixture]
    public class Class1
    {

        [Test]
        public void Test()
        {
            var x = DateTime.Parse("Sun, 07 Nov 2010 10:33:37 -0800");
            var y = x.ToString();
        }
    }
}
