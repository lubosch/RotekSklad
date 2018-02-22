<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form78
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form78))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.RotekBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.RotekTableAdapter()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.MenoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PriezviskoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SpoluDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.srot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.srotcena = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.Button18 = New System.Windows.Forms.Button()
        Me.Button20 = New System.Windows.Forms.Button()
        Me.Button21 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button24 = New System.Windows.Forms.Button()
        Me.Button23 = New System.Windows.Forms.Button()
        Me.Button22 = New System.Windows.Forms.Button()
        Me.Button25 = New System.Windows.Forms.Button()
        Me.Button19 = New System.Windows.Forms.Button()
        Me.Button16 = New System.Windows.Forms.Button()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.Button26 = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button27 = New System.Windows.Forms.Button()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 151)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(117, 31)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Pridať zamestnanca"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RotekBindingSource
        '
        Me.RotekBindingSource.DataMember = "Rotek"
        Me.RotekBindingSource.DataSource = Me.RotekDataSet
        Me.RotekBindingSource.Sort = "ID"
        '
        'RotekTableAdapter
        '
        Me.RotekTableAdapter.ClearBeforeFill = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(135, 151)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 31)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Vymazať"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(422, 152)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(102, 31)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Sklad nástrojov"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MenoDataGridViewTextBoxColumn, Me.PriezviskoDataGridViewTextBoxColumn, Me.SpoluDataGridViewTextBoxColumn, Me.srot, Me.srotcena})
        Me.DataGridView1.DataSource = Me.RotekBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 260)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1276, 463)
        Me.DataGridView1.TabIndex = 4
        '
        'MenoDataGridViewTextBoxColumn
        '
        Me.MenoDataGridViewTextBoxColumn.DataPropertyName = "Meno"
        Me.MenoDataGridViewTextBoxColumn.HeaderText = "Meno"
        Me.MenoDataGridViewTextBoxColumn.Name = "MenoDataGridViewTextBoxColumn"
        Me.MenoDataGridViewTextBoxColumn.ReadOnly = True
        Me.MenoDataGridViewTextBoxColumn.Width = 300
        '
        'PriezviskoDataGridViewTextBoxColumn
        '
        Me.PriezviskoDataGridViewTextBoxColumn.DataPropertyName = "Priezvisko"
        Me.PriezviskoDataGridViewTextBoxColumn.HeaderText = "Priezvisko"
        Me.PriezviskoDataGridViewTextBoxColumn.Name = "PriezviskoDataGridViewTextBoxColumn"
        Me.PriezviskoDataGridViewTextBoxColumn.ReadOnly = True
        Me.PriezviskoDataGridViewTextBoxColumn.Width = 300
        '
        'SpoluDataGridViewTextBoxColumn
        '
        Me.SpoluDataGridViewTextBoxColumn.DataPropertyName = "Spolu"
        Me.SpoluDataGridViewTextBoxColumn.HeaderText = "Spolu"
        Me.SpoluDataGridViewTextBoxColumn.Name = "SpoluDataGridViewTextBoxColumn"
        Me.SpoluDataGridViewTextBoxColumn.ReadOnly = True
        '
        'srot
        '
        Me.srot.DataPropertyName = "srot"
        Me.srot.HeaderText = "Zničil"
        Me.srot.Name = "srot"
        Me.srot.ReadOnly = True
        '
        'srotcena
        '
        Me.srotcena.DataPropertyName = "srotcena"
        Me.srotcena.HeaderText = "Nalámal za:"
        Me.srotcena.Name = "srotcena"
        Me.srotcena.ReadOnly = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(12, 188)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(88, 31)
        Me.Button4.TabIndex = 7
        Me.Button4.Text = "Informacje"
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(670, 152)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(108, 31)
        Me.Button5.TabIndex = 8
        Me.Button5.Text = "Sklad vzácností"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(548, 152)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(100, 31)
        Me.Button6.TabIndex = 9
        Me.Button6.Text = "Hutný sklad"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(106, 188)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(117, 31)
        Me.Button7.TabIndex = 10
        Me.Button7.Text = "Medzifiremné pôžičky"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(422, 188)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(103, 31)
        Me.Button8.TabIndex = 11
        Me.Button8.Text = "Spotrebný materiál"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(536, 188)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(75, 31)
        Me.Button9.TabIndex = 12
        Me.Button9.Text = "Zveráky a i."
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(617, 188)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(116, 31)
        Me.Button10.TabIndex = 13
        Me.Button10.Text = "Upínací + špeciálne"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(870, 188)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(83, 31)
        Me.Button11.TabIndex = 14
        Me.Button11.Text = "Elektronáradie"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Location = New System.Drawing.Point(959, 188)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(79, 31)
        Me.Button12.TabIndex = 15
        Me.Button12.Text = "Príslušenstvo"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button13
        '
        Me.Button13.Location = New System.Drawing.Point(739, 188)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(62, 31)
        Me.Button13.TabIndex = 16
        Me.Button13.Text = "Náradie"
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Button14
        '
        Me.Button14.Location = New System.Drawing.Point(807, 188)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(61, 31)
        Me.Button14.TabIndex = 17
        Me.Button14.Text = "Meradlá"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'Button15
        '
        Me.Button15.Location = New System.Drawing.Point(276, 152)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(88, 30)
        Me.Button15.TabIndex = 18
        Me.Button15.Text = "Exportovať"
        Me.Button15.UseVisualStyleBackColor = True
        '
        'Button18
        '
        Me.Button18.Location = New System.Drawing.Point(1044, 188)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(104, 31)
        Me.Button18.TabIndex = 21
        Me.Button18.Text = "Spojovací materiál"
        Me.Button18.UseVisualStyleBackColor = True
        '
        'Button20
        '
        Me.Button20.Location = New System.Drawing.Point(1154, 188)
        Me.Button20.Name = "Button20"
        Me.Button20.Size = New System.Drawing.Size(62, 31)
        Me.Button20.TabIndex = 23
        Me.Button20.Text = "Iné"
        Me.Button20.UseVisualStyleBackColor = True
        '
        'Button21
        '
        Me.Button21.Location = New System.Drawing.Point(990, 151)
        Me.Button21.Name = "Button21"
        Me.Button21.Size = New System.Drawing.Size(133, 32)
        Me.Button21.TabIndex = 24
        Me.Button21.Text = "Majetok"
        Me.Button21.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Button24)
        Me.Panel1.Controls.Add(Me.Button23)
        Me.Panel1.Controls.Add(Me.Button22)
        Me.Panel1.Location = New System.Drawing.Point(870, 151)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(374, 52)
        Me.Panel1.TabIndex = 25
        Me.Panel1.Visible = False
        '
        'Button24
        '
        Me.Button24.Location = New System.Drawing.Point(250, 4)
        Me.Button24.Name = "Button24"
        Me.Button24.Size = New System.Drawing.Size(117, 38)
        Me.Button24.TabIndex = 2
        Me.Button24.Text = "Majetok zákazníka"
        Me.Button24.UseVisualStyleBackColor = True
        '
        'Button23
        '
        Me.Button23.Location = New System.Drawing.Point(107, 4)
        Me.Button23.Name = "Button23"
        Me.Button23.Size = New System.Drawing.Size(137, 38)
        Me.Button23.TabIndex = 1
        Me.Button23.Text = "Nehmotný majetok"
        Me.Button23.UseVisualStyleBackColor = True
        '
        'Button22
        '
        Me.Button22.Location = New System.Drawing.Point(3, 3)
        Me.Button22.Name = "Button22"
        Me.Button22.Size = New System.Drawing.Size(98, 39)
        Me.Button22.TabIndex = 0
        Me.Button22.Text = "Hmotný majetok"
        Me.Button22.UseVisualStyleBackColor = True
        '
        'Button25
        '
        Me.Button25.Location = New System.Drawing.Point(1222, 189)
        Me.Button25.Name = "Button25"
        Me.Button25.Size = New System.Drawing.Size(61, 30)
        Me.Button25.TabIndex = 26
        Me.Button25.Text = "Kvapaliny"
        Me.Button25.UseVisualStyleBackColor = True
        '
        'Button19
        '
        Me.Button19.Location = New System.Drawing.Point(1082, 45)
        Me.Button19.Name = "Button19"
        Me.Button19.Size = New System.Drawing.Size(106, 31)
        Me.Button19.TabIndex = 22
        Me.Button19.Text = "Evidencia zákazok"
        Me.Button19.UseVisualStyleBackColor = True
        Me.Button19.Visible = False
        '
        'Button16
        '
        Me.Button16.Location = New System.Drawing.Point(785, 152)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(79, 30)
        Me.Button16.TabIndex = 27
        Me.Button16.Text = "Odpadky"
        Me.Button16.UseVisualStyleBackColor = True
        '
        'Button17
        '
        Me.Button17.Location = New System.Drawing.Point(12, 12)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(102, 34)
        Me.Button17.TabIndex = 28
        Me.Button17.Text = "Odpalovac"
        Me.Button17.UseVisualStyleBackColor = True
        '
        'Button26
        '
        Me.Button26.Location = New System.Drawing.Point(144, 12)
        Me.Button26.Name = "Button26"
        Me.Button26.Size = New System.Drawing.Size(102, 46)
        Me.Button26.TabIndex = 29
        Me.Button26.Text = "zamestnanci"
        Me.Button26.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(339, 45)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(200, 67)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'Button27
        '
        Me.Button27.Location = New System.Drawing.Point(48, 52)
        Me.Button27.Name = "Button27"
        Me.Button27.Size = New System.Drawing.Size(90, 42)
        Me.Button27.TabIndex = 30
        Me.Button27.Text = "Sklad"
        Me.Button27.UseVisualStyleBackColor = True
        '
        'Form78
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1276, 723)
        Me.Controls.Add(Me.Button27)
        Me.Controls.Add(Me.Button26)
        Me.Controls.Add(Me.Button17)
        Me.Controls.Add(Me.Button16)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button25)
        Me.Controls.Add(Me.Button21)
        Me.Controls.Add(Me.Button20)
        Me.Controls.Add(Me.Button19)
        Me.Controls.Add(Me.Button18)
        Me.Controls.Add(Me.Button15)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.Button13)
        Me.Controls.Add(Me.Button12)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form78"
        Me.Text = "s"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents RotekBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RotekTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.RotekTableAdapter
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents Button18 As System.Windows.Forms.Button
    Friend WithEvents Button20 As System.Windows.Forms.Button
    Friend WithEvents Button21 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button24 As System.Windows.Forms.Button
    Friend WithEvents Button23 As System.Windows.Forms.Button
    Friend WithEvents Button22 As System.Windows.Forms.Button
    Friend WithEvents Button25 As System.Windows.Forms.Button
    Friend WithEvents Button19 As System.Windows.Forms.Button
    Friend WithEvents Button16 As System.Windows.Forms.Button
    Friend WithEvents MenoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PriezviskoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SpoluDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents srot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents srotcena As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents Button26 As System.Windows.Forms.Button
    Friend WithEvents Button27 As System.Windows.Forms.Button
End Class
