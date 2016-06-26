Imports System.Runtime.InteropServices
Imports System.IO

Module Module1
    Private Declare Sub CopyWaveBufFromPointer Lib "kernel32" Alias "RtlMoveMemory" (ByVal dest() As Short, ByVal source As Integer, ByVal cb As Integer) '(ByRef dest As Any, ByRef src As Any, ByVal cb As Integer)
    Private Declare Sub CopyWaveBufToPointer Lib "kernel32" Alias "RtlMoveMemory" (ByVal dest As Integer, ByVal source() As Short, ByVal cb As Integer) '(ByRef dest As Any, ByRef src As Any, ByVal cb As Integer)
    Private Const MMIO_READ As Integer = &H0
    Private Const MMIO_FINDCHUNK As Integer = &H10
    Private Const MMIO_FINDRIFF As Integer = &H20
    Private Const MM_WOM_DONE As Integer = &H3BD
    Private Const MMSYSERR_NOERROR As Integer = 0
    Private Const SEEK_CUR As Integer = 1
    Private Const SEEK_END As Integer = 2
    Private Const SEEK_SET As Integer = 0
    Private Const TIME_BYTES As Integer = &H4
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure MMIOINFO
        Friend dwFlags As Integer
        Friend fccIOProc As Integer
        Friend pIOProc As Integer
        Friend wErrorRet As Integer
        Friend htask As Integer
        Friend cchBuffer As Integer
        Friend pchBuffer As String
        Friend pchNext As String
        Friend pchEndRead As String
        Friend pchEndWrite As String
        Friend lBufOffset As Integer
        Friend lDiskOffset As Integer
        Friend adwInfo1 As Integer
        Friend adwInfo2 As Integer
        Friend adwInfo3 As Integer
        Friend adwInfo4 As Integer
        Friend dwReserved1 As Integer
        Friend dwReserved2 As Integer
        Friend hmmio As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure WAVEHDR
        Friend lpData As Integer
        Friend dwBufferLength As Integer
        Friend dwBytesRecorded As Integer
        Friend dwUser As Integer
        Friend dwFlags As Integer
        Friend dwLoops As Integer
        Friend lpNext As Integer
        Friend Reserved As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure WAVEINCAPS
        Friend wMid As Short
        Friend wPid As Short
        Friend vDriverVersion As Integer
        Friend szPname As String
        Friend dwFormats As Integer
        Friend wChannels As Short
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure WAVEFORMAT
        Friend wFormatTag As Short
        Friend nChannels As Short
        Friend nSamplesPerSec As Integer
        Friend nAvgBytesPerSec As Integer
        Friend nBlockAlign As Short
        Friend wBitsPerSample As Short
        Friend cbSize As Short
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure MMCKINFO
        Friend ckid As Integer
        Friend ckSize As Integer
        Friend fccType As Integer
        Friend dwDataOffset As Integer
        Friend dwFlags As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure MMTIME
        Friend wType As Integer
        Friend u As Integer
        Friend x As Integer
    End Structure
    Private Declare Function mmioClose Lib "winmm.dll" (ByVal hmmio As IntPtr, ByVal uFlags As Integer) As Integer
    Private Declare Function mmioDescend Lib "winmm.dll" (ByVal hmmio As IntPtr, ByRef lpck As MMCKINFO, ByRef lpckParent As MMCKINFO, ByVal uFlags As Integer) As Integer
    Private Declare Function mmioDescendParent Lib "winmm.dll" Alias "mmioDescend" (ByVal hmmio As IntPtr, ByRef lpck As MMCKINFO, ByVal x As Integer, ByVal uFlags As Integer) As Integer
    Private Declare Ansi Function mmioOpen Lib "winmm.dll" Alias "mmioOpenA" (ByVal szFileName As String, ByRef lpmmioinfo As MMIOINFO, ByVal dwOpenFlags As Integer) As IntPtr
    Private Declare Function mmioRead Lib "winmm.dll" (ByVal hmmio As IntPtr, ByVal pch As Integer, ByVal cch As Integer) As Integer
    Private Declare Function mmioReadString Lib "winmm.dll" Alias "mmioRead" (ByVal hmmio As IntPtr, ByVal pch() As Byte, ByVal cch As Integer) As Integer
    Private Declare Function mmioSeek Lib "winmm.dll" (ByVal hmmio As IntPtr, ByVal lOffset As Integer, ByVal iOrigin As Integer) As Integer
    Private Declare Ansi Function mmioStringToFOURCC Lib "winmm.dll" Alias "mmioStringToFOURCCA" (ByVal sz As String, ByVal uFlags As Integer) As Integer
    Private Declare Function mmioAscend Lib "winmm.dll" (ByVal hmmio As IntPtr, ByRef lpck As MMCKINFO, ByVal uFlags As Integer) As Integer
    Private Declare Function GlobalAlloc Lib "kernel32" (ByVal wFlags As Integer, ByVal dwBytes As Integer) As IntPtr
    Private Declare Function GlobalLock Lib "kernel32" (ByVal hmem As IntPtr) As IntPtr
    Private Declare Function GlobalFree Lib "kernel32" (ByVal hmem As IntPtr) As Integer
    Private Declare Sub CopyWaveFormatFromBytes Lib "kernel32" Alias "RtlMoveMemory" (ByRef dest As WAVEFORMAT, ByVal source() As Byte, ByVal cb As Integer)
    Private Declare Sub CopyWaveHeaderFromPointer Lib "kernel32" Alias "RtlMoveMemory" (ByRef dest As WAVEHDR, ByVal source As Integer, ByVal cb As Integer)
    Private Delegate Sub WaveDelegate(ByVal hwo As IntPtr, ByVal uMsg As Integer, ByVal dwInstance As Integer, ByRef wavhdr As WAVEHDR, ByVal dwParam2 As Integer)
    ' for getting the WAV from the sound source.
    'Dim WavInGCHnd As GCHandle ' lock WAV buffers against Garbage collector of .Net
    Public Const NumBits As Short = 10 ' log2(NumSaples)
    ' Private ReversedBits(NumSamples) As Integer
    Dim nebuf As Integer
    Dim maxV As Integer
    Private CmdBase As New Collection
    Public Const AngleNumerator As Double = 6.28318530717958 ' 2 * Pi = 2 * 3.14159265358979
    Dim PrintFont As New Font("Courier New", 10)
    Dim maxVol As Integer = 10000
    Dim InputHandle As IntPtr = IntPtr.Zero
    Private m_FormatBuffer(49) As Byte
    Private m_Format As WAVEFORMAT ' waveformat structure
    Private m_DataOffset As Integer = 0
    Private m_AudioLength As Integer = 0
    Private m_BufferSize As Integer = 0
    Private m_StartPos As Integer = 0
    Private m_Initialized As Boolean = False
    Private m_DataRemaining As Integer = 0
    Private hmem(0) As IntPtr ' memory handle
    Private pmem(0) As IntPtr ' memory pointer
    Private hdr(0) As WAVEHDR ' wave header
    Private hHdr, hhmem, hpmem As GCHandle
    Friend FileName As String

    Friend nSamples As Integer = 8192
    Dim HalfSamples As Integer
    Friend REX(nSamples) As Double ' holds the real part of the frequency domain
    Friend IMX(nSamples) As Double ' holds the imaginary part of the frequency domain
    Friend RightSamples(nSamples) As Integer ' holds the right channel frequency domain
    Friend LeftSamples(nSamples) As Integer ' holds the left channel frequency domain
    Friend Samples(nSamples) As Single ' holds the wave form  
    Friend FftData(nSamples) As Double ' holds the fft to be displayed  
    Friend Sub OpenFile()
        m_Initialized = False
        FileName = FileName.Trim()
        If Not File.Exists(FileName) Then
            Throw New FileNotFoundException
            Exit Sub
        End If
        Dim mmckinfoParentIn As New MMCKINFO
        Dim mmckinfoSubchunkIn As New MMCKINFO
        Dim mmioinf As New MMIOINFO
        Dim rc, i As Integer
        'Open the input file
        InputHandle = mmioOpen(FileName, mmioinf, MMIO_READ)
        If (InputHandle.ToInt64 = 0) Then
            Throw New MediaException("Error while opening the input file.")
        End If
        'Check if this is a wave file
        mmckinfoParentIn.fccType = mmioStringToFOURCC("MPEG", 0)
        rc = mmioDescendParent(InputHandle, mmckinfoParentIn, 0, MMIO_FINDRIFF)
        'If (rc <> MMSYSERR_NOERROR) Then
        'CloseFile()
        'Throw New MediaException("Invalid file type.")
        'End If
        'Get format info
        mmckinfoSubchunkIn.ckid = mmioStringToFOURCC("fmt", 0)
        rc = mmioDescend(InputHandle, mmckinfoSubchunkIn, mmckinfoParentIn, MMIO_FINDCHUNK)
        ' If (rc <> MMSYSERR_NOERROR) Then
        'CloseFile()
        'Throw New MediaException("Couldn't find format chunk.")
        'End If
        rc = mmioReadString(InputHandle, m_FormatBuffer, mmckinfoSubchunkIn.ckSize)
        If (rc = -1) Then
            CloseFile()
            Throw New MediaException("Couldn't read from WAVE file.")
        End If
        rc = mmioAscend(InputHandle, mmckinfoSubchunkIn, 0)
        CopyWaveFormatFromBytes(m_Format, m_FormatBuffer, Marshal.SizeOf(m_Format))
        ' If m_Format.nChannels <> 2 Then
        'CloseFile()
        'Throw New MediaException("not stereo file")
        'End If
        ' If m_Format.nSamplesPerSec <> 44100 Then
        'CloseFile()
        'Throw New MediaException("only 44100 s/s")
        'End If
        'If m_Format.wBitsPerSample <> 16 Then
        'CloseFile()
        'Throw New MediaException("only 16 bit")
        'End If
        'Find the data subchunk
        mmckinfoSubchunkIn.ckid = mmioStringToFOURCC("data", 0)
        rc = mmioDescend(InputHandle, mmckinfoSubchunkIn, mmckinfoParentIn, MMIO_FINDCHUNK)
        'If (rc <> MMSYSERR_NOERROR) Then
        'CloseFile()
        'Throw New MediaException("Unable to find the data chunk.")
        'End If
        m_DataOffset = mmioSeek(InputHandle, 0, SEEK_CUR)
        'Get the length of the audio
        m_AudioLength = mmckinfoSubchunkIn.ckSize
        'Allocate 1 audio buffer
        m_BufferSize = nSamples
        i = 0
        GlobalFree(hmem(i))
        ' get pointers to the buffers and lock the garbage collector against moving the buffer
        hmem(i) = GlobalAlloc(0, m_BufferSize)
        pmem(i) = GlobalLock(hmem(i))
        hhmem = GCHandle.Alloc(hmem, GCHandleType.Pinned)
        hpmem = GCHandle.Alloc(pmem, GCHandleType.Pinned)
        hHdr = GCHandle.Alloc(hdr, GCHandleType.Pinned)
        i = 0
        hdr(i).lpData = pmem(i).ToInt32
        hdr(i).dwBufferLength = m_BufferSize
        hdr(i).dwFlags = 0
        hdr(i).dwLoops = 0
        '  successfully initialized
        m_Initialized = True
        m_StartPos = mmioSeek(InputHandle, 0, SEEK_CUR) - m_DataOffset
        m_DataRemaining = (m_DataOffset + m_AudioLength - mmioSeek(InputHandle, 0, SEEK_CUR))
        HalfSamples = Math.Truncate(m_BufferSize / 2)
        For j As Integer = 0 To HalfSamples Step nSamples
            LeftSamples(j) = 0
            RightSamples(j) = 0
        Next
        If m_BufferSize < m_DataRemaining Then
            readBlock(m_BufferSize, 0)
        End If
        If m_BufferSize < m_DataRemaining Then
            readBlock(m_BufferSize, HalfSamples)
        End If
    End Sub
    Sub readBlock(ByVal Blksize As Integer, ByVal hs As Integer)
        mmioRead(InputHandle, hdr(0).lpData, Blksize)
        m_DataRemaining = (m_DataOffset + m_AudioLength - mmioSeek(InputHandle, 0, SEEK_CUR))
        Dim WavBuf(Blksize - 1) As Short
        CopyWaveBufFromPointer(WavBuf, hdr(0).lpData, Blksize)
        Dim j As Integer = 0
        For i As Integer = 0 To Blksize - 1 Step 2
            LeftSamples(hs + j) = WavBuf(i)
            RightSamples(hs + j) = WavBuf(i + 1)
            j += 1
        Next
    End Sub
    Friend Function NextFile() As Boolean
        For j As Integer = 0 To HalfSamples Step nSamples
            LeftSamples(j) = 0
            RightSamples(j) = 0
        Next
        If (m_DataRemaining > 0) Then
            readBlock(m_BufferSize, 0)
        End If
        If (m_DataRemaining > 0) Then
            readBlock(m_BufferSize, HalfSamples)
        End If
        Return m_DataRemaining > 0
    End Function
    Friend Sub CloseFile()
        Dim i As Integer
        mmioClose(InputHandle, 0)
        i = 0
        GlobalFree(hmem(i))
        If hHdr.IsAllocated Then hHdr.Free()
        If hhmem.IsAllocated Then hhmem.Free()
        If hpmem.IsAllocated Then hpmem.Free()
    End Sub

    Function FftF(ByVal ReX() As Double, ByVal ImX() As Double, ByVal nTot As Integer, ByVal nCur As Integer, ByVal nSpan As Integer, ByVal iSign As Integer) As Boolean  ' && Fast Fourier Transform
        ' ======================================================================
        ' NIST Guide to Available Math Software.
        ' Source for module FFT from package GO.
        ' Retrieved from NETLIB on Wed Jul  5 11:50:07 1995.
        ' ======================================================================
        '  multivariate complex fourier transform, computed in place
        '    using mixed-radix fast fourier transform algorithm.
        '  by r. c. singleton, stanford research institute, sept. 1968
        '    translated from Fortran in Basic by Ambusy, june 2011
        '  arrays ReX and ImX originally hold the real and imaginary
        '    components of the data, and return the real and
        '    imaginary components of the resulting fourier coefficients.
        '  multivariate data is indexed according to the fortran
        '    array element successor function, without limit
        '    on the number of implied multiple subscripts.
        '    the subroutine is called once for each variate.
        '    the calls for ReX multivariate transform may be in any order.
        '  nTot is the total number of complex data values.
        '  nCur is the dimension of the current variable.
        '  nSpan/nCur is the spacing of consecutive data values
        '    while indexing the current variable.
        '  the sign of iSign determines the sign of the complex
        '    exponential, and the magnitude of iSign is normally one.
        '  When computing the inverse, the resulting ReX vector values must be divided by nCur
        '    to get the original value.
        '  ReX single-variate transform,
        '    is computed by
        '    nTot = nCur = nSpan = (number of complex data values), e.g.
        '      call fft(ReX,ImX,nTot,nTot,nTot,1)
        '  ReX tri-variate transform with ReX(n1,n2,n3), ImX(n1,n2,n3)
        '    is computed by
        '      call fft(ReX,ImX,n1*n2*n3,n1,n1,1)
        '      call fft(ReX,ImX,n1*n2*n3,n2,n1*n2,1)
        '      call fft(ReX,ImX,n1*n2*n3,n3,n1*n2*n3,1)
        '  arrays at(maxf), ck(maxf), bt(maxf), sk(maxf), and np(maxp)
        '    are used for temporary storage.  if the available storage
        '    is insufficient, the program is terminated by ReX stop.
        '    maxf must be > the maximum prime factor of nCur.
        '    maxp must be > the number of prime factors of nCur.
        '    in addition, if the square-free portion k of nCur has two or
        '    more prime factors, then maxp must be >= k-1.
        '  array storage in nfac for ReX maximum of 15 prime factors of nCur.
        '  if nCur has more than one square-free factor, the product of the
        '    square-free factors must be <= 210
        Dim nfac(11) As Integer, np(209) As Integer
        '  array storage for maximum prime factor of 23
        Dim at(23) As Double, ck(23) As Double, bt(23) As Double, sk(23) As Double
        '  the following two constants should agree with the array dimensions.
        Dim maxp As Integer = 209
        Dim maxf As Integer = 23
        If (nCur < 2) Then Return False
        Dim inc As Integer = iSign
        Dim c72 As Double = 0.3090169943749474
        Dim s72 As Double = 0.95105651629515353
        Dim s120 As Double = 0.8660254037844386
        Dim rad As Double = 6.2831853071796
        If (iSign < 0) Then
            s72 = -s72
            s120 = -s120
            rad = -rad
            inc = -inc
        End If
        ' continue
        Dim nt, ks, kspan, nn, jc, i, j, jj, jf, m, k, kk, kt, k1, k2 As Integer
        Dim radf, sd, cd, ak, bk, c1, s1 As Double
        nt = inc * nTot
        ks = inc * nSpan
        kspan = ks
        nn = nt - inc
        jc = Math.Truncate(ks / nCur)
        radf = rad * CDbl(jc) * 0.5
        i = 0
        jf = 0
        m = 0
        k = nCur
20:     If ((k And &HF) = 0) Then
            m = m + 1
            nfac(m) = 4
            k = Math.Truncate(k / 16)
            GoTo 20
        End If
        j = 3
        jj = 9
30:     If ((k Mod jj) = 0) Then
            m = m + 1
            nfac(m) = j
            k = Math.Truncate(k / jj)
            GoTo 30
        End If
        j = j + 2
        jj = j * j
        If (jj <= k) Then GoTo 30
        If (k <= 4) Then
            kt = m
            nfac(m + 1) = k
            If (k <> 1) Then m = m + 1
        Else
            If ((k And &H4) = 0) Then
                m = m + 1
                nfac(m) = 2
                k = Math.Truncate(k / 4)
            End If
            kt = m
            j = 2
60:         If ((k Mod j) = 0) Then
                m = m + 1
                nfac(m) = j
                k = Math.Truncate(k / j)
            End If
            j = Math.Truncate((j + 1) / 2) * 2 + 1
            If (j <= k) Then GoTo 60
        End If
        ' continue
        If (kt <> 0) Then
            j = kt
90:         m = m + 1
            nfac(m) = nfac(j)
            j = j - 1
            If (j <> 0) Then GoTo 90
        End If
100:    ' continue
        sd = radf / CDbl(kspan)
        cd = Math.Sin(sd)
        cd = 2.0 * cd * cd
        sd = Math.Sin(sd + sd)
        kk = 1
        i = i + 1
        If (nfac(i) <> 2) Then GoTo 400
        kspan = Math.Truncate(kspan / 2)
        k1 = kspan + 2
210:    k2 = kk + kspan
        ak = ReX(k2)
        bk = ImX(k2)
        ReX(k2) = ReX(kk) - ak
        ImX(k2) = ImX(kk) - bk
        ReX(kk) = ReX(kk) + ak
        ImX(kk) = ImX(kk) + bk
        kk = k2 + kspan
        If (kk <= nn) Then GoTo 210
        kk = kk - nn
        If (kk <= jc) Then GoTo 210
        If (kk > kspan) Then GoTo 800
220:    c1 = 1.0 - cd
        s1 = sd
230:    k2 = kk + kspan
        ak = ReX(kk) - ReX(k2)
        bk = ImX(kk) - ImX(k2)
        ReX(kk) = ReX(kk) + ReX(k2)
        ImX(kk) = ImX(kk) + ImX(k2)
        ReX(k2) = c1 * ak - s1 * bk
        ImX(k2) = s1 * ak + c1 * bk
        kk = k2 + kspan
        If (kk < nt) Then GoTo 230
        k2 = kk - nt
        c1 = -c1
        kk = k1 - k2
        If (kk > k2) Then GoTo 230
        ak = c1 - (cd * c1 + sd * s1)
        s1 = (sd * c1 - cd * s1) + s1
        c1 = 2.0 - (ak * ak + s1 * s1)
        s1 = c1 * s1
        c1 = c1 * ak
        kk = kk + jc
        If (kk < k2) Then GoTo 230
        k1 = k1 + inc + inc
        kk = Math.Truncate((k1 - kspan) / 2) + jc
        If (kk <= jc + jc) Then GoTo 220
        GoTo 100
320:    ' continue
        Dim aj, akp, akm, ajp, ajm, bj, bkp, bkm, bjp, bjm, c2, c3, s2, s3, aa, bb As Double
        Dim kspnn, k3, k4 As Integer
        k1 = kk + kspan
        k2 = k1 + kspan
        ak = ReX(kk)
        bk = ImX(kk)
        aj = ReX(k1) + ReX(k2)
        bj = ImX(k1) + ImX(k2)
        ReX(kk) = ak + aj
        ImX(kk) = bk + bj
        ak = ak - 0.5 * aj
        bk = bk - 0.5 * bj
        aj = (ReX(k1) - ReX(k2)) * s120
        bj = (ImX(k1) - ImX(k2)) * s120
        ReX(k1) = ak - bj
        ImX(k1) = bk + aj
        ReX(k2) = ak + bj
        ImX(k2) = bk - aj
        kk = k2 + kspan
        If (kk < nn) Then GoTo 320
        kk = kk - nn
        If (kk <= kspan) Then GoTo 320
        GoTo 700
400:    ' continue
        If (nfac(i) <> 4) Then GoTo 600
        kspnn = kspan
        kspan = Math.Truncate(kspan / 4)
410:    ' continue
        c1 = 1.0
        s1 = 0
420:    ' continue
        k1 = kk + kspan
        k2 = k1 + kspan
        k3 = k2 + kspan
        akp = ReX(kk) + ReX(k2)
        akm = ReX(kk) - ReX(k2)
        ajp = ReX(k1) + ReX(k3)
        ajm = ReX(k1) - ReX(k3)
        ReX(kk) = akp + ajp
        ajp = akp - ajp
        bkp = ImX(kk) + ImX(k2)
        bkm = ImX(kk) - ImX(k2)
        bjp = ImX(k1) + ImX(k3)
        bjm = ImX(k1) - ImX(k3)
        ImX(kk) = bkp + bjp
        bjp = bkp - bjp
        If (iSign < 0) Then
            GoTo 450
        Else
            akp = akm - bjm
            akm = akm + bjm
            bkp = bkm + ajm
            bkm = bkm - ajm
        End If
        If (s1 = 0) Then GoTo 460
430:    ' continue
        ReX(k1) = akp * c1 - bkp * s1
        ImX(k1) = akp * s1 + bkp * c1
        ReX(k2) = ajp * c2 - bjp * s2
        ImX(k2) = ajp * s2 + bjp * c2
        ReX(k3) = akm * c3 - bkm * s3
        ImX(k3) = akm * s3 + bkm * c3
        kk = k3 + kspan
        If (kk <= nt) Then GoTo 420
440:    ' continue
        c2 = c1 - (cd * c1 + sd * s1)
        s1 = (sd * c1 - cd * s1) + s1
        c1 = 2.0 - (c2 * c2 + s1 * s1)
        s1 = c1 * s1
        c1 = c1 * c2
        c2 = c1 * c1 - s1 * s1
        s2 = 2.0 * c1 * s1
        c3 = c2 * c1 - s2 * s1
        s3 = c2 * s1 + s2 * c1
        kk = kk - nt + jc
        If (kk <= kspan) Then GoTo 420
        kk = kk - kspan + inc
        If (kk <= jc) Then GoTo 410
        If (kspan = jc) Then GoTo 800
        GoTo 100
450:    ' continue
        akp = akm + bjm
        akm = akm - bjm
        bkp = bkm - ajm
        bkm = bkm + ajm
        If (s1 <> 0) Then GoTo 430
460:    ' continue
        ReX(k1) = akp
        ImX(k1) = bkp
        ReX(k2) = ajp
        ImX(k2) = bjp
        ReX(k3) = akm
        ImX(k3) = bkm
        kk = k3 + kspan
        If (kk <= nt) Then GoTo 420
        GoTo 440
510:    ' continue
        c2 = c72 * c72 - s72 * s72
        s2 = 2.0 * c72 * s72
520:    k1 = kk + kspan
        k2 = k1 + kspan
        k3 = k2 + kspan
        k4 = k3 + kspan
        akp = ReX(k1) + ReX(k4)
        akm = ReX(k1) - ReX(k4)
        bkp = ImX(k1) + ImX(k4)
        bkm = ImX(k1) - ImX(k4)
        ajp = ReX(k2) + ReX(k3)
        ajm = ReX(k2) - ReX(k3)
        bjp = ImX(k2) + ImX(k3)
        bjm = ImX(k2) - ImX(k3)
        aa = ReX(kk)
        bb = ImX(kk)
        ReX(kk) = aa + akp + ajp
        ImX(kk) = bb + bkp + bjp
        ak = akp * c72 + ajp * c2 + aa
        bk = bkp * c72 + bjp * c2 + bb
        aj = akm * s72 + ajm * s2
        bj = bkm * s72 + bjm * s2
        ReX(k1) = ak - bj
        ReX(k4) = ak + bj
        ImX(k1) = bk + aj
        ImX(k4) = bk - aj
        ak = akp * c2 + ajp * c72 + aa
        bk = bkp * c2 + bjp * c72 + bb
        aj = akm * s2 - ajm * s72
        bj = bkm * s2 - bjm * s72
        ReX(k2) = ak - bj
        ReX(k3) = ak + bj
        ImX(k2) = bk + aj
        ImX(k3) = bk - aj
        kk = k4 + kspan
        If (kk < nn) Then GoTo 520
        kk = kk - nn
        If (kk <= kspan) Then GoTo 520
        GoTo 700
600:    ' continue
        k = nfac(i)
        kspnn = kspan
        kspan = Math.Truncate(kspan / k)
        If (k = 3) Then
            GoTo 320
        End If
        If (k = 5) Then
            GoTo 510
        End If
        If (k = jf) Then GoTo 640
        jf = k
        s1 = rad / CDbl(k)
        c1 = Math.Cos(s1)
        s1 = Math.Sin(s1)
        If (jf > maxf) Then GoTo 998
        ck(jf) = 1.0
        sk(jf) = 0.0
        j = 1
630:    ck(j) = ck(k) * c1 + sk(k) * s1
        sk(j) = ck(k) * s1 - sk(k) * c1
        k = k - 1
        ck(k) = ck(j)
        sk(k) = -sk(j)
        j = j + 1
        If (j < k) Then GoTo 630
640:    ' continue
        k1 = kk
        k2 = kk + kspnn
        aa = ReX(kk)
        bb = ImX(kk)
        ak = aa
        bk = bb
        j = 1
        k1 = k1 + kspan
650:    k2 = k2 - kspan
        j = j + 1
        at(j) = ReX(k1) + ReX(k2)
        ak = at(j) + ak
        bt(j) = ImX(k1) + ImX(k2)
        bk = bt(j) + bk
        j = j + 1
        at(j) = ReX(k1) - ReX(k2)
        bt(j) = ImX(k1) - ImX(k2)
        k1 = k1 + kspan
        If (k1 < k2) Then GoTo 650
        ReX(kk) = ak
        ImX(kk) = bk
        k1 = kk
        k2 = kk + kspnn
        j = 1
660:    k1 = k1 + kspan
        k2 = k2 - kspan
        jj = j
        ak = aa
        bk = bb
        aj = 0.0
        bj = 0.0
        k = 1
670:    k = k + 1
        ak = at(k) * ck(jj) + ak
        bk = bt(k) * ck(jj) + bk
        k = k + 1
        aj = at(k) * sk(jj) + aj
        bj = bt(k) * sk(jj) + bj
        jj = jj + j
        If (jj > jf) Then jj = jj - jf
        If (k < jf) Then GoTo 670
        k = jf - j
        ReX(k1) = ak - bj
        ImX(k1) = bk + aj
        ReX(k2) = ak + bj
        ImX(k2) = bk - aj
        j = j + 1
        If (j < k) Then GoTo 660
        kk = kk + kspnn
        If (kk <= nn) Then GoTo 640
        kk = kk - nn
        If (kk <= kspan) Then GoTo 640
700:    ' continue
        If (i = m) Then GoTo 800
        kk = jc + 1
710:    c2 = 1.0 - cd
        s1 = sd
720:    c1 = c2
        s2 = s1
        kk = kk + kspan
730:    ak = ReX(kk)
        ReX(kk) = c2 * ak - s2 * ImX(kk)
        ImX(kk) = s2 * ak + c2 * ImX(kk)
        kk = kk + kspnn
        If (kk <= nt) Then GoTo 730
        ak = s1 * s2
        s2 = s1 * c2 + c1 * s2
        c2 = c1 * c2 - ak
        kk = kk - nt + kspan
        If (kk <= kspnn) Then GoTo 730
        c2 = c1 - (cd * c1 + sd * s1)
        s1 = s1 + (sd * c1 - cd * s1)
        c1 = 2.0 - (c2 * c2 + s1 * s1)
        s1 = c1 * s1
        c2 = c1 * c2
        kk = kk - kspnn + jc
        If (kk <= kspan) Then GoTo 720
        kk = kk - kspan + jc + inc
        If (kk <= jc + jc) Then GoTo 710
        GoTo 100
800:    ' continue
        np(1) = ks
        If (kt = 0) Then GoTo 890
        k = kt + kt + 1
        If (m < k) Then k = k - 1
        j = 1
        np(k + 1) = jc
810:    np(j + 1) = Math.Truncate(np(j) / nfac(j))
        np(k) = np(k + 1) * nfac(j)
        j = j + 1
        k = k - 1
        If (j < k) Then GoTo 810
        k3 = np(k + 1)
        kspan = np(2)
        kk = jc + 1
        k2 = kspan + 1
        j = 1
        If (nCur <> nTot) Then GoTo 850
820:    ' continue
825:    ak = ReX(kk)
        ReX(kk) = ReX(k2)
        ReX(k2) = ak
        bk = ImX(kk)
        ImX(kk) = ImX(k2)
        ImX(k2) = bk
        kk = kk + inc
        k2 = k2 + kspan
        If (k2 < ks) Then GoTo 825
830:    ' continue
835:    ' continue
        k2 = k2 - np(j)
        j = j + 1
        k2 = k2 + np(j + 1)
        If (k2 > np(j)) Then GoTo 835
        j = 1
840:    If (kk < k2) Then GoTo 820
        kk = kk + inc
        k2 = k2 + kspan
        If (k2 < ks) Then GoTo 840
        If (kk < ks) Then GoTo 835
        jc = k3
        GoTo 890
850:    ' continue
        k = kk + jc
860:    ak = ReX(kk)
        ReX(kk) = ReX(k2)
        ReX(k2) = ak
        bk = ImX(kk)
        ImX(kk) = ImX(k2)
        ImX(k2) = bk
        kk = kk + inc
        k2 = k2 + inc
        If (kk < k) Then GoTo 860
        kk = kk + ks - jc
        k2 = k2 + ks - jc
        If (kk < nt) Then GoTo 850
        k2 = k2 - nt + kspan
        kk = kk - nt + jc
        If (k2 < ks) Then GoTo 850
870:    ' continue
875:    ' continue
        k2 = k2 - np(j)
        j = j + 1
        k2 = k2 + np(j + 1)
        If (k2 > np(j)) Then GoTo 875
        j = 1
880:    ' continue
        If (kk < k2) Then GoTo 850
        kk = kk + jc
        k2 = k2 + kspan
        If (k2 < ks) Then GoTo 880
        If (kk < ks) Then GoTo 870
        jc = k3
890:    ' continue
        If (2 * kt + 1 >= m) Then Return True
        kspnn = np(kt + 1)
        j = m - kt
        nfac(j + 1) = 1
900:    nfac(j) = nfac(j) * nfac(j + 1)
        j = j - 1
        If (j <> kt) Then GoTo 900
        kt = kt + 1
        nn = nfac(kt) - 1
        If (nn > maxp) Then GoTo 998
        jj = 0
        j = 0
        GoTo 906
904:    ' continue
        jj = jj + kk
        If (jj >= k2) Then
            jj = jj - k2
            k2 = kk
            k = k + 1
            kk = nfac(k)
        Else
            GoTo 903
        End If
903:    ' continue
        np(j) = jj
        GoTo 906
906:    ' continue
        k2 = nfac(kt)
        k = kt + 1
        kk = nfac(k)
        j = j + 1
        If (j <= nn) Then GoTo 904
        j = 0
        GoTo 914
910:    ' continue
        k = kk
        kk = np(k)
        np(k) = -kk
        If (kk <> j) Then GoTo 910
        k3 = kk
914:    ' continue
        j = j + 1
        kk = np(j)
        If (kk < 0) Then GoTo 914
        If (kk <> j) Then GoTo 910
        np(j) = -j
        If (j <> nn) Then GoTo 914
        maxf = inc * maxf
        GoTo 920
920:    ' continue      
        j = k3 + 1
        nt = nt - kspnn
        i = nt - inc + 1
        If (nt < 0) Then Return True
924:    j = j - 1
        If (np(j) < 0) Then GoTo 924
        jj = jc
926:    kspan = jj
        If (jj > maxf) Then kspan = maxf
        jj = jj - kspan
        k = np(j)
        kk = jc * k + i + jj
        k1 = kk + kspan
        k2 = 0
928:    k2 = k2 + 1
        at(k2) = ReX(k1)
        bt(k2) = ImX(k1)
        k1 = k1 - inc
        If (k1 <> kk) Then GoTo 928
932:    k1 = kk + kspan
        k2 = k1 - jc * (k + np(k))
        k = -np(k)
936:    ReX(k1) = ReX(k2)
        ImX(k1) = ImX(k2)
        k1 = k1 - inc
        k2 = k2 - inc
        If (k1 <> kk) Then GoTo 936
        kk = k2
        If (k <> j) Then GoTo 932
        k1 = kk + kspan
        k2 = 0
940:    k2 = k2 + 1
        ReX(k1) = at(k2)
        ImX(k1) = bt(k2)
        k1 = k1 - inc
        If (k1 <> kk) Then GoTo 940
        If (jj <> 0) Then GoTo 926
        If (j <> 1) Then GoTo 924
        GoTo 920
998:    ' continue
        iSign = 0
        MsgBox("array bounds exceeded within subroutine fft", MsgBoxStyle.Critical)
        Return False
    End Function


End Module
