using AOC.Common.Services;

namespace AOC.App2024.Resolvers.Day4
{
    public class ResolverDay4Part2 : IAdventServiceResolver
    {
        public int Resolve(List<string> data)
        {
            var patterns = new List<Pattern>();
            for (var cptRow = 0; cptRow < data.Count - 2; cptRow++)
            {
                for (var cptCol = 0; cptCol < data.Count - 2; cptCol++)
                {
                    var pattern = new Pattern
                    {
                        StartX = cptRow,
                        StartY = cptCol,
                    };
                    var topLine = data[cptRow].Substring(cptCol, 3);
                    topLine = $"{topLine[0]}.{topLine[2]}";

                    var midLine = data[cptRow + 1].Substring(cptCol, 3);
                    midLine = $".{midLine[1]}.";

                    var botLine = data[cptRow + 2].Substring(cptCol, 3);
                    botLine = $"{botLine[0]}.{botLine[2]}";

                    pattern.TopLine = topLine;
                    pattern.MidLine = midLine;
                    pattern.BotLine = botLine;

                    pattern.Validate();
                    if (pattern.IsValid)
                    {
                        patterns.Add(pattern);
                    }
                }
            }
            return patterns.Count;
        }

        private class Pattern
        {
            public int StartX { get; set; }
            public int StartY { get; set; }

            public string TopLine { get; set; } = "";
            public string MidLine { get; set; } = "";
            public string BotLine { get; set; } = "";

            public bool IsValid { get; private set; }

            public void Validate()
            {
                var full = TopLine + BotLine + MidLine;
                if (full.Contains('X'))
                {
                    IsValid = false;
                    return;
                }

                if (MidLine != ".A.")
                {
                    IsValid = false;
                    return;
                }

                var combined = TopLine + BotLine;
                if (TopLine.Contains('A') || BotLine.Contains('A') || !combined.Contains('S') || !combined.Contains('M'))
                {
                    IsValid = false;
                    return;
                }

                if (TopLine.Equals(BotLine) || TopLine.Equals(BotLine.Reverse())
                    || (TopLine.Equals("M.M") && BotLine.Equals("S.S"))
                    || (TopLine.Equals("S.S") && BotLine.Equals("M.M")))
                {
                    IsValid = true;
                }
                else IsValid = false;
            }
        }
    }
}
