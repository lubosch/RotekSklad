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

Public Class Prijemky
    Private excelSheet As ExcelWorksheet
    Dim vyska As Integer = 15

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        rozmers()

        ' Add any initialization after the InitializeComponent() call.


    End Sub
    Private Sub Prijemky_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.PrijemkyTableAdapter.Fill(Me.RotekDataSet.Prijemky)

        rozmers()
        poverenie()
    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo

            Case Form78.admin
                DataGridView1.Columns("Zmazat").Visible = True
            Case Form78.zakazkar
                DataGridView1.Columns("Zmazat").Visible = False
            Case Form78.skladnik
                DataGridView1.Columns("Zmazat").Visible = True
            Case Else
                DataGridView1.Columns("Zmazat").Visible = False
                DataGridView1.Columns("Upravit").Visible = False

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
        Me.PrijemkyBindingSource.Filter = String.Format("{2} LIKE '%{3}%' AND {4} LIKE '{5}%' AND Dodavatel LIKE '{7}%' AND {8}<='{9}'", RotekDataSet.Prijemka.pocetColumn, 1, RotekDataSet.Prijemky.NazovColumn, TextBox1.Text, RotekDataSet.Prijemky.DodaciListColumn, TextBox2.Text, RotekDataSet.Prijemky.DodavatelColumn, TextBox3.Text, RotekDataSet.Prijemky.Datum_DLColumn, DateTimePicker1.Value.ToString("yyyy-MM-dd"))

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

    Private Sub DateTimePicker1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        filtruj()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = DataGridView1.Columns("Export").Index Then
            Dim nazov As String = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            prijemka(nazov)
        ElseIf e.ColumnIndex = DataGridView1.Columns("Upravit").Index Then
            Dim nazov As String = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            Dim f As New Hpridat(nazov)
            f.ShowDialog()
            f.Dispose()
        ElseIf e.ColumnIndex = DataGridView1.Columns("Zmazat").Index Then
            Dim nazov As String = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            SQL_main.AddCommand("UPDATE m SET m.kusov = m.kusov - mp.Ks  FROM Material m JOIN Material_Prijemka mp ON mp.Material_ID = m.ID JOIN Prijemky p ON p.ID = mp.Prijemka_ID WHERE p.Nazov = '" + nazov + "'")
            SQL_main.AddCommand("DELETE FROM Material_Prijemka WHERE Prijemka_ID = (SELECT ID FROM Prijemky WHERE Nazov = '" + nazov + "')")
            SQL_main.AddCommand("DELETE FROM Prijemky WHERE Nazov = '" + nazov + "'")
            SQL_main.Commit_Transaction()
            Me.PrijemkyTableAdapter.Fill(Me.RotekDataSet.Prijemky)

        End If

    End Sub
    Public Sub prijemka(ByVal nazov As String)

        Dim prijemka As Prijemka_SQL = New Prijemka_SQL(nazov)

        If prijemka.id = "" Then
            Chyby.Show("Prijemka nenajdena")
            Exit Sub
        End If

        If IsDBNull(prijemka.poznamka) Then
            prijemka.poznamka = "-"
        End If

        Dim strPath As String = My.Settings.Rotek3 & "Prijemky\"

        strPath = strPath + nazov + ".xlsx"

        Dim cesta As String = My.Settings.Rotek3 & "Prijemky\Prijemka.xlsx"

        Try
            My.Computer.FileSystem.CopyFile(cesta, strPath, True)
        Catch ex As Exception
            Chyby.Show("Subor bol zmazany. Prosime vratit Prijemka.xlsx do adresara " + cesta)
            Exit Sub
        End Try


        'zaciatok

        Dim fufukeh As FileInfo = New FileInfo(strPath)
        Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)

        Try
            excelSheet = excelApp.Workbook.Worksheets.First

            Dim intNewRow As Int32 = 5 'prve volne kde pisat
            vyska = 38
            Dim strana As Integer = 19 'zmesti sa na stranu


            excelSheet.Cells("A2").Value = prijemka.dodaciList
            excelSheet.Cells("C2").Value = prijemka.datumDL
            excelSheet.Cells("D2").Value = prijemka.dodavatel
            excelSheet.Cells("G2").Value = prijemka.poznamka
            excelSheet.Cells("J1").Value = nazov

            Dim dd As DataTable = Prijemka_SQL.materialByName(nazov)

            Dim i As Integer = 0
            Dim ix As Integer = intNewRow

            For i = 0 To dd.Rows.Count - 1
                excelSheet.Cells("A" & ix).Value = i + 1

                For ii As Integer = 1 To 3
                    Dim b As String = (Chr(Asc("A") + ii) & ix)
                    'excelSheet.Range(b).RowHeight = vyska
                    excelSheet.Cells(b).Value = dd.Rows(i).Item(ii - 1)
                    'pismo(b)
                Next
                Dim druh, nazovM, typ As String
                Dim sirka, rozmer, srozmer, velkost, kusov As Integer
                Dim KsKg, Kg, CenaKg, Cena As Decimal
                druh = dd.Rows(i).Item(0)
                nazovM = dd.Rows(i).Item(1)
                typ = dd.Rows(i).Item(2)
                sirka = dd.Rows(i).Item(3)
                rozmer = dd.Rows(i).Item(4)
                srozmer = dd.Rows(i).Item(5)
                velkost = dd.Rows(i).Item(6)
                kusov = dd.Rows(i).Item(7)
                KsKg = dd.Rows(i).Item(8)
                Kg = dd.Rows(i).Item(9)
                CenaKg = dd.Rows(i).Item(10)
                Cena = dd.Rows(i).Item(11)

                excelSheet.Cells("E" & ix).Value = Huta_SQL.rozmer_slovom(sirka, rozmer, srozmer, velkost, typ)
                excelSheet.Cells("F" & ix).Value = kusov
                excelSheet.Cells("G" & ix).Value = KsKg
                excelSheet.Cells("H" & ix).Value = Kg
                excelSheet.Cells("I" & ix).Value = CenaKg
                excelSheet.Cells("J" & ix).Value = Cena

                If zaokruhli_dole((i + 1) / strana) = zaokruhli_hore((i + 1) / strana) Then
                    pata(ix, Form78.uzivatel, Date.Now.ToShortDateString)

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
            pata(ix, Form78.uzivatel, Date.Now.ToShortDateString)

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
        excelSheet.Cells("C" & ix + 1).Value = "Vyhotovil"
        ' pismo("C"get & ix + 1)               2
        excelSheet.Cells("C" & ix + 2).Value = vystavil
        '  pismo("Cget" & ix + 2)              2
        excelSheet.Cells("C" & ix + 3).Value = "Dátum: " + d_p
        '   pismo("getC" & ix + 3)             2
        excelSheet.Cells("E" & ix + 1).Value = "Prevzal"
        '    pismo(get"E" & ix + 1)            2
        excelSheet.Cells("E" & ix + 3).Value = "Dátum: "
        '     pismoget("E" & ix + 3)           2
        excelSheet.Cells("G" & ix + 1).Value = "Schválil"
        '      pismgeto("G" & ix + 1)          2
        excelSheet.Cells("G" & ix + 3).Value = "Dátum"
        '       pisgetmo("G" & ix + 3)         2
        excelSheet.Cells("J" & ix + 1).Value = "Kontroloval"
        'pismo("J" get& ix + 1)                2
        excelSheet.Cells("J" & ix + 3).Value = "Dátum: "

        ix = ix + 3
    End Sub

    Private Sub pismo(ByVal ix As Integer)
        excelSheet.Cells("A" & ix + 3 & ":B" & ix + 3).Merge = True
        excelSheet.Cells("A" & ix + 1 & ":B" & ix + 1).Merge = True
        excelSheet.Cells("C" & ix + 1 & ":D" & ix + 1).Merge = True
        excelSheet.Cells("A" & ix + 2 & ":B" & ix + 2).Merge = True
        excelSheet.Cells("C" & ix + 2 & ":D" & ix + 2).Merge = True
        excelSheet.Cells("C" & ix + 3 & ":D" & ix + 3).Merge = True
        excelSheet.Cells("E" & ix + 1 & ":F" & ix + 1).Merge = True
        excelSheet.Cells("J" & ix + 1 & ":K" & ix + 1).Merge = True
        excelSheet.Cells("E" & ix + 2 & ":F" & ix + 2).Merge = True
        excelSheet.Cells("J" & ix + 2 & ":K" & ix + 2).Merge = True
        excelSheet.Cells("E" & ix + 3 & ":F" & ix + 3).Merge = True
        excelSheet.Cells("J" & ix + 3 & ":K" & ix + 3).Merge = True
        'excelSheet.Cells("G" & ix + 1 & ":H" & ix + 1).Merge = True
        'excelSheet.Cells("G" & ix + 2 & ":H" & ix + 2).Merge = True
        'excelSheet.Cells("G" & ix + 3 & ":H" & ix + 3).Merge = True
        excelSheet.Cells("G" & ix + 1 & ":I" & ix + 1).Merge = True
        excelSheet.Cells("G" & ix + 2 & ":I" & ix + 2).Merge = True
        excelSheet.Cells("G" & ix + 3 & ":I" & ix + 3).Merge = True


        excelSheet.Cells("A" & ix & ":K" & ix).Style.Border.Bottom.Style = ExcelBorderStyle.Medium

        excelSheet.Cells("A" & ix + 2 & ":K" & ix + 2).Style.Border.Top.Style = ExcelBorderStyle.None
        excelSheet.Cells("A" & ix + 3 & ":K" & ix + 3).Style.Border.Top.Style = ExcelBorderStyle.None
        excelSheet.Cells("A" & ix + 2 & ":K" & ix + 2).Style.Border.Bottom.Style = ExcelBorderStyle.None
        excelSheet.Cells("A" & ix + 1 & ":K" & ix + 1).Style.Border.Bottom.Style = ExcelBorderStyle.None

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
        TextBox3.Text = ""
        DateTimePicker1.Value = Now
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex > -1 Then
            Dim f As New Prijemka_prehlad
            f.prijemka = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            f.ShowDialog()
            f.Dispose()
        End If
    End Sub

End Class