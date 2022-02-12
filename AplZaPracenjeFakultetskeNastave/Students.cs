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
    public partial class Students : Form
    {
        public Students()
        {
            InitializeComponent();
            DisplayStudents();
        }
        static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=bp_2022_projekat";
        MySqlConnection databaseConnection = new MySqlConnection(Students.MySQLConnectionString);


        //imeStudent,prezimeStudent,idSmer,jmbgStudent,emailStudent,polStudent,datumRodjenjaStudent,mestoRodjenjaStudent,adresaStudent,godUpisa,indeksStudent

        bool varify()
        {
            if (imeStudentTb.Text == "" || prezimeStudentTb.Text == "" || jmbgStudentTb.Text == ""
                || polStudentCb.SelectedIndex == -1 || adresaStudentTb.Text == ""
                || moduleCb.SelectedIndex == -1 || emailStudentTb.Text == "" || gradCb.SelectedIndex == -1)
            {
                return true;
            }
            else { return false; }
        }
        private void DisplayStudents(string searchQuery = "Select student.idStudent, student.imeStudent, student.prezimeStudent, student.jmbgStudent, student.emailStudent, student.polStudent, student.datumRodjenjaStudent, student.adresaStudent, student.godUpisa, smer.nazivSmer, grad.naziv FROM smer, student, grad WHERE student.idSmer = smer.idSmer AND student.ptt = grad.ptt")
        {
            this.databaseConnection.Open();
            string query = "Select student.idStudent, student.imeStudent, student.prezimeStudent, student.jmbgStudent, student.emailStudent, student.polStudent, student.datumRodjenjaStudent, student.adresaStudent, student.godUpisa, smer.nazivSmer, grad.naziv FROM smer, student, grad WHERE student.idSmer = smer.idSmer AND student.ptt = grad.ptt";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, databaseConnection);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            StudentsDVG.DataSource = ds.Tables[0];
            //this.databaseConnection.Close();
            //this.databaseConnection.Open();
            MySqlDataAdapter sda2 = new MySqlDataAdapter(searchQuery, databaseConnection);
            MySqlCommandBuilder Builder2 = new MySqlCommandBuilder(sda2);
            var ds2 = new DataSet();
            sda2.Fill(ds2);
            StudentsDVGSearch.DataSource = ds2.Tables[0];
            this.databaseConnection.Close();

        }
        int Key = 0;
        private void Reset()
        {
            Key = 0;
            imeStudentTb.Text = "";
            prezimeStudentTb.Text = "";
            moduleCb.SelectedItem = null;
            jmbgStudentTb.Text = "";
            emailStudentTb.Text = "";
            gradCb.SelectedItem = null;
            adresaStudentTb.Text = "";
            godUpisaTb.Text = "";
            polStudentCb.SelectedItem = null;
            imeStudentSearchTb.Text = "";
            prezimeStudentSearchTb.Text = "";
        }

        public List<int> idPredmetaZaSmer(int idSmer)
        {
            string query = "SELECT predmet.idPredmet FROM predmet WHERE predmet.idSmer = @idSmer";
            MySqlCommand cmd = new MySqlCommand(query, this.databaseConnection);
            cmd.Parameters.AddWithValue("@idSmer", idSmer);
            MySqlDataReader reader = cmd.ExecuteReader();
            int idPredmet = 0;
            List<int> idPredmetList = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //valjda hvata id studenta
                    idPredmet = reader.GetInt32(0);
                    Console.WriteLine("Ovo vidim:" + idPredmet);
                    idPredmetList.Add(idPredmet);
                    Console.WriteLine(idPredmetList);
                }
                cmd.Dispose();
                reader.Close();
            }
            return idPredmetList;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Students_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM smer";

            MySqlCommand commandDatabase = new MySqlCommand(query, this.databaseConnection);

            MySqlDataReader reader;

            /*      Modules     */
            try
            {
                this.databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Smer has idSmer, nazivSmer, stepenObr, idFakultet
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                        moduleCb.Items.Add(row[1]);
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

            /*      Cities      */

            string queryCity = "SELECT * FROM grad";

            MySqlCommand commandDatabaseCity = new MySqlCommand(queryCity, this.databaseConnection);
            //commandDatabase.CommandTimeout = 60;
            //MySqlDataReader readerCity;
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

            // search bar
            /*
            try 
            {
                this.databaseConnection.Open();
                ShowData(new MySqlCommand("SELECT * FROM `student`"));
                this.databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StudentsDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //imeStudent,prezimeStudent,idSmer,jmbgStudent,emailStudent,polStudent,datumRodjenjaStudent,mestoRodjenjaStudent,adresaStudent,godUpisa,indeksStudent
        {
            imeStudentTb.Text = StudentsDVG.SelectedRows[0].Cells[1].Value.ToString();
            prezimeStudentTb.Text = StudentsDVG.SelectedRows[0].Cells[2].Value.ToString();
            jmbgStudentTb.Text = StudentsDVG.SelectedRows[0].Cells[3].Value.ToString();
            emailStudentTb.Text = StudentsDVG.SelectedRows[0].Cells[4].Value.ToString();
            polStudentCb.SelectedItem = StudentsDVG.SelectedRows[0].Cells[5].Value.ToString();
            datumRodjenjaStudentDtp.Text = StudentsDVG.SelectedRows[0].Cells[6].Value.ToString();
            moduleCb.SelectedItem = StudentsDVG.SelectedRows[0].Cells[9].Value.ToString();
            adresaStudentTb.Text = StudentsDVG.SelectedRows[0].Cells[7].Value.ToString();
            godUpisaTb.Text = StudentsDVG.SelectedRows[0].Cells[8].Value.ToString();
            gradCb.SelectedItem = StudentsDVG.SelectedRows[0].Cells[10].Value.ToString();

            if (imeStudentTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(StudentsDVG.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Student");
            }
            else
            {
                try
                {
                    this.databaseConnection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM student WHERE idStudent = @StudentKey", databaseConnection);
                    cmd.Parameters.AddWithValue("@StudentKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Deleted");
                    this.databaseConnection.Close();
                    DisplayStudents();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void InsertSlusaRelation(int idPredmet, int idStudent)
        {
            try
            {
                string queryInsertToSlusa = "INSERT INTO slusa(idStudent,idPredmet) values (@idStudent,@idPredmet)";
                MySqlCommand cmd = new MySqlCommand(queryInsertToSlusa, this.databaseConnection);
                cmd.Parameters.AddWithValue("@idStudent", idStudent);
                cmd.Parameters.AddWithValue("@idPredmet", idPredmet);
                cmd.ExecuteNonQuery();

                //MessageBox.Show("Slusa relation added");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void moduleCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if ( varify() == true)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {


                // insert
                try
                {
                    this.databaseConnection.Open();

                    // catching idModule from moduleCb that uses only nazivSmer

                    string queryModuleId = "SELECT idSmer FROM smer WHERE nazivSmer = @selectedModule";

                    MySqlCommand cmd1 = new MySqlCommand(queryModuleId, this.databaseConnection);
                    cmd1.Parameters.AddWithValue("@selectedModule", moduleCb.SelectedItem.ToString());

                    int idSmer = 0;
                    MySqlDataReader reader;
                    reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idSmer = reader.GetInt32("idSmer");
                        }
                    }
                    reader.Close();

                    Console.WriteLine(value: idSmer);

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

                    // checking
                    int bornYear = datumRodjenjaStudentDtp.Value.Year;
                    int thisYear = DateTime.Now.Year;
                    if ((thisYear - bornYear) < 18 || (thisYear - bornYear) > 100)
                    {
                        MessageBox.Show("The student age must be between  10 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    string queryInsertToStudent = "INSERT INTO student(imeStudent,prezimeStudent,idSmer,jmbgStudent,emailStudent,polStudent,datumRodjenjaStudent,adresaStudent,godUpisa,ptt) values (@imeStudent,@prezimeStudent,@idSmer,@jmbgStudent,@emailStudent,@polStudent,@datumRodjenjaStudent,@adresaStudent,@godUpisa,@ptt)";
                    MySqlCommand cmd3 = new MySqlCommand(queryInsertToStudent, this.databaseConnection);
                    cmd3.Parameters.AddWithValue("@imeStudent", imeStudentTb.Text);
                    cmd3.Parameters.AddWithValue("@prezimeStudent", prezimeStudentTb.Text);
                    cmd3.Parameters.AddWithValue("@idSmer", idSmer);
                    cmd3.Parameters.AddWithValue("@jmbgStudent", jmbgStudentTb.Text);
                    cmd3.Parameters.AddWithValue("@ptt", ptt);
                    cmd3.Parameters.AddWithValue("@emailStudent", emailStudentTb.Text);
                    cmd3.Parameters.AddWithValue("@polStudent", polStudentCb.SelectedItem.ToString());
                    cmd3.Parameters.AddWithValue("@datumRodjenjaStudent", datumRodjenjaStudentDtp.Value.Date);
                    cmd3.Parameters.AddWithValue("@adresaStudent", adresaStudentTb.Text);
                    cmd3.Parameters.AddWithValue("@godUpisa", godUpisaTb.Text);

                    cmd3.ExecuteNonQuery();

                    string queryLastInsertIdStudent = "SELECT last_insert_id()";

                    MySqlCommand cmd5 = new MySqlCommand(queryLastInsertIdStudent, this.databaseConnection);

                    int LastInsertIdStudent = 0;
                    reader = cmd5.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            LastInsertIdStudent = reader.GetInt32("last_insert_id()");
                        }
                    }
                    reader.Close();
                    Console.WriteLine(value: LastInsertIdStudent);

                    List<int> idSvihPredmeta = idPredmetaZaSmer(idSmer);
                    for (int i = 0; i < idSvihPredmeta.Count; i++)
                    {
                        InsertSlusaRelation(idSvihPredmeta[i], LastInsertIdStudent);
                    }


                    MessageBox.Show("Student Added");
                    this.databaseConnection.Close();
                    DisplayStudents();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    this.databaseConnection.Close();
                }
            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (varify() == true)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    this.databaseConnection.Open();

                    // catching idFakultet from FacultyCb that uses onlu nazivFakultet

                    // catching idModule from moduleCb that uses only nazivSmer

                    string queryModuleId = "SELECT idSmer FROM smer WHERE nazivSmer = @selectedModule";

                    MySqlCommand cmd1 = new MySqlCommand(queryModuleId, this.databaseConnection);
                    cmd1.Parameters.AddWithValue("@selectedModule", moduleCb.SelectedItem.ToString());

                    int idSmer = 0;
                    MySqlDataReader reader;
                    reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idSmer = reader.GetInt32("idSmer");
                        }
                    }
                    reader.Close();

                    Console.WriteLine(value: idSmer);
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

                    string query = "UPDATE student SET imeStudent=@imeStudent,prezimeStudent=@prezimeStudent,idSmer=@idSmer,jmbgStudent=@jmbgStudent,ptt=@ptt,emailStudent=@emailStudent,polStudent=@polStudent,datumRodjenjaStudent=@datumRodjenjaStudent,adresaStudent=@adresaStudent,godUpisa=@godUpisa WHERE student.idStudent = @Key";
                    MySqlCommand cmd = new MySqlCommand(query, this.databaseConnection);
                    cmd.Parameters.AddWithValue("@imeStudent", imeStudentTb.Text);
                    cmd.Parameters.AddWithValue("@prezimeStudent", prezimeStudentTb.Text);
                    cmd.Parameters.AddWithValue("@idSmer", idSmer);
                    cmd.Parameters.AddWithValue("@jmbgStudent", jmbgStudentTb.Text);
                    cmd.Parameters.AddWithValue("@ptt", ptt);
                    cmd.Parameters.AddWithValue("@emailStudent", emailStudentTb.Text);
                    cmd.Parameters.AddWithValue("@polStudent", polStudentCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@datumRodjenjaStudent", datumRodjenjaStudentDtp.Value.Date);
                    cmd.Parameters.AddWithValue("@adresaStudent", adresaStudentTb.Text);
                    cmd.Parameters.AddWithValue("@godUpisa", godUpisaTb.Text);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();



                    int LastInsertIdStudent = Key;
                    query = "DELETE FROM slusa WHERE slusa.idStudent = @Key";
                    cmd = new MySqlCommand(query, this.databaseConnection);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    cmd.ExecuteNonQuery();

                    List<int> idSvihPredmeta = idPredmetaZaSmer(idSmer);
                    for (int i = 0; i < idSvihPredmeta.Count; i++)
                    {
                        InsertSlusaRelation(idSvihPredmeta[i], LastInsertIdStudent);
                    }



                    MessageBox.Show("Student Updated");
                    this.databaseConnection.Close();
                    DisplayStudents();
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


        private string Select()
        {
            string selectQuery = "Select student.idStudent, student.imeStudent, student.prezimeStudent, student.jmbgStudent, student.emailStudent, student.polStudent,student.datumRodjenjaStudent, student.adresaStudent, student.godUpisa, smer.nazivSmer, grad.naziv FROM smer, student, grad WHERE student.idSmer = smer.idSmer AND student.ptt = grad.ptt"; ;
            int idSmer = 0;

            // checking if ime and prezime are entered
            
            if (imeStudentSearchTb.Text == "" & prezimeStudentSearchTb.Text == "")
            {
                selectQuery = "Select student.idStudent, student.imeStudent, student.prezimeStudent, student.jmbgStudent, student.emailStudent, student.polStudent,student.datumRodjenjaStudent, student.adresaStudent, student.godUpisa, smer.nazivSmer, grad.naziv FROM smer, student, grad WHERE student.idSmer = smer.idSmer AND student.ptt = grad.ptt";
            }
            else if (imeStudentSearchTb.Text != "" ^ prezimeStudentSearchTb.Text != "")
            {

                MessageBox.Show("Please enter full name,\nif you're opting for name search!");
            }
            else if (imeStudentSearchTb.Text != "" & prezimeStudentSearchTb.Text != "")
            {
                string ime = imeStudentSearchTb.Text;
                string prezime = prezimeStudentSearchTb.Text;
                selectQuery = "Select student.idStudent, student.imeStudent, student.prezimeStudent, student.jmbgStudent, student.emailStudent, student.polStudent, " +
                    "student.datumRodjenjaStudent, student.adresaStudent, student.godUpisa, smer.nazivSmer, grad.naziv FROM smer, " +
                    "student, grad WHERE student.idSmer = smer.idSmer AND student.ptt = grad.ptt AND `imeStudent`=" + "'" + ime + "'" + " AND `prezimeStudent`=" + "'" + prezime + "'";

                return selectQuery;

            }
            
            /*if (idStudentSearchTb.Text == "")
            {
                selectQuery = "Select student.idStudent, student.imeStudent, student.prezimeStudent, student.jmbgStudent, student.emailStudent, student.polStudent,student.datumRodjenjaStudent, student.adresaStudent, student.godUpisa, smer.nazivSmer, grad.naziv FROM smer, student, grad WHERE student.idSmer = smer.idSmer AND student.ptt = grad.ptt";
            }
            else if (idStudentSearchTb.Text != "")
            {
                int indeks = Convert.ToInt32(idStudentSearchTb.Text);

                selectQuery = "Select student.idStudent, student.imeStudent, student.prezimeStudent, student.jmbgStudent, student.emailStudent, student.polStudent, " +
                    "student.datumRodjenjaStudent, student.adresaStudent, student.godUpisa, smer.nazivSmer, grad.naziv FROM smer, " +
                    "student, grad WHERE student.idSmer = smer.idSmer AND student.ptt = grad.ptt AND `idStudent`=" + "'" + indeks + "'";

                return selectQuery;
            }
            else
            {
                MessageBox.Show("This student does not exist");
            }*/

            return selectQuery;
        }
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string searchQuery = Select();
            DisplayStudents(searchQuery);
            Reset();
        }

        private void ClearSearchBtn_Click(object sender, EventArgs e)
        {
            Reset();
            DisplayStudents();
        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        private void idStudentSearchTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
