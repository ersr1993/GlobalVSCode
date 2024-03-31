using Microsoft.Extensions.Logging;

namespace StandardTools.Analysis
{
    public class GameScene
    {
        public List<IGameObject> gameObjects { get; private set; }
        private GameEngine _engine;
        public GameScene(
                            GameEngine engine
                        )
        {
            _engine = engine;
            GameObjectsInit();
        }

        public void Play()
        {
            GameObjectsStart();
            while (this._engine.playState == statePlayPause.play)
            {
                foreach (IGameObject go in gameObjects)
                {
                    go.Update();
                }
            }
        }
        
        private void GameObjectsInit()
        {
            this.gameObjects = new List<IGameObject>()
            {
                new Player(_engine),
            };
        }
        private void GameObjectsStart()
        {
            foreach (IGameObject go in gameObjects)
            {
                go.Start();
            }
        }
    }
}
