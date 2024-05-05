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
    public partial class UserControlClient : UserControl
    {
        DbConnector db;
        private string ID = "";

        public UserControlClient()
        {
            InitializeComponent();
            db = new DbConnector();
        }

        public void Clear()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxPhoneNo.Clear();
            textBoxAddress.Clear();
            tabControlClient.SelectedTab = tabPageAddClient;
        }

        private void Clear1()
        {
            textBoxFirstName1.Clear();
            textBoxLastName1.Clear();
            textBoxPhoneNo1.Clear();
            textBoxAddress1.Clear();
            ID = "";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            bool check;
            if(textBoxFirstName.Text.Trim() == string.Empty || textBoxLastName.Text.Trim() == string.Empty ||
                textBoxPhoneNo.Text.Trim() == string.Empty || textBoxAddress.Text.Trim() == string.Empty)
                MessageBox.Show("Please fill out all fields.", "Require all fields.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                check = db.AddClient(textBoxFirstName.Text.Trim(), textBoxLastName.Text.Trim(), textBoxPhoneNo.Text.Trim(), textBoxAddress.Text.Trim());
                if (check)
                    Clear();
            }
        }

        private void tabPageAddClient_Leave(object sender, EventArgs e)
        {
            Clear();
            Clear1();
        }

        private void tabPageSearchClient_Enter(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Client_Table", dataGridViewClient);
        }

        private void tabPageSearchClient_Leave(object sender, EventArgs e)
        {
            textBoxSearchPhoneNo.Clear();
        }

        private void textBoxSearchPhoneNo_TextChanged(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Client_Table WHERE Client_Phone LIKE '%" + textBoxSearchPhoneNo.Text + "%'", dataGridViewClient);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            bool check;
            if (ID != "")
            {
                if (textBoxFirstName1.Text.Trim() == string.Empty || textBoxLastName1.Text.Trim() == string.Empty ||
                textBoxPhoneNo1.Text.Trim() == string.Empty || textBoxAddress1.Text.Trim() == string.Empty)
                    MessageBox.Show("Please fill out all fields.", "Require all fields.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    check = db.UpdateClient(ID, textBoxFirstName1.Text.Trim(), textBoxLastName1.Text.Trim(), textBoxPhoneNo1.Text.Trim(), textBoxAddress1.Text.Trim());
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
                if (textBoxFirstName1.Text.Trim() == string.Empty || textBoxLastName1.Text.Trim() == string.Empty ||
                textBoxPhoneNo1.Text.Trim() == string.Empty || textBoxAddress1.Text.Trim() == string.Empty)
                    MessageBox.Show("Please fill out all fields.", "Require all fields.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    DialogResult result = MessageBox.Show("Are you want to delete this client?", "Client delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == result)
                    {
                        check = db.DeleteClient(ID);
                        if (check)
                            Clear1();
                    }
                }
            }
            else
                MessageBox.Show("Please first select row from table.", "Selection of row", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tabPageUpdateAndDeleteClient_Leave(object sender, EventArgs e)
        {
            Clear1();
        }

        private void dataGridViewClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewClient.Rows[e.RowIndex];
                ID = row.Cells[0].Value.ToString();
                textBoxFirstName1.Text = row.Cells[1].Value.ToString();
                textBoxLastName1.Text = row.Cells[2].Value.ToString();
                textBoxPhoneNo1.Text = row.Cells[3].Value.ToString();
                textBoxAddress1.Text = row.Cells[4].Value.ToString();
            }
        }
    }
}
