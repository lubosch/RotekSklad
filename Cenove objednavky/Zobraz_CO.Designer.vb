<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Zobraz_CPO
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.COBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.COTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.COTableAdapter()
        Me.Polozka = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KusovDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DatumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EvidovalDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.COBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Polozka, Me.KusovDataGridViewTextBoxColumn, Me.CenaDataGridViewTextBoxColumn, Me.Column1, Me.DatumDataGridViewTextBoxColumn, Me.EvidovalDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.COBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 58)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(921, 340)
        Me.DataGridView1.TabIndex = 0
        '
        'COBindingSource
        '
        Me.COBindingSource.DataMember = "CO"
        Me.COBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Viner Hand ITC", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(398, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 55)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Label1"
        '
        'COTableAdapter
        '
        Me.COTableAdapter.ClearBeforeFill = True
        '
        'Polozka
        '
        Me.Polozka.DataPropertyName = "Polozka"
        Me.Polozka.HeaderText = "Polozka"
        Me.Polozka.Name = "Polozka"
        Me.Polozka.ReadOnly = True
        Me.Polozka.Width = 70
        '
        'KusovDataGridViewTextBoxColumn
        '
        Me.KusovDataGridViewTextBoxColumn.DataPropertyName = "Kusov"
        Me.KusovDataGridViewTextBoxColumn.HeaderText = "Kusov"
        Me.KusovDataGridViewTextBoxColumn.Name = "KusovDataGridViewTextBoxColumn"
        Me.KusovDataGridViewTextBoxColumn.ReadOnly = True
        Me.KusovDataGridViewTextBoxColumn.Width = 62
        '
        'CenaDataGridViewTextBoxColumn
        '
        Me.CenaDataGridViewTextBoxColumn.DataPropertyName = "Cena"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N2"
        Me.CenaDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
        Me.CenaDataGridViewTextBoxColumn.HeaderText = "Cena / ks"
        Me.CenaDataGridViewTextBoxColumn.Name = "CenaDataGridViewTextBoxColumn"
        Me.CenaDataGridViewTextBoxColumn.ReadOnly = True
        Me.CenaDataGridViewTextBoxColumn.Width = 79
        '
        'Column1
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column1.HeaderText = "Cena"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 57
        '
        'DatumDataGridViewTextBoxColumn
        '
        Me.DatumDataGridViewTextBoxColumn.DataPropertyName = "Datum"
        Me.DatumDataGridViewTextBoxColumn.HeaderText = "Datum"
        Me.DatumDataGridViewTextBoxColumn.Name = "DatumDataGridViewTextBoxColumn"
        Me.DatumDataGridViewTextBoxColumn.ReadOnly = True
        Me.DatumDataGridViewTextBoxColumn.Width = 63
        '
        'EvidovalDataGridViewTextBoxColumn
        '
        Me.EvidovalDataGridViewTextBoxColumn.DataPropertyName = "Evidoval"
        Me.EvidovalDataGridViewTextBoxColumn.HeaderText = "Evidoval"
        Me.EvidovalDataGridViewTextBoxColumn.Name = "EvidovalDataGridViewTextBoxColumn"
        Me.EvidovalDataGridViewTextBoxColumn.ReadOnly = True
        Me.EvidovalDataGridViewTextBoxColumn.Width = 73
        '
        'Zobraz_CPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 398)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Zobraz_CPO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zobraz_CP"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.COBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents COBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents COTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.COTableAdapter
    Friend WithEvents Polozka As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KusovDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DatumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EvidovalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
