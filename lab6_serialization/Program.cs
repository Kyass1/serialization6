using serialization6;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace serialization6
{
    class Program
    {
        static readonly string message1 = "Путь до файла";
        static readonly string message2 = "Если хотите продолжить - нажмите Пробел";
        static readonly string message3 = "Файл с данным именем не существует по данному пути";
        static readonly string message4 = "Недопустимый формат файла";

        static void Main()
        { 
            Console.Clear();
            Console.WriteLine(message1);

            string input1 = Console.ReadLine();
            string input2;
            string filePath1;
            string filePath2;
            string extension1;
            string extension2;
            List<Human> humans = new List<Human>();
            string text1;
            string text2;

            if(!File.Exists(input1) || input1 == null)
            {
                Console.Clear();
                Console.WriteLine(message3);
                Environment.Exit(1);
            }

            filePath1 = input1;

            text1 = File.ReadAllText(filePath1);

            extension1 = filePath1.Substring(filePath1.LastIndexOf('.')+1);
            Console.WriteLine("ext is {0}", extension1);


            humans = func1(text1, extension1)!;

            foreach(var i in humans)
            {
                Console.WriteLine(i.Name);
                Console.WriteLine(i.Weigth);
                Console.WriteLine(i.Height);
            }

            Console.WriteLine();
            Console.WriteLine(message2);
            ConsoleKeyInfo key = Console.ReadKey();

            while(key.Key == ConsoleKey.Spacebar) {
                Console.Clear();
                Console.WriteLine(message1);

                input2 = Console.ReadLine();

                if (input2.IndexOf('.') == -1)
                {
                    Console.Clear();
                    Console.WriteLine(message4);
                    Environment.Exit(1);
                }

                filePath2 = input2;

                func2(humans, filePath2);

                Console.Clear();
                Console.WriteLine(message2);
                key = Console.ReadKey();
            }   
        }

        public static List<Human> func1(string text, string extension)
        {
            if (extension == "json")
            {
                return JsonConvert.DeserializeObject<List<Human>>(text);
            }
            else if (extension == "txt")
            {
                List<Human> humans = new List<Human>();

                text = text.TrimEnd('\n');
                string[] array = text.Split('\n');
                
                Human human;

                for (int i = 0; i < array.Length; i = i + 3)
                {
                    human = new Human(array[i], Convert.ToInt32(array[i + 1]),
                        Convert.ToInt32(array[i + 2]));
                    
                    humans.Add(human);
                }

                return humans;
            }
            else if (extension == "xml")
            {
                List<Human> list = new List<Human>();

                XmlSerializer xml = new XmlSerializer(typeof(List<Human>));

                using (FileStream fs = new FileStream(text, FileMode.Open))
                {
                    list = (List<Human>)xml.Deserialize(fs);
                }

                return list;
            }
            else
            {
                Console.Clear();
                Console.WriteLine(message4);
                Environment.Exit(1);

                List<Human> hl = new List<Human>();
                return hl;
            }
        }
        public static void func2(List<Human> humans, string filePath)
        {
            var extension = filePath.Substring(filePath.LastIndexOf('.') + 1);

            if (extension == "json")
            {
                string json = JsonConvert.SerializeObject(humans);

                File.WriteAllText(filePath, json);
            } else if (extension == "txt")
            {
                string text = "";

                foreach (var human in humans)
                {
                    text += human.Name;
                    text += '\n';
                    text += human.Weigth;
                    text += '\n';
                    text += human.Height;
                    text += '\n';
                }
                text = text.TrimEnd('\r', '\n');

                File.WriteAllText(filePath, text);
            } else if (extension == "xml")
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Human>));

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, humans);
                }
            } else
            {
                Console.Clear();
                Console.WriteLine(message4);
                Environment.Exit(1);
            }
        }

    }
}