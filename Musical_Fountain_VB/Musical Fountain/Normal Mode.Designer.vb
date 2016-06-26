<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Normal_Mode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Normal_Mode))
        Me.add = New System.Windows.Forms.Button()
        Me.del = New System.Windows.Forms.Button()
        Me.addfile = New System.Windows.Forms.OpenFileDialog()
        Me.VolumeScroll = New System.Windows.Forms.TrackBar()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.l3l7 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l3l8 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l3l4 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l3l5 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l3l6 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l3l3 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l3l2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l3l1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l2l7 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l2l8 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l2l4 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l2l5 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l2l6 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l2l3 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l2l2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l2l1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l1l1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l1l2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l1l3 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l1l6 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l1l5 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l1l4 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l1l8 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.l1l7 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.fileslist = New System.Windows.Forms.ListBox()
        Me.rightch = New System.Windows.Forms.RadioButton()
        Me.leftch = New System.Windows.Forms.RadioButton()
        Me.ExitApps = New System.Windows.Forms.Button()
        Me.Min = New System.Windows.Forms.Button()
        Me.SlpM = New System.Windows.Forms.Button()
        Me.FuncgenPb = New System.Windows.Forms.PictureBox()
        Me.IpPlotPb = New System.Windows.Forms.PictureBox()
        Me.Stopbtn = New System.Windows.Forms.Button()
        Me.pausebtn = New System.Windows.Forms.Button()
        Me.playbtn = New System.Windows.Forms.Button()
        Me.hold = New System.Windows.Forms.Button()
        Me.pause = New System.Windows.Forms.Button()
        CType(Me.VolumeScroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FuncgenPb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IpPlotPb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'add
        '
        Me.add.ForeColor = System.Drawing.SystemColors.ControlText
        Me.add.Location = New System.Drawing.Point(265, 48)
        Me.add.Name = "add"
        Me.add.Size = New System.Drawing.Size(33, 21)
        Me.add.TabIndex = 4
        Me.add.Text = "+"
        Me.add.UseVisualStyleBackColor = True
        '
        'del
        '
        Me.del.ForeColor = System.Drawing.SystemColors.ControlText
        Me.del.Location = New System.Drawing.Point(265, 87)
        Me.del.Name = "del"
        Me.del.Size = New System.Drawing.Size(33, 24)
        Me.del.TabIndex = 5
        Me.del.Text = "-"
        Me.del.UseVisualStyleBackColor = True
        '
        'addfile
        '
        Me.addfile.FileName = "addfile"
        Me.addfile.Filter = "wav|*.wav"
        '
        'VolumeScroll
        '
        Me.VolumeScroll.Location = New System.Drawing.Point(181, 222)
        Me.VolumeScroll.Name = "VolumeScroll"
        Me.VolumeScroll.Size = New System.Drawing.Size(145, 45)
        Me.VolumeScroll.TabIndex = 8
        '
        'SerialPort1
        '
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.l3l7, Me.l3l8, Me.l3l4, Me.l3l5, Me.l3l6, Me.l3l3, Me.l3l2, Me.l3l1, Me.l2l7, Me.l2l8, Me.l2l4, Me.l2l5, Me.l2l6, Me.l2l3, Me.l2l2, Me.l2l1, Me.l1l1, Me.l1l2, Me.l1l3, Me.l1l6, Me.l1l5, Me.l1l4, Me.l1l8, Me.l1l7})
        Me.ShapeContainer1.Size = New System.Drawing.Size(789, 463)
        Me.ShapeContainer1.TabIndex = 9
        Me.ShapeContainer1.TabStop = False
        '
        'l3l7
        '
        Me.l3l7.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l3l7.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l3l7.Location = New System.Drawing.Point(431, 62)
        Me.l3l7.Name = "l3l7"
        Me.l3l7.Size = New System.Drawing.Size(46, 18)
        '
        'l3l8
        '
        Me.l3l8.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l3l8.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l3l8.Location = New System.Drawing.Point(431, 45)
        Me.l3l8.Name = "l3l8"
        Me.l3l8.Size = New System.Drawing.Size(46, 18)
        '
        'l3l4
        '
        Me.l3l4.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l3l4.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l3l4.Location = New System.Drawing.Point(431, 117)
        Me.l3l4.Name = "l3l4"
        Me.l3l4.Size = New System.Drawing.Size(46, 19)
        '
        'l3l5
        '
        Me.l3l5.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l3l5.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l3l5.Location = New System.Drawing.Point(431, 98)
        Me.l3l5.Name = "l3l5"
        Me.l3l5.Size = New System.Drawing.Size(46, 19)
        '
        'l3l6
        '
        Me.l3l6.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l3l6.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l3l6.Location = New System.Drawing.Point(431, 80)
        Me.l3l6.Name = "l3l6"
        Me.l3l6.Size = New System.Drawing.Size(46, 18)
        '
        'l3l3
        '
        Me.l3l3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l3l3.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l3l3.Location = New System.Drawing.Point(431, 136)
        Me.l3l3.Name = "l3l3"
        Me.l3l3.Size = New System.Drawing.Size(46, 19)
        '
        'l3l2
        '
        Me.l3l2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l3l2.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l3l2.Location = New System.Drawing.Point(431, 154)
        Me.l3l2.Name = "l3l2"
        Me.l3l2.Size = New System.Drawing.Size(46, 18)
        '
        'l3l1
        '
        Me.l3l1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l3l1.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l3l1.Location = New System.Drawing.Point(431, 172)
        Me.l3l1.Name = "l3l1"
        Me.l3l1.Size = New System.Drawing.Size(46, 17)
        '
        'l2l7
        '
        Me.l2l7.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l2l7.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l2l7.Location = New System.Drawing.Point(374, 63)
        Me.l2l7.Name = "l2l7"
        Me.l2l7.Size = New System.Drawing.Size(46, 17)
        '
        'l2l8
        '
        Me.l2l8.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l2l8.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l2l8.Location = New System.Drawing.Point(374, 45)
        Me.l2l8.Name = "l2l8"
        Me.l2l8.Size = New System.Drawing.Size(46, 18)
        '
        'l2l4
        '
        Me.l2l4.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l2l4.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l2l4.Location = New System.Drawing.Point(374, 117)
        Me.l2l4.Name = "l2l4"
        Me.l2l4.Size = New System.Drawing.Size(46, 18)
        '
        'l2l5
        '
        Me.l2l5.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l2l5.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l2l5.Location = New System.Drawing.Point(374, 98)
        Me.l2l5.Name = "l2l5"
        Me.l2l5.Size = New System.Drawing.Size(46, 19)
        '
        'l2l6
        '
        Me.l2l6.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l2l6.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l2l6.Location = New System.Drawing.Point(374, 80)
        Me.l2l6.Name = "l2l6"
        Me.l2l6.Size = New System.Drawing.Size(46, 19)
        '
        'l2l3
        '
        Me.l2l3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l2l3.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l2l3.Location = New System.Drawing.Point(374, 135)
        Me.l2l3.Name = "l2l3"
        Me.l2l3.Size = New System.Drawing.Size(46, 20)
        '
        'l2l2
        '
        Me.l2l2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l2l2.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l2l2.Location = New System.Drawing.Point(374, 153)
        Me.l2l2.Name = "l2l2"
        Me.l2l2.Size = New System.Drawing.Size(46, 19)
        '
        'l2l1
        '
        Me.l2l1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l2l1.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l2l1.Location = New System.Drawing.Point(374, 170)
        Me.l2l1.Name = "l2l1"
        Me.l2l1.Size = New System.Drawing.Size(46, 19)
        '
        'l1l1
        '
        Me.l1l1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l1l1.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l1l1.Location = New System.Drawing.Point(317, 172)
        Me.l1l1.Name = "l1l1"
        Me.l1l1.Size = New System.Drawing.Size(46, 17)
        '
        'l1l2
        '
        Me.l1l2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l1l2.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l1l2.Location = New System.Drawing.Point(317, 154)
        Me.l1l2.Name = "l1l2"
        Me.l1l2.Size = New System.Drawing.Size(46, 20)
        '
        'l1l3
        '
        Me.l1l3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l1l3.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l1l3.Location = New System.Drawing.Point(317, 135)
        Me.l1l3.Name = "l1l3"
        Me.l1l3.Size = New System.Drawing.Size(46, 19)
        '
        'l1l6
        '
        Me.l1l6.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l1l6.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l1l6.Location = New System.Drawing.Point(317, 80)
        Me.l1l6.Name = "l1l6"
        Me.l1l6.Size = New System.Drawing.Size(46, 18)
        '
        'l1l5
        '
        Me.l1l5.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l1l5.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l1l5.Location = New System.Drawing.Point(317, 98)
        Me.l1l5.Name = "l1l5"
        Me.l1l5.Size = New System.Drawing.Size(46, 19)
        '
        'l1l4
        '
        Me.l1l4.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l1l4.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l1l4.Location = New System.Drawing.Point(317, 117)
        Me.l1l4.Name = "l1l4"
        Me.l1l4.Size = New System.Drawing.Size(46, 18)
        '
        'l1l8
        '
        Me.l1l8.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l1l8.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l1l8.Location = New System.Drawing.Point(317, 45)
        Me.l1l8.Name = "l1l8"
        Me.l1l8.Size = New System.Drawing.Size(46, 18)
        '
        'l1l7
        '
        Me.l1l7.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.l1l7.BorderColor = System.Drawing.SystemColors.ActiveBorder
        Me.l1l7.Location = New System.Drawing.Point(317, 63)
        Me.l1l7.Name = "l1l7"
        Me.l1l7.Size = New System.Drawing.Size(46, 17)
        '
        'fileslist
        '
        Me.fileslist.FormattingEnabled = True
        Me.fileslist.HorizontalScrollbar = True
        Me.fileslist.Location = New System.Drawing.Point(12, 35)
        Me.fileslist.Name = "fileslist"
        Me.fileslist.Size = New System.Drawing.Size(244, 160)
        Me.fileslist.TabIndex = 0
        '
        'rightch
        '
        Me.rightch.AutoSize = True
        Me.rightch.ForeColor = System.Drawing.Color.Black
        Me.rightch.Location = New System.Drawing.Point(416, 225)
        Me.rightch.Name = "rightch"
        Me.rightch.Size = New System.Drawing.Size(50, 17)
        Me.rightch.TabIndex = 22
        Me.rightch.TabStop = True
        Me.rightch.Text = "Right"
        Me.rightch.UseVisualStyleBackColor = True
        '
        'leftch
        '
        Me.leftch.AutoSize = True
        Me.leftch.ForeColor = System.Drawing.Color.Black
        Me.leftch.Location = New System.Drawing.Point(343, 225)
        Me.leftch.Name = "leftch"
        Me.leftch.Size = New System.Drawing.Size(43, 17)
        Me.leftch.TabIndex = 23
        Me.leftch.TabStop = True
        Me.leftch.Text = "Left"
        Me.leftch.UseVisualStyleBackColor = True
        '
        'ExitApps
        '
        Me.ExitApps.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ExitApps.Location = New System.Drawing.Point(739, 3)
        Me.ExitApps.Name = "ExitApps"
        Me.ExitApps.Size = New System.Drawing.Size(38, 25)
        Me.ExitApps.TabIndex = 24
        Me.ExitApps.Text = "X"
        Me.ExitApps.UseVisualStyleBackColor = True
        '
        'Min
        '
        Me.Min.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Min.Location = New System.Drawing.Point(697, 3)
        Me.Min.Name = "Min"
        Me.Min.Size = New System.Drawing.Size(38, 25)
        Me.Min.TabIndex = 25
        Me.Min.Text = "__"
        Me.Min.UseVisualStyleBackColor = True
        '
        'SlpM
        '
        Me.SlpM.ForeColor = System.Drawing.Color.Black
        Me.SlpM.Location = New System.Drawing.Point(697, 219)
        Me.SlpM.Name = "SlpM"
        Me.SlpM.Size = New System.Drawing.Size(80, 43)
        Me.SlpM.TabIndex = 26
        Me.SlpM.Text = "Sleep Mode"
        Me.SlpM.UseVisualStyleBackColor = True
        '
        'FuncgenPb
        '
        Me.FuncgenPb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FuncgenPb.BackColor = System.Drawing.Color.Black
        Me.FuncgenPb.Location = New System.Drawing.Point(509, 35)
        Me.FuncgenPb.Name = "FuncgenPb"
        Me.FuncgenPb.Size = New System.Drawing.Size(268, 160)
        Me.FuncgenPb.TabIndex = 13
        Me.FuncgenPb.TabStop = False
        '
        'IpPlotPb
        '
        Me.IpPlotPb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IpPlotPb.BackColor = System.Drawing.Color.Black
        Me.IpPlotPb.Location = New System.Drawing.Point(56, 276)
        Me.IpPlotPb.Name = "IpPlotPb"
        Me.IpPlotPb.Size = New System.Drawing.Size(684, 184)
        Me.IpPlotPb.TabIndex = 12
        Me.IpPlotPb.TabStop = False
        '
        'Stopbtn
        '
        Me.Stopbtn.Image = Global.Musical_Fountain.My.Resources.Resources.Oxygen_Icons1
        Me.Stopbtn.Location = New System.Drawing.Point(111, 222)
        Me.Stopbtn.Name = "Stopbtn"
        Me.Stopbtn.Size = New System.Drawing.Size(41, 40)
        Me.Stopbtn.TabIndex = 3
        Me.Stopbtn.UseVisualStyleBackColor = True
        '
        'pausebtn
        '
        Me.pausebtn.Image = Global.Musical_Fountain.My.Resources.Resources.Oxygen_Icons_org_Oxygen_Actions_media_playback_pause
        Me.pausebtn.Location = New System.Drawing.Point(63, 222)
        Me.pausebtn.Name = "pausebtn"
        Me.pausebtn.Size = New System.Drawing.Size(42, 40)
        Me.pausebtn.TabIndex = 2
        Me.pausebtn.UseVisualStyleBackColor = True
        '
        'playbtn
        '
        Me.playbtn.Image = Global.Musical_Fountain.My.Resources.Resources.Gakuseisean_Ivista_2_Alarm_Play
        Me.playbtn.Location = New System.Drawing.Point(14, 222)
        Me.playbtn.Name = "playbtn"
        Me.playbtn.Size = New System.Drawing.Size(43, 40)
        Me.playbtn.TabIndex = 1
        Me.playbtn.UseVisualStyleBackColor = True
        '
        'hold
        '
        Me.hold.Image = Global.Musical_Fountain.My.Resources.Resources.Oxygen_Icons1
        Me.hold.Location = New System.Drawing.Point(421, 293)
        Me.hold.Name = "hold"
        Me.hold.Size = New System.Drawing.Size(41, 40)
        Me.hold.TabIndex = 3
        Me.hold.UseVisualStyleBackColor = True
        '
        'pause
        '
        Me.pause.Image = Global.Musical_Fountain.My.Resources.Resources.Oxygen_Icons_org_Oxygen_Actions_media_playback_pause
        Me.pause.Location = New System.Drawing.Point(373, 293)
        Me.pause.Name = "pause"
        Me.pause.Size = New System.Drawing.Size(42, 40)
        Me.pause.TabIndex = 2
        Me.pause.UseVisualStyleBackColor = True
        '
        'Normal_Mode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(789, 463)
        Me.ControlBox = False
        Me.Controls.Add(Me.SlpM)
        Me.Controls.Add(Me.Min)
        Me.Controls.Add(Me.ExitApps)
        Me.Controls.Add(Me.rightch)
        Me.Controls.Add(Me.leftch)
        Me.Controls.Add(Me.FuncgenPb)
        Me.Controls.Add(Me.IpPlotPb)
        Me.Controls.Add(Me.VolumeScroll)
        Me.Controls.Add(Me.del)
        Me.Controls.Add(Me.add)
        Me.Controls.Add(Me.Stopbtn)
        Me.Controls.Add(Me.pausebtn)
        Me.Controls.Add(Me.playbtn)
        Me.Controls.Add(Me.fileslist)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.ForeColor = System.Drawing.SystemColors.ActiveBorder
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Normal_Mode"
        Me.Text = "Musical_Fountain"
        CType(Me.VolumeScroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FuncgenPb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IpPlotPb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents playbtn As System.Windows.Forms.Button
    Friend WithEvents pausebtn As System.Windows.Forms.Button
    Friend WithEvents add As System.Windows.Forms.Button
    Friend WithEvents del As System.Windows.Forms.Button
    Friend WithEvents Stopbtn As System.Windows.Forms.Button
    Friend WithEvents addfile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents hold As System.Windows.Forms.Button
    Friend WithEvents pause As System.Windows.Forms.Button
    Friend WithEvents VolumeScroll As System.Windows.Forms.TrackBar
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents l1l1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l1l2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l1l3 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l1l6 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l1l5 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l1l4 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l1l8 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l1l7 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l2l7 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l2l8 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l2l4 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l2l5 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l2l6 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l2l3 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l2l2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l2l1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l3l7 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l3l8 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l3l4 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l3l5 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l3l6 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l3l3 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l3l2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents l3l1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents IpPlotPb As System.Windows.Forms.PictureBox
    Friend WithEvents FuncgenPb As System.Windows.Forms.PictureBox
    Friend WithEvents fileslist As System.Windows.Forms.ListBox
    Friend WithEvents rightch As System.Windows.Forms.RadioButton
    Friend WithEvents leftch As System.Windows.Forms.RadioButton
    Friend WithEvents ExitApps As System.Windows.Forms.Button
    Friend WithEvents Min As System.Windows.Forms.Button
    Friend WithEvents SlpM As System.Windows.Forms.Button
End Class
