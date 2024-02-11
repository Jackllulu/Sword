using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sword.DataBase
{
    [Table("User")]
    public class Account
    {
        [Required][Key] public long ID { get; set; }
        [Required]public string UserName { get; set; }
        public string Password { get; set; }
        [Required] public DateTime CreateTime { get; set; }
        [Required]public bool Online { get; set; }   

    }
}
