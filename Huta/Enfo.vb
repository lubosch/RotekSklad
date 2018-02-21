Public Class enfo


    Property zakazka As String


    Private Sub Firma_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Zakazka' table. You can move, or remove it, as needed.
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)

        

        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)

        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)

        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka)
        Label7.Text = zakazka
        Try
            Dim nazov, ulica, psc, mesto, veduci, krajina, dv, du, duk, zk, dp, stav, evidol As String
            Try
                veduci = DataGridView2.Rows(0).Cells(9).Value
                If veduci.Length <> 0 Then Label13.Text = veduci
            Catch ex As Exception
            End Try
            Try
                zk = DataGridView2.Rows(0).Cells(6).Value
                If zk.Length <> 0 Then Label23.Text = zk Else Label23.Text = "Nik"
            Catch ex As Exception
            End Try
            Try
                dp = DataGridView2.Rows(0).Cells(3).Value
                If dp.Length <> 0 Then Label22.Text = dp
            Catch ex As Exception

            End Try

            Try
                nazov = DataGridView2.Rows(0).Cells(1).Value
                If nazov.Length <> 0 Then Label8.Text = nazov
            Catch ex As Exception
            End Try
            Try
                dv = DataGridView2.Rows(0).Cells(2).Value
                If dv.Length <> 0 Then Label15.Text = dv
            Catch ex As Exception
            End Try

            Try
                evidol = DataGridView2.Rows(0).Cells(0).Value
                If evidol.Length <> 0 Then Label27.Text = evidol
            Catch ex As Exception
            End Try

            stav = DataGridView2.Rows(0).Cells(10).Value
            If stav = 0 Then
                Label19.Text = "Nezačatá"
            ElseIf stav = 100 Then
                Label19.Text = "Ukončená"
            Else
                Label19.Text = "Rozpracovaná"
            End If


            Try
                du = DataGridView2.Rows(0).Cells(4).Value
                If du.Length <> 0 Then Label18.Text = du
            Catch ex As Exception
            End Try

            Try
                duk = DataGridView2.Rows(0).Cells(7).Value
                If duk.Length <> 0 Then Label25.Text = duk
            Catch ex As Exception
            End Try

           

            Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, Label8.Text)

            '            If DataGridView1.RowCount = 0 Then Exit Sub

            ulica = DataGridView1.Rows(0).Cells(1).Value
            If ulica.Length <> 0 Then Label9.Text = ulica
            psc = DataGridView1.Rows(0).Cells(3).Value
            If psc.Length <> 0 Then Label10.Text = psc
            mesto = DataGridView1.Rows(0).Cells(2).Value
            If mesto.Length <> 0 Then Label11.Text = mesto
            krajina = DataGridView1.Rows(0).Cells(4).Value
            If krajina.Length <> 0 Then Label12.Text = krajina
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

End Class