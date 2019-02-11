Public Class StartClass
    Shared Sub Main()
        Dim formctrl As ShowForm = New ShowForm
        ShowForm.ShowFormInstance = formctrl
        formctrl.ShowDialog()
    End Sub

End Class
