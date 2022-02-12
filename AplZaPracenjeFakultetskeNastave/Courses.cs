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
    public partial class Courses : Form
    {
        public Courses()
        {
            InitializeComponent();
            DisplayCourses();

            //inheritance
            Examination examination = new Examination(this, this.Key);
        }
        static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=bp_2022_projekat";
        MySqlConnection databaseConnection = new MySqlConnection(Courses.MySQLConnectionString);

        private void DisplayCourses(string searchQuery = "Select predmet.idPredmet, predmet.nazivPredmet, predmet.brEspb, smer.nazivSmer FROM smer, predmet WHERE predmet.idSmer = smer.idSmer")
        {
            this.databaseConnection.Open();
            string query = "Select predmet.idPredmet, predmet.nazivPredmet, predmet.brEspb, smer.nazivSmer FROM smer, predmet WHERE predmet.idSmer = smer.idSmer";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, databaseConnection);
            
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CoursesDVG.DataSource = ds.Tables[0];

            MySqlDataAdapter sda2 = new MySqlDataAdapter(searchQuery, databaseConnection);
            MySqlCommandBuilder Builder2 = new MySqlCommandBuilder(sda2);
            var ds2 = new DataSet();
            sda2.Fill(ds2);
            CoursesSearchDVG.DataSource = ds2.Tables[0];

            this.databaseConnection.Close();

        }
        int Key = 0;
        private void Reset()
        {
            Key = 0;
            nazivPredmetTb.Text = "";
            brEspbTb.Text = "";
            profesorCb.SelectedItem = null;
            asistentCb.SelectedItem = null;
            smerCb.SelectedItem = null;
        }
        private string Select()
        {
            string selectQuery = "Select predmet.idPredmet, predmet.nazivPredmet, predmet.brEspb, smer.nazivSmer FROM smer, predmet WHERE predmet.idSmer = smer.idSmer";
            int idSmer = 0;

            // checking if ime and prezime are entered
            if (nazivPredmetSearchTb.Text == "")
            {
                selectQuery = "Select predmet.idPredmet, predmet.nazivPredmet, predmet.brEspb, smer.nazivSmer FROM smer, predmet WHERE predmet.idSmer = smer.idSmer";
                MessageBox.Show("You need to enter the name of the course you are searching for!");
            }
            else if (nazivPredmetSearchTb.Text != "")
            {
                string naziv= nazivPredmetSearchTb.Text;

                selectQuery = "Select predmet.idPredmet, predmet.nazivPredmet, predmet.brEspb, smer.nazivSmer FROM smer, predmet WHERE predmet.idSmer = smer.idSmer AND predmet.nazivPredmet=" + "'" + naziv + "'";

                return selectQuery;

            }
            else
            {
                MessageBox.Show("This course does not exist");
            }

            return selectQuery;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string searchQuery = Select();
            DisplayCourses(searchQuery);
        }
        private void InsertPredajeRelation(int idPredavac, int idPredmet)
        {
            try 
            {

                // inserting to predaje table for predaje
                string queryInsertToPredaje = "INSERT INTO predaje(idPredavac,idPredmet) values (@idPredavac,@idPredmet)";
                MySqlCommand cmd = new MySqlCommand(queryInsertToPredaje, this.databaseConnection);
                cmd.Parameters.AddWithValue("@idPredavac", idPredavac);
                cmd.Parameters.AddWithValue("@idPredmet", idPredmet);

                cmd.ExecuteNonQuery();
                //MessageBox.Show("dodato predaje");

                //this.databaseConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void InsertSlusaRelation(int idPredmet, int idStudent)
        {
            try
            {
                // inserting into slusa
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

        private int EspbPredmetSmer(int idSmer)
        {
            //string queryEspb = "SELECT sum(predmet.brEspb) FROM predmet WHERE predmet.idSmer = @idSmer";
            string queryEspb = "SELECT IFNULL(sum(predmet.brEspb),0) AS sum FROM predmet WHERE predmet.idSmer = @idSmer";
            MySqlCommand cmd = new MySqlCommand(queryEspb, this.databaseConnection);
            cmd.Parameters.AddWithValue("@idSmer", idSmer);

            int sum = 0;
            //string ssum = Convert.ToString(cmd.ExecuteScalar());
            //sum = Int32.Parse(ssum);
            //string ssum = "";


            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //ssum = reader[0].ToString();
                    sum = reader.GetInt32("sum");
                }
            }
            Console.WriteLine(value: sum);
            //int sum = cmd.ExecuteNonQuery();
            string queryEspbTotal = "SELECT smer.stepenObr FROM smer WHERE smer.idSmer = @idSmer";
            MySqlCommand cmd1 = new MySqlCommand(queryEspbTotal, this.databaseConnection);
            cmd1.Parameters.AddWithValue("@idSmer", idSmer);

            int total = Convert.ToInt32(cmd1.ExecuteScalar());
            Console.WriteLine(value: total);

            int espbLeft = total - sum;
            Console.WriteLine(value: espbLeft);
            return espbLeft;
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (nazivPredmetTb.Text == "" || brEspbTb.Text == "" || asistentCb.SelectedIndex == -1 || profesorCb.SelectedIndex == -1 || smerCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                // inserting
                try
                {
                    this.databaseConnection.Open();

                    // catching idModule from moduleCb that uses only nazivSmer

                    string queryModuleId = "SELECT idSmer FROM smer WHERE smer.nazivSmer = @selectedModule";

                    MySqlCommand cmd1 = new MySqlCommand(queryModuleId, this.databaseConnection);
                    cmd1.Parameters.AddWithValue("@selectedModule", smerCb.SelectedItem.ToString());

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

                    // catching idPredavac from profesorCb

                    string queryProfessorId = "SELECT idPredavac FROM predavac WHERE idPredavac = @idPredavac";

                    MySqlCommand cmd2 = new MySqlCommand(queryProfessorId, this.databaseConnection);
                    string[] idPredavacProf = profesorCb.SelectedItem.ToString().Split(' ');
                    cmd2.Parameters.AddWithValue("@idPredavac", idPredavacProf[1]);

                    int idProfessor = 0;
                    reader = cmd2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idProfessor = reader.GetInt32("idPredavac");
                        }
                    }
                    reader.Close();
                    Console.WriteLine(value: idProfessor);

                    // catching idPredavac from assistantCb

                    string queryAssistantId = "SELECT idPredavac FROM predavac WHERE idPredavac = @idPredavac";

                    MySqlCommand cmd3 = new MySqlCommand(queryAssistantId, this.databaseConnection);
                    string[] idPredavacAss = asistentCb.SelectedItem.ToString().Split(' ');
                    cmd3.Parameters.AddWithValue("@idPredavac", idPredavacAss[1]);

                    int idAssistant = 0;
                    reader = cmd3.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idAssistant = reader.GetInt32("idPredavac");
                        }
                    }
                    reader.Close();
                    Console.WriteLine(value: idAssistant);

                    // inserting to predmet table

                    int espbLeft = EspbPredmetSmer(idSmer);
                    int brEspb = Int32.Parse(brEspbTb.Text);

                    if (espbLeft > 0 & (espbLeft - brEspb) >= 0)
                    {
                        string queryInsertToPredmet = "INSERT INTO predmet(nazivPredmet,brEspb,idSmer) values (@nazivPredmet,@brEspb,@idSmer)";
                        MySqlCommand cmd4 = new MySqlCommand(queryInsertToPredmet, this.databaseConnection);
                        cmd4.Parameters.AddWithValue("@nazivPredmet", nazivPredmetTb.Text);
                        cmd4.Parameters.AddWithValue("@brEspb", brEspbTb.Text);
                        cmd4.Parameters.AddWithValue("@idSmer", idSmer);

                        cmd4.ExecuteNonQuery();
                        MessageBox.Show("Course added!");

                        string queryLastInsertIdPredmet = "SELECT last_insert_id()";

                        MySqlCommand cmd5 = new MySqlCommand(queryLastInsertIdPredmet, this.databaseConnection);

                        int LastInsertIdPredmet = 0;
                        reader = cmd5.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                LastInsertIdPredmet = reader.GetInt32("last_insert_id()");
                            }
                        }
                        reader.Close();
                        Console.WriteLine(value: LastInsertIdPredmet);

                        InsertPredajeRelation(idProfessor, LastInsertIdPredmet);
                        InsertPredajeRelation(idAssistant, LastInsertIdPredmet);


                       // MessageBox.Show("Predaje relation for selected professor added");

                        //inserting to slusa table for all students that are following the module
                        string querySlusa = "SELECT student.idStudent, student.idSmer, predmet.idPredmet, predmet.idSmer from student, predmet WHERE student.idSmer = predmet.idSmer AND predmet.idPredmet = @idPredmet";

                        MySqlCommand cmd8 = new MySqlCommand(querySlusa, this.databaseConnection);
                        cmd8.Parameters.AddWithValue("@idPredmet", LastInsertIdPredmet);
                        reader = cmd8.ExecuteReader();
                        int idStudent = 0;
                        List<int> slusaStudents = new List<int>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //valjda hvata id studenta
                                idStudent = reader.GetInt32(0);
                                //Console.WriteLine("Ovo vidim:" + idStudent);
                                slusaStudents.Add(idStudent);
                                //Console.WriteLine(slusaStudents);
                            }
                            cmd8.Dispose();
                            reader.Close();
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }

                        for (int i = 0; i < slusaStudents.Count; i++)
                        {
                            InsertSlusaRelation(LastInsertIdPredmet, slusaStudents[i]);
                        }
                    }
                    else if ((espbLeft - brEspb) < 0 & espbLeft != 0)
                    {
                        MessageBox.Show("There is " + (espbLeft).ToString() + " espb left for this module,\nso lower the number of espb!");
                    }
                    else 
                    {
                        MessageBox.Show("This module doesn't have any more free ESPB!\nThat means no more courses can be added!");
                    }

                    this.databaseConnection.Close();
                    

                    DisplayCourses();
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

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Courses_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM smer";

            MySqlCommand commandDatabase = new MySqlCommand(query, this.databaseConnection);
            //commandDatabase.CommandTimeout = 60;
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
                        smerCb.Items.Add(row[1]);
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

            /*      Professors     */

            string queryProffesor = "SELECT * FROM predavac WHERE predavac.titula=@titula";

            MySqlCommand commandDatabaseProfessor = new MySqlCommand(queryProffesor, this.databaseConnection);
            commandDatabaseProfessor.Parameters.AddWithValue("@titula", "Professor");
            //commandDatabase.CommandTimeout = 60;
            //MySqlDataReader readerCity;
            try
            {
                this.databaseConnection.Open();
                reader = commandDatabaseProfessor.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //predavac has idPredavac, imePredavac, prezimePredavac
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2) };
                        // adding id in front of the name in case there is two predavac that go by the same name
                        // this will later help me get the correct values for predavac
                        string imePrezimePredavaca = "id: "+ row[0] + " "+ row[1] + " " + row[2];
                        profesorCb.Items.Add(imePrezimePredavaca);
                    }
                    commandDatabaseProfessor.Dispose();
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
            /*      Assistants     */

            string queryAssistant = "SELECT * FROM predavac WHERE predavac.titula=@titula";

            MySqlCommand commandDatabaseAssistant = new MySqlCommand(queryAssistant, this.databaseConnection);
            commandDatabaseAssistant.Parameters.AddWithValue("@titula", "Assistant");
            //commandDatabase.CommandTimeout = 60;
            //MySqlDataReader readerCity;
            try
            {
                this.databaseConnection.Open();
                reader = commandDatabaseAssistant.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Predavac has id, imePredavac, prezimePredavac
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2) };
                        // adding id in front of the name in case there is two predavac that go by the same name
                        // this will later help me get the correct values for predavac
                        string imePrezimePredavaca = "id: " + row[0] + " " + row[1] + " " + row[2];
                        asistentCb.Items.Add(imePrezimePredavaca);
                    }
                    commandDatabaseAssistant.Dispose();
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

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (nazivPredmetTb.Text == "" || brEspbTb.Text == "" || asistentCb.SelectedIndex == -1 || profesorCb.SelectedIndex == -1 || smerCb.SelectedIndex == -1)
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
                    cmd1.Parameters.AddWithValue("@selectedModule", smerCb.SelectedItem.ToString());

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

                    // catching idPredavac from profesorCb

                    string queryProfessorId = "SELECT idPredavac FROM predavac WHERE idPredavac = @idPredavac";

                    MySqlCommand cmd2 = new MySqlCommand(queryProfessorId, this.databaseConnection);
                    string[] idPredavacProf = profesorCb.SelectedItem.ToString().Split(' ');
                    cmd2.Parameters.AddWithValue("@idPredavac", idPredavacProf[1]);

                    int idProfessor = 0;
                    reader = cmd2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idProfessor = reader.GetInt32("idPredavac");
                        }
                    }
                    reader.Close();
                    Console.WriteLine(value: idProfessor);

                    // catching idPredavac from assistantCb

                    string queryAssistantId = "SELECT idPredavac FROM predavac WHERE idPredavac = @idPredavac";

                    MySqlCommand cmd3 = new MySqlCommand(queryAssistantId, this.databaseConnection);
                    string[] idPredavacAss = asistentCb.SelectedItem.ToString().Split(' ');
                    cmd3.Parameters.AddWithValue("@idPredavac", idPredavacAss[1]);

                    int idAssistant = 0;
                    reader = cmd3.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idAssistant = reader.GetInt32("idPredavac");
                        }
                    }
                    reader.Close();
                    Console.WriteLine(value: idAssistant);


                    string query = "UPDATE predmet SET nazivPredmet=@nazivPredmet,brEspb=@brEspb,idSmer=@idSmer,idProfessor=@idProfessor,idAssistant=@idAssistant WHERE predmet.idPredmet = @Key";
                    MySqlCommand cmd = new MySqlCommand(query, this.databaseConnection);
                    cmd.Parameters.AddWithValue("@nazivPredmet", nazivPredmetTb.Text);
                    cmd.Parameters.AddWithValue("@brEspb", brEspbTb.Text);
                    cmd.Parameters.AddWithValue("@idSmer", idSmer);
                    cmd.Parameters.AddWithValue("@idProfessor", idProfessor);
                    cmd.Parameters.AddWithValue("@idAssistant", idAssistant);
                    cmd3.Parameters.AddWithValue("@Key", Key);
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("Course Updated");
                    this.databaseConnection.Close();
                    DisplayCourses();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Course");
            }
            else
            {
                try
                {
                    this.databaseConnection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM predmet WHERE idPredmet = @PredmetKey", databaseConnection);
                    cmd.Parameters.AddWithValue("@PredmetKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Predmet Deleted");
                    this.databaseConnection.Close();
                    DisplayCourses();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void selectCourse()
        {
            this.databaseConnection.Open();
            //int idPredmet = SelectId(nameOfId: "idPredmet", nameOfTable: "predmet", nameOfEqualsTo: "Key", valOfEqualsTo: Key);

            if (nazivPredmetTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CoursesDVG.SelectedRows[0].Cells[0].Value.ToString());
            }
            int idPredmet = Key;
            
            string query = "SELECT predaje.idPredavac, predavac.imePredavac, predavac.prezimePredavac, predavac.titula" +
                " FROM predaje, predavac WHERE predaje.idPredmet = @idPredmet AND predaje.idPredavac = predavac.idPredavac";

            MySqlCommand cmd = new MySqlCommand(query, this.databaseConnection);
            cmd.Parameters.AddWithValue("@idPredmet", idPredmet);

            int idProf = 0;
            int idAss = 0;
            string ime = "";
            string prezime = "";
            string titula = "";
            string imeA = "";
            string prezimeA = "";
            MySqlDataReader reader = cmd.ExecuteReader();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                    titula = row[3];
                    if (titula == "Professor")
                    {
                        idProf = Convert.ToInt32(row[0]);
                        ime = row[1];
                        prezime = row[2];
                        Console.WriteLine(idProf);
                        Console.WriteLine(ime);
                        Console.WriteLine(prezime);
                    }
                    else if (titula == "Assistant")
                    {
                        idAss = Convert.ToInt32(row[0]);
                        imeA = row[1];
                        prezimeA = row[2];
                    }

                }
            }

            nazivPredmetTb.Text = CoursesDVG.SelectedRows[0].Cells[1].Value.ToString();
            brEspbTb.Text = CoursesDVG.SelectedRows[0].Cells[2].Value.ToString();
            smerCb.SelectedItem = CoursesDVG.SelectedRows[0].Cells[3].Value.ToString();
            profesorCb.SelectedItem = "id: " + idProf + " " + ime + " " + prezime;
            asistentCb.SelectedItem = "id: " + idAss + " " + imeA + " " + prezimeA;
            cmd.Dispose();
            reader.Close();
            this.databaseConnection.Close();
            
            
        }
        private void CoursesDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectCourse();
            /*
            this.databaseConnection.Open();
            int idPredmet = SelectId(nameOfId: "idPredmet", nameOfTable: "predmet", nameOfEqualsTo: "Key", valOfEqualsTo: Key);
            
            string query = "SELECT predaje.idPredavac, predavac.imePredavac, predavac.prezimePredavac, predavac.titula" +
                " FROM predaje, predavac WHERE predaje.idPredmet = @idPredmet AND predaje.idPredavac = predavac.idPredavac";

            MySqlCommand cmd = new MySqlCommand(query, this.databaseConnection);
            cmd.Parameters.AddWithValue("@idPredmet", idPredmet);

            int idPredavac = 0;
            string ime = "";
            string prezime = "";
            string titula = "";
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            nazivPredmetTb.Text = CoursesDVG.SelectedRows[0].Cells[1].Value.ToString();
            brEspbTb.Text = CoursesDVG.SelectedRows[0].Cells[2].Value.ToString();
            smerCb.SelectedItem = CoursesDVG.SelectedRows[0].Cells[3].Value.ToString();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                    idPredavac = Convert.ToInt32(row[0]);
                    ime = row[1];
                    prezime = row[2];
                    titula = row[3];
                    if (titula == "Professor")
                    {
                        profesorCb.SelectedItem = "id: " + row[0] + " " + row[1] + " " + row[2];
                    }
                    else if (titula == "Assistant")
                    {
                        asistentCb.SelectedItem = "id: " + row[0] + " " + row[1] + " " + row[2];
                    }
                }
            }
            cmd.Dispose();
            reader.Close();
            this.databaseConnection.Close();

            if (nazivPredmetTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CoursesDVG.SelectedRows[0].Cells[0].Value.ToString());
            }*/
        }

        private void ExaminatonBtn_Click(object sender, EventArgs e)
        {
            selectCourse();
            if (Key == 0)
            {
                MessageBox.Show("Select a course to which you'd like to add some type of examination!");
            }
            else if (nazivPredmetTb.Text != "" & brEspbTb.Text != "" & asistentCb.SelectedIndex != -1 & profesorCb.SelectedIndex != -1 & smerCb.SelectedIndex != -1)
            {
                Key = Convert.ToInt32(CoursesDVG.SelectedRows[0].Cells[0].Value.ToString());
                Examination examination = new Examination(this, Key);
                examination.Show();

                this.Hide();
            }
            
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ClearSearchBtn_Click(object sender, EventArgs e)
        {
            Reset();
            DisplayCourses();
        }
    }
}
