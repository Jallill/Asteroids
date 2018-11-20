using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    static class Save {
        static byte[] GetBytes(List<Int32> scores) {
            List<byte> bytes = new List<byte>();

            byte[] quantityScoresBytes = BitConverter.GetBytes((Int32)scores.Count());
            bytes.AddRange(quantityScoresBytes);

            byte[] scoreBytes;

            foreach(Int32 individualScore in scores) {
                scoreBytes = BitConverter.GetBytes(individualScore);
                bytes.AddRange(scoreBytes);
            }

            return bytes.ToArray();
        }

        public static void SaveBytesFile(List<Int32> scores) {
            byte[] bytes = GetBytes(scores);

            string filePath = Directory.GetCurrentDirectory() + "\\savefile.sav";

            using (FileStream stream = File.Open(filePath, FileMode.OpenOrCreate)) {
                foreach(byte b in bytes) {
                    stream.WriteByte(b);
                }
                stream.Close();
            }
            
        }

        static List<int> LoadBytesFile(byte[] bytes) {
            
            int header = 0;

            Int32 quantityScores = BitConverter.ToInt32(bytes, header);
            header += sizeof(Int32);

            List<Int32> scores = new List<Int32>();

            for (Int32 i = 0; i < quantityScores; i++) {
                scores.Add(BitConverter.ToInt32(bytes, header));
                header += sizeof(Int32);
            }

            return scores;

        }



        public static List<Int32> LoadSave() {
            string filePath = Directory.GetCurrentDirectory() + "\\savefile.sav";
            if (File.Exists(filePath)) {
                byte[] bytes = File.ReadAllBytes(filePath);
                List<Int32> scores = LoadBytesFile(bytes);
                return scores;
            }
            return null;
        }

    }

}
