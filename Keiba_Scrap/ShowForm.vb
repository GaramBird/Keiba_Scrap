Imports System.ComponentModel

Public Class ShowForm
    Shared scrap As SyutubahyouScrap = New SyutubahyouScrap
    Public gsInputCount As Integer = 0
    Public gsInputText As List(Of String) = New List(Of String)
    Public gsBackandGoFlg As Integer = 0

#Region "プロパティ"
    '別クラスから値を取得・設定するためのフィールドとプロパティ
    Private Shared _showforminstance As ShowForm
    Public Shared Property ShowFormInstance() As ShowForm
        Get
            Return _showforminstance
        End Get
        Set(value As ShowForm)
            _showforminstance = value
        End Set
    End Property

    Public Property JikkouBarValue() As Integer
        Get
            Return jikkouBar.Value
        End Get
        Set(ByVal Value As Integer)
            jikkouBar.Value = Value
        End Set
    End Property
    Public Property JikkouMethodText() As String
        Get
            Return jikkoumethod.Text
        End Get
        Set(ByVal Value As String)
            jikkoumethod.Text = Value
        End Set
    End Property

    Public Property SyutubahyouDataGridSet() As DataTable
        Get
            Return dgvSyutubahyou.DataSource
        End Get
        Set(ByVal Value As DataTable)
            dgvSyutubahyou.DataSource = Value
        End Set
    End Property

    Public Property YosouRaceDataGridSet() As DataTable
        Get
            Return dgvYosouRace.DataSource
        End Get
        Set(ByVal Value As DataTable)
            dgvYosouRace.DataSource = Value
        End Set
    End Property

#End Region



    'フォームロード（初期値設定）
    Private Sub Fromload(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "netkeiba.comスクレイピング"
        JikkouMethodText = "初期画面"
        Me.MinimumSize = New Size(897, 593)
        Me.txtSyutubahyouURL.SelectAll()
    End Sub

    'ブラウザ表示ボタン
    Private Sub btnBrowserView_Click(sender As Object, e As EventArgs) Handles btnBrowserView.Click
        Try
            System.Diagnostics.Process.Start(Me.txtSyutubahyouURL.Text)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Me.txtSyutubahyouURL.Focus()
        End Try
    End Sub

    'CSV出力ボタン
    Private Sub btnCSVGridView_Click(sender As Object, e As EventArgs) Handles btnCSVGridView.Click
        'If MessageBox.Show("表示しているグリッドビューをCSV出力しますか？（未実装、処理なし）", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
        'End If
        Me.txtSyutubahyouURL.Focus()
    End Sub

    'キャンセルボタン
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.txtSyutubahyouURL.Focus()
    End Sub

    'レース取得ボタン
    Private Sub btnGetSyutubahyou_Click(sender As Object, e As EventArgs) Handles btnGetSyutubahyou.Click

        If txtSyutubahyouURL.Text <> "" Then
            If txtSyutubahyouURL.Text.IndexOf("race.netkeiba.com") >= 0 Then
                Me.btnGetSyutubahyou.Enabled = False
                Me.btnCancel.Enabled = True
                Me.btnCSVGridView.Enabled = False
                Me.btnAiLogic.Enabled = False
                JikkouBarValue += 1  '実行ステータスバーを加算する。
                JikkouMethodText = "処理開始" '実行中の処理を記載する。
                SyutubahyouDataGridSet = Nothing
                Me.dgvSyutubahyou.Refresh()
                YosouRaceDataGridSet = Nothing
                Me.dgvYosouRace.Refresh()

                If Not scrap.Scraping(txtSyutubahyouURL.Text) Then  'スクレイピングを実行する。
                    JikkouBarValue = 0
                    JikkouMethodText = "処理失敗"
                    Me.btnGetSyutubahyou.Enabled = True
                    Me.btnCancel.Enabled = False
                    Me.dgvSyutubahyou.CurrentCell = Nothing
                    Me.dgvYosouRace.CurrentCell = Nothing
                    Exit Sub
                End If

            Else
                If MessageBox.Show("netkeiba.comの出馬表（５柱）のURLを入力してください。" & vbCrLf & "トップページ→レース→レース一覧→レース名→出馬表→出馬表（5柱）" & vbCrLf & vbCrLf & "netkeiba.comを開きますか？", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start("http://www.netkeiba.com/?rf=logo")
                    Me.btnGetSyutubahyou.Enabled = True
                    Me.btnCancel.Enabled = False
                    Exit Sub
                Else
                    Me.btnGetSyutubahyou.Enabled = True
                    Me.btnCancel.Enabled = False
                    Exit Sub
                End If
            End If
        Else
            If MessageBox.Show("URLが入力されていません。netkeiba.comを開きますか", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                System.Diagnostics.Process.Start("http://www.netkeiba.com/?rf=logo")
                Me.btnGetSyutubahyou.Enabled = True
                Me.btnCancel.Enabled = False
                Exit Sub
            Else
                Me.btnGetSyutubahyou.Enabled = True
                Me.btnCancel.Enabled = False
                Exit Sub
            End If
        End If

        JikkouBarValue = 0
        If JikkouMethodText.IndexOf("表示しています") >= 0 Then
            Me.btnGetSyutubahyou.Enabled = True
            Me.btnCancel.Enabled = False
            Me.btnCSVGridView.Enabled = True
            Me.btnAiLogic.Enabled = True
        Else
            Me.btnGetSyutubahyou.Enabled = False
            Me.btnCancel.Enabled = False
            Me.btnCSVGridView.Enabled = False
            Me.btnAiLogic.Enabled = False
        End If
        Me.txtSyutubahyouURL.Focus()
        Me.dgvSyutubahyou.CurrentCell = Nothing
        Me.dgvYosouRace.CurrentCell = Nothing
    End Sub

    '
    Private Sub btnGetSyutubahyou_Enter(sender As Object, e As EventArgs) Handles btnGetSyutubahyou.Enter
        Me.txtSyutubahyouURL.SelectAll()
    End Sub


    Private Sub jikkoumethod_TextChanged(sender As Object, e As EventArgs) Handles jikkoumethod.TextChanged
        Me.jikkoumethod.Refresh()
    End Sub

    'テキストボックス内で特定キーが押された場合
    Private Sub txtSyutubahyouURL_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSyutubahyouURL.KeyDown
        'Enterキー
        If e.KeyCode = Keys.Enter Then
            Call btnGetSyutubahyou_Click(Me.btnGetSyutubahyou, e)
        End If

        'Ctrl+Aキー
        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso (e.KeyData And Keys.KeyCode) = Keys.A Then
            Me.txtSyutubahyouURL.SelectAll()
        End If

        'Ctrl+Zキー
        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso (e.KeyData And Keys.KeyCode) = Keys.Z Then
            If gsInputCount > 0 Then
                gsBackandGoFlg = 1
                If gsInputCount = 1 Then
                    Me.txtSyutubahyouURL.Text = ""
                Else
                    Dim selectlen = Me.txtSyutubahyouURL.SelectionStart
                    Dim beforelen = Me.txtSyutubahyouURL.Text.Length
                    Me.txtSyutubahyouURL.Text = gsInputText(gsInputCount - 2)
                    If beforelen > Me.txtSyutubahyouURL.Text.Length Then
                        '文字列が減ったら引く
                        If selectlen > beforelen - gsInputText(gsInputCount - 2).Length Then
                            Me.txtSyutubahyouURL.SelectionStart = selectlen - (beforelen - gsInputText(gsInputCount - 2).Length)
                        Else
                            Me.txtSyutubahyouURL.SelectionStart = 0
                        End If

                    ElseIf beforelen < Me.txtSyutubahyouURL.Text.Length Then
                        '文字列が増えたら足す
                        Me.txtSyutubahyouURL.SelectionStart = selectlen + (gsInputText(gsInputCount - 2).Length - beforelen)
                    Else
                        '文字列の数が同じなら変わらない
                        Me.txtSyutubahyouURL.SelectionStart = selectlen
                    End If
                End If
                gsInputCount -= 1
            End If
        End If

        'Ctrl+Yキー
        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso (e.KeyData And Keys.KeyCode) = Keys.Y Then
            If gsInputCount <= gsInputText.Count Then
                gsBackandGoFlg = 1
                If gsInputCount = gsInputText.Count Then
                Else
                    Dim selectlen = Me.txtSyutubahyouURL.SelectionStart
                    Dim beforelen = Me.txtSyutubahyouURL.Text.Length
                    Me.txtSyutubahyouURL.Text = gsInputText(gsInputCount)
                    If beforelen > Me.txtSyutubahyouURL.Text.Length Then
                        '文字列が減ったら引く
                        If selectlen > beforelen - gsInputText(gsInputCount).Length Then
                            Me.txtSyutubahyouURL.SelectionStart = selectlen - (beforelen - gsInputText(gsInputCount).Length)
                        Else
                            Me.txtSyutubahyouURL.SelectionStart = 0
                        End If
                    ElseIf beforelen < Me.txtSyutubahyouURL.Text.Length Then
                        '文字列が増えたら足す
                        Me.txtSyutubahyouURL.SelectionStart = selectlen + (gsInputText(gsInputCount).Length - beforelen)
                    Else
                        '文字列の数が同じなら変わらない
                        Me.txtSyutubahyouURL.SelectionStart = selectlen
                    End If
                    gsInputText.Remove(gsInputCount - 1)
                    gsInputCount += 1
                End If
            End If
        End If

    End Sub

    Private Sub txtSyutubahyouURL_TextChanged(sender As Object, e As EventArgs) Handles txtSyutubahyouURL.TextChanged
        Try
            If gsBackandGoFlg = 0 Then
                gsInputText.Add(txtSyutubahyouURL.Text)
                gsInputCount += 1
            End If
            gsBackandGoFlg = 0

        Catch ex As Exception
            MessageBox.Show("入力した履歴が記憶しきれません。フォームを閉じます。")
            Me.Close()
        End Try

        If Me.txtSyutubahyouURL.Text.IndexOf("http://") >= 0 Or Me.txtSyutubahyouURL.Text.IndexOf("https://") >= 0 Then
            Me.btnBrowserView.Enabled = True
        Else
            Me.btnBrowserView.Enabled = False
        End If
    End Sub

    Private Sub レース情報を取得ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles レース情報を取得ToolStripMenuItem.Click
        Call btnGetSyutubahyou_Click(Me.btnGetSyutubahyou, e)
    End Sub

    Private Sub 閉じるToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 閉じるToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub 年12月有馬記念サンプルToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 年12月有馬記念サンプルToolStripMenuItem.Click
        Me.txtSyutubahyouURL.Text = "https://race.netkeiba.com/?pid=race&id=c201806050811&mode=shutuba"
        Me.txtSyutubahyouURL.Refresh()
        Call btnGetSyutubahyou_Click(Me.btnGetSyutubahyou, e)
    End Sub
    Private Sub 年2月共同通信杯サンプル7頭ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 年2月共同通信杯サンプル7頭ToolStripMenuItem.Click
        Me.txtSyutubahyouURL.Text = "https://race.netkeiba.com/?pid=race&id=p201905010611&mode=shutuba"
        Me.txtSyutubahyouURL.Refresh()
        Call btnGetSyutubahyou_Click(Me.btnGetSyutubahyou, e)
    End Sub

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles TopLink.Click
        System.Diagnostics.Process.Start("http://www.netkeiba.com/?rf=logo")
    End Sub


    Private Sub dgvSyutubahyou_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvSyutubahyou.CellFormatting
        'グリッドビューの値が0の時(Integer)
        If TypeOf e.Value Is Integer Then
            Dim val As Integer = CInt(e.Value)
            If val <= 0 Then
                e.Value = "なし"
            End If
        End If
        'グリッドビューの値が0の時(DateTime)
        If TypeOf e.Value Is DateTime Then
            Dim val As DateTime = CObj(e.Value)
            If val = Nothing Then
                e.Value = "なし"
            End If
        End If
        'グリッドビューの値が0の時(TimeSpan)
        If TypeOf e.Value Is TimeSpan Then
            Dim val As TimeSpan = CObj(e.Value)
            If val = Nothing Then
                e.Value = "なし"
            End If
        End If
    End Sub

    Private Sub dgvSyutubahyou_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvSyutubahyou.ColumnHeaderMouseClick
        Dim clickedColumn As DataGridViewColumn = Me.dgvSyutubahyou.Columns(e.ColumnIndex)
        If clickedColumn.SortMode <> DataGridViewColumnSortMode.Automatic Then
            'ソート前に選択している馬名を取得する。
            '選択されている行を表示
            Dim sentakuindex As Integer = -1
            Dim sentakurow As List(Of String) = New List(Of String)
            For Each r As DataGridViewRow In Me.dgvSyutubahyou.SelectedRows
                sentakurow.Add(r.Cells("馬名").Value)
            Next r

            'ソート実行
            Me.dgvSyutubahyou.CurrentCell = Nothing
            Me.SortRows(clickedColumn, True)
            Me.dgvSyutubahyou.Refresh()

            '合致する行から最後のインデックスを取得するための前処理
            For Each r In sentakurow
                For Each r2 As DataGridViewRow In Me.dgvSyutubahyou.Rows
                    If r = r2.Cells("馬名").Value Then
                        r2.Cells("馬名").Selected = True
                        sentakuindex = r2.Index
                    End If
                Next
            Next
            '最後に選択したセルをカレントする。
            If sentakuindex >= 0 Then
                Me.dgvSyutubahyou.CurrentCell = Me.dgvSyutubahyou.Rows.Item(sentakuindex).Cells("馬名")
            End If
            '合致する行を選択するための後処理
            For Each r In sentakurow
                For Each r2 As DataGridViewRow In Me.dgvSyutubahyou.Rows
                    If r = r2.Cells("馬名").Value Then
                        r2.Cells("馬名").Selected = True
                        sentakuindex = r2.Index
                    End If
                Next
            Next

        End If
    End Sub


    Private Sub SortRows(ByVal sortColumn As DataGridViewColumn,
        ByVal orderToggle As Boolean)
        If sortColumn Is Nothing Then
            Return
        End If


        '今までの並び替えグリフを消す
        If sortColumn.SortMode = DataGridViewColumnSortMode.Programmatic AndAlso
            Not (Me.dgvSyutubahyou.SortedColumn Is Nothing) AndAlso
            Not Me.dgvSyutubahyou.SortedColumn.Equals(sortColumn) Then
            Me.dgvSyutubahyou.SortedColumn.HeaderCell.SortGlyphDirection =
                SortOrder.None
        End If

        '並び替えの方向（昇順か降順か）を決める
        Dim sortDirection As System.ComponentModel.ListSortDirection
        If orderToggle Then
            sortDirection = IIf(Me.dgvSyutubahyou.SortOrder = SortOrder.Descending,
                System.ComponentModel.ListSortDirection.Ascending,
                System.ComponentModel.ListSortDirection.Descending)
        Else
            sortDirection = IIf(Me.dgvSyutubahyou.SortOrder = SortOrder.Descending,
                System.ComponentModel.ListSortDirection.Descending,
                System.ComponentModel.ListSortDirection.Ascending)
        End If
        Dim sOrder As SortOrder =
            IIf(sortDirection = System.ComponentModel.ListSortDirection.Ascending,
                SortOrder.Ascending, SortOrder.Descending)

        '並び替えを行う
        Me.dgvSyutubahyou.Sort(sortColumn, sortDirection)

        If sortColumn.SortMode = DataGridViewColumnSortMode.Programmatic Then
            '並び替えグリフを変更
            sortColumn.HeaderCell.SortGlyphDirection = sOrder
        End If
    End Sub

    Private Sub dgvSyutubahyou_Paint(sender As Object, e As PaintEventArgs) Handles dgvSyutubahyou.Paint
        'プログラムでしか並び替えられないようにする
        Dim dgvsyutubahyou_col As DataGridViewColumn
        For Each dgvsyutubahyou_col In dgvSyutubahyou.Columns
            dgvsyutubahyou_col.SortMode = DataGridViewColumnSortMode.Programmatic
        Next dgvsyutubahyou_col

        Me.dgvSyutubahyou.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvSyutubahyou.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvSyutubahyou.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvSyutubahyou.AllowUserToAddRows = False
        Me.dgvSyutubahyou.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName("WhiteSmoke")  '奇数行を黄色にする
        Me.dgvSyutubahyou.DefaultCellStyle.NullValue = "なし"
    End Sub

    Private Sub dgvYosouRace_Paint(sender As Object, e As PaintEventArgs) Handles dgvYosouRace.Paint
        Me.dgvYosouRace.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvYosouRace.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvYosouRace.AllowUserToAddRows = False
    End Sub

End Class
