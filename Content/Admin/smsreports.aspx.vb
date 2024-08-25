﻿Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.UI.WebControls
Partial Class Admin_allstudents
    Inherits System.Web.UI.Page
    Dim pagebefore As String
    Dim studentId As Integer
    Dim termID As Integer
    Dim checkedSubjects As New ArrayList
    Dim uncheckedSubjects As New ArrayList
    Dim alert As New Literal
    Dim alertmsg As New Alerts
    Dim logify As New notify
    Dim check As New CheckUser
    Dim alertPLC As New PlaceHolder
    Private Sub Show_Alert(type As Boolean, msg As String)
        alertPLC = Me.Master.FindControl("plcAlert")
        If type = True Then
            alert.Text = alertmsg.success_message(msg)
        Else
            alert.Text = alertmsg.error_message(msg)
        End If
        alertPLC.Controls.Add(alert)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If check.Check_Admin(Session("roles"), Session("usertype")) = False Then Response.Redirect("~/default.aspx")
        Try

       
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)
                con.Open()
                Dim cmdSelect2a As New MySql.Data.MySqlClient.MySqlCommand("SELECT smsno FROM Session WHERE id = ?", con)
                cmdSelect2a.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("Session", Session("SessionId")))
                Dim reader2a As MySql.Data.MySqlClient.MySqlDataReader = cmdSelect2a.ExecuteReader
                reader2a.Read()
                Label1.Text = "Available Units: " & reader2a(0)

                reader2a.Close()
                Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT cast(sms.time as char) as date, staffprofile.surname as 'Sender', sms.subject, sms.pages, sms.units, sms.recipienttype, sms.recipientsno from sms inner join staffprofile on staffprofile.staffid = sms.sender order by sms.time desc", con)
                Dim ds As New DataTable
                ds.Columns.Add("date")
                ds.Columns.Add("sender")

                ds.Columns.Add("subject")
                ds.Columns.Add("pages")
                ds.Columns.Add("units")
                ds.Columns.Add("recipienttype")
                ds.Columns.Add("recipients")
                Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader

                Do While reader.Read
                    ds.Rows.Add(Convert.ToDateTime(reader(0)).ToString("dd/MM/yyyy hh:mm tt"), reader(1), reader(2), reader(3), reader(4), reader(5), reader(6))
                Loop
                reader.Close()


                GridView1.DataSource = ds
                GridView1.DataBind()
                If GridView1.PageIndex = GridView1.PageCount - 1 Then
                    btnNext.Visible = False
                Else
                    btnNext.Visible = True
                End If
                If GridView1.PageIndex = 0 Then
                    btnPrevious.Visible = False
                Else
                    btnPrevious.Visible = True
                End If

                con.Close()            End Using
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)

                con.Open()
                Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT cast(sms.time as char) as date, staffprofile.surname as 'Sender', sms.subject, sms.pages, sms.units, sms.recipienttype, sms.recipientsno from sms inner join staffprofile on staffprofile.staffid = sms.sender order by sms.time desc", con)
                Dim ds As New DataTable
                ds.Columns.Add("date")
                ds.Columns.Add("sender")

                ds.Columns.Add("subject")
                ds.Columns.Add("pages")
                ds.Columns.Add("units")
                ds.Columns.Add("recipienttype")
                ds.Columns.Add("recipients")
                Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader

                Do While reader.Read
                    ds.Rows.Add(Convert.ToDateTime(reader(0)).ToString("dd/MM/yyyy hh:mm tt"), reader(1), reader(2), reader(3), reader(4), reader(5), reader(6))
                Loop
                reader.Close()



                GridView1.DataSource = ds
                GridView1.DataBind()
                If GridView1.PageIndex = GridView1.PageCount - 1 Then
                    btnNext.Visible = False
                Else
                    btnNext.Visible = True
                End If
                If GridView1.PageIndex = 0 Then
                    btnPrevious.Visible = False
                Else
                    btnPrevious.Visible = True
                End If

                con.Close()            End Using

            If GridView1.PageIndex + 1 <= GridView1.PageCount Then
                GridView1.PageIndex = GridView1.PageIndex + 1
                GridView1.DataBind()
                If GridView1.PageIndex = GridView1.PageCount - 1 Then
                    btnNext.Visible = False
                Else
                    btnNext.Visible = True
                End If
                If GridView1.PageIndex = 0 Then
                    btnPrevious.Visible = False
                Else
                    btnPrevious.Visible = True
                End If
            End If
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(sender As Object, e As EventArgs) Handles GridView1.PageIndexChanged

    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)

                con.Open()
                Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT cast(sms.time as char) as date, staffprofile.surname as 'Sender', sms.subject, sms.pages, sms.units, sms.recipienttype, sms.recipientsno from sms inner join staffprofile on staffprofile.staffid = sms.sender order by sms.time desc", con)
                Dim ds As New DataTable
                ds.Columns.Add("date")
                ds.Columns.Add("sender")

                ds.Columns.Add("subject")
                ds.Columns.Add("pages")
                ds.Columns.Add("units")
                ds.Columns.Add("recipienttype")
                ds.Columns.Add("recipients")
                Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader

                Do While reader.Read
                    ds.Rows.Add(Convert.ToDateTime(reader(0)).ToString("dd/MM/yyyy hh:mm tt"), reader(1), reader(2), reader(3), reader(4), reader(5), reader(6))
                Loop
                reader.Close()


                GridView1.DataSource = ds
                GridView1.DataBind()
                If GridView1.PageIndex = GridView1.PageCount - 1 Then
                    btnNext.Visible = False
                Else
                    btnNext.Visible = True
                End If
                If GridView1.PageIndex = 0 Then
                    btnPrevious.Visible = False
                Else
                    btnPrevious.Visible = True
                End If
                con.Close()            End Using

            GridView1.PageIndex = e.NewPageIndex
            GridView1.DataBind()

            If GridView1.PageIndex = GridView1.PageCount - 1 Then
                btnNext.Visible = False
            Else
                btnNext.Visible = True
            End If
            If GridView1.PageIndex = 0 Then
                btnPrevious.Visible = False
            Else
                btnPrevious.Visible = True
            End If
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub GridView1_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs)

    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)

                con.Open()
                Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT cast(sms.time as char) as date, staffprofile.surname as 'Sender', sms.subject, sms.pages, sms.units, sms.recipienttype, sms.recipientsno from sms inner join staffprofile on staffprofile.staffid = sms.sender where sms.subject like '%" & txtSearch.Text & "%' order by sms.time desc", con)
                Dim ds As New DataTable
                ds.Columns.Add("date")
                ds.Columns.Add("sender")

                ds.Columns.Add("subject")
                ds.Columns.Add("pages")
                ds.Columns.Add("units")
                ds.Columns.Add("recipienttype")
                ds.Columns.Add("recipients")
                Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader

                Do While reader.Read
                    ds.Rows.Add(Convert.ToDateTime(reader(0)).ToString("dd/MM/yyyy hh:mm tt"), reader(1), reader(2), reader(3), reader(4), reader(5), reader(6))
                Loop
                reader.Close()


                GridView1.AllowPaging = False
                GridView1.DataSource = ds
                GridView1.DataBind()
                If GridView1.PageIndex = GridView1.PageCount - 1 Then
                    btnNext.Visible = False
                Else
                    btnNext.Visible = True
                End If
                If GridView1.PageIndex = 0 Then
                    btnPrevious.Visible = False
                Else
                    btnPrevious.Visible = True
                End If
                con.Close()            End Using
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try

            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.Open()
                Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT cast(sms.time as char) as date, staffprofile.surname as 'Sender', sms.subject, sms.pages, sms.units, sms.recipienttype, sms.recipientsno from sms inner join staffprofile on staffprofile.staffid = sms.sender order by sms.time desc", con)
                Dim ds As New DataTable
                ds.Columns.Add("date")
                ds.Columns.Add("sender")

                ds.Columns.Add("subject")
                ds.Columns.Add("pages")
                ds.Columns.Add("units")
                ds.Columns.Add("recipienttype")
                ds.Columns.Add("recipients")
                Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader

                Do While reader.Read
                    ds.Rows.Add(Convert.ToDateTime(reader(0)).ToString("dd/MM/yyyy hh:mm tt"), reader(1), reader(2), reader(3), reader(4), reader(5), reader(6))
                Loop
                reader.Close()


                GridView1.DataSource = ds
                GridView1.DataBind()
                If GridView1.PageIndex = GridView1.PageCount - 1 Then
                    btnNext.Visible = False
                Else
                    btnNext.Visible = True
                End If
                If GridView1.PageIndex = 0 Then
                    btnPrevious.Visible = False
                Else
                    btnPrevious.Visible = True
                End If
                con.Close()            End Using
            If GridView1.PageIndex - 1 >= 0 Then
                GridView1.PageIndex = GridView1.PageIndex - 1
                GridView1.DataBind()
                If GridView1.PageIndex = GridView1.PageCount - 1 Then
                    btnNext.Visible = False
                Else
                    btnNext.Visible = True
                End If
                If GridView1.PageIndex = 0 Then
                    btnPrevious.Visible = False
                Else
                    btnPrevious.Visible = True
                End If

            End If
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub btnMsg_Click(sender As Object, e As EventArgs) Handles btnMsg.Click
        Response.Redirect("~/Content/admin/newmsg.aspx")
    End Sub

   
End Class
