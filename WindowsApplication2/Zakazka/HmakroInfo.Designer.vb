<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HmakroInfo
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
        Me.Druh = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nazov = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Typ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sirka = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Rozmer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.S_rozmer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Velkost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cena = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Hustota = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.srot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.srotcena = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DruhDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NazovDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HustotaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RozmerDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VelkostDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VahaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZakazkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SrotDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SrotcenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HutaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.HutaTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.HutaTableAdapter()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.PodzakazkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Razy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kusov = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Meno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Objednavka = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Zakaznik = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_prijatia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZakazkaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ZakazkaTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.ZakazkaTableAdapter()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HutaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ZakazkaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Viner Hand ITC", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(211, -2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 55)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Druh, Me.Nazov, Me.Typ, Me.Sirka, Me.Rozmer, Me.S_rozmer, Me.Velkost, Me.Cena, Me.Hustota, Me.srot, Me.srotcena, Me.Column1, Me.IDDataGridViewTextBoxColumn, Me.DruhDataGridViewTextBoxColumn, Me.NazovDataGridViewTextBoxColumn, Me.HustotaDataGridViewTextBoxColumn, Me.RozmerDataGridViewTextBoxColumn, Me.VelkostDataGridViewTextBoxColumn, Me.VahaDataGridViewTextBoxColumn, Me.ZakazkaDataGridViewTextBoxColumn, Me.CenaDataGridViewTextBoxColumn, Me.PocetDataGridViewTextBoxColumn, Me.SrotDataGridViewTextBoxColumn, Me.SrotcenaDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.HutaBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(-2, 96)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(644, 189)
        Me.DataGridView1.TabIndex = 1
        '
        'Druh
        '
        Me.Druh.DataPropertyName = "Druh"
        Me.Druh.HeaderText = "Druh"
        Me.Druh.Name = "Druh"
        Me.Druh.ReadOnly = True
        '
        'Nazov
        '
        Me.Nazov.DataPropertyName = "Nazov"
        Me.Nazov.HeaderText = "Názov"
        Me.Nazov.Name = "Nazov"
        Me.Nazov.ReadOnly = True
        '
        'Typ
        '
        Me.Typ.DataPropertyName = "Typ"
        Me.Typ.HeaderText = "Typ"
        Me.Typ.Name = "Typ"
        Me.Typ.ReadOnly = True
        '
        'Sirka
        '
        Me.Sirka.DataPropertyName = "Sirka"
        Me.Sirka.HeaderText = "Šírka"
        Me.Sirka.Name = "Sirka"
        Me.Sirka.ReadOnly = True
        '
        'Rozmer
        '
        Me.Rozmer.DataPropertyName = "Rozmer"
        Me.Rozmer.HeaderText = "Rozmer"
        Me.Rozmer.Name = "Rozmer"
        Me.Rozmer.ReadOnly = True
        '
        'S_rozmer
        '
        Me.S_rozmer.DataPropertyName = "S_rozmer"
        Me.S_rozmer.HeaderText = "S_rozmer"
        Me.S_rozmer.Name = "S_rozmer"
        Me.S_rozmer.ReadOnly = True
        '
        'Velkost
        '
        Me.Velkost.DataPropertyName = "Velkost"
        Me.Velkost.HeaderText = "Dĺžka"
        Me.Velkost.Name = "Velkost"
        Me.Velkost.ReadOnly = True
        '
        'Cena
        '
        Me.Cena.DataPropertyName = "Cena"
        Me.Cena.HeaderText = "Cena [€]"
        Me.Cena.Name = "Cena"
        Me.Cena.ReadOnly = True
        Me.Cena.Visible = False
        '
        'Hustota
        '
        Me.Hustota.DataPropertyName = "Hustota"
        Me.Hustota.HeaderText = "Hustota"
        Me.Hustota.Name = "Hustota"
        Me.Hustota.ReadOnly = True
        Me.Hustota.Visible = False
        '
        'srot
        '
        Me.srot.DataPropertyName = "srot"
        Me.srot.HeaderText = "Použité [Kg]"
        Me.srot.Name = "srot"
        Me.srot.ReadOnly = True
        Me.srot.Visible = False
        '
        'srotcena
        '
        Me.srotcena.DataPropertyName = "srotcena"
        Me.srotcena.HeaderText = "Pou6"
        Me.srotcena.Name = "srotcena"
        Me.srotcena.ReadOnly = True
        Me.srotcena.Visible = False
        '
        'Column1
        '
        Me.Column1.HeaderText = "Vymazať"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Text = "Vymazať"
        Me.Column1.UseColumnTextForButtonValue = True
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn.Visible = False
        '
        'DruhDataGridViewTextBoxColumn
        '
        Me.DruhDataGridViewTextBoxColumn.DataPropertyName = "Druh"
        Me.DruhDataGridViewTextBoxColumn.HeaderText = "Druh"
        Me.DruhDataGridViewTextBoxColumn.Name = "DruhDataGridViewTextBoxColumn"
        Me.DruhDataGridViewTextBoxColumn.ReadOnly = True
        Me.DruhDataGridViewTextBoxColumn.Visible = False
        '
        'NazovDataGridViewTextBoxColumn
        '
        Me.NazovDataGridViewTextBoxColumn.DataPropertyName = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.HeaderText = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.Name = "NazovDataGridViewTextBoxColumn"
        Me.NazovDataGridViewTextBoxColumn.ReadOnly = True
        Me.NazovDataGridViewTextBoxColumn.Visible = False
        '
        'HustotaDataGridViewTextBoxColumn
        '
        Me.HustotaDataGridViewTextBoxColumn.DataPropertyName = "Hustota"
        Me.HustotaDataGridViewTextBoxColumn.HeaderText = "Hustota"
        Me.HustotaDataGridViewTextBoxColumn.Name = "HustotaDataGridViewTextBoxColumn"
        Me.HustotaDataGridViewTextBoxColumn.ReadOnly = True
        Me.HustotaDataGridViewTextBoxColumn.Visible = False
        '
        'RozmerDataGridViewTextBoxColumn
        '
        Me.RozmerDataGridViewTextBoxColumn.DataPropertyName = "Rozmer"
        Me.RozmerDataGridViewTextBoxColumn.HeaderText = "Rozmer"
        Me.RozmerDataGridViewTextBoxColumn.Name = "RozmerDataGridViewTextBoxColumn"
        Me.RozmerDataGridViewTextBoxColumn.ReadOnly = True
        Me.RozmerDataGridViewTextBoxColumn.Visible = False
        '
        'VelkostDataGridViewTextBoxColumn
        '
        Me.VelkostDataGridViewTextBoxColumn.DataPropertyName = "Velkost"
        Me.VelkostDataGridViewTextBoxColumn.HeaderText = "Velkost"
        Me.VelkostDataGridViewTextBoxColumn.Name = "VelkostDataGridViewTextBoxColumn"
        Me.VelkostDataGridViewTextBoxColumn.ReadOnly = True
        Me.VelkostDataGridViewTextBoxColumn.Visible = False
        '
        'VahaDataGridViewTextBoxColumn
        '
        Me.VahaDataGridViewTextBoxColumn.DataPropertyName = "Vaha"
        Me.VahaDataGridViewTextBoxColumn.HeaderText = "Vaha"
        Me.VahaDataGridViewTextBoxColumn.Name = "VahaDataGridViewTextBoxColumn"
        Me.VahaDataGridViewTextBoxColumn.ReadOnly = True
        Me.VahaDataGridViewTextBoxColumn.Visible = False
        '
        'ZakazkaDataGridViewTextBoxColumn
        '
        Me.ZakazkaDataGridViewTextBoxColumn.DataPropertyName = "zakazka"
        Me.ZakazkaDataGridViewTextBoxColumn.HeaderText = "zakazka"
        Me.ZakazkaDataGridViewTextBoxColumn.Name = "ZakazkaDataGridViewTextBoxColumn"
        Me.ZakazkaDataGridViewTextBoxColumn.ReadOnly = True
        Me.ZakazkaDataGridViewTextBoxColumn.Visible = False
        '
        'CenaDataGridViewTextBoxColumn
        '
        Me.CenaDataGridViewTextBoxColumn.DataPropertyName = "Cena"
        Me.CenaDataGridViewTextBoxColumn.HeaderText = "Cena"
        Me.CenaDataGridViewTextBoxColumn.Name = "CenaDataGridViewTextBoxColumn"
        Me.CenaDataGridViewTextBoxColumn.ReadOnly = True
        Me.CenaDataGridViewTextBoxColumn.Visible = False
        '
        'PocetDataGridViewTextBoxColumn
        '
        Me.PocetDataGridViewTextBoxColumn.DataPropertyName = "pocet"
        Me.PocetDataGridViewTextBoxColumn.HeaderText = "pocet"
        Me.PocetDataGridViewTextBoxColumn.Name = "PocetDataGridViewTextBoxColumn"
        Me.PocetDataGridViewTextBoxColumn.ReadOnly = True
        Me.PocetDataGridViewTextBoxColumn.Visible = False
        '
        'SrotDataGridViewTextBoxColumn
        '
        Me.SrotDataGridViewTextBoxColumn.DataPropertyName = "srot"
        Me.SrotDataGridViewTextBoxColumn.HeaderText = "srot"
        Me.SrotDataGridViewTextBoxColumn.Name = "SrotDataGridViewTextBoxColumn"
        Me.SrotDataGridViewTextBoxColumn.ReadOnly = True
        Me.SrotDataGridViewTextBoxColumn.Visible = False
        '
        'SrotcenaDataGridViewTextBoxColumn
        '
        Me.SrotcenaDataGridViewTextBoxColumn.DataPropertyName = "srotcena"
        Me.SrotcenaDataGridViewTextBoxColumn.HeaderText = "srotcena"
        Me.SrotcenaDataGridViewTextBoxColumn.Name = "SrotcenaDataGridViewTextBoxColumn"
        Me.SrotcenaDataGridViewTextBoxColumn.ReadOnly = True
        Me.SrotcenaDataGridViewTextBoxColumn.Visible = False
        '
        'HutaBindingSource
        '
        Me.HutaBindingSource.DataMember = "Huta"
        Me.HutaBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'HutaTableAdapter
        '
        Me.HutaTableAdapter.ClearBeforeFill = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PodzakazkaDataGridViewTextBoxColumn, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.Razy, Me.Kusov, Me.Column2, Me.Meno, Me.Objednavka, Me.Zakaznik, Me.D_prijatia})
        Me.DataGridView2.DataSource = Me.ZakazkaBindingSource
        Me.DataGridView2.Location = New System.Drawing.Point(648, 45)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersWidth = 15
        Me.DataGridView2.Size = New System.Drawing.Size(255, 239)
        Me.DataGridView2.TabIndex = 4
        '
        'PodzakazkaDataGridViewTextBoxColumn
        '
        Me.PodzakazkaDataGridViewTextBoxColumn.DataPropertyName = "Podzakazka"
        Me.PodzakazkaDataGridViewTextBoxColumn.HeaderText = "Dielec"
        Me.PodzakazkaDataGridViewTextBoxColumn.Name = "PodzakazkaDataGridViewTextBoxColumn"
        Me.PodzakazkaDataGridViewTextBoxColumn.ReadOnly = True
        Me.PodzakazkaDataGridViewTextBoxColumn.Width = 62
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "srot"
        Me.DataGridViewTextBoxColumn1.HeaderText = "srot"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 49
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "srotcena"
        Me.DataGridViewTextBoxColumn2.HeaderText = "srotcena"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Visible = False
        Me.DataGridViewTextBoxColumn2.Width = 73
        '
        'Razy
        '
        Me.Razy.DataPropertyName = "Razy"
        Me.Razy.HeaderText = "Razy"
        Me.Razy.Name = "Razy"
        Me.Razy.ReadOnly = True
        Me.Razy.Visible = False
        Me.Razy.Width = 56
        '
        'Kusov
        '
        Me.Kusov.DataPropertyName = "Kusov"
        Me.Kusov.HeaderText = "Kusov"
        Me.Kusov.Name = "Kusov"
        Me.Kusov.ReadOnly = True
        Me.Kusov.Visible = False
        Me.Kusov.Width = 62
        '
        'Column2
        '
        Me.Column2.HeaderText = "Tento"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 41
        '
        'Meno
        '
        Me.Meno.DataPropertyName = "Meno"
        Me.Meno.HeaderText = "Meno"
        Me.Meno.Name = "Meno"
        Me.Meno.ReadOnly = True
        Me.Meno.Visible = False
        Me.Meno.Width = 59
        '
        'Objednavka
        '
        Me.Objednavka.DataPropertyName = "Objednavka"
        Me.Objednavka.HeaderText = "Objednavka"
        Me.Objednavka.Name = "Objednavka"
        Me.Objednavka.ReadOnly = True
        Me.Objednavka.Visible = False
        Me.Objednavka.Width = 90
        '
        'Zakaznik
        '
        Me.Zakaznik.DataPropertyName = "Zakaznik"
        Me.Zakaznik.HeaderText = "Zakaznik"
        Me.Zakaznik.Name = "Zakaznik"
        Me.Zakaznik.ReadOnly = True
        Me.Zakaznik.Visible = False
        Me.Zakaznik.Width = 76
        '
        'D_prijatia
        '
        Me.D_prijatia.DataPropertyName = "D_prijatia"
        Me.D_prijatia.HeaderText = "D_prijatia"
        Me.D_prijatia.Name = "D_prijatia"
        Me.D_prijatia.ReadOnly = True
        Me.D_prijatia.Visible = False
        Me.D_prijatia.Width = 76
        '
        'ZakazkaBindingSource
        '
        Me.ZakazkaBindingSource.DataMember = "Zakazka"
        Me.ZakazkaBindingSource.DataSource = Me.RotekDataSet
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label2.Location = New System.Drawing.Point(693, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Dielce"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label3.Location = New System.Drawing.Point(100, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Podzákazka:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label4.Location = New System.Drawing.Point(195, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 17)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Label4"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(573, 8)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(69, 36)
        Me.Button3.TabIndex = 10
        Me.Button3.Text = "Exportovať"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ZakazkaTableAdapter
        '
        Me.ZakazkaTableAdapter.ClearBeforeFill = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label6.Location = New System.Drawing.Point(138, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 17)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Kusov:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(195, 71)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 17)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Label7"
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button4.Location = New System.Drawing.Point(508, 8)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(59, 36)
        Me.Button4.TabIndex = 11
        Me.Button4.Text = "Info"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(540, 12)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(102, 29)
        Me.Button5.TabIndex = 12
        Me.Button5.Text = "Zmeniť stav"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Zadaná", "Rozpracovaná", "Ukončená"})
        Me.ComboBox1.Location = New System.Drawing.Point(413, 17)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 13
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Button2.Location = New System.Drawing.Point(544, 54)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(98, 38)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Minúť všetko"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button6.Location = New System.Drawing.Point(441, 54)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(97, 40)
        Me.Button6.TabIndex = 16
        Me.Button6.Text = "Minúť vybrané zákazku"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Button7.Location = New System.Drawing.Point(360, 54)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(75, 40)
        Me.Button7.TabIndex = 17
        Me.Button7.Text = "Minúť zvyšné"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(10, 54)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(93, 36)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Čo chýba"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial Unicode MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Green
        Me.Label5.Location = New System.Drawing.Point(10, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 21)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Dá sa"
        '
        'HmakroInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(903, 284)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button5)
        Me.Name = "HmakroInfo"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zákazka - Info"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HutaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ZakazkaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents HutaBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents HutaTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.HutaTableAdapter
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents IdentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZakazkaBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ZakazkaTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.ZakazkaTableAdapter
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents PodzakazkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Razy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Kusov As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Meno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Objednavka As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Zakaznik As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_prijatia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Druh As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nazov As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Typ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sirka As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rozmer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_rozmer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Velkost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cena As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Hustota As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents srot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents srotcena As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DruhDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NazovDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HustotaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RozmerDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VahaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZakazkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrotDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrotcenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
