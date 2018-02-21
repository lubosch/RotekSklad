Public Class infosky

    Property typ As Integer

    Property popis As Object
    Private Sub rozmer()
        Dim sw As Integer = Me.Height
        Dim rww As Integer = Me.Width / 2

        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(Label1.Text, Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2
        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New Point(rw, 20)

    End Sub
    Private Sub infosky_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.pocetColumn, typ, RotekDataSet.Rotek.NástrojColumn, popis)
        Label1.Text = popis
        rozmer()

        
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Form78.exportovat(DataGridView1)
    End Sub

    Private Sub infosky_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        rozmer
    End Sub
End Class