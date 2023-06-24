using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AC4_UserRegisteration : System.Web.UI.Page
{
    private static string User_Id;
    private static List<string> Campuses = new List<string>();
    private static List<string> Departments = new List<string>();
    private string getCurrentYear()
    {
        string res;
        string query = "SELECT YEAR(GETDATE())";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
            res = reader.GetValue(0).ToString();
        else res = "2020";
        connection.Close();
        reader.Close();
        return res;
    }
    private void LoadMonthList()
    {
        MonthList.Items.Clear();
        MonthList.Items.Add("January");
        MonthList.Items.Add("February");
        MonthList.Items.Add("March");
        MonthList.Items.Add("April");
        MonthList.Items.Add("May");
        MonthList.Items.Add("June");
        MonthList.Items.Add("July");
        MonthList.Items.Add("August");
        MonthList.Items.Add("September");
        MonthList.Items.Add("October");
        MonthList.Items.Add("November");
        MonthList.Items.Add("December");
        MonthList.DataBind();
    }
    private int GetDaysPerMonth()
    {
        int TotalDays = 30;
        int MonthNumber = MonthList.SelectedIndex + 1;
        if (MonthNumber < 7 && MonthNumber % 2 == 1)
            TotalDays = 31;
        else if (MonthNumber > 7 && MonthNumber % 2 == 0)
            TotalDays = 31;
        else if (MonthNumber == 2)
            TotalDays = 28;
        return TotalDays;
    }
    private void LoadDayList()
    {
        int TotalDays = GetDaysPerMonth();
        List<int> dayOptions = new List<int>();
        for (int i = 1; i <= TotalDays; i++)
            dayOptions.Add(i);
        DayList.DataSource = dayOptions;
        DayList.DataBind();
    }
    private void LoadUserInfo()
    {
        GenderList.Items.Clear();
        GenderList.Items.Add("Male");
        GenderList.Items.Add("Female");
        GenderList.Items.Add("Other");
        GenderList.DataBind();

        YearList.Items.Clear();
        int year = Convert.ToInt32(getCurrentYear()) - 10;

        for (int i = 0; i < 70; i++, year--)
            YearList.Items.Add(year.ToString());
        YearList.DataBind();

        LoadMonthList();
        LoadDayList();
    }
    private void LoadCampuses()
    {
        string query = "SELECT Location, Campus_Id FROM CAMPUS ORDER BY Campus_Id";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        CampusList.Items.Clear();
        while(reader.Read())
        {
            Campuses.Add(reader.GetValue(1).ToString());
            CampusList.Items.Add(reader.GetValue(0).ToString());
        }
        connection.Close();
        CampusList.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //User_Id = Request.QueryString["id"];
            User_Id = "1";
            UserTypeList.Items.Clear();
            UserTypeList.Items.Add("Admin");
            UserTypeList.Items.Add("Faculty");
            UserTypeList.Items.Add("Student");
            UserTypeList.DataBind();
            LoadCampuses();
            UserTypeList_SelectedIndexChanged(null, null);
            LoadUserInfo();
        }
        LoadDayList();

    }
    private string InsertUser()
    {
        string fn = FirstNameText.Text;
        string ln = LastNameText.Text;
        string cnic = CNICText.Text;
        string Dob = YearList.Text + "/" + MonthList.Text + "/" + DayList.Text;
        char gen = (GenderList.Text == "Male") ? 'M' : (GenderList.Text == "Female") ? 'F' : 'O';
        string add = AddressText.Text;
        string ct = CityText.Text;
        string ph = PhoneText.Text;

        string Insquery = "INSERT INTO USERS (First_Name, Last_Name, CNIC, DoB, Gender, Address, City, Phone ) VALUES ( '" +
              fn + "', '" + ln + "', '" + cnic + "', '" + Dob + "', '" + gen + "', '" + add + "', '" + ct + "', '" + ph + "')";
        ExecuteNonQuery(Insquery);
        return ExecuteReader("SELECT MAX(User_Id) AS UserId FROM USERS", "UserId");
    }
    public string ExecuteReader(string query, string Column)
    {
        string res;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataReader reader = cmd.ExecuteReader();
        if (!reader.Read())
            res = "";
        else res = reader[Column].ToString();
        connection.Close();
        return res;
    }
    private void InsertAccount(string UserId)
    {
        string identification = "Username";
        string un = UsernameText.Text;
        char type = 'A';
        if (UserTypeList.SelectedIndex == 1)
            type = 'F';
        if (UserTypeList.SelectedIndex == 2)
        {
            identification = "RegNo"; 
            type = 'S';
        }
        string em = EmailText.Text, ps = PasswordText.Text;
        string Insquery = "INSERT INTO USERACCOUNT (User_Id, " + identification + ", Email, Password, Type) VALUES (" + UserId + " ,'" + un + "', '" + em + "','" + ps + "','" + type + "') ";
        ExecuteNonQuery(Insquery);
    }
    private void InsertAdmin(string UserId)
    {
        string cmp = Campuses[CampusList.SelectedIndex];
        string jobtitle = JobTitleList.Text;
        string sal = SalaryText.Text;
        ExecuteNonQuery("INSERT INTO ADMIN VALUES (" + UserId + ", " + cmp + ", '" + jobtitle + "', " + sal + ")");
    }
    private void InsertFaculty(string UserId)
    {
        string dept = Departments[DepartmentList.SelectedIndex];
        string jobtitle = JobTitleList.Text;
        string sal = SalaryText.Text;
        ExecuteNonQuery("INSERT INTO FACULTY VALUES (" + UserId + ", " + dept + ", '" + jobtitle + "', " + sal + ")");
    }
    private void InsertStudent(string UserId)
    {
        string dept = Departments[DepartmentList.SelectedIndex];
        string ps = ParentSectionBox.Text;
        string batch = BatchText.Text;
        string deg = DegreeBox.Text;
        ExecuteNonQuery("INSERT INTO STUDENT VALUES (" + UserId + ", " + dept + ", '" + ps + "','" + batch + "','" + deg + "' , 'A')");
    }
    private void AddUser()
    {
        string UserId = InsertUser();
        InsertAccount(UserId);
        LogEvent(Convert.ToInt32(User_Id), "Registered New User : " + UsernameText.Text);

        if (UserTypeList.SelectedIndex == 0)
            InsertAdmin(UserId);
        else if (UserTypeList.SelectedIndex == 1)
            InsertFaculty(UserId);
        else if (UserTypeList.SelectedIndex == 2)
            InsertStudent(UserId);
    }
    public int ExecuteNonQuery(string query)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        int res = cmd.ExecuteNonQuery();
        connection.Close();
        return res;
    }
    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        if (UsernameText.Text == "" || PasswordText.Text == "" || EmailText.Text == "" || FirstNameText.Text == "" || 
            LastNameText.Text == "" || CNICText.Text == "" || CityText.Text == "" || AddressText.Text == "" ||
            PhoneText.Text == "")
        {
            FailLabel.Visible = true;
            SuccessLabel.Visible = false;
        }
        else
        {
            SuccessLabel.Visible = true;
            FailLabel.Visible = false;
            AddUser();
        }

    }
    private bool ValidFormat(string regNo)
    {
        string query = "SELECT '" + regNo + "' WHERE '" + regNo + "' LIKE '__[ILKPF]-____'";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        bool res = command.ExecuteScalar() != null;
        connection.Close();
        return res;
    }
    private bool ValidRegNo(string regNo)
    {
        if (!ValidFormat(regNo))
            return false;
        string query = "SELECT RegNo FROM USERACCOUNT WHERE RegNo = '" + regNo + "'";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        bool res = command.ExecuteScalar() == null;
        connection.Close();
        return res;
    }
    private bool ValidUsername(string un)
    {
        string query = "SELECT Username FROM USERACCOUNT WHERE Username = '" + un + "'";
        if (UserTypeList.SelectedIndex == 2)
            query = "SELECT RegNo FROM USERACCOUNT WHERE RegNo = '" + un +"'";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        bool res = command.ExecuteScalar() == null;
        connection.Close();
        return res;
    }

    protected void UsernameText_TextChanged(object sender, EventArgs e)
    {
        if (UserTypeList.SelectedIndex < 2 && !ValidUsername(UsernameText.Text)
            || UserTypeList.SelectedIndex == 2 && !ValidRegNo(UsernameText.Text))
            UsernameText.Text = "";
    }
    private string GetAdminDept()
    {
        string res;
        string query = "SELECT Campus_Id FROM ADMIN WHERE User_Id = " + User_Id ;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read()) 
            res = reader.GetValue(0).ToString();
        else res = "";
        return res;
    }
    private void LoadDepartments(string campus_Id)
    {
        string query = "SELECT Name, Dept_Id FROM DEPARTMENT INNER JOIN CAMPUS ON CAMPUS.Campus_Id = DEPARTMENT.Campus_Id WHERE CAMPUS.Campus_Id = " + campus_Id;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        DepartmentList.Items.Clear();
        Departments.Clear();
        while (reader.Read()) {
            DepartmentList.Items.Add(reader.GetValue(0).ToString());
            Departments.Add(reader.GetValue(1).ToString());
        }

        connection.Close();
        DepartmentList.DataBind();
    }

    protected void UserTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idx = UserTypeList.SelectedIndex;
        if (idx == 0)
            UserTypeHeader.Text = "Admin Information";
        else if (idx == 1)
            UserTypeHeader.Text = "Faculty Information";
        else if (idx == 2)
            UserTypeHeader.Text = "Student Information";


        SalaryText.Visible = false;
        SalaryLabel.Visible = false;
        JobTitleList.Visible = false;
        JobTitleLabel.Visible = false;
        DepartmentLabel.Visible = false;
        DepartmentList.Visible = false;
        ParentSectionBox.Visible = false;
        ParentSectionLabel.Visible = false;
        DegreeBox.Visible = false;
        DegreeLabel.Visible = false;
        BatchLabel.Visible = false;
        BatchText.Visible = false;
        CampusLabel.Visible = false;
        CampusList.Visible = false;

        if (idx == 0)
        {
            CampusLabel.Visible = true;
            CampusList.Visible = true; 
        }
        if (idx == 0 || idx == 1)
        {
            SalaryText.Visible = true;
            SalaryLabel.Visible = true;
            JobTitleList.Visible = true;
            JobTitleLabel.Visible = true;
            UsernameLabel.Text = "Username";
        }
        if (idx == 1)
        {
            LoadDepartments(Campuses[CampusList.SelectedIndex]);
        }
        if (idx == 1 || idx == 2)
        {
            DepartmentLabel.Visible = true;
            DepartmentList.Visible = true;
        }
        if(idx == 2)
        {
            ParentSectionBox.Visible = true;
            ParentSectionLabel.Visible = true;
            DegreeBox.Visible = true;
            DegreeLabel.Visible = true;
            BatchLabel.Visible = true;
            BatchText.Visible = true;
            UsernameLabel.Text = "Registeration Number";
            LoadDepartments(GetAdminDept());
        }
    }
    protected void EmailText_TextChanged(object sender, EventArgs e)
    {
        if (!EmailText.Text.Contains('@'))
            EmailText.Text = "";
    }
    protected void CNICText_TextChanged(object sender, EventArgs e)
    {
        if (CNICText.Text.Length != 13)
            CNICText.Text = "";
    }
    protected void PhoneText_TextChanged(object sender, EventArgs e)
    {
        string str = PhoneText.Text;
        foreach (char c in str)
            if (c < '0' || c > '9')
            {
                PhoneText.Text = "";
                break;
            }
    }
    protected void ParentSectionBox_TextChanged(object sender, EventArgs e)
    {
        if (ParentSectionBox.Text.Length > 1 || ParentSectionBox.Text[0] < 'A' || ParentSectionBox.Text[0] > 'Z') 
            ParentSectionBox.Text = "";
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        if (!Menu1.Visible)
            Menu1.Visible = true;
        else Menu1.Visible = false;
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        if (e.Item.Text == "Home")
            e.Item.NavigateUrl = "~/AdminMain.aspx?id=" + User_Id;
        else if (e.Item.Text == "Offer Courses")
            e.Item.NavigateUrl = "~/AC1_CourseOffer.aspx?id=" + User_Id;
        else if (e.Item.Text == "Assign Coordinators")
            e.Item.NavigateUrl = "~/AC2_AssignCoordinators.aspx?id=" + User_Id;
        else if (e.Item.Text == "Allocate Instructors")
            e.Item.NavigateUrl = "~/AC3_CourseAllocation.aspx?id=" + User_Id;
        else if (e.Item.Text == "Register Users")
            e.Item.NavigateUrl = "~/AC4_UserRegisteration.aspx?id=" + User_Id;
        else if (e.Item.Text == "Audit Trail Report")
            e.Item.NavigateUrl = "~/AC5_AuditTrail.aspx?id=" + User_Id;
        else if (e.Item.Text == "Offered Courses Report")
            e.Item.NavigateUrl = "~/AC6_OfferedCoursesReport.aspx?id=" + User_Id;
        else if (e.Item.Text == "Course Allocation Report")
            e.Item.NavigateUrl = "~/AC7_CourseAllocationReport.aspx?id=" + User_Id;
        else if (e.Item.Text == "Student Sections Report")
            e.Item.NavigateUrl = "~/AC8_StudentSectionsReport.aspx?id=" + User_Id;
        Response.Redirect(e.Item.NavigateUrl);
    }
    public static void LogEvent(int userId, string action)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO AUDITTRAIL (User_Id, Activity) VALUES (@UserId, @Action)", connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@Action", action);
            command.ExecuteNonQuery();
        }
    }
}