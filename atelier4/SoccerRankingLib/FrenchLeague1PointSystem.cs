using System;
using SoccerRankingLib;

namespace SoccerRankingLib.France.League1
{
    public class FrenchLeague1PointSystem : PointSystem
    {
        private class PointTotal : ITotal
        {
            private int points, goalaverage;

            public PointTotal()
            {
                points = goalaverage = 0;
            }

            public PointTotal(Match m, bool home)
            {
                int me, other;

                if (m.IsSomeoneForfeit(home))
                {
                    me = 0;
                    other = 3;
                }
                else if (m.IsSomeoneForfeit(!home))
                {
                    me = 3;
                    other = 0;
                }
                else
                {
                    me   =m.GetGoals(home); 
                    other=m.GetGoals(!home);
                }
                this.goalaverage = me - other;
                if(me > other)
                    this.points=3;
                else if(me==other)
                    this.points=1;
                else
                    this.points=0;
            }

            #region PointSystem.ITotal Membres

            public void Increment(ITotal with)
            {
                PointTotal other = (PointTotal)with;

                this.points += other.points;
                this.goalaverage += other.goalaverage;
            }

            public override string ToString()
            {
                return String.Format("{0}, {1}", this.points, this.goalaverage);
            }
            #endregion

            #region IComparable Membres

            public int CompareTo(object obj)
            {
                PointTotal other=(PointTotal)obj;

                return this.points != other.points
                    ? this.points - other.points
                    : this.goalaverage - other.goalaverage;
            }

            #endregion
        }

        #region Singleton

        private static FrenchLeague1PointSystem theInstance=new FrenchLeague1PointSystem();

        public static PointSystem Instance
        {
            get { return theInstance; }
        }

        #endregion

        #region PointSystem Membres

        public override PointSystem.ITotal InitialPoints
        {
            get { return new PointTotal(); }
        }

        public override PointSystem.ITotal GetPointsFromMatch(Match m, bool isHome)
        {
            return new PointTotal(m, isHome);
        }

        #endregion
    }
}
