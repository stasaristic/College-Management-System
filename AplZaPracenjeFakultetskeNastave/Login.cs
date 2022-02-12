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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }
        static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=bp_2022_projekat";
        MySqlConnection databaseConnection = new MySqlConnection(Login.MySQLConnectionString);

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            string username, password;
            if (usernameTb.Text == "" || passwordTb.Text == "")
            {
                MessageBox.Show("Enter Username and Password");
            }
            else if (usernameTb.Text != "" & passwordTb.Text != "")
            {
                string query = "SELECT * FROM user";
                
                MySqlCommand commandDatabase = new MySqlCommand(query, this.databaseConnection);
                try
                {
                    this.databaseConnection.Open();
                    MySqlDataReader reader = commandDatabase.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //Smer has idSmer, nazivSmer, stepenObr, idFakultet
                            string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                            username = row[1];
                            password = row[2];
                            if (usernameTb.Text == username & passwordTb.Text == password)
                            {
                                MainMenu mainMenu = new MainMenu();
                                mainMenu.Show();
                                this.Hide();
                            }
                            else if (usernameTb.Text == username & passwordTb.Text != password)
                            {
                                MessageBox.Show("Wrong password!");
                            }
                            else if (usernameTb.Text != username & passwordTb.Text != password)
                            {
                                MessageBox.Show("This account does not exist!");
                            }
                        }
                        commandDatabase.Dispose();
                        reader.Close();
                    }
                    else
                    {
                        Console.WriteLine("No rows!");
                    }
                    this.databaseConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.ForeColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.ForeColor = Color.Transparent;
        }

        private void passwordTb_TextChanged(object sender, EventArgs e)
        {
            passwordTb.PasswordChar = '*';
        }
    }
}
