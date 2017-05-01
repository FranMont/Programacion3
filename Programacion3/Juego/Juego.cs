using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Juego
{
    class Juego
    {
        static private string ciudad;
        static private string clima;
        static private int scoreP1;
        static private int scoreP2;
        static private int highScore;
        private bool j2 = false;
        FileStream fs;
        BinaryWriter bw;
        public static void SetConsoleClima()
        {
            if (clima == "Cloudy" || clima == "Breezy")
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (clima == "Sunny")
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (clima == "Thunderstorms")
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static bool CheckClima()
        {
            Console.Clear();
            Console.Write("Escriba una Ciudad: ");
            ciudad = Console.ReadLine();
            WebRequest req = WebRequest.Create("https://query.yahooapis.com/v1/public/yql?q=select%20item.condition.text%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" + ciudad + "%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
            WebResponse respuesta = req.GetResponse();
            Stream stream = respuesta.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            JObject data = JObject.Parse(sr.ReadToEnd());
            int errorfound = (int)data["query"]["count"];
            if (errorfound == 0)
            {
                Console.WriteLine("Esa ciudad no existe");
                return false;
            }
            else
            {
                clima = (string)data["query"]["results"]["channel"]["item"]["condition"]["text"];
                Console.WriteLine("Tiempo: " + clima);
                return true;
            }
        }
        public Juego()
        {            
            if (!File.Exists("HighScore.dat"))
            {
                fs = File.Create("HighScore.dat");
                bw = new BinaryWriter(fs);
                highScore = 0;
                bw.Write(highScore);
                bw.Close();
                fs.Close();
            }
            else
            {
                BinaryReader br;
                fs = File.OpenRead("HighScore.dat");
                br = new BinaryReader(fs);
                highScore = br.ReadInt32();
                br.Close();
                fs.Close();
            }
            scoreP1 = -1;
            scoreP2 = -1;
            if (CheckForInternetConnection() == true)
            {
                bool cityExist = false;
                while(cityExist == false)
                {
                    cityExist = CheckClima();
                }
                if (cityExist == true)
                {
                    SetConsoleClima();
                }
            }
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        static public void giveScoreToPlayer(int player, int score)
        {
            if (player == 1)
            {
                scoreP1 += score;
                if(scoreP1 > highScore)
                {
                    highScore = scoreP1;
                }
            }
            else if(player == 2)
            {
                scoreP2 += score;
                if(scoreP2 > highScore)
                {
                    highScore = scoreP2;
                }
            }
        }
        static void ResetScores()
        {
            scoreP1 = 0;
            scoreP2 = 0;
        }
        static void UpdateScore()
        {
            if(scoreP1 >= 0)
            {
                Console.SetCursorPosition(0, 0); Console.Write("Puntaje P1: " + scoreP1);
            }
            if(scoreP2 >= 0)
            {
                Console.SetCursorPosition(58, 0); Console.Write("Puntjae P2: " + scoreP2);
            }
            Console.SetCursorPosition(35, 0); Console.Write("Highscore: " + highScore);
        }
        static void gameOver()
        {
            ResetScores();
            Console.Clear();
            Console.SetCursorPosition(20, 12);
            Console.Write("GAME OVER! Presione 'x' para reiniciar.");
        }
        static void DrawMap()// ╣ ╠ ╩ ╦ ╚ ╝ ╗ ╔ ═ ║
        {
            Console.SetCursorPosition(0, 2); Console.Write("╔");
            Console.SetCursorPosition(79, 2); Console.Write("╗");
            Console.SetCursorPosition(0, 24); Console.Write("╚");
            Console.SetCursorPosition(79, 24); Console.Write("╝");
            for (int i = 1; i < 79; i++)
            {
                Console.SetCursorPosition(i, 2);
                Console.Write("═");
                Console.SetCursorPosition(i, 24);
                Console.Write("═");
            }
            for (int i = 3; i < 24; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("║");
                Console.SetCursorPosition(79, i);
                Console.Write("║");
            }
        }
        static void drawMenu()
        {

            Console.SetCursorPosition(28, 18);
            Console.Write("Highscore: " + highScore);
            Console.SetCursorPosition(22, 20);
            Console.Write("Presione 1 para jugar solo");
            Console.SetCursorPosition(20, 21);
            Console.Write("Presione 2 para jugar de a dos");
            Console.SetCursorPosition(24, 22);
            Console.Write("Presione Esc para salir.");
        }
        public void Iniciar()
        {
            Random rnd = new Random();

            int randspawnY = rnd.Next(3, 23);
            int randspawnX = rnd.Next(1, 77);
            bool playerDead = false;
            bool finishGame = false;
            bool gameOverShow = false;
            bool inMenu = false;
            bool showMenu = false;
            ConsoleKeyInfo userKey;
            Personaje player = new Personaje();
            Personaje player2 = new Personaje();
            Enemigo[] enemies = new Enemigo[5];
            Enemigo[] enemiesMove = new Enemigo[4];
            Console.Clear();
            while (inMenu == false)
            {
                if (showMenu == false)
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
                        inMenu = true;
                    }
                    else if (userKey.Key == ConsoleKey.NumPad1)
                    {
                        j2 = false;
                        inMenu = true;
                        scoreP1 = 0;
                    }
                    else if (userKey.Key == ConsoleKey.NumPad2)
                    {
                        j2 = true;
                        inMenu = true;
                        scoreP1 = 0;
                        scoreP2 = 0;
                    }

                }
            }
            Console.Clear();
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Obstaculo(randspawnX, randspawnY, 'X');
                enemies[i].Draw();
                randspawnY = rnd.Next(3, 23);
                randspawnX = rnd.Next(1, 77);
            }
            for (int i = 0; i < enemiesMove.Length; i++)
            {
                randspawnX = rnd.Next(2, 76);
                enemiesMove[i] = new Enemlr(randspawnX, i + i * i + 4, 'Y');
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
                    if (enemies[i].collision(player.posX(), player.posY()) == true && gameOverShow == false)
                    {
                        Personaje.setVidas(Personaje.getVidas() - 1);
                        if (Personaje.getVidas() > 0)
                        {
                            player.ResetPersonaje();
                        }
                        else
                        {
                            playerDead = true;
                        }
                    }
                    if (enemies[i].collision(player2.posX(), player2.posY()) == true && j2 == true && gameOverShow == false)
                    {
                        Personaje.setVidas(Personaje.getVidas() - 1);
                        if (Personaje.getVidas() > 0)
                        {                            
                            player2.ResetPersonaje();
                        }
                        else
                        {
                            playerDead = true;
                        }
                    }
                    if (gameOverShow == false)
                    {
                        enemies[i].Draw();
                    }
                }
                for (int i = 0; i < enemiesMove.Length; i++)
                {
                    if (playerDead == false)
                    {
                        enemiesMove[i].EnemiesMove();
                    }
                    if (enemiesMove[i].collision(player2.posX(), player2.posY()) == true && j2 == true && gameOverShow == false)
                    {
                        Personaje.setVidas(Personaje.getVidas() - 1);
                        if (Personaje.getVidas() > 0)
                        {
                            player2.ResetPersonaje();
                        }
                        else
                        {
                            playerDead = true;
                        }
                    }
                    if (enemiesMove[i].collision(player.posX(), player.posY()) == true && gameOverShow == false)
                    {
                        Personaje.setVidas(Personaje.getVidas() - 1);
                        if (Personaje.getVidas() > 0)
                        {
                            player.ResetPersonaje();
                        }
                        else
                        {
                            playerDead = true;
                        }
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
                    if (userKey.Key == ConsoleKey.X && gameOverShow == true)
                    {
                        Console.Clear();
                        for (int i = 0; i < enemies.Length; i++)
                        {
                            enemies[i].ResetEnemigo();
                        }
                        playerDead = false;
                        DrawMap();
                        Personaje.setVidas(Personaje.MAX_VIDAS);
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
                            case ConsoleKey.Escape:
                                finishGame = true;
                                break;
                        }
                    }
                }
                Personaje.UpdateVidas();
                UpdateScore();
                System.Threading.Thread.Sleep(50);
            }
            fs = File.OpenWrite("HighScore.dat");
            bw = new BinaryWriter(fs);
            bw.Write(highScore);
            bw.Close();
            fs.Close();
        }
    }
}
