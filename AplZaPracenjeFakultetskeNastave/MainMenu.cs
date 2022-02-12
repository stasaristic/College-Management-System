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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        static string MySQLConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=bp_2022_projekat";
        MySqlConnection databaseConnection = new MySqlConnection(MainMenu.MySQLConnectionString);
        private void ModulesLbl_Click(object sender, EventArgs e)
        {
            Modules modulesForm = new Modules();
            modulesForm.Show();
            this.Hide();
        }

        private void ExitPicturePanel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ModulesPb_Click(object sender, EventArgs e)
        {
            Modules modulesForm = new Modules();
            modulesForm.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Students studentsForm = new Students();
            studentsForm.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Students studentsForm = new Students();
            studentsForm.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Courses coursesForm = new Courses();
            coursesForm.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Courses coursesForm = new Courses();
            coursesForm.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Teachers teachersForm = new Teachers();
            teachersForm.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Teachers teachersForm = new Teachers();
            teachersForm.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        public string dataCount(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, this.databaseConnection);
            this.databaseConnection.Open();
            string count = cmd.ExecuteScalar().ToString();
            this.databaseConnection.Close();
            return count;
        }

        public string totalStudent()
        {
            return dataCount("SELECT COUNT(*) FROM student");
        }
        public string totalCourses()
        {
            return dataCount("SELECT COUNT(*) FROM predmet");
        }
        public string totalModules()
        {
            return dataCount("SELECT COUNT(*) FROM smer");
        }
        public string totalTeachersProfessors()
        {
            return dataCount("SELECT COUNT(*) FROM predavac WHERE `titula`='Professor'");
        }
        public string totalTeachersAssistants()
        {
            return dataCount("SELECT COUNT(*) FROM predavac WHERE `titula`='Assistant'");
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            // Display values
            label_totalStudents.Text = "Total students: " + totalStudent();
            label_totalCourses.Text = "Total courses: " + totalCourses();
            label_totalModules.Text = "Total modules: " + totalModules();
            label_Assistants.Text = "Assistants: " + totalTeachersAssistants();
            label_Professors.Text = "Professors: " + totalTeachersProfessors();
        }

        private void label_hello_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
