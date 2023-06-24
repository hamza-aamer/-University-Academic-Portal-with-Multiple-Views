using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AC6_OfferedCoursesReport : System.Web.UI.Page
{
    static string User_Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGridView(GetCurrentSemester());
    }
    protected void BindGridView(string offeredIn)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString; // Replace with your actual connection string

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT COURSE.Course_Code, COURSE.Name, COURSE.CrdHrs " +
                           "FROM OFFEREDCOURSE " +
                           "INNER JOIN COURSE ON OFFEREDCOURSE.Course_Id = COURSE.Course_Id " +
                           "WHERE OFFEREDCOURSE.OfferedIn = @OfferedIn";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@OfferedIn", offeredIn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();

            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
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
    private string GetCurrentSemester()
    {
        return ExecuteReader("SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC", "Semester");
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
}

