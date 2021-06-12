using System;
using System.Linq;
using Volga_IT.Models;
using System.Reflection;
using System.Data.Entity;
using System.Diagnostics;
using Volga_IT.CounterWords;
using Volga_IT.Parser;
using Microsoft.Win32;

namespace Volga_IT
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationContext context = new ApplicationContext();
            context.UniqueWords.Load();

            Console.Write("Что сделать? (1-вывести из базы, 2-ввести путь): ");
            var answer = int.Parse(Console.ReadLine());

            if (answer == 1)
            {
                Console.WriteLine("\n\nСписок слов:\n");
                foreach (var word in context.UniqueWords.Local.ToList())
                    Console.WriteLine($"{word.Word} - {word.Count}");
            }
            else if (answer == 2)
            {

                ParseHtml parse = new ParseHtml();

                Console.WriteLine();

                Console.Write("Введите кол-во вхождений слова: ");
                var countText = Console.ReadLine();

                try
                {
                    if (int.TryParse(countText, out int count))
                    {
                        var streamFile = parse.GetFileStream();

                        var parsedFile = parse.ParseHtmlOfStream(streamFile);

                        UniqueWords uniqueWords = new UniqueWords();

                        var clearArray = uniqueWords.ClearArrayWithWord(parsedFile);

                        var dictionaryUniqueWord = uniqueWords.CountUniqueWord(clearArray);

                        foreach (var keyValue in dictionaryUniqueWord)
                        {
                            if (keyValue.Value >= count)
                            {
                                Console.WriteLine($"{keyValue.Key} - {keyValue.Value}");
                                var item = context.UniqueWords.Local.FirstOrDefault(x => x.Word == keyValue.Key && x.Url == streamFile.ToString());
                                if (item == null)
                                {
                                    var lastItem = context.UniqueWords.Local.LastOrDefault();
                                    var lastIndex = lastItem == null ? 1 : lastItem.IdWord + 1;
                                    context.UniqueWords.Add(new UniqueWord { IdWord = lastIndex, Url = streamFile.ToString(), Word = keyValue.Key, Count = keyValue.Value });
                                }
                                else
                                    item.Count += keyValue.Value;

                            }
                        }
                        context.SaveChanges();
                        Console.WriteLine("\nНажмите на любую кнопку");
                    }
                    else
                        throw new Exception("Некорректное число");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("\nНажмите любую кнопку...");
            Console.ReadKey();
            Process.Start(Assembly.GetExecutingAssembly().Location);
            Environment.Exit(0);
        }
    }
}
