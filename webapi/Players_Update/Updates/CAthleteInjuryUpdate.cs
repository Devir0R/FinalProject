﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updates
{
    public class CAthleteInjuryUpdate
    {
        public CAthleteInjuryUpdate()
        {
            Active = true;
        }

        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public string ExceptedReturn { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        public IList<long> AddedToGames { get; set; }
        public IList<long> RemovedFromGames { get; set; }

        public void AddUpdatedGame(long GameID)
        {
            if (AddedToGames == null)
            {
                AddedToGames = new List<long>();
            }
            AddedToGames.Add(GameID);
        }
        public void AddRemovedGame(long GameID)
        {
            if (RemovedFromGames == null)
            {
                RemovedFromGames = new List<long>();
            }
            RemovedFromGames.Add(GameID);
        }
    }
}
