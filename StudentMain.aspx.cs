using System;
using System.Activities.Statements;
using System.Activities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

public partial class Student : System.Web.UI.Page
{
    static string User_Id;
 
    protected void Home_Page(SqlDataReader reader)
    {
        StdRollNo.Text = "RollNo : " + reader.GetValue(reader.GetOrdinal("RegNo")).ToString();
        StdDegree.Text = "Degree : " + reader.GetValue(reader.GetOrdinal("Degree")).ToString();
        StdCampus.Text = "Campus : " + reader.GetValue(reader.GetOrdinal("Location")).ToString();
        StdSection.Text = "Section : " + reader.GetValue(reader.GetOrdinal("PSection")).ToString();
        StdBatch.Text = "Batch : " + reader.GetValue(reader.GetOrdinal("BatchNo")).ToString();

        if (reader.GetValue(reader.GetOrdinal("Status")).ToString() == "A")
            StdStatus.Text = "Status : Current";
        else StdStatus.Text = "Status : Current";

        StdName.Text = "Name : " + reader.GetValue(reader.GetOrdinal("First_name")).ToString()
            + " " + reader.GetValue(reader.GetOrdinal("Last_name")).ToString();
        if (reader.GetValue(reader.GetOrdinal("Gender")).ToString() == "M")
            StdGender.Text = "Gender : Male";
        else StdGender.Text = "Gender : Female";

        StdDOB.Text = "DOB : " + reader.GetValue(reader.GetOrdinal("DOB")).ToString();
        StdCNIC.Text = "CNIC : " + reader.GetValue(reader.GetOrdinal("CNIC")).ToString();
        StdEmail.Text = "email : " + reader.GetValue(reader.GetOrdinal("Email")).ToString();
        StdMobile.Text = "Mobile No : " + reader.GetValue(reader.GetOrdinal("Phone")).ToString();


        StdAddress.Text = "Address : " + reader.GetValue(reader.GetOrdinal("Address")).ToString();
        StdCity.Text = "City : " + reader.GetValue(reader.GetOrdinal("City")).ToString();
        StdCountry.Text = "Country : Pakistan";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
            connection.Open();

            string query = "SELECT First_name, Last_name, CNIC, DoB, Gender, USERS.Address, " +
                            "City, RegNo, Email, PSection, BatchNo, Degree, Status, Location, " +
                            "USERS.Phone FROM USERS " +
                            "INNER JOIN USERACCOUNT ON USERS.User_Id = USERACCOUNT.User_Id "+
                            "INNER JOIN STUDENT ON USERS.User_Id = STUDENT.User_id "+
                            "INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = STUDENT.Dept_Id "+
                            "INNER JOIN CAMPUS ON DEPARTMENT.Campus_Id = CAMPUS.Campus_Id "+
                            "WHERE USERS.User_Id = " + User_Id;
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

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {

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