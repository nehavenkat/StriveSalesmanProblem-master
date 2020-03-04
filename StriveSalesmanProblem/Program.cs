using System;
using System.Diagnostics;

namespace StriveSalesmanProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(@"
Welcome to TSP and Knapsack solver.
Please make your choice:
1. Traveling Salesman Problem
2. Knapsack Problem
3. Exit");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SolveTsp();
                        break;
                    case "2":
                        SolveKnapsack();
                        break;
                    case "3": return;
                }
            }
        }

        private static void SolveTsp()
        {
            int maxCities;
            while (true)
            {
                Console.WriteLine($"Please enter its max complexity (3 to {TspSolver.MaxCitiesAllowed })");
                string choice = Console.ReadLine();

                if (int.TryParse(choice, out maxCities) && (maxCities <= TspSolver.MaxCitiesAllowed) && (maxCities > 2))
                    break;               
            }

            Console.WriteLine($"Solving your Traveling Salesman Problem with {maxCities} cities..");

            TspSolver solver = new TspSolver(maxCities);
            Stopwatch timer = new Stopwatch();
            timer.Start();

            Path optimalSolution = solver.Solve();

            timer.Stop();

            Console.WriteLine($"\nOptimal solution found for {solver.NumberOfCitiesConsidered} cities:\n{optimalSolution.Description}");

            Console.WriteLine($"That took {timer.ElapsedMilliseconds:#,0}ms");
        }

        private static void SolveKnapsack()
        {
            Console.WriteLine("Solving your Knapsack problem..");

            KnapsackSolver solver = new KnapsackSolver();
            Stopwatch timer = new Stopwatch();
            timer.Start();

            KnapsackContent optimalSolution = solver.Solve();

            timer.Stop();

            Console.WriteLine($"\nOptimal solution found for Knapsack:\n{optimalSolution.Description}");

            Console.WriteLine($"That took {timer.ElapsedMilliseconds:#,0}ms");
        }
    }
}
