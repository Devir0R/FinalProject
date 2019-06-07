using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using webapi.Logic_Layer;

namespace ProjectUnitTests
{
    [TestClass]
    public class PlayersTests
    {
        PlayersLogic Pc;
        [TestInitialize]
        public void TestInitialize()
        {
             Pc = new PlayersLogic();
        }

        [TestMethod]
        public void TestCreatePredicateLogic()
        {
            Func<Players, bool> pred = Pc.CreatePredicate("Anna", "Real Madrid", 60, 40, "Israel");
            Assert.IsFalse(pred.Invoke(CreatePlayerA()));
            Assert.IsTrue(pred.Invoke(CreatePlayerB()));
            Assert.IsFalse(pred.Invoke(CreatePlayerC()));
            Assert.IsFalse(pred.Invoke(CreatePlayerD()));

            pred = Pc.CreatePredicate("", "rEAL", 55, 30, "Israel");
            Assert.IsTrue(pred.Invoke(CreatePlayerA()));
            Assert.IsTrue(pred.Invoke(CreatePlayerB()));
            Assert.IsTrue(pred.Invoke(CreatePlayerC()));
            Assert.IsTrue(pred.Invoke(CreatePlayerD()));

            pred = Pc.CreatePredicate("tia", "rid", 65, 40, "is");
            Assert.IsFalse(pred.Invoke(CreatePlayerA()));
            Assert.IsFalse(pred.Invoke(CreatePlayerB()));
            Assert.IsFalse(pred.Invoke(CreatePlayerC()));
            Assert.IsFalse(pred.Invoke(CreatePlayerD()));

        }
        private Players CreatePlayerA()
        {
            return new Players()
            {
                name = "Daddy".ToLower(),
                club = "Real Madrid".ToLower(),
                date_of_birth = new DateTime(DateTime.Now.Year-50, 1, 1),
                nationality = "Israel".ToLower()

            };
        }

        private Players CreatePlayerB()
        {
            return new Players()
            {
                name = "annastasiA".ToLower(),
                club = "Real Madrid".ToLower(),
                date_of_birth = new DateTime(DateTime.Now.Year - 50, 1, 1),
                nationality = "Israel".ToLower()

            };
        }

        private Players CreatePlayerC()
        {
            return new Players()
            {
                name = "annastasi".ToLower(),
                club = "Real Madrid".ToLower(),
                date_of_birth = new DateTime(DateTime.Now.Year - 35, 1, 1),
                nationality = "Israel".ToLower()

            };
        }


        private Players CreatePlayerD()
        {
            return new Players()
            {
                name = "annastasiA".ToLower(),
                club = "Real Madrif".ToLower(),
                date_of_birth = new DateTime(1970, 1, 1),
                nationality = "IsrAEl".ToLower()

            };
        }


    }
}
