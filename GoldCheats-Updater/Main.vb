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
            Directory.CreateDirectory(Application.StartupPath & "/shn")
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
            Dim lvi2 As ListViewItem
            Dim di As New DirectoryInfo(Application.StartupPath & "/json")
            Dim di2 As New DirectoryInfo(Application.StartupPath & "/shn")
            Dim myIcon As Icon

            ' ext/icon lookup
            Dim exts As New List(Of String)
            ImageList1.Images.Clear()


            For Each fi As FileInfo In di.EnumerateFiles("*.json*")
                Try
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
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                End Try

            Next

            For Each fi2 As FileInfo In di2.EnumerateFiles("*.shn*")
                Try
                    lvi2 = New ListViewItem
                    lvi2.Text = fi2.Name
                    lvi2.SubItems.Add(Path.GetDirectoryName(fi2.FullName))

                    lvi2.SubItems.Add(((fi2.Length / 1024)).ToString("0.00"))
                    lvi2.SubItems.Add(fi2.CreationTime.ToShortDateString)

                    If exts.Contains(fi2.Extension) = False Then
                        myIcon = Icon.ExtractAssociatedIcon(fi2.FullName)
                        ImageList1.Images.Add(fi2.Extension, myIcon)
                        exts.Add(fi2.Extension)
                    End If

                    lvi2.ImageKey = fi2.Extension
                    ListView1.Items.Add(lvi2)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                End Try

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
        extract(Application.StartupPath & "/GoldCheats.zip", Application.StartupPath & "/GoldCheats")
        Me.UseWaitCursor = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If File.Exists(Application.StartupPath & "/GoldCheats.zip") Then
                File.Delete(Application.StartupPath & "/GoldCheats.zip")
            End If

            'ProgressBar1.Visible = True
            Me.UseWaitCursor = True
            'Label1.Visible = True
            Dim client As WebClient = New WebClient
            AddHandler client.DownloadProgressChanged, AddressOf client_ProgressChanged
            AddHandler client.DownloadFileCompleted, AddressOf client_DownloadCompleted
            client.DownloadFileAsync(New Uri("https://github.com/GoldHEN/GoldHEN_Cheat_Repository/archive/refs/heads/main.zip"), Application.StartupPath & "/GoldCheats.zip")
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

            Dim di As New DirectoryInfo(Application.StartupPath & "/GoldCheats/GoldHEN_Cheat_Repository-main/json")
            Dim di2 As New DirectoryInfo(Application.StartupPath & "/GoldCheats/GoldHEN_Cheat_Repository-main/shn")

            For Each fi As FileInfo In di.EnumerateFiles("*.json")
                System.IO.File.Copy(fi.FullName, Application.StartupPath & "\json\" & fi.Name, True)
            Next


            For Each fi2 As FileInfo In di2.EnumerateFiles("*.shn")
                System.IO.File.Copy(fi2.FullName, Application.StartupPath & "\shn\" & fi2.Name, True)
            Next

            Directory.Delete(Application.StartupPath & "/GoldCheats", True)
            System.IO.File.Delete(Application.StartupPath & "/GoldCheats.zip")
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
            Dim di2 As New DirectoryInfo(Application.StartupPath & "/shn")

            For Each fi As FileInfo In di.EnumerateFiles("*.json")
                System.IO.File.Delete(Application.StartupPath & "\json\" & fi.Name)
            Next

            For Each fi2 As FileInfo In di.EnumerateFiles("*.shn")
                System.IO.File.Delete(Application.StartupPath & "\shn\" & fi2.Name)
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
            Dim di2 As New DirectoryInfo(Application.StartupPath & "/shn")

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

            For Each fi2 As FileInfo In di2.EnumerateFiles("*.shn")
                If File.Exists("ftp://" & TextBox1.Text & ":2121/data/GoldHEN/cheats/shn/" & fi2.Name) And overwrite = True Then
                    ftp.UploadFile(fi2.FullName, "ftp://" & TextBox1.Text & ":2121/data/GoldHEN/cheats/shn/" & fi2.Name)
                ElseIf File.Exists("ftp://" & TextBox1.Text & ":2121/data/GoldHEN/cheats/shn/" & fi2.Name) And overwrite = False Then

                Else
                    ftp.UploadFile(fi2.FullName, "ftp://" & TextBox1.Text & ":2121/data/GoldHEN/cheats/shn/" & fi2.Name)
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
