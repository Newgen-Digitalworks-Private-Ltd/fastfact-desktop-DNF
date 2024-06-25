<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcess
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProcess))
        Me.lblFileSelection = New System.Windows.Forms.Label()
        Me.lblChallanMatching = New System.Windows.Forms.Label()
        Me.lblPANValidations = New System.Windows.Forms.Label()
        Me.lblForecasting = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblTan = New System.Windows.Forms.Label()
        Me.lblFy = New System.Windows.Forms.Label()
        Me.lblQ = New System.Windows.Forms.Label()
        Me.lblDedName = New System.Windows.Forms.Label()
        Me.lblCsiFile = New System.Windows.Forms.Label()
        Me.lblchlStatus = New System.Windows.Forms.Label()
        Me.lblPanStatus = New System.Windows.Forms.Label()
        Me.lblDNFStatus = New System.Windows.Forms.Label()
        Me.lblProbshortDedAmt = New System.Windows.Forms.Label()
        Me.lblProbshortDepAmt = New System.Windows.Forms.Label()
        Me.lblProbIntPay = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.lblSummary = New System.Windows.Forms.Label()
        Me.lblPanProcess1 = New System.Windows.Forms.LinkLabel()
        Me.lblPanProcess = New System.Windows.Forms.Label()
        Me.lblUnmatchchlStatus = New System.Windows.Forms.LinkLabel()
        Me.lblOpenFile = New System.Windows.Forms.LinkLabel()
        Me.lblOpenFolder = New System.Windows.Forms.LinkLabel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lblHead = New System.Windows.Forms.Label()
        Me.lblHead3 = New System.Windows.Forms.Label()
        Me.lblHead2 = New System.Windows.Forms.Label()
        Me.lblHead1 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.lblProbshortDedAmt1 = New System.Windows.Forms.Label()
        Me.lblProbshortDepAmt1 = New System.Windows.Forms.Label()
        Me.lblProbIntPay1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFormNo = New System.Windows.Forms.Label()
        Me.lblIgnoredPANList = New System.Windows.Forms.LinkLabel()
        Me.lblLateFilingFee = New System.Windows.Forms.Label()
        Me.lblLateFilingFeeText = New System.Windows.Forms.Label()
        Me.PANValidateBrowser = New System.Windows.Forms.WebBrowser()
        Me.btnePayment = New System.Windows.Forms.Button()
        Me.pbFNF = New System.Windows.Forms.PictureBox()
        Me.pctSummaryCorrect = New System.Windows.Forms.PictureBox()
        Me.pctProcess3 = New System.Windows.Forms.PictureBox()
        Me.pctProcess4 = New System.Windows.Forms.PictureBox()
        Me.pctDNFWrong = New System.Windows.Forms.PictureBox()
        Me.cmdShowData = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.pctProcess1 = New System.Windows.Forms.PictureBox()
        Me.pctProcess2 = New System.Windows.Forms.PictureBox()
        Me.pctFileWrong = New System.Windows.Forms.PictureBox()
        Me.pctFileCorrect = New System.Windows.Forms.PictureBox()
        Me.pctChallanWrong = New System.Windows.Forms.PictureBox()
        Me.pctChallanCorrect = New System.Windows.Forms.PictureBox()
        Me.pctPANWrong = New System.Windows.Forms.PictureBox()
        Me.pctForeCastCorrect = New System.Windows.Forms.PictureBox()
        Me.pctPANCorrect = New System.Windows.Forms.PictureBox()
        Me.Userstatus1 = New eTdsDNF2223.Userstatus()
        CType(Me.pbFNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctSummaryCorrect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctProcess3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctProcess4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctDNFWrong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctProcess1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctProcess2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctFileWrong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctFileCorrect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctChallanWrong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctChallanCorrect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctPANWrong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctForeCastCorrect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctPANCorrect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFileSelection
        '
        Me.lblFileSelection.AutoSize = True
        Me.lblFileSelection.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileSelection.Location = New System.Drawing.Point(167, 68)
        Me.lblFileSelection.Name = "lblFileSelection"
        Me.lblFileSelection.Size = New System.Drawing.Size(93, 15)
        Me.lblFileSelection.TabIndex = 134
        Me.lblFileSelection.Text = "Input File Name"
        '
        'lblChallanMatching
        '
        Me.lblChallanMatching.AutoSize = True
        Me.lblChallanMatching.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanMatching.Location = New System.Drawing.Point(167, 149)
        Me.lblChallanMatching.Name = "lblChallanMatching"
        Me.lblChallanMatching.Size = New System.Drawing.Size(104, 15)
        Me.lblChallanMatching.TabIndex = 134
        Me.lblChallanMatching.Text = "Challan Matching"
        '
        'lblPANValidations
        '
        Me.lblPANValidations.AutoSize = True
        Me.lblPANValidations.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPANValidations.Location = New System.Drawing.Point(167, 230)
        Me.lblPANValidations.Name = "lblPANValidations"
        Me.lblPANValidations.Size = New System.Drawing.Size(95, 15)
        Me.lblPANValidations.TabIndex = 134
        Me.lblPANValidations.Text = "PAN Validations"
        '
        'lblForecasting
        '
        Me.lblForecasting.AutoSize = True
        Me.lblForecasting.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblForecasting.Location = New System.Drawing.Point(167, 311)
        Me.lblForecasting.Name = "lblForecasting"
        Me.lblForecasting.Size = New System.Drawing.Size(74, 15)
        Me.lblForecasting.TabIndex = 134
        Me.lblForecasting.Text = "Forecasting"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Black
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Location = New System.Drawing.Point(278, 1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(4, 444)
        Me.Panel4.TabIndex = 135
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Orange
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Location = New System.Drawing.Point(697, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(4, 444)
        Me.Panel1.TabIndex = 136
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.LightBlue
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel5.Location = New System.Drawing.Point(125, 114)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(782, 4)
        Me.Panel5.TabIndex = 137
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightBlue
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Location = New System.Drawing.Point(125, 195)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(763, 4)
        Me.Panel2.TabIndex = 138
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightBlue
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Location = New System.Drawing.Point(125, 276)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(763, 4)
        Me.Panel3.TabIndex = 139
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.LightBlue
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel6.Location = New System.Drawing.Point(125, 357)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(763, 4)
        Me.Panel6.TabIndex = 140
        '
        'lblTan
        '
        Me.lblTan.AutoSize = True
        Me.lblTan.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTan.Location = New System.Drawing.Point(289, 55)
        Me.lblTan.Name = "lblTan"
        Me.lblTan.Size = New System.Drawing.Size(35, 15)
        Me.lblTan.TabIndex = 134
        Me.lblTan.Text = "TAN :"
        '
        'lblFy
        '
        Me.lblFy.AutoSize = True
        Me.lblFy.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFy.Location = New System.Drawing.Point(418, 55)
        Me.lblFy.Name = "lblFy"
        Me.lblFy.Size = New System.Drawing.Size(26, 15)
        Me.lblFy.TabIndex = 134
        Me.lblFy.Text = "FY :"
        '
        'lblQ
        '
        Me.lblQ.AutoSize = True
        Me.lblQ.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQ.Location = New System.Drawing.Point(501, 55)
        Me.lblQ.Name = "lblQ"
        Me.lblQ.Size = New System.Drawing.Size(57, 15)
        Me.lblQ.TabIndex = 134
        Me.lblQ.Text = "Quarter :"
        '
        'lblDedName
        '
        Me.lblDedName.AutoSize = True
        Me.lblDedName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDedName.Location = New System.Drawing.Point(289, 85)
        Me.lblDedName.Name = "lblDedName"
        Me.lblDedName.Size = New System.Drawing.Size(46, 15)
        Me.lblDedName.TabIndex = 134
        Me.lblDedName.Text = "Name :"
        '
        'lblCsiFile
        '
        Me.lblCsiFile.AutoSize = True
        Me.lblCsiFile.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCsiFile.Location = New System.Drawing.Point(289, 134)
        Me.lblCsiFile.Name = "lblCsiFile"
        Me.lblCsiFile.Size = New System.Drawing.Size(90, 15)
        Me.lblCsiFile.TabIndex = 134
        Me.lblCsiFile.Text = "CSI File Name :"
        '
        'lblchlStatus
        '
        Me.lblchlStatus.AutoSize = True
        Me.lblchlStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchlStatus.Location = New System.Drawing.Point(289, 164)
        Me.lblchlStatus.Name = "lblchlStatus"
        Me.lblchlStatus.Size = New System.Drawing.Size(53, 15)
        Me.lblchlStatus.TabIndex = 134
        Me.lblchlStatus.Text = "Status  :"
        '
        'lblPanStatus
        '
        Me.lblPanStatus.AutoSize = True
        Me.lblPanStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPanStatus.Location = New System.Drawing.Point(289, 213)
        Me.lblPanStatus.Name = "lblPanStatus"
        Me.lblPanStatus.Size = New System.Drawing.Size(53, 15)
        Me.lblPanStatus.TabIndex = 134
        Me.lblPanStatus.Text = "Status  :"
        '
        'lblDNFStatus
        '
        Me.lblDNFStatus.AutoSize = True
        Me.lblDNFStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDNFStatus.Location = New System.Drawing.Point(289, 296)
        Me.lblDNFStatus.Name = "lblDNFStatus"
        Me.lblDNFStatus.Size = New System.Drawing.Size(159, 15)
        Me.lblDNFStatus.TabIndex = 134
        Me.lblDNFStatus.Text = "Status : DNF File Generated"
        '
        'lblProbshortDedAmt
        '
        Me.lblProbshortDedAmt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProbshortDedAmt.Location = New System.Drawing.Point(289, 367)
        Me.lblProbshortDedAmt.Name = "lblProbshortDedAmt"
        Me.lblProbshortDedAmt.Size = New System.Drawing.Size(218, 15)
        Me.lblProbshortDedAmt.TabIndex = 134
        Me.lblProbshortDedAmt.Text = "Probable Short Deduction Amount     :"
        '
        'lblProbshortDepAmt
        '
        Me.lblProbshortDepAmt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProbshortDepAmt.Location = New System.Drawing.Point(289, 385)
        Me.lblProbshortDepAmt.Name = "lblProbshortDepAmt"
        Me.lblProbshortDepAmt.Size = New System.Drawing.Size(218, 15)
        Me.lblProbshortDepAmt.TabIndex = 134
        Me.lblProbshortDepAmt.Text = "Probable Short Deposit Amount         :"
        '
        'lblProbIntPay
        '
        Me.lblProbIntPay.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProbIntPay.Location = New System.Drawing.Point(289, 403)
        Me.lblProbIntPay.Name = "lblProbIntPay"
        Me.lblProbIntPay.Size = New System.Drawing.Size(218, 15)
        Me.lblProbIntPay.TabIndex = 134
        Me.lblProbIntPay.Text = "Probable Interest Payable                    :"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.LightBlue
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel9.Location = New System.Drawing.Point(149, 1)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(4, 444)
        Me.Panel9.TabIndex = 153
        '
        'lblSummary
        '
        Me.lblSummary.AutoSize = True
        Me.lblSummary.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSummary.Location = New System.Drawing.Point(167, 392)
        Me.lblSummary.Name = "lblSummary"
        Me.lblSummary.Size = New System.Drawing.Size(62, 15)
        Me.lblSummary.TabIndex = 160
        Me.lblSummary.Text = "Summary"
        '
        'lblPanProcess1
        '
        Me.lblPanProcess1.AutoSize = True
        Me.lblPanProcess1.Location = New System.Drawing.Point(289, 249)
        Me.lblPanProcess1.Name = "lblPanProcess1"
        Me.lblPanProcess1.Size = New System.Drawing.Size(51, 14)
        Me.lblPanProcess1.TabIndex = 162
        Me.lblPanProcess1.TabStop = True
        Me.lblPanProcess1.Text = "Status  :"
        Me.lblPanProcess1.Visible = False
        '
        'lblPanProcess
        '
        Me.lblPanProcess.AutoSize = True
        Me.lblPanProcess.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPanProcess.Location = New System.Drawing.Point(292, 249)
        Me.lblPanProcess.Name = "lblPanProcess"
        Me.lblPanProcess.Size = New System.Drawing.Size(53, 15)
        Me.lblPanProcess.TabIndex = 163
        Me.lblPanProcess.Text = "Status  :"
        '
        'lblUnmatchchlStatus
        '
        Me.lblUnmatchchlStatus.AutoSize = True
        Me.lblUnmatchchlStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnmatchchlStatus.Location = New System.Drawing.Point(491, 164)
        Me.lblUnmatchchlStatus.Name = "lblUnmatchchlStatus"
        Me.lblUnmatchchlStatus.Size = New System.Drawing.Size(103, 15)
        Me.lblUnmatchchlStatus.TabIndex = 164
        Me.lblUnmatchchlStatus.TabStop = True
        Me.lblUnmatchchlStatus.Text = "Unmatch Challan"
        Me.lblUnmatchchlStatus.Visible = False
        '
        'lblOpenFile
        '
        Me.lblOpenFile.AutoSize = True
        Me.lblOpenFile.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpenFile.Location = New System.Drawing.Point(289, 328)
        Me.lblOpenFile.Name = "lblOpenFile"
        Me.lblOpenFile.Size = New System.Drawing.Size(84, 15)
        Me.lblOpenFile.TabIndex = 164
        Me.lblOpenFile.TabStop = True
        Me.lblOpenFile.Text = "Open DNF File"
        Me.lblOpenFile.Visible = False
        '
        'lblOpenFolder
        '
        Me.lblOpenFolder.AutoSize = True
        Me.lblOpenFolder.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpenFolder.Location = New System.Drawing.Point(397, 328)
        Me.lblOpenFolder.Name = "lblOpenFolder"
        Me.lblOpenFolder.Size = New System.Drawing.Size(166, 15)
        Me.lblOpenFolder.TabIndex = 164
        Me.lblOpenFolder.TabStop = True
        Me.lblOpenFolder.Text = " Open Folder Containing DNF"
        Me.lblOpenFolder.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.LightBlue
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel7.Location = New System.Drawing.Point(149, 33)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(763, 4)
        Me.Panel7.TabIndex = 166
        '
        'lblHead
        '
        Me.lblHead.BackColor = System.Drawing.Color.LightGray
        Me.lblHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHead.Location = New System.Drawing.Point(149, 0)
        Me.lblHead.Name = "lblHead"
        Me.lblHead.Size = New System.Drawing.Size(645, 34)
        Me.lblHead.TabIndex = 167
        '
        'lblHead3
        '
        Me.lblHead3.AutoSize = True
        Me.lblHead3.BackColor = System.Drawing.Color.LightGray
        Me.lblHead3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHead3.ForeColor = System.Drawing.Color.Black
        Me.lblHead3.Location = New System.Drawing.Point(723, 9)
        Me.lblHead3.Name = "lblHead3"
        Me.lblHead3.Size = New System.Drawing.Size(47, 16)
        Me.lblHead3.TabIndex = 173
        Me.lblHead3.Text = "Status"
        '
        'lblHead2
        '
        Me.lblHead2.AutoSize = True
        Me.lblHead2.BackColor = System.Drawing.Color.LightGray
        Me.lblHead2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHead2.ForeColor = System.Drawing.Color.Black
        Me.lblHead2.Location = New System.Drawing.Point(435, 9)
        Me.lblHead2.Name = "lblHead2"
        Me.lblHead2.Size = New System.Drawing.Size(51, 16)
        Me.lblHead2.TabIndex = 174
        Me.lblHead2.Text = "Details"
        '
        'lblHead1
        '
        Me.lblHead1.AutoSize = True
        Me.lblHead1.BackColor = System.Drawing.Color.LightGray
        Me.lblHead1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHead1.ForeColor = System.Drawing.Color.Black
        Me.lblHead1.Location = New System.Drawing.Point(190, 9)
        Me.lblHead1.Name = "lblHead1"
        Me.lblHead1.Size = New System.Drawing.Size(43, 16)
        Me.lblHead1.TabIndex = 172
        Me.lblHead1.Text = "Steps"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Gray
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel8.Location = New System.Drawing.Point(278, -16)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(2, 52)
        Me.Panel8.TabIndex = 176
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Gray
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel10.Location = New System.Drawing.Point(697, -16)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(2, 52)
        Me.Panel10.TabIndex = 177
        '
        'lblProbshortDedAmt1
        '
        Me.lblProbshortDedAmt1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProbshortDedAmt1.Location = New System.Drawing.Point(511, 367)
        Me.lblProbshortDedAmt1.Name = "lblProbshortDedAmt1"
        Me.lblProbshortDedAmt1.Size = New System.Drawing.Size(83, 15)
        Me.lblProbshortDedAmt1.TabIndex = 178
        Me.lblProbshortDedAmt1.Text = "0"
        Me.lblProbshortDedAmt1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProbshortDepAmt1
        '
        Me.lblProbshortDepAmt1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProbshortDepAmt1.Location = New System.Drawing.Point(511, 385)
        Me.lblProbshortDepAmt1.Name = "lblProbshortDepAmt1"
        Me.lblProbshortDepAmt1.Size = New System.Drawing.Size(83, 15)
        Me.lblProbshortDepAmt1.TabIndex = 179
        Me.lblProbshortDepAmt1.Text = "0"
        Me.lblProbshortDepAmt1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProbIntPay1
        '
        Me.lblProbIntPay1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProbIntPay1.Location = New System.Drawing.Point(511, 403)
        Me.lblProbIntPay1.Name = "lblProbIntPay1"
        Me.lblProbIntPay1.Size = New System.Drawing.Size(83, 15)
        Me.lblProbIntPay1.TabIndex = 180
        Me.lblProbIntPay1.Text = "0"
        Me.lblProbIntPay1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(292, 212)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(379, 33)
        Me.Label1.TabIndex = 181
        Me.Label1.Text = "This feature is currently not available as we are awaiting permission from Income" &
    "-Tax-Department"
        Me.Label1.Visible = False
        '
        'lblFormNo
        '
        Me.lblFormNo.AutoSize = True
        Me.lblFormNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormNo.Location = New System.Drawing.Point(587, 56)
        Me.lblFormNo.Name = "lblFormNo"
        Me.lblFormNo.Size = New System.Drawing.Size(60, 15)
        Me.lblFormNo.TabIndex = 182
        Me.lblFormNo.Text = "Form No :"
        '
        'lblIgnoredPANList
        '
        Me.lblIgnoredPANList.AutoSize = True
        Me.lblIgnoredPANList.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIgnoredPANList.Location = New System.Drawing.Point(588, 246)
        Me.lblIgnoredPANList.Name = "lblIgnoredPANList"
        Me.lblIgnoredPANList.Size = New System.Drawing.Size(100, 15)
        Me.lblIgnoredPANList.TabIndex = 183
        Me.lblIgnoredPANList.TabStop = True
        Me.lblIgnoredPANList.Text = "Ignored PAN List"
        Me.lblIgnoredPANList.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIgnoredPANList.Visible = False
        '
        'lblLateFilingFee
        '
        Me.lblLateFilingFee.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLateFilingFee.Location = New System.Drawing.Point(511, 421)
        Me.lblLateFilingFee.Name = "lblLateFilingFee"
        Me.lblLateFilingFee.Size = New System.Drawing.Size(83, 15)
        Me.lblLateFilingFee.TabIndex = 185
        Me.lblLateFilingFee.Text = "0"
        Me.lblLateFilingFee.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLateFilingFeeText
        '
        Me.lblLateFilingFeeText.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLateFilingFeeText.Location = New System.Drawing.Point(289, 421)
        Me.lblLateFilingFeeText.Name = "lblLateFilingFeeText"
        Me.lblLateFilingFeeText.Size = New System.Drawing.Size(218, 15)
        Me.lblLateFilingFeeText.TabIndex = 184
        Me.lblLateFilingFeeText.Text = "Probable Late Filing Fee                        :"
        '
        'PANValidateBrowser
        '
        Me.PANValidateBrowser.Location = New System.Drawing.Point(439, 237)
        Me.PANValidateBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.PANValidateBrowser.Name = "PANValidateBrowser"
        Me.PANValidateBrowser.Size = New System.Drawing.Size(34, 23)
        Me.PANValidateBrowser.TabIndex = 186
        Me.PANValidateBrowser.Visible = False
        '
        'btnePayment
        '
        Me.btnePayment.AutoSize = True
        Me.btnePayment.BackColor = System.Drawing.Color.Transparent
        Me.btnePayment.BackgroundImage = CType(resources.GetObject("btnePayment.BackgroundImage"), System.Drawing.Image)
        Me.btnePayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnePayment.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnePayment.FlatAppearance.BorderSize = 0
        Me.btnePayment.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnePayment.ForeColor = System.Drawing.Color.Black
        Me.btnePayment.Location = New System.Drawing.Point(603, 403)
        Me.btnePayment.Name = "btnePayment"
        Me.btnePayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnePayment.Size = New System.Drawing.Size(87, 35)
        Me.btnePayment.TabIndex = 258
        Me.btnePayment.UseVisualStyleBackColor = False
        Me.btnePayment.Visible = False
        '
        'pbFNF
        '
        Me.pbFNF.BackColor = System.Drawing.Color.White
        Me.pbFNF.Image = Global.eTdsDNF2223.My.Resources.Resources.etdsndf1
        Me.pbFNF.Location = New System.Drawing.Point(-1, 1)
        Me.pbFNF.Name = "pbFNF"
        Me.pbFNF.Size = New System.Drawing.Size(150, 442)
        Me.pbFNF.TabIndex = 154
        Me.pbFNF.TabStop = False
        '
        'pctSummaryCorrect
        '
        Me.pctSummaryCorrect.Image = CType(resources.GetObject("pctSummaryCorrect.Image"), System.Drawing.Image)
        Me.pctSummaryCorrect.Location = New System.Drawing.Point(729, 383)
        Me.pctSummaryCorrect.Name = "pctSummaryCorrect"
        Me.pctSummaryCorrect.Size = New System.Drawing.Size(35, 36)
        Me.pctSummaryCorrect.TabIndex = 175
        Me.pctSummaryCorrect.TabStop = False
        Me.pctSummaryCorrect.Visible = False
        '
        'pctProcess3
        '
        Me.pctProcess3.Image = CType(resources.GetObject("pctProcess3.Image"), System.Drawing.Image)
        Me.pctProcess3.Location = New System.Drawing.Point(704, 206)
        Me.pctProcess3.Name = "pctProcess3"
        Me.pctProcess3.Size = New System.Drawing.Size(82, 62)
        Me.pctProcess3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pctProcess3.TabIndex = 142
        Me.pctProcess3.TabStop = False
        '
        'pctProcess4
        '
        Me.pctProcess4.Image = CType(resources.GetObject("pctProcess4.Image"), System.Drawing.Image)
        Me.pctProcess4.Location = New System.Drawing.Point(704, 287)
        Me.pctProcess4.Name = "pctProcess4"
        Me.pctProcess4.Size = New System.Drawing.Size(82, 62)
        Me.pctProcess4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pctProcess4.TabIndex = 142
        Me.pctProcess4.TabStop = False
        '
        'pctDNFWrong
        '
        Me.pctDNFWrong.Image = CType(resources.GetObject("pctDNFWrong.Image"), System.Drawing.Image)
        Me.pctDNFWrong.Location = New System.Drawing.Point(729, 299)
        Me.pctDNFWrong.Name = "pctDNFWrong"
        Me.pctDNFWrong.Size = New System.Drawing.Size(35, 36)
        Me.pctDNFWrong.TabIndex = 165
        Me.pctDNFWrong.TabStop = False
        '
        'cmdShowData
        '
        Me.cmdShowData.BackColor = System.Drawing.Color.Transparent
        Me.cmdShowData.BackgroundImage = CType(resources.GetObject("cmdShowData.BackgroundImage"), System.Drawing.Image)
        Me.cmdShowData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cmdShowData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShowData.FlatAppearance.BorderSize = 0
        Me.cmdShowData.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShowData.ForeColor = System.Drawing.Color.Black
        Me.cmdShowData.Location = New System.Drawing.Point(507, 442)
        Me.cmdShowData.Name = "cmdShowData"
        Me.cmdShowData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShowData.Size = New System.Drawing.Size(89, 35)
        Me.cmdShowData.TabIndex = 159
        Me.cmdShowData.Text = "Show Data"
        Me.cmdShowData.UseVisualStyleBackColor = False
        Me.cmdShowData.Visible = False
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.Transparent
        Me.btnNext.BackgroundImage = CType(resources.GetObject("btnNext.BackgroundImage"), System.Drawing.Image)
        Me.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnNext.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.ForeColor = System.Drawing.Color.Black
        Me.btnNext.Location = New System.Drawing.Point(603, 443)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnNext.Size = New System.Drawing.Size(87, 35)
        Me.btnNext.TabIndex = 158
        Me.btnNext.Text = "Next File.."
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Transparent
        Me.btnExit.BackgroundImage = CType(resources.GetObject("btnExit.BackgroundImage"), System.Drawing.Image)
        Me.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnExit.FlatAppearance.BorderSize = 0
        Me.btnExit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Black
        Me.btnExit.Location = New System.Drawing.Point(697, 442)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnExit.Size = New System.Drawing.Size(87, 35)
        Me.btnExit.TabIndex = 157
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'pctProcess1
        '
        Me.pctProcess1.Image = CType(resources.GetObject("pctProcess1.Image"), System.Drawing.Image)
        Me.pctProcess1.Location = New System.Drawing.Point(704, 43)
        Me.pctProcess1.Name = "pctProcess1"
        Me.pctProcess1.Size = New System.Drawing.Size(84, 62)
        Me.pctProcess1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pctProcess1.TabIndex = 142
        Me.pctProcess1.TabStop = False
        '
        'pctProcess2
        '
        Me.pctProcess2.Image = CType(resources.GetObject("pctProcess2.Image"), System.Drawing.Image)
        Me.pctProcess2.Location = New System.Drawing.Point(704, 125)
        Me.pctProcess2.Name = "pctProcess2"
        Me.pctProcess2.Size = New System.Drawing.Size(84, 62)
        Me.pctProcess2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pctProcess2.TabIndex = 142
        Me.pctProcess2.TabStop = False
        '
        'pctFileWrong
        '
        Me.pctFileWrong.Image = CType(resources.GetObject("pctFileWrong.Image"), System.Drawing.Image)
        Me.pctFileWrong.Location = New System.Drawing.Point(729, 58)
        Me.pctFileWrong.Name = "pctFileWrong"
        Me.pctFileWrong.Size = New System.Drawing.Size(35, 36)
        Me.pctFileWrong.TabIndex = 143
        Me.pctFileWrong.TabStop = False
        '
        'pctFileCorrect
        '
        Me.pctFileCorrect.Image = CType(resources.GetObject("pctFileCorrect.Image"), System.Drawing.Image)
        Me.pctFileCorrect.Location = New System.Drawing.Point(729, 56)
        Me.pctFileCorrect.Name = "pctFileCorrect"
        Me.pctFileCorrect.Size = New System.Drawing.Size(35, 36)
        Me.pctFileCorrect.TabIndex = 141
        Me.pctFileCorrect.TabStop = False
        '
        'pctChallanWrong
        '
        Me.pctChallanWrong.BackColor = System.Drawing.Color.White
        Me.pctChallanWrong.Image = CType(resources.GetObject("pctChallanWrong.Image"), System.Drawing.Image)
        Me.pctChallanWrong.Location = New System.Drawing.Point(729, 137)
        Me.pctChallanWrong.Name = "pctChallanWrong"
        Me.pctChallanWrong.Size = New System.Drawing.Size(35, 36)
        Me.pctChallanWrong.TabIndex = 143
        Me.pctChallanWrong.TabStop = False
        '
        'pctChallanCorrect
        '
        Me.pctChallanCorrect.Image = CType(resources.GetObject("pctChallanCorrect.Image"), System.Drawing.Image)
        Me.pctChallanCorrect.Location = New System.Drawing.Point(729, 137)
        Me.pctChallanCorrect.Name = "pctChallanCorrect"
        Me.pctChallanCorrect.Size = New System.Drawing.Size(35, 36)
        Me.pctChallanCorrect.TabIndex = 141
        Me.pctChallanCorrect.TabStop = False
        '
        'pctPANWrong
        '
        Me.pctPANWrong.Image = CType(resources.GetObject("pctPANWrong.Image"), System.Drawing.Image)
        Me.pctPANWrong.Location = New System.Drawing.Point(729, 219)
        Me.pctPANWrong.Name = "pctPANWrong"
        Me.pctPANWrong.Size = New System.Drawing.Size(35, 36)
        Me.pctPANWrong.TabIndex = 143
        Me.pctPANWrong.TabStop = False
        '
        'pctForeCastCorrect
        '
        Me.pctForeCastCorrect.Image = CType(resources.GetObject("pctForeCastCorrect.Image"), System.Drawing.Image)
        Me.pctForeCastCorrect.Location = New System.Drawing.Point(729, 299)
        Me.pctForeCastCorrect.Name = "pctForeCastCorrect"
        Me.pctForeCastCorrect.Size = New System.Drawing.Size(35, 36)
        Me.pctForeCastCorrect.TabIndex = 141
        Me.pctForeCastCorrect.TabStop = False
        '
        'pctPANCorrect
        '
        Me.pctPANCorrect.Image = CType(resources.GetObject("pctPANCorrect.Image"), System.Drawing.Image)
        Me.pctPANCorrect.Location = New System.Drawing.Point(729, 219)
        Me.pctPANCorrect.Name = "pctPANCorrect"
        Me.pctPANCorrect.Size = New System.Drawing.Size(35, 36)
        Me.pctPANCorrect.TabIndex = 141
        Me.pctPANCorrect.TabStop = False
        '
        'Userstatus1
        '
        Me.Userstatus1.BackgroundImage = CType(resources.GetObject("Userstatus1.BackgroundImage"), System.Drawing.Image)
        Me.Userstatus1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Userstatus1.Location = New System.Drawing.Point(-5, 443)
        Me.Userstatus1.Name = "Userstatus1"
        Me.Userstatus1.Size = New System.Drawing.Size(800, 98)
        Me.Userstatus1.TabIndex = 156
        '
        'frmProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(792, 561)
        Me.Controls.Add(Me.btnePayment)
        Me.Controls.Add(Me.PANValidateBrowser)
        Me.Controls.Add(Me.pbFNF)
        Me.Controls.Add(Me.lblLateFilingFee)
        Me.Controls.Add(Me.lblLateFilingFeeText)
        Me.Controls.Add(Me.lblIgnoredPANList)
        Me.Controls.Add(Me.lblFormNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblProbIntPay1)
        Me.Controls.Add(Me.lblProbshortDepAmt1)
        Me.Controls.Add(Me.lblProbshortDedAmt1)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.pctSummaryCorrect)
        Me.Controls.Add(Me.lblHead3)
        Me.Controls.Add(Me.lblHead2)
        Me.Controls.Add(Me.lblHead1)
        Me.Controls.Add(Me.pctProcess3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.pctProcess4)
        Me.Controls.Add(Me.pctDNFWrong)
        Me.Controls.Add(Me.lblOpenFolder)
        Me.Controls.Add(Me.lblOpenFile)
        Me.Controls.Add(Me.lblUnmatchchlStatus)
        Me.Controls.Add(Me.lblPanProcess)
        Me.Controls.Add(Me.lblPanProcess1)
        Me.Controls.Add(Me.lblSummary)
        Me.Controls.Add(Me.cmdShowData)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Userstatus1)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.lblForecasting)
        Me.Controls.Add(Me.lblPANValidations)
        Me.Controls.Add(Me.lblChallanMatching)
        Me.Controls.Add(Me.lblQ)
        Me.Controls.Add(Me.lblProbIntPay)
        Me.Controls.Add(Me.lblProbshortDepAmt)
        Me.Controls.Add(Me.lblProbshortDedAmt)
        Me.Controls.Add(Me.lblDNFStatus)
        Me.Controls.Add(Me.lblPanStatus)
        Me.Controls.Add(Me.lblchlStatus)
        Me.Controls.Add(Me.lblCsiFile)
        Me.Controls.Add(Me.lblDedName)
        Me.Controls.Add(Me.lblFy)
        Me.Controls.Add(Me.lblTan)
        Me.Controls.Add(Me.lblFileSelection)
        Me.Controls.Add(Me.pctProcess1)
        Me.Controls.Add(Me.pctProcess2)
        Me.Controls.Add(Me.pctFileWrong)
        Me.Controls.Add(Me.pctFileCorrect)
        Me.Controls.Add(Me.pctChallanWrong)
        Me.Controls.Add(Me.pctChallanCorrect)
        Me.Controls.Add(Me.pctPANWrong)
        Me.Controls.Add(Me.pctForeCastCorrect)
        Me.Controls.Add(Me.pctPANCorrect)
        Me.Controls.Add(Me.lblHead)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel9)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProcess"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "  Default Notice Forecaster"
        CType(Me.pbFNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctSummaryCorrect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctProcess3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctProcess4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctDNFWrong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctProcess1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctProcess2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctFileWrong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctFileCorrect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctChallanWrong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctChallanCorrect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctPANWrong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctForeCastCorrect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctPANCorrect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblFileSelection As System.Windows.Forms.Label
    Friend WithEvents lblChallanMatching As System.Windows.Forms.Label
    Friend WithEvents lblPANValidations As System.Windows.Forms.Label
    Friend WithEvents lblForecasting As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents pctFileCorrect As System.Windows.Forms.PictureBox
    Friend WithEvents pctProcess1 As System.Windows.Forms.PictureBox
    Friend WithEvents pctFileWrong As System.Windows.Forms.PictureBox
    Friend WithEvents lblTan As System.Windows.Forms.Label
    Friend WithEvents lblFy As System.Windows.Forms.Label
    Friend WithEvents lblQ As System.Windows.Forms.Label
    Friend WithEvents lblDedName As System.Windows.Forms.Label
    Friend WithEvents lblCsiFile As System.Windows.Forms.Label
    Friend WithEvents lblchlStatus As System.Windows.Forms.Label
    Friend WithEvents lblPanStatus As System.Windows.Forms.Label
    Friend WithEvents lblDNFStatus As System.Windows.Forms.Label
    Friend WithEvents lblProbshortDedAmt As System.Windows.Forms.Label
    Friend WithEvents lblProbshortDepAmt As System.Windows.Forms.Label
    Friend WithEvents lblProbIntPay As System.Windows.Forms.Label
    Friend WithEvents pctProcess2 As System.Windows.Forms.PictureBox
    Friend WithEvents pctProcess3 As System.Windows.Forms.PictureBox
    Friend WithEvents pctChallanCorrect As System.Windows.Forms.PictureBox
    Friend WithEvents pctPANCorrect As System.Windows.Forms.PictureBox
    Friend WithEvents pctChallanWrong As System.Windows.Forms.PictureBox
    Friend WithEvents pctPANWrong As System.Windows.Forms.PictureBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents pbFNF As System.Windows.Forms.PictureBox
    Friend WithEvents Userstatus1 As eTdsDNF2223.Userstatus
    Public WithEvents btnNext As System.Windows.Forms.Button
    Public WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents pctProcess4 As System.Windows.Forms.PictureBox
    Friend WithEvents pctForeCastCorrect As System.Windows.Forms.PictureBox
    Public WithEvents cmdShowData As System.Windows.Forms.Button
    Friend WithEvents lblSummary As System.Windows.Forms.Label
    Friend WithEvents lblPanProcess1 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblPanProcess As System.Windows.Forms.Label
    Friend WithEvents lblUnmatchchlStatus As System.Windows.Forms.LinkLabel
    Friend WithEvents lblOpenFile As System.Windows.Forms.LinkLabel
    Friend WithEvents lblOpenFolder As System.Windows.Forms.LinkLabel
    Friend WithEvents pctDNFWrong As System.Windows.Forms.PictureBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents lblHead As System.Windows.Forms.Label
    Friend WithEvents lblHead3 As System.Windows.Forms.Label
    Friend WithEvents lblHead2 As System.Windows.Forms.Label
    Friend WithEvents lblHead1 As System.Windows.Forms.Label
    Friend WithEvents pctSummaryCorrect As System.Windows.Forms.PictureBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents lblProbshortDedAmt1 As System.Windows.Forms.Label
    Friend WithEvents lblProbshortDepAmt1 As System.Windows.Forms.Label
    Friend WithEvents lblProbIntPay1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFormNo As System.Windows.Forms.Label
    Friend WithEvents lblIgnoredPANList As System.Windows.Forms.LinkLabel
    Friend WithEvents lblLateFilingFee As System.Windows.Forms.Label
    Friend WithEvents lblLateFilingFeeText As System.Windows.Forms.Label
    Friend WithEvents PANValidateBrowser As System.Windows.Forms.WebBrowser
    Public WithEvents btnePayment As System.Windows.Forms.Button
End Class
