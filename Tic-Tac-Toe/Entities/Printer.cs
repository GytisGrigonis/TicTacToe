using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.Entities
{
    public class Printer
    {
        public void printEnterMove()
        {
            Console.WriteLine("enter your move");
        }
        public void printMachineStarts()
        {
            Console.WriteLine("machine starts");
        }
        public void printPlayerStarts()
        {
            Console.WriteLine("you start");
        }
        public void printCustom(string text)
        {
            Console.WriteLine(text);
        }
    }
}
