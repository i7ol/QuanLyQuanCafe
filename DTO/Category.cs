﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyQuanCafe.DTO
{
    public class Category
    {
        public Category(int id, string name) 
        {
            this.Id = id;
            this.Name = name;
        }

        public Category(DataRow row) 
        {
            this.Id = (int)row["id"];
            this.Name = row["name"].ToString();
        }

        private int id;
        private string name;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
