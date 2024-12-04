using AOC.Common.Services;

namespace AOC.App2024.Resolvers.Day2
{
    public class ResolverDay2Part1 : IAdventServiceResolver
    {
        public int Resolve(List<string> data)
        {
            var safeCount = 0;
            foreach (var line in data)
            {
                var inputs = line.Split(" ").Select(i => Convert.ToInt32(i)).ToList();

                if (inputs[0] == inputs[1]) continue;

                var isIncreasing = inputs[0] < inputs[1];
                var isSafe = true;
                for (var cpt = 0; cpt < inputs.Count - 1; cpt++)
                {
                    var current = inputs[cpt];
                    var next = inputs[cpt + 1];

                    if (isIncreasing)
                    {
                        if (current > next)
                        {
                            isSafe = false;
                            break;
                        }
                    }
                    else
                    {
                        if (current < next)
                        {
                            isSafe = false;
                            break;
                        }
                    }

                    var diff = Math.Abs(next - current);
                    if (diff == 0 || diff > 3)
                    {
                        isSafe = false;
                        break;
                    }
                }
                if (isSafe) safeCount++;
            }
            return safeCount;
        }
    }
}
