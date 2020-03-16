using System;
using System.Linq;

namespace Lab02.Domain
{
    public class User : Party
    {
        public int Age { get; }

        public User(int age)
        {
            this.Age = age;
        }
    }
}
