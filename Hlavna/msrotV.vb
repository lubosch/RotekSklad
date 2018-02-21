Public Class srotV
    Public tex As String
    Public k, j, pox As Integer
    Public prvy As String

    Property crc As String

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Sklad' table. You can move, or remove it, as needed.
        zamestnanec.bmp = 0
        
        k = 0
        DataGridView1.Hide()
        Dim x As Integer

        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        x = DataGridView1.RowCount
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
        Try
            Me.RotekTableAdapter.FillBy2(Me.RotekDataSet.Rotek)
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try




        prvy = TextBox2.Text

        'TODO: This line of code loads data into the 'RotekDataSet.pocet' table. You can move, or remove it, as needed.
        zamestnanec.bmp = 7
        TextBox1.Text = "Tu zadaj počet"
        hladaj(0, ListBox1, TextBox2)
        hladaj(1, ListBox2, TextBox3)
        pox = 0
        tex = ""
    End Sub
    Private Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByRef textar As TextBox)
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

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
                ' MessageBox.Show(ztabule & " " & ztabule2 & " " & ztabule.IndexOf(tex) & " " & ztabule <> ztabule2)
                If (ztabulky.IndexOf(tex2) <> -1) And (ztabulky <> ztabule2) Then
                    If ii <> 3 Then
                        pom.Items.Add(ztabule)
                        tex = ztabule
                        pox = 1
                    Else
                        ztabule = (DataGridView1.Rows(i).Cells(1).Value)
                        ztabulky = UCase(ztabule)
                        If (ztabulky.IndexOf(TextBox3.Text) <> -1) And (ztabulky <> ztabule2) Then
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

        Dim nastr As String = TextBox2.Text
        Dim nastr2 As String = TextBox3.Text
        Try
            Dim pocet As Integer = TextBox1.Text
            Dim a As Integer = 1
            Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Sklad.NastrojColumn, nastr, RotekDataSet.Sklad.VelkostSColumn, nastr2)

            Dim kolko As String = (DataGridView1.Rows(0).Cells(2).Value)
            Dim regal As String
            Try
                regal = (DataGridView1.Rows(0).Cells(3).Value)
            Catch ex As Exception
                regal = ""
            End Try
            Dim evra As Double
            Dim cenka As String
            Dim k As String = (DataGridView1.Rows(0).Cells(4).Value())
            cenka = k
            Try
                k = k.Substring(0, k.Length - 1)
                k = Replace(k, ".", ",")
                evra = CDbl(k)
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
                Dim con As New OleDb.OleDbConnection
                Dim cesta As String
                cesta = Application.ExecutablePath
                cesta = Replace(cesta, "Rotek sklad.exe", "")
                cesta = Replace(cesta, "Rotek sklad.EXE", "")
                con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & cesta & "Rotek.mdb"
                con.Open()

                Dim cmd As New OleDb.OleDbCommand
                Dim com As New OleDb.OleDbCommand

                com.Connection = con
                com.CommandText = "DELETE FROM Sklad WHERE VelkostS ='" + nastr2 + "' AND Nastroj ='" + nastr + "'"
                com.ExecuteNonQuery()

                Sql = "Insert INTO Sklad (Nastroj, Pocet, Regal, Cena, VelkostS, zosrot, srotcena) VALUES ('" + nastr + "', '" & kolko & "', '" + regal + "', '" & cenka & "', '" + nastr2 + "', '" & srot & "', '" + srotcena.ToString + "')"
                cmd = New OleDb.OleDbCommand(Sql, con)
                cmd.ExecuteNonQuery()


                con.Close()
                Me.Close()

            End If
        Catch ex As SystemException

            MessageBox.Show("Nezadal si pocet")
        End Try

        zamestnanec.bmp = 1
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then kliky()
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Private Sub textbox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If j <> 1 Then
            hladaj(0, ListBox1, TextBox2)
            k = 0
        Else
            j = 0

        End If

    End Sub

    Private Sub listbox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        pox = 0
        j = 1
        tex = ListBox1.Text
        TextBox2.Text = tex
        TextBox2.Focus()
        TextBox2.Select(0, TextBox2.Text.Length)
        ' TextBox2.SelectionStart = TextBox2.Text.Length
    End Sub

    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp

        Try
            If k < 0 Then
                k = ListBox1.Items.Count - 1

            ElseIf k >= ListBox1.Items.Count Then
                k = 0
            End If


            If e.KeyCode = 40 Then
                pox = 0
                j = 1
                TextBox2.Text = (ListBox1.Items(k).ToString)
                k = k + 1
                TextBox2.Select(0, TextBox2.Text.Length)
                TextBox2.SelectionStart = TextBox2.Text.Length
                j = 0
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Up Then
                pox = 0
                j = 1
                TextBox2.Text = (ListBox1.Items(k).ToString)
                k = k - 1
                TextBox2.Select(0, TextBox2.Text.Length)
                TextBox2.SelectionStart = TextBox2.Text.Length
                j = 0
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If j <> 1 Then
            hladaj(3, ListBox2, TextBox2)


            k = 0
        Else
            j = 0

        End If
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
            If k < 0 Then
                k = ListBox2.Items.Count - 1

            ElseIf k >= ListBox2.Items.Count Then
                k = 0
            End If

            If e.KeyCode = 40 Then
                pox = 0
                j = 1
                TextBox3.Text = (ListBox2.Items(k).ToString)
                k = k + 1
                TextBox3.Select(0, TextBox3.Text.Length)
                'TextBox3.SelectionStart = TextBox3.Text.Length
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Up Then
                pox = 0
                j = 1
                TextBox3.Text = (ListBox2.Items(k).ToString)
                k = k - 1
                TextBox3.Select(0, TextBox3.Text.Length)
                'TextBox3.SelectionStart = TextBox3.Text.Length

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        If pox = 1 Then
            TextBox2.Text = tex
        End If
        pox = 0
        hladaj(3, ListBox2, TextBox2)
        TextBox3.Text = ""
    End Sub
End Class