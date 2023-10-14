Imports System.Data
Imports System.Net
Imports System.Text
Imports System.IO



Public Class frmGeneralInfo

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        'If btnAccept.Text = "Demo" Then
        '    pnlDemoVersion.Visible = False
        '    lblDemoMessage.Text = ""
        '    pnlLicenseScreen.Visible = False
        '    btnAccept.Text = "I Agree"
        '    Exit Sub
        'End If

        blnFormClose = False
        chkInternetConn.Visible = False
       
        If chkInternetConn.Checked = True Then

            If File.Exists(Application.StartupPath & "\InternetConnection.txt") = False Then
                File.Create(Application.StartupPath & "\InternetConnection.txt")
            End If

            lblDownload.Visible = True
            pctProcess.Visible = True

            lblDownload.BringToFront()
            pctProcess.BringToFront()

            pnlInstructions.Visible = False
            pnlGeneInfo.Visible = False

            blnProcessValidationDelegate = False
            blnProcessValidation = False



            Dim dlgDnf As DoProcessValidation
            dlgDnf = AddressOf DownLoadchecking
            dlgDnf.BeginInvoke(Nothing, Nothing)
            Do While blnProcessValidationDelegate = False
                Application.DoEvents()
            Loop

            If blnProcessValidation = False Then
                frmFileSelection.Show()
                Me.Hide()
            Else
                frmDownLoad.Show()
                Me.Hide()
            End If
            lblDownload.Visible = False
            pctProcess.Visible = False


        Else
            File.Delete(Application.StartupPath & "\InternetConnection.txt")
            frmFileSelection.Show()
            Me.Hide()
        End If

    End Sub

    Private Sub frmGeneralInfo_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    Private Sub frmGeneralInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        'Ver 3.0.7-REQ417 start
        'Dim bPassStartUpPath As Boolean
        'bPassStartUpPath = True
        'blnchkVerifyPAN = True
        'blnChkDNFIntegrate = 0
        'Dim strTdsPacParamter() As String
        'strTdsPacParamter = Command.Split("~")
        'If strTdsPacParamter.Length > 0 And strTdsPacParamter(0) <> "" Then
        '    strFilePath = strTdsPacParamter(0)
        '    blnIsProductRegistered = (strTdsPacParamter(1))
        '    blnchkVerifyPAN = strTdsPacParamter(2)
        '    blnchkLateFiling = strTdsPacParamter(3)
        '    strLateFilingDate = strTdsPacParamter(4)
        '    strCSIValidPath = strTdsPacParamter(5)
        '    blnChkListPANName = strTdsPacParamter(6)
        '    blnChkDNFIntegrate = 1
        '    ' MsgBox("-1" & blnChkDNFIntegrate)
        'Else
        '    blnChkDNFIntegrate = 0
        'End If
        '*******************************************Testing purpose.........***********************************************
        'strFilePath = "C:\Working Dir\FF-TFS\DotNet Projects\Window Based\TdsPAcSql_DNF\bin\British Gas India Private Limited2014-2015\26Q\26QQ1.txt"
        'blnIsProductRegistered = True
        'blnchkVerifyPAN = True
        'blnchkLateFiling = True
        'strLateFilingDate = "17/10/2014"
        'strCSIValidPath = "C:\Users\u0167635\Desktop\MUMF03364E180914.csi"
        'blnChkListPANName = True
        'blnChkDNFIntegrate = 1
        '*******************************************Testing purpose.........***********************************************


        'If Application.StartupPath <> "C:\eTdsDNF" & ConstStrFYyr Then
        '    MessageBox.Show("Please run the application from C:\eTdsDNF" & ConstStrFYyr & " folder.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End
        'End If


        ' If blnChkDNFIntegrate = False Then
        'If Application.StartupPath <> "C:\eTdsDNF" & ConstStrFYyr Then
        '    MessageBox.Show("Please run the application from C:\eTdsDNF" & ConstStrFYyr & " folder.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End
        'End If
        'End If
        'Ver 3.0.7-REQ417 end 


        ' If blnChkDNFIntegrate = False Then   'Ver 3.0.7-REQ417 start 
        If blnDemoVersion = True Then
            '==>Jitendra
            ' ''If blnIsSoftLicenceActivated = True Then
            ' ''    Me.Hide()
            ' ''    Dim obja As New frmLicense
            ' ''    obja.Show()
            ' ''    blnChkDNFIntegrate = False
            ' ''    blnIsDemoVersion = True
            ' ''    ' MsgBox("-2" & blnChkDNFIntegrate)
            ' ''    Exit Sub                
            ' ''End If
            'If blnIsDemoVersion = False Then
            '    Exit Sub
            'End If
            '<==Jitendra 
            'lblDongleSrno.Text = "Demo"
            'If strDemoMessage <> "" Then
            '    pnlDemoVersion.Visible = True
            '    pnlLicenseScreen.Visible = False
            '    lblDemoMessage.Text = strDemoMessage
            'Else
            pnlDemoVersion.Visible = False
            '    pnlLicenseScreen.Visible = True

            '    If strExistingProduct <> "" Then
            '        lblExistingPro.Text = lblExistingPro.Text & " " & strExistingProduct
            '        lblDongleSrno.Text = lblDongleSrno.Text & " " & strDongleSrNoforBuying
            '        lblExistingPro.Visible = True
            '        lblDongleSrno.Visible = True
            '    Else
            '        lblExistingPro.Visible = False
            '        lblDongleSrno.Visible = False
            '    End If
            'End If
            btnAccept.Text = "Demo"

        Else
            pnlDemoVersion.Visible = False
            lblDemoMessage.Text = ""
            pnlLicenseScreen.Visible = False
            btnAccept.Text = "I Agree"

            'Ver 1.01-E585 start
            'ver 3.01-REQ297 start
            'Call CheckCustomerRegistration()

            ''License Free Start
            'If blnIsProductRegistered = False Then
            '    'License Free end
            ''If blnIsDemoVersion = True Then
            'Call CheckNewCustomerRegistration()
            ''End If


            '    'License Free Start
            'End If
            ''License Free end

            'ver 3.01-REQ297
            'Ver 1.01-E585 End
        End If
        'Ver 3.0.7-REQ417 start 
        'Else
        '        pnlDemoVersion.Visible = False
        '        lblDemoMessage.Text = ""
        '        pnlLicenseScreen.Visible = False
        '        btnAccept.Text = "I Agree"
        '        'Call CheckNewCustomerRegistration()
        'End If
        'Ver 3.0.7-REQ417 end 
        Me.CenterToScreen()

        If File.Exists(Application.StartupPath & "\InternetConnection.txt") = True Then
            chkInternetConn.Checked = True
        End If


        If blnChkDNFIntegrate = True Then
            Me.Size = New System.Drawing.Size(798, 525)
        End If


    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        End
    End Sub

    Private Sub frmGeneralInfo_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        blnFormClose = True
        'Ver 3.0.7-REQ417 start
        'MsgBox("-3" & blnChkDNFIntegrate)

        If blnChkDNFIntegrate = True And blnDemoVersion = False Then
            Call btnAccept_Click(sender, e)
        End If
        'Ver 3.0.7-REQ417 end 

        '==>Jitendra
        'MsgBox("-4" & blnIsSoftLicenceActivated)
        'MsgBox("-5" & blnIsDemoVersion)
        ' ''If blnIsSoftLicenceActivated = True And blnIsDemoVersion = True Then
        ' ''    Me.Hide()
        ' ''End If

        '<==Jitendra
    End Sub
    Private Function DownLoadchecking() As Boolean
        Dim strUpgradeDate As Date

        Application.DoEvents()
        blnProcessValidation = False

        If File.Exists(Application.StartupPath & "\tmpDNFUpgrade.txt") = False Then
            Call WriteTxtFile()

            'If CheckInternetConnection() = True Then
            If ValidVersion(ConstStrFYyr, My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision) = False Then
                blnProcessValidation = True
            End If
            'End If
        Else
            Dim fs As New FileStream(Application.StartupPath & "\tmpDNFUpgrade.txt", FileMode.Open)
            Dim sr As New StreamReader(fs)
            Do Until sr.Peek = -1
                strUpgradeDate = sr.ReadLine
                If Val(strUpgradeDate.ToString("yyyyMMdd")) >= Val(Today.Year & Format(Today.Month, "00") & Format(Today.Day, "00")) Then
                    sr.Dispose()
                    fs.Dispose()
                    blnProcessValidationDelegate = True
                    blnProcessValidation = False
                    Return False
                Else
                    sr.Dispose()
                    File.Delete(Application.StartupPath & "\tmpDNFUpgrade.txt")
                    Call WriteTxtFile()
                    'If CheckInternetConnection() = True Then
                    If ValidVersion(ConstStrFYyr, My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision) = False Then
                        blnProcessValidation = True
                        'End If
                    Else
                        blnProcessValidationDelegate = True
                        blnProcessValidation = False
                        Return False
                    End If
                    blnProcessValidationDelegate = True
                    Return True
                    Exit Do
                End If
            Loop
            sr.Dispose()
            fs.Dispose()
        End If

        'If File.Exists("C:\etdsDNF" & ConstStrFYyr & "\tmpDNFUpgrade.txt") = False Then
        '    Call WriteTxtFile()

        '    'If CheckInternetConnection() = True Then
        '    If ValidVersion(ConstStrFYyr, My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision) = False Then
        '        blnProcessValidation = True
        '    End If
        '    'End If
        'Else
        '    Dim fs As New FileStream("C:\etdsDNF" & ConstStrFYyr & "\tmpDNFUpgrade.txt", FileMode.Open)
        '    Dim sr As New StreamReader(fs)
        '    Do Until sr.Peek = -1
        '        strUpgradeDate = sr.ReadLine
        '        If Val(strUpgradeDate.ToString("yyyyMMdd")) >= Val(Today.Year & Format(Today.Month, "00") & Format(Today.Day, "00")) Then
        '            sr.Dispose()
        '            fs.Dispose()
        '            blnProcessValidationDelegate = True
        '            blnProcessValidation = False
        '            Return False
        '        Else
        '            sr.Dispose()
        '            File.Delete("C:\etdsDNF" & ConstStrFYyr & "\tmpDNFUpgrade.txt")
        '            Call WriteTxtFile()
        '            'If CheckInternetConnection() = True Then
        '            If ValidVersion(ConstStrFYyr, My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision) = False Then
        '                blnProcessValidation = True
        '                'End If
        '            Else
        '                blnProcessValidationDelegate = True
        '                blnProcessValidation = False
        '                Return False
        '            End If
        '            blnProcessValidationDelegate = True
        '            Return True
        '            Exit Do
        '        End If
        '    Loop
        '    sr.Dispose()
        '    fs.Dispose()
        'End If

        blnProcessValidationDelegate = True
        Return False

    End Function
    Public Shared Function CheckInternetConnection() As Boolean

        Dim objUrl As New System.Uri("http:\\www.google.com")
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objUrl)
        Dim objResp As System.Net.WebResponse
        Try
            objWebReq.Timeout = 20000
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            objWebReq = Nothing
            Return False
        End Try
        
    End Function

    Private Sub chkInternetConn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInternetConn.CheckedChanged
        'If chkInternetConn.Checked = True Then
        '    If File.Exists(Application.StartupPath & "\InternetConnection.txt") = False Then
        '        File.Create(Application.StartupPath & "\InternetConnection.txt")

        '    End If
        'Else
        '    File.Delete(Application.StartupPath & "\InternetConnection.txt")
        'End If
    End Sub
    Public Sub WriteTxtFile()

        'Dim fs As New FileStream("C:\etdsDNF" & ConstStrFYyr & "\tmpDNFUpgrade.txt", FileMode.Create)
        Dim fs As New FileStream(Application.StartupPath & "\tmpDNFUpgrade.txt", FileMode.Create)
        Dim sw As New StreamWriter(fs)
        sw.WriteLine(Format(Today.Day, "00") & "/" & Format(Today.Month, "00") & "/" & Format(Today.Year, "0000"))
        sw.Flush()
        sw.Dispose()
        fs.Dispose()

    End Sub


    Private Sub lblBuyingLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblBuyingLink.LinkClicked

        Process.Start("http:\\www.fastfacts.co.in\eTdsDNF.asp")
    End Sub

   
    
End Class
