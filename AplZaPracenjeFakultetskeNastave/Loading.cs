using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AplZaPracenjeFakultetskeNastave
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
            //string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=bp_2021_projekat";
            //MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bunifuProgressBar1.Value < 100)
            {
                bunifuProgressBar1.Value += 1;

                label3.Text = bunifuProgressBar1.Value.ToString() + "%";
            }
            else
            {
                timer1.Stop();

                Login login = new Login();
                login.Show();
                this.Hide();
                
            }
        }

        private void bunifuProgressBar1_progressChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
