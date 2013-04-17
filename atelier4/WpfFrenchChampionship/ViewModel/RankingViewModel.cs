using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoccerRankingLib;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WpfFrenchChampionship.ViewModel
{
    public class RankingViewModel : ViewModel//, INotifyCollectionChanged
    {
        public struct RankedClub
        {
            public int Rank { get; set; }
            public Club Club { get; set; }
            public string Total { get; set; }
        }
        private Ranking _ranking;
        
        public RankingViewModel(Ranking ranking)
        {
            this._ranking = ranking;
            ranking.NewMatchRegistered += new EventHandler<Ranking.MatchRegistrationEventArgs>(ranking_NewMatchRegistered);
        }

        private void ranking_NewMatchRegistered(object sender, Ranking.MatchRegistrationEventArgs e)
        {
            
            RaisePropertyChanged("RankedClubs");
            /*if(CollectionChanged!=null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));*/
        }

        public IEnumerable<RankedClub> RankedClubs
        {
            get
            { 
                for(int i=0; i<this._ranking.Size; i++)
                    yield return new RankedClub()
                    {
                        Rank = i + 1,
                        Club = this._ranking.GetClub(i),
                        Total = this._ranking.GetPoints(i).ToString()
                    };
            }
        }

        //public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
