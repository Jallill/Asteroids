using System;
using System.Collections.Generic;

namespace TestEngine {
    public class Program {

        const int MS_PER_FRAME = 1000 / 60;


        public enum States { MainMenu, MainGame, HighScore, Exit }

        public static States currentState = States.MainMenu;

        static bool loop = true;
        static float deltaTime;
        static DateTime lastFrameTime = DateTime.Now;
        static DateTime startDate;

        static MainGame mainGame;
        static MainMenu mainMenu;

        static void Main(string[] args) {
            int sleepTime;

            setUp();

            while (loop) {
                float start = GetCurrentTime();
                calculateDeltaTime();

                input();
                update();
                render();

                sleepTime = Convert.ToInt32(start + MS_PER_FRAME - GetCurrentTime());
                
                if (sleepTime > 0) {
                    System.Threading.Thread.Sleep(sleepTime);
                }

                
            }
        }

        static void setUp() {
            Game.Initialize("Parcial",800,600,false);

            mainGame = new MainGame();
            mainMenu = new MainMenu();
            
            startDate = DateTime.Now;
        }


        static void input() {
            switch (currentState) {
                case (States.MainMenu):
                    mainMenu.input(deltaTime);
                    break;
                case (States.MainGame):
                    mainGame.input(deltaTime);
                    break;
            }

        }
        
        static void update() {
            switch (currentState) {
                case (States.MainMenu):
                    mainMenu.update(deltaTime);
                    break;
                case (States.MainGame):
                    mainGame.update(deltaTime);
                    break;

            }

        }

        static void render() {
            switch (currentState) {
                case (States.MainMenu):
                    mainMenu.render();
                    break;
                case (States.MainGame):
                    mainGame.render();
                    break;
            }
            Game.Show();
        }

        static float GetCurrentTime() {
            TimeSpan diffStart = DateTime.Now.Subtract(startDate);
            return (float)(diffStart.TotalMilliseconds);
        }
        
        static void calculateDeltaTime() {
            TimeSpan deltaSpan = DateTime.Now - lastFrameTime;
            deltaTime = (float)deltaSpan.TotalSeconds;
            lastFrameTime = DateTime.Now;
        }

        public static void loadGame() {

        }

        public static void changeState(States state) {
            currentState = state;
        }
    }
}