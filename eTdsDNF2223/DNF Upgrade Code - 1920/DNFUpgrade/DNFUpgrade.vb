Imports System.IO

Public Class DNFUpgrade

    'Const strFinyear As String = "2223"
    Const strFinyear As String = "2324"
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        End
    End Sub

    Private Sub DNFUpgrade_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Hide()
        Dim GetTempPathName As String
        Dim mCurrFile As String
        Dim mDestFile As String
        Dim mFiles() As String
        Dim AllProcesses() As Process
        Dim CurrProcess As Process
        Dim mTempPath As String
        'Me.Show()
        'Kill the eform1wizard process before running upgrade
        Try

            AllProcesses = Process.GetProcessesByName("eTdsDNF" & strFinyear)
            For Each CurrProcess In AllProcesses
                CurrProcess.Kill()
                CurrProcess.WaitForExit(3000)
                CurrProcess.Dispose()
            Next

            GetTempPathName = Application.StartupPath
            lstFileNames.Items.Clear()
            lstFileNames.Refresh()
            Application.DoEvents()

            If Directory.Exists("C:\eTdsDNF" & strFinyear) = False Or File.Exists("C:\eTdsDNF" & strFinyear & "\eTdsDNF" & strFinyear & ".exe") = False Then
                MessageBox.Show("eTdsDNF" & strFinyear & " is not installed.", "DNF Upgrade", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End
            End If

            mTempPath = "C:\eTdsDNF" & strFinyear & "\temp"

            Pbr.Visible = True

            If Directory.Exists(GetTempPathName) = True And Application.StartupPath <> "C:\eTdsDNF" & strFinyear Then
                mFiles = Directory.GetFiles(GetTempPathName)
                Pbr.Minimum = 0
                Pbr.Maximum = mFiles.GetUpperBound(0) + 1
                Pbr.Value = 0
                For Each mCurrFile In Directory.GetFiles(GetTempPathName)
                    Pbr.Value += 1

                    'If Not (UCase(Path.GetFileName(mCurrFile)) = "ETDSDNF1213.EXE" Or UCase(Path.GetFileName(mCurrFile)) = "ETDSDNFUPGRADE.EXE") Then
                    If UCase(Path.GetFileName(mCurrFile)) <> "ETDSDNFUPGRADE.EXE" Then


                        If Not Directory.Exists(mTempPath) = True Then
                            Directory.CreateDirectory(mTempPath)
                        End If

                        mDestFile = "C:\eTdsDNF" & strFinyear & "\" & Path.GetFileName(mCurrFile)
                        If UCase(Path.GetFileName(mCurrFile)).StartsWith("TEMPLATE") = True Or UCase(Path.GetFileName(mCurrFile)).StartsWith("SAMPLE") = True Then
                            If File.Exists(mDestFile) = True Then
                                File.Copy(mDestFile, "C:\eTdsDNF" & strFinyear & "\" & Replace(Path.GetFileName(mCurrFile), ".xls", ".bak"), True)
                            End If
                        End If
                        lstFileNames.Items.Add("Updating..          " & Path.GetFileName(mCurrFile))

                        If File.Exists(mDestFile) = True Then
                            If ((File.GetAttributes(mDestFile) And FileAttributes.ReadOnly) = FileAttributes.ReadOnly) Then
                                File.SetAttributes(mDestFile, FileAttributes.Archive)
                            End If
                        End If
                        File.Copy(mCurrFile, mDestFile, True)
                    End If
                Next

            End If
            Pbr.Visible = False
            'If mFile.Exists("C:\eForm1Wizard0607\msinet.ocx") = True Then
            '    Shell("regsvr32 -s " & "C:\eForm1Wizard0607\msinet.ocx", AppWinStyle.Hide)
            'End If
            'Ver DNF1920 8.01 Start
            'If Not File.Exists("C:\eTdsDNF" & strFinyear & "\GoogleChromePortable\App\Chrome-bin\chrome.exe") Then
            '    If File.Exists("C:\eTdsDNF" & strFinyear & "\ChromePortable_online.paf.exe") Then
            '        MessageBox.Show("Please install Google Chrome Portable, Select the Language 'English', Then Click 'Agree' and Click 'Next' and then Click 'Finish'")
            '        Dim proc As New System.Diagnostics.Process()
            '        proc = Process.Start("C:\eTdsDNF" & strFinyear & "\ChromePortable_online.paf.exe", "")
            '    Else
            '        MessageBox.Show("ChromePortable_online.paf.exe File not found")
            '    End If
            'End If
            

            'Ver DNF1920 8.01 End
            MessageBox.Show("Upgradation successfully over..", "DNF Upgrade", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
