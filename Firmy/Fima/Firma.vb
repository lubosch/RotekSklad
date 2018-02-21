Imports System.Data.SqlClient

Public Class Firma

    Property kra As String

    Property ps As String

    Property mes As String

    Property uli As String

    Property naz As String

    Property veduci As String

    Private Sub TextBox5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.Enter
        TextBox5.SelectAll()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        stuk()
        Me.Close()
    End Sub
    Private Sub stuk()
        Dim nazov, mesto, psc, ulica, krajina, ico, dico As String
        nazov = TextBox1.Text
        ulica = TextBox2.Text
        psc = TextBox3.Text
        mesto = TextBox4.Text
        krajina = TextBox5.Text
        ico = TextBox7.Text
        dico = TextBox8.Text

        If nazov.Length = 0 Then
            Chyby.Show("Niečo nie je zadané")
            Exit Sub
        End If
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Dim sql As String

        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, nazov)
        If (DataGridView1.RowCount = 1) OrElse (String.IsNullOrEmpty(naz) = False) Then
            Dim aks As String
            Dim ifg As Integer
            aks = "Firma už je v zozname. Chcete ju aktualizovať so zadanými údajmi?"
            ifg = MsgBox(aks, vbExclamation + vbYesNo, "Overenie")
            If ifg = vbYes Then
                sql = "UPDATE ZoznamF SET Nazov='" & nazov & "', Ulica='" + ulica + "', Mest='" + mesto + "', PSČ='" + psc + "', Krajina='" + krajina + "', ICO='" & ico & "', DICO='" & dico & "' WHERE Nazov='" + naz + "' AND pocet=0"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                If naz <> nazov Then
                    sql = "UPDATE Zakazka SET Zakaznik='" & nazov & "' WHERE Zakaznik='" & naz & "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()

                    sql = "UPDATE Evidencia SET Zakaznik='" & nazov & "' WHERE Zakaznik='" & naz & "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                    naz = nazov

                End If
            Else
                con.Close()
                Exit Sub
            End If
        Else
            sql = "Insert INTO ZoznamF (Nazov, Ulica, Mest, PSČ, Krajina, pocet, ICO, DICO) VALUES ('" + nazov + "', '" + ulica + "', '" + mesto + "', '" + psc + "', '" + krajina + "', '" & 0 & "','" & ico & "','" & dico & "')"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            sql = "Insert INTO Firmy (Meno, pocet, Kolko) VALUES ('" + nazov + "',  '" & 0 & "' , '" & 0 & "')"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()

        End If


        con.Close()

        If TextBox6.Text.Length <> 0 Then
            stuk2()
        End If

    End Sub

    Private Sub Firma_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        Try

            If String.IsNullOrEmpty(naz) = False Then
                TextBox1.Text = naz
                Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, naz)
                If DataGridView1.RowCount = 1 Then
                    Try
                        TextBox2.Text = DataGridView1.Rows(0).Cells(1).Value
                        TextBox3.Text = DataGridView1.Rows(0).Cells(3).Value
                        TextBox4.Text = DataGridView1.Rows(0).Cells(2).Value
                        TextBox5.Text = DataGridView1.Rows(0).Cells(4).Value

                        Try
                            TextBox7.Text = DataGridView1.Rows(0).Cells(7).Value
                        Catch ex As Exception
                        End Try

                        Try
                            TextBox8.Text = DataGridView1.Rows(0).Cells(8).Value
                        Catch ex As Exception
                        End Try

                        If TextBox5.Text.Length = 0 Then TextBox5.Text = "Slovensko"
                    Catch ex As Exception
                    End Try
                End If
            End If
            If String.IsNullOrEmpty(veduci) Then
            Else

                TextBox6.Text = veduci
            End If

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp, TextBox3.KeyUp, TextBox2.KeyUp, TextBox1.KeyUp
        If e.KeyValue = Keys.Enter Then
            stuk()
            Me.Close()
        ElseIf e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Sub stuk2()
        Dim nazov, veduci As String
        nazov = TextBox1.Text
        veduci = TextBox6.Text
        If veduci.Length = 0 Then
            Chyby.Show("Nie je zadané meno vedúceho")
            Exit Sub
        End If
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Dim sql As String

        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.ZoznamF.pocetColumn, 1, RotekDataSet.ZoznamF.NazovColumn, nazov, RotekDataSet.ZoznamF.VeducColumn, veduci)
        If DataGridView1.RowCount = 0 Then
        Else
            Chyby.Show("Už je pridaný")
            Exit Sub
        End If
        sql = "Insert INTO ZoznamF (Nazov, pocet, Veduc) VALUES ('" + nazov + "', '" & 1 & "','" + veduci + "')"
        cmd = New SqlCommand(sql, con)
        cmd.ExecuteNonQuery()
        con.Close()
        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        zmena()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        stuk2()
    End Sub
    Public Sub zmena()
        Try
            ListBox1.Items.Clear()
            Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.ZoznamF.pocetColumn, 1, RotekDataSet.ZoznamF.NazovColumn, TextBox1.Text)
            For i As Integer = 0 To DataGridView1.RowCount - 1
                ListBox1.Items.Add(DataGridView1.Rows(i).Cells(6).Value)
            Next
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        zmena()
    End Sub

    Private Sub TextBox6_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        If e.KeyValue = Keys.Enter Then
            stuk2()
        ElseIf e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub ZmazaťToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ZmazaťToolStripMenuItem.Click
        If MessageBox.Show("Naozajchcete zmazať zamestnanca """ & ListBox1.SelectedItem.ToString & """ z firmy""" & TextBox1.Text & """?", " Nerob to!", MessageBoxButtons.YesNo) = vbYes Then
            Dim sql As String = "DELETE FROM ZoznamF WHERE pocet=1 AND Nazov='" & TextBox1.Text & "' AND Veduc='" & ListBox1.SelectedItem.ToString & "'"
            Form78.sqa(sql)
            Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
            zmena()
        End If

    End Sub

    Private Sub ListBox1_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseUp
        If e.Button = MouseButtons.Right Then
            If Form78.heslo = Form78.admin OrElse Form78.heslo = Form78.zakazkar Then
                ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
                ContextMenuStrip1.Tag = ListBox1.IndexFromPoint(New Point(e.X, e.Y))
                ListBox1.SelectedItem = ListBox1.Items(ContextMenuStrip1.Tag)
            End If
        End If

    End Sub


End Class