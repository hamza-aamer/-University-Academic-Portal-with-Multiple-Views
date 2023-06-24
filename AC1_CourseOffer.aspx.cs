using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class AC1_CourseOffer : System.Web.UI.Page
{
    static string User_Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
        }
    }
    private bool Validate(string dept, string crs, string sem)
    {
        string query = "SELECT Dept_Id FROM OFFEREDCOURSE WHERE Dept_Id = " + 
            dept + " AND Course_Id = " + crs + " AND OfferedIn = '" + sem + "';";
        string SqlConnectionStr = ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(SqlConnectionStr);
        connection.Open();
        SqlCommand cm = new SqlCommand(query, connection);
        bool res = (cm.ExecuteScalar() == null);
        connection.Close();
        return res;
    }
    private void InsertQuery(string dept, string crs, string sem)
    {
        string query = "INSERT INTO FLEX.DBO.OFFEREDCOURSE (Dept_Id, Course_Id, OfferedIn)" +
            "VALUES (" + dept + ", " + crs + ",'" + sem + "');";
        string SqlConnectionStr = ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(SqlConnectionStr);
        connection.Open();
        SqlCommand cm = new SqlCommand(query, connection);
        cm.ExecuteNonQuery();
        connection.Close();
        cm.Dispose();
        SuccessLabel.Visible = true;
        InvalidDeptCode.Visible = false;
        InvalidCrsCode.Visible = false;
        LogEvent(Convert.ToInt32(User_Id), "Offered a course");
    }
    private string LoadCurrentSemester()
    {
        return  ExecuteReader("SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC", "Semester");
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        string SqlConnectionStr = ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString;
        SqlConnection connection1 = new SqlConnection(SqlConnectionStr);
        SqlConnection connection2 = new SqlConnection(SqlConnectionStr);

        string Semcode = LoadCurrentSemester();
        string query1 = "SELECT Dept_Id FROM DEPARTMENT WHERE DEPARTMENT.Name = '" + DeptCode.Text + "';";
        string query2 = "SELECT Course_Id FROM COURSE WHERE COURSE.Course_Code = '" + CrsCode.Text + "';";

        connection1.Open();
        SqlCommand cm1 = new SqlCommand(query1, connection1);
        SqlDataReader reader1 = cm1.ExecuteReader();

        connection2.Open();
        SqlCommand cm2 = new SqlCommand(query2, connection2);
        SqlDataReader reader2 = cm2.ExecuteReader();

        if (reader1.HasRows && reader2.HasRows)
        {
            reader1.Read();
            reader2.Read();
            string dept = reader1.GetValue(reader1.GetOrdinal("Dept_Id")).ToString();
            string crs = reader2.GetValue(reader2.GetOrdinal("Course_Id")).ToString();
            if (Validate(dept, crs, Semcode))
                InsertQuery(dept, crs, Semcode);
            else
            {
                FailLabel.Visible = true;
                SuccessLabel.Visible = false;
                DeptCode.Text = "";
                CrsCode.Text = "";
            }
        }
        if (!reader1.HasRows)
        {
            InvalidDeptCode.Visible = true;
            DeptCode.Text = "";
        }
        if (!reader2.HasRows)
        {
            InvalidCrsCode.Visible = true;
            CrsCode.Text = "";
        }

        cm1.Dispose();
        cm2.Dispose();
        reader1.Dispose();
        reader2.Dispose();
        connection1.Close();
        connection2.Close();
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