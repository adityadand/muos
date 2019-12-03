Public Class minibrowser
    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted


    End Sub

    Private Sub minibrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WebBrowser1.Url = New Uri(Form1.ComboBox1.SelectedItem.ToString)
    End Sub
End Class