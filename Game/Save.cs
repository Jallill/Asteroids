using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    static class Save {
        static byte[] GetBytes(Player player, Enemy enemy) {
            List<byte> bytes = new List<byte>();

            //Esto convierte el float vida en su versión en bytes y lo
            //guardo en la lista de bytes final
            byte[] lifeBytes = BitConverter.GetBytes(player.life);
            bytes.AddRange(lifeBytes);

            byte[] yPlayerBytes = BitConverter.GetBytes(player.y);
            bytes.AddRange(yPlayerBytes);

            byte[] xPlayerBytes = BitConverter.GetBytes(player.x);
            bytes.AddRange(xPlayerBytes);

            byte[] yEnemyBytes = BitConverter.GetBytes(enemy.y);
            bytes.AddRange(yEnemyBytes);

            byte[] xEnemyBytes = BitConverter.GetBytes(enemy.x);
            bytes.AddRange(xEnemyBytes);
            //Esto devuelve una copia de la lista en formato
            //array
            return bytes.ToArray();
        }

        public static void SaveBytesFile(Player jugador, Enemy enemigo) {
            byte[] bytes = GetBytes(jugador,enemigo);

            string filePath = Directory.GetCurrentDirectory() + "\\savefile.sav";

            using (FileStream stream = File.Open(filePath, FileMode.OpenOrCreate)) {
                foreach(byte b in bytes) {
                    stream.WriteByte(b);
                }
                stream.Close();
            }
            
        }

        public static void CheckInputQ(Player player, Enemy enemy) {
            if (Game.GetKey(Keys.Q)) SaveBytesFile(player, enemy);
        }
        
        static SaveData LoadBytesFile(byte[] bytes) {
            //Arranco el indicador del byte actual en 0
            int header = 0;

            Player player = new Player();
            Enemy enemy = new Enemy();
            //Leo un float en la posicion del indicador
            //Y avanzo el cabezal la cantidad de bytes que tien un float

            player.life = BitConverter.ToSingle(bytes, header);
            header += sizeof(float);

            player.y = BitConverter.ToSingle(bytes, header);
            header += sizeof(float);

            player.x = BitConverter.ToSingle(bytes, header);
            header += sizeof(float);

            enemy.y = BitConverter.ToSingle(bytes, header);
            header += sizeof(float);

            enemy.x = BitConverter.ToSingle(bytes, header);

            SaveData saveData = new SaveData(player, enemy);

            return saveData;

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

    }

    class SaveData {

        public SaveData(Player player, Enemy enemy) {
            this.player = player;
            this.enemy = enemy;
        }

        public Player player;
        public Enemy enemy;
    }

}
