<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class sklad
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.NastrojDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VelkostS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cena = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PocetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.zosrot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.srotcena = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Regal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SkladBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.SkladTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.SkladTableAdapter()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button5 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SkladBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Vladimir Script", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(254, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 58)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sklad"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 95)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(126, 37)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Pridať nástroj"
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
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NastrojDataGridViewTextBoxColumn, Me.VelkostS, Me.Cena, Me.PocetDataGridViewTextBoxColumn, Me.zosrot, Me.srotcena, Me.Regal})
        Me.DataGridView1.DataSource = Me.SkladBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 243)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(845, 489)
        Me.DataGridView1.TabIndex = 3
        '
        'NastrojDataGridViewTextBoxColumn
        '
        Me.NastrojDataGridViewTextBoxColumn.DataPropertyName = "Nastroj"
        Me.NastrojDataGridViewTextBoxColumn.HeaderText = "Nastroj"
        Me.NastrojDataGridViewTextBoxColumn.Name = "NastrojDataGridViewTextBoxColumn"
        Me.NastrojDataGridViewTextBoxColumn.ReadOnly = True
        Me.NastrojDataGridViewTextBoxColumn.Width = 150
        '
        'VelkostS
        '
        Me.VelkostS.DataPropertyName = "VelkostS"
        Me.VelkostS.HeaderText = "Polomer"
        Me.VelkostS.Name = "VelkostS"
        Me.VelkostS.ReadOnly = True
        Me.VelkostS.Width = 50
        '
        'Cena
        '
        Me.Cena.DataPropertyName = "Cena"
        Me.Cena.HeaderText = "Cena"
        Me.Cena.Name = "Cena"
        Me.Cena.ReadOnly = True
        Me.Cena.Width = 50
        '
        'PocetDataGridViewTextBoxColumn
        '
        Me.PocetDataGridViewTextBoxColumn.DataPropertyName = "Pocet"
        Me.PocetDataGridViewTextBoxColumn.HeaderText = "Pocet"
        Me.PocetDataGridViewTextBoxColumn.Name = "PocetDataGridViewTextBoxColumn"
        Me.PocetDataGridViewTextBoxColumn.ReadOnly = True
        Me.PocetDataGridViewTextBoxColumn.Width = 50
        '
        'zosrot
        '
        Me.zosrot.DataPropertyName = "zosrot"
        Me.zosrot.HeaderText = "Zničených"
        Me.zosrot.Name = "zosrot"
        Me.zosrot.ReadOnly = True
        Me.zosrot.Width = 70
        '
        'srotcena
        '
        Me.srotcena.DataPropertyName = "srotcena"
        Me.srotcena.HeaderText = "Zničených (€)"
        Me.srotcena.Name = "srotcena"
        Me.srotcena.ReadOnly = True
        Me.srotcena.Width = 80
        '
        'Regal
        '
        Me.Regal.DataPropertyName = "Regal"
        Me.Regal.HeaderText = "Regal"
        Me.Regal.Name = "Regal"
        Me.Regal.ReadOnly = True
        Me.Regal.Width = 500
        '
        'SkladBindingSource
        '
        Me.SkladBindingSource.DataMember = "Sklad"
        Me.SkladBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SkladTableAdapter
        '
        Me.SkladTableAdapter.ClearBeforeFill = True
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
        Me.TextBox2.Location = New System.Drawing.Point(12, 145)
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
        Me.TextBox3.Location = New System.Drawing.Point(165, 145)
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
        Me.Button5.Text = "Zošrotovať"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'sklad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 732)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button3)
        Me.Name = "sklad"
        Me.ShowInTaskbar = False
        Me.Text = "Sklad"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SkladBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents SkladBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SkladTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.SkladTableAdapter
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents NastrojDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VelkostS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cena As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PocetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents zosrot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents srotcena As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Regal As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
