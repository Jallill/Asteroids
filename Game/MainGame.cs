using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class MainGame {

        bool pause = true;
        int gameLevel;



        Player player;

        List<Asteroid> asteroids;
        List<Asteroid> spawnedAsteroids;

        Random random = new Random((int)DateTime.Now.Ticks);

        Text countDownText;
        float countDown;

        Text points;
        int pointCount;

        public MainGame() {
                restart();
        }

        public void input(float deltaTime) {
            if (!pause) {
                player.inputKey(deltaTime);
            }
        }

        public void update(float deltaTime) {
            if (!pause) {
                player.update(deltaTime);

                foreach (Bullet bullet in player.bulletsShot) {
                    bullet.update(deltaTime);
                }

                spawnedAsteroids = new List<Asteroid>();

                foreach(Asteroid asteroid in asteroids) {
                    asteroid.update(deltaTime);

                    asteroid.checkCollision(player);

                    if (player.bulletsShot.Count > 0) {
                        pointCount += asteroid.checkCollision(player.bulletsShot);
                        points.changeText(Utils.Truncate("00000000" + pointCount.ToString(),8));
                    }

                    if (asteroid.destroyAsteroid == true) {
                        switch (asteroid.level) {
                            case (1):
                                spawnAsteroids(2, 2, asteroid.x, asteroid.y, true);
                                break;
                            case (2):
                                spawnAsteroids(2, 3, asteroid.x, asteroid.y, true);
                                break;
                        }
                    }
                }

                if (spawnedAsteroids.Count > 0) asteroids.AddRange(spawnedAsteroids);

                asteroids.RemoveAll(asteroid => asteroid.destroyAsteroid == true);

                if(asteroids.Count() == 0) {
                    loadNewLevel();
                }

            } else if (countDown > 0) {
                countDown -= deltaTime;
                countDownText.changeText(((int)countDown+1).ToString());
            } else {
                pause = false;
            }

        }

        public void render() {

            Game.Clear(0, 0, 0);
            Game.Draw(player.currentT, player.x, player.y,1,1,player.angle,player.pivotX,player.pivotY);

            foreach(Bullet bullet in player.bulletsShot) {
                Game.Draw(bullet.currentT, bullet.x, bullet.y, 1, 1, 0, bullet.pivotX, bullet.pivotY);
            }
            foreach(Asteroid asteroid in asteroids) {
                Game.Draw(asteroid.currentT, asteroid.x, asteroid.y, asteroid.scale, asteroid.scale, asteroid.rotationAngle, asteroid.r, asteroid.r);
            }

            points.drawText();
            if (countDown > 0) countDownText.drawText();

            float xOffSet = 0;
            for(int i = 0; i < player.lives; i++) {
                Game.Draw(player.tIdle,xOffSet,0);
                xOffSet += player.height * 2;
            }
        }

        void restart() {
            pause = true;
            countDown = 3;
            player = new Player(400, 300, 3);
            asteroids = new List<Asteroid>();
            countDownText = new Text("3", 400, 200,0,0);
            points = new Text("00000000", 600, 30, 0, 0);
            gameLevel = 1;
            setUpAsteroids(gameLevel);
        }

        void loadNewLevel() {
            gameLevel++;
            player.restartPosition(400,300);
            setUpAsteroids(gameLevel);
            countDown = 3;
            pause = true;
        }

        void spawnAsteroids(int quant, int level, float x = 0, float y = 0, bool fix = false) {
            for (int i = 0; i < quant; i++) {
                if(!fix) {
                    x = random.Next(0, 800);
                    y = random.Next(0, 600);
                }
                float angle = random.Next(0, 360);
                spawnedAsteroids.Add(new Asteroid(x, y, angle, level));
            }
        }

        void setUpAsteroids(int gameLevel) {
            spawnedAsteroids = new List<Asteroid>();
            spawnAsteroids(gameLevel + 1, 1);
            asteroids.AddRange(spawnedAsteroids);
        }

        void drawCricleCollider(float x, float y, float r) {
            float drawX;
            float drawY;
            for (float i = 0; i <= 360; i++) {
                drawX = (float)Math.Cos((i) * Math.PI / 180) * r;
                drawY = (float)Math.Sin((i) * Math.PI / 180) * r;
                Game.Draw("pixel.png", x + drawX, y + drawY);
            }
        }
    }
}
