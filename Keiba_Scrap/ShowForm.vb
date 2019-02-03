Public Class ShowForm
    Dim a As Scrap = New Scrap
    Private Sub Fromload(sender As Object, e As EventArgs) Handles MyBase.Load
        a.Scraping()
    End Sub
End Class
