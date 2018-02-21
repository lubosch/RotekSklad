<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Materialy
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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.MaterialDruhBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RotekDataSet = New WindowsApplication2.RotekDataSet()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.MaterialNazovBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.ListBox4 = New System.Windows.Forms.ListBox()
        Me.MaterialNazovBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MaterialDruhTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.MaterialDruhTableAdapter()
        Me.MaterialNazovTableAdapter = New WindowsApplication2.RotekDataSetTableAdapters.MaterialNazovTableAdapter()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ListBox5 = New System.Windows.Forms.ListBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.ListBox6 = New System.Windows.Forms.ListBox()
        Me.MaterialDruhBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.MaterialDruhBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MaterialNazovBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MaterialNazovBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MaterialDruhBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox1.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ListBox1.DataSource = Me.MaterialDruhBindingSource
        Me.ListBox1.DisplayMember = "Nazov"
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(58, 435)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(123, 121)
        Me.ListBox1.TabIndex = 52
        Me.ListBox1.ValueMember = "Nazov"
        '
        'MaterialDruhBindingSource
        '
        Me.MaterialDruhBindingSource.DataMember = "MaterialDruh"
        Me.MaterialDruhBindingSource.DataSource = Me.RotekDataSet
        '
        'RotekDataSet
        '
        Me.RotekDataSet.DataSetName = "RotekDataSet"
        Me.RotekDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ListBox3
        '
        Me.ListBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox3.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ListBox3.DataSource = Me.MaterialNazovBindingSource
        Me.ListBox3.DisplayMember = "Nazov"
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.Location = New System.Drawing.Point(187, 436)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(120, 121)
        Me.ListBox3.TabIndex = 51
        Me.ListBox3.ValueMember = "Nazov"
        '
        'MaterialNazovBindingSource
        '
        Me.MaterialNazovBindingSource.DataMember = "MaterialNazov"
        Me.MaterialNazovBindingSource.DataSource = Me.RotekDataSet
        '
        'TextBox6
        '
        Me.TextBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox6.BackColor = System.Drawing.Color.PaleTurquoise
        Me.TextBox6.Location = New System.Drawing.Point(185, 410)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(122, 20)
        Me.TextBox6.TabIndex = 50
        '
        'TextBox4
        '
        Me.TextBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox4.BackColor = System.Drawing.Color.PaleTurquoise
        Me.TextBox4.Location = New System.Drawing.Point(58, 409)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(123, 20)
        Me.TextBox4.TabIndex = 49
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Viner Hand ITC", 36.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(94, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(593, 78)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Manažment materiálov"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ListBox2
        '
        Me.ListBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox2.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ListBox2.DataSource = Me.MaterialDruhBindingSource
        Me.ListBox2.DisplayMember = "Nazov"
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(386, 435)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(123, 121)
        Me.ListBox2.TabIndex = 57
        Me.ListBox2.ValueMember = "Nazov"
        '
        'ListBox4
        '
        Me.ListBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox4.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ListBox4.DataSource = Me.MaterialNazovBindingSource1
        Me.ListBox4.DisplayMember = "Nazov"
        Me.ListBox4.FormattingEnabled = True
        Me.ListBox4.Location = New System.Drawing.Point(515, 436)
        Me.ListBox4.Name = "ListBox4"
        Me.ListBox4.Size = New System.Drawing.Size(120, 121)
        Me.ListBox4.TabIndex = 56
        Me.ListBox4.ValueMember = "Nazov"
        '
        'MaterialNazovBindingSource1
        '
        Me.MaterialNazovBindingSource1.DataMember = "MaterialNazov"
        Me.MaterialNazovBindingSource1.DataSource = Me.RotekDataSet
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.Color.PaleTurquoise
        Me.TextBox1.Location = New System.Drawing.Point(513, 410)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(122, 20)
        Me.TextBox1.TabIndex = 55
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Viner Hand ITC", 36.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(312, 414)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 78)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "="
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button1
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(641, 501)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(97, 46)
        Me.Button1.TabIndex = 59
        Me.Button1.Text = "Rovnaké"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(185, 281)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(97, 46)
        Me.Button2.TabIndex = 60
        Me.Button2.Text = "Zmazať"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(185, 616)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(97, 46)
        Me.Button3.TabIndex = 61
        Me.Button3.Text = "Zmazať"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label3.Location = New System.Drawing.Point(30, 262)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(128, 91)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "Vymazať druh so všetkými príjemkami, výdajkami a rôznymi názvami"
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label4.Location = New System.Drawing.Point(51, 583)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(128, 99)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "Vymazať tento názov.  Ak neexistuje alternatíva tak sa vymažu aj všetky príjemky " & _
    "a výdajky"
        '
        'MaterialDruhTableAdapter
        '
        Me.MaterialDruhTableAdapter.ClearBeforeFill = True
        '
        'MaterialNazovTableAdapter
        '
        Me.MaterialNazovTableAdapter.ClearBeforeFill = True
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.BackColor = System.Drawing.Color.PaleTurquoise
        Me.TextBox2.Location = New System.Drawing.Point(386, 409)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(123, 20)
        Me.TextBox2.TabIndex = 64
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label5.Location = New System.Drawing.Point(308, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 36)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "Druh"
        '
        'ListBox5
        '
        Me.ListBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox5.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ListBox5.DataSource = Me.MaterialDruhBindingSource
        Me.ListBox5.DisplayMember = "Nazov"
        Me.ListBox5.FormattingEnabled = True
        Me.ListBox5.Location = New System.Drawing.Point(184, 154)
        Me.ListBox5.Name = "ListBox5"
        Me.ListBox5.Size = New System.Drawing.Size(123, 121)
        Me.ListBox5.TabIndex = 67
        Me.ListBox5.ValueMember = "Nazov"
        '
        'TextBox3
        '
        Me.TextBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox3.BackColor = System.Drawing.Color.PaleTurquoise
        Me.TextBox3.Location = New System.Drawing.Point(184, 128)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(123, 20)
        Me.TextBox3.TabIndex = 66
        '
        'TextBox5
        '
        Me.TextBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox5.BackColor = System.Drawing.Color.PaleTurquoise
        Me.TextBox5.Location = New System.Drawing.Point(386, 128)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(123, 20)
        Me.TextBox5.TabIndex = 70
        '
        'ListBox6
        '
        Me.ListBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox6.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ListBox6.DataSource = Me.MaterialDruhBindingSource1
        Me.ListBox6.DisplayMember = "Nazov"
        Me.ListBox6.FormattingEnabled = True
        Me.ListBox6.Location = New System.Drawing.Point(386, 154)
        Me.ListBox6.Name = "ListBox6"
        Me.ListBox6.Size = New System.Drawing.Size(123, 121)
        Me.ListBox6.TabIndex = 68
        Me.ListBox6.ValueMember = "Nazov"
        '
        'MaterialDruhBindingSource1
        '
        Me.MaterialDruhBindingSource1.DataMember = "MaterialDruh"
        Me.MaterialDruhBindingSource1.DataSource = Me.RotekDataSet
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Viner Hand ITC", 36.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(309, 130)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 78)
        Me.Label6.TabIndex = 69
        Me.Label6.Text = "="
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button4
        '
        Me.Button4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(521, 187)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(97, 46)
        Me.Button4.TabIndex = 71
        Me.Button4.Text = "Rovnaké"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label7.Location = New System.Drawing.Point(296, 373)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 34)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Názov"
        '
        'Materialy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 701)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.ListBox6)
        Me.Controls.Add(Me.ListBox5)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.ListBox4)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Name = "Materialy"
        Me.Text = "Manažment materiálov"
        CType(Me.MaterialDruhBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RotekDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MaterialNazovBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MaterialNazovBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MaterialDruhBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox3 As System.Windows.Forms.ListBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox4 As System.Windows.Forms.ListBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents RotekDataSet As WindowsApplication2.RotekDataSet
    Friend WithEvents MaterialDruhBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MaterialDruhTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.MaterialDruhTableAdapter
    Friend WithEvents MaterialNazovBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MaterialNazovTableAdapter As WindowsApplication2.RotekDataSetTableAdapters.MaterialNazovTableAdapter
    Friend WithEvents MaterialNazovBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ListBox5 As System.Windows.Forms.ListBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents ListBox6 As System.Windows.Forms.ListBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents MaterialDruhBindingSource1 As System.Windows.Forms.BindingSource
End Class
