'競馬スクレイピングクラス
Imports System.Text.RegularExpressions

Public Class SyutubahyouScrap
    Public Sub Scraping(syutubahyouurl)
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
        Dim h_weight As New List(Of String)
        Dim h_sex As New List(Of String)
        Dim h_age As New List(Of Integer)
        Dim h_keiro As New List(Of String)
        Dim h_kinryou As New List(Of String)
        Dim h_kisyu As New List(Of String)
        Dim h_oz As New List(Of String)
        Dim h_ninki As New List(Of String)

        '1走前情報変数コレクション
        Dim itisoumae_count As Integer = 0
        Dim iti_racename As New List(Of String)
        Dim iti_nagasa As New List(Of String)
        Dim iti_kaisaibi As New List(Of String)
        Dim iti_basho As New List(Of String)
        Dim iti_baba As New List(Of String)
        Dim iti_baba_status As New List(Of String)
        Dim iti_wether As New List(Of String)
        Dim iti_juni As New List(Of String)
        Dim iti_time As New List(Of TimeSpan)
        Dim iti_noboriharon As New List(Of TimeSpan)


        '2走前情報変数コレクション
        Dim nisoumae_count As Integer = 0
        Dim ni_racename As New List(Of String)
        Dim ni_nagasa As New List(Of String)
        Dim ni_kaisaibi As New List(Of String)
        Dim ni_basho As New List(Of String)
        Dim ni_baba As New List(Of String)
        Dim ni_baba_status As New List(Of String)
        Dim ni_wether As New List(Of String)
        Dim ni_juni As New List(Of String)
        Dim ni_time As New List(Of TimeSpan)
        Dim ni_noboriharon As New List(Of TimeSpan)

        '3走前情報変数コレクション
        Dim sansoumae_count As Integer = 0
        Dim san_racename As New List(Of String)
        Dim san_nagasa As New List(Of String)
        Dim san_kaisaibi As New List(Of String)
        Dim san_basho As New List(Of String)
        Dim san_baba As New List(Of String)
        Dim san_baba_status As New List(Of String)
        Dim san_wether As New List(Of String)
        Dim san_juni As New List(Of String)
        Dim san_time As New List(Of TimeSpan)
        Dim san_noboriharon As New List(Of TimeSpan)

        '4走前情報変数コレクション
        Dim yonsoumae_count As Integer = 0
        Dim yon_racename As New List(Of String)
        Dim yon_nagasa As New List(Of String)
        Dim yon_kaisaibi As New List(Of String)
        Dim yon_basho As New List(Of String)
        Dim yon_baba As New List(Of String)
        Dim yon_baba_status As New List(Of String)
        Dim yon_wether As New List(Of String)
        Dim yon_juni As New List(Of String)
        Dim yon_time As New List(Of TimeSpan)
        Dim yon_noboriharon As New List(Of TimeSpan)

        '5走前情報変数コレクション
        Dim gosoumae_count As Integer = 0
        Dim go_racename As New List(Of String)
        Dim go_nagasa As New List(Of String)
        Dim go_kaisaibi As New List(Of String)
        Dim go_basho As New List(Of String)
        Dim go_baba As New List(Of String)
        Dim go_baba_status As New List(Of String)
        Dim go_wether As New List(Of String)
        Dim go_juni As New List(Of String)
        Dim go_time As New List(Of TimeSpan)
        Dim go_noboriharon As New List(Of TimeSpan)

        'URL情報の操作オブジェクト
        Dim objWC As System.Net.WebClient = New System.Net.WebClient()
        objWC.Encoding = System.Text.Encoding.GetEncoding("euc-jp")

        'URL捜査準備
        'Dim syutubahyou_url As String = "https://race.netkeiba.com/?pid=race&id=c201806050811&mode=shutuba"    'sampleURL（2018年有馬記念）
        Dim syutubahyou_url As String = syutubahyouurl
        Dim syutubahyou_html As String
        Dim syutubahyou_objDOC As HtmlAgilityPack.HtmlDocument

        Try
            syutubahyou_html = objWC.DownloadString(syutubahyou_url)
            syutubahyou_objDOC = New HtmlAgilityPack.HtmlDocument()
            syutubahyou_objDOC.LoadHtml(syutubahyou_html)
        Catch ex As Exception
            MessageBox.Show("URLの取得に失敗しました。")
            Exit Sub
        End Try

        ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
        ShowForm.ShowFormInstance.JikkouMethodText = "HTMLを取得" '実行中の処理を記載する。
        syutubahyou_html = objWC.DownloadString(syutubahyou_url)
        syutubahyou_objDOC = New HtmlAgilityPack.HtmlDocument()
        syutubahyou_objDOC.LoadHtml(syutubahyou_html)

        '取得したレース名を取得する。
        Dim racename = syutubahyou_objDOC.DocumentNode.SelectNodes("//div[@class=""data_intro""]/dl/dd/h1").Item(0).InnerText.Replace("&nbsp;", "")

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
        For Each row In syutubahyou_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr/td[@class=""umaban""]")
            h_umaban.Add(Integer.Parse(row.InnerText))
        Next




        'テーブル取得を行う。
        For Each temp_h_umaban In h_umaban
            ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。

            For Each row In syutubahyou_objDOC.DocumentNode.SelectNodes("//table[@class=""race_table_01 nk_tb_common shutuba_table""]/tr[" & temp_h_umaban + 1 & "]")
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。

                Dim sansho_umaban As Integer = temp_h_umaban - 1    'テーブル列位置が相対変化するためインデックスを1つ下げる。

                '馬名を取得する。
                h_name.Add(row.SelectNodes("//td/span[@class=""h_name""]/a").Item(sansho_umaban).InnerText)

                '馬情報を取得する
                Dim h_smaller() = Split(row.SelectNodes("//td/span[@class=""txt_smaller""]").Item(sansho_umaban).InnerText, vbLf,)
                h_smaller_father.Add(h_smaller(1))
                h_smaller_mother.Add(h_smaller(2))

                Select Case Split(row.SelectNodes("//td[" & 4 + colyosousiCount & "]/img").Item(sansho_umaban).OuterHtml, "horse_race_type")(1).Substring(0, 2)
                    Case "01"   '逃げ
                        h_reg_type.Add("逃げ")
                    Case "02"   '先行
                        h_reg_type.Add("先行")
                    Case "03"   '差し
                        h_reg_type.Add("差し")
                    Case "04"   '追込
                        h_reg_type.Add("追込")
                End Select

                h_weight.Add(Split(row.SelectNodes("//td/strong[@class=""weight""]").Item(sansho_umaban).InnerText, "(")(0))    '前走から計算するので±値は考慮しない事にする。


                Dim kinryo() = Split(row.SelectNodes("//td[" & 5 + colyosousiCount & "]").Item(sansho_umaban).InnerText, vbLf)
                h_sex.Add(kinryo(0).Substring(0, 1))
                h_age.Add(reg_int.Replace(kinryo(0), ""))
                h_keiro.Add(Split(kinryo(0), "/")(1))
                h_kinryou.Add(kinryo(1))
                h_kisyu.Add(kinryo(2))

                Dim oz() = Split(row.SelectNodes("//td[" & 6 + colyosousiCount & "]").Item(sansho_umaban).InnerText, vbLf)
                h_oz.Add(oz(1))
                h_ninki.Add(reg_int.Replace(oz(2), ""))

                '1走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = temp_h_umaban & "." & h_name(sansho_umaban) & "1走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 7 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '1走前レース情報をリンクから取得する。
                    Dim itisoumae_info = row.SelectNodes("//td[" & 7 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - itisoumae_count)
                    Dim itisoumae_race_url = itisoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), itisoumae_race_url, iti_racename, iti_nagasa, iti_kaisaibi, iti_basho, iti_baba, iti_baba_status, iti_wether, iti_juni, iti_time)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    itisoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(iti_racename, iti_nagasa, iti_kaisaibi, iti_basho, iti_baba, iti_baba_status, iti_wether, iti_juni, iti_time)
                End If


                '2走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = temp_h_umaban & "." & h_name(sansho_umaban) & "2走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 8 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '2走前レース情報をリンクから取得する。
                    Dim nisoumae_info = row.SelectNodes("//td[" & 8 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - nisoumae_count)
                    Dim nisoumae_race_url = nisoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), nisoumae_race_url, ni_racename, ni_nagasa, ni_kaisaibi, ni_basho, ni_baba, ni_baba_status, ni_wether, ni_juni, ni_time)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    nisoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(ni_racename, ni_nagasa, ni_kaisaibi, ni_basho, ni_baba, ni_baba_status, ni_wether, ni_juni, ni_time)
                End If


                '3走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = temp_h_umaban & "." & h_name(sansho_umaban) & "3走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 9 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '3走前レース情報をリンクから取得する。
                    Dim sansoumae_info = row.SelectNodes("//td[" & 9 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - sansoumae_count)
                    Dim sansoumae_race_url = sansoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), sansoumae_race_url, san_racename, san_nagasa, san_kaisaibi, san_basho, san_baba, san_baba_status, san_wether, san_juni, san_time)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    sansoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(san_racename, san_nagasa, san_kaisaibi, san_basho, san_baba, san_baba_status, san_wether, san_juni, san_time)
                End If


                '4走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = temp_h_umaban & "." & h_name(sansho_umaban) & "4走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 10 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '4走前レース情報をリンクから取得する。
                    Dim yonsoumae_info = row.SelectNodes("//td[" & 10 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - yonsoumae_count)
                    Dim yonsoumae_race_url = yonsoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), yonsoumae_race_url, yon_racename, yon_nagasa, yon_kaisaibi, yon_basho, yon_baba, yon_baba_status, yon_wether, yon_juni, yon_time)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    yonsoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(yon_racename, yon_nagasa, yon_kaisaibi, yon_basho, yon_baba, yon_baba_status, yon_wether, yon_juni, yon_time)
                End If


                '5走前レースがあったかの判定をする。（ないOR休養）その他のレースなし情報があれば条件を追加することにする。
                ShowForm.ShowFormInstance.JikkouBarValue += 1    '実行ステータスバーを加算する。
                ShowForm.ShowFormInstance.JikkouMethodText = temp_h_umaban & "." & h_name(sansho_umaban) & "5走前レース取得" '実行中の処理を記載する。
                If row.SelectNodes("//td[" & 11 + colyosousiCount & "]").Item(sansho_umaban).InnerHtml.IndexOf("<a") >= 0 Then
                    '5走前レース情報をリンクから取得する。
                    Dim gosoumae_info = row.SelectNodes("//td[" & 11 + colyosousiCount & "]/div[@class=""inner""]/div[@class=""racebox""]/span[@class=""race_name""]/a").Item(sansho_umaban - gosoumae_count)
                    Dim gosoumae_race_url = gosoumae_info.Attributes("href").Value.Trim()
                    Call getinfo.GetAgoRaceInfo(h_name(sansho_umaban), gosoumae_race_url, go_racename, go_nagasa, go_kaisaibi, go_basho, go_baba, go_baba_status, go_wether, go_juni, go_time)
                Else
                    'レース情報（<a>タグ）がない場合の処理、休養は直近の出場レースから日時を取得するので考慮しないものとする。
                    gosoumae_count += 1
                    Call getinfo.NULLAgoRaceInfo(go_racename, go_nagasa, go_kaisaibi, go_basho, go_baba, go_baba_status, go_wether, go_juni, go_time)
                End If

            Next
        Next

        ShowForm.ShowFormInstance.JikkouMethodText = "「" & racename & "」情報を表示しています。" '実行中の処理を記載する。
        ShowForm.ShowFormInstance.JikkouBarValue = ShowForm.jikkouBar.Maximum    '実行ステータスバーを加算する。
        MessageBox.Show("出馬表のスクレイピングが完了しました。")

        '取得したスクレイピングデータをデータグリッドビューのデータソースへとセットする。
        Dim ds = New DataSet("スクレイピングデータセット")
        Dim dt = New DataTable("スクレイピングデータテーブル")

        'グリッドビュー設定
        dt.Columns.Add("馬番", GetType(Integer))
        dt.Columns.Add("馬名", GetType(String))

        'For i = 0 To h_umaban.Count - 1
        '    Dim datarow As DataRow = dt.NewRow
        '    datarow("馬番") = h_umaban(i)
        '    datarow("馬名") = h_name(i)
        'Next

        For i = 0 To h_umaban.Count - 1
            dt.Rows.Add(h_umaban(i), h_name(i))
        Next

        ShowForm.ShowFormInstance.DataGridSet = dt

    End Sub


End Class
