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
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        //store the current user in a variable
        private User currentUser = null;

        public SomerenUI()
        {
            InitializeComponent();
        }
        //Show dashboard panel
        private void SomerenUI_Load(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        private void showPanel(string panelName)
        {
            #region Dashboard
            /***************DASHBOARD PANEL************************/
            if (panelName == "Dashboard")
            {
                HidePanels();

                // show dashboard
                pnlDashboard.Show();
                imgDashboard.Show();

                //clear the text
                txtLoginPassword.Text = "";
                txtLoginUsername.Text = "";

                //disable the button and make it transparent
                btn_Login.Enabled = false;
                btn_Login.BackColor = Color.Transparent;

                //color the register button to make it stand out
                btnRegister.BackColor = Color.FromArgb(39, 126, 172);

                //change the program depending on its user
                ProgramAdministration();
            }
            #endregion

            #region Students panel
            /***************STUDENTS PANEL************************/
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
            #endregion

            #region Rooms panel
            /***************ROOMS PANEL************************/
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
            #endregion

            #region Lecturers panel
            /***************LECTURERS PANEL************************/
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
            #endregion

            #region Drinks panel
            /***************DRINKS PANEL************************/
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
            #endregion

            #region Cash register panel
            /***************CASH REGISTER PANEL************************/
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
                    btnCheckOut.BackColor = Color.Transparent;

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
            #endregion

            #region Revenue panel
            /***************REVENUE REPORT PANEL************************/
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
            #endregion

            #region Activities Panel
            /*************ACTIVITIES PANEL************/
            else if (panelName == "Activities")
            {
                //hide these panels
                HidePanels();

                //show drinks panel
                pnlActivity.Show();

                //disabble the buttons
                btn_addActivity.Enabled = false;
                btn_addActivity.BackColor = Color.Transparent;
                btn_removeActivity.Enabled = false;
                btn_removeActivity.BackColor = Color.Transparent;
                btn_updateActivity.Enabled = false;
                btn_updateActivity.BackColor = Color.Transparent;

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
            #endregion

            #region Supervisor Panel
            /***************SUPERVISOR PANEL************************/
            else if (panelName == "Supervisors")
            {
                HidePanels();

                pnlSupervisors.Show();

                //disable the buttons and make them gray until an activity is selected
                btnAddSupervisor.Enabled = false;
                btnRemoveSupervisor.Enabled = false;
                btnAddSupervisor.BackColor = Color.Transparent;
                btnRemoveSupervisor.BackColor = Color.Transparent;

                try
                {
                    //create connection to activity layer
                    ActivityService activityService = new ActivityService(); ;

                    //retrieve activities
                    List<Activity> activityList = activityService.GetActivity(); ;

                    //clear lists
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

                        listViewSupervisorActivities.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the activities: " + e.Message); //error pop up
                    LogError(e); //error log
                }
            }
            #endregion

            #region Register Panel
            else if (panelName == "Register")
            {
                //hide the panels
                HidePanels();

                //show the register panel
                pnlRegister.Show();

                //diable the button and make it transparent
                btnRegisterNow.Enabled = false;
                btnRegisterNow.BackColor = Color.Transparent;
                
                //disable all fields except licence key
                txtUserName.Enabled = false;
                txtName.Enabled = false;
                txtPassword.Enabled = false;

                //color the back to login button to make it stand out
                btnGoBackToLogin.BackColor = Color.FromArgb(39, 126, 172);
            }
            #endregion
        }



        #region Revenue Listview and buttons
        /********************REVENUE LISTVIEW & PANEL***************************/
        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEnd.Enabled = true; //once a start date has been chosen, the end date picker is enabled
            dateTimePickerEnd.MinDate = dateTimePickerStart.Value; //the minimum date for the end date picker becomes the value of the start date picker, so that the user can only choose valid dates
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
        #endregion

        #region Cash Register Listviews and buttons 
        /********************CASH REGISTER LISTVIEWS***************************/
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
        #endregion

        #region Activity listview and buttons 
        /****************ACTIVITY LIST VIEW***************/
        private void listViewActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewActivities.SelectedItems.Count == 0)
            {
                txtActivityDesc.Text = "";
                btn_removeActivity.Enabled = false;
                btn_updateActivity.Enabled = false;
            }
            else
            {
                //if the txtbox is not empty fill it with the details of the selected activities
                txtActivityDesc.Text = listViewActivities.SelectedItems[0].SubItems[1].Text;
                dateTimePIcker_ActivityStart.Value = Convert.ToDateTime(listViewActivities.SelectedItems[0].SubItems[2].Text);
                dateTimePicker_ActivityEnd.Value = Convert.ToDateTime(listViewActivities.SelectedItems[0].SubItems[3].Text);

                btn_addActivity.Enabled = true;
                btn_removeActivity.Enabled = true;
                btn_updateActivity.Enabled = true;
            }
        }


        /*****************ACTIVITY BUTTONS****************/

        //create connection to register database
        ActivityService activityService = new ActivityService();

        private void btn_updateActivity_Click(object sender, EventArgs e)
        {
            //create activity object
            Activity activity = new Activity();
            {
                activity.Id = int.Parse(listViewActivities.SelectedItems[0].SubItems[0].Text);
                activity.Name = txtActivityDesc.Text;
                activity.StartDateTime = dateTimePIcker_ActivityStart.Value;
                activity.EndDateTime = dateTimePicker_ActivityEnd.Value;
            };

            //add the data to the register database
            activityService.UpdateActivity(activity);

            MessageBox.Show("Successfully updated activity!");
            //refresh panel
            showPanel("Activities");
        }
        private void btn_addActivity_Click(object sender, EventArgs e)
        {
            //create activity object and link to textboxes
            Activity activity = new Activity();
            {
                activity.Name = txtActivityDesc.Text;
                activity.StartDateTime = dateTimePIcker_ActivityStart.Value;
                activity.EndDateTime = dateTimePicker_ActivityEnd.Value;
            };

            List<Activity> listActivities = activityService.GetActivity();
            foreach (Activity a in listActivities)
            {
                if (a.Name == activity.Name)
                {
                    MessageBox.Show("Name already exists, choose a new name");
                    return;
                }
            }


            //add the data to the activity database
            activityService.AddToActivity(activity);
            //show that the add was successful
            MessageBox.Show("Succeesfully added actiivty!");

            //refresh the panel
            showPanel("Activities");
        }

        private void btn_removeActivity_Click_1(object sender, EventArgs e)
        {
            //dialog pop up asking the user if he is sure of the action
            DialogResult dialogResult = MessageBox.Show("Are you sure you wish to remove this activity? ", "Remove activity", MessageBoxButtons.YesNo);

            //if the answer is yes proceed to remove activity
            if (dialogResult == DialogResult.Yes)
            {
                //create activity object
                Activity activity = new Activity();
                {
                    activity.Id = int.Parse(listViewActivities.SelectedItems[0].SubItems[0].Text);
                    activity.Name = txtActivityDesc.Text;
                    activity.StartDateTime = dateTimePIcker_ActivityStart.Value;
                    activity.EndDateTime = dateTimePicker_ActivityEnd.Value;
                };

                //delete activity
                activityService.DeleteActivity(activity);
                // show that delete was successfull
                MessageBox.Show("Succeesfully deleted actiivty!");
                //refresh panel
                showPanel("Activities");
            }

            //if the answer is no do nothing
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void txtActivityDesc_TextChanged(object sender, EventArgs e)
        {
            if (txtActivityDesc.Text != "" && dateTimePicker_ActivityEnd != null && dateTimePIcker_ActivityStart != null)
            {
                btn_addActivity.Enabled = true;
            }
            else
            {
                btn_addActivity.Enabled = false;
            }
        }
        #endregion

        #region Supervisor Lisitview and buttons
        /******************SUPERVISORS LISTVIEW AND BUTTONS ***********************/

        private void SupervisorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Supervisors");
        }

        private void listViewSupervisorActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listViewSupervisorActivities.SelectedIndices.Count == 0)
                {
                    //if no activity is selected clear the lists
                    listViewSupervisors.Items.Clear();
                    listViewNotSupervisors.Items.Clear();
                }
                else

                {
                    //get the id of the selected activity
                    int activityID = int.Parse(listViewSupervisorActivities.SelectedItems[0].SubItems[0].Text);

                    //create connection to supervisor service
                    SupervisorService supervisorService = new SupervisorService();

                    //get list of supervisors of the selected activity
                    List<Teacher> supervisors = supervisorService.GetSupervisors(activityID);

                    //get list of teachers who are not supervisors of the selected activity
                    List<Teacher> teachers = supervisorService.GetTeachersNotSupervising(activityID);

                    //disable the add and remove buttons until a teacher or supervisor is selected
                    btnAddSupervisor.Enabled = false;
                    btnRemoveSupervisor.Enabled = false;
                    btnAddSupervisor.BackColor = Color.Transparent;
                    btnRemoveSupervisor.BackColor = Color.Transparent;

                    foreach (Teacher t in supervisors)
                    {
                        //add these items to the listview
                        ListViewItem li = new ListViewItem(t.Number.ToString());
                        li.SubItems.Add(t.Name);

                        //add items to listview
                        listViewSupervisors.Items.Add(li);
                    }

                    foreach (Teacher t in teachers)
                    {
                        //add these items to the listview
                        ListViewItem li = new ListViewItem(t.Number.ToString());
                        li.SubItems.Add(t.Name);

                        //add items to listview
                        listViewNotSupervisors.Items.Add(li);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while loading the supervisors: " + ex.Message); //error pop up
                LogError(ex); //error log
            }
        }

        private void btnAddSupervisor_Click(object sender, EventArgs e)
        {
            //create connections to teacher and supervisor service
            TeacherService teacherService = new TeacherService();
            SupervisorService supervisorService = new SupervisorService();

            //get the id of the teacher to be added as supervisor and the id of the selected activity
            int teacherID = int.Parse(listViewNotSupervisors.SelectedItems[0].SubItems[0].Text);
            int activityID = int.Parse(listViewSupervisorActivities.SelectedItems[0].SubItems[0].Text);

            //add the supervisor
            supervisorService.AddSupervisor(teacherID, activityID);

            //modify the IsSupervisor column in Teacher to true
            teacherService.AddIsSupervisor();

            //disable the add button until another teacher is selected
            btnAddSupervisor.Enabled = false;
            btnAddSupervisor.BackColor = Color.Transparent;

            //clear the lists to update them
            listViewSupervisors.Items.Clear();
            listViewNotSupervisors.Items.Clear();

            //get the new teacher and supervisor lists
            List<Teacher> supervisors = supervisorService.GetSupervisors(activityID);
            List<Teacher> teachers = supervisorService.GetTeachersNotSupervising(activityID);

            foreach (Teacher t in supervisors)
            {
                //add these items to the listview
                ListViewItem li = new ListViewItem(t.Number.ToString());
                li.SubItems.Add(t.Name);

                //add items to listview
                listViewSupervisors.Items.Add(li);
            }

            foreach (Teacher t in teachers)
            {
                //add these items to the listview
                ListViewItem li = new ListViewItem(t.Number.ToString());
                li.SubItems.Add(t.Name);

                //add items to listview
                listViewNotSupervisors.Items.Add(li);
            }
        }

        private void listViewNotSupervisors_SelectedIndexChanged(object sender, EventArgs e)
        {
            //enable the button and color it to stand out
            btnAddSupervisor.Enabled = true;
            btnAddSupervisor.BackColor = Color.FromArgb(39, 126, 172);
        }

        private void btnRemoveSupervisor_Click(object sender, EventArgs e)
        {
            //dialog pop up asking the user if he is sure of the action
            DialogResult dialogResult = MessageBox.Show("Are you sure you wish to remove this supervisor? ", "Remove supservisor", MessageBoxButtons.YesNo);

            //if the answer is yes proceed to remove the supervisor
            if (dialogResult == DialogResult.Yes)
            {
                //create connection to teacher and supervisor service
                TeacherService teacherService = new TeacherService();
                SupervisorService supervisorService = new SupervisorService();

                //get the id of the selected teacher and the id of the selected activity
                int teacherID = int.Parse(listViewSupervisors.SelectedItems[0].SubItems[0].Text);
                int activityID = int.Parse(listViewSupervisorActivities.SelectedItems[0].SubItems[0].Text);

                //remove the supervisor
                supervisorService.RemoveSupervisor(teacherID, activityID);

                //modify the IsSupervisor column in Teacher to false if the teacher is not supervising another activity
                teacherService.RemoveIsSupervisor();

                //disable the button and color it gray until another teacher is selected
                btnRemoveSupervisor.Enabled = false;
                btnRemoveSupervisor.BackColor = Color.Transparent;

                //clear the lits
                listViewSupervisors.Items.Clear();
                listViewNotSupervisors.Items.Clear();

                //get the new lists
                List<Teacher> supervisors = supervisorService.GetSupervisors(activityID);
                List<Teacher> teachers = supervisorService.GetTeachersNotSupervising(activityID);

                foreach (Teacher t in supervisors)
                {
                    //add these items to the listview
                    ListViewItem li = new ListViewItem(t.Number.ToString());
                    li.SubItems.Add(t.Name);

                    //add items to listview
                    listViewSupervisors.Items.Add(li);
                }
                foreach (Teacher t in teachers)
                {
                    //add these items to the listview
                    ListViewItem li = new ListViewItem(t.Number.ToString());
                    li.SubItems.Add(t.Name);

                    //add items to listview
                    listViewNotSupervisors.Items.Add(li);
                }
            }

            //if the answer is no do nothing
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void listViewSupervisors_SelectedIndexChanged(object sender, EventArgs e)
        {
            //enable the button and color it to stand out
            btnRemoveSupervisor.Enabled = true;
            btnRemoveSupervisor.BackColor = Color.FromArgb(39, 126, 172);
        }
        #endregion




        #region What happens in someren
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }
        #endregion

        #region Hide Panels
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
            pnlSupervisors.Hide();
            pnlRegister.Hide();
        }
        #endregion

        #region error log
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
        #endregion

        #region Show panel Methods
        /******************SHOW PANEL COMMANDS *****************/

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Dashboard");
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
        #endregion


        #region Registration buttons/ panel
        private void btnRegister_Click(object sender, EventArgs e)
        {
            //show the register panel
            showPanel("Register");
        }

        private void btnRegisterNow_Click(object sender, EventArgs e)
        {
            //create connection to user layer
            UserService userService = new UserService();

            //password hasher
            PasswordWithSaltHasher pwHasher = new PasswordWithSaltHasher();

            //store the username and password
            string username = txtUserName.Text;
            string password = txtPassword.Text;

            //if both the password and username are valid add the user
            if (PasswordRequirements(password) && UsernameValidation(username))
            {
                //this hashes the password
                HashWithSaltResult hashPassword = pwHasher.HashWithSalt(password, 64, SHA256.Create());

                //add the username, hashed password, salt and the user role to the database
                userService.AddToRegister(username, hashPassword.Digest, hashPassword.Salt, false);
                MessageBox.Show("Succesfully Registered! You can now login.");

                //hide the panels and show the dashboard again
                HidePanels();
                showPanel("Dashboard");
                txtUserName.Clear();
            }
            else if(!PasswordRequirements(password)) //if the password does not meet the requirements inform the user
            {
                MessageBox.Show("The password does not meet the requirements");
            }
            else //if the username is taken inform the user
            {
                MessageBox.Show("The username is already in use");
                txtUserName.Clear();
            }
            txtPassword.Clear();

        }

        private bool PasswordRequirements(string password)
        {
            //the special characters string
            string specialCharacters = "!#$%&()*+,-./:;<=>?@[]^_`{|}~";

            //the number of conditions to be met 
            int goodConditions = 5;

            //the current conditions
            int conditions = 0;

            //if the length of the password is greater than 8, add 1 to the current conditions
            if (password.Length >= 8)
                conditions++;

            //if the password contains at least 1 lowercase character, add 1 to the current conditions
            foreach (char c in password)
            {
                if (c >= 'a' && c <= 'z')
                {
                    conditions++;
                    break;
                }
            }

            //if the password contains at least 1 uppercase character, add 1 to the current conditions
            foreach (char c in password)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    conditions++;
                    break;
                }
            }

            //if the password contains at least 1 number, add 1 to the current conditions
            foreach (char c in password)
            {
                if (c >= '0' && c <= '9')
                {
                    conditions++;
                    break;
                }
            }

            //if the password contains at least 1 special character, add 1 to the current conditions
            foreach (char c in specialCharacters)
            {
                if(password.Contains(c))
                {
                    conditions++;
                    break;
                }
            }

            //return true if the current conditions are the same as the goodconditions, false otherwise
            return conditions == goodConditions;
        }

        private bool UsernameValidation(string username)
        {
            //connect to user layer
            UserService userService = new UserService();

            //get a list of all users
            List<User> users = userService.GetAllUsers();

            foreach (User user in users)
            {
                if(user.Username == username)
                {
                    //if the entered username is found in the db, return false
                    return false;
                }
            }
            return true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != "" && txtUserName.Text != "" && txtName.Text != "")
            {
                //enable and color the register button if the fields are not empty
                btnRegisterNow.Enabled = true;
                btnRegisterNow.BackColor = Color.FromArgb(39, 126, 172);

                //if the entered password meets the requirements make the red label disappear
                if (PasswordRequirements(txtPassword.Text))
                    lblPasswordRequirements.Visible = false;
                else
                    lblPasswordRequirements.Visible = true;
            }
            else
            {
                //if the password field is empty disable the button
                btnRegisterNow.Enabled = false;
                btnRegisterNow.BackColor = Color.Transparent;
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != "" && txtUserName.Text != "" && txtName.Text != "")
            {
                //enable and color the register button if the fields are not empty
                btnRegisterNow.Enabled = true;
                btnRegisterNow.BackColor = Color.FromArgb(39, 126, 172);
            }
            else
            {
                //if the username field is empty disable the button
                btnRegisterNow.Enabled = false;
                btnRegisterNow.BackColor = Color.Transparent;
            }
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != "" && txtUserName.Text != "" && txtName.Text != "")
            {
                //enable and color the register button if the fields are not empty
                btnRegisterNow.Enabled = true;
                btnRegisterNow.BackColor = Color.FromArgb(39, 126, 172);
            }
            else
            {
                //if the name field is empty disable the button
                btnRegisterNow.Enabled = false;
                btnRegisterNow.BackColor = Color.Transparent;
            }
        }
        private void txtLicenceKey_TextChanged(object sender, EventArgs e)
        {
            //if the entered licence matches the good one enable every other field
            if (txtLicenceKey.Text == "XsZAb - tgz3PsD - qYh69un - WQCEx")
            {
                txtPassword.Enabled = true;
                txtName.Enabled = true;
                txtUserName.Enabled = true;
                lblLicenceExplained.Visible = false;
            }
            //else diable everything
            else
            {
                txtPassword.Enabled = false;
                txtName.Enabled = false;
                txtUserName.Enabled = false;
                lblLicenceExplained.Visible = true;
            }
        }

        private void btnGoBackToLogin_Click(object sender, EventArgs e)
        {
            //go back to the dashboard
            showPanel("Dashboard");
        }
        #endregion

        #region Login buttons/ panel
        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                //create connection to user layer
                UserService userService = new UserService();

                //store the entered username and password
                string username = txtLoginUsername.Text;
                string enteredPassword = txtLoginPassword.Text;

                //get the user by the entered username
                User user = userService.GetUserByUsername(username);

                //password hasher
                PasswordWithSaltHasher passwordHasher = new PasswordWithSaltHasher();

                //if the entered password matches the one in the db
                if (passwordHasher.PasswordValidation(enteredPassword, user.Password, user.Salt))
                {
                    MessageBox.Show("Login successful!");
                    
                    //the current user becomes the entered user
                    currentUser = user;

                    //hide the panels and show the dashboard again
                    HidePanels();
                    showPanel("Dashboard");
                }
                else
                    MessageBox.Show("Login failed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");

                //clear the text boxes
                txtLoginPassword.Text = "";
                txtLoginUsername.Text = "";

                LogError(ex);
            }
        }
        private void txtLoginUsername_TextChanged(object sender, EventArgs e)
        {
            //if the username and password fields are not empty enable and color the button
            if (txtLoginUsername.Text != "" && txtLoginPassword.Text != "")
            {
                btn_Login.Enabled = true;
                btn_Login.BackColor = Color.FromArgb(39, 126, 172);
            }
            //else disable it
            else
            {
                btn_Login.Enabled = false;
                btn_Login.BackColor = Color.Transparent;
            }
        }
        private void txtLoginPassword_TextChanged(object sender, EventArgs e)
        {
            //if the username and password fields are not empty enable and color the button
            if (txtLoginUsername.Text != "" && txtLoginPassword.Text != "")
            {
                btn_Login.Enabled = true;
                btn_Login.BackColor = Color.FromArgb(39, 126, 172);
            }
            //else diable it
            else
            {
                btn_Login.Enabled = false;
                btn_Login.BackColor = Color.Transparent;
            }
        }
        #endregion


        #region Hide and show menu strip items
        private void HideMenuStripItems()
        {
            //hide all menu strip items except dashboard
            studentsToolStripMenuItem.Visible = false;
            lecturersToolStripMenuItem.Visible = false;
            activitiesToolStripMenuItem.Visible = false;
            drinktoolStripMenuItem1.Visible = false;
            roomsToolStripMenuItem.Visible = false;
            CashRegtoolStripMenuItem1.Visible = false;
            RevenueReportToolStripMenuItem.Visible = false;
            SupervisorsToolStripMenuItem.Visible = false;
        }

        private void ShowMenuStripItems()
        {
            //show all menu strip items
            studentsToolStripMenuItem.Visible = true;
            lecturersToolStripMenuItem.Visible = true;
            activitiesToolStripMenuItem.Visible = true;
            drinktoolStripMenuItem1.Visible = true;
            roomsToolStripMenuItem.Visible = true;
            CashRegtoolStripMenuItem1.Visible = true;
            RevenueReportToolStripMenuItem.Visible = true;
            SupervisorsToolStripMenuItem.Visible = true;
        }

        #endregion

        #region Administration
        private void ProgramAdministration()
        {
            if(currentUser == null)
            {
                //if there is no user yet hide every menu strip item
                HideMenuStripItems();
            }    
            else
            {
                //if a user has been entered show the menu strip items
                ShowMenuStripItems();

                //make the login form invisible
                lbl_Username.Visible = false;
                lbl_Password.Visible = false;
                txtLoginPassword.Visible = false;
                txtLoginUsername.Visible = false;
                btn_Login.Visible = false;
                btnRegister.Visible = false;
                label3.Visible = false;

                if (currentUser.Role == false)
                {
                    //if the user is not an admin keep only reading rights
                    btnAddDrink.Visible = false;
                    btnRemoveDrink.Visible = false;
                    btnUpdateDrink.Visible = false;
                    btn_addActivity.Visible = false;
                    btn_updateActivity.Visible = false;
                    btn_removeActivity.Visible = false;
                    btnAddSupervisor.Visible = false;
                    btnRemoveSupervisor.Visible = false;
                    lblStudentID.Visible = false;
                    lblStudentName.Visible = false;
                    lblDrinkName.Visible = false;
                    lblDrinkPrice.Visible = false;
                    txtBDrinkName.Visible = false;
                    txtBDrinkPrice.Visible = false;
                    txtBStudentID.Visible = false;
                    txtBStudentName.Visible = false;
                    btnCheckOut.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    txtActivityDesc.Visible = false;
                    dateTimePIcker_ActivityStart.Visible = false;
                    dateTimePicker_ActivityEnd.Visible = false;
                    RevenueReportToolStripMenuItem.Visible = false;
                }
            }
        }
        #endregion

    }
}
