﻿Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.UI.WebControls
Partial Class dashboard
    Inherits System.Web.UI.Page

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
        Dim lbldashboard As New Label
        lbldashboard = Me.Master.FindControl("lblDashBoard")
        lbldashboard.Visible = True
        Dim lnkDashboard As New LinkButton
        lnkDashboard = Me.Master.FindControl("lnkDashBoard")
        lnkDashboard.Visible = False
        logify.Read_notification("~/content/dashboards/studentdashboard.aspx", Session("studentid"))

    End Sub
    Private Sub clas_info()

        Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)            con.open()

            Dim cmdLoad As New MySql.Data.MySqlClient.MySqlCommand("SELECT class.class as Class, studentsummary.classNo as 'No of Students', class.type as 'Type', depts.dept as 'Department' from class inner join depts on class.superior = depts.id left join studentsummary on class.id = studentsummary.class where class.id = '" & Session("classid") & "' and studentsummary.class = '" & Session("classid") & "' order by studentsummary.session desc", con)
            cmdLoad.Parameters.Add(New MySql.Data.MySqlClient.MySqlParameter("studentname", Session("staffid")))
            Dim student As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad.ExecuteReader
            Dim stu As Integer = 0
            Do While student.Read
                stu = stu + 1
            Loop
            lblClassStu.Text = stu
            student.Close()
            Dim cmdLoad0 As New MySql.Data.MySqlClient.MySqlCommand("SELECT * from subjectreg where student = '" & Session("studentid") & "' and session = '" & Session("sessionid") & "'", con)
            Dim reader0 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad0.ExecuteReader
            Dim subjectno As Integer = 0
            Do While reader0.Read

                subjectno = subjectno + 1
            Loop
            lblClassSubjects.Text = subjectno
            reader0.Close()
            Dim ds2 As New DataTable
            Dim CA1 As Boolean
            Dim CA2 As Boolean
            Dim CA3 As Boolean
            Dim project As Boolean
            Dim exams As Boolean
            Dim cmd2 As New MySql.Data.MySqlClient.MySqlCommand("SELECT * from scorespublish where term = '" & Session("SessionId") & "'", con)
            Dim subjectreader2 As MySql.Data.MySqlClient.MySqlDataReader = cmd2.ExecuteReader
            subjectreader2.Read()
            CA1 = subjectreader2("CA1")
            CA2 = subjectreader2("CA2")
            CA3 = subjectreader2("CA3")
            exams = subjectreader2("Exams")
            project = subjectreader2.Item("project")
            subjectreader2.Close()
            If exams = True Then
                lblPublishedCA.Text = "Exams"
            ElseIf project = True Then
                lblPublishedCA.Text = "CA 4"
            ElseIf CA3 = True Then
                lblPublishedCA.Text = "CA 3"
            ElseIf CA2 = True Then
                lblPublishedCA.Text = "CA 2"
            ElseIf CA1 = True Then
                lblPublishedCA.Text = "CA 1"
            Else
                lblPublishedCA.Text = "No CA"
            End If


            con.close()        End Using


    End Sub
    Private Sub Daily_schedule()

        Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)            con.open()
            Dim subjects As New ArrayList
            Dim classes As New ArrayList
            Dim periods As New ArrayList
            Dim range As New ArrayList

            Dim cmdLo As New MySql.Data.MySqlClient.MySqlCommand("SELECT subjects.alias, timetable.period, class.class from timetable inner join ttname on timetable.tname = ttname.id inner join class on timetable.class = class.id inner join (subjects inner join subjectreg on subjectreg.subjectsofferred = subjects.id) on timetable.subject = subjects.id where timetable.class = '" & Session("classid") & "' and timetable.day = '" & Now.DayOfWeek.ToString & "' and ttname.default = '" & 1 & "' and subjectreg.student = '" & Session("studentid") & "' and subjectreg.session = '" & Session("sessionid") & "'order by timetable.period", con)
            Dim studen As MySql.Data.MySqlClient.MySqlDataReader = cmdLo.ExecuteReader
            Do While studen.Read
                subjects.Add(studen(0).ToString)
                periods.Add(studen(1).ToString)
                classes.Add(studen(2).ToString)
            Loop
            studen.Close()

            Dim count As Integer = 0
            Dim lite As New Literal
            Dim rows As String = ""
            For Each item As Integer In periods
                Dim cmdLoad10 As New MySql.Data.MySqlClient.MySqlCommand("SELECT tperiods.timestart, tperiods.timeend, tperiods.activity from tperiods inner join ttname on ttname.id = tperiods.timetable where ttname.default = '" & 1 & "' and tperiods.day = '" & Now.DayOfWeek.ToString & "' order by tperiods.timestart", con)
                Dim student10 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad10.ExecuteReader
                Dim period As Integer = 0

                Do While student10.Read
                    If student10(2).ToString = "Tutorial" Then
                        period = period + 1
                    End If
                    If period = item Then

                        rows = rows + "<li><span class='message-serial message-cl-" & IIf(count + 1 = 1, "one", IIf(count + 1 = 2, "two", IIf(count + 1 = 3, "three", IIf(count + 1 = 4, "four", IIf(count + 1 = 5, "five", IIf(count + 1 = 6, "six", IIf(count + 1 = 7, "seven", IIf(count + 1 = 8, "eight", "one")))))))) & "'>" & count + 1 & "</span> <span class='message-info'>" & subjects(count) & "</span> <span class='message-time'>" & student10(0).ToString & " - " & student10(1).ToString & "</span></li>"
                        Exit Do
                    End If

                Loop
                student10.Close()
                count = count + 1
            Next
            lite.Text = rows
            plcSchedule.Controls.Add(lite)
            con.close()        End Using
        recent_submissions()
    End Sub
    Private Sub recent_submissions()
        Using con As New MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("PortalDBConnectionString").ConnectionString)            con.open()
            Dim ref As New ArrayList
            Dim dates As New ArrayList
            Dim subject As New ArrayList
            Dim title As New ArrayList
            Dim deadline As New ArrayList
            Dim passports As New ArrayList
            Dim cmdLoad1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT assignments.id, assignments.date, subjects.subject, assignments.title, assignments.deadline, staffprofile.passport from assignments inner join staffprofile on staffprofile.staffid = assignments.teacher inner join subjects on subjects.Id = assignments.subject inner join class on class.id = assignments.class where assignments.class = '" & Session("ClassId") & "'  order by assignments.date desc", con)
            Dim reader As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad1.ExecuteReader
            Do While reader.Read
                ref.Add(reader.Item(0))
                dates.Add(reader.Item(1))
                subject.Add(reader.Item(2))
                title.Add(reader.Item(3))
                deadline.Add(reader.Item(4))
                passports.Add(reader.Item(5))
            Loop
            reader.Close()
            Dim count As Integer = 0
            Dim status As New ArrayList
            Dim path As String = "http://" & Request.Url.Authority
            Dim submit As String = ""

            For Each item As Integer In ref
                Dim cmdLoad11 As New MySql.Data.MySqlClient.MySqlCommand("SELECT * from submittedassignments where assignment = '" & item & "' and student = '" & Session("studentid") & "'", con)
                Dim reader2 As MySql.Data.MySqlClient.MySqlDataReader = cmdLoad11.ExecuteReader
                If reader2.HasRows Then
                Else
                    Dim dob As Date = dates(count)
                    Dim sage As TimeSpan = Now.Subtract(dob)
                    Dim timelag As String = ""
                    If sage.Days < 1 Then
                        If sage.Hours < 1 Then
                            If sage.Minutes < 1 Then
                                timelag = "Just now"
                            ElseIf sage.Minutes = 1 Then
                                timelag = 1 & " Minute ago"
                            Else
                                timelag = sage.Minutes & " Minutes ago"
                            End If
                        ElseIf sage.Hours = 1 Then
                            timelag = "1 hour ago"
                        Else
                            timelag = sage.Hours & " Hours ago"
                        End If
                    ElseIf sage.Days = 1 Then
                        timelag = "1 Day ago"
                    Else
                        timelag = sage.Days & " Days ago"
                    End If
                    Dim pass As String = path + "/img/noimage.jpg"
                    If passports(count) <> "" Then pass = path + Replace(passports(count), "~", "")

                    submit = submit & "<div class='daily-feed-list'>" & _
                                                "<div class='daily-feed-img'>" & _
                                                    "<a href='#'><img style='height:55px; width:55px; vertical-align:center;' src='" & pass & "' alt=''/>" & _
                                                    "</a>" & _
                                                "</div>" & _
                                                "<div class='daily-feed-content'>" & _
                                                    "<h4 id='submi' style='width:95%; text-align:left;'>You have an undone assignment on " & subject(count) & " - " & title(count) & ". Deadline is " & deadline(count) & "</h4>" & _
                                                    "<span class='feed-ago'>" & timelag & "</span>" & _
                                                "</div>" & _
                                            "</div>" & _
                                           "<div class='clear'></div>"






                End If
                reader2.Close()
                count = count + 1
            Next

            Dim assignments As New Literal

            assignments.Text = submit
            plcSubmissions.Controls.Add(assignments)
            con.close()        End Using
    End Sub


    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Try
            clas_info()
            Daily_schedule()
        Catch ex As Exception
            Show_Alert(False, logify.error_log(sender.ToString, Replace(ex.ToString, "'", "")))
        End Try

    End Sub
End Class
