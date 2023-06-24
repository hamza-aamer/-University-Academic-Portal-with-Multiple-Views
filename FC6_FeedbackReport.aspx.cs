using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


public partial class FC6_FeedbackReport : System.Web.UI.Page
{
    static string User_Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int TeacherID = Convert.ToInt32(User_Id);
        BindGridView(TeacherID);
    }

    protected void BindGridView(int TeacherID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString; // Replace with your actual connection string

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "Select(sum(FEEDBACK.Eval1) / (Count(FEEDBAck.Eval1) * 5) * 100) as EVAL1_Perc,(sum(FEEDBACK.Eval2) / (Count(FEEDBAck.Eval2) * 5) * 100) as EVAL2_Perc,(sum(FEEDBACK.Eval3) / (Count(FEEDBAck.Eval3) * 5) * 100) as EVAL3_Perc,(sum(FEEDBACK.Eval4) / (Count(FEEDBAck.Eval4) * 5) * 100) as EVAL4_Perc,(sum(FEEDBACK.Eval5) / (Count(FEEDBAck.Eval5) * 5) * 100) as EVAL5_Percfrom FEEDBACK group by FEEDBACK.Instructor_Id having FEEDBACK.Instructor_Id = @instructorID";



            ; // Replace with your actual table name

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@instructorID", TeacherID);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                GridView1.DataSource = dataTable;
                GridView1.DataBind();
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
