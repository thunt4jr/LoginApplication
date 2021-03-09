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

namespace LoginApplication
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        // Database Connection

        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyDatabase.mdf;Integrated Security=True;";

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (text_Username.Text=="" || text_Password.Text=="")
            {
                MessageBox.Show("Please provide Username and Password");
                return;
            }
            try
            {
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("Select * from tbl_Login where UserName=@username and Password=@password", con);
                cmd.Parameters.AddWithValue("@username", txt_UserName.Text);
                cmd.Parameters.AddWithValue("@password", txt_Password.Text);
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                con.Close();
                int count = ds.Tables[0].Rows.Count;

                if (count == 1)
                {
                    MessageBox.Show("Login Successful!");
                    this.Hide();
                    FormMain fm = new FormMain();
                    fm.Show(); 
                }
                else
                {
                    MessageBox.Show("Login Falled!");
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
