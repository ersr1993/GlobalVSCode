using System.Diagnostics.Eventing.Reader;
using System.Drawing;

namespace StandardTools.Analysis
{
    public class Player : MBehaviour, IGameObject
    {
        public Player(
                //IDebug debug
                GameEngine engine
            ) : base(engine)
        {
            this.name = "Player";
        }
        public void Start()
        {
            this._debug.Log(name);
        }
        public override void Update()
        {
            UserInteraction();
            base.Update();
        }
        private void UserInteraction()
        {
            ConsoleKey userInput;

            userInput = AskUserKey("Press Space", ConsoleColor.Yellow);

            if (IsCorrect(userInput))
            {
                Eureka();
            }
            else
            {
                Wrong();
            }
        }
        private bool IsCorrect(ConsoleKey userInput)
        {
            bool isCorrect;
            ConsoleKey expected;
            expected = ConsoleKey.Spacebar;
            return userInput == expected;
        }
        // ---
        private void Wrong()
        {
            MyWriteLine("Wrong", ConsoleColor.Red);
        }
        private void Eureka()
        {
            MyWriteLine("Ok ...\n", ConsoleColor.Green);
            Console.ReadKey();
            this.engine.PlayPause.Invoke();
        }
        // ---
        private ConsoleKey AskUserKey(string msg, ConsoleColor color)
        {
            ConsoleKey output;
            MyWriteLine(msg, color);
            output = Console.ReadKey().Key;
            return output;
        }
        private void MyWriteLine(string someStr, ConsoleColor myColor)
        {
            Console.ForegroundColor = myColor;
            Console.WriteLine(someStr ?? "null", myColor);
            Console.ResetColor();
        }
    }
}
