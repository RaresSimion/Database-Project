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
                HidePanels();

                // show dashboard
                pnlDashboard.Show();
                imgDashboard.Show();
            }
            else if (panelName == "Students")
            {
                // hide all other panels
                HidePanels();

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
                HidePanels();

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
                HidePanels();
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
                HidePanels();

                //show drinks panel
                pnlDrink.Show();

                try
                {
                    DrinkService drinkService = new DrinkService(); ;// create connection to the drink service layer
                    List<Drink> drinkList = drinkService.GetDrinks(); ;// retrieve list from the drink layer

                    listViewDrink.Items.Clear();// clear drink panel
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
                HidePanels();

                //show cash register panel
                pnlCashRegister.Show();

                try
                {
                    //disable button until student and drink are selected
                    btnCheckOut.Enabled = false;

                    //DRINKS
                    DrinkService drinkService = new DrinkService(); ;// create connection to the drink service layer
                    List<Drink> drinkList = drinkService.GetDrinks(); ;// retrieve drink list

                    listViewCRDrinks.Items.Clear();// clear drink list view
                    listViewCRStudent.Items.Clear();// clear student list view

                    foreach (Drink d in drinkList)
                    {
                        // add items to listviewitem
                        ListViewItem li = new ListViewItem(d.Number.ToString());
                        li.SubItems.Add(d.Name.ToString());
                        li.SubItems.Add(d.Price.ToString());
                        li.SubItems.Add(d.Stock.ToString());

                        //add item to listview
                        listViewCRDrinks.Items.Add(li);

                    }

                    //STUDENTS
                    StudentService studService = new StudentService(); ;// create connection to the student service layer 
                    List<Student> studentList = studService.GetStudents(); ;// retrieve student list

                    foreach (Student s in studentList)
                    {
                        // add these items to the listview
                        ListViewItem l = new ListViewItem(s.Number.ToString());
                        l.SubItems.Add(s.Name);

                        listViewCRStudent.Items.Add(l);
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
                HidePanels();
                //show revenue report panel
                pnlRevenueReport.Show();

                //the max date for the 2 pickers is the present day
                dateTimePickerStart.MaxDate = DateTime.Now;
                dateTimePickerEnd.MaxDate = DateTime.Now;

                //the end date picker is disabled until a start date is chosen
                dateTimePickerEnd.Enabled = false;
                listViewRevenueReport.Items.Clear();
            }
            /*************ACTIVITIES************/
            else if (panelName == "Activities")
            {
                //hide these panels
                HidePanels();

                //show drinks panel
                pnlActivity.Show();

                try
                {
                    ActivityService activityService = new ActivityService(); ;// create connection to the activity service layer
                    List<Activity> activityList = activityService.GetActivity(); ;// retrieve list from the activity layer

                    listViewActivities.Items.Clear();// clear drink panel
                    foreach (Activity a in activityList)
                    {
                        // add these items to the listview
                        ListViewItem li = new ListViewItem(a.Id.ToString());
                        li.SubItems.Add(a.Name);
                        li.SubItems.Add(a.StartDateTime.ToString());
                        li.SubItems.Add(a.EndDateTime.ToString());

                        listViewActivities.Items.Add(li);// add items to listview
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the activities: " + e.Message); //error pop up
                    LogError(e); //error log
                }
            }
            else if (panelName == "Supervisors")
            {
                HidePanels();

                pnlSupervisors.Show();

                try
                {
                    ActivityService activityService = new ActivityService(); ;
                    List<Activity> activityList = activityService.GetActivity(); ;

                    listViewSupervisorActivities.Items.Clear();
                    listViewSupervisors.Items.Clear();
                    listViewNotSupervisors.Items.Clear();

                    foreach (Activity a in activityList)
                    {
                        // add these items to the listview
                        ListViewItem li = new ListViewItem(a.Id.ToString());
                        li.SubItems.Add(a.Name);
                        li.SubItems.Add(a.StartDateTime.ToString("yyyy-MM-dd    HH:mm"));
                        li.SubItems.Add(a.EndDateTime.ToString("yyyy-MM-dd    HH:mm"));

                        listViewSupervisorActivities.Items.Add(li);// add items to listview
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the activities: " + e.Message); //error pop up
                    LogError(e); //error log
                }
            }

        }
        //method to log the errors to file
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
        private void activitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Activities");
        }

        private void CashRegtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Cash Register");
        }

        private void RevenueReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Revenue Report");
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEnd.Enabled = true; //once a start date has been chosen, the end date picker is enabled
            dateTimePickerEnd.MinDate = dateTimePickerStart.Value; //the minimum date for the end date picker becomes the value of the start date picker, so that the user can only choose valid dates
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //get the revenue report for the selected date
                RevenueReportService revenueService = new RevenueReportService();
                RevenueReport revenueReport = revenueService.GetReport(dateTimePickerStart.Value, dateTimePickerEnd.Value);

                //remove the report from before and add the new one
                listViewRevenueReport.Items.Clear();
                ListViewItem li = new ListViewItem(revenueReport.NumberOfDrinks.ToString());
                li.SubItems.Add($"€{revenueReport.Turnover}");
                li.SubItems.Add(revenueReport.NumberOfCustomers.ToString());
                listViewRevenueReport.Items.Add(li);
            }
            catch (Exception ex)
            {
                listViewRevenueReport.Items.Clear();
                MessageBox.Show("Something went wrong while loading the revenue report: " + ex.Message); //error pop up
                LogError(ex);
            }
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
                //Student student = (Student)listViewCRStudent.SelectedItems[0].Tag;

                txtBStudentID.Text = listViewCRStudent.SelectedItems[0].SubItems[0].Text;
                txtBStudentName.Text = listViewCRStudent.SelectedItems[0].SubItems[1].Text;
            }
            if (txtBDrinkName.Text != "" && txtBDrinkPrice.Text != "" && txtBStudentID.Text != "" && txtBStudentName.Text != "")
            {
                btnCheckOut.Enabled = true;
                btnCheckOut.BackColor = Color.FromArgb(39, 126, 172);
            }
        }

        private void listViewCRDrinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if nothing is selected the textbox remains empty
            if (listViewCRDrinks.SelectedItems.Count == 0)
            {
                txtBDrinkName.Text = "";
                txtBDrinkPrice.Text = "";
            }
            else
            {
                //if the txtbox is not empty fill it with the details of the selected drink
                txtBDrinkName.Text = listViewCRDrinks.SelectedItems[0].SubItems[1].Text;
                txtBDrinkPrice.Text = $"€{listViewCRDrinks.SelectedItems[0].SubItems[2].Text}";
            }
            // if the textbox of the drinks and students arent empty enable the button and change the color 
            if (txtBDrinkName.Text != "" && txtBDrinkPrice.Text != "" && txtBStudentID.Text != "" && txtBStudentName.Text != "")
            {
                btnCheckOut.Enabled = true;
                btnCheckOut.BackColor = Color.FromArgb(39, 126, 172);
            }
        }
        /****************ACTIVITY LIST VIEW***************/
        private void listViewActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewActivities.SelectedItems.Count == 0)
            {
                txtActivityID.Text = "";
                txtActivityDesc.Text = "";
            }
            else
            {
                //if the txtbox is not empty fill it with the details of the selected activities
                txtActivityID.Text = listViewActivities.SelectedItems[0].SubItems[0].Text;
                txtActivityDesc.Text = listViewActivities.SelectedItems[0].SubItems[1].Text;
                dateTimePIcker_ActivityStart.Value = Convert.ToDateTime(listViewActivities.SelectedItems[0].SubItems[2].Text);
                dateTimePicker_ActivityEnd.Value = Convert.ToDateTime(listViewActivities.SelectedItems[0].SubItems[3].Text);

            }
            // if the textbox of the activities arent empty enable the button 
            if (txtActivityID.Text != "" && txtActivityDesc.Text != "" && dateTimePIcker_ActivityStart.Value != null && dateTimePicker_ActivityEnd.Value != null)
            {
                btn_addActivity.Enabled = true;
                btn_removeActivity.Enabled = true;
                btn_updateActivity.Enabled = true;
            }
        }
        /********************CASH REGISTER BUTTONS***************************/
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            //get text from textboxes for student id and drink id
            int studentNumber = int.Parse(txtBStudentID.Text);
            int drinkNumber = int.Parse(listViewCRDrinks.SelectedItems[0].SubItems[0].Text);
            DateTime orderDate = DateTime.Now;

            //create connection to register database
            CashRegisterService registerService = new CashRegisterService();
            //add the data to the rgister database
            registerService.AddToRegister(studentNumber, drinkNumber, orderDate);

            //create connection to drink database
            DrinkService drinkService = new DrinkService();

            //update drinks
            drinkService.UpdateDrink(drinkNumber);

            //show that the order was successful
            MessageBox.Show("Order succesful!");

            //refresh the panel
            showPanel("Cash Register");
        }

        /*****************ACTIVITY BUTTONS****************/

        //create connection to register database
        ActivityService activityService = new ActivityService();

        private void btn_updateActivity_Click(object sender, EventArgs e)
        {
            //create activity object
            Activity activity = new Activity();
            {
                activity.Id = int.Parse(txtActivityID.Text);
                activity.Name = txtActivityDesc.Text;
                activity.StartDateTime = DateTime.Parse(dateTimePIcker_ActivityStart.Text);
                activity.EndDateTime = DateTime.Parse(dateTimePicker_ActivityEnd.Text);
            };

            //add the data to the register database
            activityService.UpdateActivity(activity);
            //refresh panel
            showPanel("Activities");
        }
        private void btn_addActivity_Click(object sender, EventArgs e)
        {
            //create activity object
            Activity activity = new Activity();
            {
                activity.Id = int.Parse(txtActivityID.Text);
                activity.Name = txtActivityDesc.Text;
                activity.StartDateTime = DateTime.Parse(dateTimePIcker_ActivityStart.Text);
                activity.EndDateTime = DateTime.Parse(dateTimePicker_ActivityEnd.Text);
            };
            //add the data to the activity database
            activityService.AddToActivity(activity);
            //show that the add was successful
            MessageBox.Show("Succeesfully added actiivty!");

            //refresh the panel
            showPanel("Activities");
        }
        private void btn_removeActivity_Click(object sender, EventArgs e)
        {
            //create activity object
            Activity activity = new Activity();
            {
                activity.Id = int.Parse(txtActivityID.Text);
                activity.Name = txtActivityDesc.Text;
                activity.StartDateTime = DateTime.Parse(dateTimePIcker_ActivityStart.Text);
                activity.EndDateTime = DateTime.Parse(dateTimePicker_ActivityEnd.Text);
            };

            //delete activity
            activityService.DeleteActivity(activity);
            // show that delete was successfull
            MessageBox.Show("Succeesfully deleted actiivty!");
            //refresh panel
            showPanel("Activities");
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }
        public void HidePanels()
        {
            // hide all other panels

            pnlDashboard.Hide();
            imgDashboard.Hide();
            pnlStudents.Hide();
            pnlTeachers.Hide();
            pnlRooms.Hide();
            pnlDrink.Hide();
            pnlCashRegister.Hide();
            pnlRevenueReport.Hide();
            pnlActivity.Hide();
        }

    }
}
