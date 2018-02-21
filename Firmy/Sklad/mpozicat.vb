Imports System.Data.SqlClient

Public Class mpozicat
    Dim k, j As Integer
    Property nastr As String
    Property tex As String
    Property menko As String
    Dim crc As String



    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)

        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        sklad.v = 47
        DataGridView1.Hide()
        DataGridView2.Hide()



        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        TextBox1.Text = "Tu zadaj počet"

        hladaj(0, ListBox1, TextBox4)
        hladaj(1, ListBox2, TextBox5)
        hladaj(2, ListBox4, TextBox2)
        meno()
    End Sub


    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = "1"
        TextBox1.SelectAll()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        sklad.v = 1
        Me.Close()
    End Sub
    Public Sub stuk()
        meno()
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.pocetColumn, 0, RotekDataSet.Rotek.MenprColumn, crc)
        Dim vlast, men, nastroj, nastroj2, priezvisk As String
        men = DataGridView2.Rows(0).Cells(1).Value
        priezvisk = DataGridView2.Rows(0).Cells(2).Value
        nastroj = TextBox4.Text
        nastroj2 = TextBox5.Text
        vlast = TextBox2.Text
        If vlast.Length = 0 Then
            Chyby.Show("Nezadal si vlastnosť")
            Exit Sub
        End If
        If nastroj.Length = 0 Then
            Chyby.Show("Nezadal si nástroj")
            Exit Sub
        End If
        If nastroj2.Length = 0 Then
            Chyby.Show("Nezadal si priemer")
            Exit Sub
        End If
        If TextBox6.Text.Length = 0 Then
            Chyby.Show("Osoba nenájdená")
            Exit Sub
        End If
        If (priezvisk & " " & men) <> TextBox6.Text Then
            Chyby.Show("Osoba nenájdená")
            Exit Sub
        End If

        Dim pocet As Integer
        Try
            Try
                pocet = TextBox1.Text
            Catch ex As Exception
                Chyby.Show("Zle zadaný počet")
                Exit Sub
            End Try
            Dim con As New SqlConnection
            Dim sql As String
            con.ConnectionString = My.Settings.Rotek2
            con.Open()

            Me.RotekBindingSource.Filter = Nothing
            Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}='{7}' AND {8} = '{9}' AND {10}='{11}' AND {12} = '{13}'", RotekDataSet.Rotek.pocetColumn, 2, RotekDataSet.Rotek.MenprColumn, menko, RotekDataSet.Rotek.NástrojColumn, nastroj, RotekDataSet.Rotek.VelkostRColumn, nastroj2, RotekDataSet.Rotek.MenoColumn, men, RotekDataSet.Rotek.PriezviskoColumn, priezvisk, RotekDataSet.Rotek.VlastnostColumn, vlast)
            Dim cmd As New SqlCommand
            If DataGridView2.RowCount = 0 Then
                sql = "Insert INTO Rotek (Meno, Priezvisko, Nástroj, pocet, Menpr, Kolko, VelkostR, Vlastnost) VALUES ('" + men + "', '" + priezvisk + "', '" & nastroj & "', '" & 2 & "', '" + menko + "', '" & pocet & "', '" & nastroj2 & "','" + vlast + "')"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            Else
                Dim pocett As Integer = DataGridView2.Rows(0).Cells(3).Value
                pocet = pocet + pocett
                sql = "UPDATE Rotek SET Kolko='" & pocet & "' WHERE Nástroj='" + nastroj + "' AND VelkostR='" & nastroj2 & "'  AND Menpr='" + menko + "' AND Meno='" + men + "' AND Priezvisko='" + priezvisk + "' AND Vlastnost='" + vlast + "' AND pocet=2"

                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            End If
            con.Close()
            Me.Close()
        Catch ex As SystemException
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        stuk()
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then stuk()
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

            If e.KeyCode = Keys.Escape Then
                Me.Close()


            End If
            If ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(0, ListBox1, TextBox4)
            End If

        Catch ex As Exception


        End Try

    End Sub

    Public Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByRef textar As TextBox)
        Try

            pom.Items.Clear()
            If stlpec = 0 Then
                tex = TextBox4.Text
                Me.SkladBindingSource.Sort = "Nastroj"
                Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%'", RotekDataSet.Sklad.NastrojColumn, TextBox4.Text)
            ElseIf stlpec = 1 Then
                Me.SkladBindingSource.Sort = "VelkostS"
                Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}%'", RotekDataSet.Sklad.NastrojColumn, TextBox4.Text, RotekDataSet.Sklad.VelkostSColumn, TextBox5.Text)
            ElseIf stlpec = 2 Then
                Me.SkladBindingSource.Sort = "Vlastnost"
                Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%'", RotekDataSet.Sklad.VlastnostColumn, TextBox6.Text)
            End If

            Dim slovo As String = ""
            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Cells(stlpec).Value <> slovo Then
                    pom.Items.Add(DataGridView1.Rows(i).Cells(stlpec).Value)
                    slovo = DataGridView1.Rows(i).Cells(stlpec).Value
                End If
            Next


            If pom.Items.Count = 1 Then
                Dim listbs As List(Of ListBox) = New List(Of ListBox)
                Dim aree As List(Of String) = New List(Of String)
                Dim stlpc() As Integer = {0, 1}

                listbs.Add(ListBox1)
                listbs.Add(ListBox2)

                aree.Add("Nastroj")
                aree.Add("VelkostS")

                Hpridat.hladaj3(listbs, aree, stlpc, DataGridView1, SkladBindingSource)
            End If
        Catch ex As Exception
            '  Chyby.Show(ex.Message)
        End Try

        '  Chyby.Show(tex & " " & tex.Length & " " & textar.Text)
    End Sub


    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            Dim tex As String
            j = 1
            tex = ListBox1.Text
            TextBox4.Text = tex
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)
            'TextBox4.SelectionStart = TextBox4.Text.Length

        Catch ex As Exception

        End Try
    End Sub


    Private Sub TextBox5_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox2, e)

            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
            If ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(1, ListBox2, TextBox5)
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        Try
            j = 1
            TextBox5.Text = ListBox2.Text
            TextBox5.Focus()
            TextBox5.Select(0, TextBox5.Text.Length)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
    Sub meno()
        Dim slovo As String = TextBox6.Text
        Dim men, priezvisk As String

        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.pocetColumn, 0)

        ListBox3.Items.Clear()
        Dim x As Integer = DataGridView2.RowCount - 1

        For i As Integer = 0 To x
            Dim pom As String = DataGridView2.Rows(i).Cells(0).Value
            priezvisk = DataGridView2.Rows(i).Cells(2).Value

            men = DataGridView2.Rows(i).Cells(1).Value

            Dim pom2 As String = UCase(priezvisk & " " & men)

            If pom2.IndexOf(UCase(slovo)) = 0 Then
                crc = pom
                ListBox3.Items.Add(priezvisk & " " & men)
            End If
        Next
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox4, e)

            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(2, ListBox4, TextBox2)
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

    Private Sub TextBox6_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox3, e)

            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                meno()
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub ListBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox4.SelectedIndexChanged
        Try

            j = 1
            TextBox2.Text = ListBox4.Text
            TextBox2.Focus()
            TextBox2.Select(0, TextBox2.Text.Length)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox5_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox5.Enter
        hladaj(1, ListBox2, TextBox5)

    End Sub

    Private Sub TextBox2_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox2.Enter
        hladaj(2, ListBox4, TextBox2)

    End Sub

    Private Sub TextBox4_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox4.Leave
        'hladaj(1, ListBox2, TextBox5)

    End Sub

    Private Sub TextBox5_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox5.Leave
        'hladaj(0, ListBox1, TextBox4)
        '  hladaj(2, ListBox4, TextBox2)
        '   meno()

    End Sub

    Private Sub TextBox4_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox4.Enter
        'hladaj(0, ListBox1, TextBox4)
    End Sub
End Class



