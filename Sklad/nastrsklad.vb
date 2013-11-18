Imports System.Threading
Imports System.Data.SqlClient

Public Class nastrsklad
    Dim k, j As Integer
    Property nastr As String
    'Property tex As String
    Private lockThis As New Object
    Private x As New Object
    Dim napln As Thread


    Public Sub New(ByVal nastr As String, ByVal nastr2 As String, ByVal vlast As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        TextBox4.Text = nastr
        TextBox5.Text = nastr2
        TextBox6.Text = vlast

    End Sub
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'tex = ""
        Try

            Dim napln As Thread
            napln = New Thread(Sub() Invoke(Sub() fill()))
            napln.IsBackground = True
            napln.Start()
            TextBox4.Focus()

            'fill()

            '        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)



            '    hladaj_thread(0, ListBox1, TextBox4)
            '    hladaj_thread(1, ListBox2, TextBox5)
            'SyncLock lockThis
            '    hladaj_thread(3, ListBox3, TextBox6)
            'End SyncLock

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub
    Private Sub fill()

        SyncLock lockThis

            Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        End SyncLock

    End Sub



    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = "1"
        TextBox1.SelectAll()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        sklad.v = 1
        Me.Close()
    End Sub

    Public Sub stuk()
        Dim v As Integer = 2
        Dim nastr2, Vlast As String
        sklad.v = 1
        nastr = TextBox4.Text
        nastr2 = TextBox5.Text
        Vlast = TextBox6.Text
        If Vlast.Length = 0 Then Vlast = "HSS"
        If nastr.Length = 0 Or nastr2.Length = 0 Then
            Chyby.Show("Niečo nie je zadané")
            Exit Sub
        End If

        If nastr = "" Then Exit Sub
        If nastr2 = "" Then Exit Sub

        Dim p As Integer = 0
        Dim sku As Integer
        sklad.sku = sku
        Try
            p = TextBox1.Text
            If (p < 0) And (Form78.heslo <> Form78.admin) Then
                Chyby.Show("Nemáš práva na len tak odstraňovanie zo skladu ;) Popýtaj administrátora o heslo")
                Exit Sub
            End If
            sklad.v = v
            sklad.p = p
            Dim reg As String = TextBox2.Text
            sklad.reg = reg


            Dim con As New SqlConnection
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            Dim cmd As New SqlCommand
            sklad.nastr = nastr
            sklad.nastr2 = nastr2
            sklad.vlast = Vlast

            sklad.sku = 1
            Dim a As Double
            Dim cenaa As String
            Try
                a = TextBox3.Text
                If InStr(a, ",") <> 0 Then cenaa = a & "€" Else cenaa = a & ",00€"
                sklad.cenaa = cenaa

            Catch ex As SystemException

                If InStr(TextBox3.Text, ".") <> 0 Then cenaa = TextBox3.Text & "€" Else If TextBox3.Text = "Tu zadaj cenu v €" Or TextBox3.Text = "" Then cenaa = "0.00€" Else cenaa = TextBox3.Text
                sklad.cenaa = cenaa
            End Try


            Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.Sklad.NastrojColumn, nastr, RotekDataSet.Sklad.VelkostSColumn, nastr2, RotekDataSet.Sklad.VlastnostColumn, Vlast)
            Dim Sql As String
            zamestnanec.doexcel("Sklad", nastr, nastr2, Vlast, p.ToString)

            If DataGridView1.RowCount = 0 Then
                Sql = "Insert INTO Sklad (Nastroj, Pocet, Regal, Cena, VelkostS, Vlastnost) VALUES ('" + nastr + "', '" & p & "', '" + reg + "', '" & cenaa & "', '" + nastr2 + "', '" & Vlast & "')"
                cmd = New SqlCommand(Sql, con)
                cmd.ExecuteNonQuery()
            Else
                Dim rega As String
                a = DataGridView1.Rows(0).Cells(4).Value
                a = a + p
                rega = DataGridView1.Rows(0).Cells(5).Value
                Dim cenka As String
                cenka = DataGridView1.Rows(0).Cells(6).Value

                If cenka.Length = 0 Then cenka = "0,00€"
                If reg.Length <> 0 Then rega = reg
                If cenaa = "0.00€" Then cenaa = cenka

                Sql = "UPDATE Sklad SET Pocet='" & a & "', Regal='" + rega + "', Cena='" + cenaa + "' WHERE VelkostS='" + nastr2 + "' AND Nastroj='" + nastr + "' AND Vlastnost='" + Vlast + "'"
                cmd = New SqlCommand(Sql, con)
                cmd.ExecuteNonQuery()

            End If


            con.Close()

            Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox6.Text = ""
            TextBox5.Text = ""

            hladaj_thread(1, ListBox2, TextBox5)
            hladaj_thread(2, ListBox3, TextBox6)

            TextBox4.Focus()
            hladaj_thread(0, ListBox1, TextBox4)

        Catch ex As SystemException
            Chyby.Show(ex.ToString)


        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        stuk()

    End Sub

    Private Sub TextBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Click
        TextBox2.Text = ""
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then stuk()
    End Sub

    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then stuk()
    End Sub

    Private Sub TextBox3_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox3.MouseClick
        TextBox3.Text = ""
    End Sub

    Private Sub TextBox3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then stuk()
    End Sub

    Private Sub nastrsklad_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub ComboBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        If e.KeyCode = Keys.Escape Then Me.Close()


    End Sub
    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp, TextBox4.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox1, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Back Then
                If ListBox1.Items.Count > 0 Then
                    'If TextBox4.Text.Length = tex.Length Then
                    '    TextBox4.Text = tex.Substring(0, tex.Length - 1)
                    'Else
                    '    tex = TextBox4.Text
                    'End If

                End If

            End If

            'hladaj_thread(0, ListBox1, TextBox4)
            If (((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95))) Or e.KeyCode = 8 Then

                hladaj_thread(0, ListBox1, TextBox4)
            End If

        Catch ex As Exception


        End Try

    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try

            '  Dim tex As String
            j = 1
            ' tex = ListBox1.SelectedItems(0)
            'TextBox4.Text = tex
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)
            'TextBox4.SelectionStart = TextBox4.Text.Length
            '  hladaj(0, ListBox1, TextBox4)

        Catch ex As Exception
            '   Chyby.Show(ex.ToString)
        End Try
    End Sub
    Public Sub hladaj(ByVal stlpec As Integer, ByVal pom As ListBox, ByVal textar As TextBox)
        'SyncLock x
        '   MessageBox.Show("zacate")

        Try
            'Dim a1 As Integer
            'If textar = Nothing Then
            '    a1 = 0
            'Else
            '    a1 = textar.Text.Length
            'End If

            If stlpec = 0 Then
                'tex = TextBox4.Text
                Me.SkladBindingSource.Sort = "Nastroj"
                '    Me.SkladBindingSource.Filter = Nothing
                Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%'", RotekDataSet.Sklad.NastrojColumn, TextBox4.Text)
                '          MessageBox.Show("*" & TextBox4.Text & "*")
                '         Chyby.Show(TextBox4.Text)
            ElseIf stlpec = 1 Then
                Me.SkladBindingSource.Sort = "VelkostS"
                Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}%'", RotekDataSet.Sklad.NastrojColumn, TextBox4.Text, RotekDataSet.Sklad.VelkostSColumn, TextBox5.Text)
            ElseIf stlpec = 2 Then
                Exit Sub
            ElseIf stlpec = 3 Then
                stlpec = 2
                Me.SkladBindingSource.Sort = "Vlastnost"
                Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%'", RotekDataSet.Sklad.VelkostSColumn, TextBox6.Text)

            End If

            pom.Items.Clear()
            Dim slovo As String = ""
            For i As Integer = 0 To DataGridView1.RowCount - 1
                If IsDBNull(DataGridView1.Rows(i).Cells(stlpec).Value) = False AndAlso DataGridView1.Rows(i).Cells(stlpec).Value <> slovo Then
                    pom.Items.Add(DataGridView1.Rows(i).Cells(stlpec).Value)
                    slovo = DataGridView1.Rows(i).Cells(stlpec).Value
                End If
                'If TextBox4.Text.Length <> a1 Then
                '    Exit Sub
                'End If
            Next

            If stlpec = 0 And pom.Items.Count <> 0 Then
                'textar.Text = pom.Items(0)
                'textar.SelectionLength = textar.Text.Length
                'textar.SelectionStart = tex.Length
            End If

            If pom.Items.Count = 1 And stlpec <> 2 Then
                Dim listbs As List(Of ListBox) = New List(Of ListBox)
                Dim aree As List(Of String) = New List(Of String)
                Dim stlpc() As Integer = {0, 1}

                listbs.Add(ListBox1)
                listbs.Add(ListBox2)

                aree.Add("Nastroj")
                aree.Add("VelkostS")

                Hpridat.hladaj3(listbs, aree, stlpc, DataGridView1, SkladBindingSource)
            End If

        Catch ex As Exception
            Chyby.Show(ex.Message)
        End Try
        'End SyncLock
        'MessageBox.Show("skoncene")

    End Sub


    Public Sub hladaj_thread(ByVal stlpec As Integer, ByVal pom As ListBox, ByVal textar As TextBox)
        '   SyncLock lockThis

        Try
            ' GC.Collect()

            'If (pom.IsDisposed = False AndAlso textar.IsDisposed = False) Then
            napln = New Thread(Sub() pom.Invoke(Sub() hladaj(stlpec, pom, textar)))
            napln.IsBackground = True
            napln.Start()
            'End If
        Catch ex As Exception
        End Try
        'End SyncLock
    End Sub

    Private Sub TextBox5_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox2, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj_thread(1, ListBox2, TextBox5)
            End If

        Catch ex As Exception


        End Try
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

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox6_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox3, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj_thread(2, ListBox3, TextBox6)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        Try

            j = 1
            TextBox6.Text = ListBox3.Text
            TextBox6.Focus()
            TextBox6.Select(0, TextBox6.Text.Length)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox6_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox6.Enter
        hladaj_thread(3, ListBox3, TextBox6)
    End Sub

    Private Sub TextBox5_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox5.Enter
        hladaj_thread(1, ListBox2, TextBox5)

    End Sub

    Private Sub TextBox4_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox4.Enter
        'hladaj(0, ListBox1, TextBox4)

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim regal As String = TextBox2.Text
        If regal.Length = 0 Then
            Chyby.Show("Nezadaný regál")
            Exit Sub
        End If

        Panel1.Show()
        Try

            Me.SkladBindingSource.Sort = "Nastroj, VelkostS"
            Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%'", RotekDataSet.Sklad.NastrojColumn, TextBox4.Text, RotekDataSet.Sklad.VelkostSColumn, TextBox5.Text, RotekDataSet.Sklad.VlastnostColumn, TextBox6.Text)

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Dim regal As String = TextBox2.Text

        Dim con As New SqlConnection
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Dim cmd As New SqlCommand
        Dim sql As String
        Try

            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Cells(3).Value = 1 Then
                    sql = "UPDATE Sklad SET Regal='" & regal & "' WHERE Nastroj='" & DataGridView1.Rows(i).Cells(0).Value & "' AND VelkostS='" & DataGridView1.Rows(i).Cells(1).Value & "' AND Vlastnost='" & DataGridView1.Rows(i).Cells(2).Value & "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End If
            Next

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

        con.Close()
        Panel1.Hide()

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        For i As Integer = 0 To DataGridView1.RowCount - 1
            DataGridView1.Rows(i).Cells(3).Value = 1
        Next
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        For i As Integer = 0 To DataGridView1.RowCount - 1
            DataGridView1.Rows(i).Cells(3).Value = 0
        Next
    End Sub

    Private Sub nastrsklad_Click(sender As System.Object, e As System.EventArgs) Handles MyBase.Click
        If (Panel1.Visible) And ((Cursor.Position.X < Panel1.Location.X) Or (Cursor.Position.X > (Panel1.Location.X + Panel1.Size.Width)) Or (Cursor.Position.Y < Panel1.Location.Y) Or (Cursor.Position.Y > (Panel1.Location.Y + Panel1.Size.Height))) Then
            Panel1.Hide()
        End If
    End Sub


    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 3 And e.RowIndex >= 0 Then
            DataGridView1.Rows(e.RowIndex).Cells(3).Value = 1 - DataGridView1.Rows(e.RowIndex).Cells(3).Value
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text.IndexOf(".") > -1 Then
            TextBox5.Text = TextBox5.Text.Replace(".", ",")
            TextBox5.Select(TextBox5.Text.Length, 0)


        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedCells.Count > 1 Then
            For i As Integer = 0 To DataGridView1.SelectedCells.Count - 1
                DataGridView1.Rows(DataGridView1.SelectedCells(i).RowIndex).Cells(3).Value = 1 - DataGridView1.Rows(DataGridView1.SelectedCells(i).RowIndex).Cells(3).Value

            Next

        End If
    End Sub


    Private Sub nastrsklad_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub
End Class



