Imports System.Data.SqlClient

Public Class srotV
    Public tex As String
    Public k, j, pox As Integer
    Public prvy As String

    Property crc As String

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        zamestnanec.bmp = 0

        k = 0
        DataGridView1.Hide()
        Dim x As Integer

        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        x = DataGridView1.RowCount
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
        Try

        Catch ex As System.Exception
            Chyby.Show(ex.ToString)
        End Try


        prvy = TextBox2.Text


        zamestnanec.bmp = 7
        TextBox1.Text = "Tu zadaj počet"
        hladaj(0, ListBox1, TextBox2)
        hladaj(1, ListBox2, TextBox3)
        hladaj(7, ListBox3, TextBox4)

        pox = 0
        tex = ""
    End Sub
    Private Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByRef textar As TextBox)
        Try

            pom.Items.Clear()
            If stlpec = 0 Then
                tex = TextBox2.Text
                Me.SkladBindingSource.Sort = "Nastroj"
                Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%'", RotekDataSet.Sklad.NastrojColumn, TextBox2.Text)
            ElseIf stlpec = 1 Then
                Me.SkladBindingSource.Sort = "VelkostS"
                Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%'", RotekDataSet.Sklad.NastrojColumn, TextBox2.Text, RotekDataSet.Sklad.VelkostSColumn, TextBox3.Text, RotekDataSet.Sklad.VlastnostColumn, TextBox4.Text)
            ElseIf stlpec = 7 Then
                Me.SkladBindingSource.Sort = "Vlastnost"
                Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%'", RotekDataSet.Sklad.VlastnostColumn, TextBox4.Text)
            End If

            Dim slovo As String = ""
            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Cells(stlpec).Value <> slovo Then
                    pom.Items.Add(DataGridView1.Rows(i).Cells(stlpec).Value)
                    slovo = DataGridView1.Rows(i).Cells(stlpec).Value
                End If
            Next

            If stlpec = 0 And pom.Items.Count <> 0 Then
                textar.Text = pom.Items(0)
                textar.SelectionLength = textar.Text.Length
                textar.SelectionStart = tex.Length
            End If

            If pom.Items.Count = 1 Then
                Dim listbs As List(Of ListBox) = New List(Of ListBox)
                Dim aree As List(Of String) = New List(Of String)
                Dim stlpc() As Integer = {0, 1, 7}

                listbs.Add(ListBox1)
                listbs.Add(ListBox2)
                listbs.Add(ListBox3)

                aree.Add("Nastroj")
                aree.Add("VelkostS")
                aree.Add("Vlastnost")

                Hpridat.hladaj3(listbs, aree, stlpc, DataGridView1, SkladBindingSource)
            End If

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
                Chyby.Show("Nezadal si pocet")
                Exit Sub
            End Try
            If (pocet < 0) And (Form78.admin <> Form78.heslo) Then
                Chyby.Show("Nemôžeš vyberať zo smetiska. Iba admin môže")
                Exit Sub
            End If
            Dim a As Integer = 1
            Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.Sklad.NastrojColumn, nastr, RotekDataSet.Sklad.VelkostSColumn, nastr2, RotekDataSet.Sklad.VlastnostColumn, vlast)
            Dim kolko As String
            Try
                kolko = (DataGridView1.Rows(0).Cells(2).Value)
            Catch ex As Exception
                Chyby.Show("Nástroj nenájdený")
                Exit Sub
            End Try

            Dim evra As Double
            Dim cenka As String
            Dim k As String = (DataGridView1.Rows(0).Cells(4).Value())
            cenka = k
            Try
                k = k.Substring(0, k.Length - 1)
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
                Dim srot As Integer
                Try
                    srot = (DataGridView1.Rows(0).Cells(5).Value)
                Catch ex As Exception
                    srot = 0
                End Try
                srot = srot + pocet
                Dim srotcena As Double
                Try
                    srotcena = (DataGridView1.Rows(0).Cells(6).Value)
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



                Sql = "UPDATE Sklad SET Pocet='" & kolko & "', zosrot='" & srot & "', srotcena='" + srotcena.ToString + "' WHERE VelkostS='" + nastr2 + "' AND Nastroj='" + nastr + "' AND Vlastnost='" + vlast + "'"
                cmd = New SqlCommand(Sql, con)
                cmd.ExecuteNonQuery()

                zamestnanec.doexcel("Sklad", nastr, nastr2, vlast, "#" & pocet)
                con.Close()
                Me.Close()

            End If
        Catch ex As SystemException

            Chyby.Show(ex.ToString)
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
            ElseIf e.KeyCode = Keys.Back Then
                If ListBox1.Items.Count > 0 Then TextBox2.Text = tex.Substring(0, tex.Length - 1)
            End If
            If ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(0, ListBox1, TextBox2)
                k = 0
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
                hladaj(1, ListBox2, TextBox2)
                k = 0
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
    End Sub

    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox3, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(7, ListBox3, TextBox4)
                k = 0
            End If

        Catch ex As Exception
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

    Private Sub TextBox3_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox3.Enter
        If pox = 1 Then
            TextBox2.Text = tex
        End If
        pox = 0
        hladaj(1, ListBox2, TextBox2)
        TextBox3.Text = ""

    End Sub

    Private Sub TextBox2_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox2.Enter
        'hladaj(0, ListBox1, TextBox2)

    End Sub

    Private Sub TextBox4_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox4.Enter
        hladaj(7, ListBox3, TextBox4)

    End Sub
End Class