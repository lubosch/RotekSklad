<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DL
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ZakazkaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.ZakazkaTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.ZakazkaTableAdapter()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.UlicaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MestDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PSČDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KrajinaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VeducDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ICO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZoznamFBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ZoznamFTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.ZoznamFTableAdapter()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker3 = New System.Windows.Forms.DateTimePicker()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PodzakazkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PotvrdenieDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RozpracDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Zakaznik = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Veduci = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Zakazkasdw = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Objednavka = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cena = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ZakazkaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ZoznamFBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox1.Location = New System.Drawing.Point(152, 19)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(132, 20)
        Me.TextBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.Location = New System.Drawing.Point(64, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Dodací list č."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label2.Location = New System.Drawing.Point(101, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Dátum"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label4.Location = New System.Drawing.Point(-1, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(149, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Tovar vyskladnený dňa"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(129, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Tovar odoslaný dňa"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label6.Location = New System.Drawing.Point(39, 153)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(109, 16)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Spôsob dopravy"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(154, 152)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(132, 20)
        Me.TextBox6.TabIndex = 5
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(154, 179)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(132, 20)
        Me.TextBox7.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label7.Location = New System.Drawing.Point(53, 180)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 16)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Miesto určenia"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.PodzakazkaDataGridViewTextBoxColumn, Me.PotvrdenieDataGridViewTextBoxColumn, Me.RozpracDataGridViewTextBoxColumn, Me.Column2, Me.Column4, Me.DataGridViewTextBoxColumn1, Me.Zakaznik, Me.Veduci, Me.Zakazkasdw, Me.Objednavka, Me.Cena})
        Me.DataGridView1.DataSource = Me.ZakazkaBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(8, 245)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 20
        Me.DataGridView1.Size = New System.Drawing.Size(405, 390)
        Me.DataGridView1.TabIndex = 14
        '
        'ZakazkaBindingSource
        '
        Me.ZakazkaBindingSource.DataMember = "Zakazka"
        Me.ZakazkaBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ZakazkaTableAdapter
        '
        Me.ZakazkaTableAdapter.ClearBeforeFill = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(134, 222)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(67, 17)
        Me.RadioButton1.TabIndex = 9
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Zákazka"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(134, 205)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(55, 17)
        Me.RadioButton2.TabIndex = 8
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Dielce"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DateTimePicker1.Location = New System.Drawing.Point(152, 72)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(132, 20)
        Me.DateTimePicker1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button1.Location = New System.Drawing.Point(163, 723)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 28)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Vytvor"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox2.Location = New System.Drawing.Point(123, 643)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 644)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(109, 16)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Ev. č. automobilu"
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(123, 670)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(100, 20)
        Me.TextBox8.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label9.Location = New System.Drawing.Point(42, 671)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 16)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "Vyskladnil"
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(124, 697)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(100, 20)
        Me.TextBox9.TabIndex = 12
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label10.Location = New System.Drawing.Point(36, 698)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 16)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Kontroloval"
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UlicaDataGridViewTextBoxColumn, Me.MestDataGridViewTextBoxColumn, Me.PSČDataGridViewTextBoxColumn, Me.KrajinaDataGridViewTextBoxColumn, Me.PocetDataGridViewTextBoxColumn, Me.VeducDataGridViewTextBoxColumn, Me.ICO})
        Me.DataGridView2.DataSource = Me.ZoznamFBindingSource
        Me.DataGridView2.Location = New System.Drawing.Point(11, 366)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersWidth = 10
        Me.DataGridView2.Size = New System.Drawing.Size(413, 98)
        Me.DataGridView2.TabIndex = 25
        Me.DataGridView2.Visible = False
        '
        'UlicaDataGridViewTextBoxColumn
        '
        Me.UlicaDataGridViewTextBoxColumn.DataPropertyName = "Ulica"
        Me.UlicaDataGridViewTextBoxColumn.HeaderText = "Ulica"
        Me.UlicaDataGridViewTextBoxColumn.Name = "UlicaDataGridViewTextBoxColumn"
        Me.UlicaDataGridViewTextBoxColumn.ReadOnly = True
        Me.UlicaDataGridViewTextBoxColumn.Width = 56
        '
        'MestDataGridViewTextBoxColumn
        '
        Me.MestDataGridViewTextBoxColumn.DataPropertyName = "Mest"
        Me.MestDataGridViewTextBoxColumn.HeaderText = "Mest"
        Me.MestDataGridViewTextBoxColumn.Name = "MestDataGridViewTextBoxColumn"
        Me.MestDataGridViewTextBoxColumn.ReadOnly = True
        Me.MestDataGridViewTextBoxColumn.Width = 55
        '
        'PSČDataGridViewTextBoxColumn
        '
        Me.PSČDataGridViewTextBoxColumn.DataPropertyName = "PSČ"
        Me.PSČDataGridViewTextBoxColumn.HeaderText = "PSČ"
        Me.PSČDataGridViewTextBoxColumn.Name = "PSČDataGridViewTextBoxColumn"
        Me.PSČDataGridViewTextBoxColumn.ReadOnly = True
        Me.PSČDataGridViewTextBoxColumn.Width = 53
        '
        'KrajinaDataGridViewTextBoxColumn
        '
        Me.KrajinaDataGridViewTextBoxColumn.DataPropertyName = "Krajina"
        Me.KrajinaDataGridViewTextBoxColumn.HeaderText = "Krajina"
        Me.KrajinaDataGridViewTextBoxColumn.Name = "KrajinaDataGridViewTextBoxColumn"
        Me.KrajinaDataGridViewTextBoxColumn.ReadOnly = True
        Me.KrajinaDataGridViewTextBoxColumn.Width = 64
        '
        'PocetDataGridViewTextBoxColumn
        '
        Me.PocetDataGridViewTextBoxColumn.DataPropertyName = "pocet"
        Me.PocetDataGridViewTextBoxColumn.HeaderText = "pocet"
        Me.PocetDataGridViewTextBoxColumn.Name = "PocetDataGridViewTextBoxColumn"
        Me.PocetDataGridViewTextBoxColumn.ReadOnly = True
        Me.PocetDataGridViewTextBoxColumn.Width = 59
        '
        'VeducDataGridViewTextBoxColumn
        '
        Me.VeducDataGridViewTextBoxColumn.DataPropertyName = "Veduc"
        Me.VeducDataGridViewTextBoxColumn.HeaderText = "Veduc"
        Me.VeducDataGridViewTextBoxColumn.Name = "VeducDataGridViewTextBoxColumn"
        Me.VeducDataGridViewTextBoxColumn.ReadOnly = True
        Me.VeducDataGridViewTextBoxColumn.Width = 63
        '
        'ICO
        '
        Me.ICO.DataPropertyName = "ICO"
        Me.ICO.HeaderText = "ICO"
        Me.ICO.Name = "ICO"
        Me.ICO.ReadOnly = True
        Me.ICO.Width = 50
        '
        'ZoznamFBindingSource
        '
        Me.ZoznamFBindingSource.DataMember = "ZoznamF"
        Me.ZoznamFBindingSource.DataSource = Me.RotekDataSet
        '
        'ZoznamFTableAdapter
        '
        Me.ZoznamFTableAdapter.ClearBeforeFill = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(154, 98)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(132, 20)
        Me.DateTimePicker2.TabIndex = 3
        Me.DateTimePicker2.Value = New Date(1800, 1, 1, 0, 0, 0, 0)
        '
        'DateTimePicker3
        '
        Me.DateTimePicker3.Location = New System.Drawing.Point(154, 126)
        Me.DateTimePicker3.Name = "DateTimePicker3"
        Me.DateTimePicker3.Size = New System.Drawing.Size(132, 20)
        Me.DateTimePicker3.TabIndex = 4
        Me.DateTimePicker3.Value = New Date(1800, 1, 1, 0, 0, 0, 0)
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox4.Location = New System.Drawing.Point(152, 45)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(132, 20)
        Me.TextBox4.TabIndex = 26
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label11.Location = New System.Drawing.Point(34, 46)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(112, 16)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "Číslo objednávky"
        '
        'Column1
        '
        Me.Column1.HeaderText = "Túto"
        Me.Column1.Name = "Column1"
        Me.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column1.Width = 40
        '
        'PodzakazkaDataGridViewTextBoxColumn
        '
        Me.PodzakazkaDataGridViewTextBoxColumn.DataPropertyName = "Podzakazka"
        Me.PodzakazkaDataGridViewTextBoxColumn.HeaderText = "Názov a druh tovaru"
        Me.PodzakazkaDataGridViewTextBoxColumn.Name = "PodzakazkaDataGridViewTextBoxColumn"
        Me.PodzakazkaDataGridViewTextBoxColumn.Width = 91
        '
        'PotvrdenieDataGridViewTextBoxColumn
        '
        Me.PotvrdenieDataGridViewTextBoxColumn.DataPropertyName = "Potvrdenie"
        Me.PotvrdenieDataGridViewTextBoxColumn.HeaderText = "Potvrdenie"
        Me.PotvrdenieDataGridViewTextBoxColumn.Name = "PotvrdenieDataGridViewTextBoxColumn"
        Me.PotvrdenieDataGridViewTextBoxColumn.Visible = False
        Me.PotvrdenieDataGridViewTextBoxColumn.Width = 83
        '
        'RozpracDataGridViewTextBoxColumn
        '
        Me.RozpracDataGridViewTextBoxColumn.DataPropertyName = "Rozprac"
        Me.RozpracDataGridViewTextBoxColumn.HeaderText = "Rozprac"
        Me.RozpracDataGridViewTextBoxColumn.Name = "RozpracDataGridViewTextBoxColumn"
        Me.RozpracDataGridViewTextBoxColumn.Visible = False
        Me.RozpracDataGridViewTextBoxColumn.Width = 72
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "Kusov"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column2.HeaderText = "Množstvo"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 60
        '
        'Column4
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column4.HeaderText = "Cena za jednotku"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 60
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "DL"
        Me.DataGridViewTextBoxColumn1.HeaderText = "DL"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'Zakaznik
        '
        Me.Zakaznik.DataPropertyName = "Zakaznik"
        Me.Zakaznik.HeaderText = "Zakaznik"
        Me.Zakaznik.Name = "Zakaznik"
        Me.Zakaznik.Visible = False
        '
        'Veduci
        '
        Me.Veduci.DataPropertyName = "Veduci"
        Me.Veduci.HeaderText = "Veduci"
        Me.Veduci.Name = "Veduci"
        Me.Veduci.Visible = False
        '
        'Zakazkasdw
        '
        Me.Zakazkasdw.DataPropertyName = "Zakazka"
        Me.Zakazkasdw.HeaderText = "Zakazkaf"
        Me.Zakazkasdw.Name = "Zakazkasdw"
        Me.Zakazkasdw.Visible = False
        '
        'Objednavka
        '
        Me.Objednavka.DataPropertyName = "Objednavka"
        Me.Objednavka.HeaderText = "Objednavka"
        Me.Objednavka.Name = "Objednavka"
        Me.Objednavka.Visible = False
        '
        'Cena
        '
        Me.Cena.DataPropertyName = "Cena"
        Me.Cena.HeaderText = "Cena"
        Me.Cena.Name = "Cena"
        Me.Cena.Visible = False
        '
        'DL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 755)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.DateTimePicker3)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TextBox9)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBox8)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBox7)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "DL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DL"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ZakazkaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ZoznamFBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents ZakazkaBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ZakazkaTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.ZakazkaTableAdapter
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents ZoznamFBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ZoznamFTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.ZoznamFTableAdapter
    Friend WithEvents UlicaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MestDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PSČDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KrajinaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VeducDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ICO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PodzakazkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PotvrdenieDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RozpracDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Zakaznik As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Veduci As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Zakazkasdw As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Objednavka As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cena As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
