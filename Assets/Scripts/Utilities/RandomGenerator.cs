using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public static class RandomGenerator
    {
        public static List<int> GenerateRandom(int count, int minInclusive, int maxExclusive)
        {

            HashSet<int> candidates = new HashSet<int>();

            for (int i = 0; i < count * 2; i++)
            {
                candidates.Add(UnityEngine.Random.Range(minInclusive, maxExclusive));
                if (candidates.Count >= count) break;
            }

            List<int> result = candidates.ToList();

            return result;
        }

        public static bool CalculateChance(float chance) => UnityEngine.Random.Range(0f, 100f) <= chance;

    }
}