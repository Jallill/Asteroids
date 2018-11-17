using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class Bullet {

        public float x { get; private set;}
        public float y { get; private set; }
        public float r { get; private set; }
        public float tragectoryAngle { get; private set; }
        //public int height { get; private set; }
        //public int width { get; private set; }
        public float pivotX { get; private set; }
        public float pivotY { get; private set; }
        public Texture currentT { get; private set; }

        public bool bulletDisapear;

        private float dirX;
        private float dirY;
        private Texture tIdle;
        private float speed = 300;
        private float travelDistance = 1000;
        

        public Bullet(float x, float y, float tragectoryAngle, float xOffSet, float yOffSet, float r) {
            this.tragectoryAngle = tragectoryAngle;

            dirX = (float)Math.Cos((tragectoryAngle) * Math.PI / 180);
            dirY = (float)Math.Sin((tragectoryAngle) * Math.PI / 180);

            this.x = x + dirX * xOffSet;
            this.y = y + dirY * yOffSet;
            this.r = r;
            bulletDisapear = false;
            
            loadAnimation();

            pivotX = r / 2;
            pivotY = r / 2;
        }

        public void loadAnimation() {
            tIdle = Game.GetTexture("Texturas/Bullet.png");
            currentT = tIdle;
        }

        public void update(float deltaTime) {

            this.travelDistance -= (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) * deltaTime;

            if (travelDistance <= 0) {
                bulletDisapear = true;
            } else {
                x += speed * dirX * deltaTime;
                y += speed * dirY * deltaTime;
            }

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
