using SoccerRankingLib;

namespace TpSoccerRanking
{
    public class BasicPointSystemMock : PointSystem
    {
        private class TotalMock : ITotal
        {
            private int points;

            public TotalMock()
            {
                this.points = 0;
            }
            public TotalMock(Match m, bool home)
            {
                this.points = m.GetGoals(home) - m.GetGoals(!home);
            }
            public void Increment(PointSystem.ITotal with)
            {
                this.points += ((TotalMock)with).points;
            }
            public int CompareTo(object obj)
            {
                return this.points - ((TotalMock)obj).points;
            }
        }
        public override ITotal InitialPoints
        {
            get { return new TotalMock(); }
        }

        public override ITotal GetPointsFromMatch(Match m, bool isHome)
        {
            return new TotalMock(m, isHome);
        }
    }
}
