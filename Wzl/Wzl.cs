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

            string weazel = CreateWeazel(goal.Length);
            int gen = 1;

            while (true)
            {
                Print(string.Format("{0}: {1} ({2})", gen, weazel, GetGoalLikenessScore(weazel)));

                if (weazel == goal)
                    break;

                string[] newGeneration = CreateNewGeneration(weazel, numGenerations);

                for(int i = 0; i < numGenerations; i++)
                {
                    newGeneration[i] = AdvanceWeazel(newGeneration[i]);
                }

                weazel = GetSoleSurvivorOfNewGeneration(newGeneration);

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

        static string CreateWeazel(int length)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < length; i++)
            {
                sb.Append(set[rand.Next(0, set.Length - 1)]);
            }

            return sb.ToString();
        }

        static string AdvanceWeazel(string weazel)
        {
            char[] weazelChars = weazel.ToCharArray();

            for(int i = 0; i < weazelChars.Length; i++)
            {
                int randNum = rand.Next(0, 99);
                if(randNum < muntationChancePercent)
                {
                    weazelChars[i] = set[rand.Next(0, set.Length)];
                }
            }

            return new string(weazelChars);
        }

        static string GetSoleSurvivorOfNewGeneration(string[] weazels)
        {
            string bestWeasel = weazels[0];
            int bestScore = 0;

            for(int i = 0; i < numGenerations; i++)
            {
                int score = GetGoalLikenessScore(weazels[i]);

                if(score > bestScore)
                {
                    bestScore = score;
                    bestWeasel = weazels[i];
                }
            }

            return bestWeasel;
        }

        static int GetGoalLikenessScore(string weazel)
        {
            int score = 0;

            for (int i = 0; i < weazel.Length; i++)
            {
                if (weazel[i] == goal[i])
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
