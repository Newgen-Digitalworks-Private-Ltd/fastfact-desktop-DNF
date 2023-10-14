<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDownLoad
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDownLoad))
        Me.pnlUpgrade = New System.Windows.Forms.GroupBox()
        Me.lblRegistration = New System.Windows.Forms.Label()
        Me.pbProcess = New System.Windows.Forms.PictureBox()
        Me.cmdDownload = New System.Windows.Forms.Button()
        Me.pb = New System.Windows.Forms.ProgressBar()
        Me.lblVersionOnNet = New System.Windows.Forms.Label()
        Me.lblYourVersion = New System.Windows.Forms.Label()
        Me.lblLatestWebVer = New System.Windows.Forms.Label()
        Me.lblExitVer = New System.Windows.Forms.Label()
        Me.cmdRunUpgrade = New System.Windows.Forms.Button()
        Me.pbFNF = New System.Windows.Forms.PictureBox()
        Me.pnlGeneInfo = New System.Windows.Forms.Panel()
        Me.lblHeading = New System.Windows.Forms.Label()
        Me.Userstatus1 = New eTdsDNF2122.Userstatus()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlUpgrade.SuspendLayout()
        CType(Me.pbProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFNF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlGeneInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlUpgrade
        '
        Me.pnlUpgrade.Controls.Add(Me.lblRegistration)
        Me.pnlUpgrade.Controls.Add(Me.pbProcess)
        Me.pnlUpgrade.Controls.Add(Me.cmdDownload)
        Me.pnlUpgrade.Controls.Add(Me.pb)
        Me.pnlUpgrade.Controls.Add(Me.lblVersionOnNet)
        Me.pnlUpgrade.Controls.Add(Me.lblYourVersion)
        Me.pnlUpgrade.Controls.Add(Me.lblLatestWebVer)
        Me.pnlUpgrade.Controls.Add(Me.lblExitVer)
        Me.pnlUpgrade.Controls.Add(Me.cmdRunUpgrade)
        Me.pnlUpgrade.Location = New System.Drawing.Point(188, 49)
        Me.pnlUpgrade.Name = "pnlUpgrade"
        Me.pnlUpgrade.Size = New System.Drawing.Size(559, 368)
        Me.pnlUpgrade.TabIndex = 159
        Me.pnlUpgrade.TabStop = False
        '
        'lblRegistration
        '
        Me.lblRegistration.AutoSize = True
        Me.lblRegistration.BackColor = System.Drawing.Color.Transparent
        Me.lblRegistration.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRegistration.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistration.ForeColor = System.Drawing.Color.Red
        Me.lblRegistration.Location = New System.Drawing.Point(207, 68)
        Me.lblRegistration.Name = "lblRegistration"
        Me.lblRegistration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRegistration.Size = New System.Drawing.Size(175, 15)
        Me.lblRegistration.TabIndex = 122
        Me.lblRegistration.Text = "Your product is not registered"
        Me.lblRegistration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblRegistration.Visible = False
        '
        'pbProcess
        '
        Me.pbProcess.Image = CType(resources.GetObject("pbProcess.Image"), System.Drawing.Image)
        Me.pbProcess.Location = New System.Drawing.Point(275, 282)
        Me.pbProcess.Name = "pbProcess"
        Me.pbProcess.Size = New System.Drawing.Size(45, 48)
        Me.pbProcess.TabIndex = 121
        Me.pbProcess.TabStop = False
        Me.pbProcess.Visible = False
        '
        'cmdDownload
        '
        Me.cmdDownload.BackgroundImage = CType(resources.GetObject("cmdDownload.BackgroundImage"), System.Drawing.Image)
        Me.cmdDownload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cmdDownload.Enabled = False
        Me.cmdDownload.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDownload.ForeColor = System.Drawing.Color.Black
        Me.cmdDownload.Location = New System.Drawing.Point(106, 22)
        Me.cmdDownload.Name = "cmdDownload"
        Me.cmdDownload.Size = New System.Drawing.Size(87, 34)
        Me.cmdDownload.TabIndex = 106
        Me.cmdDownload.Text = "Download"
        Me.cmdDownload.UseVisualStyleBackColor = True
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(188, 227)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(204, 15)
        Me.pb.TabIndex = 117
        Me.pb.Visible = False
        '
        'lblVersionOnNet
        '
        Me.lblVersionOnNet.BackColor = System.Drawing.Color.Transparent
        Me.lblVersionOnNet.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVersionOnNet.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersionOnNet.ForeColor = System.Drawing.Color.Black
        Me.lblVersionOnNet.Location = New System.Drawing.Point(267, 147)
        Me.lblVersionOnNet.Name = "lblVersionOnNet"
        Me.lblVersionOnNet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVersionOnNet.Size = New System.Drawing.Size(143, 23)
        Me.lblVersionOnNet.TabIndex = 114
        Me.lblVersionOnNet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblVersionOnNet.Visible = False
        '
        'lblYourVersion
        '
        Me.lblYourVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblYourVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblYourVersion.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYourVersion.ForeColor = System.Drawing.Color.Black
        Me.lblYourVersion.Location = New System.Drawing.Point(267, 113)
        Me.lblYourVersion.Name = "lblYourVersion"
        Me.lblYourVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblYourVersion.Size = New System.Drawing.Size(143, 23)
        Me.lblYourVersion.TabIndex = 113
        Me.lblYourVersion.Text = " "
        Me.lblYourVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblYourVersion.Visible = False
        '
        'lblLatestWebVer
        '
        Me.lblLatestWebVer.BackColor = System.Drawing.Color.Transparent
        Me.lblLatestWebVer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLatestWebVer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLatestWebVer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLatestWebVer.Location = New System.Drawing.Point(123, 151)
        Me.lblLatestWebVer.Name = "lblLatestWebVer"
        Me.lblLatestWebVer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLatestWebVer.Size = New System.Drawing.Size(136, 15)
        Me.lblLatestWebVer.TabIndex = 111
        Me.lblLatestWebVer.Text = "Latest Version on Web"
        Me.lblLatestWebVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblLatestWebVer.Visible = False
        '
        'lblExitVer
        '
        Me.lblExitVer.BackColor = System.Drawing.Color.Transparent
        Me.lblExitVer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblExitVer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExitVer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblExitVer.Location = New System.Drawing.Point(111, 115)
        Me.lblExitVer.Name = "lblExitVer"
        Me.lblExitVer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblExitVer.Size = New System.Drawing.Size(149, 16)
        Me.lblExitVer.TabIndex = 110
        Me.lblExitVer.Text = "Your Existing Version"
        Me.lblExitVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblExitVer.Visible = False
        '
        'cmdRunUpgrade
        '
        Me.cmdRunUpgrade.AutoEllipsis = True
        Me.cmdRunUpgrade.BackgroundImage = CType(resources.GetObject("cmdRunUpgrade.BackgroundImage"), System.Drawing.Image)
        Me.cmdRunUpgrade.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cmdRunUpgrade.Enabled = False
        Me.cmdRunUpgrade.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRunUpgrade.ForeColor = System.Drawing.Color.Black
        Me.cmdRunUpgrade.Location = New System.Drawing.Point(341, 22)
        Me.cmdRunUpgrade.Name = "cmdRunUpgrade"
        Me.cmdRunUpgrade.Size = New System.Drawing.Size(87, 34)
        Me.cmdRunUpgrade.TabIndex = 107
        Me.cmdRunUpgrade.Text = "Upgrade"
        Me.cmdRunUpgrade.UseVisualStyleBackColor = True
        '
        'pbFNF
        '
        Me.pbFNF.BackColor = System.Drawing.Color.White
        Me.pbFNF.Image = Global.eTdsDNF2122.My.Resources.Resources.etdsndf12
        Me.pbFNF.Location = New System.Drawing.Point(-1, 0)
        Me.pbFNF.Name = "pbFNF"
        Me.pbFNF.Size = New System.Drawing.Size(150, 442)
        Me.pbFNF.TabIndex = 160
        Me.pbFNF.TabStop = False
        '
        'pnlGeneInfo
        '
        Me.pnlGeneInfo.BackColor = System.Drawing.Color.DarkGray
        Me.pnlGeneInfo.Controls.Add(Me.lblHeading)
        Me.pnlGeneInfo.Location = New System.Drawing.Point(180, 13)
        Me.pnlGeneInfo.Name = "pnlGeneInfo"
        Me.pnlGeneInfo.Size = New System.Drawing.Size(463, 32)
        Me.pnlGeneInfo.TabIndex = 161
        '
        'lblHeading
        '
        Me.lblHeading.BackColor = System.Drawing.Color.Transparent
        Me.lblHeading.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.ForeColor = System.Drawing.Color.White
        Me.lblHeading.Location = New System.Drawing.Point(14, 5)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(358, 26)
        Me.lblHeading.TabIndex = 0
        Me.lblHeading.Text = "Auto-Upgrade eTdsDNF"
        '
        'Userstatus1
        '
        Me.Userstatus1.BackgroundImage = CType(resources.GetObject("Userstatus1.BackgroundImage"), System.Drawing.Image)
        Me.Userstatus1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Userstatus1.Location = New System.Drawing.Point(-5, 443)
        Me.Userstatus1.Name = "Userstatus1"
        Me.Userstatus1.Size = New System.Drawing.Size(797, 98)
        Me.Userstatus1.TabIndex = 158
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.LightBlue
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(145, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(4, 444)
        Me.Panel4.TabIndex = 162
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Location = New System.Drawing.Point(695, 447)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnCancel.Size = New System.Drawing.Size(87, 34)
        Me.btnCancel.TabIndex = 163
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmDownLoad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(792, 542)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.pnlGeneInfo)
        Me.Controls.Add(Me.pbFNF)
        Me.Controls.Add(Me.pnlUpgrade)
        Me.Controls.Add(Me.Userstatus1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDownLoad"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "  DownLoad"
        Me.pnlUpgrade.ResumeLayout(False)
        Me.pnlUpgrade.PerformLayout()
        CType(Me.pbProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFNF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlGeneInfo.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Userstatus1 As eTdsDNF2122.Userstatus
    Friend WithEvents pnlUpgrade As System.Windows.Forms.GroupBox
    Friend WithEvents pbProcess As System.Windows.Forms.PictureBox
    Friend WithEvents cmdDownload As System.Windows.Forms.Button
    Public WithEvents pb As System.Windows.Forms.ProgressBar
    Public WithEvents lblVersionOnNet As System.Windows.Forms.Label
    Public WithEvents lblYourVersion As System.Windows.Forms.Label
    Public WithEvents lblLatestWebVer As System.Windows.Forms.Label
    Public WithEvents lblExitVer As System.Windows.Forms.Label
    Friend WithEvents cmdRunUpgrade As System.Windows.Forms.Button
    Friend WithEvents pbFNF As System.Windows.Forms.PictureBox
    Friend WithEvents pnlGeneInfo As System.Windows.Forms.Panel
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Public WithEvents btnCancel As System.Windows.Forms.Button
    Public WithEvents lblRegistration As System.Windows.Forms.Label
End Class
