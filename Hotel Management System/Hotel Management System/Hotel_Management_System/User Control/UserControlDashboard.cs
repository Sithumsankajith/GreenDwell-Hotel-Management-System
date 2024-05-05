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
    public partial class UserControlDashboard : UserControl
    {
        DbConnector db;

        public UserControlDashboard()
        {
            InitializeComponent();
            db = new DbConnector();
        }

        public void User()
        {
            labelUserCount.Text = db.Count("SELECT COUNT(*) FROM User_Table").ToString();
        }

        public void Client()
        {
            labelClientCount.Text = db.Count("SELECT COUNT(*) FROM Client_Table").ToString();
        }

        public void Room()
        {
            labelRoomCount.Text = db.Count("SELECT COUNT(*) FROM Room_Table").ToString();
        }

        private void UserControlDashboard_Load(object sender, EventArgs e)
        {
            User();
            Client();
            Room();
        }
    }
}
