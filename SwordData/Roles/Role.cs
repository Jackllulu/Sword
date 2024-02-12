using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordData
{
    public class Role : IRole
    {
        public string Name { get; }
        public long Id { get; }
        public string Description { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Defence { get; set; }
        public int Skill { get; set; }
        public int Critical { get; set; }
        public Camp RoleCamp { get; set; }
        public Rank RoleRank { get; set; }
        public Role(string name)
        {
            Name= name;
        }
        public Role(string name,long id)
        {
            Name = name;
            Id = id;
        }
    }
}
