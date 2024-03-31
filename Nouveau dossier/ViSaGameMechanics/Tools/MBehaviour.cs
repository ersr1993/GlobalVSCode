using Microsoft.Extensions.DependencyInjection;

namespace StandardTools.Analysis
{
    public abstract class MBehaviour : IGameObject
    {
        public virtual string name { get; protected set; }

        protected virtual GameEngine engine { get; private set; }
        protected virtual IDebug _debug { get; private set; }
        public MBehaviour(
                        GameEngine gameEngine
                        //IDebug debug
                    )
        {
            //_debug = ActivatorUtilities.GetServiceOrCreateInstance<IDebug>();
            _debug = gameEngine.Debug;
            this.engine = gameEngine;
        }
        public virtual void Start() { }
        public virtual void Update() { }
    }
}
