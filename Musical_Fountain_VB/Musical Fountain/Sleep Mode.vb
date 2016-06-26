Public Class Sleep_Mode
    Public SleepMode As Boolean = True
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer


    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        drag = True 'Sets the variable drag to true
        mousex = Windows.Forms.Cursor.Position.X - Me.Left 'Sets variable mousex
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top 'Sets variable mousey
    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        'If drag is set to true then move the form accordingly.
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        drag = False 'Sets drag to false, so the form does not move according to the code in MouseMove
    End Sub



    Private Sub NrlM_Click(sender As System.Object, e As System.EventArgs) Handles NrlM.Click
        SleepMode = False
        Me.Hide()
        Normal_Mode.Show()
    End Sub

    Private Sub QApps_Click(sender As System.Object, e As System.EventArgs) Handles QApps.Click
        If (Normal_Mode.SerialPort1.IsOpen) Then
            Normal_Mode.SerialPort1.Close()
        End If
        Application.Exit()
    End Sub

   

    Private Sub Sleep_Mode_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.Show()
        Connection.Hide()
        If (SleepMode = False) Then
            Me.Hide()
        End If
    End Sub

End Class