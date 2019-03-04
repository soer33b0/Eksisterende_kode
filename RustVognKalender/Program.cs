using RustVognKalender;
using System;
using System.Collections.Generic;

namespace Console_Menu
{
    class Program
    {
        private static int index = 0;
        
        private static void Main(string[] args)
        {
            Controller c = new Controller();
            
            // Create a list of menu items
            List<string> menuItems = new List<string>() {
                "Opret Booking",
                "Rediger Booking",
                "Slet Booking",
                "Updater Database",
                "Opret Rustvogn",
                "Rediger Rustvogn",
                "Slet Rustvogn",
                "Afslut program"
            };



            // Startup skal k¯res f¯rst
            c.StartUp();
            // Remove the flashing line and add functionality to the menu items
            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = DrawMenu(menuItems);
                if (selectedMenuItem == "Opret Booking")
                {
                    Console.Clear();
                    bool reservation; 
                    Console.WriteLine("Vaelg starttidspunkt [dd-mm-yy hh:mm]");
                    string start = Console.ReadLine();
                    Console.WriteLine("Vaelg sluttidspunkt [dd-mm-yy hh:mm]");
                    string end = Console.ReadLine();
                    Console.WriteLine("Skal en rustvogn reserveres? [J/N]");
                    reservation = BooleanChoice();
                    Console.WriteLine("Skriv en addresse");
                    string address = Console.ReadLine();
                    Console.WriteLine("Skriv en kommentar");
                    string comment = Console.ReadLine();
                    if (!c.CreateEventType(reservation, start, end, address, comment))
                    {
                        Console.WriteLine("noget gik galt, pr¯v igen [tryk enter for at fortsÊtte]");
                        Console.ReadLine();
                    }
                    Console.WriteLine("");
                    Console.WriteLine("========== Booking blev oprettet ==========");
                    Console.WriteLine("");
                    IntermissionToMenu();
                    Console.Clear();

                }
                else if (selectedMenuItem == "Rediger Booking")
                {
                    Console.Clear();
                    
                    Console.WriteLine("Indtast identifikationsn¯glen pÂ begivenheden.");
                    string key = Console.ReadLine();
                    Console.WriteLine("Vaelg starttidspunkt [dd-mm-yy hh:mm], eller efterlad tom for ingen aendring");
                    string start = Console.ReadLine();
                    Console.WriteLine("Vaelg sluttidspunkt [dd-mm-yy hh:mm], eller efterlad tom for ingen aendring");
                    string end = Console.ReadLine();
                    Console.WriteLine("VÊlg den Rustvogn der skal reserveres? [Rustvognens key eller 0]");
                    string reservation = Console.ReadLine();
                    Console.WriteLine("Skriv en addresse, eller efterlad tom for ingen Êndring");
                    string address = Console.ReadLine();
                    Console.WriteLine("Skriv en kommentar, eller efterlad tom for ingen aendring");
                    string comment = Console.ReadLine();
                    if (!c.AlterEvent(key, reservation, start, end, address, comment))
                    {
                        Console.WriteLine("noget gik galt, pr¯v igen [tryk enter for at fortsÊtte]");
                        Console.ReadLine();
                    }
                    Console.WriteLine("");
                    Console.WriteLine("========== Booking blev redigeret ==========");
                    Console.WriteLine("");
                    IntermissionToMenu();
                    Console.Clear();

                }
                else if (selectedMenuItem == "Slet Booking")
                {
                    Console.Clear();
                    Console.WriteLine("Indtast identifikationsn¯glen pÂ begivenheden.");
                    string key = Console.ReadLine();
                    c.DeleteEvent(key);
                    Console.WriteLine("");
                    Console.WriteLine("========== Booking blev slettet ==========");
                    Console.WriteLine("");
                    IntermissionToMenu();
                    Console.Clear();
                }
                else if (selectedMenuItem == "Opdater Database")
                {
                    c.Update();
                }
                else if (selectedMenuItem == "Opret Rustvogn")
                {
                    Console.Clear();
                    Console.WriteLine("Indtast rustvognens prioritet.");
                    string hearsePrio = Console.ReadLine();
                  


                    Console.WriteLine("");
                    Console.WriteLine("========== Rustvogn blev prioriteret ==========");
                    Console.WriteLine("");
                    IntermissionToMenu();
                    Console.Clear();

                }
                else if (selectedMenuItem == "Rediger Rustvogn")
                {
                    Console.Clear();
                    Console.WriteLine("Indtast prioriteten pÅEden rustvogn som skal redigeres.");
                    string hearsePrio = Console.ReadLine();



                    Console.WriteLine("");
                    Console.WriteLine("========== Rustvogn blev redigeret ==========");
                    Console.WriteLine("");
                    IntermissionToMenu();
                    Console.Clear();
                }
                else if (selectedMenuItem == "Slet Rustvogn")
                {
                    Console.Clear();
                    Console.WriteLine("Indtast prioriteten pÅEden rustvogn som skal fjernes.");
                    string hearsePrio = Console.ReadLine();



                    Console.WriteLine("");
                    Console.WriteLine("========== Rustvogn blev slettet ==========");
                    Console.WriteLine("");
                    IntermissionToMenu();
                    Console.Clear();
                }
                else if (selectedMenuItem == "Afslut program")
                {
                    c.Update();
                    Environment.Exit(0);
                }
            }





            void IntermissionToMenu()
            {
                Console.WriteLine("Vender tilbage til hovedmenuen om 3 sekunder...");
                System.Threading.Thread.Sleep(3000);
            }





            bool BooleanChoice()
            {
                string input = Console.ReadLine();
                if (input == "J" || input == "j")
                {
                    return true;
                }
                else if (input == "N" || input == "n")
                {
                    return false;
                }
                    Console.WriteLine("J/N");
                    bool ans = BooleanChoice();
                    return ans;
            }
        }
        





        private static string DrawMenu(List<string> items)
        {
            // for loop, for counting the items in the list
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(items[i]);

                }
                else
                {
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }





            // Add functionality to the up, down and enter keys
            ConsoleKeyInfo ckey = Console.ReadKey();

            if (ckey.Key == ConsoleKey.DownArrow)
            {
                // stops you from keep going down through the menu.
                if (index == items.Count - 1)
                {

                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                // stops you from going further up than the menu has items.
                if (index <= 0)
                {

                }
                else { index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[index];
            }
            else if (!(ckey.Key == ConsoleKey.Enter || ckey.Key == ConsoleKey.UpArrow || ckey.Key == ConsoleKey.DownArrow))
            {
                
            }





            Console.Clear();
            return "";
        }
    }
}