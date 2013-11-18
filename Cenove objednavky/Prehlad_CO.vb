'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel
'Imports OfficeOpenXml
Imports System.IO
Imports System.Threading
Imports Bullzip.PdfWriter
Imports System.Drawing.Printing
Imports GemBox.Spreadsheet


Public Class PrehladO
    Private excelSheet As ExcelWorksheet
    Private riadok As Integer
    Private coo As String

    Private Sub Prehlad_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.CO' table. You can move, or remove it, as needed.
        Me.COTableAdapter.Fill(Me.RotekDataSet.CO)
        'TODO: This line of code loads data into the 'RotekDataSet.ZoznamF' table. You can move, or remove it, as needed.
        'Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        'TODO: This line of code loads data into the 'RotekDataSet.CP' table. You can move, or remove it, as needed.
        rozmers()
        napln()
        poverenie()
    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo

            Case Form78.admin
                Button1.Show()
                DataGridView1.Columns(11).Visible = True
                DataGridView1.Columns(10).Visible = True
                DataGridView1.Columns(15).Visible = True
                DataGridView1.Columns(16).Visible = True
            Case Form78.zakazkar
                Button1.Show()
                DataGridView1.Columns(11).Visible = False
                DataGridView1.Columns(10).Visible = True
                DataGridView1.Columns(15).Visible = True
                DataGridView1.Columns(16).Visible = True
            Case Form78.skladnik
                Button1.Hide()
                DataGridView1.Columns(11).Visible = False
                DataGridView1.Columns(10).Visible = False
                DataGridView1.Columns(15).Visible = False
                DataGridView1.Columns(16).Visible = False
            Case Else
                Button1.Hide()
                DataGridView1.Columns(11).Visible = False
                DataGridView1.Columns(10).Visible = False
                DataGridView1.Columns(15).Visible = False
                DataGridView1.Columns(16).Visible = False

        End Select
    End Sub
    Private Sub napln()
        Me.COBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}%' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%'", RotekDataSet.CO.pocetColumn, 1, RotekDataSet.CO.NazovColumn, TextBox1.Text, RotekDataSet.CO.PopisColumn, TextBox2.Text, RotekDataSet.CO.FirmaColumn, TextBox3.Text)
    End Sub
    Private Sub rozmers()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2, sw - 165)
        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString("Objednávky", Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2
        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New System.Drawing.Point(rw, 0)

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim f As New Pridat_CPO
        f.ShowDialog()
        f.Dispose()
        Me.COTableAdapter.Fill(Me.RotekDataSet.CO)

    End Sub

    Private Sub Prehlad_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged
        rozmers()

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox2.Text = ""
        DateTimePicker1.Value = Now

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        napln()

    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        napln()

    End Sub

    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged
        napln()
    End Sub
    Private Sub tlac(ByVal dokedy As String)
        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)

        Dim cp As String = DataGridView1.Rows(riadok).Cells(0).Value
        Me.COBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}= '{3}'", RotekDataSet.CO.pocetColumn, 2, RotekDataSet.CO.NazovColumn, cp)
        Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2}= '{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, DataGridView1.Rows(riadok).Cells(2).Value)

        If DataGridView2.RowCount = 0 Then
            Chyby.Show("Nič nezadané")
            Exit Sub
        End If

        Dim strPath As String = My.Settings.Rotek3 & "CO\" & cp.Replace("/", "•") & ".xls"
        Dim cesta As String = My.Settings.Rotek3 & "CO.xls"

        Dim fs As System.IO.FileStream

        Dim fileInUse As Boolean = True
        If File.Exists(strPath) Then
            Try
                ' If Open() succeeds, then we know the file is not currently in use.
                fs = System.IO.File.Open(strPath, FileMode.Open, FileAccess.Read, FileShare.None)
                fileInUse = False
                fs.Close()
            Catch en As Exception
                Chyby.Show("Subor pouziva iny pouzivatel")
                Exit Sub
            End Try

        End If
        If File.Exists(strPath) Then
            If MessageBox.Show("Už existuje chcete vytvoriť novú?", "Existuje", MessageBoxButtons.YesNo) = vbYes Then
            Else
                Process.Start(strPath)
                Exit Sub
            End If
        End If

        Try
            My.Computer.FileSystem.CopyFile(cesta, strPath, True)
        Catch ex As Exception
            Chyby.Show("Subor bol zmazany. Prosime vratit CO.xls do adresara " + cesta)
            Chyby.Show(ex.ToString)
            Exit Sub
        End Try
        'zaciatok
        Dim fufukeh As FileInfo = New FileInfo(strPath)
        'Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)
        Dim excelApp As ExcelFile = New ExcelFile


        Try
            excelApp.LoadXls(strPath, XlsOptions.PreserveAll)

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
        Try
            excelSheet = excelApp.Worksheets(0)

            Dim intNewRow As Int32 = 23
            Dim vyska As Integer = 38
            Dim strana As Integer = 32

            Dim i As Integer = 0

            Dim zaciatok As Integer = i
            Dim d As DateTime = New DateTime
            excelSheet.Cells("F17").Value = cp
            excelSheet.Cells("D15").Value = DataGridView1.Rows(riadok).Cells(9).Value
            Dim datumm As DateTime = DataGridView1.Rows(riadok).Cells(4).Value
            excelSheet.Cells("H15").Value = datumm.ToShortDateString
            excelSheet.Cells("D8").Value = DataGridView1.Rows(riadok).Cells(3).Value
            excelSheet.Cells("D9").Value = DataGridView1.Rows(riadok).Cells(2).Value
            excelSheet.Cells("D10").Value = DataGridView3.Rows(0).Cells(1).Value
            excelSheet.Cells("D11").Value = DataGridView3.Rows(0).Cells(2).Value
            excelSheet.Cells("D12").Value = DataGridView3.Rows(0).Cells(3).Value
            ' excelSheet.Cells("I51").Value = dokedy
            If DataGridView3.Rows(i).Cells(4).Value = "Slovensko" Then
            Else
                excelSheet.Cells("D13").Value = DataGridView3.Rows(i).Cells(4).Value
            End If

            For i = 0 To DataGridView2.RowCount - 1
                excelSheet.Cells("A" & intNewRow + i).Value = i + 1
                excelSheet.Cells.GetSubrange("B" & intNewRow + i, "C" & intNewRow + i).Merged = True
                excelSheet.Cells("B" & intNewRow + i).Value = DataGridView2.Rows(i).Cells(0).Value
                excelSheet.Cells("E" & intNewRow + i).Value = DataGridView2.Rows(i).Cells(1).Value
                excelSheet.Cells("G" & intNewRow + i).Value = CDec(DataGridView2.Rows(i).Cells(2).Value)
                excelSheet.Cells("A" & intNewRow + i).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin)
                excelSheet.Cells("B" & intNewRow + i).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin)
                excelSheet.Cells("E" & intNewRow + i).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin)
                excelSheet.Cells("G" & intNewRow + i).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin)
                excelSheet.Cells("I" & intNewRow + i).Formula = "=IF(E" & intNewRow + i & "*G" & intNewRow + i & "=0,"""",E" & intNewRow + i & "*G" & intNewRow + i & ")"
                excelSheet.Cells("I" & intNewRow + i).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin)
            Next
            Dim potom As Integer = DataGridView2.RowCount + 1
            If potom / strana - Math.Floor(potom / strana) > (strana - 2) / strana Then
                potom = (Math.Ceiling(potom / strana) + 1) * strana - 2 + intNewRow
            Else
                potom = Math.Ceiling(potom / strana) * strana - 2 + intNewRow
            End If
            excelSheet.Rows(potom - 2).Height = excelSheet.Rows(intNewRow + DataGridView2.RowCount).Height / 2
            excelSheet.Cells.GetSubrange("F" & potom, "G" & potom).Merged = True
            excelSheet.Cells.GetSubrange("A" & potom + 1, "G" & potom + 1).Merged = True
            excelSheet.Cells("F" & potom).Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
            excelSheet.Cells("F" & potom).Value = "Spolu:"
            excelSheet.Cells("I" & potom).Formula = "=IF(SUM(I" & intNewRow & ":I" & potom - 1 & ")=0,"""",SUM(I" & intNewRow & ":I" & potom - 1 & "))"
            excelSheet.Cells("I" & potom).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin)

            If RadioButton1.Checked = True Then
                excelSheet.Cells("A" & potom + 1).Value = "Termín dodania:"
                excelSheet.Cells("A" & potom + 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
                excelSheet.Cells("I" & potom + 1).Value = dokedy
                excelSheet.Cells("I" & potom + 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
                excelSheet.Cells("I" & potom + 1).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, GemBox.Spreadsheet.LineStyle.Thin)
            Else
                excelSheet.Cells("A" & potom + 1).Value = RichTextBox1.Text
            End If

            For i = DataGridView2.RowCount + intNewRow To potom - 1
                excelSheet.Cells.GetSubrange("B" & i, "C" & i).Merged = True
                excelSheet.Cells("I" & i).Formula = "=IF(E" & i & "*G" & i & "=0,"""",E" & i & "*G" & i & ")"

            Next

            'strPath = strPath.Substring(0, strPath.Length - 1)
            excelApp.SaveXls(strPath)
            'excelApp.SaveAs(fufukeh)

            'Dim xls As New Spreadsheet.ExcelFile()
            'xls.Loadxls(strPath, Spreadsheet.xlsOptions.PreserveMakeCopy)

            'strPath = strPath.Substring(0, strPath.Length - 1)
            'xls.SaveXls(strPath)

            '|            Dim mmm As New FileInfo(strPath)
            '            Dim excelpackage As ExcelPackage = New ExcelPackage(fufukeh)
            '            excelpackage.SaveAs(mmm)


            'excelApp.Workbooks.Open(strPath)
            '   excelSheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, strPath.Replace(".xls", ""), XlFixedFormatQuality.xlQualityStandard, True, True, 1, 1, True)

            Prehlad.img_excel(strPath, "acert.png")

            Process.Start(strPath)

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try


    End Sub
    Public Shared Sub ukonci_excel(ByVal hwnd As Int32)
        Try
            PostMessage(hwnd, &H12, 0, 0)
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Int32, ByVal wMsg As Int32, _
ByVal wParam As Int32, ByVal lParam As Int32) As Int32



    Private Sub subory(ByVal cesta As String)
        GroupBox2.Text = cesta
        GroupBox2.Show()
        Button5.Show()
        Button6.Show()
        ListView1.Clear()
        Dim cesta2 As String
        cesta2 = My.Settings.Rotek3 + "CO\"
        cesta2 = cesta2 & coo.Replace("/", "•") & "\"

        If IsDBNull(DataGridView1.Rows(GroupBox2.Tag).Cells(14).Value) = False Then
            ListView1.Items.Add("Papiere:")
            Dim potvrdenia As String = DataGridView1.Rows(GroupBox2.Tag).Cells(14).Value
            While potvrdenia.IndexOf("|") >= 0
                ListView1.Items.Add(potvrdenia.Substring(potvrdenia.LastIndexOf("|") + 1))
                potvrdenia = potvrdenia.Substring(0, potvrdenia.LastIndexOf("|"))
            End While
        End If
        ListView1.Items.Add("Súbory:")
        Try
            For Each files In System.IO.Directory.GetFiles(cesta2)
                ListView1.Items.Add(files.Substring(files.LastIndexOf("\") + 1), "")
            Next
        Catch ex As Exception

        End Try


    End Sub
    Private Sub DataGridView1_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex > -1 Then
            Dim f As New Zobraz_CPO
            f.cp = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            f.ShowDialog()
            f.Dispose()
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        tlac("")
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        tlac(MonthCalendar1.SelectionEnd.ToString("yyyy-MM-dd"))
        If RadioButton1.Checked = True Then
            Dim sql As String = "UPDATE CO SET DU='" & MonthCalendar1.SelectionEnd.ToString("yyyy-MM-dd") & "' WHERE pocet=1 AND Nazov='" & DataGridView1.Rows(riadok).Cells(0).Value & "'"
            Form78.sqa(sql)
        End If
        Me.COTableAdapter.Fill(Me.RotekDataSet.CO)
        GroupBox1.Hide()

    End Sub

    Private Sub Prehlad_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseClick
        zmizni()
    End Sub
    Private Sub zmizni()
        If (GroupBox1.Visible) And ((Cursor.Position.X < GroupBox1.Location.X) Or (Cursor.Position.X > (GroupBox1.Location.X + GroupBox1.Size.Width)) Or (Cursor.Position.Y < GroupBox1.Location.Y) Or (Cursor.Position.Y > (GroupBox1.Location.Y + GroupBox1.Size.Height))) Then
            GroupBox1.Hide()
        End If
        If (GroupBox2.Visible) And ((Cursor.Position.X < GroupBox2.Location.X) Or (Cursor.Position.X > (GroupBox2.Location.X + GroupBox2.Size.Width)) Or (Cursor.Position.Y < GroupBox2.Location.Y) Or (Cursor.Position.Y > (GroupBox2.Location.Y + GroupBox2.Size.Height))) Then
            GroupBox2.Hide()
        End If
    End Sub

    Private Sub DataGridView1_Click(sender As System.Object, e As System.EventArgs) Handles DataGridView1.Click
        zmizni()
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        If Form78.heslo = Form78.admin Or Form78.heslo = Form78.zakazkar Then
        Else
            Exit Sub
        End If

        Dim text As String = ""
        ListView1.Items.Add("Papiere:")
        If IsDBNull(DataGridView1.Rows(GroupBox2.Tag).Cells(14).Value) = False Then
            text = DataGridView1.Rows(GroupBox2.Tag).Cells(14).Value
        End If

        Dim slovo As String = InputBox("Napis identifikaciu cenovej ponuky", "Cenova ponuka")
        If slovo.Length > 0 Then
            text = text + "|" + slovo
            Dim sql As String
            sql = "UPDATE CO SET CP='" + text + "' WHERE Nazov='" + coo + "' AND pocet=" & 1
            Form78.sqa(sql)
            ListView1.Items.Add(slovo)
            Me.COTableAdapter.Fill(Me.RotekDataSet.CO)
            napln()
        End If
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        If Form78.heslo = Form78.admin Or Form78.heslo = Form78.zakazkar Then
        Else
            Exit Sub
        End If


        Dim cesta2 As String = My.Settings.Rotek3
        cesta2 = cesta2 & "CO\" + coo.Replace("/", "•") + "\"


        Dim openFileDialog1 As New OpenFileDialog
        openFileDialog1.Title = "Pridať súbory"
        openFileDialog1.InitialDirectory = "\\192.168.1.150"
        openFileDialog1.Filter = "Všetky súbory(*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Multiselect = True
        If openFileDialog1.ShowDialog() = DialogResult.OK Then

            For Each sl As String In openFileDialog1.FileNames
                If System.IO.Directory.Exists(cesta2) Then
                Else
                    System.IO.Directory.CreateDirectory(cesta2)
                End If
                My.Computer.FileSystem.CopyFile(sl, cesta2 + sl.Substring(sl.LastIndexOf("\") + 1), True)
                ListView1.Items.Add(sl.Substring(sl.LastIndexOf("\") + 1))
            Next
        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles ListView1.DoubleClick
        Dim cesta2 As String = My.Settings.Rotek3 + "CO\" + coo.Replace("/", "•") & "\"
        cesta2 = cesta2 & ListView1.SelectedItems(0).Text
        cesta2 = cesta2.Replace("/", "\")
        cesta2 = cesta2.Replace("\\", "\")
        cesta2 = "\" + cesta2

        If System.IO.File.Exists(cesta2) Then
            If cesta2.LastIndexOf(".") > 20 Then
                Process.Start(cesta2)
            End If
        ElseIf ListView1.SelectedItems(0).Text = "Súbory:" Then
            Shell("explorer " & cesta2.Replace(ListView1.SelectedItems(0).Text, ""))
        Else
            Shell("explorer " & cesta2)
        End If

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If (e.RowIndex <> -1) Then
            'Vyrobit
            If (e.ColumnIndex = 8) Then
                Me.riadok = e.RowIndex
                Dim point3 As New Point(CInt(Math.Round(CDec((Cursor.Position.X - (CDec(Me.GroupBox1.Size.Width) / 2))))), (180 + (Me.DataGridView1.Rows.Item(0).Height * (e.RowIndex + 1))))
                Me.GroupBox1.Location = point3
                Me.GroupBox1.Show()
                If IsDBNull(Me.DataGridView1.Rows.Item(Me.riadok).Cells.Item(6).Value) Then
                Else
                    Me.MonthCalendar1.SelectionEnd = Me.DataGridView1.Rows.Item(Me.riadok).Cells.Item(6).Value
                End If
                'Upravit
            ElseIf (e.ColumnIndex = 10) Then
                Dim t_cpo As New Pridat_CPO
                t_cpo.co = Me.DataGridView1.Rows.Item(e.RowIndex).Cells.Item(0).Value
                t_cpo.ShowDialog()
                t_cpo.Dispose()
                Me.COTableAdapter.Fill(Me.RotekDataSet.CO)
                'Zmazat
            ElseIf (e.ColumnIndex = 11) Then
                Form78.sqa("DELETE FROM CO WHERE Nazov='" & Me.DataGridView1.Rows.Item(e.RowIndex).Cells.Item(0).Value & "'")
                Me.COTableAdapter.Fill(Me.RotekDataSet.CO)
                'Otvorit
            ElseIf (e.ColumnIndex = 12) Then
                Me.riadok = e.RowIndex
                Dim co As String = DataGridView1.Rows(riadok).Cells(0).Value
                Dim strPath As String = My.Settings.Rotek3 & "CO\" & co.Replace("/", "•") & ".xls"
                If File.Exists(strPath) Then
                    Process.Start(strPath)
                Else
                    Me.riadok = e.RowIndex
                    Dim point3 As New Point(CInt(Math.Round(CDec((Cursor.Position.X - (CDec(Me.GroupBox1.Size.Width) / 2))))), (180 + (Me.DataGridView1.Rows.Item(0).Height * (e.RowIndex + 1))))
                    Me.GroupBox1.Location = point3
                    Me.GroupBox1.Show()
                    If IsDBNull(Me.DataGridView1.Rows.Item(Me.riadok).Cells.Item(6).Value) Then
                    Else
                        Me.MonthCalendar1.SelectionEnd = Me.DataGridView1.Rows.Item(Me.riadok).Cells.Item(6).Value
                    End If
                End If
                'Cenove ponuky
            ElseIf (e.ColumnIndex = 13) Then
                Me.coo = Me.DataGridView1.Rows.Item(e.RowIndex).Cells.Item(0).Value
                Dim point2 As Point
                point2 = New Point(CInt(Math.Round(CDec((Cursor.Position.X - (CDec(Me.GroupBox1.Size.Width) / 2))))), (200 + (Me.DataGridView1.Rows.Item(0).Height * (e.RowIndex + 1))))
                Me.GroupBox2.Location = point2
                Me.GroupBox2.Tag = e.RowIndex
                Me.subory("Cenov" & ChrW(233) & " ponuky")
                'Skopirovat
            ElseIf (e.ColumnIndex = 16) Then
                skopiruj(e.RowIndex)
                'Poslat mailom
            ElseIf (e.ColumnIndex = 15) Then
                posli_mail(e.RowIndex)
            End If
        End If

    End Sub
    Private Sub posli_mail(ByVal riadok As Integer)
        Me.riadok = riadok
        Dim priloha As String = (My.Settings.Rotek3 & "CO\" & Me.DataGridView1.Rows.Item(riadok).Cells.Item(0).Value.ToString.Replace("/", ChrW(8226)) & ".xls")
        If File.Exists(priloha) Then

            Try

                tlacDoPdf(priloha)

                priloha = priloha.Replace(".xls", ".pdf")
                Thread.Sleep(2000)
                'If (File.Exists(priloha)) Then
                'Else
                '    Chyby.Show("Nepodaril sa export do PDF")
                '    Exit Sub
                'End If

                Dim str4 As String
                If IsDBNull(Me.DataGridView1.Rows.Item(riadok).Cells.Item(3).Value) Then
                    str4 = "0"
                Else
                    str4 = Me.DataGridView1.Rows.Item(riadok).Cells.Item(3).Value
                End If


                Dim mail As New Mail(priloha, "Posielam objednávku " & Me.DataGridView1.Rows.Item(riadok).Cells.Item(0).Value, DataGridView1.Rows.Item(riadok).Cells.Item(2).Value, str4)
                mail.ShowDialog()
                mail.Dispose()
                Exit Sub
            Catch exception7 As Exception
                Chyby.Show((exception7.ToString))
                Exit Sub
            End Try
        Else
            Dim point2 As Point
            point2 = New Point(CInt(Math.Round(CDec((Cursor.Position.X - (CDec(Me.GroupBox1.Size.Width) / 2))))), (180 + (Me.DataGridView1.Rows.Item(0).Height * (riadok + 1))))
            Me.GroupBox1.Location = point2
            Me.GroupBox1.Show()
            If Not IsDBNull(Me.DataGridView1.Rows.Item(Me.riadok).Cells.Item(6).Value) Then
                Me.MonthCalendar1.SelectionEnd = Me.DataGridView1.Rows.Item(Me.riadok).Cells.Item(6).Value
            End If
        End If
    End Sub
    Private Sub skopiruj(ByVal riadok As Integer)
        Dim nazov As String = DataGridView1.Rows(riadok).Cells(0).Value
        Dim cena, co, popis, Firma, veduci As String
        popis = DataGridView1.Rows(riadok).Cells(1).Value
        Firma = DataGridView1.Rows(riadok).Cells(2).Value
        veduci = DataGridView1.Rows(riadok).Cells(3).Value
        cena = DataGridView1.Rows(riadok).Cells(5).Value
        Dim datum As DateTime
        datum = DataGridView1.Rows(riadok).Cells(4).Value

        Dim sql As String
        co = COTableAdapter.MaxNazov
        co = co.Replace("OB ", "")
        co = co.Replace("/" & Year(Now).ToString.Substring(2), "")
        Try
            Dim i As Integer = co
            co = "OB " & Format(i + 1, "0000") & "/" & Year(Now).ToString.Substring(2)
        Catch ex As Exception
            Chyby.Show("Chyba")
            Exit Sub
        End Try

        sql = "INSERT INTO CO (Nazov, Popis, Firma, Veduci, Datum, Cena, Poznamka, pocet, Evidoval) VALUES('" & co & "','" & popis & "','" & Firma & "','" & veduci & "', '" & datum.ToString("yyyy-MM-dd") & "' ,'" & cena & "','" & "-" & "'," & 1 & ",'" & Form78.uzivatel & "')"
        Form78.sqa(sql)
        Me.COBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}= '{3}'", RotekDataSet.CO.pocetColumn, 2, RotekDataSet.CO.NazovColumn, nazov)
        For i As Integer = 0 To DataGridView2.RowCount - 1
            Dim polozka, kusov, cena1 As String
            polozka = DataGridView2.Rows(i).Cells(0).Value
            kusov = DataGridView2.Rows(i).Cells(1).Value
            cena1 = DataGridView2.Rows(i).Cells(2).Value
            sql = "INSERT INTO CO (Nazov, Polozka, Kusov, Datum, Cena, pocet, Evidoval) VALUES('" & co & "','" & polozka & "','" & kusov & "','" & datum.ToString("yyyy-MM-dd") & "','" & cena1 & "'," & 2 & ",'" & Form78.uzivatel & "')"
            Form78.sqa(sql)
        Next
        Chyby.Show("Skopírovaná ako objednavka <" & co & ">")
        Me.COTableAdapter.Fill(Me.RotekDataSet.CO)

    End Sub

    Public Shared Sub sleduj(ByVal cesta As String)
        Dim i As Integer = -1
        While Not File.Exists(cesta)
            Thread.Sleep(2000)
            'Console.WriteLine(cesta)

        End While
        'Console.WriteLine(cesta)
        cesta = cesta.Substring(cesta.LastIndexOf("\") + 1)
        cesta = cesta.Substring(0, cesta.LastIndexOf("."))
        While (1)

            Dim processes() As Process = Process.GetProcessesByName("EXCEL")
            Dim proc As Process

            For Each proc In processes
                'Chyby.Show(proc.MainWindowTitle)
                If proc.MainWindowTitle.IndexOf(cesta) > -1 Then
                    proc.Kill()
                    Exit Sub
                End If
            Next
        End While
    End Sub


    Public Shared Sub tlacDoPdf(ByVal cesta As String)

        Try
            Dim fileName As String = cesta
            cesta = cesta.Substring(0, cesta.LastIndexOf(".")) & ".pdf"
            If (File.Exists(cesta)) Then
                File.Delete(cesta)
            End If
            Dim teraz As DateTime = DateTime.Now
            Dim nazov As String = "0"
            Dim PdfSettings As PdfSettings = New PdfSettings()
            Dim j As Integer = 0

            While nazov = "0" Or nazov = "1"
                For Each meno As String In PrinterSettings.InstalledPrinters
                    If meno.ToLower.IndexOf("bullzip") > -1 Then
                        nazov = meno
                        Exit For
                    End If
                Next

                If nazov = "0" Then
                    '            Dim WSHShell = CreateObject("WScript.Shell")
                    Dim trd As New Thread(Sub() Chyby.Show("Nepodarilo sa nájsť virtuálnu tlačiareň. Už sa zapla inštalácia, po nainštalovní opakujte akciu a už to bude fungovať (+- 2 minuty). "))
                    trd.IsBackground = True
                    trd.Start()
                    Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                    Dim p As Process = New Process()
                    p.StartInfo.FileName = "\\192.168.1.150\Sklad\BullzipPDFPrinter.msi"
                    p.StartInfo.Arguments = "/quiet WRAPPED_ARGUMENTS=""/VERYSILENT /SUPPRESSMSGBOXES /PRESERVEDEFAULTPRINTER   """
                    p.Start()
                    p.WaitForExit()
                    Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    If p.ExitCode = 0 Then
                    Else
                        Chyby.Show("Nepodarilo sa nainstalova tlaciaren. Nainštalujte ručne (\\192.168.1.150\Sklad\BullzipPDFPrinter.msi)")
                        Exit Sub
                    End If
                End If
            End While

            'PdfSettings.PrinterName = "\\192.168.1.150\Bullzip PDF Printer"
            PdfSettings.PrinterName = nazov
            PdfSettings.SetValue("Output", cesta)
            PdfSettings.SetValue("ShowPDF", "yes")
            PdfSettings.SetValue("ShowSettings", "never")
            PdfSettings.SetValue("ShowSaveAS", "never")
            PdfSettings.SetValue("ShowProgress", "no")
            PdfSettings.SetValue("ShowProgressFinished", "no")
            PdfSettings.SetValue("ConfirmOverwrite", "no")
            PdfSettings.WriteSettings(PdfSettingsFileType.RunOnce)

            Dim thrd As New Thread(AddressOf sleduj)
            thrd.IsBackground = True
            thrd.Start(cesta)
            PdfUtil.PrintFile(fileName, nazov)

        Catch ex As Exception

            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Public Shared Sub AddNetworkPrinter(ByRef printerName As String, _
    Optional ByRef useExistingDriver As Boolean = 0, Optional ByRef setDefaultPrinter As Boolean = 0)

        Dim cmdToSend As String = "rundll32 printui.dll,PrintUIEntry /in /n " & Chr(34) & printerName & Chr(34)
        If useExistingDriver Then cmdToSend += " /u" '  /u = use the existing printer driver if it's already installed
        If setDefaultPrinter Then cmdToSend += " /y" '  /y = set printer as the default
        Shell(cmdToSend, AppWinStyle.Hide) ' execute the command

    End Sub

    Private Sub UpraviťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpraviťToolStripMenuItem.Click
        Dim t_cpo As New Pridat_CPO
        t_cpo.co = Me.DataGridView1.Rows.Item(ContextMenuStrip1.Tag).Cells.Item(0).Value
        t_cpo.ShowDialog()
        t_cpo.Dispose()
        Me.COTableAdapter.Fill(Me.RotekDataSet.CO)

    End Sub

    Private Sub TlačiťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TlačiťToolStripMenuItem.Click
        Me.riadok = ContextMenuStrip1.Tag
        Dim point3 As New Point(CInt(Math.Round(CDec((Cursor.Position.X - (CDec(Me.GroupBox1.Size.Width) / 2))))), (180 + (Me.DataGridView1.Rows.Item(0).Height * (ContextMenuStrip1.Tag + 1))))
        Me.GroupBox1.Location = point3
        Me.GroupBox1.Show()

        If IsDBNull(Me.DataGridView1.Rows.Item(Me.riadok).Cells.Item(6).Value) Then
        Else
            Me.MonthCalendar1.SelectionEnd = Me.DataGridView1.Rows.Item(Me.riadok).Cells.Item(6).Value
        End If

    End Sub

    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp
        If e.Button = MouseButtons.Right Then
            If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
                Me.DataGridView1.CurrentCell = Me.DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
            End If

            If Form78.heslo = Form78.admin OrElse Form78.heslo = Form78.zakazkar Then
                ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
                ContextMenuStrip1.Tag = e.RowIndex
                If Form78.heslo = Form78.zakazkar Then
                    ContextMenuStrip1.Items(4).Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub ZmazaťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZmazaťToolStripMenuItem.Click
        If MessageBox.Show("Naozaj chcete zmazať " & Me.DataGridView1.Rows.Item(ContextMenuStrip1.Tag).Cells.Item(0).Value & " ?", "Zmazať", MessageBoxButtons.YesNo) = vbYes Then
            Form78.sqa("DELETE FROM CO WHERE Nazov='" & Me.DataGridView1.Rows.Item(ContextMenuStrip1.Tag).Cells.Item(0).Value & "'")
            Me.COTableAdapter.Fill(Me.RotekDataSet.CO)
        End If
    End Sub

    Private Sub OtvoriťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OtvoriťToolStripMenuItem.Click
        Me.riadok = ContextMenuStrip1.Tag
        Dim co As String = DataGridView1.Rows(riadok).Cells(0).Value
        Dim strPath As String = My.Settings.Rotek3 & "CO\" & co.Replace("/", "•") & ".xls"
        If File.Exists(strPath) Then
            Process.Start(strPath)
        Else
            Dim point3 As New Point(CInt(Math.Round(CDec((Cursor.Position.X - (CDec(Me.GroupBox1.Size.Width) / 2))))), (180 + (Me.DataGridView1.Rows.Item(0).Height * (ContextMenuStrip1.Tag + 1))))
            Me.GroupBox1.Location = point3
            Me.GroupBox1.Show()
            If IsDBNull(Me.DataGridView1.Rows.Item(Me.riadok).Cells.Item(6).Value) Then
            Else
                Me.MonthCalendar1.SelectionEnd = Me.DataGridView1.Rows.Item(Me.riadok).Cells.Item(6).Value
            End If
        End If

    End Sub

    Private Sub CenovePonukyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CenovePonukyToolStripMenuItem.Click
        Me.coo = Me.DataGridView1.Rows.Item(ContextMenuStrip1.Tag).Cells.Item(0).Value
        Dim point2 As Point
        point2 = New Point(CInt(Math.Round(CDec((Cursor.Position.X - (CDec(Me.GroupBox1.Size.Width) / 2))))), (200 + (Me.DataGridView1.Rows.Item(0).Height * (ContextMenuStrip1.Tag + 1))))
        Me.GroupBox2.Location = point2
        Me.GroupBox2.Tag = ContextMenuStrip1.Tag
        Me.subory("Cenov" & ChrW(233) & " ponuky")
    End Sub

    Private Sub SkopírovaťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SkopírovaťToolStripMenuItem.Click
        skopiruj(ContextMenuStrip1.Tag)

    End Sub

    Private Sub PoslaťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PoslaťToolStripMenuItem.Click
        posli_mail(ContextMenuStrip1.Tag)
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            RichTextBox1.Enabled = False
            MonthCalendar1.Enabled = True
        Else
            RichTextBox1.Enabled = True
            MonthCalendar1.Enabled = False
        End If
    End Sub

    Private Sub GroupBox1_VisibleChanged(sender As Object, e As EventArgs) Handles GroupBox1.VisibleChanged
        If GroupBox1.Visible = True Then
            RadioButton1.Checked = True
        End If
        If GroupBox1.Location.X < 0 Then
            GroupBox1.Location = New Point(1, GroupBox1.Location.Y)
        End If
        If GroupBox1.Location.Y + GroupBox1.Size.Height > Me.Height Then
            GroupBox1.Location = New Point(GroupBox1.Location.X, GroupBox1.Location.Y - GroupBox1.Height - 70)
        End If

    End Sub
End Class
