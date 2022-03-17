﻿using SomerenLogic;
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
using System.IO;
using System.Diagnostics;

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
                pnlDrink.Hide();
                pnlCashRegister.Hide();
                pnlRevenueReport.Hide();


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
                pnlDrink.Hide();
                pnlCashRegister.Hide();
                pnlRevenueReport.Hide();

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
                    MessageBox.Show("Something went wrong while loading the students: " + e.Message); //error pop up
                    LogError(e); //error log
                }
            }
            else if (panelName == "Rooms")
            {
                //hide these panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlTeachers.Hide();
                pnlDrink.Hide();
                pnlCashRegister.Hide();
                pnlRevenueReport.Hide();

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
                    MessageBox.Show("Something went wrong while loading the rooms: " + e.Message); //error pop up
                    LogError(e); //error log
                }

            }
            else if (panelName == "Lecturers")
            {
                //hide these panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlRooms.Hide();
                pnlDrink.Hide();
                pnlCashRegister.Hide();
                pnlRevenueReport.Hide();
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
                    MessageBox.Show("Something went wrong while loading the teachers: " + e.Message); //error pop up
                    LogError(e); //error log
                }
            }
            else if (panelName == "Drinks")
            {
                //hide these panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlRooms.Hide();
                pnlTeachers.Hide();
                pnlCashRegister.Hide();
                pnlRevenueReport.Hide();
                //show drinks panel

                pnlDrink.Show();

                try
                {
                    DrinkService drinkService = new DrinkService(); ;// create connection to the drink service layer
                    List<Drink> drinkList = drinkService.GetDrinks(); ;//retrieve list from the drink layer and save to the variable teacherList

                    listViewDrink.Items.Clear();// clear teacher panel
                    foreach (Drink d in drinkList)
                    {
                        // add these items to the listview
                        ListViewItem li = new ListViewItem(d.Number.ToString());


                        li.SubItems.Add(d.Name);
                        //if drink is alcoholic display yes else no
                        if (d.IsAlcoholic)
                            li.SubItems.Add("Yes");
                        else
                            li.SubItems.Add("No");
                        li.SubItems.Add(d.Price.ToString());
                        li.SubItems.Add(d.Stock.ToString());


                        //if the drink number is even change the background color
                        if (d.Number % 2 == 0)
                            li.BackColor = Color.FromArgb(169, 210, 229);

                        listViewDrink.Items.Add(li);// add items to listview
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the drinks: " + e.Message); //error pop up
                    LogError(e); //error log
                }
            }
            else if (panelName == "Cash Register")
            {
                //hide these panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlRooms.Hide();
                pnlTeachers.Hide();
                pnlDrink.Hide();
                pnlRevenueReport.Hide();
                //show drinks panel

                pnlCashRegister.Show();

                try
                {
                    /**************DRINKS**************/

                    DrinkService drinkService = new DrinkService(); ;// create connection to the drink service layer
                    List<Drink> drinkList = drinkService.GetDrinks(); ;//retrieve list from the drink layer and save to the variable teacherList

                    listViewCRDrinks.Items.Clear();// clear drink panel
                    listViewCRStudent.Items.Clear();// clear list view in student 
                    foreach (Drink d in drinkList)
                    {
                        // add these items to the listview
                        ListViewItem li = new ListViewItem(d.Number.ToString());
                        li.SubItems.Add(d.Name.ToString());
                        li.SubItems.Add(d.Price.ToString());
                        li.SubItems.Add(d.Stock.ToString());


                        //if the drink number is even change the background color
                        if (d.Number % 2 == 0)
                            li.BackColor = Color.FromArgb(169, 210, 229);

                        listViewCRDrinks.Items.Add(li);// add items to listview
                    }

                    /**************STUDENTS**************/
                    // fill the students listview within the students panel with a list of students
                    StudentService studService = new StudentService(); ;
                    List<Student> studentList = studService.GetStudents(); ;

                    // clear the listview before filling it again

                    foreach (Student s in studentList)
                    {
                        ListViewItem l = new ListViewItem(s.Number.ToString());
                        l.SubItems.Add(s.Name.ToString());
                        //if the student number is even change the background color
                        if (s.Number % 2 == 0)
                            l.BackColor = Color.FromArgb(169, 210, 229);
                        listViewCRStudent.Items.Add(l);
                        //listBoxStudents.Items.Add();



                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the cash register: " + e.Message); //error pop up
                    LogError(e); //error log
                }
            }
            else if (panelName == "Revenue Report")
            {
                //hide these panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlRooms.Hide();
                pnlTeachers.Hide();
                pnlDrink.Hide();
                pnlCashRegister.Hide();

                //show revenue report panel
                pnlRevenueReport.Show();

                try
                {

                    dateTimePickerStart.MaxDate = DateTime.Now;
                    dateTimePickerEnd.MaxDate = DateTime.Now;

                    dateTimePickerEnd.Enabled = false;
                    listViewRevenueReport.Items.Clear();

                    //RevenueReportService revenueService = new RevenueReportService();
                    //RevenueReport revenueReport = revenueService.GetReport();

                    // lblSales.Text = $"Sales: {revenueReport.NumberOfDrinks}";
                    //lblTurnover.Text = $"Ttal profit: {revenueReport.Turnover:0.00}";
                    //lblNumberOfCustomers.Text = $"Number of customers: {revenueReport.NumberOfCustomers}";

                    //DateTime miau = dateTimePickerStart.Value;
                    //MessageBox.Show($"{miau:yyyy-MM-dd}");
                }

                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the revenue report: " + e.Message); //error pop up
                    LogError(e); //error log
                }
            }
        }

        private void LogError(Exception ex)
        {
            string message = string.Format($"Time: {DateTime.Now:dd/MM/yyyy hh:mm:ss tt}");
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format($"Message: {ex.Message}");
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = "../../../ErrorLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
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

        private void drinktoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Drinks");
        }


        private void CashRegtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Cash Register"); }

        private void RevenueReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Revenue Report");
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEnd.Enabled = true;
            dateTimePickerEnd.MinDate = dateTimePickerStart.Value;
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            RevenueReportService revenueService = new RevenueReportService();
            RevenueReport revenueReport = revenueService.GetReport(dateTimePickerStart.Value, dateTimePickerEnd.Value);

            listViewRevenueReport.Items.Clear();
            ListViewItem li = new ListViewItem(revenueReport.NumberOfDrinks.ToString());
            li.SubItems.Add($"{revenueReport.Turnover}$");
            li.SubItems.Add(revenueReport.NumberOfCustomers.ToString());
            listViewRevenueReport.Items.Add(li);
        }

        private void listViewCRStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewCRStudent.SelectedItems.Count == 0)
            {
                txtBStudentID.Text = "";
                txtBStudentName.Text = "";
            }
            else
            {
               Student student = (Student)listViewCRStudent.SelectedItems[0].Tag;

                txtBStudentID.Text = listViewCRStudent.SelectedItems[0].SubItems[0].Text;
                txtBStudentName.Text = listViewCRStudent.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void listViewCRDrinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewCRDrinks.SelectedItems.Count == 0)
            {
                txtBDrinkName.Text = "";
                txtBDrinkPrice.Text = "";
            }
            else
            {
                Drink drink = (Drink)listViewCRDrinks.SelectedItems[0].Tag;

                txtBDrinkName.Text = listViewCRDrinks.SelectedItems[0].SubItems[1].Text;
                txtBDrinkPrice.Text = listViewCRDrinks.SelectedItems[0].SubItems[2].Text;
            }
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            List<int> drinkReserved = new List<int>();
            int selectedStudent = int.Parse(listViewCRStudent.SelectedItems[0].SubItems[0].Text);


            for (int i = 0; i < listViewDrink.Items.Count; i++)
            {
                if (listViewCRDrinks.Items[i].Checked)
                {
                    drinkReserved.Add(int.Parse(listViewDrink.Items[i].SubItems[0].Text));
                }
            }

            CashRegisterService registerService = new CashRegisterService();
            foreach (int DrinkID in drinkReserved)
            {
                registerService.AddRegister(selectedStudent, DrinkID);
            }
            showPanel("Register");

        }
    }
}
