using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class Button {
        public float x { get; private set; }
        public float y { get; private set; }
        public Text text { get; private set; }
        public Button buttonUp { get; private set; }
        public Button buttonDown { get; private set; }

        private float timer = 0;
        private float timeBetween = 0.1f;


        public Button(float x, float y, string text, float width, float height) {
            this.x = x;
            this.y = y;

            this.text = new Text(text, this.x, this.y, width, height);
        }

        public Button update(float deltaTime) {

            timer += deltaTime;

            if (Game.GetKey(Keys.W) && timer >= timeBetween) {
                timer = 0f;
                return getUp();
            } else if (Game.GetKey(Keys.S) && timer >= timeBetween) {
                timer = 0f;
                return getDown();
            } else return this;

        }

        public void setButtons(Button up, Button down) {
            buttonUp = up;
            buttonDown = down;
        }

        public Button getUp() {
            if (buttonUp != null) {
                return buttonUp;
            } else return this;
        }

        public Button getDown() {
            if (buttonDown != null) {
                return buttonDown;
            } else return this;
        }


    }
}
