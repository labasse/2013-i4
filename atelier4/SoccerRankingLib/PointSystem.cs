using System;

namespace SoccerRankingLib
{
    public abstract class PointSystem
    {
        public interface ITotal : IComparable
        {
            void Increment(ITotal with);
            string ToString();
        }
        public abstract ITotal InitialPoints { get; }
        public abstract ITotal GetPointsFromMatch(Match m, bool isHome);
    }
}
