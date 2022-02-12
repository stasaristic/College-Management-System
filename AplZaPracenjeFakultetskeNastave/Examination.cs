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
    public partial class Examination : Form
    {
        Courses courses;
        int idPredmeta;
        public Examination(Courses courses, int idPredmeta)
        {
            InitializeComponent();
            this.courses = courses;
            this.idPredmeta = idPredmeta;
            DisplayExams();
        }
        string Key;
        private void Reset()
        {
            Key = "";
            osvojeniPoeniTb.Text = "";
            ocenaTb.Text = "";
            studentCb.SelectedIndex = -1;

        }
        static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=bp_2022_projekat";
        MySqlConnection databaseConnection = new MySqlConnection(Examination.MySQLConnectionString);

        bool varify()
        {
            if (label_gradingProf.Text == "" || label_courseGraded.Text == "" || osvojeniPoeniTb.Text == "" || ocenaTb.Text == "")
            {
                return true;
            }
            else { return false; }
        }
        private void DisplayExams()
        {
            this.databaseConnection.Open();
            string query = "Select ispit.idPredavac, ispit.idStudent, ispit.idPredmet, student.imeStudent, student.prezimeStudent, predavac.imePredavac, predavac.prezimePredavac, predmet.nazivPredmet, ispit.datum, ispit.ocena, ispit.osvojeniPoeni  FROM student, predavac, predmet, ispit WHERE student.idStudent = ispit.idStudent AND predavac.idPredavac = ispit.idPredavac AND predmet.idPredmet=ispit.idPredmet";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, databaseConnection);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExamsDVG.DataSource = ds.Tables[0];
            //this.databaseConnection.Close();
            //this.databaseConnection.Open();
            /*
            MySqlDataAdapter sda2 = new MySqlDataAdapter(searchQuery, databaseConnection);
            MySqlCommandBuilder Builder2 = new MySqlCommandBuilder(sda2);
            var ds2 = new DataSet();
            sda2.Fill(ds2);
            StudentsDVGSearch.DataSource = ds2.Tables[0];*/
            this.databaseConnection.Close();

        }
        private void profesorCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            Courses courses = new Courses();
            courses.Show();
            this.Hide();
        }

        private void Examination_Load(object sender, EventArgs e)
        {

            /*  Professor Grading   */
            string query = "SELECT predmet.idPredmet, predavac.idPredavac,predavac.imePredavac, predavac.prezimePredavac, predaje.idPredmet, predaje.idPredavac FROM predmet, predavac, predaje WHERE predmet.idPredmet = @Key AND predmet.idPredmet = predaje.idPredmet AND predavac.idPredavac = predaje.idPredavac AND predavac.titula = 'Professor'";

            MySqlCommand cmd = new MySqlCommand(query, this.databaseConnection);
            cmd.Parameters.AddWithValue("@Key", idPredmeta);

            MySqlDataReader reader;
            try
            {
                this.databaseConnection.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        string[] row = { reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                        // adding id in front of the name in case there is two predavac that go by the same name
                        // this will later help me get the correct values for predavac
                        string imePrezimePredavaca = "id: " + row[0] + " " + row[1] + " " + row[2];
                        label_gradingProf.Text = imePrezimePredavaca;
                    }
                    cmd.Dispose();
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

            /*      Students    */

            query = "SELECT predmet.idPredmet, student.idStudent,student.imeStudent,student.prezimeStudent, slusa.idPredmet, slusa.idStudent FROM predmet, student, slusa WHERE predmet.idPredmet=@Key AND predmet.idPredmet = slusa.idPredmet AND student.idStudent = slusa.idStudent";

            cmd = new MySqlCommand(query, this.databaseConnection);
            cmd.Parameters.AddWithValue("@Key", idPredmeta);

            try
            {
                this.databaseConnection.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        string[] row = { reader.GetString(1), reader.GetString(2), reader.GetString(3) };

                        string imePrezimeStudenta = "id: " + row[0] + " " + row[1] + " " + row[2];
                        studentCb.Items.Add(imePrezimeStudenta);
                    }
                    cmd.Dispose();
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

            /*      Course Graded    */

            query = "SELECT predmet.idPredmet, predmet.nazivPredmet FROM predmet WHERE predmet.idPredmet=@Key";

            cmd = new MySqlCommand(query, this.databaseConnection);
            cmd.Parameters.AddWithValue("@Key", idPredmeta);

            try
            {
                this.databaseConnection.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        string[] row = { reader.GetString(0), reader.GetString(1) };

                        label_courseGraded.Text = "id: " + row[0] + " " + row[1];
                    }
                    cmd.Dispose();
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
            

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (varify() == true)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                // inserting
                try
                {
                    this.databaseConnection.Open();
                  
                    string[] idPredavacProf = label_gradingProf.Text.Split(' ');
                    int idPredavac = Convert.ToInt32(idPredavacProf[1]);
                    string[] idCourseLabel = label_courseGraded.Text.Split(' ');
                    int idCourse = Convert.ToInt32(idCourseLabel[1]);
                    //Console.WriteLine(datumDtp.Value.Date);
                    // catching idS from profesorCb
                    string query = "SELECT brEspb FROM predmet WHERE idPredmet = @idPredmet";

                    MySqlCommand cmd = new MySqlCommand(query, this.databaseConnection);
                    
                    cmd.Parameters.AddWithValue("@idPredmet", idPredmeta);

                    int brEspb = 0;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            brEspb = reader.GetInt32(0);
                        }
                    }
                    reader.Close();
                    Console.WriteLine(value: brEspb);

                    query = "SELECT idStudent, ostvareniEspb FROM student WHERE idStudent = @idStudent";
                    int ostvareniEspb = 0;
                    cmd = new MySqlCommand(query, this.databaseConnection);
                    string[] idStudentCb = studentCb.SelectedItem.ToString().Split(' ');
                    cmd.Parameters.AddWithValue("@idStudent", idStudentCb[1]);

                    int idStudent = 0;
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idStudent = reader.GetInt32(0);
                            ostvareniEspb =reader.GetInt32(1);
                        }
                    }
                    reader.Close();
                    Console.WriteLine(value: idStudent);

                    // inserting to ispit table
                    query = "INSERT INTO `ispit` (idPredavac,idStudent,idPredmet,datum,ocena,osvojeniPoeni) values (@idPredavac,@idStudent,@idPredmet,@datum,@ocena,@osvojeniPoeni)";
                    cmd = new MySqlCommand(query, this.databaseConnection);
                    cmd.Parameters.AddWithValue("@idPredavac", idPredavac);
                    cmd.Parameters.AddWithValue("@idStudent", idStudent);
                    cmd.Parameters.AddWithValue("@idPredmet", idPredmeta);
                    cmd.Parameters.AddWithValue("@datum", datumDtp.Value.Date);
                    
;
                    if (Convert.ToInt32(osvojeniPoeniTb.Text) < 51 & Convert.ToInt32(ocenaTb.Text) != 5)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else if (Convert.ToInt32(osvojeniPoeniTb.Text) >= 51 & Convert.ToInt32(osvojeniPoeniTb.Text) < 61 & Convert.ToInt32(ocenaTb.Text) != 6)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else if (Convert.ToInt32(osvojeniPoeniTb.Text) >= 61 & Convert.ToInt32(osvojeniPoeniTb.Text) < 71 & Convert.ToInt32(ocenaTb.Text) != 7)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else if (Convert.ToInt32(osvojeniPoeniTb.Text) >= 71 & Convert.ToInt32(osvojeniPoeniTb.Text) < 81 & Convert.ToInt32(ocenaTb.Text) != 8)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else if (Convert.ToInt32(osvojeniPoeniTb.Text) >= 81 & Convert.ToInt32(osvojeniPoeniTb.Text) < 91 & Convert.ToInt32(ocenaTb.Text) != 9)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else if (Convert.ToInt32(osvojeniPoeniTb.Text) >= 91 & Convert.ToInt32(osvojeniPoeniTb.Text) <= 100 & Convert.ToInt32(ocenaTb.Text) != 10)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else 
                    {
                        cmd.Parameters.AddWithValue("@ocena", Convert.ToInt32(ocenaTb.Text));
                        cmd.Parameters.AddWithValue("@osvojeniPoeni", Convert.ToInt32(osvojeniPoeniTb.Text));
                    }

                    cmd.ExecuteNonQuery();

                    ostvareniEspb += brEspb;
                    query = "UPDATE student SET ostvareniEspb = @ostvareniEspb WHERE idStudent= @idStudent";
                    cmd = new MySqlCommand(query, this.databaseConnection);
                    cmd.Parameters.AddWithValue("@idStudent", idStudent);                  
                    cmd.Parameters.AddWithValue("@ostvareniEspb", ostvareniEspb);
                    cmd.ExecuteNonQuery();

                    this.databaseConnection.Close();


                    DisplayExams();
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
            if (Key == "")
            {
                MessageBox.Show("Select Exam");
            }
            else
            {
                try
                {
                    this.databaseConnection.Open();
                    string[] KeyId = Key.Split(' ');

                    int keyIdPredmet = Convert.ToInt32(KeyId[0]);
                    int keyIdStudent = Convert.ToInt32(KeyId[1]);
                    int keyIdPredavac = Convert.ToInt32(KeyId[2]);
                    //MessageBox.Show(Key.ToString());
                    //MessageBox.Show(keyIdPredmet.ToString() + " " + keyIdStudent.ToString() + " "+ keyIdPredavac.ToString());
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM ispit WHERE idPredavac = @keyIdPredavac AND idStudent = @keyIdStudent AND idPredmet = @keyIdPredmet", databaseConnection);
                    cmd.Parameters.AddWithValue("@keyIdPredavac", keyIdPredavac);
                    cmd.Parameters.AddWithValue("@keyIdStudent", keyIdStudent);
                    cmd.Parameters.AddWithValue("@keyIdPredmet", keyIdPredmet);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ispit Deleted");
                    this.databaseConnection.Close();
                    DisplayExams();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }

        }

        private void moduleCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (varify() == true)
            {
                MessageBox.Show("Missing information!");
            }
            else 
            {
                try
                {
                    this.databaseConnection.Open();

                    string[] idPredavacProf = label_gradingProf.Text.Split(' ');
                    int idPredavac = Convert.ToInt32(idPredavacProf[1]);
                    string[] idCourseLabel = label_courseGraded.Text.Split(' ');
                    int idCourse = Convert.ToInt32(idCourseLabel[1]);

                    // catching idStudent from studentCb

                    string query = "SELECT idStudent FROM student WHERE idStudent = @idStudent";

                    MySqlCommand cmd = new MySqlCommand(query, this.databaseConnection);
                    string[] idStudentCb = studentCb.SelectedItem.ToString().Split(' ');
                    cmd.Parameters.AddWithValue("@idStudent", idStudentCb[1]);

                    int idStudent = 0;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idStudent = reader.GetInt32(0);
                        }
                    }
                    reader.Close();
                    Console.WriteLine(value: idStudent);

                    // key = 2_3_11
                    string[] KeyId = Key.Split(' ');

                    int keyIdPredmet = Convert.ToInt32(KeyId[0]);
                    int keyIdStudent = Convert.ToInt32(KeyId[1]);
                    int keyIdPredavac = Convert.ToInt32(KeyId[2]);

                    if (idStudent != keyIdStudent)
                    {
                       // MessageBox.Show(idStudent+ " " +keyIdStudent);
                        //MessageBox.Show(Key.ToString());
                        MessageBox.Show("You can't change the student that was graded");
                    }
                    // inserting to ispit table
                    query = "UPDATE ispit SET datum=@datum, ocena=@ocena, osvojeniPoeni=@osvojeniPoeni where ispit.idPredmet = @keyIdPredmet AND ispit.idPredavac = @keyIdPredavac AND ispit.idStudent = @keyIdStudent";
                    cmd = new MySqlCommand(query, this.databaseConnection);
                    cmd.Parameters.AddWithValue("@keyIdPredavac", keyIdPredavac);
                    cmd.Parameters.AddWithValue("@keyIdStudent", keyIdStudent);
                    cmd.Parameters.AddWithValue("@keyIdPredmet", keyIdPredmet);
                    cmd.Parameters.AddWithValue("@datum", datumDtp.Value.Date);

                    ;
                    if (Convert.ToInt32(osvojeniPoeniTb.Text) < 51 & Convert.ToInt32(ocenaTb.Text) != 5)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else if (Convert.ToInt32(osvojeniPoeniTb.Text) >= 51 & Convert.ToInt32(osvojeniPoeniTb.Text) < 61 & Convert.ToInt32(ocenaTb.Text) != 6)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else if (Convert.ToInt32(osvojeniPoeniTb.Text) >= 61 & Convert.ToInt32(osvojeniPoeniTb.Text) < 71 & Convert.ToInt32(ocenaTb.Text) != 7)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else if (Convert.ToInt32(osvojeniPoeniTb.Text) >= 71 & Convert.ToInt32(osvojeniPoeniTb.Text) < 81 & Convert.ToInt32(ocenaTb.Text) != 8)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else if (Convert.ToInt32(osvojeniPoeniTb.Text) >= 81 & Convert.ToInt32(osvojeniPoeniTb.Text) < 91 & Convert.ToInt32(ocenaTb.Text) != 9)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else if (Convert.ToInt32(osvojeniPoeniTb.Text) >= 91 & Convert.ToInt32(osvojeniPoeniTb.Text) <= 100 & Convert.ToInt32(ocenaTb.Text) != 10)
                    {
                        MessageBox.Show("Wrong grade entered!\nTry again!");
                        this.databaseConnection.Close();
                        return;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ocena", Convert.ToInt32(ocenaTb.Text));
                        cmd.Parameters.AddWithValue("@osvojeniPoeni", Convert.ToInt32(osvojeniPoeniTb.Text));
                    }

                    cmd.ExecuteNonQuery();

                    this.databaseConnection.Close();
                    idStudent = 0;

                    DisplayExams();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    this.databaseConnection.Close();
                }
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void ExamsDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idPredavac = Convert.ToInt32(ExamsDVG.SelectedRows[0].Cells[0].Value.ToString());
            int idStudent = Convert.ToInt32(ExamsDVG.SelectedRows[0].Cells[1].Value.ToString());
            int idPredmet = Convert.ToInt32(ExamsDVG.SelectedRows[0].Cells[2].Value.ToString());
            string imeStudent = ExamsDVG.SelectedRows[0].Cells[3].Value.ToString();
            string prezimeStudent = ExamsDVG.SelectedRows[0].Cells[4].Value.ToString();
            string imePredavac = ExamsDVG.SelectedRows[0].Cells[5].Value.ToString();
            string prezimePredavac = ExamsDVG.SelectedRows[0].Cells[6].Value.ToString();
            string nazivPredmet = ExamsDVG.SelectedRows[0].Cells[7].Value.ToString();
            label_gradingProf.Text = "id: " + idPredavac.ToString() + " " + imePredavac + " " + prezimePredavac;
            label_courseGraded.Text = "id: " + idPredmet.ToString() + " " + nazivPredmet;
            studentCb.SelectedItem = "id: " + idStudent.ToString() + " " + imeStudent + " " + prezimeStudent;
            datumDtp.Text = ExamsDVG.SelectedRows[0].Cells[8].Value.ToString();
            ocenaTb.Text = ExamsDVG.SelectedRows[0].Cells[9].Value.ToString();
            osvojeniPoeniTb.Text = ExamsDVG.SelectedRows[0].Cells[10].Value.ToString();

            if (ocenaTb.Text == "")
            {
                Key = "";
            }
            else
            {
                Key = (ExamsDVG.SelectedRows[0].Cells[0].Value.ToString() + " "+ ExamsDVG.SelectedRows[0].Cells[1].Value.ToString()+ " " + ExamsDVG.SelectedRows[0].Cells[2].Value.ToString());
                Console.WriteLine(Key);
                
            }
        }


    }
}
