Imports System.IO

Public Class frmDownLoad

    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        cmdDownload.Enabled = False
        'If vbYes = MessageBox.Show("Do you want to download ?", "DNF", MessageBoxButtons.YesNo) Then
        'End If
        pb.Visible = True
        pbProcess.Visible = True
        Call GetFile("http://www.ffcs.in/downloads/DNFUpgrade" & ConstStrFYyr & ".exe", "000000", ConstStrFYyr, Application.StartupPath & "\Upgrade\DNFUpgrade" & ConstStrFYyr & ".exe")
        cmdDownload.Enabled = True
    End Sub

    Private Sub frmDownLoad_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    Private Sub frmDownLoad_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.CenterToScreen()
        lblYourVersion.Text = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision
        lblVersionOnNet.Text = strWebAppVersion
        cmdDownload.Enabled = True
        lblExitVer.Visible = True
        lblLatestWebVer.Visible = True
        lblYourVersion.Visible = True
        lblVersionOnNet.Visible = True

        'Ver 1.01-E585 start
        If blnDemoVersion = False Then
            If blnIsProductRegistered = False Then
                lblRegistration.Visible = True
            Else
                lblRegistration.Visible = False
            End If
        End If
        'Ver 1.01-E585 End

        If blnChkDNFIntegrate = True Then
            Me.Size = New System.Drawing.Size(798, 512)
        End If

    End Sub



    Private Sub frmDownLoad_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'If ValidVersion(ConstStrFYyr, My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision) = False Then
        'lblYourVersion.Text = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision
        'lblVersionOnNet.Text = strWebAppVersion
        'cmdDownload.Enabled = True
        'lblExitVer.Visible = True
        'lblLatestWebVer.Visible = True
        'lblYourVersion.Visible = True
        'lblVersionOnNet.Visible = True
        'End If
    End Sub

    Private Sub cmdRunUpgrade_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRunUpgrade.Click
        Process.Start(Application.StartupPath & "\Upgrade\DNFUpgrade" & ConstStrFYyr & ".exe")
        End
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        frmFileSelection.Show()
        Me.Hide()
    End Sub

    Private Sub Userstatus1_Load(sender As Object, e As EventArgs) Handles Userstatus1.Load

    End Sub

    Private Sub pnlUpgrade_Enter(sender As Object, e As EventArgs) Handles pnlUpgrade.Enter

    End Sub

    Private Sub frmDownLoad_MinimumSizeChanged(sender As Object, e As EventArgs) Handles Me.MinimumSizeChanged

    End Sub
End Class