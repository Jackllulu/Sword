using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordData
{
    public interface IRole
    {
        string Name { get; }
        long Id { get; }
        string Description { get; }
        int Strength {  get; }
        int Intelligence {  get; }
        int Defence {  get; }
        int Skill {  get; }
        int Critical { get; }  
        Camp RoleCamp { get; }

    }
}
