using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;


public class CourseTaken
{
    public string Course_Code;
    public string Course_Id;
}
public partial class ViewAttendence : System.Web.UI.Page
{
    private static string User_Id;
    private static List<CourseTaken> Courses;

    private void LoadCourseOptions()
    {
        string query = "SELECT Course_Code, OfferCourse_Id FROM COURSE INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = COURSE.Course_Id INNER JOIN " +
            "SECTION ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id INNER JOIN TRANSCRIPT ON TRANSCRIPT.Section_Id = SECTION.Section_id " +
            "WHERE Student_Id = " + User_Id + " AND OfferedIn = (SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date)";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        CourseList.Items.Clear();
        Courses = new List<CourseTaken>();
        while (reader.Read())
        {
            CourseTaken course = new CourseTaken();
            course.Course_Code = reader.GetString(0);
            course.Course_Id = reader.GetValue(1).ToString();
            Courses.Add(course);
            CourseList.Items.Add(course.Course_Code);
        }
        CourseList.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            LoadCourseOptions();
        }

    }

    private void LoadAttendence(int idx)
    {
        string query = "SELECT LectureNo, Duration, Date, Status FROM ATTENDENCE INNER JOIN CLASS ON CLASS.Class_Id = ATTENDENCE.Class_Id " +
            "INNER JOIN SECTION ON SECTION.Section_id = CLASS.Section_id WHERE Course_Id = " + Courses[idx].Course_Id + " AND Student_Id = " + User_Id;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        AttendenceList.DataSource = reader;
        AttendenceList.DataBind(); 
    }
    protected void CourseOptionSelected(object sender, EventArgs e)
    {
        LoadAttendence(CourseList.SelectedIndex);
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
            e.Item.NavigateUrl = "~/StudentMain.aspx?id=" + User_Id;
        else if (e.Item.Text == "Course Registeration")
            e.Item.NavigateUrl = "~/SC1_RegisterCourse.aspx?id=" + User_Id;
        else if (e.Item.Text == "Attendence")
            e.Item.NavigateUrl = "~/SC2_ViewAttendence.aspx?id=" + User_Id;
        else if (e.Item.Text == "Evaluations")
            e.Item.NavigateUrl = "~/SC3_ViewEvaluations.aspx?id=" + User_Id;
        else if (e.Item.Text == "Transcript")
            e.Item.NavigateUrl = "~/SC4_ViewTranscript.aspx?id=" + User_Id;
        else if (e.Item.Text == "Course Feedback")
            e.Item.NavigateUrl = "~/SC6_CrsFeedbackSelect.aspx?id=" + User_Id;
        Response.Redirect(e.Item.NavigateUrl);
    }
}