Imports System.Data.SqlClient

Public Class infoFirma


    Property nastr2 As String
    Property nastr As String

    Property vlast As String
    Private Sub poverenie()
        Select Case Form78.heslo
            Case Form78.admin
                DataGridView1.Columns(5).Visible = True
                DataGridView1.Columns(6).Visible = True
            Case Form78.zakazkar
                DataGridView1.Columns(5).Visible = True
            Case Form78.skladnik
            Case Else
                DataGridView1.Columns(5).Visible = False
                DataGridView1.Columns(6).Visible = False
        End Select
    End Sub
    Private Sub info_nastroj_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.ZoznamF.pocetColumn, 0)

        Dim crc As String = "Zoznam firiem"
        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(crc, Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2
        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New Point(rw, 10)
        Label1.Text = crc
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Form78.exportovat(DataGridView1)
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 8 And e.RowIndex >= 0 Then
            Dim nazov As String = DataGridView1.Rows(e.RowIndex).Cells(0).Value

            Dim con As New SqlConnection
            Dim com As New SqlCommand
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            com.Connection = con
            com.CommandText = "DELETE FROM ZoznamF WHERE Nazov='" & nazov & "' "
            com.ExecuteNonQuery()
            con.Close()

            Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
            Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.ZoznamF.pocetColumn, 0)
        End If

        If e.ColumnIndex = 7 And e.RowIndex >= 0 Then
            Dim f As New Firma
            f.TopLevel = True
            f.naz = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            f.uli = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            f.mes = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            f.ps = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            f.kra = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            f.Dock = DockStyle.None
            f.ShowDialog()
        End If
        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.ZoznamF.pocetColumn, 0)

    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox2.Text = ""

    End Sub
    Private Sub filtruj()
        Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}%' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' ", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, TextBox1.Text, RotekDataSet.ZoznamF.MestColumn, TextBox2.Text, RotekDataSet.ZoznamF.KrajinaColumn, TextBox3.Text)


    End Sub
    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        filtruj()
    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        filtruj()

    End Sub

    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged
        filtruj()

    End Sub
End Class