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

public partial class SC5_SubmitFeedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            string teacherFirstName = Request.Form["teacherFirstName"];
            string teacherLastName = Request.Form["teacherLastName"];
            string semester = Request.Form["semester"];
            string fDate = Request.Form["fDate"];
            string roomNo = Request.Form["roomNo"];
            string schedule = Request.Form["schedule"];
            string eval1 = Request.Form["eval1"];
            string eval2 = Request.Form["eval2"];
            string eval3 = Request.Form["eval3"];
            string eval4 = Request.Form["eval4"];
            string eval5 = Request.Form["eval5"];
            string comment = Request.Form["comment"];

            int userID = getTeacherId(teacherFirstName, teacherLastName);
            int studentId = Convert.ToInt32(Request.QueryString["User_Id"]);
            int sectionId = Convert.ToInt32(Request.QueryString["Section_Id"]);

            insertIntoDatabase(userID, studentId, sectionId, Convert.ToInt32(semester), fDate, Convert.ToInt32(roomNo), schedule, eval1, eval2, eval3, eval4, eval5, comment);

            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack)
        {

            List<string> teacherNames = getTeacherNamesFromDatabase();

            teacherFirstNameDropDown.DataSource = teacherNames;
            teacherFirstNameDropDown.DataBind();

            teacherLastNameDropDown.DataSource = teacherNames;
            teacherLastNameDropDown.DataBind();
        }
    }

    private List<string> getTeacherNamesFromDatabase()
    {
        List<string> teacherNames = new List<string>();
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString))
        {
            connection.Open();
            string query = "select USERS.First_name,USERS.Last_name from USERS inner join FACULTY on USERS.User_Id= FACULTY.User_Id";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string firstname = reader.GetString(0);
                    string secondname = reader.GetString(1);
                    string fullname = $"{firstname},{secondname}";
                    teacherNames.Add(fullname);

                }
            }
        }
        return teacherNames;
    }
    private int getTeacherId(string fn, string ln)
    {
        int userId = -1;
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString))
        {
            // Open the connection
            connection.Open();

            // Create a SqlCommand object
            using (SqlCommand command = new SqlCommand("SELECT USER_ID FROM USERS WHERE First_name = @FirstName AND Last_name = @LastName", connection))
            {
                // Add the parameters for first name and last name
                command.Parameters.AddWithValue("@FirstName", fn);
                command.Parameters.AddWithValue("@LastName", ln);

                // Execute the command and retrieve the user ID
                object result = command.ExecuteScalar();

                // Check if a result was returned
                if (result != null)
                {
                    // Parse the user ID from the result
                    userId = (int)result;
                }
            }
        }

        return userId;
    }
    private void insertIntoDatabase(int teacherid, int studentid, int sectionID, int sem, string fdate, int roomNo, string schedule, string eval1, string eval2, string eval3, string eval4, string eval5, string comment)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString))
        {
            // Open the connection
            connection.Open();

            // Create a SqlCommand object with the INSERT statement
            using (SqlCommand command = new SqlCommand("insert into FACULTY values(@teacherID,@studentId,@sectionid,@semester,@fdate,@roomno,@schedule,@eval1,@eval2,@eval3,@eval4,@eval5,@comment)", connection))
            {
                // Add the parameters for first name, last name, and age
                command.Parameters.AddWithValue("@teacherID", teacherid);
                command.Parameters.AddWithValue("@studentid", studentid);
                command.Parameters.AddWithValue("@sectionid", sectionID);
                command.Parameters.AddWithValue("@semester", sem);
                command.Parameters.AddWithValue("@fdate", fdate);
                command.Parameters.AddWithValue("@roomno", roomNo);
                command.Parameters.AddWithValue("@schedule", schedule);
                command.Parameters.AddWithValue("@eval1", eval1);
                command.Parameters.AddWithValue("@eval2", eval2);
                command.Parameters.AddWithValue("@eval3", eval3);
                command.Parameters.AddWithValue("@eval4", eval4);
                command.Parameters.AddWithValue("@eval5", eval5);
                command.Parameters.AddWithValue("@comment", comment);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }
    }

}