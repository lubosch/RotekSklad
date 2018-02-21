Public Class enfo


    Property zakazka As String


    Private Sub Firma_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Huta' table. You can move, or remove it, as needed.
        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        'TODO: This line of code loads data into the 'RotekDataSet1.ZoznamF' table. You can move, or remove it, as needed.
        'TODO: This line of code loads data into the 'RotekDataSet.ZoznamF' table. You can move, or remove it, as needed.
        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        'TODO: This line of code loads data into the 'RotekDataSet.ZoznamF' table. You can move, or remove it, as needed.
        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)

        Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 2, RotekDataSet.Huta.zakazkaColumn, zakazka)

        Label13.Text = DataGridView2.Rows(0).Cells(0).Value
        Label8.Text = DataGridView2.Rows(0).Cells(2).Value
        Label15.Text = DataGridView2.Rows(0).Cells(1).Value
        Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, label8.Text)
        Label9.Text = DataGridView1.Rows(0).Cells(1).Value
        Label10.Text = DataGridView1.Rows(0).Cells(3).Value
        Label11.Text = DataGridView1.Rows(0).Cells(2).Value
        Label12.Text = DataGridView1.Rows(0).Cells(4).Value




    End Sub

End Class