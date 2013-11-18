Imports System.Data.SqlClient

Public Class srotIne
    Public tex As String
    Public k, j, pox As Integer
    Public prvy As String

    Property crc As String

    Property typ As Integer

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)

        zamestnanec.bmp = 0
        k = 0
        DataGridView1.Hide()
        Dim x As Integer
        x = DataGridView1.RowCount
        prvy = TextBox2.Text

        zamestnanec.bmp = 7
        TextBox1.Text = "Tu zadaj počet"
        hladaj(0, ListBox1, TextBox2)
        hladaj(1, ListBox2, TextBox3)
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
    Private Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByRef textar As TextBox)
        Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)

        Dim ztabule, ztabulky As String
        Dim ztabule2 As String = ""
        Dim tex2 As String
        Dim x As Integer = DataGridView1.RowCount - 1
        tex2 = UCase(textar.Text)
        Dim i As Integer = 0
        Dim ii As Integer = 0
        Dim xx As Integer = 1
        Dim b As Integer = 0
        pom.Items.Clear()
        If stlpec = 3 Then
            ii = 3
            stlpec = 0
        End If
        While i <= x

            Try
                ztabule = (DataGridView1.Rows(i).Cells(stlpec).Value)

                ztabulky = UCase(ztabule)
                Try
                    If ii <> 3 Then
                        While (i - xx >= 0)
                            ztabule2 = UCase(DataGridView1.Rows(i - xx).Cells(stlpec).Value)
                            xx = xx + 1
                            If ztabulky = ztabule2 Then
                                xx = 1
                                Exit While
                            End If
                        End While
                    Else
                        ztabule2 = ""
                    End If
                Catch ex As SystemException
                End Try
                ' Chyby.Show(ztabule & " " & ztabule2 & " " & ztabule.IndexOf(tex) & " " & ztabule <> ztabule2)
                If (ztabulky.IndexOf(tex2) = 0) And (ztabulky <> ztabule2) Then
                    If ii <> 3 Then
                        pom.Items.Add(ztabule)
                        tex = ztabule
                        pox = 1
                    Else
                        ztabule = (DataGridView1.Rows(i).Cells(1).Value)
                        ztabulky = UCase(ztabule)
                        If (ztabulky.IndexOf(UCase(TextBox3.Text)) = 0) And (ztabulky <> ztabule2) Then
                            pom.Items.Add(DataGridView1.Rows(i).Cells(1).Value)
                        End If
                    End If
                    'j = 1
                    'textar.Text = ztabule
                End If
            Catch ex As Exception
            End Try
            i = i + 1
            xx = 1
        End While
        Dim a As Integer = tex2.Length
        textar.Select(a, textar.Text.Length - a)

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
        If ident.Length = 0 Or nazov.Length = 0 Then
            Chyby.Show("Niečo nie je zadané")
            Exit Sub
        End If
        Try
            Dim pocet As Double = 0
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
                Chyby.Show("Zle zadaný počet")
                Exit Sub
            End Try
            Dim a As Integer = 1
            Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Ine.NazovColumn, nazov, RotekDataSet.Ine.pocetColumn, typ, RotekDataSet.Ine.IdentColumn, ident)
            If DataGridView1.RowCount <> 1 Then
                Chyby.Show("Zle zadané údaje")
                Exit Sub
            End If
            Dim kolko As Double = (DataGridView1.Rows(0).Cells(3).Value)
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
                aks = "na sklade je už len: " & a & ". Naozaj sa zničilo až " & pocet & "?"
                ifg = MsgBox(aks, vbExclamation + vbYesNo, "Overenie")
                If ifg = vbYes Then kolko = 0 Else xxx = 1
            End If
            If xxx = 0 Then
                Dim srot As Double
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
                Sql = "UPDATE Ine SET Kolko='" & kolko & "', Srot='" & srot & "', Srotcena='" & srotcena & "' WHERE Nazov='" & nazov & "' AND pocet='" & typ & "' AND Ident='" & ident & "'"
                cmd = New SqlCommand(Sql, con)
                cmd.ExecuteNonQuery()
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
                hladaj(0, ListBox1, TextBox2)
                k = 0
            End If
            If e.KeyCode = Keys.Tab Then
                Me.IneBindingSource.Filter = Nothing
                Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
                Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.IdentColumn, TextBox2.Text, RotekDataSet.Ine.pocetColumn, typ)
                If DataGridView1.RowCount = 1 Then TextBox3.Text = DataGridView1.Rows(0).Cells(1).Value
                Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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
                hladaj(3, ListBox2, TextBox2)
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

    Private Sub TextBox2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.IdentColumn, TextBox2.Text, RotekDataSet.Ine.pocetColumn, typ)
        'If DataGridView1.RowCount = 1 Then TextBox3.Text = DataGridView1.Rows(0).Cells(1).Value
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)

    End Sub

    Private Sub TextBox3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.Leave
        'Me.IneBindingSource.Filter = Nothing
        'Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.NazovColumn, TextBox3.Text, RotekDataSet.Ine.pocetColumn, typ)
        'If DataGridView1.RowCount = 1 Then TextBox2.Text = DataGridView1.Rows(0).Cells(0).Value
        'Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
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

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox2_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox2.MouseClick
        Me.IneBindingSource.Filter = Nothing
        Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        Me.IneBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Ine.NazovColumn, TextBox3.Text, RotekDataSet.Ine.pocetColumn, typ)
        If DataGridView1.RowCount = 1 Then TextBox2.Text = DataGridView1.Rows(0).Cells(0).Value
        Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
    End Sub

    Private Sub TextBox2_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox2.Enter
        'hladaj(0, ListBox1, TextBox2)

    End Sub

    Private Sub TextBox3_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox3.Enter
        hladaj(1, ListBox2, TextBox3)

    End Sub
End Class