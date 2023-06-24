using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class OfferedCourses
{
    public string Course_Id { get; set; }
    public string Course_Code { get; set; }
    public string Course_Name { get; set; }
    public string CrdHrs { get; set; }
    public bool Registered { get; set; }

}
public partial class RegisterCourse : System.Web.UI.Page
{
    static string User_Id;
    static string currSemester;
    static List<OfferedCourses> courses = new List<OfferedCourses>();
    static List<OfferedCourses> Registered = new List<OfferedCourses>();


    private static List<OfferedCourses> GetOfferedCourses(string User_Id)
    {
        courses.Clear();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);

        string query = @"SELECT OfferCourse_Id, C.Course_Code, C.Name, CrdHrs FROM OFFEREDCOURSE O INNER JOIN COURSE C ON C.Course_Id = O.Course_Id 
            INNER JOIN DEPARTMENT D ON D.Dept_Id = O.Dept_Id WHERE O.OfferedIn = '" + currSemester + "'AND O.Dept_Id = (SELECT Dept_Id FROM STUDENT WHERE User_Id = " + 
            User_Id + @") AND NOT EXISTS ( SELECT C.Course_Id FROM PREREQ PR WHERE PR.Course_Id = C.Course_Id AND PR.PreReq_Id NOT IN (SELECT SC.Course_Id FROM STUDENT
            S JOIN TRANSCRIPT T ON S.User_Id = T.Student_Id JOIN SECTION SC ON T.Section_Id = SC.Section_Id WHERE S.User_Id = " + User_Id + " AND T.Percentage >= 50))";

        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            OfferedCourses course = new OfferedCourses();
            course.Course_Id = reader.GetValue(0).ToString();
            course.Course_Name = reader.GetValue(2).ToString();
            course.Course_Code = reader.GetValue(1).ToString();
            course.CrdHrs = reader.GetValue(3).ToString();
            courses.Add(course);
        }
        return courses;
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
    private void LoadCurrentSemester()
    {
        currSemester = ExecuteReader("SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC", "Semester");
    }
    private bool CheckRegAvailable()
    {
        string str = ExecuteReader("SELECT COUNT(*) AS R FROM REGISTERATION WHERE Student_Id = " + User_Id + " AND Semester = '" + currSemester + "'; ", "R");
        if (Convert.ToInt32(str) < 6)
            return true;
        return false;
    }

    protected void OfferedCrsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string CourseID = courses[e.Row.RowIndex].Course_Id;
            CheckBox checkBox = (CheckBox)e.Row.FindControl("chkEnabled");

            if (Registered.Any(x => x.Course_Id == CourseID))
                checkBox.Checked = true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            User_Id = "9";
            if (!CheckRegAvailable())
                UnavailableLabel.Visible = true; 
            LoadCurrentSemester();
            LoadRegisteredCourses();
            courses = GetOfferedCourses(User_Id);
            OfferedCourseList.DataSource = courses;
            OfferedCourseList.DataBind();
        }
        LoadRegisteredCourses();
    }

    private bool ValidRegisteration(OfferedCourses course)
    {
        string query = "SELECT Reg_Id FROM REGISTERATION WHERE Student_Id = " + User_Id +
                       " AND Course_Id = " + course.Course_Id + " AND Semester = '" + currSemester + "';";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        bool res = command.ExecuteScalar() == null;
        connection.Close();
        return res;
    }
    private bool RegisterStudent(OfferedCourses course)
    {
        string query = "INSERT INTO REGISTERATION VALUES (" + User_Id + ", " + course.Course_Id + ", '" + currSemester + "');";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        if (ValidRegisteration(course))
        {
            command.ExecuteNonQuery();
            return true;
        }
        else FailLabel.Visible = true;
        return false;
    }
    protected void LoadRegisteredCourses()
    {
        Registered.Clear();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);

        string query = @"SELECT R.Course_Id, C.Course_Code, C.Name FROM REGISTERATION R INNER JOIN OFFEREDCOURSE O ON 
                        O.OfferCourse_Id = R.Course_Id INNER JOIN COURSE C ON C.Course_Id = O.Course_Id
                        WHERE Student_Id = " + User_Id + " AND Semester = '" + currSemester + "'";

        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            OfferedCourses course = new OfferedCourses();
            course.Course_Id = reader.GetValue(0).ToString();
            course.Course_Name = reader.GetValue(2).ToString();
            course.Course_Code = reader.GetValue(1).ToString();
            Registered.Add(course);
        }
        RegisteredCourses.DataSource = Registered;
        RegisteredCourses.DataBind();
    }
    protected void DeleteRegisteration(string CourseID)
    {
        string query = "DELETE FROM REGISTERATION WHERE Student_Id = " + User_Id + " AND Course_Id = " + CourseID + " AND Semester = '" + currSemester + "'";
        ExecuteNonQuery(query);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in OfferedCourseList.Rows)
        {
            CheckBox checkbox = (CheckBox)row.FindControl("chkEnabled");
            if (checkbox != null && checkbox.Checked)
            {
                OfferedCourses course = courses[row.RowIndex];
                if (RegisterStudent(course))
                    OfferedCourseList.Rows[row.RowIndex].BackColor = System.Drawing.Color.Green;
                else {
                    checkbox.Checked = false;
                    OfferedCourseList.Rows[row.RowIndex].BackColor = System.Drawing.Color.Red;
                }
            }
            else if(!checkbox.Checked)
            {
                string CourseID = courses[row.RowIndex].Course_Id;
                if (Registered.Any(x => x.Course_Id == CourseID))
                   DeleteRegisteration(CourseID);
            }
        }
        Page_Load(null, null);
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

/*        User_Id = Request.QueryString["id"];
        User_Id = "9";
        List<OfferedCourses> courses = GetOfferedCourses(User_Id);
        SqlDataReader reader = GetOfferedCourse(User_Id);
        CoursesRepeater.DataSource = reader;//= //courses;
        CoursesRepeater.DataBind();

        //if (!IsPostBack)
        //{
        //User_Id = Request.QueryString["id"];
        //User_Id = "9";
        //OfferedCourseList.DataSource = GetOfferedCourses(User_Id);
        //OfferedCourseList.DataBind();



    
*/