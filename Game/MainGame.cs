using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class MainGame {

        Player player;
        List<Asteroid> asteroids;
        int gameLevel;
        List<Asteroid> spawnedAsteroids;
        Random random = new Random((int)DateTime.Now.Ticks);


        public MainGame() {
            restart();
        }

        public void input(float deltaTime) {
            player.inputKey(deltaTime);
        }

        public void update(float deltaTime) {
            player.update(deltaTime);

            foreach (Bullet bullet in player.bulletsShot) {
                bullet.update(deltaTime);
            }

            spawnedAsteroids = new List<Asteroid>();

            foreach(Asteroid asteroid in asteroids) {
                asteroid.update(deltaTime);

                if (player.bulletsShot.Count > 0) {
                    asteroid.checkCollision(player.bulletsShot);
                }

                if (asteroid.destroyAsteroid == true) {
                    switch (asteroid.level) {
                        case (1):
                            spawnAsteroids(2, 2, asteroid.x, asteroid.y);
                            break;
                        case (2):
                            spawnAsteroids(2, 3, asteroid.x, asteroid.y);
                            break;
                    }
                }
            }

            if (spawnedAsteroids.Count > 0) asteroids.AddRange(spawnedAsteroids);

            asteroids.RemoveAll(asteroid => asteroid.destroyAsteroid == true);
            

        }

        public void render() {
            Game.Clear(0, 0, 0);
            Game.Draw(player.currentT, player.x, player.y,1,1,player.angle,player.pivotX,player.pivotY);
            foreach(Bullet bullet in player.bulletsShot) {
                Game.Draw(bullet.currentT, bullet.x, bullet.y, 1, 1, 0, bullet.pivotX, bullet.pivotY);
            }
            foreach(Asteroid asteroid in asteroids) {
                Game.Draw(asteroid.currentT, asteroid.x, asteroid.y, asteroid.scale, asteroid.scale, asteroid.rotationAngle, asteroid.pivotX, asteroid.pivotY);
            }
        }

        void restart() {
            player = new Player(400, 300);
            asteroids = new List<Asteroid>();
            gameLevel = 1;
            setUpAsteroids(gameLevel);
        }

        void spawnAsteroids(int quant, int level, float x = 0, float y = 0) {
            for (int i = 0; i < quant; i++) {
                if(x == 0 && y == 0) {
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
    }
}
