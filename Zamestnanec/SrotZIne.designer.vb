<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SrotZIne
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.IdentDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NazovDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KolkoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SrotDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SrotcenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fotka = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IneBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.IneTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.IneTableAdapter()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.VelkostRDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NástrojDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PriezviskoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenprDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KolkoDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SpoluDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SrotDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SrotcenaDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RotekBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.RotekTableAdapter()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IneBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(13, 194)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 25)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Zrušiť"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(121, 194)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(117, 24)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Zničiť"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(322, 15)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 2
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdentDataGridViewTextBoxColumn, Me.NazovDataGridViewTextBoxColumn, Me.CenaDataGridViewTextBoxColumn, Me.KolkoDataGridViewTextBoxColumn, Me.SrotDataGridViewTextBoxColumn, Me.SrotcenaDataGridViewTextBoxColumn, Me.Fotka})
        Me.DataGridView1.DataSource = Me.IneBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(-5, 71)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(610, 54)
        Me.DataGridView1.TabIndex = 4
        '
        'IdentDataGridViewTextBoxColumn
        '
        Me.IdentDataGridViewTextBoxColumn.DataPropertyName = "Ident"
        Me.IdentDataGridViewTextBoxColumn.HeaderText = "Ident"
        Me.IdentDataGridViewTextBoxColumn.Name = "IdentDataGridViewTextBoxColumn"
        Me.IdentDataGridViewTextBoxColumn.ReadOnly = True
        Me.IdentDataGridViewTextBoxColumn.Width = 50
        '
        'NazovDataGridViewTextBoxColumn
        '
        Me.NazovDataGridViewTextBoxColumn.DataPropertyName = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.HeaderText = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.Name = "NazovDataGridViewTextBoxColumn"
        Me.NazovDataGridViewTextBoxColumn.ReadOnly = True
        Me.NazovDataGridViewTextBoxColumn.Width = 50
        '
        'CenaDataGridViewTextBoxColumn
        '
        Me.CenaDataGridViewTextBoxColumn.DataPropertyName = "Cena"
        Me.CenaDataGridViewTextBoxColumn.HeaderText = "Cena"
        Me.CenaDataGridViewTextBoxColumn.Name = "CenaDataGridViewTextBoxColumn"
        Me.CenaDataGridViewTextBoxColumn.ReadOnly = True
        Me.CenaDataGridViewTextBoxColumn.Width = 50
        '
        'KolkoDataGridViewTextBoxColumn
        '
        Me.KolkoDataGridViewTextBoxColumn.DataPropertyName = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn.HeaderText = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn.Name = "KolkoDataGridViewTextBoxColumn"
        Me.KolkoDataGridViewTextBoxColumn.ReadOnly = True
        '
        'SrotDataGridViewTextBoxColumn
        '
        Me.SrotDataGridViewTextBoxColumn.DataPropertyName = "Srot"
        Me.SrotDataGridViewTextBoxColumn.HeaderText = "Srot"
        Me.SrotDataGridViewTextBoxColumn.Name = "SrotDataGridViewTextBoxColumn"
        Me.SrotDataGridViewTextBoxColumn.ReadOnly = True
        Me.SrotDataGridViewTextBoxColumn.Width = 50
        '
        'SrotcenaDataGridViewTextBoxColumn
        '
        Me.SrotcenaDataGridViewTextBoxColumn.DataPropertyName = "Srotcena"
        Me.SrotcenaDataGridViewTextBoxColumn.HeaderText = "Srotcena"
        Me.SrotcenaDataGridViewTextBoxColumn.Name = "SrotcenaDataGridViewTextBoxColumn"
        Me.SrotcenaDataGridViewTextBoxColumn.ReadOnly = True
        Me.SrotcenaDataGridViewTextBoxColumn.Width = 50
        '
        'Fotka
        '
        Me.Fotka.DataPropertyName = "Fotka"
        Me.Fotka.HeaderText = "Fotka"
        Me.Fotka.Name = "Fotka"
        Me.Fotka.ReadOnly = True
        '
        'IneBindingSource
        '
        Me.IneBindingSource.DataMember = "Ine"
        Me.IneBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(13, 15)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(143, 20)
        Me.TextBox2.TabIndex = 0
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(13, 41)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(143, 147)
        Me.ListBox1.TabIndex = 6
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(162, 15)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(154, 20)
        Me.TextBox3.TabIndex = 1
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(162, 41)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(153, 147)
        Me.ListBox2.TabIndex = 8
        '
        'IneTableAdapter
        '
        Me.IneTableAdapter.ClearBeforeFill = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(162, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Názov"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(323, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Počet"
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.VelkostRDataGridViewTextBoxColumn, Me.NástrojDataGridViewTextBoxColumn, Me.MenoDataGridViewTextBoxColumn, Me.PriezviskoDataGridViewTextBoxColumn, Me.PocetDataGridViewTextBoxColumn, Me.MenprDataGridViewTextBoxColumn, Me.KolkoDataGridViewTextBoxColumn1, Me.SpoluDataGridViewTextBoxColumn, Me.SrotDataGridViewTextBoxColumn1, Me.SrotcenaDataGridViewTextBoxColumn1})
        Me.DataGridView2.DataSource = Me.RotekBindingSource
        Me.DataGridView2.Location = New System.Drawing.Point(-5, 117)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(1125, 56)
        Me.DataGridView2.TabIndex = 12
        '
        'VelkostRDataGridViewTextBoxColumn
        '
        Me.VelkostRDataGridViewTextBoxColumn.DataPropertyName = "VelkostR"
        Me.VelkostRDataGridViewTextBoxColumn.HeaderText = "VelkostR"
        Me.VelkostRDataGridViewTextBoxColumn.Name = "VelkostRDataGridViewTextBoxColumn"
        Me.VelkostRDataGridViewTextBoxColumn.ReadOnly = True
        '
        'NástrojDataGridViewTextBoxColumn
        '
        Me.NástrojDataGridViewTextBoxColumn.DataPropertyName = "Nástroj"
        Me.NástrojDataGridViewTextBoxColumn.HeaderText = "Nástroj"
        Me.NástrojDataGridViewTextBoxColumn.Name = "NástrojDataGridViewTextBoxColumn"
        Me.NástrojDataGridViewTextBoxColumn.ReadOnly = True
        '
        'MenoDataGridViewTextBoxColumn
        '
        Me.MenoDataGridViewTextBoxColumn.DataPropertyName = "Meno"
        Me.MenoDataGridViewTextBoxColumn.HeaderText = "Meno"
        Me.MenoDataGridViewTextBoxColumn.Name = "MenoDataGridViewTextBoxColumn"
        Me.MenoDataGridViewTextBoxColumn.ReadOnly = True
        '
        'PriezviskoDataGridViewTextBoxColumn
        '
        Me.PriezviskoDataGridViewTextBoxColumn.DataPropertyName = "Priezvisko"
        Me.PriezviskoDataGridViewTextBoxColumn.HeaderText = "Priezvisko"
        Me.PriezviskoDataGridViewTextBoxColumn.Name = "PriezviskoDataGridViewTextBoxColumn"
        Me.PriezviskoDataGridViewTextBoxColumn.ReadOnly = True
        '
        'PocetDataGridViewTextBoxColumn
        '
        Me.PocetDataGridViewTextBoxColumn.DataPropertyName = "pocet"
        Me.PocetDataGridViewTextBoxColumn.HeaderText = "pocet"
        Me.PocetDataGridViewTextBoxColumn.Name = "PocetDataGridViewTextBoxColumn"
        Me.PocetDataGridViewTextBoxColumn.ReadOnly = True
        '
        'MenprDataGridViewTextBoxColumn
        '
        Me.MenprDataGridViewTextBoxColumn.DataPropertyName = "Menpr"
        Me.MenprDataGridViewTextBoxColumn.HeaderText = "Menpr"
        Me.MenprDataGridViewTextBoxColumn.Name = "MenprDataGridViewTextBoxColumn"
        Me.MenprDataGridViewTextBoxColumn.ReadOnly = True
        '
        'KolkoDataGridViewTextBoxColumn1
        '
        Me.KolkoDataGridViewTextBoxColumn1.DataPropertyName = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn1.HeaderText = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn1.Name = "KolkoDataGridViewTextBoxColumn1"
        Me.KolkoDataGridViewTextBoxColumn1.ReadOnly = True
        '
        'SpoluDataGridViewTextBoxColumn
        '
        Me.SpoluDataGridViewTextBoxColumn.DataPropertyName = "Spolu"
        Me.SpoluDataGridViewTextBoxColumn.HeaderText = "Spolu"
        Me.SpoluDataGridViewTextBoxColumn.Name = "SpoluDataGridViewTextBoxColumn"
        Me.SpoluDataGridViewTextBoxColumn.ReadOnly = True
        '
        'SrotDataGridViewTextBoxColumn1
        '
        Me.SrotDataGridViewTextBoxColumn1.DataPropertyName = "srot"
        Me.SrotDataGridViewTextBoxColumn1.HeaderText = "srot"
        Me.SrotDataGridViewTextBoxColumn1.Name = "SrotDataGridViewTextBoxColumn1"
        Me.SrotDataGridViewTextBoxColumn1.ReadOnly = True
        '
        'SrotcenaDataGridViewTextBoxColumn1
        '
        Me.SrotcenaDataGridViewTextBoxColumn1.DataPropertyName = "srotcena"
        Me.SrotcenaDataGridViewTextBoxColumn1.HeaderText = "srotcena"
        Me.SrotcenaDataGridViewTextBoxColumn1.Name = "SrotcenaDataGridViewTextBoxColumn1"
        Me.SrotcenaDataGridViewTextBoxColumn1.ReadOnly = True
        '
        'RotekBindingSource
        '
        Me.RotekBindingSource.DataMember = "Rotek"
        Me.RotekBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekTableAdapter
        '
        Me.RotekTableAdapter.ClearBeforeFill = True
        '
        'SrotZIne
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(430, 230)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "SrotZIne"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zošrotovať"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IneBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents IneBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents IneTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.IneTableAdapter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RotekTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.RotekTableAdapter
    Friend WithEvents IdentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NazovDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KolkoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrotDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrotcenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fotka As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostRDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NástrojDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MenoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PriezviskoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MenprDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KolkoDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SpoluDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrotDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrotcenaDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
