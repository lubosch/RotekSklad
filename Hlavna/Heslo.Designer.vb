<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Heslo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.NickDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HesloDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MailDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MailhesloDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PravaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UcetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.UcetTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.UcetTableAdapter()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UcetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.Location = New System.Drawing.Point(86, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Zadajte heslo:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(3, 27)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(258, 20)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.UseSystemPasswordChar = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(176, 72)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 25)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(3, 72)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(94, 25)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Nemám žiadne"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(3, 49)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(93, 17)
        Me.CheckBox1.TabIndex = 7
        Me.CheckBox1.Text = "Zobraziť heslo"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(95, 72)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "Konvert"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NickDataGridViewTextBoxColumn, Me.HesloDataGridViewTextBoxColumn, Me.MailDataGridViewTextBoxColumn, Me.MailhesloDataGridViewTextBoxColumn, Me.PravaDataGridViewTextBoxColumn})
        Me.DataGridView2.DataSource = Me.UcetBindingSource
        Me.DataGridView2.Location = New System.Drawing.Point(-31, 7)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(501, 25)
        Me.DataGridView2.TabIndex = 29
        Me.DataGridView2.Visible = False
        '
        'NickDataGridViewTextBoxColumn
        '
        Me.NickDataGridViewTextBoxColumn.DataPropertyName = "Nick"
        Me.NickDataGridViewTextBoxColumn.HeaderText = "Nick"
        Me.NickDataGridViewTextBoxColumn.Name = "NickDataGridViewTextBoxColumn"
        Me.NickDataGridViewTextBoxColumn.ReadOnly = True
        Me.NickDataGridViewTextBoxColumn.Width = 54
        '
        'HesloDataGridViewTextBoxColumn
        '
        Me.HesloDataGridViewTextBoxColumn.DataPropertyName = "Heslo"
        Me.HesloDataGridViewTextBoxColumn.HeaderText = "Heslo"
        Me.HesloDataGridViewTextBoxColumn.Name = "HesloDataGridViewTextBoxColumn"
        Me.HesloDataGridViewTextBoxColumn.ReadOnly = True
        Me.HesloDataGridViewTextBoxColumn.Width = 59
        '
        'MailDataGridViewTextBoxColumn
        '
        Me.MailDataGridViewTextBoxColumn.DataPropertyName = "Mail"
        Me.MailDataGridViewTextBoxColumn.HeaderText = "Mail"
        Me.MailDataGridViewTextBoxColumn.Name = "MailDataGridViewTextBoxColumn"
        Me.MailDataGridViewTextBoxColumn.ReadOnly = True
        Me.MailDataGridViewTextBoxColumn.Width = 51
        '
        'MailhesloDataGridViewTextBoxColumn
        '
        Me.MailhesloDataGridViewTextBoxColumn.DataPropertyName = "Mail_heslo"
        Me.MailhesloDataGridViewTextBoxColumn.HeaderText = "Mail_heslo"
        Me.MailhesloDataGridViewTextBoxColumn.Name = "MailhesloDataGridViewTextBoxColumn"
        Me.MailhesloDataGridViewTextBoxColumn.ReadOnly = True
        Me.MailhesloDataGridViewTextBoxColumn.Width = 82
        '
        'PravaDataGridViewTextBoxColumn
        '
        Me.PravaDataGridViewTextBoxColumn.DataPropertyName = "Prava"
        Me.PravaDataGridViewTextBoxColumn.HeaderText = "Prava"
        Me.PravaDataGridViewTextBoxColumn.Name = "PravaDataGridViewTextBoxColumn"
        Me.PravaDataGridViewTextBoxColumn.ReadOnly = True
        Me.PravaDataGridViewTextBoxColumn.Width = 60
        '
        'UcetBindingSource
        '
        Me.UcetBindingSource.DataMember = "Ucet"
        Me.UcetBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'UcetTableAdapter
        '
        Me.UcetTableAdapter.ClearBeforeFill = True
        '
        'Heslo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(264, 109)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Heslo"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Heslo"
        Me.TopMost = True
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UcetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents UcetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents UcetTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.UcetTableAdapter
    Friend WithEvents NickDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HesloDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MailDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MailhesloDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PravaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
