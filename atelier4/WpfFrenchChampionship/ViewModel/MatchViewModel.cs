using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SoccerRankingLib;
using System.Windows.Input;

namespace WpfFrenchChampionship.ViewModel
{
    public class MatchViewModel : ViewModel
    {
        private Club _home, _away;
        private int _hgoals, _agoals;
        private IEnumerable<Club> _clubs;
        private Ranking _ranking;

        public MatchViewModel(IEnumerable<Club> clubs, Ranking ranking)
        {
            this._clubs = clubs;
            this._home = clubs.ElementAt(0);
            this._away = clubs.ElementAt(1);
            this._ranking = ranking;
            this._hgoals =
            this._agoals = 0;
        }

        public IEnumerable<Club> ClubList
        {
            get { return _clubs; }
        }

        public Club SelectedHome
        {
            get { return _home; }
            set {
                _home = value;
                RaisePropertyChanged("SelectedHome");
            }
        }

        public Club SelectedAway
        {
            get { return _away; }
            set
            {
                _away = value;
                RaisePropertyChanged("SelectedAway");
            }
        }

        public int HomeGoals
        {
            get { return this._hgoals;  }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                this._hgoals = value;
                RaisePropertyChanged("HomeGoals");
            }
        }
        
        public int AwayGoals
        {
            get { return this._agoals; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                this._agoals = value;
                RaisePropertyChanged("AwayGoals");
            }
        }
        
        public ICommand ValidateCommand
        {
            get { return new BasicCommand(delegate(object parameter) { 
                this._ranking.Register(new Match(_home, _away, _hgoals, _agoals));
            }); }
        }
    }
}
