Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types


Public Class DBaccess
    Private Function GetaccessString(ByRef strConnectstring) As Boolean
        Dim strUser As String 'ユーザー
        Dim strPasswd As String 'パスワード
        Dim strDataSource As String 'データソース
        strUser = "birdDB"
        strPasswd = "tori1023"
        strDataSource = "orcl"
        strConnectstring = "User Id=" & strUser & "; " & "Password=" & strPasswd & "; " & "Data Source=" & strDataSource
    End Function


    Public Function dbaccess_Open() As Boolean
        Dim str As String = ""
        GetaccessString(str)

        If Not DbOpen(str) Then
            Return False
        End If

        Return True
    End Function

    Public Function dbaccdess_Close() As Boolean
        Dim str As String = ""
        GetaccessString(str)

        If Not DbClose(str) Then
            Return False
        End If
        Return True
    End Function


    Public Function DbCommit() As Boolean
        Dim oraTran As OracleTransaction
        Try
            oraTran.Commit()
            Return True
        Catch ex As Exception
            MessageBox.Show("データベースアクセスエラー")
            Return False
        End Try
    End Function

    Public Function DbRollback() As Boolean
        Dim oraTran As OracleTransaction
        Try
            oraTran.Rollback()
            Return True
        Catch ex As Exception
            MessageBox.Show("データベースアクセスエラー")
            Return False
        End Try
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

    Private Function DbClose(ByVal constr As String) As Boolean
        Dim oraCon As OracleConnection
        oraCon = New OracleConnection(constr)
        Try
            oraCon.Close()
            Return True
        Catch ex As Exception
            MessageBox.Show("データベースアクセスエラー")
            Return False
        End Try
    End Function

    Private Function DbTransaction(ByVal constr As String) As Boolean
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

End Class
