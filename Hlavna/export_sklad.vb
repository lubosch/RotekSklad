Imports RotekIS

Public Class export_sklad
    Private slovnik As Dictionary(Of Integer, String)


    Private Sub export_sklad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Ine' table. You can move, or remove it, as needed.
        Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        slovnik = New Dictionary(Of Integer, String)
        slovnik.Add(1, "Spotrebný materiál")
        slovnik.Add(2, "Zveráky a ostatné")
        slovnik.Add(3, "Upínací + špeciálne")
        slovnik.Add(4, "Elektronáradie")
        slovnik.Add(5, "Príslušenstvo")
        slovnik.Add(6, "Náradie")
        slovnik.Add(7, "Meradlá")
        slovnik.Add(8, "Spojovací materiál")
        slovnik.Add(9, "Iné")
        slovnik.Add(10, "Kvapaliny")

        '1
        SQL.Stock_SQL.Add(slovnik(1), New String() {"Názov"})
        '2
        SQL.Stock_SQL.Add(slovnik(2), New String() {"Názov"})
        '3
        SQL.Stock_SQL.Add(slovnik(3), New String() {"ID", "Názov"})
        '4
        SQL.Stock_SQL.Add(slovnik(4), New String() {"ID", "Názov"})
        '5
        SQL.Stock_SQL.Add(slovnik(5), New String() {"Názov"})
        '6
        SQL.Stock_SQL.Add(slovnik(6), New String() {"Názov", "Popis"})
        '7
        SQL.Stock_SQL.Add(slovnik(7), New String() {"ID", "Názov"})
        '8
        SQL.Stock_SQL.Add(slovnik(8), New String() {"Názov"})
        '9
        SQL.Stock_SQL.Add(slovnik(9), New String() {"Názov"})
        '10
        SQL.Stock_SQL.Add(slovnik(10), New String() {"Druh", "Typ druhu"})


        Try
            ProgressBar1.Maximum = DataGridView1.RowCount - 1
            ProgressBar1.Minimum = 0


            For i As Integer = 0 To DataGridView1.RowCount - 1
                Dim sql As String = ""
                Dim pocet As Integer = DataGridView1(3, i).Value
                Dim stock_id As Integer = RotekIS.SQL.Stock_SQL.stock_ID(slovnik(pocet))
                Dim ident As String = DataGridView1(0, i).Value
                Dim nazov As String = DataGridView1(1, i).Value
                Dim values As String()
                Dim note As String = DataGridView1(7, i).Value
                Dim photo As String = DataGridView1(6, i).Value
                Dim count As Decimal = DataGridView1(4, i).Value
                Dim wrecked As Decimal = DataGridView1(2, i).Value
                Dim cost As Decimal = DataGridView1(5, i).Value

                Select Case (pocet)
                    Case 1, 2, 5, 8, 9
                        values = New String() {nazov}
                    Case Else
                        values = New String() {ident, nazov}

                End Select

                RotekIS.SQL.Employer_Stock.AddItem(stock_id, values, note, photo, Nothing, count, wrecked, cost, 1)


                ProgressBar1.Value = i
            Next
            ProgressBar1.Hide()
            Chyby.Show("Hotovo")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub


End Class

