using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OOP_FinalP
{
    public class Parfume
    {
        public string path;
        private string[,] array2D;

        public Parfume(string path)
        {
            this.path = path;
        }

        public string[,] txtToArray()
        {
            List<String> listStrLineElements = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        listStrLineElements = line.Split('~').ToList();
                    }
                }
            }
            catch
            {
                Console.WriteLine($"{path} could not be read.");
            }

            for (int i = 0; i < listStrLineElements.Count; i++)
            {
                listStrLineElements[i] = listStrLineElements[i].Substring(1);
            }
            List<string> list2 = new List<string>();
            foreach (var line in listStrLineElements)
            {
                string[] temp;
                temp = line.Split(new[] { ';' }, 2);
                foreach (var t in temp)
                {
                    list2.Add(t);
                }
            }
            var height = listStrLineElements.Count; //satır sayısı
            var width = list2.Count / height; //sütun sayısı
            var index = 0;
            string[,] twoDimensionalArray = new string[height, width];
            for (var x = 0; x < height; x++)
            {
                for (var y = 0; y < width; y++)
                {
                    twoDimensionalArray[x, y] = list2[index]; // 2 boyutlu hale getirildi.
                    index++;
                }
            }

            this.array2D = twoDimensionalArray;
            return twoDimensionalArray;
        }

        public void showMenWomenNumbers()
        {
            Console.WriteLine();
            var men = 0; var women = 0; var unisex = 0;
            for (int i = 0; i < this.array2D.GetLength(0); i++)
            {
                if (this.array2D[i, 0].Contains("for women") && this.array2D[i, 0].Contains("and men"))
                    unisex++;
                else if (this.array2D[i, 0].Contains("for women") && !(this.array2D[i, 0].Contains("and men")))
                    women++;
                else if (this.array2D[i, 0].Contains("for men"))
                    men++;
            } // parfüm sayıları hesaplandı.
            Console.WriteLine($"No. of Women Parfume: {women}");
            Console.WriteLine($"No. of Men Parfume: {men}");
            Console.WriteLine($"No. of Unisex Parfume: {unisex}");
        }

        public void findByName(string s)
        {
            Console.WriteLine($"\nParfumes whose name including '{s}'\n");
            var id = 1;
            for (int i = 0; i < array2D.GetLength(0); i++)
            {
                if (this.array2D[i,0].Contains(s))
                {
                    Console.WriteLine($"{id}) {this.array2D[i,0]}=>{this.array2D[i,1]}");
                    id += 1; // Girilen string'i içeren parfümler filtrelendi.
                }
            }   
        }

        public string[,] filterByAccord(string condition,string[,] array2D)
        {
            List<int> indexList = new List<int>();
            for (int i = 0; i < array2D.GetLength(0); i++)
            {
                if (condition.Split('<').Length > 1);
                {
                    if (array2D[i, 1].Contains(condition.Split('<')[0]))
                    {
                        var s = (array2D[i,1].Split(new string[] { condition.Split('<')[0] }, StringSplitOptions.None))[1];
                        var value = Int32.Parse(getBetween(s, ":", ";"));
                        if (value < Int64.Parse(condition.Split('<')[1]))
                        {
                            indexList.Add(i);
                        }
                    }
                }
                if (condition.Split('>').Length > 1);
                {
                    if (array2D[i, 1].Contains(condition.Split('>')[0]))
                    {
                        var s = (array2D[i,1].Split(new string[] { condition.Split('>')[0] }, StringSplitOptions.None))[1];
                        var value = Int32.Parse(getBetween(s, ":", ";"));
                        if (value > Int64.Parse(condition.Split('>')[1]))
                        {
                            indexList.Add(i);
                        }
                    }
                }
                if (condition.Split('=').Length > 1);
                {
                    if (array2D[i, 1].Contains(condition.Split('=')[0]))
                    {
                        var s = (array2D[i,1].Split(new string[] { condition.Split('=')[0] }, StringSplitOptions.None))[1];
                        var value = Int32.Parse(getBetween(s, ":", ";"));
                        if (value == Int64.Parse(condition.Split('=')[1]))
                        {
                            indexList.Add(i);
                        }
                    }
                }
            }
            
            List<string> listA = new List<string>();
            

            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < indexList.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    listA.Add(array2D[indexList[i],j]);
                }
            }

            var height=indexList.Count; //satır sayısı
            var width=2; //sütun sayısı
            var index = 0;
            string[,] temp = new string[height, width];
            
            for (var x = 0; x < height; x++)
            {
                for (var y = 0; y < width; y++)
                {
                    temp[x, y] = listA[index]; // 2 boyutlu hale getirildi.
                    index++;
                }
            }
            // Sonuç olarak; verisetinde, girilen koşulu sağlayan parfümlerin indexleri ile yeni bir dizi oluşturuldu.
            
            return temp;
        }

        public void showFilteredAccords(string[,] array2D)
        {
            Console.WriteLine($"\n{array2D.GetLength(0)} perfume found matching the features you are looking for\n");
            var id = 1;
            for (int i = 0; i < array2D.GetLength(0); i++)
            {
                Console.WriteLine($"{id}) {array2D[i,0]} \n {array2D[i,1]}\n"); // Filtrelenen parfümlerden oluşan dizi yazdırıldı.
                id += 1;
            }
        }
        
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
                // bu method filtrelemede kullanıldı. Bir string'in iki karakteri arasındaki string'i döndürür.
            }

            return "";
        }

        public void suggestClosest(string first, string second)
        {
            List<int> list1 = new List<int>();
            for (int i = 0; i < this.array2D.GetLength(0); i++)
            {
                if (first.Split('=').Length > 1);
                {
                    if (this.array2D[i, 1].Contains(first.Split('=')[0]))
                    {
                        var s = (array2D[i,1].Split(new string[] { first.Split('=')[0] }, StringSplitOptions.None))[1];
                        
                        try
                        {
                            var value = Int32.Parse(getBetween(s, ":", ";"));
                            var diff = value - Int32.Parse(first.Split('=')[1]);
                            list1.Add(diff*diff); // uzaklıkların karesi hesaplanıp listede tutuluyor.
                        }
                        catch (Exception e)
                        {
                            var value = s.Split(':')[1];
                            var diff = Int32.Parse(value) - Int32.Parse(first.Split('=')[1]);
                            list1.Add(diff*diff);
                        }
                    }
                    else
                    {
                        list1.Add(Convert.ToInt32(Math.Pow(10,6))); 
                        // accord, ilgili indexteki parfümde bulnmuyorsa yüksek bir değer atanıyor.
                    }
                }
            }
            
            List<int> list2 = new List<int>();
            for (int i = 0; i < this.array2D.GetLength(0); i++)
            {
                if (second.Split('=').Length > 1);
                {
                    if (this.array2D[i, 1].Contains(second.Split('=')[0]))
                    {
                        var s = (array2D[i,1].Split(new string[] { second.Split('=')[0] }, StringSplitOptions.None))[1];
                        
                        try
                        {
                            var value = Int32.Parse(getBetween(s, ":", ";"));
                            var diff = value - Int32.Parse(second.Split('=')[1]);
                            list2.Add(diff*diff); // uzaklıkların karesi hesaplanıp listede tutuluyor.
                        }
                        catch (Exception e)
                        {
                            var value = s.Split(':')[1];
                            var diff = Int32.Parse(value) - Int32.Parse(second.Split('=')[1]);
                            list2.Add(diff*diff);
                        }
                    }
                    else
                    {
                        list2.Add(Convert.ToInt32(Math.Pow(10,6)));
                        // accord, ilgili indexteki parfümde bulnmuyorsa yüksek bir değer atanıyor.
                    }
                }
            }
            List<int> listFinal = new List<int>();
            for (int i = 0; i < list1.Count; i++)
            {
                listFinal.Add(list1[i]+list2[i]); // toplam uzaklık hesaplanıyor.
            }
            
            var minValue = listFinal.Min();
            var minIndex = listFinal.IndexOf(minValue);
            // en yakın parfümün indexi belirleniyor.
            
            Console.WriteLine($"\nClosest parfume for given accords:\n{this.array2D[minIndex,0]} => {this.array2D[minIndex,1]}\n");
            
        }
        
        
    }
}