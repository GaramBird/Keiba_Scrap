Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types


Public Class DBaccess
    Private Function GetaccessString() As Boolean
        Dim strUser As String 'ユーザー
        Dim strPasswd As String 'パスワード
        Dim strDataSource As String 'データソース
        Dim strConnectstring As String
        strUser = "birdDB"
        strPasswd = "tori1023"
        strDataSource = "orcl"
        strConnectstring = "User Id=" & strUser & "; " & "Password=" & strPasswd & "; " & "Data Source=" & strDataSource
    End Function

    Private Function DbOpen(ByVal constr As String) As Boolean
        Dim oraCon As OracleConnection
        oraCon = New OracleConnection(constr)

        Try
            oraCon.Open()
            Return True
        Catch ex As Exception
            MessageBox.Show("データベースアクセスエラー")
            Return False
        End Try
    End Function

    Private Sub DbTransaction()
        Dim oraTran As OracleTransaction
        oraTran.Commit()
        oraTran.Rollback()

    End Sub

End Class
