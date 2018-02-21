Public Class Materialy
    Private k As Integer

    Private Sub Materialy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaterialNazovTableAdapter.Fill(Me.RotekDataSet.MaterialNazov)
        Me.MaterialDruhTableAdapter.Fill(Me.RotekDataSet.MaterialDruh)
        k = 0
        hladaj()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        rovnake()
    End Sub

    Private Sub TextBox4_Click(sender As Object, e As EventArgs) Handles TextBox4.Click
        TextBox4.Text = ""
        TextBox2.Text = ""
        hladaj()
    End Sub


    Private Sub TextBox2_Click(sender As Object, e As EventArgs)
        TextBox2.Text = ""
        TextBox4.Text = ""
        hladaj()
    End Sub

    Private Sub hladaj()
        hladajDruh()
        hladajNazov()
        hladaj2Nazov()
    End Sub

    Private Sub hladajDruh()
        MaterialDruhBindingSource.Filter = "Druh_ID IS NULL AND Nazov LIKE '%" & TextBox4.Text & "%'"
    End Sub
    Private Sub hladajNazov()
        MaterialNazovBindingSource.Filter = "Nazov LIKE '%" & TextBox6.Text & "%' AND Druh LIKE '%" & TextBox4.Text & "%'"
    End Sub

    Private Sub hladaj2Nazov()
        MaterialNazovBindingSource1.Filter = "Nazov LIKE '%" & TextBox1.Text & "%' AND Druh LIKE '%" & TextBox4.Text & "%'"
    End Sub

    Private Sub TextBox4_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyUp
        Try
            Hpridat.stlacBound(k, TextBox4, ListBox1, e)
            Hpridat.stlacBound(k, TextBox2, ListBox1, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox2_KeyUp(sender As Object, e As KeyEventArgs)
        Try
            Hpridat.stlacBound(k, TextBox4, ListBox1, e)
            Hpridat.stlacBound(k, TextBox2, ListBox1, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox6_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox6.KeyUp
        Try
            Hpridat.stlacBound(k, sender, ListBox3, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        Try
            Hpridat.stlacBound(k, sender, ListBox4, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseClick
        If ListBox1.SelectedItems.Count > 0 Then
            TextBox4.Text = ListBox1.SelectedValue
            TextBox2.Text = ListBox1.SelectedValue
            hladajNazov()
            TextBox4.Focus()
            TextBox4.Select(TextBox4.Text.Length, 0)
        End If

    End Sub

    Private Sub ListBox2_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox2.MouseClick
        If ListBox2.SelectedItems.Count > 0 Then
            TextBox4.Text = ListBox2.SelectedValue
            TextBox2.Text = ListBox2.SelectedValue
            hladaj2Nazov()
            TextBox2.Focus()
            TextBox2.Select(TextBox2.Text.Length, 0)
        End If

    End Sub

    Private Sub ListBox3_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox3.MouseClick
        If ListBox3.SelectedItems.Count > 0 Then
            TextBox6.Text = ListBox3.SelectedValue
            TextBox6.Focus()
            TextBox6.Select(TextBox6.Text.Length, 0)
        End If

    End Sub

    Private Sub ListBox4_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox4.MouseClick
        If ListBox4.SelectedItems.Count > 0 Then
            TextBox1.Text = ListBox4.SelectedValue
            TextBox1.Focus()
            TextBox1.Select(TextBox1.Text.Length, 0)
        End If
    End Sub

    Private Sub TextBox6_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox6.MouseClick
        hladaj()
    End Sub

    Private Sub TextBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseClick
        hladaj()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        zmazatDruh()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        zmazatNazov()
    End Sub

    Private Sub zmazatDruh()
        Dim druh As String
        Dim druh_id As Integer
        druh = TextBox3.Text
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT TOP 1 ID FROM MaterialDruh WHERE Nazov = '" & druh & "'", dd)
        If dd.Rows.Count <> 1 Then
            MessageBox.Show("Nenasiel sa taky druh")
            Exit Sub
        End If
        druh_id = dd.Rows(0).Item("ID")


        If MessageBox.Show("Naozajchcete zmazať Druh= """ & druh & """ so všetkými náveznosťami na výdajky a príjemky navždy???", "Nerob to!", MessageBoxButtons.YesNo) = vbYes Then

            SQL_main.AddCommand("DELETE FROM Material_Prijemka WHERE Material_ID IN ")
            SQL_main.AddCommand("~(SELECT m.ID FROM MaterialVseobecne mv JOIN Material m ON m.Material_ID = mv.ID JOIN MaterialNazov mn ON mn.Vseobecne_ID = mv.ID WHERE mn.Druh_ID = " & druh_id & " ) ")

            SQL_main.AddCommand("DELETE FROM Material_Vydajka WHERE Material_ID IN ")
            SQL_main.AddCommand("~(SELECT m.ID FROM MaterialVseobecne mv JOIN Material m ON m.Material_ID = mv.ID JOIN MaterialNazov mn ON mn.Vseobecne_ID = mv.ID WHERE mn.Druh_ID = " & druh_id & " ) ")

            SQL_main.AddCommand("DELETE FROM Odpady WHERE Material_ID IN ")
            SQL_main.AddCommand("~(SELECT m.ID FROM MaterialVseobecne mv JOIN Material m ON m.Material_ID = mv.ID JOIN MaterialNazov mn ON mn.Vseobecne_ID = mv.ID WHERE mn.Druh_ID = " & druh_id & " ) ")

            SQL_main.AddCommand("DELETE FROM MaterialCena WHERE Material_ID IN ")
            SQL_main.AddCommand("~(SELECT m.ID FROM MaterialVseobecne mv JOIN Material m ON m.Material_ID = mv.ID JOIN MaterialNazov mn ON mn.Vseobecne_ID = mv.ID WHERE mn.Druh_ID = " & druh_id & " ) ")

            SQL_main.AddCommand("DELETE FROM Material WHERE Material_ID IN ")
            SQL_main.AddCommand("~(SELECT mv.ID FROM MaterialVseobecne mv JOIN MaterialNazov mn ON mn.Vseobecne_ID = mv.ID WHERE mn.Druh_ID = " & druh_id & " ) ")

            SQL_main.AddCommand("DELETE FROM MaterialNazov WHERE Druh_ID = " & druh_id & " ")

            SQL_main.AddCommand("DELETE FROM Material WHERE Material_ID IN ")
            SQL_main.AddCommand("~(SELECT mv.ID FROM MaterialVseobecne mv WHERE ID NOT IN ( SELECT Vseobecne_ID FROM MaterialNazov ))")

            SQL_main.AddCommand("DELETE FROM MaterialVseobecne WHERE ID NOT IN ( SELECT Vseobecne_ID FROM MaterialNazov )")

            SQL_main.AddCommand("UPDATE MaterialDruh SET Druh_ID = NULL WHERE Druh_ID = " & druh_id & " ")

            SQL_main.AddCommand("DELETE FROM MaterialDruh WHERE ID = " & druh_id & " ")

            SQL_main.Commit_Transaction()
        End If
        clear_all()

    End Sub
    Private Sub zmazatNazov()
        Dim druh, nazov As String
        druh = TextBox4.Text
        nazov = TextBox6.Text
        Dim nazov_id As Integer
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT mn.ID ID, mn.Vseobecne_ID Vseobecne_ID FROM MaterialNazov mn JOIN MaterialVseobecne mv ON mv.ID = mn.Vseobecne_ID JOIN MaterialDruh md ON md.ID = mn.Druh_ID JOIN MaterialNazov mnn ON mnn.Vseobecne_ID = mv.ID WHERE mn.Nazov = '" & nazov & "' AND md.Nazov = '" & druh & "'", dd)
        If dd.Rows.Count = 1 Then
            If MessageBox.Show("Naozajchcete zmazať material Druh= """ & druh & """ Nazov = """ & nazov & """ so všetkými náveznosťami na výdajky a príjemky navždy???", "Nerob to!", MessageBoxButtons.YesNo) = vbYes Then
                Dim vseobecne_id As Integer
                nazov_id = dd.Rows(0).Item("ID")
                vseobecne_id = dd.Rows(0).Item("Vseobecne_ID")

                SQL_main.AddCommand("DELETE FROM Material_Prijemka WHERE Material_ID IN ")
                SQL_main.AddCommand("~(SELECT m.ID FROM MaterialVseobecne mv JOIN Material m ON m.Material_ID = mv.ID WHERE mv.ID = " & vseobecne_id & " ) ")

                SQL_main.AddCommand("DELETE FROM Material_Vydajka WHERE Material_ID IN ")
                SQL_main.AddCommand("~(SELECT m.ID FROM MaterialVseobecne mv JOIN Material m ON m.Material_ID = mv.ID WHERE mv.ID = " & vseobecne_id & " ) ")

                SQL_main.AddCommand("DELETE FROM Odpady WHERE Material_ID IN ")
                SQL_main.AddCommand("~(SELECT m.ID FROM MaterialVseobecne mv JOIN Material m ON m.Material_ID = mv.ID WHERE mv.ID = " & vseobecne_id & " ) ")

                SQL_main.AddCommand("DELETE FROM MaterialCena WHERE Material_ID IN ")
                SQL_main.AddCommand("~(SELECT m.ID FROM MaterialVseobecne mv JOIN Material m ON m.Material_ID = mv.ID WHERE mv.ID = " & vseobecne_id & " ) ")

                SQL_main.AddCommand("DELETE FROM Material WHERE Material_ID IN ")
                SQL_main.AddCommand("~(SELECT mv.ID FROM MaterialVseobecne mv WHERE mv.ID = " & vseobecne_id & " ) ")

                SQL_main.AddCommand("DELETE FROM MaterialNazov WHERE ID = " & nazov_id & " ")
                SQL_main.AddCommand("DELETE FROM MaterialVseobecne WHERE ID = " & vseobecne_id)

                SQL_main.Commit_Transaction()

            End If
        ElseIf dd.Rows.Count = 0 Then
            MessageBox.Show("Nenasiel sa taky material")
            Exit Sub
        Else
            If MessageBox.Show("Naozajchcete zmazať material Druh= """ & druh & """ Nazov = """ & nazov & """ ? Náveznosti ostanú, keďže existuje alternatíva k tomuto názvu", "Nerob to!", MessageBoxButtons.YesNo) = vbYes Then
                SQL_main.AddCommand("DELETE FROM MaterialNazov WHERE ID IN (SELECT mn.ID FROM MaterialNazov mn JOIN MaterialDruh md ON md.ID = mn.Druh_ID WHERE md.Nazov = '" & druh & "' AND mn.Nazov = '" & nazov & "')")
                SQL_main.Commit_Transaction()
            End If
        End If
        clear_all()
    End Sub

    Private Sub rovnake()
        Dim druh, nazov1, nazov2 As String
        druh = TextBox4.Text
        nazov1 = TextBox6.Text
        nazov2 = TextBox1.Text

        Dim vseobecne1, vseobecne2, nazov_id1, nazov_id2 As Integer
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT TOP 1 mn.ID ID , mn.Vseobecne_ID Vseobecne_ID FROM MaterialNazov mn JOIN MaterialDruh md ON md.ID = mn.Druh_ID WHERE md.Nazov = '" & druh & "' AND mn.Nazov = '" & nazov1 & "'", dd)
        If dd.Rows.Count = 0 Then
            MessageBox.Show("Nenasiel sa material nalavo")
            Exit Sub
        End If
        vseobecne1 = dd.Rows(0).Item("Vseobecne_ID")
        nazov_id1 = dd.Rows(0).Item("ID")

        dd = New DataTable
        SQL_main.List("SELECT TOP 1 mn.ID ID , mn.Vseobecne_ID Vseobecne_ID FROM MaterialNazov mn JOIN MaterialDruh md ON md.ID = mn.Druh_ID WHERE md.Nazov = '" & druh & "' AND mn.Nazov = '" & nazov2 & "'", dd)
        If dd.Rows.Count = 0 Then
            MessageBox.Show("Nenasiel sa material napravo")
            Exit Sub
        End If
        vseobecne2 = dd.Rows(0).Item("Vseobecne_ID")
        nazov_id2 = dd.Rows(0).Item("ID")

        If MessageBox.Show("Naozaj je """ & druh & " " & nazov1 & """ to isté ako """ & druh & " " & nazov2 & """ ? Táto zmena je nezvratná. ", "Ekvivalencia", MessageBoxButtons.YesNo) = vbYes Then
            If vseobecne1 = vseobecne2 Then
                SQL_main.AddCommand("UPDATE MaterialVseobecne SET Nazov_ID = " & nazov_id1 & " WHERE ID = " & vseobecne2)
                SQL_main.Commit_Transaction()
            Else
                SQL_main.AddCommand("UPDATE MaterialNazov SET Vseobecne_ID = " & vseobecne1 & " WHERE Vseobecne_ID = " & vseobecne2)
                SQL_main.AddCommand("UPDATE Material SET Material_ID = " & vseobecne1 & " WHERE Material_ID = " & vseobecne2)
                SQL_main.AddCommand("DELETE FROM MaterialVseobecne WHERE ID = " & vseobecne2)
                SQL_main.Commit_Transaction()
            End If

        End If
        clear_all()

    End Sub

    Private Sub clear_all()
        TextBox4.Text = ""
        TextBox2.Text = ""
        TextBox1.Text = ""
        TextBox6.Text = ""
        hladaj()
        Me.MaterialNazovTableAdapter.Fill(Me.RotekDataSet.MaterialNazov)
        Me.MaterialDruhTableAdapter.Fill(Me.RotekDataSet.MaterialDruh)
    End Sub



    Private Sub TextBox3_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyUp
        Try
            Hpridat.stlacBound(k, TextBox3, ListBox5, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                MaterialDruhBindingSource.Filter = "Nazov LIKE '%" & TextBox3.Text & "%'"
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox5_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyUp
        Try
            Hpridat.stlacBound(k, TextBox5, ListBox6, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                MaterialDruhBindingSource1.Filter = "Nazov LIKE '%" & TextBox5.Text & "%'"
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ListBox6_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox6.MouseClick
        If ListBox6.SelectedItems.Count > 0 Then
            TextBox5.Text = ListBox6.SelectedValue
            TextBox5.Focus()
            TextBox5.Select(TextBox5.Text.Length, 0)
        End If
    End Sub

    Private Sub ListBox5_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox5.MouseClick
        If ListBox5.SelectedItems.Count > 0 Then
            TextBox3.Text = ListBox5.SelectedValue
            TextBox3.Focus()
            TextBox3.Select(TextBox3.Text.Length, 0)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim druh1, druh2 As MaterialDruh_SQL
        druh1 = New MaterialDruh_SQL(TextBox3.Text)
        druh2 = New MaterialDruh_SQL(TextBox5.Text)
        If MessageBox.Show("Naozaj je """ & druh1.nazov & """ to isté ako """ & druh2.nazov & """ ? Táto zmena je nezvratná. ", "Ekvivalencia", MessageBoxButtons.YesNo) = vbYes Then
            druh2.combineInto(druh1)
            clear_all()
        End If
    End Sub
End Class