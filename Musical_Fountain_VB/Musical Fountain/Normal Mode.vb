Imports System
Imports System.Text
Imports System.IO.Ports
Imports System.Runtime.InteropServices
Imports System.Math
Imports System.Windows.Forms

Public Class Normal_Mode
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim foldersPath(256) As String
    Dim filescount As Integer = 0
    Private Shared ReadOnly sAlias As String = "TeaTimerAudio"
    Private isPaused As Boolean = False
    Private isStoped As Boolean = False
    Dim lch = True, rch = False
    Dim VMtab(3) As Long
    Dim val1 As Long = 0
    Dim GSamplingRate As Integer
    Dim GAmplitude As Integer
    Friend PrintFont As New Font("Courier New", 8)
    Friend CurrentY As Integer
    Friend CurrentX As Integer
    Friend Ce As System.Windows.Forms.PaintEventArgs
    Friend IpPlot As New Collection
    Friend FuncGen As New Collection
    Dim cPen As Pen
    Dim vwLeft As Boolean ' viewing left or right?
    Dim SamplingRate, UD As Integer
    Dim vm1, vm2, vm3 As Integer
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Declare the variables
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Function VarPtr(ByVal e) As Long
        Dim GC As GCHandle = GCHandle.Alloc(e, GCHandleType.Pinned)
        Dim NewGC As Integer = GC.AddrOfPinnedObject.ToInt32
        GC.Free()
        Return NewGC
    End Function
    Public Function StructPtr(ByVal StructureName As Object) As Long
        Dim pt As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(StructureName))
        Marshal.StructureToPtr(StructureName, pt, True)
        Return pt.ToInt64
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


    '---------------------------------------------------------------------------------------------------------

    <DllImport("winmm.dll")> _
    Private Shared Function mciSendString(ByVal strCommand As String, ByVal strReturn As StringBuilder, ByVal iReturnLength As Integer, ByVal hwndCallback As IntPtr) As Long
    End Function

    <DllImport("winmm.dll")> _
    Public Shared Function waveOutSetVolume(ByVal hwo As IntPtr, ByVal dwVolume As Int32) As Integer
    End Function

    Public Shared Function Status() As String 'Get Output Device Status function
        Dim sBuffer As New StringBuilder(128)
        mciSendString("status " & sAlias & " mode", sBuffer, sBuffer.Capacity, IntPtr.Zero)
        Return sBuffer.ToString()
    End Function

    Private Sub Open(ByVal sFileName As String)
        If Status() <> "" Then
            Stop_pl() ' Stop
        ElseIf sFileName.Length() > 0 Then
            FileName = sFileName
            SamplingRate = "44100"
            UD = 3
            OpenFile()
            Dim sCommand As String = "open """ & sFileName & """ alias " & sAlias
            mciSendString(sCommand, Nothing, 0, IntPtr.Zero)
        End If
    End Sub

    Public Shared Sub Stop_pl() 'Stop function
        Dim sCommand As String = "close " & sAlias
        mciSendString(sCommand, Nothing, 0, IntPtr.Zero)
    End Sub
    Public Shared Sub Resume_pl() 'Resume function
        Dim sCommand As String = "resume " & sAlias
        mciSendString(sCommand, Nothing, 0, IntPtr.Zero)
    End Sub
    Public Shared Sub Pause_pl() 'Pause function
        Dim sCommand As String = "pause " & sAlias
        mciSendString(sCommand, Nothing, 0, IntPtr.Zero)
    End Sub

    Public Shared Sub Play()
        Dim sCommand As String = "play " & sAlias
        mciSendString(sCommand, Nothing, 0, IntPtr.Zero)
    End Sub


    Public Sub PlotInput()
        Dim yOffset As Double
        Dim maxvalue, oldval, newval As Double
        IpPlot.Clear()
        IpPlot.Add(New CmdLn("C"c, Brushes.Black)) ' Cls
        yOffset = IpPlotPb.Height / 2    '&& x axis halfway down
        IpPlot.Add(New CmdLn("L"c, yOffset, 0, yOffset, IpPlotPb.Width, Pens.Green)) ' line
        maxvalue = Samples(1)
        For cnt As Integer = 2 To nSamples
            If maxvalue < Samples(cnt) Then
                maxvalue = Samples(cnt)
            End If
        Next cnt
        If maxvalue <> 0 Then
            'InputBx.Text = "Input                                                                                       Max=" & CStr(CInt(maxvalue / nSamples * 100)) & "%"
            Dim halfH As Double = IpPlotPb.Height / 2.0
            oldval = halfH - (Samples(1) * halfH / maxvalue)
            Dim sp As Integer = UD
            Dim cnt As Integer = 0
            For j As Integer = 2 To Math.Min(IpPlotPb.Width * sp, nSamples) - sp Step sp
                Dim nv As Double
                nv = Samples(j)
                For i As Integer = 1 To sp - 1
                    nv += Samples(i + j)
                Next
                newval = halfH - ((nv / sp) * halfH / maxvalue)
                cnt += 1
                IpPlot.Add(New CmdLn("L"c, oldval, cnt, newval, cnt + 1, Pens.Red)) ' line
                oldval = newval
            Next
        End If
        IpPlotPb.Invalidate()
    End Sub
    Sub ClearFunc()
        ' For cnt As Integer = 0 To nSamples
        '   FftData(cnt) = 0
        '  Next cnt
        FuncGen.Clear()
        FuncGen.Add(New CmdLn("C"c, Brushes.Black)) ' Cls
    End Sub
    Public Sub PlotOutput(ByVal cpen As Pen)
        Dim maxvalue, oldval, newval As Double
        maxvalue = FftData(1)
        For cnt As Integer = 2 To nSamples
            If maxvalue < FftData(cnt) Then
                maxvalue = FftData(cnt)
            End If
        Next cnt
        If maxvalue <> 0 Then
            Dim FromPt, ToPt, stepW As Integer
            stepW = 100
            If nSamples > 3000 Then stepW = 1000
            For cnt = stepW To (nSamples / 2) Step stepW
                Dim str As String = CStr(CInt(cnt * CInt(SamplingRate) / nSamples)) & "Hz"
                Dim textSize As Size = TextRenderer.MeasureText(str, PrintFont)
                Dim pOffst As Integer = textSize.Width / 2
                FromPt = cnt * FuncgenPb.Width / (nSamples / 2)
                FuncGen.Add(New CmdLn("L"c, 0, FromPt, FuncgenPb.Height, FromPt, Pens.Aquamarine)) ' line
                FuncGen.Add(New CmdLn("P"c, 0, FromPt - pOffst, str, Brushes.Black, Color.White)) ' Print
            Next
            Dim halfH As Double = FuncgenPb.Height / 2
            oldval = halfH - 0.8 * (FftData(1) * halfH / maxvalue)
            FromPt = 0
            For cnt As Integer = 2 To nSamples
                ToPt = cnt * FuncgenPb.Width / (nSamples / 2)
                newval = halfH - 0.8 * (FftData(cnt) * halfH / maxvalue)
                FuncGen.Add(New CmdLn("L"c, oldval, FromPt, newval, ToPt + 1, cpen)) ' line
                oldval = newval
                FromPt = ToPt
            Next cnt
        End If
        FuncgenPb.Invalidate()
    End Sub

    Sub PrepOutput()
        Dim cPen As Pen
        cPen = Pens.Green
        For cnt = 1 To nSamples / 2
            FftData(cnt) = Math.Sqrt((IMX(cnt) * IMX(cnt)) + (REX(cnt) * REX(cnt)))
        Next cnt

        For cnt = 1 To nSamples / 6
            val1 = FftData(cnt)
            VMtab(1) = val1 + VMtab(1)
        Next
        VMtab(1) = VMtab(1) / (nSamples / 6)

        For cnt = (nSamples / 6) + 1 To nSamples / 3
            val1 = FftData(cnt)
            VMtab(2) = val1 + VMtab(2)
        Next
        VMtab(2) = VMtab(2) / (nSamples / 6)

        For cnt = (nSamples / 3) + 1 To nSamples / 2
            val1 = FftData(cnt)
            VMtab(3) = val1 + VMtab(3)
        Next
        VMtab(3) = VMtab(3) / (nSamples / 6)
        'MsgBox(VMtab(1) & Chr(13) & VMtab(2) & Chr(13) & VMtab(3))
        PlotOutput(cPen)
    End Sub
    Private Sub LeftChannel()
        For i As Integer = 0 To nSamples
            Samples(i) = LeftSamples(i)
        Next
        vwLeft = True
        PlotInput()
        ClearFunc()
    End Sub
    Private Sub RightChannel()
        For i As Integer = 0 To nSamples
            Samples(i) = LeftSamples(i)
        Next
        vwLeft = True
        PlotInput()
        ClearFunc()
    End Sub

    Private Sub plot_sound_graph()
        NextFile()
        If lch Then
            For i As Integer = 0 To nSamples
                Samples(i) = LeftSamples(i)
            Next
        Else
            For i As Integer = 0 To nSamples
                Samples(i) = RightSamples(i)
            Next
        End If
        PlotInput()
        ClearFunc()
    End Sub

    Private Sub Plot_Spectrum()

        For cnt = 1 To nSamples ' left or right, whichever is visible
            REX(cnt) = Samples(cnt)
            IMX(cnt) = 0
        Next cnt
        FftF(REX, IMX, nSamples, nSamples, nSamples, 1)

        ' Calculate the inverse as follows:
        'FftF(REX, IMX, nSamples, nSamples, nSamples, -1)
        'For i As Integer = 0 To nSamples ' as the output is the value multiplied by number of samples
        '    REX(i) = REX(i) / nSamples
        'Next

        PrepOutput()
        ' waitLb.Visible = False
    End Sub

    Private Sub Clear_Sound_Graph()
        For cnt As Integer = 1 To nSamples
            Samples(cnt) = 0
        Next cnt
        IpPlot.Clear()
        IpPlot.Add(New CmdLn("C"c, Brushes.Black)) ' Cls
        IpPlotPb.Invalidate()
    End Sub
    Private Sub Clear_Sound_Spectrum()
        ClearFunc()
        PlotOutput(Pens.Black)
    End Sub
    '---------------------------------------------------------------------------------------------------------
    Private Sub PaintObj(ByVal cm As CmdLn, ByVal e As System.Windows.Forms.PaintEventArgs)
        If cm.cd = "C"c Then ' CLS
            e.Graphics.FillRectangle(cm.br, 0, 0, Me.Width, Me.Height)
        ElseIf cm.cd = "P"c Then ' PRINT
            Dim aRectangle As Rectangle
            Dim textSize As Size = TextRenderer.MeasureText(cm.str, PrintFont)
            aRectangle = New Rectangle(New Point(cm.x1, cm.y1), textSize)
            If Not cm.ovs Then e.Graphics.FillRectangle(cm.br, aRectangle)
            TextRenderer.DrawText(e.Graphics, cm.str, PrintFont, aRectangle, cm.cl)
        ElseIf cm.cd = "L"c Then ' LINE
            e.Graphics.DrawLine(cm.pn, cm.x1, cm.y1, cm.x2, cm.y2)
        Else
        End If
    End Sub
    Private Sub FuncgenPb_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles FuncgenPb.Paint
        For Each cm As CmdLn In FuncGen
            PaintObj(cm, e)
        Next
    End Sub
    Private Sub IpPlotPb_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles IpPlotPb.Paint
        For Each cm As CmdLn In IpPlot
            PaintObj(cm, e)
        Next
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub MusicalFountainHandler(ByVal freq, ByVal lev)
        If freq > 0 And freq < 4 And lev >= 0 And lev < 9 Then
            Dim cmd As Byte = 0
            cmd = freq << 4 Or lev
            SerialPort1.Write(Chr(cmd))
            If freq = 1 Then
                Select Case lev


                    Case 8
                        l1l1.BackColor = Color.Green
                        l1l2.BackColor = Color.Green
                        l1l3.BackColor = Color.Green
                        l1l4.BackColor = Color.Yellow
                        l1l5.BackColor = Color.Yellow
                        l1l6.BackColor = Color.Yellow
                        l1l7.BackColor = Color.Red
                        l1l8.BackColor = Color.Red

                    Case 7
                        l1l1.BackColor = Color.Green
                        l1l2.BackColor = Color.Green
                        l1l3.BackColor = Color.Green
                        l1l4.BackColor = Color.Yellow
                        l1l5.BackColor = Color.Yellow
                        l1l6.BackColor = Color.Yellow
                        l1l7.BackColor = Color.Red
                        l1l8.BackColor = Color.Transparent

                    Case 6
                        l1l1.BackColor = Color.Green
                        l1l2.BackColor = Color.Green
                        l1l3.BackColor = Color.Green
                        l1l4.BackColor = Color.Yellow
                        l1l5.BackColor = Color.Yellow
                        l1l6.BackColor = Color.Yellow
                        l1l7.BackColor = Color.Transparent
                        l1l8.BackColor = Color.Transparent

                    Case 5
                        l1l1.BackColor = Color.Green
                        l1l2.BackColor = Color.Green
                        l1l3.BackColor = Color.Green
                        l1l4.BackColor = Color.Yellow
                        l1l5.BackColor = Color.Yellow
                        l1l6.BackColor = Color.Transparent
                        l1l7.BackColor = Color.Transparent
                        l1l8.BackColor = Color.Transparent

                    Case 4
                        l1l1.BackColor = Color.Green
                        l1l2.BackColor = Color.Green
                        l1l3.BackColor = Color.Green
                        l1l4.BackColor = Color.Yellow
                        l1l5.BackColor = Color.Transparent
                        l1l6.BackColor = Color.Transparent
                        l1l7.BackColor = Color.Transparent
                        l1l8.BackColor = Color.Transparent

                    Case 3
                        l1l1.BackColor = Color.Green
                        l1l2.BackColor = Color.Green
                        l1l3.BackColor = Color.Green
                        l1l4.BackColor = Color.Transparent
                        l1l5.BackColor = Color.Transparent
                        l1l6.BackColor = Color.Transparent
                        l1l7.BackColor = Color.Transparent
                        l1l8.BackColor = Color.Transparent

                    Case 2
                        l1l1.BackColor = Color.Green
                        l1l2.BackColor = Color.Green
                        l1l3.BackColor = Color.Transparent
                        l1l4.BackColor = Color.Transparent
                        l1l5.BackColor = Color.Transparent
                        l1l6.BackColor = Color.Transparent
                        l1l7.BackColor = Color.Transparent
                        l1l8.BackColor = Color.Transparent

                    Case 1
                        l1l1.BackColor = Color.Green
                        l1l2.BackColor = Color.Transparent
                        l1l3.BackColor = Color.Transparent
                        l1l4.BackColor = Color.Transparent
                        l1l5.BackColor = Color.Transparent
                        l1l6.BackColor = Color.Transparent
                        l1l7.BackColor = Color.Transparent
                        l1l8.BackColor = Color.Transparent

                    Case 0
                        l1l1.BackColor = Color.Transparent
                        l1l2.BackColor = Color.Transparent
                        l1l3.BackColor = Color.Transparent
                        l1l4.BackColor = Color.Transparent
                        l1l5.BackColor = Color.Transparent
                        l1l6.BackColor = Color.Transparent
                        l1l7.BackColor = Color.Transparent
                        l1l8.BackColor = Color.Transparent

                End Select
            ElseIf freq = 2 Then
                Select Case lev


                    Case 8
                        l2l1.BackColor = Color.Green
                        l2l2.BackColor = Color.Green
                        l2l3.BackColor = Color.Green
                        l2l4.BackColor = Color.Yellow
                        l2l5.BackColor = Color.Yellow
                        l2l6.BackColor = Color.Yellow
                        l2l7.BackColor = Color.Red
                        l2l8.BackColor = Color.Red

                    Case 7
                        l2l1.BackColor = Color.Green
                        l2l2.BackColor = Color.Green
                        l2l3.BackColor = Color.Green
                        l2l4.BackColor = Color.Yellow
                        l2l5.BackColor = Color.Yellow
                        l2l6.BackColor = Color.Yellow
                        l2l7.BackColor = Color.Red
                        l2l8.BackColor = Color.Transparent

                    Case 6
                        l2l1.BackColor = Color.Green
                        l2l2.BackColor = Color.Green
                        l2l3.BackColor = Color.Green
                        l2l4.BackColor = Color.Yellow
                        l2l5.BackColor = Color.Yellow
                        l2l6.BackColor = Color.Yellow
                        l2l7.BackColor = Color.Transparent
                        l2l8.BackColor = Color.Transparent

                    Case 5
                        l2l1.BackColor = Color.Green
                        l2l2.BackColor = Color.Green
                        l2l3.BackColor = Color.Green
                        l2l4.BackColor = Color.Yellow
                        l2l5.BackColor = Color.Yellow
                        l2l6.BackColor = Color.Transparent
                        l2l7.BackColor = Color.Transparent
                        l2l8.BackColor = Color.Transparent

                    Case 4
                        l2l1.BackColor = Color.Green
                        l2l2.BackColor = Color.Green
                        l2l3.BackColor = Color.Green
                        l2l4.BackColor = Color.Yellow
                        l2l5.BackColor = Color.Transparent
                        l2l6.BackColor = Color.Transparent
                        l2l7.BackColor = Color.Transparent
                        l2l8.BackColor = Color.Transparent

                    Case 3
                        l2l1.BackColor = Color.Green
                        l2l2.BackColor = Color.Green
                        l2l3.BackColor = Color.Green
                        l2l4.BackColor = Color.Transparent
                        l2l5.BackColor = Color.Transparent
                        l2l6.BackColor = Color.Transparent
                        l2l7.BackColor = Color.Transparent
                        l2l8.BackColor = Color.Transparent

                    Case 2
                        l2l1.BackColor = Color.Green
                        l2l2.BackColor = Color.Green
                        l2l3.BackColor = Color.Transparent
                        l2l4.BackColor = Color.Transparent
                        l2l5.BackColor = Color.Transparent
                        l2l6.BackColor = Color.Transparent
                        l2l7.BackColor = Color.Transparent
                        l2l8.BackColor = Color.Transparent

                    Case 1
                        l2l1.BackColor = Color.Green
                        l2l2.BackColor = Color.Transparent
                        l2l3.BackColor = Color.Transparent
                        l2l4.BackColor = Color.Transparent
                        l2l5.BackColor = Color.Transparent
                        l2l6.BackColor = Color.Transparent
                        l2l7.BackColor = Color.Transparent
                        l2l8.BackColor = Color.Transparent

                    Case 0
                        l2l1.BackColor = Color.Transparent
                        l2l2.BackColor = Color.Transparent
                        l2l3.BackColor = Color.Transparent
                        l2l4.BackColor = Color.Transparent
                        l2l5.BackColor = Color.Transparent
                        l2l6.BackColor = Color.Transparent
                        l2l7.BackColor = Color.Transparent
                        l2l8.BackColor = Color.Transparent

                End Select
            Else
                Select Case lev

                    Case 8
                        l3l1.BackColor = Color.Green
                        l3l2.BackColor = Color.Green
                        l3l3.BackColor = Color.Green
                        l3l4.BackColor = Color.Yellow
                        l3l5.BackColor = Color.Yellow
                        l3l6.BackColor = Color.Yellow
                        l3l7.BackColor = Color.Red
                        l3l8.BackColor = Color.Red

                    Case 7
                        l3l1.BackColor = Color.Green
                        l3l2.BackColor = Color.Green
                        l3l3.BackColor = Color.Green
                        l3l4.BackColor = Color.Yellow
                        l3l5.BackColor = Color.Yellow
                        l3l6.BackColor = Color.Yellow
                        l3l7.BackColor = Color.Red
                        l3l8.BackColor = Color.Transparent

                    Case 6
                        l3l1.BackColor = Color.Green
                        l3l2.BackColor = Color.Green
                        l3l3.BackColor = Color.Green
                        l3l4.BackColor = Color.Yellow
                        l3l5.BackColor = Color.Yellow
                        l3l6.BackColor = Color.Yellow
                        l3l7.BackColor = Color.Transparent
                        l3l8.BackColor = Color.Transparent

                    Case 5
                        l3l1.BackColor = Color.Green
                        l3l2.BackColor = Color.Green
                        l3l3.BackColor = Color.Green
                        l3l4.BackColor = Color.Yellow
                        l3l5.BackColor = Color.Yellow
                        l3l6.BackColor = Color.Transparent
                        l3l7.BackColor = Color.Transparent
                        l3l8.BackColor = Color.Transparent

                    Case 4
                        l3l1.BackColor = Color.Green
                        l3l2.BackColor = Color.Green
                        l3l3.BackColor = Color.Green
                        l3l4.BackColor = Color.Yellow
                        l3l5.BackColor = Color.Transparent
                        l3l6.BackColor = Color.Transparent
                        l3l7.BackColor = Color.Transparent
                        l3l8.BackColor = Color.Transparent

                    Case 3
                        l3l1.BackColor = Color.Green
                        l3l2.BackColor = Color.Green
                        l3l3.BackColor = Color.Green
                        l3l4.BackColor = Color.Transparent
                        l3l5.BackColor = Color.Transparent
                        l3l6.BackColor = Color.Transparent
                        l3l7.BackColor = Color.Transparent
                        l3l8.BackColor = Color.Transparent

                    Case 2
                        l3l1.BackColor = Color.Green
                        l3l2.BackColor = Color.Green
                        l3l3.BackColor = Color.Transparent
                        l3l4.BackColor = Color.Transparent
                        l3l5.BackColor = Color.Transparent
                        l3l6.BackColor = Color.Transparent
                        l3l7.BackColor = Color.Transparent
                        l3l8.BackColor = Color.Transparent

                    Case 1
                        l3l1.BackColor = Color.Green
                        l3l2.BackColor = Color.Transparent
                        l3l3.BackColor = Color.Transparent
                        l3l4.BackColor = Color.Transparent
                        l3l5.BackColor = Color.Transparent
                        l3l6.BackColor = Color.Transparent
                        l3l7.BackColor = Color.Transparent
                        l3l8.BackColor = Color.Transparent

                    Case 0
                        l3l1.BackColor = Color.Transparent
                        l3l2.BackColor = Color.Transparent
                        l3l3.BackColor = Color.Transparent
                        l3l4.BackColor = Color.Transparent
                        l3l5.BackColor = Color.Transparent
                        l3l6.BackColor = Color.Transparent
                        l3l7.BackColor = Color.Transparent
                        l3l8.BackColor = Color.Transparent

                End Select
            End If
        Else
            Return
        End If

    End Sub



    Sub Delay(ByVal dblSecs As Double)

        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents() ' Allow windows messages to be processed
        Loop
    End Sub

    Private Function get_playlist_length() As Integer
        Dim length As Integer = 0
        For i = 0 To 255
            If foldersPath(i) <> Nothing Then
                length = length + 1
            End If
        Next
        Return (length)
    End Function

    Private Sub add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles add.Click
        Dim l As Integer = get_playlist_length()
        If l = 256 Then
            MsgBox("PlayList is Full")
        Else
            addfile.ShowDialog()
            Dim browseFile As System.IO.FileInfo
            browseFile = My.Computer.FileSystem.GetFileInfo(addfile.FileName)
            foldersPath(filescount) = browseFile.DirectoryName
            fileslist.Items.Add(browseFile.Name)
            filescount = filescount + 1
        End If
        If (get_playlist_length() > 0) Then
            del.Enabled = True
        End If
    End Sub

    Private Sub del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles del.Click
        Dim delpos As Integer = fileslist.SelectedIndex
        If delpos <> -1 Then
            Dim l As Integer = get_playlist_length()
            foldersPath(delpos) = Nothing
            fileslist.Items.Remove(fileslist.SelectedItem)
            While delpos < l - 1
                foldersPath(delpos) = foldersPath(delpos + 1)
                delpos = delpos + 1
            End While
        End If
        If (get_playlist_length() = 0) Then
            del.Enabled = False
        End If
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub playbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles playbtn.Click
        If fileslist.SelectedItem = Nothing Then
            MsgBox("Please Select your audio file to play")
            Exit Sub
        Else
            playbtn.Enabled = False
            pausebtn.Enabled = True
            Stopbtn.Enabled = True
            isStoped = False
            isPaused = False
            Dim filetoplay As String = foldersPath(fileslist.SelectedIndex) & "\" & fileslist.SelectedItem.ToString
            If leftch.Checked = False And rightch.Checked = False Then
                leftch.Checked = True
                rightch.Checked = False
            End If
            leftch.Visible = False
            rightch.Visible = False
            Open(filetoplay)
            Play()
            If lch Then
                LeftChannel()
            Else
                RightChannel()
            End If
            While isPaused = False And isStoped = False
                plot_sound_graph()
                Plot_Spectrum()
                vm1 = VMtab(1) / 70000
                vm2 = VMtab(2) / 5000
                vm3 = VMtab(3) / 4000
                MusicalFountainHandler(1, vm1)
                MusicalFountainHandler(2, vm2)
                MusicalFountainHandler(3, vm3)
                Delay(0.09)
            End While
            If Not isPaused Then
                leftch.Visible = True
                rightch.Visible = True
            End If
        End If
    End Sub

    Private Sub pausebtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pausebtn.Click
        leftch.Visible = False
        rightch.Visible = False
        playbtn.Enabled = False
        Stopbtn.Enabled = True
        If isPaused = False Then
            isPaused = True
            Pause_pl() 'Pause
            'Clear_Sound_Graph()
            'Clear_Sound_Spectrum()
            'MusicalFountainHandler(1, 0)
            'MusicalFountainHandler(2, 0)
            'MusicalFountainHandler(3, 0)
        Else
            Resume_pl()
            isPaused = False
            While isPaused = False And isStoped = False
                plot_sound_graph()
                Plot_Spectrum()
                vm1 = VMtab(1) / 57000
                vm2 = VMtab(2) / 5000
                vm3 = VMtab(3) / 4000
                MusicalFountainHandler(1, vm1)
                MusicalFountainHandler(2, vm2)
                MusicalFountainHandler(3, vm3)
                Delay(0.09)
            End While
        End If
    End Sub
    Private Sub StopFN()
        Stop_pl() 'stop
        isPaused = False
        isStoped = True
        Clear_Sound_Graph()
        Clear_Sound_Spectrum()
        MusicalFountainHandler(1, 0)
        MusicalFountainHandler(2, 0)
        MusicalFountainHandler(3, 0)
        playbtn.Enabled = True
        pausebtn.Enabled = False
        Stopbtn.Enabled = False
        leftch.Visible = True
        rightch.Visible = True
    End Sub
    Private Sub Stopbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Stopbtn.Click
        StopFN()
    End Sub

    Private Sub Volume_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VolumeScroll.Scroll
        'On Error Resume Next
        'UShort.MaxValue = 2^16 - 1= 65535
        'UShort.MinValue = 0
        Dim Volevel As Integer = ((UShort.MaxValue / 10) * VolumeScroll.Value)
        Volevel = (Volevel << 16) Or Volevel
        waveOutSetVolume(IntPtr.Zero, Volevel)
    End Sub

    Public Sub SerialPortCFG()
        SerialPort1.PortName = Connection.SelectedCOMPort 'Set SerialPort1 to the selected COM port at startup
        SerialPort1.BaudRate = 115200 'Set Baud rate to the selected value on
        SerialPort1.Parity = IO.Ports.Parity.None
        SerialPort1.StopBits = IO.Ports.StopBits.One
        SerialPort1.DataBits = 8 'Open our serial port
        If Not SerialPort1.IsOpen Then
            SerialPort1.Open()
        End If
    End Sub


    '---------------------------------------------------------------------------------------------------

    '---------------------------------------------------------------------------------------------------
    Private Sub Normal_Mode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        VolumeScroll.Value = 5
        playbtn.Enabled = True
        pausebtn.Enabled = False
        Stopbtn.Enabled = False
        del.Enabled = False
        SerialPortCFG()
        '---------------------------------------------------------------------------------------------------
    End Sub
    '---------------------------------------------------------------------------------------------------
    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        MsgBox(SerialPort1.ReadExisting()) 'Automatically called every time a data is received at the serialPort
    End Sub
    '---------------------------------------------------------------------------------------------------
    Private Sub Normal_Mode_UnLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing
        If SerialPort1.IsOpen Then
            SerialPort1.Close()
        End If
    End Sub

    Private Sub left_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles leftch.CheckedChanged
        If leftch.Checked = True Then
            lch = True
            rch = False
        End If
        leftch.Visible = False
        rightch.Visible = False
    End Sub

    Private Sub right_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rightch.CheckedChanged
        If rightch.Checked = True Then
            rch = True
            lch = False
        End If
        leftch.Visible = False
        rightch.Visible = False
    End Sub

    Private Sub Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Application.Exit()
    End Sub




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

    Private Sub ExitApps_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitApps.Click
        StopFN()
        If (SerialPort1.IsOpen) Then
            SerialPort1.Close()
        End If
        Application.Exit()
    End Sub

    Private Sub Min_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Min.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Sleep_Mode_Cycle()
        Dim cmd As Byte = 0
        Dim Ep1, Ep2, Ep3 As Integer
        While (Sleep_Mode.SleepMode = True)
            Ep1 = 1
            Ep2 = 0
            Ep3 = 0
            cmd = (1 << 6) Or (Ep3 << 2) Or (Ep2 << 1) Or Ep1
            SerialPort1.Write(Chr(cmd))
            Delay(0.03)
            Ep1 = 0
            Ep2 = 1
            Ep3 = 0
            cmd = (1 << 6) Or (Ep3 << 2) Or (Ep2 << 1) Or Ep1
            SerialPort1.Write(Chr(cmd))
            Delay(0.03)
            Ep1 = 0
            Ep2 = 0
            Ep3 = 1
            cmd = (1 << 6) Or (Ep3 << 2) Or (Ep2 << 1) Or Ep1
            SerialPort1.Write(Chr(cmd))
            Delay(0.03)
            Ep1 = 0
            Ep2 = 1
            Ep3 = 0
            cmd = (1 << 6) Or (Ep3 << 2) Or (Ep2 << 1) Or Ep1
            SerialPort1.Write(Chr(cmd))
            Delay(0.03)
            Ep1 = 1
            Ep2 = 0
            Ep3 = 0
            cmd = (1 << 6) Or (Ep3 << 2) Or (Ep2 << 1) Or Ep1
            SerialPort1.Write(Chr(cmd))
            Delay(0.03)
        End While
    End Sub

    Private Sub SlpM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SlpM.Click
        Delay(0.03)
        StopFN()
        Me.Hide()
        Sleep_Mode.SleepMode = True
        Sleep_Mode.Show()
        Sleep_Mode_Cycle()
    End Sub

End Class
Friend Class CmdLn ' to plot lines and text on the screen, simulates "old" LINE CLS and PRINT statements in Visial basic 6 in the Paint event of .NET
    Friend cd As Char ' type of operation C, L or P
    Friend x1 As Short ' coordinates
    Friend y1 As Short
    Friend x2 As Short
    Friend y2 As Short
    Friend str As String ' string to print
    Friend ovs As Boolean ' text in overstrike? else the rectangle is cleared before printing
    Friend cl As Color ' color
    Friend pn As Pen ' pen
    Friend br As Brush ' brush
    Private Sub New()
        cd = " "c
    End Sub
    Friend Sub New(ByVal c As Char, ByVal b As Brush)
        cd = c
        br = b
    End Sub
    Friend Sub New(ByVal c As Char, ByVal y As Short, ByVal x As Short, ByVal s As String)
        cd = c
        y1 = y
        x1 = x
        str = s
        br = Brushes.White
        cl = Color.Black
        ovs = False
    End Sub
    Friend Sub New(ByVal c As Char, ByVal y As Short, ByVal x As Short, ByVal s As String, ByVal b As Brush, ByVal clr As Color)
        cd = c
        y1 = y
        x1 = x
        str = s
        br = b
        cl = clr
        ovs = False
    End Sub
    Friend Sub New(ByVal c As Char, ByVal y As Short, ByVal x As Short, ByVal s As String, ByVal o As Boolean)
        cd = c
        y1 = y
        x1 = x
        str = s
        ovs = o
    End Sub
    Friend Sub New(ByVal c As Char, ByVal py1 As Short, ByVal px1 As Short, ByVal py2 As Short, ByVal px2 As Short, ByVal p As Pen)
        cd = c
        y1 = py1
        x1 = px1
        y2 = py2
        x2 = px2
        pn = p
    End Sub
End Class

Class MediaException
    Inherits System.Exception
    Public Sub New(ByVal s As String)
        Dim t As String
        t = s
    End Sub
End Class