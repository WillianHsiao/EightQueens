using System;
using System.Collections.Generic;

namespace EightQueens
{
    public enum CrossType
    {
        LeftUp,
        RightDown,
        RightUp,
        LeftDown,
    }

    class Program
    {
        static void Main(string[] args)
        {
            var results = GetResults(8);
            foreach (var result in results)
            {
                foreach (var e in result)
                {
                    for (int i = 0; i < e.Count; i++)
                    {
                        if (i == e.Count - 1)
                        {
                            Console.WriteLine(e[i]);
                        }
                        else
                        {
                            Console.Write(e[i]);
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public static List<List<List<string>>> GetResults(int queens)
        {
            var results = new List<List<List<string>>>();
            var index = 0;
            for (int i = 0; i < queens; i++)
            {
                var result = new List<List<string>>();
                for (int j = 0; j < queens; j++)
                {
                    result.Add(new List<string>());
                    var array = result[result.Count - 1];
                    for (int k = 0; k < queens; k++)
                    {
                        if(j == 0 && k == index)
                        {
                            array.Add("Q");
                        }
                        else if(!array.Contains("Q") && isValid(k, j, result))
                        {
                            array.Add("Q");
                        }
                        else
                        {
                            array.Add(".");
                        }
                    }
                }
                index++;

                if (!results.Contains(result))
                {
                    results.Add(result);
                }
            }
            return results;
        }

        /// <summary>
        /// 檢查是否可以擺放
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private static bool isValid(int x, int y, List<List<string>> map)
        {
            //檢查橫線(Y)是否有Q
            if (map[y].Contains("Q"))
                return false;
            //檢查直線(X)是否有Q
            foreach (var m in map)
            {
                if (m.Count - 1 >= x)
                {
                    if (m[x] == "Q")
                        return false;
                }
            }
            //檢查對角線是否有Q
            if (GetCrossElements(x, y, map).Contains("Q"))
                return false;
            return true;
        }

        /// <summary>
        /// 取得對角線清單
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private static List<string> GetCrossElements(int x, int y, List<List<string>> map)
        {
            var result = new List<string>();
            foreach(CrossType type in Enum.GetValues(typeof(CrossType)))
            {
                var index = 1;
                while (true)
                {
                    string e = null;
                    switch (type)
                    {
                        case CrossType.LeftUp:
                            e = GetElement(x - index, y - index, map);
                            break;
                        case CrossType.RightDown:
                            e = GetElement(x + index, y + index, map);
                            break;
                        case CrossType.RightUp:
                            e = GetElement(x - index, y + index, map);
                            break;
                        case CrossType.LeftDown:
                            e = GetElement(x + index, y - index, map);
                            break;
                    }

                    if (e != null)
                    {
                        result.Add(e);
                    }
                    else
                    {
                        break;
                    }
                    index++;
                }
            }
            return result;
        }

        /// <summary>
        /// 取得絕對位置的項目
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private static string GetElement(int x, int y, List<List<string>> map)
        {
            try
            {
                return map[y][x];
            }
            catch
            {
                return null;
            }
        }
    }
}