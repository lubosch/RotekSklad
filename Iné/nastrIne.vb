Imports System
Imports System.IO
Imports System.Data.SqlClient

Public Class nastrIne
    Dim tex, tex2, texx As String
    Dim j, k, l As Integer

    Property typ As Integer



    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
        If (typ = 1) Or (typ = 5) Or (typ = 8) Or (typ = 9) Or (typ = 2) Then
            TextBox4.Text = 0
            Label5.Hide()
            TextBox4.Hide()
            ListBox1.Hide()

            TextBox5.Location = New Point(TextBox5.Location.X - 140, TextBox5.Location.Y)
            TextBox1.Location = New Point(TextBox1.Location.X - 140, TextBox1.Location.Y)
            TextBox2.Location = New Point(TextBox2.Location.X - 140, TextBox2.Location.Y)
            TextBox3.Location = New Point(TextBox3.Location.X - 140, TextBox3.Location.Y)
            TextBox6.Location = New Point(TextBox6.Location.X - 140, TextBox6.Location.Y)
            ListBox2.Location = New Point(ListBox2.Location.X - 140, ListBox2.Location.Y)
            Button3.Location = New Point(Button3.Location.X - 140, Button3.Location.Y)
            Button1.Location = New Point(90, Button1.Location.Y)
            Button2.Location = New Point(10, Button2.Location.Y)
            Label1.Location = New Point(Label1.Location.X - 140, Label1.Location.Y)
            Label2.Location = New Point(Label2.Location.X - 140, Label2.Location.Y)
            Label3.Location = New Point(Label3.Location.X - 140, Label3.Location.Y)
            Label4.Location = New Point(Label4.Location.X - 140, Label4.Location.Y)
            Label5.Location = New Point(Label5.Location.X - 140, Label5.Location.Y)
            Me.Size = New System.Drawing.Size(Me.Size.Width - 130, Me.Size.Height)

            TextBox5.Select(0, TextBox5.Text.Length)

        End If
        j = 0
        hladaj(0, ListBox1, "Ident")
        hladaj(1, ListBox2, "Nazov")

        sklad.v = 47
        DataGridView1.Hide()
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
    Private Sub posun(ByRef textar As TextBox, ByRef list As ListBox, ByVal e As System.Windows.Forms.KeyEventArgs)
        Hpridat.stlac(k, textar, list, e)

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
        'If e.KeyCode = Keys.Tab Then
        '    Me.IneBindingSource.Filter = Nothing
        '    Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        '    Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.IdentColumn, TextBox4.Text, RotekDataSet.Ine.pocetColumn, typ)
        '    If DataGridView1.RowCount = 1 Then TextBox5.Text = DataGridView1.Rows(0).Cells(1).Value
        '    Me.IneBindingSource.Filter = Nothing
        '    Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
        'End If
        If e.KeyCode = Keys.Enter Then rob()
        posun(TextBox4, ListBox1, e)
        If e.KeyCode = Keys.Down Then
            Exit Sub
        ElseIf e.KeyCode = Keys.Up Then
            Exit Sub
        End If
        If Char.IsLetterOrDigit(CChar(CStr(e.KeyCode))) Then
            hladaj(0, ListBox1, "Ident")
        Else

        End If

    End Sub
    Public Sub rob()

        Dim pocet As Double

        Try
            pocet = TextBox1.Text
            If typ <> 10 Then
                Dim p As Integer = TextBox1.Text
                If p <> pocet Then
                    Chyby.Show("Nezadal si počet")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Chyby.Show("Nezadal si počet")
            Exit Sub
        End Try
        Dim cena As Double
        Try
            Dim cenatext As String
            cenatext = TextBox3.Text
            If cenatext.Length = 0 Then
                cena = 0
                Exit Try
            End If

            cena = TextBox3.Text
        Catch ex As Exception
            Chyby.Show("Zlý formát ceny (bodka namiesto ciarky?)")
            Exit Sub
        End Try

        Dim ID As String = TextBox4.Text
        If ID.Length = 0 Then
            Chyby.Show("ID nie je zadané")
            Exit Sub
        End If

        Dim nazov As String = TextBox5.Text
        If (nazov.Length = 0) Then
            Chyby.Show("Treba aj názov tovaru")
            Exit Sub
        End If
        Dim poznamka As String
        poznamka = TextBox6.Text

        Dim cesta As String = TextBox2.Text
        Dim cesta2 As String = My.Settings.Rotek3

        Dim connect As String = My.Settings.Rotek2
        Select Case typ
            Case 1
                cesta2 = cesta2 & "fotky\Spotrebny"
            Case 2
                cesta2 = cesta2 & "fotky\Pomocky"
            Case 3
                cesta2 = cesta2 & "fotky\Upinaci-specialne"
            Case 4
                cesta2 = cesta2 & "fotky\Elektronaradie"
            Case 5
                cesta2 = cesta2 & "fotky\Prislusenstvo"
            Case 6
                cesta2 = cesta2 & "fotky\Naradie"
            Case 7
                cesta2 = cesta2 & "fotky\Meradla"
            Case 8
                cesta2 = cesta2 & "fotky\Spojovaci"
            Case 9
                cesta2 = cesta2 & "fotky\Capiny"
            Case 10
                cesta2 = cesta2 & "fotky\Kvapaliny"


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
            cesta2 = cesta2 & "\" & nazov & " " & ID + pripona


            If My.Computer.FileSystem.FileExists(cesta2) Then My.Computer.FileSystem.DeleteFile(cesta2)
            My.Computer.FileSystem.CopyFile(cesta, cesta2)
        Catch ex As Exception
            '   Chyby.Show(ex.Message)
            cesta2 = ""
        End Try

        'Databaza

        Dim Sql As String
        Dim con As New SqlConnection
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Ine.NazovColumn, nazov, RotekDataSet.Ine.pocetColumn, typ, RotekDataSet.Ine.IdentColumn, ID)
        Dim x As Integer = DataGridView1.RowCount
        Dim cmd As New SqlCommand
        If x = 0 Then
            Sql = "Insert INTO Ine (Ident, Cena, Fotka, Kolko, pocet, Nazov, Poznámka, Srotcena, Srot) VALUES ('" + ID + "','" & cena & "','" + cesta2 + "','" & pocet & "','" & typ & "','" + nazov + "','" & poznamka & "','" & 0 & "','" & 0 & "')"
        Else
            If cena = 0 Then cena = DataGridView1.Rows(0).Cells(2).Value
            Dim orig As String = DataGridView1.Rows(0).Cells(6).Value.ToString.ToString
            If poznamka = "" Then poznamka = orig Else poznamka = poznamka & "; " & orig
            If cesta2.Length = 0 Then cesta2 = DataGridView1.Rows(0).Cells(3).Value.ToString
            pocet = pocet + DataGridView1.Rows(0).Cells(4).Value
            Sql = "UPDATE Ine SET Kolko='" & pocet & "', Poznámka='" & poznamka & "', Fotka='" & cesta2 & "', Cena='" & cena & "' WHERE Nazov='" & nazov & "' AND Ident='" & ID & "' AND pocet='" & typ & "'"
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
        posun(TextBox5, ListBox2, e)
        If e.KeyCode = Keys.Down Then
            Exit Sub
        ElseIf e.KeyCode = Keys.Up Then
            Exit Sub
        End If
        If Char.IsLetterOrDigit(CChar(CStr(e.KeyCode))) Then

            hladaj(1, ListBox2, "Nazov")

        End If

        'If e.KeyCode = Keys.Tab Then
        '    Me.IneBindingSource.Filter = Nothing
        '    Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        '    Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.NazovColumn, TextBox5.Text, RotekDataSet.Ine.pocetColumn, typ)
        '    If DataGridView1.RowCount = 1 Then TextBox4.Text = DataGridView1.Rows(0).Cells(0).Value
        '    Me.IneBindingSource.Filter = Nothing
        '    Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
        'End If


    End Sub

    Private Sub entesc(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp, TextBox6.KeyUp, TextBox3.KeyUp, MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then rob()
    End Sub

    Private Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByVal filter As String)
        Try

            pom.Items.Clear()

            Me.IneBindingSource.Sort = filter
            Me.IneBindingSource.Filter = String.Format("{0}='{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%'", RotekDataSet.Ine.pocetColumn, typ, RotekDataSet.Ine.IdentColumn, TextBox4.Text, RotekDataSet.Ine.NazovColumn, TextBox5.Text)

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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        rob()
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        Try
            TextBox5.Text = ListBox2.Text
            TextBox5.Focus()
            TextBox5.Select(0, TextBox5.Text.Length)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try

            TextBox4.Text = ListBox1.Text
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.NazovColumn, TextBox5.Text, RotekDataSet.Ine.pocetColumn, typ)
        'Dim x As Integer = DataGridView1.RowCount
        'If x = 0 Then
        '    Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        '    Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.IdentColumn, TextBox4.Text, RotekDataSet.Ine.pocetColumn, typ)
        '    x = DataGridView1.RowCount
        '    If x = 1 Then TextBox5.Text = DataGridView1.Rows(0).Cells(1).Value
        'ElseIf x = 1 Then
        '    TextBox4.Text = DataGridView1.Rows(0).Cells(0).Value
        'End If
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)

    End Sub


    Private Sub TextBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Click
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.NazovColumn, TextBox5.Text, RotekDataSet.Ine.pocetColumn, typ)
        'If DataGridView1.RowCount = 1 Then TextBox4.Text = DataGridView1.Rows(0).Cells(0).Value
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
    End Sub

    Private Sub TextBox5_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox5.MouseClick
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.IdentColumn, TextBox4.Text, RotekDataSet.Ine.pocetColumn, typ)
        'If DataGridView1.RowCount = 1 Then TextBox5.Text = DataGridView1.Rows(0).Cells(1).Value
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
    End Sub
    Private Sub TextBox4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Leave
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.IdentColumn, TextBox4.Text, RotekDataSet.Ine.pocetColumn, typ)
        'If DataGridView1.RowCount = 1 Then TextBox5.Text = DataGridView1.Rows(0).Cells(1).Value
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)

    End Sub

    Private Sub TextBox5_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.Leave
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.NazovColumn, TextBox5.Text, RotekDataSet.Ine.pocetColumn, typ)
        'If DataGridView1.RowCount = 1 Then TextBox4.Text = DataGridView1.Rows(0).Cells(0).Value
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
    End Sub

    Private Sub TextBox4_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox4.Enter
        'hladaj(0, ListBox1, "Ident")

    End Sub

    Private Sub TextBox5_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox5.Enter
        hladaj(1, ListBox2, "Nazov")

    End Sub
End Class



