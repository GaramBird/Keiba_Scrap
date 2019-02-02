'競馬スクレイピングクラス

Public Class Scrap
    Private Sub Scraping()
        'URL情報の操作オブジェクト
        Dim sURL As String
        Dim sHTML As String
        Dim objWC As System.Net.WebClient
        Dim objDOC As HtmlAgilityPack.HtmlDocument
        Dim objNodes As HtmlAgilityPack.HtmlNodeCollection
        Dim objH_name As HtmlAgilityPack.HtmlNodeCollection
        Dim objRace As HtmlAgilityPack.HtmlNodeCollection

        '取得したリスト
        Dim race As New List(Of String)
        Dim h_name As New List(Of String)
        Dim itisoumae As New List(Of String)
        Dim nisoumae As New List(Of String)
        Dim sansoumae As New List(Of String)
        Dim yonsoumae As New List(Of String)
        Dim tosoumae As New List(Of String)


        '変数宣言
        sURL = "https://race.netkeiba.com/?pid=race&id=c201806050811&mode=shutuba"
        objWC = New System.Net.WebClient()
        objWC.Encoding = System.Text.Encoding.GetEncoding("euc-jp")
        sHTML = objWC.DownloadString(sURL)

        objDOC = New HtmlAgilityPack.HtmlDocument()
        objDOC.LoadHtml(sHTML)

        objNodes = objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/td")
        objH_name = objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/td[@class=""txt_l""]/span[@class=""h_name""]/a")
        objRace = objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/td[@class=""txt_l""]/span[class=""race_name""]")

        '取得するデータテーブルを定義する。

        Dim dt As DataTable = New DataTable
        dt.Columns.Add("馬名")


        For Each row In objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr")
            Dim Nodes = row.SelectNodes("td/span[@class=""h_name""]")
            Dim h_namea = Nodes
            dt.Rows.Add(h_name)
        Next


        For Each row In objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr[2]")
            Dim Nodes = row.SelectNodes("td/span")
        Next
        dt.Dispose()

    End Sub


End Class
