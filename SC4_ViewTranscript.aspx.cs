using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


public class Grade
{
    public string Course_Code { get; set; }
    public string Course_Name { get; set; }
    public string SectionName { get; set; }
    public int CrdHrs { get; set; }
    public string letter { get; set; }
    public double points { get; set; }
    public void SetPoints()
    {
        points = PointsTable(letter);
    }
    private double PointsTable(string ch)
    {
        if (ch == "A+" || ch == "A")
            return 4;
        else if (ch == "A-")
            return 3.67;
        else if (ch == "B+")
            return 3.33;
        else if (ch == "B")
            return 3;
        else if (ch == "B-")
            return 2.67;
        else if (ch == "C+")
            return 2.33;
        else if (ch == "C")
            return 2;
        else if (ch == "C-")
            return 1.7;
        else if (ch == "D+")
            return 2.67;
        else if (ch == "D")
            return 1;
        else if (ch == "D-")
            return 0.67;
        else return 0;
    }
}


public class Semester
{
    public int TotalCredits { set; get; }
    public double SGPA { set; get; }
    public string semester { set; get; }
    public List<Grade> grades { set; get; }
    public Semester(string sem)
    {
        SGPA = 0;
        semester = sem;
        grades = new List<Grade>();
    }
    public void SetPoints()
    {
        foreach(Grade grade in grades)
            grade.SetPoints();
    }
    public void CalculateGPA()
    {
        SGPA = 0;
        TotalCredits = 0;
        foreach (Grade g in grades)
        {
            SGPA += g.CrdHrs * g.points;
            TotalCredits += g.CrdHrs;
        }
        SGPA /= TotalCredits;
    }
}
public partial class SC4_ViewTranscript : System.Web.UI.Page
{
    private static string User_Id;
    private static List<Semester> gradeReport;
    private string EvalGrade(object obj)
    {
        if (obj == null)
            return "I";
        return obj.ToString();
    }
    private void LoadGrades()
    {
        string query = @"SELECT O.OfferedIn, C.Course_Code, C.Name AS CN, S.Name AS SN, C.CrdHrs, T.Grade 
                        FROM TRANSCRIPT T
                        INNER JOIN SECTION S ON S.Section_Id = T.Section_Id
                        INNER JOIN OFFEREDCOURSE O ON O.OfferCourse_Id = S.Course_Id
                        INNER JOIN COURSE C ON C.Course_Id = O.Course_Id
                        WHERE Student_Id = " + User_Id + " ORDER BY OfferedIn ASC";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        string prevSem = "", nextSem;
        gradeReport = new List<Semester>();
        while (reader.Read())
        {
            Grade g = new Grade();
            nextSem = reader.GetString(0);
            if (nextSem != prevSem)
                gradeReport.Add(new Semester(nextSem));

            g.Course_Code = reader.GetString(1);
            g.Course_Name = reader.GetString(2);
            g.SectionName = reader.GetString(3);
            g.CrdHrs = Convert.ToInt32(reader.GetValue(4));
            g.letter = EvalGrade(reader.GetValue(5));

            gradeReport.Last().grades.Add(g);
        }

        foreach(Semester sem in gradeReport)
        {
            sem.SetPoints();
            sem.CalculateGPA();
        }
    }
    private void GenerateTranscripts()
    {
        foreach(Semester semester in gradeReport) {
            GridView gridview = new GridView();
            gridview.AutoGenerateColumns = false;

            BoundField Col1CourseCode = new BoundField();
            Col1CourseCode.DataField = "Course_Code";
            Col1CourseCode.HeaderText = "Course Code";
            Col1CourseCode.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


            BoundField Col2CourseName = new BoundField();
            Col2CourseName.DataField = "Course_Name";
            Col2CourseName.HeaderText = "Course Title";
            Col2CourseName.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


            BoundField Col3SectionName = new BoundField();
            Col3SectionName.DataField = "SectionName";
            Col3SectionName.HeaderText = "Section";
            Col3SectionName.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


            BoundField Col4CrdHrs = new BoundField();
            Col4CrdHrs.DataField = "CrdHrs";
            Col4CrdHrs.HeaderText = "Credits";
            Col4CrdHrs.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


            BoundField Col5Grade = new BoundField();
            Col5Grade.DataField = "letter";
            Col5Grade.HeaderText = "Grade";
            Col5Grade.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


            BoundField Col6Points = new BoundField();
            Col6Points.DataField = "points";
            Col6Points.HeaderText = "Points";
            Col6Points.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


            gridview.Columns.Add(Col1CourseCode);
            gridview.Columns.Add(Col2CourseName);
            gridview.Columns.Add(Col3SectionName);
            gridview.Columns.Add(Col4CrdHrs);
            gridview.Columns.Add(Col5Grade);
            gridview.Columns.Add(Col6Points);

            gridview.Width = new Unit("80%");
            gridview.HorizontalAlign = HorizontalAlign.Center;
            gridview.DataSource = semester.grades;
            gridview.DataBind();
            gridview.CssClass = "TranscriptList";
            gridview.HeaderRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#006699");
            gridview.HeaderRow.ForeColor = System.Drawing.Color.White;


            System.Web.UI.WebControls.Label HeaderLabel = new System.Web.UI.WebControls.Label();
            HeaderLabel.Text = semester.semester;
            HeaderLabel.Width = new Unit("100%");
            HeaderLabel.Height = 50;
            HeaderLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#3366FF");
            HeaderLabel.ForeColor = System.Drawing.Color.White;
            HeaderLabel.Font.Size = FontUnit.Large;

            System.Web.UI.WebControls.Label GPALabel = new System.Web.UI.WebControls.Label();
            GPALabel.Text = "SGPA : " + semester.SGPA.ToString() + '\n';
            GPALabel.Font.Size = FontUnit.Large;
            GPALabel.CssClass = "TranscriptInfo";

            System.Web.UI.WebControls.Label TotalCredits = new System.Web.UI.WebControls.Label();
            TotalCredits.Text = "Total Crds. " + semester.TotalCredits.ToString() + '\n';
            TotalCredits.Font.Size = FontUnit.Large;
            TotalCredits.CssClass = "TranscriptInfo";


            TranscriptPanel.Controls.Add(HeaderLabel);
            TranscriptPanel.Controls.Add(GPALabel);
            TranscriptPanel.Controls.Add(new LiteralControl("<br/>"));
            TranscriptPanel.Controls.Add(TotalCredits);
            TranscriptPanel.Controls.Add(new LiteralControl("<br/><br>"));
            TranscriptPanel.Controls.Add(gridview);
            TranscriptPanel.Controls.Add(new LiteralControl("<br/><br>"));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        User_Id = Request.QueryString["id"];
        LoadGrades();
        GenerateTranscripts(); 
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