﻿Public Class ShowForm
    Shared scrap As SyutubahyouScrap = New SyutubahyouScrap

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





    Private Sub Fromload(sender As Object, e As EventArgs) Handles MyBase.Load
        JikkouMethodText = "処理なし"
        Me.txtSyutubahyouURL.Text = "https://race.netkeiba.com/?pid=race&id=c201806050811&mode=shutuba"  '初期値サンプル
    End Sub

    Private Sub btnGetSyutubahyou_Click(sender As Object, e As EventArgs) Handles btnGetSyutubahyou.Click
        Me.btnGetSyutubahyou.Enabled = False
        JikkouBarValue += 1  '実行ステータスバーを加算する。
        JikkouMethodText = "処理開始" '実行中の処理を記載する。


        If txtSyutubahyouURL.Text <> "" Then
            If txtSyutubahyouURL.Text.IndexOf("mode=shutuba") >= 0 And txtSyutubahyouURL.Text.IndexOf("race.netkeiba.com") >= 0 Then
                scrap.Scraping(txtSyutubahyouURL.Text)
            Else
                If MessageBox.Show("netkeiba.comのレース情報URLを入力してください。サイトを開きますか？", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start("http://www.netkeiba.com/?rf=logo")

                End If
            End If
        Else
            MessageBox.Show("URLが入力されていません。")
        End If

        JikkouBarValue = 0
        If JikkouMethodText.IndexOf("表示しています") >= 0 Then
        Else
            JikkouMethodText = "処理なし" '実行中の処理を記載する。
        End If
        Me.btnGetSyutubahyou.Enabled = True
    End Sub

    Private Sub btnGetSyutubahyou_Enter(sender As Object, e As EventArgs) Handles btnGetSyutubahyou.Enter
        Me.txtSyutubahyouURL.SelectAll()
    End Sub

    Private Sub btnGetSyutubahyou_KeyDown(sender As Object, e As KeyEventArgs) Handles btnGetSyutubahyou.KeyDown
        If e.Control And e.KeyCode = Keys.A = True Then
            Me.txtSyutubahyouURL.SelectAll()
        End If
    End Sub

    Private Sub jikkoumethod_TextChanged(sender As Object, e As EventArgs) Handles jikkoumethod.TextChanged
        Me.jikkoumethod.Refresh()
    End Sub
End Class