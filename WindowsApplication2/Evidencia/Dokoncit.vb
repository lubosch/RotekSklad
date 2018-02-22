Imports System.Data.SqlClient

Public Class Dokoncit

    Private zakazka As String
    Private poradove_cislo As Integer
    Private preskumal As String
    Private zakaznik As String
    Private pocet_kusov As Integer
    Private prijata As Date
    Private dodania_plan As Date
    Private dodania_skutoc As Date
    Private dodady_kusov As Integer
    Private dodaci As String
    Private ponuka_cislo As String
    Private poznamka As String

    Sub New(ByVal zakazka As String)
        InitializeComponent()
        Me.zakazka = zakazka
    End Sub


    Private Sub Dokoncit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Zakazka' table. You can move, or remove it, as needed.
        Try

            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
            Me.EvidenciaTableAdapter.Fill(Me.RotekDataSet.Evidencia)
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka)
            Try
                poradove_cislo = zakazka.Substring(0, zakazka.IndexOf("/"))
            Catch ex As Exception
                poradove_cislo = DataGridView2.RowCount + 1
            End Try
            Label2.Text = poradove_cislo
            TextBox1.Text = Form78.uzivatel

            TextBox2.Text = DataGridView1.Rows(0).Cells(0).Value
            TextBox3.Text = DataGridView1.Rows(0).Cells(8).Value
            TextBox7.Text = TextBox3.Text
            DateTimePicker1.Value = DataGridView1.Rows(0).Cells(2).Value
            DateTimePicker2.Value = DataGridView1.Rows(0).Cells(3).Value
            DateTimePicker3.Value = Date.Now
            Dim dl As String = ""
            Try
                dl = DataGridView1.Rows(0).Cells(7).Value
                dl = dl.Substring(1)
                TextBox8.Text = dl.Substring(0, dl.IndexOf("|"))
            Catch ex As Exception
                TextBox8.Text = dl
            End Try

            Dim cp As String = ""
            Try
                cp = DataGridView1.Rows(0).Cells(9).Value
                cp = cp.Substring(1)
                TextBox9.Text = cp.Substring(0, cp.IndexOf("|"))
            Catch ex As Exception
                TextBox9.Text = cp
            End Try


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Private Sub stuk()
        If TextBox1.Text.Length <> 0 And TextBox2.Text.Length <> 0 And TextBox3.Text.Length <> 0 And TextBox7.Text.Length <> 0 Then
            preskumal = TextBox1.Text
            zakaznik = TextBox2.Text
            Try
                pocet_kusov = TextBox3.Text
            Catch ex As Exception
                Chyby.Show("Zle zadany pocet kusov")
                Exit Sub
            End Try
            prijata = DateTimePicker3.Value
            dodania_plan = DateTimePicker1.Value
            dodania_skutoc = DateTimePicker2.Value
            Try
                dodady_kusov = TextBox7.Text

            Catch ex As Exception
                Chyby.Show("Zlý počet kusov")
                Exit Sub
            End Try
            dodaci = TextBox8.Text
            Try
                If TextBox9.Text.Length Then ponuka_cislo = TextBox9.Text
            Catch ex As Exception
                Chyby.Show("Zle zadané číslo ponuky")
                Exit Sub
            End Try
            poznamka = TextBox10.Text
            If String.IsNullOrEmpty(poznamka) Then poznamka = " "


            Dim con As New SqlConnection
            Dim cmd As New SqlCommand
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            Dim sql As String = ""
            Try
                sql = "Insert INTO Evidencia (Poradie, Preskumal, Zakaznik, Zakazka, Kusov, Prijata, Pozadovany, Skutocny, Dodany, Dodaci, Ponuka_c, Poznamka) VALUES ('" & poradove_cislo & "', '" + preskumal + "', '" + zakaznik + "','" + zakazka + "','" & pocet_kusov & "','" + prijata.ToString("yyyy-MM-dd") + "','" + dodania_plan.ToString("yyyy-MM-dd") + "','" + dodania_skutoc.ToString("yyyy-MM-dd") + "','" & dodady_kusov & "','" + dodaci + "','" & ponuka_cislo & "','" + poznamka + "')"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                Dim aaaaa As String = "Ukončená"
                Dim datum As DateTime = New Date(2085, 10, 10)
                sql = "UPDATE Zakazka SET D_real='" + Date.Now.ToString("yyyy-MM-dd") + "', D_plan='" + datum.ToString("yyyy-MM-dd") + "' WHERE zakazka='" + zakazka + "' AND pocet=1"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                sql = "UPDATE Huta SET D_ukoncenia='" & datum.ToString("yyyy-MM-dd") & "' WHERE zakazka='" + zakazka + "' AND pocet='2'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                Chyby.Show(ex.ToString)
            End Try
            con.Close()
            Dispose()
        Else
            Chyby.Show("Nieco nie je zadane")

        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        stuk()
    End Sub

    Private Sub TextBox10_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyUp
        If e.KeyData = Keys.Enter Then
            stuk()
        End If
    End Sub

    Private Sub TextBox9_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyUp
        If e.KeyData = Keys.Enter Then
            stuk()
        End If

    End Sub

    Private Sub TextBox8_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyUp
        If e.KeyData = Keys.Enter Then
            stuk()
        End If

    End Sub

    Private Sub TextBox7_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyUp
        If e.KeyData = Keys.Enter Then
            stuk()
        End If

    End Sub

    Private Sub TextBox6_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyData = Keys.Enter Then
            stuk()
        End If

    End Sub

End Class