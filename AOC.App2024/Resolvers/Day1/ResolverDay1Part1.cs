using AOC.Common.Services;

namespace AOC.App2024.Resolvers.Day1
{
    public class ResolverDay1Part1 : IAdventServiceResolver
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

            items1 = items1.OrderBy(i => i).ToList();
            items2 = items2.OrderBy(i => i).ToList();

            var totalDiff = 0;
            for (var cpt = 0; cpt < items1.Count; cpt++)
            {
                var currentItem1 = items1[cpt];
                var currentItem2 = items2[cpt];

                var diff = Math.Abs(currentItem1 - currentItem2);
                totalDiff += diff;
            }

            Console.WriteLine($"Total diff: {totalDiff}");
            return totalDiff;
        }
    }
}
