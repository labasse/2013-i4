using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Collections.ObjectModel;
using SoccerRankingLib;


namespace WpfFrenchChampionship.ViewModel
{
    public class MatchListViewModel : ViewModel
    {
        private ObservableCollection<Match> matches;

        public MatchListViewModel(Ranking ranking)
        {
            matches = new ObservableCollection<Match>();
            ranking.NewMatchRegistered += new EventHandler<Ranking.MatchRegistrationEventArgs>(ranking_NewMatchRegistered);
        }

        private void ranking_NewMatchRegistered(object sender, Ranking.MatchRegistrationEventArgs e)
        {
            matches.Add(e.NewMatch);
        }

        public INotifyCollectionChanged Matches
        {
            get { return matches; }
        }
    }
}
