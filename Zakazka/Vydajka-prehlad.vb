Public Class Vydajka_prehlad
    Public vydajka As String

    Private Sub Prijemka_prehlad_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Vydajky' table. You can move, or remove it, as needed.
        Me.VydajkyTableAdapter.Fill(Me.RotekDataSet.Vydajky)
        'TODO: This line of code loads data into the 'RotekDataSet.Material_Vydajka' table. You can move, or remove it, as needed.
        Label1.Text = vydajka
        DataGridView1.DataSource = Vydajka_SQL.materialByName(vydajka)

        DataGridView1.Columns("ID").Visible = False
        DataGridView1.Columns("mz_ID").Visible = False

        zafarbi()


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
        If e.Value Is Nothing OrElse Not e.Value.GetType.Equals(New Decimal().GetType) OrElse e.Value <> -1 Then
        Else
            e.Value = ""
        End If
    End Sub

    Private Sub zafarbi()
        Dim i As Integer
        For i = 0 To 2
            DataGridView1.Columns(i).DefaultCellStyle.BackColor = Color.Yellow
        Next
        For i = 3 To 9
            DataGridView1.Columns(i).DefaultCellStyle.BackColor = Color.Azure
        Next
        For i = 10 To 15
            DataGridView1.Columns(i).DefaultCellStyle.BackColor = Color.LightGreen
        Next

    End Sub

End Class