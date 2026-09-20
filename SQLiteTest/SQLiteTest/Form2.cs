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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                clsCon.con.Open();
                FillListView();
            }
            catch
            {
                MessageBox.Show("Cannot Open Database", "Connection Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void FillListView()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteDataReader dr;

            lvList.Items.Clear();

            cmd.CommandText = "SELECT * FROM tblUser";
            cmd.Connection = clsCon.con;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListViewItem list = new ListViewItem(dr["UserID"].ToString());
                list.SubItems.Add(dr["UserName"].ToString());
                list.SubItems.Add(dr["Password"].ToString());
                lvList.Items.AddRange(new ListViewItem[] { list });

            }
            dr.Close();
        }

       
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillListView();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.FormState = "ADD";
            f.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModifyCustomer();
        }

        public void ModifyCustomer()
        {
            SQLiteCommand com = new SQLiteCommand();
            SQLiteDataReader read;

            try
            {
                Form1 frm = new Form1();
                frm.FormState = "EDIT";

                com.CommandText = "Select * from tblUser where UserID='" + lvList.FocusedItem.Text + "'";
                com.Connection = clsCon.con;
                read = com.ExecuteReader();
                while (read.Read())
                {
                    frm.txtUsername.Text = read["Username"].ToString();
                    frm.txtPassword.Text = read["Password"].ToString();
                    frm.id = read["UserID"].ToString();
                }
                read.Close();

                frm.Focus();
                frm.Show();
            }
            catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
        }

        public void DeleteQRY(String ID)
        {
            SQLiteCommand com = new SQLiteCommand();
            com.CommandText = " DELETE FROM tblUser WHERE UserID='" + ID + "'";
            com.Connection = clsCon.con;
            com.ExecuteNonQuery();

            MessageBox.Show("Record Successfully Deleted", "SQLite Application",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteQRY(lvList.FocusedItem.Text);
        }
    }
}
