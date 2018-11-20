using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class Arrow {

        public float x { get; private set; }
        public float y { get; private set; }
        
        public float pivotX { get; private set; }
        public float pivotY { get; private set; }

        private float xOffSet;

        public Arrow(float xOffSet) {
            this.xOffSet = xOffSet;
            pivotX = 50 / 2;
            pivotY = 32 / 2;
        }

        public void update(float x, float y) {
            this.x = x - xOffSet;
            this.y = y;

        }

    }
}
