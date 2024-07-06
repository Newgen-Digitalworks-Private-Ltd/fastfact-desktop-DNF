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

Public Class frmProcess
    Private Sub frmProcess_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If blnFormClose = True Then
            End
        End If
    End Sub
    Private Sub frmProcess_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If File.Exists("C:\eTdsDNF" & ConstStrFYyr & "\Default-Notice-Forecaster.sys") Then
        '    FileSystem.Rename("C:\eTdsDNF" & ConstStrFYyr & "\Default-Notice-Forecaster.sys", Application.StartupPath & "\Default-Notice-Forecaster.xls")
        'End If

        Me.CenterToScreen()
        btnNext.Enabled = False
        If blnChkDNFIntegrate = True Then
            Me.Size = New System.Drawing.Size(798, 512)
            btnNext.Visible = False
        End If

    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        End
    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        blnFormClose = False
        blnNextFile = True
        DisposeAll()

        frmFileSelection.Show()
        Me.Close()
    End Sub
    'live
    'Private Sub frmProcess_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    'Ver 7.03-REQ816 start
    '    If dsMain.Tables("SalaryDetails").Columns.Contains("PANValid") = False Then
    '        dsMain.Tables("SalaryDetails").Columns.Add("PANValid")
    '    End If

    '    'Ver 7.03-REQ816 end
    '    Application.DoEvents()
    '    blnFormClose = True
    '    lblTan.Text = lblTan.Text & " " & TANDeductor
    '    lblFy.Text = lblFy.Text & " " & Mid(dtBH.Rows(0)(16), 1, 4) & "-" & Mid(dtBH.Rows(0)(16), 5, 4) 'Fin Yr
    '    lblQ.Text = lblQ.Text & " " & dtBH.Rows(0)(17) 'Period
    '    lblDedName.Text = lblDedName.Text & " " & dtBH.Rows(0)(18) 'Deductor Name
    '    lblFormNo.Text = lblFormNo.Text & " " & strFormNo

    '    Dim strPath() As String
    '    If blnVerifyChallanSkip = False Then
    '        strPath = strCSIPath.Split("\")
    '        lblCsiFile.Text = lblCsiFile.Text & strPath(strPath.Length - 1)
    '    Else
    '        lblCsiFile.Text = lblCsiFile.Text & " Skipped by user."
    '    End If

    '    lblchlStatus.Text = lblchlStatus.Text & " Total challan : " & dtCD.Rows.Count
    '    lblUnmatchchlStatus.Visible = True
    '    If blnDemoVersion = True Then
    '        lblUnmatchchlStatus.Enabled = False
    '    Else
    '        lblUnmatchchlStatus.Enabled = True
    '    End If

    '    If intCountOfUnMatched > 0 Then
    '        lblUnmatchchlStatus.Text = IIf(blnVerifyChallanSkip = True, "", "Unmatched challan : " & intCountOfUnMatched)
    '    Else
    '        lblUnmatchchlStatus.Visible = False
    '    End If

    '    Application.DoEvents()
    '    pctProcess1.Visible = False
    '    pctProcess2.Visible = False
    '    pctProcess4.Visible = False
    '    pctForeCastCorrect.Visible = False
    '    pctFileWrong.Visible = False
    '    pctDNFWrong.Visible = False
    '    pctFileCorrect.Visible = True

    '    'lblForecasting.Visible = False
    '    lblDNFStatus.Visible = False

    '    'lblSummary.Visible = False
    '    lblProbshortDedAmt.Visible = False
    '    lblProbshortDepAmt.Visible = False
    '    lblProbIntPay.Visible = False
    '    pctSummaryCorrect.Visible = False

    '    lblProbshortDedAmt1.Visible = False
    '    lblProbshortDepAmt1.Visible = False
    '    lblProbIntPay1.Visible = False

    '    'Ver 2.02-REQ235 start                   
    '    lblLateFilingFeeText.Visible = False
    '    lblLateFilingFee.Visible = False
    '    'Ver 2.02-REQ235 end



    '    Application.DoEvents()
    '    If blnVerifyChallanSkip = True Then
    '        pctChallanWrong.Visible = True
    '        pctChallanCorrect.Visible = False
    '    Else
    '        pctChallanCorrect.Visible = True
    '        pctChallanWrong.Visible = False
    '    End If
    '    '********************************************************TdsPac_PanVerification ChallanWithSalary 
    '    Application.DoEvents()
    '    lblPanProcess1.Text = "Status  :"
    '    lblPanProcess1.Visible = False
    '    intCntInvalidPan = 0
    '    If blnVerifyPANSkip = True Then
    '        pctPANWrong.Visible = True
    '        pctPANCorrect.Visible = False
    '        pctProcess3.Visible = False
    '        lblPanStatus.Text = lblPanStatus.Text & " Skipped by user."

    '    Else
    '        pctProcess3.Visible = True

    '        Application.DoEvents()
    '        pctPANWrong.Visible = False
    '        pctPANCorrect.Visible = False

    '        blnPANDelegate = False

    '        lblPanProcess.Visible = True

    '        Dim Delgt As DoPanValidation
    '        'Ver 4.042-QC?? start
    '        'Delgt = AddressOf PANVerificationLatest
    '        Delgt = AddressOf PanVerification
    '        'Ver 4.042-QC?? end
    '        Delgt.Invoke()

    '        Do While blnPANDelegate = False
    '            Application.DoEvents()
    '            lblPanProcess.Text = strPanProcess
    '            lblPanStatus.Text = "Status  : " & strPanStatus
    '        Loop


    '        If blnPANVerification = True Then
    '            pctPANWrong.Visible = True
    '            pctPANCorrect.Visible = False

    '            If blnInternetConnectionFailed = True Then 'Added for Ver 8.0.0.4 for check internet is not active status
    '                lblPanStatus.Text = "Status  :  Connection failed please re-run DNF again!"
    '            Else
    '                lblPanStatus.Text = "Status  :  PAN verification cancelled."
    '            End If

    '            'lblPanStatus.Text = "Status  : PAN verification cancelled."
    '        Else
    '            pctPANWrong.Visible = False
    '            pctPANCorrect.Visible = True

    '            If blnInternetConnectionFailed = True Then 'Added for Ver 8.0.0.4 for check internet is not active status
    '                lblPanStatus.Text = "Status  :  Connection failed please re-run DNF again!"
    '            Else
    '                lblPanStatus.Text = "Status  :  PAN verification completed."
    '            End If

    '            'lblPanStatus.Text = "Status  :  PAN verification completed."
    '        End If

    '        pctProcess3.Visible = False
    '    End If

    '    lblPanProcess1.Visible = False

    '    If blnPANCount = True Then
    '        lblPanProcess.Visible = True
    '    Else
    '        lblPanProcess.Visible = False
    '    End If

    '    If intCntInvalidPan > 0 Then
    '        lblPanProcess1.Visible = True
    '        lblPanProcess1.Text = intCntInvalidPan & " PAN's not found in ITD."
    '        If blnDemoVersion = True Then
    '            lblPanProcess1.Enabled = False
    '        Else
    '            lblPanProcess1.Enabled = True
    '        End If
    '    Else
    '        lblPanProcess1.Text = ""
    '    End If

    '    If intIgnoredPANList > 0 Then
    '        If File.Exists(strIgnoredPANListFilePath) = True Then
    '            lblIgnoredPANList.Visible = True
    '        End If

    '        lblIgnoredPANList.Text = intIgnoredPANList & " PAN Ignored"
    '        If blnDemoVersion = True Then
    '            lblIgnoredPANList.Enabled = False
    '        Else
    '            lblIgnoredPANList.Enabled = True
    '        End If
    '    Else
    '        lblIgnoredPANList.Text = ""
    '        lblIgnoredPANList.Visible = False
    '    End If


    '    '*************************************************************TdsPac_PanVerification ChallanWithSalary 
    '    Application.DoEvents()
    '    If blnInternetConnectionFailed = True Then
    '        pctPANWrong.Visible = True
    '    Else
    '        pctProcess4.Visible = True
    '    End If

    '    lblForecasting.Visible = True
    '    lblDNFStatus.Visible = True
    '    lblDNFStatus.Text = "Status  : Processing.."

    '    Application.DoEvents()
    '    blnDNFCreationDelegate = False
    '    blnFileFailure = False
    '    Dim dlgDnf As DoDNFCreation
    '    'MessageBox.Show("Check DNF forecast")
    '    dlgDnf = AddressOf CreateExcelFile
    '    dlgDnf.BeginInvoke(Nothing, Nothing)
    '    Do While blnDNFCreationDelegate = False
    '        Application.DoEvents()
    '    Loop

    '    'Call CreateExcelFile()
    '    pctProcess4.Visible = False
    '    If blnFileFailure = False Then
    '        lblOpenFile.Visible = True
    '        lblOpenFolder.Visible = True
    '        lblForecasting.Visible = True
    '        lblDNFStatus.Visible = True
    '        lblDNFStatus.Text = "Status : DNF File Generated."

    '        pctForeCastCorrect.Visible = True
    '        pctDNFWrong.Visible = False
    '        lblProbshortDedAmt.Visible = True
    '        lblProbshortDepAmt.Visible = True
    '        lblProbIntPay.Visible = True
    '        pctSummaryCorrect.Visible = True

    '        lblProbshortDedAmt1.Visible = True
    '        lblProbshortDepAmt1.Visible = True
    '        lblProbIntPay1.Visible = True

    '        lblProbshortDedAmt1.Text = strProbshortDedAmt
    '        lblProbIntPay1.Text = strProbIntPay
    '        lblProbshortDepAmt1.Text = strProbshortDepAmt

    '        'Ver 2.02-REQ235 start
    '        If blnCheckLateFiling = True Then
    '            lblLateFilingFeeText.Visible = True
    '            lblLateFilingFee.Visible = True
    '            lblLateFilingFee.Text = Convert.ToString(dblBalanceLateFilingFee)
    '        Else
    '            lblLateFilingFee.Text = ""
    '            lblLateFilingFeeText.Visible = False
    '            lblLateFilingFee.Visible = False
    '        End If
    ''Ver 2.02-REQ235 end

    ''Ver 6.00-REQ660 start
    'If Convert.ToDouble(strProbshortDedAmt) > 0 Or Convert.ToDouble(strProbIntPay) > 0 Or Convert.ToDouble(strProbshortDepAmt) > 0 Or dblBalanceLateFilingFee > 0 Then
    '            btnePayment.Visible = True
    '        Else
    '            btnePayment.Visible = False
    '        End If
    ''Ver 6.00-REQ660 end

    'Else
    '        lblDNFStatus.Text = "Status : DNF Creation Failed."
    '        lblOpenFile.Visible = False
    '        lblOpenFolder.Visible = False
    '        pctForeCastCorrect.Visible = False
    '        pctDNFWrong.Visible = True
    '        lblProbshortDedAmt.Visible = False
    '        lblProbshortDepAmt.Visible = False
    '        lblProbIntPay.Visible = False
    '        pctSummaryCorrect.Visible = False
    '        lblProbshortDedAmt1.Visible = False
    '        lblProbshortDepAmt1.Visible = False
    '        lblProbIntPay1.Visible = False

    '        'Ver 2.02-REQ235 start           
    '        lblLateFilingFee.Text = ""
    '        lblLateFilingFeeText.Visible = False
    '        lblLateFilingFee.Visible = False
    '        'Ver 2.02-REQ235 end

    '    End If

    ''Ver 3.0.7-REQ417 start
    '' btnNext.Enabled = True
    'If blnChkDNFIntegrate = False Then
    '        btnNext.Visible = True
    '        btnNext.Enabled = True
    '    End If
    '    'Ver 3.0.7-REQ417 end 

    '    Application.DoEvents()
    'End Sub
    'live

    'anuETDS_DNF
    'Private Sub frmProcess_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    'Ver 7.03-REQ816 start
    '    If dsMain.Tables("SalaryDetails").Columns.Contains("PANValid") = False Then
    '        dsMain.Tables("SalaryDetails").Columns.Add("PANValid")
    '    End If

    '    'Ver 7.03-REQ816 end
    '    Application.DoEvents()
    '    blnFormClose = True
    '    lblTan.Text = lblTan.Text & " " & TANDeductor
    '    lblFy.Text = lblFy.Text & " " & Mid(dtBH.Rows(0)(16), 1, 4) & "-" & Mid(dtBH.Rows(0)(16), 5, 4) 'Fin Yr
    '    lblQ.Text = lblQ.Text & " " & dtBH.Rows(0)(17) 'Period
    '    lblDedName.Text = lblDedName.Text & " " & dtBH.Rows(0)(18) 'Deductor Name
    '    lblFormNo.Text = lblFormNo.Text & " " & strFormNo

    '    Dim strPath() As String
    '    If blnVerifyChallanSkip = False Then
    '        strPath = strCSIPath.Split("\")
    '        lblCsiFile.Text = lblCsiFile.Text & strPath(strPath.Length - 1)
    '    Else
    '        lblCsiFile.Text = lblCsiFile.Text & " Skipped by user."
    '    End If

    '    lblchlStatus.Text = lblchlStatus.Text & " Total challan : " & dtCD.Rows.Count
    '    lblUnmatchchlStatus.Visible = True
    '    If blnDemoVersion = True Then
    '        lblUnmatchchlStatus.Enabled = False
    '    Else
    '        lblUnmatchchlStatus.Enabled = True
    '    End If

    '    If intCountOfUnMatched > 0 Then
    '        lblUnmatchchlStatus.Text = IIf(blnVerifyChallanSkip = True, "", "Unmatched challan : " & intCountOfUnMatched)
    '    Else
    '        lblUnmatchchlStatus.Visible = False
    '    End If

    '    Application.DoEvents()
    '    pctProcess1.Visible = False
    '    pctProcess2.Visible = False
    '    pctProcess4.Visible = False
    '    pctForeCastCorrect.Visible = False
    '    pctFileWrong.Visible = False
    '    pctDNFWrong.Visible = False
    '    pctFileCorrect.Visible = True

    '    'lblForecasting.Visible = False
    '    lblDNFStatus.Visible = False

    '    'lblSummary.Visible = False
    '    lblProbshortDedAmt.Visible = False
    '    lblProbshortDepAmt.Visible = False
    '    lblProbIntPay.Visible = False
    '    pctSummaryCorrect.Visible = False

    '    lblProbshortDedAmt1.Visible = False
    '    lblProbshortDepAmt1.Visible = False
    '    lblProbIntPay1.Visible = False

    '    'Ver 2.02-REQ235 start                   
    '    lblLateFilingFeeText.Visible = False
    '    lblLateFilingFee.Visible = False
    '    'Ver 2.02-REQ235 end



    '    Application.DoEvents()
    '    If blnVerifyChallanSkip = True Then
    '        pctChallanWrong.Visible = True
    '        pctChallanCorrect.Visible = False
    '    Else
    '        pctChallanCorrect.Visible = True
    '        pctChallanWrong.Visible = False
    '    End If
    '    '********************************************************TdsPac_PanVerification ChallanWithSalary 
    '    Application.DoEvents()
    '    lblPanProcess1.Text = "Status  :"
    '    lblPanProcess1.Visible = False
    '    intCntInvalidPan = 0
    '    If blnVerifyPANSkip = True Then
    '        pctPANWrong.Visible = True
    '        pctPANCorrect.Visible = False
    '        pctProcess3.Visible = False
    '        lblPanStatus.Text = lblPanStatus.Text & " Skipped by user."

    '    Else
    '        pctProcess3.Visible = True

    '        Application.DoEvents()
    '        pctPANWrong.Visible = False
    '        pctPANCorrect.Visible = False

    '        blnPANDelegate = False

    '        lblPanProcess.Visible = True

    '        Dim Delgt As DoPanValidation
    '        'Ver 4.042-QC?? start
    '        'Delgt = AddressOf PANVerificationLatest
    '        Delgt = AddressOf PanVerification
    '        'Ver 4.042-QC?? end
    '        Delgt.Invoke()

    '        Do While blnPANDelegate = False
    '            Application.DoEvents()
    '            lblPanProcess.Text = strPanProcess
    '            lblPanStatus.Text = "Status  : " & strPanStatus
    '        Loop


    '        If blnPANVerification = True Then
    '            pctPANWrong.Visible = True
    '            pctPANCorrect.Visible = False

    '            If blnInternetConnectionFailed = True Then 'Added for Ver 8.0.0.4 for check internet is not active status
    '                lblPanStatus.Text = "Status  :  Connection failed please re-run DNF again!"
    '            Else
    '                lblPanStatus.Text = "Status  :  PAN verification cancelled."
    '            End If

    '            'lblPanStatus.Text = "Status  : PAN verification cancelled."
    '        Else
    '            pctPANWrong.Visible = False
    '            pctPANCorrect.Visible = True

    '            If blnInternetConnectionFailed = True Then 'Added for Ver 8.0.0.4 for check internet is not active status
    '                lblPanStatus.Text = "Status  :  Connection failed please re-run DNF again!"
    '            Else
    '                lblPanStatus.Text = "Status  :  PAN verification completed."
    '            End If

    '            'lblPanStatus.Text = "Status  :  PAN verification completed."
    '        End If

    '        pctProcess3.Visible = False
    '    End If

    '    lblPanProcess1.Visible = False

    '    If blnPANCount = True Then
    '        lblPanProcess.Visible = True
    '    Else
    '        lblPanProcess.Visible = False
    '    End If

    '    If intCntInvalidPan > 0 Then
    '        lblPanProcess1.Visible = True
    '        lblPanProcess1.Text = intCntInvalidPan & " PAN's not found in ITD."
    '        If blnDemoVersion = True Then
    '            lblPanProcess1.Enabled = False
    '        Else
    '            lblPanProcess1.Enabled = True
    '        End If
    '    Else
    '        lblPanProcess1.Text = ""
    '    End If

    '    If intIgnoredPANList > 0 Then
    '        If File.Exists(strIgnoredPANListFilePath) = True Then
    '            lblIgnoredPANList.Visible = True
    '        End If

    '        lblIgnoredPANList.Text = intIgnoredPANList & " PAN Ignored"
    '        If blnDemoVersion = True Then
    '            lblIgnoredPANList.Enabled = False
    '        Else
    '            lblIgnoredPANList.Enabled = True
    '        End If
    '    Else
    '        lblIgnoredPANList.Text = ""
    '        lblIgnoredPANList.Visible = False
    '    End If


    '    '*************************************************************TdsPac_PanVerification ChallanWithSalary 
    '    Application.DoEvents()
    '    If blnInternetConnectionFailed = True Then
    '        pctPANWrong.Visible = True
    '    Else
    '        pctProcess4.Visible = True
    '    End If

    '    lblForecasting.Visible = True
    '    lblDNFStatus.Visible = True
    '    lblDNFStatus.Text = "Status  : Processing.."

    '    Application.DoEvents()
    '    blnDNFCreationDelegate = False
    '    blnFileFailure = False
    '    Dim dlgDnf As DoDNFCreation
    '    'MessageBox.Show("Check DNF forecast")
    '    dlgDnf = AddressOf CreateExcelFile
    '    dlgDnf.BeginInvoke(Nothing, Nothing)
    '    Do While blnDNFCreationDelegate = False
    '        Application.DoEvents()
    '    Loop

    '    'Call CreateExcelFile()
    '    pctProcess4.Visible = False
    '    If blnFileFailure = False Then
    '        lblOpenFile.Visible = True
    '        lblOpenFolder.Visible = True
    '        lblForecasting.Visible = True
    '        lblDNFStatus.Visible = True
    '        lblDNFStatus.Text = "Status : DNF File Generated."

    '        pctForeCastCorrect.Visible = True
    '        pctDNFWrong.Visible = False
    '        lblProbshortDedAmt.Visible = True
    '        lblProbshortDepAmt.Visible = True
    '        lblProbIntPay.Visible = True
    '        pctSummaryCorrect.Visible = True

    '        lblProbshortDedAmt1.Visible = True
    '        lblProbshortDepAmt1.Visible = True
    '        lblProbIntPay1.Visible = True

    '        lblProbshortDedAmt1.Text = strProbshortDedAmt
    '        lblProbIntPay1.Text = strProbIntPay
    '        lblProbshortDepAmt1.Text = strProbshortDepAmt

    '        'Ver 2.02-REQ235 start
    '        If blnCheckLateFiling = True Then
    '            lblLateFilingFeeText.Visible = True
    '            lblLateFilingFee.Visible = True
    '            lblLateFilingFee.Text = Convert.ToString(dblBalanceLateFilingFee)
    '        Else
    '            lblLateFilingFee.Text = ""
    '            lblLateFilingFeeText.Visible = False
    '            lblLateFilingFee.Visible = False
    '        End If
    '        'Ver 2.02-REQ235 end

    '        'Ver 6.00-REQ660 start
    '        If Convert.ToDouble(strProbshortDedAmt) > 0 Or Convert.ToDouble(strProbIntPay) > 0 Or Convert.ToDouble(strProbshortDepAmt) > 0 Or dblBalanceLateFilingFee > 0 Then
    '            btnePayment.Visible = True
    '        Else
    '            btnePayment.Visible = False
    '        End If
    '        'Ver 6.00-REQ660 end

    '    Else
    '        lblDNFStatus.Text = "Status : DNF Creation Failed."
    '        lblOpenFile.Visible = False
    '        lblOpenFolder.Visible = False
    '        pctForeCastCorrect.Visible = False
    '        pctDNFWrong.Visible = True
    '        lblProbshortDedAmt.Visible = False
    '        lblProbshortDepAmt.Visible = False
    '        lblProbIntPay.Visible = False
    '        pctSummaryCorrect.Visible = False
    '        lblProbshortDedAmt1.Visible = False
    '        lblProbshortDepAmt1.Visible = False
    '        lblProbIntPay1.Visible = False

    '        'Ver 2.02-REQ235 start           
    '        lblLateFilingFee.Text = ""
    '        lblLateFilingFeeText.Visible = False
    '        lblLateFilingFee.Visible = False
    '        'Ver 2.02-REQ235 end

    '    End If

    '    'Ver 3.0.7-REQ417 start
    '    ' btnNext.Enabled = True
    '    If blnChkDNFIntegrate = False Then
    '        btnNext.Visible = True
    '        btnNext.Enabled = True
    '    End If
    '    'Ver 3.0.7-REQ417 end 

    '    Application.DoEvents()
    'End Sub
    'anuETDS_DNF
    'DNF
    Private Sub frmProcess_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'Ver 7.03-REQ816 start
        If dsMain.Tables("SalaryDetails").Columns.Contains("PANValid") = False Then
            dsMain.Tables("SalaryDetails").Columns.Add("PANValid")
        End If

        'Ver 7.03-REQ816 end
        Application.DoEvents()
        blnFormClose = True
        lblTan.Text = lblTan.Text & " " & TANDeductor
        lblFy.Text = lblFy.Text & " " & Mid(dtBH.Rows(0)(16), 1, 4) & "-" & Mid(dtBH.Rows(0)(16), 5, 4) 'Fin Yr
        lblQ.Text = lblQ.Text & " " & dtBH.Rows(0)(17) 'Period
        lblDedName.Text = lblDedName.Text & " " & dtBH.Rows(0)(18) 'Deductor Name
        lblFormNo.Text = lblFormNo.Text & " " & strFormNo

        Dim strPath() As String
        If blnVerifyChallanSkip = False Then
            strPath = strCSIPath.Split("\")
            lblCsiFile.Text = lblCsiFile.Text & strPath(strPath.Length - 1)
        Else
            lblCsiFile.Text = lblCsiFile.Text & " Skipped by user."
        End If

        lblchlStatus.Text = lblchlStatus.Text & " Total challan : " & dtCD.Rows.Count
        lblUnmatchchlStatus.Visible = True
        If blnDemoVersion = True Then
            lblUnmatchchlStatus.Enabled = False
        Else
            lblUnmatchchlStatus.Enabled = True
        End If

        If intCountOfUnMatched > 0 Then
            lblUnmatchchlStatus.Text = IIf(blnVerifyChallanSkip = True, "", "Unmatched challan : " & intCountOfUnMatched)
        Else
            lblUnmatchchlStatus.Visible = False
        End If

        Application.DoEvents()
        pctProcess1.Visible = False
        pctProcess2.Visible = False
        pctProcess4.Visible = False
        pctForeCastCorrect.Visible = False
        pctFileWrong.Visible = False
        pctDNFWrong.Visible = False
        pctFileCorrect.Visible = True

        'lblForecasting.Visible = False
        lblDNFStatus.Visible = False

        'lblSummary.Visible = False
        lblProbshortDedAmt.Visible = False
        lblProbshortDepAmt.Visible = False
        lblProbIntPay.Visible = False
        pctSummaryCorrect.Visible = False

        lblProbshortDedAmt1.Visible = False
        lblProbshortDepAmt1.Visible = False
        lblProbIntPay1.Visible = False

        'Ver 2.02-REQ235 start                   
        lblLateFilingFeeText.Visible = False
        lblLateFilingFee.Visible = False
        'Ver 2.02-REQ235 end



        Application.DoEvents()
        If blnVerifyChallanSkip = True Then
            pctChallanWrong.Visible = True
            pctChallanCorrect.Visible = False
        Else
            pctChallanCorrect.Visible = True
            pctChallanWrong.Visible = False
        End If

        Application.DoEvents()
        lblPanProcess1.Text = "Status  :"
        lblPanProcess1.Visible = False
        intCntInvalidPan = 0
        If blnVerifyPANSkip = True Then
            pctPANWrong.Visible = True
            pctPANCorrect.Visible = False
            pctProcess3.Visible = False
            lblPanStatus.Text = lblPanStatus.Text & " Skipped by user."
            'Ver 4.042-QC?? start
            ''Ver 4.041-QC?? start
            Dim filename As String = ""
            If blnDemoVersion = False Then
                'Dim frm As New frmTracesUtility

                'If File.Exists(Application.StartupPath & "\TRACES.exe") = True Then
                'filename = Application.StartupPath & "\" & TANDeductor &
                '            "_" & dtBH.Rows(0)(4) & "_" & dtBH.Rows(0)(17) & "_" & dtBH.Rows(0)(16) & "_" & Today.Day & Today.Month & Today.Year & ".xls"
                'frm.lblpath.Text = filename
                'frm.ShowDialog()
                'File.Copy(Application.StartupPath & "\Default-Notice-Forecaster.sys", filename, True)
                'strDNFExcelFile = filename
                '    Call CallProcessAndWait(Application.StartupPath & "\TRACES.exe", """~" & strFilePath & "~" & strDNFExcelFile & "~~~~~" & TANDeductor & "~DNF~""")
                'Else
                '    MessageBox.Show("TRACES.exe is not found in application folder.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Exit Sub
                'End If
                'ReadExcelPANVerification(strDNFExcelFile)
                ''ReadExcelPANVerification("C:\Gajanan\TFS New\FFCS Projects\DotNet Projects\Window Based\eTdsDNF\DNF\eTdsDNF1516\DNF\bin\Debug\MUML06324D_26Q_Q2_201516_13102015.xls")
            End If
            ''Ver 4.041-QC?? end
            'Ver 4.042-QC?? end
        Else
            pctProcess3.Visible = True

            Application.DoEvents()
            pctPANWrong.Visible = False
            pctPANCorrect.Visible = False

            blnPANDelegate = False

            lblPanProcess.Visible = True

            Dim Delgt As DoPanValidation
            'Ver 4.042-QC?? start
            'Delgt = AddressOf PANVerificationLatest
            create_pan()
            'readcsv()
            'lblPanProcess.Text = strPanProcess
            'lblPanStatus.Text = "Status  : " & strPanStatus



            'Dim filename As String
            'If File.Exists(Application.StartupPath & "\TRACES.exe") = True Then
            '    filename = Application.StartupPath & "\" & TANDeductor &
            '                "_" & dtBH.Rows(0)(4) & "_" & dtBH.Rows(0)(17) & "_" & dtBH.Rows(0)(16) & "_" & Today.Day & Today.Month & Today.Year & ".xls"
            '    frm.lblpath.Text = filename
            '    frm.ShowDialog()
            '    File.Copy(Application.StartupPath & "\Default-Notice-Forecaster.sys", filename, True)
            '    strDNFExcelFile = filename
            '    Call CallProcessAndWait(Application.StartupPath & "\TRACES.exe", """~" & strFilePath & "~" & strDNFExcelFile & "~~~~~" & TANDeductor & "~DNF~""")
            'Else
            '    MessageBox.Show("TRACES.exe is not found in application folder.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End If
            'ReadExcelPANVerification(strDNFExcelFile)
            ''ReadExcelPANVerification("C:\Gajanan\TFS New\FFCS Projects\DotNet Projects\Window Based\eTdsDNF\DNF\eTdsDNF1516\DNF\bin\Debug\MUML06324D_26Q_Q2_201516_13102015.xls")

            'Delgt = AddressOf PanVerification
            ''Ver 4.042-QC?? end
            'Delgt.Invoke()

            'Do While blnPANDelegate = False
            '    Application.DoEvents()
            '    lblPanProcess.Text = strPanProcess
            '    lblPanStatus.Text = "Status  : " & strPanStatus

            'Loop


            If blnPANVerification = False Then

                pctPANWrong.Visible = True
                pctPANCorrect.Visible = False

                If blnInternetConnectionFailed = True Then 'Added for Ver 8.0.0.4 for check internet is not active status
                    lblPanStatus.Text = "Status  :  Connection failed please re-run DNF again!"
                Else
                    lblPanStatus.Text = "Status  :  PAN verification cancelled."
                End If

                lblPanStatus.Text = "Status  : PAN verification cancelled."
            Else
                'pctPANWrong.Visible = False
                'pctPANCorrect.Visible = True

                'If blnInternetConnectionFailed = True Then 'Added for Ver 8.0.0.4 for check internet is not active status
                '    lblPanStatus.Text = "Status  :  Connection failed please re-run DNF again!"
                'Else
                If actCntInvalidPan > 0 Then
                    lblPanProcess1.Text = ""
                    lblPanProcess.Visible = True
                    lblPanProcess.Text = actCntInvalidPan & " PAN's Verification Completed."
                End If
                If intCntInvalidPan > 0 Then
                    lblPanProcess1.Visible = True
                    lblPanProcess.Text = ""
                    lblPanProcess.Visible = False
                    lblPanProcess1.Text = intCntInvalidPan & " PAN's not found in Traces."
                    If blnDemoVersion = True Then
                        lblPanProcess1.Enabled = False
                    Else
                        lblPanProcess1.Enabled = True
                    End If
                Else
                    lblPanProcess1.Text = ""
                End If
                lblPanStatus.Text = "Status  :  PAN verification completed."
                'End If

                'lblPanStatus.Text = "Status  :  PAN verification completed."
            End If

            pctProcess3.Visible = False
        End If

        'lblPanProcess1.Visible = False

        'If blnPANCount = True Then
        '    lblPanProcess.Visible = True
        'Else
        '    lblPanProcess.Visible = False
        'End If

        If intCntInvalidPan > 0 Then
            lblPanProcess1.Visible = True
            lblPanProcess.Visible = False
            lblPanProcess1.Text = intCntInvalidPan & " PAN's not found in Traces."
            If blnDemoVersion = True Then
                lblPanProcess1.Enabled = False
            Else
                lblPanProcess1.Enabled = True
            End If
        Else
            lblPanProcess1.Text = ""
            lblPanProcess1.Visible = False
        End If

        'If intIgnoredPANList > 0 Then
        '    If File.Exists(strIgnoredPANListFilePath) = True Then
        '        lblIgnoredPANList.Visible = True
        '    End If

        '    lblIgnoredPANList.Text = intIgnoredPANList & " PAN Ignored"
        '    If blnDemoVersion = True Then
        '        lblIgnoredPANList.Enabled = False
        '    Else
        '        lblIgnoredPANList.Enabled = True
        '    End If
        'Else
        '    lblIgnoredPANList.Text = ""
        '    lblIgnoredPANList.Visible = False
        'End If

        Application.DoEvents()
        If blnInternetConnectionFailed = True Then
            pctPANWrong.Visible = True
        Else
            pctProcess4.Visible = True
        End If

        lblForecasting.Visible = True
        lblDNFStatus.Visible = True
        lblDNFStatus.Text = "Status  : Processing.."

        Application.DoEvents()
        blnDNFCreationDelegate = False
        blnFileFailure = False
        Dim dlgDnf As DoDNFCreation
        'MessageBox.Show("Check DNF forecast")
        dlgDnf = AddressOf CreateExcelFile
        dlgDnf.BeginInvoke(Nothing, Nothing)
        Do While blnDNFCreationDelegate = False
            Application.DoEvents()
        Loop

        'Call CreateExcelFile()
        pctProcess4.Visible = False
        If blnFileFailure = False Then

            lblOpenFile.Visible = True
            lblOpenFolder.Visible = True
            lblForecasting.Visible = True
            lblDNFStatus.Visible = True
            lblDNFStatus.Text = "Status : DNF File Generated."

            pctForeCastCorrect.Visible = True
            pctDNFWrong.Visible = False
            lblProbshortDedAmt.Visible = True
            lblProbshortDepAmt.Visible = True
            lblProbIntPay.Visible = True
            pctSummaryCorrect.Visible = True

            lblProbshortDedAmt1.Visible = True
            lblProbshortDepAmt1.Visible = True
            lblProbIntPay1.Visible = True

            lblProbshortDedAmt1.Text = strProbshortDedAmt
            lblProbIntPay1.Text = strProbIntPay
            lblProbshortDepAmt1.Text = strProbshortDepAmt

            'Ver 2.02-REQ235 start
            If blnCheckLateFiling = True Then
                lblLateFilingFeeText.Visible = True
                lblLateFilingFee.Visible = True
                lblLateFilingFee.Text = Convert.ToString(dblBalanceLateFilingFee)
            Else
                lblLateFilingFee.Text = ""
                lblLateFilingFeeText.Visible = False
                lblLateFilingFee.Visible = False
            End If
            'Ver 2.02-REQ235 end

            'Ver 6.00-REQ660 start
            If Convert.ToDouble(strProbshortDedAmt) > 0 Or Convert.ToDouble(strProbIntPay) > 0 Or Convert.ToDouble(strProbshortDepAmt) > 0 Or dblBalanceLateFilingFee > 0 Then
                btnePayment.Visible = True
            Else
                btnePayment.Visible = False
            End If
            'Ver 6.00-REQ660 end

        Else
            lblDNFStatus.Text = "Status : DNF Creation Failed."
            lblOpenFile.Visible = False
            lblOpenFolder.Visible = False
            pctForeCastCorrect.Visible = False
            pctDNFWrong.Visible = True
            lblProbshortDedAmt.Visible = False
            lblProbshortDepAmt.Visible = False
            lblProbIntPay.Visible = False
            pctSummaryCorrect.Visible = False
            lblProbshortDedAmt1.Visible = False
            lblProbshortDepAmt1.Visible = False
            lblProbIntPay1.Visible = False

            'Ver 2.02-REQ235 start           
            lblLateFilingFee.Text = ""
            lblLateFilingFeeText.Visible = False
            lblLateFilingFee.Visible = False
            'Ver 2.02-REQ235 end

        End If

        'Ver 3.0.7-REQ417 start
        ' btnNext.Enabled = True
        If blnChkDNFIntegrate = False Then
            btnNext.Visible = True
            btnNext.Enabled = True
        End If
        'Ver 3.0.7-REQ417 end 

        Application.DoEvents()
    End Sub
    'DNF
    Private Sub create_pan()
        Try
            Dim fileName As String = Application.StartupPath & "\traces\pan.txt"

            ' Check if the file already exists
            If File.Exists(fileName) Then
                File.Delete(fileName)
            End If

            Dim filerpt As String = Application.StartupPath & "\traces\report.csv"
            If File.Exists(filerpt) Then
                File.Delete(filerpt)
            End If

            Dim fs As FileStream = File.Create(fileName)
            fs.Close()

            Dim dtTempView As New DataView(dsMain.Tables("DeducteeDetails"))
            Dim dtTempViewSalaryDetails As New DataView(dsMain.Tables("SalaryDetails"))

            intCountOfValidPAN = 0
            dtTempView.Sort = "PAN"
            dtTempView.RowFilter = "IsPANValidationReq = True"
            Application.DoEvents()
            dtTempPAN = dtTempView.ToTable("PANValid", True, "PAN") ' For selecting Distinct PAN into data table
            Dim dt As DataTable = dtTempView.ToTable("PANValid", True, "PAN")

            ' Description: To Megre Distinct PAN into DD from SD for Validation <start>
            dtTempViewSalaryDetails.Sort = "PAN"
            dtTempViewSalaryDetails.RowFilter = " PAN <> 'PANNOTAVBL' " 'Ver 7.03-REQ816 start
            Dim dtsal As DataTable = dtTempViewSalaryDetails.ToTable("PANValid", True, "PAN")
            If dtsal.Rows.Count > 0 Then
                dt.Merge(dtsal)
            End If



            Using writer As New StreamWriter(fileName)
                writer.Write("Pan" & Environment.NewLine)

                If dt.Rows.Count > 0 Then
                    'dtTempPAN.Merge(dt)
                    'dtTempPAN = dtTempPAN.DefaultView.ToTable(True, "PAN")

                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim s As String = dt.Rows(i).Item(0).ToString()
                        writer.Write(s & Environment.NewLine)
                    Next
                End If
            End Using
            If dt.Rows.Count > 0 Then
                'Dim s As String = dt.Rows.Item[0]
                'writer.Write(s & Environment.NewLine)
                Dim frm As New frmTraces
                frm.lbltan.Text = TANDeductor
                frm.lblPanProcess.Text = dt.Rows.Count
                frm.lblPanProcess.Visible = False
                frm.ShowDialog()
            Else
                MsgBox("There is no PAN available for Validation")
            End If
        Catch ex As Exception
            'Throw ex
        End Try
    End Sub
    Private Sub readcsv()
        ' Define the CSV file path
        Dim fileName As String = Application.StartupPath & "\traces\report.csv"

        ' Read the contents of the CSV file
        Dim contents As String = File.ReadAllText(fileName)

        ' Remove double quotes from the contents and overwrite the file
        File.WriteAllText(fileName, contents.Replace("""", ""))

        lblPanProcess.Text = strPanProcess
        lblPanStatus.Text = "Status  : " & strPanStatus

        Dim csvdt As DataTable
        Dim mfile As File
        If mfile.Exists(Application.StartupPath & "\traces\report.csv") = True Then
            Dim strPath As String = IO.Path.GetDirectoryName(Application.StartupPath & "\traces\report.csv")
            Dim strFile As String = IO.Path.GetFileName(Application.StartupPath & "\traces\report.csv")
            csvdt = getcsvtable(strPath)
        Else
            MsgBox("Please check ...", vbCritical, "")
        End If
        csvdt.Columns.Add("ActiveStatus")

        'csvdt.[Select]("Status = 'Active'").ToList().ForEach(Sub(ByVal name As DataRow)
        '                                                         name.SetField(Of String)(1, "Apple")
        '                                                         Console.WriteLine(name.Field(Of String)("Client"))
        '                                                     End Sub)

        csvdt.Select("Status = 'Active'").ToList().ForEach(Sub(row As DataRow)
                                                               row.SetField(Of String)("ActiveStatus", "Apple")
                                                               Console.WriteLine(row.Field(Of String)("Client"))
                                                           End Sub)





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


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowData.Click
        frmShowData.Show()
    End Sub
    Private Sub lblPanProcess1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblPanProcess1.LinkClicked
        'invalidpan()
        Dim ts As StreamWriter
        Dim dtTmpInvalidPAN As DataTable
        Dim mStr As String
        Dim dtTempView As New DataView(dtDD)
        'Ver 5.05-REQ641 start
        Dim dtTempviewSalary As New DataView(dtSD) 'Jitendrax1
        'Ver 5.05-REQ641 end
        Dim ExlAppln As New Object
        Dim ExlBook As New Object

        Try

            ExlAppln = CreateObject("Excel.Application")

            ts = New StreamWriter(Application.StartupPath & "\InvalidPans.xls")

            mStr = ""
            mStr = "PAN" & Chr(9) & "Name"


            dtTempView.RowFilter = "PANStatus ='I' and DeducteeStatus='Z'"
            dtTmpInvalidPAN = dtTempView.ToTable("InValidPANValid", True, "PAN", "Name")
            mStr = "Invalid PAN in Deductee"
            mStr = mStr & vbCrLf & "PAN" & Chr(9) & "Name"
            For k As Integer = 0 To dtTmpInvalidPAN.Rows.Count - 1
                mStr = mStr & vbCrLf & dtTmpInvalidPAN.Rows(k)("PAN").ToString & Chr(9) & dtTmpInvalidPAN.Rows(k)("Name").ToString
            Next

            '==>Jitendra
            'Ver 5.05-REQ641 start
            dtTempviewSalary.RowFilter = "PANValid='N'"
            dtTmpInvalidPAN = dtTempviewSalary.ToTable("InValidPANValid", True, "PAN", "Name")
            mStr = mStr & vbCrLf
            mStr = mStr & vbCrLf
            mStr = mStr & vbCrLf & "Invalid PAN in Salary"
            mStr = mStr & vbCrLf & "PAN" & Chr(9) & "Name"
            For k As Integer = 0 To dtTmpInvalidPAN.Rows.Count - 1
                mStr = mStr & vbCrLf & dtTmpInvalidPAN.Rows(k)("PAN").ToString & Chr(9) & dtTmpInvalidPAN.Rows(k)("Name").ToString
            Next
            'Ver 5.05-REQ641 end 
            ts.WriteLine(mStr)
            ts.Flush()
            ts.Dispose()
            dtTmpInvalidPAN.Dispose()
            dtTempView.Dispose()
            'Ver 5.05-REQ641 start 
            dtTempviewSalary.Dispose()
            'Ver 5.05-REQ641 end 
            mStr = ""
        Catch ex As Exception

        End Try
        Process.Start(Application.StartupPath & "\InvalidPans.xls")
    End Sub
    Public Function invalidpan()
        'Dim ts As StreamWriter
        'Dim dtTmpInvalidPAN As DataTable
        'Dim mStr As String
        'Dim dtTempView As New DataView(dtDD)
        ''Ver 5.05-REQ641 start
        'Dim dtTempviewSalary As New DataView(dtSD) 'Jitendrax1
        ''Ver 5.05-REQ641 end
        'Dim ExlAppln As New Object
        'Dim ExlBook As New Object

        'Try

        '    ExlAppln = CreateObject("Excel.Application")

        '    ts = New StreamWriter(Application.StartupPath & "\InvalidPans.xls")

        '    mStr = ""
        '    mStr = "PAN" & Chr(9) & "Name"


        '    dtTempView.RowFilter = "PANStatus ='I' and DeducteeStatus='Z'"
        '    dtTmpInvalidPAN = dtTempView.ToTable("InValidPANValid", True, "PAN", "Name")
        '    mStr = "Invalid PAN in Deductee"
        '    mStr = mStr & vbCrLf & "PAN" & Chr(9) & "Name"
        '    For k As Integer = 0 To dtTmpInvalidPAN.Rows.Count - 1
        '        mStr = mStr & vbCrLf & dtTmpInvalidPAN.Rows(k)("PAN").ToString & Chr(9) & dtTmpInvalidPAN.Rows(k)("Name").ToString
        '    Next

        '    '==>Jitendra
        '    'Ver 5.05-REQ641 start
        '    dtTempviewSalary.RowFilter = "PANValid='N'"
        '    dtTmpInvalidPAN = dtTempviewSalary.ToTable("InValidPANValid", True, "PAN", "Name")
        '    mStr = mStr & vbCrLf
        '    mStr = mStr & vbCrLf
        '    mStr = mStr & vbCrLf & "Invalid PAN in Salary"
        '    mStr = mStr & vbCrLf & "PAN" & Chr(9) & "Name"
        '    For k As Integer = 0 To dtTmpInvalidPAN.Rows.Count - 1
        '        mStr = mStr & vbCrLf & dtTmpInvalidPAN.Rows(k)("PAN").ToString & Chr(9) & dtTmpInvalidPAN.Rows(k)("Name").ToString
        '    Next
        '    'Ver 5.05-REQ641 end 
        '    ts.WriteLine(mStr)
        '    ts.Flush()
        '    ts.Dispose()
        '    dtTmpInvalidPAN.Dispose()
        '    dtTempView.Dispose()
        '    'Ver 5.05-REQ641 start 
        '    dtTempviewSalary.Dispose()
        '    'Ver 5.05-REQ641 end 
        '    mStr = ""
        'Catch ex As Exception

        'End Try
        'Process.Start(Application.StartupPath & "\InvalidPans.xls")
        'Dim strValidPANListFileTracesPath As String = Application.StartupPath & "\traces\InvalidPAN.csv"

        'Process.Start(Application.StartupPath & "\InvalidPans.xls")
        'Process.Start(Application.StartupPath & strValidPANListFileTracesPath)
        Process.Start(strValidPANListFileTracesPath)
    End Function
    Private Sub lblUnmatchchlStatus_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblUnmatchchlStatus.LinkClicked
        Dim ts As StreamWriter
        Dim dtTmpInvalidCSI As DataTable
        Dim mStr As String
        Dim dtTempView As New DataView(dtCD)

        Dim ExlAppln As New Object
        Dim ExlBook As New Object
        Try
            ExlAppln = CreateObject("Excel.Application")
            ts = New StreamWriter(Application.StartupPath & "\InvalidCSI.xls")
            mStr = ""
            mStr = "ChlnNo" & Chr(9) & "Total"
            dtTempView.RowFilter = "IsChallanMatched=False"
            dtTmpInvalidCSI = dtTempView.ToTable("InValidPANValid", True, "ChlnNo", "Total")

            For k As Integer = 0 To dtTmpInvalidCSI.Rows.Count - 1
                mStr = mStr & vbCrLf & dtTmpInvalidCSI.Rows(k)("ChlnNo").ToString & Chr(9) & dtTmpInvalidCSI.Rows(k)("Total").ToString
            Next
            ts.WriteLine(mStr)
            ts.Flush()
            ts.Dispose()
            dtTmpInvalidCSI.Dispose()
            dtTempView.Dispose()
            mStr = ""
        Catch ex As Exception
        End Try
        Process.Start(Application.StartupPath & "\InvalidCSI.xls")
    End Sub

    Private Sub lblOpenFile_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblOpenFile.LinkClicked

        If blnDemoVersion = True Then
            Process.Start(Application.StartupPath & "\Sample-Default-Notice-Forecaster.xls")
        Else
            Process.Start(strDNFExcelFile)
            'Process.Start(Application.StartupPath & strDNFExcelFile)
        End If
    End Sub

    Private Sub lblOpenFolder_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblOpenFolder.LinkClicked
        Process.Start(Application.StartupPath)
    End Sub

    Private Sub lblIgnoredPANList_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblIgnoredPANList.LinkClicked
        'Ver 3.04-???? Start
        MessageBox.Show("Some PAN’s are ignored as there is no response or delay in response from incometax website.You may wish to verify this file again." & vbCrLf & vbCrLf _
                       & "Verifying the same file again will consider only Ignored PAN and Invalid PAN list for PAN Validation process." &
                       vbCrLf & vbCrLf & "PAN already verified will be skipped.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Ver 3.04-???? end
        Process.Start(strIgnoredPANListFilePath)
    End Sub

    Private Sub lblProbshortDepAmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblProbshortDepAmt.Click

    End Sub
    'Ver 6.00-REQ660 start
    Private Sub btnePayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnePayment.Click
        Dim strePaymentApplication As String = Application.StartupPath + "\\ePayment.exe"
        Dim strMessageboxHeading As String = "eTdsDNF"

        If File.Exists(strePaymentApplication) = True Then

            Dim strAcDedType As String = "", strAcTANNo As String, strAcFullName As String, strAcFLATNO As String, strAcBUILDING As String, strAcROAD As String, strAcAREA As String, strAcCITY As String, strAcSTATE As String, strAcPIN As String, strEmail As String, strAcBank As String, strAcAssYear As String, strAcTds As String = "", strMobileNo As String

            strAcTANNo = dtBH.Rows(0)("TAN").ToString()
            strAcFullName = dtBH.Rows(0)("Name").ToString()
            strAcFLATNO = dtBH.Rows(0)("Add1").ToString()
            strAcBUILDING = dtBH.Rows(0)("Add2").ToString()
            strAcROAD = dtBH.Rows(0)("Add3").ToString()
            strAcAREA = dtBH.Rows(0)("Add4").ToString()
            strAcCITY = dtBH.Rows(0)("Add5").ToString()
            strAcSTATE = RetriveState(dtBH.Rows(0)("stCode").ToString())
            strAcPIN = dtBH.Rows(0)("Pin").ToString()
            strEmail = dtBH.Rows(0)("Email").ToString
            strMobileNo = dtBH.Rows(0)("Mobile No").ToString()
            strAcAssYear = dtBH.Rows(0)("Ass. Yr").ToString().Substring(0, 4) & "-" & dtBH.Rows(0)("Ass. Yr").ToString().Substring(4, 2)
            strAcDedType = IIf(dtBH.Rows(0)("Status").ToString() = "K", "0020", "0021")

            Dim ps As Process = New Process

            ps.StartInfo.FileName = strePaymentApplication
            ps.StartInfo.Arguments = strAcDedType + "~" + strAcTds + "~" + strAcAssYear + "~" + strAcTANNo + "~" + strAcFullName + "~" + strAcFLATNO + "~" + strAcBUILDING + "~" + strAcROAD + "~" + strAcAREA + "~" + strAcCITY + "~" + strAcSTATE + "~" + strAcPIN + "~" + strAcBank + "~" + strEmail + "~" + strMobileNo
            ps.Start()
            ps.WaitForExit()
        Else
            MessageBox.Show("ePayment application not found", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub lblUnmatchchlStatus_Invalidated(sender As Object, e As InvalidateEventArgs) Handles lblUnmatchchlStatus.Invalidated

    End Sub

    Private Sub pctProcess1_Click(sender As Object, e As EventArgs) Handles pctProcess1.Click

    End Sub
    'Ver 6.00-REQ660 end
End Class