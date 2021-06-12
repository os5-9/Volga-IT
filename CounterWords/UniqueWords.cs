using System.Collections.Generic;
using System.Linq;

namespace Volga_IT.CounterWords
{
    class UniqueWords
    {
        private static readonly char[] arrRu = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я' };

        public string[] ClearArrayWithWord(string[] parsedHtml)
        {
            var clearArray = parsedHtml.Where(x => System.Array.Exists(x.ToCharArray(), s => arrRu.Contains(s)))
                .ToList();
            clearArray = clearArray.Select(x => string.Concat(x.Select(s => char.ToLower(s)))).ToList();
            for (int i = 0; i < clearArray.Count; i++)
            {
                var word = string.Concat(clearArray[i].Where(x => arrRu.Contains(x)));
                if (word.Length > 1 || word == "в")
                    clearArray[i] = word;
                else
                    clearArray.RemoveAt(i);
            }
            return clearArray.ToArray();
        }

        public Dictionary<string, int> CountUniqueWord(string[] parsedHtml)
        {
            Dictionary<string, int> item = new Dictionary<string, int>();
            foreach (var i in parsedHtml)
            {
                if (item.ContainsKey(i))
                    item[i]++;
                else
                    item.Add(i, 1);
            }
            return item;
        }
    }
}
