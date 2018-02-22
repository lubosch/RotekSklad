Imports System.Data.SqlClient

Public Class Odpadky_pridat
    Dim vyska As Integer = 15

    Dim k, j As Integer
    Property nastr As String
    Property tex As String
    Property menko As String
    Dim crc, menoo, priezviskoo As String

    Property druhhh As String

    Property iddd As String

    Property nazovvv As String

    Property rozmerrr As String

    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.OdpadTableAdapter.Fill(Me.RotekDataSet.Odpad)
        Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Odpad.pocetColumn, 0)

        hladaj2(ListBox1, 0, "Druh")
        hladaj2(ListBox3, 2, "Nazov")

        RadioButton1.Checked = True

        j = 1
        TextBox4.Text = ""

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub


    Public Sub stuk()
        Try
            Dim druh, nazov, DL, poznamka As String
            druh = TextBox4.Text
            nazov = TextBox6.Text
            DL = TextBox12.Text
            If String.IsNullOrEmpty(druh) Or String.IsNullOrEmpty(nazov) Then
                Chyby.Show("Niečo nezadané")
                Exit Sub
            End If
            poznamka = TextBox16.Text
            If poznamka.Length = 0 Then
                poznamka = "-"
            End If

            Dim CenaKg, Cena, Kg As Double
            Try
                CenaKg = TextBox3.Text
                Cena = TextBox11.Text
                Kg = TextBox8.Text
            Catch ex As Exception
                Chyby.Show("Nejaké čísla zle zadané")
            End Try

            Dim slovom As String

            If RadioButton1.Checked = True Then
                slovom = Kg & " Kg"
            Else
                slovom = Kg & " litrov"
            End If

            Dim cas As DateTime = DateTimePicker1.Value.ToShortDateString

            Dim con As New SqlConnection
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            Dim cmd As New SqlCommand


            Dim sql As String

            sql = "INSERT INTO Odpad (Druh, Nazov, pocet,  CenaKg, Cena, Kg, DL, Datum, Poznamka, Slovom) VALUES ('" & druh & "','" & nazov & "'," & 1 & ",'" & CenaKg & "','" & Cena & "','" & Kg & "','" & DL & "','" & cas & "','" & poznamka & "','" & slovom & "')"
            cmd = New SqlCommand(sql, con)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Chyby.Show(sql + vbNewLine + ex.ToString)
            End Try

            Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Odpad.pocetColumn, 0, RotekDataSet.Odpad.DruhColumn, druh, RotekDataSet.Odpad.NazovColumn, nazov)
            If DataGridView2.RowCount = 0 Then
                sql = "INSERT INTO Odpad (Druh, Nazov, pocet,  CenaKg, Cena, Kg, Slovom) VALUES ('" & druh & "','" & nazov & "'," & 0 & ",'" & CenaKg & "','" & Cena & "','" & Kg & "','" & slovom & "')"
                cmd = New SqlCommand(sql, con)
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Chyby.Show(sql + vbNewLine + ex.ToString)
                End Try

            Else
                If CenaKg <> DataGridView2.Rows(0).Cells(4).Value Then
                    sql = "UPDATE Odpad SET CenaKg='" & CenaKg & "' WHERE Druh='" & druh & "' AND Nazov='" & nazov & "' AND pocet=0"
                    cmd = New SqlCommand(sql, con)
                    Try
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Chyby.Show(sql + vbNewLine + ex.ToString)
                    End Try
                End If

                Cena = Cena + DataGridView2.Rows(0).Cells(6).Value
                Kg = Kg + DataGridView2.Rows(0).Cells(5).Value

                If RadioButton1.Checked = True Then
                    slovom = Kg & " Kg"
                Else
                    slovom = Kg & " litrov"
                End If

                sql = "UPDATE Odpad SET Cena='" & Cena & "', Kg='" & Kg & "', Slovom='" & slovom & "' WHERE Druh='" & druh & "' AND Nazov='" & nazov & "' AND pocet=0"
                cmd = New SqlCommand(sql, con)
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Chyby.Show(sql + vbNewLine + ex.ToString)
                End Try
            End If

            con.Close()

            Me.OdpadTableAdapter.Fill(Me.RotekDataSet.Odpad)
            Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Odpad.pocetColumn, 0)

            TextBox4.Text = ""
            TextBox6.Text = ""
            TextBox3.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = "1"
            TextBox10.Text = ""
            TextBox11.Text = ""
            TextBox13.Text = ""
            TextBox12.Text = ""
            TextBox16.Text = ""
            TextBox4.Focus()

        Catch ex As Exception
            Chyby.Show(ex.ToString)

        End Try

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        stuk()
    End Sub

    Private Sub TextBox3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then stuk()
    End Sub
    Private Sub nastrsklad_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox1, e)
            'If k < 0 Then
            '    k = ListBox1.Items.Count - 1

            'ElseIf k >= ListBox1.Items.Count Then
            '    k = 0
            'End If

            'If e.KeyCode = 40 Then
            '    j = 1
            '    TextBox4.Text = (ListBox1.Items(k).ToString)
            '    TextBox4.Select(0, TextBox4.Text.Length)
            '    k = k + 1

            If e.KeyCode = Keys.Escape Then
                Me.Close()
                'ElseIf e.KeyCode = Keys.Up Then

                '    j = 1
                '    TextBox4.Text = (ListBox1.Items(k).ToString)
                '    TextBox4.Select(0, TextBox4.Text.Length)
                '    k = k - 1

            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then

                hladaj2(ListBox1, 0, "Druh")
                'Dim a As Integer = tex.Length
                'TextBox4.Select(a, TextBox4.Text.Length - a)
            End If

        Catch ex As Exception


        End Try

    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try

            Dim tex As String
            j = 1
            tex = ListBox1.Text
            TextBox4.Text = tex
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)
            'TextBox4.SelectionStart = 0

        Catch ex As Exception

        End Try
    End Sub


    Private Sub TextBox6_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox3, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj2(ListBox3, 2, "Nazov")

                'TextBox6.Select(TextBox6.Text.Length, TextBox6.Text.Length)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        Try

            j = 1
            TextBox6.Text = ListBox3.Text
            TextBox6.Focus()
            TextBox6.Select(0, TextBox6.Text.Length)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Enter
        'Me.Text = ""
        'hladaj2(ListBox1, 0, "Druh")
    End Sub

    'Private Sub TextBox6_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.Enter
    '    Dim pom, pom2 As String
    '    pom = TextBox4.Text
    '    pom2 = TextBox5.Text

    '    Me.HutaBindingSource.Filter = Nothing
    '    Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
    '    Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Huta.DruhColumn, pom, RotekDataSet.Huta.IdentColumn, pom2, RotekDataSet.Huta.pocetColumn, 0)
    '    Dim x As Integer = DataGridView2.RowCount
    '    If x = 1 Then
    '        menoo = DataGridView2.Rows(0).Cells(2).Value
    '        TextBox6.Text = menoo
    '    End If
    '    Me.HutaBindingSource.Filter = Nothing
    '    Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
    '    Me.HutaBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Huta.pocetColumn, 0)

    'End Sub

    'Private Sub TextBox6_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.Leave
    '    Dim pom As String
    '    pom = TextBox6.Text

    '    Me.HutaBindingSource.Filter = Nothing
    '    Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
    '    Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.NazovColumn, pom, RotekDataSet.Huta.pocetColumn, 0)
    '    Dim x As Integer = DataGridView2.RowCount
    '    If x = 1 Then
    '        menoo = DataGridView2.Rows(0).Cells(0).Value
    '        TextBox4.Text = menoo
    '        menoo = DataGridView2.Rows(0).Cells(1).Value
    '        TextBox5.Text = menoo
    '    End If
    '    Me.HutaBindingSource.Filter = Nothing
    '    Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
    '    Me.HutaBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Huta.pocetColumn, 0)

    'End Sub

    Private Sub hladaj2(ByVal listb As System.Windows.Forms.ListBox, ByVal stlp As Integer, ByVal triedenie As String)

        Dim b As Integer
        Dim a, aaa, opak As String
        Try
            a = TextBox4.Text
            aaa = TextBox6.Text
            opak = ""
            If Me.OdpadBindingSource.Sort <> triedenie Then
                Me.OdpadBindingSource.Sort = triedenie
            End If

            Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%'", RotekDataSet.Odpad.pocetColumn, 0, RotekDataSet.Odpad.DruhColumn, a, RotekDataSet.Odpad.NazovColumn, aaa)

            listb.Items.Clear()
            For b = 0 To DataGridView2.RowCount - 1
                If opak <> DataGridView2.Rows(b).Cells(stlp).Value.ToString.ToUpper Then
                    listb.Items.Add(DataGridView2.Rows(b).Cells(stlp).Value)
                    opak = DataGridView2.Rows(b).Cells(stlp).Value.ToString.ToUpper
                End If

            Next

            If listb.Items.Count = 1 Then
                Dim listbs As List(Of ListBox) = New List(Of ListBox)
                Dim aree As List(Of String) = New List(Of String)
                Dim stlpc() As Integer = {0, 2}

                listbs.Add(ListBox1)
                listbs.Add(ListBox3)

                aree.Add("Druh")
                aree.Add("Nazov")
                Hpridat.hladaj3(listbs, aree, stlpc, DataGridView2, OdpadBindingSource)

            End If


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TextBox3_KeyUp_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then

                Try
                    TextBox11.Text = TextBox3.Text * TextBox8.Text
                    TextBox13.Text = TextBox3.Text * TextBox8.Text / TextBox9.Text
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox6_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.Enter
        hladaj2(ListBox3, 2, "Nazov")
    End Sub


    Private Sub TextBox8_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyUp
        Try

            If e.KeyCode = Keys.Escape Then
                Me.Close()

            ElseIf e.KeyCode = Keys.Enter Then
                stuk()

            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                TextBox10.Text = TextBox8.Text / TextBox9.Text
                Try
                    TextBox11.Text = TextBox3.Text * TextBox8.Text
                    TextBox13.Text = TextBox3.Text * TextBox8.Text / TextBox9.Text
                Catch ex As Exception
                    TextBox11.Text = ""
                End Try

                TextBox11.Text = TextBox8.Text * TextBox3.Text
            End If

        Catch ex As Exception
        End Try
    End Sub


    Private Sub TextBox11_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyUp

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try

                    TextBox3.Text = TextBox11.Text / TextBox8.Text
                    TextBox13.Text = TextBox11.Text / TextBox9.Text
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox10_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyUp

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try
                    TextBox8.Text = TextBox10.Text * TextBox9.Text
                    Try
                        TextBox11.Text = TextBox3.Text * TextBox8.Text
                        TextBox13.Text = TextBox3.Text * TextBox8.Text / TextBox9.Text
                    Catch ex As Exception
                        TextBox11.Text = ""
                    End Try

                Catch ex As Exception

                End Try

            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox9_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyUp

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then


            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox9_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox9.Leave
        Dim i As Integer
        Try
            i = TextBox9.Text
        Catch ex As Exception
            TextBox9.Text = 1
        End Try
    End Sub

    Private Sub TextBox13_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox13.KeyUp
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try
                    TextBox11.Text = TextBox13.Text * TextBox9.Text
                    TextBox3.Text = TextBox13.Text * TextBox9.Text / TextBox8.Text
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox9_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox9.TextChanged
        Try
            TextBox8.Text = TextBox9.Text * TextBox10.Text
            'prepocet1()

            Try
                TextBox11.Text = TextBox3.Text * TextBox8.Text
                TextBox13.Text = TextBox3.Text * TextBox8.Text / TextBox9.Text
            Catch ex As Exception
                TextBox11.Text = ""
            End Try

        Catch ex As Exception
            TextBox8.Text = ""
        End Try

    End Sub

    Private Sub TextBox10_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox10.TextChanged
        Try
            TextBox10.Text = TextBox10.Text.Replace(".", ",")

            If TextBox10.Text.IndexOf(",") <> TextBox10.Text.Length - 1 Then
                TextBox10.Text = Math.Floor(CDec(TextBox10.Text) * 1000) / 1000
            End If
            TextBox10.SelectionStart = TextBox10.Text.Length

        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox8_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox8.TextChanged
        Try
            TextBox8.Text = TextBox8.Text.Replace(".", ",")
            If TextBox8.Text.IndexOf(",") <> TextBox8.Text.Length - 1 Then
                TextBox8.Text = Math.Floor(CDec(TextBox8.Text) * 1000) / 1000
            End If
            TextBox8.SelectionStart = TextBox8.Text.Length
        Catch ex As Exception
        End Try

    End Sub


    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged
        Try
            TextBox3.Text = TextBox3.Text.Replace(".", ",")
            If TextBox3.Text.IndexOf(",") <> TextBox3.Text.Length - 1 Then
                TextBox3.Text = Math.Floor(CDec(TextBox3.Text) * 1000) / 1000
            End If
            TextBox3.SelectionStart = TextBox3.Text.Length

        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox13_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox13.TextChanged
        Try
            TextBox13.Text = TextBox13.Text.Replace(".", ",")
            If TextBox13.Text.IndexOf(",") <> TextBox13.Text.Length - 1 Then
                TextBox13.Text = Math.Floor(CDec(TextBox13.Text) * 1000) / 1000
            End If
            TextBox13.SelectionStart = TextBox13.Text.Length
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox11_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox11.TextChanged
        Try
            TextBox11.Text = TextBox11.Text.Replace(".", ",")

            If TextBox11.Text.IndexOf(",") <> TextBox11.Text.Length - 1 Then
                TextBox11.Text = Math.Floor(CDec(TextBox11.Text) * 1000) / 1000
            End If
            TextBox11.SelectionStart = TextBox11.Text.Length
        Catch ex As Exception
        End Try

    End Sub

    'Private Sub TextBox1_Enter(sender As System.Object, e As System.EventArgs)

    '    Dim druh, nazov As String
    '    druh = TextBox4.Text
    '    nazov = TextBox6.Text

    '    If druh.Length = 0 Or nazov.Length = 0 Then
    '        Exit Sub
    '    End If

    '    Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' ", RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.pocetColumn, 0)
    '    If DataGridView2.RowCount = 1 Then
    '        TextBox2.Text = DataGridView2.Rows(0).Cells(3).Value
    '        TextBox3.Text = DataGridView2.Rows(0).Cells(4).Value
    '        TextBox11.Text = TextBox3.Text * TextBox8.Text
    '        TextBox13.Text = TextBox3.Text * TextBox8.Text / TextBox9.Text

    '    End If
    'End Sub



    Private Sub TextBox6_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox6.Leave
        Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.Odpad.pocetColumn, 0, RotekDataSet.Odpad.NazovColumn, TextBox6.Text, RotekDataSet.Odpad.DruhColumn, TextBox4.Text)
        If DataGridView2.RowCount <> 0 Then
            TextBox3.Text = DataGridView2.Rows(0).Cells(4).Value
        End If

        '    Me.OdpadBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Odpad.pocetColumn, 0, RotekDataSet.Odpad.NazovColumn, TextBox6.Text, RotekDataSet.Odpad.DruhColumn, TextBox4.Text, RotekDataSet.Odpad.DatumColumn, DateTimePicker1.Value.ToShortDateString)

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            Label12.Text = "Váha na 1 kus"
            Label8.Text = "Celková váha"
            Label7.Text = "Cena za kilogram"
            Label13.Text = "Kg"
            Label14.Text = "Kg"
        Else
            Label12.Text = "Objem na 1 kus"
            Label8.Text = "Celkový objem"
            Label7.Text = "Cena za liter"
            Label13.Text = "Litrov"
            Label14.Text = "Litrov"

        End If

    End Sub
End Class