Imports System.Data.SqlClient

Public Class srotC
    Public tex As String
    Public k, j As Integer
    Public prvy As String

    Property crc As String

    Public Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        zamestnanec.bmp = 0
        k = 0
        DataGridView1.Hide()
        DataGridView2.Hide()

        Dim x As Integer
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        x = DataGridView1.RowCount
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
        Try

        Catch ex As System.Exception
            Chyby.Show(ex.Message)
        End Try

        prvy = TextBox2.Text


        zamestnanec.bmp = 7
        TextBox1.Text = "Tu zadaj počet"
        hladaj2(ListBox1, 0)
        hladaj2(ListBox2, 1)
        hladaj2(ListBox3, 10)
    End Sub


    Public Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = "1"
        TextBox1.SelectAll()
    End Sub

    Public Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Public Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        kliky()
    End Sub
    Public Overridable Sub kliky()
        zamestnanec.bmp = 0

        Dim nastr As String = TextBox2.Text
        Dim nastr2 As String = TextBox3.Text
        Dim vlast As String = TextBox4.Text
        If vlast.Length = 0 Then vlast = "HSS"
        If nastr.Length = 0 Or nastr2.Length = 0 Then
            Chyby.Show("Niečo nie je zadané")
            Exit Sub
        End If

        Try
            Dim pocet As Integer
            Try
                pocet = TextBox1.Text
            Catch ex As Exception
                Chyby.Show("Nezadal si počet")
                Exit Sub
            End Try

            If pocet < 0 And Form78.heslo <> Form78.admin Then
                Chyby.Show("Len šéf môže odoberať")
                Exit Sub
            End If

            Dim a As Integer = 1

            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}' AND {6} = '{7}' AND {8}='{9}' ", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 1, RotekDataSet.Rotek.NástrojColumn, nastr, RotekDataSet.Rotek.VelkostRColumn, nastr2, RotekDataSet.Rotek.VlastnostColumn, vlast)
            If DataGridView1.RowCount <> 1 Then
                Chyby.Show("Nenašiel sa daný nástroj u zamestnanca")
                Exit Sub
            End If
            Dim meno As String = (DataGridView1.Rows(0).Cells(2).Value)
            Dim priezvisko As String = (DataGridView1.Rows(0).Cells(3).Value)
            Dim kolko As Integer = (DataGridView1.Rows(0).Cells(6).Value)
            kolko = kolko - pocet
            Dim xxx As Integer = 0

            If kolko < 0 Then
                Chyby.Show("Nemá ich toľko")
                Exit Sub
            End If

            If xxx = 0 Then

                Dim srotcena As Double
                Try
                    srotcena = (DataGridView1.Rows(0).Cells(9).Value)
                Catch ex As Exception
                    srotcena = 0
                End Try

                Dim srot As Integer
                Try
                    srot = (DataGridView1.Rows(0).Cells(8).Value)
                Catch ex As Exception
                    srot = 0
                End Try
                srot = srot + pocet

                Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4}='{5}'", RotekDataSet.Sklad.VlastnostColumn, vlast, RotekDataSet.Sklad.NastrojColumn, nastr, RotekDataSet.Sklad.VelkostSColumn, nastr2)
                If DataGridView2.RowCount < 1 Then
                    Chyby.Show("Tento nástroj neexistuje na sklade. Nebude odobratý kým nebude na sklade vytvorený")
                    Exit Sub
                End If
                Dim evra As Double

                Try
                    Dim k As String = (DataGridView2.Rows(0).Cells(0).Value())
                    k = k.Substring(0, k.Length - 1)
                    k = Replace(k, ".", ",")
                    evra = CDec(k)
                Catch ex As Exception
                    evra = 0
                End Try

                srotcena = srotcena + evra * pocet

                Dim Sql As String
                Dim con As New SqlConnection
                con.ConnectionString = My.Settings.Rotek2
                con.Open()

                Dim cmd As New SqlCommand

                Sql = "UPDATE Rotek SET Kolko='" & kolko & "', Spolu='" & a & "', srot='" & srot & "', srotcena='" & srotcena & "' WHERE Nástroj='" + nastr + "' AND pocet=1 AND Menpr='" + crc + "' AND VelkostR='" + nastr2 + "' AND Vlastnost='" + vlast + "'"
                cmd = New SqlCommand(Sql, con)
                cmd.ExecuteNonQuery()

                'TODO: srot pre kompletku


                Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 0)
                Dim spoluu As Integer
                spoluu = DataGridView1.Rows(0).Cells(7).Value() - pocet
                Dim evra2 As Double
                Try
                    evra2 = evra * pocet + DataGridView1.Rows(0).Cells(9).Value()
                Catch ex As Exception
                    evra2 = evra * pocet
                End Try
                Try
                    srot = DataGridView1.Rows(0).Cells(8).Value + pocet
                Catch ex As Exception
                    srot = pocet
                End Try

                Sql = "UPDATE Rotek SET Kolko=0, Spolu='" & spoluu & "', srot='" & srot & "', srotcena='" & evra2 & "' WHERE pocet=0 AND Menpr='" + crc + "'"
                cmd = New SqlCommand(Sql, con)
                cmd.ExecuteNonQuery()
                con.Close()

                zamestnanec.doexcel(crc, nastr, nastr2, vlast, "#" & pocet)

                con.Close()
                Me.Close()

            End If
        Catch ex As SystemException

            Chyby.Show(ex.ToString)
        End Try

        zamestnanec.bmp = 1
    End Sub
    Public Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then kliky()
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Public Sub listbox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
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

    Public Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp

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

    Public Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Public Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
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

    Public Sub TextBox3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
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

    Public Sub TextBox2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        hladaj2(ListBox2, 1)
        TextBox3.Text = ""
    End Sub

    Public Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox3, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj2(ListBox3, 10)
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
                Me.RotekBindingSource.Sort = "Nástroj"
            ElseIf stlpec = 1 Then
                Me.RotekBindingSource.Sort = "VelkostR"
            ElseIf stlpec = 10 Then
                Me.RotekBindingSource.Sort = "Vlastnost"
            End If

            Me.RotekBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6} = '{7}' AND {8} = '{9}' AND {10}>'{11}'", RotekDataSet.Rotek.VlastnostColumn, TextBox4.Text, RotekDataSet.Rotek.VelkostRColumn, TextBox3.Text, RotekDataSet.Rotek.NástrojColumn, TextBox2.Text, RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 1, RotekDataSet.Rotek.KolkoColumn, 0)

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
                Dim stlpc() As Integer = {0, 1, 10}

                listbs.Add(ListBox1)
                listbs.Add(ListBox2)
                listbs.Add(ListBox3)

                aree.Add("Nástroj")
                aree.Add("VelkostR")
                aree.Add("Vlastnost")

                Hpridat.hladaj3(listbs, aree, stlpc, DataGridView1, RotekBindingSource)
            End If
        Catch ex As Exception
            Chyby.Show(ex.ToString)

        End Try
    End Sub

    Public Sub TextBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Enter
        hladaj2(ListBox3, 10)
        k = 0
    End Sub

    Public Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        Try
            j = 1
            TextBox4.Text = ListBox3.Text
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)
        Catch ex As Exception

        End Try
    End Sub

End Class