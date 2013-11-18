<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class skladvz
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.CisloDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NazovDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cena = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Znicenych = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fotka = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PoznamkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FotkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZnCenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VzacnostiBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.VzacnostiTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.VzacnostiTableAdapter()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VzacnostiBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Viner Hand ITC", 36.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(254, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(524, 78)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sklad vzácnych vecí"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 95)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(126, 37)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Pridať vec"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(481, 145)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(134, 25)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Čoho je málo"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(482, 145)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(133, 25)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "Späť - zobraziť všetky"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(621, 150)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(147, 20)
        Me.TextBox1.TabIndex = 5
        '
        'TextBox2
        '
        Me.TextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox2.Location = New System.Drawing.Point(12, 148)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(126, 20)
        Me.TextBox2.TabIndex = 6
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(330, 145)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(146, 25)
        Me.Button4.TabIndex = 8
        Me.Button4.Text = "Vymazať filter"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(164, 148)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(134, 20)
        Me.TextBox3.TabIndex = 7
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(165, 95)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(133, 36)
        Me.Button5.TabIndex = 9
        Me.Button5.Text = "Odstrániť"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CisloDataGridViewTextBoxColumn, Me.NazovDataGridViewTextBoxColumn, Me.PocetDataGridViewTextBoxColumn, Me.CenaDataGridViewTextBoxColumn, Me.Cena, Me.Znicenych, Me.bum, Me.fotka, Me.PoznamkaDataGridViewTextBoxColumn, Me.FotkaDataGridViewTextBoxColumn, Me.ZnCenaDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.VzacnostiBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 249)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1154, 483)
        Me.DataGridView1.TabIndex = 10
        '
        'CisloDataGridViewTextBoxColumn
        '
        Me.CisloDataGridViewTextBoxColumn.DataPropertyName = "Cislo"
        Me.CisloDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.CisloDataGridViewTextBoxColumn.Name = "CisloDataGridViewTextBoxColumn"
        Me.CisloDataGridViewTextBoxColumn.ReadOnly = True
        Me.CisloDataGridViewTextBoxColumn.Width = 43
        '
        'NazovDataGridViewTextBoxColumn
        '
        Me.NazovDataGridViewTextBoxColumn.DataPropertyName = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.HeaderText = "Názov"
        Me.NazovDataGridViewTextBoxColumn.Name = "NazovDataGridViewTextBoxColumn"
        Me.NazovDataGridViewTextBoxColumn.ReadOnly = True
        Me.NazovDataGridViewTextBoxColumn.Width = 63
        '
        'PocetDataGridViewTextBoxColumn
        '
        Me.PocetDataGridViewTextBoxColumn.DataPropertyName = "Pocet"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PocetDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle6
        Me.PocetDataGridViewTextBoxColumn.HeaderText = "Počet"
        Me.PocetDataGridViewTextBoxColumn.Name = "PocetDataGridViewTextBoxColumn"
        Me.PocetDataGridViewTextBoxColumn.ReadOnly = True
        Me.PocetDataGridViewTextBoxColumn.Width = 60
        '
        'CenaDataGridViewTextBoxColumn
        '
        Me.CenaDataGridViewTextBoxColumn.DataPropertyName = "Cena"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "0.##"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.CenaDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle7
        Me.CenaDataGridViewTextBoxColumn.HeaderText = "Cena"
        Me.CenaDataGridViewTextBoxColumn.Name = "CenaDataGridViewTextBoxColumn"
        Me.CenaDataGridViewTextBoxColumn.ReadOnly = True
        Me.CenaDataGridViewTextBoxColumn.Visible = False
        '
        'Cena
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "C2"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.Cena.DefaultCellStyle = DataGridViewCellStyle8
        Me.Cena.HeaderText = "Cena"
        Me.Cena.Name = "Cena"
        Me.Cena.ReadOnly = True
        Me.Cena.Width = 57
        '
        'Znicenych
        '
        Me.Znicenych.DataPropertyName = "Znicenych"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Znicenych.DefaultCellStyle = DataGridViewCellStyle9
        Me.Znicenych.HeaderText = "Zničených"
        Me.Znicenych.Name = "Znicenych"
        Me.Znicenych.ReadOnly = True
        Me.Znicenych.Width = 82
        '
        'bum
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "C2"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.bum.DefaultCellStyle = DataGridViewCellStyle10
        Me.bum.HeaderText = "Zničených (€)"
        Me.bum.Name = "bum"
        Me.bum.ReadOnly = True
        Me.bum.Width = 97
        '
        'fotka
        '
        Me.fotka.HeaderText = "fotka"
        Me.fotka.Name = "fotka"
        Me.fotka.ReadOnly = True
        Me.fotka.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.fotka.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.fotka.Width = 56
        '
        'PoznamkaDataGridViewTextBoxColumn
        '
        Me.PoznamkaDataGridViewTextBoxColumn.DataPropertyName = "Poznamka"
        Me.PoznamkaDataGridViewTextBoxColumn.HeaderText = "Poznamka"
        Me.PoznamkaDataGridViewTextBoxColumn.Name = "PoznamkaDataGridViewTextBoxColumn"
        Me.PoznamkaDataGridViewTextBoxColumn.ReadOnly = True
        Me.PoznamkaDataGridViewTextBoxColumn.Width = 82
        '
        'FotkaDataGridViewTextBoxColumn
        '
        Me.FotkaDataGridViewTextBoxColumn.DataPropertyName = "Fotka"
        Me.FotkaDataGridViewTextBoxColumn.HeaderText = "Fotka"
        Me.FotkaDataGridViewTextBoxColumn.Name = "FotkaDataGridViewTextBoxColumn"
        Me.FotkaDataGridViewTextBoxColumn.ReadOnly = True
        Me.FotkaDataGridViewTextBoxColumn.Visible = False
        '
        'ZnCenaDataGridViewTextBoxColumn
        '
        Me.ZnCenaDataGridViewTextBoxColumn.DataPropertyName = "znCena"
        Me.ZnCenaDataGridViewTextBoxColumn.HeaderText = "znCena"
        Me.ZnCenaDataGridViewTextBoxColumn.Name = "ZnCenaDataGridViewTextBoxColumn"
        Me.ZnCenaDataGridViewTextBoxColumn.ReadOnly = True
        Me.ZnCenaDataGridViewTextBoxColumn.Visible = False
        '
        'VzacnostiBindingSource
        '
        Me.VzacnostiBindingSource.DataMember = "Vzacnosti"
        Me.VzacnostiBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'VzacnostiTableAdapter
        '
        Me.VzacnostiTableAdapter.ClearBeforeFill = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "ID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(165, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Názov"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(330, 95)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(111, 36)
        Me.Button6.TabIndex = 13
        Me.Button6.Text = "Exportovať"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(621, 131)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Tu zadaj menej ako:"
        '
        'skladvz
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1154, 732)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button3)
        Me.Name = "skladvz"
        Me.ShowInTaskbar = False
        Me.Text = "Sklad vzácnych vecí"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VzacnostiBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Cislo1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents VzacnostiBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents VzacnostiTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.VzacnostiTableAdapter
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CisloDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NazovDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cena As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Znicenych As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents bum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fotka As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PoznamkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FotkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZnCenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
