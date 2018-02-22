Option Explicit On
Public Class skladvz
    Shared Property nastr As String

    Shared Property p As Integer

    Public x As Integer

    Public over As Integer

    Public tex, tex2 As String

    Shared Property sku As Integer

    Shared Property reg As String

    Shared Property v As Integer

    Shared Property cenaa As String

    Shared Property nastr2 As String
    Public Sub rozmer()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2, sw - 220)

        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString("Sklad vzácnych vecí", Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2

        Dim rw As String = Screen.PrimaryScreen.Bounds.Width / 2 - stred
        Label1.Location = New Point(rw, 10)
    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo
            Case Form78.admin
                Button1.Show()
                Button5.Show()
            Case Form78.zakazkar
            Case Form78.skladnik
                Button1.Show()
                Button5.Show()
            Case Else
                Button1.Hide()
                Button5.Hide()
        End Select
    End Sub
    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button1.Hide()
        Button5.Hide()
        poverenie()
        rozmer()

        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
       
        Button3.Hide()

        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        Me.VzacnostiBindingSource.Sort = "Nazov"
        x = DataGridView1.RowCount
        ceny
        
    End Sub

    Private Sub ceny()

        For radky = 0 To DataGridView1.RowCount - 1
            Dim pe As Double = DataGridView1.Rows(radky).Cells(3).Value
            DataGridView1.Rows(radky).Cells(4).Value = pe
            pe = DataGridView1.Rows(radky).Cells(10).Value
            DataGridView1.Rows(radky).Cells(6).Value = pe
            If DataGridView1.Rows(radky).Cells(9).Value.ToString.Length = 0 Then DataGridView1.Rows(radky).Cells(7).Value = False Else DataGridView1.Rows(radky).Cells(7).Value = True
        Next
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        sku = 1
        Do While (sku = 1)
            sku = 0
            Dim f As New nastrvz
            f.TopLevel = True
            f.Dock = DockStyle.None
            f.ShowDialog()
            Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)

            ceny()

        Loop
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Button2.Hide()
        Button3.Show()
        Try
            Dim a As Integer
            a = TextBox1.Text
            Me.VzacnostiBindingSource.Filter = String.Format("{0} < '{1}'", RotekDataSet.Vzacnosti.PocetColumn, a)
        Catch ex As SystemException
            Me.VzacnostiBindingSource.Filter = String.Format("{0} < '{1}'", RotekDataSet.Vzacnosti.PocetColumn, 10)
        End Try
        ceny()
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Button3.Hide()
        Button2.Show()
        Me.VzacnostiBindingSource.Filter = String.Format("{0} > '{1}'", RotekDataSet.Vzacnosti.PocetColumn, -1)
    End Sub

    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = "10"
        TextBox1.SelectAll()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox2.Text = ""
        TextBox2.Focus()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox2.MouseClick
        TextBox2.Text = ""
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        Me.VzacnostiBindingSource.Filter = String.Format("{0} LIKE '{1}%'", RotekDataSet.Vzacnosti.CisloColumn, TextBox2.Text)
        ceny
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        Me.VzacnostiBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}%'", RotekDataSet.Vzacnosti.CisloColumn, TextBox2.Text, RotekDataSet.Vzacnosti.NazovColumn, TextBox3.Text)
        ceny
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim f As New srotVz
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)

        ceny()

    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick, DataGridView1.CellClick
        Dim kks As Integer = e.RowIndex
        Dim cesta As String
        If DataGridView1.Rows(kks).Cells(9).Value.ToString.Length = 0 Then Exit Sub Else cesta = DataGridView1.Rows(kks).Cells(9).Value
        Dim f As New Fotk
        f.TopLevel = True
        f.cesta = cesta
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub

    Private Sub skladvz_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        rozmer()

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Form78.exportovat(DataGridView1)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        DataGridView1.Rows(e.RowIndex).Selected = True
    End Sub
End Class