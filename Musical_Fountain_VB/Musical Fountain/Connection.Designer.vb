<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Connection
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Connection))
        Me.connect = New System.Windows.Forms.Button()
        Me.quit = New System.Windows.Forms.Button()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.ComCOM = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Refre = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'connect
        '
        Me.connect.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.connect.FlatAppearance.BorderSize = 0
        Me.connect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.connect.Image = Global.Musical_Fountain.My.Resources.Resources.connect_zoom
        Me.connect.Location = New System.Drawing.Point(12, 11)
        Me.connect.Name = "connect"
        Me.connect.Size = New System.Drawing.Size(92, 48)
        Me.connect.TabIndex = 0
        Me.connect.Text = "Connect "
        Me.connect.UseVisualStyleBackColor = True
        Me.connect.UseWaitCursor = True
        '
        'quit
        '
        Me.quit.Location = New System.Drawing.Point(12, 96)
        Me.quit.Name = "quit"
        Me.quit.Size = New System.Drawing.Size(92, 37)
        Me.quit.TabIndex = 1
        Me.quit.Text = "Exit"
        Me.quit.UseVisualStyleBackColor = True
        '
        'ComCOM
        '
        Me.ComCOM.FormattingEnabled = True
        Me.ComCOM.Location = New System.Drawing.Point(135, 37)
        Me.ComCOM.Name = "ComCOM"
        Me.ComCOM.Size = New System.Drawing.Size(93, 21)
        Me.ComCOM.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(132, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Avaible COM Ports"
        '
        'Refre
        '
        Me.Refre.Location = New System.Drawing.Point(135, 96)
        Me.Refre.Name = "Refre"
        Me.Refre.Size = New System.Drawing.Size(93, 37)
        Me.Refre.TabIndex = 4
        Me.Refre.Text = "Refresh"
        Me.Refre.UseVisualStyleBackColor = True
        '
        'Connection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(239, 150)
        Me.Controls.Add(Me.Refre)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComCOM)
        Me.Controls.Add(Me.quit)
        Me.Controls.Add(Me.connect)
        Me.Cursor = System.Windows.Forms.Cursors.AppStarting
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Connection"
        Me.Text = "Connection"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents connect As System.Windows.Forms.Button
    Friend WithEvents quit As System.Windows.Forms.Button
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents ComCOM As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Refre As System.Windows.Forms.Button

End Class
