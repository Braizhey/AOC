using AOC.Common.Services;

namespace AOC.App2024.Resolvers.Day5
{
    public class ResolverDay5Part1 : IAdventServiceResolver
    {
        public int Resolve(List<string> data)
        {
            var isRule = true;
            var rules = new List<Rule>();
            var updates = new List<PrinterUpdate>();
            foreach (var input in data)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    isRule = false;
                    continue;
                }

                if (isRule)
                {
                    var rule = new Rule(input);
                    rules.Add(rule);
                }
                else
                {
                    var update = new PrinterUpdate(input, rules);
                    updates.Add(update);
                }
            }

            var sum = 0;
            foreach (var update in updates)
            {
                if (update.IsValid)
                {
                    sum += update.Middle;
                }
            }
            return sum;
        }

        public class Rule
        {
            public string Input { get; private set; }
            public string Before { get; private set; }
            public string After { get; private set; }

            public Rule(string input)
            {
                Input = input;

                var split = input.Split("|");
                Before = split[0];
                After = split[1];
            }

            public override string ToString()
            {
                return Input;
            }
        }

        public class PrinterUpdate
        {
            public string Input { get; private set; }
            public List<string> Pages { get; private set; }
            public int Middle { get; private set; }
            public bool IsValid { get; private set; }

            public List<string> OrderedPages { get; private set; } = [];
            public int OrderedMiddle { get; private set; }


            public PrinterUpdate(string input, List<Rule> rules)
            {
                Input = input;
                Pages = [.. input.Split(",")];

                Middle = FindMiddle(Pages);

                Validate(rules);
                if (!IsValid)
                {
                    Order(rules);
                    OrderedMiddle = FindMiddle(OrderedPages);
                }
            }

            private int FindMiddle(List<string> pages)
            {
                var midIndex = Convert.ToInt32(Pages.Count / 2);
                return int.Parse(Pages.ElementAt(midIndex));
            }

            private void Validate(List<Rule> rules)
            {
                var isValid = true;
                foreach (var rule in rules)
                {
                    if (Pages.Contains(rule.Before) && Pages.Contains(rule.After))
                    {
                        var indexBefore = Pages.IndexOf(rule.Before);
                        var indexAfter = Pages.IndexOf(rule.After);
                        isValid = indexBefore < indexAfter;
                    }
                    if (!isValid) break;
                }
                IsValid = isValid;
            }

            private void Order(List<Rule> rules)
            {
                OrderedPages = Pages.ToList();
            }

            public override string ToString()
            {
                return Input;
            }
        }
    }
}
