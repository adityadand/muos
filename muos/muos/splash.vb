Public Class splash
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        RectangleShape2.Width += 40

        If (RectangleShape2.Width > 380) Then
            Timer1.Stop()
            Me.Hide()
            Form1.Show()
        End If



    End Sub

    Private Sub splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
    End Sub
End Class