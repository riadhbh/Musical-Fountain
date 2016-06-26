<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Sleep_Mode
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
        Me.NrlM = New System.Windows.Forms.Button()
        Me.QApps = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'NrlM
        '
        Me.NrlM.Location = New System.Drawing.Point(26, 161)
        Me.NrlM.Name = "NrlM"
        Me.NrlM.Size = New System.Drawing.Size(97, 23)
        Me.NrlM.TabIndex = 0
        Me.NrlM.Text = "Normal Mode"
        Me.NrlM.UseVisualStyleBackColor = True
        '
        'QApps
        '
        Me.QApps.Location = New System.Drawing.Point(204, 161)
        Me.QApps.Name = "QApps"
        Me.QApps.Size = New System.Drawing.Size(97, 23)
        Me.QApps.TabIndex = 1
        Me.QApps.Text = "Exit"
        Me.QApps.UseVisualStyleBackColor = True
        '
        'Sleep_Mode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Musical_Fountain.My.Resources.Resources.fountain
        Me.ClientSize = New System.Drawing.Size(325, 217)
        Me.Controls.Add(Me.QApps)
        Me.Controls.Add(Me.NrlM)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Sleep_Mode"
        Me.Text = "Sleep_Mode"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NrlM As System.Windows.Forms.Button
    Friend WithEvents QApps As System.Windows.Forms.Button
End Class
