using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoccerRankingLib
{
    public class Club
    {
        private string name;

        public Club(string name)
        {
            this.name = name;
        }
        public override string ToString()
        {
            return this.name;
        }
    }
}
