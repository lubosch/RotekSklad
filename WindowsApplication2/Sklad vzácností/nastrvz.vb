Imports System
Imports System.IO
Imports System.Data.SqlClient

Public Class nastrvz
    Dim tex, tex2, texx As String
    Dim j, k, l As Integer


    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        j = 0
        hladaj(0, ListBox1, TextBox4)
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        sklad.v = 47
        DataGridView1.Hide()

        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        j = k = l = 0
        tex = tex2 = 0
    End Sub

    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = "1"
        TextBox1.SelectAll()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim openFileDialog1 As New OpenFileDialog

        openFileDialog1.InitialDirectory = System.Environment.CurrentDirectory
        openFileDialog1.Title = "Open Text File"
        openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True
        Dim cesta As String = ""

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            cesta = openFileDialog1.FileName
        End If
        TextBox2.Text = cesta
        TextBox2.Select(TextBox2.Text.Length, TextBox2.Text.Length)
    End Sub
    Private Sub posun(ByRef textar As TextBox, ByVal list As ListBox, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal asa As Integer)

        Try
            If k < 0 Then
                k = list.Items.Count - 1

            ElseIf k >= list.Items.Count Then
                k = 0
            End If

            If e.KeyCode = 40 Then
                j = 1
                textar.Text = (list.Items(k).ToString)
                k = k + 1
                textar.Select(0, textar.Text.Length)
                textar.SelectionStart = textar.Text.Length
                j = 0
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Up Then
                j = 1
                textar.Text = (list.Items(k).ToString)
                k = k - 1
                textar.Select(0, textar.Text.Length)
                textar.SelectionStart = textar.Text.Length
                j = 0
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(asa, list, textar)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then rob()
        posun(TextBox4, ListBox1, e, 0)
    End Sub
    Public Sub rob()
        Dim pocet As Integer
        Try
            pocet = TextBox1.Text
        Catch ex As Exception
            Chyby.Show("Nezadal si počet")
            Exit Sub
        End Try
        Dim cena As Double
        Try
            If TextBox3.Text.Length <> 0 Then cena = TextBox3.Text Else cena = 0
        Catch ex As Exception
            Chyby.Show("Zlý formát ceny (bodka namiesto ciarky?)")
            Exit Sub
        End Try

        Dim ID As String = TextBox4.Text
        If ID = ("Tu zadaj ID tovaru") Then ID = ""
        If ID.Length = 0 Then
            Chyby.Show("ID nie je zadané")
            Exit Sub
        End If
        Dim nazov As String = TextBox5.Text
        If (nazov = "Tu zadaj názov tovaru") Or (nazov.Length = 0) Then
            Chyby.Show("Treba aj názov tovaru")
            Exit Sub
        End If
        Dim poznamka As String
        If TextBox6.Text <> "Tu zadaj poznámku" Then poznamka = TextBox6.Text Else poznamka = ""

        Dim cesta As String = TextBox2.Text
        Dim cesta2 As String = My.Settings.Rotek3
        Dim connect As String = cesta2
        cesta2 = cesta2 & "fotky\Vzacnosti"
        Try
            If My.Computer.FileSystem.DirectoryExists(cesta2) Then
            Else
                Try
                    My.Computer.FileSystem.CreateDirectory(cesta2)
                Catch ex As Exception
                    Chyby.Show(ex.Message)
                End Try
            End If
            Dim pripona As String = System.IO.Path.GetExtension(cesta)
            cesta2 = cesta2 & "\" & nazov + pripona
            If My.Computer.FileSystem.FileExists(cesta2) Then My.Computer.FileSystem.DeleteFile(cesta2)
            My.Computer.FileSystem.CopyFile(cesta, cesta2)
        Catch ex As Exception
            cesta2 = ""
        End Try

        'Databaza

        Dim Sql As String
        Dim con As New SqlConnection
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Me.VzacnostiBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Vzacnosti.NazovColumn, nazov)
        Dim x As Integer = DataGridView1.RowCount
        Dim cmd As New SqlCommand
        If x = 0 Then
            Sql = "Insert INTO Vzacnosti (Cislo, Cena, Fotka, Pocet, Nazov, Poznamka, znCena, Znicenych) VALUES ('" + ID + "','" & cena & "','" + cesta2 + "','" & pocet & "','" + nazov + "','" & poznamka & "','" & 0 & "','" & 0 & "')"
        Else
            Sql = ""
            If cena = 0 Then cena = DataGridView1.Rows(0).Cells(2).Value
            Dim orig As String = DataGridView1.Rows(0).Cells(6).Value.ToString.ToString
            If poznamka = "" Then poznamka = orig Else poznamka = poznamka & "; " & orig
            If cesta = "" Then cesta = DataGridView1.Rows(0).Cells(3).Value.ToString
            pocet = pocet + DataGridView1.Rows(0).Cells(4).Value
            Sql = "UPDATE Vzacnosti SET Pocet='" & pocet & "', Poznamka='" & poznamka & "', Fotka='" & cesta & "', Cena='" & cena & "' WHERE Nazov='" & nazov & "'"

        End If
        cmd = New SqlCommand(Sql, con)
        cmd.ExecuteNonQuery()
        con.Close()
        skladvz.sku = 1
        Me.Close()
    End Sub

    Private Sub TextBox5_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then rob()
        posun(TextBox5, ListBox2, e, 1)
    End Sub

    Private Sub entesc(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp, TextBox6.KeyUp, TextBox3.KeyUp, MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then rob()
    End Sub

    Private Sub TextBox4_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox4.MouseClick
    End Sub

    Private Sub TextBox5_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox5.MouseClick
        If TextBox5.Text = "Tu zadaj názov tovaru" Then TextBox5.Text = ""
    End Sub

    Private Sub TextBox6_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox6.MouseClick
        If TextBox6.Text = "Tu zadaj poznámku" Then TextBox6.Text = ""
    End Sub

    Private Sub TextBox3_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox3.MouseClick
        If TextBox3.Text = "Tu zadaj cenu v €" Then TextBox3.Text = ""
    End Sub
    Private Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByRef textar As TextBox)

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        Me.VzacnostiBindingSource.Filter = Nothing
        Dim radky As Integer = DataGridView1.RowCount - 1
        Dim slovo As String = UCase(textar.Text)

        Dim stlpec2 As Integer
        Dim pom2 As ListBox
        If stlpec = 0 Then
            stlpec2 = 1
            pom2 = ListBox2

        Else

            stlpec2 = 0
            pom2 = ListBox1
        End If
        Dim i As Integer
        Dim tab, tab2 As String
        For i = 0 To radky
            tab2 = DataGridView1.Rows(i).Cells(stlpec).Value
            tab = UCase(tab2)
            If slovo.Length = 0 Then
                pom.Items.Add(tab2)
                pom2.Items.Add(DataGridView1.Rows(i).Cells(stlpec2).Value)
            Else
                If tab.IndexOf(slovo) = 0 Then
                    pom.Items.Add(DataGridView1.Rows(i).Cells(stlpec).Value)
                    tex = DataGridView1.Rows(i).Cells(stlpec).Value
                    pom2.Items.Add(DataGridView1.Rows(i).Cells(stlpec2).Value)
                    tex2 = DataGridView1.Rows(i).Cells(stlpec2).Value
                End If
            End If
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        rob()
    End Sub
    Private Sub TextBox5_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.Leave
        Me.VzacnostiBindingSource.Filter = Nothing
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        Me.VzacnostiBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Vzacnosti.NazovColumn, TextBox5.Text)
        If DataGridView1.RowCount = 1 Then TextBox4.Text = DataGridView1.Rows(0).Cells(0).Value
        Me.VzacnostiBindingSource.Filter = Nothing
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

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            j = 1
            TextBox4.Text = ListBox1.Text
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
        Me.VzacnostiBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Vzacnosti.NazovColumn, TextBox5.Text)
        Dim x As Integer = DataGridView1.RowCount
        If x = 0 Then
            Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
            Me.VzacnostiBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Vzacnosti.CisloColumn, TextBox4.Text)
            x = DataGridView1.RowCount
            If x = 1 Then TextBox5.Text = DataGridView1.Rows(0).Cells(1).Value
        ElseIf x = 1 Then
            TextBox4.Text = DataGridView1.Rows(0).Cells(0).Value
        End If
        Me.VzacnostiBindingSource.Filter = Nothing
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
    End Sub
    Private Sub TextBox4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Leave
        Me.VzacnostiBindingSource.Filter = Nothing
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        Me.VzacnostiBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Vzacnosti.CisloColumn, TextBox4.Text)
        If DataGridView1.RowCount = 1 Then TextBox5.Text = DataGridView1.Rows(0).Cells(1).Value
        Me.VzacnostiBindingSource.Filter = Nothing

    End Sub

End Class



