namespace AOC.App2024.services
{
    public static class AdventServiceDay1
    {
        public static void ResolveDay1Part1()
        {
            var data = File.ReadAllLines(@".\data\day_1.txt");

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
        }

        public static void ResolveDay1Part2()
        {
            var data = File.ReadAllLines(@".\data\day_1.txt");

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
                similarityScore += (currentItem1 * matchCount);
            }
            Console.WriteLine($"Similarity score: {similarityScore}");
        }
    }
}
