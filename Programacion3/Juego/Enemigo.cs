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
        private int startingX = 0;
        private int startingY = 0;
        private int locationX = 0;
        private int locationY = 0;
        private int oldlocationX = 0;
        private int oldlocationY = 0;
        public virtual void EnemiesMove() { }
        public virtual void Draw()
        {
            Console.SetCursorPosition(oldlocationX, oldlocationY);
            Console.Write(" ");
            Console.SetCursorPosition(locationX, locationY);
            Console.Write(name);
        }
        public void SetLocationX(int x)
        {
            oldlocationX = locationX;
            locationX = x;
        }
        public void SetLocationY(int y)
        {
            oldlocationY = locationY;
            locationY = y;
        }
        public void SetName(char c)
        {
            name = c;
        }
        public int GetLocationX()
        {
            return locationX;
        }
        public int GetLocationY()
        {
            return locationY;
        }
        public char GetName()
        {
            return name;
        }
        public bool collision(int x, int y)
        {
            if (this.GetLocationX() == x && this.GetLocationY() == y)
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
            oldlocationX = locationX;
            oldlocationY = locationY;
            name = c;
            Console.SetCursorPosition(locationX, locationY);
            Console.Write(name);
        }
    }
}
