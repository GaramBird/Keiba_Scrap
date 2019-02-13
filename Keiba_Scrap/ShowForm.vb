Imports System.ComponentModel

Public Class ShowForm
    Shared scrap As SyutubahyouScrap = New SyutubahyouScrap
    Public gsInputCount As Integer = 0
    Public gsInputText As List(Of String) = New List(Of String)
    Public gsBackandGoFlg As Integer = 0
    Public gsDefaultURL As String = "https://race.netkeiba.com/?pid=race&id=c201806050811&mode=shutuba"

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

    Public Property DataGridSet() As DataTable
        Get
            Return dgvSyutubahyou.DataSource
        End Get
        Set(ByVal Value As DataTable)
            dgvSyutubahyou.DataSource = Value
        End Set
    End Property
#End Region



    'フォームロード（初期値設定）
    Private Sub Fromload(sender As Object, e As EventArgs) Handles MyBase.Load
        JikkouMethodText = "初期画面"
        Me.MinimumSize = New Size(700, 570)
        Me.txtSyutubahyouURL.Text = gsDefaultURL  '初期値サンプル
        Me.txtSyutubahyouURL.SelectAll()
        Me.dgvSyutubahyou.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvSyutubahyou.SelectionMode = DataGridViewSelectionMode.FullRowSelect
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
        If MessageBox.Show("表示しているグリッドビューをCSV出力しますか？（未実装、処理なし）", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
        End If
        Me.txtSyutubahyouURL.Focus()
    End Sub

    'キャンセルボタン
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.txtSyutubahyouURL.Focus()
    End Sub

    'レース取得ボタン
    Private Sub btnGetSyutubahyou_Click(sender As Object, e As EventArgs) Handles btnGetSyutubahyou.Click

        If txtSyutubahyouURL.Text <> "" Then
            If txtSyutubahyouURL.Text.IndexOf("race.netkeiba.com") >= 0 And txtSyutubahyouURL.Text.Substring(txtSyutubahyouURL.Text.Length - 12) = "mode=shutuba" Then
                Me.btnGetSyutubahyou.Enabled = False
                Me.btnCancel.Enabled = True
                Me.btnCSVGridView.Enabled = False
                JikkouBarValue += 1  '実行ステータスバーを加算する。
                JikkouMethodText = "処理開始" '実行中の処理を記載する。
                DataGridSet = Nothing
                Me.dgvSyutubahyou.Refresh()

                If Not scrap.Scraping(txtSyutubahyouURL.Text) Then  'スクレイピングを実行する。
                    JikkouBarValue = 0
                    JikkouMethodText = "処理失敗"
                    Me.btnGetSyutubahyou.Enabled = True
                    Me.btnCancel.Enabled = False
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
        Else
            Me.btnGetSyutubahyou.Enabled = False
            Me.btnCancel.Enabled = False
            Me.btnCSVGridView.Enabled = False
        End If
        Me.txtSyutubahyouURL.Focus()
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
                    Me.txtSyutubahyouURL.Text = gsDefaultURL
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


End Class
