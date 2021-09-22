using System;

namespace Patterns.Patterns
{
    class GameMemento
    {
        private GameState state;

        public GameState GameState => state;

        public GameMemento(GameState gameState)
        {
            this.state = new GameState(gameState.Health, gameState.KilledMen);
        }
    }

    class GameState
    {
        public int Health { get; set; }
        public int KilledMen { get; set; }

        public GameState(int health, int killedMen)
        {
            this.Health = health;
            this.KilledMen = killedMen;
        }
    }

    class Game
    {
        private GameState state = new GameState(100, 0);

        public void Play()
        {
            state.Health = state.Health - 5;
            state.KilledMen = state.KilledMen + 1;
        }

        public GameMemento SaveGame()
        {
            return new GameMemento(state);
        }

        public void LoadGame(GameMemento memento)
        {
            this.state = new GameState(memento.GameState.Health, memento.GameState.KilledMen);
        }

        public GameState Info => state;
       
    }

    class GamePlayer
    {
        private Game game = new Game();
        private GameMemento gameState;

        public void PlayGame()
        {
            game.Play();
        }

        public void F5()
        {
            gameState = game.SaveGame();
        }

        public void F9()
        {
            game.LoadGame(gameState);
        }

        public void DisplayGameInfo()
        {
            Console.WriteLine($"Health: {game.Info.Health}, KilledMen: {game.Info.KilledMen}");
        }
    }
}
