Public Class Vydajka_prehlad
    Public vydajka As String

    Private Sub Prijemka_prehlad_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Material_Vydajka' table. You can move, or remove it, as needed.
        Label1.Text = vydajka
        DataGridView1.DataSource = Vydajka_SQL.materialByName(vydajka)



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

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.Value Is Nothing OrElse e.Value.ToString <> "-1" Then
        Else
            e.Value = ""
        End If

    End Sub
End Class