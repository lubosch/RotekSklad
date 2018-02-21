<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class msrotC
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
        Me.NástrojDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VelkostRDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KolkoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SpoluDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Vlastnost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FirmyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.FirmyTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.FirmyTableAdapter()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.SkladBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SkladTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.SkladTableAdapter()
        Me.NastrojDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RegalDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenaDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pocet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VelkostSDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.zosrot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.srotcena = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VlastnostDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FirmyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SkladBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(11, 201)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 25)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Zrušiť"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(120, 202)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(117, 24)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Vrátiť"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(409, 23)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 3
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NástrojDataGridViewTextBoxColumn, Me.VelkostRDataGridViewTextBoxColumn, Me.PocetDataGridViewTextBoxColumn, Me.KolkoDataGridViewTextBoxColumn, Me.SpoluDataGridViewTextBoxColumn, Me.CenaDataGridViewTextBoxColumn, Me.Vlastnost})
        Me.DataGridView1.DataSource = Me.FirmyBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(-3, 101)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(496, 34)
        Me.DataGridView1.TabIndex = 4
        Me.DataGridView1.Visible = False
        '
        'NástrojDataGridViewTextBoxColumn
        '
        Me.NástrojDataGridViewTextBoxColumn.DataPropertyName = "Nástroj"
        Me.NástrojDataGridViewTextBoxColumn.HeaderText = "Nástroj"
        Me.NástrojDataGridViewTextBoxColumn.Name = "NástrojDataGridViewTextBoxColumn"
        Me.NástrojDataGridViewTextBoxColumn.ReadOnly = True
        Me.NástrojDataGridViewTextBoxColumn.Width = 65
        '
        'VelkostRDataGridViewTextBoxColumn
        '
        Me.VelkostRDataGridViewTextBoxColumn.DataPropertyName = "VelkostR"
        Me.VelkostRDataGridViewTextBoxColumn.HeaderText = "VelkostR"
        Me.VelkostRDataGridViewTextBoxColumn.Name = "VelkostRDataGridViewTextBoxColumn"
        Me.VelkostRDataGridViewTextBoxColumn.ReadOnly = True
        Me.VelkostRDataGridViewTextBoxColumn.Width = 75
        '
        'PocetDataGridViewTextBoxColumn
        '
        Me.PocetDataGridViewTextBoxColumn.DataPropertyName = "pocet"
        Me.PocetDataGridViewTextBoxColumn.HeaderText = "pocet"
        Me.PocetDataGridViewTextBoxColumn.Name = "PocetDataGridViewTextBoxColumn"
        Me.PocetDataGridViewTextBoxColumn.ReadOnly = True
        Me.PocetDataGridViewTextBoxColumn.Width = 59
        '
        'KolkoDataGridViewTextBoxColumn
        '
        Me.KolkoDataGridViewTextBoxColumn.DataPropertyName = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn.HeaderText = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn.Name = "KolkoDataGridViewTextBoxColumn"
        Me.KolkoDataGridViewTextBoxColumn.ReadOnly = True
        Me.KolkoDataGridViewTextBoxColumn.Width = 59
        '
        'SpoluDataGridViewTextBoxColumn
        '
        Me.SpoluDataGridViewTextBoxColumn.DataPropertyName = "Spolu"
        Me.SpoluDataGridViewTextBoxColumn.HeaderText = "Spolu"
        Me.SpoluDataGridViewTextBoxColumn.Name = "SpoluDataGridViewTextBoxColumn"
        Me.SpoluDataGridViewTextBoxColumn.ReadOnly = True
        Me.SpoluDataGridViewTextBoxColumn.Width = 59
        '
        'CenaDataGridViewTextBoxColumn
        '
        Me.CenaDataGridViewTextBoxColumn.DataPropertyName = "Cena"
        Me.CenaDataGridViewTextBoxColumn.HeaderText = "Cena"
        Me.CenaDataGridViewTextBoxColumn.Name = "CenaDataGridViewTextBoxColumn"
        Me.CenaDataGridViewTextBoxColumn.ReadOnly = True
        Me.CenaDataGridViewTextBoxColumn.Width = 57
        '
        'Vlastnost
        '
        Me.Vlastnost.DataPropertyName = "Vlastnost"
        Me.Vlastnost.HeaderText = "Vlastnost"
        Me.Vlastnost.Name = "Vlastnost"
        Me.Vlastnost.ReadOnly = True
        Me.Vlastnost.Width = 75
        '
        'FirmyBindingSource
        '
        Me.FirmyBindingSource.DataMember = "Firmy"
        Me.FirmyBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(11, 23)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(143, 20)
        Me.TextBox2.TabIndex = 0
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(12, 49)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(143, 147)
        Me.ListBox1.TabIndex = 6
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(161, 23)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(115, 20)
        Me.TextBox3.TabIndex = 1
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(162, 49)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(114, 147)
        Me.ListBox2.TabIndex = 8
        '
        'FirmyTableAdapter
        '
        Me.FirmyTableAdapter.ClearBeforeFill = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(406, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Počet"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Druh nástroja"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(162, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Priemer"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(283, 23)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(120, 20)
        Me.TextBox4.TabIndex = 2
        '
        'ListBox3
        '
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.Location = New System.Drawing.Point(283, 49)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(120, 147)
        Me.ListBox3.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(283, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Vlastnosť"
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NastrojDataGridViewTextBoxColumn, Me.RegalDataGridViewTextBoxColumn, Me.CenaDataGridViewTextBoxColumn1, Me.Pocet, Me.VelkostSDataGridViewTextBoxColumn, Me.zosrot, Me.srotcena, Me.VlastnostDataGridViewTextBoxColumn})
        Me.DataGridView2.DataSource = Me.SkladBindingSource
        Me.DataGridView2.Location = New System.Drawing.Point(-3, 141)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersWidth = 4
        Me.DataGridView2.Size = New System.Drawing.Size(512, 38)
        Me.DataGridView2.TabIndex = 15
        Me.DataGridView2.Visible = False
        '
        'SkladBindingSource
        '
        Me.SkladBindingSource.DataMember = "Sklad"
        Me.SkladBindingSource.DataSource = Me.RotekDataSet
        '
        'SkladTableAdapter
        '
        Me.SkladTableAdapter.ClearBeforeFill = True
        '
        'NastrojDataGridViewTextBoxColumn
        '
        Me.NastrojDataGridViewTextBoxColumn.DataPropertyName = "Nastroj"
        Me.NastrojDataGridViewTextBoxColumn.HeaderText = "Nastroj"
        Me.NastrojDataGridViewTextBoxColumn.Name = "NastrojDataGridViewTextBoxColumn"
        Me.NastrojDataGridViewTextBoxColumn.ReadOnly = True
        Me.NastrojDataGridViewTextBoxColumn.Width = 65
        '
        'RegalDataGridViewTextBoxColumn
        '
        Me.RegalDataGridViewTextBoxColumn.DataPropertyName = "Regal"
        Me.RegalDataGridViewTextBoxColumn.HeaderText = "Regal"
        Me.RegalDataGridViewTextBoxColumn.Name = "RegalDataGridViewTextBoxColumn"
        Me.RegalDataGridViewTextBoxColumn.ReadOnly = True
        Me.RegalDataGridViewTextBoxColumn.Width = 60
        '
        'CenaDataGridViewTextBoxColumn1
        '
        Me.CenaDataGridViewTextBoxColumn1.DataPropertyName = "Cena"
        Me.CenaDataGridViewTextBoxColumn1.HeaderText = "Cena"
        Me.CenaDataGridViewTextBoxColumn1.Name = "CenaDataGridViewTextBoxColumn1"
        Me.CenaDataGridViewTextBoxColumn1.ReadOnly = True
        Me.CenaDataGridViewTextBoxColumn1.Width = 57
        '
        'Pocet
        '
        Me.Pocet.DataPropertyName = "Pocet"
        Me.Pocet.HeaderText = "Pocet"
        Me.Pocet.Name = "Pocet"
        Me.Pocet.ReadOnly = True
        Me.Pocet.Width = 60
        '
        'VelkostSDataGridViewTextBoxColumn
        '
        Me.VelkostSDataGridViewTextBoxColumn.DataPropertyName = "VelkostS"
        Me.VelkostSDataGridViewTextBoxColumn.HeaderText = "VelkostS"
        Me.VelkostSDataGridViewTextBoxColumn.Name = "VelkostSDataGridViewTextBoxColumn"
        Me.VelkostSDataGridViewTextBoxColumn.ReadOnly = True
        Me.VelkostSDataGridViewTextBoxColumn.Width = 74
        '
        'zosrot
        '
        Me.zosrot.DataPropertyName = "zosrot"
        Me.zosrot.HeaderText = "zosrot"
        Me.zosrot.Name = "zosrot"
        Me.zosrot.ReadOnly = True
        Me.zosrot.Width = 60
        '
        'srotcena
        '
        Me.srotcena.DataPropertyName = "srotcena"
        Me.srotcena.HeaderText = "srotcena"
        Me.srotcena.Name = "srotcena"
        Me.srotcena.ReadOnly = True
        Me.srotcena.Width = 73
        '
        'VlastnostDataGridViewTextBoxColumn
        '
        Me.VlastnostDataGridViewTextBoxColumn.DataPropertyName = "Vlastnost"
        Me.VlastnostDataGridViewTextBoxColumn.HeaderText = "Vlastnost"
        Me.VlastnostDataGridViewTextBoxColumn.Name = "VlastnostDataGridViewTextBoxColumn"
        Me.VlastnostDataGridViewTextBoxColumn.ReadOnly = True
        Me.VlastnostDataGridViewTextBoxColumn.Width = 75
        '
        'msrotC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 230)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "msrotC"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Vrátiť"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FirmyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SkladBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FirmyBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FirmyTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.FirmyTableAdapter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents ListBox3 As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NástrojDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostRDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KolkoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SpoluDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Vlastnost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents SkladBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SkladTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.SkladTableAdapter
    Friend WithEvents NastrojDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RegalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenaDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pocet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostSDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents zosrot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents srotcena As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VlastnostDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
