<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SC5_SubmitFeedback.aspx.cs" Inherits="SC5_SubmitFeedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Feedback Submission</title>
    <style>
        body {
			font-family: Arial, sans-serif;
			background-color: #f2f2f2;
			padding: 20px;
		}

		h2 {
			text-align: center;
			color: #333;
			margin-bottom: 20px;
		}

		form {
			background-color: #fff;
			padding: 20px;
			border-radius: 5px;
			box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
		}

		label {
			display: inline-block;
			width: 150px;
			font-weight: bold;
			margin-bottom: 10px;
			color: #333;
		}

		input[type="text"],
		input[type="date"],
		textarea {
			width: 250px;
			padding: 8px;
			border: 1px solid #ccc;
			border-radius: 4px;
			font-size: 14px;
			color: #555;
		}

		input[type="radio"] {
			margin-right: 5px;
		}

		textarea {
			resize: vertical;
		}

		input[type="submit"] {
			padding: 12px 24px;
			border: none;
			border-radius: 4px;
			background-color: #4CAF50;
			color: #fff;
			cursor: pointer;
			font-size: 16px;
			transition: background-color 0.3s;
		}

		input[type="submit"]:hover {
			background-color: #45a049;
		}
    </style>
</head>
<body>
<h2>Teacher Assessment Form</h2>
    <form id="assessmentForm" runat="server" action="submit_report.aspx" method="post">
        <label for="teacherFirstName">Teacher First Name:</label>
    
        <asp:DropDownList ID="teacherFirstNameDropDown" runat="server"></asp:DropDownList><br /><br />

        <label for="teacherLastName">Teacher Last Name:</label>
        <asp:DropDownList ID="teacherLastNameDropDown" runat="server"></asp:DropDownList><br /><br />
        

        <label for="semester">Semester:</label>
        <input type="text" id="semester" name="semester" runat="server" required /><br /><br />

        <label for="fDate">Date:</label>
        <input type="date" id="fDate" name="fDate" runat="server" required /><br /><br />

        <label for="roomNo">Room No:</label>
        <input type="text" id="roomNo" name="roomNo" runat="server" required /><br /><br />

        <label for="schedule">Schedule:</label>
        <input type="text" id="schedule" name="schedule" runat="server" required /><br /><br />

        <label>1 to 5 Rating : </label>
 	    <label>1    (Poor) </label>
 	    <label>5    (Excellent) </label><br /><br />

        <label>Knowledge and Expertise :</label>
        <input type="radio" id="eval1_1" name="eval1" runat="server" value="1" required />
        <label for="eval1_1">1</label>
        <input type="radio" id="eval1_2" name="eval1" runat="server" value="2" />
        <label for="eval1_2">2</label>
        <input type="radio" id="eval1_3" name="eval1" runat="server" value="3" />
        <label for="eval1_3">3</label>
        <input type="radio" id="eval1_4" name="eval1" runat="server" value="4" />
        <label for="eval1_4">4</label>
        <input type="radio" id="eval1_5" name="eval1" runat="server" value="5" />
        <label for="eval1_5">5</label><br /><br />
    


        <label>Classroom Management :</label>
        <input type="radio" id="eval2_1" name="eval2" runat="server" value="1" required />
            <label for="eval2_1">1</label>
        <input type="radio" id="eval2_2" name="eval2" runat="server" value="2" />
            <label for="eval2_2">2</label>
        <input type="radio" id="eval2_3" name="eval2" runat="server" value="3" />
        <label for="eval2_3">3</label>
        <input type="radio" id="eval2_4" name="eval2" runat="server" value="4" />
        <label for="eval2_4">4</label>
        <input type="radio" id="eval2_5" name="eval2" runat="server" value="5" />
        <label for="eval2_5">5</label><br /><br />
        
        <label>Teaching Effectiveness :</label>
        <input type="radio" id="eval3_1" name="eval3" runat="server" value="1" required />
        <label for="eval3_1">1</label>
        <input type="radio" id="eval3_2" name="eval3" runat="server" value="2" />
              <label for="eval3_2">2</label>
        <input type="radio" id="eval3_3" name="eval3" runat="server" value="3" />
              <label for="eval3_3">3</label>
        <input type="radio" id="eval3_4" name="eval3" runat="server" value="4" />
              <label for="eval3_4">4</label>
        <input type="radio" id="eval3_5" name="eval3" runat="server" value="5" />
              <label for="eval3_5">5</label><br /><br />

           <label>Communication and Clarity :</label>
        <input type="radio" id="eval4_1" name="eval4" runat="server" value="1" required />
              <label for="eval4_1">1</label>
        <input type="radio" id="eval4_2" name="eval4" runat="server" value="2" />
           <label for="eval4_2">2</label>
        <input type="radio" id="eval4_3" name="eval4" runat="server" value="3" />
           <label for="eval4_3">3</label>
        <input type="radio" id="eval4_4" name="eval4" runat="server" value="4" />
           <label for="eval4_4">4</label>
        <input type="radio" id="eval4_5" name="eval4" runat="server" value="5" />
           <label for="eval4_5">5</label><br /><br />
           <label>Feedback and Assessment :</label>
        <input type="radio" id="eval5_1" name="eval5" runat="server" value="1" required />
           <label for="eval5_1">1</label>
        <input type="radio" id="eval5_2" name="eval5" runat="server" value="2" />
         <label for="eval5_2">2</label>
        <input type="radio" id="eval5_3" name="eval5" runat="server" value="3" />
         <label for="eval5_3">3</label>
        <input type="radio" id="eval5_4" name="eval5" runat="server" value="4" />
         <label for="eval5_4">4</label>
        <input type="radio" id="eval5_5" name="eval5" runat="server" value="5" />
         <label for="eval5_5">5</label><br /><br />

        <label for="comment">Comment:</label>
        <textarea id="comment" name="comment" rows="3" cols="40" runat="server"></textarea><br /><br />

        <input type="submit" value="Submit" runat="server" />
    </form>
</body>
</html>
