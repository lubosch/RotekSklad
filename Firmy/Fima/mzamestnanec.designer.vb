<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mzamestnanec
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.FirmyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.MenoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PriezviskoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NástrojDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VelkostRDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KolkoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RotekBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NástrojDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VelkostRDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KolkoDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RotekTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.RotekTableAdapter()
        Me.FirmyTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.FirmyTableAdapter()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.NastrojDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VelkostSDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenaDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RegalDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZosrotDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SrotcenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SkladBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SkladTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.SkladTableAdapter()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NástrojDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VelkostRDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Vlastnost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KolkoDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenaDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Spolu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FirmyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SkladBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Monotype Corsiva", 26.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.Location = New System.Drawing.Point(510, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 43)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NástrojDataGridViewTextBoxColumn2, Me.VelkostRDataGridViewTextBoxColumn2, Me.Vlastnost, Me.KolkoDataGridViewTextBoxColumn2, Me.CenaDataGridViewTextBoxColumn1, Me.Spolu})
        Me.DataGridView1.DataSource = Me.FirmyBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(-8, 169)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(530, 401)
        Me.DataGridView1.TabIndex = 1
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
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 77)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(113, 38)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Dať nástroj"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(409, 77)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(113, 38)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Zrušiť"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(43, 143)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(141, 20)
        Me.TextBox1.TabIndex = 5
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(308, 136)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(98, 27)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "Zrušenie filtra"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(131, 77)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(103, 37)
        Me.Button4.TabIndex = 7
        Me.Button4.Text = "Vracajú"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(190, 143)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(112, 20)
        Me.TextBox2.TabIndex = 6
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(592, 77)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(102, 37)
        Me.Button5.TabIndex = 9
        Me.Button5.Text = "Požičať si od nich"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'DataGridView3
        '
        Me.DataGridView3.AllowUserToAddRows = False
        Me.DataGridView3.AllowUserToDeleteRows = False
        Me.DataGridView3.AllowUserToOrderColumns = True
        Me.DataGridView3.AutoGenerateColumns = False
        Me.DataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MenoDataGridViewTextBoxColumn, Me.PriezviskoDataGridViewTextBoxColumn, Me.NástrojDataGridViewTextBoxColumn, Me.VelkostRDataGridViewTextBoxColumn, Me.DataGridViewTextBoxColumn1, Me.KolkoDataGridViewTextBoxColumn})
        Me.DataGridView3.DataSource = Me.RotekBindingSource
        Me.DataGridView3.Location = New System.Drawing.Point(515, 164)
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.ReadOnly = True
        Me.DataGridView3.Size = New System.Drawing.Size(400, 150)
        Me.DataGridView3.TabIndex = 10
        '
        'MenoDataGridViewTextBoxColumn
        '
        Me.MenoDataGridViewTextBoxColumn.DataPropertyName = "Meno"
        Me.MenoDataGridViewTextBoxColumn.HeaderText = "Meno"
        Me.MenoDataGridViewTextBoxColumn.Name = "MenoDataGridViewTextBoxColumn"
        Me.MenoDataGridViewTextBoxColumn.ReadOnly = True
        Me.MenoDataGridViewTextBoxColumn.Width = 59
        '
        'PriezviskoDataGridViewTextBoxColumn
        '
        Me.PriezviskoDataGridViewTextBoxColumn.DataPropertyName = "Priezvisko"
        Me.PriezviskoDataGridViewTextBoxColumn.HeaderText = "Priezvisko"
        Me.PriezviskoDataGridViewTextBoxColumn.Name = "PriezviskoDataGridViewTextBoxColumn"
        Me.PriezviskoDataGridViewTextBoxColumn.ReadOnly = True
        Me.PriezviskoDataGridViewTextBoxColumn.Width = 80
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
        Me.VelkostRDataGridViewTextBoxColumn.HeaderText = "Priemer"
        Me.VelkostRDataGridViewTextBoxColumn.Name = "VelkostRDataGridViewTextBoxColumn"
        Me.VelkostRDataGridViewTextBoxColumn.ReadOnly = True
        Me.VelkostRDataGridViewTextBoxColumn.Width = 67
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Vlastnost"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Vlastnosť"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 76
        '
        'KolkoDataGridViewTextBoxColumn
        '
        Me.KolkoDataGridViewTextBoxColumn.DataPropertyName = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn.HeaderText = "Koľko"
        Me.KolkoDataGridViewTextBoxColumn.Name = "KolkoDataGridViewTextBoxColumn"
        Me.KolkoDataGridViewTextBoxColumn.ReadOnly = True
        Me.KolkoDataGridViewTextBoxColumn.Width = 61
        '
        'RotekBindingSource
        '
        Me.RotekBindingSource.DataMember = "Rotek"
        Me.RotekBindingSource.DataSource = Me.RotekDataSet
        '
        'CenaDataGridViewTextBoxColumn
        '
        Me.CenaDataGridViewTextBoxColumn.DataPropertyName = "Cena"
        Me.CenaDataGridViewTextBoxColumn.HeaderText = "Cena"
        Me.CenaDataGridViewTextBoxColumn.Name = "CenaDataGridViewTextBoxColumn"
        '
        'NástrojDataGridViewTextBoxColumn1
        '
        Me.NástrojDataGridViewTextBoxColumn1.DataPropertyName = "Nástroj"
        Me.NástrojDataGridViewTextBoxColumn1.HeaderText = "Nástroj"
        Me.NástrojDataGridViewTextBoxColumn1.Name = "NástrojDataGridViewTextBoxColumn1"
        '
        'VelkostRDataGridViewTextBoxColumn1
        '
        Me.VelkostRDataGridViewTextBoxColumn1.DataPropertyName = "VelkostR"
        Me.VelkostRDataGridViewTextBoxColumn1.HeaderText = "VelkostR"
        Me.VelkostRDataGridViewTextBoxColumn1.Name = "VelkostRDataGridViewTextBoxColumn1"
        Me.VelkostRDataGridViewTextBoxColumn1.Width = 70
        '
        'KolkoDataGridViewTextBoxColumn1
        '
        Me.KolkoDataGridViewTextBoxColumn1.DataPropertyName = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn1.HeaderText = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn1.Name = "KolkoDataGridViewTextBoxColumn1"
        Me.KolkoDataGridViewTextBoxColumn1.Width = 70
        '
        'RotekTableAdapter
        '
        Me.RotekTableAdapter.ClearBeforeFill = True
        '
        'FirmyTableAdapter
        '
        Me.FirmyTableAdapter.ClearBeforeFill = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToOrderColumns = True
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NastrojDataGridViewTextBoxColumn, Me.VelkostSDataGridViewTextBoxColumn, Me.PocetDataGridViewTextBoxColumn, Me.CenaDataGridViewTextBoxColumn2, Me.RegalDataGridViewTextBoxColumn, Me.ZosrotDataGridViewTextBoxColumn, Me.SrotcenaDataGridViewTextBoxColumn, Me.DataGridViewTextBoxColumn2})
        Me.DataGridView2.DataSource = Me.SkladBindingSource
        Me.DataGridView2.Location = New System.Drawing.Point(-8, 200)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(869, 150)
        Me.DataGridView2.TabIndex = 11
        '
        'NastrojDataGridViewTextBoxColumn
        '
        Me.NastrojDataGridViewTextBoxColumn.DataPropertyName = "Nastroj"
        Me.NastrojDataGridViewTextBoxColumn.HeaderText = "Nastroj"
        Me.NastrojDataGridViewTextBoxColumn.Name = "NastrojDataGridViewTextBoxColumn"
        Me.NastrojDataGridViewTextBoxColumn.ReadOnly = True
        '
        'VelkostSDataGridViewTextBoxColumn
        '
        Me.VelkostSDataGridViewTextBoxColumn.DataPropertyName = "VelkostS"
        Me.VelkostSDataGridViewTextBoxColumn.HeaderText = "VelkostS"
        Me.VelkostSDataGridViewTextBoxColumn.Name = "VelkostSDataGridViewTextBoxColumn"
        Me.VelkostSDataGridViewTextBoxColumn.ReadOnly = True
        '
        'PocetDataGridViewTextBoxColumn
        '
        Me.PocetDataGridViewTextBoxColumn.DataPropertyName = "Pocet"
        Me.PocetDataGridViewTextBoxColumn.HeaderText = "Pocet"
        Me.PocetDataGridViewTextBoxColumn.Name = "PocetDataGridViewTextBoxColumn"
        Me.PocetDataGridViewTextBoxColumn.ReadOnly = True
        '
        'CenaDataGridViewTextBoxColumn2
        '
        Me.CenaDataGridViewTextBoxColumn2.DataPropertyName = "Cena"
        Me.CenaDataGridViewTextBoxColumn2.HeaderText = "Cena"
        Me.CenaDataGridViewTextBoxColumn2.Name = "CenaDataGridViewTextBoxColumn2"
        Me.CenaDataGridViewTextBoxColumn2.ReadOnly = True
        '
        'RegalDataGridViewTextBoxColumn
        '
        Me.RegalDataGridViewTextBoxColumn.DataPropertyName = "Regal"
        Me.RegalDataGridViewTextBoxColumn.HeaderText = "Regal"
        Me.RegalDataGridViewTextBoxColumn.Name = "RegalDataGridViewTextBoxColumn"
        Me.RegalDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ZosrotDataGridViewTextBoxColumn
        '
        Me.ZosrotDataGridViewTextBoxColumn.DataPropertyName = "zosrot"
        Me.ZosrotDataGridViewTextBoxColumn.HeaderText = "zosrot"
        Me.ZosrotDataGridViewTextBoxColumn.Name = "ZosrotDataGridViewTextBoxColumn"
        Me.ZosrotDataGridViewTextBoxColumn.ReadOnly = True
        '
        'SrotcenaDataGridViewTextBoxColumn
        '
        Me.SrotcenaDataGridViewTextBoxColumn.DataPropertyName = "srotcena"
        Me.SrotcenaDataGridViewTextBoxColumn.HeaderText = "srotcena"
        Me.SrotcenaDataGridViewTextBoxColumn.Name = "SrotcenaDataGridViewTextBoxColumn"
        Me.SrotcenaDataGridViewTextBoxColumn.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Vlastnost"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Vlastnost"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
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
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(700, 77)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(107, 37)
        Me.Button6.TabIndex = 12
        Me.Button6.Text = "Vrátiť im"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(240, 77)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(93, 38)
        Me.Button7.TabIndex = 13
        Me.Button7.Text = "Exportovať"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(813, 77)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(102, 37)
        Me.Button8.TabIndex = 14
        Me.Button8.Text = "Exportovať"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(43, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Nástroj"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(190, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Priemer"
        '
        'NástrojDataGridViewTextBoxColumn2
        '
        Me.NástrojDataGridViewTextBoxColumn2.DataPropertyName = "Nástroj"
        Me.NástrojDataGridViewTextBoxColumn2.HeaderText = "Nástroj"
        Me.NástrojDataGridViewTextBoxColumn2.Name = "NástrojDataGridViewTextBoxColumn2"
        Me.NástrojDataGridViewTextBoxColumn2.ReadOnly = True
        Me.NástrojDataGridViewTextBoxColumn2.Width = 65
        '
        'VelkostRDataGridViewTextBoxColumn2
        '
        Me.VelkostRDataGridViewTextBoxColumn2.DataPropertyName = "VelkostR"
        Me.VelkostRDataGridViewTextBoxColumn2.HeaderText = "Priemer"
        Me.VelkostRDataGridViewTextBoxColumn2.Name = "VelkostRDataGridViewTextBoxColumn2"
        Me.VelkostRDataGridViewTextBoxColumn2.ReadOnly = True
        Me.VelkostRDataGridViewTextBoxColumn2.Width = 67
        '
        'Vlastnost
        '
        Me.Vlastnost.DataPropertyName = "Vlastnost"
        Me.Vlastnost.HeaderText = "Vlastnosť"
        Me.Vlastnost.Name = "Vlastnost"
        Me.Vlastnost.ReadOnly = True
        Me.Vlastnost.Width = 76
        '
        'KolkoDataGridViewTextBoxColumn2
        '
        Me.KolkoDataGridViewTextBoxColumn2.DataPropertyName = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn2.HeaderText = "Koľko"
        Me.KolkoDataGridViewTextBoxColumn2.Name = "KolkoDataGridViewTextBoxColumn2"
        Me.KolkoDataGridViewTextBoxColumn2.ReadOnly = True
        Me.KolkoDataGridViewTextBoxColumn2.Width = 61
        '
        'CenaDataGridViewTextBoxColumn1
        '
        Me.CenaDataGridViewTextBoxColumn1.DataPropertyName = "Cena"
        Me.CenaDataGridViewTextBoxColumn1.HeaderText = "Cena [€]"
        Me.CenaDataGridViewTextBoxColumn1.Name = "CenaDataGridViewTextBoxColumn1"
        Me.CenaDataGridViewTextBoxColumn1.ReadOnly = True
        Me.CenaDataGridViewTextBoxColumn1.Width = 72
        '
        'Spolu
        '
        Me.Spolu.DataPropertyName = "Spolud"
        Me.Spolu.HeaderText = "Spolu"
        Me.Spolu.Name = "Spolu"
        Me.Spolu.ReadOnly = True
        Me.Spolu.Visible = False
        '
        'mzamestnanec
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(927, 590)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.DataGridView3)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "mzamestnanec"
        Me.ShowInTaskbar = False
        Me.Text = "Info o firme"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FirmyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SkladBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents CenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NástrojDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostRDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KolkoDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents RotekBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RotekTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.RotekTableAdapter
    Friend WithEvents FirmyBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FirmyTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.FirmyTableAdapter
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents SkladBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SkladTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.SkladTableAdapter
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents MenoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PriezviskoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NástrojDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostRDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KolkoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NastrojDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostSDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenaDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RegalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZosrotDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrotcenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NástrojDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostRDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Vlastnost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KolkoDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenaDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Spolu As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
