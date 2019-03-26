using RustVognKalender;
using System;
using System.Collections.Generic;

namespace Console_Menu
{
    
    class Program
    {
        Menu menu = new Menu();
        private static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.Run();
        }

        private void Run()
        {
            menu.ShowMenu();
        }
    }
}