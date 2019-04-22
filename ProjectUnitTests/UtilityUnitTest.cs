using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using webapi.Utilities;

namespace ProjectUnitTests
{
    [TestClass]
    public class UtilityUnitTest
    {
        [TestMethod]
        public void StringDeviceIDToLongTest()
        {
            KeyValuePair<string, long>[] tests_outs = new KeyValuePair<string, long>[5];
            tests_outs[0] = new KeyValuePair<string, long>("EE", 238);
            tests_outs[1] = new KeyValuePair<string, long>("FF", 255);
            tests_outs[2] = new KeyValuePair<string, long>("acdef", 708079);
            tests_outs[3] = new KeyValuePair<string, long>("10FFFFFFA",4563402746);
            tests_outs[4] = new KeyValuePair<string, long>("ab123076f1", 734744573681);
            foreach(KeyValuePair<string,long> test_out in tests_outs)
            {
                Assert.AreEqual(Utilities.StringDeviceIDToLong(test_out.Key), test_out.Value);
            }


        }
    }
}
