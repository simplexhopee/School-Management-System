﻿Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.UI.WebControls



Partial Class Admin_staffprofile
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
        If Session("staffid") = Nothing Then            Dim x As ArrayList = check.Check_app_staff(Request.QueryString("username"), Request.QueryString("password"))            If x.Count <> 0 Then                Session("staffid") = x.Item(0)                Session("sessionid") = x.Item(1)            End If        End If
        Try
            If IsPostBack Then
            Else

                panel3.Visible = False
                Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                    con.Open()

                    Dim ds As New DataTable
                    ds.Columns.Add("passport")
                    ds.Columns.Add("staffname")
                    If DropDownList1.Text = "Active" Then
                        Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 1 & "'", con)
                        Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                        Do While student0.Read
                            ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                        Loop
                        student0.Close()
                    ElseIf DropDownList1.Text = "Inactive" Then
                        Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 0 & "'", con)
                        Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                        Do While student0.Read
                            ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                        Loop
                        student0.Close()
                    Else
                        Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile", con)
                        Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                        Do While student0.Read
                            ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                        Loop
                        student0.Close()
                    End If
                    gridview1.DataSource = ds
                    gridview1.DataBind()
                    con.Close()
                End Using
                panel3.Visible = False

            End If
            gridview1.SelectedIndex = -1
        Catch ex As Exception

            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub
    Protected Sub Staff_Details()

        Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)            con.Open()

            Dim cmdLoad10 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, activated, pension from staffprofile where staffid = ?", con)
            cmdLoad10.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("studentname", Session("staffadd")))
            Dim student1 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad10.ExecuteReader
            Dim pass As String
            student1.Read()
            pass = student1.Item("passport").ToString
            If student1.Item("activated") = True Then
                chkActivated.Checked = True
                chkActivated.Text = "Activated"
            Else
                chkActivated.Checked = False
                chkActivated.Text = "Deactivated"
            End If
            student1.Close()

            Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT staffid as 'Staff ID', surname as 'Name', sex as Sex, phone as 'Phone No', address as 'Address', email as 'Email', designation as 'Designation', salary as 'Salary', accountno as 'Account Number', bank as 'Banker', pfa as 'PFA' from staffprofile where staffid = ?", con)
            cmdLoad1.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("studentname", Session("staffadd")))
            Dim student As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
            student.Read()
            student.Close()
            Dim ds As New DataTable
            Dim adapter1 As New MySql.Data.MySqlClient.MySqlDataAdapter
            adapter1.SelectCommand = cmdLoad1
            adapter1.Fill(ds)
            DetailsView1.DataSource = ds
            DetailsView1.DataBind()
            If pass = "" Then
                pass = "~/img/noimage.jpg"
            End If
            Image1.ImageUrl = pass


            con.Close()
        End Using
        For Each row As GridViewRow In gridview1.Rows
            Dim x As Array = Split(row.Cells(1).Text, " - ")
            If x(0) = Session("staffadd") Then
                gridview1.SelectedIndex = row.RowIndex
            End If
        Next
        panel3.Visible = True
        pnlAll.Visible = False
    End Sub
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

        Session("edit") = "teacherprofile"
        Response.Redirect("~/content/app/Account/editprofile.aspx")

    End Sub





    Protected Sub DetailsView1_PageIndexChanging(sender As Object, e As DetailsViewPageEventArgs) Handles DetailsView1.PageIndexChanging

    End Sub

    Protected Sub gridview1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridview1.PageIndexChanging
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.Open()

                Dim ds As New DataTable
                ds.Columns.Add("passport")
                ds.Columns.Add("staffname")
                If DropDownList1.Text = "Active" Then
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 1 & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                ElseIf DropDownList1.Text = "Inactive" Then
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 0 & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                Else
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                End If
                gridview1.DataSource = ds
                gridview1.DataBind()
                con.Close()
            End Using
            gridview1.PageIndex = e.NewPageIndex
            gridview1.DataBind()
            Session("staffadd") = Nothing
            panel3.Visible = False
            gridview1.SelectedIndex = -1
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))

        End Try
    End Sub

    Protected Sub gridview1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gridview1.SelectedIndexChanging
        Try


            Dim x As Array = Split(gridview1.Rows(e.NewSelectedIndex).Cells(1).Text, " - ")
            Session("staffadd") = RTrim(x(0))

            Staff_Details()
            pnlAll.Visible = False
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))

        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.Open()

                Dim ds As New DataTable
                ds.Columns.Add("passport")
                ds.Columns.Add("staffname")
                Dim s As String = "%" & txtSearch.Text & "%"
                Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where surname like '" & s & "' or staffid = '" & txtSearch.Text & "'", con)
                Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                Dim count As Integer = 0
                Do While student0.Read
                    ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    count = count + 1
                Loop
                If count = 0 Then
                    Show_Alert(False, "Your Search produced no results")
                Else
                    Show_Alert(True, "Your Search produced " & count & " matches")
                End If

                student0.Close()
                gridview1.DataSource = ds
                gridview1.DataBind()
                con.Close()
            End Using
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub


    Protected Sub chkActivated_CheckedChanged(sender As Object, e As EventArgs) Handles chkActivated.CheckedChanged
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.Open()
                Dim active As Boolean = -Val(chkActivated.Checked)
                Dim cmdCheck3 As New MySql.Data.MySqlClient.MySqlCommand("Update StaffProfile Set activated = ? Where staffID = ?", con)
                cmdCheck3.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("active", active))
                cmdCheck3.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("surname", Session("staffadd")))
                cmdCheck3.ExecuteNonQuery()
                Dim insert As New MySql.Data.MySqlClient.MySqlCommand("Update classsubjects set teacher = ? where teacher = ?", con)
                insert.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("ID", ""))
                insert.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("password", Session("staffadd")))
                insert.ExecuteNonQuery()
                Dim insertw As New MySql.Data.MySqlClient.MySqlCommand("Delete from classteacher where teacher = ?", con)
                insertw.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("ID", Session("StaffAdd")))
                insertw.ExecuteNonQuery()

                Dim insertw1 As New MySql.Data.MySqlClient.MySqlCommand("Delete from admin where username = ?", con)
                insertw1.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("ID", Session("StaffAdd")))
                insertw1.ExecuteNonQuery()

                Dim insertw2 As New MySql.Data.MySqlClient.MySqlCommand("Delete from accounts where username = ?", con)
                insertw2.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("ID", Session("StaffAdd")))
                insertw2.ExecuteNonQuery()
                Dim ds As New DataTable
                ds.Columns.Add("passport")
                ds.Columns.Add("staffname")
                If DropDownList1.Text = "Active" Then
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 1 & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                ElseIf DropDownList1.Text = "Inactive" Then
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 0 & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                Else
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                End If
                gridview1.DataSource = ds
                gridview1.DataBind()
                con.Close()
            End Using
            panel3.Visible = False
            Session("staffadd") = Nothing
            gridview1.SelectedIndex = -1
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub lnkmsg_Click(sender As Object, e As EventArgs) Handles lnkmsg.Click
        Session("receivert") = "Staff"
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.Open()

                Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT surname from staffprofile where staffid = ?", con)
                cmdLoad1.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("studentname", Session("staffadd")))
                Dim student As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                student.Read()
                Session("receiver") = student.Item(0).ToString
                student.Close()
                con.Close()
            End Using
            Response.Redirect("~/content/app/account/personnewmg.aspx")
            Session("sendermsg") = Request.RawUrl
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub


    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.Open()
                Dim ds As New DataTable
                ds.Columns.Add("passport")
                ds.Columns.Add("staffname")
                If DropDownList1.Text = "Active" Then
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 1 & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                ElseIf DropDownList1.Text = "Inactive" Then
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 0 & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                Else
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                End If
                gridview1.DataSource = ds
                gridview1.DataBind()
                gridview1.SelectedIndex = -1
                panel3.Visible = False
                con.Close()
            End Using
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        pnlAll.Visible = True
        panel3.Visible = False
    End Sub

  
    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.Open()

                Dim ds As New DataTable
                ds.Columns.Add("passport")
                ds.Columns.Add("staffname")
                If DropDownList1.Text = "Active" Then
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 1 & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                ElseIf DropDownList1.Text = "Inactive" Then
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 0 & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                Else
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                End If
                gridview1.DataSource = ds
                gridview1.DataBind()
                con.Close()
            End Using
            gridview1.DataBind()
            Session("staffadd") = Nothing
            panel3.Visible = False
            gridview1.SelectedIndex = -1
            If gridview1.PageIndex = gridview1.PageCount - 1 Then
                btnNext.Visible = False
            Else
                btnNext.Visible = True
            End If
            If gridview1.PageIndex = 0 Then
                btnPrevious.Visible = False
            Else
                btnPrevious.Visible = True
            End If


            If gridview1.PageIndex + 1 <= gridview1.PageCount Then
                gridview1.PageIndex = gridview1.PageIndex + 1
                gridview1.DataBind()
                If gridview1.PageIndex = gridview1.PageCount - 1 Then
                    btnNext.Visible = False
                Else
                    btnNext.Visible = True
                End If
                If gridview1.PageIndex = 0 Then
                    btnPrevious.Visible = False
                Else
                    btnPrevious.Visible = True
                End If
            End If
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.Open()
                Dim ds As New DataTable
                ds.Columns.Add("passport")
                ds.Columns.Add("staffname")
                If DropDownList1.Text = "Active" Then
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 1 & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                ElseIf DropDownList1.Text = "Inactive" Then
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile where activated = '" & 0 & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                Else
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT passport, staffid, surname from staffprofile", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Do While student0.Read
                        ds.Rows.Add(student0.Item(0).ToString, student0.Item(1) & " - " & student0.Item(2).ToString)
                    Loop
                    student0.Close()
                End If
                gridview1.DataSource = ds
                gridview1.DataBind()
                con.Close()
            End Using
            gridview1.DataBind()
            Session("staffadd") = Nothing
            panel3.Visible = False
            gridview1.SelectedIndex = -1
            If gridview1.PageIndex = gridview1.PageCount - 1 Then
                btnNext.Visible = False
            Else
                btnNext.Visible = True
            End If
            If gridview1.PageIndex = 0 Then
                btnPrevious.Visible = False
            Else
                btnPrevious.Visible = True
            End If


            If gridview1.PageIndex - 1 >= 0 Then
                gridview1.PageIndex = gridview1.PageIndex - 1
                gridview1.DataBind()
                If gridview1.PageIndex = gridview1.PageCount - 1 Then
                    btnNext.Visible = False
                Else
                    btnNext.Visible = True
                End If
                If gridview1.PageIndex = 0 Then
                    btnPrevious.Visible = False
                Else
                    btnPrevious.Visible = True
                End If

            End If
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub
End Class
