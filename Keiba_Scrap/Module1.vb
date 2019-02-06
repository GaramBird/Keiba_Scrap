
Module Module1
    Dim a As Scrap = New Scrap
    Dim con As DBaccess = New DBaccess
    Dim showform As Form = New Form

    Public Sub main()

        con.dbaccess_Open()
        showform.ShowDialog()

        a.Scraping()


        showform.Dispose()
        con.dbaccdess_Close()


    End Sub
End Module
