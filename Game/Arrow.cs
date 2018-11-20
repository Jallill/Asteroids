using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class Arrow {

        public float x;
        public float y;
        public float offSet = 100;

        public void update(float x, float y) {
            this.x = x - offSet;
            this.y = y;
        }

    }
}
