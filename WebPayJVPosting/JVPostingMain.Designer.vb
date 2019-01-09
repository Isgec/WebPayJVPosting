<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JVPostingMain
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
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtJVFile = New System.Windows.Forms.TextBox()
    Me.cmdBrowseJVTextFile = New System.Windows.Forms.Button()
    Me.ofd1 = New System.Windows.Forms.OpenFileDialog()
    Me.cmdConvertPSV = New System.Windows.Forms.Button()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.pBar = New System.Windows.Forms.ProgressBar()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.pLbl = New System.Windows.Forms.Label()
    Me.chkSplit = New System.Windows.Forms.CheckBox()
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(13, 13)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(109, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "WebPay JV Text File:"
    '
    'txtJVFile
    '
    Me.txtJVFile.Location = New System.Drawing.Point(128, 6)
    Me.txtJVFile.Name = "txtJVFile"
    Me.txtJVFile.Size = New System.Drawing.Size(356, 20)
    Me.txtJVFile.TabIndex = 1
    '
    'cmdBrowseJVTextFile
    '
    Me.cmdBrowseJVTextFile.Location = New System.Drawing.Point(501, 6)
    Me.cmdBrowseJVTextFile.Name = "cmdBrowseJVTextFile"
    Me.cmdBrowseJVTextFile.Size = New System.Drawing.Size(32, 19)
    Me.cmdBrowseJVTextFile.TabIndex = 2
    Me.cmdBrowseJVTextFile.Text = ". . ."
    Me.cmdBrowseJVTextFile.UseVisualStyleBackColor = True
    '
    'ofd1
    '
    Me.ofd1.DefaultExt = "txt"
    Me.ofd1.FileName = "OpenFileDialog1"
    Me.ofd1.Title = "Select WebPay JV Text File"
    '
    'cmdConvertPSV
    '
    Me.cmdConvertPSV.Location = New System.Drawing.Point(194, 32)
    Me.cmdConvertPSV.Name = "cmdConvertPSV"
    Me.cmdConvertPSV.Size = New System.Drawing.Size(128, 23)
    Me.cmdConvertPSV.TabIndex = 3
    Me.cmdConvertPSV.Text = "Convert to PSV"
    Me.cmdConvertPSV.UseVisualStyleBackColor = True
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(334, 32)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(128, 23)
    Me.Button1.TabIndex = 4
    Me.Button1.Text = "Convert to XML"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'pBar
    '
    Me.pBar.Location = New System.Drawing.Point(128, 62)
    Me.pBar.Name = "pBar"
    Me.pBar.Size = New System.Drawing.Size(356, 23)
    Me.pBar.Step = 1
    Me.pBar.TabIndex = 5
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(10, 67)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(85, 13)
    Me.Label2.TabIndex = 6
    Me.Label2.Text = "XML Conversion"
    '
    'pLbl
    '
    Me.pLbl.AutoSize = True
    Me.pLbl.Location = New System.Drawing.Point(498, 67)
    Me.pLbl.Name = "pLbl"
    Me.pLbl.Size = New System.Drawing.Size(13, 13)
    Me.pLbl.TabIndex = 7
    Me.pLbl.Text = "0"
    '
    'chkSplit
    '
    Me.chkSplit.AutoSize = True
    Me.chkSplit.Checked = True
    Me.chkSplit.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkSplit.Location = New System.Drawing.Point(68, 36)
    Me.chkSplit.Name = "chkSplit"
    Me.chkSplit.RightToLeft = System.Windows.Forms.RightToLeft.Yes
    Me.chkSplit.Size = New System.Drawing.Size(75, 17)
    Me.chkSplit.TabIndex = 8
    Me.chkSplit.Text = "Split in IJT"
    Me.chkSplit.UseVisualStyleBackColor = True
    '
    'JVPostingMain
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(562, 326)
    Me.Controls.Add(Me.chkSplit)
    Me.Controls.Add(Me.pLbl)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.pBar)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.cmdConvertPSV)
    Me.Controls.Add(Me.cmdBrowseJVTextFile)
    Me.Controls.Add(Me.txtJVFile)
    Me.Controls.Add(Me.Label1)
    Me.Name = "JVPostingMain"
    Me.Text = "JVPostingMain"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtJVFile As System.Windows.Forms.TextBox
  Friend WithEvents cmdBrowseJVTextFile As System.Windows.Forms.Button
  Friend WithEvents ofd1 As System.Windows.Forms.OpenFileDialog
  Friend WithEvents cmdConvertPSV As System.Windows.Forms.Button
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents pBar As System.Windows.Forms.ProgressBar
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents pLbl As System.Windows.Forms.Label
  Friend WithEvents chkSplit As System.Windows.Forms.CheckBox
End Class
