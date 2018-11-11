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
        public List<Bullet> bulletsShot { get; private set; }
        public Texture currentT { get; private set; }
        public Texture tIdle { get; private set; }

        private float dirx;
        private float diry;
        private float timeBeetweenBullets = 0.1f;
        private float waitUntilNextBullet = 0;
        private float rotationSpeed = 200;
        private float acceleration = 0;
        private float accelerationPower = 1;
        private float speed = 200;

        public Player(float x = 0, float y = 0) {
            this.x = x;
            this.y = y;
            this.angle = 0;
            dirx = (float)Math.Cos((angle) * Math.PI / 180);
            diry = (float)Math.Sin((angle) * Math.PI / 180);
            bulletsShot = new List<Bullet>();
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

            if (Game.GetKey(Keys.D)) {
                angle += rotationSpeed * deltaTime;
                dirx = (float)Math.Cos((angle) * Math.PI / 180);
                diry = (float)Math.Sin((angle) * Math.PI / 180);
            }

            if (Game.GetKey(Keys.A)) {
                angle -= rotationSpeed * deltaTime;
                dirx = (float)Math.Cos((angle) * Math.PI / 180);
                diry = (float)Math.Sin((angle) * Math.PI / 180);
            }

            if (Game.GetKey(Keys.W)) {
                if (acceleration < 1)  acceleration += accelerationPower * deltaTime; 

            } else {
                acceleration -= accelerationPower * deltaTime;
                if (acceleration < 0) acceleration = 0;
            }

            if (Game.GetKey(Keys.SPACE)) {
                if (waitUntilNextBullet <= 0) {
                    shootBullet();
                    waitUntilNextBullet = timeBeetweenBullets;
                } else {
                    waitUntilNextBullet -= deltaTime;
                }
                
            }

            bulletsShot.RemoveAll(bullet => bullet.bulletDisapear == true);
            
            if (acceleration > 0) {
                x += speed * dirx * acceleration * deltaTime;
                y += speed * diry * acceleration * deltaTime;
            }

            
            Console.WriteLine("Angle:(" + angle + ") x:(" + x + ") y:(" + y + ") cosAngle:(" + Math.Cos(angle) + ") sinAngle(" + Math.Sin(angle) + ")" + ") acceleration(" + acceleration + ")");

        }

        public void update(float Tiempo) {

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

        public void checkDeath() {
            bool death = life <= 0;
            if (death) {
                x = 0f;
                y = 0f;
                life = 100;
            }
        }

        private void shootBullet() {
            if (bulletsShot.Count < 3) {
                Bullet bullet = new Bullet(this.x, this.y, angle);
                bulletsShot.Add(bullet);
            }
        }

    }
}
