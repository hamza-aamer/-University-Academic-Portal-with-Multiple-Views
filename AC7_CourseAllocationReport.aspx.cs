using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AC7_CourseAllocationReport : System.Web.UI.Page
{
    static string User_Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            User_Id = "1";
        }
    }
    private string LoadCurrentSemester()
    {
        return ExecuteReader("SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC", "Semester");
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
    public void LoadAllocationTable(string Semester, string User_Id)
    {
        string query = "SELECT DEPARTMENT.Name AS Dept_Code, Course_Code, SECTION.Name AS Section_Name, " +
                       "CONCAT(First_name, ' ', Last_Name) AS Instructor_Name FROM SECTION " +
                       "INNER JOIN OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id " +
                       "INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id " +
                       "INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = OFFEREDCOURSE.Dept_Id " +
                       "INNER JOIN CAMPUS ON CAMPUS.Campus_Id = DEPARTMENT.Campus_Id " +
                       "LEFT JOIN USERS ON USERS.User_Id = SECTION.Instructor_Id " +
                       "WHERE OfferedIn = '" + Semester + "' AND CAMPUS.Campus_Id = ( " +
                       "SELECT Campus_Id FROM ADMIN WHERE User_Id = " + User_Id + ") ";

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        CourseAllocationList.DataSource = reader;
        CourseAllocationList.DataBind();
        connection.Close();
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


    protected void Button1_Click(object sender, EventArgs e)
    {
        string semCode = LoadCurrentSemester();
        LoadAllocationTable(semCode, User_Id);
    }
}