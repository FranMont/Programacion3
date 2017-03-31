using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego
{
    class Enemigo
    {
        char name = 'I';
        int startingX = 0;
        int startingY = 0;
        int locationX = 0;
        int locationY = 0;

        public bool collision(int x, int y)
        {
            if (locationX == x && locationY == y)
            {
                return true;
            }
            else
                return false;
        }
        public void ResetEnemigo()
        {
            locationX = startingX;
            locationY = startingY;
            Console.SetCursorPosition(locationX, locationY);
            Console.Write(name);
        }
        public Enemigo(int x, int y, char c)
        {
            startingX = x;
            startingY = y;
            locationX = x;
            locationY = y;
            name = c;
            Console.SetCursorPosition(locationX, locationY);
            Console.Write(name);
            //Console.Write(locationX.ToString(), locationY.ToString());
        }
    }
}
