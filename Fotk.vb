Public Class Fotk

    Property cesta As String



    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Me.Close()

    End Sub

    Private Sub Fotk_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            PictureBox1.Image = System.Drawing.Image.FromFile(cesta)

            Me.BackColor = Color.Red
            Dim sirka As Integer = PictureBox1.Image.Width
            Dim vyska As Integer = PictureBox1.Image.Height

            Dim obrv As Integer = PictureBox1.Size.Height - 100
            Dim obrs As Integer = PictureBox1.Size.Width - 100
            If vyska < 200 Then
                Dim pom As Double
                pom = vyska / sirka
                vyska = 200
                sirka = vyska / pom
            ElseIf sirka < 200 Then
                Dim pom As Double
                pom = sirka / vyska
                sirka = 200
                vyska = sirka / pom
            End If
            If vyska > obrv Then
                Dim pom As Double
                pom = vyska / sirka
                vyska = obrv
                sirka = vyska / pom
            ElseIf sirka > obrs Then
                Dim pom As Double
                pom = sirka / vyska
                sirka = obrs
                vyska = sirka / pom
            End If

            vecsi(vyska, sirka)
        Catch ex As Exception
            'Chyby.Show(ex.ToString)
            Me.Close()
        End Try

    End Sub
    Public Sub vecsi(ByRef vys As Integer, ByRef sir As Integer)

        Dim bm_source As New Bitmap(PictureBox1.Image)
        Dim bm_dest As New Bitmap(CInt(sir), CInt(vys))
        Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)
        gr_dest.DrawImage(bm_source, 0, 0, sir, vys)
        PictureBox1.Image = bm_dest
    End Sub

    Private Sub Fotk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click
        Me.Close()
    End Sub
End Class