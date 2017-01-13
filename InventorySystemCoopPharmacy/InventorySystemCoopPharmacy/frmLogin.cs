using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace InventorySystemCoopPharmacy
{
    public partial class frmLogin : Form
    {
        String cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\Invent_DB.accdb;";
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                if (txtUserName.Text == "")
                {
                    MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Focus();
                    return;
                }
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    return;
                }
                try
                {
                    OleDbConnection myConnection = default(OleDbConnection);
                    myConnection = new OleDbConnection(cs);

                    OleDbCommand myCommand = default(OleDbCommand);

                    myCommand = new OleDbCommand("SELECT Username,User_password FROM Users WHERE Username = @username AND User_password = @UserPassword", myConnection);
                    OleDbParameter uName = new OleDbParameter("@username", OleDbType.VarChar);
                    OleDbParameter uPassword = new OleDbParameter("@UserPassword", OleDbType.VarChar);
                    uName.Value = txtUserName.Text;
                    uPassword.Value = txtPassword.Text;
                    myCommand.Parameters.Add(uName);
                    myCommand.Parameters.Add(uPassword);

                    myCommand.Connection.Open();

                    OleDbDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

                    if (myReader.Read() == true)
                    {
                        int i;
                        ProgressBar1.Visible = true;
                        ProgressBar1.Maximum = 5000;
                        ProgressBar1.Minimum = 0;
                        ProgressBar1.Value = 4;
                        ProgressBar1.Step = 1;

                        for (i = 0; i <= 5000; i++)
                        {
                            ProgressBar1.PerformStep();
                        }
                        this.Hide();
                        frmMainMenu frm = new frmMainMenu();
                        frm.Show();


                    }


                    else
                    {
                        MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtUserName.Focus();

                    }
                    if (myConnection.State == ConnectionState.Open)
                    {
                        myConnection.Dispose();
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
      
        }
    }
}
