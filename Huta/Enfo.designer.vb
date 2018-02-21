<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class enfo
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.NazovDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UlicaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MestDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PSČDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KrajinaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VeducDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZoznamFBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.ZoznamFTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.ZoznamFTableAdapter()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.ZaevidovalDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZakaznikDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DprijatiaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DplanDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DrealDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VydajkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZodpKonstrukcieDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KUkonceniaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FakturaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VeduciDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RozpracDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Subory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DLDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZakazkaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.ZakazkaTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.ZakazkaTableAdapter()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ZoznamFBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ZakazkaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Názov firmy:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Ulica + popisové číslo:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "PSČ:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Mesto:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 135)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Štát:"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(64, 337)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 37)
        Me.Button2.TabIndex = 14
        Me.Button2.Text = "Zrušiť"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 153)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Vedúci:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NazovDataGridViewTextBoxColumn, Me.UlicaDataGridViewTextBoxColumn, Me.MestDataGridViewTextBoxColumn, Me.PSČDataGridViewTextBoxColumn, Me.KrajinaDataGridViewTextBoxColumn, Me.PocetDataGridViewTextBoxColumn, Me.VeducDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.ZoznamFBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(136, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(467, 32)
        Me.DataGridView1.TabIndex = 17
        Me.DataGridView1.Visible = False
        '
        'NazovDataGridViewTextBoxColumn
        '
        Me.NazovDataGridViewTextBoxColumn.DataPropertyName = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.HeaderText = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.Name = "NazovDataGridViewTextBoxColumn"
        Me.NazovDataGridViewTextBoxColumn.ReadOnly = True
        Me.NazovDataGridViewTextBoxColumn.Width = 63
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
        'ZoznamFBindingSource
        '
        Me.ZoznamFBindingSource.DataMember = "ZoznamF"
        Me.ZoznamFBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ZoznamFTableAdapter
        '
        Me.ZoznamFTableAdapter.ClearBeforeFill = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Viner Hand ITC", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(12, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 32)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Label7"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(83, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Nezadané"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(123, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 13)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Nezadané"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(41, 91)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "Nezadané"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(49, 113)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 13)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "Nezadané"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(41, 135)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Nezadané"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(54, 153)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 13)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "Nezadané"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(12, 191)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(93, 13)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "Dátum vytvorenia:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(111, 191)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(56, 13)
        Me.Label15.TabIndex = 26
        Me.Label15.Text = "Nezadané"
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ZaevidovalDataGridViewTextBoxColumn, Me.ZakaznikDataGridViewTextBoxColumn, Me.DprijatiaDataGridViewTextBoxColumn, Me.DplanDataGridViewTextBoxColumn, Me.DrealDataGridViewTextBoxColumn, Me.VydajkaDataGridViewTextBoxColumn, Me.ZodpKonstrukcieDataGridViewTextBoxColumn, Me.KUkonceniaDataGridViewTextBoxColumn, Me.FakturaDataGridViewTextBoxColumn, Me.VeduciDataGridViewTextBoxColumn, Me.RozpracDataGridViewTextBoxColumn, Me.Subory, Me.DLDataGridViewTextBoxColumn})
        Me.DataGridView2.DataSource = Me.ZakazkaBindingSource
        Me.DataGridView2.Location = New System.Drawing.Point(-18, 41)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(464, 26)
        Me.DataGridView2.TabIndex = 27
        Me.DataGridView2.Visible = False
        '
        'ZaevidovalDataGridViewTextBoxColumn
        '
        Me.ZaevidovalDataGridViewTextBoxColumn.DataPropertyName = "Zaevidoval"
        Me.ZaevidovalDataGridViewTextBoxColumn.HeaderText = "Zaevidoval"
        Me.ZaevidovalDataGridViewTextBoxColumn.Name = "ZaevidovalDataGridViewTextBoxColumn"
        Me.ZaevidovalDataGridViewTextBoxColumn.ReadOnly = True
        Me.ZaevidovalDataGridViewTextBoxColumn.Width = 85
        '
        'ZakaznikDataGridViewTextBoxColumn
        '
        Me.ZakaznikDataGridViewTextBoxColumn.DataPropertyName = "Zakaznik"
        Me.ZakaznikDataGridViewTextBoxColumn.HeaderText = "Zakaznik"
        Me.ZakaznikDataGridViewTextBoxColumn.Name = "ZakaznikDataGridViewTextBoxColumn"
        Me.ZakaznikDataGridViewTextBoxColumn.ReadOnly = True
        Me.ZakaznikDataGridViewTextBoxColumn.Width = 76
        '
        'DprijatiaDataGridViewTextBoxColumn
        '
        Me.DprijatiaDataGridViewTextBoxColumn.DataPropertyName = "D_prijatia"
        Me.DprijatiaDataGridViewTextBoxColumn.HeaderText = "D_prijatia"
        Me.DprijatiaDataGridViewTextBoxColumn.Name = "DprijatiaDataGridViewTextBoxColumn"
        Me.DprijatiaDataGridViewTextBoxColumn.ReadOnly = True
        Me.DprijatiaDataGridViewTextBoxColumn.Width = 76
        '
        'DplanDataGridViewTextBoxColumn
        '
        Me.DplanDataGridViewTextBoxColumn.DataPropertyName = "D_plan"
        Me.DplanDataGridViewTextBoxColumn.HeaderText = "D_plan"
        Me.DplanDataGridViewTextBoxColumn.Name = "DplanDataGridViewTextBoxColumn"
        Me.DplanDataGridViewTextBoxColumn.ReadOnly = True
        Me.DplanDataGridViewTextBoxColumn.Width = 66
        '
        'DrealDataGridViewTextBoxColumn
        '
        Me.DrealDataGridViewTextBoxColumn.DataPropertyName = "D_real"
        Me.DrealDataGridViewTextBoxColumn.HeaderText = "D_real"
        Me.DrealDataGridViewTextBoxColumn.Name = "DrealDataGridViewTextBoxColumn"
        Me.DrealDataGridViewTextBoxColumn.ReadOnly = True
        Me.DrealDataGridViewTextBoxColumn.Width = 63
        '
        'VydajkaDataGridViewTextBoxColumn
        '
        Me.VydajkaDataGridViewTextBoxColumn.DataPropertyName = "Vydajka"
        Me.VydajkaDataGridViewTextBoxColumn.HeaderText = "Vydajka"
        Me.VydajkaDataGridViewTextBoxColumn.Name = "VydajkaDataGridViewTextBoxColumn"
        Me.VydajkaDataGridViewTextBoxColumn.ReadOnly = True
        Me.VydajkaDataGridViewTextBoxColumn.Width = 70
        '
        'ZodpKonstrukcieDataGridViewTextBoxColumn
        '
        Me.ZodpKonstrukcieDataGridViewTextBoxColumn.DataPropertyName = "Zodp_Konstrukcie"
        Me.ZodpKonstrukcieDataGridViewTextBoxColumn.HeaderText = "Zodp_Konstrukcie"
        Me.ZodpKonstrukcieDataGridViewTextBoxColumn.Name = "ZodpKonstrukcieDataGridViewTextBoxColumn"
        Me.ZodpKonstrukcieDataGridViewTextBoxColumn.ReadOnly = True
        Me.ZodpKonstrukcieDataGridViewTextBoxColumn.Width = 119
        '
        'KUkonceniaDataGridViewTextBoxColumn
        '
        Me.KUkonceniaDataGridViewTextBoxColumn.DataPropertyName = "K_Ukoncenia"
        Me.KUkonceniaDataGridViewTextBoxColumn.HeaderText = "K_Ukoncenia"
        Me.KUkonceniaDataGridViewTextBoxColumn.Name = "KUkonceniaDataGridViewTextBoxColumn"
        Me.KUkonceniaDataGridViewTextBoxColumn.ReadOnly = True
        Me.KUkonceniaDataGridViewTextBoxColumn.Width = 97
        '
        'FakturaDataGridViewTextBoxColumn
        '
        Me.FakturaDataGridViewTextBoxColumn.DataPropertyName = "Faktura"
        Me.FakturaDataGridViewTextBoxColumn.HeaderText = "Faktura"
        Me.FakturaDataGridViewTextBoxColumn.Name = "FakturaDataGridViewTextBoxColumn"
        Me.FakturaDataGridViewTextBoxColumn.ReadOnly = True
        Me.FakturaDataGridViewTextBoxColumn.Width = 68
        '
        'VeduciDataGridViewTextBoxColumn
        '
        Me.VeduciDataGridViewTextBoxColumn.DataPropertyName = "Veduci"
        Me.VeduciDataGridViewTextBoxColumn.HeaderText = "Veduci"
        Me.VeduciDataGridViewTextBoxColumn.Name = "VeduciDataGridViewTextBoxColumn"
        Me.VeduciDataGridViewTextBoxColumn.ReadOnly = True
        Me.VeduciDataGridViewTextBoxColumn.Width = 65
        '
        'RozpracDataGridViewTextBoxColumn
        '
        Me.RozpracDataGridViewTextBoxColumn.DataPropertyName = "Rozprac"
        Me.RozpracDataGridViewTextBoxColumn.HeaderText = "Rozprac"
        Me.RozpracDataGridViewTextBoxColumn.Name = "RozpracDataGridViewTextBoxColumn"
        Me.RozpracDataGridViewTextBoxColumn.ReadOnly = True
        Me.RozpracDataGridViewTextBoxColumn.Width = 72
        '
        'Subory
        '
        Me.Subory.DataPropertyName = "Subory"
        Me.Subory.HeaderText = "Subory"
        Me.Subory.Name = "Subory"
        Me.Subory.ReadOnly = True
        Me.Subory.Width = 65
        '
        'DLDataGridViewTextBoxColumn
        '
        Me.DLDataGridViewTextBoxColumn.DataPropertyName = "DL"
        Me.DLDataGridViewTextBoxColumn.HeaderText = "DL"
        Me.DLDataGridViewTextBoxColumn.Name = "DLDataGridViewTextBoxColumn"
        Me.DLDataGridViewTextBoxColumn.ReadOnly = True
        Me.DLDataGridViewTextBoxColumn.Width = 46
        '
        'ZakazkaBindingSource
        '
        Me.ZakazkaBindingSource.DataMember = "Zakazka"
        Me.ZakazkaBindingSource.DataSource = Me.RotekDataSet
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(11, 239)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(94, 13)
        Me.Label16.TabIndex = 28
        Me.Label16.Text = "Dátum ukončenia:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(11, 315)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(32, 13)
        Me.Label17.TabIndex = 29
        Me.Label17.Text = "Stav:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(111, 239)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(69, 13)
        Me.Label18.TabIndex = 30
        Me.Label18.Text = "Neukončená"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(47, 315)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(45, 13)
        Me.Label19.TabIndex = 31
        Me.Label19.Text = "Label19"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(12, 213)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(101, 13)
        Me.Label21.TabIndex = 34
        Me.Label21.Text = "Požadovaný dátum:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(119, 213)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(45, 13)
        Me.Label22.TabIndex = 35
        Me.Label22.Text = "Label22"
        '
        'ZakazkaTableAdapter
        '
        Me.ZakazkaTableAdapter.ClearBeforeFill = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(7, 261)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(101, 13)
        Me.Label20.TabIndex = 36
        Me.Label20.Text = "Vedúci konštrukcie:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(109, 261)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(45, 13)
        Me.Label23.TabIndex = 37
        Me.Label23.Text = "Label23"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(4, 283)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(152, 13)
        Me.Label24.TabIndex = 38
        Me.Label24.Text = "Dátum ukončenia konštrukcie:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(152, 283)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(69, 13)
        Me.Label25.TabIndex = 39
        Me.Label25.Text = "Neukončená"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(9, 173)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(63, 13)
        Me.Label26.TabIndex = 40
        Me.Label26.Text = "Zaevidoval:"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(70, 175)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(45, 13)
        Me.Label27.TabIndex = 41
        Me.Label27.Text = "Label27"
        '
        'enfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(221, 377)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label9)
        Me.Name = "enfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Informacie"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ZoznamFBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ZakazkaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents ZoznamFBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ZoznamFTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.ZoznamFTableAdapter
    Friend WithEvents NazovDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UlicaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MestDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PSČDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KrajinaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VeducDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents ZakazkaBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ZakazkaTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.ZakazkaTableAdapter
    Friend WithEvents ZaevidovalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZakaznikDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DprijatiaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DplanDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DrealDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VydajkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZodpKonstrukcieDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KUkonceniaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FakturaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VeduciDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RozpracDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Subory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DLDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
End Class
