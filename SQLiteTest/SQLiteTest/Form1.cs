using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SQLiteTest
{
    public partial class Form1 : Form
    {
    	public string FormState;
        public string id;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			switch (FormState)
			{
				case "ADD":
				SaveData();
				break;
				case "EDIT":
				UpdateData();
				break;
			}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void UpdateData()
        {
            String sSQL;
            SQLiteCommand cmd = new SQLiteCommand();

            if (txtUsername.Text == "")
            { }
            else if (txtPassword.Text == "")
            { }
            else
            {
                sSQL = "Update tblUser SET Username = '" + txtUsername.Text + "', Password = '" + txtPassword.Text + "' WHERE UserID = " + id + "";
                cmd.CommandText = sSQL;
                cmd.Connection = clsCon.con;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Record Successfully Updated!", "SQLite Test App", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        void SaveData()
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Please fill up the Username!", "SQLite Test App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                txtUsername.Focus();
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Please fill up the Password", "SQLite Test App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();
            }
            else
            {
                CheckData();

            } 
        }

        void CheckData()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            String sSQL;

            sSQL = "Select * from tblUser Where username = '" + txtUsername.Text + "'";
            cmd.CommandText = sSQL;
            cmd.Connection = clsCon.con;
            SQLiteDataReader dr2;
            dr2 = cmd.ExecuteReader();
            dr2.Read();

            if (dr2.HasRows)
            {
                MessageBox.Show("Username Already Exist!", "SQLite Test Application", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsername.Focus();

            }

            else
            {
                dr2.Close();
                sSQL = "Insert Into tblUser([username],[password]) Values('" + txtUsername.Text + "','" + txtPassword.Text + "')";
                cmd.CommandText = sSQL;
                cmd.Connection = clsCon.con;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Record Successfuly Save!", "SQLite Test Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            dr2.Close();
            dr2.Dispose();
        }

           private void Form1_Load(object sender, EventArgs e)
        {
            if (clsCon.con.State == ConnectionState.Open)
            { clsCon.con.Close(); }
            clsCon.con.Open();
        }

    }


}
