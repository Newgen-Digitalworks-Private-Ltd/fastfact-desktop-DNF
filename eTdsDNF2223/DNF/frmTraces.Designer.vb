<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraces
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblPanProcess = New System.Windows.Forms.Label()
        Me.lblPanProcess1 = New System.Windows.Forms.LinkLabel()
        Me.btnvalidpan = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.lblpath = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtuserid = New System.Windows.Forms.TextBox()
        Me.lbltan = New System.Windows.Forms.Label()
        Me.txtpwd = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnpanexport = New System.Windows.Forms.Button()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.Panel1.BackgroundImage = Global.eTdsDNF2223.My.Resources.Resources.Traces_image1
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(613, 102)
        Me.Panel1.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.Menu
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.btnpanexport)
        Me.Panel4.Controls.Add(Me.lblPanProcess)
        Me.Panel4.Controls.Add(Me.lblPanProcess1)
        Me.Panel4.Controls.Add(Me.btnvalidpan)
        Me.Panel4.Controls.Add(Me.CheckBox1)
        Me.Panel4.Controls.Add(Me.lblpath)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.txtuserid)
        Me.Panel4.Controls.Add(Me.lbltan)
        Me.Panel4.Controls.Add(Me.txtpwd)
        Me.Panel4.Controls.Add(Me.Button2)
        Me.Panel4.Controls.Add(Me.Button1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 102)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(613, 215)
        Me.Panel4.TabIndex = 99
        '
        'lblPanProcess
        '
        Me.lblPanProcess.AutoSize = True
        Me.lblPanProcess.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPanProcess.Location = New System.Drawing.Point(156, 154)
        Me.lblPanProcess.Name = "lblPanProcess"
        Me.lblPanProcess.Size = New System.Drawing.Size(53, 15)
        Me.lblPanProcess.TabIndex = 164
        Me.lblPanProcess.Text = "Status  :"
        '
        'lblPanProcess1
        '
        Me.lblPanProcess1.AutoSize = True
        Me.lblPanProcess1.Location = New System.Drawing.Point(156, 157)
        Me.lblPanProcess1.Name = "lblPanProcess1"
        Me.lblPanProcess1.Size = New System.Drawing.Size(46, 13)
        Me.lblPanProcess1.TabIndex = 163
        Me.lblPanProcess1.TabStop = True
        Me.lblPanProcess1.Text = "Status  :"
        Me.lblPanProcess1.Visible = False
        '
        'btnvalidpan
        '
        Me.btnvalidpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnvalidpan.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnvalidpan.Location = New System.Drawing.Point(270, 126)
        Me.btnvalidpan.Name = "btnvalidpan"
        Me.btnvalidpan.Size = New System.Drawing.Size(100, 23)
        Me.btnvalidpan.TabIndex = 94
        Me.btnvalidpan.Text = "Validate PAN"
        Me.btnvalidpan.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(447, 97)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 93
        Me.CheckBox1.UseVisualStyleBackColor = True
        Me.CheckBox1.Visible = False
        '
        'lblpath
        '
        Me.lblpath.AutoSize = True
        Me.lblpath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpath.Location = New System.Drawing.Point(117, 313)
        Me.lblpath.Name = "lblpath"
        Me.lblpath.Size = New System.Drawing.Size(0, 13)
        Me.lblpath.TabIndex = 92
        Me.lblpath.Visible = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(138, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 18)
        Me.Label1.TabIndex = 80
        Me.Label1.Text = "TAN No :"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(137, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 18)
        Me.Label2.TabIndex = 81
        Me.Label2.Text = "Uesr ID :"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Location = New System.Drawing.Point(138, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 18)
        Me.Label3.TabIndex = 82
        Me.Label3.Text = "Password :"
        '
        'txtuserid
        '
        Me.txtuserid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtuserid.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txtuserid.Location = New System.Drawing.Point(244, 56)
        Me.txtuserid.Name = "txtuserid"
        Me.txtuserid.Size = New System.Drawing.Size(160, 22)
        Me.txtuserid.TabIndex = 89
        '
        'lbltan
        '
        Me.lbltan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbltan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltan.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lbltan.Location = New System.Drawing.Point(244, 22)
        Me.lbltan.Name = "lbltan"
        Me.lbltan.Size = New System.Drawing.Size(160, 20)
        Me.lbltan.TabIndex = 83
        '
        'txtpwd
        '
        Me.txtpwd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpwd.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txtpwd.Location = New System.Drawing.Point(244, 89)
        Me.txtpwd.Name = "txtpwd"
        Me.txtpwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpwd.Size = New System.Drawing.Size(160, 22)
        Me.txtpwd.TabIndex = 85
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Button2.Location = New System.Drawing.Point(356, 238)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 23)
        Me.Button2.TabIndex = 87
        Me.Button2.Text = "Remember me "
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.BackgroundImage = Global.eTdsDNF2223.My.Resources.Resources.pwd
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Location = New System.Drawing.Point(408, 91)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(20, 20)
        Me.Button1.TabIndex = 86
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnpanexport
        '
        Me.btnpanexport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpanexport.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnpanexport.Location = New System.Drawing.Point(231, 185)
        Me.btnpanexport.Name = "btnpanexport"
        Me.btnpanexport.Size = New System.Drawing.Size(173, 23)
        Me.btnpanexport.TabIndex = 165
        Me.btnpanexport.Text = "Export to Excel"
        Me.btnpanexport.UseVisualStyleBackColor = True
        Me.btnpanexport.Visible = False
        '
        'frmTraces
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(613, 317)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTraces"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmTraces"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel4 As Panel
    Public WithEvents lblpath As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtuserid As TextBox
    Friend WithEvents lbltan As Label
    Friend WithEvents txtpwd As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents btnvalidpan As Button
    Friend WithEvents lblPanProcess1 As LinkLabel
    Friend WithEvents lblPanProcess As Label
    Friend WithEvents btnpanexport As Button
End Class
