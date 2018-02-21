<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Majetok
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.IdentDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NazovDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObrazokDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IzbaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MiestoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fotka = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PoznamkaDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MajetokBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.MajetokTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.MajetokTableAdapter()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MajetokBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdentDataGridViewTextBoxColumn, Me.NazovDataGridViewTextBoxColumn, Me.ObrazokDataGridViewTextBoxColumn, Me.PocetDataGridViewTextBoxColumn, Me.IzbaDataGridViewTextBoxColumn, Me.MiestoDataGridViewTextBoxColumn, Me.Fotka, Me.PoznamkaDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.MajetokBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 136)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1008, 594)
        Me.DataGridView1.TabIndex = 0
        '
        'IdentDataGridViewTextBoxColumn
        '
        Me.IdentDataGridViewTextBoxColumn.DataPropertyName = "Ident"
        Me.IdentDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IdentDataGridViewTextBoxColumn.Name = "IdentDataGridViewTextBoxColumn"
        Me.IdentDataGridViewTextBoxColumn.ReadOnly = True
        Me.IdentDataGridViewTextBoxColumn.Width = 43
        '
        'NazovDataGridViewTextBoxColumn
        '
        Me.NazovDataGridViewTextBoxColumn.DataPropertyName = "Nazov"
        Me.NazovDataGridViewTextBoxColumn.HeaderText = "Názov"
        Me.NazovDataGridViewTextBoxColumn.Name = "NazovDataGridViewTextBoxColumn"
        Me.NazovDataGridViewTextBoxColumn.ReadOnly = True
        Me.NazovDataGridViewTextBoxColumn.Width = 63
        '
        'ObrazokDataGridViewTextBoxColumn
        '
        Me.ObrazokDataGridViewTextBoxColumn.DataPropertyName = "Obrazok"
        Me.ObrazokDataGridViewTextBoxColumn.HeaderText = "Obrázok"
        Me.ObrazokDataGridViewTextBoxColumn.Name = "ObrazokDataGridViewTextBoxColumn"
        Me.ObrazokDataGridViewTextBoxColumn.ReadOnly = True
        Me.ObrazokDataGridViewTextBoxColumn.Visible = False
        Me.ObrazokDataGridViewTextBoxColumn.Width = 72
        '
        'PocetDataGridViewTextBoxColumn
        '
        Me.PocetDataGridViewTextBoxColumn.DataPropertyName = "Pocet"
        Me.PocetDataGridViewTextBoxColumn.HeaderText = "Počet"
        Me.PocetDataGridViewTextBoxColumn.Name = "PocetDataGridViewTextBoxColumn"
        Me.PocetDataGridViewTextBoxColumn.ReadOnly = True
        Me.PocetDataGridViewTextBoxColumn.Width = 60
        '
        'IzbaDataGridViewTextBoxColumn
        '
        Me.IzbaDataGridViewTextBoxColumn.DataPropertyName = "Izba"
        Me.IzbaDataGridViewTextBoxColumn.HeaderText = "Izba"
        Me.IzbaDataGridViewTextBoxColumn.Name = "IzbaDataGridViewTextBoxColumn"
        Me.IzbaDataGridViewTextBoxColumn.ReadOnly = True
        Me.IzbaDataGridViewTextBoxColumn.Width = 52
        '
        'MiestoDataGridViewTextBoxColumn
        '
        Me.MiestoDataGridViewTextBoxColumn.DataPropertyName = "Miesto"
        Me.MiestoDataGridViewTextBoxColumn.HeaderText = "Miesto"
        Me.MiestoDataGridViewTextBoxColumn.Name = "MiestoDataGridViewTextBoxColumn"
        Me.MiestoDataGridViewTextBoxColumn.ReadOnly = True
        Me.MiestoDataGridViewTextBoxColumn.Width = 63
        '
        'Fotka
        '
        Me.Fotka.FalseValue = ""
        Me.Fotka.HeaderText = "Fotka"
        Me.Fotka.Name = "Fotka"
        Me.Fotka.ReadOnly = True
        Me.Fotka.Width = 40
        '
        'PoznamkaDataGridViewTextBoxColumn
        '
        Me.PoznamkaDataGridViewTextBoxColumn.DataPropertyName = "Poznamka"
        Me.PoznamkaDataGridViewTextBoxColumn.HeaderText = "Poznámka"
        Me.PoznamkaDataGridViewTextBoxColumn.Name = "PoznamkaDataGridViewTextBoxColumn"
        Me.PoznamkaDataGridViewTextBoxColumn.ReadOnly = True
        Me.PoznamkaDataGridViewTextBoxColumn.Width = 82
        '
        'MajetokBindingSource
        '
        Me.MajetokBindingSource.DataMember = "Majetok"
        Me.MajetokBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'MajetokTableAdapter
        '
        Me.MajetokTableAdapter.ClearBeforeFill = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Viner Hand ITC", 36.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(376, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(510, 78)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Majetok zakaznika"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(13, 110)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 2
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(119, 110)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 3
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(225, 109)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 4
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(331, 109)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(100, 20)
        Me.TextBox4.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "ID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(119, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Názov"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(225, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Izba"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(328, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Miesto"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button1.Location = New System.Drawing.Point(12, 58)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(101, 33)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Pridať"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!)
        Me.Button2.Location = New System.Drawing.Point(124, 59)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(104, 32)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "Vymazať"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Majetok
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 730)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Majetok"
        Me.Text = "Majetok"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MajetokBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents MajetokBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MajetokTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.MajetokTableAdapter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents IdentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NazovDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ObrazokDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IzbaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MiestoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fotka As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PoznamkaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
