Imports System.Data.SqlClient

Public Class pridatIne

    Public tex As String
    Public k, j, pox As Integer
    Public prvy As String
    Property crc As String
    Property typ As Integer

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)

        Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)

        zamestnanec.bmp = 0
        k = 0
        DataGridView1.Hide()
        DataGridView2.Hide()
        Dim x As Integer
        x = DataGridView1.RowCount
        prvy = TextBox2.Text

        zamestnanec.bmp = 7
        TextBox1.Text = "Tu zadaj počet"
        hladaj(0, ListBox1, "Ident")
        hladaj(1, ListBox2, "Nazov")
        pox = 0
        tex = ""

        If (typ = 1) Or (typ = 5) Or (typ = 8) Or (typ = 9) Or (typ = 2) Then
            TextBox2.Text = 0
            Label1.Hide()
            TextBox2.Hide()
            ListBox1.Hide()

            TextBox1.Location = New Point(TextBox1.Location.X - 140, TextBox1.Location.Y)
            TextBox3.Location = New Point(TextBox3.Location.X - 140, TextBox3.Location.Y)
            ListBox2.Location = New Point(ListBox2.Location.X - 140, ListBox2.Location.Y)
            Label2.Location = New Point(Label2.Location.X - 140, Label2.Location.Y)
            Label3.Location = New Point(Label3.Location.X - 140, Label3.Location.Y)
            Me.Size = New System.Drawing.Size(Me.Size.Width - 130, Me.Size.Height)

            TextBox3.Select(0, TextBox3.Text.Length)
        End If





    End Sub
    Private Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByVal filter As String)
        Try

            pom.Items.Clear()

            Me.IneBindingSource.Sort = filter
            Me.IneBindingSource.Filter = String.Format("{0}='{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%'", RotekDataSet.Ine.pocetColumn, typ, RotekDataSet.Ine.IdentColumn, TextBox2.Text, RotekDataSet.Ine.NazovColumn, TextBox3.Text)
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
    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = "1"
        TextBox1.SelectAll()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        kliky()
    End Sub
    Public Sub kliky()
        zamestnanec.bmp = 0
        Dim ident As String = TextBox2.Text
        Dim nazov As String = TextBox3.Text
        If nazov.Length = 0 Or ident.Length = 0 Then
            Chyby.Show("Niečo nie je zadané")
            Exit Sub
        End If
        Try
            Dim pocet As Integer = 0
            Try
                pocet = TextBox1.Text
            Catch ex As Exception
                Chyby.Show("Zle zadaný počet")
                Exit Sub
            End Try
            Dim a As Integer = 1
            Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Ine.NazovColumn, nazov, RotekDataSet.Ine.pocetColumn, typ, RotekDataSet.Ine.IdentColumn, ident)
            If DataGridView1.RowCount <> 1 Then
                Chyby.Show("Zle zadané údaje")
                Exit Sub
            End If
            Dim kolko As String = (DataGridView1.Rows(0).Cells(3).Value)
            Dim cesta2 As String = ""
            Try
                If (DataGridView1.Rows(0).Cells(6).Value.ToString.Length <> 0) Then cesta2 = (DataGridView1.Rows(0).Cells(6).Value) Else cesta2 = ""
            Catch ex As Exception
                cesta2 = ""
            End Try
            Dim evra As Double
            Dim k As String = (DataGridView1.Rows(0).Cells(2).Value())
            Try
                k = Replace(k, ".", ",")
                evra = CDec(k)
            Catch ex As Exception
                evra = 0
            End Try
            a = kolko
            kolko = kolko - pocet
            Dim xxx As Integer = 0
            If kolko < 0 Then
                Dim aks As String
                Dim ifg As Integer
                aks = "Na sklade ich je už len: " & a & ". Naozaj mu dať až " & pocet & "?"
                ifg = MsgBox(aks, vbExclamation + vbYesNo, "Overenie")
                If ifg = vbYes Then kolko = 0 Else xxx = 1
            End If
            If xxx = 0 Then
                Dim srot As Integer
                Try
                    srot = (DataGridView1.Rows(0).Cells(4).Value)
                Catch ex As Exception
                    srot = 0
                End Try
                srot = srot + pocet
                Dim srotcena As Double
                Try
                    srotcena = (DataGridView1.Rows(0).Cells(5).Value)
                Catch ex As Exception
                    srotcena = 0
                End Try
                srotcena = srotcena + pocet * evra

                Dim Sql As String
                Dim con As New SqlConnection
                Dim cesta As String
                cesta = "\\192.168.1.140\admin\Sklad\"
                cesta = Replace(cesta, "Rotek sklad.exe", "")
                cesta = Replace(cesta, "Rotek sklad.EXE", "")
                con.ConnectionString = My.Settings.Rotek2
                con.Open()

                Dim cmd As New SqlCommand
                Dim com As New SqlCommand

                com.Connection = con
                Sql = "UPDATE Ine SET Kolko='" & kolko & "' WHERE Nazov='" & nazov & "' AND pocet='" & typ & "' AND Ident='" & ident & "'"
                cmd = New SqlCommand(Sql, con)
                cmd.ExecuteNonQuery()

                typ = typ + 3
                Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}='{7}'", RotekDataSet.Rotek.NástrojColumn, nazov, RotekDataSet.Rotek.pocetColumn, typ, RotekDataSet.Rotek.VelkostRColumn, ident, RotekDataSet.Rotek.MenprColumn, crc)

                If DataGridView2.RowCount = 1 Then
                    Dim pocett As Integer = DataGridView2.Rows(0).Cells(5).Value
                    pocet = pocet + pocett
                    Sql = "UPDATE Rotek SET Kolko='" & pocet & "', Meno='" + cesta2 + "' WHERE Nástroj='" + nazov + "' AND pocet=" & typ & " AND VelkostR='" + ident + "' AND Menpr='" + crc + "'"
                    cmd = New SqlCommand(Sql, con)
                    cmd.ExecuteNonQuery()

                Else
                    Sql = "Insert INTO Rotek (Nástroj, pocet, Menpr, Kolko, Spolu, VelkostR, srot, srotcena, Meno) VALUES ('" + nazov + "', '" & typ & "', '" + crc + "', '" & pocet & "', '" & 0 & "', '" + ident + "', '" & 0 & "', '" & 0 & "','" + cesta2 + "')"
                    cmd = New SqlCommand(Sql, con)
                    cmd.ExecuteNonQuery()
                End If
                con.Close()
                Me.Close()

            End If
        Catch ex As SystemException

            Chyby.Show("Vec nenájdená na sklade. Prosím, skontrolujte hodnoty")
        End Try

        zamestnanec.bmp = 1
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then kliky()
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Private Sub listbox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            pox = 0
            j = 1
            tex = ListBox1.Text
            TextBox2.Text = tex
            TextBox2.Focus()
            TextBox2.Select(0, TextBox2.Text.Length)
            ' TextBox2.SelectionStart = TextBox2.Text.Length

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp

        Try
            Hpridat.stlac(k, sender, ListBox1, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(0, ListBox1, "Ident")
                k = 0
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        Try
            Dim tex2 As String
            j = 1
            tex2 = ListBox2.Text
            TextBox3.Text = tex2
            TextBox3.Focus()
            TextBox3.Select(0, TextBox3.Text.Length)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox2, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(1, ListBox2, "Nazov")
                k = 0
            End If
            If e.KeyCode = Keys.Tab Then
                Me.IneBindingSource.Filter = Nothing
                Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
                Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.NazovColumn, TextBox3.Text, RotekDataSet.Ine.pocetColumn, typ)
                If DataGridView1.RowCount = 1 Then TextBox2.Text = DataGridView1.Rows(0).Cells(0).Value
                Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
    Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
        Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.NazovColumn, TextBox3.Text, RotekDataSet.Ine.pocetColumn, typ)
        Dim x As Integer = DataGridView1.RowCount
        If x = 0 Then
            Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
            Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.IdentColumn, TextBox2.Text, RotekDataSet.Ine.pocetColumn, typ)
            x = DataGridView1.RowCount
            If x = 1 Then TextBox3.Text = DataGridView1.Rows(0).Cells(1).Value
        ElseIf x = 1 Then
            TextBox2.Text = DataGridView1.Rows(0).Cells(0).Value
        End If
        Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)

    End Sub

    Private Sub TextBox2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        hladaj(1, ListBox2, "Nazov")

    End Sub

    Private Sub TextBox3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.Leave
        hladaj(0, ListBox1, "Ident")

    End Sub
End Class