namespace AOC.Common.Services
{
    public class AdventService(int day)
    {
        private readonly int _day = day;
        private const string _resolverName = "Resolver";
        public int Resolve(int part)
        {
            var data = GetData();
            var partName = $"Day{_day}Part{part}";
            var resolverName = $"{_resolverName}{partName}";

            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            if (assembly == null) throw new InvalidProgramException("Cannot find entry assembly!");

            var resolverType = assembly.GetTypes().SingleOrDefault(t => t.Name == resolverName);
            var instance = assembly.CreateInstance(resolverType?.FullName ?? resolverName);
            if (instance == null) throw new InvalidOperationException($"Cannot retrieve resolver \"{resolverName}\"!");

            IAdventServiceResolver resolver = (IAdventServiceResolver)instance;
            var result = resolver.Resolve(data);
            Console.WriteLine($"{partName} result: {result}");
            return result;
        }

        private List<string> GetData()
        {
            var path = @$".\data\day_{_day}.txt";
            if (!File.Exists(path)) throw new InvalidDataException($"No data file at \"{path}\"!");
            return [.. File.ReadAllLines(path)];
        }
    }
}
