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
    public partial class Teachers : Form
    {
        public Teachers()
        {
            InitializeComponent();
            DisplayTeachers();
        }
        static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=bp_2022_projekat";
        MySqlConnection databaseConnection = new MySqlConnection(Teachers.MySQLConnectionString);
        //imeStudent,prezimeStudent,idSmer,jmbgStudent,emailStudent,polStudent,datumRodjenjaStudent,mestoRodjenjaStudent,adresaStudent,godUpisa,indeksStudent
        private void DisplayTeachers(string searchQuery = "Select predavac.imePredavac, predavac.prezimePredavac, predavac.emailPredavac, predavac.polPredavac, predavac.datumRodjenjaPredavac, predavac.titula FROM predavac")
        {
            this.databaseConnection.Open();
            string query = "Select predavac.idPredavac, predavac.imePredavac, predavac.prezimePredavac, predavac.jmbgPredavac, predavac.emailPredavac, predavac.polPredavac, predavac.datumRodjenjaPredavac, predavac.adresaPredavac, predavac.titula, grad.naziv FROM predavac, grad WHERE predavac.ptt = grad.ptt";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, databaseConnection);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TeachersDVG.DataSource = ds.Tables[0];
            

            MySqlDataAdapter sda2 = new MySqlDataAdapter(searchQuery, databaseConnection);
            MySqlCommandBuilder Builder2 = new MySqlCommandBuilder(sda2);
            var ds2 = new DataSet();
            sda2.Fill(ds2);
            TeachersSearchDVG.DataSource = ds2.Tables[0];
            this.databaseConnection.Close();

        }
        int Key = 0;
        private void Reset()
        {
            Key = 0;
            imePredavacTb.Text = "";
            prezimePredavacTb.Text = "";
            titulaCb.SelectedItem = null;
            jmbgPredavacTb.Text = "";
            emailPredavacTb.Text = "";
            gradCb.SelectedItem = null;
            adresaPredavacTb.Text = "";
            polPredavacCb.SelectedItem = null;
            imeSearchTb.Text = "";
            prezimeSearchTb.Text = "";
        }
        private void Teachers_Load(object sender, EventArgs e)
        {
            /*      Cities      */

            string queryCity = "SELECT * FROM grad";

            MySqlCommand commandDatabaseCity = new MySqlCommand(queryCity, this.databaseConnection);
            MySqlDataReader reader;
            try
            {
                this.databaseConnection.Open();
                reader = commandDatabaseCity.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Grad has ptt, naziv
                        string[] row = { reader.GetString(0), reader.GetString(1) };
                        gradCb.Items.Add(row[1]);
                    }
                    commandDatabaseCity.Dispose();
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
            string selectQuery = "Select predavac.imePredavac, predavac.prezimePredavac, predavac.emailPredavac, predavac.polPredavac, predavac.datumRodjenjaPredavac, predavac.titula FROM predavac"; 
            int idSmer = 0;

            // checking if ime and prezime are entered
            if (imeSearchTb.Text == "" & prezimeSearchTb.Text == "")
            {
                selectQuery = "Select predavac.imePredavac, predavac.prezimePredavac, predavac.emailPredavac, predavac.polPredavac, predavac.datumRodjenjaPredavac, predavac.titula FROM predavac";
            }
            else if (imeSearchTb.Text != "" ^ prezimeSearchTb.Text != "")
            {

                MessageBox.Show("Please enter full name,\nif you're opting for name search!");
            }
            else if (imeSearchTb.Text != "" & prezimeSearchTb.Text != "")
            {
                string ime = imeSearchTb.Text;
                string prezime = prezimeSearchTb.Text;
                selectQuery =" Select predavac.imePredavac, predavac.prezimePredavac, predavac.emailPredavac, predavac.polPredavac, predavac.datumRodjenjaPredavac, predavac.titula FROM predavac " +
                    "WHERE `imePredavac`=" + "'" + ime + "'" + " AND `prezimePredavac`=" + "'" + prezime + "'";

                return selectQuery;

            }
            else
            {
                MessageBox.Show("This student does not exist");
            }

            return selectQuery;
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (imePredavacTb.Text == "" || prezimePredavacTb.Text == "" || jmbgPredavacTb.Text == ""
                || polPredavacCb.SelectedIndex == -1 || adresaPredavacTb.Text == ""
                || titulaCb.SelectedIndex == -1 || emailPredavacTb.Text == "" || gradCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {


                // inserting
                try
                {
                    this.databaseConnection.Open();
                    // catching ptt from gradCb that uses only nazivGrad

                    string queryGradPtt = "SELECT ptt FROM grad WHERE naziv = @selectedCity";

                    MySqlCommand cmd2 = new MySqlCommand(queryGradPtt, this.databaseConnection);
                    cmd2.Parameters.AddWithValue("@selectedCity", gradCb.SelectedItem.ToString());

                    int ptt = 0;
                    //MySqlDataReader readerCity;
                    MySqlDataReader reader;
                    reader = cmd2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ptt = reader.GetInt32("ptt");
                        }
                    }
                    reader.Close();
                    Console.WriteLine(value: ptt);

                    string queryInsertToStudent = "INSERT INTO predavac(imePredavac,prezimePredavac,jmbgPredavac,emailPredavac,polPredavac,datumRodjenjaPredavac,adresaPredavac,titula,ptt) values (@imePredavac,@prezimePredavac,@jmbgPredavac,@emailPredavac,@polPredavac,@datumRodjenjaPredavac,@adresaPredavac,@titula,@ptt)";
                    MySqlCommand cmd3 = new MySqlCommand(queryInsertToStudent, this.databaseConnection);
                    cmd3.Parameters.AddWithValue("@imePredavac", imePredavacTb.Text);
                    cmd3.Parameters.AddWithValue("@prezimePredavac", prezimePredavacTb.Text);
                    cmd3.Parameters.AddWithValue("@jmbgPredavac", jmbgPredavacTb.Text);
                    cmd3.Parameters.AddWithValue("@ptt", ptt);
                    cmd3.Parameters.AddWithValue("@emailPredavac", emailPredavacTb.Text);
                    cmd3.Parameters.AddWithValue("@polPredavac", polPredavacCb.SelectedItem.ToString());
                    cmd3.Parameters.AddWithValue("@datumRodjenjaPredavac", datumRodjenjaPredavacDtp.Value.Date);
                    cmd3.Parameters.AddWithValue("@adresaPredavac", adresaPredavacTb.Text);
                    cmd3.Parameters.AddWithValue("@titula", titulaCb.SelectedItem.ToString());
                    cmd3.ExecuteNonQuery();

                    MessageBox.Show("Teacher Added");
                    this.databaseConnection.Close();
                    DisplayTeachers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    this.databaseConnection.Close();
                }
            }
        }

        private void BackPb_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TeachersDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            imePredavacTb.Text = TeachersDVG.SelectedRows[0].Cells[1].Value.ToString();
            prezimePredavacTb.Text = TeachersDVG.SelectedRows[0].Cells[2].Value.ToString();
            jmbgPredavacTb.Text = TeachersDVG.SelectedRows[0].Cells[3].Value.ToString();
            emailPredavacTb.Text = TeachersDVG.SelectedRows[0].Cells[4].Value.ToString();
            polPredavacCb.SelectedItem = TeachersDVG.SelectedRows[0].Cells[5].Value.ToString();
            datumRodjenjaPredavacDtp.Text = TeachersDVG.SelectedRows[0].Cells[6].Value.ToString();
            adresaPredavacTb.Text = TeachersDVG.SelectedRows[0].Cells[7].Value.ToString();
            titulaCb.Text = TeachersDVG.SelectedRows[0].Cells[8].Value.ToString();
            gradCb.SelectedItem = TeachersDVG.SelectedRows[0].Cells[9].Value.ToString();

            if (imePredavacTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TeachersDVG.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (imePredavacTb.Text == "" || prezimePredavacTb.Text == "" || jmbgPredavacTb.Text == ""
                || polPredavacCb.SelectedIndex == -1 || adresaPredavacTb.Text == ""
                || titulaCb.SelectedIndex == -1 || emailPredavacTb.Text == "" || gradCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    this.databaseConnection.Open();
                    MySqlDataReader reader;

                    // catching ptt from gradCb that uses only nazivGrad

                    string queryGradPtt = "SELECT ptt FROM grad WHERE naziv = @selectedCity";

                    MySqlCommand cmd2 = new MySqlCommand(queryGradPtt, this.databaseConnection);
                    cmd2.Parameters.AddWithValue("@selectedCity", gradCb.SelectedItem.ToString());

                    int ptt = 0;
                    //MySqlDataReader readerCity;
                    reader = cmd2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ptt = reader.GetInt32("ptt");
                        }
                    }
                    reader.Close();
                    Console.WriteLine(value: ptt);

                    string query = "UPDATE predavac SET imePredavac=@imePredavac,prezimePredavac=@prezimePredavac,jmbgPredavac=@jmbgPredavac,ptt=@ptt,emailPredavac=@emailPredavac,polPredavac=@polPredavac,datumRodjenjaPredavac=@datumRodjenjaPredavac,adresaPredavac=@adresaPredavac,titula=@titula WHERE predavac.idPredavac = @Key";
                    MySqlCommand cmd3 = new MySqlCommand(query, this.databaseConnection);
                    cmd3.Parameters.AddWithValue("@imePredavac", imePredavacTb.Text);
                    cmd3.Parameters.AddWithValue("@prezimePredavac", prezimePredavacTb.Text);
                    cmd3.Parameters.AddWithValue("@jmbgPredavac", jmbgPredavacTb.Text);
                    cmd3.Parameters.AddWithValue("@ptt", ptt);
                    cmd3.Parameters.AddWithValue("@emailPredavac", emailPredavacTb.Text);
                    cmd3.Parameters.AddWithValue("@polPredavac", polPredavacCb.SelectedItem.ToString());
                    cmd3.Parameters.AddWithValue("@datumRodjenjaPredavac", datumRodjenjaPredavacDtp.Value.Date);
                    cmd3.Parameters.AddWithValue("@adresaPredavac", adresaPredavacTb.Text);
                    cmd3.Parameters.AddWithValue("@titula", titulaCb.SelectedItem.ToString());
                    cmd3.Parameters.AddWithValue("@Key", Key);
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("Teacher Updated");
                    this.databaseConnection.Close();
                    DisplayTeachers();
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

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Teacher");
            }
            else
            {
                try
                {
                    this.databaseConnection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM predavac WHERE idPredavac = @TeacherKey", databaseConnection);
                    cmd.Parameters.AddWithValue("@TeacherKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Deleted");
                    this.databaseConnection.Close();
                    DisplayTeachers();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string searchQuery = Select();
            DisplayTeachers(searchQuery);
        }

        private void ClearSearchBtn_Click(object sender, EventArgs e)
        {
            Reset();
            DisplayTeachers();
        }
    }
}
