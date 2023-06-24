using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AC8_StudentSectionsReport : System.Web.UI.Page
{
    static string User_Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            // Populate the dropdown list on initial page load
            PopulateCoursesDropDown();
        }
    }

    protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourses.SelectedIndex == 0)
            return;
        // Retrieve the selected course number from the dropdown
        string courseNumber = ddlCourses.SelectedValue;

        // Fetch the result from the SQL Server based on the selected course number
        DataTable result = GetGradeCount(courseNumber);

        // Bind the result to the GridView control for display
        gvResults.DataSource = result;
        gvResults.DataBind();
    }

    private void PopulateCoursesDropDown()
    {
        // Connect to the SQL Server and retrieve the list of course numbers
        DataTable courses = GetCourseNumbers();

        // Bind the course numbers to the dropdown list
        ddlCourses.DataSource = courses;
        ddlCourses.DataTextField = "BatchNo";
        ddlCourses.DataValueField = "BatchNo";
        ddlCourses.DataBind();
    }

    private DataTable GetCourseNumbers()
    {
        // Replace "YourConnectionString" with your actual SQL Server connection string
        string connectionString = ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "select '-' as BatchNo union select distinct BatchNo from STUDENT";

            using (SqlCommand command = new SqlCommand(query, connection))

            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                DataTable courses = new DataTable();
                courses.Load(reader);

                return courses;
            }
        }
    }

    private DataTable GetGradeCount(string BatchNumber)
    {
        // Replace "YourConnectionString" with your actual SQL Server connection string
        string connectionString = ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT USERS.First_name, USERS.Last_name, STUDENT.PSection,USERACCOUNT.RegNo FROM USERS INNER JOIN STUDENT ON USERS.User_Id = STUDENT.User_Id inner join USERACCOUNT on USERACCOUNT.User_Id= Users.User_Id WHERE STUDENT.Status = 'A' and Student.BatchNo = @BatchNo GROUP BY STUDENT.PSection,Users.First_name,Users.Last_name,USERACCOUNT.RegNo;";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BatchNo", BatchNumber);
                connection.Open();

                DataTable result = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(result);

                return result;
            }
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
