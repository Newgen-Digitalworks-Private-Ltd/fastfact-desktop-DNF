Imports System.Net
Imports System.IO

Public Class Userstatus

    Private Sub Userstatus_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If strDogSrNo = "" Then
            ''License Free Start
            'Dim wbClient As New WebClient
            'strWebAppVersion = ""
            'Try
            '    strWebAppVersion = wbClient.DownloadString("http://www.ffcs.in/FreeWizardDNFTest.html")
            '    blnDemoVersion = False
            '    blnIsProductRegistered = True
            '    strDogSrNo = "00000"
            'Catch ex As Exception
            '    'License Free end
            ''If Dongle() = False Then
            ''End If
            ''License Free Start
            'End Try
            ''License Free end
        End If
        If blnDemoVersion = True Then
            strDogSrNo = "Demo"
        End If


        ' lblVer.Text = "Version: " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision & "    Dated: " & My.Application.Info.Description & "    SrNo.: " & strDogSrNo
        If blnChkDNFIntegrate = True Then
            lblVer.Text = "Version: " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision & "    Dated: " & My.Application.Info.Description
        Else
            lblVer.Text = "Version: " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision & "    Dated: " & My.Application.Info.Description & "  Customer Code.: " & strCustomerCode
        End If

        cmslcust.Text = ""
        getCMSL()
    End Sub
    Private Sub getCMSL()
        Dim cmsl_licno As String = String.Empty
        Dim cmsl_custcode As String = String.Empty
        If File.Exists(Application.StartupPath & "\SLDetails.xml") Then
            Dim ds_LIC As New DataSet
            ds_LIC.ReadXml((Application.StartupPath & "\SLDetails.xml"))
            If ds_LIC.Tables(0).Rows(0)("RegKey").ToString() <> "" Then
                cmsl_licno = ds_LIC.Tables(0).Rows(0)("RegKey").ToString().Substring(ds_LIC.Tables(0).Rows(0)("RegKey").ToString().Length - 4)
            End If


            Dim web As New ffcsCMSL.UpgradeLicense
            Try
                cmsl_custcode = web.GetCustSerialNoByLicNo(cmsl_licno)
            Catch ex As Exception
                'Exit Function
            End Try

            If cmsl_custcode <> "NO RECORD FOUND" Then
                cmslcust.Text = "Customer ID : " & cmsl_custcode
            Else
                cmslcust.Text = ""
            End If


        End If




    End Sub


    Private Sub lblBottomEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBottomEmail.Click
        Try

            Process.Start(String.Format("mailto:{0}", "etds@fastfacts.co"))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub lblTeamViewer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTeamViewer.Click
        CallApplication(My.Application.Info.DirectoryPath & "\QS.exe")
    End Sub

    Private Sub lblShowMyPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblShowMyPC.Click
        CallApplication(My.Application.Info.DirectoryPath & "\ShowMyPC.exe")
    End Sub


    Public Sub CallApplication(ByVal strApplication As String)
        Try
            If (File.Exists(strApplication) = True) Then
                Process.Start(strApplication)
            Else
                MessageBox.Show(strApplication + " application is missing", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub pbBottomEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbBottomEmail.Click
        Try

            Process.Start(String.Format("mailto:{0}", "etds@fastfacts.co"))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub pcTeamViewer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcTeamViewer.Click
        CallApplication(My.Application.Info.DirectoryPath & "\QS.exe")
    End Sub

    Private Sub pbShowMyPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbShowMyPC.Click
        CallApplication(My.Application.Info.DirectoryPath & "\ShowMyPC.exe")
    End Sub

    Public Sub OpenWebLink()

        Process.Start("www.fastfacts.co.in")
    End Sub

    Private Sub lblWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblWeb.Click
        OpenWebLink()
    End Sub

    Private Sub pbWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbWeb.Click
        OpenWebLink()
    End Sub
End Class