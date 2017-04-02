using System;
using System.Text;

namespace Wzl.Src
{
    class Wzl : IWzl
    {
        protected int NumPerGeneration { get; set; }
        protected int MutationChancePercent { get; set; }
        protected string Set { get; set; }
        protected string Goal { get; set; }
        private Random _rand;

        public Wzl(int numPerGeneration, int mutationChancePercent, string set, string goal, Random rand)
        {
            NumPerGeneration = numPerGeneration;
            MutationChancePercent = mutationChancePercent;
            Set = set;
            Goal = goal;
            _rand = rand;
        }

        public string[] CreateNewGeneration(string parent)
        {
            string[] children = new string[NumPerGeneration];

            for(int i = 0; i < NumPerGeneration; i++)
            {
                children[i] = parent;
            }

            return children;
        }

        public string CreateWeasel(int length)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < length; i++)
            {
                sb.Append(Set[_rand.Next(0, Set.Length - 1)]);
            }

            return sb.ToString();
        }

        public string Advanceweasel(string weasel)
        {
            char[] weaselChars = weasel.ToCharArray();

            for(int i = 0; i < weaselChars.Length; i++)
            {
                int randNum = _rand.Next(0, 99);
                if(randNum < MutationChancePercent)
                {
                    weaselChars[i] = Set[_rand.Next(0, Set.Length)];
                }
            }

            return new string(weaselChars);
        }

        public string GetSoleSurvivorOfNewGeneration(string[] weasels)
        {
            string bestWeasel = weasels[0];
            int bestScore = 0;

            for(int i = 0; i < NumPerGeneration; i++)
            {
                int score = GetGoalLikenessScore(weasels[i]);

                if(score > bestScore)
                {
                    bestScore = score;
                    bestWeasel = weasels[i];
                }
            }

            return bestWeasel;
        }

        public int GetGoalLikenessScore(string weasel)
        {
            int score = 0;

            for (int i = 0; i < weasel.Length; i++)
            {
                if (weasel[i] == Goal[i])
                    score++;
            }

            return score;
        }
    }
}
