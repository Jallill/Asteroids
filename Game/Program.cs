using System;
using System.Collections.Generic;

namespace TestEngine {
    public class Program {

        const int MS_PER_FRAME = 1000 / 60;


        enum States { MainGame }

        static States state = States.MainGame;

        static bool loop = true;
        static float deltaTime;
        static DateTime lastFrameTime = DateTime.Now;
        static DateTime startDate;

        static MainGame mainGame;

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

            startDate = DateTime.Now;
        }


        static void input() {
            switch (state) {
                case (States.MainGame):
                    mainGame.input(deltaTime);
                    break;
            }

        }
        
        static void update() {
            switch (state) {
                case (States.MainGame):
                    mainGame.update(GetCurrentTime(), deltaTime);
                    break;
            }

        }

        static void render() {
            switch (state) {
                case (States.MainGame):
                    mainGame.render();
                    break;
            }

            //testing
            //drawBoxcollider(P1.x, P1.y, P1.width, P1.height);
            //drawBoxcollider(enemy.x, enemy.y, enemy.width,enemy.height);

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

        public static void drawBoxcollider(float x, float y, float width, float height) {
            
            for (float i = (x); i < width + x; i++) {
                Game.Draw("pixel.png", i , y);
                Game.Draw("pixel.png", i , y + height);
            }
            for(float j = (y + height); j > y; j--) {
                Game.Draw("pixel.png", x, j);
                Game.Draw("pixel.png", x + width, j);
            }
        }


    }
}