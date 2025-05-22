using DevExpress.ChartRangeControlClient.Core;
using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using BindingSource = System.Windows.Forms.BindingSource;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        BindingSource tableFoodList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();
        public Account loginAccount;

        public fAdmin()
        {
            InitializeComponent();
            LoadData();

        }

        #region methos
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }

        void LoadData()
        {
            dtgvTableFood.DataSource = tableFoodList;
            dtgvCategory.DataSource = categoryList;
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadListCategory();
            LoadListTableFood();
            LoadAccount();
            LoadCategoryInfoComboBox(cbFoodCategory);
            AddFoodBinding();
            AddAccountBinding();
            AddCategoryBinding();
            AddTableFoodBinding();
        }

        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text",dtgvAccount.DataSource,"UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);

        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }
        
        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void AddCategoryBinding()
        {
            txbCategoryId.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void LoadListTableFood()
        {
            tableFoodList.DataSource = TableDAO.Instance.GetListTableFood();
        }

        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbFoodId.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Id", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void AddTableFoodBinding()
        {
            txbTableId.DataBindings.Add(new Binding("Text", dtgvTableFood.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTableFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbTableStatus.DataBindings.Add(new Binding("Text", dtgvTableFood.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryInfoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        void AddAccount(string userName, string displayName, int type)
        {
            if(AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công!");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại!");
            }
            LoadAccount();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công!");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại!");
            }
            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Không thể xóa tài khoản hiện tại");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công!");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại!");
            }
            LoadAccount();
        }

        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công!");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại!");
            }
        }


        #endregion


        #region events

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            ResetPass(userName);
        }
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)nmAccountType.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)nmAccountType.Value;

            EditAccount(userName, displayName, type);
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodId_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                try
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["IdCategory"].Value; ;

                    Category category = CategoryDAO.Instance.GetCategoryById(id);

                    cbFoodCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.Id == category.Id)
                        {
                            index = i; break;
                        }
                        i++;
                    }
                    cbFoodCategory.SelectedIndex = index;
                }
                catch
                {
                    
                }
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idCategory = (cbFoodCategory.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, idCategory, price))
            {
                MessageBox.Show("Thêm món thành công!");
                LoadListFood();
                if(insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn!");
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idCategory = (cbFoodCategory.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodId.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, idCategory, price))
            {
                MessageBox.Show("Sửa món thành công!");
                LoadListFood();
                if(updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn!");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodId.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công!");
                LoadListFood();
                if(deleteFood != null)
                    deleteFood(this, new EventArgs());
              
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn!");
            }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove{ insertFood -= value;}
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private void btnFirstBillPage_Click(object sender, EventArgs e)
        {
            txbPageBill.Text = "1";
        }

        private void btnLastBillPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);
            int lastPage = sumRecord / 10;
            if (sumRecord % 10 != 0)
            {
                lastPage++;
            }
            txbPageBill.Text = lastPage.ToString();

        }

        private void txbPageBill_TextChanged(object sender, EventArgs e)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(txbPageBill.Text));
        }

        private void btnPreviousBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            if (page > 1)
                page--;
            txbPageBill.Text = page.ToString();
        }

        private void btnNextBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);

            if (page < sumRecord)
                page++;
            txbPageBill.Text = page.ToString();
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công!");
                LoadListCategory();
                if (insertCategory != null)
                    insertCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục!");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryId.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa danh mục thành công!");
                LoadListCategory();
                if (deleteCategory != null)
                    deleteCategory(this, new EventArgs());

            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa danh mục!");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            int id = Convert.ToInt32(txbCategoryId.Text);

            if (CategoryDAO.Instance.UpdateCategory(name,id))
            {
                MessageBox.Show("Sửa danh mục thành công!");
                LoadListCategory();
                if (updateCategory != null)
                    updateCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa danh mục!");
            }
        }
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            string status = txbTableStatus.Text;

            if (TableDAO.Instance.InsertTableFood(name,status))
            {
                MessageBox.Show("Thêm bàn thành công!");
                LoadListTableFood();
                if (insertTableFood != null)
                    insertTableFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục!");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableId.Text);

            if (TableDAO.Instance.DeleteTableFood(id))
            {
                MessageBox.Show("Xóa bàn thành công!");
                LoadListTableFood();
                if (deleteTableFood != null)
                    deleteTableFood(this, new EventArgs());

            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa bàn!");
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            string status = txbTableStatus.Text;
            int id = Convert.ToInt32(txbTableId.Text);

            if (TableDAO.Instance.UpdateTableFood(name, status, id))
            {
                MessageBox.Show("Sửa bàn thành công!");
                LoadListTableFood();
                if (updateTableFood != null)
                    updateTableFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa bàn!");
            }
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTableFood();
        }

        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }

        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }

        private event EventHandler insertTableFood;
        public event EventHandler InsertTableFood
        {
            add { insertTableFood += value; }
            remove { insertTableFood -= value; }
        }

        private event EventHandler deleteTableFood;
        public event EventHandler DeleteTableFood
        {
            add { deleteTableFood += value; }
            remove { deleteTableFood -= value; }
        }

        private event EventHandler updateTableFood;
        public event EventHandler UpdateTableFood
        {
            add { updateTableFood += value; }
            remove { updateTableFood -= value; }
        }



        #endregion


    }
}