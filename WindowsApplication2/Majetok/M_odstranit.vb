Imports System.Data.SqlClient

Public Class M_odstranit
    Inherits M_pridat


    Public Sub New(ByVal typ As Integer)

        MyBase.New(typ)
        RichTextBox1.Hide()
        Button1.Hide()
        TextBox6.Hide()


    End Sub

    Public Overrides Sub rob()
        Dim ident, nazov, izba, miesto As String
        Dim pocet, typp As Integer
        typp = typ

        ident = TextBox1.Text
        nazov = TextBox2.Text
        izba = TextBox3.Text
        miesto = TextBox4.Text

        If (ident.Length = 0 Or nazov.Length = 0 Or izba.Length = 0) Then
            Chyby.Show("Nezadané povinné hodnoty")
            Return
        End If
        Try
            pocet = TextBox5.Text
        Catch ex As Exception
            Chyby.Show("Nezadaný počet")
            Return
        End Try

        Dim connect As String = My.Settings.Rotek2
        Dim Sql As String
        Dim con As New SqlConnection
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Dim cmd As New SqlCommand


        Me.MajetokBindingSource.Filter = String.Format("{0}='{1}' AND {2} ='{3}' AND {4} = '{5}' AND {6} = '{7}'", RotekDataSet.Majetok.TypColumn, typ, RotekDataSet.Majetok.IdentColumn, TextBox1.Text, RotekDataSet.Majetok.NazovColumn, TextBox2.Text, RotekDataSet.Majetok.IzbaColumn, TextBox3.Text)
        If DataGridView1.RowCount = 0 Then
            Chyby.Show("Nenašla sa táto položka")
            Return
        Else
            If miesto.Length > 1 And DataGridView1.Rows(0).Cells(3).Value.ToString.IndexOf(miesto) <> -1 Then

                Dim result As DialogResult = MessageBox.Show("Vymazať z kolónky miesto: " & miesto & " ?", "Problem?", MessageBoxButtons.YesNo)
                If result = vbYes Then
                    miesto = DataGridView1.Rows(0).Cells(3).Value.ToString.Replace(miesto, "")
                Else
                    miesto = DataGridView1.Rows(0).Cells(3).Value
                End If
            End If

            pocet = -pocet + DataGridView1.Rows(0).Cells(6).Value
            Sql = "UPDATE Majetok SET Pocet='" & pocet & "', Miesto='" + miesto + "' WHERE Nazov='" + nazov + "' AND Ident='" + ident + "'  AND Izba='" + izba + "' AND Typ=" & typp & ""

        End If
        cmd = New SqlCommand(Sql, con)
        cmd.ExecuteNonQuery()
        con.Close()
        Me.Close()

    End Sub


    Private Sub M_odstranit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'M_odstranit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(644, 262)
        Me.Name = "M_odstranit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class
