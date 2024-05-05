using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AMRConnector;

namespace Hotel_Management_System.User_Control
{
    public partial class UserControlSetting : UserControl
    {
        DbConnector db;
        private string ID = "";

        public UserControlSetting()
        {
            InitializeComponent();
            db = new DbConnector();
        }

        public void Clear()
        {
            textBoxUsername.Clear();
            textBoxPassword.Clear();
            tabControlUser.SelectedTab = tabPageAddUser;
        }


        private void Clear1()
        {
            textBoxUsername1.Clear();
            textBoxPassword1.Clear();
            ID = "";
        }

        private void tabPageAddUser_Leave(object sender, EventArgs e)
        {
            Clear();
            Clear1();
        }

        private void tabPageSearchUser_Enter(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM User_Table", dataGridViewUser);
        }

        private void tabPageSearchUser_Leave(object sender, EventArgs e)
        {
            textBoxSearchUsername.Clear();
        }

        private void tabPageUpdateAndDeleteUser_Leave(object sender, EventArgs e)
        {
            Clear1();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            bool check;
            if (textBoxUsername.Text.Trim() == string.Empty || textBoxPassword.Text.Trim() == string.Empty)
                MessageBox.Show("Please fill out all fields.", "Require all fields.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                check = db.AddUser(textBoxUsername.Text.Trim(), textBoxPassword.Text.Trim());
                if (check)
                    Clear();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            bool check;
            if(ID != "")
            {
                if (textBoxUsername1.Text.Trim() == string.Empty || textBoxPassword1.Text.Trim() == string.Empty)
                    MessageBox.Show("Please fill out all fields.", "Require all fields.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    check = db.UpdateUser(ID, textBoxUsername1.Text.Trim(), textBoxPassword1.Text.Trim());
                    if (check)
                        Clear1();
                }
            }
            else
                MessageBox.Show("Please first select row from table.", "Selection of row", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            bool check;
            if (ID != "")
            {
                if (textBoxUsername1.Text.Trim() == string.Empty || textBoxPassword1.Text.Trim() == string.Empty)
                    MessageBox.Show("Please fill out all fields.", "Require all fields.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    DialogResult result = MessageBox.Show("Are you want to delete this user?", "User delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(DialogResult.Yes == result)
                    {
                        check = db.DeleteUser(ID);
                        if (check)
                            Clear1();
                    }
                }
            }
            else
                MessageBox.Show("Please first select row from table.", "Selection of row", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridViewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewUser.Rows[e.RowIndex];
                ID = row.Cells[0].Value.ToString();
                textBoxUsername1.Text = row.Cells[1].Value.ToString();
                textBoxPassword1.Text = row.Cells[2].Value.ToString();
            }
        }

        private void textBoxSearchUsername_TextChanged(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM User_Table WHERE User_Name LIKE '%" + textBoxSearchUsername.Text + "%'", dataGridViewUser);
        }
    }
}
