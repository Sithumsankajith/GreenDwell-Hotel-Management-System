using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMRConnector
{
    public class DbConnector
    {
        private bool check = false;

        private SqlConnection GetConnection()
        {
            string sql = @"Data Source = .\SQLEXPRESS;
                           Initial Catalog = Hotel_Management_System;
                           Integrated Security = true";
         
            SqlConnection conn = new SqlConnection(sql);
            try
            {
                conn.Open();    // Connection Open
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error! \n" + ex.ToString(), "SQL connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;
        }

        public bool IsValidNamePass(string Username, string Password)
        {
            try
            {
                string sql = "SELECT User_Name, User_Password FROM User_Table WHERE User_Name = '" + Username + "' AND User_Password = '" + Password + "'";
                //  Get Connection
                SqlConnection conn = GetConnection();
                //  Passing Query & Connection to SqlCommand object
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sdp = new SqlDataAdapter(cmd);
                //  This is creating a Virtual Table  
                DataTable dt = new DataTable();
                sdp.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                    check = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error! \n" + ex.ToString(), "Username and Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return check;
        }

        public bool AddUser(string Username, string Password)
        {
            string sql = "INSERT INTO User_Table VALUES (@User_Name, @User_Password)";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@User_Name", SqlDbType.VarChar).Value = Username;
            cmd.Parameters.Add("@User_Password", SqlDbType.VarChar).Value = Password;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully!", "User Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Username already exist.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error! \n" + ex.ToString(), "Add User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public bool UpdateUser(string ID, string Username, string Password)
        {
            string sql = "UPDATE User_Table SET User_Name = @UserName, User_Password = @UserPassword WHERE User_ID = @UserID";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = ID;
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = Username;
            cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = Password;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully!", "User Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Username already exist.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error! \n" + ex.ToString(), "Update User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public bool DeleteUser(string ID)
        {
            string sql = "DELETE FROM User_Table WHERE User_ID = @UserID";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = ID;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully!", "User Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error! \n" + ex.ToString(), "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public void DisplayAndSearch(string query, DataGridView dgv)
        {
            string sql = query;
            //  Get Connection
            SqlConnection conn = GetConnection();
            //  Passing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgv.DataSource = table;
        }

        public bool AddClient(string FirstName, string LastName, string Phone, string Address)
        {
            string sql = "INSERT INTO Client_Table VALUES (@Client_FirstName, @Client_LastName, @Client_Phone, @Client_Address)";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Client_FirstName", SqlDbType.VarChar).Value = FirstName;
            cmd.Parameters.Add("@Client_LastName", SqlDbType.VarChar).Value = LastName;
            cmd.Parameters.Add("@Client_Phone", SqlDbType.VarChar).Value = Phone;
            cmd.Parameters.Add("@Client_Address", SqlDbType.VarChar).Value = Address;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully!", "Client Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Phone No. already exist.", "Phone No.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error! \n" + ex.ToString(), "Add Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public bool UpdateClient(string ID, string FirstName, string LastName, string Phone, string Address)
        {
            string sql = "UPDATE Client_Table SET Client_FirstName = @ClientFirstName, Client_LastName = @ClientLastName, Client_Phone = @ClientPhone, Client_Address = @ClientAddress WHERE Client_ID = @ClientID";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ID;
            cmd.Parameters.Add("@ClientFirstName", SqlDbType.VarChar).Value = FirstName;
            cmd.Parameters.Add("@ClientLastName", SqlDbType.VarChar).Value = LastName;
            cmd.Parameters.Add("@ClientPhone", SqlDbType.VarChar).Value = Phone;
            cmd.Parameters.Add("@ClientAddress", SqlDbType.VarChar).Value = Address;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully!", "Client Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Phone No. already exist.", "Phone No.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error! \n" + ex.ToString(), "Update Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public bool DeleteClient(string ID)
        {
            string sql = "DELETE FROM Client_Table WHERE Client_ID = @ClientID";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ID;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully!", "Client Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error! \n" + ex.ToString(), "Delete Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public bool AddRoom(string Type, string Phone, string Free)
        {
            string sql = "INSERT INTO Room_Table VALUES (@Type, @Phone, @Free)";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Phone;
            cmd.Parameters.Add("@Free", SqlDbType.VarChar).Value = Free;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully!", "Room Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Phone No. already exist.", "Phone No.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error! \n" + ex.ToString(), "Add Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public bool UpdateRoom(string No, string Type, string Phone, string Free)
        {
            string sql = "UPDATE Room_Table SET Room_Type = @RoomType, Room_Phone = @RoomPhone, Room_Free = @RoomFree WHERE Room_Number = @RoomNumber";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@RoomNumber", SqlDbType.Int).Value = No;
            cmd.Parameters.Add("@RoomType", SqlDbType.VarChar).Value = Type;
            cmd.Parameters.Add("@RoomPhone", SqlDbType.VarChar).Value = Phone;
            cmd.Parameters.Add("@RoomFree", SqlDbType.VarChar).Value = Free;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully!", "Room Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Phone No. already exist.", "Phone No.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error! \n" + ex.ToString(), "Update Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public bool DeleteRoom(string No)
        {
            string sql = "DELETE FROM Room_Table WHERE Room_Number = @RoomNumber";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@RoomNumber", SqlDbType.Int).Value = No;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully!", "Room Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error! \n" + ex.ToString(), "Delete Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }


        public void RoomTypeAndNo(string query, ComboBox cb)
        {
            string sql = query;
            //  Get Connection
            SqlConnection conn = GetConnection();
            //  Passing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            cb.Items.Clear();
            cb.Items.Add("Please select ...");
            cb.SelectedIndex = 0;
            while (dr.Read())
                cb.Items.Add(dr[0]);
        }

        public void UpdateReservationRoom(string No, string Free)
        {
            string sql = "UPDATE Room_Table SET Room_Free = @RoomFree WHERE Room_Number = @RoomNumber";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@RoomNumber", SqlDbType.Int).Value = No;
            cmd.Parameters.Add("@RoomFree", SqlDbType.VarChar).Value = Free;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error! \n" + ex.ToString(), "Update Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();   // Connection Close
        }

        public bool AddReservation(string Type, string No, string CID, string In, string Out)
        {
            string sql = "INSERT INTO Reservation_Table VALUES (@Type, @No, @CID, @In, @Out)";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
            cmd.Parameters.Add("@No", SqlDbType.Int).Value = No;
            cmd.Parameters.Add("@CID", SqlDbType.Int).Value = CID;
            cmd.Parameters.Add("@In", SqlDbType.VarChar).Value = In;
            cmd.Parameters.Add("@Out", SqlDbType.VarChar).Value = Out;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully!", "Reservation Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Client ID already exist.", "Client ID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error! \n" + ex.ToString(), "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public bool UpdateReservation(string RID, string Type, string No, string CID, string In, string Out)
        {
            string sql = "UPDATE Reservation_Table SET Reservation_Room_Type = @ReservationRoomType, Reservation_Room_Number = @ReservationRoomNumber, Reservation_Client_ID = @ReservationClientID, Reservation_In = @ReservationIn, Reservation_Out = @ReservationOut WHERE Reservation_ID = @ReservationID";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ReservationID", SqlDbType.Int).Value = RID;
            cmd.Parameters.Add("@ReservationRoomType", SqlDbType.VarChar).Value = Type;
            cmd.Parameters.Add("@ReservationRoomNumber", SqlDbType.Int).Value = No;
            cmd.Parameters.Add("@ReservationClientID", SqlDbType.Int).Value = CID;
            cmd.Parameters.Add("@ReservationIn", SqlDbType.VarChar).Value = In;
            cmd.Parameters.Add("@ReservationOut", SqlDbType.VarChar).Value = Out;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully!", "Reservation Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Client ID already exist.", "Client ID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error! \n" + ex.ToString(), "Update Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public bool DeleteReservation(string ID)
        {
            string sql = "DELETE FROM Reservation_Table WHERE Reservation_ID = @ReservationID";
            // Get Connection
            SqlConnection conn = GetConnection();
            // Passsing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            // Command Type (Text, Store Procedure)
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ReservationID", SqlDbType.Int).Value = ID;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Canceled Successfully!", "Reservation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error! \n" + ex.ToString(), "Canceled Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();   // Connection Close
            return true;
        }

        public int Count(string query)
        {
            string sql = query;
            //  Get Connection
            SqlConnection conn = GetConnection();
            //  Passing Query & Connection to SqlCommand object
            SqlCommand cmd = new SqlCommand(sql, conn);
            int rows = (int)cmd.ExecuteScalar();
            return rows;
        }
    }
}
