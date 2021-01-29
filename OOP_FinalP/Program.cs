using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OOP_FinalP
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            var path = "parfume-data.txt";
            Parfume pf = new Parfume(path);

            var array2D = pf.txtToArray(); // Txt verisetimizi bir diziye dönüştürdük.
            
            
            pf.showMenWomenNumbers(); // kullanıcı method ile kadın, erkek ve unisex parfüm sayılarına erişebilir.
            
            pf.findByName("Orchidee"); // İsminde 'Orchidee' geçen parfümler filtrelenir.
            
            array2D = pf.filterByAccord("autumn<5", array2D);
            array2D = pf.filterByAccord("winter>15", array2D);
            array2D = pf.filterByAccord("summer<50", array2D);
            array2D = pf.filterByAccord("loved>50", array2D);
            array2D = pf.filterByAccord("day<50", array2D);
            array2D = pf.filterByAccord("warm spicy<80", array2D); // kullanıcının girişine göre Accord'lar filtrelenir.
            pf.showFilteredAccords(array2D); // Filtrelenen Accordlar yazdırılır.
            

            pf.suggestClosest("amber=30","citrus=70"); // method kullanıcının girdiği Accordlar'a en yakın parfümü gösterir.
            
        }
    }
}