'競馬スクレイピングクラス
Imports System.Text.RegularExpressions

Public Class SyutubahyouScrap
    Public Function Scraping(syutubahyouurl) As Boolean
        '正規表現
        Dim reg_int As New Regex("[^0-9]")  '数字のみを抽出に使用
        'インスタンス作成
        Dim getinfo As GetDBRaceInfo = New GetDBRaceInfo

        '取得したリスト
        Dim h_umaban As New List(Of Integer)
        Dim h_name As New List(Of String)
        Dim h_smaller_father As New List(Of String)
        Dim h_smaller_mother As New List(Of String)
        Dim h_reg_type As New List(Of String)
        Dim h_weight As New List(Of Integer)
        Dim h_sex As New List(Of String)
        Dim h_age As New List(Of Integer)
        Dim h_keiro As New List(Of String)
        Dim h_kinryou As New List(Of Integer)
        Dim h_kisyu As New List(Of String)
        Dim h_oz As New List(Of Double)
        Dim h_ninki As New List(Of Integer)

        '1走前情報変数コレクション
        Dim itisoumae_count As Integer = 0
        Dim iti_racename As New List(Of String)
        Dim iti_nagasa As New List(Of Integer)
        Dim iti_kaisaibi As New List(Of DateTime)
        Dim iti_basho As New List(Of String)
        Dim iti_baba As New List(Of String)
        Dim iti_baba_status As New List(Of String)
        Dim iti_wether As New List(Of String)
        Dim iti_juni As New List(Of Integer)
        Dim iti_time As New List(Of TimeSpan)
        Dim iti_weight As New List(Of Integer)
        Dim iti_noboriharon As New List(Of TimeSpan)


        '2走前情報変数コレクション
        Dim nisoumae_count As Integer = 0
        Dim ni_racename As New List(Of String)
        Dim ni_nagasa As New List(Of Integer)
        Dim ni_kaisaibi As New List(Of DateTime)
        Dim ni_basho As New List(Of String)
        Dim ni_baba As New List(Of String)
        Dim ni_baba_status As New List(Of String)
        Dim ni_wether As New List(Of String)
        Dim ni_juni As New List(Of Integer)
        Dim ni_time As New List(Of TimeSpan)
        Dim ni_weight As New List(Of Integer)
        Dim ni_noboriharon As New List(Of TimeSpan)

        '3走前情報変数コレクション
        Dim sansoumae_count As Integer = 0
        Dim san_racename As New List(Of String)
        Dim san_nagasa As New List(Of Integer)
        Dim san_kaisaibi As New List(Of DateTime)
        Dim san_basho As New List(Of String)
        Dim san_baba As New List(Of String)
        Dim san_baba_status As New List(Of String)
        Dim san_wether As New List(Of String)
        Dim san_juni As New List(Of Integer)
        Dim san_time As New List(Of TimeSpan)
        Dim san_weight As New List(Of Integer)
        Dim san_noboriharon As New List(Of TimeSpan)

        '4走前情報変数コレクション
        Dim yonsoumae_count As Integer = 0
        Dim yon_racename As New List(Of String)
        Dim yon_nagasa As New List(Of Integer)
        Dim yon_kaisaibi As New List(Of DateTime)
        Dim yon_basho As New List(Of String)
        Dim yon_baba As New List(Of String)
        Dim yon_baba_status As New List(Of String)
        Dim yon_wether As New List(Of String)
        Dim yon_juni As New List(Of Integer)
        Dim yon_time As New List(Of TimeSpan)
        Dim yon_weight As New List(Of Integer)
        Dim yon_noboriharon As New List(Of TimeSpan)

        '5走前情報変数コレクション
        Dim gosoumae_count As Integer = 0
        Dim go_racename As New List(Of String)
        Dim go_nagasa As New List(Of Integer)
        Dim go_kaisaibi As New List(Of DateTime)
        Dim go_basho As New List(Of String)
        Dim go_baba As New List(Of String)
        Dim go_baba_status As New List(Of String)
        Dim go_wether As New List(Of String)
        Dim go_juni As New List(Of Integer)
        Dim go_time As New List(Of TimeSpan)
        Dim go_weight As New List(Of Integer)
        Dim go_noboriharon As New List(Of TimeSpan)

        'URL情報の操作オブジェクト
        Dim objWC As System.Net.WebClient = New System.Net.WebClient()
        objWC.Encoding = System.Text.Encoding.GetEncoding("euc-jp")

        'URL捜査準備
        Dim syutubahyou_url As String = syutubahyouurl
        Dim syutubahyou_html As String
        Dim syutubahyou_objDOC As HtmlAgilityPack.HtmlDocument

        'URLの取得検証
        Try
            ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
            ShowForm.ShowFormInstance.JikkouMethodText = "URLの取得検証" '実行中の処理を記載する。
            syutubahyou_html = objWC.DownloadString(syutubahyou_url)
            syutubahyou_objDOC = New HtmlAgilityPack.HtmlDocument()
            syutubahyou_objDOC.LoadHtml(syutubahyou_html)
        Catch ex As Exception
            MessageBox.Show("URLの取得に失敗しました。")
            Return False
        End Try


        '取得したURLのレース名を取得検証する。
        Try
            ShowForm.ShowFormInstance.JikkouMethodText = "レース名の取得検証" '実行中の処理を記載する。
            syutubahyou_objDOC.DocumentNode.SelectNodes("//div[@class=""data_intro""]/dl/dd/h1").Item(0).InnerText.Replace("&nbsp;", "")
        Catch ex As Exception
            If MessageBox.Show("レース名の取得に失敗しました。URLを確認してください。" & vbCrLf & "netkeiba.comを開きますか？", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                System.Diagnostics.Process.Start("http://www.netkeiba.com/?rf=logo")
            End If
            Return False
        End Try
        '予想レースの情報を取得する

        Dim racebashoinfo = syutubahyou_objDOC.DocumentNode.SelectNodes("//div[@class=""race_otherdata""]/p").Item(0).InnerText
        Dim racebasho = racebashoinfo.Substring(racebashoinfo.IndexOf("回") + 1, 2)
        Dim racename = syutubahyou_objDOC.DocumentNode.SelectNodes("//div[@class=""data_intro""]/dl/dd/h1").Item(0).InnerText.Replace("&nbsp;", "")
        Dim racebabainfo = syutubahyou_objDOC.DocumentNode.SelectNodes("//div[@class=""data_intro""]/dl/dd/p").Item(0).InnerText
        Dim temp_racebaba = racebabainfo.Substring(0, 1)
        Dim racebaba As String
        Select Case temp_racebaba
            Case "芝"
                racebaba = "芝"
            Case "ダ"
                racebaba = "ダート"
            Case "障"
                Select Case temp_racebaba.Substring(1, 1)
                    Case "芝"
                        racebaba = "障害芝"
                    Case "ダ"
                        racebaba = "障害ダート"
                End Select
        End Select

        Dim racenagasa As Integer = reg_int.Replace(racebabainfo.Substring(1), "")
            Dim racemawari = racebabainfo.Substring(racebabainfo.IndexOf("(") + 1, 1) & "回り"
        Dim racewetherinfo() = Split(syutubahyou_objDOC.DocumentNode.SelectNodes("//div[@class=""data_intro""]/dl/dd/p").Item(1).InnerText, "：")
        Dim racewether As String = "未発表"
        Dim racebabastutas = "未発表"
        Dim racehassou As TimeSpan = New TimeSpan(0, 0, 0)
        If racewetherinfo(1).IndexOf("&nbsp;") <> 0 Then
            racewether = racewetherinfo(1).Substring(0, racewetherinfo(1).IndexOf("&nbsp;"))
        End If
        If racewetherinfo(2).IndexOf("&nbsp;") <> 0 Then
            racebabastutas = racewetherinfo(2).Substring(0, racewetherinfo(2).IndexOf("&nbsp;"))
        End If
        If racewetherinfo(3).Length <> 0 Then
            racehassou = New TimeSpan(Split(racewetherinfo(3), ":")(0), Split(racewetherinfo(3), ":")(1), 0)
        End If

#Region "予想レース情報データテーブル"
        '取得したスクレイピングデータをデータグリッドビューのデータソースへとセットする。
        Dim dtRace = New DataTable("予想レース情報データテーブル")

        dtRace.Columns.Add("開催地", GetType(String))
        dtRace.Columns.Add("レース名", GetType(String))
        dtRace.Columns.Add("天気", GetType(String))
        dtRace.Columns.Add("馬場", GetType(String))
        dtRace.Columns.Add("馬場状況", GetType(String))
        dtRace.Columns.Add("長さ", GetType(Integer))
        dtRace.Columns.Add("回り", GetType(String))
        dtRace.Columns.Add("発走", GetType(TimeSpan))

        dtRace.Rows.Add(racebasho, racename, racewether, racebaba, racebabastutas, racenagasa, racemawari, racehassou)
#End Region


#Region "グリッドビュー設定"
        ShowForm.ShowFormInstance.YosouRaceDataGridSet = dtRace
        ShowForm.dgvYosouRace.Refresh()
#End Region



        '予想紙の有無によって参照するTDインデックスを変化させる。
        Dim colCount As Integer = 1
        Dim colcount_min As Integer
        Dim colCount_max As Integer
        Dim colyosousiCount As Integer
        For Each col In syutubahyou_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/th")
            If col.InnerText.IndexOf("あなた") >= 0 Then
                colcount_min = colCount
            End If
            If col.InnerText.IndexOf("馬名") >= 0 Then
                colCount_max = colCount
            End If
            colCount += 1
        Next
        colyosousiCount = colCount_max - colcount_min - 1



        '馬番を取得して出馬表から取得するナンバリングを取得する。
        ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
        ShowForm.ShowFormInstance.JikkouMethodText = "馬番を取得" '実行中の処理を記載する。

        '取得したURLのレースの馬番を取得検証する。
        Try
            ShowForm.ShowFormInstance.JikkouMethodText = "レース名の取得検証" '実行中の処理を記載する。
            syutubahyou_objDOC.DocumentNode.SelectNodes("//div[@class=""data_intro""]/dl/dd/h1").Item(0).InnerText.Replace("&nbsp;", "")
            For Each row In syutubahyou_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/td[@class=""umaban""]")
            Next
        Catch ex As Exception
            'MessageBox.Show("馬番の取得に失敗しました。馬番を含まない出馬表データを取得します。")
            If Scraping_Numberless(racename, syutubahyouurl) Then
                Return True
            Else
                Return False
            End If
        End Try


        For Each row In syutubahyou_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/td[@class=""umaban""]")
            h_umaban.Add(Integer.Parse(row.InnerText))
        Next

        'テーブル取得を行う。
        For Each temp_h_umaban In h_umaban
            ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。

            For Each row In syutubahyou_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr[" & temp_h_umaban + 1 & "]")
                Dim sansho_umaban As Integer = temp_h_umaban - 1    'テーブル列位置が相対変化するためインデックスを1つ下げる。

                '馬名を取得する。
                h_name.Add(row.SelectNodes("//td/span[@class=""h_name""]/a").Item(sansho_umaban).InnerText)

                '馬情報を取得する
                Dim h_smaller() = Split(row.SelectNodes("//td/span[@class=""txt_smaller""]").Item(sansho_umaban).InnerText, vbLf,)
                h_smaller_father.Add(h_smaller(1))
                h_smaller_mother.Add(h_smaller(2))

                Select Case Split(row.SelectNodes("//td[" & 4 + colyosousiCount & "]/img").Item(sansho_umaban).OuterHtml, "horse_race_type")(1).Substring(0, 2)
                    Case "00"   '逃げ
                        h_reg_type.Add("0:データなし")
                    Case "01"   '逃げ
                        h_reg_type.Add("1:逃げ")
                    Case "02"   '先行
                        h_reg_type.Add("2:先行")
                    Case "03"   '差し
                        h_reg_type.Add("3:差し")
                    Case "04"   '追込
                        h_reg_type.Add("4:追込")
                End Select

                '体重を取得（情報がNothingを追加）
                Try
                    h_weight.Add(Split(row.SelectNodes("//td/strong[@class=""weight""]").Item(sansho_umaban).InnerText, "(")(0))    '前走から計算するので±値は考慮しない事にする。
                Catch ex As Exception
                    h_weight.Add(Nothing)
                End Try

                '斤量（性別、歳、経路、斤量、機種）を取得
                Dim kinryo() = Split(row.SelectNodes("//td[" & 5 + colyosousiCount & "]").Item(sansho_umaban).InnerText, vbLf)
                h_sex.Add(kinryo(0).Substring(0, 1))
                h_age.Add(reg_int.Replace(kinryo(0), ""))
                h_keiro.Add(Split(kinryo(0), "/")(1))
                h_kinryou.Add(kinryo(1))
                h_kisyu.Add(kinryo(2))

                'オッズ、人気を取得
                Dim oz() = Split(row.SelectNodes("//td[" & 6 + colyosousiCount & "]").Item(sansho_umaban).InnerText, vbLf)
                h_oz.Add(oz(1))
                h_ninki.Add(reg_int.Replace(oz(2), ""))

                '1走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = h_umaban.Count & "頭中：" & temp_h_umaban & "." & h_name(sansho_umaban) & "1走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 7 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '1走前レース情報をリンクから取得する。
                    Dim itisoumae_info = row.SelectNodes("//td[" & 7 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - itisoumae_count)
                    Dim itisoumae_race_url = itisoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), itisoumae_race_url, iti_racename, iti_nagasa, iti_kaisaibi, iti_basho, iti_baba, iti_baba_status, iti_wether, iti_juni, iti_time, iti_weight)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    itisoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(iti_racename, iti_nagasa, iti_kaisaibi, iti_basho, iti_baba, iti_baba_status, iti_wether, iti_juni, iti_time, iti_weight)
                End If


                '2走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = h_umaban.Count & "頭中：" & temp_h_umaban & "." & h_name(sansho_umaban) & "2走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 8 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '2走前レース情報をリンクから取得する。
                    Dim nisoumae_info = row.SelectNodes("//td[" & 8 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - nisoumae_count)
                    Dim nisoumae_race_url = nisoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), nisoumae_race_url, ni_racename, ni_nagasa, ni_kaisaibi, ni_basho, ni_baba, ni_baba_status, ni_wether, ni_juni, ni_time, ni_weight)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    nisoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(ni_racename, ni_nagasa, ni_kaisaibi, ni_basho, ni_baba, ni_baba_status, ni_wether, ni_juni, ni_time, ni_weight)
                End If


                '3走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = h_umaban.Count & "頭中：" & temp_h_umaban & "." & h_name(sansho_umaban) & "3走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 9 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '3走前レース情報をリンクから取得する。
                    Dim sansoumae_info = row.SelectNodes("//td[" & 9 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - sansoumae_count)
                    Dim sansoumae_race_url = sansoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), sansoumae_race_url, san_racename, san_nagasa, san_kaisaibi, san_basho, san_baba, san_baba_status, san_wether, san_juni, san_time, san_weight)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    sansoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(san_racename, san_nagasa, san_kaisaibi, san_basho, san_baba, san_baba_status, san_wether, san_juni, san_time, san_weight)
                End If


                '4走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = h_umaban.Count & "頭中：" & temp_h_umaban & "." & h_name(sansho_umaban) & "4走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 10 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '4走前レース情報をリンクから取得する。
                    Dim yonsoumae_info = row.SelectNodes("//td[" & 10 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - yonsoumae_count)
                    Dim yonsoumae_race_url = yonsoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), yonsoumae_race_url, yon_racename, yon_nagasa, yon_kaisaibi, yon_basho, yon_baba, yon_baba_status, yon_wether, yon_juni, yon_time, yon_weight)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    yonsoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(yon_racename, yon_nagasa, yon_kaisaibi, yon_basho, yon_baba, yon_baba_status, yon_wether, yon_juni, yon_time, yon_weight)
                End If


                '5走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = h_umaban.Count & "頭中：" & temp_h_umaban & "." & h_name(sansho_umaban) & "5走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 11 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '5走前レース情報をリンクから取得する。
                    Dim gosoumae_info = row.SelectNodes("//td[" & 11 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - gosoumae_count)
                    Dim gosoumae_race_url = gosoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), gosoumae_race_url, go_racename, go_nagasa, go_kaisaibi, go_basho, go_baba, go_baba_status, go_wether, go_juni, go_time, go_weight)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    gosoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(go_racename, go_nagasa, go_kaisaibi, go_basho, go_baba, go_baba_status, go_wether, go_juni, go_time, go_weight)
                End If

            Next
        Next


#Region "スクレイピングデータテーブル"
        '取得したスクレイピングデータをデータグリッドビューのデータソースへとセットする。
        Dim dt = New DataTable("出馬表データテーブル")

        dt.Columns.Add("馬番", GetType(Integer))
        dt.Columns.Add("馬名", GetType(String))
        dt.Columns.Add("父", GetType(String))
        dt.Columns.Add("母", GetType(String))
        dt.Columns.Add("脚質", GetType(String))
        dt.Columns.Add("体重", GetType(String))
        dt.Columns.Add("性", GetType(String))
        dt.Columns.Add("歳", GetType(Integer))
        dt.Columns.Add("毛色", GetType(String))
        dt.Columns.Add("斤量", GetType(String))
        dt.Columns.Add("騎手", GetType(String))
        dt.Columns.Add("オッズ", GetType(Double))
        dt.Columns.Add("人気", GetType(Integer))
        dt.Columns.Add("1走前レース名", GetType(String))
        dt.Columns.Add("1走前長さ", GetType(Integer))
        dt.Columns.Add("1走前開催日", GetType(DateTime))
        dt.Columns.Add("1走前場所", GetType(String))
        dt.Columns.Add("1走前馬場", GetType(String))
        dt.Columns.Add("1走前馬場状況", GetType(String))
        dt.Columns.Add("1走前天気", GetType(String))
        dt.Columns.Add("1走前順位", GetType(Integer))
        dt.Columns.Add("1走前タイム", GetType(TimeSpan))
        dt.Columns.Add("1走前体重", GetType(Integer))
        dt.Columns.Add("2走前レース名", GetType(String))
        dt.Columns.Add("2走前長さ", GetType(Integer))
        dt.Columns.Add("2走前開催日", GetType(DateTime))
        dt.Columns.Add("2走前場所", GetType(String))
        dt.Columns.Add("2走前馬場", GetType(String))
        dt.Columns.Add("2走前馬場状況", GetType(String))
        dt.Columns.Add("2走前天気", GetType(String))
        dt.Columns.Add("2走前順位", GetType(Integer))
        dt.Columns.Add("2走前タイム", GetType(TimeSpan))
        dt.Columns.Add("2走前体重", GetType(Integer))
        dt.Columns.Add("3走前レース名", GetType(String))
        dt.Columns.Add("3走前長さ", GetType(Integer))
        dt.Columns.Add("3走前開催日", GetType(DateTime))
        dt.Columns.Add("3走前場所", GetType(String))
        dt.Columns.Add("3走前馬場", GetType(String))
        dt.Columns.Add("3走前馬場状況", GetType(String))
        dt.Columns.Add("3走前天気", GetType(String))
        dt.Columns.Add("3走前順位", GetType(Integer))
        dt.Columns.Add("3走前タイム", GetType(TimeSpan))
        dt.Columns.Add("3走前体重", GetType(Integer))
        dt.Columns.Add("4走前レース名", GetType(String))
        dt.Columns.Add("4走前長さ", GetType(Integer))
        dt.Columns.Add("4走前開催日", GetType(DateTime))
        dt.Columns.Add("4走前場所", GetType(String))
        dt.Columns.Add("4走前馬場", GetType(String))
        dt.Columns.Add("4走前馬場状況", GetType(String))
        dt.Columns.Add("4走前天気", GetType(String))
        dt.Columns.Add("4走前順位", GetType(Integer))
        dt.Columns.Add("4走前タイム", GetType(TimeSpan))
        dt.Columns.Add("4走前体重", GetType(Integer))
        dt.Columns.Add("5走前レース名", GetType(String))
        dt.Columns.Add("5走前長さ", GetType(Integer))
        dt.Columns.Add("5走前開催日", GetType(DateTime))
        dt.Columns.Add("5走前場所", GetType(String))
        dt.Columns.Add("5走前馬場", GetType(String))
        dt.Columns.Add("5走前馬場状況", GetType(String))
        dt.Columns.Add("5走前天気", GetType(String))
        dt.Columns.Add("5走前順位", GetType(Integer))
        dt.Columns.Add("5走前タイム", GetType(TimeSpan))
        dt.Columns.Add("5走前体重", GetType(Integer))

        For i = 0 To h_umaban.Count - 1
            dt.Rows.Add(h_umaban(i), h_name(i), h_smaller_father(i), h_smaller_mother(i), h_reg_type(i), h_weight(i), h_sex(i), h_age(i), h_keiro(i), h_kinryou(i), h_kisyu(i), h_oz(i), h_ninki(i),
                        iti_racename(i), iti_nagasa(i), iti_kaisaibi(i), iti_basho(i), iti_baba(i), iti_baba_status(i), iti_wether(i), iti_juni(i), iti_time(i), iti_weight(i),
                        ni_racename(i), ni_nagasa(i), ni_kaisaibi(i), ni_basho(i), ni_baba(i), ni_baba_status(i), ni_wether(i), ni_juni(i), ni_time(i), ni_weight(i),
                        san_racename(i), san_nagasa(i), san_kaisaibi(i), san_basho(i), san_baba(i), san_baba_status(i), san_wether(i), san_juni(i), san_time(i), san_weight(i),
                        yon_racename(i), yon_nagasa(i), yon_kaisaibi(i), yon_basho(i), yon_baba(i), yon_baba_status(i), yon_wether(i), yon_juni(i), yon_time(i), yon_weight(i),
                        go_racename(i), go_nagasa(i), go_kaisaibi(i), go_basho(i), go_baba(i), go_baba_status(i), go_wether(i), go_juni(i), go_time(i), go_weight(i))
        Next

#End Region


#Region "グリッドビュー設定"
        ShowForm.ShowFormInstance.SyutubahyouDataGridSet = dt
#End Region

        ShowForm.ShowFormInstance.JikkouMethodText = "「" & racename & "」情報を表示しています。" '実行中の処理を記載する。
        ShowForm.ShowFormInstance.JikkouBarValue = ShowForm.jikkouBar.Maximum    '実行ステータスバーを加算する。
        MessageBox.Show("出馬表のスクレイピングが完了しました。")


        Return True
    End Function


    Public Function Scraping_Numberless(ByVal racename As String, ByVal syutubahyouurl As String) As Boolean
        '正規表現
        Dim reg_int As New Regex("[^0-9]")  '数字のみを抽出に使用
        'インスタンス作成
        Dim getinfo As GetDBRaceInfo = New GetDBRaceInfo

        '取得したリスト
        Dim number As New List(Of Integer)
        Dim rowcount As Integer = 1
        Dim h_name As New List(Of String)
        Dim h_smaller_father As New List(Of String)
        Dim h_smaller_mother As New List(Of String)
        Dim h_reg_type As New List(Of String)
        Dim h_weight As New List(Of Integer)
        Dim h_sex As New List(Of String)
        Dim h_age As New List(Of Integer)
        Dim h_keiro As New List(Of String)
        Dim h_kinryou As New List(Of Integer)
        Dim h_kisyu As New List(Of String)
        Dim h_oz As New List(Of Double)
        Dim h_ninki As New List(Of Integer)

        '1走前情報変数コレクション
        Dim itisoumae_count As Integer = 0
        Dim iti_racename As New List(Of String)
        Dim iti_nagasa As New List(Of Integer)
        Dim iti_kaisaibi As New List(Of DateTime)
        Dim iti_basho As New List(Of String)
        Dim iti_baba As New List(Of String)
        Dim iti_baba_status As New List(Of String)
        Dim iti_wether As New List(Of String)
        Dim iti_juni As New List(Of Integer)
        Dim iti_time As New List(Of TimeSpan)
        Dim iti_weight As New List(Of Integer)
        Dim iti_noboriharon As New List(Of TimeSpan)


        '2走前情報変数コレクション
        Dim nisoumae_count As Integer = 0
        Dim ni_racename As New List(Of String)
        Dim ni_nagasa As New List(Of Integer)
        Dim ni_kaisaibi As New List(Of DateTime)
        Dim ni_basho As New List(Of String)
        Dim ni_baba As New List(Of String)
        Dim ni_baba_status As New List(Of String)
        Dim ni_wether As New List(Of String)
        Dim ni_juni As New List(Of Integer)
        Dim ni_time As New List(Of TimeSpan)
        Dim ni_weight As New List(Of Integer)
        Dim ni_noboriharon As New List(Of TimeSpan)

        '3走前情報変数コレクション
        Dim sansoumae_count As Integer = 0
        Dim san_racename As New List(Of String)
        Dim san_nagasa As New List(Of Integer)
        Dim san_kaisaibi As New List(Of DateTime)
        Dim san_basho As New List(Of String)
        Dim san_baba As New List(Of String)
        Dim san_baba_status As New List(Of String)
        Dim san_wether As New List(Of String)
        Dim san_juni As New List(Of Integer)
        Dim san_time As New List(Of TimeSpan)
        Dim san_weight As New List(Of Integer)
        Dim san_noboriharon As New List(Of TimeSpan)

        '4走前情報変数コレクション
        Dim yonsoumae_count As Integer = 0
        Dim yon_racename As New List(Of String)
        Dim yon_nagasa As New List(Of Integer)
        Dim yon_kaisaibi As New List(Of DateTime)
        Dim yon_basho As New List(Of String)
        Dim yon_baba As New List(Of String)
        Dim yon_baba_status As New List(Of String)
        Dim yon_wether As New List(Of String)
        Dim yon_juni As New List(Of Integer)
        Dim yon_time As New List(Of TimeSpan)
        Dim yon_weight As New List(Of Integer)
        Dim yon_noboriharon As New List(Of TimeSpan)

        '5走前情報変数コレクション
        Dim gosoumae_count As Integer = 0
        Dim go_racename As New List(Of String)
        Dim go_nagasa As New List(Of Integer)
        Dim go_kaisaibi As New List(Of DateTime)
        Dim go_basho As New List(Of String)
        Dim go_baba As New List(Of String)
        Dim go_baba_status As New List(Of String)
        Dim go_wether As New List(Of String)
        Dim go_juni As New List(Of Integer)
        Dim go_time As New List(Of TimeSpan)
        Dim go_weight As New List(Of Integer)
        Dim go_noboriharon As New List(Of TimeSpan)

        'URL情報の操作オブジェクト
        Dim objWC As System.Net.WebClient = New System.Net.WebClient()
        objWC.Encoding = System.Text.Encoding.GetEncoding("euc-jp")

        'URL捜査準備
        Dim syutubahyou_url As String = syutubahyouurl
        Dim syutubahyou_html As String
        Dim syutubahyou_objDOC As HtmlAgilityPack.HtmlDocument

        'URLの取得検証
        Try
            ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
            ShowForm.ShowFormInstance.JikkouMethodText = "URLの取得検証" '実行中の処理を記載する。
            syutubahyou_html = objWC.DownloadString(syutubahyou_url)
            syutubahyou_objDOC = New HtmlAgilityPack.HtmlDocument()
            syutubahyou_objDOC.LoadHtml(syutubahyou_html)
        Catch ex As Exception
            MessageBox.Show("URLの取得に失敗しました。")
            Return False
        End Try


        '予想紙の有無によって参照するTDインデックスを変化させる。
        Dim colCount As Integer = 1
        Dim colcount_min As Integer
        Dim colCount_max As Integer
        Dim colyosousiCount As Integer
        For Each col In syutubahyou_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/th")
            If col.InnerText.IndexOf("あなた") >= 0 Then
                colcount_min = colCount
            End If
            If col.InnerText.IndexOf("馬名") >= 0 Then
                colCount_max = colCount
            End If
            colCount += 1
        Next
        colyosousiCount = colCount_max - colcount_min - 1



        '馬番を取得して出馬表から取得するナンバリングを取得する。
        ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
        ShowForm.ShowFormInstance.JikkouMethodText = "馬番を取得" '実行中の処理を記載する。

        For Each row In syutubahyou_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/td[2]")
            number.Add(rowcount)
            rowcount += 1
        Next


        'テーブル取得を行う。
        For Each temp_number In number
            ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。

            For Each row In syutubahyou_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr[" & temp_number + 1 & "]")
                Dim sansho_umaban As Integer = temp_number - 1    'テーブル列位置が相対変化するためインデックスを1つ下げる。

                '馬名を取得する。
                h_name.Add(row.SelectNodes("//td/span[@class=""h_name""]/a").Item(sansho_umaban).InnerText)

                '馬情報を取得する
                Dim h_smaller() = Split(row.SelectNodes("//td/span[@class=""txt_smaller""]").Item(sansho_umaban).InnerText, vbLf,)
                h_smaller_father.Add(h_smaller(1))
                h_smaller_mother.Add(h_smaller(2))

                Select Case Split(row.SelectNodes("//td[" & 2 + colyosousiCount & "]/img").Item(sansho_umaban).OuterHtml, "horse_race_type")(1).Substring(0, 2)
                    Case "00"   '逃げ
                        h_reg_type.Add("0:データなし")
                    Case "01"   '逃げ
                        h_reg_type.Add("1:逃げ")
                    Case "02"   '先行
                        h_reg_type.Add("2:先行")
                    Case "03"   '差し
                        h_reg_type.Add("3:差し")
                    Case "04"   '追込
                        h_reg_type.Add("4:追込")
                End Select

                '体重を取得（情報がNothingを追加）
                Try
                    h_weight.Add(Split(row.SelectNodes("//td/strong[@class=""weight""]").Item(sansho_umaban).InnerText, "(")(0))    '前走から計算するので±値は考慮しない事にする。
                Catch ex As Exception
                    h_weight.Add(Nothing)
                End Try

                '斤量（性別、歳、経路、斤量、機種）を取得
                Dim kinryo() = Split(row.SelectNodes("//td[" & 3 + colyosousiCount & "]").Item(sansho_umaban).InnerText, vbLf)
                h_sex.Add(kinryo(0).Substring(0, 1))
                h_age.Add(reg_int.Replace(kinryo(0), ""))
                h_keiro.Add(Split(kinryo(0), "/")(1))
                h_kinryou.Add(kinryo(1))
                h_kisyu.Add(kinryo(2))

                'オッズ、人気を取得
                Dim oz() = Split(row.SelectNodes("//td[" & 4 + colyosousiCount & "]").Item(sansho_umaban).InnerText, vbLf)
                h_oz.Add(oz(1))
                h_ninki.Add(reg_int.Replace(oz(2), ""))

                '1走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = number.Count & "頭中：" & temp_number & "." & h_name(sansho_umaban) & "1走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 5 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '1走前レース情報をリンクから取得する。
                    Dim itisoumae_info = row.SelectNodes("//td[" & 5 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - itisoumae_count)
                    Dim itisoumae_race_url = itisoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), itisoumae_race_url, iti_racename, iti_nagasa, iti_kaisaibi, iti_basho, iti_baba, iti_baba_status, iti_wether, iti_juni, iti_time, iti_weight)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    itisoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(iti_racename, iti_nagasa, iti_kaisaibi, iti_basho, iti_baba, iti_baba_status, iti_wether, iti_juni, iti_time, iti_weight)
                End If


                '2走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = number.Count & "頭中：" & temp_number & "." & h_name(sansho_umaban) & "2走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 6 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '2走前レース情報をリンクから取得する。
                    Dim nisoumae_info = row.SelectNodes("//td[" & 6 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - nisoumae_count)
                    Dim nisoumae_race_url = nisoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), nisoumae_race_url, ni_racename, ni_nagasa, ni_kaisaibi, ni_basho, ni_baba, ni_baba_status, ni_wether, ni_juni, ni_time, ni_weight)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    nisoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(ni_racename, ni_nagasa, ni_kaisaibi, ni_basho, ni_baba, ni_baba_status, ni_wether, ni_juni, ni_time, ni_weight)
                End If


                '3走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = number.Count & "頭中：" & temp_number & "." & h_name(sansho_umaban) & "3走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 7 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '3走前レース情報をリンクから取得する。
                    Dim sansoumae_info = row.SelectNodes("//td[" & 7 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - sansoumae_count)
                    Dim sansoumae_race_url = sansoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), sansoumae_race_url, san_racename, san_nagasa, san_kaisaibi, san_basho, san_baba, san_baba_status, san_wether, san_juni, san_time, san_weight)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    sansoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(san_racename, san_nagasa, san_kaisaibi, san_basho, san_baba, san_baba_status, san_wether, san_juni, san_time, san_weight)
                End If


                '4走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = number.Count & "頭中：" & temp_number & "." & h_name(sansho_umaban) & "4走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 8 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '4走前レース情報をリンクから取得する。
                    Dim yonsoumae_info = row.SelectNodes("//td[" & 8 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - yonsoumae_count)
                    Dim yonsoumae_race_url = yonsoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), yonsoumae_race_url, yon_racename, yon_nagasa, yon_kaisaibi, yon_basho, yon_baba, yon_baba_status, yon_wether, yon_juni, yon_time, yon_weight)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    yonsoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(yon_racename, yon_nagasa, yon_kaisaibi, yon_basho, yon_baba, yon_baba_status, yon_wether, yon_juni, yon_time, yon_weight)
                End If


                '5走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = number.Count & "頭中：" & temp_number & "." & h_name(sansho_umaban) & "5走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 9 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '5走前レース情報をリンクから取得する。
                    Dim gosoumae_info = row.SelectNodes("//td[" & 9 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - gosoumae_count)
                    Dim gosoumae_race_url = gosoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), gosoumae_race_url, go_racename, go_nagasa, go_kaisaibi, go_basho, go_baba, go_baba_status, go_wether, go_juni, go_time, go_weight)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    gosoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(go_racename, go_nagasa, go_kaisaibi, go_basho, go_baba, go_baba_status, go_wether, go_juni, go_time, go_weight)
                End If

            Next
        Next


#Region "スクレイピングデータテーブル"
        '取得したスクレイピングデータをデータグリッドビューのデータソースへとセットする。
        Dim dt = New DataTable("出馬表データテーブル")

        dt.Columns.Add("馬名", GetType(String))
        dt.Columns.Add("父", GetType(String))
        dt.Columns.Add("母", GetType(String))
        dt.Columns.Add("脚質", GetType(String))
        dt.Columns.Add("体重", GetType(String))
        dt.Columns.Add("性", GetType(String))
        dt.Columns.Add("歳", GetType(Integer))
        dt.Columns.Add("毛色", GetType(String))
        dt.Columns.Add("斤量", GetType(String))
        dt.Columns.Add("騎手", GetType(String))
        dt.Columns.Add("オッズ", GetType(Double))
        dt.Columns.Add("人気", GetType(Integer))
        dt.Columns.Add("1走前レース名", GetType(String))
        dt.Columns.Add("1走前長さ", GetType(Integer))
        dt.Columns.Add("1走前開催日", GetType(DateTime))
        dt.Columns.Add("1走前場所", GetType(String))
        dt.Columns.Add("1走前馬場", GetType(String))
        dt.Columns.Add("1走前馬場状況", GetType(String))
        dt.Columns.Add("1走前天気", GetType(String))
        dt.Columns.Add("1走前順位", GetType(Integer))
        dt.Columns.Add("1走前タイム", GetType(TimeSpan))
        dt.Columns.Add("1走前体重", GetType(Integer))
        dt.Columns.Add("2走前レース名", GetType(String))
        dt.Columns.Add("2走前長さ", GetType(Integer))
        dt.Columns.Add("2走前開催日", GetType(DateTime))
        dt.Columns.Add("2走前場所", GetType(String))
        dt.Columns.Add("2走前馬場", GetType(String))
        dt.Columns.Add("2走前馬場状況", GetType(String))
        dt.Columns.Add("2走前天気", GetType(String))
        dt.Columns.Add("2走前順位", GetType(Integer))
        dt.Columns.Add("2走前タイム", GetType(TimeSpan))
        dt.Columns.Add("2走前体重", GetType(Integer))
        dt.Columns.Add("3走前レース名", GetType(String))
        dt.Columns.Add("3走前長さ", GetType(Integer))
        dt.Columns.Add("3走前開催日", GetType(DateTime))
        dt.Columns.Add("3走前場所", GetType(String))
        dt.Columns.Add("3走前馬場", GetType(String))
        dt.Columns.Add("3走前馬場状況", GetType(String))
        dt.Columns.Add("3走前天気", GetType(String))
        dt.Columns.Add("3走前順位", GetType(Integer))
        dt.Columns.Add("3走前タイム", GetType(TimeSpan))
        dt.Columns.Add("3走前体重", GetType(Integer))
        dt.Columns.Add("4走前レース名", GetType(String))
        dt.Columns.Add("4走前長さ", GetType(Integer))
        dt.Columns.Add("4走前開催日", GetType(DateTime))
        dt.Columns.Add("4走前場所", GetType(String))
        dt.Columns.Add("4走前馬場", GetType(String))
        dt.Columns.Add("4走前馬場状況", GetType(String))
        dt.Columns.Add("4走前天気", GetType(String))
        dt.Columns.Add("4走前順位", GetType(Integer))
        dt.Columns.Add("4走前タイム", GetType(TimeSpan))
        dt.Columns.Add("4走前体重", GetType(Integer))
        dt.Columns.Add("5走前レース名", GetType(String))
        dt.Columns.Add("5走前長さ", GetType(Integer))
        dt.Columns.Add("5走前開催日", GetType(DateTime))
        dt.Columns.Add("5走前場所", GetType(String))
        dt.Columns.Add("5走前馬場", GetType(String))
        dt.Columns.Add("5走前馬場状況", GetType(String))
        dt.Columns.Add("5走前天気", GetType(String))
        dt.Columns.Add("5走前順位", GetType(Integer))
        dt.Columns.Add("5走前タイム", GetType(TimeSpan))
        dt.Columns.Add("5走前体重", GetType(Integer))

        For i = 0 To number.Count - 1
            dt.Rows.Add(h_name(i), h_smaller_father(i), h_smaller_mother(i), h_reg_type(i), h_weight(i), h_sex(i), h_age(i), h_keiro(i), h_kinryou(i), h_kisyu(i), h_oz(i), h_ninki(i),
                        iti_racename(i), iti_nagasa(i), iti_kaisaibi(i), iti_basho(i), iti_baba(i), iti_baba_status(i), iti_wether(i), iti_juni(i), iti_time(i), iti_weight(i),
                        ni_racename(i), ni_nagasa(i), ni_kaisaibi(i), ni_basho(i), ni_baba(i), ni_baba_status(i), ni_wether(i), ni_juni(i), ni_time(i), ni_weight(i),
                        san_racename(i), san_nagasa(i), san_kaisaibi(i), san_basho(i), san_baba(i), san_baba_status(i), san_wether(i), san_juni(i), san_time(i), san_weight(i),
                        yon_racename(i), yon_nagasa(i), yon_kaisaibi(i), yon_basho(i), yon_baba(i), yon_baba_status(i), yon_wether(i), yon_juni(i), yon_time(i), yon_weight(i),
                        go_racename(i), go_nagasa(i), go_kaisaibi(i), go_basho(i), go_baba(i), go_baba_status(i), go_wether(i), go_juni(i), go_time(i), go_weight(i))
        Next
#End Region


#Region "グリッドビュー設定"
        ShowForm.ShowFormInstance.SyutubahyouDataGridSet = dt
#End Region

        ShowForm.ShowFormInstance.JikkouMethodText = "「" & racename & "」情報を表示しています。（馬番が取得出来ませんでした。馬番が発表された後に再取得してください。）" '実行中の処理を記載する。
        ShowForm.ShowFormInstance.JikkouBarValue = ShowForm.jikkouBar.Maximum    '実行ステータスバーを加算する。
        MessageBox.Show("出馬表のスクレイピングが完了しました。" & vbCrLf & vbCrLf & "馬番が取得出来ませんでした。" & vbCrLf & "馬番が発表された後に再取得してください。", "注意")


        Return True
    End Function


End Class
