using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sword.DataBase
{
    public class Asset
    {
        [Required]
        [Key]
        public long ID { get; set; }
        [Required]
        public int Coins {  get; set; }
        [Required]
        public int Dimends {  get; set; }
        [Required]
        public int Level {  get; set; }

    }
}
