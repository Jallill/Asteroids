using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    static class Save {
        static byte[] GetBytes(Player player, Int32 score, Int32 gameLevel) {
            List<byte> bytes = new List<byte>();

            byte[] scoreBytes = BitConverter.GetBytes(score);
            bytes.AddRange(scoreBytes);

            byte[] gameLevelBytes = BitConverter.GetBytes(gameLevel);
            bytes.AddRange(gameLevelBytes);

            byte[] xBytes = BitConverter.GetBytes(player.x);
            bytes.AddRange(xBytes);

            byte[] yBytes = BitConverter.GetBytes(player.y);
            bytes.AddRange(yBytes);

            byte[] livesBytes = BitConverter.GetBytes(player.lives);
            bytes.AddRange(livesBytes);

            return bytes.ToArray();
        }

        public static void SaveBytesFile(Player player, Int32 score, Int32 gameLevel) {
            byte[] bytes = GetBytes(player, score, gameLevel);

            string filePath = Directory.GetCurrentDirectory() + "\\savefile.sav";

            using (FileStream stream = File.Open(filePath, FileMode.OpenOrCreate)) {
                foreach(byte b in bytes) {
                    stream.WriteByte(b);
                }
                stream.Close();
            }
            
        }

        static SaveData LoadBytesFile(byte[] bytes) {
            
            int header = 0;
            

            Int32 score = BitConverter.ToInt32(bytes, header);
            header += sizeof(Int32);

            Int32 gameLevel = BitConverter.ToInt32(bytes, header);
            header += sizeof(Int32);

            float x = BitConverter.ToSingle(bytes, header);
            header += sizeof(float);

            float y = BitConverter.ToSingle(bytes, header);
            header += sizeof(float);

            int lives = BitConverter.ToInt32(bytes, header);
            header += sizeof(Int32);

            Player player = new Player(x, y, lives);

            return new SaveData(player, score, gameLevel);

        }

        public static SaveData LoadSave() {
            string filePath = Directory.GetCurrentDirectory() + "\\savefile.sav";
            if (File.Exists(filePath)) {
                byte[] bytes = File.ReadAllBytes(filePath);
                SaveData saveData = LoadBytesFile(bytes);
                return saveData;
            }
            return null;
        }

        public class SaveData {
            public Player player { get; private set; }
            public int score { get; private set; }
            public int gameLevel { get; private set; }

            public SaveData(Player player, int score, int gameLevel) {
                this.player = player;
                this.score = score;
                this.gameLevel = gameLevel;
            }

        }

    }

}
