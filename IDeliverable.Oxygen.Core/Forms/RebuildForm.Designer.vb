<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RebuildForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.PictureBox1 = New System.Windows.Forms.PictureBox
		Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
		Me.TheCancelButton = New System.Windows.Forms.Button
		Me.ProgressLabel = New System.Windows.Forms.Label
		CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'PictureBox1
		'
		Me.PictureBox1.Image = My.Resources.Oxygen_Entity_48x48
		Me.PictureBox1.Location = New System.Drawing.Point(334, 12)
		Me.PictureBox1.Name = "PictureBox1"
		Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
		Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
		Me.PictureBox1.TabIndex = 0
		Me.PictureBox1.TabStop = False
		'
		'ProgressBar1
		'
		Me.ProgressBar1.Location = New System.Drawing.Point(12, 106)
		Me.ProgressBar1.Name = "ProgressBar1"
		Me.ProgressBar1.Size = New System.Drawing.Size(370, 23)
		Me.ProgressBar1.TabIndex = 1
		'
		'TheCancelButton
		'
		Me.TheCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.TheCancelButton.Location = New System.Drawing.Point(161, 153)
		Me.TheCancelButton.Name = "TheCancelButton"
		Me.TheCancelButton.Size = New System.Drawing.Size(75, 23)
		Me.TheCancelButton.TabIndex = 2
		Me.TheCancelButton.Text = "Cancel"
		Me.TheCancelButton.UseVisualStyleBackColor = True
		'
		'ProgressLabel
		'
		Me.ProgressLabel.AutoSize = True
		Me.ProgressLabel.Location = New System.Drawing.Point(9, 90)
		Me.ProgressLabel.Name = "ProgressLabel"
		Me.ProgressLabel.Size = New System.Drawing.Size(102, 13)
		Me.ProgressLabel.TabIndex = 0
		Me.ProgressLabel.Text = "Rebuilding entities..."
		'
		'RebuildForm
		'
		Me.AcceptButton = Me.TheCancelButton
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.TheCancelButton
		Me.ClientSize = New System.Drawing.Size(394, 199)
		Me.Controls.Add(Me.ProgressLabel)
		Me.Controls.Add(Me.TheCancelButton)
		Me.Controls.Add(Me.ProgressBar1)
		Me.Controls.Add(Me.PictureBox1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "RebuildForm"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Rebuild All Entities"
		CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
	Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
	Friend WithEvents TheCancelButton As System.Windows.Forms.Button
    Friend WithEvents ProgressLabel As System.Windows.Forms.Label
End Class
