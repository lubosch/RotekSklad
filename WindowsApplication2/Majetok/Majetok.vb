Public Class Majetok
    Private typ As Integer
    Private typp As String

    Public Sub New(ByVal t As Integer)

        typ = t
        ' This call is required by the designer.
        InitializeComponent()




        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub rozmer()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2, sw - 180)
        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(typp, Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New Point(rw, 10)
    End Sub

    Private Sub Majetok_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.MajetokTableAdapter.Fill(Me.RotekDataSet.Majetok)
        Me.MajetokBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Majetok.TypColumn, typ)

        Select Case typ
            Case 1
                Label1.Text = "Hmotný majetok"
            Case 2
                Label1.Text = "Nehmotný majetok"
            Case 3
                Label1.Text = "Majetok zákazníka"

        End Select
        typp = Label1.Text

        stlpce()
        rozmer()


    End Sub
    Private Sub stlpce()
        Try
            For i As Integer = 0 To DataGridView1.RowCount
                If DataGridView1.Rows(i).Cells(2).Value.ToString.Length <> 0 Then
                    DataGridView1.Rows(i).Cells(6).Value = True
                Else : DataGridView1.Rows(i).Cells(6).Value = False
                End If
            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New M_pridat(typ)
        f.ShowDialog()
        Me.MajetokTableAdapter.Fill(Me.RotekDataSet.Majetok)
        stlpce()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Me.MajetokBindingSource.Filter = String.Format("{0}='{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8} LIKE '%{9}%' ", RotekDataSet.Majetok.TypColumn, typ, RotekDataSet.Majetok.IdentColumn, TextBox1.Text, RotekDataSet.Majetok.NazovColumn, TextBox2.Text, RotekDataSet.Majetok.IzbaColumn, TextBox3.Text, RotekDataSet.Majetok.MiestoColumn, TextBox4.Text)

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Me.MajetokBindingSource.Filter = String.Format("{0}='{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8} LIKE '%{9}%' ", RotekDataSet.Majetok.TypColumn, typ, RotekDataSet.Majetok.IdentColumn, TextBox1.Text, RotekDataSet.Majetok.NazovColumn, TextBox2.Text, RotekDataSet.Majetok.IzbaColumn, TextBox3.Text, RotekDataSet.Majetok.MiestoColumn, TextBox4.Text)

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Me.MajetokBindingSource.Filter = String.Format("{0}='{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8} LIKE '%{9}%' ", RotekDataSet.Majetok.TypColumn, typ, RotekDataSet.Majetok.IdentColumn, TextBox1.Text, RotekDataSet.Majetok.NazovColumn, TextBox2.Text, RotekDataSet.Majetok.IzbaColumn, TextBox3.Text, RotekDataSet.Majetok.MiestoColumn, TextBox4.Text)

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Me.MajetokBindingSource.Filter = String.Format("{0}='{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8} LIKE '%{9}%' ", RotekDataSet.Majetok.TypColumn, typ, RotekDataSet.Majetok.IdentColumn, TextBox1.Text, RotekDataSet.Majetok.NazovColumn, TextBox2.Text, RotekDataSet.Majetok.IzbaColumn, TextBox3.Text, RotekDataSet.Majetok.MiestoColumn, TextBox4.Text)

    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick

        stlpce()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim f As New M_odstranit(typ)
        f.ShowDialog()
        Me.MajetokTableAdapter.Fill(Me.RotekDataSet.Majetok)
        stlpce()
    End Sub

    Private Sub Majetok_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        rozmer()

    End Sub
End Class