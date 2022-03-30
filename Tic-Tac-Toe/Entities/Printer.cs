using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.Entities
{
    public class Printer
    {
        private IOutput output;

        public Printer(IOutput output)
        {
            this.output = output;
        }

        public void printFirstMove()
        {
            output.printFirstMove();
        }

        public void printEnterMove()
        {
            Console.WriteLine("enter yout move");
        }
    }
}
