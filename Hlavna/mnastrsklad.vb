
Public Class nastrsklad
    Dim k, j As Integer
    Property nastr As String
    Property tex As String



    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sklad.v = 47
        DataGridView1.Hide()
        
        TextBox3.Text = "Tu zadaj cenu v €"
        'TODO: This line of code loads data into the 'RotekDataSet.Sklad' table. You can move, or remove it, as needed.
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        TextBox1.Text = "Tu zadaj počet"
        TextBox2.Text = "Tu zadaj č.regalu"
        hladaj(0, ListBox1, TextBox4)
        hladaj(1, ListBox2, TextBox5)
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
        Dim nastr2 As String
        sklad.v = 1
        nastr = TextBox4.Text
        nastr2 = TextBox5.Text
        If nastr = "" Then Exit Sub
        If nastr2 = "" Then Exit Sub
        sklad.v = v
        Dim p As Integer = 0
        Dim sku As Integer
        sklad.sku = sku
        Try
            p = TextBox1.Text

            sklad.p = p
            Dim reg As String = TextBox2.Text
            sklad.reg = reg


            Dim con As New OleDb.OleDbConnection
            Dim cesta As String
            cesta = Application.ExecutablePath
            cesta = Replace(cesta, "Rotek sklad.exe", "")
            cesta = Replace(cesta, "Rotek sklad.EXE", "")
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & cesta & "Rotek.mdb"
            con.Open()
            Dim cmd As New OleDb.OleDbCommand
            sklad.nastr = nastr
            sklad.nastr2 = nastr2
            sklad.sku = 1
            Try
                Dim a As Double
                Dim b As String
                a = TextBox3.Text
                If InStr(a, ",") <> 0 Then b = a & "€" Else b = a & ",00€"
                sklad.cenaa = b

            Catch ex As SystemException
                Dim b As String

                If InStr(TextBox3.Text, ".") <> 0 Then b = TextBox3.Text & "€" Else If TextBox3.Text = "Tu zadaj cenu v €" Or TextBox3.Text = "" Then b = "0.00€" Else b = TextBox3.Text
                sklad.cenaa = b
            End Try

            con.Close()

            Me.Close()
        Catch ex As SystemException
            MessageBox.Show(" Nezadal si počet")


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
            If k < 0 Then
                k = ListBox1.Items.Count - 1

            ElseIf k >= ListBox1.Items.Count Then
                k = 0
            End If


            If e.KeyCode = 40 Then
                j = 1
                TextBox4.Text = (ListBox1.Items(k).ToString)
                k = k + 1
                TextBox4.Select(0, TextBox2.Text.Length)
                TextBox4.SelectionStart = TextBox2.Text.Length
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Up Then

                j = 1
                TextBox4.Text = (ListBox1.Items(k).ToString)
                k = k - 1
                TextBox4.Select(0, TextBox2.Text.Length)
                TextBox4.SelectionStart = TextBox2.Text.Length
            ElseIf e.KeyCode = Keys.Back Then
                TextBox4.Text = tex.Substring(0, tex.Length - 1)


            End If

        Catch ex As Exception


        End Try

    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim tex As String
        j = 1
        tex = ListBox1.Text
        TextBox4.Text = tex
        TextBox4.Focus()
        TextBox4.Select(0, TextBox4.Text.Length)
        'TextBox4.SelectionStart = TextBox4.Text.Length

    End Sub
    Private Sub hladaj(ByVal stlpec As Integer, ByRef pom As ListBox, ByRef textar As TextBox)
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

        Dim ztabule, ztabulky As String
        Dim ztabule2 As String = ""

        Dim x As Integer = DataGridView1.RowCount - 1
        Dim xx As Integer = 1
        tex = UCase(textar.Text)
        Dim i As Integer = 0

        pom.Items.Clear()
        While i <= x

            Try

                ztabule = (DataGridView1.Rows(i).Cells(stlpec).Value)
                ztabulky = UCase(ztabule)
                Try
                    ztabule2 = UCase(DataGridView1.Rows(i - 1).Cells(stlpec).Value)
                Catch ex As SystemException
                End Try

                While (i - xx >= 0)
                    ztabule2 = UCase(DataGridView1.Rows(i - xx).Cells(stlpec).Value)
                    xx = xx + 1
                    If ztabulky = ztabule2 Then
                        xx = 1

                        Exit While
                    End If
                End While

                ' MessageBox.Show(ztabule & " " & ztabule2 & " " & ztabule.IndexOf(tex) & " " & ztabule <> ztabule2)
                If (ztabulky.IndexOf(tex) <> -1) And (ztabulky <> ztabule2) Then
                    pom.Items.Add(ztabule)
                    j = 1
                    textar.Text = ztabule
                End If
            Catch ex As Exception
            End Try
            i = i + 1
            xx = 1
        End While
        Dim a As Integer = tex.Length
        textar.Select(a, textar.Text.Length - a)

    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        If j <> 1 Then
            hladaj(0, ListBox1, TextBox4)
        Else : j = 0
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If j <> 1 Then
            hladaj(1, ListBox2, TextBox5)
        Else : j = 0
        End If
    End Sub

    Private Sub TextBox5_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyUp
        Try
            If k < 0 Then
                k = ListBox2.Items.Count - 1

            ElseIf k >= ListBox2.Items.Count Then
                k = 0
            End If


            If e.KeyCode = 40 Then
                j = 1
                TextBox5.Text = (ListBox2.Items(k).ToString)
                k = k + 1
                TextBox5.Select(0, TextBox5.Text.Length)
                TextBox5.SelectionStart = TextBox5.Text.Length
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Up Then

                j = 1
                TextBox5.Text = (ListBox2.Items(k).ToString)
                k = k - 1
                TextBox5.Select(0, TextBox5.Text.Length)
                TextBox5.SelectionStart = TextBox5.Text.Length
            ElseIf e.KeyCode = Keys.Back Then
                TextBox5.Text = tex.Substring(0, tex.Length - 1)


            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged

        j = 1
        TextBox5.Text = ListBox2.Text
        TextBox5.Focus()
        TextBox5.Select(0, TextBox5.Text.Length)
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class



