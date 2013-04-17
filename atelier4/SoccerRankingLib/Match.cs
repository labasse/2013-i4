using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoccerRankingLib
{
    public class Match
    {
        private int?[] goals;
        private Club[] opponents;

        public Match(Club home, Club away, int hgoals, int agoals)
        {
            this.opponents = new Club[] { home, away };
            this.goals = new int?[] { hgoals, agoals };
        }
        public Match(Club home, Club away, bool forfeitIsFromHome)
        {
            this.opponents = new Club[] { home, away };
            this.goals = forfeitIsFromHome
                ? new int?[] { null, 0 }
                : new int?[] { 0, null };
        }
        public bool IsSomeoneForfeit(bool home)
        {
            return this.opponents[home ? 0:1] == null;
        }
        public bool IsHomeForfeit
        {
            get
            {
                return IsSomeoneForfeit(true);
            }
        }
        public bool IsAwayForfeit
        {
            get 
            {
                return IsSomeoneForfeit(false);
            }
        }
        private void CheckForfeit()
        {
            if (IsHomeForfeit || IsAwayForfeit)
                throw new InvalidOperationException("Unable to check a draw (forfeit)");
        }
        public bool IsDraw
        {
            get
            {
                CheckForfeit();
                return this.goals[0] == this.goals[1];
            }
        }
        public int GetGoals(bool forHome)
        { 
            CheckForfeit();
            return goals[forHome ? 0:1].Value;
        }
        public int HomeGoals
        {
            get
            {
                return GetGoals(true);
            }
        }
        public int AwayGoals
        {
            get
            {
                return GetGoals(false);
            }
        }
        public Club GetOpponent(bool home)
        {
            return opponents[home ? 0 : 1];
        }

        public Club Home
        {
            get
            {
                return GetOpponent(true);
            }
        }
        public Club Away
        {
            get
            {
                return GetOpponent(false);
            }
        }
    }
}
