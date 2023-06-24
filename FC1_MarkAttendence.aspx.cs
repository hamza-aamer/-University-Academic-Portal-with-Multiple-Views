using Microsoft.SqlServer.Server;
using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

public class StudentRecord
{
    public int Srno { get; set; }
    public string RegNo { get; set; }
    public string User_Id { get; set; }
    public string Student_Name { get; set; }
}
public class SectionInfo
{
    public string Name { get; set; }
    public string Id { get;set ;}
}
public partial class MarkAttendence : System.Web.UI.Page
{
    static string User_Id;
    static List<StudentRecord> Students;
    private void LoadCampuses()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();

        string query = "SELECT Location FROM CAMPUS " +
                       "INNER JOIN DEPARTMENT ON CAMPUS.Campus_Id = DEPARTMENT.Dept_Id " +
                       "INNER JOIN FACULTY ON FACULTY.Dept_Id = DEPARTMENT.Dept_Id " +
                       "WHERE FACULTY.User_Id = " + User_Id;
        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();
        while (reader.Read())
            CampusList.Items.Add(reader.GetString(0));
        CampusList.DataBind();
    }
    private void LoadSemesters()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();

        string query = "SELECT DISTINCT OfferedIn FROM SECTION S INNER JOIN OFFEREDCOURSE O ON S.Course_Id = O.OfferCourse_Id WHERE Instructor_Id = " + User_Id;

        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();
        SemesterList.Items.Clear();

        while(reader.Read())
            SemesterList.Items.Add(reader.GetString(0));
        SemesterList.DataBind();
    }
    private void LoadCourses()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();

        string query = "SELECT DISTINCT CONCAT(Course_Code, ' ', COURSE.Name) AS Code FROM SECTION " +
                       "INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = SECTION.Course_Id " +
                       "INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id " +
                       "WHERE SECTION.Instructor_Id = " + User_Id + " AND OfferedIn = '" + SemesterList.SelectedValue+"'";

        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();
        CourseList.Items.Clear();

        while (reader.Read())
            CourseList.Items.Add(reader.GetString(0));
        CourseList.DataBind();
        LoadSections();
    }
    private void SelectMonth()
    {
        string semester = SemesterList.SelectedValue;
        MonthList.Items.Clear();
        for (int i = 1; i <= 12; i++)
            MonthList.Items.Add(i.ToString());
        MonthList.DataBind();
        LoadDayList();
    }
    private List<SectionInfo> getSectionList()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();

        if (CourseList.Items.Count == 0)
            return null;
        string course = CourseList.SelectedValue;
        course = course.Substring(0, course.IndexOf(' '));

        string query = "SELECT 0, '-' UNION SELECT SECTION.Section_Id AS ID, SECTION.Name AS Name FROM SECTION INNER JOIN OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id " +
                "INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id WHERE Instructor_Id = " + User_Id +
                " AND OfferedIn = '" + SemesterList.SelectedValue + "' AND Course_Code = '" + course + "'";
        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();

        List <SectionInfo> list = new List<SectionInfo>();

        while (reader.Read())
        {
            SectionInfo s = new SectionInfo();
            s.Name = reader.GetString(reader.GetOrdinal("Name"));
            s.Id = reader.GetValue(reader.GetOrdinal("ID")).ToString(); ; 
            list.Add(s);
        }
        return list;
    }
    private void LoadSections()
    {
        List<SectionInfo> Section_Ids = getSectionList();
        if (Section_Ids == null) return;
        SectionList.Items.Clear();

        foreach(SectionInfo S in Section_Ids)
            SectionList.Items.Add(S.Name);

        SectionList.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            LoadCampuses();
            LoadSemesters();
            LoadCourses();
            SelectMonth();
            LoadClassForm();
        }
        SuccessUpdateLabel.Visible = false;
        FailUpdateLabel.Visible = false;

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
    private void LoadClassForm()
    {
        List<double> durationOptions = new List<double>();
        for(double i = 1; i < 3; i += 0.5)
            durationOptions.Add(i);
        DurationList.DataSource = durationOptions;
        DurationList.DataBind();
    }
    private bool ClassExists(string class_Id)
    {
        return ExecuteScalar("SELECT Class_Id FROM CLASS WHERE Class_Id = " + class_Id);
    }
    private void LoadStudentSheet(string class_Id, string date)
    {
        if (!ClassExists(class_Id))
            return;


        string query = "SELECT USERS.User_Id AS ID, RegNo, CONCAT(First_Name, ' ', Last_Name) AS Student_Name FROM USERS INNER JOIN USERACCOUNT " +
                        "ON USERS.User_id = USERACCOUNT.User_Id INNER JOIN ATTENDENCE ON ATTENDENCE.Student_Id = USERS.User_Id INNER JOIN CLASS " +
                        "ON CLASS.Class_Id = ATTENDENCE.Class_Id WHERE CLASS.Class_Id = " + class_Id + " AND Date = '" + date+"'";

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();
        Students = new List<StudentRecord>();
        if (!reader.HasRows)
            NoStdExistLabel.Visible = true;
        else NoStdExistLabel.Visible = false;

        int StudentSrNo = 1;
        while (reader.Read())
        {
            StudentRecord std = new StudentRecord();
            std.Srno = StudentSrNo++;
            std.User_Id = reader["ID"].ToString();
            std.RegNo = reader["RegNo"].ToString();
            std.Student_Name = reader["Student_Name"].ToString();
            Students.Add(std);
        }
        SectionStudentsList.DataSource = Students;
        SectionStudentsList.DataBind();
    }
    protected void SecAttButton_Click(object sender, EventArgs e)
    {
        List<SectionInfo> sections = getSectionList();
        string semester = SemesterList.SelectedItem.Text;
        string year = semester.Substring(semester.IndexOf(' ') + 1, 4);
        string section = sections[SectionList.SelectedIndex].Id;
        string date = year + '-' + MonthList.SelectedItem.ToString() + '-' + DayList.SelectedItem.ToString();
        string class_id = ExecuteReader("SELECT Class_Id FROM CLASS WHERE Section_Id = " + section + " AND Date = '" + date + "'", "Class_Id");

        LoadStudentSheet(class_id, date);
        Page_Load(null, null);
    }
    public bool ExecuteScalar(string query, int Column = 0)
    {
        bool res = false;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
            res = true;
        connection.Close();
        return res;
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
    public int ExecuteNonQuery(string query)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        int res = cmd.ExecuteNonQuery();
        connection.Close();
        return res;
    }
    private List<StudentRecord> ConstructStudentList(string section)
    {
        string query = "SELECT USERS.User_Id AS ID, RegNo, CONCAT(First_Name, ' ', Last_Name) AS Student_Name FROM TRANSCRIPT INNER JOIN " +
            "USERACCOUNT ON TRANSCRIPT.Student_Id = USERACCOUNT.User_Id INNER JOIN USERS ON USERS.User_Id = TRANSCRIPT.Student_Id " +
            "WHERE TRANSCRIPT.Section_Id = " + section;

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();

        Students = new List<StudentRecord>();
        int StudentSrNo = 1;
        while (reader.Read())
        {
            StudentRecord std = new StudentRecord();
            std.Srno = StudentSrNo++;
            std.User_Id = reader["ID"].ToString();
            std.RegNo = reader["RegNo"].ToString();
            std.Student_Name = reader["Student_Name"].ToString();
            Students.Add(std);
        }
        return Students;
    }
    private string ConstructStudentInsertQuery(string section, string class_Id)
    {
        string res = "";
        string Ins = "INSERT INTO ATTENDENCE (Class_Id, Student_Id, Status) VALUES ";
        ConstructStudentList(section);

        for (int i = 0; i < Students.Count; i++) 
            res += "(" + class_Id + ", " + Students[i].User_Id + " , 'P'),";
        res = res.Remove(res.Length - 1, 1);
        if (res != "")
            res = Ins + res;
        return res;
    }
    private void AddClass(string section, string date)
    {
        string query = "SELECT MAX(LectureNo) + 1 AS L FROM CLASS WHERE CLASS.Section_Id = " + section;
        string lecture_Num = ExecuteReader(query, "L"), duration = DurationList.SelectedValue.ToString();
        if (lecture_Num == "")
            lecture_Num = "1";
        ExecuteNonQuery("INSERT INTO CLASS(Section_Id, LectureNo, Duration, Date) VALUES ( " + section + ", " + lecture_Num + ", " + duration + ", '" + date + "')");
        string class_Id = ExecuteReader("SELECT Class_id FROM CLASS WHERE LectureNo = " + lecture_Num + " AND Section_Id = " + section, "Class_Id");
        string Insquery = ConstructStudentInsertQuery(section, class_Id);

        ExecuteNonQuery(Insquery);
    }
    protected void ClassAddButton_Click(object sender, EventArgs e)
    {
        List<SectionInfo> sections = getSectionList();
        string semester = SemesterList.SelectedItem.Text;
        string year = semester.Substring(semester.IndexOf(' ') + 1, 4);
        string section = sections[SectionList.SelectedIndex].Id;
        string date = year + '-' + MonthList.SelectedItem.ToString() + '-' + DayList.SelectedItem.ToString();
        string query = "SELECT Class_Id FROM CLASS WHERE Section_Id = " + section + " AND Date = '" + date + "'";
        
        if(!ExecuteScalar(query))
        {
            AddClass(section, date);
            FailLabel.Visible = false;
            SuccessLabel.Visible = true;
            SaveUpdatesButton.Visible = true;
        }
        else
        {
            SuccessLabel.Visible = false;
            FailLabel.Visible = false;
        }
    }

    protected void CourseList_TextChanged(object sender, EventArgs e)
    {
        LoadSections();
        LoadClassForm();
    }
    protected void SemesterList_TextChanged(object sender, EventArgs e)
    {
        LoadCourses();
    }

    protected void MonthList_TextChanged(object sender, EventArgs e)
    {
        LoadDayList();
    }

    private string GenerateStudentAttendence(string classId, string date, string section)
    {
        bool UpdatesMade = false;
        string res = "UPDATE ATTENDENCE SET Status = 'A' WHERE Student_Id IN (";
        Students = ConstructStudentList(section);

        foreach (GridViewRow row in SectionStudentsList.Rows)
        {
            CheckBox checkbox = (CheckBox)row.FindControl("chkEnabled");
            if (checkbox != null && !checkbox.Checked) {
                res += Students[row.RowIndex].User_Id + ',';
                UpdatesMade = true;
            }
        }
        res = res.Remove(res.Length - 1, 1);
        res += ')';
        if (!UpdatesMade) res = "";
        return res;
    }
    protected void SecSaveButton_Click(object sender, EventArgs e)
    {
        List<SectionInfo> sections = getSectionList();
        string semester = SemesterList.SelectedItem.Text;
        string year = semester.Substring(semester.IndexOf(' ') + 1, 4);
        string section = sections[SectionList.SelectedIndex].Id;
        string date = year + '-' + MonthList.SelectedItem.ToString() + '-' + DayList.SelectedItem.ToString();
        string class_id = ExecuteReader("SELECT Class_Id FROM CLASS WHERE Section_Id = " + section + " AND Date = '" + date + "'", "Class_Id");

        string query = GenerateStudentAttendence(class_id, date, section);
        if (query != "" && ExecuteNonQuery(query) != 0)
        {
            SuccessUpdateLabel.Visible = true;
            FailUpdateLabel.Visible = false;
        }
        else
        {
            SuccessUpdateLabel.Visible = false;
            FailUpdateLabel.Visible = true;
        }
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
            e.Item.NavigateUrl = "~/FacultyMain.aspx?id=" + User_Id;
        else if (e.Item.Text == "Mark Attendence")
            e.Item.NavigateUrl = "~/FC1_MarkAttendence.aspx?id=" + User_Id;
        else if (e.Item.Text == "Set Marks Distribution")
            e.Item.NavigateUrl = "~/FC2_MarksDistribution.aspx?id=" + User_Id;
        else if (e.Item.Text == "Mark Evaluations")
            e.Item.NavigateUrl = "~/FC3_MarkEvaluations.aspx?id=" + User_Id;
        else if (e.Item.Text == "Finalize Grades")
            e.Item.NavigateUrl = "~/FC4_FinalizeGrades.aspx?id=" + User_Id;
        else if (e.Item.Text == "Grade Report")
            e.Item.NavigateUrl = "~/FC5_GradeReport.aspx?id=" + User_Id;
        else if (e.Item.Text == "Feedback Report")
            e.Item.NavigateUrl = "~/FC6_FeedbackReport.aspx?id=" + User_Id;
        else if (e.Item.Text == "Transcript Report")
            e.Item.NavigateUrl = "~/FC7_TranscriptReport.aspx?id=" + User_Id;
        Response.Redirect(e.Item.NavigateUrl);
    }
}



//private List<string> ConstructStudentList(string section)
//{
//    string query = "SELECT Student_Id FROM TRANSCRIPT WHERE Section_Id = 1" + section;

//    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
//    connection.Open();
//    SqlCommand cm = new SqlCommand(query, connection);
//    SqlDataReader reader = cm.ExecuteReader();
//    List<string> std = new List<string>();
//    while (reader.Read())
//        std.Add(reader.GetString(0).ToString());

//    return std;
//}
