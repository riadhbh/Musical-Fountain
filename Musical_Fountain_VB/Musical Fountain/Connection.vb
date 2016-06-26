Imports System
Imports System.IO.Ports
Public Class Connection
    Dim NoDevice As Boolean = True
    Public SelectedCOMPort As String = Nothing
    Private Sub quit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles quit.Click
        End
    End Sub

    Dim Data As Char
    Private Sub connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles connect.Click
        SelectedCOMPort = ComCOM.SelectedItem.ToString
        Normal_Mode.Show()
        Me.Hide()
    End Sub

    Private Sub GetAvaibleCOMPorts()
        NoDevice = True
        Dim availableSerialPorts As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Computer.Ports.SerialPortNames
        Dim PortCount As Integer
        For PortCount = 1 To 256
            If availableSerialPorts.Contains("COM" & PortCount) Then
                ComCOM.Items.Add("COM" & PortCount)
                NoDevice = False
            End If
        Next
        If NoDevice Then
            ComCOM.SelectedIndex = -1
            connect.Enabled = False
        Else
            ComCOM.SelectedIndex = 0
            connect.Enabled = True
        End If
    End Sub


    Private Sub Connection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetAvaibleCOMPorts()
    End Sub

    Private Sub Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Refre.Click
        ComCOM.SelectedIndex = -1
        ComCOM.Items.Clear()
        GetAvaibleCOMPorts()
    End Sub

End Class

