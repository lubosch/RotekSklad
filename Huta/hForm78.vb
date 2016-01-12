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

        DataGridView1.Size = New Size((rww + 40), (sw - 280))
        DataGridView1.Location = New Point(0, 199)

        DataGridView2.Location = New Point((rww + 40), 199)
        DataGridView2.Size = New Size((rww - 50), (sw - 240) / 2)

        Label6.Location = New Point((rww + 40), 179)
        NumericUpDown1.Location = New Point((rww + 80), 179)

        DataGridView3.Location = New Point((rww + 40), 199 + (sw - 240) / 2)
        DataGridView3.Size = New Size((rww - 50), (sw - 240) / 2)

        Dim strSz As SizeF = g.MeasureString(Label1.Text, Label1.Font)
        Dim stred As Integer

        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred

        Label1.Location = New Point(rw, 10)

    End Sub
    Private Sub poverenie()
        Dim dd As List(Of Dictionary(Of String, Boolean)) = New List(Of Dictionary(Of String, Boolean))
        Dim d As Dictionary(Of String, Boolean)

        dd = Povolenia_SQL.getRights("Huta")
        Select Case Form78.heslo
            Case Form78.admin
                d = dd(0)
            Case Form78.zakazkar
                d = dd(1)
            Case Form78.skladnik
                d = dd(2)
            Case Else
                d = dd(3)
        End Select

        If (Not d("Pridávať")) Then
            Button5.Hide()
        End If
        If (Not d("Meniť dodávateľov")) Then
            Button2.Hide()
        End If
        If (Not d("Pridávať")) Then
            Button1.Hide()
        End If
        If (Not d("Odoberať")) Then
            Button8.Hide()
        End If
        If (Not d("Mazať materiál")) Then
            DataGridView1.Columns("Zmazat").Visible = False
        End If
        If (Not d("Vyhadzovať do odpadov")) Then
            DataGridView1.Columns("Vkusov").Visible = False
            DataGridView1.Columns("VKbutton").Visible = False
        End If


        dd = Povolenia_SQL.getRights("Príjemka")
        Select Case Form78.heslo
            Case Form78.admin
                d = dd(0)
            Case Form78.zakazkar
                d = dd(1)
            Case Form78.skladnik
                d = dd(2)
            Case Else
                d = dd(3)
        End Select

        If (Not d("Upravovať")) Then
            ContextMenuStrip2.Items(0).Visible = False
        End If



        dd = Povolenia_SQL.getRights("Výdajka")
        Select Case Form78.heslo
            Case Form78.admin
                d = dd(0)
            Case Form78.zakazkar
                d = dd(1)
            Case Form78.skladnik
                d = dd(2)
            Case Else
                d = dd(3)
        End Select

        If (Not d("Upravovať")) Then
            ContextMenuStrip1.Items(0).Visible = False
        End If


    End Sub

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        poverenie()
        MaterialBindingSource.Sort = "Nazov, Druh, Typ"
        NumericUpDown1.Value = Date.Now.Year.ToString().Substring(2)
        rozmers()
        zmena()
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
        start_zmena_timer(TextBox1)
    End Sub



    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        start_zmena_timer(TextBox2)
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
        Debug.WriteLine(stopky.ElapsedMilliseconds)

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
                    filt = String.Format("{0} AND m.{1} {3} '{2}'", filt, RotekDataSet.Huta.RozmerColumn, c, znamienko)
                Catch ex As Exception
                    c = 0
                End Try
            End If
            If TextBox6.Text.Length <> 0 Then


                Try
                    ccc = TextBox6.Text
                    filt = String.Format("{0} AND m.{1} {3} '{2}'", filt, RotekDataSet.Huta.VelkostColumn, ccc, znamienko)
                Catch ex As Exception
                    ccc = 0
                End Try
            End If
            If TextBox7.Text.Length <> 0 Then
                Try
                    cc = TextBox7.Text
                    filt = String.Format("{0} AND m.{1} {3} '{2}'", filt, RotekDataSet.Huta.SirkaColumn, cc, znamienko)
                Catch ex As Exception
                    cc = 0
                End Try
            End If
            If TextBox8.Text.Length <> 0 Then
                Try
                    cccc = TextBox8.Text
                    filt = String.Format("{0} AND m.{1} {3} '{2}'", filt, RotekDataSet.Huta.S_rozmerColumn, cccc, znamienko)
                Catch ex As Exception
                    cccc = 0
                End Try
            End If
            filt = String.Format("{0} AND 1=1", filt)

            Debug.WriteLine("Pred materialom " & stopky.ElapsedMilliseconds)
            Me.MaterialTableAdapter.FillFiltered(Me.RotekDataSet.Material, druh, typ, nazov, filt)

            'Material_SQL.fillFiltered(Me.RotekDataSet.Material, filt, nazov)
            Debug.WriteLine("Material " & stopky.ElapsedMilliseconds)
            Me.VydajkyTableAdapter.fillFiltered(Me.RotekDataSet.Vydajky, NumericUpDown1.Value, druh, typ, nazov, filt)
            Debug.WriteLine("Vydajka " & stopky.ElapsedMilliseconds)
            Me.PrijemkyTableAdapter.fillFiltered(Me.RotekDataSet.Prijemky, typ, nazov, NumericUpDown1.Value, "rok2", druh, filt)
            Debug.WriteLine("Prijemka " & stopky.ElapsedMilliseconds)
            stopky.Stop()
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        start_zmena_timer(TextBox4)
    End Sub

    Private Function getMaterialByRow(riadok As Integer) As Material_SQL
        'Dim druh, nazov, rozmer, sirka, s_rozmer, velkost, typ As String
        'druh = DataGridView1.Rows(riadok).Cells("Druh").Value
        'nazov = DataGridView1.Rows(riadok).Cells("Nazov").Value
        'sirka = DataGridView1.Rows(riadok).Cells("Sirka").Value
        'rozmer = DataGridView1.Rows(riadok).Cells("Rozmer").Value
        's_rozmer = DataGridView1.Rows(riadok).Cells("S_rozmer").Value
        'velkost = DataGridView1.Rows(riadok).Cells("Velkost").Value
        'typ = DataGridView1.Rows(riadok).Cells("Typ").Value
        Dim material_id = DataGridView1.Rows(riadok).Cells("ID").Value
        Dim material As Material_SQL = New Material_SQL(material_id)
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
                zmena()
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
        material.throwOut(kusov)
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
        If e.ColumnIndex <= 3 OrElse e.ColumnIndex >= 8 OrElse (e.Value Is Nothing OrElse e.Value <> -1) Then
        Else
            e.Value = ""
        End If
        If e.ColumnIndex = 9 And DataGridView1.Rows(e.RowIndex).Cells("Typ").Value = "Plech" Then
            e.Value = e.Value / 10000
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
        start_zmena_timer(TextBox3)

    End Sub
    Private Sub start_zmena_timer(text_box As TextBox)
        Timer1.Stop()
        If text_box.Text.Length >= 3 Then
            Timer1.Interval = 500
        Else
            Timer1.Interval = 1500
        End If
        Timer1.Start()

    End Sub
    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        start_zmena_timer(TextBox7)

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        start_zmena_timer(TextBox6)

    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        start_zmena_timer(TextBox8)

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
                Case 1
                    TextBox1.Text = slovo
                Case 2
                    TextBox2.Text = slovo
                Case 3
                    TextBox3.Text = slovo
                Case 4
                    TextBox7.Text = slovo
                Case 5
                    TextBox4.Text = slovo
                Case 6
                    TextBox8.Text = slovo
                Case 7
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
        zmena()
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim f As New Dodavatelia()
        f.ShowDialog()
        zmena()
    End Sub



    Private Sub DataGridView3_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView3.CellMouseUp
        If e.Button = MouseButtons.Right And e.RowIndex > -1 Then
            ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
            ContextMenuStrip1.Tag = e.RowIndex
            DataGridView3.ClearSelection()
            DataGridView3.Rows(e.RowIndex).Selected = True

        End If


    End Sub

    Private Sub UpraviťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpraviťToolStripMenuItem.Click
        Dim nazov As String = DataGridView3.Rows(ContextMenuStrip1.Tag).Cells(0).Value
        Dim f As New Hvydat(nazov)
        f.ShowDialog()
        f.Dispose()
        zmena()
    End Sub

    Private Sub DataGridView2_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseUp
        If e.Button = MouseButtons.Right Then
            ContextMenuStrip2.Show(MousePosition.X, MousePosition.Y)
            ContextMenuStrip2.Tag = e.RowIndex
            DataGridView2.ClearSelection()
            DataGridView2.Rows(e.RowIndex).Selected = True
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim nazov As String = DataGridView2.Rows(ContextMenuStrip2.Tag).Cells(0).Value
        Dim f As New Hpridat(nazov)
        f.ShowDialog()
        f.Dispose()
        zmena()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim f As New Odpadky
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        zmena()
    End Sub

    Private Sub TextBox6_Enter(sender As Object, e As EventArgs) Handles TextBox6.Enter
        MaterialBindingSource.Sort = "Velkost, " & MaterialBindingSource.Sort
    End Sub

    Private Sub TextBox8_Enter(sender As Object, e As EventArgs) Handles TextBox8.Enter
        MaterialBindingSource.Sort = "S_rozmer, " & MaterialBindingSource.Sort

    End Sub

    Private Sub TextBox4_Enter(sender As Object, e As EventArgs) Handles TextBox4.Enter
        MaterialBindingSource.Sort = "Rozmer, " & MaterialBindingSource.Sort

    End Sub

    Private Sub TextBox7_Enter(sender As Object, e As EventArgs) Handles TextBox7.Enter
        MaterialBindingSource.Sort = "Sirka, " & MaterialBindingSource.Sort

    End Sub

    Private Sub TextBox3_Enter(sender As Object, e As EventArgs) Handles TextBox3.Enter
        MaterialBindingSource.Sort = "Typ, " & MaterialBindingSource.Sort

    End Sub

    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        MaterialBindingSource.Sort = "Nazov, " & MaterialBindingSource.Sort

    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        MaterialBindingSource.Sort = "Druh, " & MaterialBindingSource.Sort

    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        zmena()
    End Sub

    Private Sub Button9_Click_1(sender As Object, e As EventArgs) Handles Button9.Click
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Selected").Value = True Then
                Dim material As Material_SQL = getMaterialByRow(row.Index)
                material.throwOut(row.Cells("Kusov").Value)
            End If
        Next
        zmena()
    End Sub

    Private Sub Button10_Click_1(sender As Object, e As EventArgs) Handles Button10.Click
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("VKusov").Value IsNot Nothing Then
                
                Vyhod_Material(row.Index)
            End If
        Next
        zmena()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        If e.ColumnIndex = DataGridView1.Columns("Selected").Index Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.Cells("Selected").Value = 1 - row.Cells("Selected").Value
            Next
        End If
    End Sub
End Class