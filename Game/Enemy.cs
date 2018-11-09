using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class Enemy 
    {

        public float width = 173;
        public float height = 145.5f;

        public bool iSentido;

        public float x { get; set; }
        public float y { get; set; }
        float speed = 500;
        
        public Enemy(float x = 400, float y = 400) {
            this.x = x;
            this.y = y;
        }

        public void update(float deltaTime) 
        {
            if (x + width > 800) {
                iSentido = false;
            } else if(x < 0) {
                iSentido = true;
            }
            if (iSentido) {
                x += speed * deltaTime;
            } else {
                x -= speed * deltaTime;
            }


        }
        
        public void collision(Player player) {

            float xMaxPlayer = player.x + player.width;
            float xMax = x + width;
            float yMaxPlyaer = player.y + player.height;
            float yMax = y + height;

            bool colMinXPly = player.x >= x && player.x <= xMax;
            bool colMaxXPly = xMaxPlayer >= x && xMaxPlayer <= xMax;
            bool colMinXEne = x >= player.x && x <= xMaxPlayer;
            bool colMaxXEne = xMax >= player.x && xMax <= xMaxPlayer;
            bool colX = colMinXPly || colMaxXPly || colMinXEne || colMaxXEne;

            bool colMinYPly = player.y >= y && player.y <= yMax;
            bool colMaxYPly = yMaxPlyaer >= y && yMaxPlyaer <= yMax;
            bool colMinYEne = y >= player.y && y <= yMaxPlyaer;
            bool colMaxYEne = yMax >= player.y && yMax <= yMaxPlyaer;
            bool colY = colMinYPly || colMaxYPly || colMinYEne || colMaxYEne;

            bool collision = colX && colY;

            if (collision) {
                player.life -= 1;
            }
        }

    }
}

