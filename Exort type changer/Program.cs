using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Exort_type_changer
{
    class Program
    {
        static int Main(string[] args)
        {
            string username = "";
            XmlDocument list = new XmlDocument();
            do
            {
                Console.WriteLine("Нажмите S для попытки экспорта с шики и любую другую клавишу для использования list.xml");
                if (Console.ReadKey(true).Key == ConsoleKey.S)
                {
                    try
                    {
                        
                        Console.WriteLine("Нажмите:\nM, если ваш ник MaKeMaK\nR, если ваш ник RuruDotaOneLove\nV, если ваш ник RuffyZ\nЛюбую другую клавишу, чтобы ввести свой ник.");
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.M:
                                username = "MaKeMaK";
                                break;
                            case ConsoleKey.R:
                                username = "RuruDotaOneLove";
                                break;
                            case ConsoleKey.V:
                                username = "RuffyZ";
                                break;
                            default:
                                Console.WriteLine("Введите имя пользователя:");
                                username = Console.ReadLine();
                                break;
                        }


                        list.Load("https://shikimori.one/" + username + "/list_export/animes.xml");
                    }
                    catch { ShowErrorMessage("export xml from shiki"); continue; }
                }
                else
                {
                    try
                    {
                        list.Load("list.xml");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        ShowErrorMessage("load list.xml - файл не найден");
                        continue;
                    }
                    catch { ShowErrorMessage("load list.xml - неопознанная ошибка"); continue; }
                }
                break;
            } while (true);
            XmlElement xroot = list.DocumentElement;
            XmlNode tmp;
            foreach (XmlNode xnode in xroot)
            {
                if (xnode.Name == "anime")
                {
                    tmp = list.CreateElement("my_start_date");
                    tmp.InnerText = "0000-00-00";
                    xnode.AppendChild(tmp);
                    tmp = list.CreateElement("my_finish_date");
                    tmp.InnerText = "0000-00-00";
                    xnode.AppendChild(tmp);
                }
            }

            try
            {
                list.Save(username==""?"":$"{username}'s " + $"AnimeList {DateTime.Now.ToLongTimeString().Replace(':', '：')}　{DateTime.Now.ToShortDateString()} .xml");
            }
            catch (Exception e)
            {
                ShowErrorMessage($"saving list ({e.Message})");
            }


            Console.WriteLine("\nДело сделано...");
            //Console.ReadKey(true);
            return 0;
        }

        static void ShowErrorMessage(string error)
        {
            Console.WriteLine("|||Error: " + (error ?? "unknown error") + "|||");
        }
    }
}
