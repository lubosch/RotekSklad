Imports System.Data.SqlClient

Public Class skladIne
    Shared Property nastr As String

    Shared Property p As Integer

    Public x As Integer

    Public over As Integer
    Dim typ As Integer

    Public tex, tex2 As String

    Shared Property sku As Integer

    Shared Property reg As String

    Shared Property v As Integer

    Shared Property cenaa As String

    Shared Property nastr2 As String

    Property crc As String

    Property menpr As String
    Public Sub rozmer()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2, sw - 220)
        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(crc, Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2
        Label1.Text = crc
        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New Point(rw, 10)
    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo
            Case Form78.admin
                DataGridView1.Columns(12).Visible = True
                Button1.Show()
                Button5.Show()
            Case Form78.zakazkar
            Case Form78.skladnik
                Button1.Show()
                Button5.Show()
            Case Else
                Button1.Hide()
                Button5.Hide()
        End Select
    End Sub
    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button1.Hide()
        Button5.Hide()
        DataGridView1.Columns(12).Visible = False

        poverenie()

        Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)

        rozmer()

        Button3.Hide()
        TextBox1.Text = "10"

        Me.IneBindingSource.Sort = "Nazov"
        x = DataGridView1.RowCount


        Me.IneBindingSource.Filter = Nothing

        Select Case crc
            Case "Spotrebný materiál"
                typ = 1
            Case "Zveráky a ostatné"
                typ = 2
            Case "Upínací + špeciálne"
                typ = 3
            Case "Elektronáradie"
                typ = 4
            Case "Príslušenstvo"
                typ = 5
            Case "Náradie"
                typ = 6
            Case "Meradlá"
                typ = 7
            Case "Spojovací materiál"
                typ = 8
            Case "Iné"
                typ = 9
            Case "Kvapaliny"
                typ = 10

        End Select
        Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)

        ceny()

        If (typ = 1) Or (typ = 5) Or (typ = 8) Or (typ = 9) Or (typ = 2) Then
            DataGridView1.Columns(0).Visible = False
            TextBox2.Hide()
            TextBox3.Location = New Point(30, 148)
            Button4.Location = New Point(180, 145)
            Button2.Location = New Point(350, 145)
            TextBox1.Location = New Point(500, 148)
        End If
        DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)
    End Sub
    Public Sub ceny()
        For radky = 0 To DataGridView1.RowCount - 1
            Dim pe As Double = DataGridView1.Rows(radky).Cells(9).Value
            DataGridView1.Rows(radky).Cells(3).Value = pe
            pe = DataGridView1.Rows(radky).Cells(10).Value
            DataGridView1.Rows(radky).Cells(6).Value = pe
            If DataGridView1.Rows(radky).Cells(8).Value.ToString.Length = 0 Then DataGridView1.Rows(radky).Cells(4).Value = False Else DataGridView1.Rows(radky).Cells(4).Value = True
        Next
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        sku = 1

        Do While (sku = 1)
            sku = 0
            Dim f As New nastrIne
            f.TopLevel = True
            f.typ = typ
            f.Dock = DockStyle.None
            f.ShowDialog()
            Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)

            ceny()
        Loop
    End Sub
    Sub nic()

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Button2.Hide()
        Button3.Show()
        Try
            Dim a As Integer
            a = TextBox1.Text
            Me.IneBindingSource.Filter = String.Format("{0} < '{1}' AND {2}='{3}' ", RotekDataSet.Ine.KolkoColumn, a, RotekDataSet.Ine.pocetColumn, typ)

        Catch ex As SystemException
            Me.IneBindingSource.Filter = String.Format("{0} < '{1}' AND {2}='{3}' ", RotekDataSet.Ine.KolkoColumn, 10, RotekDataSet.Ine.pocetColumn, typ)

        End Try
        Me.IneBindingSource.Sort = "Kolko"

        ceny()

    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Button3.Hide()
        Button2.Show()
        Me.IneBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Ine.pocetColumn, typ)
        Me.IneBindingSource.Sort = "Ident"

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
    Private Sub filtruj()
        Dim fil, slovo As String
        fil = String.Format("{4}='{5}'", RotekDataSet.Ine.IdentColumn, TextBox2.Text, RotekDataSet.Ine.NazovColumn, TextBox3.Text, RotekDataSet.Ine.pocetColumn, typ)
        ceny()

        slovo = TextBox2.Text
        While slovo.IndexOf(" ") > -1
            fil = fil & String.Format(" AND {0} LIKE '%{1}%'", RotekDataSet.Ine.IdentColumn, slovo.Substring(0, slovo.IndexOf(" ")))
            slovo = slovo.Remove(0, slovo.IndexOf(" ") + 1)
        End While
        fil = fil & String.Format(" AND {0} LIKE '%{1}%'", RotekDataSet.Ine.IdentColumn, slovo)

        slovo = TextBox3.Text
        While slovo.IndexOf(" ") > -1
            fil = fil & String.Format(" AND {0} LIKE '%{1}%'", RotekDataSet.Ine.NazovColumn, slovo.Substring(0, slovo.IndexOf(" ")))
            slovo = slovo.Remove(0, slovo.IndexOf(" ") + 1)
        End While
        fil = fil & String.Format(" AND {0} LIKE '%{1}%'", RotekDataSet.Ine.NazovColumn, slovo)

        Me.IneBindingSource.Filter = fil



    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        filtruj()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        filtruj()
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim f As New srotIne
        f.TopLevel = True
        f.typ = typ
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)
        ceny()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick, DataGridView1.CellClick
        Try

            Dim kks As Integer = e.RowIndex
            If e.ColumnIndex = 11 Then
                Dim f As New infosky
                f.TopLevel = True
                f.typ = typ + 3
                f.popis = DataGridView1.Rows(kks).Cells(1).Value
                f.Dock = DockStyle.None
                f.ShowDialog()
            ElseIf e.ColumnIndex = 12 Then
                Dim ID, nazov As String
                ID = DataGridView1.Rows(kks).Cells(0).Value
                nazov = DataGridView1.Rows(kks).Cells(1).Value
                Dim sql As String
                Dim con As New SqlConnection
                Dim cmd As New SqlCommand
                con.ConnectionString = My.Settings.Rotek2
                con.Open()
                Dim com As New SqlCommand
                com.Connection = con
                sql = "DELETE FROM Ine WHERE Nazov='" & nazov & "' AND Ident='" & ID & "' AND pocet='" & typ & "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                con.Close()
                con.Close()
                Me.IneTableAdapter.Fill(Me.RotekDataSet.Ine)


            Else
                Dim cesta As String

                If DataGridView1.Rows(kks).Cells(8).Value.ToString.Length = 0 Then Exit Sub Else cesta = DataGridView1.Rows(kks).Cells(8).Value
                '                 If cesta.IndexOf("#") = 0 Then cesta = cesta.Substring(1, cesta.Length - 2)

                Dim f As New Fotk
                f.TopLevel = True
                f.cesta = cesta
                f.Dock = DockStyle.None
                f.ShowDialog()
            End If
        Catch ex As Exception
            ' Chyby.Show(ex.ToString)
        End Try

    End Sub

    Private Sub rozm(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        rozmer()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Form78.exportovat(DataGridView1)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex = -1 Then Exit Sub
        DataGridView1.Rows(e.RowIndex).Selected = True
    End Sub
End Class