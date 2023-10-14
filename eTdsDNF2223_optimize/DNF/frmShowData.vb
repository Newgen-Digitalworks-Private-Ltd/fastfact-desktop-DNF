
Imports System.IO
Public Class frmShowData

    Private Sub frmShowData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DataGridView1.DataSource = dsMain.Tables("DeducteeDetails")
        DataGridView1.Refresh()



        Dim dv As New DataView(dsMain.Tables("DeducteeDetails"))
        dv.RowFilter = "TotInterest >0"
        DataGridView2.DataSource = dv
        DataGridView2.Refresh()


        Dim dv1 As New DataView(dsMain.Tables("DeducteeDetails"))
        dv1.RowFilter = "ShortDedAmount >0"
        DataGridView3.DataSource = dv1
        DataGridView3.Refresh()


        Dim dv2 As New DataView(dsMain.Tables("ChallanDetails"))
        dv2.RowFilter = "IsChallanMatched = False"
        DataGridView4.DataSource = dv2
        DataGridView4.Refresh()


        DataGridView5.DataSource = dsMain.Tables("SalaryDetails")
        DataGridView5.Refresh()


    End Sub
    Public Sub ExportToExcel(ByVal dsExport As DataTable)
        Try
            Dim IEProcess As Process = New Process

            Dim ts As StreamWriter
            Dim i, j As Integer
            Dim mExpText As String
            Dim mStr As String
            Dim mPos As Integer

            Dim ExlAppln As New Object
            Dim ExlBook As New Object

            ExlAppln = CreateObject("Excel.Application")

            ts = New StreamWriter(Application.StartupPath & "\ExportP.xls")

            mExpText = ""
            mStr = ""


            For k As Integer = 0 To dsExport.Columns.Count - 1
                mStr = dsExport.Columns(k).ColumnName.ToString
                If mExpText = "" Then
                    mExpText = IIf(mStr = "", Space(1), mStr)
                    'mExpText = mExpText & Chr(9) & mStr
                Else
                    mExpText = mExpText & Chr(9) & IIf(mStr = "", Space(1), mStr)
                End If

                'memptext=
                'MsgBox(dsExport.Tables(0).Columns(k).ColumnName)
            Next
            ts.WriteLine(mExpText)

            mExpText = ""
            mStr = ""


            For i = 0 To dsExport.Rows.Count - 1
                mStr = ""
                For j = 0 To dsExport.Columns.Count - 1
                    If mExpText = "" Then

                        'mExpText = IIf((dsExport.Tables(0).Rows(i).Item(j) = ""), Space(1), dsExport.Tables(0).Rows(i).Item(j))
                        If IsDBNull(dsExport.Rows(i).Item(j)) = False Then

                            'mExpText = IIf((dsExport.Tables(0).Rows(i).Item(j) = ""), Space(1), dsExport.Tables(0).Rows(i).Item(j))
                            mExpText = IIf((Convert.ToString(dsExport.Rows(i).Item(j)) = ""), Space(1), Convert.ToString(dsExport.Rows(i).Item(j)))

                        End If

                        mExpText = IIf(Mid(mExpText, 1, 1) = "0", "'" & mExpText, mExpText)
                    Else

                        'mPos = InStr(1, dsExport.Tables(0).Rows(i).Item(j), Chr(13))
                        'mStr = dsExport.Tables(0).Rows(i).Item(j)
                        If IsDBNull(dsExport.Rows(i).Item(j)) = False Then
                            mPos = InStr(1, dsExport.Rows(i).Item(j), Chr(13))

                            ' mStr = dsExport.Tables(0).Rows(i).Item(j)
                            mStr = Convert.ToString(dsExport.Rows(i).Item(j))

                        Else
                            mStr = vbNullString
                        End If


                        Do While mPos > 0
                            mStr = Mid(mStr, 1, Val(mPos) - 1) & " " & Mid(mStr, Val(mPos) + 2)
                            mPos = InStr(1, mStr, Chr(13))
                        Loop

                        mExpText = mExpText & Chr(9) & mStr
                    End If
                Next
                ts.WriteLine(mExpText)
                mExpText = ""
            Next
            ts.Close()

            If File.Exists(Application.StartupPath & "\ExportP.xls") = True Then
                Process.Start(Application.StartupPath & "\ExportP.xls")
            End If
        Catch ex As Exception
            MsgBox("The process cannot access the file """ & Application.StartupPath & "\ExportP.xls"" because it is being used by another process.")
            If ex.Message = "The process cannot access the file """ & Application.StartupPath & "\ExportP.xls"" because it is being used by another process." Then
                MsgBox("Close file 'ExportP.xls' and then try again!")
            Else
                MsgBox(ex.Message)
            End If

        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ExportToExcel(dsMain.Tables("DeducteeDetails"))
    End Sub


    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        ExportToExcel(dsMain.Tables("ChallanDetails"))
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MsgBox(MonthDifference(TextBox1.Text, TextBox2.Text, TextBox3.Text))
    End Sub
End Class