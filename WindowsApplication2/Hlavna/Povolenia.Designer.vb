<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Povolenia
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
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.NazovDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SkladnikDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ZakazkarDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AdministratorDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.KtokolvekDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PovoleniaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.PovoleniaTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.PovoleniaTableAdapter()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SkladListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SkladListTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.SkladListTableAdapter()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PovoleniaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SkladListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.RadioButton1.Location = New System.Drawing.Point(12, 216)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(56, 21)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Huta"
        Me.RadioButton1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(74, 216)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(79, 21)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Príjemky"
        Me.RadioButton2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.RadioButton3.Location = New System.Drawing.Point(159, 216)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(75, 21)
        Me.RadioButton3.TabIndex = 2
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "Výdajky"
        Me.RadioButton3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NazovDataGridViewTextBoxColumn, Me.SkladnikDataGridViewTextBoxColumn, Me.ZakazkarDataGridViewTextBoxColumn, Me.AdministratorDataGridViewTextBoxColumn, Me.KtokolvekDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.PovoleniaBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(0, 243)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(643, 206)
        Me.DataGridView1.TabIndex = 3
        '
        'NazovDataGridViewTextBoxColumn
        '
        Me.NazovDataGridViewTextBoxColumn.DataPropertyName = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.HeaderText = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.Name = "NazovDataGridViewTextBoxColumn"
        Me.NazovDataGridViewTextBoxColumn.Width = 200
        '
        'SkladnikDataGridViewTextBoxColumn
        '
        Me.SkladnikDataGridViewTextBoxColumn.DataPropertyName = "Skladnik"
        Me.SkladnikDataGridViewTextBoxColumn.FalseValue = "0"
        Me.SkladnikDataGridViewTextBoxColumn.HeaderText = "Skladnik"
        Me.SkladnikDataGridViewTextBoxColumn.Name = "SkladnikDataGridViewTextBoxColumn"
        Me.SkladnikDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SkladnikDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ZakazkarDataGridViewTextBoxColumn
        '
        Me.ZakazkarDataGridViewTextBoxColumn.DataPropertyName = "Zakazkar"
        Me.ZakazkarDataGridViewTextBoxColumn.FalseValue = "0"
        Me.ZakazkarDataGridViewTextBoxColumn.HeaderText = "Zakazkar"
        Me.ZakazkarDataGridViewTextBoxColumn.Name = "ZakazkarDataGridViewTextBoxColumn"
        Me.ZakazkarDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ZakazkarDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'AdministratorDataGridViewTextBoxColumn
        '
        Me.AdministratorDataGridViewTextBoxColumn.DataPropertyName = "Administrator"
        Me.AdministratorDataGridViewTextBoxColumn.FalseValue = "0"
        Me.AdministratorDataGridViewTextBoxColumn.HeaderText = "Administrator"
        Me.AdministratorDataGridViewTextBoxColumn.Name = "AdministratorDataGridViewTextBoxColumn"
        Me.AdministratorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AdministratorDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'KtokolvekDataGridViewTextBoxColumn
        '
        Me.KtokolvekDataGridViewTextBoxColumn.DataPropertyName = "Ktokolvek"
        Me.KtokolvekDataGridViewTextBoxColumn.FalseValue = "0"
        Me.KtokolvekDataGridViewTextBoxColumn.HeaderText = "Ktokolvek"
        Me.KtokolvekDataGridViewTextBoxColumn.Name = "KtokolvekDataGridViewTextBoxColumn"
        Me.KtokolvekDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.KtokolvekDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'PovoleniaBindingSource
        '
        Me.PovoleniaBindingSource.DataMember = "Povolenia"
        Me.PovoleniaBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PovoleniaTableAdapter
        '
        Me.PovoleniaTableAdapter.ClearBeforeFill = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Viner Hand ITC", 36.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(230, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(266, 78)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Povolenia"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(361, 148)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Pridať"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(216, 150)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(139, 20)
        Me.TextBox1.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(216, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Nový sklad"
        '
        'ComboBox1
        '
        Me.ComboBox1.DataSource = Me.SkladListBindingSource
        Me.ComboBox1.DisplayMember = "Nazov"
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(8, 148)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(141, 21)
        Me.ComboBox1.TabIndex = 8
        Me.ComboBox1.ValueMember = "Nazov"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 132)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Zoznam skladov"
        '
        'SkladListBindingSource
        '
        Me.SkladListBindingSource.DataMember = "SkladList"
        Me.SkladListBindingSource.DataSource = Me.RotekDataSet
        '
        'SkladListTableAdapter
        '
        Me.SkladListTableAdapter.ClearBeforeFill = True
        '
        'Povolenia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 448)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.RadioButton3)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Name = "Povolenia"
        Me.Text = "Povolenia"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PovoleniaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SkladListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents PovoleniaBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PovoleniaTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.PovoleniaTableAdapter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NazovDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SkladnikDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ZakazkarDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents AdministratorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents KtokolvekDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SkladListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SkladListTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.SkladListTableAdapter
End Class
