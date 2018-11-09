using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
     class Player {

        public float life = 3;
        public float height { get; private set; }
        public float width { get; private set; }
        public float x { get; private set; }
        public float y { get; private set; }
        public float angle { get; private set; }

        private float rotationSpeed = 200;
        private float acceleration = 0;
        private float accelerationPower = 1;
        private float speed = 200;
        
        public Texture currentT { get; private set; }
        public Texture tIdle;
        
        public Player(float x = 0, float y = 0) {
            this.x = x;
            this.y = y;
            this.angle = 0;
            animationLoad();
        }

        private void animationLoad() 
        {
            tIdle = Game.GetTexture("Texturas/SpaceShip.png");
            currentT = tIdle;
            height = tIdle.Height;
            width = tIdle.Width;
        }
        
        public void inputKey(float deltaTime) 
        {
            
            if (Game.GetKey(Keys.D)) angle += rotationSpeed * deltaTime;
            if (Game.GetKey(Keys.A)) angle -= rotationSpeed * deltaTime;

            float dirx = (float)Math.Cos((angle) * Math.PI / 180);
            float diry = (float)Math.Sin((angle) * Math.PI / 180);

            if (Game.GetKey(Keys.W)) {
                if (acceleration < 1)  acceleration += accelerationPower * deltaTime; 

            } else {
                acceleration -= accelerationPower * deltaTime;
                if (acceleration < 0) acceleration = 0;
            }
            if (acceleration > 0) {
                x += speed * dirx * acceleration * deltaTime;
                y += speed * diry * acceleration * deltaTime;
            }


            Console.WriteLine("Angle:(" + angle + ") x:(" + x + ") y:(" + y + ") cosAngle:(" + Math.Cos(angle) + ") sinAngle(" + Math.Sin(angle) + ")" + ") acceleration(" + acceleration + ")");

        }

        public void update(float Tiempo) {

            if (x + width > 800) {
                x = 800 - width;
            }
            if (x < 0) {
                x = 0;
            }
            if (y + height > 600) {
                y = 600 - height;
            }
            if (y < 0) {
                y = 0;
            }

        }

        public void checkDeath() {
            bool death = life <= 0;
            if (death) {
                x = 0f;
                y = 0f;
                life = 100;
            }
        }

    }
}
