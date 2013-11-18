Imports System.Data.SqlClient

Public Class M_pridat

    Public typ As Integer

    Public k As Integer

    Public Sub New(ByVal typ As Integer)

        Me.typ = typ
        InitializeComponent()

    End Sub

    Public Sub New()

        InitializeComponent()

    End Sub


    Private Sub M_pridat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.MajetokTableAdapter.Fill(Me.RotekDataSet.Majetok)
        Me.MajetokBindingSource.Filter = String.Format("{0}='{1}'", RotekDataSet.Majetok.TypColumn, typ)
        k = 0

        hladaj(0, ListBox1, "Ident")
        hladaj(1, ListBox2, "Nazov")
        hladaj(2, ListBox3, "Izba")
        hladaj(3, ListBox4, "Miesto")

    End Sub


    Private Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByVal filter As String)
        Try

            pom.Items.Clear()

            Me.MajetokBindingSource.Sort = filter
            Me.MajetokBindingSource.Filter = String.Format("{0}='{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8} LIKE '{9}%' ", RotekDataSet.Majetok.TypColumn, typ, RotekDataSet.Majetok.IdentColumn, TextBox1.Text, RotekDataSet.Majetok.NazovColumn, TextBox2.Text, RotekDataSet.Majetok.IzbaColumn, TextBox3.Text, RotekDataSet.Majetok.MiestoColumn, TextBox4.Text)


            Dim slovo As String = ""
            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Cells(stlpec).Value <> slovo Then
                    pom.Items.Add(DataGridView1.Rows(i).Cells(stlpec).Value)
                    slovo = DataGridView1.Rows(i).Cells(stlpec).Value
                End If
            Next

        Catch ex As Exception
            '  Chyby.Show(ex.Message)
        End Try
    End Sub

    Private Sub hladaj2()
        ListBox4.Items.Clear()
        Me.MajetokBindingSource.Filter = String.Format("{0}='{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8} LIKE '{9}%' ", RotekDataSet.Majetok.TypColumn, typ, RotekDataSet.Majetok.IdentColumn, TextBox1.Text, RotekDataSet.Majetok.NazovColumn, TextBox2.Text, RotekDataSet.Majetok.IzbaColumn, TextBox3.Text, RotekDataSet.Majetok.MiestoColumn, TextBox4.Text)
        If DataGridView1.RowCount = 1 Then
            Dim miesto As String = DataGridView1.Rows(0).Cells(3).Value
            While (miesto.IndexOf(";") <> -1)
                Dim s As String = miesto.Substring(miesto.LastIndexOf(";") + 1)

                If s.IndexOf(TextBox4.Text) <> -1 Then
                    ListBox4.Items.Add(s)
                End If

                miesto = miesto.Remove(miesto.LastIndexOf(";"))

            End While
            ListBox4.Items.Add(miesto)



        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

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
        TextBox6.Text = cesta
        TextBox6.Select(TextBox2.Text.Length, TextBox2.Text.Length)

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        hladaj(0, ListBox1, "Ident")
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        hladaj(1, ListBox2, "Nazov")
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        hladaj(2, ListBox3, "Izba")
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        hladaj2()
    End Sub
    Public Overridable Sub rob()
        Dim ident, nazov, izba, miesto, poznamka, cesta As String
        Dim pocet, typp As Integer
        typp = typ

        ident = TextBox1.Text
        nazov = TextBox2.Text
        izba = TextBox3.Text
        miesto = TextBox4.Text
        poznamka = RichTextBox1.Text
        cesta = TextBox6.Text
        If (ident.Length = 0 Or nazov.Length = 0 Or izba.Length = 0) Then
            Chyby.Show("Nezadané povinné hodnoty")
            Return
        End If
        Try
            pocet = TextBox5.Text
        Catch ex As Exception
            Chyby.Show("Nezadaný počet")
            Return
        End Try
        'koniec


        Dim cesta2 As String = My.Settings.Rotek3
        Dim connect As String = My.Settings.Rotek2
        Select Case typ
            Case 1
                cesta2 = cesta2 & "fotky\Spotrebny"
            Case 2
                cesta2 = cesta2 & "fotky\Pomocky"
            Case 3
        End Select

        Try

            If My.Computer.FileSystem.DirectoryExists(cesta2) Then
            Else
                Try
                    'Chyby.Show(cesta2.Length & " " & cesta2)
                    My.Computer.FileSystem.CreateDirectory(cesta2)
                Catch ex As Exception
                    Chyby.Show(ex.ToString)
                End Try
            End If

            Dim pripona As String = System.IO.Path.GetExtension(cesta)
            cesta2 = cesta2 & "\" & nazov & " " & ident & " " & izba + pripona


            If My.Computer.FileSystem.FileExists(cesta2) Then My.Computer.FileSystem.DeleteFile(cesta2)
            My.Computer.FileSystem.CopyFile(cesta, cesta2)
        Catch ex As Exception
            '   Chyby.Show(ex.Message)
            cesta2 = ""
        End Try
        'koniec cesty


        Dim Sql As String
        Dim con As New SqlConnection
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Dim cmd As New SqlCommand

        Me.MajetokBindingSource.Filter = String.Format("{0}='{1}' AND {2} ='{3}' AND {4} = '{5}' AND {6} = '{7}'", RotekDataSet.Majetok.TypColumn, typ, RotekDataSet.Majetok.IdentColumn, TextBox1.Text, RotekDataSet.Majetok.NazovColumn, TextBox2.Text, RotekDataSet.Majetok.IzbaColumn, TextBox3.Text)
        If DataGridView1.RowCount = 0 Then
            Sql = "Insert INTO Majetok (Ident, Nazov, Izba, Miesto, Pocet, Typ, Poznamka, Obrazok) VALUES ('" + ident + "','" + nazov + "','" + izba + "','" + miesto + "','" & pocet & "','" & typ & "','" + poznamka + "','" + cesta2 + "')"
        ElseIf DataGridView1.RowCount = 1 Then
            If miesto.Length = 0 Then
                miesto = DataGridView1.Rows(0).Cells(3).Value

            ElseIf miesto <> DataGridView1.Rows(0).Cells(3).Value Then
                Dim result As DialogResult = MessageBox.Show("Miesto už je zadané a je to na: " & DataGridView1.Rows(0).Cells(3).Value & "; Chcete zmazať staré miesto?", "Problem", MessageBoxButtons.YesNo)
                If result = vbYes Then

                Else
                    miesto = DataGridView1.Rows(0).Cells(3).Value + "; " + miesto

                End If
            Else
                Return

            End If

            Dim orig As String = DataGridView1.Rows(0).Cells(5).Value.ToString
            If poznamka.Length = 0 Then poznamka = orig Else poznamka = poznamka & "; " & orig
            If cesta2.Length = 0 Then cesta2 = DataGridView1.Rows(0).Cells(4).Value.ToString
            pocet = pocet + DataGridView1.Rows(0).Cells(6).Value
            Sql = "UPDATE Majetok SET Pocet='" & pocet & "', Poznamka='" + poznamka + "', Obrazok='" + cesta2 + "', Miesto='" + miesto + "' WHERE Nazov='" + nazov + "' AND Ident='" + ident + "'  AND Izba='" + izba + "' AND Typ=" & typp & ""
        Else
            Chyby.Show("Nejaka chyba")
            Exit Sub
        End If
        cmd = New SqlCommand(Sql, con)
        cmd.ExecuteNonQuery()
        con.Close()
        Me.Close()


    End Sub

    Private Sub posun(ByRef textar As TextBox, ByVal list As ListBox, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If k < 0 Then
                k = list.Items.Count - 1
            ElseIf k >= list.Items.Count Then
                k = 0
            End If
            If e.KeyCode = 40 Then
                textar.Text = (list.Items(k).ToString)
                k = k + 1
                textar.Select(0, textar.Text.Length)
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Up Then
                textar.Text = (list.Items(k).ToString)
                k = k - 1
                textar.Select(0, textar.Text.Length)
            ElseIf e.KeyCode = Keys.Enter Then
                rob()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RichTextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RichTextBox1.KeyUp, TextBox6.KeyUp, TextBox5.KeyUp

        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then rob()

    End Sub


    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        posun(TextBox1, ListBox1, e)
    End Sub

    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        posun(TextBox2, ListBox2, e)
    End Sub

    Private Sub TextBox3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        posun(TextBox3, ListBox3, e)
    End Sub


    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        posun(TextBox4, ListBox4, e)
    End Sub

    Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
        'hladaj(0, ListBox1, "Ident")
        k = 0
    End Sub

    Private Sub TextBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Enter
        hladaj(1, ListBox2, "Nazov")
        k = 0
    End Sub

    Private Sub TextBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.Enter
        hladaj(2, ListBox3, "Izba")

        k = 0
    End Sub

    Private Sub TextBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Enter
        hladaj2()

        k = 0
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            TextBox1.Text = ListBox1.Text
            TextBox1.Focus()
            TextBox1.Select(0, TextBox1.Text.Length)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        Try
            TextBox2.Text = ListBox2.Text
            TextBox2.Focus()
            TextBox2.Select(0, TextBox2.Text.Length)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        Try
            TextBox3.Text = ListBox3.Text
            TextBox3.Focus()
            TextBox3.Select(0, TextBox3.Text.Length)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox4.SelectedIndexChanged
        Try
            TextBox4.Text = ListBox4.Text
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        rob()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()

    End Sub
End Class