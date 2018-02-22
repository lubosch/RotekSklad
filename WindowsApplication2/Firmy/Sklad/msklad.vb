Imports System.Data.SqlClient

Public Class msklad

    Shared Property nastr As String

    Shared Property p As Integer

    Public x As Integer

    Public over As Integer

    Public tex, tex2 As String

    Shared Property sku As Integer

    Shared Property reg As String

    Shared Property v As Integer

    Shared Property cenaa As String

    Shared Property nastr2 As String
    Public Sub rozmer()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2, sw - 280)

        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString("Sklad", Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New Point(rw, 20)
    End Sub
    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        TextBox2.Text = "Zadajte názov nástroja, ktorý chcete nájsť"
        Button3.Hide()
        TextBox1.Text = "Tu zadaj menej ako: "

        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        Me.SkladBindingSource.Sort = "Nastroj"
        x = DataGridView1.RowCount
        rozmer()







    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        v = 2
        While v = 2
            nastr = "fd"
            Dim f As New nastrsklad
            Dim rega As String
            f.TopLevel = True

            f.Dock = DockStyle.None
            f.nastr = nastr
            Dim a As Integer
            f.ShowDialog()
            If v = 2 Then

                Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Sklad.NastrojColumn, nastr, RotekDataSet.Sklad.VelkostSColumn, nastr2)
                Dim Sql As String
                Try

                    a = DataGridView1.Rows(0).Cells(3).Value
                    a = a + p
                    rega = DataGridView1.Rows(0).Cells(6).Value
                    Dim cenka As String

                    cenka = DataGridView1.Rows(0).Cells(2).Value
                    If cenka = "" Then cenka = "0,00€"
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
                    com.CommandText = "DELETE FROM Sklad WHERE Nastroj ='" & nastr & "' AND VelkostS ='" & nastr2 & "'"
                    com.ExecuteNonQuery()
                    If reg <> "Tu zadaj č.regalu" And reg <> "" Then rega = reg
                    If cenaa = "0.00€" Then cenaa = cenka
                    Sql = "Insert INTO Sklad (Nastroj, Pocet, Regal, Cena, VelkostS) VALUES ('" + nastr + "', '" & a & "', '" + rega + "', '" & cenaa & "', '" & nastr2 & "')"


                    cmd = New SqlCommand(Sql, con)
                    cmd.ExecuteNonQuery()

                    con.Close()

                    Me.SkladBindingSource.Filter = Nothing

                Catch ex As SystemException
                    '  System.Windows.Forms.Chyby.Show(ex.Message)
                    Dim con As New SqlConnection
                    Dim cesta As String
                    cesta = "\\192.168.1.140\admin\Sklad\"
                    cesta = Replace(cesta, "Rotek sklad.exe", "")
                    cesta = Replace(cesta, "Rotek sklad.EXE", "")
                    con.ConnectionString = My.Settings.Rotek2
                    Dim cmd As SqlCommand
                    con.Open()
                    If reg <> "Tu zadaj č.regalu" Then rega = reg Else rega = ""

                    Sql = "Insert INTO Sklad (Nastroj, Pocet, Regal, Cena, VelkostS) VALUES ('" + nastr + "', '" & p & "', '" + rega + "', '" & cenaa & "', '" + nastr2 + "')"

                    cmd = New SqlCommand(Sql, con)
                    cmd.ExecuteNonQuery()

                    con.Close()
                    Me.SkladBindingSource.Filter = Nothing
                End Try
            End If
            Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

        End While
        x = DataGridView1.RowCount
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Button2.Hide()
        Button3.Show()
        Try
            Dim a As Integer
            a = TextBox1.Text
            Me.SkladBindingSource.Filter = String.Format("{0} < '{1}'", RotekDataSet.Sklad.PocetColumn, a)


        Catch ex As SystemException
            Me.SkladBindingSource.Filter = String.Format("{0} < '{1}'", RotekDataSet.Sklad.PocetColumn, 10)

        End Try


    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Button3.Hide()
        Button2.Show()
        Me.SkladBindingSource.Filter = String.Format("{0} > '{1}'", RotekDataSet.Sklad.PocetColumn, -1)

    End Sub

    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = "10"
        TextBox1.SelectAll()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox2.Text = ""
        TextBox2.Focus()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox2.MouseClick
        TextBox2.Text = ""
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If over = 1 Then
            Try
                Dim pom, pom2 As String
                Dim i As Integer = 0
                tex = TextBox2.Text
                Dim xx As Integer = x
                i = 0
                Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

                While i < xx


                    Try
                        pom = DataGridView1.Rows(i).Cells(0).Value
                        pom2 = UCase(pom)
                        ' Chyby.Show(pom2.IndexOf(UCase(tex)))
                        If pom2.IndexOf(UCase(tex)) = -1 Then
                            DataGridView1.Rows.Remove(DataGridView1.Rows(i))
                            xx = xx - 1

                        ElseIf pom2.IndexOf(UCase(tex)) = 0 Then
                            over = 0
                            TextBox2.Text = pom
                            TextBox2.Select(tex.Length, pom.Length - tex.Length)
                            i = i + 1
                        Else
                            DataGridView1.Rows.Remove(DataGridView1.Rows(i))
                            xx = xx - 1
                        End If
                        over = 1
                    Catch ex As Exception
                        Exit While
                    End Try

                End While

            Catch ex As SystemException
                ' Chyby.Show(ex.Message)
            End Try
        Else
            over = 1
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        'If over = 1 Then
        Try
            Dim pom, pom2, prom, prom2 As String
            Dim i As Integer = 0
            tex2 = TextBox3.Text
            Dim xx As Integer = x
            i = 0

            Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

            While i < xx


                Try
                    pom = DataGridView1.Rows(i).Cells(0).Value
                    pom2 = UCase(pom)
                    prom = DataGridView1.Rows(i).Cells(1).Value
                    prom2 = UCase(prom)
                    If (prom2.IndexOf(UCase(tex2)) = -1) Or (pom <> TextBox2.Text) Then
                        DataGridView1.Rows.Remove(DataGridView1.Rows(i))
                        xx = xx - 1


                    Else
                        'over = 0
                        'TextBox3.Text = prom
                        'TextBox3.Select(tex2.Length, prom.Length - tex2.Length)
                        i = i + 1
                    End If
                    'over = 1
                Catch ex As Exception
                    Exit While
                End Try

            End While

        Catch ex As SystemException
            ' Chyby.Show(ex.Message)
        End Try
        'Else
        'over = 1
        'End If

    End Sub

    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Back Then
            over = 1
            Try
                tex = tex.Substring(0, tex.Length - 1)
            Catch ex As Exception

            End Try
            TextBox2.Text = tex
            'TextBox2.Select()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim f As New srotV
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

    End Sub
    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Dim riadok As Integer
        riadok = e.RowIndex
        Dim nastr As String = DataGridView1.Rows(riadok).Cells(0).Value
        Dim nastr2 As String = DataGridView1.Rows(riadok).Cells(1).Value

        Dim f As New info_nastroj
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.nastr = nastr
        f.nastr2 = nastr2
        f.ShowDialog()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub msklad_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        rozmer()

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Form78.exportovat(DataGridView1)
    End Sub
End Class