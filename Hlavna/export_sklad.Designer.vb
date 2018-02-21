<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class export_sklad
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
        Me.IdentDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NazovDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Srot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KolkoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CenaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FotkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PoznámkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IneBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.IneTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.IneTableAdapter()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IneBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdentDataGridViewTextBoxColumn, Me.NazovDataGridViewTextBoxColumn, Me.Srot, Me.PocetDataGridViewTextBoxColumn, Me.KolkoDataGridViewTextBoxColumn, Me.CenaDataGridViewTextBoxColumn, Me.FotkaDataGridViewTextBoxColumn, Me.PoznámkaDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.IneBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, -147)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1276, 888)
        Me.DataGridView1.TabIndex = 0
        '
        'IdentDataGridViewTextBoxColumn
        '
        Me.IdentDataGridViewTextBoxColumn.DataPropertyName = "Ident"
        Me.IdentDataGridViewTextBoxColumn.HeaderText = "Ident"
        Me.IdentDataGridViewTextBoxColumn.Name = "IdentDataGridViewTextBoxColumn"
        Me.IdentDataGridViewTextBoxColumn.ReadOnly = True
        '
        'NazovDataGridViewTextBoxColumn
        '
        Me.NazovDataGridViewTextBoxColumn.DataPropertyName = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.HeaderText = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.Name = "NazovDataGridViewTextBoxColumn"
        Me.NazovDataGridViewTextBoxColumn.ReadOnly = True
        '
        'Srot
        '
        Me.Srot.DataPropertyName = "Srot"
        Me.Srot.HeaderText = "Srot"
        Me.Srot.Name = "Srot"
        Me.Srot.ReadOnly = True
        '
        'PocetDataGridViewTextBoxColumn
        '
        Me.PocetDataGridViewTextBoxColumn.DataPropertyName = "pocet"
        Me.PocetDataGridViewTextBoxColumn.HeaderText = "pocet"
        Me.PocetDataGridViewTextBoxColumn.Name = "PocetDataGridViewTextBoxColumn"
        Me.PocetDataGridViewTextBoxColumn.ReadOnly = True
        '
        'KolkoDataGridViewTextBoxColumn
        '
        Me.KolkoDataGridViewTextBoxColumn.DataPropertyName = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn.HeaderText = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn.Name = "KolkoDataGridViewTextBoxColumn"
        Me.KolkoDataGridViewTextBoxColumn.ReadOnly = True
        '
        'CenaDataGridViewTextBoxColumn
        '
        Me.CenaDataGridViewTextBoxColumn.DataPropertyName = "Cena"
        Me.CenaDataGridViewTextBoxColumn.HeaderText = "Cena"
        Me.CenaDataGridViewTextBoxColumn.Name = "CenaDataGridViewTextBoxColumn"
        Me.CenaDataGridViewTextBoxColumn.ReadOnly = True
        '
        'FotkaDataGridViewTextBoxColumn
        '
        Me.FotkaDataGridViewTextBoxColumn.DataPropertyName = "Fotka"
        Me.FotkaDataGridViewTextBoxColumn.HeaderText = "Fotka"
        Me.FotkaDataGridViewTextBoxColumn.Name = "FotkaDataGridViewTextBoxColumn"
        Me.FotkaDataGridViewTextBoxColumn.ReadOnly = True
        '
        'PoznámkaDataGridViewTextBoxColumn
        '
        Me.PoznámkaDataGridViewTextBoxColumn.DataPropertyName = "Poznámka"
        Me.PoznámkaDataGridViewTextBoxColumn.HeaderText = "Poznámka"
        Me.PoznámkaDataGridViewTextBoxColumn.Name = "PoznámkaDataGridViewTextBoxColumn"
        Me.PoznámkaDataGridViewTextBoxColumn.ReadOnly = True
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
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button1.Location = New System.Drawing.Point(621, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(229, 71)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Push to magic"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'IneTableAdapter
        '
        Me.IneTableAdapter.ClearBeforeFill = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 84)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1115, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'export_sklad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1276, 741)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "export_sklad"
        Me.Text = "export_sklad"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IneBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents IneBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents IneTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.IneTableAdapter
    Friend WithEvents IdentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NazovDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Srot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KolkoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CenaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FotkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PoznámkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
End Class
