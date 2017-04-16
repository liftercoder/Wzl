using System;
using System.Diagnostics;
using Wzl.Src;

namespace Wzl
{
    class WzlProgram
    {
        const int numPerGeneration = 100,
                  muntationChancePercent = 5;

        const string set = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ",
                     goal = "METHINKS IT IS LIKE A WEASEL";

        static Stopwatch _sw = new Stopwatch();
        static Random _rand = new Random();

        static IWzl _wzl = new Src.Wzl(numPerGeneration, muntationChancePercent, set, goal, _rand);

        static void Main(string[] args)
        {
            _sw.Start();

            string weasel = _wzl.CreateWeasel(goal.Length);
            int gen = 1;

            while (true)
            {
                Print(string.Format("{0}: {1} ({2})", gen, weasel, _wzl.GetGoalLikenessScore(weasel)));

                if (weasel == goal)
                    break;

                string[] newGeneration = _wzl.CreateNewGeneration(weasel);

                for (int i = 0; i < numPerGeneration; i++)
                {
                    newGeneration[i] = _wzl.AdvanceWeasel(newGeneration[i]);
                }

                weasel = _wzl.GetSoleSurvivorOfNewGeneration(newGeneration);

                gen++;
            }

            _sw.Stop();

            Print(string.Format("Took {0} ms", _sw.ElapsedMilliseconds));
            Print(string.Format("Total generations: {0}", gen));
            Print(string.Format("Total population size: {0}", gen * numPerGeneration + 1));

            Console.ReadKey();
        }

        static void Print(object s)
        {
            Console.WriteLine(s);
        }
    }
}
