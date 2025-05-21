using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyQuanCafe.DTO
{
    public class Table
    {
        public Table(int id, string name, string status) 
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }

        public Table(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
        }

        private int id;

        private string name;

        private string status;

        public int Id { get { return id; } set  { id = value; }}

        public string Name { get { return name; } set { name = value; } }
        public string Status { get { return status; } set { status = value; } }
    }
}
