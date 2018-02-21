Imports System.Windows.Forms

Public Class datum_box
    Public datum As Date
    Public dz As Date
    Public evidol As String
    Public zakazka As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        datum = DateTimePicker1.Value
        dz = DateTimePicker2.Value
        zakazka = TextBox1.Text
        evidol = TextBox2.Text
        If String.IsNullOrEmpty(evidol) or String.IsNullOrEmpty(zakazka) Then
            Chyby.Show("Nevyplnené všetky údaje")
            Exit Sub
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub datum_box_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Zakazka' table. You can move, or remove it, as needed.
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        'TODO: This line of code loads data into the 'RotekDataSet.Zakazka' table. You can move, or remove it, as needed.
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        Try
            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)

            DateTimePicker2.Text = DateValue(Now)
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}' ", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, "/" & Now.Year.ToString.Replace("20", ""))
            ZakazkaBindingSource.Sort = "Zakazka"

            Try
                Dim posledna As String = DataGridView4.Rows(DataGridView4.RowCount - 1).Cells(0).Value
                Dim i As Integer = posledna.Substring(0, posledna.IndexOf("/"))
                TextBox1.Text = Format(i + 1, "0000") & "/" & Year(Now).ToString.Substring(2)
            Catch ex As Exception
                Dim kks As Integer = DataGridView4.RowCount + 1
                TextBox1.Text = Format(kks, "0000") & "/" & Year(Now).ToString.Substring(2)
            End Try
            TextBox2.Text = Form78.uzivatel
            TextBox1.Focus()

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
End Class
