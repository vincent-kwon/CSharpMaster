using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NETConsoleApp
{
    [Serializable]
    class NameCard
    {
        public string Name;
        public string Phone;
        public int Age;
    }

    class Files
    {
        public void TestFile()
        {
            // create file & directory
            string path = "./FileTest";
            string type = "Directory";

            if (File.Exists(path) || Directory.Exists(path))
            {
                if (File.Exists(path))
                {
                    File.SetLastWriteTime(path, DateTime.Now);
                }
                if (Directory.Exists(path))
                {
                    Directory.SetLastWriteTime(path, DateTime.Now);
                }
            }
            else
            {
                if (type == "File")
                {
                    File.Create(path).Close();
                }
                else if (type == "Directory")
                {
                    Directory.CreateDirectory(path); //@20180113-vincent: Create Vs. CreateDirectory are different
                }
            }

            // binary reader & writer
            BinaryWriter bw = new BinaryWriter(new FileStream("a.dat", FileMode.Create));

            bw.Write(int.MaxValue);
            bw.Write("good morning");
            bw.Write(uint.MaxValue);
            bw.Write("안녕하세요");
            bw.Close();

            BinaryReader br = new BinaryReader(new FileStream("a.dat", FileMode.Open));

            Console.WriteLine("{0}", br.ReadInt32());
            Console.WriteLine("{0}", br.ReadString());
            Console.WriteLine("{0}", br.ReadUInt32());
            Console.WriteLine("{0}", br.ReadInt32());
            Console.WriteLine("{0}", br.ReadDouble());

            br.Close();

            // string stream
            StreamWriter sw = new StreamWriter(new FileStream("a.txt", FileMode.Create));

            sw.WriteLine(int.MaxValue);
            sw.WriteLine("Good morning");
            sw.WriteLine(uint.MaxValue);
            sw.WriteLine("안녕하세요");
            sw.WriteLine(double.MaxValue);

            sw.Close();

            StreamReader sr = new StreamReader(new FileStream("a.txt", FileMode.Open));

            while (sr.EndOfStream == false)
            {
                Console.WriteLine(sr.ReadLine());
            }

            // Serailize, deserialize
            Stream ws = new FileStream("a1.dat", FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();
            NameCard nc = new NameCard();
            nc.Name = "Park";
            nc.Phone = "010-2342-d343";
            nc.Age = 40;

            serializer.Serialize(ws, nc); //@20180113-vincent: money ball
            ws.Close();

            Stream rs = new FileStream("a1.dat", FileMode.Open); //@20180113-vincent: one object one file
            BinaryFormatter deserializer = new BinaryFormatter();
            NameCard nc2;

            nc2 = (NameCard)deserializer.Deserialize(rs);
            Console.WriteLine("Name: {0}", nc2.Name);
            Console.WriteLine("Phone: {0}", nc2.Phone);
            Console.WriteLine("Age: {0}", nc2.Age);
        }
    }
}
