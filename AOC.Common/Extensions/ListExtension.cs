namespace AOC.Common.Extensions
{
    public static class ListExtension
    {
        public static List<T> AddItemAtIndex<T>(this IEnumerable<T> items, T newItem, int index)
        {
            var newItems = new List<T>();
            for (var cpt = 0; cpt < items.Count(); cpt++)
            {
                var rule = items.ElementAt(cpt);
                if (cpt == index)
                {
                    newItems.Add(newItem);
                }
                newItems.Add(rule);
            }
            if (index == newItems.Count)
            {
                newItems.Add(newItem);
            }
            return newItems;
        }
    }
}
