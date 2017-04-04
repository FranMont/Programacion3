using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego 
{
    class Enemlr : Enemigo
    {
        private bool movingRight = true;
        public override void EnemiesMove()
        {
            if (movingRight == false)
            {
                if (GetLocationX() > 1)
                {
                    SetLocationX(GetLocationX() - 1);
                }
                else
                {
                    movingRight = true;
                }
            }
            else
            {
                if (GetLocationX() < 78)
                {
                    SetLocationX(GetLocationX() + 1);
                }
                else
                {
                    movingRight = false;
                }
            }
            Draw();
        }
        public Enemlr(int x, int y, char c) : base(x, y, c)
        {
            movingRight = true;
            Console.SetCursorPosition(GetLocationX(), GetLocationY());
            Console.Write(GetName());
        }
    }
}
