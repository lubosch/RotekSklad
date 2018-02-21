Public Class Odpadky

    Private Sub Odpadky_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Odpad' table. You can move, or remove it, as needed.
        Me.OdpadTableAdapter.Fill(Me.RotekDataSet.Odpad)
        Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Odpad.pocetColumn, 0)

        rozmers()

    End Sub

    Private Sub rozmers()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2, sw - 165)
        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString("Evidencia zakaziek", Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New System.Drawing.Point(rw, 0)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim f As New Odpadky_pridat
        f.ShowDialog()
        f.Dispose()
        Me.OdpadTableAdapter.Fill(Me.RotekDataSet.Odpad)
        filtruj()

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Form78.exportovat(DataGridView1)
    End Sub

    Private Sub Odpadky_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged
        rozmers
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If DateTimePicker1.Visible=False Then
            DataGridView1.Columns(2).Visible = True
            DataGridView1.Columns(3).Visible = True
            DataGridView1.Columns(7).Visible = True
            TextBox3.Visible = True
            Label5.Visible = True
            DateTimePicker1.Visible = True
            Label4.Visible = True
            filtruj()
            Button4.Show()
        ElseIf TextBox3.Visible = True Then
            TextBox3.Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(4).HeaderText = "Rozmer"
            DataGridView1.Columns(5).HeaderText = "Kusov"
            DataGridView1.Columns(7).Visible = False
            DataGridView1.Columns(6).Visible = False

            Label5.Visible = False
            filtruj()
        Else
            Button4.Hide()
            DataGridView1.Columns(4).HeaderText = "Koľko"
            DataGridView1.Columns(5).HeaderText = "Cena"
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(6).Visible = True
            DateTimePicker1.Visible = False
            TextBox3.Visible = False
            Label5.Visible = False
            Label4.Visible = False
            filtruj()
        End If

    End Sub
    Private Sub filtruj()
        
        If DateTimePicker1.Visible = False Then
            Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' ", RotekDataSet.Odpad.pocetColumn, 0, RotekDataSet.Odpad.NazovColumn, TextBox2.Text, RotekDataSet.Odpad.DruhColumn, TextBox1.Text)
        ElseIf TextBox3.Visible = True Then
            Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6}<='{7}' AND {8} LIKE '{9}%'", RotekDataSet.Odpad.pocetColumn, 1, RotekDataSet.Odpad.NazovColumn, TextBox2.Text, RotekDataSet.Odpad.DruhColumn, TextBox1.Text, RotekDataSet.Odpad.DatumColumn, DateTimePicker1.Value.ToShortDateString, RotekDataSet.Odpad.DLColumn, TextBox3.Text)
        Else
            Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6}<='{7}'", RotekDataSet.Odpad.pocetColumn, 2, RotekDataSet.Odpad.NazovColumn, TextBox2.Text, RotekDataSet.Odpad.DruhColumn, TextBox1.Text, RotekDataSet.Odpad.DatumColumn, DateTimePicker1.Value.ToShortDateString)
        End If
        Button4.Text = "Iba a len"

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        filtruj()

    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        filtruj
    End Sub

    Private Sub TextBox1_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox1.Enter
        TextBox1.Text = ""
    End Sub

    Private Sub TextBox2_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox2.Enter
        TextBox2.Text = ""
    End Sub

    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged
        filtruj()

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If Button4.Text = "Iba a len" Then
            If TextBox3.Visible = True Then
                Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6}='{7}' AND {8} LIKE '{9}%'", RotekDataSet.Odpad.pocetColumn, 1, RotekDataSet.Odpad.NazovColumn, TextBox2.Text, RotekDataSet.Odpad.DruhColumn, TextBox1.Text, RotekDataSet.Odpad.DatumColumn, DateTimePicker1.Value.ToShortDateString, RotekDataSet.Odpad.DLColumn, TextBox3.Text)
            Else
                Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6}='{7}'", RotekDataSet.Odpad.pocetColumn, 2, RotekDataSet.Odpad.NazovColumn, TextBox2.Text, RotekDataSet.Odpad.DruhColumn, TextBox1.Text, RotekDataSet.Odpad.DatumColumn, DateTimePicker1.Value.ToShortDateString, RotekDataSet.Odpad.DLColumn, TextBox3.Text)
            End If
            Button4.Text = "Iba a menej"
        Else
            filtruj()
        End If

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        filtruj()
    End Sub
End Class