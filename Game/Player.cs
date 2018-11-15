﻿using System;
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
        public float pivotX { get; private set; }
        public float pivotY { get; private set; }

        private bool rotate = true;
        private Direction rotateDirection = Direction.None;
        private float dirX;
        private float dirY;
        private int maxBullets = 4;
        private float timeBetweenShot = 0.5f;
        private float waitUntilNextBullet = 0;
        private float rotationSpeed = 200;
        private float acceleration = 0;
        private float accelerationPower = 1;
        private float speed = 200;

        enum Direction { Left = -1, None = 0, Right = 1}

        public Player(float x = 0, float y = 0) {
            this.x = x;
            this.y = y;
            angle = 0;

            bulletsShot = new List<Bullet>();
            animationLoad();

            pivotX = width / 2;
            pivotY = height / 2;
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
                rotateDirection = Direction.Right;
                rotate = true;
            }

            if (Game.GetKey(Keys.A)) {
                rotateDirection = Direction.Left;
                rotate = true;
            }

            if (rotate) {
                angle += rotationSpeed * deltaTime * (float)rotateDirection;
                dirX = (float)Math.Cos((angle) * Math.PI / 180);
                dirY = (float)Math.Sin((angle) * Math.PI / 180);
                rotate = false;
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
                    waitUntilNextBullet = timeBetweenShot;
                }
                
            }
            waitUntilNextBullet -= deltaTime;
            Console.WriteLine("Angle:(" + angle + ") x:(" + x + ") y:(" + y + ") cosAngle:(" + Math.Cos(angle) + ") sinAngle(" + Math.Sin(angle) + ")" + ") acceleration(" + acceleration + ")");

        }

        public void update(float deltaTime) {

            if (x > 800 + height) {
                x = -height;
            }
            if (x < -height) {
                x = 800 + height;
            }
            if (y > 600 + height) {
                y = -height;
            }
            if (y < -height) {
                y = 600 + height;
            }


            bulletsShot.RemoveAll(bullet => bullet.bulletDisapear == true);

            if (acceleration > 0) {
                x += speed * dirX * acceleration * deltaTime;
                y += speed * dirY * acceleration * deltaTime;
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
            if (bulletsShot.Count < maxBullets) {
                Bullet bullet = new Bullet(this.x, this.y, angle, pivotX, pivotY, 10);
                bulletsShot.Add(bullet);
            }
        }

    }
}
