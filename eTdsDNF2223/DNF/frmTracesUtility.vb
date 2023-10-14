
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Resources
Imports System.Reflection
Imports System.Diagnostics
Imports System.IO.Compression
Imports System.Text
Imports Newtonsoft.Json
Imports System.Data.OleDb
Imports Newtonsoft.Json.Linq
'Imports officeOpenxml

Public Class frmTracesUtility
    Dim destTable As New DataTable()
    'Dim loadConfigFiles As New LoadConfigFiles()
    Dim loadConfigFiles As String = Application.StartupPath + "\\ACF.dll"
    Dim dsConfig As New DataSet()
    Dim dt As New DataTable()
    Dim csvdt As New DataTable()
    Dim objDs As New DataSet()
    Dim dt1 As New DataTable()
    Dim csvdt1 As New DataTable()
    Dim objDs1 As New DataSet()
    Dim chlnfromdt As String
    Dim chlntodt As String
    Dim strProductName As String = "eTdsDNF"
    Dim strMessageboxHeading As String = "eTdsDNF"
    Dim data_Structure As New DataSet()

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtuserid.Text <> "" AndAlso txtpwd.Text <> "" Then
            Try
                Dim fileName As String = Application.StartupPath & "\traceslogin.txt"

                If File.Exists(fileName) Then
                    File.Delete(fileName)
                End If

                Dim fs As FileStream = File.Create(fileName)
                fs.Close()

                Using writer As New StreamWriter(fileName)
                    writer.Write(txtuserid.Text & Environment.NewLine)
                    writer.Write(txtpwd.Text)
                    MessageBox.Show("Data Saved Successfully", "eTdsDNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End If

    End Sub

    Private Sub btnvalidate_Click(sender As Object, e As EventArgs) Handles btnvalidate.Click
        create_pan()
        Pantraces()
        readcsv()
        'validatepan()
        DataGridView1.DataSource = Nothing
        DataGridView1.DataSource = csvdt
    End Sub
    Private Sub create_pan()
        Try
            Dim fileName As String = Application.StartupPath & "\traces\pan.txt"

            ' Check if the file already exists
            If File.Exists(fileName) Then
                File.Delete(fileName)
            End If

            Dim fs As FileStream = File.Create(fileName)
            fs.Close()

            Using writer As New StreamWriter(fileName)
                writer.Write("Pan" & Environment.NewLine)

                ' Assuming data_Structure is a DataSet or DataTable
                Dim dtPanFiltered As DataTable = data_Structure.Tables("Deductee$").Copy()
                Dim distinctRows = (From dRow As DataRow In dtPanFiltered.Rows
                                    Select dRow(5)).Distinct().ToList()

                For i As Integer = 0 To distinctRows.Count - 1
                    Dim s As String = distinctRows(i).ToString()
                    writer.Write(s & Environment.NewLine)
                Next

                ' MessageBox.Show("Data Saved Successfully", "eTdsWizard", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Pantraces()
        Try
            Dim result As String = Nothing
            Dim qtid As String
            qtid = " PANVERIFY " & Application.StartupPath & "\traces\pan.txt" & " " & txtuserid.Text & " " & txtpwd.Text & " " & lbltan.Text

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
    Private Sub readcsv()
        ' Define the CSV file path
        Dim fileName As String = Application.StartupPath & "\traces\report.csv"

        ' Read the contents of the CSV file
        Dim contents As String = File.ReadAllText(fileName)

        ' Remove double quotes from the contents and overwrite the file
        File.WriteAllText(fileName, contents.Replace("""", ""))

        ' Optionally, load the CSV data into a DataTable (if getcsvtable is a valid function)
        ' csvdt = getcsvtable(fileName)
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        readcsv()
        'validatepan()
        DataGridView1.DataSource = Nothing
        DataGridView1.DataSource = csvdt
    End Sub

    Private Sub btnexport_Click(sender As Object, e As EventArgs) Handles btnexport.Click
        If DataGridView1.Rows.Count > 0 Then
            Panexporttoexcel()
        End If
    End Sub
    Private Sub Panexporttoexcel()
        'Try
        '    Dim folderPath As String = Application.StartupPath

        '    Using wb As New XLWorkbook()
        '        wb.Worksheets.Add(csvdt, "PanData")
        '        wb.SaveAs(folderPath & "\ValidatePan.xlsx")
        '    End Using

        '    Dim IEProcess As New Process()

        '    If File.Exists(Application.StartupPath & "\ValidatePan.xlsx") Then
        '        System.Diagnostics.Process.Start(Application.StartupPath & "\ValidatePan.xlsx")
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try

        'Imports OfficeOpenXml
        'Imports System.IO

        '' Assuming you have a DataTable named "dataTable" that you want to export

        '' Specify the file path where you want to save the XLS file
        'Dim filePath As String = "C:\Path\To\Your\File.xlsx"

        '' Create a new Excel package
        'Using excelPackage As New ExcelPackage()
        '    ' Create a new worksheet in the Excel package
        '    Dim worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1")

        '    ' Loop through the DataTable and populate the worksheet
        '    For colIndex As Integer = 0 To DataTable.Columns.Count - 1
        '        worksheet.Cells(1, colIndex + 1).Value = DataTable.Columns(colIndex).ColumnName
        '    Next

        '    For rowIndex As Integer = 0 To DataTable.Rows.Count - 1
        '        For colIndex As Integer = 0 To DataTable.Columns.Count - 1
        '            worksheet.Cells(rowIndex + 2, colIndex + 1).Value = DataTable.Rows(rowIndex)(colIndex)
        '        Next
        '    Next

        '    ' Save the Excel package to the specified file path
        '    Dim fileStream As New FileInfo(filePath)
        '    excelPackage.SaveAs(fileStream)
        'End Using



    End Sub

    Private Sub frmTracesUtility_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpwd.UseSystemPasswordChar = True
        'LoadTracesYear()
        loadtan()

        Try
            Dim fileName As String = Application.StartupPath & "\traceslogin.txt"

            If File.Exists(fileName) Then
                'Using sr As StreamReader = File.OpenText(fileName)
                Dim s As String = ""
                    Dim itlogin As String = ""
                'While (s = SR.ReadLine()) IsNot Nothing
                'While SR.Read()
                'If rowcount > 1 Then
                'itlogin = itlogin & sr & ","
                'MsgBox("anu")
                ''End While
                'Using sr As New StreamReader(fileName)
                '    'Dim s As String = sr.ReadLine()
                '    While s = sr.ReadLine() IsNot Nothing

                '        Do While s IsNot Nothing
                '        MsgBox(s)
                '        's = sr.ReadLine()
                '        itlogin = itlogin & s & ","
                '    Loop
                'End Using
                Using reader As New StreamReader(fileName)
                    Dim fileContents As String = reader.ReadToEnd()
                    reader.Close()
                    'Dim values() As String = fileContents.Split(","c)
                    Dim it_login As String() = fileContents.Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
                    Dim values() As String = fileContents.Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
                    For Each value As String In values
                        MsgBox(value)
                    Next
                    txtuserid.Text = it_login(0)
                    txtpwd.Text = it_login(1)
                End Using



                'Dim it_login As String() = itlogin.Split(","c)

                'End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub loadtan()
        ' Dim appfile As String = Application.StartupPath & "\\traces\\26qq2.xls"
        ' Dim appfile As String = Application.StartupPath & "\\traces request for 26q q2.xls"
        Dim appfile As String = lblpath.Text

        data_Structure = Import(appfile)
        gettan()
    End Sub
    Public Shared Function Import(ByVal path As String) As DataSet
        Dim dataStructure As New DataSet()
        Dim connectionString As String = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path};Extended Properties=""Excel 12.0;IMEX=1;HDR=Yes;TypeGuessRows=0;ImportMixedTypes=Text"""

        Using conn As New OleDbConnection(connectionString)
            Try
                conn.Open()
            Catch e As Exception
                MessageBox.Show($"Cannot connect to the OLEDB (Excel) driver with the connection string ""{connectionString}"".{vbCrLf}{e}")
                Return Nothing
            End Try

            Dim sheets As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})

            Using cmd As OleDbCommand = conn.CreateCommand()
                For Each row As DataRow In sheets.Rows
                    Dim tableName As String = row("TABLE_NAME").ToString()
                    Dim sql As String = $"SELECT * FROM [{tableName}]"
                    Dim oleDbDataAdapter As New OleDbDataAdapter(sql, conn)
                    oleDbDataAdapter.Fill(dataStructure, tableName)
                Next
            End Using

            conn.Close()
        End Using

        Return dataStructure
    End Function
    Private Sub gettan()
        Dim tan As DataTable
        tan = data_Structure.Tables("Deductor")

        Dim tanno As String = data_Structure.Tables("Deductor$").Rows(0).Field(Of String)(1)

        lbltan.Text = tanno
    End Sub


End Class