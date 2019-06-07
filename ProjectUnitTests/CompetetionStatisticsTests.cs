using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Updates;
using webapi.Logic_Layer;

namespace ProjectUnitTests
{

    [TestClass]
    public class CompetetionStatisticsTests
    {

        CompetitionsStatisticsLogic CSL;
        [TestInitialize]
        public void TestInitialize()
        {
            CSL = new CompetitionsStatisticsLogic();
        }

        [TestMethod]
        public void TestGetCompetitionsFromUpdateLogic()
        {
            CPlayerUpdate pa = CreateCPlayerUpdate();
            HashSet<KeyValuePair<string, int>> comps = CSL.GetCompetitionsFromUpdate(pa.Statistics);
            Assert.AreEqual(3, comps.Count);
            Assert.IsTrue(comps.Contains(new KeyValuePair<string, int>("LigaBBVA", 2019)));
            Assert.IsTrue(comps.Contains(new KeyValuePair<string, int>("LigaBBVA", 2018)));
            Assert.IsTrue(comps.Contains(new KeyValuePair<string, int>("BundesLeague", 2019)));

        }

        [TestMethod]
        public void TestGenerateUpdatesLogic()
        {
            CPlayerUpdate pa = CreateCPlayerUpdate();
            CompetitionStatistics cs = CreateCompetitionStatistics();
            List<KeyValuePair<KeyValuePair<string, int>, Func<DataAccess.CompetitionStatistics, bool>>> comps = CSL.GenerateUpdates(pa.Statistics);
            Assert.AreEqual(4, comps.Count);
            foreach(KeyValuePair<KeyValuePair<string, int>, Func<DataAccess.CompetitionStatistics, bool>> update in comps)
            {
                update.Value.Invoke(cs);
            }
            Assert.AreEqual(cs.Yellow_Cards, 9);
            Assert.AreEqual(cs.Assists, 2);
        }

        private CompetitionStatistics CreateCompetitionStatistics()
        {
            return new CompetitionStatistics()
            {
                Appearance = 0,
                Assists = 0,
                Goals = 0,
                Red_cards = 0,
                Yellow_Cards = 0
            };
        }
        private static CPlayerUpdate CreateCPlayerUpdate()
        {
            ParsableValue<string> spain = new ParsableValue<string>("LigaBBVA");
            spain.Parse(spain.Value);
            ParsableValue<string> league = new ParsableValue<string>("BundesLeague");
            league.Parse(league.Value);
            CPlayerUpdate pa = new CPlayerUpdate()
            {
                Competition = "LigaBBVA",
                DOB = DateTime.Now,
                Competitor = "BBB",
                FormationPosition = 11,
                Injury = new CAthleteInjuryUpdate()
                {
                    Active = true,
                    AddedToGames = new List<long>(),
                    ExceptedReturn = "expected return",
                    LastUpdate = DateTime.Now,
                    Reason = "reason",
                    RemovedFromGames = new List<long>(),
                    StartDate = DateTime.Now
                },
                Suspension = new CAthleteSuspensionUpdate()
                {
                    Active = false,
                    StartDate = DateTime.Now,
                    Competition = "comp",
                    Country = "country",
                    GamesBannedLeft = 1,
                    GamesCount = 5,
                    SuspensionType = 5
                },
                JerseyNum = 55,
                Message = "hey!",
                Name = "Daddy",
                Nationality = "German",
                Position = 15,
                Statistics = new List<CAthleteStatisticsUpdate>()
                {
                     new CAthleteStatisticsUpdate()
                    {
                        Competition = spain,
                        Country = new ParsableValue<int>(6),
                        Season = new ParsableValue<int>(2019),

                        Stats = new List<CPlayerIndividualStat>()
                        {
                            new CPlayerIndividualStat()
                            {
                                StatisticType = 3,
                                Value = "9"
                            },
                        }
                    },
                    new CAthleteStatisticsUpdate()
                    {
                        Competition = spain,
                        Country = new ParsableValue<int>(6),
                        Season = new ParsableValue<int>(2018),

                        Stats = new List<CPlayerIndividualStat>()
                        {
                            new CPlayerIndividualStat()
                            {
                                StatisticType = 3,
                                Value = "9"
                            },
                        }
                    },
                     new CAthleteStatisticsUpdate()
                    {
                        Competition = league,
                        Country = new ParsableValue<int>(6),
                        Season = new ParsableValue<int>(2019),

                        Stats = new List<CPlayerIndividualStat>()
                        {
                            new CPlayerIndividualStat()
                            {
                                StatisticType = 3,
                                Value = "9"
                            },
                             new CPlayerIndividualStat()
                            {
                                StatisticType = 2,
                                Value = "2"
                            },
                        }
                    }
                }
            };
            return pa;
        }
    }
}
