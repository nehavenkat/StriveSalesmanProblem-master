using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StriveSalesmanProblem
{
    /// <summary>
    /// We provide a simple solver that uses recursion and a brute force approach
    /// to solve the Traveling Salesman Problem
    /// </summary>
    class TspSolver
    {
        const int INF = int.MaxValue;

        #region Branch-and-bound lesson example
        /*int[][] _distances = new int[][]
            {
                new int[] { INF, 10, 8, 9, 7 },
                new int[] { 10, INF, 10, 5, 6 },
                new int[] { 8, 10, INF, 8, 9 },
                new int[] { 9, 5, 8, INF, 8, 9 },
                new int[] { 7, 6, 9, 6, INF }
            };*/
        #endregion Branch-and-bound lesson example

        public static int MaxCitiesAllowed { get { return _distances.Length; } }

        static int[][] _distances = new int[][] {
                                        new [] { INF, 29, 82, 46, 68, 52, 72, 42, 51, 55, 29, 74, 23, 72, 46 },
                                        new [] { 29, INF, 55, 46, 42, 43, 43, 23, 23, 31, 41, 51, 11, 52, 21 },
                                        new [] { 82, 55, INF, 68, 46, 55, 23, 43, 41, 29, 79, 21, 64, 31, 51 },
                                        new [] { 46, 46, 68, INF, 82, 15, 72, 31, 62, 42, 21, 51, 51, 43, 64 },
                                        new [] { 68, 42, 46, 82, INF, 74, 23, 52, 21, 46, 82, 58, 46, 65, 23 },
                                        new [] { 52, 43, 55, 15, 74, INF, 61, 23, 55, 31, 33, 37, 51, 29, 59 },
                                        new [] { 72, 43, 23, 72, 23, 61, INF, 42, 23, 31, 77, 37, 51, 46, 33 },
                                        new [] { 42, 23, 43, 31, 52, 23, 42, INF, 33, 15, 37, 33, 33, 31, 37 },
                                        new [] { 51, 23, 41, 62, 21, 55, 23, 33, INF, 29, 62, 46, 29, 51, 11 },
                                        new [] { 55, 31, 29, 42, 46, 31, 31, 15, 29, INF, 51, 21, 41, 23, 37 },
                                        new [] { 29, 41, 79, 21, 82, 33, 77, 37, 62, 51, INF, 65, 42, 59, 61 },
                                        new [] { 74, 51, 21, 51, 58, 37, 37, 33, 46, 21, 65, INF, 61, 11, 55 },
                                        new [] { 23, 11, 64, 51, 46, 51, 51, 33, 29, 41, 42, 61, INF, 62, 23 },
                                        new [] { 72, 52, 31, 43, 65, 29, 46, 31, 51, 23, 59, 11, 62, INF, 59 },
                                        new [] { 46, 21, 51, 64, 23, 59, 33, 37, 11, 37, 61, 55, 23, 59, INF }};

        public int NumberOfCitiesConsidered { get; private set; }

        public TspSolver(int numberOfCities)
        {
            // Set a limit to the complexity of our calculation 
            NumberOfCitiesConsidered = numberOfCities;
        }

        public Path Solve()
        {
            Path solution = new Path();
            solution.CitiesVisited.Add(0);
            solution = FindBetterSubPath(solution);
            solution.TotalCost += _distances[solution.LastVisitedNode][0];
            solution.CitiesVisited.Add(0);

            return solution;
        }

        private Path FindBetterSubPath(Path currentPath)
        {
            int cityToLeaveFrom = currentPath.LastVisitedNode;

            Path bestSolution = currentPath;

            for (int i = 0; i < NumberOfCitiesConsidered; i++) // Ask to each of our children...
            {
                if (currentPath.CitiesVisited.Contains(i))  // ...excluding cities I already visited...
                    continue;

                Path extendedPath = new Path(currentPath);  // ...to try extending the current path in their direction...
                extendedPath.CitiesVisited.Add(i);  // ...by visiting them...
                extendedPath.TotalCost += _distances[cityToLeaveFrom][i];   // ...and considering the direct cost to travel there...

                extendedPath = FindBetterSubPath(extendedPath); // ...plus the cost of the most cheap option they have to reach the end or our journey.

                if ((bestSolution != currentPath) && (bestSolution.TotalCost < extendedPath.TotalCost)) // If we already have a better solution for this subpath...
                    continue;   // ...we discard it...

                bestSolution = extendedPath;    // ...otherwise, it will become our current best local solution for this subpath.
            }

            Console.Write(".");

            return bestSolution;
        }
    }

    /// <summary>
    /// Handy class to hold a list of visited cities and the total cost to go though that path
    /// </summary>
    class Path
    {
        public int TotalCost;
        public List<int> CitiesVisited;

        public int LastVisitedNode
        {
            get
            {
                return CitiesVisited.Last();
            }
        }

        public Path(Path currentPathToCopyFrom = null)
        {
            if (currentPathToCopyFrom != null)
            {
                CitiesVisited = new List<int>(currentPathToCopyFrom.CitiesVisited);
                TotalCost = currentPathToCopyFrom.TotalCost;
            }
            else
                CitiesVisited = new List<int>();
        }

        public string Description
        {
            get
            {
                return $"Cost: {TotalCost}, order: { string.Join(" -> ", CitiesVisited) }";
            }
        }
    }
}
