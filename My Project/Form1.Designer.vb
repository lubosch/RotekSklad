<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SkladBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.SkladTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.SkladTableAdapter()
        Me.RotekTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.RotekTableAdapter()
        Me.RotekBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.MenoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PriezviskoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Spolu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.SkladBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(72, 220)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(102, 31)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "Sklad"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(-9, 220)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 31)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Vymazať"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(-132, 220)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(117, 31)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Pridať zamestnanca"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'SkladBindingSource
        '
        Me.SkladBindingSource.DataMember = "Sklad"
        Me.SkladBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "Rotek2DataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SkladTableAdapter
        '
        Me.SkladTableAdapter.ClearBeforeFill = True
        '
        'RotekTableAdapter
        '
        Me.RotekTableAdapter.ClearBeforeFill = True
        '
        'RotekBindingSource
        '
        Me.RotekBindingSource.DataMember = "Rotek"
        Me.RotekBindingSource.DataSource = Me.RotekDataSet
        Me.RotekBindingSource.Sort = "ID"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MenoDataGridViewTextBoxColumn, Me.PriezviskoDataGridViewTextBoxColumn, Me.Spolu})
        Me.DataGridView1.DataSource = Me.RotekBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 186)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(857, 400)
        Me.DataGridView1.TabIndex = 6
        '
        'MenoDataGridViewTextBoxColumn
        '
        Me.MenoDataGridViewTextBoxColumn.DataPropertyName = "Meno"
        Me.MenoDataGridViewTextBoxColumn.HeaderText = "Meno"
        Me.MenoDataGridViewTextBoxColumn.Name = "MenoDataGridViewTextBoxColumn"
        Me.MenoDataGridViewTextBoxColumn.ReadOnly = True
        Me.MenoDataGridViewTextBoxColumn.Width = 400
        '
        'PriezviskoDataGridViewTextBoxColumn
        '
        Me.PriezviskoDataGridViewTextBoxColumn.DataPropertyName = "Priezvisko"
        Me.PriezviskoDataGridViewTextBoxColumn.HeaderText = "Priezvisko"
        Me.PriezviskoDataGridViewTextBoxColumn.Name = "PriezviskoDataGridViewTextBoxColumn"
        Me.PriezviskoDataGridViewTextBoxColumn.ReadOnly = True
        Me.PriezviskoDataGridViewTextBoxColumn.Width = 400
        '
        'Spolu
        '
        Me.Spolu.DataPropertyName = "Spolu"
        Me.Spolu.HeaderText = "Spolu"
        Me.Spolu.Name = "Spolu"
        Me.Spolu.ReadOnly = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(857, 586)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.SkladBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents SkladBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents SkladTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.SkladTableAdapter
    Friend WithEvents RotekTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.RotekTableAdapter
    Friend WithEvents RotekBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents MenoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PriezviskoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Spolu As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
