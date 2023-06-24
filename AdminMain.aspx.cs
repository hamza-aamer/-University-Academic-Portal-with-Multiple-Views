using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.Page
{
    protected static string User_Id;
    protected void Home_Page(SqlDataReader reader)
    {
        AdmUsername.Text = "Username : " + reader.GetValue(reader.GetOrdinal("Username")).ToString();
        AdmName.Text = "Name : " + reader.GetValue(reader.GetOrdinal("First_name")).ToString()
            + " " + reader.GetValue(reader.GetOrdinal("Last_name")).ToString();
        if (reader.GetValue(reader.GetOrdinal("Gender")).ToString() == "M")
            AdmGender.Text = "Gender : Male";
        else AdmGender.Text = "Gender : Female";

        AdmDOB.Text = "DOB : " + reader.GetValue(reader.GetOrdinal("DOB")).ToString();
        AdmCNIC.Text = "CNIC : " + reader.GetValue(reader.GetOrdinal("CNIC")).ToString();

        AdmAddress.Text = "Address : " + reader.GetValue(reader.GetOrdinal("Address")).ToString();
        AdmEmail.Text = "Email : " + reader.GetValue(reader.GetOrdinal("Email")).ToString();
        AdmMobile.Text = "Mobile No : " + reader.GetValue(reader.GetOrdinal("Phone")).ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
            connection.Open();

            string query = "SELECT First_name, Last_name, CNIC, DOB, Gender, Address, " +
                            "Phone, Username, Email, JobTitle, Salary FROM ADMIN " +
                            "INNER JOIN USERS ON ADMIN.User_Id = USERS.User_Id " +
                            "INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = ADMIN.User_Id " +
                            "WHERE ADMIN.User_Id = " + User_Id;
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

    protected void HyperLink1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AC1_CourseOffer.aspx?id=" + User_Id);
    }
    protected void HyperLink2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AC2_AssignCoordinators.aspx?id=" + User_Id);
    }
    protected void HyperLink3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AC3_CourseAllocation.aspx?id=" + User_Id);
    }
    protected void HyperLink7_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AC4_UserRegisteration.aspx?id=" + User_Id);
    }
    protected void HyperLink4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AC5_AuditTrail.aspx?id=" + User_Id);
    }
    protected void HyperLink5_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AC6_OfferedCoursesReport.aspx?id=" + User_Id);
    }
    protected void HyperLink6_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AC7_CourseAllocationReport.aspx?id=" + User_Id);
    }
    protected void HyperLink8_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AC8_StudentSectionReport.aspx?id=" + User_Id);
    }
}