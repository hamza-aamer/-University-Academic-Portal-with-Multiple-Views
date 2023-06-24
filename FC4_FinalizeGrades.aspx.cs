using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

public class SectionFin
{
    public int Srno { get; set; }
    public string Section_Name { get; set; }
    public string Section_Id { get; set; }
    public string Course_Code { get; set; }
    public string Course_Name { get; set; }
}
public struct Std
{
    public string Student_Id { get; set; }
    public string Obtained { get; set; }
}
public partial class FC4_FinalizeGrades : System.Web.UI.Page
{
    static string User_Id;
    static string currSemester;
    private static List<SectionFin> sections = new List<SectionFin>();
    private void LoadCourses()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();

        string query = "SELECT Course_Code, COURSE.Name, SECTION.Name, Section_Id FROM SECTION " +
                       "INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = SECTION.Course_Id " +
                       "INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id " +
                       "WHERE SECTION.Instructor_Id = " + User_Id + " AND OfferedIn = '" + currSemester + "'";

        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();
        sections.Clear();

        int rowNum = 1;
        while (reader.Read())
        {
            SectionFin sectionFin = new SectionFin();
            sectionFin.Srno = rowNum++;
            sectionFin.Section_Id = reader.GetValue(3).ToString();
            sectionFin.Section_Name = reader.GetValue(2).ToString();
            sectionFin.Course_Name = reader.GetValue(1).ToString();
            sectionFin.Course_Code = reader.GetValue(0).ToString();
            sections.Add(sectionFin);
        }
        CourseList.DataSource = sections;
        CourseList.DataBind();
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            LoadCurrentSemester();
            LoadCourses();
        }
    }
    private List<Std> getStudentMarksList(string section_Id)
    {
        List<Std> students = new List<Std>();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();

        string query = @"SELECT T.Student_Id, SUM(AbsolutesScored) FROM MARKS M
                INNER JOIN TRANSCRIPT T ON T.Student_Id = M.Student_id
                WHERE T.Section_Id = " + section_Id + " GROUP BY T.Student_Id, T.Section_Id";

        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();

        while (reader.Read())
        {
            Std std = new Std();
            std.Student_Id = reader.GetValue(0).ToString();
            std.Obtained = reader.GetValue(1).ToString();

            students.Add(std);
        }
        return students;
    }
    private string UpdatePercentageQuery(List<Std> Students, string section_Id)
    {
        string res = "";
        string Ins = "UPDATE TRANSCRIPT Set Percentage = CASE ";

        for (int i = 0; i < Students.Count; i++)
            res += "WHEN Student_Id = " + Students[i].Student_Id + " THEN " + Students[i].Obtained + '\n';

        if (res != "")
            res = Ins + res + "END WHERE Section_Id = " + section_Id;
        return res;
    }
    private string PercentageToGrade(string percentage)
    {
        double percent = Convert.ToDouble(percentage);
        if (percent >= 90)
            return "A+";
        else if (percent > 85)
            return "A";
        else if (percent > 81)
            return "A-";
        else if (percent > 77)
            return "B+";
        else if (percent > 73)
            return "B";
        else if (percent >= 69)
            return "B-";
        else if (percent >= 65)
            return "C+";
        else if (percent >= 61)
            return "C";
        else if (percent >= 57)
            return "C-";
        else if (percent >= 53)
            return "D+";
        else if (percent >= 49)
            return "D";
        else
            return "F";
    }
    private string UpdateGradesQuery(List<Std> Students, string section_Id)
    {
        string res = "";
        string Ins = "UPDATE TRANSCRIPT Set Grade = CASE ";

        for (int i = 0; i < Students.Count; i++)
            res += "WHEN Student_Id = " + Students[i].Student_Id + " THEN '" + PercentageToGrade(Students[i].Obtained) + "'\n";

        if (res != "")
            res = Ins + res + "END WHERE Section_Id = " + section_Id;
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
    private void UpdateSectionGrades(string section_Id)
    {
        List<Std> students = getStudentMarksList(section_Id);
        string query = UpdatePercentageQuery(students, section_Id);
        ExecuteNonQuery(query);
        query = UpdateGradesQuery(students, section_Id);
        ExecuteNonQuery(query);
    }
    protected void CourseList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Confirm")
        {
            int id = Convert.ToInt32(e.CommandArgument) - 1;
            UpdateSectionGrades(sections[id].Section_Id);
            MessageBox.Show("Grades Updated Successfully");
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