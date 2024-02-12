using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwordData;
namespace Sword
{
    public class RoleManager
    {
        public List<IRole> Roles;
        public void Initialize()
        {
            Roles = new List<IRole>();
        }
    }
}
