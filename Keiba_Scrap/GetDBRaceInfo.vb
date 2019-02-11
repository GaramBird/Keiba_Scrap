'競馬スクレイピングクラス
Imports System.Text.RegularExpressions

Public Class GetDBRaceInfo

    Public Function NULLAgoRaceInfo(ByRef racename As List(Of String), ByRef nagasa As List(Of Integer), ByRef kaisaibi As List(Of DateTime), ByRef basho As List(Of String), ByRef baba As List(Of String), ByRef baba_status As List(Of String), ByRef wether As List(Of String), ByRef juni As List(Of Integer), ByRef time As List(Of TimeSpan)) As Boolean
        racename.Add(Nothing)
        baba.Add(Nothing)
        nagasa.Add(Nothing)
        wether.Add(Nothing)
        baba_status.Add(Nothing)
        kaisaibi.Add(Nothing)
        basho.Add(Nothing)
        juni.Add(Nothing)
        time.Add(Nothing)

        Return True
    End Function



    Public Function GetAgoRaceInfo(ByVal name As String, ByVal sURL As String, ByRef racename As List(Of String), ByRef nagasa As List(Of Integer), ByRef kaisaibi As List(Of DateTime), ByRef basho As List(Of String), ByRef baba As List(Of String), ByRef baba_status As List(Of String), ByRef wether As List(Of String), ByRef juni As List(Of Integer), ByRef time As List(Of TimeSpan)) As Boolean
        '正規表現
        Dim reg_int As New Regex("[^0-9]")  '数字のみを抽出に使用

        'URL情報の操作オブジェクト
        Dim objWC As System.Net.WebClient = New System.Net.WebClient()
        objWC.Encoding = System.Text.Encoding.GetEncoding("euc-jp")

        'URL捜査準備
        Dim race_html = objWC.DownloadString(sURL)
        Dim race_objDOC = New HtmlAgilityPack.HtmlDocument()
        race_objDOC.LoadHtml(race_html)


        'レース名情報を取得する。
        Dim raceinfo = race_objDOC.DocumentNode.SelectNodes("//div[@class=""data_intro""]").Item(0)
        Dim tempracename = raceinfo.SelectNodes("//dl[@class=""racedata fc""]/dd/h1").Item(0).InnerText
        If tempracename.IndexOf("回") >= 0 Then
            racename.Add(tempracename.Substring(tempracename.IndexOf("回") + 1))
        Else
            racename.Add(tempracename)
        End If


        Dim babainfo() = Split(raceinfo.SelectNodes("//dl[@class=""racedata fc""]/dd/diary_snap_cut/span").Item(0).InnerText.Replace("&nbsp;", "").Trim(), "/")
        baba.Add(babainfo(0).Substring(0, 1))
        nagasa.Add(reg_int.Replace(babainfo(0).Substring(0, babainfo(0).IndexOf("m")), ""))
        wether.Add(babainfo(1).Substring(babainfo(1).IndexOf(":") + 1))
        baba_status.Add(babainfo(2).Substring(babainfo(2).IndexOf(":") + 1))

        Dim bashoinfo = raceinfo.SelectNodes("//p[@class=""smalltxt""]").Item(0).InnerText.Trim()
        Dim nen() = Split(bashoinfo, "年")
        Dim gatu() = Split(nen(1), "月")
        Dim niti() = Split(gatu(1), "日")
        kaisaibi.Add(DateTime.Parse((nen(0) & "/" & gatu(0) & "/" & niti(0))))
        basho.Add(bashoinfo.Substring(bashoinfo.IndexOf("回") + 1, bashoinfo.IndexOf("日目") - 2 - bashoinfo.IndexOf("回")))

        'レース結果情報を表から取得する。
        '馬番を取得して出馬表から取得するナンバリングを取得する。
        Dim h_name As New List(Of String)
        Dim sanshouNo As Integer
        For Each row In race_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common""]/tr/td[4]/a")
            h_name.Add(row.InnerText)
        Next
        sanshouNo = h_name.IndexOf(name)

        '順位を取得
        If reg_int.Replace(race_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common""]/tr/td[1]").Item(sanshouNo).InnerText, "") <> "" Then
            juni.Add(Integer.Parse(race_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common""]/tr/td[1]").Item(sanshouNo).InnerText))
        Else
            juni.Add(99)
        End If

        'レースタイムを取得する
        If race_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common""]/tr/td[8]").Item(sanshouNo).InnerText <> "" Then
            Dim minits = Split(race_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common""]/tr/td[8]").Item(sanshouNo).InnerText, ":")
            Dim seconds = Split(minits(1), ".")
            Dim miliseconds = seconds(1) * 100
            Dim ts = New TimeSpan(0, 0, minits(0), seconds(0), miliseconds)
            time.Add(ts)
        Else
            Dim ts2 = New TimeSpan(0, 0, 0, 0, 0)
            time.Add(ts2)
        End If

        Return True
    End Function
End Class
