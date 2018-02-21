Public Class Form1


    Public kks As Integer

    Shared Property w As Integer

    Shared Property pocett As Integer

    Shared Property Nastroj As String

    Shared Property ww As Integer


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New Pridatt
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()
        Try

        Catch ex As System.Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)


    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim rw As String = Screen.PrimaryScreen.Bounds.Width / 2


        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        Try

        Catch ex As System.Exception
            Chyby.Show(ex.ToString)
        End Try



    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim f As New vymazať
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()
        Try

        Catch ex As System.Exception
            Chyby.Show(ex.ToString)
        End Try


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim f As New sklad
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

End Class