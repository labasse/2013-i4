using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoccerRankingLib
{
    public class Ranking
    {
        private class RankingEntry : IComparable
        {
            private Club club;
            private PointSystem.ITotal points;

            public RankingEntry(Club club, PointSystem.ITotal points)
            {
                this.club = club;
                this.points = points;
            }
            public PointSystem.ITotal Points
            {
                get { return this.points; }
            }
            public Club Club
            {
                get { return this.club; }
            }

            #region IComparable Membres

            public int CompareTo(object obj)
            {
                return -this.points.CompareTo(((RankingEntry)obj).Points);
            }

            #endregion
        }
        private PointSystem system;
        private RankingEntry[] entries;

        public Ranking(PointSystem system, Club [] clubs)
        {
            this.system = system;
            this.entries=new RankingEntry[clubs.Length];
            for(int i=0; i<clubs.Length; i++)
                this.entries[i]=new RankingEntry(clubs[i], system.InitialPoints);
        }
        private RankingEntry EntryFromClub(Club c)
        {
            foreach (RankingEntry entry in entries)
                if (entry.Club == c)
                    return entry;
            return null;
        }
        public class MatchRegistrationEventArgs : EventArgs
        {
            public Match NewMatch;
        }
        public event EventHandler<MatchRegistrationEventArgs> NewMatchRegistered;

        public void Register(Match m)
        {
            EntryFromClub(m.Home).Points.Increment(system.GetPointsFromMatch(m, true));
            EntryFromClub(m.Away).Points.Increment(system.GetPointsFromMatch(m, false));
            Array.Sort(entries);
            if (NewMatchRegistered != null)
                NewMatchRegistered(this, new MatchRegistrationEventArgs() { NewMatch = m });
        }
        public Club GetClub(int index)
        {
            return entries[index].Club;
        }
        public PointSystem.ITotal GetPoints(int index)
        {
            return entries[index].Points;
        }
        public PointSystem.ITotal GetPoints(Club club)
        {
            return EntryFromClub(club).Points;
        }
        public int Size
        {
            get { return entries.Length; }
        }
    }
}
