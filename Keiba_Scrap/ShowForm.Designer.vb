<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ShowForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnGetSyutubahyou = New System.Windows.Forms.Button()
        Me.txtSyutubahyouURL = New System.Windows.Forms.TextBox()
        Me.jikkouBar = New System.Windows.Forms.ProgressBar()
        Me.jikkoumethod = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvSyutubahyou = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnBrowserView = New System.Windows.Forms.Button()
        Me.btnCSVGridView = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        CType(Me.dgvSyutubahyou, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnGetSyutubahyou
        '
        Me.btnGetSyutubahyou.Location = New System.Drawing.Point(22, 24)
        Me.btnGetSyutubahyou.Name = "btnGetSyutubahyou"
        Me.btnGetSyutubahyou.Size = New System.Drawing.Size(127, 23)
        Me.btnGetSyutubahyou.TabIndex = 1
        Me.btnGetSyutubahyou.TabStop = False
        Me.btnGetSyutubahyou.Text = "レース情報取得"
        Me.btnGetSyutubahyou.UseVisualStyleBackColor = True
        '
        'txtSyutubahyouURL
        '
        Me.txtSyutubahyouURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSyutubahyouURL.Location = New System.Drawing.Point(288, 26)
        Me.txtSyutubahyouURL.Name = "txtSyutubahyouURL"
        Me.txtSyutubahyouURL.Size = New System.Drawing.Size(558, 19)
        Me.txtSyutubahyouURL.TabIndex = 0
        '
        'jikkouBar
        '
        Me.jikkouBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.jikkouBar.Location = New System.Drawing.Point(-3, 524)
        Me.jikkouBar.Maximum = 150
        Me.jikkouBar.Name = "jikkouBar"
        Me.jikkouBar.Size = New System.Drawing.Size(863, 13)
        Me.jikkouBar.TabIndex = 3
        '
        'jikkoumethod
        '
        Me.jikkoumethod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.jikkoumethod.BackColor = System.Drawing.SystemColors.Control
        Me.jikkoumethod.Enabled = False
        Me.jikkoumethod.Location = New System.Drawing.Point(61, 504)
        Me.jikkoumethod.Margin = New System.Windows.Forms.Padding(0)
        Me.jikkoumethod.Name = "jikkoumethod"
        Me.jikkoumethod.ReadOnly = True
        Me.jikkoumethod.Size = New System.Drawing.Size(799, 19)
        Me.jikkoumethod.TabIndex = 4
        Me.jikkoumethod.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 507)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "ステータス："
        '
        'dgvSyutubahyou
        '
        Me.dgvSyutubahyou.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSyutubahyou.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSyutubahyou.Location = New System.Drawing.Point(22, 51)
        Me.dgvSyutubahyou.Name = "dgvSyutubahyou"
        Me.dgvSyutubahyou.ReadOnly = True
        Me.dgvSyutubahyou.RowTemplate.Height = 21
        Me.dgvSyutubahyou.Size = New System.Drawing.Size(824, 450)
        Me.dgvSyutubahyou.TabIndex = 6
        Me.dgvSyutubahyou.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(297, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(243, 12)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "▼netkeiba.comの出馬表URLを入力してください。"
        '
        'btnBrowserView
        '
        Me.btnBrowserView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowserView.Enabled = False
        Me.btnBrowserView.Location = New System.Drawing.Point(155, 1)
        Me.btnBrowserView.Name = "btnBrowserView"
        Me.btnBrowserView.Size = New System.Drawing.Size(127, 23)
        Me.btnBrowserView.TabIndex = 8
        Me.btnBrowserView.TabStop = False
        Me.btnBrowserView.Text = "URLをブラウザで表示"
        Me.btnBrowserView.UseVisualStyleBackColor = True
        '
        'btnCSVGridView
        '
        Me.btnCSVGridView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCSVGridView.Enabled = False
        Me.btnCSVGridView.Location = New System.Drawing.Point(22, 1)
        Me.btnCSVGridView.Name = "btnCSVGridView"
        Me.btnCSVGridView.Size = New System.Drawing.Size(127, 23)
        Me.btnCSVGridView.TabIndex = 9
        Me.btnCSVGridView.TabStop = False
        Me.btnCSVGridView.Text = "CSV出力（未実装）"
        Me.btnCSVGridView.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Location = New System.Drawing.Point(155, 24)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(127, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = "キャンセル（未実装）"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'ShowForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(858, 539)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCSVGridView)
        Me.Controls.Add(Me.btnBrowserView)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dgvSyutubahyou)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.jikkoumethod)
        Me.Controls.Add(Me.jikkouBar)
        Me.Controls.Add(Me.txtSyutubahyouURL)
        Me.Controls.Add(Me.btnGetSyutubahyou)
        Me.Name = "ShowForm"
        Me.Text = "Form1"
        CType(Me.dgvSyutubahyou, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnGetSyutubahyou As Button
    Public WithEvents txtSyutubahyouURL As TextBox
    Friend WithEvents Label1 As Label
    Public WithEvents jikkoumethod As TextBox
    Public WithEvents jikkouBar As ProgressBar
    Friend WithEvents Label2 As Label
    Public WithEvents dgvSyutubahyou As DataGridView
    Friend WithEvents btnBrowserView As Button
    Friend WithEvents btnCSVGridView As Button
    Friend WithEvents btnCancel As Button
End Class
