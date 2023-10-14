Imports System.Data
Imports System.IO
Public Class frmRegister

    Private Sub lblLicence_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblLicence.LinkClicked
        Process.Start("http://www.fastfacts.co.in/Resources/FastFactsLicensingPolicy.pdf")
    End Sub

    Private Sub lblPrivacy_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblPrivacy.LinkClicked
        Process.Start("http://www.fastfacts.co.in/privacy_policy.html")
    End Sub

    Private Sub frmRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.CenterToScreen()
        lblRegkey.Visible = False
        txtRegistrationKey.Visible = False
        btnProceed.Visible = False
        txtCustomerName.Text = ""
        txtContPer.Text = ""
        TxtPhoneNo.Text = ""
        txtMobileNo.Text = ""
        txtEmail.Text = ""
        txtCustomerType.Text = ""
        txtDongleSrNo.Text = ""
        txtProduct.Text = ""
        txtYear.Text = ""

        If File.Exists("C:\eTdsDNF1314\Data.XML") Then
            Try

                Dim dt As New DataTable("CustInfo")
                dt.Columns.Add("Field")
                dt.Columns.Add("Value")
                dt.ReadXml("C:\eTdsDNF1314\Data.XML")

                txtCustomerName.Text = dt.Rows(0)(1).ToString()
                txtContPer.Text = dt.Rows(1)(1).ToString()
                TxtPhoneNo.Text = dt.Rows(2)(1).ToString()
                txtMobileNo.Text = dt.Rows(3)(1).ToString()
                txtEmail.Text = dt.Rows(4)(1).ToString()
                txtCustomerType.Text = dt.Rows(5)(1).ToString()
                txtDongleSrNo.Text = dt.Rows(6)(1).ToString()
                txtProduct.Text = dt.Rows(7)(1).ToString()
                txtYear.Text = dt.Rows(8)(1).ToString()
            Catch ex As Exception
                MsgBox("Fail to read C:\eTdsDNF1314\Data.XML")
            End Try

        ElseIf File.Exists("C:\eTDS1314\Data.XML") And strDogSrNo.Length = 8 Then
            Try


                Dim dt As New DataTable("CustInfo")
                dt.Columns.Add("Field")
                dt.Columns.Add("Value")
                dt.ReadXml("C:\eTDS1314\Data.XML")

                txtCustomerName.Text = dt.Rows(0)(1).ToString()
                txtContPer.Text = dt.Rows(1)(1).ToString()
                TxtPhoneNo.Text = dt.Rows(2)(1).ToString()
                txtMobileNo.Text = dt.Rows(3)(1).ToString()
                txtEmail.Text = dt.Rows(4)(1).ToString()
                txtCustomerType.Text = dt.Rows(5)(1).ToString()
                txtDongleSrNo.Text = dt.Rows(6)(1).ToString()
                txtProduct.Text = dt.Rows(7)(1).ToString()
                txtYear.Text = dt.Rows(8)(1).ToString()
            Catch ex As Exception
                MsgBox("Fail to read C:\eTDS1314\Data.XML")
            End Try
        End If

    End Sub

    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim strProduct As String = ""


        If CStr(strDogSrNo.Substring(strDogSrNo.Length - 1, 1)) = "D" Then
            strProduct = "eTdsWizard-DNF"
        ElseIf CStr(strDogSrNo.Substring(strDogSrNo.Length - 1, 1)) = "E" Then
            strProduct = "eTdsWizard"
        Else
            strProduct = "DNF"
        End If

        Dim strProd As String
        strProd = "DNF"

        Process.Start("http://www.ffcs.in/CusRegtion/CustomerRegistration.aspx?donProduct=" & (strProd) & "&pn=" & (strProduct) & "&FY=" & (strDongleFyYr) & "&SRNo=" & (strDogSrNo) & "&Cust=" & Replace(txtCustomerName.Text, "&", "%26") & "&comp=" & Replace(txtContPer.Text, "&", "%26") & "&Tel=" & (TxtPhoneNo.Text) & "&Mob=" & (txtMobileNo.Text) & "&EmId=" & (txtEmail.Text) & "&CT=" & (txtCustomerType.Text) & "&oldSrNo=" & (txtDongleSrNo.Text))

        System.Threading.Thread.Sleep(2000)
        lblRegkey.Visible = True
        txtRegistrationKey.Visible = True
        btnProceed.Visible = True

    End Sub

    Private Sub btnProceed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        Dim StrProduct As String = ""
        Dim strFinYr As String
        Dim strLicenceNo As String

        If txtRegistrationKey.Text.Trim = "" Then
            MessageBox.Show("Customer Registration Key cannot be blank", "eTdsDNF", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If File.Exists(strActivationFileName) Then
            File.Delete(strActivationFileName)
        End If

        Dim objWriter As New StreamWriter(strActivationFileName)
        objWriter.WriteLine(Trim(txtRegistrationKey.Text))
        objWriter.Flush()
        objWriter.Close()

        Dim strLine As String = ""
        Dim objReader As New StreamReader(strActivationFileName)
        strLine = objReader.ReadLine
        objReader.Close()


        If CStr(strDogSrNo.Substring(strDogSrNo.Length - 1, 1)) = "D" Then
            StrProduct = "eTdsWizard-DNF"
        ElseIf CStr(strDogSrNo.Substring(strDogSrNo.Length - 1, 1)) = "E" Then
            StrProduct = "eTdsWizard"
        Else
            StrProduct = "DNF"
        End If

        strLicenceNo = EncryptNew(strDogSrNo)
        strFinYr = EncryptNew(strDongleFyYr)
        StrProduct = EncryptNew(StrProduct)


        Dim strSr As String
        Dim strPrd As String
        Dim strFYr As String

        'strSr = Mid(strLicenceNo, 19, Len(strLicenceNo) - 18) & Mid(strFinYr, 1, 6) & Mid(StrProduct, 1, 6)
        'strPrd = Mid(StrProduct, 1, Len(StrProduct) - 6) & Mid(strLicenceNo, 1, 9)
        'strFYr = Mid(strFinYr, 1, Len(strFinYr) - 6) & Mid(strLicenceNo, 10, 9)

        If strDogSrNo.Length <> "8" Then
            strSr = Mid(strLicenceNo, 11, Len(strLicenceNo) - 10) & Mid(strFinYr, 1, 6) & Mid(StrProduct, 1, 6)
            strPrd = Mid(StrProduct, 1, Len(StrProduct) - 6) & Mid(strLicenceNo, 1, 5)
            strFYr = Mid(strFinYr, 1, Len(strFinYr) - 6) & Mid(strLicenceNo, 6, 5)
        Else
            strSr = Mid(strLicenceNo, 19, Len(strLicenceNo) - 18) & Mid(strFinYr, 1, 6) & Mid(StrProduct, 1, 6)
            strPrd = Mid(StrProduct, 1, Len(StrProduct) - 6) & Mid(strLicenceNo, 1, 9)
            strFYr = Mid(strFinYr, 1, Len(strFinYr) - 6) & Mid(strLicenceNo, 10, 9)
        End If


        blnIsProductRegistered = False
        If strLine <> strFYr & "-" & strPrd & "-" & strSr Then
            MsgBox("Please copy Valid [Customer Registration Key]", vbCritical, "eTdsDNF")
        Else
            blnIsProductRegistered = True
            MsgBox("Registration Successful!", vbInformation, "eTdsDNF")
            Me.Close()
            frmFileSelection.Show()
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        End
    End Sub

    Private Sub frmRegister_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        MsgBox("Please register your product by clicking on [submit] button", vbCritical, "Registration")
    End Sub
End Class