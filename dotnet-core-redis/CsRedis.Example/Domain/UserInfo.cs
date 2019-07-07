using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsRedis.Example.Domain
{
    public class UserInfo
    {
        public UserInfo(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
