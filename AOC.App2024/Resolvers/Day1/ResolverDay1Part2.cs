using AOC.Common.Services;

namespace AOC.App2024.Resolvers.Day1
{

    public class ResolverDay1Part2 : IAdventServiceResolver
    {
        public int Resolve(List<string> data)
        {
            var items1 = new List<int>();
            var items2 = new List<int>();

            foreach (var line in data)
            {
                var lineItems = line.Split("   ");
                items1.Add(Convert.ToInt32(lineItems[0]));
                items2.Add(Convert.ToInt32(lineItems[1]));
            }

            var similarityScore = 0;
            for (var cpt = 0; cpt < items1.Count; cpt++)
            {
                var currentItem1 = items1[cpt];
                var matchCount = items2.Where(i => i == currentItem1).Count();
                similarityScore += currentItem1 * matchCount;
            }
            Console.WriteLine($"Similarity score: {similarityScore}");
            return similarityScore;
        }
    }
}
