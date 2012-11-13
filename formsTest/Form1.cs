using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace formsTest
{
    public partial class Form1 : Form
    {
        Users myUsers = new Users();
        const string CONSTRING = "Server=instance29437.db.xeround.com;Port=19153;Database=users;Uid=appharbor;Pwd=NNDKjRzh";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDoIt_Click(object sender, EventArgs e)
        {

            MySqlConnection con = new MySqlConnection(CONSTRING);


            MySqlDataAdapter adapter = new MySqlDataAdapter();

            string cmdText = "SELECT * FROM myapptable";
            MySqlCommand cmd = new MySqlCommand(cmdText, con);

            DataTable dt = new DataTable();
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;


            lblStatus.Text = "Daten abgefragt";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSchreiben_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(CONSTRING);
            if (con.State != ConnectionState.Open)
                try
                {
                    con.Open();
                }
                catch (MySqlException ex)
                {
                    throw (ex);
                }

            string cmdText = "INSERT INTO `myapptable`(`ID`, `changed`, `PushNotificationURI`, `delete`, `UsePushNotifications`) VALUES (NULL,1,'myFormUri',0,1)";
            MySqlCommand cmd = new MySqlCommand(cmdText, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //testzweck:
            lstBox1.Items.Add("------------------- new tick ----------------------");

            // algo vom backgroundworker
            myUsers.updateUserArray(false);


            //TESTZWECK
            testLbl.Text = myUsers.count + "  tick max: " + myUsers.max;
            string[] sArray = myUsers.checkUsers();
            foreach (string s in sArray)
            {
                if (s != null)
                lstBox1.Items.Add(s);
            }
        }

        private void usersTestBtn_Click(object sender, EventArgs e)
        {
            myUsers.updateUserArray(true);
        
            testLbl.Text = myUsers.count + "  INIT max: " + myUsers.max;

            timer1.Enabled = true;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //testzweck:
            lstBox1.Items.Add("------------------- new tick ----------------------");

            // algo vom backgroundworker
            myUsers.updateUserArray(false);


            //TESTZWECK
            testLbl.Text = myUsers.count + "  tick max: " + myUsers.max;
            string[] sArray = myUsers.checkUsers();
            foreach (string s in sArray)
            {
                if (s != null)
                    lstBox1.Items.Add(s);
            }





        }

    }
}
