using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.Entities
{
    public class PlayerStartsOutput : IOutput
    {
        public void printFirstMove()
        {
            Console.WriteLine("you start");
        }
    }
}
