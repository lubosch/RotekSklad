'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel

'Imports TcKs.MSOffice.Excel
'Imports TcKs.MSOffice
'Imports TcKs.MSOffice.Common
'Imports GemBox.Spreadsheet
Imports OfficeOpenXml
Imports System.IO
Imports OfficeOpenXml.Drawing
Imports OfficeOpenXml.Style

Public Class Vydajky
    Private excelSheet As ExcelWorksheet
    Dim vyska As Integer = 15

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        rozmers()

        ' Add any initialization after the InitializeComponent() call.


    End Sub


    Private Sub Prijemky_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Vydajky' table. You can move, or remove it, as needed.
        Me.VydajkyTableAdapter.Fill(Me.RotekDataSet.Vydajky)
        'TODO: This line of code loads data into the 'RotekDataSet.Vydajka' table. You can move, or remove it, as needed.
        'Me.PrijemkaTableAdapter.Fill(Me.RotekDataSet.Prijemka)
        'Me.PrijemkaBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Prijemka.pocetColumn, 1)

        rozmers()
        poverenie()
    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo

            Case Form78.admin
                DataGridView1.Columns(5).Visible = True
                DataGridView1.Columns(6).Visible = True

            Case Form78.zakazkar
                DataGridView1.Columns(5).Visible = False
                DataGridView1.Columns(6).Visible = False
            Case Form78.skladnik
                DataGridView1.Columns(6).Visible = False
                DataGridView1.Columns(5).Visible = False
            Case Else
                DataGridView1.Columns(6).Visible = False
                DataGridView1.Columns(5).Visible = False

        End Select
    End Sub
    Private Sub rozmers()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2, sw - 165)
        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString("Evidencia zakaziek", Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New System.Drawing.Point(rw, 20)
    End Sub

    Private Sub Prijemky_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged
        rozmers()
    End Sub

    Private Sub filtruj()
        If TextBox2.Text.Length = 0 Then
            If CheckBox1.Checked Then
                Me.VydajkyBindingSource.Filter = String.Format("{0} LIKE '%{1}%' AND {2}<='{3}' AND Pokazil IS NOT NULL", RotekDataSet.Vydajky.NazovColumn, TextBox1.Text, RotekDataSet.Vydajky.DatumColumn, DateTimePicker1.Value)
            Else
                Me.VydajkyBindingSource.Filter = String.Format("{0} LIKE '%{1}%' AND {2}<='{3}'", RotekDataSet.Vydajky.NazovColumn, TextBox1.Text, RotekDataSet.Vydajky.DatumColumn, DateTimePicker1.Value)
            End If
        Else
            If CheckBox1.Checked Then
                Me.VydajkyBindingSource.Filter = String.Format("{0} LIKE '%{1}%' AND {2} LIKE '%{3}%' AND {4} >='{5}' AND Pokazil IS NOT NULL", RotekDataSet.Vydajky.NazovColumn, TextBox1.Text, RotekDataSet.Vydajky.ZakazkaColumn, TextBox2.Text, RotekDataSet.Vydajky.DatumColumn, DateTimePicker1.Value)
            Else
                Me.VydajkyBindingSource.Filter = String.Format("{0} LIKE '%{1}%' AND {2} LIKE '%{3}%' AND {4} >='{5}'", RotekDataSet.Vydajky.NazovColumn, TextBox1.Text, RotekDataSet.Vydajky.ZakazkaColumn, TextBox2.Text, RotekDataSet.Vydajky.DatumColumn, DateTimePicker1.Value)
            End If
        End If

    End Sub
    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        filtruj()
    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        filtruj()
    End Sub

    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs)
        filtruj()

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        filtruj()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = DataGridView1.Columns("Export").Index Then 'Exportovat
            vydajka(e.RowIndex)
        ElseIf e.ColumnIndex = DataGridView1.Columns("Upravit").Index Then 'upravit
            Dim nazov As String = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            Dim f As New Hvydat(nazov)
            f.ShowDialog()
            f.Dispose()
            Me.VydajkyTableAdapter.Fill(Me.RotekDataSet.Vydajky)
            filtruj()
        ElseIf e.ColumnIndex = DataGridView1.Columns("Zmazat").Index Then 'zmazat
            Dim nazov As String = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            SQL_main.OpenConnection()
            SQL_main.AddCommand("UPDATE m SET m.kusov = m.kusov - mv.Ks  FROM Material m JOIN Material_Vydajka mv ON mv.Material_ID = m.ID JOIN Vydajky v ON v.ID = mv.Vydajka_ID WHERE v.Nazov = '" + nazov + "'")
            SQL_main.AddCommand("UPDATE m SET m.kusov = m.kusov - mv.Zvysok_ks  FROM Material m JOIN Material_Vydajka mv ON mv.Material_zvysok_ID = m.ID JOIN Vydajky v ON v.ID = mv.Vydajka_ID  WHERE v.Nazov = '" + nazov + "'")

            SQL_main.AddCommand("DELETE FROM Material_Vydajka WHERE Vydajka_ID IN (SELECT ID FROM Vydajky WHERE Nazov =  '" + nazov + "')")
            SQL_main.AddCommand("DELETE FROM Reklamacia_Vydajka WHERE Vydajka_ID =  (SELECT ID FROM Vydajky WHERE Nazov = '" + nazov + "')")
            SQL_main.AddCommand("DELETE FROM Vydajky WHERE Nazov =  '" + nazov + "'")
            SQL_main.Commit_Transaction()
            Me.VydajkyTableAdapter.Fill(Me.RotekDataSet.Vydajky)
            filtruj()

        End If

    End Sub
    Public Sub vydajka(ByVal riadok As Integer)

        Dim zakazka, poznamka, nazov, vyhotovil As String
        Dim datum As Date
        nazov = DataGridView1.Rows(riadok).Cells(0).Value
        zakazka = DataGridView1.Rows(riadok).Cells(1).Value
        datum = DataGridView1.Rows(riadok).Cells(2).Value
        vyhotovil = DataGridView1.Rows(riadok).Cells(3).Value
        If IsDBNull(DataGridView1.Rows(riadok).Cells(7).Value) Then
            poznamka = "-"
        Else
            poznamka = DataGridView1.Rows(riadok).Cells(7).Value
        End If


        Dim strPath As String = My.Settings.Rotek3 & "Vydajky\"

        strPath = strPath + nazov + ".xlsx"

        Dim cesta As String = My.Settings.Rotek3 & "Vydajky\Vydajka.xlsx"

        Try
            My.Computer.FileSystem.CopyFile(cesta, strPath, True)
        Catch ex As Exception
            Chyby.Show("Subor bol zmazany. Prosime vratit Vydajka.xlsx do adresara " + cesta)
            Exit Sub
        End Try


        'zaciatok
        'SpreadsheetInfo.SetLicense()

        Dim fufukeh As FileInfo = New FileInfo(strPath)
        Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)
        ' Loads Excel file.

        'Dim excelBook As Excel.Workbook
        'Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

        Try
            'excelBook = excelApp.Workbooks.Open(strPath, 0, False, 5, _
            '    System.Reflection.Missing.Value, System.Reflection.Missing.Value, _
            '    False, System.Reflection.Missing.Value, System.Reflection.Missing.Value, _
            '    True, False, System.Reflection.Missing.Value, False)
            'excelBook = excelApp.Workbooks.Open(strPath)
            'Dim excelSheets As Excel.Worksheets = excelBook.Worksheets

            excelSheet = excelApp.Workbook.Worksheets.First

            Dim intNewRow As Int32 = 5 'prve volne kde pisat
            vyska = 38
            Dim strana As Integer = 31 'zmesti sa na stranu

            'Me.PrijemkaBindingSource1.Sort = "Druh, Nazov_m, Rozmer"
            excelSheet.Cells("A2").Value = zakazka
            excelSheet.Cells("C2").Value = datum.ToShortDateString
            excelSheet.Cells("D2").Value = vyhotovil
            excelSheet.Cells("E2").Value = poznamka
            excelSheet.Cells("I1").Value = nazov

            Dim dataTable As DataTable = Vydajka_SQL.materialByName(nazov)

            Dim sirka, rozmer, srozmer, velkost, kusov As Integer
            Dim typ, rozmer_slovom, zvysok_slovom As String
            Dim objem As Double

            zvysok_slovom = ""

            Dim i As Integer = 0
            Dim ix As Integer = intNewRow
            For i = 0 To dataTable.Rows.Count - 1
                excelSheet.Cells("A" & ix).Value = i + 1

                For ii As Integer = 1 To 3
                    Dim b As String = (Chr(Asc("A") + ii) & ix)
                    'excelSheet.Range(b).RowHeight = vyska
                    excelSheet.Cells(b).Value = dataTable.Rows(i).Item(ii - 1)
                    'pismo(b)
                Next

                typ = dataTable.Rows(i).Item(2)
                sirka = dataTable.Rows(i).Item(3)
                rozmer = dataTable.Rows(i).Item(4)
                srozmer = dataTable.Rows(i).Item(5)
                velkost = dataTable.Rows(i).Item(6)
                rozmer_slovom = Huta_SQL.rozmer_slovom(sirka, rozmer, srozmer, velkost, typ)

                objem = Huta_SQL.objem(rozmer, velkost, sirka, srozmer, typ)


                If dataTable.Rows(i).Item(8).Value().ToString.Length <> 0 Then
                    sirka = dataTable.Rows(i).Item(8)
                    rozmer = dataTable.Rows(i).Item(9)
                    srozmer = dataTable.Rows(i).Item(10)
                    velkost = dataTable.Rows(i).Item(11)
                    zvysok_slovom = Huta_SQL.rozmer_slovom(sirka, rozmer, srozmer, velkost, typ)
                End If

                kusov = dataTable.Rows(i).Item(7)

                excelSheet.Cells("E" & ix).Value = rozmer_slovom
                excelSheet.Cells("F" & ix).Value = kusov
                excelSheet.Cells("H" & ix).Value = zvysok_slovom


                If zaokruhli_dole((i + 1) / strana) = zaokruhli_hore((i + 1) / strana) Then
                    pata(ix, Form78.uzivatel, Date.Now)

                End If

                ix = ix + 1
            Next
            Dim xixi As Integer = i + 1
            While (zaokruhli_dole((xixi) / strana) <> zaokruhli_hore((xixi) / strana))
                If xixi = 19 Then
                    'Chyby.Show(zaokruhli_dole((xixi) / strana), zaokruhli_hore((xixi) / strana))
                End If
                xixi = xixi + 1
                ix = ix + 1
            End While
            pata(ix, Form78.uzivatel, Date.Now)

            'wSheet = excelBook.ActiveSheet()

            'excelBook.Save()
            'excelSheet.e ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, strPath.Replace(".xlsx", ""), XlFixedFormatQuality.xlQualityStandard, True, True, 1, zaokruhli_hore((i) / strana), True)
            'excelApp.Visible = True
            'excelBook.Close()


            For i = 0 To excelSheet.Drawings.Count - 1
                If excelSheet.Drawings(i).GetType.ToString = "OfficeOpenXml.Drawing.ExcelPicture" Then
                    Dim pics As ExcelPicture
                    pics = excelSheet.Drawings(i)
                    pics.SetSize(52)
                    '                    MessageBox.Show(excelSheet.Drawings(i).GetType.ToString)
                End If
            Next

            excelApp.SaveAs(fufukeh)


            'PrehladO.ukonci_excel(excelApp.Hwnd)
            Process.Start(strPath)
        Catch ex As Exception
            Chyby.Show(ex.ToString)
            'excelApp.Quit()
            'PrehladO.ukonci_excel(excelApp.Hwnd)
        End Try

    End Sub
    Private Sub pata(ByRef ix As Integer, ByVal vystavil As String, ByVal d_p As String)
        pismo(ix)

        excelSheet.Cells("A" & ix + 1).Value = "Poznámka"
        'pismo("A" get& ix + 1)                2
        excelSheet.Cells("D" & ix + 1).Value = "Vyhotovil"
        ' pismo("C"get & ix + 1)               2
        excelSheet.Cells("D" & ix + 2).Value = vystavil
        '  pismo("Cget" & ix + 2)              2
        excelSheet.Cells("D" & ix + 3).Value = "Dátum: " + d_p
        '   pismo("getC" & ix + 3)             2
        excelSheet.Cells("F" & ix + 1).Value = "Prevzal"
        '    pismo(get"E" & ix + 1)            2
        excelSheet.Cells("F" & ix + 3).Value = "Dátum: "
        '     pismoget("E" & ix + 3)           2
        excelSheet.Cells("H" & ix + 1).Value = "Schválil"
        '      pismgeto("G" & ix + 1)          2
        excelSheet.Cells("G" & ix + 3).Value = "Dátum"
        '       pisgetmo("G" & ix + 3)         2
        excelSheet.Cells("K" & ix + 1).Value = "Kontroloval"
        'pismo("J" get& ix + 1)                2
        excelSheet.Cells("K" & ix + 3).Value = "Dátum: "

        ix = ix + 3
    End Sub

    Private Sub pismo(ByVal ix As Integer)
        excelSheet.Cells("A" & ix + 3 & ":C" & ix + 3).Merge = True
        excelSheet.Cells("A" & ix + 1 & ":C" & ix + 1).Merge = True
        excelSheet.Cells("D" & ix + 1 & ":E" & ix + 1).Merge = True
        excelSheet.Cells("A" & ix + 2 & ":C" & ix + 2).Merge = True
        excelSheet.Cells("D" & ix + 2 & ":E" & ix + 2).Merge = True
        excelSheet.Cells("D" & ix + 3 & ":E" & ix + 3).Merge = True
        excelSheet.Cells("F" & ix + 1 & ":G" & ix + 1).Merge = True
        excelSheet.Cells("K" & ix + 1 & ":L" & ix + 1).Merge = True
        excelSheet.Cells("K" & ix + 2 & ":L" & ix + 2).Merge = True
        excelSheet.Cells("K" & ix + 3 & ":L" & ix + 3).Merge = True
        excelSheet.Cells("F" & ix + 2 & ":G" & ix + 2).Merge = True
        excelSheet.Cells("F" & ix + 3 & ":G" & ix + 3).Merge = True
        'excelSheet.Cells("G" & ix + 1 & ":H" & ix + 1).Merge = True
        'excelSheet.Cells("G" & ix + 2 & ":H" & ix + 2).Merge = True
        'excelSheet.Cells("G" & ix + 3 & ":H" & ix + 3).Merge = True
        excelSheet.Cells("H" & ix + 1 & ":J" & ix + 1).Merge = True
        excelSheet.Cells("H" & ix + 2 & ":J" & ix + 2).Merge = True
        excelSheet.Cells("H" & ix + 3 & ":J" & ix + 3).Merge = True


        excelSheet.Cells("A" & ix & ":L" & ix).Style.Border.Bottom.Style = ExcelBorderStyle.Medium

        excelSheet.Cells("A" & ix + 2 & ":L" & ix + 2).Style.Border.Top.Style = ExcelBorderStyle.None
        excelSheet.Cells("A" & ix + 3 & ":L" & ix + 3).Style.Border.Top.Style = ExcelBorderStyle.None
        excelSheet.Cells("A" & ix + 2 & ":L" & ix + 2).Style.Border.Bottom.Style = ExcelBorderStyle.None
        excelSheet.Cells("A" & ix + 1 & ":L" & ix + 1).Style.Border.Bottom.Style = ExcelBorderStyle.None

        'excelSheet.Cells("A" & ix + 2, "C" & ix + 2).BorderAround(eLineStyle., Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Color.Black) ' (Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlLineStyleNone
        '        excelSheet.Cells("A" & ix + 2 & ":K" & ix + 2).Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlLineStyleNone

        Dim b As String = "A" & ix + 1 & ":J" & ix + 1
        excelSheet.Cells(b).Style.Font.Name = "Times"
        excelSheet.Cells(b).Style.Font.Size = 12
        excelSheet.Cells(b).Style.HorizontalAlignment = eTextAlignment.Left
        excelSheet.Cells(b).Style.Font.VerticalAlign = eTextAlignment.Left
        b = "A" & ix + 3 & ":J" & ix + 3
        excelSheet.Cells(b).Style.Font.Name = "Times"
        excelSheet.Cells(b).Style.Font.Size = 12
        excelSheet.Cells(b).Style.HorizontalAlignment = eTextAlignment.Left
        excelSheet.Cells(b).Style.Font.VerticalAlign = eTextAlignment.Left
        b = "A" & ix + 2 & ":J" & ix + 2
        excelSheet.Cells(b).Style.Font.Name = "Times"
        excelSheet.Cells(b).Style.Font.Size = 21
        excelSheet.Cells(b).Style.Font.VerticalAlign = eTextAlignment.Center
        excelSheet.Cells(b).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

        'Excel.borders()

        'excelSheet.cells(b).AutoFormat(Nothing, Nothing, Nothing, Excel.Constants.xlLeft, , Nothing, Nothing)
        '        excelSheet.cells(b).HorizontalAlignment = Excel.Constants.xlLeft
        b = "A" & ix + 2 & ":J" & ix + 2
        'excelSheet.cells(b).Font.Size = 14
        excelSheet.Row(ix + 2).Height = 18
        '        excelSheet.Cells(b).Style.HorizontalAlignment = eTextAlignment.Center

    End Sub



    'private static Bitmap GetIcon(string FileName)
    '    {
    '        try
    '        {
    '            SHFILEINFO shinfo = new SHFILEINFO();                

    '            var ret = SHGetFileInfo(FileName,
    '                                      0,
    '                                      ref shinfo,
    '                                      (uint)Marshal.SizeOf(shinfo),
    '                                      SHGFI_ICON | SHGFI_SMALLICON);

    '            if (shinfo.hIcon == IntPtr.Zero) return null;

    '            Bitmap bmp = Icon.FromHandle(shinfo.hIcon).ToBitmap();
    '            DestroyIcon(shinfo.hIcon);

    '            //Fix transparant color 
    '            Color InvalidColor = Color.FromArgb(0, 0, 0, 0);
    '            for (int w = 0; w < bmp.PhysicalDimension.Width; w++)
    '            {
    '                for (int h = 0; h < bmp.PhysicalDimension.Height; h++)
    '                {
    '                    if (bmp.GetPixel(w, h) == InvalidColor)
    '                    {
    '                        bmp.SetPixel(w, h, Color.Transparent);
    '                    }
    '                }
    '            }

    '            return bmp;
    '        }
    '        catch
    '        {
    '            return null;
    '        }
    '    }

    Public Function zaokruhli_hore(ByVal b As Double) As Integer
        Return (Math.Ceiling(b))
    End Function

    Public Function zaokruhli_dole(ByVal b As Double) As Integer
        Return (Math.Floor(b))
    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        DateTimePicker1.Value = Now
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim f As New Vydajka_prehlad
        f.vydajka = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        f.ShowDialog()
        f.Dispose()

    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 5 Then
            If DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString <> "" Then
                DataGridView1.Rows(e.RowIndex).Cells(4).Value = True
            End If
        End If
    End Sub

    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        Chyby.Show(e.Exception.ToString)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        filtruj()

    End Sub
End Class

