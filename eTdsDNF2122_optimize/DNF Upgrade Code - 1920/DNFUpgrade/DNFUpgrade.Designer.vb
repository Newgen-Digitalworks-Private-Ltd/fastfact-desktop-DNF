<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DNFUpgrade
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DNFUpgrade))
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lstFileNames = New System.Windows.Forms.ListBox()
        Me.Pbr = New System.Windows.Forms.ProgressBar()
        Me.lblUpdate = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(493, 322)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(78, 24)
        Me.btnExit.TabIndex = 32
        Me.btnExit.Text = "&Exit"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lstFileNames)
        Me.Panel1.Controls.Add(Me.Pbr)
        Me.Panel1.Location = New System.Drawing.Point(7, 49)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(564, 263)
        Me.Panel1.TabIndex = 31
        '
        'lstFileNames
        '
        Me.lstFileNames.Location = New System.Drawing.Point(24, 16)
        Me.lstFileNames.Name = "lstFileNames"
        Me.lstFileNames.Size = New System.Drawing.Size(488, 199)
        Me.lstFileNames.TabIndex = 39
        '
        'Pbr
        '
        Me.Pbr.Location = New System.Drawing.Point(24, 224)
        Me.Pbr.Name = "Pbr"
        Me.Pbr.Size = New System.Drawing.Size(486, 24)
        Me.Pbr.TabIndex = 38
        Me.Pbr.Visible = False
        '
        'lblUpdate
        '
        Me.lblUpdate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUpdate.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblUpdate.Location = New System.Drawing.Point(15, 21)
        Me.lblUpdate.Name = "lblUpdate"
        Me.lblUpdate.Size = New System.Drawing.Size(446, 20)
        Me.lblUpdate.TabIndex = 30
        Me.lblUpdate.Text = "Starting update process...."
        '
        'DNFUpgrade
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 356)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblUpdate)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DNFUpgrade"
        Me.Text = " DNFUpgrade"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lstFileNames As System.Windows.Forms.ListBox
    Friend WithEvents Pbr As System.Windows.Forms.ProgressBar
    Friend WithEvents lblUpdate As System.Windows.Forms.Label

End Class
