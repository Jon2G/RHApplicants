using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit;
using Kit.Model;
using Kit.Sql.Attributes;
using Kit.WPF;

namespace EvaluadorRH.Classes
{
    [Table("ADMINISTRADORES")]
    public class Admin : ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column("USUARIO")]
        public string User { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        
        public Admin(int Id, string User, string Name)
        {
            this.Id = Id;
            this.User = User;
            this.Name = Name;
        }
        [InitTable]
        public static void InitTable()
        {
            AppData.SQLiteConnection.InsertOrReplace(new Admin(1, "JANIS", "Lic. Janis Zafra")
            {
                Password = "1234"
            });
        }
    }
}
