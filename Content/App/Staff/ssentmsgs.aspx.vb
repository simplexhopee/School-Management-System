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
    Dim par As New parentcheck
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


            If Not IsPostBack Then
                Dim staffs As New ArrayList
                DropDownList1.Items.Clear()
                DropDownList1.Items.Add("All")
                Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                    con.Open()
                    Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT dept, headtitle from depts where head = '" & Session("staffid") & "'", con)
                    Dim student0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
                    Dim deptshead As New ArrayList
                    Do While student0.Read
                        deptshead.Add(student0.Item(0))
                    Loop
                    student0.Close()
                    Dim classcontroled As New ArrayList
                    Dim secsub As New ArrayList
                    Dim mysub As New ArrayList
                    For Each item As String In deptshead
                        classcontroled.Add(item)
                        mysub = Get_subordinates(item)

                        For Each subitem As String In mysub
                            classcontroled.Add(subitem)
                            secsub.Add(subitem)
                        Next
                    Next
                    Dim thirdsub As New ArrayList
                    For Each item As String In secsub

                        mysub = Get_subordinates(item)
                        For Each subitem As String In mysub
                            classcontroled.Add(subitem)
                            thirdsub.Add(subitem)
                        Next
                    Next
                    Dim fourthsub As New ArrayList
                    For Each item As String In thirdsub

                        mysub = Get_subordinates(item)
                        For Each subitem As String In mysub
                            classcontroled.Add(subitem)
                            fourthsub.Add(subitem)
                        Next
                    Next
                    Dim fifthsub As New ArrayList
                    For Each item As String In fourthsub

                        mysub = Get_subordinates(item)
                        For Each subitem As String In mysub
                            classcontroled.Add(subitem)
                        Next
                    Next
                    Dim classgroups As New ArrayList
                    For Each item As String In deptshead
                        Dim classes As New MySql.Data.MySqlClient.MySqlCommand("Select class.class from class inner join depts on class.superior = depts.Id where depts.dept = '" & item & "'", con)
                        Dim schclass As MySql.Data.MySqlClient.MySqlDataReader = classes.ExecuteReader
                        Dim d As Boolean = False
                        Do While schclass.Read
                            d = True
                        Loop
                        If d = True Then
                            classgroups.Add(item)
                        End If
                        schclass.Close()
                    Next
                    Dim deptstaff As New ArrayList
                    For Each item As String In classcontroled
                        Dim f As Boolean = False
                        Dim classes As New MySql.Data.MySqlClient.MySqlCommand("Select class.class from class inner join depts on class.superior = depts.Id where depts.dept = '" & item & "'", con)
                        Dim schclass As MySql.Data.MySqlClient.MySqlDataReader = classes.ExecuteReader
                        Do While schclass.Read
                            f = True
                        Loop
                        If f = True Then
                            classgroups.Add(item)
                        End If
                        schclass.Close()
                        Dim classes1 As New MySql.Data.MySqlClient.MySqlCommand("Select staff from staffdept inner join depts on staffdept.dept = depts.id where depts.dept = '" & item & "'", con)
                        Dim schclass1 As MySql.Data.MySqlClient.MySqlDataReader = classes1.ExecuteReader
                        Do While schclass1.Read
                            deptstaff.Add(schclass1.Item(0))
                        Loop
                        schclass1.Close()

                        For Each sitem As String In deptstaff
                            Dim classes11 As New MySql.Data.MySqlClient.MySqlCommand("Select staffprofile.surname from staffprofile where staffprofile.staffid = '" & sitem & "'  and staffprofile.activated = '" & 1 & "'", con)
                            Dim schclass11 As MySql.Data.MySqlClient.MySqlDataReader = classes11.ExecuteReader
                            Do While schclass11.Read
                                If Not staffs.Contains(schclass11(0)) Then
                                    staffs.Add(schclass11(0))
                                    DropDownList1.Items.Add(schclass11.Item(0).ToString)
                                End If
                            Loop
                            schclass11.Close()
                        Next
                    Next
                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date")
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")

                    For Each staff As String In staffs

                        Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & staff & "'  order by date desc", con)
                        Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                        Do While reader.Read
                            ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
                        Loop
                        reader.Close()
                    Next
                    Try
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
                        Response.Write(ex.Message)
                    End Try
                    con.Close()                End Using

            End If
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Public Function Get_subordinates(ByVal dept As String) As ArrayList
        Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)            con.Open()

            Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT dept from depts where superior = '" & dept & "'", con)
            Dim student1 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
            Dim subo As New ArrayList
            Do While student1.Read
                subo.Add(student1.Item(0))
            Loop
            student1.Close()
            Return subo
            con.Close()
        End Using
    End Function

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.open()
                If DropDownList1.Text = "All" Then
                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date")
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")

                    For Each staff As ListItem In DropDownList1.Items
                        Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & staff.Text & "'  order by date desc", con)
                        Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                        Do While reader.Read
                            ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
                        Loop
                        reader.Close()
                    Next

                    ds.DefaultView.Sort = "date desc"
                    GridView1.DataSource = ds.DefaultView
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

                Else

                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date")
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")


                    Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & DropDownList1.Text & "'  order by date desc", con)
                    Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                    Do While reader.Read
                        ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
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

                End If
                con.close()            End Using

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
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.open()
                If DropDownList1.Text = "All" Then
                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date")
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")

                    For Each staff As ListItem In DropDownList1.Items
                        Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & staff.Text & "'  order by date desc", con)
                        Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                        Do While reader.Read
                            ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
                        Loop
                        reader.Close()
                    Next

                    ds.DefaultView.Sort = "date desc"
                    GridView1.DataSource = ds.DefaultView
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

                Else

                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date")
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")


                    Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & DropDownList1.Text & "'  order by date desc", con)
                    Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                    Do While reader.Read
                        ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
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

                End If
                con.close()            End Using

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
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.open()
                If DropDownList1.Text = "All" Then
                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date", GetType(System.DateTime))
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")

                    For Each staff As ListItem In DropDownList1.Items

                        Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & staff.Text & "' and sentmsgs.subject like '" & "%" & txtSearch.Text & "%" & "'  order by date desc", con)
                        Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                        Do While reader.Read
                            ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
                        Loop
                        reader.Close()
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

                Else

                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date")
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")


                    Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & DropDownList1.Text & "' and sentmsgs.subject like '" & "%" & txtSearch.Text & "%" & "' order by date desc", con)
                    Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                    Do While reader.Read
                        ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
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

                End If
                con.close()            End Using
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub

    Protected Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.open()
                If DropDownList1.Text = "All" Then
                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date")
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")

                    For Each staff As ListItem In DropDownList1.Items
                        Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & staff.Text & "'  order by date desc", con)
                        Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                        Do While reader.Read
                            ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
                        Loop
                        reader.Close()
                    Next

                    ds.DefaultView.Sort = "date desc"
                    GridView1.DataSource = ds.DefaultView
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



                Else

                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date")
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")


                    Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & DropDownList1.Text & "'  order by date desc", con)
                    Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                    Do While reader.Read
                        ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
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



                End If
                con.close()            End Using

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



    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("~/content/app/Staff/smessages.aspx")
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Try
            Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)                con.open()
                If DropDownList1.Text = "All" Then
                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date")
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")

                    For Each staff As ListItem In DropDownList1.Items
                        Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & staff.Text & "'  order by date desc", con)
                        Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                        Do While reader.Read
                            ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
                        Loop
                        reader.Close()
                    Next

                    ds.DefaultView.Sort = "date desc"
                    GridView1.DataSource = ds.DefaultView
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



                Else

                    Dim ds As New DataTable
                    ds.Columns.Add("id")
                    ds.Columns.Add("date")
                    ds.Columns.Add("sender")
                    ds.Columns.Add("receivertype")
                    ds.Columns.Add("receiver")
                    ds.Columns.Add("subject")


                    Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT sentmsgs.id, sentmsgs.date, staffprofile.surname, sentmsgs.receivertype, sentmsgs.receiver, sentmsgs.subject from sentmsgs inner join staffprofile on staffprofile.staffid = sentmsgs.sender inner join (classteacher inner join (class inner join depts on depts.id = class.superior) on class.id = classteacher.class) on sentmsgs.sender = classteacher.teacher where staffprofile.surname = '" & DropDownList1.Text & "'  order by date desc", con)
                    Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
                    Do While reader.Read
                        ds.Rows.Add(reader(0), Convert.ToDateTime(reader(1)).ToString("dd/MM/yyyy hh:mm tt"), reader(2), reader(3), reader(4), reader(5))
                    Loop
                    reader.Close()


                    GridView1.DataSource = ds
                    GridView1.AllowPaging = False
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
                con.close()            End Using
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try
    End Sub
End Class
