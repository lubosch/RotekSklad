Imports System.Data.SqlClient

Public Class VratitNaSklad
    Inherits srotC

    Public Overrides Sub kliky()
        Dim nastroj As String = TextBox2.Text
        Dim nastroj2 As String = TextBox3.Text
        Dim vlast As String = TextBox4.Text
        If vlast.Length = 0 Then vlast = "HSS"
        If nastroj.Length = 0 Or nastroj2.Length = 0 Then
            Chyby.Show("Niečo nie je zadané")
            Exit Sub
        End If
        Try
            Dim pocett As Integer
            Try
                pocett = TextBox1.Text
            Catch ex As Exception
                Chyby.Show("Nezadal si pocet")
                Exit Sub
            End Try

            If pocett < 0 Then
                Chyby.Show("Počet musí byť kladné číslo")
                Exit Sub
            End If


            Dim a As Integer
            pocett = -pocett


            Dim Sql As String
            Dim con As New SqlConnection
            con.ConnectionString = My.Settings.Rotek2
            con.Open()

            Dim cmd As New SqlCommand
            '  Dim com As New SqlCommand
            ' com.Connection = con


            Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4}='{5}'", RotekDataSet.Sklad.NastrojColumn, nastroj, RotekDataSet.Sklad.VelkostSColumn, nastroj2, RotekDataSet.Sklad.VlastnostColumn, vlast)

            If DataGridView2.RowCount <> 1 Then
                Chyby.Show("Nastroj nenajdeny")
                Exit Sub
            End If

            a = DataGridView2.Rows(0).Cells(1).Value
            a = a - pocett
            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 0)
            Dim spoluu As Integer
            Try
                spoluu = DataGridView1.Rows(0).Cells(7).Value + pocett
            Catch ex As Exception
                spoluu = pocett
            End Try

            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}' AND {6}='{7}' AND {8}='{9}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.NástrojColumn, nastroj, RotekDataSet.Rotek.VelkostRColumn, nastroj2, RotekDataSet.Rotek.VlastnostColumn, vlast, RotekDataSet.Rotek.pocetColumn, 1)
            If DataGridView1.RowCount = 0 Then
                Chyby.Show("Nenašlo sa u neho")
                Exit Sub
            End If

            Dim b As Integer

            b = DataGridView1.Rows(0).Cells(6).Value()
            b = b + pocett

            If b < 0 Then
                Chyby.Show("Nemá toľko")
                Exit Sub
            End If


            Sql = "UPDATE Rotek SET Kolko='" & b & "' WHERE Menpr='" + crc + "' AND pocet=1 AND Nástroj='" & nastroj & "' AND VelkostR='" & nastroj2 & "' AND Vlastnost='" + vlast + "'"
            cmd = New SqlCommand(Sql, con)
            cmd.ExecuteNonQuery()

            Sql = "UPDATE Rotek SET Spolu='" & spoluu & "' WHERE Menpr='" + crc + "' AND pocet=0"
            cmd = New SqlCommand(Sql, con)
            cmd.ExecuteNonQuery()

            Sql = "UPDATE Sklad SET Pocet='" & a & "' WHERE Nastroj='" & nastroj & "' AND VelkostS='" & nastroj2 & "' AND Vlastnost='" + vlast + "'"
            cmd = New SqlCommand(Sql, con)
            cmd.ExecuteNonQuery()

            zamestnanec.doexcel(crc, nastroj, nastroj2, vlast, pocett.ToString)

            con.Close()
            Me.Close()

        Catch ex As SystemException
            Chyby.Show(ex.ToString)
        End Try

    End Sub


    Private Sub VratitNaSklad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button2.Text = "Vrátiť"
    End Sub
End Class