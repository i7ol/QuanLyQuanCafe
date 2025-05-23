﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;
        private string connectionStr = "Data Source=DESKTOP-8AH97PP\\WUANKK;Initial Catalog=QuanLyQuanCafe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public static DataProvider Instance 
        {
            get {   if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }
        private DataProvider() { }

        public DataTable ExecuteQuery(string query, object[] parameter = null )
       {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item,parameter[i]);
                            i++;
                        }
                    }
                }
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(data);

                conn.Close();
            }
            
            return data;
       }
        public int ExecuteNonQuery(string query, object[] parameter = null)
       {
            int data = 0;

            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = cmd.ExecuteNonQuery();

                conn.Close();
            }

            return data;
       }
        public object ExecuteScalar(string query, object[] parameter = null)
       {
            object data = 0;

            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = cmd.ExecuteScalar();

                conn.Close();
            }

            return data;
       }
    }
}
