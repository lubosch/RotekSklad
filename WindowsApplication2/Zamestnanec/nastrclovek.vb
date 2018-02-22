Imports System.Data.SqlClient

Public Class nastrclovek
    Public tex As String
    Public k, j As Integer
    Public prvy As String

    Property crc As String

    Property menko As String

    Property prezvo As String

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Rotek' table. You can move, or remove it, as needed.
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)

        k = 0
        DataGridView3.Hide()
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        prvy = TextBox2.Text


        zamestnanec.bmp = 7
        mzamestnanec.bmp = 7

        TextBox1.Text = "Tu zadaj počet"
        hladaj2(ListBox1, 0)
        hladaj2(ListBox2, 1)
        hladaj2(ListBox3, 2)

    End Sub
    Private Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByRef textar As TextBox)
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

        Dim ztabule, ztabulky As String
        Dim ztabule2 As String = ""

        Dim x As Integer = DataGridView3.RowCount - 1
        tex = UCase(textar.Text)
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
                ztabule = (DataGridView3.Rows(i).Cells(stlpec).Value)
                ztabulky = UCase(ztabule)
                Try
                    If ii <> 3 Then
                        While (i - xx >= 0)
                            ztabule2 = UCase(DataGridView3.Rows(i - xx).Cells(stlpec).Value)
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
                If (ztabulky.IndexOf(tex) = 0) And (ztabulky <> ztabule2) Then
                    If ii <> 3 Then
                        pom.Items.Add(ztabule)
                    Else
                        ztabule = (DataGridView3.Rows(i).Cells(1).Value)
                        ztabulky = UCase(ztabule)
                        If (ztabulky.IndexOf(TextBox3.Text) = 0) And (ztabulky <> ztabule2) Then
                            pom.Items.Add(DataGridView3.Rows(i).Cells(1).Value)
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
    Public Overridable Sub kliky()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

        Dim nastroj As String = TextBox2.Text
        Dim nastroj2 As String = TextBox3.Text
        Dim vlast As String = TextBox4.Text
        If vlast.Length = 0 Then vlast = "HSS"
        If nastroj.Length = 0 Or nastroj2.Length = 0 Then
            Chyby.Show("Niečo nie je zadané")
            Exit Sub
        End If
        Try
            Dim spoluu As Integer
            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 0)
            Try
                spoluu = DataGridView4.Rows(0).Cells(0).Value()
            Catch ex As Exception
                spoluu = 0
            End Try

            Dim pocett As Integer
            Dim sql As String

            Try
                pocett = TextBox1.Text
            Catch ex As Exception
                Chyby.Show("Zle zadaný počet")
                Exit Sub
            End Try

            spoluu = spoluu + pocett


            If pocett < 0 Then
                Chyby.Show("Musíš to vrátiť cez tlačítko vrátiť, takto to nefunguje")
                Me.Close()
                Exit Sub
            End If


            Dim xxx As Integer = 0
            Dim a As Integer


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
            Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.Sklad.NastrojColumn, nastroj, RotekDataSet.Sklad.VelkostSColumn, nastroj2, RotekDataSet.Sklad.VlastnostColumn, vlast)
            If DataGridView3.RowCount <> 1 Then
                Chyby.Show("Nastroj nenajdeny")
                Exit Sub
            End If

            a = DataGridView3.Rows(0).Cells(3).Value

            'If a - Pocett < 0 Then
            '    Dim aks As String
            '    Dim ifg As Integer
            '    aks = "Na sklade je už len: " & a & " nastrojov. Chcete mu ich aj tak dať?"
            '    ifg = MsgBox(aks, vbExclamation + vbYesNo, "Overenie")
            '    If ifg = vbYes Then a = 0 Else xxx = 1
            'End If

            a = a - pocett
            Try
                zamestnanec.doexcel(crc, nastroj, nastroj2, vlast, pocett.ToString)
            Catch ex As Exception
                Chyby.Show(ex.ToString)

            End Try

            sql = "UPDATE Sklad SET Pocet='" & a & "' WHERE Nastroj='" & nastroj & "' AND VelkostS='" & nastroj2 & "' AND Vlastnost='" + vlast + "'"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            Dim fsd As Integer = 1


            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}' AND {6}='{7}' AND {8}='{9}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.NástrojColumn, nastroj, RotekDataSet.Rotek.VelkostRColumn, nastroj2, RotekDataSet.Rotek.VlastnostColumn, vlast, RotekDataSet.Rotek.pocetColumn, 1)

            If DataGridView4.RowCount = 0 Then
                Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 0)
                Dim srot As Double
                Try
                    srot = DataGridView3.Rows(0).Cells(4).Value()
                Catch exc As Exception
                    srot = 0
                End Try

                sql = "Insert INTO Rotek (Meno, Priezvisko, Nástroj, pocet, Menpr, Kolko, Spolu, VelkostR, Vlastnost) VALUES ('" + menko + "', '" + prezvo + "', '" + nastroj + "', '" & fsd & "', '" + crc + "', '" & pocett & "', '" & spoluu & "', '" + nastroj2 + "','" + vlast + "')"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()

            Else
                Dim b As Integer
                b = DataGridView4.Rows(0).Cells(3).Value + pocett

                sql = "UPDATE Rotek SET Kolko='" & b & "' WHERE Menpr='" + crc + "' AND pocet=1 AND Nástroj='" & nastroj & "' AND VelkostR='" & nastroj2 & "' AND Vlastnost='" + vlast + "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()

            End If

            sql = "UPDATE Rotek SET Spolu='" & spoluu & "' WHERE Menpr='" + crc + "' AND pocet=0"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()

            con.Close()

        Catch ex As SystemException
            Chyby.Show(ex.ToString)
        End Try
        Me.Close()

    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then kliky()
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub nastrclovek_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseClick
    End Sub
    Private Sub listbox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try

            Dim tex As String
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
                hladaj2(ListBox1, 0)
                k = 0
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        Try
            Dim tex As String
            j = 1
            tex = ListBox2.Text
            TextBox3.Text = tex
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
                hladaj2(ListBox2, 1)
                k = 0
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        hladaj(3, ListBox2, TextBox2)
        TextBox3.Text = ""
    End Sub

    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox3, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj2(ListBox3, 2)

                k = 0
            End If

        Catch ex As Exception
        End Try

    End Sub
    Public Sub hladaj2(ByRef pom As ListBox, ByVal stlpec As Integer)
        Try

            pom.Items.Clear()
            If stlpec = 0 Then
                tex = TextBox4.Text
                Me.SkladBindingSource.Sort = "Nastroj"
            ElseIf stlpec = 1 Then
                Me.SkladBindingSource.Sort = "VelkostS"
            ElseIf stlpec = 2 Then
                Me.SkladBindingSource.Sort = "Vlastnost"
            End If

            Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%'", RotekDataSet.Sklad.VlastnostColumn, TextBox4.Text, RotekDataSet.Sklad.VelkostSColumn, TextBox3.Text, RotekDataSet.Sklad.NastrojColumn, TextBox2.Text)

            Dim slovo As String = ""
            For i As Integer = 0 To DataGridView3.RowCount - 1
                If DataGridView3.Rows(i).Cells(stlpec).Value <> slovo Then
                    pom.Items.Add(DataGridView3.Rows(i).Cells(stlpec).Value)
                    slovo = DataGridView3.Rows(i).Cells(stlpec).Value
                End If
            Next

            If pom.Items.Count = 1 Then
                Dim listbs As List(Of ListBox) = New List(Of ListBox)
                Dim aree As List(Of String) = New List(Of String)
                Dim stlpc() As Integer = {0, 1, 2}

                listbs.Add(ListBox1)
                listbs.Add(ListBox2)
                listbs.Add(ListBox3)

                aree.Add("Nastroj")
                aree.Add("VelkostS")
                aree.Add("Vlastnost")

                Hpridat.hladaj3(listbs, aree, stlpc, DataGridView3, SkladBindingSource)
            End If
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        Try
            j = 1
            TextBox4.Text = ListBox3.Text
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Enter
        hladaj2(ListBox3, 2)
        If DataGridView3.RowCount = 1 Then
            TextBox4.Text = DataGridView3.Rows(0).Cells(2).Value
        End If
    End Sub
End Class