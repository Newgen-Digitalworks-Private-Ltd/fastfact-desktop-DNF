Imports System.Data
Imports System.Data.OleDb
Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Threading
Imports System.Security.Cryptography
Imports System.Xml
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Net.NetworkInformation
Imports eTdsDNF2223.IVRStatusSvc
Imports NewPANValidate

Public Module CommonModule
    Public dtBH As New DataTable("BatchHeader")
    Public dtCD As New DataTable("ChallanDetails")
    Public dtDD As New DataTable("DeducteeDetails")
    Public dtSD As New DataTable("SalaryDetails")
    Public strFormNo As String
    Public dsMain As New DataSet
    Public dr As DataRow
    Public dtTempPAN As DataTable
    Public dtRateMaster As New DataTable("RateMaster")
    'Ver 7.03-REQ817 start
    Public dtNRIRateMaster As New DataTable("NRIRateMaster")
    'Ver 7.03-REQ817 end
    Public strLine As String
    Public TANDeductor As String
    Public strCSIPath As String
    Public strXmlPath As String
    Public Const dcmDelayeDeductionIntrst As Decimal = 0.12
    Public dcmDelayeDepositIntrst As Decimal = 0.18
    Public Const dcmDelayeDepositInt As Decimal = 0.09
    Public strFilePath As String
    Public intCountOfUnMatched As Integer
    Public intCountOfValidPAN As Int64
    Public blnVerifyChallanSkip As Boolean = True
    Public blnVerifyPANSkip As Boolean = True
    Dim WithEvents wb As New WebBrowser
    Public Delegate Sub DoPanValidation()
    Public Delegate Sub DoProcessValidation()
    Public Delegate Sub DoDNFCreation()
    Public blnInternetConnectionFailed As Boolean 'Ver 8.0.0.4 Changes for if internet connection break
    Public blnDNFCreationDelegate As Boolean
    Public blnPANVerification As Boolean
    Public blnPANDelegate As Boolean
    Public blnProcessValidationDelegate As Boolean
    Public blnProcessValidation As Boolean
    Public strPanProcess As String = ""
    Public strPanStatus As String = ""
    Public blnNextFile As Boolean = False
    Public intCntInvalidPan As Int64
    Public actCntInvalidPan As Int64
    Public strDNFExcelFile As String
    Public blnFileFailure As Boolean = False
    Public strProbshortDedAmt As String
    Public strProbshortDepAmt As String
    Public strProbIntPay As String
    Public blnFileClose As Boolean = False
    '--for implementing Dongle adding hte Function below
    Public Declare Function DogRead Lib "Win32dll" (ByVal DogBytes As Integer, ByVal DogAddr As Integer, ByVal DogData As String) As Integer
    Public Declare Function DogWrite Lib "Win32dll" (ByVal DogBytes As Integer, ByVal DogAddr As Integer, ByVal DogData As String) As Integer
    'Ver 1.01-E590 start
    'Public Const ConstStrFYyr As String = "1213"
    'Ver 3.01-REQ303 start
    'Public Const ConstStrFYyr As String = "1314"
    ' Public Const ConstStrFYyr As String = "1415"
    'Ver 6.00-REQ689 start
    'Public Const ConstStrFYyr As String = "1617"
    'Ver 18.00-7.00 start
    'Public Const ConstStrFYyr As String = "1718"
    'Ver 8.00-FastFacts-652->FastFacts-664 start
    'Public Const ConstStrFYyr As String = "1819"
    'Public Const ConstStrFYyr As String = "1920"
    Public Const ConstStrFYyr As String = "2425"
    'Ver 8.00-FastFacts-652->FastFacts-664 end
    'Ver 18.00-7.00 end
    'Ver 6.00-REQ689 end
    'Ver 3.01-REQ303 end
    'Ver 1.01-E590 end
    Public strDogSrNo As String
    Public blnFormClose As Boolean = True
    Public blnDemoVersion As Boolean
    Public strWebAppVersion As String = ""
    Public strDemoMessage As String = ""
    Public strDongleSrNoforBuying As String = ""
    Public strExistingProduct As String = ""
    Public strFinYear As String = ""
    Public DocComplete As Boolean
    Public WithEvents pIE1 As New WebBrowser
    Public blnPANCount As Boolean = False
    'Ver 4.042-QC?? start
    'Dim strValidPANListFilePath As String = Application.StartupPath & "\Interop.VPL.dll"
    'Dim strValidPANListFilePath As String = Application.StartupPath & "\Interop.VPL.sys"
    Dim strValidPANListFilePath As String = Application.StartupPath & "\Interop.VPL.txt"

    Dim strThreadSleepValue As String
    'Ver 4.042-QC?? end
    Public Const dcmDelayeDepositIntrstTill30062010 As Decimal = 0.12
    Public strIgnoredPANListFilePath As String = Application.StartupPath & "\IgnoredPANList.txt"
    Public intIgnoredPANList As Int64

    'Ver 1.01-E585 start
    'Public blnIsProductRegistered As Boolean = False
    Public blnIsProductRegistered As Boolean = True
    Public strActivationFileName As String = Application.StartupPath & "\Interop.eTds.Config"
    'Ver 1.01-E585 End

    Public strValidPANListFileTracesPath As String = Application.StartupPath & "\traces\InvalidPAN.csv"

    'Ver 2.01-REQ233 Start
    Public intFHColumnCount As Integer
    Public intBHColumnCount As Integer
    Public intCDColumnCount As Integer
    Public intDDColumnCount As Integer
    Public intSalaryColumnCount As Integer
    Public strFVUVersion As String
    'Ver 2.01-REQ233 Start

    'Ver 2.02-REQ235 start
    Public dtLateFiling As New DataTable("LateFiling")
    Public strSubmittedDate As String
    Public blnCheckLateFiling As Boolean
    Public strFileQuarter As String
    Public strDeductorType As String
    Public lngFee As Long
    Public dblTotTaxDeducted As Double
    Public dblBalanceLateFilingFee As Double
    'Ver 2.02-REQ235 end
    'Ver 3.01-REQ297 start
    Public strEtdsVal As String
    Public strDongleFyYr As String
    'Ver 3.01-REQ297 end
    'Ver 3.02-REQ311 start
    Public blnIsBookEntry As Boolean
    'Ver 3.02-REQ311 end

    'Ver 3.06-???? start
    Public blnChkVerifyLowerRate As Boolean
    Public blnChkListPANName As Boolean
    'Ver 3.06-???? end
    'Ver 3.0.7-REQ417 start 
    Public blnchkVerifyPAN As Boolean
    Public blnchkLateFiling As Boolean
    Public blnChkDNFIntegrate As Boolean
    Public strLateFilingDate As String
    Public strCSIValidPath As String

    'Ver 3.0.7-REQ417 end 



    '==>Jitendra
    Public blnIsDemoVersion As Boolean = True  'jk
    Public blnIsSoftLicenceActivated As Boolean = True
    Public strCustomerCode As String
    Public strLICXMLFile As String
    '<==Jitendra
    'Ver 18.00-7.00 start
    'Public strStandaloneDNFPcode As String = "E040"  '' Only for 1718
    'Ver 8.00-FastFacts-652->FastFacts-664 start
    'Public strStandaloneDNFPcode As String = "E045"  '' Only for 1819
    'Public strStandaloneDNFPcode As String = "E049"  '' Only for 1920
    'Public strStandaloneDNFPcode As String = "E052"  '' Only for 2021
    'Public strStandaloneDNFPcode As String = "E061"  '' Only for 2122
    Public strStandaloneDNFPcode As String = "E067"  '' Only for 2425
    'Ver 8.00-FastFacts-652->FastFacts-664 end
    'Ver 18.00-7.00 end
    'Ver 4.041 start
    Public strStoreProcessId As String = ""
    Public ExcelFileNm As String = ""
    Private MyBook As Excel.Workbook
    Private MyApp As Excel.Application
    Private MySheet As Excel.Worksheet
    'Ver 4.041 end
    'Ver 4.042-QC?? start
    Public pIEdoc As MSHTML.HTMLDocument
    Public blnCheckNetConnetion As Boolean
    'Ver 4.042-QC?? end
    'Ver 18.00-7.00 start
    'Public Const strProductName As String = "eTdsDNF1718"
    'Ver 8.00-FastFacts-652->FastFacts-664 start
    'Public Const strProductName As String = "eTdsDNF1819"
    'Public Const strProductName As String = "eTdsDNF1920"
    Public Const strProductName As String = "eTdsDNF2425"
    'Ver 8.00-FastFacts-652->FastFacts-664 end
    'Ver 18.00-7.00 end
    Public strMessageboxHeading As String = "eTdsDNF"
    'Ver 7.03-REQ816 start
    Public dsMainDNFValidation As New DataSet
    Public dtTempPANSalary As DataTable
    Dim strPANs() As Object
    Public iswizard As Boolean = False
    'Dim cp As New CheckPAN.VerifyPAN     Ver 8.0.0.3 for rollback in ver 8.0.0.1
    'Ver 7.03-REQ816 end 
    'Ver 7.05-FASTFACTS-584 start
    ' Public blnchkNetConnetion As Boolean
    'Ver 7.05-FASTFACTS-584 end 
    Public Sub RateMaster()
        dtRateMaster.Columns.Add("Section", System.Type.GetType("System.String"))
        dtRateMaster.Columns.Add("DeducteeStatus", System.Type.GetType("System.String"))
        dtRateMaster.Columns.Add("Rate", System.Type.GetType("System.String"))
        'MsgBox(0.3)
    End Sub
    'Ver 7.03-REQ817 start
    Public Sub NRIRateMaster()
        dtNRIRateMaster.Columns.Add("Flag", System.Type.GetType("System.String"))
        dtNRIRateMaster.Columns.Add("Section", System.Type.GetType("System.String"))
        dtNRIRateMaster.Columns.Add("PaymentTypeCode", System.Type.GetType("System.String"))
        dtNRIRateMaster.Columns.Add("Country", System.Type.GetType("System.String"))
        dtNRIRateMaster.Columns.Add("Code", System.Type.GetType("System.String"))
        dtNRIRateMaster.Columns.Add("Others", System.Type.GetType("System.String"))
        dtNRIRateMaster.Columns.Add("Company", System.Type.GetType("System.String"))
    End Sub
    'Ver 7.03-REQ817 end

    Public Sub BatchHeader()
        'Bach Header Table Structure

        dtBH.Columns.Add("Line")
        dtBH.Columns.Add("RT")
        dtBH.Columns.Add("Batch")
        dtBH.Columns.Add("TotChaln")
        dtBH.Columns.Add("Form")
        dtBH.Columns.Add("Tran Type")
        dtBH.Columns.Add("Batch Ind")
        dtBH.Columns.Add("OG RRR")
        dtBH.Columns.Add("Pr RRR")
        dtBH.Columns.Add("RRR No.")
        dtBH.Columns.Add("RRR Date")
        dtBH.Columns.Add("Last Tan ")
        dtBH.Columns.Add("TAN")
        dtBH.Columns.Add("Expected CH")
        dtBH.Columns.Add("PAN")
        dtBH.Columns.Add("Ass. Yr")
        dtBH.Columns.Add("Fin Yr")
        dtBH.Columns.Add("Period")
        dtBH.Columns.Add("Name")
        dtBH.Columns.Add("Branch")
        dtBH.Columns.Add("Add1")
        dtBH.Columns.Add("Add2")
        dtBH.Columns.Add("Add3")
        dtBH.Columns.Add("Add4")
        dtBH.Columns.Add("Add5")
        dtBH.Columns.Add("StCode")
        dtBH.Columns.Add("Pin")
        dtBH.Columns.Add("Email")
        dtBH.Columns.Add("STD")
        dtBH.Columns.Add("Phone")
        dtBH.Columns.Add("Changed")
        dtBH.Columns.Add("Status")
        dtBH.Columns.Add("ResPerson")
        dtBH.Columns.Add("Design")
        dtBH.Columns.Add("RAdd1")
        dtBH.Columns.Add("RAdd2")
        dtBH.Columns.Add("RAdd3")
        dtBH.Columns.Add("RAdd4")
        dtBH.Columns.Add("RAdd5")
        dtBH.Columns.Add("RStCode")
        dtBH.Columns.Add("RPin")
        dtBH.Columns.Add("REmail")
        'Ver 6.00-REQ660 start (it is existing bug. but after discussion with divyesh ePayment Enhancement no is given.)
        'dtBH.Columns.Add("Expected salary")
        dtBH.Columns.Add("Mobile No")
        'Ver 6.00-REQ660 end
        dtBH.Columns.Add("RSTD")
        dtBH.Columns.Add("RPhone")
        dtBH.Columns.Add("TotChlnAmt")
        'Ver 6.00-REQ660 start
        'dtBH.Columns.Add("Mobile No")
        dtBH.Columns.Add("Expected salary")
        'Ver 6.00-REQ660 end
        dtBH.Columns.Add("SalRdCnt")
        dtBH.Columns.Add("TotSTrnAmt")
        dtBH.Columns.Add("AO")
        dtBH.Columns.Add("AO No.")
        dtBH.Columns.Add("Deductor Type")
        dtBH.Columns.Add("State ")
        dtBH.Columns.Add("PAO Code")
        dtBH.Columns.Add("DDO Code")
        dtBH.Columns.Add("Ministry Name")
        dtBH.Columns.Add("Ministry Name (Other)")


        'Ver 2.01-REQ233 Start            
        If strFinYear >= "201314" Then
            dtBH.Columns.Add("TANRegNo")
        End If
        'Ver 2.01-REQ233 End
        dtBH.Columns.Add("Reg.Id")
        dtBH.Columns.Add("PAO No")
        dtBH.Columns.Add("DDO No")

        'Ver 2.01-REQ233 Start
        intBHColumnCount = 61
        If strFinYear >= "201314" Then
            dtBH.Columns.Add("EmpStdCode")
            dtBH.Columns.Add("EmpTelNo")
            dtBH.Columns.Add("EmpEmailId")
            dtBH.Columns.Add("ResPerStdCode")
            dtBH.Columns.Add("ResPerTelNo")
            dtBH.Columns.Add("ResPerEmailId")
            dtBH.Columns.Add("AOCode")
            intBHColumnCount = 69
        End If
        'Ver 2.01-REQ233 End


        dtBH.Columns.Add("RHash")


    End Sub

    Public Sub ChallanDetails()
        'Challan Details Table Structure
        dtCD.Columns.Add("Line", System.Type.GetType("System.UInt32"))
        dtCD.Columns.Add("RT", System.Type.GetType("System.String"))
        dtCD.Columns.Add("Batch", System.Type.GetType("System.UInt16"))
        dtCD.Columns.Add("ChlnSrNo", System.Type.GetType("System.UInt16"))
        dtCD.Columns.Add("CntDedRd", System.Type.GetType("System.UInt32"))
        dtCD.Columns.Add("NILInd", System.Type.GetType("System.String")) 'Char
        dtCD.Columns.Add("UpdateInd", System.Type.GetType("System.String")) 'Char
        dtCD.Columns.Add("DedSrno", System.Type.GetType("System.UInt32"))
        dtCD.Columns.Add("MatchInd", System.Type.GetType("System.String")) 'Char
        dtCD.Columns.Add("Filler1", System.Type.GetType("System.String")) 'Filler
        dtCD.Columns.Add("LtChlnNo", System.Type.GetType("System.String"))
        dtCD.Columns.Add("ChlnNo", System.Type.GetType("System.String"))
        dtCD.Columns.Add("LtTfrNo", System.Type.GetType("System.String"))
        dtCD.Columns.Add("TfrNo", System.Type.GetType("System.String"))
        dtCD.Columns.Add("LtBSR", System.Type.GetType("System.String"))
        dtCD.Columns.Add("BSR", System.Type.GetType("System.String"))
        dtCD.Columns.Add("LtChlnDT", System.Type.GetType("System.String"))
        dtCD.Columns.Add("ChlnDT", System.Type.GetType("System.String"))
        dtCD.Columns.Add("Filler2", System.Type.GetType("System.String")) 'Filler
        dtCD.Columns.Add("Filler3", System.Type.GetType("System.String")) 'Filler
        dtCD.Columns.Add("Section", System.Type.GetType("System.String"))

        'dtCD.Columns.Add("Tds", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("Surcharge", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("EduCess", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("Interest", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("Others", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("Total", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("LtTotPaid", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("TotDedPaid", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("TotDedTds", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("TotDedSur", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("TotDedCess", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("TotDedTax", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("IntAmt", System.Type.GetType("System.UInt32"))
        'dtCD.Columns.Add("Others1", System.Type.GetType("System.UInt32"))

        dtCD.Columns.Add("Tds", GetType(Double))
        dtCD.Columns.Add("Surcharge", GetType(Double))
        dtCD.Columns.Add("EduCess", GetType(Double))
        dtCD.Columns.Add("Interest", GetType(Double))
        dtCD.Columns.Add("Others", GetType(Double))
        dtCD.Columns.Add("Total", GetType(Double))
        dtCD.Columns.Add("LtTotPaid", GetType(Double))
        dtCD.Columns.Add("TotDedPaid", GetType(Double))
        dtCD.Columns.Add("TotDedTds", GetType(Double))
        dtCD.Columns.Add("TotDedSur", GetType(Double))
        dtCD.Columns.Add("TotDedCess", GetType(Double))
        dtCD.Columns.Add("TotDedTax", GetType(Double))
        dtCD.Columns.Add("IntAmt", GetType(Double))
        dtCD.Columns.Add("Others1", GetType(Double))

        dtCD.Columns.Add("ChqNo", System.Type.GetType("System.String"))
        dtCD.Columns.Add("Book Entry", System.Type.GetType("System.String")) 'Char
        dtCD.Columns.Add("Remark", System.Type.GetType("System.String"))
        'Ver 2.01-REQ233 Start
        intCDColumnCount = 39
        If strFinYear >= "201213" Then
            If Val(Mid(strFVUVersion, 5)) >= 3.8 Then
                intCDColumnCount = 40
                'dtCD.Columns.Add("Fees", System.Type.GetType("System.UInt32"))
                dtCD.Columns.Add("Fees", GetType(Double))
            End If

            If strFinYear >= "201314" Then
                intCDColumnCount = 41
                dtCD.Columns.Add("MinorHead", System.Type.GetType("System.String"))
            End If
        End If
        'Ver 2.01-REQ233 End
        dtCD.Columns.Add("RHash", System.Type.GetType("System.String"))

        'New Columns
        dtCD.Columns.Add("IsChallanMatched", System.Type.GetType("System.Boolean"))
        dtCD.Columns.Add("Reason1", System.Type.GetType("System.String"))
        dtCD.Columns("IsChallanMatched").DefaultValue = False



        Dim pk(1) As DataColumn
        pk(0) = dtCD.Columns("ChlnSrNo")
        dtCD.PrimaryKey = pk

    End Sub

    Public Sub DeducteeDetails()
        'Deductee Details Table Structure
        dtDD.Columns.Add("Line", System.Type.GetType("System.UInt32"))
        dtDD.Columns.Add("RT", System.Type.GetType("System.String"))
        dtDD.Columns.Add("Batch", System.Type.GetType("System.UInt32"))
        dtDD.Columns.Add("ChlnSrNo", System.Type.GetType("System.UInt16"))
        dtDD.Columns.Add("Record", System.Type.GetType("System.UInt32"))
        dtDD.Columns.Add("Mode", System.Type.GetType("System.String"))
        dtDD.Columns.Add("SalRdNo", System.Type.GetType("System.String"))
        dtDD.Columns.Add("Dcode", System.Type.GetType("System.Double"))
        dtDD.Columns.Add("LPAN", System.Type.GetType("System.String"))
        dtDD.Columns.Add("PAN", System.Type.GetType("System.String"))
        dtDD.Columns.Add("LRefNo", System.Type.GetType("System.String"))
        dtDD.Columns.Add("RefNo", System.Type.GetType("System.String"))
        dtDD.Columns.Add("Name", System.Type.GetType("System.String"))
        dtDD.Columns.Add("Tds", System.Type.GetType("System.Double"))
        dtDD.Columns.Add("Sur", System.Type.GetType("System.Double"))
        dtDD.Columns.Add("Cess", System.Type.GetType("System.Double"))
        dtDD.Columns.Add("TotTds", System.Type.GetType("System.Double"))  'Total TDS Deducted
        dtDD.Columns.Add("LTotTds", System.Type.GetType("System.Double"))
        dtDD.Columns.Add("Deposit", System.Type.GetType("System.Double")) 'Total TDS Deposited
        dtDD.Columns.Add("LDeposit", System.Type.GetType("System.Double"))
        dtDD.Columns.Add("PurValue", System.Type.GetType("System.Double"))
        dtDD.Columns.Add("DocAmt", System.Type.GetType("System.Double"))
        dtDD.Columns.Add("DatePayment", System.Type.GetType("System.String")) 'Date of Payment Credit
        dtDD.Columns.Add("DateDeducted", System.Type.GetType("System.String")) ' Date of Deduction
        dtDD.Columns.Add("PaidON", System.Type.GetType("System.String"))
        dtDD.Columns.Add("Rate", System.Type.GetType("System.Single"))
        dtDD.Columns.Add("GrossInd", System.Type.GetType("System.String")) 'Char
        dtDD.Columns.Add("BookEntry", System.Type.GetType("System.String")) 'Char
        dtDD.Columns.Add("Furnish", System.Type.GetType("System.String"))
        dtDD.Columns.Add("Remark1", System.Type.GetType("System.String"))
        dtDD.Columns.Add("Remark2", System.Type.GetType("System.String"))
        dtDD.Columns.Add("Remark3", System.Type.GetType("System.String"))

        'Ver 2.01-REQ233 Start  
        intDDColumnCount = 33
        If strFinYear >= "201314" Then
            dtDD.Columns.Add("Section", System.Type.GetType("System.String"))
            dtDD.Columns.Add("AOCertNo", System.Type.GetType("System.String"))
            dtDD.Columns.Add("DTTA", System.Type.GetType("System.String"))
            dtDD.Columns.Add("Remitance", System.Type.GetType("System.String"))
            dtDD.Columns.Add("15CA", System.Type.GetType("System.String"))
            dtDD.Columns.Add("CountryCode", System.Type.GetType("System.String"))
            intDDColumnCount = 39
        End If
        'Ver 2.01-REQ233 End
        'Ver 6.04-QC1239 start the following code commented since change in datatable structure short deduction not working
        'Ver 6.03-QC1205 start 
        'If strFinYear >= "201617" Then
        '    dtDD.Columns.Add("DeducteeEmailID", System.Type.GetType("System.String"))
        '    dtDD.Columns.Add("DeducteeContactNo", System.Type.GetType("System.Double"))
        '    dtDD.Columns.Add("DeducteeAddressOfResidence", System.Type.GetType("System.String"))
        '    dtDD.Columns.Add("TaxIdentificationNo", System.Type.GetType("System.String"))
        '    intDDColumnCount = 43
        'End If
        'Ver 6.03-QC1205 end
        'Bug no. QC1239 is reverted start
        'If strFinYear >= "201617" And strFormNo = "27Q" Then
        If strFinYear >= "201617" Then
            'Bug no. QC1239 is reverted end
            dtDD.Columns.Add("DeducteeEmailID", System.Type.GetType("System.String"))
            dtDD.Columns.Add("DeducteeContactNo", System.Type.GetType("System.Double"))
            dtDD.Columns.Add("DeducteeAddressOfResidence", System.Type.GetType("System.String"))
            dtDD.Columns.Add("TaxIdentificationNo", System.Type.GetType("System.String"))

            intDDColumnCount = 43
        End If
        'Ver 6.04-QC1239 end
        dtDD.Columns.Add("RHash", System.Type.GetType("System.String"))
        'Challan Details             
        dtDD.Columns.Add("ChlnDT", System.Type.GetType("System.String"))

        'Ver 2.01-REQ233 Start        
        If strFinYear < "201314" Then
            'Ver 2.01-REQ233 End
            dtDD.Columns.Add("Section", System.Type.GetType("System.String"))
            'Ver 2.01-REQ233 Start      
        End If
        'Ver 2.01-REQ233 End

        'New Columns
        dtDD.Columns.Add("PANStatus", System.Type.GetType("System.Char"))
        dtDD.Columns.Add("PrescribedRate", System.Type.GetType("System.Single")).DefaultValue = 0
        dtDD.Columns.Add("AmountDeductible", System.Type.GetType("System.Double"))
        dtDD.Columns.Add("ShortDedAmount", System.Type.GetType("System.Double"))
        dtDD.Columns.Add("DelayedDedMonth", System.Type.GetType("System.UInt16"))
        dtDD.Columns.Add("DelayedDedAmt", System.Type.GetType("System.Single"))
        dtDD.Columns.Add("DelayedDepMonth", System.Type.GetType("System.UInt16"))
        dtDD.Columns.Add("DelayedDepAmt", System.Type.GetType("System.Single"))
        dtDD.Columns.Add("DeducteeStatus", System.Type.GetType("System.Char"))
        dtDD.Columns.Add("Reason1", System.Type.GetType("System.String"))
        dtDD.Columns.Add("Reason2", System.Type.GetType("System.String"))
        dtDD.Columns.Add("Reason3", System.Type.GetType("System.String"))
        dtDD.Columns.Add("IsPANValidationReq", System.Type.GetType("System.Boolean"))
        dtDD.Columns.Add("IntShortDed", System.Type.GetType("System.UInt32"))
        dtDD.Columns.Add("TotInterest", System.Type.GetType("System.UInt32"))
        dtDD.Columns.Add("ChallanDueDate", System.Type.GetType("System.String"))


        'DefaultValues
        dtDD.Columns("PANStatus").DefaultValue = "V"
        dtDD.Columns("IsPANValidationReq").DefaultValue = True
        'Ver 5.03-REQ650 start
        'If strFinYear >= "201617" And strFormNo = "27Q" Then
        '    dtDD.Columns.Add("DeducteeEmail", System.Type.GetType("System.String"))
        '    dtDD.Columns.Add("DeducteeCount", System.Type.GetType("System.String"))
        '    dtDD.Columns.Add("DeducteeAddress", System.Type.GetType("System.String"))
        '    dtDD.Columns.Add("DeducteeTIN", System.Type.GetType("System.String"))
        '    intDDColumnCount = 62
        'End If
        'Ver 5.03-REQ650 end

        Dim pk(2) As DataColumn
        pk(0) = dtDD.Columns("ChlnSrNo")
        pk(1) = dtDD.Columns("Record")
        dtDD.PrimaryKey = pk
    End Sub
    Public Sub SalaryDetails()

        dtSD.Columns.Add("Line", System.Type.GetType("System.UInt32"))
        dtSD.Columns.Add("Rec Type", System.Type.GetType("System.String"))
        dtSD.Columns.Add("Batch", System.Type.GetType("System.UInt32"))
        dtSD.Columns.Add("SalSrNo", System.Type.GetType("System.UInt32"))
        dtSD.Columns.Add("Mode", System.Type.GetType("System.Char"))
        dtSD.Columns.Add("Filler", System.Type.GetType("System.String"))
        dtSD.Columns.Add("PAN", System.Type.GetType("System.String"))
        dtSD.Columns.Add("RefNo", System.Type.GetType("System.String"))
        dtSD.Columns.Add("Name", System.Type.GetType("System.String"))
        dtSD.Columns.Add("Category", System.Type.GetType("System.Char"))
        dtSD.Columns.Add("FromDt", System.Type.GetType("System.String"))
        dtSD.Columns.Add("ToDt", System.Type.GetType("System.String"))
        dtSD.Columns.Add("TotSalAmt", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("Filler1", System.Type.GetType("System.String"))
        dtSD.Columns.Add("Cnt 16", System.Type.GetType("System.UInt32"))
        dtSD.Columns.Add("16(ii)", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("16(iii)", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("Tot 16", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("TotSalAmt-Tot 16", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("OthrInc", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("GrossInc", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("LGrossInc", System.Type.GetType("System.String"))
        dtSD.Columns.Add("Cnt VIA", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("80CCE", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("OTHERS", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("Gross VIA", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("TotIncome", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("GrossTax", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("Surcharge", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("Educess", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("Relief89", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("NetTax", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("FYearTax", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("Shortfall/Excess", System.Type.GetType("System.Double"))
        dtSD.Columns.Add("Remark1", System.Type.GetType("System.String"))
        dtSD.Columns.Add("Remark2", System.Type.GetType("System.String"))
        dtSD.Columns.Add("Remark3", System.Type.GetType("System.String"))

        'Ver 2.01-REQ233 Start      
        intSalaryColumnCount = 34
        If strFinYear >= "201314" Then
            dtSD.Columns.Add("Section", System.Type.GetType("System.String"))
            dtSD.Columns.Add("AOCertNo", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Filler2", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Filler3", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Filler4", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Filler5", System.Type.GetType("System.String"))
            'Ver 3.02-QC530 start
            'intSalaryColumnCount = 44
            intSalaryColumnCount = 43
            'Ver 3.02-QC530 end
        End If
        'Ver 2.01-REQ233 End
        'Ver 5.03-REQ650 start
        If strFinYear >= "201617" Then
            dtSD.Columns.Add("IsRentExceed1L", System.Type.GetType("System.String"))
            dtSD.Columns.Add("CountLandlordPan", System.Type.GetType("System.String"))
            dtSD.Columns.Add("OwnerPAN1", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Landlord1", System.Type.GetType("System.String"))
            dtSD.Columns.Add("OwnerPAN2", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Landlord2", System.Type.GetType("System.String"))
            dtSD.Columns.Add("OwnerPAN3", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Landlord3", System.Type.GetType("System.String"))
            dtSD.Columns.Add("OwnerPAN4", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Landlord4", System.Type.GetType("System.String"))
            dtSD.Columns.Add("IsInterestLoan", System.Type.GetType("System.String"))
            dtSD.Columns.Add("CountLenderPan", System.Type.GetType("System.String"))
            dtSD.Columns.Add("LenderPAN1", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Lender1", System.Type.GetType("System.String"))
            dtSD.Columns.Add("LenderPAN2", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Lender2", System.Type.GetType("System.String"))
            dtSD.Columns.Add("LenderPAN3", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Lender3", System.Type.GetType("System.String"))
            dtSD.Columns.Add("LenderPAN4", System.Type.GetType("System.String"))
            dtSD.Columns.Add("Lender4", System.Type.GetType("System.String"))
            dtSD.Columns.Add("IsSuperAnnuation", System.Type.GetType("System.String"))
            dtSD.Columns.Add("SuperAnnName", System.Type.GetType("System.String"))
            dtSD.Columns.Add("DateFromSuper", System.Type.GetType("System.String"))
            dtSD.Columns.Add("DateToSuper", System.Type.GetType("System.String"))
            dtSD.Columns.Add("SuperAnnCont", System.Type.GetType("System.String"))
            dtSD.Columns.Add("SuperTdsRate", System.Type.GetType("System.String"))
            dtSD.Columns.Add("SuperTdsDed", System.Type.GetType("System.String"))
            dtSD.Columns.Add("GrossTIWithSuper", System.Type.GetType("System.String"))
            intSalaryColumnCount = 71
        End If
        If strFinYear >= "202021" Then
            dtSD.Columns.Add("Taxation", System.Type.GetType("System.String"))
            intSalaryColumnCount = 72
        End If
        dtSD.Columns.Add("RHash", System.Type.GetType("System.String"))

        Dim pk(1) As DataColumn
        pk(0) = dtSD.Columns("SalSrNo")
        dtSD.PrimaryKey = pk
    End Sub
    Public Sub GetData()
        Dim fs As FileStream
        Dim strRd As StreamReader
        Dim mCode As String = ""

        Try
            fs = New FileStream(strFilePath, FileMode.Open, FileAccess.Read)
            strRd = New StreamReader(fs)

            Dim values() As String
            'For Each strLine In File.ReadLines(strFilePath)
            '    'strLine = strRd.ReadLine
            '    values = strLine.Split("^")
            '    If values.Length > 1 Then
            '        mCode = values(1)
            '    Else
            '        Exit For
            '    End If

            '    If mCode = "FH" Then
            '        If values(3) <> "R" Then
            '            MessageBox.Show("Please Select Regular Statements only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            blnFileClose = True
            '            GoTo contn
            '        End If

            '        'Ver 2.02-REQ235 start
            '        If Len(values(4)) = 8 Then
            '            strSubmittedDate = Mid(values(4), 1, 2) & "/" & Mid(values(4), 3, 2) & "/" & Mid(values(4), 5, 4)
            '        End If

            '        'Ver 2.02-REQ235 end
            '    ElseIf mCode = "BH" Then
            '        strFormNo = values(4)

            '        If values(4) <> "26Q" And values(4) <> "24Q" And values(4) <> "27Q" And values(4) <> "27EQ" Then
            '            MessageBox.Show("Please Select 24Q,26Q,27Q,27EQ File only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            blnFileClose = True
            '            GoTo contn
            '            'Ver 1.01-E590 start
            '            ' ElseIf values(16) < "201011" Or values(16) > "201213" Then 'Fin Year
            '            'MessageBox.Show("Please select file of financial year 2010-2011, 2011-2012 or 2012-2013 only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            'Ver 3.01-REQ303 start
            '            'ElseIf values(16) < "201011" Or values(16) > "201314" Then 'Fin Year
            '            'MessageBox.Show("Please select file of financial year 2010-2011, 2011-2012, 2012-2013 or 2013-2014 only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            'ElseIf values(16) < "201011" Or values(16) > "201415" Then 'Fin Year
            '            'Ver 5.2-REQ590 start 
            '            'ElseIf values(16) < "201011" Or values(16) > "201516" Then 'Fin Year
            '            '    MessageBox.Show("Please select file of financial year 2010-2011, 2011-2012, 2012-2013 or 2013-2014 or 2014-2015 or 2015-2016 only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            'Ver 6.00-REQ689 start
            '            'ElseIf values(16) < "201011" Or values(16) > "201617" Then 'Fin Year
            '            '   MessageBox.Show("Please select file of financial year 2010-2011, 2011-2012, 2012-2013 or 2013-2014 or 2014-2015 or 2015-2016 or 2016-2017 only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)    
            '            'Ver 7.03-REQ797 start
            '            'ElseIf values(16) < "201011" Or values(16) > "201718" Then 'Fin Year

            '            'Ver 8.01 - 129703 start
            '            'ElseIf values(16) < "201011" Or values(16) > "201819" Then 'Fin Year
            '            'ElseIf values(16) < "201011" Or values(16) > "201920" Then 'Fin Year
            '        ElseIf values(16) < "201011" Or values(16) > "202122" Then 'Fin Year
            '            'Ver 8.01 - 129703 end

            '            'Ver 7.03-REQ797 end
            '            'Ver 6.00-REQ689 end
            '            MessageBox.Show("Please select file of financial year 2010-2011, 2011-2012, 2012-2013 or 2013-2014 or 2014-2015 or 2015-2016 or 2016-2017 or 2017-2018 or 2018-2019 or 2019-20 or 2020-21 or 2021-22 only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information) 'or 2019-20
            '            'Ver 5.2-REQ590 end 
            '            'Ver 3.01-REQ303 end
            '            'Ver 1.01-E590 End
            '            blnFileClose = True
            '            GoTo contn
            '        End If
            '        strFinYear = values(16)
            '        'Ver 2.01-REQ233 Start
            '        'FillData(dtBH, values, 61)
            '        If blnNextFile = False Then
            '            BatchHeader()
            '            ChallanDetails()
            '            DeducteeDetails()
            '            SalaryDetails()
            '        End If
            '        FillData(dtBH, values, intBHColumnCount)
            '        'Ver 2.01-REQ233 Start
            '        TANDeductor = dtBH.Rows(0)(12)

            '        'Ver 2.02-REQ235 start
            '        'Deductor Type
            '        strDeductorType = dtBH.Rows(0)("status")
            '        'Ver 2.02-REQ235 end

            '    ElseIf mCode = "CD" Then
            '        'Ver 2.01-REQ233 Start
            '        'FillData(dtCD, values, 39)
            '        FillData(dtCD, values, intCDColumnCount)
            '        'Ver 2.01-REQ233 End

            '        'Ver 2.02-REQ235 start
            '        If strFinYear >= "201213" Then
            '            If values(38) <> "" Then
            '                'Fee
            '                lngFee = lngFee + Convert.ToInt64(Val(values(38)))
            '            End If
            '        End If


            '        If values(32) <> "" Then
            '            'TotDedTax
            '            dblTotTaxDeducted = dblTotTaxDeducted + Convert.ToDouble(values(32))
            '        End If
            '        'Ver 2.02-REQ235 end

            '        'Ver 3.02-REQ311 start
            '        If values(36) = "Y" Then
            '            'Is book Entry
            '            blnIsBookEntry = True
            '        End If
            '        'Ver 3.02-REQ311 end
            '    ElseIf mCode = "DD" Then
            '        'Ver 2.01-REQ233 Start
            '        'FillData(dtDD, values, 33)
            '        FillData(dtDD, values, intDDColumnCount)
            '        If dtBH.Rows(0).Item("Form").ToString = "27Q" Then
            '            For Each dr As DataRow In dtDD.Rows
            '                If dr("DTTA") = "B" Then dr("Remark1") = ""
            '            Next
            '        End If
            '        'Ver 2.01-REQ233 End
            '    ElseIf mCode = "SD" Then
            '        'Ver 2.01-REQ233 Start
            '        'FillDataForSalary(dtSD, values, 34)
            '        FillDataForSalary(dtSD, values, intSalaryColumnCount)
            '        'Ver 2.01-REQ233 End
            '    ElseIf mCode = "S16" Then
            '        Dim dtTempView As New DataView(dtSD)
            '        dtTempView.RowFilter = "SalSrNo = '" & values(3) & "'"
            '        If values(5) = "16(ii)" Then
            '            dtTempView.Item(0)("16(ii)") = values(6)
            '        ElseIf values(5) = "16(iii)" Then
            '            dtTempView.Item(0)("16(iii)") = values(6)
            '        End If
            '    ElseIf mCode = "C6A" Then
            '        Dim dtTempView As New DataView(dtSD)
            '        dtTempView.RowFilter = "SalSrNo = '" & values(3) & "'"
            '        If values(5) = "80CCE" Then
            '            dtTempView.Item(0)("80CCE") = values(6)
            '        ElseIf values(5) = "OTHERS" Then
            '            dtTempView.Item(0)("OTHERS") = values(6)
            '        End If
            '    End If

            '    Array.Resize(values, 0)
            'Next

            Do Until strRd.EndOfStream = True
                strLine = strRd.ReadLine
                values = strLine.Split("^")
                'Ver 2.01-REQ233 start
                'If values.Length > 0 Then
                If values.Length > 1 Then
                    'Ver 2.01-REQ233 End
                    mCode = values(1)
                    'Ver 2.01-REQ233 start
                Else
                    Exit Do
                    'Ver 2.01-REQ233 End
                End If

                If mCode = "FH" Then
                    If values(3) <> "R" Then
                        MessageBox.Show("Please Select Regular Statements only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        blnFileClose = True
                        GoTo contn
                    End If

                    'Ver 2.02-REQ235 start
                    If Len(values(4)) = 8 Then
                        strSubmittedDate = Mid(values(4), 1, 2) & "/" & Mid(values(4), 3, 2) & "/" & Mid(values(4), 5, 4)
                    End If

                    'Ver 2.02-REQ235 end
                ElseIf mCode = "BH" Then
                    strFormNo = values(4)

                    If values(4) <> "26Q" And values(4) <> "24Q" And values(4) <> "27Q" And values(4) <> "27EQ" Then
                        MessageBox.Show("Please Select 24Q,26Q,27Q,27EQ File only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        blnFileClose = True
                        GoTo contn
                        'Ver 1.01-E590 start
                        ' ElseIf values(16) < "201011" Or values(16) > "201213" Then 'Fin Year
                        'MessageBox.Show("Please select file of financial year 2010-2011, 2011-2012 or 2012-2013 only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Ver 3.01-REQ303 start
                        'ElseIf values(16) < "201011" Or values(16) > "201314" Then 'Fin Year
                        'MessageBox.Show("Please select file of financial year 2010-2011, 2011-2012, 2012-2013 or 2013-2014 only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'ElseIf values(16) < "201011" Or values(16) > "201415" Then 'Fin Year
                        'Ver 5.2-REQ590 start 
                        'ElseIf values(16) < "201011" Or values(16) > "201516" Then 'Fin Year
                        '    MessageBox.Show("Please select file of financial year 2010-2011, 2011-2012, 2012-2013 or 2013-2014 or 2014-2015 or 2015-2016 only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Ver 6.00-REQ689 start
                        'ElseIf values(16) < "201011" Or values(16) > "201617" Then 'Fin Year
                        '   MessageBox.Show("Please select file of financial year 2010-2011, 2011-2012, 2012-2013 or 2013-2014 or 2014-2015 or 2015-2016 or 2016-2017 only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information)    
                        'Ver 7.03-REQ797 start
                        'ElseIf values(16) < "201011" Or values(16) > "201718" Then 'Fin Year

                        'Ver 8.01 - 129703 start
                        'ElseIf values(16) < "201011" Or values(16) > "201819" Then 'Fin Year
                        'ElseIf values(16) < "201011" Or values(16) > "201920" Then 'Fin Year
                    ElseIf values(16) < "201011" Or values(16) > "202425" Then 'Fin Year
                        'Ver 8.01 - 129703 end
                        'Ver 7.03-REQ797 end
                        'Ver 6.00-REQ689 end
                        MessageBox.Show("Please select file of financial year 2010-2011, 2011-2012, 2012-2013 or 2013-2014 or 2014-2015 or 2015-2016 or 2016-2017 or 2017-2018 or 2018-2019 or 2019-20 or 2020-21 or 2021-22 or 2022-23 or 2023-24 or 2024-25 only.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Information) 'or 2019-20
                        'Ver 5.2-REQ590 end 
                        'Ver 3.01-REQ303 end
                        'Ver 1.01-E590 End
                        blnFileClose = True
                        GoTo contn
                    End If
                    strFinYear = values(16)
                    'Ver 2.01-REQ233 Start
                    'FillData(dtBH, values, 61)
                    If blnNextFile = False Then
                        BatchHeader()
                        ChallanDetails()
                        DeducteeDetails()
                        SalaryDetails()
                    End If
                    FillData(dtBH, values, intBHColumnCount)
                    'Ver 2.01-REQ233 Start
                    TANDeductor = dtBH.Rows(0)(12)

                    'Ver 2.02-REQ235 start
                    'Deductor Type
                    strDeductorType = dtBH.Rows(0)("status")
                    'Ver 2.02-REQ235 end

                ElseIf mCode = "CD" Then
                    'Ver 2.01-REQ233 Start
                    'FillData(dtCD, values, 39)
                    FillData(dtCD, values, intCDColumnCount)
                    'Ver 2.01-REQ233 End

                    'Ver 2.02-REQ235 start
                    If strFinYear >= "201213" Then
                        If values(38) <> "" Then
                            'Fee
                            lngFee = lngFee + Convert.ToInt64(Val(values(38)))
                        End If
                    End If


                    If values(32) <> "" Then
                        'TotDedTax
                        dblTotTaxDeducted = dblTotTaxDeducted + Convert.ToDouble(values(32))
                    End If
                    'Ver 2.02-REQ235 end

                    'Ver 3.02-REQ311 start
                    If values(36) = "Y" Then
                        'Is book Entry
                        blnIsBookEntry = True
                    End If
                    'Ver 3.02-REQ311 end
                ElseIf mCode = "DD" Then
                    'Ver 2.01-REQ233 Start
                    'FillData(dtDD, values, 33)
                    FillData(dtDD, values, intDDColumnCount)
                    If dtBH.Rows(0).Item("Form").ToString = "27Q" Then
                        For Each dr As DataRow In dtDD.Rows
                            If dr("DTTA") = "B" Then dr("Remark1") = ""
                        Next
                    End If
                    'Ver 2.01-REQ233 End
                ElseIf mCode = "SD" Then
                    'Ver 2.01-REQ233 Start
                    'FillDataForSalary(dtSD, values, 34)
                    FillDataForSalary(dtSD, values, intSalaryColumnCount)
                    'Ver 2.01-REQ233 End
                ElseIf mCode = "S16" Then
                    Dim dtTempView As New DataView(dtSD)
                    dtTempView.RowFilter = "SalSrNo = '" & values(3) & "'"
                    If values(5) = "16(ii)" Then
                        dtTempView.Item(0)("16(ii)") = values(6)
                    ElseIf values(5) = "16(iii)" Then
                        dtTempView.Item(0)("16(iii)") = values(6)
                    End If
                ElseIf mCode = "C6A" Then
                    Dim dtTempView As New DataView(dtSD)
                    dtTempView.RowFilter = "SalSrNo = '" & values(3) & "'"
                    If values(5) = "80CCE" Then
                        dtTempView.Item(0)("80CCE") = values(6)
                    ElseIf values(5) = "OTHERS" Then
                        dtTempView.Item(0)("OTHERS") = values(6)
                    End If
                End If

                Array.Resize(values, 0)
            Loop

contn:
            fs.Close()
            strRd.Dispose()
            fs.Dispose()

        Catch ex As Exception
            fs.Close()
            strRd.Dispose()
            fs.Dispose()
            MessageBox.Show(ex.Message, "DNF")

        End Try
    End Sub

    Public Sub FillData(ByRef dtTemp As DataTable, ByRef value() As String, ByVal ColCount As Integer)
        Try
            dr = dtTemp.NewRow
            For i = 0 To ColCount - 1
                If value(i).ToString = "" Then   ' For Empty String according to data type assign 0 OR blank

                    If (dtTemp.Columns(i).DataType.ToString = "System.Decimal") Or (dtTemp.Columns(i).DataType.ToString = "System.Int32") Or (dtTemp.Columns(i).DataType.ToString = "System.Int64") Then
                        dr(i) = 0
                    ElseIf (dtTemp.Columns(i).DataType.ToString = "System.Char") Or (dtTemp.Columns(i).DataType.ToString = "System.String") Then
                        If dtTemp.Columns(i).Caption = "Dcode" And strFormNo = "24Q" Then
                            dr(i) = "2"
                        Else
                            dr(i) = ""
                        End If
                    End If
                Else    ' For Uint DataType Convert String Type data into Integer before assigning to datarow
                    If (dtTemp.Columns(i).DataType.ToString = "System.UInt32") Or (dtTemp.Columns(i).DataType.ToString = "System.UInt16") Then
                        dr(i) = CInt(value(i))
                    Else
                        dr(i) = value(i)
                    End If
                End If

                If dtTemp.TableName = "DeducteeDetails" Then
                    If i = 23 Then 'Date on tax Deducted
                        If value(i) <> "" Then
                            dr("ChallanDueDate") = CalculateDueDate(value(i)) 'Calsulate Challan due Date
                        End If
                    ElseIf i = 9 Then  ' PAN
                        dr("DeducteeStatus") = Mid(value(i), 4, 1)  ' 4th Character of PAN
                    End If
                End If
            Next i

            'Fields other than text File...
            If dtTemp.TableName = "DeducteeDetails" Then
                Dim drTemp() As DataRow
                drTemp = dtCD.Select("ChlnSrNo ='" & dr("ChlnSrNo") & "'")   'Filtering Challan Data On basis of challan Srno to which Deductee is belonging
                dr("ChlnDT") = drTemp(0).Item("ChlnDT").ToString             'From Filtered Data Assing ChallanDate to Deductee

                'Ver 2.01-REQ233 Start
                If strFinYear < "201314" Then
                    dr("Section") = drTemp(0).Item("Section").ToString           'From Filtered Data Assing Section to Deductee
                End If
                'Ver 2.01-REQ233 End
                Array.Resize(drTemp, 0)

                If (dr("PAN").ToString = "PANAPPLIED") Or (dr("PAN").ToString = "PANINVALID") Or (dr("PAN").ToString = "PANNOTAVBL") Then
                    dr("IsPANValidationReq") = False
                    dr("PANStatus") = "N"
                    dr("DeducteeStatus") = "Z" '4th Character Updated to 'Z' For Invalid PAN
                    'Ver 6.04-QC1239 start
                    ''Ver 6.03-QC1205 start
                    'If strFinYear >= "201617" Then
                    '    If strFormNo = "27Q" And dr("Remark1").ToString = "C" And Val(dr("Rate").ToString) < 20 And dr("DeducteeEmailID").ToString.Trim <> "" And Val(dr("DeducteeContactNo").ToString) > 0 And dr("DeducteeAddressOfResidence").ToString.Trim <> "" And dr("TaxIdentificationNo").ToString.Trim <> "" And (dr("Remitance").ToString = "27" Or dr("Remitance").ToString = "49" Or dr("Remitance").ToString = "21" Or dr("Remitance").ToString = "52" Or dr("Remitance").ToString = "31") Then
                    '        dr("PANStatus") = "X" ' put x for applying lower rate for invalid pan when above conditions satisfy
                    '    End If
                    'End If
                    ''Ver 6.03-QC1205 end

                    If strFinYear >= "201617" And strFormNo = "27Q" Then
                        If dr("Remark1").ToString = "C" And Val(dr("Rate").ToString) < 20 And dr("DeducteeEmailID").ToString.Trim <> "" And Val(dr("DeducteeContactNo").ToString) > 0 And dr("DeducteeAddressOfResidence").ToString.Trim <> "" And dr("TaxIdentificationNo").ToString.Trim <> "" And (dr("Remitance").ToString = "27" Or dr("Remitance").ToString = "49" Or dr("Remitance").ToString = "21" Or dr("Remitance").ToString = "52" Or dr("Remitance").ToString = "31") Then
                            dr("PANStatus") = "X" ' put x for applying lower rate for invalida pan when above conditions satisfy
                        End If
                    End If
                    'Ver 6.04-QC1239 end
                End If

                'DatePayment, DateDeducted 
                If dr("DateDeducted") = "" Then
                    dr("DelayedDedMonth") = 0
                    dr("DelayedDepMonth") = 0
                Else
                    dr("DelayedDedMonth") = MonthDifference(dr("DatePayment"), dr("DateDeducted"))

                    dr("DelayedDepMonth") = MonthDifference(dr("DateDeducted"), dr("ChlnDT"), dr("ChallanDueDate"))
                End If


                'TotTds,Deposit
                If dr("DelayedDedMonth") = 0 Then
                    dr("DelayedDedAmt") = 0
                End If

                If dr("DelayedDepMonth") = 0 Then
                    dr("DelayedDepAmt") = 0
                End If
                If dr("DelayedDedMonth") = 0 And dr("DelayedDepMonth") = 0 Then
                    dr("TotInterest") = 0
                End If
            End If

            dtTemp.Rows.Add(dr)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")
        End Try
    End Sub

    Public Function ValidateFileWithJAR() As Boolean
        Dim fs As New FileStream(strFilePath, FileMode.Open, FileAccess.Read)
        Dim values() As String
        Dim valuesFvu() As String
        Dim strData As String
        Dim cnt As Integer
        Dim flag As Boolean
        Dim strRd As New StreamReader(fs)
        flag = False

        If File.Exists(Replace(strFilePath, ".txt", ".fvu", , , Microsoft.VisualBasic.CompareMethod.Text)) = False Then
            Return False
        End If

        fs = New FileStream(Replace(strFilePath, ".txt", ".fvu", , , Microsoft.VisualBasic.CompareMethod.Text), FileMode.Open, FileAccess.Read)
        Dim strRdfvu As New StreamReader(fs)
        For j = 0 To 1
            strData = strRd.ReadLine
            values = strData.Split("^")

            strData = strRdfvu.ReadLine
            valuesFvu = strData.Split("^")
            cnt = 0

            If values.Length <> valuesFvu.Length Then
                flag = True
                Exit For
            End If

            If values(1) = "FH" And valuesFvu(1) = "FH" Then
                cnt = 9
                'Ver 2.01-REQ233 Start
                strFVUVersion = valuesFvu(11)
                'Ver 2.01-REQ233 End

            ElseIf values(1) = "BH" And valuesFvu(1) = "BH" Then
                cnt = 17
            Else
                flag = True
                Exit For
            End If

            For i = 0 To cnt
                If values(i) <> valuesFvu(i) Then
                    flag = True
                    Exit For
                End If
            Next
            Array.Resize(values, 0)
            Array.Resize(valuesFvu, 0)
            If flag = True Then
                Exit For
            End If

        Next

        fs.Close()
        strRd.Close()
        strRdfvu.Close()
        fs.Dispose()
        strRd.Dispose()
        strRdfvu.Dispose()
        Array.Resize(values, 0)
        Array.Resize(valuesFvu, 0)

        If flag = True Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function CalculateDueDate(ByVal strDedDate As String) As String

        Dim strDD As String
        Dim strMM As String
        Dim strYY As String
        Dim strDate As String
        strDD = Mid(strDedDate, 1, 2)
        strMM = Mid(strDedDate, 3, 2)
        strYY = Mid(strDedDate, 5, 4)

        'Ver 2.02-QC354 start
        'If strMM = "03" Then
        If strMM = "03" And strFormNo <> "27EQ" Then
            'Ver 2.02-QC354 end
            strDate = "3004" & strYY
        Else
            If strMM = "12" Then
                strDate = "0701" & (CInt(strYY) + 1).ToString
                'Ver 3.06-REQ-388 start
            ElseIf strMM = "09" And strYY = "2014" Then
                strDate = "10" & (CInt(strMM) + 1).ToString & strYY
                'Ver 3.06-REQ-388 end
                'Changes had done for 2020 March Change due Date Start
            ElseIf (strMM = "03" And strFinYear >= "201920") And strFormNo = "27EQ" Then
                strDate = "3004" & strYY
                'Changes had done for 2020 March Change due Date End
            Else
                If CInt(strMM) > 8 Then
                    strDate = "07" & (CInt(strMM) + 1).ToString & strYY
                Else
                    strDate = "070" & (CInt(strMM) + 1).ToString & strYY  ' For Months less october(10) CInt returns single digit.
                End If

            End If


        End If

        strDD = Mid(strDate, 1, 2)
        strMM = Mid(strDate, 3, 2)
        strYY = Mid(strDate, 5, 4)
        Dim strD As Date
        strD = Convert.ToDateTime(strYY & "/" & strMM & "/" & strDD)

        If strD.DayOfWeek.ToString = "Sunday" Then
            If strDD = "07" Then
                strDate = "08" & strMM & strYY
            ElseIf strDD = "30" Then
                strDate = "0105" & strYY
            End If
        End If


        Return strDate
    End Function

    Public Function MonthDifference(ByVal FirstDate As String, ByVal SecondDate As String, Optional ByVal strChlDueDate As String = "") As Integer
        Dim newdate1 As Date
        Dim newdate2 As Date
        Dim newdate3 As Date
        Dim intdifference As Integer

        If FirstDate = SecondDate Then
            Return 0
        End If


        newdate1 = Mid(FirstDate, 1, 2) & "/" & Mid(FirstDate, 3, 2) & "/" & Mid(FirstDate, 5, 4) ' Date of Deducted
        newdate2 = Mid(SecondDate, 1, 2) & "/" & Mid(SecondDate, 3, 2) & "/" & Mid(SecondDate, 5, 4) 'Challan Date
        If strChlDueDate <> "" Then
            newdate3 = Mid(strChlDueDate, 1, 2) & "/" & Mid(strChlDueDate, 3, 2) & "/" & Mid(strChlDueDate, 5, 4) 'Due Date
            If newdate2 > newdate3 Then
                intdifference = DateDiff(DateInterval.Month, newdate1, newdate2)
                'Ver 2.01-REQ233 Start
                'If Mid(SecondDate, 1, 2) >= Mid(FirstDate, 1, 2) Then
                '    intdifference = intdifference + 1
                'End If

                intdifference = intdifference + 1
                'Ver 2.01-REQ233 Start

            Else
                Return 0
            End If
        Else
            intdifference = DateDiff(DateInterval.Month, newdate1, newdate2)
            If intdifference < 0 Then Return 0

            'Ver 2.01-REQ233 Start
            'If Mid(SecondDate, 1, 2) >= Mid(FirstDate, 1, 2) And intdifference > 0 Then
            '    intdifference = intdifference + 1
            'End If
            If intdifference > 0 Then
                intdifference = intdifference + 1
            End If
            'Ver 2.01-REQ233 End
        End If

        Return intdifference


    End Function
    Public Function VerifyChallan(ByVal CsiFilePath As String, ByVal BankBranchCode As String, ByVal DateOfChallanNo As String, ByVal BankChallanNo As String, ByVal DepositAmountAsPerChallan As Double) As Boolean
        Try

            ' VerifyChallan = False

            Dim header As String
            Dim footer As String
            Dim InputString As String
            Dim ChlnAmount As String

            header = "1qi5b63p"
            footer = "9rtio7lb"

            'TanOfDeductor
            ' DateOfChallanNo = Replace(DateOfChallanNo, "/", "")
            ChlnAmount = Format(DepositAmountAsPerChallan, ".00")
            InputString = header & BankBranchCode & DateOfChallanNo & BankChallanNo & TANDeductor & ChlnAmount & footer


            Dim tsFile As String
            Dim objReader As New System.IO.StreamReader(CsiFilePath)

            tsFile = objReader.ReadLine
            Dim strValue As String

            Dim md5hash As MD5 = MD5.Create()
            Dim hash As String = GetMd5Hash(md5hash, InputString)
            'Dim hashchk As String = GetMd5HashDecrypt(md5hash, "8f95dff65de63718e282b967087c49ed")

            Do While objReader.Peek <> -1
                strValue = objReader.ReadLine
                If (strValue = hash) Then
                    Return True
                    'Exit Do
                End If
            Loop
            objReader.Close()
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")
            Return False
        End Try
    End Function
    Public Function GetMd5Hash(ByVal md5Hash As MD5, ByVal input As String) As String
        Try
            ' Convert the input string to a byte array and compute the hash.
            Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input))

            ' Create a new Stringbuilder to collect the bytes
            ' and create a string.
            Dim sBuilder As New StringBuilder()

            ' Loop through each byte of the hashed data 
            ' and format each one as a hexadecimal string.
            Dim i As Integer
            For i = 0 To data.Length - 1
                sBuilder.Append(data(i).ToString("x2"))
            Next i

            ' Return the hexadecimal string.
            Return sBuilder.ToString()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function 'GetMd5Hash
    Public Function GetMd5HashDecrypt(ByVal md5Hash As MD5, ByVal input As String) As String
        Try
            Dim byteArray As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            Dim sBuilder As New StringBuilder()
            Dim i As Integer
            For i = 0 To byteArray.Length - 1
                sBuilder.Append(byteArray(i).ToString("x"))
            Next i
            Return sBuilder.ToString()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function VerifyMd5Hash(ByVal md5Hash As MD5, ByVal input As String, ByVal hash As String) As Boolean
        ' Hash the input.
        Dim hashOfInput As String = GetMd5Hash(md5Hash, input)

        ' Create a StringComparer an compare the hashes.
        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase

        If 0 = comparer.Compare(hashOfInput, hash) Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Sub CsiFile(ByVal TANDeductorVal As String, ByVal StrFinYear As String)
        Try
            AddHandler wb.DocumentCompleted, AddressOf wb_DocumentCompleted

            Dim strValue As String
            Dim StrFinToYear As String

            If Val(Mid(StrFinYear, 1, 2) & Mid(StrFinYear, 5, 4) & "0630") > Val(Today.Year & Format(Today.Month, "00") & Format(Today.Day, "00")) Then
                StrFinToYear = Format(Today.Day, "00") & "/" & Format(Today.Month, "00") & "/" & Today.Year
            Else
                StrFinToYear = "30/06/" & Mid(StrFinYear, 1, 2) & Mid(StrFinYear, 5, 4)
            End If

            Dim objReader As New System.IO.StreamReader(Application.StartupPath & "\Temp_CsiDownload.htm")

            strValue = objReader.ReadToEnd

            Dim objWriter As New System.IO.StreamWriter(Application.StartupPath & "\CsiDownload.htm")


            strValue = strValue.Replace("TAN_OF_DEDUCTOR", TANDeductorVal)
            strValue = strValue.Replace("T_FROM_DT_DD", "01")
            strValue = strValue.Replace("T_FROM_DT_MM", "04")
            strValue = strValue.Replace("T_FROM_DT_YY", Mid(StrFinYear, 1, 4))
            strValue = strValue.Replace("T_TO_DT_DD", Mid(StrFinToYear, 1, 2))
            strValue = strValue.Replace("T_TO_DT_MM", Mid(StrFinToYear, 4, 2))
            strValue = strValue.Replace("T_TO_DT_YY", Mid(StrFinToYear, 7, 4))

            strValue = strValue.Replace("H_FROM_DT_DD", "01")
            strValue = strValue.Replace("H_FROM_DT_MM", "04")
            strValue = strValue.Replace("H_FROM_DT_YY", Mid(StrFinYear, 1, 4))
            strValue = strValue.Replace("H_TO_DT_DD", Mid(StrFinToYear, 1, 2))
            strValue = strValue.Replace("H_TO_DT_MM", Mid(StrFinToYear, 4, 2))
            strValue = strValue.Replace("H_TO_DT_YY", Mid(StrFinToYear, 7, 4))

            objWriter.Write(strValue)
            objReader.Close()
            objWriter.Close()

            wb.Navigate(Application.StartupPath & "\CsiDownload.htm")

            'WebBrowser1.Navigate(Application.StartupPath & "\CsiDownload.htm")
            Application.DoEvents()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")

        End Try

    End Sub


    Private Sub wb_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles wb.DocumentCompleted
        Try
            wb.Document.GetElementById("DOWLOADFILE").Focus()
            wb.Document.GetElementById("DOWLOADFILE").InvokeMember("click")
            Application.DoEvents()

        Catch ex As Exception
            MessageBox.Show("Unable to download CSI file.", "DNF")
        End Try

    End Sub
    Public Sub UpdateRates()
        Dim StrErr As String = String.Empty
        Try

            If strFormNo = "27EQ" Then dcmDelayeDepositIntrst = 0.12 Else dcmDelayeDepositIntrst = 0.18

            Dim strRate() As String
            Dim dtTempView As New DataView(dsMain.Tables("DeducteeDetails"))
            Dim strChlSrno As String
            Dim intIntAmt As Integer
            Dim drtemp() As DataRow
            strChlSrno = ""
            'StrErr = 1
            If blnNextFile = False Then
                Call RateMaster()

                dtRateMaster.ReadXml(Application.StartupPath & "\RateMaster" & strFinYear.Substring(2, 4) & ".xml")
            End If
            dtTempView.Sort = "DeducteeStatus"

            Dim intRateMasterCount As Integer
            If strFormNo = "26Q" Or strFormNo = "27EQ" Or strFormNo = "27Q" Then
                'Ver 2.02-REQ234 start
                'If strFormNo = "27Q" Then
                '    Dim dv As New DataView(dtRateMaster)
                '    dv.RowFilter = "Section = '195'"
                '    dtRateMaster = dv.ToTable()
                'End If
                'Ver 2.02-REQ234 end
                intRateMasterCount = dtRateMaster.Rows.Count
            Else
                intRateMasterCount = 1
            End If

            'StrErr = 2
            For i = 0 To intRateMasterCount - 1
                If strFormNo = "26Q" Or strFormNo = "27EQ" Or strFormNo = "27Q" Then
                    dtTempView.RowFilter = "DeducteeStatus = '" & dtRateMaster.Rows(i)("DeducteeStatus") &
                                           "' And Section = '" & dtRateMaster.Rows(i)("Section") & "'"
                End If
                For j = 0 To dtTempView.Count - 1
                    If dtTempView.Item(j)("DateDeducted").ToString = "" Then Continue For
                    Dim LFlag As String = dtTempView.Item(j)("Remark1").ToString
                    Dim Ddate As Date = Format(Date.Parse(dtTempView.Item(j)("DateDeducted").ToString.Substring(0, 2) & "-" &
                        dtTempView.Item(j)("DateDeducted").ToString.Substring(2, 2) & "-" &
                        dtTempView.Item(j)("DateDeducted").ToString.Substring(4, 4)), "dd-MMM-yyyy")

                    If strFormNo = "26Q" Or strFormNo = "27EQ" Or strFormNo = "27Q" Then

                        If Ddate >= "01-Jul-2021" And (LFlag = "U" Or LFlag = "I" Or LFlag = "J") Then
                            If (dtTempView.Item(j)("PANStatus") = "V" Or dtTempView.Item(j)("PANStatus") = "X") And dtTempView.Item(j)("Remark1") = "" Then
                                If dtRateMaster.Rows(i)("Section") = "94I" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    If dtTempView.Item(j)("Rate") > 2 Then
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(0) < 5, 5, strRate(0) * 2)  'Upper Rate
                                    Else
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(1) < 5, 5, strRate(1) * 2)  'Lower Rate
                                    End If
                                    Array.Resize(strRate, 0)

                                ElseIf dtRateMaster.Rows(i)("Section") = "94C" Or dtRateMaster.Rows(i)("Section") = "94J" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    If dtTempView.Item(j)("Dcode") = "1" Then
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(0) < 5, 5, strRate(0) * 2)  'Companies Rate
                                    Else
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(1) < 5, 5, strRate(1) * 2)  'Non-Companies Rate
                                    End If
                                    Array.Resize(strRate, 0)
                                ElseIf dtRateMaster.Rows(i)("Section") = "94H" Or dtRateMaster.Rows(i)("Section") = "94G" Or dtRateMaster.Rows(i)("Section") = "4EE" Or dtRateMaster.Rows(i)("Section") = "4DA" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    Dim dtDateDeducted94H As String
                                    dtDateDeducted94H = Mid(dtTempView.Item(j)("DateDeducted"), 5, 4) & Mid(dtTempView.Item(j)("DateDeducted"), 3, 2) & Mid(dtTempView.Item(j)("DateDeducted"), 1, 2)  ' Date of Deducted
                                    If dtDateDeducted94H >= "20160601" Then
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(1) < 5, 5, strRate(1) * 2)
                                    Else
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(0) < 5, 5, strRate(0) * 2)
                                    End If
                                    Array.Resize(strRate, 0)
                                ElseIf dtRateMaster.Rows(i)("Section") = "94D" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    If dtTempView.Item(j)("Dcode") = "1" Then
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(0) < 5, 5, strRate(0) * 2)  'Companies Rate
                                    Else
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(1) < 5, 5, strRate(1) * 2)  'Non-Companies Rate
                                    End If
                                    Array.Resize(strRate, 0)
                                ElseIf dtRateMaster.Rows(i)("Section") = "LBB" Or dtRateMaster.Rows(i)("Section") = "LBC" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    If strFormNo = "26Q" Then
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(0) < 5, 5, strRate(0) * 2)
                                    ElseIf strFormNo = "27Q" Then
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(1) < 5, 5, strRate(1) * 2)
                                    End If
                                    Array.Resize(strRate, 0)
                                Else
                                    dtTempView.Item(j)("PrescribedRate") = IIf(dtRateMaster.Rows(i)("Rate") < 5, 5, dtRateMaster.Rows(i)("Rate") * 2)
                                End If
                            ElseIf dtTempView.Item(j)("PANStatus") = "I" Or dtTempView.Item(j)("PANStatus") = "N" Then
                                dtTempView.Item(j)("PrescribedRate") = IIf(dtRateMaster.Rows(i)("Rate") < 5, 5, dtRateMaster.Rows(i)("Rate") * 2)
                            ElseIf (dtTempView.Item(j)("PANStatus") = "V" Or dtTempView.Item(j)("PANStatus") = "X") And dtTempView.Item(j)("Remark1") <> "" Then
                                'dtTempView.Item(j)("PrescribedRate") = IIf(dtTempView.Item(j)("Rate") < 5, 5, dtTempView.Item(j)("Rate") * 2)
                                'If dtRateMaster.Rows(i)("Rate").ToString.Contains(Char.Parse(",")) Then
                                If dtRateMaster.Rows(i)("Section") = "94C" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    If dtTempView.Item(j)("Dcode") = "1" Then
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(0) < 5, 5, strRate(0) * 2)  'Companies Rate
                                    Else
                                        dtTempView.Item(j)("PrescribedRate") = IIf(strRate(1) < 5, 5, strRate(1) * 2)  'Non-Companies Rate
                                    End If
                                    Array.Resize(strRate, 0)
                                Else
                                    dtTempView.Item(j)("PrescribedRate") = IIf(dtTempView.Item(j)("Rate") < 5, 5, dtRateMaster.Rows(i)("Rate") * 2)
                                End If
                            End If
                        Else
                            If (dtTempView.Item(j)("PANStatus") = "V" Or dtTempView.Item(j)("PANStatus") = "X") And dtTempView.Item(j)("Remark1") = "" Then
                                If dtRateMaster.Rows(i)("Section") = "94I" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    If dtTempView.Item(j)("Rate") > 2 Then
                                        dtTempView.Item(j)("PrescribedRate") = strRate(0)  'Upper Rate
                                    Else
                                        dtTempView.Item(j)("PrescribedRate") = strRate(1)  'Lower Rate
                                    End If
                                    Array.Resize(strRate, 0)

                                ElseIf dtRateMaster.Rows(i)("Section") = "94C" Or dtRateMaster.Rows(i)("Section") = "94J" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    If dtTempView.Item(j)("Dcode") = "1" Then
                                        dtTempView.Item(j)("PrescribedRate") = strRate(0)  'Companies Rate
                                    Else
                                        dtTempView.Item(j)("PrescribedRate") = strRate(1)  'Non-Companies Rate
                                    End If
                                    Array.Resize(strRate, 0)
                                ElseIf dtRateMaster.Rows(i)("Section") = "94H" Or dtRateMaster.Rows(i)("Section") = "94G" Or dtRateMaster.Rows(i)("Section") = "4EE" Or dtRateMaster.Rows(i)("Section") = "4DA" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    Dim dtDateDeducted94H As String
                                    dtDateDeducted94H = Mid(dtTempView.Item(j)("DateDeducted"), 5, 4) & Mid(dtTempView.Item(j)("DateDeducted"), 3, 2) & Mid(dtTempView.Item(j)("DateDeducted"), 1, 2)  ' Date of Deducted
                                    If dtDateDeducted94H >= "20160601" Then
                                        dtTempView.Item(j)("PrescribedRate") = strRate(1)
                                    Else
                                        dtTempView.Item(j)("PrescribedRate") = strRate(0)
                                    End If
                                    Array.Resize(strRate, 0)
                                ElseIf dtRateMaster.Rows(i)("Section") = "94D" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    If dtTempView.Item(j)("Dcode") = "1" Then
                                        dtTempView.Item(j)("PrescribedRate") = strRate(0)  'Companies Rate
                                    Else
                                        dtTempView.Item(j)("PrescribedRate") = strRate(1)  'Non-Companies Rate
                                    End If
                                    Array.Resize(strRate, 0)
                                ElseIf dtRateMaster.Rows(i)("Section") = "LBB" Or dtRateMaster.Rows(i)("Section") = "LBC" Then
                                    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                    If strFormNo = "26Q" Then
                                        dtTempView.Item(j)("PrescribedRate") = strRate(0)
                                    ElseIf strFormNo = "27Q" Then
                                        dtTempView.Item(j)("PrescribedRate") = strRate(1)
                                    End If
                                    Array.Resize(strRate, 0)
                                Else
                                    dtTempView.Item(j)("PrescribedRate") = dtRateMaster.Rows(i)("Rate")
                                End If
                            ElseIf dtTempView.Item(j)("PANStatus") = "I" Or dtTempView.Item(j)("PANStatus") = "N" Then
                                dtTempView.Item(j)("PrescribedRate") = dtRateMaster.Rows(i)("Rate")
                            ElseIf (dtTempView.Item(j)("PANStatus") = "V" Or dtTempView.Item(j)("PANStatus") = "X") And dtTempView.Item(j)("Remark1") <> "" Then
                                'If dtRateMaster.Rows(i)("Section") = "94C" Then
                                '    strRate = dtRateMaster.Rows(i)("Rate").ToString.Split(",")
                                '    If dtTempView.Item(j)("Dcode") = "1" Then
                                '        dtTempView.Item(j)("PrescribedRate") = strRate(0) 'Companies Rate
                                '    Else
                                '        dtTempView.Item(j)("PrescribedRate") = strRate(1) 'Non-Companies Rate
                                '    End If
                                '    Array.Resize(strRate, 0)
                                'Else
                                dtTempView.Item(j)("PrescribedRate") = dtTempView.Item(j)("Rate") ' Tds Rate From Text File
                                'End If
                            End If
                        End If


                    ElseIf strFormNo = "24Q" Then
                        If dtTempView.Item(j)("Remark1") = "C" Then
                            dtTempView.Item(j)("PrescribedRate") = "20"
                            dtTempView.Item(j)("AmountDeductible") = Math.Round((dtTempView.Item(j)("PrescribedRate") * dtTempView.Item(j)("DocAmt")) / 100, 2)
                            dtTempView.Item(j)("ShortDedAmount") = dtTempView.Item(j)("AmountDeductible") - dtTempView.Item(j)("TotTds")

                            If dtTempView.Item(j)("ShortDedAmount") > 0 Then
                                dtTempView.Item(j)("Reason1") = "R13"
                            End If
                        End If
                    End If

                    If strFormNo = "26Q" Or strFormNo = "27EQ" Or strFormNo = "27Q" Then
                        dtTempView.Item(j)("AmountDeductible") = Math.Round((dtTempView.Item(j)("PrescribedRate") * dtTempView.Item(j)("DocAmt")) / 100, 2)
                        dtTempView.Item(j)("ShortDedAmount") = dtTempView.Item(j)("AmountDeductible") - dtTempView.Item(j)("TotTds")
                    End If

                    If dtTempView.Item(j)("DelayedDedMonth") > 0 Then
                        'Ver 2.01-REQ233 Start
                        'dtTempView.Item(j)("DelayedDedAmt") = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDeductionIntrst * (dtTempView.Item(j)("DelayedDedMonth") / 12), MidpointRounding.AwayFromZero)
                        'dtTempView.Item(j)("DelayedDedAmt") = Math.Truncate((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 100)) * dcmDelayeDeductionIntrst * (Val(dtTempView.Item(j)("DelayedDedMonth")) / 12))
                        dtTempView.Item(j)("DelayedDedAmt") = Math.Round((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 50)) * dcmDelayeDeductionIntrst * (Val(dtTempView.Item(j)("DelayedDedMonth")) / 12))
                        'Ver 2.01-REQ233 End
                    End If

                    If dtTempView.Item(j)("DelayedDepMonth") > 0 Then
                        If strFinYear <> "201011" Then
                            'Ver 2.01-REQ233 Start
                            'dtTempView.Item(j)("DelayedDepAmt") = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrst * (dtTempView.Item(j)("DelayedDepMonth") / 12), MidpointRounding.AwayFromZero)
                            'dtTempView.Item(j)("DelayedDepAmt") = Math.Truncate((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 100)) * dcmDelayeDepositIntrst * (Val(dtTempView.Item(j)("DelayedDepMonth")) / 12))
                            dtTempView.Item(j)("DelayedDepAmt") = Math.Round((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 10)) * dcmDelayeDepositIntrst * (Val(dtTempView.Item(j)("DelayedDepMonth")) / 12))
                            'Ver 2.01-REQ233 End

                            'Month=03,Year=2020 Start
                            '<Bhaskaran N,16jul202,Rate for 20-21 Late Filing(To add 04,05,06 Months in Conditions)>
                            Dim StrMonth As String = dtTempView.Item(j)("DatePayment").ToString().Substring(2, 2)
                            'If dtTempView.Item(j)("DatePayment").ToString().Substring(2, 2) = "03" And dtTempView.Item(j)("DatePayment").ToString().Substring(4, 4) = "2020" Then
                            If (StrMonth = "03" Or StrMonth = "04" Or StrMonth = "05" Or StrMonth = "06") And dtTempView.Item(j)("DatePayment").ToString().Substring(4, 4) = "2020" Then
                                'dtTempView.Item(j)("DelayedDepAmt") = Math.Truncate((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 100)) * dcmDelayeDepositInt * (Val(dtTempView.Item(j)("DelayedDepMonth")) / 12))
                                dtTempView.Item(j)("DelayedDepAmt") = Math.Round((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 50)) * dcmDelayeDepositInt * (Val(dtTempView.Item(j)("DelayedDepMonth")) / 12))
                            End If
                            'Month=03,Year=2020 End
                        Else
                            Dim dtCutoff As Date
                            Dim mIntAmt As Double
                            Dim intMonthdiffTill062010 As Integer
                            dtCutoff = "30/06/2010"
                            Dim dtDateDeducted As Date
                            Dim dtChlnDate As Date

                            dtDateDeducted = Mid(dtTempView.Item(j)("DateDeducted"), 1, 2) & "/" & Mid(dtTempView.Item(j)("DateDeducted"), 3, 2) & "/" & Mid(dtTempView.Item(j)("DateDeducted"), 5, 4) ' Date of Deducted
                            dtChlnDate = Mid(dtTempView.Item(j)("ChlnDT"), 1, 2) & "/" & Mid(dtTempView.Item(j)("ChlnDT"), 3, 2) & "/" & Mid(dtTempView.Item(j)("ChlnDT"), 5, 4) 'Challan date

                            If DateDiff(DateInterval.Month, dtCutoff, dtDateDeducted) > 0 And DateDiff(DateInterval.Month, dtCutoff, dtChlnDate) > 0 Then
                                mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrst * ((dtTempView.Item(j)("DelayedDepMonth")) / 12), MidpointRounding.AwayFromZero)
                            ElseIf DateDiff(DateInterval.Month, dtDateDeducted, dtCutoff) >= 0 And DateDiff(DateInterval.Month, dtCutoff, dtChlnDate) > 0 Then

                                intMonthdiffTill062010 = MonthDifference("30062010", dtTempView.Item(j)("DateDeducted"))
                                mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrstTill30062010 * (intMonthdiffTill062010 / 12), MidpointRounding.AwayFromZero)
                                mIntAmt = mIntAmt + Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrst * ((dtTempView.Item(j)("DelayedDepMonth") - intMonthdiffTill062010) / 12), MidpointRounding.AwayFromZero)
                            ElseIf DateDiff(DateInterval.Month, dtDateDeducted, dtCutoff) >= 0 And DateDiff(DateInterval.Month, dtChlnDate, dtCutoff) >= 0 Then
                                mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrstTill30062010 * ((dtTempView.Item(j)("DelayedDepMonth")) / 12), MidpointRounding.AwayFromZero)
                            End If
                            dtTempView.Item(j)("DelayedDepAmt") = mIntAmt
                        End If
                    End If

                    dtTempView.Item(j)("TotInterest") = Val(dtTempView.Item(j)("DelayedDedAmt")) + Val(dtTempView.Item(j)("DelayedDepAmt"))

                    If strFormNo = "26Q" Or strFormNo = "27EQ" Or strFormNo = "27Q" Then
                        If dtTempView.Item(j)("PANStatus") <> "I" And (dtTempView.Item(j)("PrescribedRate") > dtTempView.Item(j)("Rate")) Then
                            dtTempView.Item(j)("Reason1") = "R1"
                        ElseIf dtTempView.Item(j)("PANStatus") = "I" And (dtTempView.Item(j)("PrescribedRate") > dtTempView.Item(j)("Rate")) Then
                            dtTempView.Item(j)("Reason1") = "R2"
                        ElseIf dtTempView.Item(j)("TotTds") <> Math.Round(dtTempView.Item(j)("DocAmt") * dtTempView.Item(j)("Rate") / 100, MidpointRounding.AwayFromZero) Then
                            dtTempView.Item(j)("Reason1") = "R3"
                        End If
                    End If

                    If dtTempView.Item(j)("TotInterest") > 0 Then
                        If dtTempView.Item(j)("DelayedDedAmt") > 0 And dtTempView.Item(j)("DelayedDepAmt") > 0 Then
                            dtTempView.Item(j)("Reason2") = "R7"
                        ElseIf dtTempView.Item(j)("DelayedDedAmt") > 0 Then
                            dtTempView.Item(j)("Reason2") = "R5"
                        ElseIf dtTempView.Item(j)("DelayedDepAmt") > 0 Then
                            dtTempView.Item(j)("Reason2") = "R6"
                        Else
                            If strChlSrno <> dtTempView.Item(j)("ChlnSrNo") Then
                                strChlSrno = dtTempView.Item(j)("ChlnSrNo")
                                drtemp = dtCD.Select("ChlnSrNo = '" & strChlSrno & "'")
                                intIntAmt = drtemp(0).Item("IntAmt")
                            End If

                            If intIntAmt = 0 Then
                                dtTempView.Item(j)("Reason2") = "R8"
                            ElseIf intIntAmt > 0 And intIntAmt < dtTempView.Item(j)("TotInterest") Then
                                dtTempView.Item(j)("Reason2") = "R9"
                            End If
                        End If
                    End If
                Next
            Next

            StrErr = 3
            ''Ver 7.03-REQ817 start As per kiran the following validation of 195 will work for 1819 and onward=================================================================================
            If strFinYear >= "201819" Then
                If strFormNo = "27Q" Then
                    dtTempView.RowFilter = ""
                    Dim blnPaymentType As Boolean = False
                    Dim blnCountryCode As Boolean = False
                    If blnNextFile = False Then
                        Call NRIRateMaster()
                        Dim xmlFile As XmlReader
                        xmlFile = XmlReader.Create(Application.StartupPath & "\NRIRATES" & strFinYear.Substring(2, 4) & ".xml", New XmlReaderSettings())
                        Dim dsNRIRateMaster As New DataSet()
                        dsNRIRateMaster.ReadXml(xmlFile)
                        dtNRIRateMaster = dsNRIRateMaster.Tables("NRIRATES")
                    End If
                    dtTempView.Sort = "DeducteeStatus"
                    intRateMasterCount = 0
                    intRateMasterCount = dtNRIRateMaster.Rows.Count
                    Dim drFindVal() As DataRow
                    Dim strReason As String
                    strReason = ""

                    For i = 0 To intRateMasterCount - 1
                        dtTempView.RowFilter = "DTTA = '" & IIf(dtNRIRateMaster.Rows(i)("Flag") = "DTAA", "B", "A") &
                                               "' And Section = '" & dtNRIRateMaster.Rows(i)("Section") & "' And Remitance ='" & dtNRIRateMaster.Rows(i)("PaymentTypeCode") & "'  And CountryCode ='" & dtNRIRateMaster.Rows(i)("Code") & "'"
                        If dtTempView.Count = 0 Then
                            dtTempView.RowFilter = ""
                            dtTempView.RowFilter = "DTTA = '" & IIf(dtNRIRateMaster.Rows(i)("Flag") = "DTAA", "B", "A") &
                           "' And Section = '" & dtNRIRateMaster.Rows(i)("Section") & "'"
                            For j = 0 To dtTempView.Count - 1
                                If strFormNo = "27Q" And dtTempView.Item(j)("DTTA").ToString().Trim().ToUpper() = "B" And dtTempView.Item(j)("Remark1").ToString() = "" Then
                                    drFindVal = dtNRIRateMaster.Select("Flag = 'DTAA' And Section ='" & dtTempView.Item(j)("Section") & "' And Code = '" & dtTempView.Item(j)("CountryCode") & "' And PaymentTypeCode = '" & dtTempView.Item(j)("Remitance") & "'")
                                    If drFindVal.Count = 0 Then
                                        drFindVal = dtNRIRateMaster.Select("Flag = 'DTAA' And Section ='" & dtTempView.Item(j)("Section") & "' And Code = '" & dtTempView.Item(j)("CountryCode") & "'")
                                        If drFindVal.Count = 0 Then
                                            strReason = "R16"
                                        Else
                                            strReason = "R15"
                                        End If
                                    End If
                                    If strReason <> "" Then
                                        dtTempView.Item(j)("Reason1") = strReason
                                        dtTempView.Item(j)("DelayedDepAmt") = 0
                                        dtTempView.Item(j)("AmountDeductible") = 0
                                        dtTempView.Item(j)("ShortDedAmount") = 0
                                        dtTempView.Item(j)("DelayedDedAmt") = 0
                                    Else
                                        If dtTempView.Item(j)("Dcode") = "2" Then
                                            dtTempView.Item(j)("PrescribedRate") = drFindVal(0)("Others")
                                        Else
                                            dtTempView.Item(j)("PrescribedRate") = drFindVal(0)("Company")
                                        End If

                                        If dtTempView.Item(j)("PrescribedRate") > 0 Then
                                            'If dtTempView.Item(j)("Rate") < dtTempView.Item(j)("PrescribedRate") Then
                                            'TdsAmount As per IT Rate Chart
                                            dtTempView.Item(j)("AmountDeductible") = Math.Round((dtTempView.Item(j)("PrescribedRate") * dtTempView.Item(j)("DocAmt")) / 100, 2)

                                            'ShortDedAmount 
                                            dtTempView.Item(j)("ShortDedAmount") = dtTempView.Item(j)("AmountDeductible") - dtTempView.Item(j)("TotTds")

                                            'Delayed deduction interest calculation
                                            If dtTempView.Item(j)("DelayedDedMonth") > 0 Then
                                                dtTempView.Item(j)("DelayedDedAmt") = Math.Truncate((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 50)) * dcmDelayeDeductionIntrst * (Val(dtTempView.Item(j)("DelayedDedMonth")) / 12))
                                            End If

                                            'Delayed deposit interest calculation
                                            If dtTempView.Item(j)("DelayedDepMonth") > 0 Then
                                                If strFinYear <> "201011" Then
                                                    dtTempView.Item(j)("DelayedDepAmt") = Math.Truncate((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 50)) * dcmDelayeDepositIntrst * (Val(dtTempView.Item(j)("DelayedDepMonth")) / 12))
                                                Else
                                                    Dim dtCutoff As Date
                                                    Dim mIntAmt As Double
                                                    Dim intMonthdiffTill062010 As Integer
                                                    dtCutoff = "30/06/2010"
                                                    Dim dtDateDeducted As Date
                                                    Dim dtChlnDate As Date

                                                    dtDateDeducted = Mid(dtTempView.Item(j)("DateDeducted"), 1, 2) & "/" & Mid(dtTempView.Item(j)("DateDeducted"), 3, 2) & "/" & Mid(dtTempView.Item(j)("DateDeducted"), 5, 4) ' Date of Deducted
                                                    dtChlnDate = Mid(dtTempView.Item(j)("ChlnDT"), 1, 2) & "/" & Mid(dtTempView.Item(j)("ChlnDT"), 3, 2) & "/" & Mid(dtTempView.Item(j)("ChlnDT"), 5, 4) 'Challan date

                                                    If DateDiff(DateInterval.Month, dtCutoff, dtDateDeducted) > 0 And DateDiff(DateInterval.Month, dtCutoff, dtChlnDate) > 0 Then
                                                        mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrst * ((dtTempView.Item(j)("DelayedDepMonth")) / 12), MidpointRounding.AwayFromZero)
                                                    ElseIf DateDiff(DateInterval.Month, dtDateDeducted, dtCutoff) >= 0 And DateDiff(DateInterval.Month, dtCutoff, dtChlnDate) > 0 Then

                                                        intMonthdiffTill062010 = MonthDifference("30062010", dtTempView.Item(j)("DateDeducted"))
                                                        mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrstTill30062010 * (intMonthdiffTill062010 / 12), MidpointRounding.AwayFromZero)
                                                        mIntAmt = mIntAmt + Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrst * ((dtTempView.Item(j)("DelayedDepMonth") - intMonthdiffTill062010) / 12), MidpointRounding.AwayFromZero)
                                                    ElseIf DateDiff(DateInterval.Month, dtDateDeducted, dtCutoff) >= 0 And DateDiff(DateInterval.Month, dtChlnDate, dtCutoff) >= 0 Then
                                                        mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrstTill30062010 * ((dtTempView.Item(j)("DelayedDepMonth")) / 12), MidpointRounding.AwayFromZero)
                                                    End If
                                                    dtTempView.Item(j)("DelayedDepAmt") = mIntAmt
                                                End If
                                            End If
                                            'Total interest calculation
                                            dtTempView.Item(j)("TotInterest") = Val(dtTempView.Item(j)("DelayedDedAmt")) + Val(dtTempView.Item(j)("DelayedDepAmt"))
                                            'End If

                                            If strFormNo = "26Q" Or strFormNo = "27EQ" Or strFormNo = "27Q" Then
                                                If dtTempView.Item(j)("PANStatus") <> "I" And (dtTempView.Item(j)("PrescribedRate") > dtTempView.Item(j)("Rate")) Then
                                                    dtTempView.Item(j)("Reason1") = "R1"
                                                ElseIf dtTempView.Item(j)("PANStatus") = "I" And (dtTempView.Item(j)("PrescribedRate") > dtTempView.Item(j)("Rate")) Then
                                                    dtTempView.Item(j)("Reason1") = "R2"
                                                ElseIf dtTempView.Item(j)("TotTds") <> Math.Round(dtTempView.Item(j)("DocAmt") * dtTempView.Item(j)("Rate") / 100, MidpointRounding.AwayFromZero) Then
                                                    dtTempView.Item(j)("Reason1") = "R3"
                                                Else
                                                    dtTempView.Item(j)("Reason1") = ""
                                                End If
                                            End If

                                            'Update Reasons........ Reason2  for LatePayment
                                            If dtTempView.Item(j)("TotInterest") > 0 Then
                                                If dtTempView.Item(j)("DelayedDedAmt") > 0 And dtTempView.Item(j)("DelayedDepAmt") > 0 Then
                                                    dtTempView.Item(j)("Reason2") = "R7"
                                                ElseIf dtTempView.Item(j)("DelayedDedAmt") > 0 Then
                                                    dtTempView.Item(j)("Reason2") = "R5"
                                                ElseIf dtTempView.Item(j)("DelayedDepAmt") > 0 Then
                                                    dtTempView.Item(j)("Reason2") = "R6"
                                                Else
                                                    If strChlSrno <> dtTempView.Item(j)("ChlnSrNo") Then
                                                        strChlSrno = dtTempView.Item(j)("ChlnSrNo")
                                                        drtemp = dtCD.Select("ChlnSrNo = '" & strChlSrno & "'")
                                                        intIntAmt = drtemp(0).Item("IntAmt")
                                                    End If

                                                    If intIntAmt = 0 Then
                                                        dtTempView.Item(j)("Reason2") = "R8"
                                                    ElseIf intIntAmt > 0 And intIntAmt < dtTempView.Item(j)("TotInterest") Then
                                                        dtTempView.Item(j)("Reason2") = "R9"
                                                    End If
                                                End If
                                            End If

                                        End If

                                    End If
                                Else
                                    If (dtTempView.Item(j)("PAN").ToString() <> "PANAPPLIED" And dtTempView.Item(j)("PAN").ToString() <> "PANINVALID" And dtTempView.Item(j)("PAN").ToString() <> "PANNOTAVBL") And dtTempView.Item(j)("Remark1").ToString() = "" Then
                                        If dtTempView.Item(j)("DTTA").ToString().Trim().ToUpper() = "A" Then
                                            drFindVal = dtNRIRateMaster.Select("Flag = 'IT' And Section ='" & dtTempView.Item(j)("Section") & "' And PaymentTypeCode = '" & dtTempView.Item(j)("Remitance") & "'")
                                            If drFindVal.Count = 0 Then
                                                dtTempView.Item(j)("Reason1") = "R18"
                                                dtTempView.Item(j)("DelayedDepAmt") = 0
                                                dtTempView.Item(j)("AmountDeductible") = 0
                                                dtTempView.Item(j)("ShortDedAmount") = 0
                                                dtTempView.Item(j)("DelayedDedAmt") = 0
                                            Else
                                                If dtTempView.Item(j)("Dcode") = "2" Then
                                                    dtTempView.Item(j)("PrescribedRate") = drFindVal(0)("Others")
                                                Else
                                                    dtTempView.Item(j)("PrescribedRate") = drFindVal(0)("Company")
                                                End If

                                                If dtTempView.Item(j)("PrescribedRate") > 0 Then
                                                    If dtTempView.Item(j)("Rate") < dtTempView.Item(j)("PrescribedRate") And dtTempView.Item(j)("DTTA").ToString().Trim().ToUpper() = "A" Then

                                                        dtTempView.Item(j)("Reason1") = "R17"
                                                        'TdsAmount As per IT Rate Chart
                                                        dtTempView.Item(j)("AmountDeductible") = Math.Round((dtTempView.Item(j)("PrescribedRate") * dtTempView.Item(j)("DocAmt")) / 100, 2)

                                                        'ShortDedAmount 
                                                        dtTempView.Item(j)("ShortDedAmount") = dtTempView.Item(j)("AmountDeductible") - dtTempView.Item(j)("TotTds")

                                                        'Delayed deduction interest calculation
                                                        If dtTempView.Item(j)("DelayedDedMonth") > 0 Then
                                                            dtTempView.Item(j)("DelayedDedAmt") = Math.Truncate((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 50)) * dcmDelayeDeductionIntrst * (Val(dtTempView.Item(j)("DelayedDedMonth")) / 12))
                                                        End If

                                                        'Delayed deposit interest calculation
                                                        If dtTempView.Item(j)("DelayedDepMonth") > 0 Then
                                                            If strFinYear <> "201011" Then
                                                                dtTempView.Item(j)("DelayedDepAmt") = Math.Truncate((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 50)) * dcmDelayeDepositIntrst * (Val(dtTempView.Item(j)("DelayedDepMonth")) / 12))
                                                            Else
                                                                Dim dtCutoff As Date
                                                                Dim mIntAmt As Double
                                                                Dim intMonthdiffTill062010 As Integer
                                                                dtCutoff = "30/06/2010"
                                                                Dim dtDateDeducted As Date
                                                                Dim dtChlnDate As Date

                                                                dtDateDeducted = Mid(dtTempView.Item(j)("DateDeducted"), 1, 2) & "/" & Mid(dtTempView.Item(j)("DateDeducted"), 3, 2) & "/" & Mid(dtTempView.Item(j)("DateDeducted"), 5, 4) ' Date of Deducted
                                                                dtChlnDate = Mid(dtTempView.Item(j)("ChlnDT"), 1, 2) & "/" & Mid(dtTempView.Item(j)("ChlnDT"), 3, 2) & "/" & Mid(dtTempView.Item(j)("ChlnDT"), 5, 4) 'Challan date

                                                                If DateDiff(DateInterval.Month, dtCutoff, dtDateDeducted) > 0 And DateDiff(DateInterval.Month, dtCutoff, dtChlnDate) > 0 Then
                                                                    mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrst * ((dtTempView.Item(j)("DelayedDepMonth")) / 12), MidpointRounding.AwayFromZero)
                                                                ElseIf DateDiff(DateInterval.Month, dtDateDeducted, dtCutoff) >= 0 And DateDiff(DateInterval.Month, dtCutoff, dtChlnDate) > 0 Then

                                                                    intMonthdiffTill062010 = MonthDifference("30062010", dtTempView.Item(j)("DateDeducted"))
                                                                    mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrstTill30062010 * (intMonthdiffTill062010 / 12), MidpointRounding.AwayFromZero)
                                                                    mIntAmt = mIntAmt + Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrst * ((dtTempView.Item(j)("DelayedDepMonth") - intMonthdiffTill062010) / 12), MidpointRounding.AwayFromZero)
                                                                ElseIf DateDiff(DateInterval.Month, dtDateDeducted, dtCutoff) >= 0 And DateDiff(DateInterval.Month, dtChlnDate, dtCutoff) >= 0 Then
                                                                    mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrstTill30062010 * ((dtTempView.Item(j)("DelayedDepMonth")) / 12), MidpointRounding.AwayFromZero)
                                                                End If
                                                                dtTempView.Item(j)("DelayedDepAmt") = mIntAmt
                                                            End If
                                                        End If

                                                        'Total interest calculation
                                                        dtTempView.Item(j)("TotInterest") = Val(dtTempView.Item(j)("DelayedDedAmt")) + Val(dtTempView.Item(j)("DelayedDepAmt"))
                                                    End If
                                                End If
                                            End If
                                        ElseIf dtTempView.Item(j)("DTTA").ToString().Trim().ToUpper() = "B" Then
                                            'drFindVal = dtNRIRateMaster.Select("Flag = 'DTAA' And Section ='" & dtTempView.Item(j)("Section") & "' And Code = '" & dtTempView.Item(j)("CountryCode") & "'")
                                            'If drFindVal.Count = 0 Then
                                            '    strReason = IIf(strReason <> "", strReason & ",", "") & "R16"
                                            'End If
                                            'drFindVal = dtNRIRateMaster.Select("Flag = 'DTAA' And Section ='" & dtTempView.Item(j)("Section") & "' And PaymentTypeCode = '" & dtTempView.Item(j)("Remitance") & "'")
                                            'If drFindVal.Count = 0 Then
                                            '    strReason = IIf(strReason <> "", strReason & ",", "") & "R15"
                                            'End If
                                            drFindVal = dtNRIRateMaster.Select("Flag = 'DTAA' And Section ='" & dtTempView.Item(j)("Section") & "' And Code = '" & dtTempView.Item(j)("CountryCode") & "' And PaymentTypeCode = '" & dtTempView.Item(j)("Remitance") & "'")
                                            If drFindVal.Count = 0 Then
                                                drFindVal = dtNRIRateMaster.Select("Flag = 'DTAA' And Section ='" & dtTempView.Item(j)("Section") & "' And Code = '" & dtTempView.Item(j)("CountryCode") & "'")
                                                If drFindVal.Count = 0 Then
                                                    strReason = "R16"
                                                Else
                                                    strReason = "R15"
                                                End If
                                            End If
                                            If strReason <> "" Then
                                                dtTempView.Item(j)("Reason1") = strReason
                                                dtTempView.Item(j)("DelayedDepAmt") = 0
                                                dtTempView.Item(j)("AmountDeductible") = 0
                                                dtTempView.Item(j)("ShortDedAmount") = 0
                                                dtTempView.Item(j)("DelayedDedAmt") = 0
                                            End If
                                        End If
                                    End If
                                End If
                                strReason = ""
                            Next
                            dtTempView.RowFilter = ""
                        Else

                            For j = 0 To dtTempView.Count - 1
                                'If Not ((dtTempView.Item(j)("PAN").ToString() = "PANAPPLIED" Or dtTempView.Item(j)("PAN").ToString() = "PANINVALID" Or dtTempView.Item(j)("PAN").ToString() = "PANNOTAVBL") And dtTempView.Item(j)("Remark1").ToString() = "") Then
                                If ((dtTempView.Item(j)("PAN").ToString() <> "PANAPPLIED" And dtTempView.Item(j)("PAN").ToString() <> "PANINVALID" And dtTempView.Item(j)("PAN").ToString() <> "PANNOTAVBL") And dtTempView.Item(j)("Remark1").ToString() = "") Then
                                    If dtTempView.Item(j)("Dcode") = "2" Then
                                        dtTempView.Item(j)("PrescribedRate") = dtNRIRateMaster.Rows(i)("Others")
                                    Else
                                        dtTempView.Item(j)("PrescribedRate") = dtNRIRateMaster.Rows(i)("Company")
                                    End If

                                    If dtTempView.Item(j)("PrescribedRate") > 0 Then
                                        If dtTempView.Item(j)("Rate") < dtTempView.Item(j)("PrescribedRate") And dtTempView.Item(j)("DTTA").ToString().Trim().ToUpper() = "A" Then
                                            dtTempView.Item(j)("Reason1") = "R17"
                                        ElseIf dtTempView.Item(j)("Rate") < dtTempView.Item(j)("PrescribedRate") And dtTempView.Item(j)("DTTA").ToString().Trim().ToUpper() = "B" Then
                                            dtTempView.Item(j)("Reason1") = "R14"
                                        End If

                                        '===================================

                                        If dtTempView.Item(j)("PrescribedRate") > 0 And IsDBNull(dtTempView.Item(j)("Reason1")) = False Then
                                            If dtTempView.Item(j)("Rate") < dtTempView.Item(j)("PrescribedRate") Then

                                                'TdsAmount As per IT Rate Chart
                                                dtTempView.Item(j)("AmountDeductible") = Math.Round((dtTempView.Item(j)("PrescribedRate") * dtTempView.Item(j)("DocAmt")) / 100, 2)

                                                'ShortDedAmount 
                                                dtTempView.Item(j)("ShortDedAmount") = dtTempView.Item(j)("AmountDeductible") - dtTempView.Item(j)("TotTds")


                                                'Delayed deduction interest calculation
                                                If dtTempView.Item(j)("DelayedDedMonth") > 0 Then
                                                    dtTempView.Item(j)("DelayedDedAmt") = Math.Truncate((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 50)) * dcmDelayeDeductionIntrst * (Val(dtTempView.Item(j)("DelayedDedMonth")) / 12))
                                                End If

                                                'Delayed deposit interest calculation
                                                If dtTempView.Item(j)("DelayedDepMonth") > 0 Then
                                                    If strFinYear <> "201011" Then
                                                        dtTempView.Item(j)("DelayedDepAmt") = Math.Truncate((Val(dtTempView.Item(j)("Deposit")) - (Val(dtTempView.Item(j)("Deposit")) Mod 50)) * dcmDelayeDepositIntrst * (Val(dtTempView.Item(j)("DelayedDepMonth")) / 12))
                                                    Else
                                                        Dim dtCutoff As Date
                                                        Dim mIntAmt As Double
                                                        Dim intMonthdiffTill062010 As Integer
                                                        dtCutoff = "30/06/2010"
                                                        Dim dtDateDeducted As Date
                                                        Dim dtChlnDate As Date

                                                        dtDateDeducted = Mid(dtTempView.Item(j)("DateDeducted"), 1, 2) & "/" & Mid(dtTempView.Item(j)("DateDeducted"), 3, 2) & "/" & Mid(dtTempView.Item(j)("DateDeducted"), 5, 4) ' Date of Deducted
                                                        dtChlnDate = Mid(dtTempView.Item(j)("ChlnDT"), 1, 2) & "/" & Mid(dtTempView.Item(j)("ChlnDT"), 3, 2) & "/" & Mid(dtTempView.Item(j)("ChlnDT"), 5, 4) 'Challan date

                                                        If DateDiff(DateInterval.Month, dtCutoff, dtDateDeducted) > 0 And DateDiff(DateInterval.Month, dtCutoff, dtChlnDate) > 0 Then
                                                            mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrst * ((dtTempView.Item(j)("DelayedDepMonth")) / 12), MidpointRounding.AwayFromZero)
                                                        ElseIf DateDiff(DateInterval.Month, dtDateDeducted, dtCutoff) >= 0 And DateDiff(DateInterval.Month, dtCutoff, dtChlnDate) > 0 Then

                                                            intMonthdiffTill062010 = MonthDifference("30062010", dtTempView.Item(j)("DateDeducted"))
                                                            mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrstTill30062010 * (intMonthdiffTill062010 / 12), MidpointRounding.AwayFromZero)
                                                            mIntAmt = mIntAmt + Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrst * ((dtTempView.Item(j)("DelayedDepMonth") - intMonthdiffTill062010) / 12), MidpointRounding.AwayFromZero)
                                                        ElseIf DateDiff(DateInterval.Month, dtDateDeducted, dtCutoff) >= 0 And DateDiff(DateInterval.Month, dtChlnDate, dtCutoff) >= 0 Then
                                                            mIntAmt = Math.Round(dtTempView.Item(j)("Deposit") * dcmDelayeDepositIntrstTill30062010 * ((dtTempView.Item(j)("DelayedDepMonth")) / 12), MidpointRounding.AwayFromZero)
                                                        End If
                                                        dtTempView.Item(j)("DelayedDepAmt") = mIntAmt
                                                    End If
                                                End If

                                                'Total interest calculation
                                                dtTempView.Item(j)("TotInterest") = Val(dtTempView.Item(j)("DelayedDedAmt")) + Val(dtTempView.Item(j)("DelayedDepAmt"))
                                            End If
                                        End If

                                        '===================================

                                    End If
                                End If
                            Next

                            dtTempView.RowFilter = ""
                        End If
                    Next
                End If
            End If
            'Ver 7.03-REQ817 end===========================================================================
        Catch ex As Exception

            'Dim trace = New Diagnostics.StackTrace(ex, True)
            'Dim line As String = trace.ToString()
            'Dim nombreMetodo As String = ""
            'For Each sf As StackFrame In trace.GetFrames
            '    nombreMetodo = sf.GetMethod().Name & vbCrLf
            'Next
            MessageBox.Show(ex.Message & " - " & StrErr & vbCrLf & ex.StackTrace.ToString, "DNF")
        End Try
    End Sub
    Public Sub DisposeAll()
        dtBH.Dispose()
        dtBH.Clear()

        dtCD.Dispose()
        dtCD.Clear()

        dtDD.Dispose()
        dtDD.Clear()


        dtSD.Dispose()
        dtSD.Clear()


        dsMain.Dispose()
        dsMain.Tables.Clear()

        If IsNothing(dtTempPAN) = False Then
            dtTempPAN.Dispose()
            dtTempPAN.Clear()
        End If

        strLine = ""
        TANDeductor = ""
        strCSIPath = ""
        strXmlPath = ""
        strFilePath = ""
        intCountOfUnMatched = 0
        intCountOfValidPAN = 0
        blnVerifyChallanSkip = True
        blnVerifyPANSkip = True



        blnDNFCreationDelegate = False
        blnPANVerification = False
        blnPANDelegate = False
        blnProcessValidationDelegate = False
        blnProcessValidation = False
        strPanProcess = ""
        strPanStatus = ""
        intCntInvalidPan = 0
        strDNFExcelFile = ""
        blnFileFailure = False
        strProbshortDedAmt = ""
        strProbshortDepAmt = ""
        strProbIntPay = ""

        strFormNo = ""
        blnPANCount = False

        intIgnoredPANList = 0


        'Ver 2.02-REQ235 start
        strDeductorType = ""
        strSubmittedDate = ""
        strFileQuarter = ""
        lngFee = 0
        dblTotTaxDeducted = 0
        dblBalanceLateFilingFee = 0
        If blnCheckLateFiling = True Then
            dtLateFiling.Dispose()
            dtLateFiling.Clear()
            dtLateFiling.Reset()
            blnCheckLateFiling = False
        End If
        'Ver 2.02-REQ235 end

        'Ver 3.02-REQ311 start
        blnIsBookEntry = False
        'Ver 3.02-REQ311 end

    End Sub
    Public Function ValidVersion(ByVal strFYyr As String, ByVal strAppVersion As String) As Boolean

        Dim wbClient As New WebClient
        strWebAppVersion = ""
        Try

            ' If My.Computer.Network.Ping("http://www.fastfacts.co.in") = True Then
            strWebAppVersion = wbClient.DownloadString("http://www.fastfacts.co.in/etdsDNF" & strFYyr & ".htm")
            'End If
        Catch ex As System.InvalidOperationException
            Return True
        Catch ex As Exception
            Return True
        End Try
        'Dim wbAppVer As New WebBrowser
        'Dim uriString As String = "http://www.fastfacts.co.in/etdsDNF" & strFYyr & ".htm"       
        'wbAppVer.Navigate(uriString)

        'Application.DoEvents()
        'System.Threading.Thread.Sleep(20000)
        'Application.DoEvents()
        'If Val(wbAppVer.DocumentText) = 0 Then
        '    MessageBox.Show("Internet connection failed. ")
        'End If


        'strWebAppVersion = Replace(wbAppVer.DocumentText, " ", ".")
        ' wbAppVer.Dispose()

        If strWebAppVersion <> "" Then
            strWebAppVersion = Replace(strWebAppVersion, " ", ".")
            If Val(strWebAppVersion) > Val(strAppVersion) Then
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If

    End Function
    Public Sub DownLoad(ByVal strFYyr As String, ByVal strURL As String, ByVal strSaveFilePath As String, ByRef pb As ProgressBar)
        If Directory.Exists("C:\etdsDNF" & strFYyr & "\Upgrade") = False Then
            Directory.CreateDirectory("C:\etdsDNF" & strFYyr & "\Upgrade")
        End If


        Dim request As WebRequest
        Dim response As WebResponse
        Dim reader As Stream
        Dim writer As Stream
        Dim data(1023) As Byte
        Dim count As Integer
        Dim total As Integer

        request = WebRequest.Create(strURL)

        response = request.GetResponse()

        reader = response.GetResponseStream()

        pb.Maximum = CInt(response.ContentLength)

        pb.Value = 0

        total = 0

        writer = File.Create(strSaveFilePath)

        While True

            count = reader.Read(data, 0, 1024)

            If count <= 0 Then

                Exit While

            End If

            writer.Write(data, 0, count)

            total += 1024

            If total < pb.Maximum Then pb.Value = total

            Application.DoEvents()

        End While

        reader.Close()

        writer.Close()

    End Sub
    Public Sub GetFile(ByRef sUrl As Object, ByVal strProductSrNo As String, ByVal strFYyr As String, ByVal strFileName As String)
        DownLoad(strFYyr, sUrl, strFileName, frmDownLoad.pb)
        frmDownLoad.pb.Visible = False
        frmDownLoad.pbProcess.Visible = False
        MessageBox.Show("DownLoad Complete!", "DNF")
        frmDownLoad.cmdRunUpgrade.Enabled = True
    End Sub
    Public Function Dongle() As Boolean


        Dim DogAddr As Integer
        Dim DogBytes As Integer
        Dim DogData As String
        Dim Y As Integer
        Dim z As Integer
        strDemoMessage = ""
        'Ver 3.01-REQ297 start
        strEtdsVal = ""
        strDongleFyYr = ""
        'Ver 3.01-REQ297 end
        'Ver 2.06-REQ291 start
        Dim blneTds As Boolean
        blneTds = False
        DogAddr = 70
        DogBytes = 8
        DogData = "AAAAAAAA"

        Y = DogRead(DogBytes, DogAddr, DogData)
        If DogData.Substring(0, 4) = "ETDS" Then
            blneTds = True
            'Ver 3.01-REQ297 start
            strEtdsVal = DogData
            strDongleFyYr = DogData.Substring(4, 4)
            'Ver 3.01-REQ297 end
        End If
        'Ver 2.06-REQ291 end


        DogAddr = 40
        DogBytes = 7
        DogData = "AAAAAAA"

        Y = DogRead(DogBytes, DogAddr, DogData)
        'Ver 2.05-REQ272 start
        'If DogData = "DNF" & ConstStrFYyr Then
        If DogData.Substring(0, 3) = "DNF" And Val(DogData.Substring(3, 4)) >= Val(ConstStrFYyr) Then
            'Ver 2.05-REQ272 end
            DogAddr = 60
            'Ver 2.06-REQ291 start
            'DogBytes = 5
            'DogData = "AAAAA"

            'Ver 3.01-REQ297 start
            strEtdsVal = DogData
            strDongleFyYr = DogData.Substring(3, 4)
            'Ver 3.01-REQ297 end

            If Val(DogData.Substring(3, 4)) >= 1415 And blneTds = True Then
                DogBytes = 8
                DogData = "AAAAAAAA"
            Else
                DogBytes = 5
                DogData = "AAAAA"
            End If
            'Ver 2.06-REQ291 end
            z = DogRead(DogBytes, DogAddr, DogData)
            strDogSrNo = DogData
            blnDemoVersion = False
            Return True
        Else

            DogAddr = 70
            DogBytes = 8
            DogData = "AAAAAAAA"

            Y = DogRead(DogBytes, DogAddr, DogData)
            'Ver 2.05-REQ272 start
            'If DogData = "ETDS" & ConstStrFYyr Then
            If DogData.Substring(0, 4) = "ETDS" And Val(DogData.Substring(4, 4)) >= Val(ConstStrFYyr) Then
                'Ver 2.05-REQ272 end
                DogAddr = 60
                'Ver 2.06-REQ291 start
                'DogBytes = 6
                'DogData = "AAAAAA"
                If Val(DogData.Substring(4, 4)) >= 1415 Then
                    DogBytes = 8
                    DogData = "AAAAAAAA"
                Else
                    DogBytes = 6
                    DogData = "AAAAAA"
                End If
                'Ver 2.06-REQ291 end
                z = DogRead(DogBytes, DogAddr, DogData)
                strDogSrNo = DogData

                strDongleSrNoforBuying = strDogSrNo
                strExistingProduct = "ETDS" & ConstStrFYyr

                If CheckLicenseKey() = True Then
                    'Ver 3.01-REQ297 start
                    strDongleFyYr = ConstStrFYyr
                    'Ver 3.01-REQ297 end
                    blnDemoVersion = False
                    Return True
                End If
            End If

            DogAddr = 0
            DogBytes = 8
            DogData = "AAAAAAAA"

            Y = DogRead(DogBytes, DogAddr, DogData)
            If Mid(DogData, 1, 7) = "TdsPacE" Or Mid(DogData, 1, 8) = "TdsY" & ConstStrFYyr Or Mid(DogData, 1, 8) = "TdsPacMU" Or Mid(DogData, 1, 8) = "TdsYMU" & Mid(ConstStrFYyr, 1, 2) Then
                If Mid(DogData, 1, 7) = "TdsPacE" Then
                    strExistingProduct = "TdsPacE"
                ElseIf Mid(DogData, 1, 8) = "TdsPacMU" Then
                    strExistingProduct = "TdsPacMU"
                ElseIf Mid(DogData, 1, 8) = "TdsYMU" & Mid(ConstStrFYyr, 1, 2) Then
                    strExistingProduct = "TdsYMU" & Mid(ConstStrFYyr, 1, 2)
                Else
                    strExistingProduct = "TdsY" & ConstStrFYyr
                End If

                DogAddr = 60
                DogBytes = 5
                DogData = "AAAAA"
                z = DogRead(DogBytes, DogAddr, DogData)
                strDogSrNo = DogData
                strDongleSrNoforBuying = strDogSrNo

                If CheckLicenseKey() = True Then
                    'Ver 3.01-REQ297 start
                    strDongleFyYr = ConstStrFYyr
                    'Ver 3.01-REQ297 end
                    blnDemoVersion = False
                    Return True
                End If
            End If


            'For Paypac
            DogAddr = 10
            DogBytes = 6
            DogData = "AAAAAA"

            Y = DogRead(DogBytes, DogAddr, DogData)
            'Ver 3.05-REQ371 Start
            'If Mid(DogData, 1, 6) = "PAYPAC" Then
            If Mid(DogData, 1, 6) = "PayPac" Then

                'Ver 3.05-REQ371 End

                strExistingProduct = DogData
                DogAddr = 60
                DogBytes = 5
                DogData = "AAAAA"
                z = DogRead(DogBytes, DogAddr, DogData)
                strDogSrNo = DogData

                strDongleSrNoforBuying = strDogSrNo


                If CheckLicenseKey() = True Then
                    'Ver 3.01-REQ297 start
                    strDongleFyYr = ConstStrFYyr
                    'Ver 3.01-REQ297 end
                    blnDemoVersion = False
                    Return True
                End If
            End If

            'For FAMS
            DogAddr = 20
            DogBytes = 4
            DogData = "AAAA"

            Y = DogRead(DogBytes, DogAddr, DogData)
            'Ver 3.05-REQ371 Start
            'If Mid(DogData, 1, 4) = "FAMS" Then
            If Mid(DogData, 1, 4) = "Fams" Then
                'Ver 3.05-REQ371 end
                strExistingProduct = DogData

                DogAddr = 60
                DogBytes = 5
                DogData = "AAAAA"
                z = DogRead(DogBytes, DogAddr, DogData)
                strDogSrNo = DogData

                strDongleSrNoforBuying = strDogSrNo


                If CheckLicenseKey() = True Then
                    'Ver 3.01-REQ297 start
                    strDongleFyYr = ConstStrFYyr
                    'Ver 3.01-REQ297 end
                    blnDemoVersion = False
                    Return True
                End If
            End If

            'For SALTDS
            DogAddr = 30
            DogBytes = 6
            DogData = "AAAAAA"

            Y = DogRead(DogBytes, DogAddr, DogData)
            'Ver 3.05-REQ371 Start
            'If Mid(DogData, 1, 6) = "SALTDS" Then
            If Mid(DogData, 1, 6) = "SalTds" Then
                'Ver 3.05-REQ371 end
                strExistingProduct = DogData
                DogAddr = 60
                DogBytes = 5
                DogData = "AAAAA"
                z = DogRead(DogBytes, DogAddr, DogData)
                strDogSrNo = DogData

                strDongleSrNoforBuying = strDogSrNo


                If CheckLicenseKey() = True Then
                    'Ver 3.01-REQ297 start
                    strDongleFyYr = ConstStrFYyr
                    'Ver 3.01-REQ297 end
                    blnDemoVersion = False
                    Return True
                End If
            End If

            'Ver 3.05-REQ371 Start
            'For TDSPACSQL
            DogAddr = 80
            DogBytes = 10
            DogData = "AAAAAAAAAA"

            Y = DogRead(DogBytes, DogAddr, DogData)
            If Mid(DogData, 1, 8) = "TdsPacSQ" Then
                strExistingProduct = DogData

                DogAddr = 60
                DogBytes = 5
                DogData = "AAAAA"


                z = DogRead(DogBytes, DogAddr, DogData)
                strDogSrNo = DogData

                strDongleSrNoforBuying = strDogSrNo


                If CheckLicenseKey() = True Then
                    'Ver 3.01-REQ297 start
                    strDongleFyYr = ConstStrFYyr
                    'Ver 3.01-REQ297 end
                    blnDemoVersion = False
                    Return True
                End If

            End If

            'Ver 3.05-REQ371 end
        End If
        If strDemoMessage = "" Then
            If File.Exists(Application.StartupPath & "\DNFLicense.key") = True Then
                strDemoMessage = "Please attach USB Dongle to the USB Port."
            End If
        End If
        strDogSrNo = "Demo Version"
        blnDemoVersion = True
        Return False

    End Function
    Public Function CheckLicenseKey() As Boolean
        Try

            If File.Exists(Application.StartupPath & "\DNFLicense.key") = True Then
                Dim oleCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;data source=" & Application.StartupPath & "\DNFLicense.key; Jet Oledb:Database Password=Ffcs")
                Dim oleCmd As New OleDbCommand
                Dim objReader As OleDbDataReader
                oleCmd.CommandText = "select * from Licence"
                oleCmd.Connection = oleCon
                oleCon.Open()
                objReader = oleCmd.ExecuteReader()


                If objReader.HasRows Then
                    While objReader.Read()
                        'Ver 2.05-REQ272  start
                        'If strDogSrNo = Decrypt(objReader("DSrNo")) And ConstStrFYyr = Decrypt(objReader("FinYr")) And Decrypt(objReader("ProductName")) = "DNF" Then
                        'Ver 3.01-REQ303 start
                        'If strDogSrNo = Decrypt(objReader("DSrNo")) And ConstStrFYyr <= Decrypt(objReader("FinYr")) And Decrypt(objReader("ProductName")) = "DNF" Then
                        Dim strDongSrno As String = ""
                        If strDogSrNo.Length = 8 Then
                            strDongSrno = strDogSrNo.Substring(0, 7)
                        Else
                            strDongSrno = strDogSrNo
                        End If
                        If strDongSrno = Decrypt(objReader("DSrNo")) And ConstStrFYyr <= Decrypt(objReader("FinYr")) And Decrypt(objReader("ProductName")) = "DNF" Then
                            'Ver 3.01-REQ303 end
                            'Ver 2.05-REQ272  end
                            CheckLicenseKey = True
                            Exit Function
                        End If
                    End While
                End If
                oleCon.Close()
                strDemoMessage = "Dongle serial number is not matching with DNFLicense."
                CheckLicenseKey = False
            Else

                'download Key File From WEB
                Try
                    frmLicenseVerifications.Show()
                    Application.DoEvents()

                    'If My.Computer.Network.Ping("http://www.ffcs.in") = True Then
                    Dim smsurl As String = "http://www.ffcs.in/dnflic/GetRequest.aspx"
                    'Ver 3.01-REQ303 start
                    'Dim smsRequest As WebRequest = WebRequest.Create(smsurl & "?licno=" & strDogSrNo & "&typ=SW")
                    Dim smsRequest As WebRequest
                    Dim strfYr As String = "20" & Mid(ConstStrFYyr, 1, 2) & "-20" & Mid(ConstStrFYyr, 3, 2)
                    If strDogSrNo.Length = 8 Then
                        smsRequest = WebRequest.Create(smsurl & "?licno=" & strDogSrNo.Substring(0, 7) & "&typ=SW&dglicYr=" & strfYr)
                    Else
                        smsRequest = WebRequest.Create(smsurl & "?licno=" & strDogSrNo & "&typ=SW&dglicYr=" & strfYr)
                    End If
                    'Ver 3.01-REQ303 end
                    Dim resp As WebResponse = smsRequest.GetResponse()
                    Dim respStream As System.IO.Stream = resp.GetResponseStream()
                    Dim read As New StreamReader(respStream)
                    Dim msgresp As String = read.ReadToEnd()

                    If msgresp = "true" Then
                        'Ver 3.01-REQ303 start
                        'GenerateKey(strDogSrNo)
                        If strDogSrNo.Length = 8 Then
                            GenerateKey(strDogSrNo.Substring(0, 7))
                        Else
                            GenerateKey(strDogSrNo)
                        End If
                        CheckLicenseKey = True
                        ''Ver 3.1-REQ303 start 
                        'blnIsProductRegistered = True
                        ''Ver 3.1-REQ303 end
                    Else
                        CheckLicenseKey = False
                    End If
                    respStream.Dispose()
                    read.Close()
                    read.Dispose()
                    'End If
                    frmLicenseVerifications.Close()
                    Application.DoEvents()
                Catch ex As System.InvalidOperationException
                    frmLicenseVerifications.Close()
                    CheckLicenseKey = False
                Catch ex As Exception
                    frmLicenseVerifications.Close()
                    CheckLicenseKey = False
                End Try
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")
            CheckLicenseKey = False
        End Try
    End Function
    Function Decrypt(ByVal OutPutTxt As String)
        Dim ctr As Double
        Dim str As String
        str = ""
        For ctr = Len(OutPutTxt) To 1 Step -1

            str = str & Chr(Asc(Mid(OutPutTxt, ctr, 1)) - (30))
        Next ctr
        Decrypt = str
    End Function

    Public Function Encrypt(ByVal InputTxt As String) As String

        Dim functionReturnValue As String = ""
        Dim ctr As Integer = 0
        For ctr = Len(InputTxt) To 1 Step -1
            functionReturnValue = functionReturnValue & Convert.ToChar(Convert.ToInt32(Convert.ToChar(InputTxt.Substring(ctr - 1, 1))) + 30)
        Next
        Return functionReturnValue

    End Function
    Public Sub GenerateKey(ByVal strSrno As String)

        File.Copy(Application.StartupPath & "\Interop.DNF.dll", Application.StartupPath & "\DNFLicense.key")
        Dim cmd As New OleDbCommand
        Dim con As New OleDbConnection
        Dim strNewSrno As String
        Dim strFY As String
        Dim strProduct As String
        con.ConnectionString = "PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source = " & Application.StartupPath & "\DNFLicense.key; Jet Oledb:Database Password=Ffcs"
        con.Open()

        cmd = New OleDb.OleDbCommand("Create Table Licence (DSrNo  Text,FinYr text,ProductName Memo)", con)
        cmd.ExecuteNonQuery()
        strNewSrno = Encrypt(strSrno)
        strFY = Encrypt(ConstStrFYyr)
        strProduct = Encrypt("DNF")
        cmd = New OleDb.OleDbCommand("Insert into Licence(DSrNo,FinYr,ProductName) values ('" & strNewSrno & "','" & strFY & "','" & strProduct & "')", con)
        cmd.ExecuteNonQuery()
        con.Close()

    End Sub

    'Public Sub GetProxy()

    '    Dim iwp As System.Net.IWebProxy = System.Net.WebRequest.GetSystemWebProxy()
    '    Dim wp As Uri = iwp.GetProxy(New Uri("http://www.ffcs.in"))
    '    MessageBox.Show(wp.AbsoluteUri.ToString())

    'End Sub

    Public Sub FillDataForSalary(ByRef dtTemp As DataTable, ByRef value() As String, ByVal ColCount As Integer)
        Try
            dr = dtTemp.NewRow
            Dim i As Integer
            i = 0
            For j = 0 To ColCount - 1

                If value(i).ToString = "" Then   ' For Empty String according to data type assign 0 OR blank
                    If (dtTemp.Columns(j).DataType.ToString = "System.Decimal") Or (dtTemp.Columns(j).DataType.ToString = "System.Int32") Or (dtTemp.Columns(j).DataType.ToString = "System.Int64") Then
                        dr(j) = 0
                    ElseIf (dtTemp.Columns(j).DataType.ToString = "System.Char") Or (dtTemp.Columns(j).DataType.ToString = "System.String") Then
                        dr(j) = ""
                    End If
                Else    ' For Uint DataType Convert String Type data into Integer before assigning to datarow
                    If (dtTemp.Columns(j).DataType.ToString = "System.UInt32") Or (dtTemp.Columns(j).DataType.ToString = "System.UInt16") Then
                        dr(j) = CInt(value(i))
                    Else
                        dr(j) = value(i)
                    End If
                End If
                i = i + 1

                If j = 14 Or j = 22 Then  ' Column no 15 & 16 for Section16 Details and 23 & 24 for ChapterVIA
                    j = j + 2
                End If
                If strFinYear >= "202021" And j = 71 Then dr(j) = value(78)
            Next j

            dtTemp.Rows.Add(dr)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF")
        End Try
    End Sub

    Public Sub Tmp(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles pIE1.DocumentCompleted
        'Ver 4.042-QC?? start
        ' pIE1.ScriptErrorsSuppressed = True

        'Ver 4.042-QC?? end
        DocComplete = True
    End Sub

    Public Sub CheckPANWithExistingList()
        Dim fs As FileStream
        Dim strRd As StreamReader
        Dim mCode As String = ""

        Try
            'Ver 4.042-QC?? start
            If File.Exists(Application.StartupPath & "\Interop.VPL.dll") Then
                'FileSystem.Rename(Application.StartupPath & "\Interop.VPL.dll", Application.StartupPath & "\Interop.VPL.sys")
                FileSystem.Rename(Application.StartupPath & "\Interop.VPL.dll", Application.StartupPath & "\Interop.VPL.txt")
            End If
            'Ver 4.042-QC?? end

            If File.Exists(strValidPANListFilePath) Then
                fs = New FileStream(strValidPANListFilePath, FileMode.Open, FileAccess.Read)
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
    End Sub



    Public Sub PANVerificationLatest()
        'PAN Status Values
        'V= Valid
        'I= InValid
        'X= Exceptional Error Occured during verification / Skipped Verification
        'N=Not To be Checked


        blnPANDelegate = False
        Dim intTempPANCount As Integer
        Dim intCount As Integer
        Dim dtTempView As New DataView(dsMain.Tables("DeducteeDetails"))
        Dim i As Long
        Dim strVal As String = ""

        Dim strValidPANList As New StringBuilder
        Dim fs As FileStream
        Dim fs1 As FileStream
        Dim strWr As StreamWriter
        Dim strRd As StreamReader

        Dim strIngnoredPANList As New StringBuilder
        intIgnoredPANList = 0


        Dim isValid As Boolean
        Dim strtAssessmentYear As String

        strtAssessmentYear = Mid(dtBH.Rows(0)("Fin Yr"), 1, 4) & "-" & Mid(dtBH.Rows(0)("Fin Yr"), 5)

        frmProcess.lblPanProcess.Text = ""
        frmProcess.lblPanStatus.Text = "Status  : " & "Wait......."
        Application.DoEvents()

        intCountOfValidPAN = 0

        dtTempView.Sort = "PAN"
        dtTempView.RowFilter = "IsPANValidationReq = True"
        Application.DoEvents()
        dtTempPAN = dtTempView.ToTable("PANValid", True, "PAN") ' For selecting Distinct PAN into data table
        Application.DoEvents()
        dtTempView.RowFilter = ""

        Application.DoEvents()
        CheckPANWithExistingList()
        Application.DoEvents()

        intCntInvalidPan = 0
        intTempPANCount = dtTempPAN.Rows.Count
        intCount = dtTempPAN.Rows.Count

        strPanProcess = ""
        frmProcess.lblPanProcess.Text = ""
        frmProcess.lblPanStatus.Text = "Status  : " & "Verifying Internet connection..."

        Try


            For i = 0 To intTempPANCount - 1
                strVal = ""

                If i = 0 Then
                    strPanStatus = "Verifying Internet connection..."
                    frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
                End If

                Try
                    'Ver 3.0.7-QC??? start
                    'ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    'Ver 3.0.7-QC??? end
                    isValid = False
                    Dim response As getITRVStatusResponse = (New itrvstatusPortClient()).getITRVStatus(New getITRVStatusRequest() With {.PAN = dtTempPAN.Rows(0)(0).ToString(), .AssessmentYear = strtAssessmentYear})
                    isValid = True

                    If i = 0 Then
                        strPanStatus = "Processing..."
                        frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
                    End If


                Catch ex1 As System.ServiceModel.FaultException

                    If i = 0 Then
                        strPanStatus = "Processing..."
                        frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
                    End If


                    If ex1.Message = "Please provide a PAN" OrElse ex1.Message = "Invalid PAN. Please retry." OrElse ex1.Message = "PAN does not exist." Then
                        isValid = False
                    ElseIf ex1.Message = "No e-Return has been filed for this PAN and Assessment Year." Then
                        isValid = True
                    Else
                        isValid = True
                    End If

                Catch ex2 As Exception
                    If i = 0 Then
                        strPanStatus = "Processing..."
                        frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
                    End If
                    If File.Exists(Application.StartupPath & "\DNFDebug.txt") = True Then
                        If MessageBox.Show(ex2.Message & vbCrLf & "Do you want to proceed?", "DNF", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
                            Exit For
                        End If
                    End If
                    'Ver 2.04-?? start
                    If ex2.InnerException.Message = "The remote server returned an error: (500) Internal Server Error." Then
                        isValid = False
                        GoTo takeAction
                    End If
                    'Ver 2.04-?? end

                    'Ver 3.04-?? start
                    'Dim webex As WebException = TryCast(ex2.InnerException, WebException)
                    'Dim obj As Object = webex
                    'If Not IsNothing(obj) Then
                    'Ver 3.04-?? end
                    intIgnoredPANList = intIgnoredPANList + 1
                    Application.DoEvents()
                    strIngnoredPANList.Append(dtTempPAN.Rows(0)("PAN").ToString & vbCrLf)
                    dtTempView.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "' And IsPANValidationReq = True"
                    For l = 0 To dtTempView.Count - 1
                        dtTempView.Item(l)("PANStatus") = "X"
                    Next
                    dtTempPAN.Rows(0).Delete()

                    strPanProcess = "Verifying PAN's : " & i + 1 & " of " & intCount
                    frmProcess.lblPanProcess.Text = strPanProcess
                    Application.DoEvents()
                    Continue For
                    'Ver 3.04-?? start
                    'End If
                    'Ver 3.04-?? end
                End Try
takeAction:


                If isValid = True Then
                    intCountOfValidPAN = intCountOfValidPAN + 1
                    strValidPANList.Append("," & dtTempPAN.Rows(0)("PAN").ToString)
                    dtTempPAN.Rows(0).Delete()
                Else
                    intCntInvalidPan = intCntInvalidPan + 1
                    dtTempView.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "' And IsPANValidationReq = True"
                    For l = 0 To dtTempView.Count - 1
                        dtTempView.Item(l)("PANStatus") = "I"
                        dtTempView.Item(l)("DeducteeStatus") = "Z" '4th Character Updated to 'Z' For Invalid PAN
                    Next
                    dtTempPAN.Rows(0).Delete()
                End If
                strPanProcess = "Verifying PAN's : " & i + 1 & " of " & intCount
                frmProcess.lblPanProcess.Text = strPanProcess
                Application.DoEvents()

            Next
            Try


                ''Store Valid PAN List
                If strValidPANList.Length > 0 Then
                    If File.Exists(strValidPANListFilePath) = False Then
                        Dim f As FileStream = File.Create(strValidPANListFilePath)
                        f.Close()
                        f.Dispose()
                    End If

                    If File.ReadAllText(strValidPANListFilePath).Length = 0 Then
                        strValidPANList = New StringBuilder(strValidPANList.ToString.Substring(1))
                    End If

                    fs = New FileStream(strValidPANListFilePath, FileMode.Append, FileAccess.Write)
                    strWr = New StreamWriter(fs)

                    strWr.Write(strValidPANList.ToString)
                    strWr.Flush()
                    strWr.Close()
                    strWr.Dispose()

                    fs.Dispose()
                End If
                ''Store Ignored PAN List
                If strIngnoredPANList.Length > 0 Then
                    If File.Exists(strIgnoredPANListFilePath) = False Then
                        Dim f As FileStream = File.Create(strIgnoredPANListFilePath)
                        f.Close()
                        f.Dispose()
                    End If

                    File.WriteAllText(strIgnoredPANListFilePath, strIngnoredPANList.ToString)
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
                blnPANDelegate = True
                blnPANVerification = False
            End Try
            blnPANDelegate = True
            blnPANVerification = False

        Catch ex As Exception
            blnPANDelegate = True
            blnPANVerification = True
        End Try
    End Sub

    'Ver 1.01-E585 Start
    Public Sub CheckCustomerRegistration()
        Try
            If File.Exists(strActivationFileName) = True Then
                Dim cmd As New OleDbCommand
                Dim con As New OleDbConnection
                Dim objReader As OleDbDataReader

                Dim strLicenceNo As String
                Dim strFY As String
                Dim strProduct As String

                strLicenceNo = Encrypt(strDogSrNo)
                strFY = Encrypt(ConstStrFYyr)
                strProduct = Encrypt("DNF")

                con.ConnectionString = "PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source = " & strActivationFileName & "; Jet Oledb:Database Password=Ffcs"
                'Ver 2.05-REQ272  start
                'cmd.CommandText = "select * from Licence Where DsrNo= '" & strLicenceNo & "' and FinYr ='" & strFY & "' and ProductName = '" & strProduct & "'"
                cmd.CommandText = "select FinYr from Licence Where DsrNo= '" & strLicenceNo & "' and ProductName = '" & strProduct & "'"
                'Ver 2.05-REQ272 end
                cmd.Connection = con
                con.Open()
                objReader = cmd.ExecuteReader()
                If objReader.HasRows = True Then
                    'Ver 2.05-REQ272  start
                    'blnIsProductRegistered = True
                    objReader.Read()
                    If Val(Decrypt(objReader.Item(0).ToString)) >= Val(ConstStrFYyr) Then
                        blnIsProductRegistered = True
                    Else
                        blnIsProductRegistered = False
                    End If
                    'Ver 2.05-REQ272  end
                Else
                    blnIsProductRegistered = False
                End If
                con.Close()

                Exit Sub
            End If

            blnIsProductRegistered = False

            'Ver 2.03-QC355 start
            'Catch 
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'Ver 2.03-QC355 End
            blnIsProductRegistered = False
        End Try
    End Sub
    'Ver 1.01-E585 End
    '

    'Ver 2.02-REQ235 start
    Public Sub LateFiling()
        dtLateFiling.Columns.Add("Quarter", System.Type.GetType("System.String"))
        dtLateFiling.Columns.Add("FormNo", System.Type.GetType("System.String"))
        dtLateFiling.Columns.Add("DeductorType", System.Type.GetType("System.String"))
        dtLateFiling.Columns.Add("DueDate", System.Type.GetType("System.String"))
        dtLateFiling.Columns.Add("Fee", System.Type.GetType("System.String"))
    End Sub
    'Ver 2.02-REQ235 end

    'Ver 2.02-REQ235 start
    Public Function DayDifference(ByVal FirstDate As String, ByVal SecondDate As String) As Integer
        Dim newdate1 As Date
        Dim newdate2 As Date
        Dim intdifference As Integer

        If FirstDate = SecondDate Then
            Return 0
        End If

        If IsDate(FirstDate) = False Or IsDate(SecondDate) = False Then
            Return 0
        End If


        newdate1 = FirstDate
        newdate2 = SecondDate

        intdifference = DateDiff(DateInterval.Day, newdate1, newdate2)
        If intdifference < 0 Then Return 0

        Return intdifference

    End Function
    'Ver 2.02-REQ235 end
    'Ver 3.01-REQ297 start
    Public Sub CheckNewCustomerRegistration()
        Try
            blnIsProductRegistered = False
            If File.Exists(strActivationFileName) = False Then
                If File.Exists("C:\eTDS" & ConstStrFYyr & "\Interop.eTds.Config") = True Then
                    File.Copy("C:\eTDS" & ConstStrFYyr & "\Interop.eTds.Config", strActivationFileName)
                    Call CheckRegistrationValue()
                End If
            Else
                Call CheckRegistrationValue()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            blnIsProductRegistered = False
        End Try
    End Sub
    'Ver 3.01-REQ297 end
    'Ver 3.01-REQ297 start
    Public Function EncryptNew(ByVal InputTxt As String) As String
        Dim strPd As String = ""
        Dim functionReturnValue As String = ""
        Dim ctr As Integer = 0
        For ctr = Len(InputTxt) To 1 Step -1
            strPd = ""
            strPd = strPd & Convert.ToInt32((Convert.ToInt32(Convert.ToChar(InputTxt.Substring(ctr - 1, 1)))) - 10)
            functionReturnValue = functionReturnValue & strPd.PadLeft(3, "0").ToString()
        Next
        Return functionReturnValue

    End Function
    'Ver 3.01-REQ297 end
    'Ver 3.01-REQ297 start
    Public Sub CheckRegistrationValue()

        Dim StrProduct As String = ""
        Dim strFinYr As String = ""
        Dim strLine As String = ""

        Dim strLicenceNo As String = ""
        Dim strFY As String = ""

        Dim strSr As String = ""
        Dim strPrd As String = ""
        Dim strFYr1 As String = ""

        strLine = FileIO.FileSystem.ReadAllText(strActivationFileName).Trim

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

        strSr = ""
        strPrd = ""
        strFYr1 = ""

        If strDogSrNo.Length <> "8" Then
            strSr = Mid(strLicenceNo, 11, Len(strLicenceNo) - 10) & Mid(strFinYr, 1, 6) & Mid(StrProduct, 1, 6)
            strPrd = Mid(StrProduct, 1, Len(StrProduct) - 6) & Mid(strLicenceNo, 1, 5)
            strFYr1 = Mid(strFinYr, 1, Len(strFinYr) - 6) & Mid(strLicenceNo, 6, 5)
        Else
            strSr = Mid(strLicenceNo, 19, Len(strLicenceNo) - 18) & Mid(strFinYr, 1, 6) & Mid(StrProduct, 1, 6)
            strPrd = Mid(StrProduct, 1, Len(StrProduct) - 6) & Mid(strLicenceNo, 1, 9)
            strFYr1 = Mid(strFinYr, 1, Len(strFinYr) - 6) & Mid(strLicenceNo, 10, 9)
        End If


        If CStr(strLine) <> CStr(strFYr1 & "-" & strPrd & "-" & strSr) Then
            blnIsProductRegistered = False
        Else
            blnIsProductRegistered = True
        End If
        'Ver 3.01-REQ297 end
    End Sub



    'Ver 3.06-???? start
    Public Sub CallProcessAndWait(ByVal ProcessPath As String, Optional ByVal CommandLine As String = "")
        Dim objProcess As System.Diagnostics.Process
        ' Try
        objProcess = New System.Diagnostics.Process()
        objProcess.StartInfo.FileName = ProcessPath
        objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        objProcess.StartInfo.Arguments = CommandLine
        objProcess.Start()

        'Wait until the process passes back an exit code 
        objProcess.WaitForExit()

        'Free resources associated with this process
        objProcess.Close()
        'Catch
        '    ' MessageBox.Show("Could not start process " & ProcessPath, "DNF")

        'End Try
    End Sub
    'Ver 3.06-???? End

    Public Sub ReadCustomerCode()

        Try

            If (File.Exists(strLICXMLFile)) Then

                Using dsLicense = New DataSet()

                    dsLicense.ReadXml(strLICXMLFile)
                    strCustomerCode = dsLicense.Tables(0).Rows(0)("CustomerCode").ToString()
                End Using

            Else

                strCustomerCode = ""
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Ver 4.041-QC?? start
    Public Sub ReadExcelPANVerification(ExcelFileName As String)
        'PAN Status Values
        'V= Valid
        'I= InValid
        'X= Exceptional Error Occured during verification / Skipped Verification
        'N=Not To be Checked


        blnPANDelegate = False
        Dim intTempPANCount As Integer
        Dim intCount As Integer
        Dim dtTempView As New DataView(dsMain.Tables("DeducteeDetails"))


        intIgnoredPANList = 0


        Dim strtAssessmentYear As String

        strtAssessmentYear = Mid(dtBH.Rows(0)("Fin Yr"), 1, 4) & "-" & Mid(dtBH.Rows(0)("Fin Yr"), 5)

        frmProcess.lblPanProcess.Text = ""
        frmProcess.lblPanStatus.Text = "Status  : " & "Wait......."
        Application.DoEvents()


        intCountOfValidPAN = 0
        dtTempView.Sort = "PAN"
        dtTempView.RowFilter = "IsPANValidationReq = True"
        Application.DoEvents()
        dtTempPAN = dtTempView.ToTable("PANValid", True, "PAN") ' For selecting Distinct PAN into data table
        Application.DoEvents()
        dtTempView.RowFilter = ""

        Application.DoEvents()

        intCntInvalidPan = 0
        intTempPANCount = dtTempPAN.Rows.Count
        intCount = dtTempPAN.Rows.Count

        strPanProcess = ""
        frmProcess.lblPanProcess.Text = ""
        frmProcess.lblPanStatus.Text = "Status  : " & "Verifying Internet connection..."

        Dim APP As New Excel.Application
        Dim worksheet As Excel.Worksheet
        Dim workbook As Excel.Workbook
        Dim j As Integer

        Call GetExcelProcess()

        workbook = APP.Workbooks.Open(ExcelFileName)
        worksheet = workbook.Worksheets("PAN Verification")

        For j = 5 To worksheet.Rows.Count - 1
            If worksheet.Cells(j, 1).Value <> "" Then
                dtTempView.RowFilter = "PAN = '" & worksheet.Cells(j, 1).Value() & "' And IsPANValidationReq = True"
                For l = 0 To dtTempView.Count - 1
                    If worksheet.Cells(j, 3).Value() = "Not Available" Then
                        dtTempView.Item(l)("PANStatus") = "I"
                        dtTempView.Item(l)("DeducteeStatus") = "Z" '4th Character Updated to 'Z' For Invalid PAN
                    End If
                Next

            Else
                Exit For
            End If
        Next

        workbook.Close()
        APP.Workbooks.Close()
        APP.Quit()
        Call KillProcess()

    End Sub
    'Public Sub FillValidPanList(strValidPANList As StringBuilder, strValidPANListFilePath As String)
    '    Dim fss As FileStream
    '    Dim strWr As StreamWriter

    '    If strValidPANList.Length > 0 Then
    '        If File.Exists(strValidPANListFilePath) = False Then
    '            Dim f As FileStream = File.Create(strValidPANListFilePath)
    '            f.Close()
    '            f.Dispose()
    '        End If

    '        If File.ReadAllText(strValidPANListFilePath).Length = 0 Then
    '            strValidPANList = New StringBuilder(strValidPANList.ToString.Substring(1))
    '        End If

    '        fss = New FileStream(strValidPANListFilePath, FileMode.Append, FileAccess.Write)
    '        strWr = New StreamWriter(fss)

    '        strWr.Write(strValidPANList.ToString)
    '        strWr.Flush()
    '        strWr.Close()
    '        strWr.Dispose()

    '        fss.Dispose()
    '    End If
    'End Sub
    'Ver 4.041-QC?? end
    'Ver 4.042-QC?? start
    Public Sub PanVerification()
        ''Dim dtTempView As New DataView(dsMain.Tables("DeducteeDetails"))
        ''Dim blnValidPan As Boolean = False
        ''Dim intTempPANCount As Integer
        ''Dim intCount As Integer
        ''Dim intCountComplete As Integer

        ''frmProcess.lblPanStatus.Text = "Status  : " & "Wait......."
        ''dtTempView.Sort = "PAN"
        ''dtTempView.RowFilter = "IsPANValidationReq = True"
        ''Application.DoEvents()
        ''dtTempPAN = dtTempView.ToTable("PANValid", True, "PAN") ' For selecting Distinct PAN into data table
        ''Application.DoEvents()
        ''intTempPANCount = dtTempPAN.Rows.Count
        ''intCount = dtTempPAN.Rows.Count
        ''dtTempView.RowFilter = ""

        ''Application.DoEvents()

        ''For i = 0 To intTempPANCount - 1

        ''    blnValidPan = PanValidation(dtTempPAN.Rows(i)(0).ToString())
        ''    dtTempView.RowFilter = "PAN = '" & dtTempPAN.Rows(i)(0).ToString() & "' And IsPANValidationReq = True"

        ''    For l = 0 To dtTempView.Count - 1
        ''        If blnValidPan = False Then
        ''            dtTempView.Item(l)("PANStatus") = "I"
        ''            dtTempView.Item(l)("DeducteeStatus") = "Z" '4th Character Updated to 'Z' For Invalid PAN
        ''        End If
        ''    Next
        ''    intCountComplete = intCountComplete + 1
        ''    strPanProcess = "Verifying PAN's : " & intCountComplete & " of " & intCount
        ''    frmProcess.lblPanProcess.Text = strPanProcess
        ''Next
        ''dtTempPAN.Rows.Clear()

        'PAN Status Values
        'V= Valid
        'I= InValid
        'X= Exceptional Error Occured during verification / Skipped Verification
        'N=Not To be Checked


        blnPANDelegate = False
        Dim intTempPANCount As Integer
        Dim intCount As Integer
        'Dim intCountValidPAN As Integer 'Changes for creation new itdConfig.txt
        Dim dtTempView As New DataView(dsMain.Tables("DeducteeDetails"))
        Dim dtTempViewSalaryDetails As New DataView(dsMain.Tables("SalaryDetails"))

        Dim i As Long
        Dim strVal As String = ""
        Dim intPANStatus As Integer
        Dim strValidPANList As New StringBuilder
        Dim fs As FileStream
        Dim strWr As StreamWriter
        Dim strIngnoredPANList As New StringBuilder

        'intCountValidPAN = 0 'Changes for creation new itdConfig.txt

        intIgnoredPANList = 0


        Dim isValid As Boolean
        Dim strtAssessmentYear As String

        strtAssessmentYear = Mid(dtBH.Rows(0)("Fin Yr"), 1, 4) & "-" & Mid(dtBH.Rows(0)("Fin Yr"), 5)

        frmProcess.lblPanProcess.Text = ""
        frmProcess.lblPanStatus.Text = "Status  : " & "Wait......."
        Application.DoEvents()

        intCountOfValidPAN = 0
        dtTempView.Sort = "PAN"
        dtTempView.RowFilter = "IsPANValidationReq = True"
        Application.DoEvents()
        dtTempPAN = dtTempView.ToTable("PANValid", True, "PAN") ' For selecting Distinct PAN into data table


        'Date:06/07/2020, Name:Bhaskaran N, Description: To Megre Distinct PAN into DD from SD for Validation <start>
        dtTempViewSalaryDetails.Sort = "PAN"
        dtTempViewSalaryDetails.RowFilter = " PAN <> 'PANNOTAVBL' " 'Ver 7.03-REQ816 start
        Dim dt As DataTable = dtTempViewSalaryDetails.ToTable("PANValid", True, "PAN")
        If dt.Rows.Count > 0 Then
            dtTempPAN.Merge(dt)
            dtTempPAN = dtTempPAN.DefaultView.ToTable(True, "PAN")
        End If
        'Date:06/07/2020 <end>

        'Ver 7.03-REQ816 start
        'Selecting the salary Pan Details and merge into the existing dtTempPAN
        '  If (dtTempSalary.Item(k).Row("PAN").ToString() = "PANINVALID" Or dtTempSalary.Item(k).Row("PAN").ToString() = "PANNOTAVBL" Or dtTempSalary.Item(k).Row("PAN").ToString() = "PANAPPLIED") Then
        '.RowFilter = "PAN <>'PANINVALID' OR PAN <>'PANNOTAVBL'OR PAN <>'PANAPPLIED' "
        'dtTempViewSalaryDetails.RowFilter = " PAN <>'PANNOTAVBL' "
        'dtTempPANSalary = dtTempViewSalaryDetails.ToTable("PANValid", True, "PAN") ' ' For selecting Distinct PAN of salary table into data table
        'dtTempPAN.Merge(dtTempPANSalary)
        'Ver 7.03-REQ816 end 

        Application.DoEvents()
        dtTempView.RowFilter = ""

        Application.DoEvents()

        If File.Exists(strValidPANListFilePath) Then
            File.Delete(strValidPANListFilePath)
        End If
        CheckPANWithExistingList()
        Application.DoEvents()

        intCntInvalidPan = 0
        intTempPANCount = dtTempPAN.Rows.Count
        intCount = dtTempPAN.Rows.Count

        'Ver 8.0.0.3 start
        If File.Exists(Application.StartupPath & "\Thread.txt") = False Then
            Dim strFilePath As String
            strFilePath = Application.StartupPath & "\Thread.txt"

            Dim fsThs As FileStream
            Dim strWrThs As StreamWriter

            fsThs = New FileStream(strFilePath, FileMode.Append, FileAccess.Write)
            strWrThs = New StreamWriter(fsThs)

            strWrThs.Write("2")
            strWrThs.Flush()
            strWrThs.Close()
            strWrThs.Dispose()

            fsThs.Dispose()
        End If

        strThreadSleepValue = File.ReadAllText(Application.StartupPath & "\Thread.txt")
        strThreadSleepValue = strThreadSleepValue + "000"
        'Ver 8.0.0.3 End

        strPanProcess = ""
        frmProcess.lblPanProcess.Text = ""
        frmProcess.lblPanStatus.Text = "Status  : " & "Verifying Internet connection..."

        Try

            For i = 0 To intTempPANCount - 1
                strVal = ""
                'Ver 8.002 - Id '0129703' 12/07/19  Start
                'cp.WebBrowserStart()
                'Ver 8.002 - Id '0129703' 12/07/19  End

                If i = 0 Then
                    strPanStatus = "Verifying Internet connection..."
                    frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
                End If

                Try
                    'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    isValid = False
                    intPANStatus = 0
                    intPANStatus = NewPanValidation(dtTempPAN.Rows(0)(0).ToString())
                    'intPANStatus = PanValidation(dtTempPAN.Rows(0)(0).ToString())
                    If intPANStatus = 1 Then
                        isValid = True
                    ElseIf intPANStatus = 0 Then
                        isValid = False
                    ElseIf intPANStatus = 2 Then
                        isValid = False
                        Exit For
                        Dim intError As Integer = 0
                        Dim intIgnore As Integer = (1 / intError)
                        'Ver 8.002 - Id '0129703' 12/07/19  Start    
                        'Else
                        '    MessageBox.Show("Program will Terminate!!!")
                        '    Exit For
                        'Ver 8.002 - Id '0129703' 12/07/19  Start
                    End If

                    If i = 0 Then
                        strPanStatus = "Processing..."
                        frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
                    End If


                Catch ex1 As System.ServiceModel.FaultException

                    If i = 0 Then
                        strPanStatus = "Processing..."
                        frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
                    End If


                    If ex1.Message = "Please provide a PAN" OrElse ex1.Message = "Invalid PAN. Please retry." OrElse ex1.Message = "PAN does not exist." Then
                        isValid = False
                    ElseIf ex1.Message = "No e-Return has been filed for this PAN and Assessment Year." Then
                        isValid = True
                    Else
                        isValid = True
                    End If

                Catch ex2 As Exception

                    If i = 0 Then
                        strPanStatus = "Processing..."
                        frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
                    End If
                    If File.Exists(Application.StartupPath & "\DNFDebug.txt") = True Then
                        If MessageBox.Show(ex2.Message & vbCrLf & "Do you want to proceed?", "DNF", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
                            Exit For
                        End If
                    End If

                    'If ex2.InnerException.Message = "The remote server returned an error: (500) Internal Server Error." Then
                    'If intPANStatus = 2 Then
                    '    isValid = False
                    '    GoTo takeAction
                    'End If

                    intIgnoredPANList = intIgnoredPANList + 1
                    Application.DoEvents()
                    strIngnoredPANList.Append(dtTempPAN.Rows(0)("PAN").ToString & vbCrLf)
                    dtTempView.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "' And IsPANValidationReq = True"
                    For l = 0 To dtTempView.Count - 1
                        dtTempView.Item(l)("PANStatus") = "X"
                    Next
                    dtTempPAN.Rows(0).Delete()

                    strPanProcess = "Verifying PAN's : " & i + 1 & " of " & intCount
                    frmProcess.lblPanProcess.Text = strPanProcess
                    Application.DoEvents()
                    Continue For
                End Try
takeAction:
                If isValid = True Then
                    intCountOfValidPAN = intCountOfValidPAN + 1
                    strValidPANList.Append("," & dtTempPAN.Rows(0)("PAN").ToString)

                    For J = 0 To dsMain.Tables("SalaryDetails").Rows.Count - 1
                        If dsMain.Tables("SalaryDetails").Rows(J)("PAN").ToString = dtTempPAN.Rows(0)("PAN").ToString Then
                            dsMain.Tables("SalaryDetails").Rows(J)("PANValid") = "Y"
                        End If
                    Next
                    dtTempPAN.Rows(0).Delete()
                    'intCountValidPAN = intCountValidPAN + 1 'Changes for creation new itdConfig.txt
                    'If (intCountValidPAN = 5) Then
                    '    FillValidPanList(strValidPANList, strValidPANListFilePath) 'GoTo lblFillInInteropVPLsys
                    '    intCountValidPAN = 0
                    'End If
                Else
                    intCntInvalidPan = intCntInvalidPan + 1
                    dtTempView.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "' And IsPANValidationReq = True"
                    For l = 0 To dtTempView.Count - 1
                        dtTempView.Item(l)("PANStatus") = "I"
                        dtTempView.Item(l)("DeducteeStatus") = "Z" '4th Character Updated to 'Z' For Invalid PAN
                    Next

                    For J = 0 To dsMain.Tables("SalaryDetails").Rows.Count - 1
                        If dsMain.Tables("SalaryDetails").Rows(J)("PAN").ToString = dtTempPAN.Rows(0)("PAN").ToString Then
                            dsMain.Tables("SalaryDetails").Rows(J)("PANValid") = "N"
                        End If
                    Next
                    dtTempPAN.Rows(0).Delete()
                End If
                strPanProcess = "Verifying PAN's : " & i + 1 & " of " & intCount
                frmProcess.lblPanProcess.Text = strPanProcess
                Application.DoEvents()
                'Ver 8.002 - Id '0129703' 12/07/19  Start
                'cp.WebBrowserClose()
                'Ver 8.002 - Id '0129703' 12/07/19  End
            Next
            '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            ' Started for Salary
            '==>Jitendra SalaryDetails
            'Ver 5.05-REQ641 start
            Try
                'Date:06/07/2020, Name:Bhaskaran N, Description: Already PAN for SD validated Above <start>
                GoTo skipSD
                'Date:06/07/2020 <End>
                Dim dtTempviewSalary As New DataView(dsMain.Tables("SalaryDetails"))
                strtAssessmentYear = Mid(dtBH.Rows(0)("Fin Yr"), 1, 4) & "-" & Mid(dtBH.Rows(0)("Fin Yr"), 5)
                dtTempviewSalary.Sort = "PAN"
                'dtTempviewSalary.RowFilter = "Filler4='N'"
                'dtTempviewSalary.RowFilter = " PAN <>'PANNOTAVBL' " 'Ver 7.03-REQ816 start
                dtTempPAN = dtTempviewSalary.ToTable("PANValid", True, "PAN")
                Application.DoEvents()

                'CheckPANWithExistingList()
                'Application.DoEvents()
                'Ver 7.03-REQ816 start
                If File.Exists(strValidPANListFilePath) Then
                    Dim dataviewPAN As New DataView(dtTempPAN)
                    For i = 0 To strPANs.Length - 1
                        dataviewPAN.RowFilter = "PAN = '" & strPANs(i) & "'"
                        If dataviewPAN.Count > 0 Then
                            dataviewPAN.Item(0).Delete()
                        End If
                    Next
                End If
                'Ver 7.03-REQ816 end 

                intTempPANCount = dtTempPAN.Rows.Count
                intCount = dtTempPAN.Rows.Count

                For i = 0 To intTempPANCount - 1
                    strVal = ""
                    'Ver 8.002 - Id '0129703' 12/07/19  Start
                    'cp.WebBrowserStart()
                    'Ver 8.002 - Id '0129703' 12/07/19  End
                    If i = 0 Then
                        strPanStatus = "Verifying Internet connection..."
                        frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
                    End If
                    Try
                        isValid = False
                        intPANStatus = 0
                        intPANStatus = NewPanValidation(dtTempPAN.Rows(0)(0).ToString())
                        If intPANStatus = 1 Then
                            isValid = True
                        ElseIf intPANStatus = 0 Then
                            isValid = False
                        ElseIf intPANStatus = 2 Then
                            isValid = False
                            Exit For
                            Dim intError As Integer = 0
                            Dim intIgnore As Integer = (1 / intError)
                            'Ver 8.002 - Id '0129703' 12/07/19  Start    
                            'Else
                            '    MessageBox.Show("Program will Terminate!!!")
                            '    Exit For
                            'Ver 8.002 - Id '0129703' 12/07/19  Start
                        End If


                    Catch ex2 As System.ServiceModel.FaultException
                        If ex2.Message = "Please provide a PAN" OrElse ex2.Message = "Invalid PAN. Please retry." OrElse ex2.Message = "PAN does not exist." Then
                            isValid = False
                        ElseIf ex2.Message = "No e-Return has been filed for this PAN and Assessment Year." Then
                            isValid = True
                        Else
                            isValid = True
                        End If
                    Catch ex2 As Exception

                        If i = 0 Then
                            strPanStatus = "Processing..."
                            frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
                        End If
                        If File.Exists(Application.StartupPath & "\DNFDebug.txt") = True Then
                            If MessageBox.Show(ex2.Message & vbCrLf & "Do you want to proceed?", "DNF", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
                                Exit For
                            End If
                        End If

                        intIgnoredPANList = intIgnoredPANList + 1
                        Application.DoEvents()
                        strIngnoredPANList.Append(dtTempPAN.Rows(0)("PAN").ToString & vbCrLf)
                        dtTempviewSalary.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "' And Filler4='N'"
                        'For l = 0 To dtTempviewSalary.Count - 1
                        '    dtTempviewSalary.Item(l)("PANStatus") = "X"
                        'Next
                        dtTempPAN.Rows(0).Delete()

                        strPanProcess = "Verifying PAN's : " & i + 1 & " of " & intCount
                        frmProcess.lblPanProcess.Text = strPanProcess
                        Application.DoEvents()
                        Continue For
                    End Try

                    'takeAction:

                    If isValid = True Then
                        intCountOfValidPAN = intCountOfValidPAN + 1

                        'strValidPANList.Append("," & dtTempPAN.Rows(0)("PAN").ToString)

                        If strValidPANList.ToString.Contains(dtTempPAN.Rows(0)("PAN").ToString) Then
                        Else
                            strValidPANList.Append("," & dtTempPAN.Rows(0)("PAN").ToString)
                        End If

                        '==>Jitendra 12082016
                        dtTempviewSalary.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "'"
                        ' dtTempviewSalary.Item(0).Delete()
                        '<==Jitendra 12082016
                        'Ver 7.03-REQ816 start
                        For J = 0 To dsMain.Tables("SalaryDetails").Rows.Count - 1
                            If dsMain.Tables("SalaryDetails").Rows(J)("PAN").ToString = dtTempPAN.Rows(0)("PAN").ToString Then
                                dsMain.Tables("SalaryDetails").Rows(J)("PANValid") = "Y"
                            End If
                        Next
                        'Ver 7.03-REQ816 end 
                        dtTempPAN.Rows(0).Delete()
                    Else
                        intCntInvalidPan = intCntInvalidPan + 1
                        'dtTempviewSalary.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "' And Filler4='N'"
                        dtTempviewSalary.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "'"
                        'For l = 0 To dtTempviewSalary.Count - 1
                        '    dtTempviewSalary.Item(l)("PANStatus") = "I"
                        '    dtTempviewSalary.Item(l)("DeducteeStatus") = "Z" '4th Character Updated to 'Z' For Invalid PAN
                        'Next
                        'For l = 0 To dtTempviewSalary.Count - 1
                        '    dtTempviewSalary.Item(l)("Filler5") = "R"
                        'Next
                        'Ver 7.03-REQ816 start
                        For J = 0 To dsMain.Tables("SalaryDetails").Rows.Count - 1
                            If dsMain.Tables("SalaryDetails").Rows(J)("PAN").ToString = dtTempPAN.Rows(0)("PAN").ToString Then
                                dsMain.Tables("SalaryDetails").Rows(J)("PANValid") = "N"
                            End If
                        Next
                        'Ver 7.03-REQ816 end 

                        dtTempPAN.Rows(0).Delete()
                    End If
                    strPanProcess = "Verifying PAN's : " & i + 1 & " of " & intCount
                    frmProcess.lblPanProcess.Text = strPanProcess
                    Application.DoEvents()
                    'Ver 8.002 - Id '0129703' 12/07/19  Start
                    'cp.WebBrowserClose()
                    'Ver 8.002 - Id '0129703' 12/07/19  End
                Next
                'Ver 7.03-REQ816 start
                dsMain.Tables("SalaryDetails").AcceptChanges()
                'Ver 7.03-REQ816 end 
                ' end for salary
                '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
skipSD:

                Try
                    ''Store Valid PAN List

                    If strValidPANList.Length > 0 Then
                        If File.Exists(strValidPANListFilePath) = False Then
                            Dim f As FileStream = File.Create(strValidPANListFilePath)
                            f.Close()
                            f.Dispose()
                        End If

                        If File.ReadAllText(strValidPANListFilePath).Length = 0 Then
                            strValidPANList = New StringBuilder(strValidPANList.ToString.Substring(1))
                        End If

                        fs = New FileStream(strValidPANListFilePath, FileMode.Append, FileAccess.Write)
                        strWr = New StreamWriter(fs)

                        strWr.Write(strValidPANList.ToString)
                        strWr.Flush()
                        strWr.Close()
                        strWr.Dispose()

                        fs.Dispose()
                    End If

                    ''Store Ignored PAN List
                    If strIngnoredPANList.Length > 0 Then
                        If File.Exists(strIgnoredPANListFilePath) = False Then
                            Dim f As FileStream = File.Create(strIgnoredPANListFilePath)
                            f.Close()
                            f.Dispose()
                        End If

                        File.WriteAllText(strIgnoredPANListFilePath, strIngnoredPANList.ToString)
                    End If

                Catch ex As Exception
                    MsgBox(ex.Message)
                    blnPANDelegate = True
                    blnPANVerification = False
                End Try
                blnPANDelegate = True
                blnPANVerification = False

            Catch ex As Exception
                blnPANDelegate = True
                blnPANVerification = True
            End Try

            ''==>Jitendra SalaryDetails
            ''Ver 5.05-REQ641 start
            'Dim dtTempviewSalary As New DataView(dsMain.Tables("SalaryDetails"))
            'strtAssessmentYear = Mid(dtBH.Rows(0)("Fin Yr"), 1, 4) & "-" & Mid(dtBH.Rows(0)("Fin Yr"), 5)
            'dtTempviewSalary.Sort = "PAN"
            ''dtTempviewSalary.RowFilter = "Filler4='N'"
            'dtTempviewSalary.RowFilter = " PAN <>'PANNOTAVBL' "
            'dtTempPAN = dtTempviewSalary.ToTable("PANValid", True, "PAN")
            'Application.DoEvents()

            ''CheckPANWithExistingList()
            ''Application.DoEvents()

            'intTempPANCount = dtTempPAN.Rows.Count
            'intCount = dtTempPAN.Rows.Count
            'Try

            '    For i = 0 To intTempPANCount - 1
            '        strVal = ""
            '        If i = 0 Then
            '            strPanStatus = "Verifying Internet connection..."
            '            frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
            '        End If
            '        Try

            '            isValid = False
            '            intPANStatus = 0
            '            intPANStatus = PanValidation(dtTempPAN.Rows(0)(0).ToString())
            '            If intPANStatus = 1 Then
            '                isValid = True
            '            ElseIf intPANStatus = 0 Then
            '                isValid = False
            '            ElseIf intPANStatus = 2 Then
            '                isValid = False
            '                Dim intError As Integer = 0
            '                Dim intIgnore As Integer = (1 / intError)
            '            End If


            '        Catch ex2 As System.ServiceModel.FaultException
            '            If ex2.Message = "Please provide a PAN" OrElse ex2.Message = "Invalid PAN. Please retry." OrElse ex2.Message = "PAN does not exist." Then
            '                isValid = False
            '            ElseIf ex2.Message = "No e-Return has been filed for this PAN and Assessment Year." Then
            '                isValid = True
            '            Else
            '                isValid = True
            '            End If
            '        Catch ex2 As Exception

            '            If i = 0 Then
            '                strPanStatus = "Processing..."
            '                frmProcess.lblPanStatus.Text = "Status  : " & strPanStatus
            '            End If
            '            If File.Exists(Application.StartupPath & "\DNFDebug.txt") = True Then
            '                If MessageBox.Show(ex2.Message & vbCrLf & "Do you want to proceed?", "DNF", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
            '                    Exit For
            '                End If
            '            End If

            '            intIgnoredPANList = intIgnoredPANList + 1
            '            Application.DoEvents()
            '            strIngnoredPANList.Append(dtTempPAN.Rows(0)("PAN").ToString & vbCrLf)
            '            dtTempviewSalary.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "' And Filler4='N'"
            '            'For l = 0 To dtTempviewSalary.Count - 1
            '            '    dtTempviewSalary.Item(l)("PANStatus") = "X"
            '            'Next
            '            dtTempPAN.Rows(0).Delete()

            '            strPanProcess = "Verifying PAN's : " & i + 1 & " of " & intCount
            '            frmProcess.lblPanProcess.Text = strPanProcess
            '            Application.DoEvents()
            '            Continue For

            '        End Try

            '        'takeAction:


            '        If isValid = True Then
            '            intCountOfValidPAN = intCountOfValidPAN + 1
            '            strValidPANList.Append("," & dtTempPAN.Rows(0)("PAN").ToString)
            '            '==>Jitendra 12082016
            '            dtTempviewSalary.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "'"
            '            ' dtTempviewSalary.Item(0).Delete()
            '            '<==Jitendra 12082016
            '            dtTempPAN.Rows(0).Delete()
            '        Else
            '            intCntInvalidPan = intCntInvalidPan + 1
            '            'dtTempviewSalary.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "' And Filler4='N'"
            '            dtTempviewSalary.RowFilter = "PAN = '" & dtTempPAN.Rows(0)("PAN").ToString & "'"
            '            'For l = 0 To dtTempviewSalary.Count - 1
            '            '    dtTempviewSalary.Item(l)("PANStatus") = "I"
            '            '    dtTempviewSalary.Item(l)("DeducteeStatus") = "Z" '4th Character Updated to 'Z' For Invalid PAN
            '            'Next
            '            'For l = 0 To dtTempviewSalary.Count - 1
            '            '    dtTempviewSalary.Item(l)("Filler5") = "R"
            '            'Next
            '            dtTempPAN.Rows(0).Delete()
            '        End If
            '        strPanProcess = "Verifying PAN's : " & i + 1 & " of " & intCount
            '        frmProcess.lblPanProcess.Text = strPanProcess
            '        Application.DoEvents()



            '    Next
        Catch ex As Exception
            'Ver 6.01-QC1191 start
            blnPANDelegate = True
            'Ver 6.01-QC1191 end
            Dim trace = New Diagnostics.StackTrace(ex, True)
            Dim line As String = Strings.Right(trace.ToString, 5)
            Dim nombreMetodo As String = ""
            For Each sf As StackFrame In trace.GetFrames
                nombreMetodo = sf.GetMethod().Name & vbCrLf
            Next
            MessageBox.Show(ex.Message & vbCrLf & nombreMetodo & "_" & line, "DNF")
        End Try
        'Ver 6.01-QC1191 start
        blnPANDelegate = True
        'Ver 6.01-QC1191 end
        'Ver 5.05-REQ641 end 
    End Sub
    'Ver 4.042-QC?? end
    'Ver 4.042-QC?? start
    Public Function NewPanValidation(strPAN As String) As Integer
        Dim oPANValidate As New PANValidate
        blnPANDelegate = False
        'Dim success As Boolean = oPANValidate.Validate("https://eportal.incometax.gov.in/iec/registrationapi/saveEntity", strPAN)
        Dim success As Boolean = oPANValidate.Validate("https://eportal.incometax.gov.in/iec/foservices/#/pre-login/register", strPAN)
        blnPANDelegate = True

        If success Then
            If Not (oPANValidate.oJsonResult.messages(0).desc = "The PAN entered does not exist. Please retry.") Then Return 1 Else Return 0
        Else
            Return 2
        End If
    End Function
    Public Function PanValidation(strPAN As String) As Integer
        '0 Invalid PAN
        '1 Valid PAN
        '2 Net Connetion fail

        Dim RegistrationType As Integer = 0
        Dim pFld As Integer = 0
        Dim pFrm As Integer = 0
        Dim i As Integer = 0 'Initialize for internet connection is active

        blnPANDelegate = False

        'Ver 8.002 - Id '0129703' 12/07/2019 Start
        Dim sError = ""
        Dim sReuslt As String = ""

        'sReuslt = cp.ProcessPJDriver(strPAN, "Test", "13/07/2019", sError)
        'If sReuslt < 0 Or sReuslt > 2 Then
        '    MessageBox.Show(sError)
        'End If


        'Commented on 12/07/2019 Ver 8.002 - Id '0129703' Start
        'Ver 8.003 start

        WebPageLoad()
        DocComplete = False

        Do While DocComplete <> True
            If blnCheckNetConnetion = True Then
                If (i <> 20) Then
                    If CheckForInternetConnection() = False Then
                        blnInternetConnectionFailed = True
                        MessageBox.Show("Kindly Check Your Internet Connection!", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return 2
                        Exit Function
                    End If
                End If
            End If
            Application.DoEvents()
            i = i + 1
        Loop


        pIEdoc = pIE1.Document.DomDocument
        For pFrm = 0 To pIEdoc.forms.length - 1
            For pFld = 1 To pIEdoc.forms.item(pFrm).Length - 1

                If pIEdoc.forms.item(pFrm)(pFld).Type <> "hidden" Then
                    'Ver 6.03-REQ768 start
                    'If (pIEdoc.forms.item(pFrm)(pFld).Type = "radio" And pIEdoc.forms.item(pFrm)(pFld).Name = "userType") Then
                    '    If (pIEdoc.forms.item(pFrm)(pFld).Value = "11" And strPAN.Substring(3, 1) = "P") Then
                    '        pIEdoc.forms.item(pFrm)(pFld).Value = 11
                    '        pIEdoc.forms.item(pFrm)(pFld).Click()
                    '    ElseIf (pIEdoc.forms.item(pFrm)(pFld).Value = "12" And strPAN.Substring(3, 1) = "H") Then
                    '        pIEdoc.forms.item(pFrm)(pFld).Value = 12
                    '        pIEdoc.forms.item(pFrm)(pFld).Click()
                    '    ElseIf (pIEdoc.forms.item(pFrm)(pFld).Value = "31" And strPAN.Substring(3, 1) = "C") Then
                    '        pIEdoc.forms.item(pFrm)(pFld).Value = 31
                    '        pIEdoc.forms.item(pFrm)(pFld).Click()
                    '    ElseIf (pIEdoc.forms.item(pFrm)(pFld).Value = "32" And strPAN.Substring(3, 1) = "B") Then
                    '        pIEdoc.forms.item(pFrm)(pFld).Value = 32
                    '        pIEdoc.forms.item(pFrm)(pFld).Click()
                    '    ElseIf (pIEdoc.forms.item(pFrm)(pFld).Value = "33" And strPAN.Substring(3, 1) = "L") Then
                    '        pIEdoc.forms.item(pFrm)(pFld).Value = 33
                    '        pIEdoc.forms.item(pFrm)(pFld).Click()
                    '    ElseIf (pIEdoc.forms.item(pFrm)(pFld).Value = "34" And strPAN.Substring(3, 1) = "F") Then
                    '        pIEdoc.forms.item(pFrm)(pFld).Value = 34
                    '        pIEdoc.forms.item(pFrm)(pFld).Click()
                    '    ElseIf (pIEdoc.forms.item(pFrm)(pFld).Value = "35" And strPAN.Substring(3, 1) = "T") Then
                    '        pIEdoc.forms.item(pFrm)(pFld).Value = 35
                    '        pIEdoc.forms.item(pFrm)(pFld).Click()
                    '    ElseIf (pIEdoc.forms.item(pFrm)(pFld).Value = "36" And strPAN.Substring(3, 1) = "A") Then
                    '        pIEdoc.forms.item(pFrm)(pFld).Value = 36
                    '        pIEdoc.forms.item(pFrm)(pFld).Click()
                    '    ElseIf (pIEdoc.forms.item(pFrm)(pFld).Value = "37" And strPAN.Substring(3, 1) = "J") Then
                    '        pIEdoc.forms.item(pFrm)(pFld).Value = 37
                    '        pIEdoc.forms.item(pFrm)(pFld).Click()
                    '    ElseIf (pIEdoc.forms.item(pFrm)(pFld).Value = "39" And strPAN.Substring(3, 1) = "G") Then
                    '        pIEdoc.forms.item(pFrm)(pFld).Value = 39
                    '        pIEdoc.forms.item(pFrm)(pFld).Click()
                    '    ElseIf Not (strPAN.Substring(3, 1) = "P" Or strPAN.Substring(3, 1) = "H" Or strPAN.Substring(3, 1) = "C" Or strPAN.Substring(3, 1) = "B" Or strPAN.Substring(3, 1) = "L" Or strPAN.Substring(3, 1) = "F" Or strPAN.Substring(3, 1) = "T" Or strPAN.Substring(3, 1) = "A" Or strPAN.Substring(3, 1) = "J" Or strPAN.Substring(3, 1) = "G") Then
                    '        Return 0
                    '    End If

                    'ElseIf (pIEdoc.forms.item(pFrm)(pFld).Type = "submit") Then
                    '    pIEdoc.forms.item(pFrm)(pFld).Click()
                    'End If


                    If (pIEdoc.forms.item(pFrm)(pFld).Name = "userTypeSel") Then
                        If (strPAN.Substring(3, 1) = "P") Then
                            pIEdoc.forms.item(pFrm)(pFld).Value = 11
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("userType")(0).value = 11
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                        ElseIf (strPAN.Substring(3, 1) = "H") Then
                            pIEdoc.forms.item(pFrm)(pFld).Value = 12
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("userType")(0).value = 12
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                        ElseIf (strPAN.Substring(3, 1) = "C") Then
                            pIEdoc.forms.item(pFrm)(pFld).Value = "00"
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("subUserTypeSel")(0).value = 31
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("userType")(0).value = 31
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                        ElseIf (strPAN.Substring(3, 1) = "B") Then
                            pIEdoc.forms.item(pFrm)(pFld).Value = "00"
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("subUserTypeSel")(0).value = 32
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("userType")(0).value = 32
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                        ElseIf (strPAN.Substring(3, 1) = "L") Then
                            pIEdoc.forms.item(pFrm)(pFld).Value = "00"
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("subUserTypeSel")(0).value = 33
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("userType")(0).value = 33
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                        ElseIf (strPAN.Substring(3, 1) = "F") Then
                            pIEdoc.forms.item(pFrm)(pFld).Value = "00"
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("subUserTypeSel")(0).value = 34
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("userType")(0).value = 34
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                        ElseIf (strPAN.Substring(3, 1) = "T") Then
                            pIEdoc.forms.item(pFrm)(pFld).Value = "00"
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("subUserTypeSel")(0).value = 35
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("userType")(0).value = 35
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                        ElseIf (strPAN.Substring(3, 1) = "A") Then
                            pIEdoc.forms.item(pFrm)(pFld).Value = "00"
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("subUserTypeSel")(0).value = 36
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("userType")(0).value = 36
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                        ElseIf (strPAN.Substring(3, 1) = "J") Then
                            pIEdoc.forms.item(pFrm)(pFld).Value = "00"
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("subUserTypeSel")(0).value = 37
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("userType")(0).value = 37
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                        ElseIf (strPAN.Substring(3, 1) = "G") Then
                            pIEdoc.forms.item(pFrm)(pFld).Value = "00"
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("subUserTypeSel")(0).value = 39
                            pIEdoc.forms.item(pFrm)(pFld).Click()

                            pIEdoc.getElementsByName("userType")(0).value = 39
                            pIEdoc.forms.item(pFrm)(pFld).Click()
                            'Ver 6.03-REQ768 end
                        ElseIf Not (strPAN.Substring(3, 1) = "P" Or strPAN.Substring(3, 1) = "H" Or strPAN.Substring(3, 1) = "C" Or strPAN.Substring(3, 1) = "B" Or strPAN.Substring(3, 1) = "L" Or strPAN.Substring(3, 1) = "F" Or strPAN.Substring(3, 1) = "T" Or strPAN.Substring(3, 1) = "A" Or strPAN.Substring(3, 1) = "J" Or strPAN.Substring(3, 1) = "G") Then
                            Return 0
                        End If

                    ElseIf (pIEdoc.forms.item(pFrm)(pFld).Type = "submit") Then
                        pIEdoc.forms.item(pFrm)(pFld).Click()
                        'Ver 6.03-REQ768 start
                        ' End
                        'Ver 6.03-REQ768 end
                    End If
                End If
            Next
        Next

        DocComplete = False
        'Checking InternetConnection is active 
        i = 0
        Do While DocComplete <> True
            If blnCheckNetConnetion = True Then
                If (i <> 20) Then
                    If CheckForInternetConnection() = False Then
                        blnInternetConnectionFailed = True
                        MessageBox.Show("Kindly Check Your Internet Connection!", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return 2
                        Exit Function
                    End If
                End If
            End If
            Application.DoEvents()
            i = i + 1
        Loop
        'Code commented to checking above code for internet connection
        'Do While DocComplete <> True
        '    If blnCheckNetConnetion = True Then
        '        If CheckForInternetConnection() = False Then
        '            Return 2
        '        End If
        '    End If
        '    Application.DoEvents()
        'Loop

        pIEdoc = pIE1.Document.DomDocument


        For pFrm = 0 To pIEdoc.forms.length - 1
            For pFld = 1 To pIEdoc.forms.item(pFrm).Length - 1
                'Ver 6.03-REQ768 start
                'For Individual
                'If strPAN.Substring(3, 1) = "P" Then
                '    If pIEdoc.forms.item(pFrm)(pFld).Type <> "hidden" Then
                '        If pIEdoc.forms.item(pFrm)(pFld).Type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.userId" Then
                '            pIEdoc.forms.item(pFrm)(pFld).value = strPAN
                '        ElseIf pIEdoc.forms.item(pFrm)(pFld).Type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.userPersonalDetails.surName" Then
                '            pIEdoc.forms.item(pFrm)(pFld).value = "Test"
                '        ElseIf pIEdoc.forms.item(pFrm)(pFld).Type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.userPersonalDetails.dateOfBirth" Then
                '            pIEdoc.forms.item(pFrm)(pFld).value = DateTime.Today.Date.ToShortDateString()
                '        End If

                '        If pIEdoc.forms.item(pFrm)(pFld).Type = "submit" Then
                '            pIEdoc.forms.item(pFrm)(pFld).Click()
                '        End If
                '    End If
                'End If

                ''For Other then Individual
                'If strPAN.Substring(3, 1) <> "P" Then
                '    If pIEdoc.forms.item(pFrm)(pFld).Type <> "hidden" Then
                '        If pIEdoc.forms.item(pFrm)(pFld).Type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.userId" Then
                '            pIEdoc.forms.item(pFrm)(pFld).value = strPAN
                '        ElseIf pIEdoc.forms.item(pFrm)(pFld).Type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.orgName" Then
                '            pIEdoc.forms.item(pFrm)(pFld).value = "Test"
                '        ElseIf pIEdoc.forms.item(pFrm)(pFld).Type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.dateOfIncorporation" Then
                '            pIEdoc.forms.item(pFrm)(pFld).value = DateTime.Today.Date.ToShortDateString()
                '        End If

                '        If pIEdoc.forms.item(pFrm)(pFld).Type = "submit" Then
                '            pIEdoc.forms.item(pFrm)(pFld).Click()
                '        End If
                '    End If
                'End If

                'For Individual
                If strPAN.Substring(3, 1) = "P" Then
                    If pIEdoc.forms.item(pFrm)(pFld).Type <> "hidden" Then
                        If pIEdoc.forms.item(pFrm)(pFld).Type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.userId" Then
                            pIEdoc.forms.item(pFrm)(pFld).value = strPAN
                        ElseIf pIEdoc.forms.item(pFrm)(pFld).Type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.userPersonalDetails.surName" Then
                            pIEdoc.forms.item(pFrm)(pFld).value = "Test"
                        ElseIf pIEdoc.forms.item(pFrm)(pFld).Type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.userPersonalDetails.dateOfBirth" Then
                            pIEdoc.forms.item(pFrm)(pFld).value = DateTime.Today.Date.ToShortDateString()
                        ElseIf pIEdoc.forms.item(pFrm)(pFld).Type = "radio" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.nriFlag" Then
                            pIEdoc.forms.item(pFrm)(pFld).value = "N"
                            pIEdoc.forms.item(pFrm)(pFld).Click()
                        End If

                        If pIEdoc.forms.item(pFrm)(pFld).Type = "submit" And pIEdoc.forms.item(pFrm)(pFld).Name = "continue" Then
                            pIEdoc.forms.item(pFrm)(pFld).Click()
                        End If
                    End If
                End If

                'For Other then Individual
                If strPAN.Substring(3, 1) <> "P" Then
                    If pIEdoc.forms.item(pFrm)(pFld).tagName = "INPUT" Then
                        If pIEdoc.forms.item(pFrm)(pFld).type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.userId" Then
                            pIEdoc.forms.item(pFrm)(pFld).value = strPAN
                        ElseIf pIEdoc.forms.item(pFrm)(pFld).type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.orgName" Then
                            pIEdoc.forms.item(pFrm)(pFld).value = "Test"
                        ElseIf pIEdoc.forms.item(pFrm)(pFld).type = "text" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.dateOfIncorporation" Then
                            pIEdoc.forms.item(pFrm)(pFld).value = DateTime.Today.Date.ToShortDateString()
                        End If

                        If strPAN.Substring(3, 1) = "C" Then
                            If pIEdoc.forms.item(pFrm)(pFld).type = "radio" And pIEdoc.forms.item(pFrm)(pFld).Name = "userProfile.corpTypeFlag" Then
                                pIEdoc.forms.item(pFrm)(pFld).value = "N"
                                pIEdoc.forms.item(pFrm)(pFld).Click()
                            End If
                        End If

                        If pIEdoc.forms.item(pFrm)(pFld).Type = "submit" Then
                            pIEdoc.forms.item(pFrm)(pFld).Click()
                        End If
                    End If
                End If
                'Ver 6.03-REQ768 end
            Next
        Next

        DocComplete = False
        i = 0
        Do While DocComplete <> True
            If blnCheckNetConnetion = True Then
                If (i <> 20) Then
                    If CheckForInternetConnection() = False Then
                        blnInternetConnectionFailed = True
                        MessageBox.Show("Kindly Check Your Internet Connection!", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return 2
                        Exit Function
                    End If
                End If
            End If
            Application.DoEvents()
            i = i + 1
        Loop
        'Do While DocComplete <> True
        '    If blnCheckNetConnetion = True Then
        '        If CheckForInternetConnection() = False Then
        '            Return 2
        '        End If
        '    End If
        '    Application.DoEvents()
        'Loop

        pIEdoc = pIE1.Document.DomDocument
        Dim PanValidate As String = pIEdoc.documentElement.innerText
        blnPANDelegate = True


        'If (PanValidate.ToString().Contains("This PAN has already been registered.") = True) Or (PanValidate.ToString().Contains("Invalid Surname. Please retry.") = True) Or (PanValidate.ToString().Contains("Invalid Organization Name. Please retry.") = True) Then 'Ver 8.0.0.3
        If Not (PanValidate.ToString().Contains("PAN does not exist.")) Then
            Return 1
        Else
            Return 0
        End If

        'If (PanValidate.ToString().Contains("PAN does not exist.") = True) Then
        'Commented on 12/07/2019 Ver 8.002 - Id '0129703' End
        'Return sReuslt
        'Ver 8.002 - Id '0129703' 12/07/2019 End
    End Function

    Public Sub WebPageLoad()
        pIE1.Name = "pIE1"
        'Ver 7.00-QC1289 start
        ' pIE1.Navigate(New System.Uri("https://incometaxindiaefiling.gov.in/e-Filing/Registration/RegistrationHome.html"))
        'Fastfacts-423 start
        ' pIE1.Navigate(New System.Uri("https://portal.incometaxindiaefiling.gov.in/e-Filing/Registration/RegistrationHome.html?lang=eng"))

        'pIE1.Navigate(New System.Uri("https://www.incometaxindiaefiling.gov.in/home"))
        'pIE1.Navigate(New System.Uri("https://www1.incometaxindiaefiling.gov.in/e-FilingGS/Registration/RegistrationHome.html?lang=eng"))

        'pIE1.Navigate(New System.Uri("https://www.incometaxindiaefiling.gov.in/home"))
        'pIE1.Navigate(New System.Uri("https://www1.incometaxindiaefiling.gov.in/e-FilingGS/Registration/RegistrationHome.html?lang=eng"))
        pIE1.Navigate(New System.Uri("https://eportal.incometax.gov.in/iec/foservices/#/pre-login/register"))

        'Fastfacts-423 end
        'Ver 7.00-QC1289 end
        pIE1.ScriptErrorsSuppressed = True  'Added on 18/10/2107 due to scripting errors.      
    End Sub
    'Ver 4.042-QC?? end
    Public Function CheckForInternetConnection() As Boolean

        'Dim objURL As New Uri("http://www.youtube.com")
        ''Dim objURL As New Uri("http://www.google.com")
        'Dim objWebReq As WebRequest
        'objWebReq = WebRequest.Create(objURL)
        'Dim objWebResp As WebResponse

        'Try
        '    ''Using client = New WebClient()
        '    ''    Using stream = client.OpenRead("http://www.google.com")
        '    ''        Return True
        '    ''    End Using
        '    ''End Using
        '    objWebResp = objWebReq.GetResponse
        '    objWebResp.Close()
        '    objWebReq = Nothing
        '    objWebResp = Nothing
        '    Return True
        'Catch ex As Exception
        '    objWebReq = Nothing
        '    objWebResp = Nothing
        '    Return False
        'End Try

        'following portion added by bharat
        Try
            Dim ping As New Ping
            Threading.Thread.Sleep(CLng(strThreadSleepValue))
            'Threading.Thread.Sleep(2000)
            Dim pingstatus As PingReply
            pingstatus = ping.Send("www.google.com", 5000)
            If (pingstatus.Status = IPStatus.Success) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    'Ver 6.00-REQ660 start
    Public Function RetriveState(ByVal strStCode As String) As String
        Try
            Select Case strStCode
                Case "1"
                    Return "ANDAMAN AND NICOBAR ISLANDS"
                Case "2"
                    Return "ANDHRA PRADESH"
                Case "3"
                    Return "ARUNACHAL PRADESH"
                Case "4"
                    Return "ASSAM"
                Case "5"
                    Return "BIHAR"
                Case "6"
                    Return "CHANDIGARH"
                Case "7"
                    Return "DADRA & NAGAR HAVELI"
                Case "8"
                    Return "DAMAN & DIU"
                Case "9"
                    Return "DELHI"
                Case "10"
                    Return "GOA"
                Case "11"
                    Return "GUJARAT"
                Case "12"
                    Return "HARYANA"
                Case "13"
                    Return "HIMACHAL PRADESH"
                Case "14"
                    Return "JAMMU & KASHMIR"
                Case "15"
                    Return "KARNATAKA"
                Case "16"
                    Return "KERALA"
                Case "17"
                    Return "LAKSHWADEEP"
                Case "18"
                    Return "MADHYA PRADESH"
                Case "19"
                    Return "MAHARASHTRA"
                Case "20"
                    Return "MANIPUR"
                Case "21"
                    Return "MEGHALAYA"
                Case "22"
                    Return "MIZORAM"
                Case "23"
                    Return "NAGALAND"
                Case "24"
                    Return "ODISHA"
                Case "25"
                    Return "PONDICHERRY"
                Case "26"
                    Return "PUNJAB"
                Case "27"
                    Return "RAJASTHAN"
                Case "28"
                    Return "SIKKIM"
                Case "29"
                    Return "TAMILNADU"
                Case "30"
                    Return "TRIPURA"
                Case "31"
                    Return "UTTAR PRADESH"
                Case "32"
                    Return "WEST BENGAL"
                Case "33"
                    Return "CHHATISHGARH"
                Case "34"
                    Return "UTTARAKHAND"
                Case "35"
                    Return "JHARKHAND"
                Case "36"
                    Return "TELANGANA"
                Case "99"
                    Return "OTHERS"
            End Select
        Catch ex As Exception
            MessageBox.Show("Invalid State Code")
        End Try
    End Function
    'Ver 6.00-REQ660 end
    'Ver 7.03-REQ816 start
    Public Function TaxSlabCalculation_old(ByVal TotIncome As Double, ByVal isPenalApplied As Boolean, ByVal Category As String, Optional _Relief89 As Double = 0) As Dictionary(Of String, Double)
        Dim dict As Dictionary(Of String, Double) = New Dictionary(Of String, Double)()
        Dim XMLFILE As XmlReader
        Dim ds As New DataSet()
        Dim dsNew As New DataSet()
        Dim finyear As String
        finyear = strFinYear.Substring(0, 4) & "-" & strFinYear.Substring(0, 2) & strFinYear.Substring(4, 2)
        XMLFILE = XmlReader.Create(Application.StartupPath & "\SalarySlab.xml", New XmlReaderSettings())
        ds.ReadXml(XMLFILE)
        Dim foundRows() As Data.DataRow
        foundRows = ds.Tables("Sheet1").Select("TaxSlabID like '" & Category & "%" & "' AND TaxSlabRangeID='" & finyear & "'")

        Dim findLastNumber As Integer = (TotIncome Mod 10)
        If findLastNumber < 5 Then
            TotIncome = TotIncome - findLastNumber
        Else
            TotIncome = TotIncome - findLastNumber + 10
        End If

        dsNew.Merge(foundRows)
        Dim grossTax As Double = 0
        Dim surcharge As Double = 0
        Dim educationCess As Double = 0
        Dim penalRate As Double = 20
        Dim Relief89 As Double = _Relief89

        Dim remainingAmount As Double = TotIncome
        Dim taxSlabRangeFromAmountForSurchage As Double = 0
        Dim idealGrossTaxPerviousToLastSlabRange As Double = 0
        Dim flag As Boolean = False
        Dim maxSurchargePerc As Double = 0
        Dim i As Integer
        For i = 0 To dsNew.Tables(0).Rows.Count - 1
            Dim rebateAmount As Double = 0
            Dim taxPercentage As Double = 0
            Dim calculatingAmount As Double = 0

            taxPercentage = dsNew.Tables(0).Rows(i)("TaxPercentage")
            If remainingAmount > 0 Then '1
                calculatingAmount = (dsNew.Tables(0).Rows(i)("ToAmount") - dsNew.Tables(0).Rows(i)("FromAmount"))
                If TotIncome <= dsNew.Tables(0).Rows(i)("ToAmount") Then
                    calculatingAmount = remainingAmount
                    rebateAmount = dsNew.Tables(0).Rows(i)("Rebate")
                End If


                If (TotIncome >= dsNew.Tables(0).Rows(0)("ToAmount") And isPenalApplied = True) Then
                    rebateAmount = 0
                    flag = True
                End If
                Dim taxAmount As Double = 0
                taxAmount = Math.Round(calculatingAmount * taxPercentage * 0.01) - rebateAmount

                grossTax += IIf(taxAmount > 0, taxAmount, 0)
                remainingAmount = TotIncome - dsNew.Tables(0).Rows(i)("ToAmount")

            End If '1

            If (dsNew.Tables(0).Rows(i)("SurchargePercentage") > 0 And (TotIncome >= dsNew.Tables(0).Rows(i)("FromAmount")) And TotIncome <= dsNew.Tables(0).Rows(i)("ToAmount")) Then

                taxSlabRangeFromAmountForSurchage = dsNew.Tables(0).Rows(i)("FromAmount")
                surcharge = Math.Round(grossTax * 0.01 * dsNew.Tables(0).Rows(i)("SurchargePercentage"))
                If (i = dsNew.Tables(0).Rows.Count() - 1) Then
                    maxSurchargePerc = dsNew.Tables(0).Rows(i)("SurchargePercentage")
                End If
                Exit For
            End If

            idealGrossTaxPerviousToLastSlabRange += Math.Round((dsNew.Tables(0).Rows(i)("ToAmount") - dsNew.Tables(0).Rows(i)("FromAmount")) * 0.01 * dsNew.Tables(0).Rows(i)("TaxPercentage"))
        Next

        'flat 20%--check

        Dim flatPenal As Double
        If flag = True Then
            flatPenal = TotIncome * penalRate * 0.01
            If flatPenal > grossTax Then
                grossTax = flatPenal
                flag = True
            End If
        Else
            flag = False
        End If

        'Marginal Relief code
        Dim differenceAmtAboveRange As Double
        If surcharge > 0 Then
            differenceAmtAboveRange = TotIncome - (taxSlabRangeFromAmountForSurchage - 1)

            If maxSurchargePerc = 0 Then
                Dim differenceAmtFromIdealGrossTax As Double = grossTax - idealGrossTaxPerviousToLastSlabRange
                Dim diffOfAbove As Double = differenceAmtAboveRange - differenceAmtFromIdealGrossTax
                surcharge = IIf(surcharge < diffOfAbove, surcharge, diffOfAbove)
            Else
                'Provision for Margianl Relief on above 1 crore value                
                If maxSurchargePerc > 0 Then
                    If (differenceAmtAboveRange < ((grossTax + surcharge) - ((idealGrossTaxPerviousToLastSlabRange * 0.1) + idealGrossTaxPerviousToLastSlabRange))) Then
                        surcharge = Math.Round(differenceAmtAboveRange - (grossTax - idealGrossTaxPerviousToLastSlabRange) + (idealGrossTaxPerviousToLastSlabRange * 0.1))
                    Else
                        surcharge = Math.Round(grossTax * (0.01 * maxSurchargePerc))
                    End If
                End If

            End If
        End If

        Dim educationCessAmt As Double = Math.Round((grossTax + surcharge) * dsNew.Tables(0).Rows(1)("EducationCessPercentage") * 0.01)
        educationCess = IIf(educationCessAmt > 0, educationCessAmt, 0)
        educationCess = IIf(flag = True, 0, educationCess)
        dict.Add("GrossTax", Math.Round(grossTax, 0, MidpointRounding.AwayFromZero))
        dict.Add("Surcharge", Math.Round(surcharge, 0, MidpointRounding.AwayFromZero))
        dict.Add("EducationCess", Math.Round(educationCess, 0, MidpointRounding.AwayFromZero))
        dict.Add("Relief89", Math.Round(Relief89, 0, MidpointRounding.AwayFromZero))

        Return dict
    End Function
    Public Function TaxSlabCalculation(ByVal TotIncome As Double, ByVal isPenalApplied As Boolean, ByVal Category As String, Optional _Relief89 As Double = 0) As Dictionary(Of String, Double)
        Dim dict As Dictionary(Of String, Double) = New Dictionary(Of String, Double)()

        Dim XMLFILE As XmlReader
        Dim ds As New DataSet()
        Dim dsNew As New DataSet()
        Dim finyear As String
        finyear = strFinYear.Substring(0, 4) & "-" & strFinYear.Substring(0, 2) & strFinYear.Substring(4, 2)
        XMLFILE = XmlReader.Create(Application.StartupPath & "\SalarySlab.xml", New XmlReaderSettings())
        ds.ReadXml(XMLFILE)

        Dim foundRows() As Data.DataRow
        foundRows = ds.Tables("Sheet1").Select("TaxSlabID like '" & Category & "%" & "' AND TaxSlabRangeID='" & finyear & "'")

        Dim findLastNumber As Integer = (TotIncome Mod 10)
        If findLastNumber < 5 Then
            TotIncome = TotIncome - findLastNumber
        Else
            TotIncome = TotIncome - findLastNumber + 10
        End If

        dsNew.Merge(foundRows)
        Dim grossTax As Double = 0
        Dim surcharge As Double = 0
        Dim educationCess As Double = 0
        Dim penalRate As Double = 20
        Dim Relief89 As Double = _Relief89

        Dim remainingAmount As Double = TotIncome
        Dim taxSlabRangeFromAmountForSurchage As Double = 0
        Dim idealGrossTaxPerviousToLastSlabRange As Double = 0
        Dim flag As Boolean = False
        Dim maxSurchargePerc As Double = 0
        Dim lastSurchargePerc As Double = 0
        Dim i As Integer
        For i = 0 To dsNew.Tables(0).Rows.Count - 1
            Dim rebateAmount As Double = 0
            Dim taxPercentage As Double = 0
            Dim calculatingAmount As Double = 0

            taxPercentage = dsNew.Tables(0).Rows(i)("TaxPercentage")
            If remainingAmount > 0 Then '1
                calculatingAmount = (dsNew.Tables(0).Rows(i)("ToAmount") - dsNew.Tables(0).Rows(i)("FromAmount"))
                If TotIncome <= dsNew.Tables(0).Rows(i)("ToAmount") Then
                    calculatingAmount = remainingAmount
                    rebateAmount = dsNew.Tables(0).Rows(i)("Rebate")
                End If
                If dsNew.Tables(0).Rows(i)("TaxSlabID") = "Y" And TotIncome <= dsNew.Tables(0).Rows(2)("ToAmount") Then
                    rebateAmount = dsNew.Tables(0).Rows(i)("Rebate")
                End If
                If (TotIncome >= dsNew.Tables(0).Rows(0)("ToAmount") And isPenalApplied = True) Then
                    rebateAmount = 0
                    flag = True
                End If
                Dim taxAmount As Double = 0
                taxAmount = Math.Round(calculatingAmount * taxPercentage * 0.01) - rebateAmount

                grossTax += IIf(taxAmount > 0, taxAmount, 0)


                remainingAmount = TotIncome - dsNew.Tables(0).Rows(i)("ToAmount")

            End If '1

            If (dsNew.Tables(0).Rows(i)("SurchargePercentage") > 0 And (TotIncome >= dsNew.Tables(0).Rows(i)("FromAmount")) And TotIncome <= dsNew.Tables(0).Rows(i)("ToAmount")) Then

                taxSlabRangeFromAmountForSurchage = dsNew.Tables(0).Rows(i)("FromAmount")
                surcharge = Math.Round(grossTax * 0.01 * dsNew.Tables(0).Rows(i)("SurchargePercentage"))
                maxSurchargePerc = dsNew.Tables(0).Rows(i)("SurchargePercentage")
                If (i = dsNew.Tables(0).Rows.Count() - 1) Then
                    'maxSurchargePerc = dsNew.Tables(0).Rows(i)("SurchargePercentage")
                End If
                Exit For
            End If

            idealGrossTaxPerviousToLastSlabRange += Math.Round((dsNew.Tables(0).Rows(i)("ToAmount") - dsNew.Tables(0).Rows(i)("FromAmount")) * 0.01 * dsNew.Tables(0).Rows(i)("TaxPercentage"))
            lastSurchargePerc = dsNew.Tables(0).Rows(i)("SurchargePercentage")
        Next

        'flat 20%--check

        Dim flatPenal As Double
        If flag = True Then
            flatPenal = TotIncome * penalRate * 0.01
            If flatPenal > grossTax Then
                grossTax = flatPenal
                flag = True
            End If
        Else
            flag = False
        End If

        'Marginal Relief code
        Dim differenceAmtAboveRange As Double
        If surcharge > 0 Then
            differenceAmtAboveRange = TotIncome - (taxSlabRangeFromAmountForSurchage - 1)

            If maxSurchargePerc = 0 Then
                Dim differenceAmtFromIdealGrossTax As Double = grossTax - idealGrossTaxPerviousToLastSlabRange
                Dim diffOfAbove As Double = differenceAmtAboveRange - differenceAmtFromIdealGrossTax
                surcharge = IIf(surcharge < diffOfAbove, surcharge, diffOfAbove)
            Else
                'Provision for Margianl Relief on above 1 crore value                
                If maxSurchargePerc > 0 Then
                    If (differenceAmtAboveRange < ((grossTax + surcharge) - ((idealGrossTaxPerviousToLastSlabRange * (lastSurchargePerc * 0.01)) + idealGrossTaxPerviousToLastSlabRange))) Then
                        surcharge = Math.Round(differenceAmtAboveRange - (grossTax - idealGrossTaxPerviousToLastSlabRange) + (idealGrossTaxPerviousToLastSlabRange * (lastSurchargePerc * 0.01)))
                    Else
                        surcharge = Math.Round(grossTax * (0.01 * maxSurchargePerc))
                    End If
                End If

            End If
        End If

        Dim educationCessAmt As Double = Math.Round((grossTax + surcharge) * dsNew.Tables(0).Rows(1)("EducationCessPercentage") * 0.01)
        educationCess = IIf(educationCessAmt > 0, educationCessAmt, 0)
        educationCess = IIf(flag = True, 0, educationCess)
        dict.Add("GrossTax", Math.Round(grossTax, 0, MidpointRounding.AwayFromZero))
        dict.Add("Surcharge", Math.Round(surcharge, 0, MidpointRounding.AwayFromZero))
        dict.Add("EducationCess", Math.Round(educationCess, 0, MidpointRounding.AwayFromZero))
        dict.Add("Relief89", Math.Round(Relief89, 0, MidpointRounding.AwayFromZero))

        Return dict
    End Function

    'Ver 7.03-REQ816 end 
End Module
