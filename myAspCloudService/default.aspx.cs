using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myAspCloudService
{
    public partial class _default : System.Web.UI.Page
    {
        long i = 0;
        Users myUsers = new Users();
        const string CONSTRING = "Server=instance29437.db.xeround.com;Port=19153;Database=users;Uid=appharbor;Pwd=NNDKjRzh";

        protected void Page_Load(object sender, EventArgs e)
        {
            myUsers.updateUserArray(true);

           // testLbl.Text = myUsers.count + "  INIT max: " + myUsers.max;

            Timer1.Enabled = true;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //testzweck:
            lstBox1.Items.Add("------------------- new tick ----------------------");

            // algo vom backgroundworker
            if (i % 5 == 0)
            {   
                myUsers.updateUserArray(false);
            }

            //TESTZWECK
         //   testLbl.Text = myUsers.count + "  tick max: " + myUsers.max;
            string[] sArray = myUsers.checkUsers();
            foreach (string s in sArray)
            {
                if (s != null)
                    lstBox1.Items.Add(s);
            }
            i++;
        }
    }
}