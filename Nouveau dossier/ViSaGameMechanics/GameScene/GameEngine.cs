using System.Runtime.CompilerServices;

namespace StandardTools.Analysis
{
    public class GameEngine
    {
        //public delegate void SwitchPlayPause();
        //public SwitchPlayPause PlayPause { get; set; }
        public Action PlayPause;
        public IDebug Debug { get; init; }
        private GameScene _mainScene { get; init; }
        public statePlayPause playState { get; private set; } = statePlayPause.play;
        public GameEngine(
                IDebug debug
            //GameScene scene
            )
        {
            Debug = debug;
            _mainScene = new GameScene(this);
            this.PlayPause = SwitchGame_PlayPause;
            //Scene = scene;
        }

        public void StartGame()
        {
            this.playState = statePlayPause.play;
            this._mainScene.Play();
        }

        private void SwitchGame_PlayPause()
        {
            switch (this.playState)
            {
                case statePlayPause.play:
                    this.playState = statePlayPause.pause;
                    break;
                case statePlayPause.pause:
                    this.playState = statePlayPause.play;
                    break;
            }
        }
    }
}
