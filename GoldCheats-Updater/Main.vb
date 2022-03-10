Imports System.IO
Imports System.Net
Imports System.Collections.Generic
Imports System.IO.Compression

Public Class Main
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        TextBox1.Clear()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Directory.Exists(Application.StartupPath & "/json") Then
            updatecheats()
        Else
            Directory.CreateDirectory(Application.StartupPath & "/json")
        End If

        If My.Settings.IP = "" Then
        Else
            TextBox1.Text = My.Settings.IP
        End If
    End Sub

    Sub updatecheats()
        Try
            ListView1.Clear()
            Dim lvi As ListViewItem
            Dim di As New DirectoryInfo(Application.StartupPath & "/json")
            Dim myIcon As Icon

            ' ext/icon lookup
            Dim exts As New List(Of String)
            ImageList1.Images.Clear()

            For Each fi As FileInfo In di.EnumerateFiles("*.json*")

                lvi = New ListViewItem
                lvi.Text = fi.Name
                lvi.SubItems.Add(Path.GetDirectoryName(fi.FullName))

                lvi.SubItems.Add(((fi.Length / 1024)).ToString("0.00"))
                lvi.SubItems.Add(fi.CreationTime.ToShortDateString)

                If exts.Contains(fi.Extension) = False Then
                    myIcon = Icon.ExtractAssociatedIcon(fi.FullName)
                    ImageList1.Images.Add(fi.Extension, myIcon)
                    exts.Add(fi.Extension)
                End If

                lvi.ImageKey = fi.Extension
                ListView1.Items.Add(lvi)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub client_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
        Dim percentage As Double = bytesIn / totalBytes * 100

        Try
            If Label1.Text = "N/A" Then
                Button2.PerformClick()
            Else
                ProgressBar1.Value = Int32.Parse(Math.Truncate(percentage).ToString())
                Label1.Text = ProgressBar1.Value & " %"
            End If

        Catch ex As Exception
            Button2.PerformClick()
        End Try


    End Sub

    Private Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        'MsgBox("Download Complete", MsgBoxStyle.Information)
        Button2.Text = "Update all cheats"
        Button2.Enabled = True
        extract(Application.StartupPath & "/kmeps4.zip", Application.StartupPath & "/kmeps4")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If File.Exists(Application.StartupPath & "/kmeps4.zip") Then
                File.Delete(Application.StartupPath & "/kmeps4.zip")
            End If

            ProgressBar1.Visible = True
            Label1.Visible = True
            Dim client As WebClient = New WebClient
            AddHandler client.DownloadProgressChanged, AddressOf client_ProgressChanged
            AddHandler client.DownloadFileCompleted, AddressOf client_DownloadCompleted
            client.DownloadFileAsync(New Uri("https://github.com/kmeps4/Cheats_Repo/raw/main/cheats.zip"), Application.StartupPath & "/kmeps4.zip")
            Button2.Text = "Download in Progress"
            Button2.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try


    End Sub

    Sub extract(file As String, path As String)
        Try
            If Directory.Exists(path) Then
                Directory.Delete(path, True)
            End If
            ZipFile.ExtractToDirectory(file, path)

            Dim di As New DirectoryInfo(Application.StartupPath & "/kmeps4/json")

            For Each fi As FileInfo In di.EnumerateFiles("*.json")
                System.IO.File.Copy(fi.FullName, Application.StartupPath & "\json\" & fi.Name, True)
            Next

            Directory.Delete(Application.StartupPath & "/kmeps4", True)
            System.IO.File.Delete(Application.StartupPath & "/kmeps4.zip")
            System.IO.File.Delete(Application.StartupPath & "/json/list.json")
            updatecheats()

            ProgressBar1.Visible = False
            Label1.Text = "N/A"
            Label1.Visible = False

            Me.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim di As New DirectoryInfo(Application.StartupPath & "/json")

            For Each fi As FileInfo In di.EnumerateFiles("*.json")
                System.IO.File.Delete(Application.StartupPath & "\json\" & fi.Name)
            Next

            updatecheats()
            Me.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        updatecheats()
    End Sub

    Sub sendcheats()
        Dim overwrite As Boolean = False
        Try
            Dim di As New DirectoryInfo(Application.StartupPath & "/json")
            Dim ftp As New FTP("", "")
            Dim ow = MsgBox("Do you want to overwrite existing cheats ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation)

            If ow = MsgBoxResult.Yes Then
                overwrite = True
            Else
                overwrite = False
            End If

            MsgBox("PLEASE WAIT AND DON'T TOUCH THE SOFTWARE !", MsgBoxStyle.Exclamation)

            For Each fi As FileInfo In di.EnumerateFiles("*.json")
                If File.Exists("ftp://" & TextBox1.Text & ":2121/data/GoldHEN/cheats/json/" & fi.Name) And overwrite = True Then
                    ftp.UploadFile(fi.FullName, "ftp://" & TextBox1.Text & ":2121/data/GoldHEN/cheats/json/" & fi.Name)
                ElseIf File.Exists("ftp://" & TextBox1.Text & ":2121/data/GoldHEN/cheats/json/" & fi.Name) And overwrite = False Then

                Else
                    ftp.UploadFile(fi.FullName, "ftp://" & TextBox1.Text & ":2121/data/GoldHEN/cheats/json/" & fi.Name)
                End If

            Next

            MsgBox("Cheats send !", MsgBoxStyle.Information)
            Me.UseWaitCursor = False

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox1.Text = "IP Here" Or TextBox1.Text = "" Then
            MsgBox("Please enter a IP", MsgBoxStyle.Critical)
        Else
            My.Settings.IP = TextBox1.Text
            Me.UseWaitCursor = True
            sendcheats()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Credit.Show()
    End Sub
End Class
