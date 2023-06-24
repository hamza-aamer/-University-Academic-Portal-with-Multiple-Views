using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


public class Section
{
    public string Course_Id { get; set; }
    public int TotalRegistered { get; set; }
    public int NoOfSections { get; set; }
}

public partial class CourseAllocation : System.Web.UI.Page
{
    static int MaxLimit = 20;
    static int MinLimit = 1;
    static string User_Id;
    static string semester;
    public void SectionInsert(List<Section> sections, string semester)
    {
        char sectionName;
        string query = "INSERT INTO SECTION (Course_Id, Name) VALUES \n";
        foreach (Section section in sections)
        {
            sectionName = 'A';
            for (int i = 0; i < section.NoOfSections; i++, sectionName++)
                query = query + "(" + section.Course_Id + ",'" + sectionName + "'),";
        }
        query = query.Remove(query.Length - 1, 1);
        int SectionsAdded = ExecuteNonQuery(query);
        LogEvent(Convert.ToInt32(User_Id), "Created Sections " + SectionsAdded.ToString() +" For Course");
    }
    public void CreateSections(string semester)
    {
        List<Section> sections = GetSections(semester);
        sections = GetNoOfSectionsPerCourse(sections);
        SectionInsert(sections, semester);
        Page_Load(null, null);
    }
    public List<Section> GetNoOfSectionsPerCourse(List<Section> sections)
    {
        int sectionCount, Extra;
        foreach (Section section in sections)
        {
            sectionCount = section.TotalRegistered / MaxLimit;
            Extra = section.TotalRegistered - sectionCount * MaxLimit;
            if (Extra >= MinLimit) sectionCount++;
            else DeleteExtraRegisterations(section, sectionCount, Extra);
            section.NoOfSections = sectionCount;
        }
        return sections;
    }
    public List<Section> GetSections(string semester)
    {
        string query = "SELECT Course_Id, COUNT(Course_Id) TotalRegistered FROM REGISTERATION WHERE Semester = '" + semester + "' GROUP BY Course_Id";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        con.Open();
        SqlCommand cm = new SqlCommand(query, con);
        SqlDataReader reader = cm.ExecuteReader();
        List<Section> sectionList = new List<Section>();

        while (reader.Read())
        {
            Section section = new Section();
            section.Course_Id = reader["Course_Id"].ToString();
            section.TotalRegistered = Convert.ToInt32(reader["TotalRegistered"]);

            if (section.TotalRegistered < MinLimit)
                DeleteCourseOffer(section.Course_Id);
            else
                sectionList.Add(section);
        }
        con.Close();
        return sectionList;
    }
    public void DeleteCourseOffer(string Course_Id)
    {
        string query = "DELETE FROM OFFEREDCOURSE WHERE Course_Id = " + Course_Id;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        command.ExecuteNonQuery();
        connection.Close();

        ExecuteNonQuery("DELETE FROM REGISTERATIONS WHERE Course_Id = " + Course_Id);
    }
    public void DeleteExtraRegisterations(Section section, int totalValid, int extra)
    {
        string query = "SELECT Reg_Id FROM REGISTERATION WHERE Course_Id = " + section.Course_Id;
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        for (int i = 0; reader.Read() && i < totalValid * MaxLimit; i++) ;

        string deletequery = "DELETE FROM REGISTERATION WHERE Reg_Id IN (";
        while (reader.Read())
            deletequery = deletequery + reader["Reg_Id"] + ",";

        connection.Close();
        deletequery = deletequery.Remove(deletequery.Length - 1, 1) + ')';
        ExecuteNonQuery(deletequery);
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
    public string ExecuteScalar(string query, string Column)
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
    public void LoadAllocationTable(string Semester, string User_Id)
    {
        string query = "SELECT DEPARTMENT.Name AS Dept_Code, Course_Code, SECTION.Name AS Section_Name, " +
                       "CONCAT(First_name, ' ', Last_Name) AS Instructor_Name, SECTION.Section_Id FROM SECTION " +
                       "INNER JOIN OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id " +
                       "INNER JOIN COURSE ON COURSE.Course_Id = OFFEREDCOURSE.Course_Id " +
                       "INNER JOIN DEPARTMENT ON DEPARTMENT.Dept_Id = OFFEREDCOURSE.Dept_Id " +
                       "INNER JOIN CAMPUS ON CAMPUS.Campus_Id = DEPARTMENT.Campus_Id " +
                       "LEFT JOIN USERS ON USERS.User_Id = SECTION.Instructor_Id " +
                       "WHERE OfferedIn = '" + Semester + "' AND CAMPUS.Campus_Id = ( " +
                       "SELECT Campus_Id FROM ADMIN WHERE User_Id = " + User_Id + ") ";

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        CourseAllocationList.DataSource = reader;
        CourseAllocationList.DataBind();
        connection.Close();
    }
    private void InitInstructors(string Section_Id)
    {
        string query = "SELECT '-' UNION SELECT CONCAT(First_name,' ', Last_name,' (', Username, ') ') AS I FROM FACULTY INNER JOIN USERS ON " +
            "FACULTY.User_Id = USERS.User_Id INNER JOIN USERACCOUNT ON USERACCOUNT.User_Id = USERS.User_Id WHERE Dept_Id IN (" +
            "SELECT Dept_Id FROM SECTION INNER JOIN OFFEREDCOURSE ON SECTION.Course_Id = OFFEREDCOURSE.OfferCourse_Id WHERE " +
            "Section_Id = " + Section_Id + ") AND FACULTY.User_Id NOT IN( SELECT Instructor_Id FROM SECTION INNER JOIN OFFEREDCOURSE " +
            "ON OFFEREDCOURSE.OfferCourse_Id = SECTION.Course_Id WHERE OfferedIn = '" + semester + "' GROUP BY Instructor_Id HAVING COUNT(Instructor_Id) >= 3) ";

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        connection.Open();
        SqlCommand cm = new SqlCommand(query, connection);
        SqlDataReader reader = cm.ExecuteReader();

        InstCode.Items.Clear();
        while (reader.Read())
            InstCode.Items.Add(reader.GetString(0));

        connection.Close();
        reader.Close();
    }
    private bool Validate(string Id)
    {
        foreach (GridViewRow row in CourseAllocationList.Rows)
            if (row.Cells[0].Text == Id)
                if (row.Cells[4].Text == " ")
                    return true;
                else break;
        return false;
    }
    protected void RefNo_TextChanged(object sender, EventArgs e)
    {
        if (Validate(SectionId.Text))
            InitInstructors(SectionId.Text);
    }
    private string LoadCurrentSemester()
    {
        return ExecuteReader("SELECT TOP(1) Semester FROM SEMESTERRECORD ORDER BY Start_Date DESC", "Semester");
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User_Id = Request.QueryString["id"];
            //User_Id = "1";
            semester = LoadCurrentSemester();
        }
        LoadAllocationTable(semester, User_Id);
    }
    private string getUsername(string Selection)
    {
        if (Selection[0] == '-') return "";

        int startIdx = -1, length = 0;
        for (int i = 0; i < Selection.Length; i++)
        {
            if (Selection[i] == ')')
                break;
            if (Selection[i] == '(')
                startIdx = i + 1;
            else if (startIdx != -1)
                length++;
        }
        return Selection.Substring(startIdx, length);
    }
    protected void AllocateButton_Click(object sender, EventArgs e)
    {
        if (InstCode.Items.Count == 1 || InstCode.SelectedIndex == 0 || !Validate(SectionId.Text))
        {
            SuccessLabel.Visible = false;
            FailLabel.Visible = true;
            return;
        }

        string InstructorName = "'" + getUsername(InstCode.Text) + '\'';
        string query = "SELECT User_Id FROM USERACCOUNT WHERE Username = " + InstructorName;
        string InstructorId = ExecuteScalar(query, "User_Id");
        string Insquery = "UPDATE SECTION SET Instructor_Id = " + InstructorId +
            " WHERE Section_Id = " + SectionId.Text;
        if (ExecuteNonQuery(Insquery) == 1)
        {
            SuccessLabel.Visible = true;
            SectionId.Text = "";
            InstCode.Items.Clear();
            LogEvent(Convert.ToInt32(User_Id), "Allocated Instructor For Course");
        }
        Page_Load(null, null);
    }
    protected void CrsCrtButton_Click(object sender, EventArgs e)
    {
        CreateSections(semester);
        SuccessLabel0.Visible = true;
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
    public static void LogEvent(int userId, string action)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO AUDITTRAIL (User_Id, Activity) VALUES (@UserId, @Action)", connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@Action", action);
            command.ExecuteNonQuery();
        }
    }
}









/*
 * //public class Classes
//{
//    public string Section_Id { get; set; }
//    public int RegStudents { get; set; }
//}

//public class SectionInfo {
//    public string Course_Id { get; set; }
//    public List<Classes> classes { get; set; }
//    public SectionInfo(int TotalSections) 
//    {
//        classes = new List<Classes>();
//        classes.Capacity = TotalSections;
//    }
//}
    private void CreateClasses(SectionInfo course, int totalsections, int extras)
    {
        Classes temp;
        int extraPerSection = extras / totalsections;
        int remaining = extras - extraPerSection * totalsections;
        for(int i = 0; i < totalsections; i++)
        {
            temp = new Classes();
            temp.Section_Id = null;
            temp.RegStudents = 50 + extraPerSection;
            course.classes.Add(new Classes());
        }
        course.classes.Last().RegStudents += remaining;
    }
    private List<SectionInfo> getNoOfSectionsList(List<Section> sectionList)
    {
        int sectionCount, Extras;
        List<SectionInfo> classes = new List<SectionInfo>();
        foreach(Section section in sectionList)
        {
            sectionCount = section.TotalRegistered / 50;
            Extras = section.TotalRegistered - sectionCount * 50;
            if (Extras > 10)
            {
                sectionCount++;
                Extras = 0;
            }
            classes.Add(new SectionInfo(sectionCount));
            classes.Last().Course_Id = section.Course_Id;
            CreateClasses(classes.Last(), sectionCount, Extras);
        }
        return classes;
    }
    public void DeleteCourseOffer(Section section, string semester)
    {
        string query = ""
    }
    public List<Section> GetSectionList(string semester)
    {
        string query = "SELECT Course_Id, TotalSeats, COUNT(Course_Id) TotalRegistered FROM REGISTERATION WHERE Semester = ' " + semester + "' GROUP BY Course_Id";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
        con.Open();
        SqlCommand cm = new SqlCommand(query, con);
        SqlDataReader reader = cm.ExecuteReader();
        List<Section> sectionList = new List<Section>();

        while (reader.Read())
        {
            Section section = new Section();
            section.Course_Id = reader["Course_Id"].ToString();
            section.TotalRegistered = Convert.ToInt32(reader["TotalRegistered"]);
            if(section.TotalRegistered < 10)
                DeleteCourseOffer(section, semester);
            else
                sectionList.Add(section);
        }
        con.Close();
        return sectionList;
    }
    public string CreateQuery(List<SectionInfo> sections, string semester)
    {
        char SectionName;
        string query = "INSERT INTO SECTION (Course_Id, Name, Semester) VALUES ";
        foreach (SectionInfo section in sections)
        {
            SectionName = 'A';
            foreach(Classes sectionObj in section.classes)
            {
                query += "("+ section.Course_Id + ", '" + SectionName + "', " + semester + "), \n";
            }
        }
        query.Remove(query.Length - 1);
        return query;
    }
    public List<SectionInfo> GetNoOfSections(List<Section> sectionsList)
    {
        int sectionCount, Extras;
        List<SectionInfo> classes = new List<SectionInfo>();
        foreach (Section section in sectionsList)
        {
            sectionCount = section.TotalRegistered / 50;
            Extras = section.TotalRegistered - sectionCount * 50;
            if (Extras > 10)
            {
                sectionCount++;
                Extras = 0;
            }
            classes.Add(new SectionInfo(sectionCount));
            classes.Last().Course_Id = section.Course_Id;
            CreateClasses(classes.Last(), sectionCount, Extras);
        }
        return classes;
    }
    public void CreateSection(string semester)
    {
        List<Section> sections = GetSectionList(semester);
        //List<SectionInfo> classes = GetNoOfSections(sections);

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreateSection("Spring 2020");
            //List<Section> sectionList = GetSectionList("Spring 2020");

            //OfferedCourseList.DataSource = courses;
            //OfferedCourseList.DataBind();
        }
    }
    //private void InitCampuses()
    //{
    //    string query = "SELECT DISTINCT Location FROM CAMPUS";
    //    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexConnectionString"].ConnectionString);
    //    connection.Open(); 
    //    SqlCommand cm = new SqlCommand(query, connection);
    //    SqlDataReader reader = cm.ExecuteReader();
    //    while(reader.Read())
    //        Campuses.Items.Add(reader["Location"].ToString());
    //    Campuses.DataBind();
    //}
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    } 
 */



