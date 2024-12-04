using AOC.Common.Services;

namespace AOC.App2024.Resolvers.Day4
{
    public class ResolverDay4Part1 : IAdventServiceResolver
    {
        private const string _search = "XMAS";
        private const string _searchRev = "SAMX";
        public int Resolve(List<string> data)
        {
            var sum = 0;
            var columns = new List<string>();

            foreach (var row in data)
            {
                sum += CountXmas(row);

                // Builds cols
                for (var cptCol = 0; cptCol < row.Length; cptCol++)
                {
                    if (columns.Count <= cptCol)
                    {
                        columns.Add(row[cptCol].ToString());
                    }
                    else
                    {
                        columns[cptCol] += row[cptCol];
                    }
                }
            }

            // Calculates cols
            foreach (var col in columns)
            {
                sum += CountXmas(col);
            }

            // Builds diags
            var diags = new List<string>();
            for (var cptRow = 0; cptRow < data.Count; cptRow++)
            {
                for (var cptCol = 0; cptCol < data.Count; cptCol++)
                {
                    if (cptRow == 0 || cptCol == 0 || cptCol == data.Count - 1)
                    {
                        var diag = $"{data[cptRow][cptCol]}";
                        for (var cptDiag = cptCol + 1; (cptDiag + cptRow - cptCol) < columns.Count && cptDiag < columns.Count; cptDiag++)
                        {
                            diag += data[cptDiag + cptRow - cptCol][cptDiag];
                        }
                        if (diag.Length > 3) diags.Add(diag);

                        diag = $"{data[cptRow][cptCol]}";
                        for (var cptDiag = cptCol + 1; (cptDiag + cptRow - cptCol) < columns.Count && cptCol - (cptDiag - cptCol) >= 0; cptDiag++)
                        {
                            diag += data[cptDiag + cptRow - cptCol][cptCol - (cptDiag - cptCol)];
                        }
                        if (diag.Length > 3) diags.Add(diag);
                    }
                }
            }

            foreach (var diag in diags)
            {
                sum += CountXmas(diag);
            }

            return sum;
        }

        private static int CountXmas(string input)
        {
            var sum = 0;
            for (var cpt = 0; cpt <= input.Length - _search.Length; cpt++)
            {
                if (cpt + _search.Length <= input.Length)
                {
                    var temp = input.Substring(cpt, _search.Length);
                    if (_search.Equals(temp) || _searchRev.Equals(temp)) sum++;
                }
            }
            return sum;
        }
    }
}
