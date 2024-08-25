﻿Imports System.IO
Imports System.Data

Partial Class Admin_addteacher



    Inherits System.Web.UI.Page
    

    Dim subselect As Integer
    Dim subremove As String
    Dim classremove As String
    Dim staffID As String




    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)            con.open()
            Dim cmdCheck20 As New MySql.Data.MySqlClient.MySqlCommand("SELECT Subjects.subject as Subject, Class.class as Class, staffprofile.surname as Instructor, staffprofile.phone as Phone, courseoverview.overview as Overview, courseoverview.texts as Textbooks FROM courseoverview inner join (classsubjects inner join staffprofile on classsubjects.teacher = staffprofile.staffid) on courseoverview.class = classsubjects.class and courseoverview.subject = classsubjects.subject inner join subjects on subjects.id = courseoverview.subject inner join session on session.id = courseoverview.session inner join class on class.id = courseoverview.class where session.ID = '" & Session("SessionId") & "' and class.Id = '" & Session("ClassId") & "' and subjects.subject = '" & Replace(Request.QueryString.ToString, "+", " ") & "'", con)
            Dim ds As New DataTable
            Dim adapter1 As New MySql.Data.MySqlClient.MySqlDataAdapter
            Try
                adapter1.SelectCommand = cmdCheck20
                adapter1.Fill(ds)
                DetailsView1.DataSource = ds
                DetailsView1.DataBind()


            Catch ex As Exception
                lblError.Text = ex.Message
            End Try


            Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT week, topic, content from courseoutline inner join subjects on subjects.id = courseoutline.subject inner join session on session.id = courseoutline.session inner join class on class.id = courseoutline.class where subjects.subject = '" & Replace(Request.QueryString.ToString, "+", " ") & "' and class.id = '" & Session("ClassId") & "' and courseoutline.session = '" & Session("SessionId") & "' order by courseoutline.week asc", con)
            Dim ds2 As New DataTable
            ds2.Columns.Add("Week")
            ds2.Columns.Add("Topic")
            ds2.Columns.Add("Content")

            Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
            Do While reader.Read
                ds2.Rows.Add(reader.Item(0), reader.Item(1), reader.Item(2))
            Loop
            reader.Close()
            GridView1.DataSource = ds2
            GridView1.DataBind()

            con.close()        End Using


    End Sub

  
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("~/staff/sclassdetails.aspx")

    End Sub

    
End Class
