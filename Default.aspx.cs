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


public partial class LogIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();

        SqlCommand cm;
        string username = TextBox1.Text;
        string password = TextBox2.Text;

        string query = "SELECT USERACCOUNT.User_Id, USERACCOUNT.Type" +
            " FROM USERACCOUNT WHERE USERACCOUNT.Password = '" + password +
            "' AND((USERACCOUNT.RegNo IS NULL AND USERACCOUNT.Username = '" + username +
            "') OR(USERACCOUNT.Username IS NULL AND USERACCOUNT.RegNo = '" + username + "'))";

        cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();
        if(reader.HasRows)
        {
            reader.Read();
            int user_Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("User_Id")).ToString());
            string result = reader.GetValue(reader.GetOrdinal("Type")).ToString();

            switch (result[result.Length - 1])
            {
                case 'A':
                    Response.Redirect("~/AdminMain.aspx?id=" + user_Id);
                    break;
                case 'S':
                    Response.Redirect("~/StudentMain.aspx?id=" + user_Id);
                    break;
                case 'F':
                    Response.Redirect("~/FacultyMain.aspx?id=" + user_Id);
                    break;
            }
        }
        else
            LoginErrorLabel.Visible = true;

        cm.Dispose();
        reader.Dispose();
        connection.Close();

        

    }
}