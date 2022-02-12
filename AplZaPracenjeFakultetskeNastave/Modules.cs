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
    public partial class Modules : Form
    {
        public Modules()
        {
            InitializeComponent();
            DisplayModules();
        }
        static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=bp_2022_projekat";
        MySqlConnection databaseConnection = new MySqlConnection(Modules.MySQLConnectionString);
        private void DisplayModules(string searchQuery = "Select smer.idSmer, smer.nazivSmer, smer.stepenObr, fakultet.nazivFakultet FROM smer, fakultet WHERE smer.idFakultet = fakultet.idFakultet")
        {
            this.databaseConnection.Open();
            string query = "Select smer.idSmer, smer.nazivSmer, smer.stepenObr, fakultet.nazivFakultet FROM smer, fakultet WHERE smer.idFakultet = fakultet.idFakultet";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, databaseConnection);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ModulesDVG.DataSource = ds.Tables[0];

            MySqlDataAdapter sda2 = new MySqlDataAdapter(searchQuery, databaseConnection);
            MySqlCommandBuilder Builder2 = new MySqlCommandBuilder(sda2);
            var ds2 = new DataSet();
            sda2.Fill(ds2);
            modulesSearchDVG.DataSource = ds2.Tables[0];
            this.databaseConnection.Close();
        }
        private void LoadFacultyComboBox()
        {
            
            string query = "SELECT * FROM fakultet";

            MySqlCommand commandDatabase = new MySqlCommand(query, this.databaseConnection);
            //commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                this.databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Faculty has idFakultet, nazivFakultet, adresaFakultet, idUniverzitet
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                        FacultyCb.Items.Add(row[1]);
                    }
                    commandDatabase.Dispose();
                    reader.Close();
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                this.databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string Select()
        {
            string selectQuery = "Select smer.idSmer, smer.nazivSmer, smer.stepenObr, fakultet.nazivFakultet FROM smer, fakultet WHERE smer.idFakultet = fakultet.idFakultet";
            int idSmer = 0;

            if (nazivSmerSearchTb.Text == "")
            {
                selectQuery = "Select smer.idSmer, smer.nazivSmer, smer.stepenObr, fakultet.nazivFakultet FROM smer, fakultet WHERE smer.idFakultet = fakultet.idFakultet";
                MessageBox.Show("You need to enter the name of the course you are searching for!");
            }
            else if (nazivSmerSearchTb.Text != "")
            {
                string naziv = nazivSmerSearchTb.Text;

                selectQuery = "Select smer.idSmer, smer.nazivSmer, smer.stepenObr, fakultet.nazivFakultet FROM smer, fakultet WHERE smer.idFakultet = fakultet.idFakultet AND smer.nazivSmer=" + "'" + naziv + "'";

                return selectQuery;

            }
            else
            {
                MessageBox.Show("This course does not exist");
            }

            return selectQuery;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void FacultyCb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (nazivSmerTb.Text == "" || stepenObrTb.Text == "" || FacultyCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                

                // inserting
                try 
                {
                    //string selectedFaculty = FacultyCb.SelectedItem.ToString();
                    this.databaseConnection.Open();

                    // catching idFakultet from FacultyCb that uses onlu nazivFakultet

                    string queryFacultyId = "SELECT idFakultet FROM fakultet WHERE nazivFakultet = @selectedFaculty";

                    MySqlCommand cmd1 = new MySqlCommand(queryFacultyId, this.databaseConnection);
                    cmd1.Parameters.AddWithValue("@selectedFaculty", FacultyCb.SelectedItem.ToString());

                    int idFakultet = 0;
                    MySqlDataReader reader;
                    reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idFakultet = reader.GetInt32("idFakultet");
                        }
                    }
                    reader.Close();
                    //this.databaseConnection.Close();
                    Console.WriteLine(value: idFakultet);

                    //this.databaseConnection.Open();
                    string queryInsertToFakultet = "INSERT INTO smer(nazivSmer,stepenObr,idFakultet) values (@nazivSmer,@stepenObr,@idFakultet)";
                    MySqlCommand cmd2 = new MySqlCommand(queryInsertToFakultet, this.databaseConnection);
                    cmd2.Parameters.AddWithValue("@nazivSmer", nazivSmerTb.Text);
                    cmd2.Parameters.AddWithValue("@stepenObr", stepenObrTb.Text);
                    cmd2.Parameters.AddWithValue("@idFakultet", idFakultet);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Course Added");
                    this.databaseConnection.Close();
                    DisplayModules();
                    Reset();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    this.databaseConnection.Close();
                }     
            }
        }

        private void Modules_Load(object sender, EventArgs e)
        {
            LoadFacultyComboBox();
        }

        private void BackPb_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void nazivSmer_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Reset()
        {
            Key = 0;
            nazivSmerTb.Text = "";
            stepenObrTb.Text = "";
            FacultyCb.SelectedItem = null;

        }
        int Key = 0;
        private void ModulesDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //ModulesDVG.ReadOnly = true;
            nazivSmerTb.Text = ModulesDVG.SelectedRows[0].Cells[1].Value.ToString();
            stepenObrTb.Text = ModulesDVG.SelectedRows[0].Cells[2].Value.ToString();
            FacultyCb.SelectedItem = ModulesDVG.SelectedRows[0].Cells[3].Value.ToString();
            
            if (nazivSmerTb.Text == "")
            {
                Key = 0;
            }else
            {
                Key = Convert.ToInt32(ModulesDVG.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Module");
            }
            else 
            {
                try
                {
                    this.databaseConnection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM smer WHERE idSmer = @ModuleKey", databaseConnection);
                    cmd.Parameters.AddWithValue("@ModuleKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Module Deleted");
                    this.databaseConnection.Close();
                    DisplayModules();
                    Reset();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private string SelectElement(string nameOfElement, string nameOfTable, string selectedItem) 
        {
            string element = "";
            try
            {
                this.databaseConnection.Open();
                string query = "SELECT " + nameOfElement + " FROM " + nameOfTable + " WHERE " + nameOfElement + " = " + "@" + nameOfTable;
                MySqlCommand cmd = new MySqlCommand(query, this.databaseConnection);
                cmd.Parameters.AddWithValue("@" + nameOfElement, selectedItem);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        element = reader.GetString(Name);
                    }
                }
                reader.Close();
                this.databaseConnection.Close();
                MessageBox.Show("Uhvatio");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return element;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (nazivSmerTb.Text == "" || stepenObrTb.Text == "" || FacultyCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                // inserting
                int idFakultet = 0;

                try
                {
                    this.databaseConnection.Open();

                    // catching idFakultet from FacultyCb that uses onlu nazivFakultet

                    string queryFacultyId = "SELECT idFakultet FROM fakultet WHERE nazivFakultet = @selectedFaculty";

                    MySqlCommand cmd1 = new MySqlCommand(queryFacultyId, this.databaseConnection);
                    cmd1.Parameters.AddWithValue("@selectedFaculty", FacultyCb.SelectedItem.ToString());
                        
                    MySqlDataReader reader;
                    reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idFakultet = reader.GetInt32("idFakultet");
                        }
                    }
                    reader.Close();

                    Console.WriteLine(value: idFakultet);
                    string query = "UPDATE smer SET nazivSmer = @nazivSmer,stepenObr = @stepenObr, idFakultet = @idFakultet WHERE smer.idSmer = @Key";
                    MySqlCommand cmd2 = new MySqlCommand(query, this.databaseConnection);
                    cmd2.Parameters.AddWithValue("@nazivSmer", nazivSmerTb.Text);
                    cmd2.Parameters.AddWithValue("@stepenObr", stepenObrTb.Text);
                    cmd2.Parameters.AddWithValue("@idFakultet", idFakultet);
                    cmd2.Parameters.AddWithValue("@Key", Key);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Course Updated");
                    this.databaseConnection.Close();
                    DisplayModules();
                    Reset();
                }   
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    this.databaseConnection.Close();
                }
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void FacultyCb_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string searchQuery = Select();
            DisplayModules(searchQuery);
        }

        private void ClearSearchBtn_Click(object sender, EventArgs e)
        {
            Reset();
            DisplayModules();
        }
    }
}
