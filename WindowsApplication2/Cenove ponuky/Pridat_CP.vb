Imports System.Data.SqlClient

Public Class Pridat_CP
    Public cp As String

    Private Sub Pridat_CP_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            'TODO: This line of code loads data into the 'RotekDataSet.CP' table. You can move, or remove it, as needed.
            Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
            'TODO: This line of code loads data into the 'RotekDataSet.ZoznamF' table. You can move, or remove it, as needed.
            Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
            Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Huta.pocetColumn, 0)
            Me.ZoznamFBindingSource1.Filter = String.Format("{0} = '{1}'", RotekDataSet.Huta.pocetColumn, -1)
            ZoznamFBindingSource.Sort = "Nazov"
            If String.IsNullOrEmpty(cp) Then
                Me.CPBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} LIKE '{3}%' AND {4} LIKE '%{5}' ", RotekDataSet.CP.pocetColumn, 1, RotekDataSet.CP.NazovColumn, "CP ", RotekDataSet.CP.NazovColumn, "/" & rok())
                Me.CPBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '{3}%' AND {4} LIKE '%{5}' ", RotekDataSet.CP.pocetColumn, 1, RotekDataSet.CP.NazovColumn, "CP fhfghfghfhfg564r98sx", RotekDataSet.CP.NazovColumn, "/" & rok())
                Me.CPBindingSource1.Sort = "Nazov DESC"
                If DataGridView2.RowCount = 0 Then
                    TextBox1.Text = "CP 0001/" & rok()
                Else
                    Dim cp As String = DataGridView2.Rows(0).Cells(1).Value
                    cp = cp.Replace("CP ", "")
                    cp = cp.Replace("/" & rok(), "")
                    Try
                        Dim i As Integer = cp
                        TextBox1.Text = "CP " & Format(i + 1, "0000") & "/" & rok()
                    Catch ex As Exception
                        TextBox1.Text = ""
                    End Try

                End If
                ComboBox1.Text = ""
                ComboBox2.Text = ""
            Else
                Try


                    Me.CPBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' ", RotekDataSet.CP.pocetColumn, 2, RotekDataSet.CP.NazovColumn, cp)
                    TextBox1.Text = cp
                    Me.CPBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.CP.pocetColumn, 1, RotekDataSet.CP.NazovColumn, cp)
                    TextBox2.Text = DataGridView2.Rows(0).Cells(5).Value
                    TextBox3.Text = DataGridView2.Rows(0).Cells(6).Value
                    ComboBox1.Text = DataGridView2.Rows(0).Cells(3).Value
                    ComboBox2.Text = DataGridView2.Rows(0).Cells(4).Value
                    DateTimePicker1.Value = (DataGridView2.Rows(0).Cells(2).Value)
                    Label7.Enabled = True
                    TextBox4.Focus()
                Catch ex As Exception
                    Chyby.Show(ex.ToString)
                End Try
            End If
            napln()
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Private Sub napln()
        For i As Integer = 0 To DataGridView1.RowCount - 1
            DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(2).Value * DataGridView1.Rows(i).Cells(1).Value
        Next
    End Sub
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Polozka()
    End Sub
    Private Function Vseobecne() As Boolean
        Try
            Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)

            Dim nazov, popis, firma, veduci, poznamka, cisloPoziadavky As String
            Dim datum, datum1 As DateTime

            nazov = TextBox1.Text
            popis = TextBox2.Text
            poznamka = TextBox3.Text
            cisloPoziadavky = TextBox8.Text
            firma = ComboBox1.Text
            veduci = ComboBox2.Text
            datum = DateTimePicker1.Value
            datum1 = DateTimePicker2.Value

            If poznamka.Length = 0 Then
                poznamka = "-"
            End If
            If String.IsNullOrEmpty(nazov) Or String.IsNullOrEmpty(popis) Or String.IsNullOrEmpty(firma) Then
                Chyby.Show("Nevyplnené hlavné údaje")
                Return False
            End If

            Me.ZoznamFBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.ZoznamF.NazovColumn, firma, RotekDataSet.ZoznamF.pocetColumn, 0)
            If DataGridView5.RowCount < 1 Then
                If MsgBox("Nenašla sa taká firma. Chcete pridať novú firmu?", vbQuestion + vbYesNo, "Neexistuje") = vbYes Then
                    Dim f As New Firma()
                    f.naz = firma
                    f.veduci = veduci
                    f.ShowDialog()

                    Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)

                    ComboBox1.Text = firma
                    ComboBox2.Text = veduci
                    Return Vseobecne()
                Else
                    Return False
                End If
            End If
            Dim con As New SqlConnection
            Dim sql As String
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            Dim cmd As New SqlCommand

            Me.ZoznamFBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.ZoznamF.NazovColumn, firma, RotekDataSet.ZoznamF.pocetColumn, 1, RotekDataSet.ZoznamF.VeducColumn, veduci)
            If DataGridView5.RowCount < 1 And ComboBox2.Text.Length > 0 Then
                If MsgBox("Nenašiel sa taký zamestnanec. Chcete pridať nového? ", vbQuestion + vbYesNo, "Neexistuje") = vbYes Then
                    sql = "Insert INTO ZoznamF (Nazov, pocet, Veduc) VALUES ('" + firma + "', '" & 1 & "','" + veduci + "')"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                    con.Close()
                    Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
                    ComboBox1.Text = firma
                    ComboBox2.Text = veduci
                    Return Vseobecne()
                Else
                    con.Close()
                    Return False
                End If
            End If

            Me.CPBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' ", RotekDataSet.CP.pocetColumn, 1, RotekDataSet.CP.NazovColumn, nazov)
            If DataGridView2.RowCount <> 0 And Label7.Enabled = False Then
                Chyby.Show("Už existuje")
                con.Close()
                Return False
            ElseIf DataGridView2.RowCount <> 0 Then

                If TextBox1.Text = Me.cp Then
                    sql = "UPDATE CP SET Firma='" & ComboBox1.Text & "', Veduci='" & ComboBox2.Text & "', CisloPoziadavky='" & cisloPoziadavky & "' Poznamka='" & TextBox3.Text & "', Datum='" & datum.ToString("yyyy-MM-dd") & "', Popis='" & popis & "' WHERE pocet=1 AND Nazov='" & nazov & "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                Else
                    Chyby.Show("Už existuje")
                    TextBox1.Text = Me.cp
                End If
                con.Close()
                Return True
            ElseIf Label7.Enabled = True Then
                sql = "UPDATE CP SET Nazov='" & nazov & "' WHERE Nazov='" & Me.cp & "'"
                Me.cp = nazov
            Else
                If datum1 <> New DateTime(1800, 1, 1) Then
                    sql = "INSERT INTO CP (Nazov, Popis, Firma, Veduci, Datum, Cena, CisloPoziadavky, Poznamka, pocet, Evidoval, DU) VALUES('" & nazov & "','" & popis & "','" & firma & "','" & veduci & "','" & datum.ToString("yyyy-MM-dd") & "','" & 0 & "','" & cisloPoziadavky & "','" & poznamka & "'," & 1 & ",'" & Form78.uzivatel & "','" & datum1.ToString("yyyy-MM-dd") & "')"
                Else
                    sql = "INSERT INTO CP (Nazov, Popis, Firma, Veduci, Datum, Cena, CisloPoziadavky, Poznamka, pocet, Evidoval) VALUES('" & nazov & "','" & popis & "','" & firma & "','" & veduci & "','" & datum.ToString("yyyy-MM-dd") & "','" & 0 & "','" & cisloPoziadavky & "','" & poznamka & "'," & 1 & ",'" & Form78.uzivatel & "')"
                End If
            End If
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            con.Close()

            Label7.Enabled = True
            Me.cp = TextBox1.Text
            'TextBox2.ReadOnly = True
            'TextBox3.ReadOnly = True
            TextBox1.Focus()

            DateTimePicker1.Enabled = False
            Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
            Me.CPBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.CP.pocetColumn, 2, RotekDataSet.CP.NazovColumn, nazov)
            Return True
        Catch ex As Exception
            Chyby.Show(ex.ToString)
            Return False
        End Try
    End Function
    Private Sub Polozka()
        If Not Vseobecne() Then
            Exit Sub
        End If
        'Chyby.Show("")
        Try

            Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
            Dim nazov, polozka, kusov As String
            Dim cena, cena1 As Double
            Dim datum As DateTime
            If TextBox4.Text.Length = 0 OrElse TextBox5.Text.Length = 0 Then
                Exit Sub
            End If

            nazov = TextBox1.Text
            datum = DateTimePicker1.Value
            polozka = TextBox4.Text
            kusov = TextBox5.Text
            Try
                cena = TextBox6.Text
            Catch ex As Exception
                cena = 0
                Exit Sub
            End Try
            Dim sql As String

            Me.CPBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.CP.pocetColumn, 1, RotekDataSet.CP.NazovColumn, nazov)
            Try
                cena1 = cena * kusov + DataGridView2.Rows(0).Cells(0).Value
            Catch ex As Exception
                Chyby.Show(ex.ToString)
                Exit Sub

            End Try

            Me.CPBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.CP.pocetColumn, 2, RotekDataSet.CP.PolozkaColumn, polozka, RotekDataSet.CP.NazovColumn, nazov)
            If DataGridView2.RowCount <> 0 Then
                cena1 = cena1 - DataGridView2.Rows(0).Cells(0).Value * DataGridView2.Rows(0).Cells(8).Value
                sql = "UPDATE CP SET Cena='" & cena & "', Kusov='" & kusov & "', Evidoval='" & Form78.uzivatel & "' WHERE Nazov='" & nazov & "' AND Polozka='" & polozka & "' AND pocet=" & 2
                'Chyby.Show("Už je položka pridaná")
            Else
                sql = "INSERT INTO CP (Nazov, Polozka, Kusov, Datum, Cena, pocet, Evidoval) VALUES('" & nazov & "','" & polozka & "','" & kusov & "','" & datum.ToString("yyyy-MM-dd") & "','" & cena & "'," & 2 & ",'" & Form78.uzivatel & "')"
            End If

            Form78.sqa(sql)

            sql = "UPDATE CP SET Cena='" & cena1 & "' WHERE pocet=1 AND Nazov='" & nazov & "'"
            Form78.sqa(sql)

            Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
            Me.CPBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.CP.pocetColumn, 2, RotekDataSet.CP.NazovColumn, nazov)
            napln()


            TextBox4.Text = ""
            TextBox7.Text = ""
            TextBox6.Text = ""
            TextBox5.Text = ""
            TextBox4.Focus()

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TextBox4_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp, TextBox5.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            Polozka()
        End If

    End Sub

    Private Sub TextBox1_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp, TextBox3.KeyUp, TextBox2.KeyUp, DateTimePicker1.KeyUp, ComboBox2.KeyUp, ComboBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            Vseobecne()

        End If
    End Sub

    Private Sub ComboBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.TextUpdate
        Dim vybrate As String = ComboBox1.Text
        Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}%'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, vybrate)
        '            Chyby.Show(vybrate)
        ComboBox1.Text = vybrate
        ComboBox1.SelectionStart = ComboBox1.Text.Length
    End Sub
    Public Sub zmena()
        Me.ZoznamFBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.ZoznamF.pocetColumn, 1, RotekDataSet.ZoznamF.NazovColumn, ComboBox1.Text)
    End Sub

    Private Sub ComboBox2_Enter(sender As System.Object, e As System.EventArgs) Handles ComboBox2.Enter
        zmena()
    End Sub

    Private Sub ComboBox1_Leave(sender As System.Object, e As System.EventArgs) Handles ComboBox1.Leave
        zmena()

    End Sub

    Private Sub TextBox1_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox1.Leave
        Dim cp As String = TextBox1.Text
        If (cp.IndexOf("/" & rok()) = cp.Length - 3) Then
            cp = cp.Replace("/" & rok(), "")
            cp = cp.Replace("CP ", "")
            cp = cp.Replace("CP", "")
            Dim i As Integer
            Try
                i = cp
                TextBox1.Text = "CP " & Format(i, "0000") & "/" & rok()
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Function rok() As Integer
        Return Year(Now).ToString.Substring(2)
    End Function

    Private Sub TextBox6_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                Polozka()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try
                    TextBox7.Text = TextBox5.Text * TextBox6.Text
                Catch ex As Exception
                End Try
            End If

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TextBox7_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyUp
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                Polozka()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try
                    TextBox6.Text = TextBox7.Text / TextBox5.Text
                Catch ex As Exception
                End Try
            End If

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub

    Private Sub TextBox6_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox6.TextChanged
        Try

            If (TextBox6.Text.Chars(TextBox6.Text.Length - 1) = "0" And TextBox6.Text.IndexOf(".") > 0) Then
                Return
            End If

            TextBox6.Text = TextBox6.Text.Replace(",", ".")
            If TextBox6.Text.IndexOf(".") <> TextBox6.Text.Length - 1 Then
                TextBox6.Text = Math.Floor(CDec(TextBox6.Text) * 100) / 100
            End If
            TextBox7.Text = TextBox6.Text * TextBox5.Text
            TextBox6.SelectionStart = TextBox6.Text.Length
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox7_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox7.TextChanged
        Try
            TextBox7.Text = TextBox7.Text.Replace(",", ".")
            If TextBox7.Text.IndexOf(".") <> TextBox7.Text.Length - 1 Then
                TextBox7.Text = Math.Floor(CDec(TextBox7.Text) * 100) / 100
            End If
            TextBox6.Text = TextBox7.Text / TextBox5.Text
            TextBox7.SelectionStart = TextBox7.Text.Length
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex = -1 Or TextBox7.Enabled = False Then
            Exit Sub
        End If
        If e.ColumnIndex = 4 Then
            Dim cena As Double
            Try
                cena = DataGridView1.Rows(e.RowIndex).Cells(1).Value * DataGridView1.Rows(e.RowIndex).Cells(2).Value
                Me.CPBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.CP.pocetColumn, 1, RotekDataSet.CP.NazovColumn, TextBox1.Text)
                cena = DataGridView2.Rows(0).Cells(0).Value - cena
            Catch ex As Exception
                Chyby.Show(ex.ToString)
                Exit Sub
            End Try

            Dim sql As String
            sql = "DELETE FROM CP WHERE pocet=2 AND Nazov='" & TextBox1.Text & "' AND Polozka='" & DataGridView1.Rows(e.RowIndex).Cells(0).Value & "'"
            Form78.sqa(sql)
            sql = "UPDATE CP SET Cena='" & cena & "' WHERE pocet=1 AND Nazov='" & TextBox1.Text & "'"
            Form78.sqa(sql)
            Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
            Me.CPBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' ", RotekDataSet.CP.pocetColumn, 2, RotekDataSet.CP.NazovColumn, cp)
            napln()

        Else
            TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            TextBox6.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            TextBox7.Text = TextBox6.Text * TextBox5.Text
        End If

    End Sub

    Private Sub DateTimePicker2_Enter(sender As System.Object, e As System.EventArgs) Handles DateTimePicker2.Enter
        sender.value = Now
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        Try
            TextBox7.Text = TextBox5.Text * TextBox6.Text
            TextBox5.SelectionStart = TextBox5.Text.Length
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
    '    If e.ColumnIndex = 3 Then
    '        Try
    '            e.Value = DataGridView1.Rows(e.RowIndex).Cells(1).Value * DataGridView1.Rows(e.RowIndex).Cells(2).Value

    '        Catch ex As Exception
    '            e.Value = 0
    '        End Try
    '    End If
    'End Sub
End Class