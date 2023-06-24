using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


public partial class FC5_GradeReport : System.Web.UI.Page
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
        int teacherid = Convert.ToInt32(User_Id);
        // Connect to the SQL Server and retrieve the list of course numbers
        DataTable courses = GetCourseNumbers(teacherid);

        // Bind the course numbers to the dropdown list
        ddlCourses.DataSource = courses;
        ddlCourses.DataTextField = "CourseNumber";
        ddlCourses.DataValueField = "Course_Id";
        ddlCourses.DataBind();
    }

    private DataTable GetCourseNumbers(int TeacherID)
    {
        // Replace "YourConnectionString" with your actual SQL Server connection string
        string connectionString = ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "Select Section.Course_Id from SECTION inner join COURSE on COURSE.Course_Id = SECTION.Course_Id where Instructor_Id = @instructorId";

            using (SqlCommand command = new SqlCommand(query, connection))

            {
                command.Parameters.AddWithValue("@instructorId", TeacherID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                DataTable courses = new DataTable();
                courses.Load(reader);

                return courses;
            }
        }
    }

    private DataTable GetGradeCount(string courseNumber)
    {
        // Replace "YourConnectionString" with your actual SQL Server connection string
        string connectionString = ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "select Grade ,count(Grade) as Total_Grades from TRANSCRIPT inner join Section on TRANSCRIPT.Section_Id=SECTION.Section_Id inner join OFFEREDCOURSE on OFFEREDCOURSE.OfferCourse_Id =Section.Course_Id group by Grade having OFFEREDCOURSE.Course_Id=@CourseNumber";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CourseNumber", courseNumber);
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
