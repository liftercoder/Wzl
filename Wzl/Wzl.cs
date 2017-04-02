using System;
using System.Text;
using System.Diagnostics;

namespace Wzl
{
    class Wzl
    {
        static Stopwatch sw = new Stopwatch();
        static Random rand = new Random();

        const int numGenerations = 100,
                  muntationChancePercent = 5;

        const string set = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ",
                     goal = "METHINKS IT IS LIKE A WEASEL";

        static void Main(string[] args)
        {
            sw.Start();

            string weasel = Createweasel(goal.Length);
            int gen = 1;

            while (true)
            {
                Print(string.Format("{0}: {1} ({2})", gen, weasel, GetGoalLikenessScore(weasel)));

                if (weasel == goal)
                    break;

                string[] newGeneration = CreateNewGeneration(weasel, numGenerations);

                for(int i = 0; i < numGenerations; i++)
                {
                    newGeneration[i] = Advanceweasel(newGeneration[i]);
                }

                weasel = GetSoleSurvivorOfNewGeneration(newGeneration);

                gen++;
            }

            sw.Stop();
            Print(string.Format("Took {0} ms", sw.ElapsedMilliseconds));

            Console.ReadKey();
        }

        static string[] CreateNewGeneration(string parent, int numPerGeneration)
        {
            string[] children = new string[numPerGeneration];

            for(int i = 0; i < numPerGeneration; i++)
            {
                children[i] = parent;
            }

            return children;
        }

        static string Createweasel(int length)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < length; i++)
            {
                sb.Append(set[rand.Next(0, set.Length - 1)]);
            }

            return sb.ToString();
        }

        static string Advanceweasel(string weasel)
        {
            char[] weaselChars = weasel.ToCharArray();

            for(int i = 0; i < weaselChars.Length; i++)
            {
                int randNum = rand.Next(0, 99);
                if(randNum < muntationChancePercent)
                {
                    weaselChars[i] = set[rand.Next(0, set.Length)];
                }
            }

            return new string(weaselChars);
        }

        static string GetSoleSurvivorOfNewGeneration(string[] weasels)
        {
            string bestWeasel = weasels[0];
            int bestScore = 0;

            for(int i = 0; i < numGenerations; i++)
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

        static int GetGoalLikenessScore(string weasel)
        {
            int score = 0;

            for (int i = 0; i < weasel.Length; i++)
            {
                if (weasel[i] == goal[i])
                    score++;
            }

            return score;
        }

        static void Print(object s)
        {
            Console.WriteLine(s);
        }
    }
}
