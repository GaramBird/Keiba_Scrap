'競馬スクレイピングクラス

Public Class Scrap
    Public Sub Scraping()
        'URL情報の操作オブジェクト
        Dim sURL As String
        Dim sHTML As String
        Dim objWC As System.Net.WebClient
        Dim objDOC As HtmlAgilityPack.HtmlDocument
        Dim objNodes As HtmlAgilityPack.HtmlNodeCollection
        Dim objH_name As HtmlAgilityPack.HtmlNodeCollection
        Dim objRace As HtmlAgilityPack.HtmlNodeCollection

        '取得したリスト
        Dim h_umaban As New List(Of Integer)
        Dim h_name As New List(Of String)
        Dim h_smaller_father As New List(Of String)
        Dim h_smaller_mother As New List(Of String)
        Dim h_reg_type As New List(Of String)
        Dim h_weight As New List(Of String)
        Dim h_sex As New List(Of String)
        Dim h_age As New List(Of String)
        Dim h_kinryou As New List(Of String)
        Dim h_kisyu As New List(Of String)
        Dim h_oz As New List(Of String)
        Dim h_ninki As New List(Of String)

        '変数宣言
        sURL = "https://race.netkeiba.com/?pid=race&id=c201806050811&mode=shutuba"
        objWC = New System.Net.WebClient()
        objWC.Encoding = System.Text.Encoding.GetEncoding("euc-jp")
        sHTML = objWC.DownloadString(sURL)

        objDOC = New HtmlAgilityPack.HtmlDocument()
        objDOC.LoadHtml(sHTML)

        'テーブル走査を行う。
        For Each row In objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/td[@class=""umaban""]")
            h_umaban.Add(Integer.Parse(row.InnerText))
        Next

        For Each temp_h_umaban In h_umaban
            For Each row In objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr[" & temp_h_umaban + 1 & "]")
                h_name.Add(row.SelectNodes("//td/span[@class=""h_name""]/a").Item(temp_h_umaban - 1).InnerText)

                Dim h_smaller() = Split(row.SelectNodes("//td/span[@class=""txt_smaller""]").Item(temp_h_umaban - 1).InnerText, vbLf)
                h_smaller_father.Add(h_smaller(1))
                h_smaller_mother.Add(h_smaller(2))

                h_weight.Add(row.SelectNodes("//td/strong[@class=""weight""]").Item(temp_h_umaban - 1).InnerText)

                Dim kinryo() = Split(row.SelectNodes("//td[8]").Item(temp_h_umaban - 1).InnerText, vbLf)
                h_age.Add(kinryo(0))
                h_kinryou.Add(kinryo(1))
                h_kisyu.Add(kinryo(2))

                Dim oz() = Split(row.SelectNodes("//td[9]").Item(temp_h_umaban - 1).InnerText, vbLf)
                h_oz.Add(oz(0))
                h_ninki.Add(oz(1))
            Next

        Next

    End Sub


End Class
