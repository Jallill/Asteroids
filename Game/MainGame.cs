using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class MainGame {

        Player player;
        List<Asteroid> asteroids;
        int level;

        Random random = new Random((int)DateTime.Now.Ticks);


        public MainGame() {
            restart();
        }

        public void input(float deltaTime) {
            player.inputKey(deltaTime);
        }

        public void update(float deltaTime) {
            player.update(deltaTime);
            foreach(Bullet bullet in player.bulletsShot) {
                bullet.update(deltaTime);
            }
            foreach(Asteroid asteroid in asteroids) {
                asteroid.update(deltaTime);
                if (player.bulletsShot.Count > 0) {
                    asteroid.checkCollision(player.bulletsShot);
                }
            }
            asteroids.RemoveAll(bullet => bullet.destroyAsteroid == true);
            

        }

        public void render() {
            Game.Clear(0, 0, 0);
            Game.Draw(player.currentT, player.x, player.y,1,1,player.angle,player.pivotX,player.pivotY);
            foreach(Bullet bullet in player.bulletsShot) {
                Game.Draw(bullet.currentT, bullet.x, bullet.y, 1, 1, 0, bullet.pivotX, bullet.pivotY);
            }
            foreach(Asteroid asteroid in asteroids) {
                Game.Draw(asteroid.currentT, asteroid.x, asteroid.y, 1, 1, asteroid.angle, asteroid.pivotX, asteroid.pivotY);
            }
        }

        void restart() {
            player = new Player(400, 300);
            asteroids = new List<Asteroid>();
            level = 1;
            spawnAsteroids(level);
        }

        void spawnAsteroids(int level) {
            for (int i = 0; i < level; i++) {
                float x = random.Next(0, 800);
                float y = random.Next(0, 600);
                float angle = random.Next(0, 360);
                asteroids.Add(new Asteroid(x,y,angle,64));
                
            }
        }
    }
}
