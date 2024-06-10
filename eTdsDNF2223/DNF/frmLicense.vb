Imports SoftLicensingLib
Imports SoftLicensingService
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports VersionCheck.ReturnStatus
Imports Microsoft.Win32

Public Class frmLicense
    'Dim strProductCode As String = My.MySettings.Default("ProductCode").ToString()
    'Dim renewalDays As Integer = My.MySettings.Default("RenewalPeriod").ToString()
    Dim strProductCode As String
    Dim renewalDays As Integer
    Dim strConfigProductName As String
    'Const strProductName As String = "eTdsDNF1718"
    Dim strRegistrationKey As String
    'Dim strMessageboxHeading As String = "eTdsDNF"  
    Dim strLICDetailXMLFile As String = Application.StartupPath + "\\SLDetails.xml"
    Dim strConfigFile As String = Application.StartupPath + "\\ACF.dll"
    Dim isValidConfig As Boolean = False
    Dim strTReTdsEmailId As String = "etds@fastfacts.co"
    Dim strFastFactWebsite As String = "http://www.fastfacts.co.in"
    Dim strTaxAccountingWebsite As String = "https://fastfacts.co.in"
    Dim strDNFActivationKey As String = Application.StartupPath + "\\activation.dll"
    'Ver 8.00-FastFacts-652->FastFacts-664 start

    Dim strApplicationPath As String = "C:\eTdsDNF2425"
    Dim strSLDetailsPath As String = "C:\eTdsDNF2425\SLDetail.xml"
    Dim strSLDetailsPathPrevious As String = "C:\eTdsDNF2425\SLDetails.xml"

    'Dim strApplicationPath As String = "C:\eTdsWizard\eTdsDNF"
    'Dim strSLDetailsPath As String = "C:\eTdsWizard\eTdsDNF\SLDetail.xml"
    'Dim strSLDetailsPathPrevious As String = "C:\eTdsWizard\eTdsDNF\SLDetails.xml"

    'Dim strSLDetailsPathPrevious As String = "C:\eTdsDNF1819\SLDetails.xml"
    'Ver 8.00-FastFacts-652->FastFacts-664 end
    'QA Link
    'Dim strCustomerRegistrationLink As String = "http://hyd-hubd-s01:8080/SLCustomer/Register/Index"
    'Dim strCustomerLoginLink As String = "http://hyd-hubd-s01:8080/SLCustomer"
    'Dim strActivationURL As String = "http://localhost/SLS/Activation"

    'Production Link
    Dim strCustomerRegistrationLink As String = "http://ffcs.in/SLCustomer/Register/Index"
    Dim strCustomerLoginLink As String = "http://ffcs.in/SLCustomer/"
    Dim strActivationURL As String = "http://ffcs.in/SLCustomer/Register/Index"

    Private StrLink As String = "http://www.fastfacts.co.in/eTdsDNF2223" & ".htm"
    Private StrDownloadLink As String = "https://www.fastfacts.co.in/downloads.aspx"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        '        If (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles))) Then
        '                        {
        '                If (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Java\jre1.8.0_65\bin\javaw.exe") == False) Then
        '                                {
        '                    MessageBox.Show("Required Java version (1.8) is not found. Please install java 8", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        '                    Application.ExitThread(); Application.Exit();
        '                }
        '            }
        'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5

        If File.Exists(Path.Combine(Application.StartupPath, "Default-Notice-Forecaster.sys")) Then
            File.Delete(Path.Combine(Application.StartupPath, "Default-Notice-Forecaster.sys"))
        End If
        Get45or451FromRegistry()

        'If (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles))) Then
        '    'Dim strarr As String() = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\Reference Assemblies\Microsoft\Framework\.NETFramework\")
        '    Dim strarr As String() = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\Reference Assemblies\Microsoft\Framework\.NETFramework\")
        '    Dim strlen As Integer = Len(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\Reference Assemblies\Microsoft\Framework\.NETFramework\")
        '    Dim decilst As New List(Of Decimal)
        '    For Each str As String In strarr
        '        If str.Contains("X") Then Continue For
        '        decilst.Add(str.Substring(strlen + 1, 3))
        '    Next
        '    Dim maxval As Decimal = decilst.Max
        '    If maxval < 4.5 Then
        '        Dim Strsql As String = "Required Donet Framework 4.5 and above."
        '        Strsql &= vbCrLf & "Click OK to Download."
        '        If (MessageBox.Show(Strsql, Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question)) = DialogResult.OK Then
        '            Process.Start("https://dotnet.microsoft.com/download/dotnet-framework/net45") : End
        '        End If
        '    End If
        'End If

        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub Get45or451FromRegistry()
        Using ndpKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\")
            Dim releaseKey As Integer = Convert.ToInt32(ndpKey.GetValue("Release"))
            If releaseKey < 378389 Then
                Dim Strsql As String = "Required Donet Framework 4.5 and above."
                Strsql &= vbCrLf & "Click OK to Download."
                If (MessageBox.Show(Strsql, Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question)) = DialogResult.OK Then
                    Process.Start("https://dotnet.microsoft.com/download/dotnet-framework/net45") : End
                End If
            End If
        End Using
    End Sub

    'Private oVersionCheck As New VersionCheck.VersionCheck(StrLink, StrDownloadLink, Application.ProductVersion)
    'Private oReturnStatus As New VersionCheck.ReturnStatus
    'Private Function AddHandlers() As VersionCheck.ReturnStatus
    '    'Try
    '    '    AddHandler BtnDownload.Click, AddressOf Download_Click
    '    '    Return New VersionCheck.ReturnStatus(True)
    '    'Catch ex As Exception
    '    '    Return New VersionCheck.ReturnStatus(False, ex.Message)
    '    'End Try
    'End Function
    Private Sub Download_Click(sender As Object, e As EventArgs)
        Try
            'Process.Start(oVersionCheck.DownloadLink)
            'Application.ExitThread()
            'Application.Exit()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Try
            If Timer.Tag = True Then
                lblversion.Text = "You Are Using Old Version. Download New Version."
                Timer.Tag = False
            Else
                lblversion.Text = ""
                Timer.Tag = True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub frmLicense()

        strLICXMLFile = strLICDetailXMLFile

        If (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() = True) Then
            optOnline.Checked = True
        Else

            optOffline.Checked = True
        End If

        Dim isExpired As Boolean = False
        blnDemoVersion = True

        isValidConfig = ReadConfigFile(strConfigFile)

        If (isValidConfig = False) Then
            Application.Exit()
            End
        End If


        ''DNF Chnages Ver 4.02 start
        If strProductCode <> strStandaloneDNFPcode Then
            Application.Exit()
            End
        End If
        ''DNF Chnages Ver 4.02 end


        ' Generate Registration Key
        Dim aes = New AESEncryption()
        aes.AesIV = "~X#M^)GZ_JB$Q(&B"
        aes.AesKey = "`B*S<D>G+{I}B[T]"

        'Dim filePath As String = Path.GetFullPath("activation.dll")
        Dim filePath As String = strDNFActivationKey

        If (ValidateRenewalTime(renewalDays, isExpired, aes, filePath)) Then
            AddLastUsedDate(filePath, aes)
            'Program.blnIsDemoVersion = false
            'CommonModule.blnIsDemoVersion = False
            CommonModule.blnDemoVersion = False
            'CommonModule.blnIsSoftLicenceActivated = True
            'this.Shown += New EventHandler(MyForm_CloseOnStart)
            ShowMainForm()
        Else
            txtRegKeyOnline.Text = Activation.GetRegistrationKey(strProductCode)
            txtRegKeyOffline.Text = txtRegKeyOnline.Text
            txtActivationUrl.Text = strActivationURL

            'Auto ReActivate
            If (File.Exists(filePath) = True) Then


                If (File.Exists(strLICDetailXMLFile) = True) Then

                    Using dsLIC = New DataSet()

                        dsLIC.ReadXml(strLICDetailXMLFile)
                        If (dsLIC.Tables(0).Rows.Count > 0) Then

                            txtProductKey.Text = dsLIC.Tables(0).Rows(0)("ProductKey").ToString()
                            txtProductKeyOffline.Text = txtProductKey.Text

                            If (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() = True) Then

                                Dim blnReActivated As Boolean
                                blnReActivated = AutoReActivate()
                                If (blnReActivated) Then

                                    'Program.blnIsDemoVersion = false;  jk
                                    'CommonModule.blnIsDemoVersion = False
                                    CommonModule.blnDemoVersion = False
                                    'CommonModule.blnIsSoftLicenceActivated = True
                                    'Me.Shown += New EventHandler(MyForm_CloseOnStart)
                                    ShowMainForm()
                                End If
                            End If

                        End If
                    End Using
                End If
            End If
        End If
    End Sub
    Private Sub btnDemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemo.Click
        ' CommonModule.blnIsDemoVersion = True
        CommonModule.blnDemoVersion = True
        'CommonModule.blnIsSoftLicenceActivated = False
        ReadCustomerCode()
        Me.Hide()
        Dim GenObj = New frmGeneralInfo()
        GenObj.Show()
    End Sub
    Private Sub frmLicense_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'If oVersionCheck.Older Then
        '    TLPVersion.Visible = True

        '    oReturnStatus = AddHandlers()
        '    If Not oReturnStatus.BlnReturn Then Throw New Exception(oReturnStatus.StrReturn)

        '    Timer = New Windows.Forms.Timer
        '    Timer.Interval = 500 : Timer.Enabled = True
        '    Timer.Start()
        'End If

        'If Application.StartupPath <> "C:\eTdsDNF" & ConstStrFYyr Then
        '    MessageBox.Show("Please run the application from C:\eTdsDNF" & ConstStrFYyr & " folder.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End
        'End If

        'Ver 4.02 start
        Dim bPassStartUpPath As Boolean
        bPassStartUpPath = True
        blnchkVerifyPAN = True
        blnChkDNFIntegrate = 0
        Dim strTdsPacParamter() As String
        Dim strTempTdsPacParamter() As String
        'Ver 4.02 end

        ''DNF Chnages Ver 4.02 start
        ' If (File.Exists(strConfigFile) = False) Then
        ''DNF Chnages Ver 4.02 end
        ''
        'Shell(DNFPath & " " & seTdsPath & " ~" & True & "~" & blnchkVerifyPAN & "~" & blnchkLateFiling & "~" & strLateFilingDate & "~" & strCSIValidPath & "~" & blnChkListPANName & "~" & True & "~" & blnchkNetConnetion, AppWinStyle.NormalFocus)
        'strTempTdsPacParamter = "D:\backup041123Anu\Anu\NewGen_CO\Noopur\IN\DNF_8.6\Form24Q4.txt~True~True~True~25/04/2024 15:50:45~D:\backup041123Anu\Anu\NewGen_CO\Noopur\IN\DNF_8.6\JDHB04817B200424.csi~True~True~True~True".Split("~")
        'strTempTdsPacParamter = "C:\eTdsWizard\MULRAJINDRAVADANSHAH202324\27Q\Q1\Form27Q1.txt~True~True~True~21/09/2023 15:50:45~E:\Anu\Noopur\IN\23-24,27Q_Q1\MUMM17774B080923.csi~True~True~True~True".Split("~")
        strTempTdsPacParamter = Command.Split("~")
        ''DNF Chnages Ver 4.02 start
        If strTempTdsPacParamter.Count > 1 Then
            ''DNF Chnages Ver 4.02 end
            'Ver 7.05-FASTFACTS-584 start
            'If (strTempTdsPacParamter.Count = 8) Then
            If (strTempTdsPacParamter.Count >= 9) Then
                'Ver 7.05-FASTFACTS-584 end 

                strFilePath = strTempTdsPacParamter(0)
                blnchkVerifyPAN = strTempTdsPacParamter(2)
                blnchkLateFiling = strTempTdsPacParamter(3)
                strLateFilingDate = strTempTdsPacParamter(4)
                strCSIValidPath = strTempTdsPacParamter(5)
                blnChkListPANName = strTempTdsPacParamter(6)

                blnChkDNFIntegrate = 1

                If strTempTdsPacParamter(7) = True Then
                    CommonModule.blnDemoVersion = False
                Else
                    CommonModule.blnDemoVersion = True
                End If
                'Ver 7.05-FASTFACTS-584 start
                blnCheckNetConnetion = strTempTdsPacParamter(8)
                If strTempTdsPacParamter.Count = 10 Then
                    iswizard = True
                Else
                    iswizard = False
                End If
                'Ver 7.05-FASTFACTS-584 end 
                Me.Hide()
                Dim GenObj = New frmGeneralInfo()
                GenObj.Show()
                Exit Sub
            Else
                End
            End If
            ''DNF Chnages Ver 4.02 start
        End If
        'End If
        ''DNF Chnages Ver 4.02 end
        'Ver 4.02 end

        If iswizard Then
            strApplicationPath = "C:\eTdsWizard\eTdsDNF"
            strSLDetailsPath = "C:\eTdsWizard\eTdsDNF\SLDetail.xml"
            strSLDetailsPathPrevious = "C:\eTdsWizard\eTdsDNF\SLDetails.xml"
            If Application.StartupPath <> "C:\eTdsWizard\eTdsDNF" Then
                MessageBox.Show("Please run the application from C:\eTdsWizard\eTdsDNF folder.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End If
        Else
            If Application.StartupPath <> "C:\eTdsDNF" & ConstStrFYyr Then
                MessageBox.Show("Please run the application from C:\eTdsDNF" & ConstStrFYyr & " folder.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End If
        End If
        Call frmLicense()

        'Ver 4.02 start
        'Dim bPassStartUpPath As Boolean
        'bPassStartUpPath = True
        'blnchkVerifyPAN = True
        'blnChkDNFIntegrate = 0
        'Dim strTdsPacParamter() As String
        'Ver 4.02 end


        ''DNF Chnages Ver 4.02 start
        'strTdsPacParamter = Command.Split("~")
        'If strTdsPacParamter.Length > 0 And strTdsPacParamter(0) <> "" Then
        '    strFilePath = strTdsPacParamter(0)
        '    'blnIsProductRegistered = (strTdsPacParamter(1))
        '    blnchkVerifyPAN = strTdsPacParamter(2)
        '    blnchkLateFiling = strTdsPacParamter(3)
        '    strLateFilingDate = strTdsPacParamter(4)
        '    strCSIValidPath = strTdsPacParamter(5)
        '    blnChkListPANName = strTdsPacParamter(6)
        '    blnChkDNFIntegrate = 1

        'Else
        '    blnChkDNFIntegrate = 0
        'End If
        ''DNF Chnages Ver 4.02 end

        Call CustomerRegistrationView()

    End Sub
    Private Sub MyForm_CloseOnStart(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosed

        'Me.Hide()
        'Dim GenObj = New frmGeneralInfo()
        'GenObj.Show()

        Application.Exit()
    End Sub

    Private Sub CustomerRegistrationView()
        Dim customerCode As String = ""
        lblExistingUser.Visible = True
        rbtnExistingUser1617.Visible = True
        grpOnlineSteps1.Visible = True
        grpOnlineSteps.Visible = False
        rbtnNewUser.Visible = True

        If File.Exists(strApplicationPath & "\\Activation.dll") = True Then
            If File.Exists(strSLDetailsPath) = True Then
                Panel1.Visible = True
                panel2.Visible = False
                linkLabelNewUser.Visible = False
                Using dsLIC As New DataSet()
                    dsLIC.ReadXml(strSLDetailsPath)
                    If dsLIC.Tables(0).Rows.Count > 0 Then
                        customerCode = dsLIC.Tables(0).Rows(0)("CustomerCode").ToString()
                        If customerCode <> "" Then
                            label1.Visible = True
                        ElseIf File.Exists(strSLDetailsPathPrevious) = True Then
                            panel2.Visible = True
                            Panel1.Visible = False
                            Using dsLICPrev As New DataSet()
                                dsLICPrev.ReadXml(strSLDetailsPathPrevious)
                                If dsLICPrev.Tables(0).Rows.Count > 0 Then
                                    customerCode = dsLICPrev.Tables(0).Rows(0)("CustomerCode").ToString()
                                    If customerCode <> "" Then
                                        lblExistingUser.Visible = True
                                        label1.Visible = False
                                        label25.Visible = False
                                        lblCustomerReg.Visible = False
                                        rbtnExistingUser1617.Visible = False
                                        rbtnNewUser.Visible = False
                                    Else
                                        rbtnExistingUser1617.Visible = True
                                        grpOnlineSteps1.Visible = True
                                        grpOnlineSteps.Visible = False
                                        linkLabelExisting1617.Visible = False
                                        rbtnNewUser.Visible = True
                                        linkLabelNewUser.Visible = False
                                        label1.Visible = False
                                        label25.Visible = False
                                        linkLabelExistingUser.Visible = False
                                        lblCustomerReg.Visible = False
                                        Panel1.Visible = True
                                        panel2.Visible = False
                                    End If
                                End If
                            End Using
                        End If

                    End If
                End Using
            End If
        Else
            If File.Exists(strSLDetailsPathPrevious) = True Then
                panel2.Visible = True
                Panel1.Visible = False

                Using dsLICPrev As New DataSet()
                    dsLICPrev.ReadXml(strSLDetailsPathPrevious)
                    If dsLICPrev.Tables(0).Rows.Count > 0 Then
                        customerCode = dsLICPrev.Tables(0).Rows(0)("CustomerCode").ToString()
                        If customerCode <> "" Then
                            lblExistingUser.Visible = True
                            label1.Visible = False
                            label25.Visible = False
                            lblCustomerReg.Visible = False
                            rbtnExistingUser1617.Visible = False
                            rbtnNewUser.Visible = False
                            panel2.Visible = True
                        Else
                            rbtnExistingUser1617.Visible = True
                            grpOnlineSteps1.Visible = True
                            grpOnlineSteps.Visible = False
                            linkLabelExisting1617.Visible = False
                            rbtnNewUser.Visible = True
                            linkLabelNewUser.Visible = False
                            label1.Visible = False
                            label25.Visible = False
                            linkLabelExistingUser.Visible = False
                            lblCustomerReg.Visible = False
                            Panel1.Visible = True
                        End If
                    End If
                End Using
            Else
                panel2.Visible = False
                rbtnExistingUser1617.Visible = True
                grpOnlineSteps1.Visible = True
                grpOnlineSteps.Visible = False
                linkLabelExisting1617.Visible = True
                rbtnNewUser.Visible = True
                linkLabelNewUser.Visible = True
                label1.Visible = False
                label25.Visible = False
                linkLabelExistingUser.Visible = False
                lblCustomerReg.Visible = False
                linkLabelNewUser.Visible = False
                Panel1.Visible = True
            End If
        End If

    End Sub

    Private Function ValidateRenewalTime(ByVal renewalDays As Integer, ByVal isExpired As Boolean, ByVal aes As AESEncryption, ByVal filePath As String) As Boolean

        Dim decryptedActivationkey As String = ""
        Dim activationFile = New FileInfo(filePath)
        If (activationFile.Exists) Then

            Using sr = New StreamReader(filePath)

                Dim aKey As String = sr.ReadToEnd()
                Dim strRegistrationKey As String = Activation.GetRegistrationKey(strProductCode)
                Try

                    decryptedActivationkey = aes.Decrypt(aKey)
                    Dim activationDetails() As String
                    activationDetails = decryptedActivationkey.Split("+")
                    Dim activatedOn, activatedTill, lastUsedDate, currentDate As DateTime
                    Dim encryptedActivationKey As String = activationDetails(0)
                    Dim strProductKey As String = activationDetails(1)

                    activatedOn = Convert.ToDateTime(activationDetails(2))
                    activatedTill = Convert.ToDateTime(activationDetails(3))
                    lastUsedDate = Convert.ToDateTime(activationDetails(4))
                    currentDate = DateTime.Now


                    'Dim date1 As DateTime = DateTime.ParseExact(activationDetails(2), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    'Dim dDate As DateTime = DateTime.Parse(activationDetails(2), CultureInfo.InvariantCulture)
                    'Dim strDayFirst As DateTime = Format(dDate, "dd/MM/yyyy hh:mm:ss tt")


                    Dim generatedActivationKey As String = KeyGenerator.GenerateActivationKey(strRegistrationKey, strProductCode, strProductKey, activatedOn, renewalDays)

                    If (generatedActivationKey = encryptedActivationKey And lastUsedDate <= currentDate And currentDate <= activatedTill) Then
                        Return True
                    Else
                        Return False

                    End If
                Catch ex As CryptographicException

                    'throw new Exception("Activation file manipulated");
                    MessageBox.Show("Activation file manipulated", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MessageBox.Show("Invalid Activation file", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End Using
        End If
        Return isExpired
    End Function

    Private Sub AddLastUsedDate(ByVal filePath As String, ByVal aes As AESEncryption)

        Dim decryptedActivationkey As String = String.Empty
        'Dim currentDate, activatedOn, activatedTill As DateTime

        'currentDate = DateTime.Now
        'activatedOn = New DateTime()
        'activatedTill = New DateTime()

        Dim activationFile = New FileInfo(filePath)
        If (activationFile.Exists) Then

            Using sr = New StreamReader(filePath)

                Dim aKey As String = sr.ReadToEnd()

                decryptedActivationkey = aes.Decrypt(aKey)
                'activatedOn = Convert.ToDateTime(decryptedActivationkey.Split("+")(1))
                'activatedTill = Convert.ToDateTime(decryptedActivationkey.Split("+")(2))
                'decryptedActivationkey = decryptedActivationkey.Replace(activatedOn.ToString(), currentDate.ToString())
                Dim lastUsedDate As DateTime = Convert.ToDateTime(decryptedActivationkey.Split("+")(4))
                decryptedActivationkey = decryptedActivationkey.Replace(lastUsedDate.ToString(), DateTime.Now.ToString())

            End Using
            Try
                ' Activation.StoreActivationKey(decryptedActivationkey, aes, filePath, currentDate, activatedTill)
                Activation.StoreActivationKey(decryptedActivationkey, aes, filePath)

            Catch ex As Exception

                MessageBox.Show("Please try again", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ProductActivation(ByVal isInsert As Boolean)

        ' Dim filePath As String = Path.GetFullPath("activation.dll")
        Dim filePath As String = strDNFActivationKey

        '    // AES for encryption and decryption
        Dim aes = New AESEncryption()
        aes.AesIV = "~X#M^)GZ_JB$Q(&B"
        aes.AesKey = "`B*S<D>G+{I}B[T]"
        Dim activatedOn, activatedTill As DateTime
        activatedOn = DateTime.Now.Date
        activatedTill = activatedOn.AddDays(renewalDays)

        ' GenerateAndStoreActivionfileOnline(aes, filePath, activatedOn, activatedTill)
        If GenerateAndStoreActivionfileOnline(aes, filePath, activatedOn, activatedTill) = False Then
            Exit Sub
        End If


        ' //Store License Details Locally
        strRegistrationKey = txtRegKeyOnline.Text
        StoreLicenseDetails(strLICDetailXMLFile, txtProductKey.Text)

        If (isInsert) Then
            MessageBox.Show("Successfully Activated!!", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Successfully Re-Activated!!", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        'CommonModule.blnIsDemoVersion = False
        CommonModule.blnDemoVersion = False
        'CommonModule.blnIsSoftLicenceActivated = True
        ShowMainForm()

    End Sub

    Private Function GenerateAndStoreActivionfileOnline(ByVal aes As AESEncryption, ByVal filePath As String, ByVal activatedOn As DateTime, ByVal activatedTill As DateTime) As Boolean
        Dim blnIsSuccess As Boolean = False
        Dim ActivationKey As String = Activation.GetActivationKey(strProductCode, txtProductKey.Text, txtRegKeyOnline.Text.Trim())

        If (String.IsNullOrEmpty(ActivationKey) = False) Then

            Dim strActivationDetails As String = ActivationKey & "+" & txtProductKey.Text & "+" & activatedOn & "+" & activatedTill & "+" & DateTime.Now
            Activation.StoreActivationKey(strActivationDetails.ToString(), aes, filePath)
            blnIsSuccess = True
        Else
            MessageBox.Show("Some problem has occurred. Please try again.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            blnIsSuccess = False
        End If
        Return blnIsSuccess
    End Function

    Private Function GenerateAndStoreActivionfileOffline(ByVal aes As AESEncryption, ByVal filePath As String, ByVal activatedOn As DateTime, ByVal activatedTill As DateTime)
        Dim blnIsSuccess As Boolean = False
        Dim ActivationKey As String = Activation.GetActivationKey(strProductCode, txtProductKeyOffline.Text, txtRegKeyOffline.Text.Trim())

        If (String.IsNullOrEmpty(ActivationKey) = False) Then

            Dim strActivationDetails As String = ActivationKey & "+" & txtProductKeyOffline.Text & "+" & activatedOn & "+" & activatedTill & "+" & DateTime.Now
            Activation.StoreActivationKey(strActivationDetails.ToString(), aes, filePath)
            blnIsSuccess = True
        Else
            MessageBox.Show("Some problem has occurred. Please try again.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            blnIsSuccess = False
        End If
        Return blnIsSuccess
    End Function


    Private Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        If (txtProductKey.Text = "") Then
            MessageBox.Show("Please enter valid Product key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtProductKey.Focus()
            Return
        ElseIf (IsValidKey(txtProductKey.Text) = False) Then
            MessageBox.Show("Key should be in XXXX-XXXX-XXXX-XXXX format", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtProductKey.Focus()
            Return
        End If

        btnValidate.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        'Checking the product key status
        Dim strProductKeyStatus As String = Activation.GetProductKeyStatus(strProductCode, txtProductKey.Text, txtRegKeyOnline.Text)

        Select Case (strProductKeyStatus)

            Case "NeedRegistration"
                'MessageBox.Show("To proceed with activation product need to be registered.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If (DialogResult.Yes = MessageBox.Show("Your Product is not yet activated." + Environment.NewLine +
                     "To activate the same, you need to register." + Environment.NewLine + "OR" + Environment.NewLine +
                     "If you are a registered customer, then activate your product using your existing customer login" + Environment.NewLine +
                      Environment.NewLine + "Click 'Yes' To launch Customer Registration website" +
                      Environment.NewLine + "Click 'No' To launch Customer Login website", strMessageboxHeading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) Then

                    Process.Start(strCustomerRegistrationLink)

                Else

                    Process.Start(strCustomerLoginLink)
                End If

            Case "LicensesCompleted"
                MessageBox.Show("Number of licenses for the product key exceeded.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Case "Undefined"
                MessageBox.Show("Invalid Product Key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtProductKey.Focus()
            Case "ProductBlocked"
                MessageBox.Show("Product Activation Blocked.Need to contact Support help Desk.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Case "ProductFoundInsert"
                ProductActivation(True)

            Case "ProductFoundUpdate"
                ProductActivation(False)
            Case "ConnectionProblem"
                'MessageBox.Show("Please check your connection and try again", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If DialogResult.Yes = MessageBox.Show("Please check your connection and try again, Or Do you wish to activate it through offline mode?", strMessageboxHeading, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
                    optOffline.Checked = True
                    txtProductKeyOffline.Text = txtProductKey.Text
                    txtRegKeyOffline.Text = txtRegKeyOnline.Text
                End If
            Case Else
                'MessageBox.Show("Some error has occurred", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If (DialogResult.Yes = MessageBox.Show("Some error occurred during online activation. Do you wish to activate it through offline mode?", strMessageboxHeading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) Then
                    optOffline.Checked = True
                    txtProductKeyOffline.Text = txtProductKey.Text
                    txtRegKeyOffline.Text = txtRegKeyOnline.Text
                End If
        End Select
        btnValidate.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub



    Private Sub btnActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivate.Click
        If (txtProductKeyOffline.Text = "") Then
            MessageBox.Show("Please enter valid Product Key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtProductKeyOffline.Focus()
            Return

        ElseIf (txtActivationKey.Text = "") Then
            MessageBox.Show("Please enter valid Activation Key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtActivationKey.Focus()
            Return

        ElseIf (txtCustCodeOffline.Text = "") Then
            MessageBox.Show("Please enter valid Customer Code", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtCustCodeOffline.Focus()
            Return
        ElseIf (IsValidKey(txtProductKeyOffline.Text) = False) Then
            MessageBox.Show("Product Key should be in XXXX-XXXX-XXXX-XXXX format", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return

        ElseIf (IsValidKey(txtActivationKey.Text) = False) Then
            MessageBox.Show("Activation Key should be in XXXX-XXXX-XXXX-XXXX format", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        btnActivate.Enabled = False
        Me.Cursor = Cursors.WaitCursor


        Dim strProductKey As String = txtProductKeyOffline.Text
        Dim browsedActivationKey As String = txtActivationKey.Text
        Dim originalActivationKey As String = KeyGenerator.GenerateActivationKey(txtRegKeyOffline.Text, strProductCode, strProductKey, DateTime.Now.Date, renewalDays)

        If (originalActivationKey.Trim() = browsedActivationKey.Trim()) Then

            'Dim filePath As String = Path.GetFullPath("activation.dll")
            Dim filePath As String = strDNFActivationKey

            ' AES for encryption and decryption
            Dim Aes = New AESEncryption()
            Aes.AesIV = "~X#M^)GZ_JB$Q(&B"
            Aes.AesKey = "`B*S<D>G+{I}B[T]"
            Dim activatedOn As DateTime = DateTime.Now.Date
            Dim activatedTill As DateTime = activatedOn.AddDays(renewalDays)


            '// Successful activation
            Dim strActivationDetails As String = browsedActivationKey & "+" & strProductKey & "+" & activatedOn & "+" & activatedTill & "+" & DateTime.Now
            Activation.StoreActivationKey(strActivationDetails.ToString(), Aes, filePath)


            '//Store License Details Locally
            strRegistrationKey = txtRegKeyOffline.Text
            StoreLicenseDetails(strLICDetailXMLFile, txtProductKeyOffline.Text, txtCustCodeOffline.Text)

            MessageBox.Show("Successfully Activated!!", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '//Jitendra

            ' CommonModule.blnIsDemoVersion = False
            CommonModule.blnDemoVersion = False
            'CommonModule.blnIsSoftLicenceActivated = True
            ShowMainForm()

            '//Jitendra
            'Me.Hide()

        Else

            MessageBox.Show("Invalid Key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        btnActivate.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub EnableDisableControl()

        If (optOnline.Checked = True) Then
            grpOnline.Visible = True
            'grpOnline.Location = New System.Drawing.Point(529, 143)
            'grpOnlineSteps.Location = New System.Drawing.Point(23, 143)
            grpOffline.Visible = False
            grpOnlineSteps.Visible = True
            grpOfflineSteps.Visible = False

        Else
            grpOnline.Visible = False
            grpOffline.Visible = True
            'grpOffline.Location = New System.Drawing.Point(529, 143)
            'grpOfflineSteps.Location = New System.Drawing.Point(23, 143)
            grpOnlineSteps.Visible = False
            grpOfflineSteps.Visible = True
        End If
    End Sub

    Private Sub optOnline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOnline.CheckedChanged
        EnableDisableControl()
    End Sub

    Private Sub optOffline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOffline.CheckedChanged
        EnableDisableControl()
    End Sub

    Private Sub btnUploadFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUploadFile.Click
        Dim activationkey As String = String.Empty

        Dim dialog = New OpenFileDialog()
        dialog.Filter = "Text files | *.txt" '// file types, that will be allowed to upload
        dialog.Multiselect = False ' // allow/deny user to upload more than one file at a time

        If (dialog.ShowDialog() = DialogResult.OK) Then '// if user clicked OK

            Dim path As String = dialog.FileName '// get name of file
            Using reader = New StreamReader(New FileStream(path, FileMode.Open), New UTF8Encoding())

                activationkey = reader.ReadToEnd()
            End Using

        End If

        txtActivationKey.Text = activationkey
    End Sub


    Private Sub lblCustomerReg_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblCustomerReg.LinkClicked
        Process.Start(strCustomerRegistrationLink)
    End Sub

    Public Function StoreLicenseDetails(ByVal strFileName As String, ByVal strProductKey As String, Optional ByVal strCustomerCode As String = "") As Boolean

        Dim blnSuccess As Boolean = False
        Dim lstRetrunData = New Dictionary(Of String, String)()
        If (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() = True And strCustomerCode = "") Then

            lstRetrunData = Activation.GetCustomerProductDetails(strProductCode, strProductKey)
            If (IsDBNull(lstRetrunData)) Then
                blnSuccess = False
                Return blnSuccess
            End If
        End If
        Dim dsLicense = New DataSet()
        Dim dtTemp = New DataTable("License")
        dtTemp.Columns.Add("CustomerCode")
        dtTemp.Columns.Add("DealerCode")
        dtTemp.Columns.Add("EmailId")
        dtTemp.Columns.Add("RegKey")
        dtTemp.Columns.Add("ProductKey")
        dtTemp.Columns.Add("ProductCode")
        dtTemp.Columns.Add("SerialNo")


        Dim drRow = dtTemp.NewRow()
        If (lstRetrunData.Count > 0) Then

            drRow("CustomerCode") = lstRetrunData("CustomerCode")
            drRow("DealerCode") = lstRetrunData("DealerCode")
            drRow("EmailId") = lstRetrunData("UserEmail")
            drRow("SerialNo") = lstRetrunData("SrNo")

        Else

            If (strCustomerCode = "") Then
                drRow("CustomerCode") = ""
            Else
                drRow("CustomerCode") = strCustomerCode
            End If
            drRow("DealerCode") = ""
            drRow("EmailId") = ""
            drRow("SerialNo") = ""
        End If

        drRow("RegKey") = strRegistrationKey
        drRow("ProductKey") = strProductKey
        drRow("ProductCode") = strProductCode

        dtTemp.Rows.Add(drRow)
        dsLicense.Tables.Add(dtTemp)
        dsLicense.AcceptChanges()

        dsLicense.WriteXml(strFileName)
        blnSuccess = True

        Return blnSuccess
    End Function

    Private Function AutoReActivate() As Boolean

        Dim blnIsSuccess As Boolean = False
        Dim strProductKeyStatus As String = Activation.GetProductKeyStatus(strProductCode, txtProductKey.Text, txtRegKeyOnline.Text)

        Select Case (strProductKeyStatus)

            Case "NeedRegistration"
                If (DialogResult.Yes = MessageBox.Show("Your Product is not yet activated." + Environment.NewLine +
                   "To activate the same, you need to register." + Environment.NewLine + "OR" + Environment.NewLine +
                   "If you are a registered customer, then activate your product using your existing customer login" + Environment.NewLine +
                    Environment.NewLine + "Click 'Yes' To launch Customer Registration website" +
                    Environment.NewLine + "Click 'No' To launch Customer Login website", strMessageboxHeading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) Then

                    Process.Start(strCustomerRegistrationLink)

                Else

                    Process.Start(strCustomerLoginLink)
                End If
            Case "LicensesCompleted"
                MessageBox.Show("Number of licenses for the product key exceeded.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Case "Undefined"
                MessageBox.Show("Invalid Product Key", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtProductKey.Focus()
            Case "ProductBlocked"
                MessageBox.Show("Product Activation Blocked.Need to contact Support help Desk.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Case "ProductFoundInsert"
                blnIsSuccess = True
                blnIsSuccess = AutoProductActivation(True)

            Case "ProductFoundUpdate"
                blnIsSuccess = True
                blnIsSuccess = AutoProductActivation(False)
            Case "ConnectionProblem"
                'MessageBox.Show("Please check your connection and try again", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If DialogResult.Yes = MessageBox.Show("Please check your connection and try again, Or Do you wish to activate it through offline mode?", strMessageboxHeading, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
                    optOffline.Checked = True
                    txtProductKeyOffline.Text = txtProductKey.Text
                    txtRegKeyOffline.Text = txtRegKeyOnline.Text
                End If
            Case Else
                'MessageBox.Show("Some error has occurred", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                If (DialogResult.Yes = MessageBox.Show("Some error occurred during online activation. Do you wish to activate it through offline mode?", strMessageboxHeading, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) Then
                    optOffline.Checked = True
                    txtProductKeyOffline.Text = txtProductKey.Text
                    txtRegKeyOffline.Text = txtRegKeyOnline.Text
                End If
        End Select

        Return blnIsSuccess
    End Function

    Private Function AutoProductActivation(ByVal isInsert As Boolean) As Boolean

        Dim blnIsSuccess As Boolean = False
        'Dim filePath As String = Path.GetFullPath("activation.dll")
        Dim filePath As String = strDNFActivationKey

        '// AES for encryption and decryption
        Dim aes = New AESEncryption()
        aes.AesIV = "~X#M^)GZ_JB$Q(&B"
        aes.AesKey = "`B*S<D>G+{I}B[T]"
        Dim activatedOn As DateTime = DateTime.Now.Date
        Dim activatedTill As DateTime = activatedOn.AddDays(renewalDays)

        'GenerateAndStoreActivionfileOnline(aes, filePath, activatedOn, activatedTill)
        If GenerateAndStoreActivionfileOnline(aes, filePath, activatedOn, activatedTill) = False Then
            blnIsSuccess = False
            Return blnIsSuccess
            Exit Function
        End If


        '//Store License Details Locally
        strRegistrationKey = txtRegKeyOnline.Text
        StoreLicenseDetails(strLICDetailXMLFile, txtProductKey.Text)
        blnIsSuccess = True
        Return blnIsSuccess

    End Function

    Private Sub ShowMainForm()
        ReadCustomerCode()
        Me.Hide()
        Dim GenObj = New frmGeneralInfo()
        GenObj.Show()

    End Sub

    Private Sub frmLicense_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If blnDemoVersion = False Then
            Me.Hide()

        End If
    End Sub


    Private Function ReadConfigFile(ByVal strConfigFile As String) As Boolean

        Dim isSuccess As Boolean = False
        Try

            If (File.Exists(strConfigFile) = True) Then

                Dim Aes = New AESEncryption()
                Aes.AesIV = "~X#M^)GZ_JB$Q(&B"
                Aes.AesKey = "`B*S<D>G+{I}B[T]"
                Using sr = New StreamReader(strConfigFile)

                    Dim strConfigData As String = sr.ReadToEnd()
                    Dim decryptedConfigData As String = Aes.Decrypt(strConfigData)

                    strProductCode = decryptedConfigData.Split(New String() {"=&"}, StringSplitOptions.None)(1).ToString()
                    strConfigProductName = decryptedConfigData.Split(New String() {"=&"}, StringSplitOptions.None)(2).ToString()
                    Dim strRenewalDays As String = decryptedConfigData.Split(New String() {"=&"}, StringSplitOptions.None)(0).ToString()

                    isSuccess = True

                    If (IsNumeric(strRenewalDays) = False) Then
                        isSuccess = False
                        MessageBox.Show(strConfigFile + " is corrupted.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        renewalDays = Val(strRenewalDays)
                    End If


                    If String.Compare(strConfigProductName, strProductName) <> 0 Then
                        MessageBox.Show(strConfigFile + " is corrupted.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        isSuccess = False
                    End If



                End Using

            Else

                MessageBox.Show(strConfigFile + " is missing.", strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
                isSuccess = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
            isSuccess = False
        End Try
        Return isSuccess
    End Function

    Public Function IsValidKey(ByVal inputKey As String) As Boolean

        Dim strRegex As String = "^[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}$"
        Dim regEx = New Regex(strRegex)

        Return regEx.IsMatch(inputKey)

    End Function

    Private Sub lblShowMyPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblShowMyPC.Click
        Process.Start(My.Application.Info.DirectoryPath & "\ShowMyPC.exe")
    End Sub

    Private Sub pbShowMyPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbShowMyPC.Click
        Process.Start(My.Application.Info.DirectoryPath & "\ShowMyPC.exe")
    End Sub

    Private Sub lblTeamViewer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTeamViewer.Click
        Process.Start(My.Application.Info.DirectoryPath & "\QS.exe")
    End Sub

    Private Sub pcTeamViewer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcTeamViewer.Click
        Process.Start(My.Application.Info.DirectoryPath & "\QS.exe")
    End Sub

    Private Sub lblBottomEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBottomEmail.Click
        OpenOutlook()
    End Sub
    Private Sub OpenOutlook()
        Try

            'Process.Start(String.Format("mailto:{0}?subject={1}&cc={2}&bcc={3}&body={4}", strTReTdsEmailId, "", "", "", ""));
            Process.Start(String.Format("mailto:{0}", strTReTdsEmailId))

        Catch ex As Exception

            MessageBox.Show(ex.Message, strMessageboxHeading, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub pbBottomEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbBottomEmail.Click
        OpenOutlook()
    End Sub

    Private Sub lblWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblWeb.Click
        Process.Start(strFastFactWebsite)
    End Sub

    Private Sub pbWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbWeb.Click
        Process.Start(strFastFactWebsite)
    End Sub

    Private Sub lblTaxAccountWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Process.Start(strTaxAccountingWebsite)
    End Sub

    Private Sub lblFastfactsWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Process.Start(strFastFactWebsite)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    'Private Sub rbtnExistingUser1617_CheckedChanged(sender As Object, e As EventArgs)
    '    linkLabelExisting1617.Visible = True
    '    linkLabelNewUser.Visible = False
    'End Sub

    Private Sub linkLabelExisting1617_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkLabelExisting1617.LinkClicked
        Process.Start(strCustomerLoginLink)
    End Sub

    'Private Sub rbtnNewUser_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnNewUser.CheckedChanged
    '    linkLabelNewUser.Visible = True
    '    linkLabelExisting1617.Visible = False
    'End Sub

    Private Sub linkLabelNewUser_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkLabelNewUser.LinkClicked
        Process.Start(strCustomerRegistrationLink)
    End Sub

    Private Sub linkLabelExistingUser_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkLabelExistingUser.LinkClicked
        Process.Start(strCustomerLoginLink)
    End Sub

    Private Sub rbtnExistingUser1617_Click(sender As Object, e As EventArgs) Handles rbtnExistingUser1617.Click
        linkLabelExisting1617.Visible = True
        linkLabelNewUser.Visible = False
        grpOnlineSteps1.Visible = True
        grpOnlineSteps.Visible = False
    End Sub

    Private Sub rbtnNewUser_Click(sender As Object, e As EventArgs) Handles rbtnNewUser.Click
        linkLabelNewUser.Visible = True
        linkLabelExisting1617.Visible = False
        grpOnlineSteps1.Visible = False
        grpOnlineSteps.Visible = True
    End Sub
End Class