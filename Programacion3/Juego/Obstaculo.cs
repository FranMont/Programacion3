using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego
{
    class Obstaculo : Enemigo
    {
        public Obstaculo(int x, int y, char c) : base(x,y,c)
        {
            Console.SetCursorPosition(GetLocationX(), GetLocationY());
            Console.Write(GetName());
        }
    }
}
