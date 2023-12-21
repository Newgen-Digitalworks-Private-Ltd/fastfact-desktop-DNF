﻿Imports System.Data.OleDb
Imports System.IO
Imports System.Text

Public Class frmTraces
    Dim csvdt As DataTable

    Private Sub btnvalidpan_Click(sender As Object, e As EventArgs) Handles btnvalidpan.Click


        If txtuserid.Text <> " " And txtpwd.Text <> " " And lbltan.Text <> "" Then
            Pantraces()
            readcsv()
        Else
            MsgBox("Please enter the Login Credentials")
        End If

    End Sub
    Private Sub readcsv()
        ' Define the CSV file path
        Dim fileName As String = Application.StartupPath & "\traces\report.csv"

        ' Read the contents of the CSV file
        Dim contents As String = File.ReadAllText(fileName)

        ' Remove double quotes from the contents and overwrite the file
        File.WriteAllText(fileName, contents.Replace("""", ""))

        'lblPanProcess.Text = strPanProcess
        'lblPanStatus.Text = "Status  : " & strPanStatus

        Dim mfile As File
        If mfile.Exists(Application.StartupPath & "\traces\report.csv") = True Then
            Dim strPath As String = IO.Path.GetDirectoryName(Application.StartupPath & "\traces\report.csv")
            Dim strFile As String = IO.Path.GetFileName(Application.StartupPath & "\traces\report.csv")
            csvdt = getcsvtable(strPath)
        Else
            MsgBox("Please check ...", vbCritical, "")
        End If
        Dim totpan, actpan, inactpan As Integer
        If IsNothing(csvdt) = False Then
            csvdt.Columns.Add("ActiveStatus")

            csvdt.Select("Status = 'Active' or Status = 'Valid and Operative'").ToList().ForEach(Sub(row As DataRow)
                                                                                                     row.SetField(Of String)("ActiveStatus", "1")
                                                                                                 End Sub)

            csvdt.Select("Status = 'Invalid PAN' or Status = 'Invalid'").ToList().ForEach(Sub(row As DataRow)
                                                                                              row.SetField(Of String)("ActiveStatus", "2")
                                                                                          End Sub)

            'If blnInternetConnectionFailed = True Then 'Added for Ver 8.0.0.4 for check internet is not active status
            '    lblPanStatus.Text = "Status  :  Connection failed please re-run DNF again!"
            'Else
            '    lblPanStatus.Text = "Status  :  PAN verification completed."
            'End If


            totpan = Convert.ToInt32(lblPanProcess.Text)
            actpan = csvdt.Select("ActiveStatus = '1'").ToList().Count
            inactpan = Convert.ToInt32(lblPanProcess.Text) - actpan
        End If



        If inactpan > 0 Then
            Dim inactft As DataTable
            Dim selectedRows As DataRow() = csvdt.Select("ActiveStatus = '2'")

            'Using writer As New StreamWriter(strValidPANListFileTracesPath)
            '    ' Write each line of data to the file
            '    For Each row As DataRow In selectedRows
            '        writer.WriteLine(line)
            '    Next
            'End Using

            Dim csvContent As New StringBuilder()

            Dim columns As String() = {"InVAlidPAN"}
            csvContent.AppendLine(String.Join(",", columns))

            For Each row As DataRow In selectedRows
                csvContent.AppendLine(String.Join(",", row(1)))
            Next
            File.WriteAllText(strValidPANListFileTracesPath, csvContent.ToString())


        End If


        If inactpan = 0 Then

            lblPanProcess1.Visible = False

            'If blnPANCount = True Then
            lblPanProcess.Visible = True
            lblPanProcess.Text = actpan & " PAN's Verification Completed."
            lblPanProcess1.Visible = False
            blnPANVerification = True
            actCntInvalidPan = actpan
            'Else
            '    lblPanProcess.Visible = False
            'End If
        End If

        If inactpan > 0 Then
            lblPanProcess1.Visible = True
            lblPanProcess1.Text = inactpan & " PAN's not found in Traces."
            lblPanProcess.Visible = False
            intCntInvalidPan = inactpan
            blnPANVerification = True
            'If blnDemoVersion = True Then
            '    lblPanProcess1.Enabled = False
            'Else
            '    lblPanProcess1.Enabled = True
            'End If
        Else
            lblPanProcess1.Text = ""
        End If

        'CheckPANwithExistingTracesList()

    End Sub
    Public Function getcsvtable(ByVal csvfilepath As String) As DataTable
        Dim objDs As New DataSet()
        Try

            Dim strPath As String = IO.Path.GetDirectoryName(Application.StartupPath & "\traces\report.csv")
            Dim strFile As String = IO.Path.GetFileName(Application.StartupPath & "\traces\report.csv")

            'Create a connection object
            Using objConn As New OleDbConnection($"Provider= Microsoft.Jet.OLEDB.4.0;Data Source={strPath};Extended Properties=""Text;""")
                objConn.Open()
                'create a command object
                Using objCmdSelect As New OleDbCommand($"Select * FROM {strFile}", objConn)
                    'create an adapter object
                    Using objAdapter1 As New OleDbDataAdapter()
                        objAdapter1.SelectCommand = objCmdSelect
                        'fill the dataset using the adapter

                        objAdapter1.Fill(objDs)

                        'DataGridView1.DataSource = objDs.Tables(0)

                        Return objDs.Tables(0)

                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Function

    Private Sub Pantraces()
        Try
            Dim result As String = Nothing
            Dim qtid As String
            qtid = " PANVERIFY " & Application.StartupPath & "\traces\pan.txt" & " " & txtuserid.Text & " " & txtpwd.Text & " " & lbltan.Text
            'qtid = " PANVERIFY " & Application.StartupPath & "\traces\pan.txt" & "   " & " " & lbltan.Text
            Dim epubCheckPath As String = Application.StartupPath & "\traces\traces.jar"
            Dim arguments As String = "-jar " & epubCheckPath & " " & qtid

            Dim pProcess As New System.Diagnostics.Process()

            pProcess.StartInfo.FileName = "java"
            pProcess.StartInfo.Arguments = arguments

            Debug.WriteLine("arguments: " & pProcess.StartInfo.Arguments)

            pProcess.StartInfo.UseShellExecute = False
            pProcess.StartInfo.CreateNoWindow = True
            pProcess.StartInfo.RedirectStandardOutput = True

            pProcess.Start()
            result = pProcess.StandardOutput.ReadToEnd()

            pProcess.WaitForExit()
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Debug.WriteLine(ex.StackTrace)
            Throw ex
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = False Then
            CheckBox1.Checked = True
            'Button1.BackgroundImage = ImageList1.Images(1)
            'Button1.BackgroundImageLayout = ImageLayout.Zoom
        Else
            CheckBox1.Checked = False
            'Button1.BackgroundImage = ImageList1.Images(0)
            'Button1.BackgroundImageLayout = ImageLayout.Zoom
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txtpwd.PasswordChar = ""
        Else
            txtpwd.PasswordChar = "*"
        End If
    End Sub

    Private Sub frmTraces_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'lbltan.Text = "CHEH00378A"
        'txtuserid.Text = "Epage@123"
        'txtpwd.Text = "Digital2017"
    End Sub

    Public Function CheckPANwithExistingTracesList()
        Dim fs As FileStream
        Dim strRd As StreamReader
        Dim mCode As String = ""
        Dim strPANs() As Object

        'Dim strValidPANListFileTracesPath As String = Application.StartupPath & "\traces\report.csv"
        Try
            If File.Exists(strValidPANListFileTracesPath) Then
                fs = New FileStream(strValidPANListFileTracesPath, FileMode.Open, FileAccess.Read)
                strRd = New StreamReader(fs)
                'Dim strPANs() As Object
                Dim dtPAN As New DataTable("ExistingPAN")
                dtPAN.Columns.Add("PAN", System.Type.GetType("System.String"))
                'dtPAN.Columns.Add("PAN1")
                Dim pk(1) As DataColumn
                pk(0) = dtPAN.Columns("PAN")
                dtPAN.PrimaryKey = pk
                Do Until strRd.EndOfStream = True
                    strLine = strRd.ReadToEnd
                    strPANs = strLine.Split(",")
                Loop
                Dim dataviewPAN As New DataView(dtTempPAN)
                For i = 0 To strPANs.Length - 1
                    dataviewPAN.RowFilter = "PAN = '" & strPANs(i) & "'"
                    If dataviewPAN.Count > 0 Then
                        dataviewPAN.Item(0).Delete()
                    End If
                Next
                strRd.Close()
                strRd.Dispose()
                fs.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Function

End Class