using Microsoft.SqlServer.Server;
using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel.Activities;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


public class StudentMark
{
    public int Srno { get; set; }
    public string RegNo { get; set; }
    public string User_Id { get; set; }
    public string Student_Name { get; set; }
}
public class SectionMarksInfo
{
    public string Name { get; set; }
    public string Id { get; set; }
}
public partial class FC3_MarkEvaluations : System.Web.UI.Page
{
    static string User_Id;
    static string currSemester;
    static List<StudentMark> Students;
    static List<string> CourseIds = new List<string>();
    static List<SectionMarksInfo> Section_Ids = new List<SectionMarksInfo>(); 
    static List<string> EvalIds = new List<string>();

    private void getSectionList()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();

        string query = "SELECT '-'AS Name, '-' AS Section_Id UNION SELECT Name, Section_Id FROM SECTION WHERE Course_Id = " + CourseIds[CourseList.SelectedIndex] + " AND Instructor_Id = " + User_Id;
        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();

        Section_Ids.Clear();

        while (reader.Read())
        {
            SectionMarksInfo s = new SectionMarksInfo();
            s.Name = reader.GetString(reader.GetOrdinal("Name"));
            s.Id = reader.GetValue(reader.GetOrdinal("Section_Id")).ToString();
            Section_Ids.Add(s);
        }
    }
    private void LoadSections()
    {
        getSectionList();
        SectionList.Items.Clear();

        foreach (SectionMarksInfo S in Section_Ids)
            SectionList.Items.Add(S.Name);

        SectionList.DataBind();
    }
    private void LoadCourses()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();

        string query = "SELECT '-', '-' AS Code UNION SELECT DISTINCT OfferCourse_Id, CONCAT(Course_Code, ' - ', COURSE.Name) AS Code FROM SECTION " +
                       "INNER JOIN OFFEREDCOURSE ON OFFEREDCOURSE.OfferCourse_Id = SECTION.Course_Id " +
                       "INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id " +
                       "WHERE SECTION.Instructor_Id = " + User_Id + " AND OfferedIn = '" + currSemester + "'";

        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();
        CourseList.Items.Clear();
        CourseIds.Clear();

        while (reader.Read())
        {
            CourseIds.Add(reader.GetValue(0).ToString());
            CourseList.Items.Add(reader.GetString(1));
        }
        CourseList.DataBind();
        if(CourseList.Items.Count > 0)  
            LoadEvaluations();
    }
    private void LoadEvaluations()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();

        string query = "SELECT '-' AS Name, 0 AS Eval_Id UNION SELECT Name, Eval_Id FROM EVALUATION Where course_Id = " + CourseIds[CourseList.SelectedIndex] + " ORDER BY Name ";

        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();
        EvaluationList.Items.Clear();
        EvalIds.Clear();

        while (reader.Read())
        {
            EvaluationList.Items.Add(reader.GetString(0));
            EvalIds.Add(reader.GetValue(1).ToString());
        }
        EvaluationList.DataBind();
    }
    private bool StudentEvalsInserted()
    {
        return ExecuteScalar("SELECT Student_Id FROM MARKS WHERE Student_Id = " + Students[0].User_Id + " AND Eval_Id = " + EvalIds[1]);
    }
    private void ExecStudentEvalInsertion()
    {
        string query = ConstructStudentInsertQuery();
        if (query != "")
            ExecuteNonQuery(query);
    }

    private void LoadStudentSheet()
    {
        string query = @"SELECT U.User_Id AS ID, RegNo, CONCAT(First_Name, ' ', Last_Name) AS Student_Name FROM USERS U INNER JOIN
                        USERACCOUNT A ON U.User_Id = A.User_Id INNER JOIN TRANSCRIPT T ON T.Student_Id = U.User_Id INNER JOIN SECTION S
                        ON S.Section_Id = T.Section_Id WHERE S.Section_Id = " + Section_Ids[SectionList.SelectedIndex].Id;

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();
        Students = new List<StudentMark>();
        //if (!reader.HasRows)
        //    NoStdExistLabel.Visible = true;
        //else NoStdExistLabel.Visible = false;

        int StudentSrNo = 1;
        while (reader.Read())
        {
            StudentMark std = new StudentMark();
            std.Srno = StudentSrNo++;
            std.User_Id = reader["ID"].ToString();
            std.RegNo = reader["RegNo"].ToString();
            std.Student_Name = reader["Student_Name"].ToString();
            Students.Add(std);
        }
        if (Students.Count > 0 && !StudentEvalsInserted())
            ExecStudentEvalInsertion();
        SectionStudentsList.DataSource = Students;
        SectionStudentsList.DataBind();
    }
    private string ConstructStudentInsertQuery()
    {
        string res = "", EvalId = EvalIds[EvaluationList.SelectedIndex];
        string Ins = "INSERT INTO MARKS (Student_Id, Eval_Id) VALUES ";

        for (int i = 0; i < Students.Count; i++)
            res += "(" + Students[i].User_Id + ", " + EvalId + "),";
        res = res.Remove(res.Length - 1, 1);
        if (res != "")
            res = Ins + res;
        return res;
    }
    private string ConstructUpdateQuery()
    {
        string res = "";
        string Ins = "UPDATE MARKS Set Obtained = CASE ";

        foreach (GridViewRow row in SectionStudentsList.Rows)
        {
            System.Web.UI.WebControls.TextBox textBox = row.FindControl("MarksTxt") as System.Web.UI.WebControls.TextBox;
            if (textBox != null)
                res += "WHEN Student_Id = " + Students[row.RowIndex].User_Id + " THEN " + textBox.Text + '\n';
        }
        if(res != "")
            res = Ins + res + " END WHERE Eval_Id = " + EvalIds[EvaluationList.SelectedIndex];
        return res;
    }
    private string ConstructAbsQuery()
    {
        return "UPDATE MARKS SET AbsolutesScored = Obtained / " + EvalRange.Text + "*" + EvalWeightage.Text 
            + "WHERE Eval_Id = " + EvalIds[EvaluationList.SelectedIndex];
    }
    private void ExecuteUpdateQuery()
    {
        string query = ConstructUpdateQuery();
        if (query == "")
            return;
        ExecuteNonQuery(query);
        query = ConstructAbsQuery();
        ExecuteNonQuery(query);
        SuccessLabel0.Visible = true;
    }
    public bool ExecuteScalar(string query)
    {;
        bool res = false;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
            res = true;
        connection.Close();
        return res;
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
    protected void CourseList_TextChanged(object sender, EventArgs e)
    {
        LoadSections();
    }
    protected void SectionList_TextChanged(object sender, EventArgs e)
    {
        LoadEvaluations();
    }
    private void ShowEvaluationDetails()
    {
        string query = "SELECT Name, Weightage, Range FROM EVALUATION WHERE Eval_Id = " + EvalIds[EvaluationList.SelectedIndex];
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            EvalDetails.Visible = true;
            EvalName.Text = reader.GetValue(0).ToString();
            EvalWeightage.Text = reader.GetValue(1).ToString();
            EvalRange.Text = reader.GetValue(2).ToString();
            TriggerMarksUpdation.Visible = true;
        }
        reader.Close();
        connection.Close();
    }
    protected void EvalList_TextChanged(object sender, EventArgs e)
    {
        ShowEvaluationDetails();
        LoadStudentSheet();
    }
    private void LoadCurrentSemester()
    {
        currSemester = ExecuteReader ("SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC", "Semester");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            LoadCurrentSemester();
            LoadCourses();
        }
    }
    private bool IsAllNum(string str)
    {
        bool Decimal = false;
        for (int i = 0; i < str.Length; i++)
        {
            if ((str[i] < '0' || str[i] > '9') && str[i] != '.')
                return false;
            if (str[i] == '.' && !Decimal)
                Decimal = true;
            else return false;
        }
        return true;
    }
    private bool ValidateInputs()
    {
        bool Valid = true;
        int maxNumber = Convert.ToInt32(EvalRange.Text);
        float inputNumber;
        foreach (GridViewRow row in SectionStudentsList.Rows)
        {
            System.Web.UI.WebControls.TextBox textBox = row.FindControl("MarksTxt") as System.Web.UI.WebControls.TextBox;
            if (textBox != null)
                if (!float.TryParse(textBox.Text, out inputNumber) || inputNumber >= maxNumber)
                {
                    textBox.Text = "";
                    Valid = false;
                }
        }
        return Valid;
    }
    protected void CrsCrtButton_Click(object sender, EventArgs e)
    {
        if (ValidateInputs())
            ExecuteUpdateQuery();
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