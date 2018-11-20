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
            mainMenuTitle = new Text("ASTEROIDS", 245, 100);

            buttonStartGame = new Button(260, 300, "START GAME", 30, 35);
            buttonHighScores = new Button(260, 360, "HIGH SCORES", 30, 35);
            buttonExit = new Button(260, 420, "EXIT", 30, 35);

            buttons.Add(buttonStartGame);
            buttons.Add(buttonHighScores);
            buttons.Add(buttonExit);

            buttonStartGame.setButtons(null, buttonHighScores);
            buttonHighScores.setButtons(buttonStartGame, buttonExit);
            buttonExit.setButtons(buttonHighScores, null);

            currentButton = buttonStartGame;

            arrow = new Arrow(100);
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
            mainMenuTitle.drawText();

            foreach (Button button in buttons) {
                button.text.drawText();
            }

            Game.Draw("Texturas/SpaceShip.png", arrow.x, arrow.y, 1, 1, 0, arrow.pivotX, arrow.pivotY);
        }

        void enter() {
            if (currentButton == buttonStartGame) {
                Program.changeState(Program.States.MainGame);
            }
            else if (currentButton == buttonHighScores) {
                Program.changeState(Program.States.HighScore);
            }
            else if (currentButton == buttonExit) {
                Program.changeState(Program.States.Exit);
            }
        }
    }
}
