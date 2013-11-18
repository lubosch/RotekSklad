<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Evidencia
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
        Me.PoradieDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PreskumalDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZakaznikDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZakazkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KusovDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrijataDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PozadovanyDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SkutocnyDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DodanyDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DodaciDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PonukacDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PoznamkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tlac = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.EvidenciaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.EvidenciaTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.EvidenciaTableAdapter()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EvidenciaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(424, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Evidencia objednávok"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PoradieDataGridViewTextBoxColumn, Me.PreskumalDataGridViewTextBoxColumn, Me.ZakaznikDataGridViewTextBoxColumn, Me.ZakazkaDataGridViewTextBoxColumn, Me.KusovDataGridViewTextBoxColumn, Me.PrijataDataGridViewTextBoxColumn, Me.PozadovanyDataGridViewTextBoxColumn, Me.SkutocnyDataGridViewTextBoxColumn, Me.DodanyDataGridViewTextBoxColumn, Me.DodaciDataGridViewTextBoxColumn, Me.PonukacDataGridViewTextBoxColumn, Me.PoznamkaDataGridViewTextBoxColumn, Me.Tlac})
        Me.DataGridView1.DataSource = Me.EvidenciaBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(-10, 25)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidth = 10
        Me.DataGridView1.Size = New System.Drawing.Size(1017, 655)
        Me.DataGridView1.TabIndex = 1
        '
        'PoradieDataGridViewTextBoxColumn
        '
        Me.PoradieDataGridViewTextBoxColumn.DataPropertyName = "Poradie"
        Me.PoradieDataGridViewTextBoxColumn.HeaderText = "P.č."
        Me.PoradieDataGridViewTextBoxColumn.Name = "PoradieDataGridViewTextBoxColumn"
        Me.PoradieDataGridViewTextBoxColumn.ReadOnly = True
        Me.PoradieDataGridViewTextBoxColumn.Width = 40
        '
        'PreskumalDataGridViewTextBoxColumn
        '
        Me.PreskumalDataGridViewTextBoxColumn.DataPropertyName = "Preskumal"
        Me.PreskumalDataGridViewTextBoxColumn.HeaderText = "Preskúmal"
        Me.PreskumalDataGridViewTextBoxColumn.Name = "PreskumalDataGridViewTextBoxColumn"
        Me.PreskumalDataGridViewTextBoxColumn.ReadOnly = True
        Me.PreskumalDataGridViewTextBoxColumn.Width = 70
        '
        'ZakaznikDataGridViewTextBoxColumn
        '
        Me.ZakaznikDataGridViewTextBoxColumn.DataPropertyName = "Zakaznik"
        Me.ZakaznikDataGridViewTextBoxColumn.HeaderText = "Zákaznik"
        Me.ZakaznikDataGridViewTextBoxColumn.Name = "ZakaznikDataGridViewTextBoxColumn"
        Me.ZakaznikDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ZakazkaDataGridViewTextBoxColumn
        '
        Me.ZakazkaDataGridViewTextBoxColumn.DataPropertyName = "Zakazka"
        Me.ZakazkaDataGridViewTextBoxColumn.HeaderText = "Názov / označenie výrobku"
        Me.ZakazkaDataGridViewTextBoxColumn.Name = "ZakazkaDataGridViewTextBoxColumn"
        Me.ZakazkaDataGridViewTextBoxColumn.ReadOnly = True
        Me.ZakazkaDataGridViewTextBoxColumn.Width = 130
        '
        'KusovDataGridViewTextBoxColumn
        '
        Me.KusovDataGridViewTextBoxColumn.DataPropertyName = "Kusov"
        Me.KusovDataGridViewTextBoxColumn.HeaderText = "Požadovaný počet kusov"
        Me.KusovDataGridViewTextBoxColumn.Name = "KusovDataGridViewTextBoxColumn"
        Me.KusovDataGridViewTextBoxColumn.ReadOnly = True
        Me.KusovDataGridViewTextBoxColumn.Width = 80
        '
        'PrijataDataGridViewTextBoxColumn
        '
        Me.PrijataDataGridViewTextBoxColumn.DataPropertyName = "Prijata"
        Me.PrijataDataGridViewTextBoxColumn.HeaderText = "Objednávka prijatá dňa"
        Me.PrijataDataGridViewTextBoxColumn.Name = "PrijataDataGridViewTextBoxColumn"
        Me.PrijataDataGridViewTextBoxColumn.ReadOnly = True
        Me.PrijataDataGridViewTextBoxColumn.Width = 70
        '
        'PozadovanyDataGridViewTextBoxColumn
        '
        Me.PozadovanyDataGridViewTextBoxColumn.DataPropertyName = "Pozadovany"
        Me.PozadovanyDataGridViewTextBoxColumn.HeaderText = "Požadovaný termín dodania"
        Me.PozadovanyDataGridViewTextBoxColumn.Name = "PozadovanyDataGridViewTextBoxColumn"
        Me.PozadovanyDataGridViewTextBoxColumn.ReadOnly = True
        Me.PozadovanyDataGridViewTextBoxColumn.Width = 70
        '
        'SkutocnyDataGridViewTextBoxColumn
        '
        Me.SkutocnyDataGridViewTextBoxColumn.DataPropertyName = "Skutocny"
        Me.SkutocnyDataGridViewTextBoxColumn.HeaderText = "Skutočný termín dodania"
        Me.SkutocnyDataGridViewTextBoxColumn.Name = "SkutocnyDataGridViewTextBoxColumn"
        Me.SkutocnyDataGridViewTextBoxColumn.ReadOnly = True
        Me.SkutocnyDataGridViewTextBoxColumn.Width = 70
        '
        'DodanyDataGridViewTextBoxColumn
        '
        Me.DodanyDataGridViewTextBoxColumn.DataPropertyName = "Dodany"
        Me.DodanyDataGridViewTextBoxColumn.HeaderText = "Dodaný počet ks"
        Me.DodanyDataGridViewTextBoxColumn.Name = "DodanyDataGridViewTextBoxColumn"
        Me.DodanyDataGridViewTextBoxColumn.ReadOnly = True
        Me.DodanyDataGridViewTextBoxColumn.Width = 60
        '
        'DodaciDataGridViewTextBoxColumn
        '
        Me.DodaciDataGridViewTextBoxColumn.DataPropertyName = "Dodaci"
        Me.DodaciDataGridViewTextBoxColumn.HeaderText = "Číslo dodacieho listu"
        Me.DodaciDataGridViewTextBoxColumn.Name = "DodaciDataGridViewTextBoxColumn"
        Me.DodaciDataGridViewTextBoxColumn.ReadOnly = True
        Me.DodaciDataGridViewTextBoxColumn.Width = 60
        '
        'PonukacDataGridViewTextBoxColumn
        '
        Me.PonukacDataGridViewTextBoxColumn.DataPropertyName = "Ponuka_c"
        Me.PonukacDataGridViewTextBoxColumn.HeaderText = "Ponuka č."
        Me.PonukacDataGridViewTextBoxColumn.Name = "PonukacDataGridViewTextBoxColumn"
        Me.PonukacDataGridViewTextBoxColumn.ReadOnly = True
        Me.PonukacDataGridViewTextBoxColumn.Width = 60
        '
        'PoznamkaDataGridViewTextBoxColumn
        '
        Me.PoznamkaDataGridViewTextBoxColumn.DataPropertyName = "Poznamka"
        Me.PoznamkaDataGridViewTextBoxColumn.HeaderText = "Poznámka"
        Me.PoznamkaDataGridViewTextBoxColumn.Name = "PoznamkaDataGridViewTextBoxColumn"
        Me.PoznamkaDataGridViewTextBoxColumn.ReadOnly = True
        Me.PoznamkaDataGridViewTextBoxColumn.Width = 120
        '
        'Tlac
        '
        Me.Tlac.HeaderText = "Zmeniť"
        Me.Tlac.Name = "Tlac"
        Me.Tlac.ReadOnly = True
        Me.Tlac.Text = "Zmeniť"
        Me.Tlac.ToolTipText = "Zmeniť"
        Me.Tlac.UseColumnTextForButtonValue = True
        Me.Tlac.Width = 50
        '
        'EvidenciaBindingSource
        '
        Me.EvidenciaBindingSource.DataMember = "Evidencia"
        Me.EvidenciaBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'EvidenciaTableAdapter
        '
        Me.EvidenciaTableAdapter.ClearBeforeFill = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(414, 686)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(91, 32)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Exportovať"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(310, 696)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Od P.č."
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(359, 693)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(49, 20)
        Me.TextBox1.TabIndex = 4
        Me.TextBox1.Text = "1"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 686)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(102, 35)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Pridať novú"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Evidencia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 730)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Evidencia"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Evidencia"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EvidenciaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents EvidenciaBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents EvidenciaTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.EvidenciaTableAdapter
    Friend WithEvents Prijata1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pozadovany1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Skutocny1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dodany1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PoradieDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PreskumalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZakaznikDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZakazkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KusovDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrijataDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PozadovanyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SkutocnyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DodanyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DodaciDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PonukacDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PoznamkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tlac As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
