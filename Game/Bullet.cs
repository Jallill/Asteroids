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
        public int height { get; private set; }
        public int width { get; private set; }
        public float pivotX { get; private set; }
        public float pivotY { get; private set; }
        public bool bulletDisapear { get; private set; }
        public Texture currentT { get; private set; }

        private float dirx;
        private float diry;
        private Texture tIdle;
        private float speed = 300;
        private float travelDistance = 1000;
        

        public Bullet(float x, float y, float tragectoryAngle, float xOffSet, float yOffSet) {
            this.tragectoryAngle = tragectoryAngle;

            dirx = (float)Math.Cos((tragectoryAngle) * Math.PI / 180);
            diry = (float)Math.Sin((tragectoryAngle) * Math.PI / 180);

            this.x = x + dirx * xOffSet;
            this.y = y + diry * xOffSet;

            bulletDisapear = false;
            
            loadAnimation();

            pivotX = width / 2;
            pivotY = height / 2;
        }

        public void loadAnimation() {
            tIdle = Game.GetTexture("Texturas/Bullet.png");
            currentT = tIdle;
            height = tIdle.Height;
            width = tIdle.Width;
        }
        public void update(float deltaTime) {

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
