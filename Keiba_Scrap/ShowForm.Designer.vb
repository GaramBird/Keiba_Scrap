<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShowForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnGetSyutubahyou = New System.Windows.Forms.Button()
        Me.txtSyutubahyouURL = New System.Windows.Forms.TextBox()
        Me.jikkouBar = New System.Windows.Forms.ProgressBar()
        Me.jikkoumethod = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnGetSyutubahyou
        '
        Me.btnGetSyutubahyou.Location = New System.Drawing.Point(22, 22)
        Me.btnGetSyutubahyou.Name = "btnGetSyutubahyou"
        Me.btnGetSyutubahyou.Size = New System.Drawing.Size(107, 23)
        Me.btnGetSyutubahyou.TabIndex = 1
        Me.btnGetSyutubahyou.Text = "レース情報取得"
        Me.btnGetSyutubahyou.UseVisualStyleBackColor = True
        '
        'txtSyutubahyouURL
        '
        Me.txtSyutubahyouURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSyutubahyouURL.Location = New System.Drawing.Point(135, 24)
        Me.txtSyutubahyouURL.Name = "txtSyutubahyouURL"
        Me.txtSyutubahyouURL.Size = New System.Drawing.Size(531, 19)
        Me.txtSyutubahyouURL.TabIndex = 0
        '
        'jikkouBar
        '
        Me.jikkouBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.jikkouBar.Location = New System.Drawing.Point(-3, 274)
        Me.jikkouBar.Name = "jikkouBar"
        Me.jikkouBar.Size = New System.Drawing.Size(683, 13)
        Me.jikkouBar.TabIndex = 3
        '
        'jikkoumethod
        '
        Me.jikkoumethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.jikkoumethod.BackColor = System.Drawing.SystemColors.Control
        Me.jikkoumethod.Location = New System.Drawing.Point(48, 254)
        Me.jikkoumethod.Margin = New System.Windows.Forms.Padding(0)
        Me.jikkoumethod.Name = "jikkoumethod"
        Me.jikkoumethod.ReadOnly = True
        Me.jikkoumethod.Size = New System.Drawing.Size(632, 19)
        Me.jikkoumethod.TabIndex = 4
        Me.jikkoumethod.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 257)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "実行中："
        '
        'ShowForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(678, 289)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.jikkoumethod)
        Me.Controls.Add(Me.jikkouBar)
        Me.Controls.Add(Me.txtSyutubahyouURL)
        Me.Controls.Add(Me.btnGetSyutubahyou)
        Me.Name = "ShowForm"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnGetSyutubahyou As Button
    Public WithEvents txtSyutubahyouURL As TextBox
    Friend WithEvents Label1 As Label
    Public WithEvents jikkoumethod As TextBox
    Public WithEvents jikkouBar As ProgressBar
End Class
