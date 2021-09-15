using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Analysis
{
    class AnalysisText
    {
        public static void Counter(object adressObj)
        {
            //Таймер времени обработки текста
            Stopwatch stopWatch1 = new Stopwatch();
            stopWatch1.Start();

            string adressFile = adressObj.ToString();

            string text = File.ReadAllText(adressFile, Encoding.GetEncoding(65001));
            //Убираем лишние знаки регуляркой и методом Split записываем массив словами
            string resultText = Regex.Replace(text, @"[^\p{L} -]", "");
            string[] splitText = resultText.Split();
            List<string> triplet = new List<string>();

            //записываем лист из массива убирая пробелы и образовувая триплеты 
            for (int i = 0; i < splitText.Length; i++)
            {
                if (splitText[i] != "")
                {
                    for (int j = 0; j < splitText.Length; j++)
                    {
                        text = splitText[j];
                        for (int d = 0; d < text.Length - 2; d++)
                        {
                            triplet.Add(text.Substring(d, 3));
                        }
                    }
                }
            }

            string[] str = new string[triplet.Count];
            // лист обратно в массив
            for (int i = 0; i < triplet.Count; i++)
            {
                str[i] = triplet[i];
            }

            Dictionary<string, int> countries = new Dictionary<string, int>();
            
            foreach (string eachNumber in str)
            {
                if (countries.ContainsKey(eachNumber))
                    countries[eachNumber]++;
                else
                    countries[eachNumber] = 1;
            }
            // создаем словарь countries и записываем по ключу триплетов, далее с помошью листа сортируем и выводим первые 10 триплетов
            // по совподениям (10 триплетов с максимальным значением)
            List<KeyValuePair<string, int>> sorted = new List<KeyValuePair<string, int>>(countries);
            sorted.Sort(new Comparison<KeyValuePair<string, int>>(CompareKvp));
            int count = 0;
            foreach (KeyValuePair<string, int> kvp in sorted)
            {
                count++;
                if (count != 10)
                    Console.Write(kvp.Key.ToString() + ",");
                else
                    Console.Write(kvp.Key.ToString() + ".");
                if (count == 10)
                    break;
            }
            //останавливаем таймер и переводим полученый результат в миллисекунды.
            stopWatch1.Stop();
            TimeSpan ts1 = stopWatch1.Elapsed;
            int time = ts1.Minutes * 60000 + ts1.Seconds * 1000 + ts1.Milliseconds;
            Console.WriteLine($"\nВремя работы программы: " + time + " миллисекунд.");
        }

        private static int CompareKvp(KeyValuePair<string, int> kvp1, KeyValuePair<string, int> kvp2)
        {
            int ret = -kvp1.Value.CompareTo(kvp2.Value);
            if (ret == 0)
            {
                ret = kvp1.Key.CompareTo(kvp2.Key);
            }
            return ret;
        }
    }
}
