Imports System.Data.SqlClient

Public Class Evid_Uprav
    Private zakazka As String
    Private poradove_cislo As Integer
    Private preskumal As String
    Private zakaznik As String
    Private pocet_kusov As Integer
    Private prijata As Date
    Private dodania_plan As Date
    Private dodania_skutoc As Date
    Private dodady_kusov As String
    Private dodaci As String
    Private ponuka_cislo As String
    Private poznamka As String
    Private i As Integer
    Private kkk As Evidencia

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Label2.Text = DataGridView1.RowCount
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal a As String, ByVal b As String, ByVal c As String, ByVal l As String, ByVal d As String, ByVal e As String, ByVal f As String, ByVal g As String, ByVal h As String, ByVal i As String, ByVal j As String, ByVal k As String, ByRef m As Evidencia)
        InitializeComponent()
        Label2.Text = a
        poradove_cislo = a
        TextBox1.Text = b
        TextBox2.Text = c
        TextBox3.Text = d
        TextBox4.Text = e
        TextBox5.Text = f
        TextBox6.Text = g
        TextBox7.Text = d
        TextBox8.Text = i
        TextBox9.Text = j
        TextBox10.Text = k
        TextBox11.Text = l
        kkk = m

    End Sub

    Public Overridable Sub stuk()
        preskumal = TextBox1.Text
        zakaznik = TextBox2.Text
        zakazka = TextBox11.Text

        Try
            pocet_kusov = TextBox3.Text
        Catch ex As Exception
            Chyby.Show("Zle zadany pocet kusov")
            Exit Sub
        End Try
        prijata = TextBox4.Text
        dodania_plan = TextBox5.Text
        dodania_skutoc = TextBox6.Text
        dodady_kusov = TextBox7.Text
        dodaci = TextBox8.Text
        Try
            If TextBox9.Text.Length Then ponuka_cislo = TextBox9.Text
        Catch ex As Exception
            Chyby.Show("Zle zadané číslo ponuky")
            Exit Sub
        End Try
        poznamka = TextBox10.Text

        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Dim sql As String = ""

        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Cells(1).Value = poradove_cislo Then
                sql = "UPDATE Evidencia SET Preskumal='" + preskumal + "', Zakaznik='" + zakaznik + "', Zakazka='" + zakazka + "', Kusov='" & pocet_kusov & "', Prijata='" + prijata + "', Pozadovany='" + dodania_plan + "', Skutocny='" + dodania_skutoc + "', Dodany='" + dodady_kusov + "', Dodaci='" + dodaci + "', Ponuka_c='" + ponuka_cislo + "', Poznamka='" + poznamka + "' WHERE Poradie=" & poradove_cislo & ""
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                cmd.ExecuteNonQuery()
                con.Close()
                kkk.EvidenciaTableAdapter.Fill(Me.RotekDataSet.Evidencia)
                Me.Close()
                Exit Sub
            End If
        Next
        sql = "Insert INTO Evidencia (Poradie, Preskumal, Zakaznik, Zakazka, Kusov, Prijata, Pozadovany, Skutocny, Dodany, Dodaci, Ponuka_c, Poznamka) VALUES ('" & poradove_cislo & "', '" + preskumal + "', '" + zakaznik + "','" + zakazka + "','" & pocet_kusov & "','" + prijata.ToString("yyyy-MM-dd") + "','" + dodania_plan.ToString("yyyy-MM-dd") + "','" + dodania_skutoc.ToString("yyyy-MM-dd") + "','" & dodady_kusov & "','" + dodaci + "','" & ponuka_cislo & "','" + poznamka + "')"
        cmd = New SqlCommand(sql, con)
        cmd.ExecuteNonQuery()
        con.Close()
        Me.Close()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        stuk()
    End Sub

    Private Sub TextBox10_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyUp, TextBox9.KeyUp, TextBox8.KeyUp, TextBox7.KeyUp, TextBox6.KeyUp, TextBox5.KeyUp, TextBox4.KeyUp, TextBox3.KeyUp, TextBox2.KeyUp, TextBox11.KeyUp
        If e.KeyData = Keys.Enter Then
            stuk()
        End If

    End Sub

    Private Sub Evid_Uprav_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.EvidenciaTableAdapter.Fill(Me.RotekDataSet.Evidencia)
        If Label2.Text = 0 Then
            Label2.Text = DataGridView1.RowCount + 1
            poradove_cislo = DataGridView1.RowCount + 1
        End If


    End Sub
End Class