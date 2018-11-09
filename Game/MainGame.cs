﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class MainGame {

        Player player;
        Random random = new Random();

        public MainGame() {
            restart();
        }

        public void input(float deltaTime) {
            player.inputKey(deltaTime);
        }

        public void update(float currentTime, float deltaTime) {
            player.update(currentTime);
            player.checkDeath();

        }

        public void render() {
            Game.Clear(0, 0, 0);
            Game.Draw(player.currentT, player.x, player.y,1,1,player.angle,player.width/2,player.height/2);
        }

        void restart() {
            player = new Player(400, 300);
        }
    }
}
