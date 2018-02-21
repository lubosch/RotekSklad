Imports RotekIS

Public Class export
    Private slovnik As Dictionary(Of Integer, String)

    Private Sub export_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Rotek' table. You can move, or remove it, as needed.
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource.Filter = String.Format("{0} > '{1}' ", RotekDataSet.Rotek.pocetColumn, 0)

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

        ProgressBar1.Maximum = DataGridView1.RowCount
        ProgressBar1.Minimum = 0
        Dim stock_id As Integer
        Dim pocet As Integer
        Dim menopriezvo As String
        For i As Integer = 0 To DataGridView1.RowCount - 1
            Try
                pocet = DataGridView1(2, i).Value
                Dim name As String
                Dim surname As String
                Dim values As String()
               menopriezvo = DataGridView1(8, i).Value.ToString

                Dim ident As String = DataGridView1(4, i).Value
                Dim nazov As String = DataGridView1(3, i).Value
                Dim vlastnost As String = DataGridView1(5, i).Value.ToString
                If vlastnost = "Klasicky" Then vlastnost = "HSS"
                If pocet = 1 Then
                    name = DataGridView1(0, i).Value
                    surname = DataGridView1(1, i).Value
                    values = New String() {nazov, ident, vlastnost}
                    stock_id = RotekIS.SQL.Stock_SQL.stock_ID("Sklad nástrojov")
                Else
                    stock_id = RotekIS.SQL.Stock_SQL.stock_ID(slovnik(pocet - 3))
                    Dim menpr As String() = Stocks.Employer.Names(menopriezvo)
                    name = menpr(0)
                    surname = menpr(1)
                    Select Case (pocet)
                        Case 4, 5, 8, 11, 12
                            values = New String() {nazov}
                        Case Else
                            values = New String() {ident, nazov}

                    End Select

                End If
                Dim count As Decimal = DataGridView1(6, i).Value
                Dim wrecked As Decimal
                If DataGridView1(7, i).Value.ToString = "" Then
                    wrecked = 0
                Else
                    wrecked = DataGridView1(7, i).Value

                End If

                SQL.Employer_Stock.AddItemEmployer_Export(name, surname, stock_id, values, count, wrecked)

                ProgressBar1.Value = i
                'Rotek_IS.SQL.Employer_Stock.AddItem();
            Catch ex As SQL.MS_SQL_Exception
            Catch ex As Exception
                'MessageBox.Show(ex.ToString & " " & stock_id & " " & pocet)
            End Try
        Next
        ProgressBar1.Hide()
        Chyby.Show("Hotovo")
    End Sub
End Class