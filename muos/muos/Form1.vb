Imports MaterialSkin


Public Class Form1

    Private psi As ProcessStartInfo
    Private cmd As Process
    Private Delegate Sub InvokeWithString(ByVal text As String)


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Text = DateString

        Dim skinManager As MaterialSkinManager = MaterialSkinManager.Instance


        skinManager.AddFormToManage(Me)
        skinManager.ColorScheme = New ColorScheme(Primary.LightBlue400, Primary.Purple200, Primary.Amber300, Primary.Purple300, Primary.Brown800)

        PictureBox1.Refresh()


        Timer1.Enabled = True







    End Sub

    Private Sub MaterialRadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles MaterialRadioButton1.CheckedChanged
        If MaterialRadioButton1.Checked = True Then
            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT

        Else MaterialRadioButton2.Checked = True
            SkinManager.Theme = MaterialSkinManager.Themes.DARK
        End If
    End Sub

    Private Sub MaterialRaisedButton1_Click(sender As Object, e As EventArgs) Handles MaterialRaisedButton1.Click
        Process.Start("cmd.exe")
    End Sub

    Private Sub MaterialDivider1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MaterialTabSelector1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        windows.Show()

    End Sub

    Private Sub MaterialRaisedButton3_Click(sender As Object, e As EventArgs) Handles MaterialRaisedButton3.Click
        othertool.Show()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        web.Show()

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub RichTextBox2_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox2.TextChanged

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        RichTextBox2.Text = DateTime.Now.ToString("hh:mm:ss")
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        minibrowser.Show()

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        MessageBox.Show(RichTextBox3.Text)
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Timer2.Enabled = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Timer2.Enabled = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Timer2.Enabled = False
        Label1.Text = 0.0
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label1.Text = Label1.Text + 0.01
    End Sub

    Private Sub TextBox1_TextAlignChanged(sender As Object, e As EventArgs) Handles TextBox1.TextAlignChanged

    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        ListBox1.Items.Add(TextBox1.Text)
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If (e.KeyCode = Keys.Enter) Then

            ListBox1.Items.Add(TextBox1.Text)

        End If




    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged



    End Sub

    Private Sub ListBox1_Enter(sender As Object, e As EventArgs) Handles ListBox1.Enter




    End Sub

    Private Sub MaterialFlatButton1_Click(sender As Object, e As EventArgs) Handles MaterialFlatButton1.Click

        ListBox1.Items.Clear()

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        CreateObject("sapi.spvoice").speak(RichTextBox4.Text)
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll


        Dim mclass As New System.Management.ManagementClass("WmiMonitorBrightnessMethods")
        mclass.Scope = New System.Management.ManagementScope("\\.\root\wmi")
        Dim instances As System.Management.ManagementObjectCollection = mclass.GetInstances

        For Each instance As System.Management.ManagementObject In instances
            Dim timeout As ULong = 1
            Dim brightness As UShort = CUShort(TrackBar1.Value * 10)
            Dim args As Object() = New Object() {timeout, brightness}
            instance.InvokeMethod("wmisetBrightness", args)

        Next

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub MaterialFlatButton2_Click(sender As Object, e As EventArgs) Handles MaterialFlatButton2.Click

        Try
            cmd.Kill()
        Catch ex As Exception
        End Try
        RichTextBox5.Clear()
        If TextBox2.Text.Contains(" ") Then
            psi = New ProcessStartInfo(TextBox2.Text.Split(" ")(0), TextBox2.Text.Split(" ")(1))
        Else
            psi = New ProcessStartInfo(TextBox2.Text$)
        End If
        Dim systemencoding As System.Text.Encoding
        System.Text.Encoding.GetEncoding(Globalization.CultureInfo.CurrentUICulture.TextInfo.OEMCodePage)
        With psi
            .UseShellExecute = False
            .RedirectStandardError = True
            .RedirectStandardOutput = True
            .RedirectStandardInput = True
            .CreateNoWindow = True
            .StandardOutputEncoding = systemencoding
            .StandardErrorEncoding = systemencoding
        End With
        cmd = New Process With {.StartInfo = psi, .EnableRaisingEvents = True}
        AddHandler cmd.ErrorDataReceived, AddressOf Async_Data_Received
        AddHandler cmd.OutputDataReceived, AddressOf Async_Data_Received
        cmd.Start()
        cmd.BeginOutputReadLine()
        cmd.BeginErrorReadLine()


    End Sub

    Private Sub Async_Data_Received(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        Me.Invoke(New InvokeWithString(AddressOf Sync_Output), e.Data)
    End Sub
    Private Sub Sync_Output(ByVal text As String)
        RichTextBox5.AppendText(text & Environment.NewLine)
        RichTextBox5.ScrollToCaret()
    End Sub

    Private Sub Panel1_Paint_1(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MaterialFlatButton3_Click(sender As Object, e As EventArgs) Handles MaterialFlatButton3.Click

        Dim proc = Process.GetProcessesByName(TextBox4.Text)
        For i As Integer = 0 To proc.Count - 1
            proc(i).CloseMainWindow()
        Next i


    End Sub

    Private Sub home_Click(sender As Object, e As EventArgs) Handles home.Click
        Form2.Close()
        minibrowser.Close()
        web.Close()
        windows.Close()
        othertool.Close()
        Me.Show()
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub

    Private Sub MaterialRaisedButton2_MouseHover(sender As Object, e As EventArgs) Handles MaterialRaisedButton2.MouseHover
        Dim but As New ToolTip
        but.SetToolTip(MaterialRaisedButton2, "muos made by aditya dand")
    End Sub

    Private Sub MaterialRaisedButton2_Click(sender As Object, e As EventArgs) Handles MaterialRaisedButton2.Click

    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If (e.KeyCode = Keys.Enter) Then

            Dim proc = Process.GetProcessesByName(TextBox4.Text)
            For i As Integer = 0 To proc.Count - 1
                proc(i).CloseMainWindow()
            Next i
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub
End Class
