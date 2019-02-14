Public Class AiLogical
    '各項目について期待値を算出するメソッドを記述するクラス
    'netkeiba.comからスクレイピングしたデータテーブルを取得する。
    Public GetData As SyutubahyouScrap = New SyutubahyouScrap




    '○競馬予想ロジック思案
    'カテゴリ：状態：影響：結果

    '※馬ステータスについて
    '血統：距離・馬場適正に影響
    'スタミナ：短距離に対しては影響少ない
    'スタミナ：長距離に対して影響多い
    '調子：体重の適正値に近づけば+
    '調子：体重の増減値の偏差が小さければ+

    Private Function house_status() As Integer

    End Function

    '▼単カテゴリ項目思案
    '■騎手について
    '騎乗経験：豊富：スタミナ：+
    '騎乗経験：欠乏：スタミナ：-
    '※特定名の騎手に対して定数値を割り振るか→保留
    Private Function kisyu() As Integer

    End Function

    '■馬場適性について
    '馬場適性：高い：スタミナ：++
    '馬場適性：低い：スタミナ：--
    '馬場適性：高い：タイム：+
    '馬場適性：低い：タイム：-
    '不良馬場：得意：スタミナ：-
    '不良馬場：不得意：スタミナ：--
    '良馬場：得意：スタミナ：++
    '良馬場：不得意：スタミナ：+
    Private Function baba_tekisei() As Integer

    End Function

    '■距離適性について
    '距離適性：高い：スタミナ：++
    '距離適性：高い：タイム：++
    '距離適性：低い：スタミナ：--
    '距離適性：低い：タイム：--
    Private Function kyori_tekisei() As Integer

    End Function

    '■血統特性について
    '※血統は情報不足のため実装しない。
    '将来的に馬名データベースを作成して血統表から適正を算出するロジックを組み込む。
    Private Function blood_tokusei() As Integer

    End Function

    '■天気について
    '天気：晴、曇：スタミナ：なし
    '天気：小雨：スタミナ：-
    '天気：雨：スタミナ：--
    Private Function wether_result() As Integer

    End Function

    '■毛色について
    '※意味ない。多分。
    '※血統ロジックに確立値として組み込むかもしれなくもないかもしれなくもない。多分。
    Private Function keiro_logic() As Integer

    End Function





    '▼相互影響のカテゴリについて思案
    '■脚質の偏りについて
    Private Function regtype_logic() As Integer

    End Function

    '■

    '■



End Class
