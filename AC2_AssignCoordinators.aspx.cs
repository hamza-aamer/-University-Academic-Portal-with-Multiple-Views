using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

public class CourseCoordinator
{
    public int Course_Id { get; set; }
    public string Course_Code { get; set; }
    public string Dept_Code { get; set; }
    public string Coordinator { get; set; }
}
public partial class AssignCoordinator : System.Web.UI.Page
{
    static string User_Id;
    static string semester;
    static List<CourseCoordinator> courses = new List<CourseCoordinator>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            LoadCurrentSemester();
        }
        Control(User_Id, semester);
    }
    private void LoadCurrentSemester()
    {
        semester = ExecuteReader("SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC", "Semester");
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
    public string ExecuteScalar(string query, string Column)
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
    private List<CourseCoordinator> GetOfferedCourses(string User_Id)
    {
        courses.Clear();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);

        string query = "SELECT OfferCourse_Id, Course_Code, DEPARTMENT.Name AS Dept_Code, " +
                        "CONCAT(First_name, ' ', Last_name) AS Coordinator FROM OFFEREDCOURSE " +
                        "INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = OFFEREDCOURSE.Dept_Id " +
                        "INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id " +
                        "LEFT JOIN USERS ON OFFEREDCOURSE.Coordinator_Id = USERS.User_Id " +
                        "WHERE OfferedIn = '" + semester + "' AND DEPARTMENT.Campus_Id = (" +
                        "SELECT Campus_Id FROM ADMIN WHERE User_Id = " + User_Id + ") ";

        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            CourseCoordinator course = new CourseCoordinator();
            course.Course_Id = Convert.ToInt32(reader["OfferCourse_Id"]);
            course.Dept_Code = reader["Dept_Code"].ToString();
            course.Course_Code = reader["Course_Code"].ToString();
            course.Coordinator = reader["Coordinator"].ToString();
            courses.Add(course);
        }
        connection.Close();
        reader.Close();
        return courses;
    }
    private void InitInstructors(string Section_Id)
    {
        string query =  "SELECT CONCAT(First_name,' ',Last_name,' (',Username,') ') AS UN " +
                        "FROM FACULTY INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id " +
                        "INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = FACULTY.User_Id " +
                        "WHERE FACULTY.Dept_Id IN (SELECT DEPARTMENT.Dept_Id FROM CAMPUS INNER JOIN " +
                        "ADMIN ON ADMIN.Campus_Id = CAMPUS.Campus_Id INNER JOIN DEPARTMENT " +
                        "ON DEPARTMENT.Campus_Id = CAMPUS.Campus_Id WHERE ADMIN.User_Id = " +
                        User_Id + ") AND FACULTY.JobTitle LIKE '%Professor' " +
                        "AND FACULTY.User_Id NOT IN (SELECT Coordinator_Id FROM OFFEREDCOURSE " +
                        "WHERE OfferedIn = '" + semester + "' AND Coordinator_Id  IS NOT NULL)";

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();

        CoordCode.Items.Clear();
        while (reader.Read())
            CoordCode.Items.Add(reader.GetString(0));

        connection.Close();
        reader.Close();
    }
    private bool ValidateRefNo(string Id)
    {
        foreach (GridViewRow row in CourseCoordinatorList.Rows)
            if (row.Cells[0].Text == Id)
                if (row.Cells[3].Text == " ")
                    return true;
                else break;
        return false;
    }
    protected void RefNo_TextChanged(object sender, EventArgs e)
    {
        if (ValidateRefNo(Course_Id.Text))
            InitInstructors(Course_Id.Text);
    }
    private void Control(string User_Id, string semester)
    {
        courses = GetOfferedCourses(User_Id);
        CourseCoordinatorList.DataSource = courses;
        CourseCoordinatorList.DataBind();
    }
    private string GetUsername(string Selection)
    {
        int startIdx = -1, length = 0;
        for (int i = 0; i < Selection.Length; i++)
        {
            if (Selection[i] == ')')
                break;
            if (Selection[i] == '(')
                startIdx = i + 1;
            else if (startIdx != -1)
                length++;
        }
        return Selection.Substring(startIdx, length);
    }
    protected void CrsCrtButton_Click(object sender, EventArgs e)
    {
        if (CoordCode.Items.Count == 0 || !ValidateRefNo(Course_Id.Text))
        {
            SuccessLabel.Visible = false;
            FailLabel.Visible = true;
            return;
        }

        string CoordinatorName = '\'' + GetUsername(CoordCode.Text) + '\'';
        string query = "SELECT User_Id FROM USERACCOUNT WHERE Username = " + CoordinatorName;
        string CoordinatorId = ExecuteScalar(query, "User_Id");
        string Insquery = "UPDATE OFFEREDCOURSE SET Coordinator_Id = " + CoordinatorId +
                          " WHERE OfferCourse_Id = " + Course_Id.Text;

        if (ExecuteNonQuery(Insquery) == 1)
        {
            SuccessLabel.Visible = true;
            Course_Id.Text = "";
            CoordCode.Items.Clear();
            LogEvent(Convert.ToInt32(User_Id), "Assigned as Coordinator for a Course");
            Page_Load(null, null);
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