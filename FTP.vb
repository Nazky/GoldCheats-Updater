Public Class FTP
    Private _credentials As System.Net.NetworkCredential
    Sub New(ByVal _FTPUser As String, ByVal _FTPPass As String)
        setCredentials(_FTPUser, _FTPPass)
    End Sub
    Public Sub UploadFile(ByVal _FileName As String, ByVal _UploadPath As String)
        Dim _FileInfo As New System.IO.FileInfo(_FileName)
        Dim _FtpWebRequest As System.Net.FtpWebRequest = CType(System.Net.FtpWebRequest.Create(New Uri(_UploadPath)), System.Net.FtpWebRequest)
        _FtpWebRequest.Credentials = _credentials
        _FtpWebRequest.KeepAlive = False
        _FtpWebRequest.Timeout = 2000
        _FtpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
        _FtpWebRequest.UseBinary = True
        _FtpWebRequest.ContentLength = _FileInfo.Length
        Dim buffLength As Integer = 2048
        Dim buff(buffLength - 1) As Byte
        Dim _FileStream As System.IO.FileStream = _FileInfo.OpenRead()
        Try
            Dim _Stream As System.IO.Stream = _FtpWebRequest.GetRequestStream()
            Dim contentLen As Integer = _FileStream.Read(buff, 0, buffLength)
            Do While contentLen <> 0
                _Stream.Write(buff, 0, contentLen)
                contentLen = _FileStream.Read(buff, 0, buffLength)
            Loop
            _Stream.Close()
            _Stream.Dispose()
            _FileStream.Close()
            _FileStream.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Upload Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub DownloadFile(ByVal _FileName As String, ByVal _ftpDownloadPath As String)
        Try
            Dim _request As System.Net.FtpWebRequest = System.Net.WebRequest.Create(_ftpDownloadPath)
            _request.KeepAlive = False
            _request.Method = System.Net.WebRequestMethods.Ftp.DownloadFile
            _request.Credentials = _credentials
            Dim _response As System.Net.FtpWebResponse = _request.GetResponse()
            Dim responseStream As System.IO.Stream = _response.GetResponseStream()
            Dim fs As New System.IO.FileStream(_FileName, System.IO.FileMode.Create)
            responseStream.CopyTo(fs)
            responseStream.Close()
            _response.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Download Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function GetDirectory(ByVal _ftpPath As String) As List(Of String)
        Dim ret As New List(Of String)
        Try
            Dim _request As System.Net.FtpWebRequest = System.Net.WebRequest.Create(_ftpPath)
            _request.KeepAlive = False
            _request.Method = System.Net.WebRequestMethods.Ftp.ListDirectoryDetails
            _request.Credentials = _credentials
            Dim _response As System.Net.FtpWebResponse = _request.GetResponse()
            Dim responseStream As System.IO.Stream = _response.GetResponseStream()
            Dim _reader As System.IO.StreamReader = New System.IO.StreamReader(responseStream)
            Dim FileData As String = _reader.ReadToEnd
            Dim Lines() As String = FileData.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
            For Each l As String In Lines
                ret.Add(l)
            Next
            _reader.Close()
            _response.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Directory Fetch Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return ret
    End Function

    Private Sub setCredentials(ByVal _FTPUser As String, ByVal _FTPPass As String)
        _credentials = New System.Net.NetworkCredential(_FTPUser, _FTPPass)
    End Sub
End Class