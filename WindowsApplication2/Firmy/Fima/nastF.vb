Imports System.Data.SqlClient

Public Class nastF
    '  Private menko As String
    Public Sub New(ByVal menko As String)
        InitializeComponent()
        Me.menko = menko

    End Sub

    Public Overrides Sub kliky()
        Dim Nastroj As String = TextBox2.Text
        Dim Nastroj2 As String = TextBox3.Text
        Dim vlast As String = TextBox4.Text
        If vlast.Length = 0 Then vlast = "HSS"
        If Nastroj.Length = 0 Or Nastroj2.Length = 0 Then
            Chyby.Show("Niečo nie je zadané")
            Exit Sub
        End If
        Dim spoluu As Integer
        Try
            Dim pocet As Integer
            Try
                pocet = TextBox1.Text
            Catch ex As Exception
                Chyby.Show("Zle zadaný počet")
            End Try

            If pocet < 0 Then
                Chyby.Show("Musíš to vrátiť cez tlačítko vrátiť, takto to nefunguje")
                Me.Close()
                Exit Sub
            End If

            Dim xxx As Integer = 0
            Dim a As Integer

            Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Firmy.MenoColumn, menko, RotekDataSet.Firmy.pocetColumn, 0)
            spoluu = DataGridView5.Rows(0).Cells(4).Value

            Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4}='{5}'", RotekDataSet.Firmy.NástrojColumn, Nastroj, RotekDataSet.Firmy.VelkostRColumn, Nastroj2, RotekDataSet.Firmy.VlastnostColumn, vlast)

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

            Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.Sklad.NastrojColumn, Nastroj, RotekDataSet.Sklad.VelkostSColumn, Nastroj2, RotekDataSet.Sklad.VlastnostColumn, vlast)
            If DataGridView3.RowCount = 0 Then
                Chyby.Show("Nenašiel sa taký nástroj")
                Exit Sub
            End If
            Dim cenaa As String
            cenaa = DataGridView3.Rows(0).Cells(4).Value
            Try
                a = DataGridView3.Rows(0).Cells(3).Value

                If a - pocet < 0 Then
                    Dim aks As String
                    Dim ifg As Integer
                    aks = "Na sklade je už len: " & a & " nastrojov. Chcete mu ich aj tak dať?"
                    ifg = MsgBox(aks, vbExclamation + vbYesNo, "Overenie")
                    If ifg = vbYes Then  Else xxx = 1
                End If

                If xxx = 0 Then
                    a = a - pocet

                    'Sql = "Insert INTO Sklad (Nastroj, Pocet, Regal, Cena, VelkostS) VALUES ('" + Nastroj + "', '" & a & "', '" + rega + "', '" + cenaa + "', '" + Nastroj2 + "')"
                    Sql = "UPDATE Sklad SET Pocet='" & a & "' WHERE Nastroj='" & Nastroj & "' AND VelkostS='" & Nastroj2 & "' AND Vlastnost='" + vlast + "'"
                    cmd = New SqlCommand(Sql, con)
                    cmd.ExecuteNonQuery()
                    Dim fsd As Integer = 1

                    Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}' AND {6} = '{7}' AND {8}='{9}'", RotekDataSet.Firmy.MenoColumn, menko, RotekDataSet.Firmy.NástrojColumn, Nastroj, RotekDataSet.Firmy.VelkostRColumn, Nastroj2, RotekDataSet.Firmy.VlastnostColumn, vlast, RotekDataSet.Firmy.pocetColumn, 1)

                    If DataGridView5.RowCount = 0 Then
                        Sql = "Insert INTO Firmy (Meno, Nástroj, pocet, Kolko, VelkostR, Cena, Vlastnost) VALUES ('" + menko + "', '" + Nastroj + "', '" & 1 & "', '" & pocet & "', '" & Nastroj2 & "', '" & cenaa & "','" + vlast + "')"
                        cmd = New SqlCommand(Sql, con)
                        cmd.ExecuteNonQuery()

                        spoluu = spoluu + pocet
                        Sql = "UPDATE Firmy SET Spolu='" & spoluu & "' WHERE Meno='" & menko & "' AND pocet=0 "
                        cmd = New SqlCommand(Sql, con)
                        cmd.ExecuteNonQuery()
                    Else
                        Dim b As Integer
                        b = DataGridView5.Rows(0).Cells(3).Value()
                        Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)
                        Me.FirmyBindingSource.Filter = Nothing
                        Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Firmy.MenoColumn, menko, RotekDataSet.Firmy.pocetColumn, 0)
                        spoluu = spoluu + pocet
                        b = b + pocet

                        Dim nula As Integer = 0
                        Sql = "UPDATE Firmy SET Spolu='" & spoluu & "' WHERE Meno='" & menko & "' AND pocet=0 "
                        cmd = New SqlCommand(Sql, con)
                        cmd.ExecuteNonQuery()

                        Sql = "UPDATE Firmy SET Kolko='" & b & "' WHERE Meno='" + menko + "' AND Nástroj='" + Nastroj + "' AND pocet=1 AND VelkostR='" & Nastroj2 & "' AND Vlastnost='" + vlast + "'"
                        cmd = New SqlCommand(Sql, con)
                        cmd.ExecuteNonQuery()
                    End If
                End If

                con.Close()
                Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)
                Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""

                hladaj2(ListBox1, 0)
                hladaj2(ListBox2, 1)
                hladaj2(ListBox3, 2)
                TextBox2.Focus()

            Catch ex As SystemException
                Chyby.Show(ex.ToString)
                con.Close()
                Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)
                Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)


            End Try


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub

    Private Sub nastF_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Firmy' table. You can move, or remove it, as needed.
        Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)
        'k = 0
        'DataGridView1.Hide()
        'Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        'prvy = TextBox2.Text


        'zamestnanec.bmp = 7
        'mzamestnanec.bmp = 7

        'TextBox1.Text = "Tu zadaj počet"
        'hladaj2(ListBox1, 0)
        'hladaj2(ListBox2, 1)
        'hladaj2(ListBox3, 2)

    End Sub

End Class
