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
    public partial class UserControlRoom : UserControl
    {
        DbConnector db;
        private string No = "", Free = "";

        public UserControlRoom()
        {
            InitializeComponent();
            db = new DbConnector();
        }

        public void Clear()
        {
            comboBoxType.SelectedIndex = 0;
            textBoxPhoneNo.Clear();
            radioButtonYes.Checked = false;
            radioButtonNo.Checked = false;
            tabControlRoom.SelectedTab = tabPageAddRoom;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (radioButtonYes.Checked)
                Free = "Yes";
            if (radioButtonNo.Checked)
                Free = "No";
            bool check;
            if (comboBoxType.SelectedIndex == 0 || textBoxPhoneNo.Text.Trim() == string.Empty || Free == "")
                MessageBox.Show("Please fill out all fields.", "Require all fields.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                check = db.AddRoom(comboBoxType.SelectedItem.ToString(), textBoxPhoneNo.Text.Trim(), Free);
                if (check)
                    Clear();
            }
        }

        private void tabPageAddRoom_Leave(object sender, EventArgs e)
        {
            Clear();
            Clear1();
        }

        private void tabPageSearchRoom_Enter(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Room_Table", dataGridViewRoom);
        }

        private void tabPageSearchRoom_Leave(object sender, EventArgs e)
        {
            textBoxSearchRoomNo.Clear();
        }

        private void textBoxSearchRoomNo_TextChanged(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Room_Table WHERE Room_Number LIKE '%" + textBoxSearchRoomNo.Text + "%'", dataGridViewRoom);
        }

        private void dataGridViewRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewRoom.Rows[e.RowIndex];
                No = row.Cells[0].Value.ToString();
                comboBoxType1.SelectedItem = row.Cells[1].Value.ToString();
                textBoxPhoneNo1.Text = row.Cells[2].Value.ToString();
                Free = row.Cells[3].Value.ToString();
                if (Free == "Yes")
                    radioButtonYes1.Checked = true;
                if (Free == "No")
                    radioButtonNo1.Checked = true;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (radioButtonYes1.Checked)
                Free = "Yes";
            if (radioButtonNo1.Checked)
                Free = "No";
            bool check;
            if (No != "")
            {
                if (comboBoxType1.SelectedIndex == 0 || textBoxPhoneNo1.Text.Trim() == string.Empty || Free == "")
                    MessageBox.Show("Please fill out all fields.", "Require all fields.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    check = db.UpdateRoom(No, comboBoxType1.SelectedItem.ToString(), textBoxPhoneNo1.Text.Trim(), Free);
                    if (check)
                        Clear1();
                }
            }
            else
                MessageBox.Show("Please first select row from table.", "Selection of row", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (radioButtonYes1.Checked)
                Free = "Yes";
            if (radioButtonNo1.Checked)
                Free = "No";
            bool check;
            if (No != "")
            {
                if (comboBoxType1.SelectedIndex == 0 || textBoxPhoneNo1.Text.Trim() == string.Empty || Free == "")
                    MessageBox.Show("Please fill out all fields.", "Require all fields.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    DialogResult result = MessageBox.Show("Are you want to delete this room?", "Room delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == result)
                    {
                        check = db.DeleteRoom(No);
                        if (check)
                            Clear1();
                    }
                }
            }
            else
                MessageBox.Show("Please first select row from table.", "Selection of row", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UserControlRoom_Load(object sender, EventArgs e)
        {
            comboBoxType.SelectedIndex = 0;
            comboBoxType1.SelectedIndex = 0;
        }

        private void tabPageUpdateAndDeleteRoom_Leave(object sender, EventArgs e)
        {
            Clear1();
        }

        private void Clear1()
        {
            comboBoxType1.SelectedIndex = 0;
            textBoxPhoneNo1.Clear();
            radioButtonYes1.Checked = false;
            radioButtonNo1.Checked = false;
            No = "";
        }
    }
}
