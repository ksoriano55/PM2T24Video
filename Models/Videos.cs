
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace PM2T24Video.Models
{
    public class Videos
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string nombre { get; set; }
        public string fecha { get; set; }
    }
}
