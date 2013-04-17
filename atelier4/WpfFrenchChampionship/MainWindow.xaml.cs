using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SoccerRankingLib;
using SoccerRankingLib.France.League1;

namespace WpfFrenchChampionship
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string[] clubnames ={
                "AC Ajaccio",
                "AS Nancy-Lorraine" ,
                "AS Saint-Etienne",
                "ESTAC Troyes",
                "Evian TG FC",
                "FC Lorient",
                "FC Sochaux-Montbéliard",
                "Girondins de Bordeaux",
                "LOSC Lille",
                "Montpellier Hérault SC",
                "OGC Nice",
                "Olympique de Marseille",
                "Olympique Lyonnais",
                "Paris Saint-Germain",
                "SC Bastia",
                "Stade Brestois 29",
                "Stade de Reims",
                "Stade Rennais FC",
                "Toulouse FC",
                "Valenciennes FC"
            };
            List<Club> clubs = new List<Club>();

            foreach (string clubname in clubnames)
                clubs.Add(new Club(clubname));

            Ranking ranking = new Ranking(new FrenchLeague1PointSystem(), clubs.ToArray());
            
            this.matchEditView.DataContext = new ViewModel.MatchViewModel(clubs, ranking);
            this.rankingView.DataContext = new ViewModel.RankingViewModel(ranking);
        }
    }
}
