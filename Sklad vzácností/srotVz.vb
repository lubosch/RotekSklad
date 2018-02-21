Imports System.Data.SqlClient

Public Class srotVz
    Public tex As String
    Public k, j, pox As Integer
    Public prvy As String

    Property crc As String

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)

        zamestnanec.bmp = 0

        k = 0
        DataGridView1.Hide()
        Dim x As Integer

        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        x = DataGridView1.RowCount



        prvy = TextBox2.Text


        zamestnanec.bmp = 7
        TextBox1.Text = "Tu zadaj počet"
        hladaj(0, ListBox1, TextBox2)
        hladaj(1, ListBox2, TextBox3)
        pox = 0
        tex = ""
    End Sub
    Private Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByRef textar As TextBox)
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        Me.VzacnostiBindingSource.Filter = Nothing

        Dim radky As Integer = DataGridView1.RowCount - 1
        Dim slovo As String = UCase(textar.Text)
        Dim stlpec2 As Integer
        Dim pom2 As ListBox
        If stlpec = 0 Then
            stlpec2 = 1
            pom2 = ListBox2
        Else
            stlpec2 = 0
            pom2 = ListBox1
        End If
        Dim i As Integer
        Dim tab, tab2 As String
        For i = 0 To radky
            tab2 = DataGridView1.Rows(i).Cells(stlpec).Value

            tab = UCase(tab2)
            If slovo.Length = 0 Then
                pom.Items.Add(tab2)
                pom2.Items.Add(DataGridView1.Rows(i).Cells(stlpec2).Value)
            Else
                If tab.IndexOf(slovo) = 0 Then
                    pom.Items.Add(DataGridView1.Rows(i).Cells(stlpec).Value)
                    pom2.Items.Add(DataGridView1.Rows(i).Cells(stlpec2).Value)
                End If
            End If
        Next

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

        Dim nazov As String = TextBox3.Text
        If nazov.Length = 0 Then
            Chyby.Show("Názov nie je zadaný")
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
            Me.VzacnostiBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Vzacnosti.NazovColumn, nazov)

            Dim kolko As String = (DataGridView1.Rows(0).Cells(4).Value)
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
                Dim srot As Integer
                Try
                    srot = (DataGridView1.Rows(0).Cells(6).Value)
                Catch ex As Exception
                    srot = 0
                End Try
                srot = srot + pocet
                Dim srotcena As Double
                Try
                    srotcena = (DataGridView1.Rows(0).Cells(7).Value)
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
                Sql = "UPDATE Vzacnosti SET Pocet='" & kolko & "', Znicenych='" & srot & "', znCena='" & srotcena & "' WHERE Nazov='" & nazov & "'"
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
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
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
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj(1, ListBox2, TextBox3)
                k = 0
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        Me.VzacnostiBindingSource.Filter = Nothing
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        Me.VzacnostiBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Vzacnosti.CisloColumn, TextBox2.Text)

        If DataGridView1.RowCount = 1 Then TextBox3.Text = DataGridView1.Rows(0).Cells(1).Value
        Me.VzacnostiBindingSource.Filter = Nothing
    End Sub

    Private Sub TextBox3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.Leave
        Me.VzacnostiBindingSource.Filter = Nothing
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
        Me.VzacnostiBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Vzacnosti.NazovColumn, TextBox3.Text)
        If DataGridView1.RowCount = 1 Then TextBox2.Text = DataGridView1.Rows(0).Cells(0).Value
        Me.VzacnostiBindingSource.Filter = Nothing
    End Sub

    Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
        Me.VzacnostiBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Vzacnosti.NazovColumn, TextBox3.Text)
        Dim x As Integer = DataGridView1.RowCount
        If x = 0 Then
            Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
            Me.VzacnostiBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Vzacnosti.CisloColumn, TextBox2.Text)
            x = DataGridView1.RowCount
            If x = 1 Then TextBox3.Text = DataGridView1.Rows(0).Cells(1).Value
        ElseIf x = 1 Then
            TextBox2.Text = DataGridView1.Rows(0).Cells(0).Value
        End If
        Me.VzacnostiTableAdapter.Fill(Me.RotekDataSet.Vzacnosti)
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class