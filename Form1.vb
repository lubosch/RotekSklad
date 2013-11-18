Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New Form2
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim f As New Form2
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()

    End Sub

    Private Sub RotekDataSet1BindingSource_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RotekDataSet1BindingSource.CurrentChanged

    End Sub
End Class

