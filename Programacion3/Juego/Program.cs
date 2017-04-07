using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego
{
    class Program
    {
        static void gameOver()
        {
            Console.Clear();
            Console.SetCursorPosition(20, 12);
            Console.Write("GAME OVER! Presione 'x' para reiniciar.");
        }
        static void DrawMap()// ╣ ╠ ╩ ╦ ╚ ╝ ╗ ╔ ═ ║
        {
            Console.SetCursorPosition(0, 0); Console.Write("╔");
            Console.SetCursorPosition(79, 0); Console.Write("╗");
            Console.SetCursorPosition(0, 24); Console.Write("╚");
            Console.SetCursorPosition(79, 24); Console.Write("╝");
            for (int i = 1; i < 79; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("═");
                Console.SetCursorPosition(i, 24);
                Console.Write("═");
            }
            for (int i = 1; i < 24; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("║");
                Console.SetCursorPosition(79, i);
                Console.Write("║");
            }
        }
        static void drawMenu()
        {
            Console.SetCursorPosition(22, 20);
            Console.Write("Presione 1 para jugar solo");
            Console.SetCursorPosition(20, 21);
            Console.Write("Presione 2 para jugar de a dos");
            Console.SetCursorPosition(24, 22);
            Console.Write("Presione Esc para salir.");
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();

            int randspawnY = rnd.Next(1, 23);
            int randspawnX = rnd.Next(1, 77);
            bool playerDead = false;
            bool finishGame = false;
            bool gameOverShow = false;
            bool inMenu = false;
            bool showMenu = false;
            bool j2 = false;
            ConsoleKeyInfo userKey;
            Personaje player = new Personaje();
            Personaje player2 = new Personaje();
            Enemigo[] enemies = new Enemigo[5];
            Enemigo[] enemiesMove = new Enemigo[4];
            Console.Clear();
            while(inMenu == false)
            {
                if(showMenu == false)
                {
                    drawMenu();
                    showMenu = true;
                }
                if (Console.KeyAvailable)
                {
                    userKey = Console.ReadKey();
                     
                    if (userKey.Key == ConsoleKey.Escape)
                    {
                        finishGame = true;
                    }
                    else if (userKey.Key == ConsoleKey.NumPad1)
                    {
                        j2 = false;
                        inMenu = true;
                    }
                    else if (userKey.Key == ConsoleKey.NumPad2)
                    {
                        j2 = true;
                        inMenu = true;
                    }
                   
                }
            }
            Console.Clear();
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Obstaculo(randspawnX, randspawnY, 'X');
                enemies[i].Draw();
                randspawnY = rnd.Next(1, 23);
                randspawnX = rnd.Next(1, 77);
            }
            for (int i = 0; i < enemiesMove.Length; i++)
            {
                randspawnX = rnd.Next(2, 76);
                enemiesMove[i] = new Enemlr(randspawnX, i + i * i + 1, 'Y');
            }
            DrawMap();
            player.Draw();
            if (j2 == true)
            {
                player2.P2(j2);
                player2.Draw();
            }
            while (finishGame == false)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    if(enemies[i].collision(player.posX(), player.posY()) == true)
                    {
                        playerDead = true;
                    }
                    if (enemies[i].collision(player2.posX(), player2.posY()) == true && j2 == true)
                    {
                        playerDead = true;
                    }
                }
                for (int i = 0; i < enemiesMove.Length; i++)
                {
                    if (playerDead == false)
                    {
                        enemiesMove[i].EnemiesMove();
                    }
                    if (enemiesMove[i].collision(player2.posX(), player2.posY()) == true && j2 == true)
                    {
                        playerDead = true;
                    }
                    if (enemiesMove[i].collision(player.posX(), player.posY()) == true)
                    {
                        playerDead = true;
                    }
                }
                if (playerDead == true && gameOverShow == false)
                {
                    gameOverShow = true;
                    gameOver();
                }
                if (Console.KeyAvailable)
                {
                    userKey = Console.ReadKey(true);                    
                    if (userKey.Key == ConsoleKey.X)
                    {
                        Console.Clear();
                        for (int i = 0; i < enemies.Length; i++)
                        {
                            enemies[i].ResetEnemigo();
                        }
                        playerDead = false;
                        DrawMap();
                        player.ResetPersonaje();
                        if (j2 == true)
                            player2.ResetPersonaje();
                        gameOverShow = false;
                    }
                    if (playerDead == false)
                    {
                        player.MoveCharacter(userKey);
                        if (j2 == true)
                        {
                            player2.MoveCharacter(userKey);
                            if (player.posX() == player2.posX() && player.posY() == player2.posY())
                            {
                                player.SetPos(player.oldX(), player.oldY());
                                player2.SetPos(player2.oldY(), player2.oldY());
                            }
                        }
                        switch (userKey.Key)
                        {
                            /*case ConsoleKey.NumPad1:
                                player.SetPos(79, 24);//Limits
                                break;
                            case ConsoleKey.Spacebar:
                                player.SetPos(0, 0);
                                break;*/
                            case ConsoleKey.Escape:
                                finishGame = true;
                                break;
                        }
                    }
                }
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
