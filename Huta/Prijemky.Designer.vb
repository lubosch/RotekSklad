<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Prijemky
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.NazovDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DodaciListDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DatumDLDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dodavatel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VyhotovilDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PoznamkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DatumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Export = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Upravit = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Zmazat = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.PrijemkyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PrijemkyTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.PrijemkyTableAdapter()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrijemkyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NazovDataGridViewTextBoxColumn, Me.DodaciListDataGridViewTextBoxColumn, Me.DatumDLDataGridViewTextBoxColumn, Me.Dodavatel, Me.VyhotovilDataGridViewTextBoxColumn, Me.PoznamkaDataGridViewTextBoxColumn, Me.DatumDataGridViewTextBoxColumn, Me.Export, Me.Upravit, Me.Zmazat})
        Me.DataGridView1.DataSource = Me.PrijemkyBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 130)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1019, 420)
        Me.DataGridView1.TabIndex = 0
        '
        'NazovDataGridViewTextBoxColumn
        '
        Me.NazovDataGridViewTextBoxColumn.DataPropertyName = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.HeaderText = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.Name = "NazovDataGridViewTextBoxColumn"
        Me.NazovDataGridViewTextBoxColumn.ReadOnly = True
        Me.NazovDataGridViewTextBoxColumn.Width = 63
        '
        'DodaciListDataGridViewTextBoxColumn
        '
        Me.DodaciListDataGridViewTextBoxColumn.DataPropertyName = "DodaciList"
        Me.DodaciListDataGridViewTextBoxColumn.HeaderText = "Dodaci List"
        Me.DodaciListDataGridViewTextBoxColumn.Name = "DodaciListDataGridViewTextBoxColumn"
        Me.DodaciListDataGridViewTextBoxColumn.ReadOnly = True
        Me.DodaciListDataGridViewTextBoxColumn.Width = 85
        '
        'DatumDLDataGridViewTextBoxColumn
        '
        Me.DatumDLDataGridViewTextBoxColumn.DataPropertyName = "Datum_DL"
        Me.DatumDLDataGridViewTextBoxColumn.HeaderText = "Datum DL"
        Me.DatumDLDataGridViewTextBoxColumn.Name = "DatumDLDataGridViewTextBoxColumn"
        Me.DatumDLDataGridViewTextBoxColumn.ReadOnly = True
        Me.DatumDLDataGridViewTextBoxColumn.Width = 80
        '
        'Dodavatel
        '
        Me.Dodavatel.DataPropertyName = "Dodavatel"
        Me.Dodavatel.HeaderText = "Dodavatel"
        Me.Dodavatel.Name = "Dodavatel"
        Me.Dodavatel.ReadOnly = True
        Me.Dodavatel.Width = 81
        '
        'VyhotovilDataGridViewTextBoxColumn
        '
        Me.VyhotovilDataGridViewTextBoxColumn.DataPropertyName = "Vyhotovil"
        Me.VyhotovilDataGridViewTextBoxColumn.HeaderText = "Vyhotovil"
        Me.VyhotovilDataGridViewTextBoxColumn.Name = "VyhotovilDataGridViewTextBoxColumn"
        Me.VyhotovilDataGridViewTextBoxColumn.ReadOnly = True
        Me.VyhotovilDataGridViewTextBoxColumn.Width = 75
        '
        'PoznamkaDataGridViewTextBoxColumn
        '
        Me.PoznamkaDataGridViewTextBoxColumn.DataPropertyName = "Poznamka"
        Me.PoznamkaDataGridViewTextBoxColumn.HeaderText = "Poznamka"
        Me.PoznamkaDataGridViewTextBoxColumn.Name = "PoznamkaDataGridViewTextBoxColumn"
        Me.PoznamkaDataGridViewTextBoxColumn.ReadOnly = True
        Me.PoznamkaDataGridViewTextBoxColumn.Width = 82
        '
        'DatumDataGridViewTextBoxColumn
        '
        Me.DatumDataGridViewTextBoxColumn.DataPropertyName = "Datum"
        Me.DatumDataGridViewTextBoxColumn.HeaderText = "Pridane"
        Me.DatumDataGridViewTextBoxColumn.Name = "DatumDataGridViewTextBoxColumn"
        Me.DatumDataGridViewTextBoxColumn.ReadOnly = True
        Me.DatumDataGridViewTextBoxColumn.Width = 68
        '
        'Export
        '
        Me.Export.HeaderText = "Exportovať"
        Me.Export.Name = "Export"
        Me.Export.ReadOnly = True
        Me.Export.Text = "Exportovať"
        Me.Export.UseColumnTextForButtonValue = True
        Me.Export.Width = 65
        '
        'Upravit
        '
        Me.Upravit.HeaderText = "Upraviť"
        Me.Upravit.Name = "Upravit"
        Me.Upravit.ReadOnly = True
        Me.Upravit.Text = "Upraviť"
        Me.Upravit.UseColumnTextForButtonValue = True
        Me.Upravit.Width = 48
        '
        'Zmazat
        '
        Me.Zmazat.HeaderText = "Zmazať"
        Me.Zmazat.Name = "Zmazat"
        Me.Zmazat.ReadOnly = True
        Me.Zmazat.Text = "Zmazať"
        Me.Zmazat.ToolTipText = "Zmazať"
        Me.Zmazat.UseColumnTextForButtonValue = True
        Me.Zmazat.Width = 49
        '
        'PrijemkyBindingSource
        '
        Me.PrijemkyBindingSource.DataMember = "Prijemky"
        Me.PrijemkyBindingSource.DataSource = Me.RotekDataSet
        Me.PrijemkyBindingSource.Sort = "Nazov DESC"
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Viner Hand ITC", 36.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(277, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(504, 78)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Evidencia príjemiek"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Názov"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(113, 103)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(113, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Dodací list"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(398, 104)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(398, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Dodávateľ"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(273, 104)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(119, 20)
        Me.DateTimePicker1.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(273, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Dátum pridania"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(505, 101)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(89, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Zmaž filter"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PrijemkyTableAdapter
        '
        Me.PrijemkyTableAdapter.ClearBeforeFill = True
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(218, 103)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(49, 20)
        Me.NumericUpDown1.TabIndex = 19
        Me.NumericUpDown1.Value = New Decimal(New Integer() {11, 0, 0, 0})
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(41, 103)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(66, 20)
        Me.TextBox1.TabIndex = 18
        '
        'Prijemky
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 550)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Prijemky"
        Me.Text = "Prijemky"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrijemkyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents PrijemkyBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PrijemkyTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.PrijemkyTableAdapter
    Friend WithEvents NazovDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DodaciListDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DatumDLDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dodavatel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VyhotovilDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PoznamkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DatumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Export As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Upravit As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Zmazat As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
