using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanCafe.DTO;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        { 
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }

        }

        public static int TableWidth = 90;
        public static int TableHeight = 90;
        private TableDAO() { }

        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("EXEC USP_SwitchTable @idTable1 , @idTable2", new object[] {id1, id2});
        }
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC USP_GetTableList");

            foreach (DataRow item in data.Rows) 
            {
                Table table = new Table(item);
                tableList.Add(table);
                
            }
            return tableList;
        }
        public List<Table> GetListTableFood()
        {
            List<Table> list = new List<Table>();

            string query = "SELECT * FROM dbo.TableFood";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                list.Add(table);
            }
            return list;
        }
        public bool InsertTableFood(string name,string status)
        {
            string query = string.Format("INSERT dbo.TableFood (name,status) VALUES (N'{0}',N'{1}')", name, status);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateTableFood(string name,string status, int id)
        {
            string query = string.Format("UPDATE dbo.TableFood SET name = N'{0}', status = N'{1}' WHERE id = {2}", name, status, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteTableFood(int id)
        {
            string query = string.Format("DELETE dbo.TableFood WHERE id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
