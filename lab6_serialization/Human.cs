using System;
namespace serialization6
{
        public class Human
        {
            private string name;
            private int weigth;
            private int height;

            public Human()
            {

            }

            public Human(string name, int weigth, int height)
            {
                this.name = name;
                this.weigth = weigth;
                this.height = height;
            }

            public string Name   
            {
                get { return name; }   
                set { name = value; }  
            }

            public int Weigth   
            {
                get { return weigth; }   
                set { weigth = value; }  
            }

            public int Height   
            {
                get { return height; }   
                set { height = value; }  
            }
        }
    
}

