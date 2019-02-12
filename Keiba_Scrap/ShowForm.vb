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
        Me.txtSyutubahyouURL.Text = "https://race.netkeiba.com/?pid=race&id=c201806050811&mode=shutuba"  '初期値サンプル
        'Me.txtSyutubahyouURL.Text = "https://race.netkeiba.com/?pid=race&id=c201905010611&mode=shutuba"
        Me.dgvSyutubahyou.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvSyutubahyou.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub


    'レース取得ボタンのクリック
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
                scrap.Scraping(txtSyutubahyouURL.Text)  'スクレイピングを実行する。

            Else
                If MessageBox.Show("netkeiba.comの出馬表（５柱）のURLを入力してください。" & vbCrLf & "トップページ→レース→レース一覧→レース名→出馬表→出馬表（5柱）" & vbCrLf & vbCrLf & "サイトを開きますか？", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
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
            If MessageBox.Show("URLが入力されていません。サイトを開きますか？", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
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
    End Sub

    '
    Private Sub btnGetSyutubahyou_Enter(sender As Object, e As EventArgs) Handles btnGetSyutubahyou.Enter
        Me.txtSyutubahyouURL.SelectAll()
    End Sub


    Private Sub jikkoumethod_TextChanged(sender As Object, e As EventArgs) Handles jikkoumethod.TextChanged
        Me.jikkoumethod.Refresh()
    End Sub


    Private Sub txtSyutubahyouURL_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSyutubahyouURL.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnGetSyutubahyou_Click(Me.btnGetSyutubahyou, e)
        End If


        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso (e.KeyData And Keys.KeyCode) = Keys.A Then
            Me.txtSyutubahyouURL.SelectAll()
        End If

    End Sub

#Region "戻る、進む"
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If gsInputCount > 0 Then
            gsBackandGoFlg = 1
            If gsInputCount = 1 Then
                Me.txtSyutubahyouURL.Text = ""
            Else
                Me.txtSyutubahyouURL.Text = gsInputText(gsInputCount - 2)
            End If
            gsInputText.Remove(gsInputCount - 1)
            gsInputCount -= 1
        End If
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

    End Sub
#End Region

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

    Private Sub btnBrowserView_Click(sender As Object, e As EventArgs) Handles btnBrowserView.Click
        Try
            System.Diagnostics.Process.Start(Me.txtSyutubahyouURL.Text)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btnCSVGridView_Click(sender As Object, e As EventArgs) Handles btnCSVGridView.Click
        If MessageBox.Show("表示しているグリッドビューをCSV出力しますか？（未実装、処理なし）", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then

        End If

    End Sub


End Class
