using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class MainMenu {

        List<Button> buttons = new List<Button>();

        Text mainMenuTitle;

        Button buttonStartGame;
        Button buttonHighScores;
        Button buttonExit;
        Button currentButton;

        bool canPressSpace = true;
        float timerSpace = 0;
        float maxTimerSpace = 0.3f;

        Arrow arrow;

        public MainMenu() {
            mainMenuTitle = new Text("ASTEROIDS", 260, 100);

            buttonStartGame = new Button(260,200,new Text("START GAME", 260, 200));
            buttonHighScores = new Button(260, 260, new Text("HIGH SCORES", 260, 260));
            buttonExit = new Button(260, 320, new Text("EXIT", 260, 320));

            buttons.Add(buttonStartGame);
            buttons.Add(buttonHighScores);
            buttons.Add(buttonExit);

            buttonStartGame.setButtons(null, buttonHighScores);
            buttonHighScores.setButtons(buttonStartGame, buttonExit);
            buttonExit.setButtons(buttonHighScores, null);

            currentButton = buttonStartGame;

            arrow = new Arrow();
            arrow.update(currentButton.x, currentButton.y);
        }

        public void input(float deltaTime) {
            if (Game.GetKey(Keys.SPACE) && canPressSpace) {
                canPressSpace = false;
                timerSpace = 0f;
                enter();
            }
        }

        public void update(float deltaTime) {
            timerSpace += deltaTime;

            if (timerSpace >= maxTimerSpace) {
                canPressSpace = true;
                timerSpace = 0f;
            }

            currentButton = currentButton.update(deltaTime);

            arrow.update(currentButton.x, currentButton.y);
        }


        public void render() {
            Game.Clear(0, 0, 0);
            mainMenuTitle.drawText();

            foreach(Button button in buttons) {
                button.text.drawText();
            }

            Game.Draw("Texturas/SpaceShip.png", arrow.x, arrow.y,1,1,0,25,16);
        }

        void enter() {
            if (currentButton == buttonStartGame) {
                Program.changeState(Program.States.MainGame);
            } else if (currentButton == buttonHighScores) {
                Program.changeState(Program.States.HighScore);
            } else if (currentButton == buttonExit) {
                Program.changeState(Program.States.Exit);
            }
        }
    }
}
