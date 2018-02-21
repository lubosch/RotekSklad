<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class export
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.MenoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PriezviskoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NástrojDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VelkostRDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VlastnostDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KolkoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.srot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Menpr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RotekBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.RotekTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.RotekTableAdapter()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button1.Location = New System.Drawing.Point(468, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(180, 40)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Push to magic"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MenoDataGridViewTextBoxColumn, Me.PriezviskoDataGridViewTextBoxColumn, Me.PocetDataGridViewTextBoxColumn, Me.NástrojDataGridViewTextBoxColumn, Me.VelkostRDataGridViewTextBoxColumn, Me.VlastnostDataGridViewTextBoxColumn, Me.KolkoDataGridViewTextBoxColumn, Me.srot, Me.Menpr})
        Me.DataGridView1.DataSource = Me.RotekBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(0, 86)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1276, 655)
        Me.DataGridView1.TabIndex = 1
        '
        'MenoDataGridViewTextBoxColumn
        '
        Me.MenoDataGridViewTextBoxColumn.DataPropertyName = "Meno"
        Me.MenoDataGridViewTextBoxColumn.HeaderText = "Meno"
        Me.MenoDataGridViewTextBoxColumn.Name = "MenoDataGridViewTextBoxColumn"
        Me.MenoDataGridViewTextBoxColumn.ReadOnly = True
        '
        'PriezviskoDataGridViewTextBoxColumn
        '
        Me.PriezviskoDataGridViewTextBoxColumn.DataPropertyName = "Priezvisko"
        Me.PriezviskoDataGridViewTextBoxColumn.HeaderText = "Priezvisko"
        Me.PriezviskoDataGridViewTextBoxColumn.Name = "PriezviskoDataGridViewTextBoxColumn"
        Me.PriezviskoDataGridViewTextBoxColumn.ReadOnly = True
        '
        'PocetDataGridViewTextBoxColumn
        '
        Me.PocetDataGridViewTextBoxColumn.DataPropertyName = "pocet"
        Me.PocetDataGridViewTextBoxColumn.HeaderText = "pocet"
        Me.PocetDataGridViewTextBoxColumn.Name = "PocetDataGridViewTextBoxColumn"
        Me.PocetDataGridViewTextBoxColumn.ReadOnly = True
        '
        'NástrojDataGridViewTextBoxColumn
        '
        Me.NástrojDataGridViewTextBoxColumn.DataPropertyName = "Nástroj"
        Me.NástrojDataGridViewTextBoxColumn.HeaderText = "Nástroj"
        Me.NástrojDataGridViewTextBoxColumn.Name = "NástrojDataGridViewTextBoxColumn"
        Me.NástrojDataGridViewTextBoxColumn.ReadOnly = True
        '
        'VelkostRDataGridViewTextBoxColumn
        '
        Me.VelkostRDataGridViewTextBoxColumn.DataPropertyName = "VelkostR"
        Me.VelkostRDataGridViewTextBoxColumn.HeaderText = "VelkostR"
        Me.VelkostRDataGridViewTextBoxColumn.Name = "VelkostRDataGridViewTextBoxColumn"
        Me.VelkostRDataGridViewTextBoxColumn.ReadOnly = True
        '
        'VlastnostDataGridViewTextBoxColumn
        '
        Me.VlastnostDataGridViewTextBoxColumn.DataPropertyName = "Vlastnost"
        Me.VlastnostDataGridViewTextBoxColumn.HeaderText = "Vlastnost"
        Me.VlastnostDataGridViewTextBoxColumn.Name = "VlastnostDataGridViewTextBoxColumn"
        Me.VlastnostDataGridViewTextBoxColumn.ReadOnly = True
        '
        'KolkoDataGridViewTextBoxColumn
        '
        Me.KolkoDataGridViewTextBoxColumn.DataPropertyName = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn.HeaderText = "Kolko"
        Me.KolkoDataGridViewTextBoxColumn.Name = "KolkoDataGridViewTextBoxColumn"
        Me.KolkoDataGridViewTextBoxColumn.ReadOnly = True
        '
        'srot
        '
        Me.srot.DataPropertyName = "srot"
        Me.srot.HeaderText = "srot"
        Me.srot.Name = "srot"
        Me.srot.ReadOnly = True
        '
        'Menpr
        '
        Me.Menpr.DataPropertyName = "Menpr"
        Me.Menpr.HeaderText = "Menpr"
        Me.Menpr.Name = "Menpr"
        Me.Menpr.ReadOnly = True
        '
        'RotekBindingSource
        '
        Me.RotekBindingSource.DataMember = "Rotek"
        Me.RotekBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RotekTableAdapter
        '
        Me.RotekTableAdapter.ClearBeforeFill = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 58)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(918, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'export
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1276, 741)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "export"
        Me.Text = "export"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents RotekBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RotekTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.RotekTableAdapter
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents MenoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PriezviskoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NástrojDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostRDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VlastnostDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KolkoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents srot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Menpr As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
