using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class Bullet {

        public float x { get; private set;}
        public float y { get; private set; }
        public float tragectoryAngle { get; private set; }
        public int height;
        public int width;
        public bool bulletDisapear { get; private set; }
        public Texture currentT { get; private set; }
        private Texture tIdle;
        private float speed = 300;
        private float travelDistance = 1000;
        

        public Bullet(float x, float y, float tragectoryAngle) {
            this.x = x;
            this.y = y;
            this.tragectoryAngle = tragectoryAngle;
            this.bulletDisapear = false;
            loadAnimation();
        }

        public void loadAnimation() {
            tIdle = Game.GetTexture("Texturas/Bullet.png");
            currentT = tIdle;
            height = tIdle.Height;
            width = tIdle.Width;
        }
        public void update(float deltaTime) {
            float dirx = (float)Math.Cos((tragectoryAngle) * Math.PI / 180);
            float diry = (float)Math.Sin((tragectoryAngle) * Math.PI / 180);

            this.travelDistance -= (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) * deltaTime;

            if (travelDistance <= 0) {
                bulletDisapear = true;
            } else {
                x += speed * dirx * deltaTime;
                y += speed * diry * deltaTime;
            }

            if (x > 800 + width) {
                x = 0;
            }
            if (x < -height) {
                x = 800;
            }
            if (y > 600 + height) {
                y = 0;
            }
            if (y < -height) {
                y = 600;
            }

        }
    }
}
