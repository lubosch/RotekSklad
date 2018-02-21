<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class nastF
    Inherits WindowsApplication2.nastrclovek

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.DataGridView5 = New System.Windows.Forms.DataGridView()
        Me.FirmyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.FirmyTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.FirmyTableAdapter()
        Me.MenoaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenobDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NástrojbDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetbDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KolkobDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SpolubDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VelkostRbDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenabDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VlastnostbDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FirmyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView5
        '
        Me.DataGridView5.AllowUserToAddRows = False
        Me.DataGridView5.AllowUserToDeleteRows = False
        Me.DataGridView5.AutoGenerateColumns = False
        Me.DataGridView5.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView5.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MenobDataGridViewTextBoxColumn, Me.NástrojbDataGridViewTextBoxColumn1, Me.PocetbDataGridViewTextBoxColumn1, Me.KolkobDataGridViewTextBoxColumn1, Me.SpolubDataGridViewTextBoxColumn1, Me.VelkostRbDataGridViewTextBoxColumn1, Me.CenabDataGridViewTextBoxColumn1, Me.VlastnostbDataGridViewTextBoxColumn1})
        Me.DataGridView5.DataSource = Me.FirmyBindingSource
        Me.DataGridView5.Location = New System.Drawing.Point(9, 76)
        Me.DataGridView5.Name = "DataGridView5"
        Me.DataGridView5.ReadOnly = True
        Me.DataGridView5.RowHeadersWidth = 4
        Me.DataGridView5.Size = New System.Drawing.Size(511, 60)
        Me.DataGridView5.TabIndex = 15
        Me.DataGridView5.Visible = False
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
        'FirmyTableAdapter
        '
        Me.FirmyTableAdapter.ClearBeforeFill = True
        '
        'MenoaDataGridViewTextBoxColumn
        '
        Me.MenoaDataGridViewTextBoxColumn.DataPropertyName = "Meno"
        Me.MenoaDataGridViewTextBoxColumn.HeaderText = "Meno"
        Me.MenoaDataGridViewTextBoxColumn.Name = "MenoaDataGridViewTextBoxColumn"
        Me.MenoaDataGridViewTextBoxColumn.ReadOnly = True
        Me.MenoaDataGridViewTextBoxColumn.Width = 59
        '
        'CenaDataGridViewTextBoxColumn
        '
        Me.CenaDataGridViewTextBoxColumn.DataPropertyName = "Cena"
        Me.CenaDataGridViewTextBoxColumn.HeaderText = "Cena"
        Me.CenaDataGridViewTextBoxColumn.Name = "CenaDataGridViewTextBoxColumn"
        Me.CenaDataGridViewTextBoxColumn.ReadOnly = True
        Me.CenaDataGridViewTextBoxColumn.Width = 57
        '
        'MenobDataGridViewTextBoxColumn
        '
        Me.MenobDataGridViewTextBoxColumn.DataPropertyName = "Meno"
        Me.MenobDataGridViewTextBoxColumn.HeaderText = "Meno"
        Me.MenobDataGridViewTextBoxColumn.Name = "MenobDataGridViewTextBoxColumn"
        Me.MenobDataGridViewTextBoxColumn.ReadOnly = True
        Me.MenobDataGridViewTextBoxColumn.Width = 59
        '
        'NástrojbDataGridViewTextBoxColumn1
        '
        Me.NástrojbDataGridViewTextBoxColumn1.DataPropertyName = "Nástroj"
        Me.NástrojbDataGridViewTextBoxColumn1.HeaderText = "Nástroj"
        Me.NástrojbDataGridViewTextBoxColumn1.Name = "NástrojbDataGridViewTextBoxColumn1"
        Me.NástrojbDataGridViewTextBoxColumn1.ReadOnly = True
        Me.NástrojbDataGridViewTextBoxColumn1.Width = 65
        '
        'PocetbDataGridViewTextBoxColumn1
        '
        Me.PocetbDataGridViewTextBoxColumn1.DataPropertyName = "pocet"
        Me.PocetbDataGridViewTextBoxColumn1.HeaderText = "pocet"
        Me.PocetbDataGridViewTextBoxColumn1.Name = "PocetbDataGridViewTextBoxColumn1"
        Me.PocetbDataGridViewTextBoxColumn1.ReadOnly = True
        Me.PocetbDataGridViewTextBoxColumn1.Width = 59
        '
        'KolkobDataGridViewTextBoxColumn1
        '
        Me.KolkobDataGridViewTextBoxColumn1.DataPropertyName = "Kolko"
        Me.KolkobDataGridViewTextBoxColumn1.HeaderText = "Kolko"
        Me.KolkobDataGridViewTextBoxColumn1.Name = "KolkobDataGridViewTextBoxColumn1"
        Me.KolkobDataGridViewTextBoxColumn1.ReadOnly = True
        Me.KolkobDataGridViewTextBoxColumn1.Width = 59
        '
        'SpolubDataGridViewTextBoxColumn1
        '
        Me.SpolubDataGridViewTextBoxColumn1.DataPropertyName = "Spolu"
        Me.SpolubDataGridViewTextBoxColumn1.HeaderText = "Spolu"
        Me.SpolubDataGridViewTextBoxColumn1.Name = "SpolubDataGridViewTextBoxColumn1"
        Me.SpolubDataGridViewTextBoxColumn1.ReadOnly = True
        Me.SpolubDataGridViewTextBoxColumn1.Width = 59
        '
        'VelkostRbDataGridViewTextBoxColumn1
        '
        Me.VelkostRbDataGridViewTextBoxColumn1.DataPropertyName = "VelkostR"
        Me.VelkostRbDataGridViewTextBoxColumn1.HeaderText = "VelkostR"
        Me.VelkostRbDataGridViewTextBoxColumn1.Name = "VelkostRbDataGridViewTextBoxColumn1"
        Me.VelkostRbDataGridViewTextBoxColumn1.ReadOnly = True
        Me.VelkostRbDataGridViewTextBoxColumn1.Width = 75
        '
        'CenabDataGridViewTextBoxColumn1
        '
        Me.CenabDataGridViewTextBoxColumn1.DataPropertyName = "Cena"
        Me.CenabDataGridViewTextBoxColumn1.HeaderText = "Cena"
        Me.CenabDataGridViewTextBoxColumn1.Name = "CenabDataGridViewTextBoxColumn1"
        Me.CenabDataGridViewTextBoxColumn1.ReadOnly = True
        Me.CenabDataGridViewTextBoxColumn1.Width = 57
        '
        'VlastnostbDataGridViewTextBoxColumn1
        '
        Me.VlastnostbDataGridViewTextBoxColumn1.DataPropertyName = "Vlastnost"
        Me.VlastnostbDataGridViewTextBoxColumn1.HeaderText = "Vlastnost"
        Me.VlastnostbDataGridViewTextBoxColumn1.Name = "VlastnostbDataGridViewTextBoxColumn1"
        Me.VlastnostbDataGridViewTextBoxColumn1.ReadOnly = True
        Me.VlastnostbDataGridViewTextBoxColumn1.Width = 75
        '
        'nastF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(510, 230)
        Me.Controls.Add(Me.DataGridView5)
        Me.Name = "nastF"
        Me.Controls.SetChildIndex(Me.DataGridView5, 0)
        CType(Me.DataGridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FirmyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView5 As System.Windows.Forms.DataGridView
    Friend Shadows WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents FirmyBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FirmyTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.FirmyTableAdapter
    '   Friend WithEvents NástrojDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    '   Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    '  Friend WithEvents KolkoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    '  Friend WithEvents SpoluDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    '  Friend WithEvents VelkostRDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    'Friend WithEvents VlastnostDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MenoaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MenobDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NástrojbDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetbDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KolkobDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SpolubDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostRbDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenabDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VlastnostbDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
