
Public Class Hvratit
    Dim k, j As Integer
    Property nastr As String
    Property tex As String
    Property menko As String
    Dim crc, menoo, priezviskoo As String



    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        DataGridView2.Hide()
        Me.HutaBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Huta.pocetColumn, 0)

        TextBox1.Text = "Tu zadaj počet"

        hladaj(0, ListBox1, TextBox4)
        hladaj(1, ListBox2, TextBox5)

        hladaj(4, ListBox4, TextBox7)
        meno()
        Me.HutaBindingSource.Sort = "Druh"
        j = 1
        TextBox4.Text = ""
        hForm78.v = 1
    End Sub
    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = "1"
        TextBox1.SelectAll()

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        hForm78.v = 1
        Me.Close()
    End Sub
    Public Sub stuk()
        Dim druh, nazov, rozmer As String
        druh = TextBox4.Text
        nazov = TextBox6.Text
        rozmer = TextBox7.Text
        If druh.Length = 0 Or nazov.Length = 0 Or rozmer.Length = 0 Then
            MessageBox.Show("Niečo nie je zadané")
            Exit Sub
        End If
        Dim hustota As Double
        Dim velkost As Double
        Try
            velkost = TextBox1.Text
          
            Dim con As New OleDb.OleDbConnection
            Dim sql As String
            Dim cesta As String
            cesta = "\\192.168.1.140\admin\Sklad\"
            cesta = Replace(cesta, "Rotek sklad.exe", "")
            cesta = Replace(cesta, "Rotek sklad.EXE", "")
            con.ConnectionString = my.Settings.Rotek2      '"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & cesta & "Rotek.mdb"
            con.Open()

            Me.HutaBindingSource.Filter = Nothing
            Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
            Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}='{7}'", RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.pocetColumn, 0)
            Dim cmd As New OleDb.OleDbCommand
            If DataGridView2.RowCount = 1 Then
                Dim pocett As Integer = DataGridView2.Rows(0).Cells(5).Value
                Dim cena As Double = CDbl(DataGridView2.Rows(0).Cells(6).Value)
                rozmer = DataGridView2.Rows(0).Cells(4).Value
                hustota = DataGridView2.Rows(0).Cells(3).Value
                Dim srot As Double = DataGridView2.Rows(0).Cells(7).Value
                Dim srotcena As Double = DataGridView2.Rows(0).Cells(8).Value
                Dim objem As Double
                If rozmer.IndexOf("x") <> -1 Then
                    Dim pos As Integer = rozmer.IndexOf("x")
                    Dim dlz As Integer = rozmer.Length - pos - 1
                    Dim sirka As Double = CDbl(rozmer.Substring(pos + 1, dlz)) / 1000
                    Dim dlzka As Double = CDbl(rozmer.Substring(0, pos)) / 1000
                    objem = sirka * dlzka * velkost / 1000
                End If
                If rozmer.IndexOf("r") <> -1 Then
                    Dim pos As Integer = rozmer.IndexOf("r")
                    Dim dlzka As Double = CDbl(rozmer.Substring(0, pos)) / 1000
                    objem = 3.14159265*dlzka*dlzka * velkost / 1000
                End If
                If rozmer.IndexOf("d") <> -1 Then
                    Dim pos As Integer = rozmer.IndexOf("d")
                    Dim dlzka As Double = CDbl(rozmer.Substring(0, pos)) / 2000
                    objem = 3.14159265 * dlzka * dlzka * velkost / 1000
                End If
                MessageBox.Show(Format(objem, "N2") & " " & hustota)
                Dim hmota As Double = objem * hustota
                srot = srot + hmota
                srotcena = srotcena + hmota * cena
                dim velkostvoer As Integer = velkost
                velkost = pocett - velkost

                If velkost < 0 Then
                    Dim aks As String
                    Dim ifg As Integer
                    aks = "Na sklade je už len: " & pocett & ". Aj tak vziať " & velkostvoer & "?"
                    ifg = MsgBox(aks, vbExclamation + vbYesNo, "Overenie")
                    If ifg = vbYes Then velkost = 0 Else Exit Sub
                End If

                sql = "UPDATE Huta SET Velkost='" & velkost & "', srot='" & srot & "', srotcena='" & srotcena & "' WHERE Druh='" + druh + "'  AND Nazov='" + nazov + "' AND Rozmer='" + rozmer + "' AND pocet='0'"
                cmd = New OleDb.OleDbCommand(sql, con)
                cmd.ExecuteNonQuery()
                cmd.ExecuteNonQuery()

            Else
                MessageBox.Show("Už nie je na sklade")
            End If
            con.Close()
            Me.Close()
        Catch ex As SystemException
            MessageBox.Show(ex.Message & " Nezadal si počet")
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        stuk()
       
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then stuk()
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then stuk()
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then stuk()
    End Sub
    Private Sub nastrsklad_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
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

            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Up Then

                j = 1
                TextBox4.Text = (ListBox1.Items(k).ToString)
                k = k - 1

            ElseIf e.KeyCode = Keys.Back Then
                If TextBox4.Text.Length <> tex.Length Then TextBox4.Text = tex.Substring(0, tex.Length - 1)
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Me.HutaBindingSource.Sort = "Druh"
                hladaj(0, ListBox1, TextBox4)
                Dim a As Integer = tex.Length
                TextBox4.Select(a, TextBox4.Text.Length - a)
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
    

    Sub meno()
        Me.HutaBindingSource.Sort = "Nazov"

        Dim slovo As String = TextBox6.Text

        Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' ", RotekDataSet.Huta.pocetColumn, 0)
        crc = ""
        ListBox3.Items.Clear()
        Dim x As Integer = DataGridView2.RowCount - 1
        Dim nad As String = ""

        For i As Integer = 0 To x
            Dim pom As String = DataGridView2.Rows(i).Cells(2).Value
            Dim pom2 As String = UCase(pom)
            If (pom2.IndexOf(UCase(slovo)) = 0) And (pom2 <> nad) Then
                ListBox3.Items.Add(pom)
                nad = pom2
            Else
            End If
        Next
        Me.HutaBindingSource.Sort = "Druh"

    End Sub

    Private Sub TextBox6_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        Try
            If k < 0 Then
                k = ListBox3.Items.Count - 1

            ElseIf k >= ListBox3.Items.Count Then
                k = 0
            End If
            If e.KeyCode = 40 Then
                j = 1
                TextBox6.Text = (ListBox3.Items(k).ToString)
                k = k + 1
                TextBox6.Select(0, TextBox6.Text.Length)
                TextBox6.SelectionStart = TextBox6.Text.Length
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Up Then

                j = 1
                TextBox6.Text = (ListBox3.Items(k).ToString)
                k = k - 1
                TextBox6.Select(0, TextBox6.Text.Length)
                TextBox6.SelectionStart = TextBox6.Text.Length
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                meno()
                TextBox6.Select(TextBox6.Text.Length, TextBox6.Text.Length)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        j = 1
        TextBox6.Text = ListBox3.Text
        TextBox6.Focus()
        TextBox6.Select(0, TextBox6.Text.Length)
    End Sub
    Private Sub TextBox4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Leave
        TextBox5.Text = ""
        hladaj(3, ListBox2, TextBox4)
    End Sub
    Private Sub TextBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Enter
        Me.Text = ""
        meno()
        hladaj(0, ListBox1, TextBox4)
    End Sub

    Private Sub TextBox6_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.Enter
        Dim pom, pom2 As String
        pom = TextBox4.Text
        pom2 = TextBox5.Text

        Me.HutaBindingSource.Filter = Nothing
        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' ", RotekDataSet.Huta.DruhColumn, pom, RotekDataSet.Huta.pocetColumn, 0)
        Dim x As Integer = DataGridView2.RowCount
        If x = 1 Then
            menoo = DataGridView2.Rows(0).Cells(2).Value
            TextBox6.Text = menoo
        End If
        Me.HutaBindingSource.Filter = Nothing
        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.HutaBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Huta.pocetColumn, 0)

    End Sub

    Private Sub TextBox6_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.Leave
        Dim pom As String
        pom = TextBox6.Text

        Me.HutaBindingSource.Filter = Nothing
        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.NazovColumn, pom, RotekDataSet.Huta.pocetColumn, 0)
        Dim x As Integer = DataGridView2.RowCount
        If x = 1 Then
            menoo = DataGridView2.Rows(0).Cells(0).Value
            TextBox4.Text = menoo
            menoo = DataGridView2.Rows(0).Cells(1).Value
            TextBox5.Text = menoo
        End If
        Me.HutaBindingSource.Filter = Nothing
        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.HutaBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Huta.pocetColumn, 0)

    End Sub

    Private Sub TextBox7_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyUp
        Try
            If k < 0 Then
                k = ListBox4.Items.Count - 1

            ElseIf k >= ListBox4.Items.Count Then
                k = 0
            End If
            If e.KeyCode = 40 Then
                j = 1
                TextBox7.Text = (ListBox4.Items(k).ToString)
                k = k + 1
                TextBox7.Select(0, TextBox7.Text.Length)
                TextBox7.SelectionStart = TextBox7.Text.Length
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Up Then
                j = 1
                TextBox7.Text = (ListBox4.Items(k).ToString)
                k = k - 1
                TextBox7.Select(0, TextBox7.Text.Length)
                TextBox7.SelectionStart = TextBox7.Text.Length
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Me.HutaBindingSource.Sort = "Rozmer"
                hladaj(4, ListBox4, TextBox7)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ListBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox4.SelectedIndexChanged
        j = 1
        TextBox7.Text = ListBox4.Text
        TextBox7.Focus()
        TextBox7.Select(0, TextBox7.Text.Length)
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox7_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.Enter

        Me.HutaBindingSource.Filter = Nothing
        Dim nazov As String
        nazov = TextBox6.Text
        Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.NazovColumn, nazov)
        Me.HutaBindingSource.Sort = "Rozmer"
        hladaj(4, ListBox4, TextBox7)
    End Sub

    Private Sub TextBox7_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.Leave
        Me.HutaBindingSource.Filter = Nothing
        Me.HutaBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Huta.pocetColumn, 0)

    End Sub
End Class



