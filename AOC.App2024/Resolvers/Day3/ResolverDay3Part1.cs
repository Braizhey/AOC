using AOC.Common.Services;

namespace AOC.App2024.Resolvers.Day3
{
    public class ResolverDay3Part1 : IAdventServiceResolver
    {
        const string _mul = "mul(";
        public int Resolve(List<string> data)
        {
            var sum = 0;
            foreach (var line in data)
            {
                var instructions = line.Split(_mul);
                foreach (var part in instructions)
                {
                    var end = part.Split(")");
                    if (end.Length == 0) continue;

                    var endOfInterest = end[0];
                    if (endOfInterest.Length > 7) continue; // invalid data : XXX,XXX

                    var figures = endOfInterest.Split(",");
                    if (figures.Length != 2) continue; // format is not XXX,XXX

                    if (!int.TryParse(figures[0], out int fig1)) continue; // not a number
                    if (!int.TryParse(figures[1], out int fig2)) continue; // not a number

                    sum += fig1 * fig2;
                }
            }
            return sum;
        }
    }
}
