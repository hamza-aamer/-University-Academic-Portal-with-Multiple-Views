using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class FC7_TranscriptReport : System.Web.UI.Page
{

    static string User_Id;
    static int instructorId;
    protected int Courseid
    {
        get { return (int)(Session["Courseid"] ?? 0); }
        set { Session["Courseid"] = value; }
    }

    protected DataTable GetCoursesForInstructor()
    {
        DataTable courseData = new DataTable();

        // Connection string
        string connectionString = ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString;

        // SQL query to retrieve course ID, course code, and section name for the instructor
        string query = @"
        SELECT DISTINCT O.Course_Id, C.Course_Id AS CourseId, CONCAT(C.Course_Code, ' - ' , C.Name , ' - ' , C.Course_Id) AS CourseDisplay
        FROM OFFEREDCOURSE O
        JOIN COURSE C ON O.Course_Id = C.Course_Id
        JOIN SECTION S ON O.OfferCourse_Id = S.Course_Id
        WHERE S.Instructor_Id = @InstructorId
    ";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create the SQL command
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InstructorId", instructorId);

            // Open the connection and execute the query
            connection.Open();

            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                // Fill the DataTable with the result of the query
                adapter.Fill(courseData);
            }
        }

        return courseData;
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            User_Id = "2";
            instructorId = Convert.ToInt32(User_Id);
            // Fetch the course ID, course name, and section name for the instructor
            DataTable courseData = GetCoursesForInstructor();

            // Bind the course data to the dropdown list
            DropDownList1.DataSource = courseData;
            DropDownList1.DataTextField = "CourseDisplay"; // Replace "CourseDisplay" with the desired column name for displaying course ID and section name
            DropDownList1.DataValueField = "Course_Id"; // Replace "Course_Id" with the actual column name for the course ID in your DataTable
            DropDownList1.DataBind();
            string selectedText = DropDownList1.SelectedItem.Text;

            Courseid = GetLastNumberAfterDash(selectedText);
            string course = Convert.ToString(Courseid);
            DataTable update = GetDataFromDatabase(String.Empty, course);
            GridView1.DataSource = update;
            GridView1.DataBind();
        }
    }


    private DataTable GetDataFromDatabase(string filter,string Course)
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(@"SELECT CONCAT( u.First_name, ' ' , u.Last_name) AS 'Full Name' , UA.RegNo as 'Roll Number', t.Grade, T.Percentage
                    FROM Transcript t
                    INNER JOIN Section s ON t.section_id = s.section_id
                    INNER JOIN OfferedCourse oc ON s.course_id = oc.offercourse_id
                    INNER JOIN Course c ON oc.course_id = c.course_id
                    INNER JOIN Users u ON t.student_id = u.user_id
                    INNER JOIN USERACCOUNT UA ON u.user_id =UA.user_id 
                    WHERE c.course_id = '" + Course + "';", con))
            {
                cmd.Parameters.AddWithValue("@Filter", "%" + filter + "%");
                con.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }

        return dt;
    }
    private int GetLastNumberAfterDash(string input)
    {
        // Split the string by '-' delimiter
        string[] parts = input.Split('-');

        // Get the last part of the split string
        string lastPart = parts[parts.Length - 1];

        // Remove any leading or trailing whitespace
        lastPart = lastPart.Trim();

        // Parse the last part as an integer
        if (int.TryParse(lastPart, out int result))
        {
            return result;
        }
        else
        {
            // Return a default value or throw an exception, depending on your requirements
            throw new InvalidOperationException("Invalid format. Unable to parse the last number.");
        }
    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedText = DropDownList1.SelectedItem.Text;

        Courseid = GetLastNumberAfterDash(selectedText);
        string course = Convert.ToString(Courseid);
        DataTable update = GetDataFromDatabase(String.Empty, course);
        GridView1.DataSource = update;
        GridView1.DataBind();

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