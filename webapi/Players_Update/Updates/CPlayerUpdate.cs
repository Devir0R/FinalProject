using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Players_Update.Updates
{

    public class CPlayerUpdate
    {
        public CPlayerUpdate(string DataSource)
        {
            Statistics = new List<CAthleteStatisticsUpdate>();
        }

        public CPlayerUpdate()
        {
            Statistics = new List<CAthleteStatisticsUpdate>();
        }

        /// <summary>
        /// The player's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The competition's country
        /// </summary>

        public List<string> Competitions { get; set; }

        /// <summary>
        /// the competitor
        /// </summary>
        public string Club { get; set; }

        /// <summary>
        /// Nationality
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// JerseyNum
        /// </summary>
        public int JerseyNum { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Formation Position
        /// </summary>
        public int FormationPosition { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        public DateTime DOB { get; set; }

        public IList<CAthleteStatisticsUpdate> Statistics { get; set; }

        public CAthleteInjuryUpdate Injury { get; set; }
        public CAthleteSuspensionUpdate Suspension { get; set; }


        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("Athlete Update for {1} of {2} ({3})", "", Name, Club, Competitions);
        }

        public CPlayerUpdate Copy()
        {
            var ath = new CPlayerUpdate()
            {
                Competitions = Competitions,
                Club = Club,
                DOB = DOB,
                FormationPosition = FormationPosition,
                Position = Position,
                Nationality = Nationality,
                JerseyNum = JerseyNum,
                Name = Name,
            };

            return ath;
        }
    }
}