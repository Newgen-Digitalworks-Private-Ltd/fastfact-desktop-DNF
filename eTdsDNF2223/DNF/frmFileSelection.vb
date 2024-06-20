Imports System.Data
Imports System.Net
Imports System.Text
Imports System.IO
Public Class frmFileSelection
    Private oclsMemory As New clsMemory
    Private Sub frmFileSelection_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If blnFormClose = True Then
            End
        End If
    End Sub
    Private Sub frmFileSelection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.CenterToScreen()
            'CreateTracesFolder()
            'Ver 3.0.7-REQ417 start 
            If blnChkDNFIntegrate = True Then
                txtFileSelection.Text = strFilePath
            End If
            'Ver 3.0.7-REQ417 end  

            'Ver 4.042-QC?? start
            ''Ver 4.041-QC?? start
            'chkVerifyPAN.Visible = False
            'chkListPANName.Left = 26
            'chkListPANName.Top = 87
            ''blnVerifyPANSkip = True
            ''Ver 4.041-QC?? end
            'Ver 4.042-QC?? end

            If File.Exists(Application.StartupPath & "\InternetConnection.txt") = False Then
                rbDownload.Enabled = False
                chkVerifyPAN.Enabled = False
                rbCSIfile.Checked = True
            End If

            lblSelectText.Visible = True

            lblTan.Visible = False
            lblFy.Visible = False
            lblQ.Visible = False
            lblDedName.Visible = False
            lblFormNo.Visible = False

            lblSelectCsiPath.Visible = False
            txtCSIFileSelection.Visible = False
            btnCsiDown.Visible = False

            'Create Datatable Which will be use to store text file Details
            'Ver 2.01-REQ233 Start
            'If blnNextFile = False Then
            '    BatchHeader()
            '    ChallanDetails()
            '    DeducteeDetails()
            '    SalaryDetails()
            'End If
            'Ver 2.01-REQ233 End

            'Ver 1.01-E585 start
            If blnDemoVersion = False Then
                If blnIsProductRegistered = False Then
                    'Ver 3.01-REQ297 start
                    'pnlRegistration.Visible = True
                    frmRegister.Show()
                    Me.Close()
                    'Ver 3.01-REQ297 end
                    lblRegistration.Visible = True
                Else
                    pnlRegistration.Visible = False
                    lblRegistration.Visible = False
                End If
            End If
            'Ver 1.01-E585 End
            'Ver 3.0.7-REQ417 start 
            If blnChkDNFIntegrate = True Then
                chkVerifyPAN.Checked = blnchkVerifyPAN
                chkLateFiling.Checked = blnchkLateFiling

                'Ver 4.04 start 
                'chkVerifyChallan.Checked = True
                If strCSIValidPath = "" Then
                    chkVerifyChallan.Checked = False
                Else
                    chkVerifyChallan.Checked = True
                End If
                'Ver 4.04 End 


                Call btnVerifyOption_Click(sender, e)
            End If
            'Ver 3.0.7-REQ417 end 

            If blnChkDNFIntegrate = True Then
                Me.Size = New System.Drawing.Size(798, 512)
            End If

            'Fill ShortDeductionIntRateValue
            Call FillShortDeduction()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")
        End Try
    End Sub
    Private Sub CreateTracesFolder()
        Try

            Dim strSourcePath As String = Application.StartupPath & "\Traces"
            'Dim strSourcePath As String = "C:\eTdsWizard\traces"

            ' If directory does not exist, create it
            If Directory.Exists(strSourcePath) Then
                If Not File.Exists(Path.Combine(strSourcePath, "traces.md")) Then
                    File.Copy(Application.StartupPath & "\traces.md", Path.Combine(strSourcePath, "traces.md"))
                End If

                If Not File.Exists(Path.Combine(strSourcePath, "README.md")) Then
                    File.Copy(Application.StartupPath & "\README.md", Path.Combine(strSourcePath, "README.md"))
                End If

                If Not File.Exists(Path.Combine(strSourcePath, "Traces.jar")) Then
                    File.Copy(Application.StartupPath & "\Traces.jar", Path.Combine(strSourcePath, "Traces.jar"))
                End If
            Else
                Directory.CreateDirectory(strSourcePath)
                ' Copy the files inside this root
                File.Copy(Application.StartupPath & "\traces.md", Path.Combine(strSourcePath, "traces.md"))
                File.Copy(Application.StartupPath & "\README.md", Path.Combine(strSourcePath, "README.md"))
                File.Copy(Application.StartupPath & "\Traces.jar", Path.Combine(strSourcePath, "Traces.jar"))
            End If
        Catch ex As Exception
            ' Handle exception
        End Try
    End Sub

    Private Sub FillShortDeduction()
        Dim filepath As String = Application.StartupPath + "\ShortDeduction.txt"
        Dim strShortDeductionVal As String
        Dim objReader As System.IO.StreamReader

        If (File.Exists(filepath)) Then
            objReader = New System.IO.StreamReader(filepath)
            strShortDeductionVal = Convert.ToString(objReader.ReadLine())

            If strShortDeductionVal <> "" Then
                txtShortDeductVal.Text = Convert.ToInt32(strShortDeductionVal)
            End If
            objReader.Close()
        End If
    End Sub
    Private Sub OnTimedEvent(source As Object, e As System.Timers.ElapsedEventArgs)
        oclsMemory.FlushMem()
    End Sub
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Dim otimer As New System.Timers.Timer
        Try
            otimer.Interval = 3000
            AddHandler otimer.Elapsed, AddressOf OnTimedEvent
            otimer.AutoReset = True
            otimer.Enabled = True
            otimer.Start()
            lblSelectText.Visible = False
            DisposeAll()

            If blnChkDNFIntegrate = False Then
                Dim cdlOpen As New OpenFileDialog
                cdlOpen.Filter = "*.txt |*.txt"
                cdlOpen.ShowDialog()
                txtFileSelection.Text = cdlOpen.FileName
            End If

            strFilePath = txtFileSelection.Text

            lblTan.Text = "TAN : "
            lblFy.Text = "FY : "
            lblQ.Text = "Quarter : "
            lblDedName.Text = "Name : "
            lblFormNo.Text = "FormNo : "

            If strFilePath = "" Then
                lblTan.Visible = False
                lblFy.Visible = False
                lblQ.Visible = False
                lblDedName.Visible = False
                lblFormNo.Visible = False
                pnlVerifyOptions.Visible = False
                pnlCsiFileSelection.Visible = False
                chkVerifyChallan.Checked = False
                chkVerifyPAN.Checked = False
                If blnDemoVersion = False Then
                    If blnIsProductRegistered = False Then
                        pnlRegistration.Visible = True
                        lblRegistration.Visible = True
                    Else
                        pnlRegistration.Visible = False
                        lblRegistration.Visible = False
                    End If
                End If
                Exit Sub
            End If

            blnFileClose = False
            Call Read()

            If blnFileClose = True Then
                txtFileSelection.Text = ""
                lblTan.Visible = False
                lblFy.Visible = False
                lblQ.Visible = False
                lblDedName.Visible = False
                lblFormNo.Visible = False
                pnlVerifyOptions.Visible = False
                pnlCsiFileSelection.Visible = False
                chkVerifyChallan.Checked = False
                chkVerifyPAN.Checked = False
                'Ver 1.01-E585 start
                If blnDemoVersion = False Then
                    If blnIsProductRegistered = False Then
                        pnlRegistration.Visible = True
                        lblRegistration.Visible = True
                    Else
                        pnlRegistration.Visible = False
                        lblRegistration.Visible = False
                    End If
                End If
                'Ver 1.01-E585 End
                Exit Sub
            End If

            If blnFileClose = False Then
                lblTan.Text = lblTan.Text & " " & dtBH.Rows(0)(12)
                lblFy.Text = lblFy.Text & " " & Mid(dtBH.Rows(0)(16), 1, 4) & "-" & Mid(dtBH.Rows(0)(16), 5, 4) 'Fin Yr

                lblQ.Text = lblQ.Text & " " & dtBH.Rows(0)(17) 'Period
                'Ver 2.02-REQ235 start
                strFileQuarter = dtBH.Rows(0)(17)
                'Ver 2.02-REQ235 end
                lblDedName.Text = lblDedName.Text & " " & dtBH.Rows(0)(18) 'Deductor Name
                lblFormNo.Text = lblFormNo.Text & " " & strFormNo
                lblFormNo.Visible = True
                lblTan.Visible = True
                lblFy.Visible = True
                lblQ.Visible = True
                lblDedName.Visible = True
            End If
            btnSelect.Enabled = False
            txtFileSelection.Enabled = False

            'Ver 1.01-E585 start
            Dim strQuarter As String = dtBH.Rows(0)(17)
            'Ver 3.01-REQ297 start
            'If strQuarter <> "Q1" And strFinYear = "201314" And blnDemoVersion = False And blnIsProductRegistered = False Then
            If blnDemoVersion = False And blnIsProductRegistered = False Then
                'Ver 3.01-REQ297 end
                pnlRegistration.Visible = True
                lblRegistration.Visible = True
                pnlVerifyOptions.Visible = False
                pnlCsiFileSelection.Visible = False
                chkVerifyChallan.Checked = False
                chkVerifyPAN.Checked = False
            Else
                pnlRegistration.Visible = False
            End If
            'Ver 1.01-E585 End


        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")
        Finally
            otimer.Dispose() : otimer.Stop()
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        End
    End Sub

    Private Sub btnVerifyOption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerifyOption.Click
        'Ver 3.05-???? start
        'Ver 3.0.7-REQ417 start
        'blnChkListPANName = chkListPANName.Checked
        ''blnChkVerifyLowerRate = chkLowerRate.Checked
        'Ver 3.05-???? end
        ' blnChkListPANName = chkListPANName.Checked
        If blnChkDNFIntegrate = False Then
            blnChkListPANName = chkListPANName.Checked
        End If
        'Ver 3.0.7-REQ417 end  
        'Ver 6.00-REQ661 start
        blnChkVerifyLowerRate = chkLowerRate.Checked
        'Ver 6.00-REQ661 end
        'Ver 2.02-REQ235 start
        If chkLateFiling.Checked = True Then
            If dtpSubmittedDate.Text = "" Or IsDate(dtpSubmittedDate.Text) = False Then
                MessageBox.Show("Please select proper return submitted on date.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                blnCheckLateFiling = True
                'Ver 3.0.7-REQ417 start 
                'strSubmittedDate = dtpSubmittedDate.Text
                If blnChkDNFIntegrate = True Then
                    dtpSubmittedDate.Text = strLateFilingDate
                    strSubmittedDate = dtpSubmittedDate.Text
                Else
                    strSubmittedDate = dtpSubmittedDate.Text
                End If
                'Ver 3.0.7-REQ417 end  
            End If
        Else
            blnCheckLateFiling = False
        End If
        'Ver 2.02-REQ235 end
        'Ver 3.0.7-REQ417 start 
        If blnChkDNFIntegrate = True Then
            strCSIPath = strCSIValidPath
            Call readCSI()
        End If
        'Ver 3.0.7-REQ417 end  

        If chkVerifyChallan.Checked = True Then
            pnlCsiFileSelection.Visible = True
            grpVerifyOptions.Visible = False
        Else
            blnFormClose = False
            frmProcess.Show()
            Me.Close()
        End If
    End Sub

    Private Sub btnCSISelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCSISelect.Click
        Try
            Dim cdlOpen As New OpenFileDialog
            cdlOpen.Filter = "*.csi |*.csi"
            cdlOpen.ShowDialog()
            txtCSISelection.Text = cdlOpen.FileName
            strCSIPath = txtCSISelection.Text
            If strCSIPath = "" Then Exit Sub

            Call readCSI()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")
        End Try
    End Sub

    Private Sub btnCsiDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCsiDown.Click
        Try
            Dim cdlOpen As New OpenFileDialog
            cdlOpen.Filter = "*.csi |*.csi"
            cdlOpen.ShowDialog()
            txtCSIFileSelection.Text = cdlOpen.FileName
            strCSIPath = txtCSIFileSelection.Text
            Call readCSI()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")
        End Try
    End Sub

    Private Sub rbCSIfile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCSIfile.CheckedChanged

        If rbCSIfile.Checked = True Then
            txtCSISelection.Enabled = True
            btnCSISelect.Enabled = True
        Else
            txtCSISelection.Enabled = False
            btnCSISelect.Enabled = False
        End If
        lblSelectCsiPath.Visible = False
        txtCSIFileSelection.Visible = False
        btnCsiDown.Visible = False

    End Sub

    Private Sub rbDownload_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDownload.CheckedChanged

        If rbDownload.Checked = True Then
            btnDownload.Enabled = True
            txtCSIFileSelection.Enabled = True
            btnCsiDown.Enabled = True
        Else

            btnDownload.Enabled = False
            txtCSIFileSelection.Enabled = False
            btnCsiDown.Enabled = False

        End If
    End Sub

    Private Sub Read()
        Try
            Dim blnValidFile As Boolean
            If strFilePath = "" Then    'FilePath Can not be Blank
                MessageBox.Show("Select the file name and try again.", "File Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtFileSelection.Focus()
                blnFileClose = True
                Exit Sub
            End If

            If Dir(strFilePath) = "" Then  'FilePath Can not be Directory
                MessageBox.Show("Please select the file and try again.", "File Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                strFilePath = ""
                blnFileClose = True
                Exit Sub
            End If

            'Before proceeding further make sure that textfile is matched with corresponding FVU File
            blnValidFile = ValidateFileWithJAR()

            If blnValidFile = True Then  '''' If File Valid then Proceed
                GetData()     'Read Text file Data and store it in respective Datatable

                'Put all datatable in DataSet
                dsMain.Tables.Add(dtBH)
                dsMain.Tables.Add(dtCD)
                dsMain.Tables.Add(dtDD)
                dsMain.Tables.Add(dtSD)
                'Ver 7.03-REQ816 start
                dsMainDNFValidation.Merge(dtSD)
                'Ver 7.03-REQ816 end 
                'dsMain.Tables("DeducteeDetails").WriteXml("C:\DefaultForecaster.xml")

                pnlVerifyOptions.Visible = True
                pnlCsiFileSelection.Visible = False

                'Ver 2.02-REQ235 start
                If IsDate(strSubmittedDate) = True Then
                    dtpSubmittedDate.Text = strSubmittedDate
                End If
                'Ver 2.02-REQ235 end


            Else  '''' If File InValid then Show Message
                MessageBox.Show("Invalid File. " & vbCrLf & " ->Make sure .FVU file exists in the same folder." &
                            vbCrLf & "->Make sure .FVU file is generated from the selected text file.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                strFilePath = ""
                blnFileClose = True
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub readCSI()
        Try

            Dim ChallanSrno As String
            Dim BsrCode As String
            Dim ChallanDt As String
            Dim Amount As Double
            Dim IsValidChallan As Boolean
            Dim intCountOfMatched As Integer

            If strCSIPath = "" Then
                Exit Sub
            End If

            For i = 0 To dtCD.Rows.Count - 1
                ChallanSrno = Val(dtCD.Rows(i)("ChlnNo"))
                BsrCode = dtCD.Rows(i)("BSR")
                ChallanDt = dtCD.Rows(i)("ChlnDT")
                Amount = dtCD.Rows(i)("Total")
                IsValidChallan = VerifyChallan(strCSIPath, BsrCode, ChallanDt, ChallanSrno, Amount)
                If IsValidChallan Then
                    dtCD.Rows(i)("IsChallanMatched") = True
                    intCountOfMatched = intCountOfMatched + 1
                Else
                    'Update Reason1 in Challan Details for Unmatched Challan
                    dtCD.Rows(i)("Reason1") = "R10"
                End If
            Next

            intCountOfUnMatched = dtCD.Rows.Count - intCountOfMatched
            blnFormClose = False
            frmProcess.Show()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")
        End Try


    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Try
            lblSelectCsiPath.Visible = True
            txtCSIFileSelection.Visible = True
            btnCsiDown.Visible = True
            'Call CsiFile(dtBH.Rows(0)("TAN"), dtBH.Rows(0)("Fin Yr"))

            Dim now As DateTime = DateTime.Now
            Dim strCSIDownloadFilePath As String = String.Concat(Path.Combine(Application.StartupPath, String.Concat(dtBH.Rows(0)("TAN"), String.Format("{0:ddMMyy}", now.Date))), ".csi")
            If (File.Exists(strCSIDownloadFilePath)) Then
                File.Delete(strCSIDownloadFilePath)
            End If

            'Ver 6.00-REQ694 start
            'Call DownloadCSI(dtBH.Rows(0)("TAN"), dtBH.Rows(0)("Fin Yr"), strCSIDownloadFilePath)
            'strCSIPath = txtCSIFileSelection.Text
            'Call readCSI()


            'Dim StrFinYear As String, StrFinToYear As String
            'StrFinYear = lblFy.Text.ToString().Replace("-", "").Replace("FY :", "").Trim
            'If (Convert.ToInt32((StrFinYear.Substring(0, 2) + (StrFinYear.Substring(4, 2) + "0630"))) > Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, "0") + DateTime.Now.Day.ToString().PadLeft(2, "0"))) Then
            '    StrFinToYear = Date.Now.Day.ToString().PadLeft(2, "0") + "/" + DateTime.Now.Month.ToString().PadLeft(2, "0") + "/" + DateTime.Now.Year.ToString
            'Else
            '    StrFinToYear = "30/06/" + StrFinYear.Substring(0, 2) + StrFinYear.Substring(4, 2)
            'End If

            Dim StrFinYear As String, StrFinToYear As String
            Dim todate As Date = Nothing
            StrFinYear = lblFy.Text.ToString().Replace("-", "").Replace("FY :", "").Trim
            If StrFinYear.Substring(0, 4) = "2020" Then
                todate = DateTime.Parse("31/12/2019").AddMonths(24)
            Else
                todate = DateTime.Parse("31/03/" + StrFinYear.Substring(0, 4)).AddMonths(24)
            End If
            If (todate > DateTime.Now) Then
                StrFinToYear = DateTime.Now.Day.ToString("00") & "/" & DateTime.Now.Month.ToString("00") & "/" & DateTime.Now.Year
            Else
                StrFinToYear = todate.ToString("dd/MM/yyyy")
            End If

            Dim TAN_NO As String = lblTan.Text.ToString.Replace("TAN :", "").Trim
            Dim TAN_FROM_DT_DD As String = "01"
            Dim TAN_FROM_DT_MM As String = IIf(StrFinYear.Substring(0, 4) = "2020", "01", "04")
            Dim TAN_FROM_DT_YY As String = StrFinYear.Substring(0, 4)
            Dim TAN_TO_DT_DD As String = StrFinToYear.Substring(0, 2)
            Dim TAN_TO_DT_MM As String = StrFinToYear.Substring(3, 2)
            Dim TAN_TO_DT_YY As String = StrFinToYear.Substring(6, 4)

            If (File.Exists(Application.StartupPath + "\Captcha.exe")) Then

                btnCsiDown.Enabled = False
                Dim Capt_process As Process = New Process()
                Capt_process.StartInfo.FileName = Application.StartupPath + "\Captcha.exe "
                Capt_process.StartInfo.Arguments = TAN_NO.ToString() + "~" + TAN_FROM_DT_DD.ToString() + "~" + TAN_FROM_DT_MM.ToString() + "~" + TAN_FROM_DT_YY.ToString() + "~" + TAN_TO_DT_DD.ToString() + "~" + TAN_TO_DT_MM.ToString() + "~" + TAN_TO_DT_YY.ToString() + "~" + strProductName
                Capt_process.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                Capt_process.Start()
                Capt_process.WaitForExit()
                Capt_process.Dispose()
                btnCsiDown.Enabled = True

            Else
                MessageBox.Show("Captcha file is not found in this machine.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            'Ver 6.00-REQ694 end
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")
        End Try
    End Sub

    'Version 2015-16 changes 
    Public Sub DownloadCSI(ByVal TANDeductorVal As String, ByVal StrFinYear As String, ByVal strCSIDownloadFilePath As String)

        Dim StrFinToYear As String

        If Val(Mid(StrFinYear, 1, 2) & Mid(StrFinYear, 5, 4) & "0630") > Val(Today.Year & Format(Today.Month, "00") & Format(Today.Day, "00")) Then
            StrFinToYear = Format(Today.Day, "00") & "/" & Format(Today.Month, "00") & "/" & Today.Year
        Else
            StrFinToYear = "30/06/" & Mid(StrFinYear, 1, 2) & Mid(StrFinYear, 5, 4)
        End If

        'Dim strArrays() As String = {"https://tin.tin.nsdl.com/oltas/servlet/TanSearch/?appUser=T&TAN_NO=", TANDeductorVal,
        '       "&TAN_FROM_DT_DD=", "01",
        '       "&TAN_FROM_DT_MM=", "04",
        '       "&TAN_FROM_DT_YY=", StrFinYear.Substring(0, 4),
        '       "&TAN_TO_DT_DD=", StrFinToYear.Substring(0, 2),
        '       "&TAN_TO_DT_MM=", StrFinToYear.Substring(3, 2),
        '       "&TAN_TO_DT_YY=", StrFinToYear.Substring(6, 4), "&submit=Download Challan file"}
        Dim strCaptcha As String = ""
        Dim strArrays() As String = {"https://tin.tin.nsdl.com/oltas/servlet/TanSearch/?appUser=T&TAN_NO=", TANDeductorVal,
       "&TAN_FROM_DT_DD=", "01",
       "&TAN_FROM_DT_MM=", "04",
       "&TAN_FROM_DT_YY=", StrFinYear.Substring(0, 4),
       "&TAN_TO_DT_DD=", StrFinToYear.Substring(0, 2),
       "&TAN_TO_DT_MM=", StrFinToYear.Substring(3, 2),
       "&TAN_TO_DT_YY=", StrFinToYear.Substring(6, 4),
       "&HID_IMG_TXT=", strCaptcha,
       "&submit=Download Challan file"}

        Dim str As String = String.Concat(strArrays)

        If System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() = False Then
            txtCSIFileSelection.Text = ""
            Return
        End If

        Dim webClient = New WebClient()
        webClient.DownloadFileAsync(New Uri(str), String.Concat("", strCSIDownloadFilePath))
        webClient.Dispose()
        Dim num As Long = 0
        Dim num1 As Long = 0
        While num1 <= 2

            If ((New FileInfo(String.Concat("", strCSIDownloadFilePath))).Length <= 0) Then
                num = num + 1
                num1 = 0
                num1 = num1 + 1
                num1 = num1 + 1
            Else
                txtCSIFileSelection.Text = strCSIDownloadFilePath
                Return
            End If

        End While

        txtCSIFileSelection.Text = strCSIDownloadFilePath
        Return


    End Sub

    Private Sub chkVerifyChallan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVerifyChallan.CheckedChanged
        If chkVerifyChallan.Checked = True Then
            blnVerifyChallanSkip = False
        Else
            blnVerifyChallanSkip = True
        End If
    End Sub

    Private Sub chkVerifyPAN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVerifyPAN.CheckedChanged
        If chkVerifyPAN.Checked = True Then
            'Ver 4.042-QC?? start
            chkNetConnetion.Visible = True
            'Ver 4.042-QC?? end
            blnVerifyPANSkip = False
        Else
            'Ver 4.042-QC?? start
            chkNetConnetion.Visible = False
            chkNetConnetion.Checked = False
            blnCheckNetConnetion = False
            'Ver 4.042-QC?? end
            blnVerifyPANSkip = True
        End If
    End Sub

    Private Sub frmFileSelection_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        blnFormClose = True
    End Sub

    'Ver 1.01-E585 Start
    Private Sub lblClickHereLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblClickHereLink.LinkClicked
        Process.Start(Application.StartupPath & "\CustomerReg.exe", vbNormalFocus)
        Application.Exit()
    End Sub
    'Ver 1.01-E585 End

    'Ver 2.02-REQ235 start
    Private Sub chkLateFiling_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLateFiling.CheckedChanged
        If chkLateFiling.Checked = True Then
            lblSubmittedDesc.Enabled = True
            lblSubmittedDate.Enabled = True
            dtpSubmittedDate.Enabled = True
        Else
            lblSubmittedDesc.Enabled = False
            lblSubmittedDate.Enabled = False
            dtpSubmittedDate.Enabled = False
        End If

    End Sub
    'Ver 2.02-REQ235 end

    'Ver 3.0.7-REQ417 start 
    Private Sub txtFileSelection_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFileSelection.TextChanged
        'Call Read()
        If blnChkDNFIntegrate = True Then
            Call btnSelect_Click(e, e)
        End If
    End Sub
    'Ver 3.0.7-REQ417 end  

    'Ver 4.042-QC?? start
    Private Sub chkNetConnetion_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkNetConnetion.CheckedChanged
        If chkNetConnetion.Checked = True Then
            blnCheckNetConnetion = True
        Else
            blnCheckNetConnetion = False
        End If
    End Sub
    'Ver 4.042-QC?? end

    Private Sub Userstatus2_Load(sender As Object, e As EventArgs) Handles Userstatus2.Load

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub
    Private Sub txtShortDeductVal_Validated(sender As Object, e As EventArgs) Handles txtShortDeductVal.Validated

        If IsNumeric(txtShortDeductVal.Text) Then
            Dim fs As FileStream = Nothing
            Dim filepath As String = Application.StartupPath + "\ShortDeduction.txt"
            If (File.Exists(filepath)) Then
                File.Delete(filepath)
                fs = File.Create(filepath)
                fs.Close()
            Else
                fs = File.Create(filepath)
                fs.Close()
            End If

            Dim objWriter As New System.IO.StreamWriter(filepath)
            objWriter.Write(txtShortDeductVal.Text)
            objWriter.Close()
        Else
            MessageBox.Show("Please enter a number.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtShortDeductVal.Text = ""
            txtShortDeductVal.Focus()
        End If

    End Sub
End Class