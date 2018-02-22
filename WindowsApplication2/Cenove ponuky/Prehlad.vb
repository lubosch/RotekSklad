'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Outlook
'Imports Microsoft.Office.Interop.Excel
Imports System.IO
'Imports OfficeOpenXml
'Imports SautinSoft
Imports GemBox.Spreadsheet
Imports NPOI
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel

Public Class Prehlad
    Private excelSheet As ExcelWorksheet
    Private riadok As Integer

    Private Sub Prehlad_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim timer As Stopwatch = New Stopwatch()
        timer.Start()
        'TODO: This line of code loads data into the 'RotekDataSet.ZoznamF' table. You can move, or remove it, as needed.
        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        'TODO: This line of code loads data into the 'RotekDataSet.CP' table. You can move, or remove it, as needed.
        Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
        'CPBindingSource.Sort = "Nazov DESC"
        rozmers()
        napln()
        poverenie()

        NumericUpDown1.Value = Now.Year - 2000

    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo

            Case Form78.admin
                Button1.Show()
                DataGridView1.Columns(11).Visible = True
                DataGridView1.Columns(12).Visible = True
                DataGridView1.Columns(13).Visible = True
                DataGridView1.Columns(15).Visible = True
                DataGridView1.Columns(16).Visible = True
            Case Form78.zakazkar
                Button1.Show()
                DataGridView1.Columns(11).Visible = True
                DataGridView1.Columns(12).Visible = False
                DataGridView1.Columns(13).Visible = True
                DataGridView1.Columns(15).Visible = True
                DataGridView1.Columns(16).Visible = True
            Case Form78.skladnik
                Button1.Hide()
                DataGridView1.Columns(11).Visible = False
                DataGridView1.Columns(12).Visible = False
                DataGridView1.Columns(13).Visible = False
                DataGridView1.Columns(15).Visible = False
                DataGridView1.Columns(16).Visible = False
            Case Else
                Button1.Hide()
                DataGridView1.Columns(11).Visible = False
                DataGridView1.Columns(12).Visible = False
                DataGridView1.Columns(13).Visible = False
                DataGridView1.Columns(15).Visible = False
                DataGridView1.Columns(16).Visible = False
        End Select
    End Sub
    Private Sub napln()

        Me.CPBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}%' AND {2} LIKE '%{4}' AND {5} LIKE '{6}%' AND {7} LIKE '{8}%'", RotekDataSet.CP.pocetColumn, 1, RotekDataSet.CP.NazovColumn, TextBox1.Text, NumericUpDown1.Value.ToString, RotekDataSet.CP.PopisColumn, TextBox2.Text, RotekDataSet.CP.FirmaColumn, TextBox3.Text)
    End Sub
    Private Sub rozmers()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2, sw - 185)
        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString("Cenové ponuky", Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2
        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New System.Drawing.Point(rw, 0)

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim f As New Pridat_CP
        f.ShowDialog()
        f.Dispose()
        Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)

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
        Me.CPBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}= '{3}'", RotekDataSet.CP.pocetColumn, 2, RotekDataSet.CP.NazovColumn, cp)
        Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2}= '{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, DataGridView1.Rows(riadok).Cells(2).Value)

        If DataGridView2.RowCount = 0 Then
            Chyby.Show("Nič nezadané")
            Exit Sub
        End If

        Dim folder_path As String = My.Settings.Rotek3 & "CP\" & (Now.Year - 2000) & "\"
        If (Not Directory.Exists(folder_path)) Then
            Directory.CreateDirectory(folder_path)
        End If

        Dim strPath As String = folder_path & cp.Replace("/", "•") & ".xls"

        '        strPath = strPath.Replace(".xlsx", ".ods")
        Dim cesta As String = My.Settings.Rotek3 & "CP.xls"

        Dim fs As System.IO.FileStream

        Dim fileInUse As Boolean = True
        If File.Exists(strPath) Then
            Try
                ' If Open() succeeds, then we know the file is not currently in use.
                fs = System.IO.File.Open(strPath, FileMode.Open, FileAccess.Read, FileShare.None)
                fileInUse = False
                fs.Close()
            Catch ex As System.Exception
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
        Catch ex As System.Exception
            Chyby.Show("Subor bol zmazany. Prosime vratit CP.xls do adresara " + cesta)
            Exit Sub
        End Try

        'zaciatok

        Dim excelApp As ExcelFile = New ExcelFile
        Try
            excelApp.LoadXls(strPath, XlsOptions.PreserveAll)
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
        'excelApp.Workbooks.Open(strPath)
        'excelSheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, strPath.Replace(".xls", ""), XlFixedFormatQuality.xlQualityStandard, True, True, 1, 1, True)
        Try

            excelSheet = excelApp.Worksheets(0)

            Dim intNewRow As Int32 = 23
            Dim vyska As Integer = 38
            Dim strana As Integer = 32

            Dim i As Integer = 0

            Dim zaciatok As Integer = i
            Dim d As DateTime = New DateTime
            excelSheet.Cells("F17").Value = cp
            excelSheet.Cells("D15").Value = DataGridView1.Rows(riadok).Cells(10).Value
            Dim datumm As DateTime = DataGridView1.Rows(riadok).Cells(5).Value
            excelSheet.Cells("H15").Value = datumm.ToShortDateString
            excelSheet.Cells("D8").Value = DataGridView1.Rows(riadok).Cells(3).Value
            excelSheet.Cells("D9").Value = DataGridView1.Rows(riadok).Cells(2).Value
            excelSheet.Cells("D10").Value = DataGridView3.Rows(0).Cells(1).Value
            excelSheet.Cells("D11").Value = DataGridView3.Rows(0).Cells(2).Value
            excelSheet.Cells("D12").Value = DataGridView3.Rows(0).Cells(3).Value
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
                excelSheet.Cells("A" & intNewRow + i).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, GemBox.Spreadsheet.LineStyle.Thin)
                excelSheet.Cells("B" & intNewRow + i).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, GemBox.Spreadsheet.LineStyle.Thin)
                excelSheet.Cells("E" & intNewRow + i).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, GemBox.Spreadsheet.LineStyle.Thin)
                excelSheet.Cells("G" & intNewRow + i).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, GemBox.Spreadsheet.LineStyle.Thin)
                excelSheet.Cells("I" & intNewRow + i).Formula = "=IF(E" & intNewRow + i & "*G" & intNewRow + i & "=0,"""",E" & intNewRow + i & "*G" & intNewRow + i & ")"
                excelSheet.Cells("I" & intNewRow + i).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, GemBox.Spreadsheet.LineStyle.Thin)
            Next

            Dim potom As Integer = DataGridView2.RowCount + 1
            potom = Math.Ceiling(potom / strana) * strana - 2 + intNewRow


            excelSheet.Rows(potom - 2).Height = excelSheet.Rows(intNewRow + DataGridView2.RowCount).Height / 2
            excelSheet.Cells.GetSubrange("F" & potom, "G" & potom).Merged = True
            excelSheet.Cells.GetSubrange("A" & potom + 1, "H" & potom + 1).Merged = True
            excelSheet.Cells("F" & potom).Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
            excelSheet.Cells("F" & potom).Value = "Spolu:"
            excelSheet.Cells("I" & potom).Formula = "=IF(SUM(I" & intNewRow & ":I" & potom - 1 & ")=0,"""",SUM(I" & intNewRow & ":I" & potom - 1 & "))"
            excelSheet.Cells("I" & potom).Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, GemBox.Spreadsheet.LineStyle.Thin)

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
                excelSheet.Cells("I" & i).Formula = "=IF(E" & intNewRow + i & "*G" & intNewRow + i & "=0,"""",E" & intNewRow + i & "*G" & intNewRow + i & ")"
            Next

            excelApp.SaveXls(strPath)
            'excelApp.Workbooks.Open(strPath)
            'excelSheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, strPath.Replace(".xls", ""), XlFixedFormatQuality.xlQualityStandard, True, True, 1, 1, True)
            img_excel(strPath, "acert.png")

            Process.Start(strPath)

        Catch ex As System.Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Public Sub img_excel(ByVal strPath As String, ByVal obrazok As String)
        Dim fss As FileStream = New FileStream(strPath, FileMode.Open, FileAccess.ReadWrite)
        Dim wb As HSSFWorkbook = New HSSFWorkbook(fss, True)
        Dim sheet As ISheet = wb.GetSheetAt(0)
        Dim patriarch = sheet.CreateDrawingPatriarch()

        Dim anchor As HSSFClientAnchor
        anchor = New HSSFClientAnchor(0, 0, 0, 255, 6, 0, 9, 2)
        anchor.AnchorType = 0
        Dim picture = patriarch.CreatePicture(anchor, LoadImage(My.Settings.Rotek3 & obrazok, wb))
        picture.Resize()
        fss.Close()
        Dim fos As FileStream = New FileStream(strPath, FileMode.Create, FileAccess.ReadWrite)
        wb.Write(fos)
        fos.Close()
    End Sub
    Public Shared Function LoadImage(ByVal path As String, ByVal wb As HSSFWorkbook) As Integer
        Dim file As New FileStream(path, FileMode.Open, FileAccess.Read)
        Dim buffer As Byte() = New Byte(file.Length - 1) {}
        file.Read(buffer, 0, CInt(file.Length))
        Return wb.AddPicture(buffer, PictureType.JPEG)

    End Function


    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If
        If e.ColumnIndex = 9 Then ' Vyrobit
            Me.riadok = e.RowIndex
            'If IsDBNull(DataGridView1.Rows(riadok).Cells(7).Value) Then
            GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 180 + DataGridView1.Rows(0).Height * (e.RowIndex + 1))
            GroupBox1.Show()
            If IsDBNull(DataGridView1.Rows(riadok).Cells("DU").Value) Then
            Else
                MonthCalendar1.SelectionEnd = DataGridView1.Rows(riadok).Cells("DU").Value
            End If
            'Else
            'MonthCalendar1.SelectionEnd = DataGridView1.Rows(riadok).Cells(6).Value
            'tlac(MonthCalendar1.SelectionEnd.ToShortDateString)
            'End If
        ElseIf e.ColumnIndex = 11 Then  ' Upravit
            Dim f As New Pridat_CP()
            f.cp = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            f.ShowDialog()
            f.Dispose()
            Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
        ElseIf e.ColumnIndex = 12 Then ' Vyrobit zakazaku
            Dim f As New Hmakro(DataGridView1.Rows(e.RowIndex).Cells(2).Value, DataGridView1.Rows(e.RowIndex).Cells(3).Value, DataGridView1.Rows(e.RowIndex).Cells(1).Value)
            f.cp = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            f.ShowDialog()
            f.Dispose()
            Owner.Dispose()
            Me.Dispose()

        ElseIf e.ColumnIndex = 13 Then ' Zmazat
            Dim sql As String = "DELETE FROM CP WHERE Nazov='" & DataGridView1.Rows(e.RowIndex).Cells(0).Value & "'"
            Form78.sqa(sql)
            Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
        ElseIf e.ColumnIndex = 14 Then ' Otvorit
            Me.riadok = e.RowIndex
            Dim cp As String = DataGridView1.Rows(riadok).Cells(0).Value
            Dim strPath As String = My.Settings.Rotek3 & "CP\" & (Now.Year - 2000) & "\" & cp.Replace("/", "•") & ".xls"
            If File.Exists(strPath) Then
                Process.Start(strPath)
            Else
                Me.riadok = e.RowIndex
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 180 + DataGridView1.Rows(0).Height * (e.RowIndex + 1))
                GroupBox1.Show()
                If IsDBNull(DataGridView1.Rows(riadok).Cells("DU").Value) Then
                Else
                    MonthCalendar1.SelectionEnd = DataGridView1.Rows(riadok).Cells("DU").Value
                End If
            End If

        ElseIf e.ColumnIndex = 16 Then ' Duplikuj
            duplikuj(e.RowIndex)

        ElseIf e.ColumnIndex = 15 Then ' Posli mail
            posli_mail(e.RowIndex)
        ElseIf e.ColumnIndex = DataGridView1.Columns("Vykresy").Index Then 'Vykresy
            Dim point2 As Point
            point2 = New Point(CInt(Math.Round(CDec((Cursor.Position.X - (CDec(Me.GroupBox1.Size.Width) / 2))))), (200 + (Me.DataGridView1.Rows.Item(0).Height * (e.RowIndex + 1))))
            Me.GroupBox2.Location = point2
            Me.GroupBox2.Tag = e.RowIndex
            Me.subory("Cenov" & ChrW(233) & " ponuky")
        End If

    End Sub

    Private Sub subory(ByVal cesta As String)
        GroupBox2.Text = cesta
        GroupBox2.Show()
        Button5.Show()
        Button6.Show()
        ListView1.Clear()
        Dim cesta2 As String
        Dim cp As String = Me.DataGridView1.Rows.Item(GroupBox2.Tag).Cells.Item(0).Value
        cesta2 = My.Settings.Rotek3 + "CP\"
        cesta2 = cesta2 & (Now.Year Mod 2000) & "\" & cp.Replace("/", "•") & "\"

        If IsDBNull(DataGridView1.Rows(GroupBox2.Tag).Cells("Vykresy_db").Value) = False Then
            ListView1.Items.Add("Papiere:")
            Dim potvrdenia As String = DataGridView1.Rows(GroupBox2.Tag).Cells("Vykresy_db").Value
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
    Private Sub posli_mail(ByVal riadok As Integer)

        Me.riadok = riadok
        Dim priloha As String = My.Settings.Rotek3 & "CP\" & DataGridView1.Rows(riadok).Cells(0).Value.ToString.Replace("/", "•") & ".xls"
        If System.IO.File.Exists(priloha) Then
            Try


                'Dim csh As ExcelToPdf = New ExcelToPdf()
                'csh.OutputFormat = ExcelToPdf.eOutputFormat.Pdf
                'If (csh.ConvertFile(priloha, priloha.Replace(".xls", ".pdf")) = 0) Then
                'Else
                '    Chyby.Show("Nepodaril sa export do PDF")
                '    Exit Sub
                'End If

                PrehladO.tlacDoPdf(priloha)

                priloha = priloha.Replace(".xls", ".pdf")
                If System.IO.File.Exists(priloha) Then
                Else
                    Chyby.Show("Nepodaril sa export")
                    Exit Sub
                End If


                'Dim proc As System.Diagnostics.Process = New System.Diagnostics.Process()
                'proc.StartInfo.FileName = "mailto:?subject=Posielam objednávku " & DataGridView1.Rows(e.RowIndex).Cells(0).Value & "&body=Dobrý deň" & vbCrLf & vbCrLf & "see attachment&attachment=""" & priloha & """"
                'Chyby.Show(proc.StartInfo.FileName)
                'proc.Start()


                'Dim moApp As Outlook.Application
                'moApp = New Outlook.Application

                'Dim oEmail As Outlook.MailItem
                'Me.Cursor = Cursors.WaitCursor
                'Try
                '    oEmail = DirectCast(moApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
                '    With oEmail
                '        '.To = "meow@example.com"
                '        '.CC = "ruff@example.com"
                '        .Subject = "Posielam objednávku " & DataGridView1.Rows(e.RowIndex).Cells(0).Value
                '        .BodyFormat = Outlook.OlBodyFormat.olFormatHTML
                '        .Body = "Dobrý deň" & vbCrLf & vbCrLf
                '        .Importance = Outlook.OlImportance.olImportanceHigh
                '        .ReadReceiptRequested = True
                '        .Attachments.Add(priloha, Outlook.OlAttachmentType.olEmbeddeditem)
                '        '.Recipients.ResolveAll()
                '        .Save()
                '        .Display() 'Show the email message and allow for editing before sending
                '        '.Send() 'You can automatically send the email without displaying it.
                '    End With
                '    Me.Cursor = Cursors.Default
                '    Exit Sub
                'Catch ex As System.Exception
                '    Chyby.Show("Nenašiel sa Outlook")
                'End Try
                ' 

                Dim veduci As String
                If IsDBNull(DataGridView1.Rows(riadok).Cells(3).Value) Then
                    veduci = "0"
                Else
                    veduci = DataGridView1.Rows(riadok).Cells(3).Value
                End If
                Dim f As Mail = New Mail(priloha, "Posielam objednávku " & DataGridView1.Rows(riadok).Cells(0).Value, DataGridView1.Rows(riadok).Cells(2).Value, veduci)
                f.ShowDialog()
                f.Dispose()

            Catch ex As System.Exception
                Chyby.Show(ex.ToString)
            End Try
        Else
            GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 180 + DataGridView1.Rows(0).Height * (riadok + 1))
            GroupBox1.Show()
            If IsDBNull(DataGridView1.Rows(riadok).Cells("DU").Value) Then
            Else
                MonthCalendar1.SelectionEnd = DataGridView1.Rows(riadok).Cells("DU").Value
            End If
        End If
    End Sub

    Private Sub duplikuj(ByVal riadok As Integer)
        Dim nazov As String = DataGridView1.Rows(riadok).Cells(0).Value
        Dim cp, cena, popis, Firma, veduci As String
        popis = DataGridView1.Rows(riadok).Cells(1).Value
        Firma = DataGridView1.Rows(riadok).Cells(2).Value
        veduci = DataGridView1.Rows(riadok).Cells(3).Value
        cena = DataGridView1.Rows(riadok).Cells("Cena").Value
        Dim datum As Date
        datum = DataGridView1.Rows(riadok).Cells("Datum").Value

        CPBindingSource.Sort = "Nazov DESC"
        Dim sql As String
        cp = CPTableAdapter.MaxNazov
        cp = cp.Replace("CP ", "")
        cp = cp.Replace("/" & Year(Now).ToString.Substring(2), "")
        Try
            Dim i As Integer = cp
            cp = "CP " & Format(i + 1, "0000") & "/" & Year(Now).ToString.Substring(2)
        Catch ex As Exception
            Chyby.Show("Chyba")
            Exit Sub
        End Try

        sql = "INSERT INTO CP (Nazov, Popis, Firma, Veduci, Datum, Cena, Poznamka, pocet, Evidoval) VALUES('" & cp & "','" & popis & "','" & Firma & "','" & veduci & "','" & datum.ToString("yyyy-MM-dd") & "','" & cena & "','" & "-" & "'," & 1 & ",'" & Form78.uzivatel & "')"
        Form78.sqa(sql)
        Me.CPBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}= '{3}'", RotekDataSet.CP.pocetColumn, 2, RotekDataSet.CP.NazovColumn, nazov)
        For i As Integer = 0 To DataGridView2.RowCount - 1
            Dim polozka, kusov, cena1 As String
            polozka = DataGridView2.Rows(i).Cells(0).Value
            kusov = DataGridView2.Rows(i).Cells(1).Value
            cena1 = DataGridView2.Rows(i).Cells(2).Value
            sql = "INSERT INTO CP (Nazov, Polozka, Kusov, Datum, Cena, pocet, Evidoval) VALUES('" & cp & "','" & polozka & "','" & kusov & "','" & datum.ToString("yyyy-MM-dd") & "','" & cena1 & "'," & 2 & ",'" & Form78.uzivatel & "')"
            Form78.sqa(sql)
        Next
        Chyby.Show("Skopírovaná ako ponuka <" & cp & ">")
        Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)

    End Sub
    Private Sub DataGridView1_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex > -1 Then
            Dim f As New Zobraz_CP
            f.cp = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            f.ShowDialog()
            f.Dispose()
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'RadioButton2.Checked = True
        tlac("")
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        tlac(MonthCalendar1.SelectionEnd.ToShortDateString)
        If RadioButton1.Checked = True Then
            Dim sql As String = "UPDATE CP SET DU='" & MonthCalendar1.SelectionEnd.ToString("yyyy-MM-dd") & "' WHERE pocet=1 AND Nazov='" & DataGridView1.Rows(riadok).Cells(0).Value & "'"
            Form78.sqa(sql)
        End If
        Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
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

    Private Sub UpraviťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpraviťToolStripMenuItem.Click
        Dim f As New Pridat_CP()
        f.cp = DataGridView1.Rows(ContextMenuStrip1.Tag).Cells(0).Value
        f.ShowDialog()
        f.Dispose()
        Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)

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
                    ContextMenuStrip1.Items(5).Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub PoslaťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PoslaťToolStripMenuItem.Click
        posli_mail(ContextMenuStrip1.Tag)

    End Sub

    Private Sub SpraviťZákazkuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpraviťZákazkuToolStripMenuItem.Click
        Dim riadok As Integer = ContextMenuStrip1.Tag
        Dim f As New Hmakro(DataGridView1.Rows(riadok).Cells(2).Value, DataGridView1.Rows(riadok).Cells(3).Value, DataGridView1.Rows(riadok).Cells(1).Value)
        f.cp = DataGridView1.Rows(riadok).Cells(0).Value
        f.ShowDialog()
        f.Dispose()
        Owner.Dispose()
        Me.Dispose()

    End Sub

    Private Sub ZmazaťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZmazaťToolStripMenuItem.Click
        If MessageBox.Show("Naozaj chcete zmazať " & Me.DataGridView1.Rows(ContextMenuStrip1.Tag).Cells.Item(0).Value & " ?", "Zmazať", MessageBoxButtons.YesNo) = vbYes Then
            Dim sql As String = "DELETE FROM CP WHERE Nazov='" & DataGridView1.Rows(ContextMenuStrip1.Tag).Cells(0).Value & "'"
            Form78.sqa(sql)
            Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
        End If

    End Sub

    Private Sub OtvoriťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OtvoriťToolStripMenuItem.Click
        Me.riadok = ContextMenuStrip1.Tag
        Dim cp As String = DataGridView1.Rows(riadok).Cells(0).Value
        Dim strPath As String = My.Settings.Rotek3 & "CP\" & (Now.Year - 2000) & "\" & cp.Replace("/", "•") & ".xls"

        If File.Exists(strPath) Then
            Process.Start(strPath)
        Else
            Me.riadok = ContextMenuStrip1.Tag
            GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 180 + DataGridView1.Rows(0).Height * (Me.riadok + 1))
            GroupBox1.Show()
            If IsDBNull(DataGridView1.Rows(riadok).Cells("DU").Value) Then
            Else
                MonthCalendar1.SelectionEnd = DataGridView1.Rows(riadok).Cells("DU").Value
            End If
        End If

    End Sub

    Private Sub SkopírovaťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SkopírovaťToolStripMenuItem.Click
        duplikuj(ContextMenuStrip1.Tag)
    End Sub

    Private Sub TlačiťToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TlačiťToolStripMenuItem.Click
        Me.riadok = ContextMenuStrip1.Tag
        'If IsDBNull(DataGridView1.Rows(riadok).Cells("DU").Value) Then
        GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 180 + DataGridView1.Rows(0).Height * (riadok + 1))
        GroupBox1.Show()
        If IsDBNull(DataGridView1.Rows(riadok).Cells("DU").Value) Then
        Else
            MonthCalendar1.SelectionEnd = DataGridView1.Rows(riadok).Cells("DU").Value
        End If
        'Else
        '    MonthCalendar1.SelectionEnd = DataGridView1.Rows(riadok).Cells(6).Value
        '    tlac(MonthCalendar1.SelectionEnd.ToShortDateString)
        'End If
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
            If GroupBox1.Location.X < 0 Then
                GroupBox1.Location = New Point(1, GroupBox1.Location.Y)
            End If
            If GroupBox1.Location.Y + GroupBox1.Size.Height > Me.Height Then
                GroupBox1.Location = New Point(GroupBox1.Location.X, GroupBox1.Location.Y - GroupBox1.Height - 70)
            End If
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        napln()
    End Sub

    Private Sub GroupBox2_VisibleChanged(sender As Object, e As EventArgs) Handles GroupBox2.VisibleChanged
        If GroupBox2.Location.Y + GroupBox2.Size.Height > Me.Height Then
            GroupBox2.Location = New System.Drawing.Point(GroupBox2.Location.X, MousePosition.Y - 50 - GroupBox2.Size.Height)
            If GroupBox2.Location.Y + GroupBox2.Size.Height > Me.Height Or GroupBox2.Location.Y < Me.Location.Y Then
                GroupBox2.Location = New System.Drawing.Point(GroupBox2.Location.X, Me.Height - 50 - GroupBox2.Size.Height)
            End If
        End If

        If GroupBox2.Location.X + GroupBox2.Size.Width > Me.Width Then
            GroupBox2.Location = New System.Drawing.Point(MousePosition.X - GroupBox2.Size.Width, GroupBox2.Location.Y)
            If GroupBox2.Location.X + GroupBox2.Size.Width > Me.Width Or GroupBox2.Location.X < Me.Location.X Then
                GroupBox2.Location = New System.Drawing.Point(Me.Width - GroupBox2.Size.Width, GroupBox2.Location.Y)
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Form78.heslo = Form78.admin Or Form78.heslo = Form78.zakazkar Then
        Else
            Exit Sub
        End If

        Dim cp As String = Me.DataGridView1.Rows.Item(GroupBox2.Tag).Cells.Item(0).Value
        Dim text As String = ""
        ListView1.Items.Add("Papiere:")
        If IsDBNull(DataGridView1.Rows(GroupBox2.Tag).Cells("Vykresy_db").Value) = False Then
            text = DataGridView1.Rows(GroupBox2.Tag).Cells("Vykresy_db").Value
        End If

        Dim slovo As String = InputBox("Napis identifikaciu cenovej ponuky", "Cenova ponuka")
        If slovo.Length > 0 Then
            text = text + "|" + slovo
            Dim sql As String
            sql = "UPDATE CP SET Vykresy='" + text + "' WHERE Nazov='" + cp + "' AND pocet=" & 1
            MessageBox.Show(sql)
            Form78.sqa(sql)
            ListView1.Items.Add(slovo)
            CPTableAdapter.Fill(Me.RotekDataSet.CP)
            napln()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Form78.heslo = Form78.admin Or Form78.heslo = Form78.zakazkar Then
        Else
            Exit Sub
        End If

        Dim cp As String = Me.DataGridView1.Rows.Item(GroupBox2.Tag).Cells.Item(0).Value

        Dim cesta2 As String = My.Settings.Rotek3
        cesta2 = cesta2 & "CP\" & (Now.Year Mod 2000) & "\" & cp.Replace("/", "•") + "\"


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

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick

        Dim cp As String = Me.DataGridView1.Rows.Item(GroupBox2.Tag).Cells.Item(0).Value
        Dim cesta2 As String = My.Settings.Rotek3
        cesta2 = cesta2 & "CP\" & (Now.Year Mod 2000) & "\" & cp.Replace("/", "•") + "\"

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
End Class