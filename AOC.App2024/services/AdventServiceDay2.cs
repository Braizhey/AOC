namespace AOC.App2024.services
{
    public static class AdventServiceDay2
    {
        public static void ResolveDay2Part1()
        {
            var data = File.ReadAllLines(@".\data\day_2.txt");

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
            Console.WriteLine($"Safe count: {safeCount}");
        }

        public static void ResolveDay2Part2()
        {
            var data = File.ReadAllLines(@".\data\day_2.txt");

            var safeCount = 0;
            foreach (var line in data)
            {
                var inputs = line.Split(" ").Select(i => Convert.ToInt32(i)).ToList();

                var cpt = 0;
                var isSafe = IsSafe(ref cpt, inputs);
                if (isSafe) safeCount++;
                else if (!isSafe)
                {
                    var attempt1Inputs = inputs.ToList();
                    attempt1Inputs.RemoveAt(cpt);

                    var attempt2Inputs = inputs.ToList();
                    attempt2Inputs.RemoveAt(cpt + 1);

                    isSafe = IsSafe(ref cpt, attempt1Inputs);
                    if (isSafe) safeCount++;
                    else if (!isSafe)
                    {
                        isSafe = IsSafe(ref cpt, attempt2Inputs);
                        if (isSafe) safeCount++;
                        else
                        {
                            Console.Error.WriteLine($"We're fucked! {inputs.Select(i => i.ToString()).Aggregate((current, next) => $"{current}, {next}")}");
                        }
                    }
                }
            }
            Console.WriteLine($"Safe count with Dampener: {safeCount}");
        }

        private static bool IsSafe(ref int cpt, List<int> inputs)
        {
            var isIncreasing = inputs[0] < inputs[1];
            var isSafe = true;
            for (cpt = 0; cpt < inputs.Count - 1; cpt++)
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
            return isSafe;
        }
    }
}
