Imports Microsoft.Office.Interop
Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Windows
Imports eTdsDNF2223
'Imports Microsoft.Office.Interop.Excel

'Imports Microsoft.VisualBasic.FileIO
'Imports Microsoft.VisualBasic.FileIO

Module ExcelReporting
    Dim hsShortDeductions As New Hashtable
    Dim hsLatePayments As New Hashtable
    Dim hsShortDeposits As New Hashtable
    Dim htReason As New Hashtable
    Public strProcessID As String
    Public aryExcel As New ArrayList
    Dim arrData(,) As Object
    Dim dt_report As New DataTable()

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub




    Public Sub CreateExcelFile()
        blnFileFailure = False

        Call UpdateRates()

        Try

            Dim filename As String
            Dim ExlWorkbook As Object
            'Dim Exlsheet As Object
            Dim ExlRange As Object
            Dim dttempDDView As New DataView(dtDD)
            Dim dttempCDView As New DataView(dtCD)
            'Ver 5.05-REQ641 start
            Dim dtTempSDView As New DataView(dtSD) 'Jitendra
            'Ver 5.05-REQ641 end 
            'Ver 7.03-REQ816 start
            Dim dtTempSalary As New DataView(dsMainDNFValidation.Tables("SalaryDetails"))
            'Ver 7.03-REQ816 end
            '
            'anu_PanNameindeductee_28Jun24

            Dim DeducteePanTable As New DataTable()
            DeducteePanTable.Columns.Add("name", GetType(String))
            DeducteePanTable.Columns.Add("pan", GetType(String))

            For Each rowView As DataRowView In dttempDDView
                Dim newRow As DataRow = DeducteePanTable.NewRow()
                newRow("name") = rowView("name")
                newRow("pan") = rowView("pan")
                DeducteePanTable.Rows.Add(newRow)
            Next

            For Each rowView As DataRowView In dtTempSDView

                Dim newRow As DataRow = DeducteePanTable.NewRow()
                newRow("name") = rowView("name")
                newRow("pan") = rowView("pan")
                DeducteePanTable.Rows.Add(newRow)
            Next

            'anu_PanNameindeductee_28Jun24

            Dim dt_PanReport As DataTable
            If File.Exists("C:\eTdsWizard\traces\report.csv") Then
                dt_PanReport = GetDataTableFromCSV("C:\eTdsWizard\traces\report.csv")
            End If


            If blnDemoVersion = False Then

                'MessageBox.Show("Licensed", Application.ProductName, MessageBoxButtons.OK)
                filename = Forms.Application.StartupPath & "\" & TANDeductor &
                            "_" & dtBH.Rows(0)(4) & "_" & dtBH.Rows(0)(17) & "_" & dtBH.Rows(0)(16) & "_" & Today.Day & Today.Month & Today.Year & ".xls"
                'Ver 4.042-QC?? start
                ''Ver 4.041-QC?? 
                ''File.Copy(Forms.Application.StartupPath & "\Default-Notice-Forecaster.sys", filename, True)
                'If blnVerifyPANSkip = False Then
                ''    File.Copy(Forms.Application.StartupPath & "\Default-Notice-Forecaster.sys", filename, True)
                'End If
                ''Ver 4.041-QC?? end
                'File.Copy(Forms.Application.StartupPath & "\Default-Notice-Forecaster.sys", filename, True)
                If File.Exists(Application.StartupPath & "\Default-Notice-Forecaster.sys") Then
                    FileSystem.Rename(Application.StartupPath & "\Default-Notice-Forecaster.sys", Application.StartupPath & "\Default-Notice-Forecaster.xls")
                End If
                File.Copy(Forms.Application.StartupPath & "\Default-Notice-Forecaster.xls", filename, True)
                'Ver 4.042-QC?? end
                strDNFExcelFile = filename

                Dim excelFile As New Excel.Application
                Call StoreExcelProcess()
                ExlWorkbook = excelFile.Workbooks.Open(filename)
                Call GetExcelProcess()

                If blnNextFile = False Then
                    CreateHashTable()
                End If

                ' Loop over all sheets.
                For i As Integer = 1 To ExlWorkbook.Sheets.Count
                    ' Get sheet.
                    Dim sheet As Excel.Worksheet = ExlWorkbook.Sheets(i)
                    'Exlsheet = DirectCast(sheet, Excel.Worksheet)
                    'ExlRange = DirectCast(Exlsheet.Range("A3:A3"), Excel.Range)

                    Dim objCompute As Object
                    Dim decmlA As Decimal
                    Dim decmlB As Decimal
                    If sheet.Name = "Summary" Then

                        'Short Deduction Summary
                        objCompute = dtDD.Compute("SUM(AmountDeductible)", "ShortDedAmount>=10")
                        sheet.Cells(4, 1) = IIf(IsDBNull(objCompute), 0, objCompute)
                        decmlA = IIf(IsDBNull(objCompute), 0, objCompute)

                        objCompute = dtDD.Compute("SUM(TotTds)", "ShortDedAmount>=10")
                        sheet.Cells(4, 2) = IIf(IsDBNull(objCompute), 0, objCompute)
                        decmlB = IIf(IsDBNull(objCompute), 0, objCompute)

                        sheet.Cells(4, 3) = Math.Round(decmlA - decmlB, 0)

                        sheet.Cells(4, 4) = dtDD.Compute("Count(TotTds)", "ShortDedAmount>=10")
                        strProbshortDedAmt = Math.Round(decmlA - decmlB, 0).ToString

                        'Late Payment Summary
                        objCompute = dtDD.Compute("SUM(TotInterest)", "TotInterest>0")
                        sheet.Cells(8, 1) = IIf(IsDBNull(objCompute), 0, objCompute)
                        decmlA = IIf(IsDBNull(objCompute), 0, objCompute)
                        objCompute = dtCD.Compute("SUM(IntAmt)", "")
                        sheet.Cells(8, 2) = IIf(IsDBNull(objCompute), 0, objCompute)
                        decmlB = IIf(IsDBNull(objCompute), 0, objCompute)

                        'If decmlA > decmlB Then
                        sheet.Cells(8, 3) = decmlA - decmlB
                        strProbIntPay = (decmlA - decmlB).ToString
                        'Else
                        '    sheet.Cells(8, 3) = 0
                        '    strProbIntPay = "0"
                        'End If


                        sheet.Cells(8, 4) = "NA"
                        sheet.Cells(8, 5) = "NA"

                        'Short Deposit Summary
                        If blnVerifyChallanSkip = False Then
                            objCompute = dtCD.Compute("SUM(Total)", "IsChallanMatched=False")
                            sheet.Cells(13, 1) = IIf(IsDBNull(objCompute), 0, objCompute)
                            strProbshortDepAmt = IIf(IsDBNull(objCompute), 0, objCompute)
                        Else
                            sheet.Cells(13, 1) = 0
                            strProbshortDepAmt = 0
                        End If
                        sheet.Cells(13, 2) = "NA"

                        'File Details Summary
                        'Ver 2.02-REQ235 start
                        'sheet.Cells(16, 2) = dtBH.Rows(0)("TAN")   'TAN
                        'sheet.Cells(17, 2) = dtBH.Rows(0)("Name")   'Deductor
                        'sheet.Cells(18, 2) = Mid(dtBH.Rows(0)("Fin Yr"), 1, 4) & "-" & Mid(dtBH.Rows(0)("Fin Yr"), 5, 4) 'Fin Yr
                        'sheet.Cells(19, 2) = dtBH.Rows(0)("Period")  'Quarter
                        'sheet.Cells(20, 2) = dtBH.Rows(0)("Form")  'FormNo
                        sheet.Cells(20, 2) = dtBH.Rows(0)("TAN")   'TAN
                        sheet.Cells(21, 2) = dtBH.Rows(0)("Name")   'Deductor
                        sheet.Cells(22, 2) = Mid(dtBH.Rows(0)("Fin Yr"), 1, 4) & "-" & Mid(dtBH.Rows(0)("Fin Yr"), 5, 4) 'Fin Yr
                        sheet.Cells(23, 2) = dtBH.Rows(0)("Period")  'Quarter
                        sheet.Cells(24, 2) = dtBH.Rows(0)("Form")  'FormNo
                        'Ver 2.02-REQ235 end

                    ElseIf sheet.Name = "Short Deductions" Then
                        'Ver 7.03-REQ817 start code 
                        'dttempDDView.Sort = "ShortDedAmount"
                        'dttempDDView.RowFilter = "ShortDedAmount>=10"
                        Dim strShortDeductionVal As String
                        strShortDeductionVal = ShortDeductionVal()

                        If strShortDeductionVal <> "" Then
                            'Dim shortDeductionIntVal As Integer
                            'shortDeductionIntVal = Convert.ToInt32(strShortDeductionVal)
                            dttempDDView.RowFilter = "ShortDedAmount>=" + strShortDeductionVal + " or (Section='195' and (Reason1='R14' or Reason1='R15' or Reason1='R16' or Reason1='R17'  or Reason1='R18'  or Reason1='R19'))"
                        Else
                            dttempDDView.RowFilter = "ShortDedAmount>=10 or (Section='195' and (Reason1='R14' or Reason1='R15' or Reason1='R16' or Reason1='R17'  or Reason1='R18'  or Reason1='R19'))"
                        End If

                        'dttempDDView.RowFilter = "ShortDedAmount>=10 or (Section='195' and (Reason1='R14' or Reason1='R15' or Reason1='R16' or Reason1='R17'  or Reason1='R18'  or Reason1='R19'))" 'comment on 02/06/2020
                        'Ver 7.03-REQ817 end
                        'DumpToExcel(dttempDDView, sheet, hsShortDeductions, 3)
                        If dttempDDView.Count > 0 Then
                            DupmToArray(dttempDDView, hsShortDeductions)
                            sheet.Range("A3:T" & dttempDDView.Count + 2).Value = arrData
                            sheet.Range("A3:T" & dttempDDView.Count + 2).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Columns.AutoFit()
                            ExlRange = DirectCast(ExlWorkbook.Sheets(2).Range("A3:T" & dttempDDView.Count + 2), Excel.Range)
                            ExlRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic)
                        End If

                    ElseIf sheet.Name = "Late Payments" Then
                        dttempDDView.Sort = "TotInterest"
                        dttempDDView.RowFilter = "TotInterest>0"
                        'Call DumpToExcel(dttempDDView, sheet, hsLatePayments, 3)
                        If dttempDDView.Count > 0 Then
                            DupmToArray(dttempDDView, hsLatePayments)
                            sheet.Range("A3:Q" & dttempDDView.Count + 2).Value = arrData
                            sheet.Range("A3:Q" & dttempDDView.Count + 2).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Columns.AutoFit()
                            ExlRange = DirectCast(ExlWorkbook.Sheets(3).Range("A3:Q" & dttempDDView.Count + 2), Excel.Range)
                            ExlRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic)
                        End If
                    ElseIf sheet.Name = "Short Deposits" And blnVerifyChallanSkip = False Then
                        dttempCDView.Sort = "IsChallanMatched"
                        dttempCDView.RowFilter = "IsChallanMatched =False"
                        'DumpToExcel(dttempCDView, sheet, hsShortDeposits, 3)
                        If dttempCDView.Count > 0 Then
                            DupmToArray(dttempCDView, hsShortDeposits)
                            sheet.Range("A3:K" & dttempCDView.Count + 2).Value = arrData
                            sheet.Range("A3:K" & dttempCDView.Count + 2).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Columns.AutoFit()
                            ExlRange = DirectCast(ExlWorkbook.Sheets(4).Range("A3:K" & dttempCDView.Count + 2), Excel.Range)
                            ExlRange.BorderAround(Excel.XlLineStyle.xlLineStyleNone, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic)
                        End If
                    ElseIf sheet.Name = "Assumptions" Then

                        'Ver 2.02-REQ235 start
                    ElseIf sheet.Name = "Late Filing" Then

                        'Ver 3.02-REQ311 start
                        If Not (InStr("AS", strDeductorType) > 0 And blnIsBookEntry = True And
                                ((strFinYear = "201213" And (strFileQuarter = "Q2" Or strFileQuarter = "Q3" Or strFileQuarter = "Q4")) _
                                Or (strFinYear = "201314" And (strFileQuarter = "Q1" Or strFileQuarter = "Q2" Or strFileQuarter = "Q3")))) Then
                            'Ver 3.02-REQ311 end 


                            If blnCheckLateFiling = True Then
                                Dim strReturnDuedate As String
                                Dim intNoOfDelaydays As Integer
                                Dim dblFeesPerDay As Double
                                Dim intRecCount As Integer
                                Dim lngFeePayable As Long
                                Dim lngTotalFees As Long


                                Call LateFiling()
                                dtLateFiling.ReadXml(Application.StartupPath & "\LateFiling" & strFinYear.Substring(2, 4) & ".xml")


                                Dim dvLateFiling As New DataView(dtLateFiling)
                                dvLateFiling.RowFilter = "FormNo = '" & strFormNo & "' and Quarter = '" & strFileQuarter & "'"

                                For intRecCount = 0 To dvLateFiling.Count - 1
                                    If InStr(dvLateFiling.Item(intRecCount)("DeductorType").ToString, strDeductorType, CompareMethod.Text) > 0 Then
                                        dblFeesPerDay = Convert.ToDouble(dvLateFiling.Item(intRecCount)("Fee").ToString)
                                        strReturnDuedate = dvLateFiling.Item(intRecCount)("DueDate").ToString
                                        Exit For
                                    End If
                                Next

                                '<Bhaskaran N, 04Aug2020, LateFiling DueDate Change to 2021 for Q1 and Q2>
                                If strFinYear = "202021" Then
                                    strReturnDuedate = strReturnDuedate & "/" & Mid(strFinYear, 1, 2) & Mid(strFinYear, 5, 2)
                                Else
                                    If strFileQuarter = "Q1" Or strFileQuarter = "Q2" Then
                                        strReturnDuedate = strReturnDuedate & "/" & Mid(strFinYear, 1, 4)
                                    Else
                                        strReturnDuedate = strReturnDuedate & "/" & Mid(strFinYear, 1, 2) & Mid(strFinYear, 5, 2)
                                    End If
                                End If

                                intNoOfDelaydays = DayDifference(strReturnDuedate, strSubmittedDate)

                                If intNoOfDelaydays > 0 Then
                                    lngTotalFees = Math.Round(dblFeesPerDay * intNoOfDelaydays, 0)

                                    If dblTotTaxDeducted = 0 Then
                                        lngFeePayable = lngTotalFees
                                    Else
                                        lngFeePayable = Math.Round(IIf(dblTotTaxDeducted > lngTotalFees, lngTotalFees, dblTotTaxDeducted), 0)
                                    End If

                                    sheet.Cells(3, 1) = "1"
                                    sheet.Cells(3, 2) = strReturnDuedate
                                    sheet.Cells(3, 3) = strSubmittedDate
                                    sheet.Cells(3, 4) = intNoOfDelaydays
                                    sheet.Cells(3, 5) = dblFeesPerDay
                                    sheet.Cells(3, 6) = lngTotalFees  ''total Fee
                                    sheet.Cells(3, 7) = dblTotTaxDeducted 'Total TDS deducted
                                    sheet.Cells(3, 8) = lngFeePayable 'Fees Payable
                                    sheet.Cells(3, 9) = Math.Round(lngFee, 0) 'Fee Paid
                                    dblBalanceLateFilingFee = Math.Round(lngFeePayable - Math.Round(lngFee, 0), 0)   'Balance Fee
                                    sheet.Cells(3, 10) = dblBalanceLateFilingFee    'Balance Fee

                                    sheet.Range("A3:J3").Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    sheet.Columns.AutoFit()
                                    ExlRange = DirectCast(ExlWorkbook.Sheets(5).Range("A3:J3"), Excel.Range)
                                    ExlRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic)


                                    sheet = ExlWorkbook.Sheets(1)
                                    sheet.Activate()
                                    sheet.Cells(17, 1) = lngFeePayable
                                    sheet.Cells(17, 2) = Math.Round(lngFee, 0)
                                    sheet.Cells(17, 3) = dblBalanceLateFilingFee

                                End If
                                dvLateFiling.RowFilter = ""
                                dvLateFiling.Dispose()


                            End If
                            sheet = ExlWorkbook.Sheets(1)
                            sheet.Activate()
                            'Ver 2.02-REQ235 end

                            'Ver 3.02-REQ311 start
                        End If

                        'Ver 3.02-REQ311 end
                        '==>Jitendra
                        'Ver 5.05-REQ641 start
                        'Ver 5.05-REQ641-01 start the '01' number is added by gajanan to avoid unresolved issue by jitu
                        'ElseIf sheet.Name = "Invalid Salary Pans" Then
                        'anu_ETDSDNF
                        'ElseIf sheet.Name = "Invalid Salary Pans" And strFinYear >= "201314" Then

                        '    'Ver 5.05-REQ641-01 end
                        '    Dim dtTmpInvalidPAN1 As DataTable


                        '    dtTempSDView.RowFilter = "PANValid='N'"
                        '    dtTmpInvalidPAN1 = dtTempSDView.ToTable("InValidPANValid", True, "PAN", "Name")
                        '    Dim J As Integer
                        '    J = 2
                        '    For k As Integer = 0 To dtTmpInvalidPAN1.Rows.Count - 1
                        '        ' mStr = mStr & vbCrLf & dtTmpInvalidPAN1.Rows(k)("PAN").ToString & Chr(9) & dtTmpInvalidPAN1.Rows(k)("Name").ToString
                        '        sheet.Columns.AutoFit()

                        '        sheet.Cells(J, 1) = dtTmpInvalidPAN1.Rows(k)("PAN").ToString
                        '        sheet.Cells(J, 2) = dtTmpInvalidPAN1.Rows(k)("Name").ToString
                        '        J = J + 1
                        '    Next
                        '    dtTempSDView.Dispose()
                        '    dtTmpInvalidPAN1.Dispose()

                        '    'Ver 5.05-REQ641 end 

                        '    'Ver 7.03-REQ816 start    'Jitendra Started :
                        'anu_ETDSDNF
                    ElseIf sheet.Name = "PAN Verification" And strFinYear >= "201314" Then

                        'Ver 5.05-REQ641-01 end
                        'Dim dtTmpInvalidPAN1 As DataTable


                        'dtTempSDView.RowFilter = "PANValid='N'"
                        'dtTmpInvalidPAN1 = dtTempSDView.ToTable("InValidPANValid", True, "PAN", "Name")
                        For p As Integer = 1 To 4
                            'If sheet.Rows(p)(0).ToString() <> "PAN" Then
                            'sheet.Rows(p).Delete()
                            'End If

                            sheet.Cells(p, 1).Value = ""
                            sheet.Cells(p, 2).Value = ""
                            sheet.Cells(p, 3).Value = ""
                            sheet.Cells(p, 4).Value = ""


                        Next


                        'DeducteePanTable
                        Dim pannameDictionary As New Dictionary(Of String, String)()
                        For Each row As DataRow In DeducteePanTable.Rows
                            Dim pan As String = row("pan").ToString()
                            Dim name As String = row("name").ToString()
                            If Not pannameDictionary.ContainsKey(pan) Then
                                pannameDictionary.Add(pan, name)
                            End If
                        Next

                        'PAN PAN Name in deductee	PAN Name in Traces	Status
                        sheet.Rows(1).Font.Bold = True
                        'Dim cell As Excel.Range = sheet.Cells(1, 1)
                        'cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)
                        'Dim cell1 As Excel.Range = sheet.Cells(1, 2)
                        'cell1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

                        Dim cell1 As Excel.Range = sheet.Cells(1, 1)
                        Dim cell2 As Excel.Range = sheet.Cells(1, 2)
                        Dim cell3 As Excel.Range = sheet.Cells(1, 3)
                        Dim cell4 As Excel.Range = sheet.Cells(1, 4)
                        sheet.Rows(1).Font.Bold = True
                        cell1.Value = "PAN"
                        cell2.Value = "PAN Name in deductee"
                        cell3.Value = "PAN Name in Traces"
                        cell4.Value = "Status"

                        'ReleaseObject(cell1)
                        'ReleaseObject(cell2)
                        'ReleaseObject(cell3)
                        'ReleaseObject(cell4)
                        'ReleaseObject(sheet)

                        Dim J As Integer
                        J = 2

                        If dt_PanReport IsNot Nothing Then
                            For k As Integer = 0 To dt_PanReport.Rows.Count - 1
                                ' mStr = mStr & vbCrLf & dtTmpInvalidPAN1.Rows(k)("PAN").ToString & Chr(9) & dtTmpInvalidPAN1.Rows(k)("Name").ToString
                                sheet.Columns.AutoFit()



                                Dim nameFromDeducteePanTable As String = String.Empty
                                If pannameDictionary.ContainsKey(dt_PanReport.Rows(k)("PAN").ToString) Then
                                    nameFromDeducteePanTable = pannameDictionary(dt_PanReport.Rows(k)("PAN").ToString)
                                End If

                                'If J = 1 Then
                                '    sheet.Cell(J, 1) = "PAN"
                                '    sheet.Cell(J, 2) = "PAN Name in deductee"
                                '    sheet.Cell(J, 3) = "PAN Name in Traces"
                                '    sheet.Cell(J, 4) = "Status"

                                'Else
                                sheet.Rows(J).Font.Bold = False
                                sheet.Rows(J).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

                                sheet.Cells(J, 1) = dt_PanReport.Rows(k)("PAN").ToString
                                sheet.Cells(J, 2) = nameFromDeducteePanTable
                                sheet.Cells(J, 3) = dt_PanReport.Rows(k)("Name").ToString
                                sheet.Cells(J, 4) = dt_PanReport.Rows(k)("STATUS").ToString
                                'End If
                                J = J + 1
                            Next
                        End If


                    ElseIf sheet.Name = "Invalid Salary Pans" And strFinYear >= "201314" Then

                        'Ver 5.05-REQ641-01 end
                        'Dim dtTmpInvalidPAN1 As DataTable


                        'dtTempSDView.RowFilter = "PANValid='N'"
                        'dtTmpInvalidPAN1 = dtTempSDView.ToTable("InValidPANValid", True, "PAN", "Name")
                        Dim J As Integer
                        J = 2
                        If dt_PanReport IsNot Nothing Then
                            For k As Integer = 0 To dt_PanReport.Rows.Count - 1
                                ' mStr = mStr & vbCrLf & dtTmpInvalidPAN1.Rows(k)("PAN").ToString & Chr(9) & dtTmpInvalidPAN1.Rows(k)("Name").ToString
                                If dt_PanReport.Rows(k)("STATUS").ToString = "Invalid" Then
                                    sheet.Columns.AutoFit()
                                    sheet.Cells(J, 1) = dt_PanReport.Rows(k)("PAN").ToString
                                    sheet.Cells(J, 2) = dt_PanReport.Rows(k)("Name").ToString
                                    J = J + 1
                                End If
                            Next
                        End If
                    ElseIf sheet.Name = "TaxComputation&ShortDeduction" And strFinYear >= "201718" Then

                        Dim W As Integer
                        Dim isPenalApplied As Boolean

                        Dim dictSum As Dictionary(Of String, Double) = New Dictionary(Of String, Double)()
                        'isPenalApplied = False
                        W = 3
                        If dtTempSalary.Count > 0 Then

                            For k As Integer = 0 To dtTempSalary.Count - 1
                                Try

                                    sheet.Columns.AutoFit()
                                    sheet.Cells(W, 1) = dtTempSalary.Item(k).Row("SalSrNo").ToString()
                                    sheet.Cells(W, 2) = dtTempSalary.Item(k).Row("PAN").ToString()
                                    sheet.Cells(W, 3) = dtTempSalary.Item(k).Row("Name").ToString()
                                    sheet.Cells(W, 4) = dtTempSalary.Item(k).Row("Category").ToString()
                                    sheet.Cells(W, 5) = dtTempSalary.Item(k).Row("TotIncome").ToString()
                                    isPenalApplied = False
                                    If (dtTempSalary.Item(k).Row("PAN").ToString() = "PANINVALID" Or dtTempSalary.Item(k).Row("PAN").ToString() = "PANNOTAVBL" Or dtTempSalary.Item(k).Row("PAN").ToString() = "PANAPPLIED") Then
                                        isPenalApplied = True
                                    End If
                                    'If strFinYear >= "202021" And dtTempSalary.Item(k).Row("Taxation").ToString = "Y" Then

                                    '    dictSum = TaxSlabCalculation(dtTempSalary.Item(k).Row("TotIncome").ToString(), isPenalApplied, dtTempSalary.Item(k).Row("Taxation").ToString,
                                    '                             dtTempSalary.Item(k).Row("Relief89").ToString())
                                    'Else
                                    '    dictSum = TaxSlabCalculation(dtTempSalary.Item(k).Row("TotIncome").ToString(), isPenalApplied, dtTempSalary.Item(k).Row("Category").ToString(),
                                    '                             dtTempSalary.Item(k).Row("Relief89").ToString())
                                    'End If
                                    'MsgBox(dtTempSalary.Item(k).Row("Taxation"))
                                    If strFinYear >= "202324" Then
                                        If dtTempSalary.Item(k).Row("Taxation").ToString = "Y" Then
                                            dtTempSalary.Item(k).Row("Taxation") = "N"
                                        ElseIf dtTempSalary.Item(k).Row("Taxation").ToString = "N" Then
                                            dtTempSalary.Item(k).Row("Taxation") = "Y"
                                        End If
                                        'MsgBox(dtTempSalary.Item(k).Row("Taxation"))
                                    End If
                                    If dtTempSalary.Item(k).Row("PAN").ToString() = "BTGPM3215F" Then
                                        'MsgBox("anu")
                                    End If
                                    If strFinYear >= "202021" Then
                                        If dtTempSalary.Item(k).Row("Taxation").ToString = "Y" Then

                                            dictSum = TaxSlabCalculation(dtTempSalary.Item(k).Row("TotIncome").ToString(), isPenalApplied, dtTempSalary.Item(k).Row("Taxation").ToString(),
                                                                 dtTempSalary.Item(k).Row("Relief89").ToString())
                                        Else


                                            dictSum = TaxSlabCalculation(dtTempSalary.Item(k).Row("TotIncome").ToString(), isPenalApplied, dtTempSalary.Item(k).Row("Category").ToString(),
                                                                     dtTempSalary.Item(k).Row("Relief89").ToString())

                                        End If
                                    Else

                                        dictSum = TaxSlabCalculation(dtTempSalary.Item(k).Row("TotIncome").ToString(), isPenalApplied, dtTempSalary.Item(k).Row("Category").ToString(),
                                                             dtTempSalary.Item(k).Row("Relief89").ToString())
                                    End If

                                    'Date:10/07/2020, Name:Bhaskaran N, Description: To (-) 89Releife code from TaxGenerated by DNF <start><End>
                                    sheet.Cells(W, 6) = (dictSum.Item("GrossTax") + dictSum.Item("Surcharge") + dictSum.Item("EducationCess")) - dictSum.Item("Relief89")
                                    sheet.Cells(W, 7) = dtTempSalary.Item(k).Row("NetTax").ToString()
                                    sheet.Cells(W, 8) = dtTempSalary.Item(k).Row("FYearTax").ToString()
                                    Dim dblGrossTax As Double = (dictSum.Item("GrossTax") + dictSum.Item("Surcharge") + dictSum.Item("EducationCess")) - dictSum.Item("Relief89")
                                    Dim NetTax As Double = dtTempSalary.Item(k).Row("NetTax").ToString()
                                    Dim FYearTax As Double = dtTempSalary.Item(k).Row("FYearTax").ToString()

                                    If dblGrossTax = NetTax Then
                                        If FYearTax < dblGrossTax Then
                                            sheet.Cells(W, 9) = IIf(NetTax - FYearTax > 0, NetTax - FYearTax, 0)
                                            sheet.Cells(W, 10) = "Short Deduction"
                                        End If
                                    Else

                                        sheet.Cells(W, 9) = 0
                                        sheet.Cells(W, 10) = "Wrong Tax Computation"
                                    End If

                                    If dblGrossTax <> NetTax Or FYearTax < dblGrossTax Then
                                        W = W + 1
                                    Else
                                        sheet.Rows(W).Delete()
                                    End If

                                Catch ex As Exception

                                    'MsgBox(ex.ToString)

                                End Try
                            Next

                        End If
                        'Ver 7.03-REQ816 end 
                    End If

                Next

                'ExlRange = DirectCast(ExlWorkbook.Sheets(1).Range("A3:A3"), Excel.Range)
                'ExlRange.Font.Bold = True

                'ExlRange = DirectCast(ExlWorkbook.Sheets(4).Range("A3:K8"), Excel.Range)
                'ExlRange.BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic)
                ExlWorkbook.save()
                ' ExlWorkbook.Releaseobject()

                excelFile.Quit()

                Call KillProcess()

                'Ver 3.06-???? start
                Dim intAction As Integer
                '0-All,1-PANNameList,2-VerifyLowerrateCert
                'MessageBox.Show("blnChkListPANName = " & blnChkListPANName.ToString(), Application.ProductName, MessageBoxButtons.OK)

                If blnChkListPANName = True Then
                    intAction = 1
                    'Ver 6.00-REQ661 start
                    'ElseIf blnChkVerifyLowerRate = True Then
                    '    intAction = 2
                    'ElseIf blnChkVerifyLowerRate = True And blnChkListPANName = True Then
                    '    intAction = 0
                ElseIf blnChkVerifyLowerRate = True Then
                    intAction = 2
                ElseIf blnChkVerifyLowerRate = True And blnChkListPANName = True Then
                    intAction = 0
                    'Ver 6.00-REQ661 end
                Else
                    intAction = -1
                End If
                'Ver 4.042-QC?? start
                ''Ver 4.041-QC?? start
                ''Since the Traces exe is called earlier, To avoid second time calling following variable is modified.
                'If blnChkListPANName = True Then
                '    intAction = -1
                'End If
                ''Ver 4.041-QC?? end
                'Ver 4.042-QC?? end
                'MessageBox.Show("intAction = " & intAction.ToString(), Application.ProductName, MessageBoxButtons.OK)

                If intAction >= 0 Then

                    Dim strmsg As String = String.Empty
                    strmsg = "There is some changes made in the TRACES website,We are working hard to stabilize this issue."
                    strmsg &= vbCrLf & "We will try to resolve this ASAP."
                    strmsg &= vbCrLf & "We appreciate your understanding and patience."
                    'If (MessageBox.Show(strmsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information) <> DialogResult.OK) Then
                    '    If File.Exists(Application.StartupPath & "\TRACES.exe") = True Then
                    '        If File.Exists("C:\eTds" & ConstStrFYyr & "\Parameters.xml") = True Then
                    '            Using dsCert197Validate = New DataSet()
                    '                dsCert197Validate.ReadXml("C:\eTds" & ConstStrFYyr & "\Parameters.xml")
                    '                If Convert.ToBoolean(dsCert197Validate.Tables(0).Rows(0)("Cert197Validate")) = True Then
                    '                    Call CallProcessAndWait(Application.StartupPath & "\TRACES.exe", """~" & strFilePath & "~" & strDNFExcelFile & "~~~~~" & TANDeductor & "~DNF1~""")
                    '                Else
                    '                    Call CallProcessAndWait(Application.StartupPath & "\TRACES.exe", """~" & strFilePath & "~" & strDNFExcelFile & "~~~~~" & TANDeductor & "~DNF~""")
                    '                End If
                    '            End Using
                    '        Else
                    '            If blnChkVerifyLowerRate = True Then
                    '                Call CallProcessAndWait(Application.StartupPath & "\TRACES.exe", """~" & strFilePath & "~" & strDNFExcelFile & "~~~~~" & TANDeductor & "~DNF1~""")
                    '            Else
                    '                Call CallProcessAndWait(Application.StartupPath & "\TRACES.exe", """~" & strFilePath & "~" & strDNFExcelFile & "~~~~~" & TANDeductor & "~DNF~""")
                    '            End If
                    '        End If
                    '    Else
                    '        MessageBox.Show("TRACES.exe is not found in application folder.", "DNF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    End If
                    'End If
                    'CallTracesJAR()
                Else
                End If
                blnDNFCreationDelegate = True
            Else

                Dim objComputeValue As Object
                Dim decmlSUMAmountDeductible As Decimal
                Dim decmlSUMTotTds As Decimal
                Dim decmlSUMTotInterest As Decimal
                Dim decmlSUMIntAmt As Decimal

                'Short Deduction Summary
                objComputeValue = dtDD.Compute("SUM(AmountDeductible)", "ShortDedAmount>=10")
                decmlSUMAmountDeductible = IIf(IsDBNull(objComputeValue), 0, objComputeValue)

                objComputeValue = dtDD.Compute("SUM(TotTds)", "ShortDedAmount>=10")
                decmlSUMTotTds = IIf(IsDBNull(objComputeValue), 0, objComputeValue)

                strProbshortDedAmt = Math.Round(decmlSUMAmountDeductible - decmlSUMTotTds, 0).ToString

                'Late Payment Summary
                objComputeValue = dtDD.Compute("SUM(TotInterest)", "TotInterest>0")
                decmlSUMTotInterest = IIf(IsDBNull(objComputeValue), 0, objComputeValue)

                objComputeValue = dtCD.Compute("SUM(IntAmt)", "")
                decmlSUMIntAmt = IIf(IsDBNull(objComputeValue), 0, objComputeValue)

                strProbIntPay = (decmlSUMTotInterest - decmlSUMIntAmt).ToString

                'Short Deposit Summary
                If blnVerifyChallanSkip = False Then
                    objComputeValue = dtCD.Compute("SUM(Total)", "IsChallanMatched=False")
                    strProbshortDepAmt = IIf(IsDBNull(objComputeValue), 0, objComputeValue)
                Else
                    strProbshortDepAmt = 0
                End If

                blnDNFCreationDelegate = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Call KillProcess()
            blnFileFailure = True
            blnDNFCreationDelegate = True
            Dim trace = New Diagnostics.StackTrace(ex, True)
            Dim line As String = Strings.Right(trace.ToString, 5)
            Dim nombreMetodo As String = ""
            For Each sf As StackFrame In trace.GetFrames
                nombreMetodo = sf.GetMethod().Name & vbCrLf
            Next
            MessageBox.Show(ex.Message & vbCrLf & nombreMetodo & "_" & line, "DNF")
        End Try

    End Sub
    Function GetDataTableFromCSV(filePath As String) As DataTable
        Dim dt As New DataTable()
        Using parser As New Microsoft.VisualBasic.FileIO.TextFieldParser(filePath)
            parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            parser.SetDelimiters(",")

            Dim isFirstRow As Boolean = True

            Dim dts As DataSet = New DataSet
            While Not parser.EndOfData
                Dim fields As String() = parser.ReadFields()

                If isFirstRow Then
                    For Each field As String In fields
                        dt.Columns.Add(New DataColumn(field, GetType(String)))
                    Next
                    isFirstRow = False
                Else
                    Dim row As DataRow = dt.NewRow()
                    row.ItemArray = fields
                    dt.Rows.Add(row)
                End If
            End While
            dt.TableName = "PanReport"
            dt.AcceptChanges()
            'dts.Tables.Add(dt)
        End Using
        Return dt

        'Using parser As New TextFieldParser(filePath)
        '        parser.TextFieldType = FieldType.Delimited
        '        parser.SetDelimiters(",")

        '        ' Read the header line
        '        If Not parser.EndOfData Then
        'Dim headers As String() = parser.ReadFields()
        'For Each header As String In headers
        '                dt.Columns.Add(header)
        '            Next
        'End If

        '' Read the data lines
        'While Not parser.EndOfData
        'Dim fields As String() = parser.ReadFields()
        '            dt.Rows.Add(fields)
        '        End While
        'End Using

        'Return dt
    End Function

    Private Function CallTracesJAR() As Boolean


        Try

            If (File.Exists(Application.StartupPath + "\\TracesJAR.bat") = True) Then
                Dim fi As FileInfo = New FileInfo(Application.StartupPath + "\\TracesJAR.bat")
                If (fi.IsReadOnly = True) Then
                    Throw New Exception("ValidateFVU.bat file is in read only mode. Ensure this file is not open or not in read only mode.")
                End If
            End If

            Dim stremWriter As StreamWriter = New StreamWriter(Application.StartupPath + "\\TracesJAR.bat", False)
            stremWriter.WriteLine("@echo off")
            stremWriter.WriteLine("set path=C:\Program Files (x86)\Java\jre1.8.0_65\bin;%path%")
            stremWriter.WriteLine("set CLASSPATH=%CLASSPATH%;.;")
            stremWriter.WriteLine("javaw -jar " + Path.Combine(Application.StartupPath, "TracesLite.jar").ToString() + " " + "ACCESS" + "~" & strFilePath & "~" & strDNFExcelFile & "~~~Ffcs~2022-2023~" & TANDeductor & "~DNF~")
            'stremWriter.WriteLine("javaw -jar " + Path.Combine(Application.StartupPath, "TracesExe.jar").ToString() + " " + """~" & strFilePath & "~" & strDNFExcelFile & "~~~~~" & TANDeductor & "~DNF~""")
            stremWriter.Flush()
            stremWriter.Close()

            Dim startInfo As ProcessStartInfo = New ProcessStartInfo(Application.StartupPath + "\\TracesJAR.bat")
            startInfo.WindowStyle = ProcessWindowStyle.Hidden
            startInfo.CreateNoWindow = True

            Using p As Process = Process.Start(startInfo)
                p.StartInfo.CreateNoWindow = False
                p.WaitForExit()
                p.Close()
            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function ShortDeductionVal() As String

        Dim filepath As String = Application.StartupPath + "\ShortDeduction.txt"
        Dim strShortDeductionVal As String
        Dim objReader As System.IO.StreamReader

        If (File.Exists(filepath)) Then
            objReader = New System.IO.StreamReader(filepath)
            strShortDeductionVal = Convert.ToString(objReader.ReadLine())
            objReader.Close()
        End If
        Return strShortDeductionVal
    End Function
    'Public Sub DumpToExcel(ByRef dttempView As DataView, ByRef sheet As Excel.Worksheet, ByRef hs As Hashtable, ByVal intRow As Integer)
    '    Dim strDate As String
    '    For i = 0 To dttempView.Count - 1
    '        For j = 1 To hs.Count
    '            If Mid(hs(j), 1, 1) = "R" Then  'For Reason
    '                sheet.Cells(intRow, j) = htReason(dttempView.Item(i)(CInt(Mid(hs(j), 2))))
    '            ElseIf Mid(hs(j), 1, 1) = "D" Then
    '                strDate = dttempView.Item(i)(CInt(Mid(hs(j), 2)))
    '                If strDate <> "" Then
    '                    sheet.Cells(intRow, j) = Mid(strDate, 1, 2) & "/" & Mid(strDate, 3, 2) & "/" & Mid(strDate, 5, 4)
    '                End If
    '            ElseIf hs(j) = 998 Then
    '                sheet.Cells(intRow, j) = i + 1
    '            ElseIf hs(j) = 999 Then
    '                sheet.Cells(intRow, j) = ""
    '            ElseIf hs(j) = 997 Then  '''''''''''Deductee Status 1 For Company-C, 2 For NonCompany-N
    '                If dttempView.Item(i)(7) = "1" Then
    '                    sheet.Cells(intRow, j) = "C"
    '                Else
    '                    sheet.Cells(intRow, j) = "N"
    '                End If
    '            Else
    '                sheet.Cells(intRow, j) = dttempView.Item(i)(hs(j))
    '            End If
    '        Next
    '        intRow = intRow + 1
    '    Next


    'End Sub

    Public Sub CreateHashTable()
        'Datatable Index is column number starting from 0
        'Exclel Column starting from 1
        '(ExcelColumn,DataTableColumn)

        hsShortDeductions.Add(1, 998)   'Generate SrNo   
        hsShortDeductions.Add(2, 3)
        hsShortDeductions.Add(3, "D33")  'D For Date  D Indicates column Datatype as date and Number i.e 33 Indicates Column Number In Datatable
        hsShortDeductions.Add(4, 4)
        hsShortDeductions.Add(5, 9)
        hsShortDeductions.Add(6, 12)
        hsShortDeductions.Add(7, 997)  'Decuctee Code C/N
        'Ver 2.01-REQ233 start
        'hsShortDeductions.Add(8, 34)
        If strFinYear >= "201314" Then
            hsShortDeductions.Add(8, 32)
        Else
            hsShortDeductions.Add(8, 34)
        End If
        'Ver 2.01-REQ233 End
        hsShortDeductions.Add(9, 29)
        hsShortDeductions.Add(10, 999)  'Leave Blank
        hsShortDeductions.Add(11, 25)
        'Ver 2.01-REQ233 start
        'hsShortDeductions.Add(12, 36)
        If strFinYear >= "201314" Then
            'Ver 6.04-QC1239 start
            'hsShortDeductions.Add(12, 41)
            'If strFinYear >= "201617" And strFormNo = "27Q" Then
            If strFinYear >= "201617" Then 'the condition "strFormNo = "27Q"" is removed
                hsShortDeductions.Add(12, 45)
            Else
                hsShortDeductions.Add(12, 41)
            End If
            'Ver 6.04-QC1239 end
        Else
            hsShortDeductions.Add(12, 36)
        End If
        'Ver 2.01-REQ233 end
        hsShortDeductions.Add(13, "D22") 'D For Date
        hsShortDeductions.Add(14, 21)
        'Ver 2.01-REQ233 start
        'hsShortDeductions.Add(15, 37)
        If strFinYear >= "201314" Then
            'Ver 6.04-QC1239 start
            'hsShortDeductions.Add(15, 42)
            'If strFinYear >= "201617" And strFormNo = "27Q" Then
            If strFinYear >= "201617" Then 'the condition "strFormNo = "27Q"" is removed from point no. QC1239
                hsShortDeductions.Add(15, 46)
            Else
                hsShortDeductions.Add(15, 42)
            End If
            'Ver 6.04-QC1239 end
        Else
            hsShortDeductions.Add(15, 37)
        End If
        'Ver 2.01-REQ233 end
        hsShortDeductions.Add(16, 16)

        'Ver 2.01-REQ233 start
        'hsShortDeductions.Add(17, 38)
        'hsShortDeductions.Add(18, 48)
        If strFinYear >= "201314" Then
            'Ver 6.04-QC1239 start
            'hsShortDeductions.Add(17, 43)
            'hsShortDeductions.Add(18, 53)
            'If strFinYear >= "201617" And strFormNo = "27Q" Then
            If strFinYear >= "201617" Then 'the condition "strFormNo = "27Q"" is removed from point no. QC1239
                hsShortDeductions.Add(17, 47)
                hsShortDeductions.Add(18, 57)
            Else
                hsShortDeductions.Add(17, 43)
                hsShortDeductions.Add(18, 53)
            End If
            'Ver 6.04-QC1239 end
        Else
            hsShortDeductions.Add(17, 38)
            hsShortDeductions.Add(18, 48)
        End If
        'Ver 2.01-REQ233 End
        hsShortDeductions.Add(19, "D23") 'D For Date


        'Ver 2.01-REQ233 start
        '   hsShortDeductions.Add(20, "R44") ' For Reason 
        If strFinYear >= "201314" Then
            'Ver 6.04-QC1239 start
            'hsShortDeductions.Add(20, "R49") ' For Reason
            hsShortDeductions.Add(20, "R53") ' For Reason
            'Ver 6.04-QC1239 end
        Else
            hsShortDeductions.Add(20, "R44") ' For Reason
        End If
        'Ver 2.01-REQ233 End

        '(ExcelColumn,DataTableColumn)
        hsLatePayments.Add(1, 998)  'Generate SrNo   
        hsLatePayments.Add(2, 3)
        hsLatePayments.Add(3, 4)
        hsLatePayments.Add(4, 9)
        hsLatePayments.Add(5, 12)
        hsLatePayments.Add(6, "D22")  'D For Date
        hsLatePayments.Add(7, 999)  'Leave Blank
        hsLatePayments.Add(8, 18)
        hsLatePayments.Add(9, "D23") 'D For Date
        'Ver 2.01-REQ233 start
        ' hsLatePayments.Add(10, "D33")  'D For Date
        ' hsLatePayments.Add(11, "D50") 'D For Date
        ' hsLatePayments.Add(12, 39)
        'hsLatePayments.Add(13, 41)
        'hsLatePayments.Add(14, 40)
        'hsLatePayments.Add(15, 42)
        'hsLatePayments.Add(16, 49)
        'hsLatePayments.Add(17, "R45") 'For Reason

        If strFinYear >= "201314" Then
            'Ver 6.03-QC1205 start
            'hsLatePayments.Add(10, "D39")  'D For Date
            'hsLatePayments.Add(11, "D55") 'D For Date
            'hsLatePayments.Add(12, 44)
            'hsLatePayments.Add(13, 46)
            'hsLatePayments.Add(14, 45)
            'hsLatePayments.Add(15, 47)
            'hsLatePayments.Add(16, 54)
            'hsLatePayments.Add(17, "R50") 'For Reason

            If strFinYear >= "201314" And strFinYear < "201617" Then
                hsLatePayments.Add(10, "D39")  'D For Date
                hsLatePayments.Add(11, "D55") 'D For Date
                hsLatePayments.Add(12, 44)
                hsLatePayments.Add(13, 46)
                hsLatePayments.Add(14, 45)
                hsLatePayments.Add(15, 47)
                hsLatePayments.Add(16, 54)
                hsLatePayments.Add(17, "R50") 'For Reason
            ElseIf strFinYear >= "201617" Then
                hsLatePayments.Add(10, "D43")  'D For Date
                hsLatePayments.Add(11, "D59") 'D For Date
                hsLatePayments.Add(12, 48)
                hsLatePayments.Add(13, 50)
                hsLatePayments.Add(14, 49)
                hsLatePayments.Add(15, 51)
                hsLatePayments.Add(16, 58)
                hsLatePayments.Add(17, "R54") 'For Reason
            End If
            'Ver 6.03-QC1205 end
        Else
            hsLatePayments.Add(10, "D33")  'D For Date
            hsLatePayments.Add(11, "D50") 'D For Date
            hsLatePayments.Add(12, 39)
            hsLatePayments.Add(13, 41)
            hsLatePayments.Add(14, 40)
            hsLatePayments.Add(15, 42)
            hsLatePayments.Add(16, 49)
            hsLatePayments.Add(17, "R45") 'For Reason
        End If
        'Ver 2.01-REQ233 end

        '(ExcelColumn,DataTableColumn)
        hsShortDeposits.Add(1, 998) 'Generate SrNo   
        hsShortDeposits.Add(2, 3)
        hsShortDeposits.Add(3, 11)
        hsShortDeposits.Add(4, 15)
        hsShortDeposits.Add(5, "D17") 'D For Date
        hsShortDeposits.Add(6, 20)
        hsShortDeposits.Add(7, 26)
        hsShortDeposits.Add(8, 32)
        hsShortDeposits.Add(9, 26)
        hsShortDeposits.Add(10, 999)   ''Pending
        'Ver 2.01-REQ233 start
        'hsShortDeposits.Add(11, "R40")  'Reason
        'Ver 2.02-QC269 start
        'If strFinYear >= "201213" Then             
        '    If strFinYear >= "201314" Then
        '        hsShortDeposits.Add(11, "R42")  'Reason
        '    Else
        '        hsShortDeposits.Add(11, "R41")  'Reason
        '    End If
        'Else
        '    hsShortDeposits.Add(11, "R40")  'Reason
        'End If
        hsShortDeposits.Add(11, "R" & intCDColumnCount + 1)  'Reason
        'Ver 2.02-QC269 end

        'Ver 2.01-REQ233 End
        htReason.Add("R1", "TDS rate is lower than the prescribed Rate. Apply prescribed Rate OR update Reason for Non-Deduction.")
        htReason.Add("R2", "PAN Not found in ITD. Apply penal rate.")
        htReason.Add("R3", "TDS Amount is less than Amount Paid/Credited * TDS Rate")
        htReason.Add("R4", "Rate lower than the prescribed rate, Update reason for Non-deduction as A")
        htReason.Add("R5", "Delayed Deduction")
        htReason.Add("R6", "Delayed Deposit")
        htReason.Add("R7", "Delayed Deduction and Delayed Deposit")
        htReason.Add("R8", "Interest amount not present in Interest Allocated column")
        htReason.Add("R9", "Interest amount shown in interest allocated column is less than Interest amount")
        htReason.Add("R10", "Challan details not matching with CSI File")
        htReason.Add("R11", "DDuplicate CIN details . <TDS Deposit Amount> is different")
        htReason.Add("R12", "TdsAmt And TDS Rate is 0. Update valid Reason for Non-Deduction i.e. A,B,T,Y")
        'Ver 3.03-REQ329 start
        htReason.Add("R13", "TDS Amount is less than Amount Paid/Credited * PENAL Rate")
        'Ver 3.03-REQ329 end
        ''Ver 7.03-REQ817 start
        htReason.Add("R14", "DTAA :- Wrong rate mentioned")
        htReason.Add("R15", "DTAA :- Payment type is invalid")
        htReason.Add("R16", "DTAA :- Country code is invalid")
        htReason.Add("R17", "IT :- Wrong rate mentioned")
        htReason.Add("R18", "IT :- Payment type is invalid")
        htReason.Add("R19", "IT :- Country code is invalid")
        ''Ver 7.03-REQ817 end
    End Sub
    Public Sub StoreExcelProcess()
        Dim AllProcesses() As Process
        Dim CurrProcess As Process
        Dim blnError As Boolean = False
        aryExcel.Clear()
        Try

            AllProcesses = Process.GetProcessesByName("Excel")
            For Each CurrProcess In AllProcesses
                aryExcel.Add(CurrProcess.Id)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub GetExcelProcess()
        Dim AllProcesses() As Process
        Dim CurrProcess As Process
        Dim blnError As Boolean = False
        Try

            AllProcesses = Process.GetProcessesByName("Excel")
            For Each CurrProcess In AllProcesses
                If aryExcel.Contains(CurrProcess.Id) = True Then
                    strProcessID = CurrProcess.Id
                    Exit Sub
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub KillProcess()
        Dim AllProcesses() As Process
        Dim CurrProcess As Process
        Dim blnError As Boolean = False
        Try

            AllProcesses = Process.GetProcessesByName("Excel")
            For Each CurrProcess In AllProcesses
                If strProcessID = CurrProcess.Id Then
                    CurrProcess.Kill()
                    Exit Sub
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub DupmToArray(ByRef dttempView As DataView, ByRef hs As Hashtable)
        Dim strDate As String
        ReDim arrData(dttempView.Count, hs.Count)
        For i = 0 To dttempView.Count - 1
            For j = 1 To hs.Count
                If Mid(hs(j), 1, 1) = "R" Then  'For Reason
                    arrData(i, j - 1) = htReason(dttempView.Item(i)(CInt(Mid(hs(j), 2))))
                ElseIf Mid(hs(j), 1, 1) = "D" Then
                    strDate = dttempView.Item(i)(CInt(Mid(hs(j), 2)))
                    If strDate <> "" Then
                        arrData(i, j - 1) = Mid(strDate, 1, 2) & "/" & Mid(strDate, 3, 2) & "/" & Mid(strDate, 5, 4)
                    End If
                ElseIf hs(j) = 998 Then
                    arrData(i, j - 1) = i + 1
                ElseIf hs(j) = 999 Then
                    arrData(i, j - 1) = ""
                ElseIf hs(j) = 997 Then  '''''''''''Deductee Status 1 For Company-C, 2 For NonCompany-N
                    If dttempView.Item(i)(7) = "1" Then
                        arrData(i, j - 1) = "C"
                    Else
                        arrData(i, j - 1) = "N"
                    End If
                Else
                    arrData(i, j - 1) = dttempView.Item(i)(hs(j))
                End If
            Next
        Next
    End Sub
End Module
