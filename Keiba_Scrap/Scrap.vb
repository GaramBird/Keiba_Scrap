﻿'競馬スクレイピングクラス

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
        For Each row In objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/td")
            Dim Nodes = row.SelectNodes("td/span[@class=""umaban""]").Item(0)
            h_umaban.Add(Integer.Parse(Nodes.ToString()))

        Next

        For Each temp_h_umaban In h_umaban
            For Each row In objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr[" & temp_h_umaban + 1 & "]")
                Dim h_Nodes As HtmlAgilityPack.HtmlNodeCollection = row.SelectNodes("//td/span[@class=""h_name""]/a")
                h_name.Add(h_Nodes.ToString())

                Dim Nodes As HtmlAgilityPack.HtmlNodeCollection = row.SelectNodes("//td/span[@class=""txt_smaller""]")
                Dim h_smaller = Split(Nodes.ToString(), "<br>")
                h_smaller_father.Add(h_smaller(0))
                h_smaller_mother.Add(h_smaller(1))
            Next

        Next

    End Sub


End Class
