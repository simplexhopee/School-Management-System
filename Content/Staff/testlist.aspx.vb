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
    Dim cbt As New CBT
    Dim ds As new ds_functions
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
        If check.Check_Staff(Session("roles"), Session("usertype")) = False Then Response.Redirect("~/default.aspx")
        Try

       
                           Dim ds As New DataTable
                ds.Columns.Add("id")
                ds.Columns.Add("date")
                ds.Columns.Add("title")
                ds.Columns.Add("subject")
                ds.Columns.Add("class")
            ds.Columns.Add("validate")
            Dim y As Array = cbt.Teacher_Tests_List(Session("staffid"), Session("sessionid"))
            For j = 0 To y.GetLength(1) - 2
                ds.Rows.Add(y(0, j), y(1, j), y(2, j), y(3, j), y(4, j), y(5, j))
            Next
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

        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            Dim ds As New DataTable
            ds.Columns.Add("id")
            ds.Columns.Add("date")
            ds.Columns.Add("title")
            ds.Columns.Add("subject")
            ds.Columns.Add("class")
            ds.Columns.Add("validate")
            Dim y As Array = cbt.Teacher_Tests_List(Session("staffid"), Session("sessionid"))
            For j = 0 To y.GetLength(1) - 2
                ds.Rows.Add(y(0, j), y(1, j), y(2, j), y(3, j), y(4, j), y(5, j))
            Next
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
            Dim ds As New DataTable
            ds.Columns.Add("id")
            ds.Columns.Add("date")
            ds.Columns.Add("title")
            ds.Columns.Add("subject")
            ds.Columns.Add("class")
            ds.Columns.Add("validate")
            Dim y As Array = cbt.Teacher_Tests_List(Session("staffid"), Session("sessionid"))
            For j = 0 To y.GetLength(1) - 2
                ds.Rows.Add(y(0, j), y(1, j), y(2, j), y(3, j), y(4, j), y(5, j))
            Next
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

    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Try
            Dim row As GridViewRow = GridView1.Rows(e.RowIndex)
            If cbt.Check_Written(row.Cells(2).Text, Session("sessionid")) = False Then
                cbt.Delete_Tests(row.Cells(2).Text, Session("sessionid"))
                Show_Alert(True, "Test Deleted Successfully")
            Else
                Show_Alert(False, "Test Written already, Invalidate instead")
            End If
            Dim ds As New DataTable
            ds.Columns.Add("id")
            ds.Columns.Add("date")
            ds.Columns.Add("title")
            ds.Columns.Add("subject")
            ds.Columns.Add("class")
            ds.Columns.Add("validate")
            Dim y As Array = cbt.Teacher_Tests_List(Session("staffid"), Session("sessionid"))
            For j = 0 To y.GetLength(1) - 2
                ds.Rows.Add(y(0, j), y(1, j), y(2, j), y(3, j), y(4, j), y(5, j))
            Next
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
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs)

    End Sub


    Protected Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try
            Dim ds As New DataTable
            ds.Columns.Add("id")
            ds.Columns.Add("date")
            ds.Columns.Add("title")
            ds.Columns.Add("subject")
            ds.Columns.Add("class")
            ds.Columns.Add("validate")
            Dim y As Array = cbt.Teacher_Tests_List(Session("staffid"), Session("sessionid"))
            For j = 0 To y.GetLength(1) - 2
                ds.Rows.Add(y(0, j), y(1, j), y(2, j), y(3, j), y(4, j), y(5, j))
            Next
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

    Protected Sub btnMsg_Click(sender As Object, e As EventArgs) Handles btnNewTest.Click
        Response.Redirect("~/Content/Staff/newtest.aspx")
    End Sub

    
End Class
