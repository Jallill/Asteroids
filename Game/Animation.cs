using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class Animation 
    {
        public List<Texture> frames = new List<Texture>();
        public bool loop = true;
        public float speed = 0.005f;
    }
}
