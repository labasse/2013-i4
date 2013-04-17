using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoccerRankingLib;
using SoccerRankingLib.France.League1;

namespace TestRanking
{
    class Program
    {
        static void Main(string[] args)
        {
            Club fcgb=new Club("FCGB");
            Club psg =new Club("PSG");
            Ranking ranking = new Ranking(FrenchLeague1PointSystem.Instance, new Club[] { fcgb, psg });

            ranking.Register(new Match(fcgb, psg, 2, 0));
            ranking.Register(new Match(psg, fcgb, 3, 0));
            for (int i = 0; i < 2; i++)
                Console.WriteLine("{0} : {1}", ranking.GetClub(i), ranking.GetPoints(i));
        }
    }
}
