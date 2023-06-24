using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty : System.Web.UI.Page
{
    static string User_Id;
    protected void Home_Page(SqlDataReader reader)
    {
        FclUsername.Text = "Username : " + reader.GetValue(reader.GetOrdinal("Username")).ToString();
        FclName.Text = "Name : " + reader.GetValue(reader.GetOrdinal("First_name")).ToString()
            + " " + reader.GetValue(reader.GetOrdinal("Last_name")).ToString();
        if (reader.GetValue(reader.GetOrdinal("Gender")).ToString() == "M")
            FclGender.Text = "Gender : Male";
        else FclGender.Text = "Gender : Female";
        FclDept.Text = "Department : " + reader.GetValue(reader.GetOrdinal("Name")).ToString();

        FclDOB.Text = "DOB : " + reader.GetValue(reader.GetOrdinal("DOB")).ToString();
        FclCNIC.Text = "CNIC : " + reader.GetValue(reader.GetOrdinal("CNIC")).ToString();

        FclAddress.Text = "Address : " + reader.GetValue(reader.GetOrdinal("Address")).ToString();
        FclEmail.Text = "Email : " + reader.GetValue(reader.GetOrdinal("Email")).ToString();
        FclMobile.Text = "Mobile No : " + reader.GetValue(reader.GetOrdinal("Phone")).ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
            connection.Open();


            string query =  "SELECT First_name, Last_name, Name, CNIC, DOB, Gender, Address, " +
                            "USERS.Phone, Username, Email, JobTitle, Salary FROM FACULTY " +
                            "INNER JOIN USERS ON FACULTY.User_Id = USERS.User_Id " +
                            "INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = FACULTY.User_Id " +
                            "LEFT JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = FACULTY.Dept_Id " +
                            "WHERE FACULTY.User_Id = " + User_Id;
            SqlCommand cm = new SqlCommand(query, connection);
            SqlDataReader reader = cm.ExecuteReader();

            reader.Read();
            Home_Page(reader);
            //Page.DataBind();

            cm.Dispose();
            reader.Dispose();
            connection.Close();
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