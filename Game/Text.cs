using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    class Text {
        public char[] charText { get; private set; }
        public List<Texture> charTextures { get; private set; }
        public float x { get; private set; }
        public float y { get; private set; }
        public float height { get; private set; }
        public float width { get; private set; }
        
        private float fontHeight = 54;
        private float fontWidth = 40;
        private float scale;

        public Text(string text, float x, float y, float height = 0, float width = 0) {
            charText = text.ToArray();
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;

            float aux = (float)charText.Count() / 5;
            if (aux < 1) aux = 1; 
            scale = 1 / aux;

            loadTextures();
        }

        void loadTextures() {
            charTextures = new List<Texture>();
            foreach(char c in charText) {
                if (!c.Equals(' ')) {
                    charTextures.Add(Game.GetTexture("Texturas/Font/" + c + ".png"));
                } else {
                    charTextures.Add(Game.GetTexture("Texturas/Font/space.png"));
                }
            }
        }

        public void changeText(string newText) {
            charText = newText.ToArray();
            loadTextures();
        }

        public void drawText() {
            float xOffSet = 0;
            foreach(Texture texture in charTextures) {
                Game.Draw(texture, x + xOffSet, y, scale, scale, 0, fontWidth/2, fontHeight/2);
                xOffSet += fontWidth - fontWidth  * (1 - scale);
            }
        }

    }
}
