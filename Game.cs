using System;

namespace RaceGame
{
    public class Game
    {
        public Player player1;
        public Player player2;
        byte[,] GameField;

        public Game()
        {
            GameField = new byte[1024,1024];
        }


        public void CreatePlayers()
        {
            
        }
    }
}
