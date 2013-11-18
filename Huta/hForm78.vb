Imports System.Data.SqlClient

Public Class hForm78

    Property crc As String
    Public x As Integer
    Shared Property Pocett As Integer

    Shared Property Nastroj As String

    Property prezvo As String

    Property spoluu As Integer

    Shared Property bmp As Integer

    Shared Property Nastroj2 As String

    Shared Property spolu As Integer

    Shared Property Cenka As Double

    Public Sub rozmers()
        Label1.Text = "Hutný sklad"
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        Dim g As Graphics = Me.CreateGraphics

        DataGridView1.Size = New Size((rww + 40), (sw - 240))
        DataGridView1.Location = New Point(0, 199)

        DataGridView2.Location = New Point((rww + 40), 199)
        DataGridView2.Size = New Size((rww - 50), (sw - 240) / 2)

        DataGridView3.Location = New Point((rww + 40), 199 + (sw - 240) / 2)
        DataGridView3.Size = New Size((rww - 50), (sw - 240) / 2)


        Dim strSz As SizeF = g.MeasureString(Label1.Text, Label1.Font)
        Dim stred As Integer

        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred

        Label1.Location = New Point(rw, 10)

    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo
            Case Form78.admin
                Button7.Show()
                Button17.Show()
                Button1.Show()
                Button8.Show()

                DataGridView1.Columns("Zmazat").Visible = True
                DataGridView1.Columns("Vkusov").Visible = True
                DataGridView1.Columns("VKbutton").Visible = True
            Case Form78.zakazkar
                Button7.Show()
                Button17.Show()
                Button1.Show()
                Button8.Show()

                DataGridView1.Columns("Zmazat").Visible = True
                DataGridView1.Columns("Vkusov").Visible = True
                DataGridView1.Columns("VKbutton").Visible = True
            Case Form78.skladnik
                Button7.Show()
                Button17.Show()
                Button1.Show()
                Button1.Show()
                Button8.Show()

                DataGridView1.Columns("Zmazat").Visible = True
                DataGridView1.Columns("Vkusov").Visible = True
                DataGridView1.Columns("VKbutton").Visible = True
            Case Else
                Button7.Hide()
                Button17.Hide()
                Button1.Hide()
                Button8.Hide()

                DataGridView1.Columns("Zmazat").Visible = False
                DataGridView1.Columns("Vkusov").Visible = False
                DataGridView1.Columns("VKbutton").Visible = False
        End Select
    End Sub

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Prijemky' table. You can move, or remove it, as needed.
        Dim stopky As Stopwatch = New Stopwatch()
        stopky.Start()

        'Me.VydajkyTableAdapter.fillFiltered(Me.RotekDataSet.Vydajky, "", "","")

        Debug.WriteLine(stopky.ElapsedMilliseconds)

        poverenie()

        Debug.WriteLine(stopky.ElapsedMilliseconds)

        x = DataGridView1.RowCount
        'Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}<>'{3}'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0)
        'Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}'", RotekDataSet.Huta.pocetColumn, 2)
        Debug.WriteLine(stopky.ElapsedMilliseconds)

        Me.MaterialBindingSource.Sort = "Druh, Nazov, Typ"
        Debug.WriteLine(stopky.ElapsedMilliseconds)


        'Me.HutaBindingSource1.Sort = "D_ukoncenia ASC"
        ' ceny()


        rozmers()
        Debug.WriteLine(stopky.ElapsedMilliseconds)
        zmena()
        Debug.WriteLine(stopky.ElapsedMilliseconds)
    End Sub



    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox4.Text = ""
        TextBox3.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""

        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = ""
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        zmena()
        MaterialBindingSource.Sort = "Druh, " & MaterialBindingSource.Sort
    End Sub



    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        zmena()
        MaterialBindingSource.Sort = "Nazov, " & MaterialBindingSource.Sort
    End Sub



    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox4.Text = ""
        TextBox3.Text =
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""

        zmena()
    End Sub



    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TextBox1.Text = "Plast"
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TextBox1.Text = "Kov"
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TextBox1.Text = "Dural"
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TextBox1.Text = "Mosadz"
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TextBox1.Text = "Bronz"
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TextBox1.Text = "Meď"
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TextBox1.Text = "Oceľ"
    End Sub

    Private Sub hForm78_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        rozmers()
    End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form78.exportovat(DataGridView1)
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Form78.exportovat(DataGridView3)
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        zmena()
    End Sub


    Private Sub zmena()
        Dim stopky As Stopwatch = New Stopwatch
        stopky.Start()
        'Debug.WriteLine(stopky.ElapsedMilliseconds)

        Try
            Dim znamienko As String = "="
            If RadioButton1.Checked Then
                znamienko = ">="
            ElseIf RadioButton2.Checked Then
                znamienko = "<="
            ElseIf RadioButton3.Checked Then
                znamienko = "="
            End If

            Dim nazov, druh, typ As String
            nazov = TextBox2.Text
            typ = TextBox3.Text
            druh = TextBox1.Text
            Dim filt As String = " 1 = 1 "

            If CheckBox1.Checked = False Then
                filt = String.Format("{0} AND {1} <> '{2}'", filt, RotekDataSet.Huta.KusovColumn, 0)
            End If

            Dim c, cc, ccc, cccc As Integer
            If TextBox4.Text.Length <> 0 Then
                Try
                    c = TextBox4.Text
                    filt = String.Format("{0} AND {1} {3} '{2}'", filt, RotekDataSet.Huta.RozmerColumn, c, znamienko)
                Catch ex As Exception
                    c = 0
                End Try
            End If
            If TextBox6.Text.Length <> 0 Then


                Try
                    ccc = TextBox6.Text
                    filt = String.Format("{0} AND {1} {3} '{2}'", filt, RotekDataSet.Huta.VelkostColumn, ccc, znamienko)
                Catch ex As Exception
                    ccc = 0
                End Try
            End If
            If TextBox7.Text.Length <> 0 Then
                Try
                    cc = TextBox7.Text
                    filt = String.Format("{0} AND {1} {3} '{2}'", filt, RotekDataSet.Huta.SirkaColumn, cc, znamienko)
                Catch ex As Exception
                    cc = 0
                End Try
            End If
            If TextBox8.Text.Length <> 0 Then
                Try
                    cccc = TextBox8.Text
                    filt = String.Format("{0} AND {1} {3} '{2}'", filt, RotekDataSet.Huta.S_rozmerColumn, cccc, znamienko)
                Catch ex As Exception
                    cccc = 0
                End Try
            End If

            'Debug.WriteLine(stopky.ElapsedMilliseconds)
            Me.MaterialTableAdapter.FillFiltered(Me.RotekDataSet.Material, druh, typ,nazov, filt)

            'Material_SQL.fillFiltered(Me.RotekDataSet.Material, filt, nazov)
            'Debug.WriteLine(stopky.ElapsedMilliseconds)
            Me.VydajkyTableAdapter.fillFiltered(Me.RotekDataSet.Vydajky, druh, typ, nazov, filt)
            'Debug.WriteLine(stopky.ElapsedMilliseconds)
            Me.PrijemkyTableAdapter.fillFiltered(Me.RotekDataSet.Prijemky, druh, typ, nazov, filt)
            'Debug.WriteLine(stopky.ElapsedMilliseconds)
            stopky.Stop()
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        zmena()
        MaterialBindingSource.Sort = "Rozmer, " & MaterialBindingSource.Sort
    End Sub

    Private Function getMaterialByRow(riadok As Integer) As Material_SQL
        Dim druh, nazov, rozmer, sirka, s_rozmer, velkost, typ As String
        druh = DataGridView1.Rows(riadok).Cells(0).Value
        nazov = DataGridView1.Rows(riadok).Cells(1).Value
        sirka = DataGridView1.Rows(riadok).Cells(3).Value
        rozmer = DataGridView1.Rows(riadok).Cells(4).Value
        s_rozmer = DataGridView1.Rows(riadok).Cells(5).Value
        velkost = DataGridView1.Rows(riadok).Cells(6).Value
        typ = DataGridView1.Rows(riadok).Cells(2).Value
        Dim material As Material_SQL = New Material_SQL(druh, nazov, sirka, rozmer, s_rozmer, velkost, 0, typ)
        Return material

    End Function

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            If e.RowIndex < 0 Then
                Exit Sub
            End If

            If (e.ColumnIndex = DataGridView1.Columns("Zmazat").Index) Then 'Zmazat
                Dim riadok As Integer = e.RowIndex
                Vymaz_material(riadok)
            End If
            If (e.ColumnIndex = DataGridView1.Columns("VKbutton").Index) Then 'Vyhodit kusy
                Vyhod_Material(e.RowIndex)

            End If

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub
    Private Sub Vymaz_material(riadok As Integer)
        Dim material As Material_SQL = getMaterialByRow(riadok)
        If MessageBox.Show("Naozajchcete zmazať material Druh= """ & material.druh & """ Nazov = """ & material.nazov & """ typ = """ & material.typ & """ rozmer = """ & material.rozmer_slovom & """ so všetkými náveznosťami na výdajky a príjemky navždy???", "Nerob to!", MessageBoxButtons.YesNo) = vbYes Then
            SQL_main.AddCommand("DELETE FROM Material_Prijemka WHERE Material_ID = " & material.id)
            SQL_main.AddCommand("DELETE FROM Material_Vydajka WHERE Material_ID = " & material.id)
            SQL_main.AddCommand("DELETE FROM MaterialCena WHERE Material_ID = " & material.id)
            SQL_main.AddCommand("DELETE FROM Odpady WHERE Material_ID = " & material.id)
            SQL_main.AddCommand("DELETE FROM Material WHERE ID = " & material.id)
            SQL_main.Commit_Transaction()

            zmena()
        End If

    End Sub
    Private Sub Vyhod_Material(riadok As Integer)
        Dim kusov As Integer
        Try
            kusov = DataGridView1.Rows(riadok).Cells("VKusov").Value
        Catch ex As Exception
            Chyby.Show("Zle zadaný počet kusov")
            Exit Sub
        End Try

        Dim material As Material_SQL = getMaterialByRow(riadok)

        SQL_main.AddCommand("INSERT INTO Odpady (Material_ID, Ks) VALUES( " & material.id & " , " & kusov & ")")
        SQL_main.AddCommand("UPDATE Material SET Kusov = Kusov - " & kusov & " WHERE ID = " & material.id)
        SQL_main.Commit_Transaction()
        zmena()

    End Sub
   

    Private Sub Button17_Click(sender As System.Object, e As System.EventArgs) Handles Button17.Click
        Dim f As New Prijemky
        Me.Hide()
        f.ShowDialog()
        f.Dispose()
        Me.Show()
        zmena()

    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex <= 2 OrElse e.ColumnIndex >= 7 OrElse (e.Value Is Nothing OrElse e.Value.ToString <> "-1") Then
        Else
            e.Value = ""
        End If
        'If e.ColumnIndex = 10 Then
        '    e.Value = DataGridView1.Rows(e.RowIndex).Cells(12).Value
        'End If
        'If e.ColumnIndex = 13 Then
        '    e.Value = Format(Huta_SQL.objem(DataGridView1.Rows(e.RowIndex).Cells(4).Value, DataGridView1.Rows(e.RowIndex).Cells(6).Value, DataGridView1.Rows(e.RowIndex).Cells(3).Value, DataGridView1.Rows(e.RowIndex).Cells(5).Value, DataGridView1.Rows(e.RowIndex).Cells(2).Value) * DataGridView1.Rows(e.RowIndex).Cells(18).Value * DataGridView1.Rows(e.RowIndex).Cells(7).Value, "N1") & " Kg"
        'End If
        'If e.ColumnIndex = 9 Then
        '    e.Value = Format(DataGridView1.Rows(e.RowIndex).Cells(11).Value)
        'End If
    End Sub


    Private Sub TextBox3_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        zmena()
        MaterialBindingSource.Sort = "Typ, " & MaterialBindingSource.Sort

    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        zmena()
        MaterialBindingSource.Sort = "Sirka, " & MaterialBindingSource.Sort
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        zmena()
        MaterialBindingSource.Sort = "Velkost, " & MaterialBindingSource.Sort

    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        zmena()
        MaterialBindingSource.Sort = "S_rozmer, " & MaterialBindingSource.Sort

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim slovo As String = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

            Select Case e.ColumnIndex
                Case 0
                    TextBox1.Text = slovo
                Case 1
                    TextBox2.Text = slovo
                Case 2
                    TextBox3.Text = slovo
                Case 3
                    TextBox7.Text = slovo
                Case 4
                    TextBox4.Text = slovo
                Case 5
                    TextBox8.Text = slovo
                Case 6
                    TextBox6.Text = slovo
            End Select
        End If

    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        Dim f As New Vydajky
        Me.Hide()
        f.ShowDialog()
        f.Dispose()
        Me.Show()
        zmena
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        Dim f As New Hvydat
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()
        f.Dispose()
        zmena()
    End Sub


    Private Sub TextBox2_DoubleClick(sender As Object, e As EventArgs) Handles TextBox2.DoubleClick, TextBox8.DoubleClick, TextBox7.DoubleClick, TextBox6.DoubleClick, TextBox4.DoubleClick, TextBox3.DoubleClick
        sender.text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim f As New Hpridat()
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
        f.Dispose()

        zmena()
    End Sub

    Private Sub DataGridView3_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellDoubleClick
        If e.RowIndex > -1 Then
            Dim f As New Vydajka_prehlad
            f.vydajka = DataGridView3.Rows(e.RowIndex).Cells(0).Value
            f.ShowDialog()
            f.Dispose()
        End If
    End Sub

    Private Sub DataGridView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        If e.RowIndex > -1 Then
            Dim f As New Prijemka_prehlad
            f.prijemka = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            f.ShowDialog()
            f.Dispose()
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        zmena()

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged, RadioButton2.CheckedChanged, RadioButton1.CheckedChanged
        If sender.Checked Then
            zmena()
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim f As New Materialy()
        f.ShowDialog()
        zmena()

    End Sub

    Private Sub DataGridView1_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellMouseEnter
        If e.RowIndex > -1 AndAlso e.ColumnIndex = 1 Then

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim f As New Dodavatelia()
        f.ShowDialog()
        zmena()
    End Sub



End Class