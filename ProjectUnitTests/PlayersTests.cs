using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using webapi.Controllers;

namespace ProjectUnitTests
{
    [TestClass]
    public class PlayersTests
    {
        PlayersController Pc;
        [TestInitialize]
        public void TestInitialize()
        {
             Pc = new PlayersController();
        }

    }
}
