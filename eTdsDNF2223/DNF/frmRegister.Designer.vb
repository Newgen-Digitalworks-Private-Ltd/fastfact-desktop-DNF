<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegister
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRegister))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtlink = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFileSelection = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblRegkey = New System.Windows.Forms.Label()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.btnProceed = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.txtRegistrationKey = New System.Windows.Forms.TextBox()
        Me.lblLicence = New System.Windows.Forms.LinkLabel()
        Me.lblPrivacy = New System.Windows.Forms.LinkLabel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.txtDongleSrNo = New System.Windows.Forms.TextBox()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.lblDongleSrNo = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me.lblContactPer = New System.Windows.Forms.Label()
        Me.lblPhoneNo = New System.Windows.Forms.Label()
        Me.lblMobileNo = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtContPer = New System.Windows.Forms.TextBox()
        Me.TxtPhoneNo = New System.Windows.Forms.TextBox()
        Me.txtMobileNo = New System.Windows.Forms.TextBox()
        Me.txtCustomerType = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtlink)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblFileSelection)
        Me.Panel1.Location = New System.Drawing.Point(91, 88)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(601, 220)
        Me.Panel1.TabIndex = 0
        '
        'txtlink
        '
        Me.txtlink.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtlink.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlink.Location = New System.Drawing.Point(191, 172)
        Me.txtlink.Name = "txtlink"
        Me.txtlink.Size = New System.Drawing.Size(347, 14)
        Me.txtlink.TabIndex = 137
        Me.txtlink.Text = "www.ffcs.in/cusregtion/customerregistration.aspx"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 172)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(175, 15)
        Me.Label4.TabIndex = 136
        Me.Label4.Text = ">>  Customer registration link"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(591, 35)
        Me.Label2.TabIndex = 135
        Me.Label2.Text = ">>  On click submit if customer registration page is not launched in your browser" &
    " copy below link and            paste in your browser"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(586, 32)
        Me.Label1.TabIndex = 135
        Me.Label1.Text = ">>  On successful Registration, Registration key will be generated, copy and past" &
    "e in 'Registration Key'        and click 'Register' to continue"
        '
        'lblFileSelection
        '
        Me.lblFileSelection.AutoSize = True
        Me.lblFileSelection.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileSelection.Location = New System.Drawing.Point(14, 27)
        Me.lblFileSelection.Name = "lblFileSelection"
        Me.lblFileSelection.Size = New System.Drawing.Size(497, 15)
        Me.lblFileSelection.TabIndex = 135
        Me.lblFileSelection.Text = ">>  For timely Upgrades / Notification /  Support, it is mandatory to register yo" &
    "ur product"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(318, 323)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(270, 15)
        Me.Label3.TabIndex = 136
        Me.Label3.Text = "Please Click on Submit to proceed registration"
        '
        'lblRegkey
        '
        Me.lblRegkey.AutoSize = True
        Me.lblRegkey.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegkey.Location = New System.Drawing.Point(104, 377)
        Me.lblRegkey.Name = "lblRegkey"
        Me.lblRegkey.Size = New System.Drawing.Size(100, 15)
        Me.lblRegkey.TabIndex = 136
        Me.lblRegkey.Text = "Registration Key"
        '
        'btnRegister
        '
        Me.btnRegister.BackColor = System.Drawing.Color.Transparent
        Me.btnRegister.BackgroundImage = CType(resources.GetObject("btnRegister.BackgroundImage"), System.Drawing.Image)
        Me.btnRegister.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnRegister.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnRegister.FlatAppearance.BorderSize = 0
        Me.btnRegister.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRegister.ForeColor = System.Drawing.Color.White
        Me.btnRegister.Location = New System.Drawing.Point(603, 313)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnRegister.Size = New System.Drawing.Size(89, 35)
        Me.btnRegister.TabIndex = 160
        Me.btnRegister.Text = "&Submit"
        Me.btnRegister.UseVisualStyleBackColor = False
        '
        'btnProceed
        '
        Me.btnProceed.BackColor = System.Drawing.Color.Transparent
        Me.btnProceed.BackgroundImage = CType(resources.GetObject("btnProceed.BackgroundImage"), System.Drawing.Image)
        Me.btnProceed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnProceed.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnProceed.FlatAppearance.BorderSize = 0
        Me.btnProceed.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProceed.ForeColor = System.Drawing.Color.White
        Me.btnProceed.Location = New System.Drawing.Point(508, 401)
        Me.btnProceed.Name = "btnProceed"
        Me.btnProceed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnProceed.Size = New System.Drawing.Size(89, 35)
        Me.btnProceed.TabIndex = 161
        Me.btnProceed.Text = "&Register"
        Me.btnProceed.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.Transparent
        Me.cmdClose.BackgroundImage = CType(resources.GetObject("cmdClose.BackgroundImage"), System.Drawing.Image)
        Me.cmdClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.FlatAppearance.BorderSize = 0
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.Color.White
        Me.cmdClose.Location = New System.Drawing.Point(603, 401)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(88, 35)
        Me.cmdClose.TabIndex = 161
        Me.cmdClose.Text = "&Close "
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'txtRegistrationKey
        '
        Me.txtRegistrationKey.Location = New System.Drawing.Point(208, 375)
        Me.txtRegistrationKey.Name = "txtRegistrationKey"
        Me.txtRegistrationKey.Size = New System.Drawing.Size(484, 20)
        Me.txtRegistrationKey.TabIndex = 162
        '
        'lblLicence
        '
        Me.lblLicence.AutoSize = True
        Me.lblLicence.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicence.Location = New System.Drawing.Point(12, 438)
        Me.lblLicence.Name = "lblLicence"
        Me.lblLicence.Size = New System.Drawing.Size(199, 15)
        Me.lblLicence.TabIndex = 163
        Me.lblLicence.TabStop = True
        Me.lblLicence.Text = "Thomson Reuters Licensing Policy"
        '
        'lblPrivacy
        '
        Me.lblPrivacy.AutoSize = True
        Me.lblPrivacy.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrivacy.Location = New System.Drawing.Point(226, 438)
        Me.lblPrivacy.Name = "lblPrivacy"
        Me.lblPrivacy.Size = New System.Drawing.Size(80, 15)
        Me.lblPrivacy.TabIndex = 163
        Me.lblPrivacy.TabStop = True
        Me.lblPrivacy.Text = "Privacy Policy"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(253, 17)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(234, 51)
        Me.PictureBox1.TabIndex = 164
        Me.PictureBox1.TabStop = False
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.txtDongleSrNo)
        Me.pnlMain.Controls.Add(Me.txtYear)
        Me.pnlMain.Controls.Add(Me.txtProduct)
        Me.pnlMain.Controls.Add(Me.lblDongleSrNo)
        Me.pnlMain.Controls.Add(Me.lblYear)
        Me.pnlMain.Controls.Add(Me.lblProduct)
        Me.pnlMain.Location = New System.Drawing.Point(111, 100)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(576, 43)
        Me.pnlMain.TabIndex = 165
        '
        'txtDongleSrNo
        '
        Me.txtDongleSrNo.Location = New System.Drawing.Point(447, 10)
        Me.txtDongleSrNo.Name = "txtDongleSrNo"
        Me.txtDongleSrNo.Size = New System.Drawing.Size(113, 20)
        Me.txtDongleSrNo.TabIndex = 165
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(240, 10)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(113, 20)
        Me.txtYear.TabIndex = 164
        '
        'txtProduct
        '
        Me.txtProduct.Location = New System.Drawing.Point(75, 10)
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.Size = New System.Drawing.Size(113, 20)
        Me.txtProduct.TabIndex = 163
        '
        'lblDongleSrNo
        '
        Me.lblDongleSrNo.AutoSize = True
        Me.lblDongleSrNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDongleSrNo.Location = New System.Drawing.Point(363, 10)
        Me.lblDongleSrNo.Name = "lblDongleSrNo"
        Me.lblDongleSrNo.Size = New System.Drawing.Size(74, 15)
        Me.lblDongleSrNo.TabIndex = 137
        Me.lblDongleSrNo.Text = "DongleSrNo"
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYear.Location = New System.Drawing.Point(198, 10)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(32, 15)
        Me.lblYear.TabIndex = 137
        Me.lblYear.Text = "Year"
        '
        'lblProduct
        '
        Me.lblProduct.AutoSize = True
        Me.lblProduct.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProduct.Location = New System.Drawing.Point(13, 10)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(52, 15)
        Me.lblProduct.TabIndex = 137
        Me.lblProduct.Text = "Product"
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = True
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(184, 156)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(99, 15)
        Me.lblCustomerName.TabIndex = 166
        Me.lblCustomerName.Text = "Customer Name"
        '
        'lblContactPer
        '
        Me.lblContactPer.AutoSize = True
        Me.lblContactPer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactPer.Location = New System.Drawing.Point(184, 181)
        Me.lblContactPer.Name = "lblContactPer"
        Me.lblContactPer.Size = New System.Drawing.Size(95, 15)
        Me.lblContactPer.TabIndex = 166
        Me.lblContactPer.Text = "Contact Person"
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.AutoSize = True
        Me.lblPhoneNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhoneNo.Location = New System.Drawing.Point(184, 206)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(61, 15)
        Me.lblPhoneNo.TabIndex = 166
        Me.lblPhoneNo.Text = "Phone No"
        '
        'lblMobileNo
        '
        Me.lblMobileNo.AutoSize = True
        Me.lblMobileNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobileNo.Location = New System.Drawing.Point(184, 231)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(62, 15)
        Me.lblMobileNo.TabIndex = 166
        Me.lblMobileNo.Text = "Mobile No"
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.Location = New System.Drawing.Point(184, 256)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(38, 15)
        Me.lblEmail.TabIndex = 166
        Me.lblEmail.Text = "Email"
        '
        'lblCustomerType
        '
        Me.lblCustomerType.AutoSize = True
        Me.lblCustomerType.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerType.Location = New System.Drawing.Point(184, 281)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(92, 15)
        Me.lblCustomerType.TabIndex = 166
        Me.lblCustomerType.Text = "Customer Type"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.Location = New System.Drawing.Point(306, 154)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(361, 20)
        Me.txtCustomerName.TabIndex = 167
        '
        'txtContPer
        '
        Me.txtContPer.Location = New System.Drawing.Point(306, 180)
        Me.txtContPer.Name = "txtContPer"
        Me.txtContPer.Size = New System.Drawing.Size(361, 20)
        Me.txtContPer.TabIndex = 167
        '
        'TxtPhoneNo
        '
        Me.TxtPhoneNo.Location = New System.Drawing.Point(306, 206)
        Me.TxtPhoneNo.Name = "TxtPhoneNo"
        Me.TxtPhoneNo.Size = New System.Drawing.Size(361, 20)
        Me.TxtPhoneNo.TabIndex = 167
        '
        'txtMobileNo
        '
        Me.txtMobileNo.Location = New System.Drawing.Point(306, 232)
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.Size = New System.Drawing.Size(361, 20)
        Me.txtMobileNo.TabIndex = 167
        '
        'txtCustomerType
        '
        Me.txtCustomerType.Location = New System.Drawing.Point(306, 284)
        Me.txtCustomerType.Name = "txtCustomerType"
        Me.txtCustomerType.Size = New System.Drawing.Size(361, 20)
        Me.txtCustomerType.TabIndex = 167
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(306, 258)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(361, 20)
        Me.txtEmail.TabIndex = 167
        '
        'frmRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(774, 462)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtCustomerType)
        Me.Controls.Add(Me.txtMobileNo)
        Me.Controls.Add(Me.TxtPhoneNo)
        Me.Controls.Add(Me.txtContPer)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.lblCustomerType)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.lblMobileNo)
        Me.Controls.Add(Me.lblPhoneNo)
        Me.Controls.Add(Me.lblContactPer)
        Me.Controls.Add(Me.lblCustomerName)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblPrivacy)
        Me.Controls.Add(Me.lblLicence)
        Me.Controls.Add(Me.txtRegistrationKey)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.btnProceed)
        Me.Controls.Add(Me.btnRegister)
        Me.Controls.Add(Me.lblRegkey)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRegister"
        Me.Text = "Registration"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFileSelection As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblRegkey As System.Windows.Forms.Label
    Public WithEvents btnRegister As System.Windows.Forms.Button
    Public WithEvents btnProceed As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents txtRegistrationKey As System.Windows.Forms.TextBox
    Friend WithEvents lblLicence As System.Windows.Forms.LinkLabel
    Friend WithEvents lblPrivacy As System.Windows.Forms.LinkLabel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtDongleSrNo As System.Windows.Forms.TextBox
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents txtProduct As System.Windows.Forms.TextBox
    Friend WithEvents lblDongleSrNo As System.Windows.Forms.Label
    Friend WithEvents lblYear As System.Windows.Forms.Label
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label
    Friend WithEvents lblContactPer As System.Windows.Forms.Label
    Friend WithEvents lblPhoneNo As System.Windows.Forms.Label
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents txtContPer As System.Windows.Forms.TextBox
    Friend WithEvents TxtPhoneNo As System.Windows.Forms.TextBox
    Friend WithEvents txtMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerType As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtlink As System.Windows.Forms.TextBox
End Class
