
Module Module1
    Dim a As Scrap = New Scrap
    Dim con As DBaccess = New DBaccess

    Public Sub main()

        con.dbaccess_Open()

        'a.Scraping()

        Dim showform As Form = New Form
        showform.ShowDialog()
        showform.Dispose()
        con.dbaccdess_Close()


    End Sub
End Module
