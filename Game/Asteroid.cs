﻿using System;
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
        public bool destroyAsteroid { get; private set; }
        public Texture tIdle { get; private set; }
        public Texture currentT { get; private set; }
        public int level { get; private set; }
        public float rotationAngle { get; private set; }
        public float scale { get; private set; }

        private float rotationSpeed = 100;
        private float speed = 200;
        private float dirX;
        private float dirY;


        public Asteroid(float x, float y, float angle, int level) {

            this.x = x;
            this.y = y;
            this.angle = angle;
            this.r = r;
            this.level = level;

            dirX = (float)Math.Cos((angle) * Math.PI / 180);
            dirY = (float)Math.Sin((angle) * Math.PI / 180);
            
            destroyAsteroid = false;
            rotationAngle = 0;

            loadAnimation();

            scaleByLevel();
        }

        void loadAnimation() {
            tIdle = Game.GetTexture("Texturas/Asteroid.png");
            currentT = tIdle;
        }

        void scaleByLevel() {
            switch (level) {
                case (1):
                    r = 32;
                    scale = 1;
                    break;

                case (2):
                    r = 16;
                    scale = 0.5f;
                    break;

                case (3):
                    r = 8;
                    scale = 0.25f;
                    break;

            }
        }

        public void update(float deltaTime) {
            x += speed * dirX * deltaTime;
            y += speed * dirY * deltaTime;

            rotationAngle += rotationSpeed * deltaTime;

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

        public int checkCollision(List<Bullet> bullets) {
            int points = 0;
            foreach (Bullet bullet in bullets) {
                var radius = this.r + bullet.r;
                var deltaX = this.x - bullet.x;
                var deltaY = this.y - bullet.y;
                if (Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2) <= Math.Pow(radius, 2)) {
                    destroyAsteroid = true;
                    bullet.bulletDisapear = true;
                    points += 100;
                }
            }
            return points;
        }

        public int checkCollision(Player player) {
            int points = 0;
            float deltaX = x - Math.Max(player.x, Math.Min(x, player.x + player.width));
            float deltaY = y - Math.Max(player.y, Math.Min(y, player.y + player.height));
            if (Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2) < Math.Pow(r,2)) {
                destroyAsteroid = true;
                points += 100;
                player.lives--;
                player.restartPosition(400, 300);
                player.startInvulneraibility(3);
            }
            return points;
        }

    }
}
