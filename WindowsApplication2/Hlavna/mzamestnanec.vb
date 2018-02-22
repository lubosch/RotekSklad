Public Class zamestnanec

    Property crc As String
    Public x As Integer
    Shared Property Pocett As Integer

    Shared Property Nastroj As String

    Property menko As String

    Property prezvo As String

    Property spoluu As Integer

    Shared Property bmp As Integer

    Shared Property Nastroj2 As String

    Shared Property spolu As Integer

    Shared Property Cenka As Double

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim rww As Integer = Screen.PrimaryScreen.Bounds.Width / 2
        Dim sw As Integer = Screen.PrimaryScreen.Bounds.Height

        DataGridView1.Size = New Size(rww * 2, sw - 280)

        TextBox1.Text = "Tu napíš meno nástroja na filter:"
        'TODO: This line of code loads data into the 'RotekDataSet.Sklad' table. You can move, or remove it, as needed.
        DataGridView2.Hide()
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        x = DataGridView1.RowCount
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
        Try
            Me.RotekTableAdapter.FillBy2(Me.RotekDataSet.Rotek)
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try
        Me.RotekBindingSource.Sort = "Nástroj"

        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(crc, Label1.Font)
        Dim stred As Integer

        stred = strSz.Width / 2

        Dim rw As String = Screen.PrimaryScreen.Bounds.Width / 2 - stred
        Label1.Location = New Point(rw, 20)
        Label1.Text = crc
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)

       
    End Sub

    Private Sub FillBy4ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.RotekTableAdapter.FillBy1(Me.RotekDataSet.Rotek)
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New nastrclovek
        Dim w As Integer = 2

        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()
        If bmp = 1 Then
            Dim xxx As Integer = 0
            Dim a As Integer
            Dim rega As String

            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Rotek.NástrojColumn, Nastroj, RotekDataSet.Rotek.VelkostRColumn, Nastroj2)

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
            Try
                Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Sklad.NastrojColumn, Nastroj, RotekDataSet.Sklad.VelkostSColumn, Nastroj2)
                a = DataGridView2.Rows(0).Cells(2).Value
                If a - Pocett < 0 Then
                    Dim aks As String
                    Dim ifg As Integer
                    aks = "Na sklade je už len: " & a & " nastrojov. Chcete mu ich aj tak dať?"
                    ifg = MsgBox(aks, vbExclamation + vbYesNo, "Overenie")
                    If ifg = vbYes Then a = 0 Else xxx = 1
                End If

                If xxx = 0 Then
                    If a = 0 And Pocett > -1 Then a = 0 Else a = a - Pocett

                    Dim cenaa As String

                    rega = DataGridView2.Rows(0).Cells(1).Value
                    cenaa = DataGridView2.Rows(0).Cells(3).Value

                    'com.CommandText = "DELETE FROM Sklad WHERE Nastroj ='" & Nastroj & "' AND VelkostS ='" & Nastroj2 & "' "
                    'com.ExecuteNonQuery()
                    com.CommandText = "DELETE FROM Rotek WHERE Menpr ='" & crc & "' AND Nástroj ='" & Nastroj & "' AND VelkostR ='" & Nastroj2 & "' AND pocet=1"
                    com.ExecuteNonQuery()

                    'Sql = "Insert INTO Sklad (Nastroj, Pocet, Regal, Cena, VelkostS) VALUES ('" + Nastroj + "', '" & a & "', '" + rega + "', '" + cenaa + "', '" + Nastroj2 + "')"
                    Sql = "UPDATE Sklad SET Pocet='" & a & "' WHERE Nastroj='" & Nastroj & "' AND VelkostS='" & Nastroj2 & "'"
                    cmd = New OleDb.OleDbCommand(Sql, con)
                    cmd.ExecuteNonQuery()
                    Dim fsd As Integer = 1

                    Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.NástrojColumn, Nastroj, RotekDataSet.Rotek.VelkostRColumn, Nastroj2)

                    Dim b As Integer

                    b = DataGridView1.Rows(0).Cells(2).Value()
                    Dim srot1 As Integer
                    Try
                        srot1 = DataGridView1.Rows(0).Cells(3).Value()
                    Catch exc As Exception
                        srot1 = 0
                    End Try


                    Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
                    Me.RotekBindingSource.Filter = Nothing

                    Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 0)
                    Dim srot As Integer
                    Try
                        srot = DataGridView1.Rows(0).Cells(4).Value()
                    Catch exc As Exception
                        srot = 0
                    End Try
                    
                    spoluu = spoluu + Pocett
                    b = b + Pocett

                    com.Connection = con
                    com.CommandText = "DELETE FROM Rotek WHERE Menpr='" & crc & "' AND pocet=0 "
                    com.ExecuteNonQuery()
                    Dim nula As Integer = 0
                    Sql = "Insert INTO Rotek (Meno, Priezvisko, Menpr, pocet, Spolu, srotcena) VALUES ('" + menko + "', '" + prezvo + "', '" + crc + "','" & nula & "','" & spoluu & "','" & srot & "')"
                    cmd = New OleDb.OleDbCommand(Sql, con)
                    cmd.ExecuteNonQuery()

                    Sql = "Insert INTO Rotek (Meno, Priezvisko, Nástroj, pocet, Menpr, Kolko, Spolu, VelkostR, srot) VALUES ('" + menko + "', '" + prezvo + "', '" + Nastroj + "', '" & fsd & "', '" + crc + "', '" & b & "', '" & spoluu & "', '" + Nastroj2 + "', '" & srot1 & "')"
                    cmd = New OleDb.OleDbCommand(Sql, con)
                    cmd.ExecuteNonQuery()
                    con.Close()

                    Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
                    Try
                        Me.RotekTableAdapter.FillBy2(Me.RotekDataSet.Rotek)
                    Catch ex As System.Exception
                        System.Windows.Forms.MessageBox.Show(ex.Message)
                    End Try

                End If
                Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
                Me.RotekTableAdapter.FillBy2(Me.RotekDataSet.Rotek)


            Catch ex As SystemException
                Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
                Me.RotekBindingSource.Filter = Nothing
                Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 0)
                Dim srot As Integer
                Try
                    srot = DataGridView1.Rows(0).Cells(4).Value()
                Catch exc As Exception
                    srot = 0
                End Try
                Dim fsd As Integer = 1
                Sql = "Insert INTO Rotek (Meno, Priezvisko, Nástroj, pocet, Menpr, Kolko, Spolu, VelkostR) VALUES ('" + menko + "', '" + prezvo + "', '" + Nastroj + "', '" & fsd & "', '" + crc + "', '" & Pocett & "', '" & spoluu & "', '" + Nastroj2 + "')"
                cmd = New OleDb.OleDbCommand(Sql, con)
                cmd.ExecuteNonQuery()

                Me.SkladBindingSource.Filter = Nothing
                Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

                Dim nula As Integer = 0

                com.Connection = con
                com.CommandText = "DELETE FROM Rotek WHERE Menpr='" & crc & "' AND pocet=0 "
                com.ExecuteNonQuery()



                spoluu = spoluu + Pocett
                Sql = "Insert INTO Rotek (Meno, Priezvisko, Menpr, pocet, Spolu, srotcena) VALUES ('" + menko + "', '" + prezvo + "', '" + crc + "','" & nula & "','" & spoluu & "','" & srot & "')"
                cmd = New OleDb.OleDbCommand(Sql, con)
                cmd.ExecuteNonQuery()
                con.Close()

                Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
                Me.RotekTableAdapter.FillBy2(Me.RotekDataSet.Rotek)



            End Try

        End If
        x = DataGridView1.RowCount

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Me.Close()
    End Sub
    Private Sub FillBy2ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.RotekTableAdapter.FillBy2(Me.RotekDataSet.Rotek)
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = ""
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            Dim a, pom As String
            Dim i As Integer = 0
            a = TextBox1.Text
            Dim xx As Integer = x
            i = 0

            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
            Try
                Me.RotekTableAdapter.FillBy2(Me.RotekDataSet.Rotek)
            Catch ex As System.Exception
                System.Windows.Forms.MessageBox.Show(ex.Message)
            End Try


            While i < xx


                Try
                    pom = UCase(DataGridView1.Rows(i).Cells(0).Value)
                    If pom.IndexOf(UCase(a)) = -1 Then
                        DataGridView1.Rows.Remove(DataGridView1.Rows(i))
                        xx = xx - 1
                    Else
                        i = i + 1
                    End If
                Catch ex As Exception
                    Exit While
                End Try


            End While


        Catch ex As SystemException
            ' MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        bmp = 0
        Dim f As New srotC
        f.TopLevel = True
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        x = DataGridView1.RowCount
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
        Try
            Me.RotekTableAdapter.FillBy2(Me.RotekDataSet.Rotek)
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try
        If bmp = 1 Then
            Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Sklad.NastrojColumn, Nastroj, RotekDataSet.Sklad.VelkostSColumn, Nastroj2)
            Dim evra As Double
            Try
                Dim k As String = (DataGridView2.Rows(0).Cells(3).Value())
                k = k.Substring(0, k.Length - 1)
                k = Replace(k, ".", ",")
                evra = CDbl(k)
            Catch ex As Exception
                evra = 0
            End Try
            evra = Cenka + evra * Pocett

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
            Dim a As Int32 = 0
            com.CommandText = "DELETE FROM Rotek WHERE pocet = 0 AND Menpr = '" & crc & "' "
            com.ExecuteNonQuery()

            Sql = "Insert INTO Rotek (Meno, Priezvisko, Nástroj, pocet, Menpr, Kolko, Spolu, VelkostR, srot, srotcena) VALUES ('" + menko + "', '" + prezvo + "', '" & 0 & "', '" & 0 & "', '" & crc & "', '" & 0 & "', '" & spolu & "', '" & 0 & "', '" & 0 & "', '" + evra.ToString + "' )"

            cmd = New OleDb.OleDbCommand(Sql, con)
            cmd.ExecuteNonQuery()

            Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
            Try
                Me.RotekTableAdapter.FillBy2(Me.RotekDataSet.Rotek)
            Catch ex As System.Exception
                System.Windows.Forms.MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Try
            Dim a, pom, pom2 As String
            Dim i As Integer = 0
            a = TextBox2.Text
            Dim aa As String = TextBox1.Text
            Dim xx As Integer = x
            i = 0

            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.MenprColumn, crc)
            Try
                Me.RotekTableAdapter.FillBy2(Me.RotekDataSet.Rotek)
            Catch ex As System.Exception
                System.Windows.Forms.MessageBox.Show(ex.Message)
            End Try


            While i < xx


                Try
                    pom = DataGridView1.Rows(i).Cells(1).Value
                    pom2 = UCase(DataGridView1.Rows(i).Cells(0).Value)

                    If pom.IndexOf(a) <> -1 Then

                        If pom2.IndexOf(UCase(aa)) <> -1 Then
                            i = i + 1
                        Else
                            DataGridView1.Rows.Remove(DataGridView1.Rows(i))
                            xx = xx - 1
                        End If
                    Else
                        DataGridView1.Rows.Remove(DataGridView1.Rows(i))
                        xx = xx - 1
                    End If

                Catch ex As Exception
                    Exit While
                End Try


            End While


        Catch ex As SystemException
            ' MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class