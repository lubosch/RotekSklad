<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Spust
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Spust))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.Button16 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.MailyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MailyTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.MailyTableAdapter()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.NickDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MailDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pocet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Datum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.UcetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.UcetTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.UcetTableAdapter()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.SkladListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SkladListTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.SkladListTableAdapter()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MailyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UcetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SkladListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.WindowsApplication2.My.Resources.Resources.Logo
        Me.PictureBox1.Location = New System.Drawing.Point(164, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(200, 67)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button1.Location = New System.Drawing.Point(96, 132)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(109, 42)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Hutný sklad"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Button3.Location = New System.Drawing.Point(385, 132)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(117, 42)
        Me.Button3.TabIndex = 1
        Me.Button3.Text = "Zákazky"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button17
        '
        Me.Button17.Location = New System.Drawing.Point(164, 220)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(90, 30)
        Me.Button17.TabIndex = 4
        Me.Button17.Text = "Zmeniť údaje"
        Me.Button17.UseVisualStyleBackColor = True
        '
        'Button16
        '
        Me.Button16.Location = New System.Drawing.Point(64, 188)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(91, 30)
        Me.Button16.TabIndex = 3
        Me.Button16.Text = "Zadať heslo"
        Me.Button16.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(180, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 17)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Vítajte"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(230, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(134, 17)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "neznámy užívateľ"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = Global.WindowsApplication2.My.Resources.Resources.krizik
        Me.PictureBox2.Location = New System.Drawing.Point(479, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(35, 34)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 25
        Me.PictureBox2.TabStop = False
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'MailyBindingSource
        '
        Me.MailyBindingSource.DataMember = "Maily"
        Me.MailyBindingSource.DataSource = Me.RotekDataSet
        '
        'MailyTableAdapter
        '
        Me.MailyTableAdapter.ClearBeforeFill = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NickDataGridViewTextBoxColumn, Me.MailDataGridViewTextBoxColumn, Me.Pocet, Me.Datum})
        Me.DataGridView1.DataSource = Me.MailyBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(21, 57)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(465, 36)
        Me.DataGridView1.TabIndex = 26
        Me.DataGridView1.Visible = False
        '
        'NickDataGridViewTextBoxColumn
        '
        Me.NickDataGridViewTextBoxColumn.DataPropertyName = "Nick"
        Me.NickDataGridViewTextBoxColumn.HeaderText = "Nick"
        Me.NickDataGridViewTextBoxColumn.Name = "NickDataGridViewTextBoxColumn"
        Me.NickDataGridViewTextBoxColumn.ReadOnly = True
        '
        'MailDataGridViewTextBoxColumn
        '
        Me.MailDataGridViewTextBoxColumn.DataPropertyName = "Mail"
        Me.MailDataGridViewTextBoxColumn.HeaderText = "Mail"
        Me.MailDataGridViewTextBoxColumn.Name = "MailDataGridViewTextBoxColumn"
        Me.MailDataGridViewTextBoxColumn.ReadOnly = True
        '
        'Pocet
        '
        Me.Pocet.DataPropertyName = "Pocet"
        DataGridViewCellStyle2.NullValue = "0"
        Me.Pocet.DefaultCellStyle = DataGridViewCellStyle2
        Me.Pocet.HeaderText = "Pocet"
        Me.Pocet.Name = "Pocet"
        Me.Pocet.ReadOnly = True
        '
        'Datum
        '
        Me.Datum.DataPropertyName = "Datum"
        Me.Datum.HeaderText = "Datum"
        Me.Datum.Name = "Datum"
        Me.Datum.ReadOnly = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(355, 188)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(101, 30)
        Me.Button4.TabIndex = 27
        Me.Button4.Text = "Zmeniť práva"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'UcetBindingSource
        '
        Me.UcetBindingSource.DataMember = "Ucet"
        Me.UcetBindingSource.DataSource = Me.RotekDataSet
        '
        'UcetTableAdapter
        '
        Me.UcetTableAdapter.ClearBeforeFill = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(260, 220)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(90, 30)
        Me.Button5.TabIndex = 28
        Me.Button5.Text = "Pridať uživateľa"
        Me.Button5.UseVisualStyleBackColor = True
        Me.Button5.Visible = False
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.Color.White
        Me.RichTextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.RichTextBox1.ForeColor = System.Drawing.Color.DarkBlue
        Me.RichTextBox1.Location = New System.Drawing.Point(520, 20)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(135, 198)
        Me.RichTextBox1.TabIndex = 29
        Me.RichTextBox1.Text = resources.GetString("RichTextBox1.Text")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(553, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 17)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Zmenené:"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(532, 224)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(114, 30)
        Me.Button6.TabIndex = 31
        Me.Button6.Text = "Pridať požiadavku"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.DataSource = Me.SkladListBindingSource
        Me.ListBox1.DisplayMember = "Nazov"
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(12, 132)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(89, 43)
        Me.ListBox1.TabIndex = 32
        Me.ListBox1.ValueMember = "ID"
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
        'Spust
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.WindowsApplication2.My.Resources.Resources.Pozadie
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(658, 262)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button17)
        Me.Controls.Add(Me.Button16)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Spust"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Spust"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MailyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UcetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SkladListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents Button16 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents MailyBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MailyTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.MailyTableAdapter
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents NickDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MailDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pocet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Datum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents UcetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents UcetTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.UcetTableAdapter
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents SkladListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SkladListTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.SkladListTableAdapter
End Class
