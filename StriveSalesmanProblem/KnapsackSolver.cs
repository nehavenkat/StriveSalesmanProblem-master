using System;
using System.Collections.Generic;
using System.Linq;

namespace StriveSalesmanProblem
{
    /// <summary>
    /// We provide a simple solver that uses recursion and a brute force approach
    /// to solve the Knapsack Problem
    /// </summary>
    class KnapsackSolver
    {

        int[] _itemWeights = new int[] { 5, 20, 50, 100, 1, 14, 11, 25 };
        int[] _itemValues = new int[] { 5, 15, 76, 80, 2, 20, 11, 30 };

        public KnapsackContent Solve()
        {

            KnapsackContent solution = new KnapsackContent();
            solution = FindBetterSubOptions(solution);

            return solution;
        }

        private KnapsackContent FindBetterSubOptions(KnapsackContent currentSack)
        {
            if (currentSack.ConsideredItems.Count >= _itemValues.Length)
                return currentSack;

            KnapsackContent bestSolution = currentSack;
            
            foreach (bool objectWillBeTaken in new[] { true, false })   // We consider both the cases in which we take or we leave the object where it is
            {
                KnapsackContent extendedSack = new KnapsackContent(currentSack);
                extendedSack.ConsideredItems.Add(objectWillBeTaken);  // we store the choice of taking this object or not
                if (objectWillBeTaken) // if we take it..
                {
                    extendedSack.TotalWeight += _itemWeights[extendedSack.ConsideredItems.Count - 1]; // we add the weight:

                    if (!extendedSack.IsValid) // if this makes our knapsack explode, we'll ignore this combination
                        continue;

                    extendedSack.TotalValue += _itemValues[extendedSack.ConsideredItems.Count - 1];   // otherwise we also add its value
                }

                extendedSack = FindBetterSubOptions(extendedSack); // we then check the best solution we can have among all the objects we still have to evaluate

                if ((bestSolution != currentSack) && (bestSolution.TotalValue >= extendedSack.TotalValue)) // If we already have a better solution for this initial combination
                    continue;   // ...we discard it...

                bestSolution = extendedSack;    // ...otherwise, it will become our current best local solution.
            }

            Console.Write(".");

            return bestSolution;
        }
    }

    /// <summary>
    /// Handy class to keep track of which items have been taken or not into our Knapsack
    /// </summary>
    class KnapsackContent
    {
        private const int MAX_CAPACITY = 100;

        public int TotalValue;
        public int TotalWeight;

        public List<bool> ConsideredItems;

        public bool IsValid
        {
            get
            {
                return TotalWeight <= MAX_CAPACITY;
            }
        }

        public KnapsackContent(KnapsackContent currentKnapsackToCopyFrom = null)
        {
            if (currentKnapsackToCopyFrom != null)
            {
                ConsideredItems = new List<bool>(currentKnapsackToCopyFrom.ConsideredItems);
                TotalValue = currentKnapsackToCopyFrom.TotalValue;
                TotalWeight = currentKnapsackToCopyFrom.TotalWeight;
            }
            else
                ConsideredItems = new List<bool>();
        }

        public string Description
        {
            get
            {
                return $"Value: {TotalValue}, weight: {TotalWeight}, items: { string.Join("|", ConsideredItems.Select(i => i ? 1 : 0)) }";
            }
        }
    }
}
