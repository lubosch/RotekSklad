Imports System.Data.SqlClient

Public Class msrotC
    Public tex As String
    Public k, j As Integer
    Public prvy As String
    Private menko As String
    Property crc As String

    Public Sub New(ByVal menko As String)
        InitializeComponent()
        Me.menko = menko
        Me.crc = menko
    End Sub


    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Sklad' table. You can move, or remove it, as needed.
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

        Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)

        mzamestnanec.bmp = 0
        k = 0
        DataGridView1.Hide()
        Dim x As Integer

        Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)
        x = DataGridView1.RowCount
        Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Firmy.pocetColumn, 1, RotekDataSet.Firmy.MenoColumn, crc)
        prvy = TextBox2.Text

        zamestnanec.bmp = 7
        TextBox1.Text = "Tu zadaj počet"
        hladaj(0, ListBox1, TextBox2)
        hladaj(1, ListBox2, TextBox3)
        hladaj(6, ListBox3, TextBox4)


    End Sub

    Public Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByRef textar As TextBox)
        Try

            pom.Items.Clear()
            If stlpec = 0 Then
                tex = TextBox4.Text
                Me.FirmyBindingSource.Sort = "Nástroj"
                Me.FirmyBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2}='{3}'", RotekDataSet.Firmy.NástrojColumn, TextBox2.Text, RotekDataSet.Firmy.MenoColumn, crc)
            ElseIf stlpec = 1 Then
                Me.FirmyBindingSource.Sort = "VelkostR"
                Me.FirmyBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}%' AND {4}='{5}'", RotekDataSet.Firmy.NástrojColumn, TextBox2.Text, RotekDataSet.Firmy.VelkostRColumn, TextBox3.Text, RotekDataSet.Firmy.MenoColumn, crc)
            ElseIf stlpec = 6 Then
                Me.FirmyBindingSource.Sort = "Vlastnost"
                Me.FirmyBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2}='{3}'", RotekDataSet.Firmy.VlastnostColumn, TextBox4.Text, RotekDataSet.Firmy.MenoColumn, crc)
            End If

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
                Dim stlpc() As Integer = {0, 1}

                listbs.Add(ListBox1)
                listbs.Add(ListBox2)

                aree.Add("Nástroj")
                aree.Add("VelkostR")

                Hpridat.hladaj3(listbs, aree, stlpc, DataGridView1, FirmyBindingSource)
            End If
        Catch ex As Exception
            '  Chyby.Show(ex.Message)
        End Try

        '  Chyby.Show(tex & " " & tex.Length & " " & textar.Text)
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
        Dim nastroj As String = TextBox2.Text
        Dim nastroj2 As String = TextBox3.Text
        Dim vlast As String = TextBox4.Text
        If nastroj.Length = 0 Then
            Chyby.Show("Nezadal si nástroj")
            Exit Sub
        End If
        If nastroj2.Length = 0 Then
            Chyby.Show("Nezadal si priemer")
            Exit Sub
        End If
        If vlast.Length = 0 Then
            Chyby.Show("Nezadal si vlastnosť")
            Exit Sub
        End If
        Dim spoluu As Integer


        Try
            Dim pocet As Integer
            Try
                pocet = 0 - TextBox1.Text
            Catch ex As Exception
                Chyby.Show("Nezadal si pocet")
            End Try

            Dim xxx As Integer = 0
            Dim a As Integer

            Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Firmy.MenoColumn, menko, RotekDataSet.Firmy.pocetColumn, 0)
            spoluu = DataGridView1.Rows(0).Cells(4).Value


            Dim Sql As String
            Dim con As New SqlConnection
            Dim cesta As String
            cesta = "\\192.168.1.140\admin\Sklad\"
            cesta = Replace(cesta, "Rotek sklad.exe", "")
            cesta = Replace(cesta, "Rotek sklad.EXE", "")
            con.ConnectionString = My.Settings.Rotek2

            Dim cmd As New SqlCommand

            Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.Sklad.NastrojColumn, nastroj, RotekDataSet.Sklad.VelkostSColumn, nastroj2, RotekDataSet.Sklad.VlastnostColumn, vlast)
            If DataGridView2.RowCount = 0 Then
                Chyby.Show("Nenašiel sa taký nástroj")
                Exit Sub
            End If

            con.Open()


            Dim cenaa As String
            cenaa = DataGridView2.Rows(0).Cells(2).Value
            a = DataGridView2.Rows(0).Cells(3).Value - pocet


            'Sql = "Insert INTO Sklad (Nastroj, Pocet, Regal, Cena, VelkostS) VALUES ('" + Nastroj + "', '" & a & "', '" + rega + "', '" + cenaa + "', '" + Nastroj2 + "')"
            Dim fsd As Integer = 1

            Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}' AND {6} = '{7}' AND {8}='{9}'", RotekDataSet.Firmy.MenoColumn, menko, RotekDataSet.Firmy.NástrojColumn, nastroj, RotekDataSet.Firmy.VelkostRColumn, nastroj2, RotekDataSet.Firmy.VlastnostColumn, vlast, RotekDataSet.Firmy.pocetColumn, 1)

            If DataGridView1.RowCount = 0 Then
                Chyby.Show("Nemá nič také požičané")
                Exit Sub

                'Sql = "Insert INTO Firmy (Meno, Nástroj, pocet, Kolko, VelkostR, Cena, Vlastnost) VALUES ('" + menko + "', '" + nastroj + "', '" & 1 & "', '" & Pocett & "', '" + nastroj2 + "', '" & cenaa & "','" + vlast + "')"
                'cmd = New SqlCommand(Sql, con)
                'cmd.ExecuteNonQuery()

                'spoluu = spoluu + pocet
                'Sql = "UPDATE Firmy SET Spolu='" & spoluu & "' WHERE Meno='" & menko & "' AND pocet=0 "
                'cmd = New SqlCommand(Sql, con)
                'cmd.ExecuteNonQuery()
                'con.Close()
                'Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)
                'Me.FirmyBindingSource.Filter = Nothing
                'Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Firmy.pocetColumn, 1, RotekDataSet.Firmy.MenoColumn, menko)
            Else
                Sql = "UPDATE Sklad SET Pocet='" & a & "' WHERE Nastroj='" & nastroj & "' AND VelkostS='" & nastroj2 & "' AND Vlastnost='" + vlast + "'"
                cmd = New SqlCommand(Sql, con)
                cmd.ExecuteNonQuery()

                Dim b As Integer
                b = DataGridView1.Rows(0).Cells(3).Value()

                Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Firmy.MenoColumn, menko, RotekDataSet.Firmy.pocetColumn, 0)
                spoluu = spoluu + pocet
                b = b + pocet

                Dim nula As Integer = 0
                Sql = "UPDATE Firmy SET Spolu='" & spoluu & "' WHERE Meno='" & menko & "' AND pocet=0 "
                cmd = New SqlCommand(Sql, con)
                cmd.ExecuteNonQuery()
                If b = 0 Then
                    Sql = "DELETE FROM Firmy WHERE Meno ='" + menko + "' AND Nástroj ='" & nastroj & "' AND VelkostR ='" & nastroj2 & "' AND Vlastnost='" + vlast + "' AND pocet=1"
                Else
                    Sql = "UPDATE Firmy SET Kolko='" & b & "' WHERE Meno ='" + menko + "' AND Nástroj ='" & nastroj & "' AND VelkostR ='" & nastroj2 & "' AND Vlastnost='" + vlast + "' AND pocet=1"

                End If
                cmd = New SqlCommand(Sql, con)
                cmd.ExecuteNonQuery()

                '                    If b > 0 Then cmd.ExecuteNonQuery() Else Chyby.Show("Vrátil všetky " & Nastroj & " " & Nastroj2)
                con.Close()
                Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)
                Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""

                hladaj(0, ListBox1, TextBox2)
                hladaj(1, ListBox2, TextBox3)
                hladaj(6, ListBox3, TextBox4)
                TextBox2.Focus()

            End If

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then kliky()
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub listbox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try

            Dim tex As String
            j = 1
            tex = ListBox1.Text
            TextBox2.Text = tex
            TextBox2.Focus()
            TextBox2.Select(0, TextBox2.Text.Length)

        Catch ex As Exception

        End Try
        ' TextBox2.SelectionStart = TextBox2.Text.Length
    End Sub

    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp

        Try
            Hpridat.stlac(k, sender, ListBox1, e)

            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(0, ListBox1, TextBox2)
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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
                hladaj(1, ListBox2, TextBox3)
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        'hladaj(1, ListBox2, TextBox2)
        '     TextBox3.Text = ""
    End Sub

    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox3, e)

            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(6, ListBox3, TextBox4)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        Try
            Dim tex As String
            j = 1
            tex = ListBox3.Text
            TextBox4.Text = tex
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox3_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox3.Enter
        hladaj(1, ListBox2, TextBox3)

    End Sub

    Private Sub TextBox2_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox2.Enter
        'hladaj(0, ListBox1, TextBox2)

    End Sub
End Class