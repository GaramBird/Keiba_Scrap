Public Class StartClass
    Shared Sub Main()
        'Mutex名を決める（必ずアプリケーション固有の文字列に変更すること！）
        Dim mutexName As String = "netkeiba.comスクレイピング"
        'Mutexオブジェクトを作成する
        Dim mutex As New System.Threading.Mutex(False, mutexName)

        Dim hasHandle As Boolean = False
        Try
            Try
                'ミューテックスの所有権を要求する
                hasHandle = mutex.WaitOne(0, False)
                '.NET Framework 2.0以降の場合
            Catch ex As System.Threading.AbandonedMutexException
                '別のアプリケーションがミューテックスを解放しないで終了した時
                hasHandle = True
            End Try
            'ミューテックスを得られたか調べる
            If hasHandle = False Then
                '得られなかった場合は、すでに起動していると判断して終了
                MessageBox.Show("既に起動しているアプリケーションを終了してください。", "エラー")
                Return
            End If

            'はじめからMainメソッドにあったコードを実行
            Dim formctrl As ShowForm = New ShowForm
            ShowForm.ShowFormInstance = formctrl
            formctrl.ShowDialog()

        Finally
            If hasHandle Then
                'ミューテックスを解放する
                mutex.ReleaseMutex()
            End If
            mutex.Close()
        End Try

    End Sub

End Class
