Public Class Zobraz_CPO
    Public cp As String

    Private Sub Zobraz_CP_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.CO' table. You can move, or remove it, as needed.
        Me.COTableAdapter.Fill(Me.RotekDataSet.CO)
        'TODO: This line of code loads data into the 'RotekDataSet.CP' table. You can move, or remove it, as needed.
        Me.COBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.CO.pocetColumn, 2, RotekDataSet.CO.NazovColumn, cp)
        For i As Integer = 0 To DataGridView1.RowCount - 1
            Dim cenaKs As Decimal = DataGridView1.Rows(i).Cells(2).Value
            Dim ks As Integer = DataGridView1.Rows(i).Cells(1).Value
            Dim cena As Decimal = cenaKs*ks
            DataGridView1.Rows(i).Cells(3).Value = cena
        Next
        Label1.Text = cp
        rozmers()

    End Sub
    Public Sub rozmers()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2, sw - 100)

        Dim g As Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(Label1.Text, Label1.Font)
        Dim stred As Integer

        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred

        Label1.Location = New Point(rw, 0)

    End Sub

    Private Sub Zobraz_CP_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged
        rozmers()
    End Sub

End Class