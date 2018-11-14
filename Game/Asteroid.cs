using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class Asteroid {
        public float x { get; private set; }
        public float y { get; private set; }
        public float angle { get; private set; }
        public float r { get; private set; }
        public float pivotX { get; private set; }
        public float pivotY { get; private set; }
        public Texture tIdle { get; private set; }
        public Texture currentT { get; private set; }

        private float rotationSpeed = 100;
        private float speed = 200;
        private float dirX;
        private float dirY;
        

        public Asteroid(float x, float y,float angle, float r) {
            this.x = x;
            this.y = y;
            this.angle = angle;
            dirX = (float)Math.Cos((angle) * Math.PI / 180);
            dirY = (float)Math.Sin((angle) * Math.PI / 180);
            this.r = r;
            pivotX = r / 2;
            pivotY = r / 2;
            loadAnimation();
        }

        public void loadAnimation() {
            tIdle = Game.GetTexture("Texturas/Asteroid.png");
            currentT = tIdle;
        }

        public void update(float deltaTime) {
            x += speed * dirX * deltaTime;
            y += speed * dirY * deltaTime;

            angle += rotationSpeed * deltaTime;

            if (x > 800 + r) {
                x = 0;
            }
            if (x < -r) {
                x = 800;
            }
            if (y > 600 + r) {
                y = 0;
            }
            if (y < -r) {
                y = 600;
            }
        }

    }
}
