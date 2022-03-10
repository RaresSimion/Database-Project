using SomerenLogic;
using SomerenLogic;
using SomerenModel;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        public SomerenUI()
        {
            InitializeComponent();
        }

        private void SomerenUI_Load(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        private void showPanel(string panelName)
        {

            if (panelName == "Dashboard")
            {
                // hide all other panels
                pnlStudents.Hide();
                pnlTeachers.Hide();
                pnlRooms.Hide();

                // show dashboard
                pnlDashboard.Show();
                imgDashboard.Show();
            }
            else if (panelName == "Students")
            {
                // hide all other panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlTeachers.Hide();
                pnlRooms.Hide();

                // show students
                pnlStudents.Show();

                try
                {
                    // fill the students listview within the students panel with a list of students
                    StudentService studService = new StudentService(); ;
                    List<Student> studentList = studService.GetStudents(); ;

                    // clear the listview before filling it again
                    listViewStudents.Items.Clear();

                    foreach (Student s in studentList)
                    {
                        ListViewItem li = new ListViewItem(s.Number.ToString());
                        string[] fullName = s.Name.Split(' ');

                        li.SubItems.Add(fullName[0]);
                        li.SubItems.Add(s.Name.Substring(fullName[0].Length));

                        //if the student number is even change the background color
                        if (s.Number % 2 == 0)
                            li.BackColor = Color.FromArgb(169, 210, 229);

                        listViewStudents.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the students: " + e.Message);
                }
            }
            else if (panelName == "Rooms")
            {
                //hide these panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlTeachers.Hide();

                //show room panel
                pnlRooms.Show();

                try
                {
                    RoomService roomService = new RoomService(); ;
                    List<Room> roomList = roomService.GetRooms(); ; //retrieve room list from the roomservice and store in the variable roomList

                    listViewRoom.Items.Clear();//clear the panel
                    foreach (Room r in roomList)
                    {
                        ListViewItem li = new ListViewItem(r.Number.ToString());// add the room number to the list 
                        li.SubItems.Add(r.Capacity.ToString());// add the capacity to the list 
                        
                        // if room type is true then display teacher else display student 
                        if (r.Type)
                            li.SubItems.Add("Teacher");
                        else
                            li.SubItems.Add("Student");

                        //if the room number is even change the background color
                        if (r.Number % 2 == 0)
                            li.BackColor = Color.FromArgb(169, 210, 229);

                        listViewRoom.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the rooms: " + e.Message);
                }

            }
            else if (panelName == "Lecturers")
            {
                //hide these panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlRooms.Hide();
                //show teachers panel
                pnlTeachers.Show();

                try
                {
                    TeacherService teacherService = new TeacherService(); ;// create connection to the teacher service layer
                    List<Teacher> teacherList = teacherService.GetTeachers(); ;//retrieve list from the service layer and save to the variable teacherList

                    listViewTeachers.Items.Clear();// clear teacher panel
                    foreach (Teacher t in teacherList)
                    {
                        // add these items to the listview
                        ListViewItem li = new ListViewItem(t.Number.ToString());
                        string[] fullName = t.Name.Split(' ');

                        li.SubItems.Add(fullName[0]);
                        li.SubItems.Add(t.Name.Substring(fullName[0].Length));
                        //if teacher is supervisor display yes else no
                        if (t.IsSupervisor)
                            li.SubItems.Add("Yes");
                        else
                            li.SubItems.Add("No");
                        
                        //if the teacher number is even change the background color
                        if (t.Number % 2 == 0)
                            li.BackColor = Color.FromArgb(169, 210, 229);
                        
                        listViewTeachers.Items.Add(li);// add items to listview
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the teachers: " + e.Message);
                }
            }
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void imgDashboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Students");
        }

        private void lecturersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Lecturers");
        }

        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Rooms");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }
    }
}
