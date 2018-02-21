Imports OfficeOpenXml
Imports System.IO
Imports System.Data.SqlClient

Public Class Hchyba
    Private filtrik As String
    Private excelSheet As ExcelWorksheet
    Property zakazka As String
    Private ds As List(Of Udaje)
    Private ds2 As List(Of Udaje)
    Private temporary As List(Of Udaje)
    Private iablaa() As Integer
    Private sablaa() As String
    Private kks As Integer
    Private ds3 As BindingSource
    Private ds4 As DataSet
    Private ds5 As WindowsApplication2.RotekDataSetTableAdapters.HutaTableAdapter

    Public Sub New(ByVal zakazka As String, ByRef kusy() As Integer, ByRef pody() As String)
        ' This call is required by the designer.
        InitializeComponent()
        poverenie()
        Dim j As Integer
        j = 4
        Dim srand As String = "{0} = '{1}' AND {2}='{3}' AND ("
        filtrik = String.Format(srand, RotekDataSet.Huta.pocetColumn, 3, RotekDataSet.Huta.zakazkaColumn, zakazka)

        For i As Integer = 0 To kusy.Count - 1
            If kusy(i) <> 0 Then
                filtrik = filtrik & String.Format(" {0}='{1}' OR", RotekDataSet.Huta.VahaColumn, pody(i))
            End If
        Next

        filtrik = filtrik.Substring(0, filtrik.Length - 3) + ")"
        ' Chyby.Show(filtrik)
        '    DataGridView1.DataSource = HutaBindingSource
        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.HutaBindingSource.Filter = filtrik
        Me.HutaBindingSource.Sort = "Vaha"
        sablaa = pody
        iablaa = kusy
        Me.zakazka = zakazka
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo
            Case Form78.admin
                Button4.Show()
            Case Form78.zakazkar
                Button4.Show()
            Case Form78.skladnik
                Button4.Hide()
            Case Else
                Button4.Hide()
        End Select
    End Sub
    Public Sub New(ByVal zakazka As String)
        Me.zakazka = zakazka
        ' This call is required by the designer.
        InitializeComponent()
        Dim srand As String = "({0} = '{1}' OR {0}='{4}') AND {2}='{3}'"
        filtrik = String.Format(srand, RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka, 3)
        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.HutaBindingSource.Filter = filtrik
        Me.HutaBindingSource.Sort = "Vaha"
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Hchyba_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Vydajka' table. You can move, or remove it, as needed.
        Me.VydajkaTableAdapter.Fill(Me.RotekDataSet.Vydajka)
        Try
            'TODO: This line of code loads data into the 'RotekDataSet.Zakazka' table. You can move, or remove it, as needed.
            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
            dasa2()
            rozmers()
            RadioButton2.Checked = True
            poverenie()
            'For i As Integer = 0 To DataGridView2.RowCount - 1
            '    DataGridView2.Rows(i).Cells(17).Value = 1
            'Next
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try            '    Chyby.Show(HutaBindingSource1.Count & " " & ds3.Count)
    End Sub
    Private Sub rozmers()
        Dim x As Integer
        For i As Integer = 0 To 2
            x = x + DataGridView2.Columns(i).Width
        Next
        x = x + 41
        Panel1.Location = New System.Drawing.Point(x - 10, 0)
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        Button4.Location = New System.Drawing.Point(rww - 60, sw - 70)
    End Sub
    Public Function dasa() As Boolean
        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Cells(11).Value < 0 Then
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub vydajka2()

        Dim nazovv As String = "V" & String.Format("{0:00000}", DataGridView4.RowCount + 1)
        Dim strPath As String = My.Settings.Rotek3 & "Vydajky\"
        strPath = strPath + nazovv + ".xlsx"
        Dim cesta As String = My.Settings.Rotek3 & "Vydajky\Vydajka.xlsx"
        Try
            My.Computer.FileSystem.CopyFile(cesta, strPath, True)
        Catch ex As Exception
            Chyby.Show("Subor bol zmazany. Prosime vratit Prijemka.xlsx do adresara " + cesta)
        End Try

        'zaciatok

        Dim fufukeh As FileInfo = New FileInfo(strPath)
        Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)
        Try
            excelSheet = excelApp.Workbook.Worksheets.First

            Dim intNewRow As Int32 = 5 'prve volne kde pisat
            Dim vyska As Integer = 38
            Dim strana As Integer = 31 'zmesti sa na stranu

            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, Me.zakazka)
            Dim vystavil As String = DataGridView3.Rows(0).Cells(2).Value
            Dim d_p As String = Date.Now.ToShortDateString

            excelSheet.Cells("A2").Value = zakazka
            excelSheet.Cells("D2").Value = DataGridView3.Rows(0).Cells(1).Value
            excelSheet.Cells("I1").Value = nazovv
            excelSheet.Cells("I1").Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right

            Dim cena As Double = 0
            Dim hustota As Double = 0
            Dim objem As Double

            Dim i As Integer = 0
            Dim ix As Integer = intNewRow

            For i = 0 To DataGridView2.RowCount - 1
                Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.DruhColumn, DataGridView2.Rows(i).Cells(1).Value, RotekDataSet.Huta.NazovColumn, DataGridView2.Rows(i).Cells(2).Value)
                cena = sklad.Rows(0).Cells(8).Value
                hustota = sklad.Rows(0).Cells(7).Value
                excelSheet.Cells("A" & ix).Value = i + 1
                For ii As Integer = 1 To 4
                    Dim b As String = (Chr(Asc("A") + ii) & ix)
                    'excelSheet.cells(b).RowHeight = vyska
                    excelSheet.Cells(b).Value = DataGridView2.Rows(i).Cells(ii - 1).Value
                    'pismo(b)
                Next

                excelSheet.Cells("F" & ix).Value = Huta_SQL.rozmer_slovom(DataGridView2.Rows(i).Cells(5).Value, DataGridView2.Rows(i).Cells(7).Value, DataGridView2.Rows(i).Cells(4).Value, DataGridView2.Rows(i).Cells(6).Value, DataGridView2.Rows(i).Cells(3).Value)
                excelSheet.Cells("H" & ix).Value = Huta_SQL.rozmer_slovom(DataGridView2.Rows(i).Cells(10).Value, DataGridView2.Rows(i).Cells(12).Value, DataGridView2.Rows(i).Cells(9).Value, DataGridView2.Rows(i).Cells(11).Value, DataGridView2.Rows(i).Cells(3).Value)
                excelSheet.Cells("K" & ix).Value = Huta_SQL.rozmer_slovom(DataGridView2.Rows(i).Cells(18).Value, DataGridView2.Rows(i).Cells(20).Value, DataGridView2.Rows(i).Cells(17).Value, DataGridView2.Rows(i).Cells(19).Value, DataGridView2.Rows(i).Cells(3).Value)
                objem = Huta_SQL.objem(DataGridView2.Rows(i).Cells(5).Value, DataGridView2.Rows(i).Cells(7).Value, DataGridView2.Rows(i).Cells(4).Value, DataGridView2.Rows(i).Cells(6).Value, DataGridView2.Rows(i).Cells(3).Value)


                excelSheet.Cells("E" & ix).Value = DataGridView2.Rows(i).Cells(3).Value
                excelSheet.Cells("I" & ix).Value = DataGridView2.Rows(i).Cells(13).Value - DataGridView2.Rows(i).Cells(14).Value
                excelSheet.Cells("J" & ix).Value = DataGridView2.Rows(i).Cells(13).Value

                excelSheet.Cells("L" & ix).Value = String.Format("{0:N2}", objem * hustota)
                excelSheet.Cells("M" & ix).Value = String.Format("{0:N2}", objem * hustota * DataGridView2.Rows(i).Cells(8).Value)
                excelSheet.Cells("N" & ix).Value = String.Format("{0:N2}", objem * hustota * cena)
                excelSheet.Cells("O" & ix).Value = String.Format("{0:N2}", objem * hustota * DataGridView2.Rows(i).Cells(8).Value * cena)

                If zaokruhli_dole((i + 1) / strana) = zaokruhli_hore((i + 1) / strana) Then
                    pata(ix, vystavil, d_p)
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

            pata(ix, vystavil, d_p)

            excelApp.SaveAs(fufukeh)

            PrehladO.tlacDoPdf(strPath)

            Try
                'System.IO.File.Delete(strPath)
            Catch ex As Exception

            End Try

            Dim sql As String
            sql = "INSERT INTO Vydajka (Zakazka, Nazov, Datum) VALUES ('" + Me.zakazka + "','" + nazovv + "','" & Now.ToString("yyyy-MM-dd") & "')"
            Form78.sqa(sql)
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, Me.zakazka)
            Dim vydajk As String
            If IsDBNull(DataGridView3.Rows(0).Cells(6).Value) Then
                vydajk = "|" + nazovv
            Else
                vydajk = DataGridView3.Rows(0).Cells(6).Value & "|" & nazovv
            End If

            sql = "UPDATE Zakazka SET Vydajka = '" + vydajk + "' WHERE Zakazka='" + Me.zakazka + "' AND pocet=1"
            Form78.sqa(sql)

        Catch ex As Exception
            Chyby.Show(ex.ToString)
            Exit Sub
        End Try

    End Sub

    Private Sub pata(ByRef ix As Integer, ByVal vystavil As String, ByVal d_p As String)
        pismo(ix)

        excelSheet.Cells("A" & ix + 1).Value = "Poznámka"
        'pismo("A" & ix + 1)
        excelSheet.Cells("C" & ix + 1).Value = "Vyhotovil"
        ' pismo("C" & ix + 1)
        excelSheet.Cells("C" & ix + 2).Value = vystavil
        '  pismo("C" & ix + 2)
        excelSheet.Cells("C" & ix + 3).Value = "Dátum: " + d_p
        '   pismo("C" & ix + 3)
        excelSheet.Cells("E" & ix + 1).Value = "Prevzal"
        '    pismo("E" & ix + 1)
        excelSheet.Cells("E" & ix + 3).Value = "Dátum: "
        '     pismo("E" & ix + 3)
        excelSheet.Cells("G" & ix + 1).Value = "Schválil"
        '      pismo("G" & ix + 1)
        excelSheet.Cells("G" & ix + 3).Value = "Dátum"
        '       pismo("G" & ix + 3)
        excelSheet.Cells("J" & ix + 1).Value = "Kontroloval"
        'pismo("J" & ix + 1)
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

        excelSheet.Cells("A" & ix & ":K" & ix).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Medium
        excelSheet.Cells("A" & ix + 1 & ":O" & ix + 1).Style.Border.Bottom.Style = Style.ExcelBorderStyle.None
        excelSheet.Cells("A" & ix + 2 & ":O" & ix + 2).Style.Border.Bottom.Style = Style.ExcelBorderStyle.None
        excelSheet.Cells("A" & ix + 2 & ":O" & ix + 2).Style.Border.Top.Style = Style.ExcelBorderStyle.None
        excelSheet.Cells("A" & ix + 2 & ":O" & ix + 3).Style.Border.Top.Style = Style.ExcelBorderStyle.None

        Dim b As String = "A" & ix + 1 & ":J" & ix + 3
        excelSheet.Cells(b).Style.Font.Name = "Times"
        excelSheet.Cells(b).Style.Font.Size = 12
        excelSheet.Cells(b).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
        b = "A" & ix + 2 & ":J" & ix + 2
        excelSheet.Cells(b).Style.Font.Size = 14
        excelSheet.Row(ix + 2).Height = 18
        excelSheet.Cells(b).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center

    End Sub
    Public Function zaokruhli_hore(ByVal b As Double) As Integer
        Return (Math.Ceiling(b))
    End Function

    Public Function zaokruhli_dole(ByVal b As Double) As Integer
        Return (Math.Floor(b))
    End Function

    Public Function dasa2() As Boolean
        Dim pravda As Boolean = True
        Try

            Dim sirka, rozmer, velkost, kusov, objem, zjednej(4), rrozmer(15), s_rozmer As Double
            Dim druh, nazov, typ As String
            Dim pom As New Udaje(1, 1, 1, 1, 1, 1, "xixixi", -2, {0, 0, 0, 0}, "fg")
            ds = New List(Of Udaje)
            For i As Integer = 0 To DataGridView1.RowCount - 1
                rrozmer(0) = 0
                rrozmer(1) = 0
                rrozmer(2) = 0
                rrozmer(3) = 0

                ds.Add(pom)
                DataGridView2.Rows.Add()
                For ii As Integer = 0 To 8
                    DataGridView2.Rows(i).Cells(ii).Value = DataGridView1.Rows(i).Cells(ii).Value
                Next

                sirka = DataGridView1.Rows(i).Cells(4).Value
                rozmer = DataGridView1.Rows(i).Cells(5).Value
                s_rozmer = DataGridView1.Rows(i).Cells(6).Value
                typ = DataGridView1.Rows(i).Cells(3).Value
                velkost = DataGridView1.Rows(i).Cells(7).Value
                If sablaa Is Nothing Then
                    kusov = DataGridView1.Rows(i).Cells(8).Value
                Else
                    For iii As Integer = 0 To sablaa.Count - 1
                        If sablaa(iii) = DataGridView2.Rows(i).Cells(0).Value Then
                            kusov = iablaa(iii)
                            DataGridView2.Rows(i).Cells(8).Value = kusov
                        End If
                    Next
                End If

                objem = Huta_SQL.objem(rozmer, velkost, sirka, s_rozmer, typ)

                druh = DataGridView1.Rows(i).Cells(1).Value
                nazov = DataGridView1.Rows(i).Cells(2).Value
                Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {4}='{5}' AND {6}='{7}' AND {8}='{9}' AND {10}='{11}' AND {12}='{13}' AND {14}='{15}'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.S_rozmerColumn, s_rozmer, RotekDataSet.Huta.TypColumn, typ)

                If sklad.RowCount = 1 Then
                    Dim kus2, kusk5 As Integer
                    kus2 = sklad.Rows(0).Cells(6).Value
                    kusk5 = kus2 - DataGridView2.Rows(i).Cells(8).Value
                    Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}>'{3}' AND {4}='{5}' AND {6}='{7}' AND {8}='{9}' AND {10}='{11}' AND {12}='{13}' AND {14}='{15}' AND {16}='{17}'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.S_rozmerColumn, s_rozmer, RotekDataSet.Huta.TypColumn, typ)
                    '  Chyby.Show(kus2 & " " & d)
                    If sklad.RowCount = 1 Then
                        For ii As Integer = 2 To 6
                            DataGridView2.Rows(i).Cells(ii + 7).Value = sklad.Rows(0).Cells(ii).Value
                        Next

                        DataGridView2.Rows(i).Cells(14).Value = kusk5
                        DataGridView2.Rows(i).Cells(15).Value = 0
                        DataGridView2.Rows(i).Cells(17).Value = 0
                        DataGridView2.Rows(i).Cells(18).Value = 0
                        DataGridView2.Rows(i).Cells(19).Value = 0
                        DataGridView2.Rows(i).Cells(20).Value = 0
                        DataGridView2.Rows(i).Cells(21).Value = 0
                        '  ds4.Tables("Huta").Rows.Remove(sklad.Rows(0).DataBoundItem)
                        'sirka2 = DataGridView2.Rows(i).Cells(9).Value
                        'rozmer2 = DataGridView2.Rows(i).Cells(10).Value
                        's_rozmer2 = DataGridView2.Rows(i).Cells(11).Value
                        'velkost2 = DataGridView2.Rows(i).Cells(12).Value
                        'strata5 = DataGridView2.Rows(i).Cells(15).Value

                        Dim s As String = String.Format("{0,4}x{1,4}x{2,4}x{3,4}| Odpad: Odpad:{4,10} Kusov:{5,3}", sirka, rozmer, s_rozmer, velkost, 0, kus2)
                        ds.Add(New Udaje(sirka, rozmer, velkost, 0, kus2, kusk5, s, i, {0, 0, 0, 0}, typ))
                        Continue For
                    Else
                        For ii As Integer = 2 To 5
                            DataGridView2.Rows(i).Cells(ii + 7).Value = DataGridView2.Rows(i).Cells(ii + 2).Value
                        Next
                        DataGridView2.Rows(i).Cells(13).Value = kus2
                        DataGridView2.Rows(i).Cells(14).Value = kusk5
                        DataGridView2.Rows(i).Cells(15).Value = 0
                        DataGridView2.Rows(i).Cells(17).Value = 0
                        DataGridView2.Rows(i).Cells(18).Value = 0
                        DataGridView2.Rows(i).Cells(19).Value = 0
                        DataGridView2.Rows(i).Cells(20).Value = 0
                        DataGridView2.Rows(i).Cells(21).Value = 0


                        Dim s As String = String.Format("{0,4}x{1,4}x{2,4}x{3,4}| Odpad:{4,10} Kusov:{5,3}", DataGridView2.Rows(i).Cells(4).Value, DataGridView2.Rows(i).Cells(5).Value, DataGridView2.Rows(i).Cells(6).Value, DataGridView2.Rows(i).Cells(7).Value, 0, kus2)
                        ds.Add(New Udaje(sirka, rozmer, velkost, 0, kus2, kusk5, s, i, {0, 0, 0, 0}, typ))


                    End If
                End If

                Select Case typ

                    Case "Valec"
                        Me.HutaBindingSource1.Filter = String.Format("({0} = '{1}' AND {2}>'{3}' AND {4}='{5}' AND {6}='{7}' AND {8}='{9}' AND {10}>='{11}' AND {12}>='{13}' AND {14}='{15}')", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.TypColumn, typ)
                        Me.HutaBindingSource1.Sort = "Velkost ASC"

                    Case "Plech"
                        Me.HutaBindingSource1.Filter = String.Format("({0} = '{1}' AND {2}>'{3}' AND {4}='{5}' AND {6}='{7}' AND {8}>='{9}' AND {10}>='{11}' AND {12}>='{13}' AND {14}='{15}')", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.TypColumn, typ)
                    Case "6 - hran"
                        Me.HutaBindingSource1.Filter = String.Format("({0} = '{1}' AND {2}>'{3}' AND {4}='{5}' AND {6}='{7}' AND {8}='{9}' AND {10}>='{11}' AND {12}>='{13}' AND {14}='{15}')", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.TypColumn, typ)
                    Case "Rurka"
                        'Me.HutaBindingSource1.Filter = String.Format("({0} = '{1}' AND {2}>'{3}' AND {4}='{5}' AND {6}='{7}' AND {8}<='{9}' AND {10}>='{11}' AND {12}>='{13}' AND {14}='{15}' AND {16}='{17}')", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.TypColumn, typ, RotekDataSet.Huta.S_rozmerColumn, s_rozmer)
                        Me.HutaBindingSource1.Filter = String.Format("({0} = '{1}' AND {2}>'{3}' AND {4}='{5}' AND {6}='{7}' AND {8}<='{9}' AND {10}>='{11}' AND {12}>='{13}' AND {14}='{15}')", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.TypColumn, typ, RotekDataSet.Huta.S_rozmerColumn, s_rozmer)
                    Case "L - profil"
                        Me.HutaBindingSource1.Filter = String.Format("({0} = '{1}' AND {2}>'{3}' AND {4}='{5}' AND {6}='{7}' AND {8}>='{9}' AND {10}>='{11}' AND {12}>='{13}' AND {14}='{15}' AND {16}>='{17}')", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.TypColumn, typ, RotekDataSet.Huta.S_rozmerColumn, s_rozmer)
                    Case "U - profil"
                        Me.HutaBindingSource1.Filter = String.Format("({0} = '{1}' AND {2}>'{3}' AND {4}='{5}' AND {6}='{7}' AND {8}>='{9}' AND {10}>='{11}' AND {12}>='{13}' AND {14}='{15}' AND {16}>='{17}' AND ({18}-{19}-{19}<={20}-{21}-{21}))", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.TypColumn, typ, RotekDataSet.Huta.S_rozmerColumn, s_rozmer, RotekDataSet.Huta.SirkaColumn, RotekDataSet.Huta.S_rozmerColumn, sirka, s_rozmer)
                    Case "Jokelt"
                        Me.HutaBindingSource1.Filter = String.Format("({0} = '{1}' AND {2}>'{3}' AND {4}='{5}' AND {6}='{7}' AND {8}>='{9}' AND {10}>='{11}' AND {12}>='{13}' AND {14}='{15}' AND {16}>='{17}' AND ({18}-{19}-{19}<={20}-{21}-{21}) AND ({22}-{19}-{19}<={23}-{21}-{21}))", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.TypColumn, typ, RotekDataSet.Huta.S_rozmerColumn, s_rozmer, RotekDataSet.Huta.SirkaColumn, RotekDataSet.Huta.S_rozmerColumn, sirka, s_rozmer, RotekDataSet.Huta.RozmerColumn, rozmer)
                End Select

                Dim pocet_moznosti As Integer = sklad.RowCount
                Dim rozdiel, jednu, sirkaN(pocet_moznosti), rozmerN(pocet_moznosti), s_rozmerN(pocet_moznosti), velkostN(pocet_moznosti), sirke(pocet_moznosti), rozmere(pocet_moznosti), velkoste(pocet_moznosti), s_rozmere(pocet_moznosti), kuse(pocet_moznosti), kusk(pocet_moznosti), objeme(pocet_moznosti), strata(pocet_moznosti) As Double

                If pocet_moznosti = 0 And ds(ds.Count - 1).poradie <> i Then
                    Dim sirka2, kus2, rozmer2, velkost2, s_rozmer2 As Integer

                    Me.HutaBindingSource1.Filter = String.Format("({0} = '{1}'AND {4}='{5}' AND {6}='{7}' AND {8}='{9}' AND {10}='{11}' AND {12}='{13}' AND {14}='{15}' AND {16}='{17}')", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.KusovColumn, 0, RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.SirkaColumn, sirka, RotekDataSet.Huta.VelkostColumn, velkost, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.TypColumn, typ, RotekDataSet.Huta.S_rozmerColumn, s_rozmer)

                    If sklad.RowCount = 1 Then
                        kus2 = sklad.Rows(0).Cells(6).Value
                        kusk(0) = kus2 - kusov
                    Else
                        kus2 = 0
                        kusk(0) = 0 - kusov
                    End If

                    For ii As Integer = 0 To 3
                        DataGridView2.Rows(i).Cells(ii + 9).Value = DataGridView2.Rows(i).Cells(ii + 4).Value
                    Next
                    DataGridView2.Rows(i).Cells(13).Value = 0
                    DataGridView2.Rows(i).Cells(13).Value = kus2
                    DataGridView2.Rows(i).Cells(14).Value = kusk(0)
                    DataGridView2.Rows(i).Cells(15).Value = 0
                    DataGridView2.Rows(i).Cells(17).Value = 0
                    DataGridView2.Rows(i).Cells(18).Value = 0
                    DataGridView2.Rows(i).Cells(19).Value = 0
                    DataGridView2.Rows(i).Cells(20).Value = 0
                    DataGridView2.Rows(i).Cells(21).Value = 0

                    'sirka2 = DataGridView2.Rows(i).Cells(7).Value
                    'rozmer2 = DataGridView2.Rows(i).Cells(8).Value
                    'velkost2 = DataGridView2.Rows(i).Cells(9).Value

                    strata(0) = 0
                    'Chyby.Show(String.Format("{0} {1} {2} ", kus2, sklad.RowCount, i))

                    Dim s As String = String.Format("{0,4}x{1,4}x{2,4}x{3,4}|  Odpad:{4,10} Kusov:{5,3}", sirka, rozmer, s_rozmer, velkost, strata(0), kus2)
                    ds.Add(New Udaje(sirka, rozmer, velkost, strata(0), kus2, kusk(0), s, i, {0, 0, 0, 0}, typ))

                    Continue For

                End If


                Dim znizujuce As Integer = pocet_moznosti - 1 + DataGridView2.RowCount - 1

                For j As Integer = 0 To znizujuce
                    If j > znizujuce Then Exit For
                    If j > pocet_moznosti - 1 Then
                        If DataGridView2.Rows(j - pocet_moznosti).Cells(1).Value = druh AndAlso DataGridView2.Rows(j - pocet_moznosti).Cells(2).Value = nazov AndAlso DataGridView2.Rows(j - pocet_moznosti).Cells(14).Value >= sirka AndAlso DataGridView2.Rows(j - pocet_moznosti).Cells(15).Value >= rozmer AndAlso DataGridView2.Rows(j - pocet_moznosti).Cells(16).Value >= velkost Then
                            sirke(j) = DataGridView2.Rows(j - pocet_moznosti).Cells(17).Value
                            s_rozmere(j) = DataGridView2.Rows(j - pocet_moznosti).Cells(19).Value
                            rozmere(j) = DataGridView2.Rows(j - pocet_moznosti).Cells(18).Value
                            velkoste(j) = DataGridView2.Rows(j - pocet_moznosti).Cells(20).Value
                            kuse(j) = DataGridView2.Rows(j - pocet_moznosti).Cells(13).Value + 1
                            ' MessageBox.Show(sirke(j))
                        Else
                            j = j - 1
                            znizujuce = znizujuce - 1
                            Continue For
                        End If
                    Else
                        sirke(j) = sklad.Rows(j).Cells(2).Value
                        rozmere(j) = sklad.Rows(j).Cells(3).Value
                        velkoste(j) = sklad.Rows(j).Cells(5).Value
                        kuse(j) = sklad.Rows(j).Cells(6).Value
                        s_rozmere(j) = sklad.Rows(j).Cells(4).Value
                    End If
                    'ODTIALTO

                    strata(j) = vyhodnot(sirke(j), rozmere(j), s_rozmere(j), velkoste(j), kuse(j), typ, objeme(j), sirka, rozmer, s_rozmer, velkost, kusov, sirkaN(j), rozmerN(j), s_rozmerN(j), velkostN(j), kusk(j), objem)

                    'POTIALTO


                    'MessageBox.Show(sirke(j))
                Next


                If pocet_moznosti - 1 >= 1 Then
                    ' MessageBox.Show(strata.Count & " " & znizujuce)

                    QuickSort(strata, 0, znizujuce, sirke, rozmere, velkoste, kuse, kusk, velkostN, rozmerN, sirkaN, s_rozmere, s_rozmerN)
                End If
                'MessageBox.Show(znizujuce)
                For j As Integer = 0 To znizujuce
                    '                    strata(j) = Math.Round(strata(j))
                    Dim s As String = String.Format("{0,4}x{1,4}x{2,4}x{3,4}| Odpad:{4,10} Kusov:{5,3}", sirke(j), rozmere(j), s_rozmere(j), velkoste(j), String.Format("{0:0.00000}", strata(j)), kuse(j))
                    ' Chyby.Show(s)
                    ds.Add(New Udaje(sirke(j), rozmere(j), velkoste(j), strata(j), kuse(j), kusk(j), s, i, {sirkaN(j), rozmerN(j), velkostN(j), s_rozmerN(j)}, typ))

                Next

                If kuse(0) <> 0 Then
                    DataGridView2.Rows(i).Cells(9).Value = sirke(0)
                    DataGridView2.Rows(i).Cells(10).Value = rozmere(0)
                    DataGridView2.Rows(i).Cells(11).Value = s_rozmere(0)
                    DataGridView2.Rows(i).Cells(12).Value = velkoste(0)
                    DataGridView2.Rows(i).Cells(13).Value = kuse(0)
                    DataGridView2.Rows(i).Cells(14).Value = kusk(0)
                    DataGridView2.Rows(i).Cells(15).Value = strata(0)
                    DataGridView2.Rows(i).Cells(17).Value = sirkaN(0)
                    DataGridView2.Rows(i).Cells(18).Value = rozmerN(0)
                    DataGridView2.Rows(i).Cells(19).Value = s_rozmerN(0)
                    DataGridView2.Rows(i).Cells(20).Value = velkostN(0)
                    If velkostN(0) <> 0 Or rozmerN(0) > 0 Or sirkaN(0) > 0 Or s_rozmerN(0) > 0 Then
                        DataGridView2.Rows(i).Cells(21).Value = 1
                    End If
                End If

                'For j As Integer = 0 To sklad.RowCount
                '    ds4.Tables("Huta").Rows.Remove(sklad.Rows(0).DataBoundItem)
                'Next

                '  DataGridView2.Rows(i).Cells().Add(ine)
            Next

        Catch ex As Exception
            Chyby.Show(ex.ToString)

        End Try
        napln()
        Return napln()

    End Function


    Private Function vyhodnot(sirke As Double, rozmere As Double, s_rozmere As Double, velkoste As Double, kuse As Double, typ As String, objeme As Double, sirka As Double, rozmer As Double, s_rozmer As Double, velkost As Double, kusov As Double, ByRef sirkaN As Double, ByRef rozmerN As Double, ByRef s_rozmerN As Double, ByRef velkostN As Double, ByRef kusk As Double, objem As Double) As Double
        Dim strata As Double
        Dim rozdiel As Double
        Select Case typ
            Case "Valec"
                objeme = Huta_SQL.objem(rozmere, velkoste, sirke, s_rozmere, typ)
                'strata  = Math.Ceiling(kusov / Math.Floor(velkoste  / velkost))
                rozdiel = Math.Floor(kusov / Math.Floor(velkoste / velkost))
                If (kusov - rozdiel * Math.Floor(velkoste / velkost)) < 1 Then
                    sirkaN = 0
                    rozmerN = 0
                    s_rozmerN = 0
                    velkostN = 0
                Else
                    sirkaN = -1
                    s_rozmerN = -1
                    rozmerN = rozmere
                    velkostN = velkoste - (kusov - rozdiel * Math.Floor(velkoste / velkost)) * velkost
                    '              Chyby.Show(kusov & " " & rozdiel & "  " & i)
                End If


                strata = objeme * Math.Ceiling(kusov / Math.Floor(velkoste / velkost)) - objem * kusov - 3.14 * rozmerN * rozmerN * velkostN / 4 / 1000 / 1000 / 1000
                strata = strata * 1000

                'Chyby.Show(rozdiel & "  " & (kusov - rozdiel * Math.Floor(velkoste  / velkost)) & "  " & objem)
                kusk = kuse - Math.Ceiling(kusov / Math.Floor(velkoste / velkost))
                ' MessageBox.Show(sirke )

            Case "Plech"

                objeme = Huta_SQL.objem(rozmere, velkoste, sirke, s_rozmere, typ)

                Dim k1, k2, k3, k4, k5, k6, k7, k8, k9 As Integer
                Dim jednu As Double
                Dim rrozmer(8) As Double
                Dim zjednej(8) As Double
                k1 = Math.Floor(sirke / sirka)
                k2 = Math.Floor(rozmere / rozmer)
                k3 = Math.Floor(velkoste / velkost)
                k4 = Math.Floor(sirke / rozmer)
                k5 = Math.Floor(rozmere / velkost)
                k6 = Math.Floor(velkoste / sirka)
                k7 = Math.Floor(sirke / velkost)
                k8 = Math.Floor(rozmere / sirka)
                k9 = Math.Floor(velkoste / rozmer)

                If k1 * k2 * k3 > k4 * k5 * k6 Then
                    If k1 * k2 * k3 > k7 * k8 * k9 Then
                        jednu = k1 * k2 * k3
                        zjednej(0) = k1
                        zjednej(1) = k2
                        zjednej(2) = k3
                    Else
                        jednu = k7 * k8 * k9
                        zjednej(0) = k7
                        zjednej(1) = k8
                        zjednej(2) = k9
                    End If
                ElseIf k4 * k5 * k6 > k7 * k8 * k9 Then
                    jednu = k4 * k5 * k6
                    zjednej(0) = k4
                    zjednej(1) = k5
                    zjednej(2) = k6
                Else
                    jednu = k7 * k8 * k9
                    zjednej(0) = k7
                    zjednej(1) = k8
                    zjednej(2) = k9
                End If

                rrozmer(5) = 0
                For jj As Integer = 0 To 2
                    rozdiel = Math.Floor(Math.Ceiling((kusov - Math.Floor(kusov / jednu) * jednu) / zjednej(jj)) * zjednej(jj))
                    '   Chyby.Show(rozdiel & "  " & i & "  " & zjednej(jj) & "  " & kusov & "  " & jednu)
                    If rozdiel < 1 Then
                        Continue For
                    End If
                    'Select Case zjednej(jj)
                    'Case k1, k4, k7
                    'Chyby.Show((sirke  - rozdiel * sirka) * rozmere  * velkoste  & "  " & (sirke  - rozdiel * sirka) & "  " & zjednej(jj))
                    If (sirke - rozdiel * sirka) * rozmere * velkoste > rrozmer(5) Then
                        rrozmer(0) = sirke - rozdiel * sirka
                        rrozmer(1) = rozmere
                        rrozmer(2) = velkoste
                        rrozmer(5) = (sirke - rozdiel * sirka) * rozmere * velkoste
                    End If

                    'Case k2, k5, k8

                    If (rozmere - rozmer * rozdiel) * sirke * velkoste > rrozmer(5) Then
                        rrozmer(1) = rozmere - rozmer * rozdiel
                        rrozmer(0) = sirke
                        rrozmer(2) = velkoste
                        rrozmer(5) = (rozmere - rozmer * rozdiel) * sirke * velkoste
                        '       Chyby.Show(rrozmer(2))
                    End If
                    'Case k3, k6, k9
                    If rozmere * sirke * (velkoste - velkost * rozdiel) > rrozmer(5) Then
                        rrozmer(1) = rozmere
                        rrozmer(0) = sirke
                        rrozmer(2) = velkoste - velkost * rozdiel
                        rrozmer(5) = rozmere * sirke * (velkoste - velkost * rozdiel)
                        '       Chyby.Show(rrozmer(2))
                    End If

                    'End Select
                Next

                If rrozmer(5) = 0 Then
                    sirkaN = 0
                    rozmerN = 0
                    s_rozmerN = -1
                    velkostN = 0
                Else
                    rrozmer(5) = 0

                    Array.Sort(rrozmer)
                    Array.Reverse(rrozmer)

                    sirkaN = rrozmer(0)
                    rozmerN = rrozmer(1)
                    velkostN = rrozmer(2)
                    s_rozmerN = -1
                    s_rozmerN = -1

                    '  Chyby.Show(rrozmer(2) & " dfsf")

                    '    If sirkaN  = -1 Then
                    '        Chyby.Show(velkostN  & "  " & velkost)
                    '        If velkostN  < velkost Then

                    '            sirkaN  = 0
                    '            rozmerN  = 0
                    '            velkostN  = 0
                    '        End If
                    '    Else
                    '        If rrozmer(0) * rrozmer(1) * rrozmer(2) < objem Then
                    '            sirkaN  = 0
                    '            rozmerN  = 0
                    '            velkostN  = 0
                    '        End If
                    '    End If
                    '    
                End If

                strata = (sirkaN * rozmerN * velkostN) / 1000 / 1000 / 1000
                strata = Math.Ceiling(kusov / jednu) * objeme - (kusov * objem) - strata  '+ objem * (kusov - Math.Floor(kusov / jednu))
                strata = strata * 1000
                'MessageBox.Show(strata )
                kusk = kuse - Math.Ceiling(kusov / jednu)
            Case "6 - hran"
                Try

                    objeme = Huta_SQL.objem(rozmere, velkoste, sirke, s_rozmere, typ)
                    'strata  = Math.Ceiling(kusov / Math.Floor(velkoste  / velkost))
                    rozdiel = Math.Floor(kusov / Math.Floor(velkoste / velkost))
                    If (kusov - rozdiel * Math.Floor(velkoste / velkost)) < 1 Then
                        sirkaN = 0
                        rozmerN = 0
                        s_rozmerN = 0
                        velkostN = 0
                    Else
                        sirkaN = -1
                        s_rozmerN = -1
                        rozmerN = rozmere
                        velkostN = velkoste - (kusov - rozdiel * Math.Floor(velkoste / velkost)) * velkost
                        '              Chyby.Show(kusov & " " & rozdiel & "  " & i)
                    End If

                    Dim objemN As Double
                    objemN = Huta_SQL.objem(rozmerN, velkostN, sirkaN, s_rozmerN, typ)
                    strata = objeme * Math.Ceiling(kusov / Math.Floor(velkoste / velkost)) - objem * kusov - objemN
                    strata = strata * 1000

                    'Chyby.Show(rozdiel & "  " & (kusov - rozdiel * Math.Floor(velkoste  / velkost)) & "  " & objem)
                    kusk = kuse - Math.Ceiling(kusov / Math.Floor(velkoste / velkost))
                    ' MessageBox.Show(sirke )

                Catch ex As Exception
                    Chyby.Show(ex.ToString)
                End Try
            Case "Rurka"
                Try
                    objeme = Huta_SQL.objem(rozmere, velkoste, sirke, s_rozmere, typ)
                    rozdiel = Math.Floor(kusov / Math.Floor(velkoste / velkost))

                    If (kusov - rozdiel * Math.Floor(velkoste / velkost)) < 1 Then
                        sirkaN = 0
                        rozmerN = 0
                        s_rozmerN = 0
                        velkostN = 0
                    Else
                        sirkaN = sirke
                        s_rozmerN = -1
                        rozmerN = rozmere
                        velkostN = velkoste - (kusov - rozdiel * Math.Floor(velkoste / velkost)) * velkost
                        '              Chyby.Show(kusov & " " & rozdiel & "  " & i)
                    End If

                    Dim objemN As Double
                    objemN = Huta_SQL.objem(rozmerN, velkostN, sirkaN, s_rozmerN, typ)
                    strata = objeme * Math.Ceiling(kusov / Math.Floor(velkoste / velkost)) - objem * kusov - objemN
                    strata = strata * 1000
                    kusk = kuse - Math.Ceiling(kusov / Math.Floor(velkoste / velkost))
                Catch ex As Exception
                    Chyby.Show(ex.ToString)
                End Try
            Case "L - profil", "U - profil", "Jokelt"

                Try
                    objeme = Huta_SQL.objem(rozmere, velkoste, sirke, s_rozmere, typ)
                    rozdiel = Math.Floor(kusov / Math.Floor(velkoste / velkost))

                    If (kusov - rozdiel * Math.Floor(velkoste / velkost)) < 1 Then
                        sirkaN = 0
                        rozmerN = 0
                        s_rozmerN = 0
                        velkostN = 0
                    Else
                        sirkaN = sirke
                        s_rozmerN = s_rozmere
                        rozmerN = rozmere
                        velkostN = velkoste - (kusov - rozdiel * Math.Floor(velkoste / velkost)) * velkost
                        '              Chyby.Show(kusov & " " & rozdiel & "  " & i)
                    End If

                    Dim objemN As Double
                    objemN = Huta_SQL.objem(rozmerN, velkostN, sirkaN, s_rozmerN, typ)
                    strata = objeme * Math.Ceiling(kusov / Math.Floor(velkoste / velkost)) - objem * kusov - objemN
                    strata = strata * 1000
                    kusk = kuse - Math.Ceiling(kusov / Math.Floor(velkoste / velkost))

                Catch ex As Exception
                    Chyby.Show(ex.ToString)
                End Try
        End Select

        Return strata
    End Function


    Public Sub QuickSort(C() As Double, ByVal First As Long, ByVal Last As Long, ByRef X() As Double, ByRef XX() As Double, ByRef XXX() As Double, ByRef XXXX() As Double, ByRef XXXXX() As Double, ByRef y() As Double, ByRef yy() As Double, ByRef yyy() As Double, ByRef yyyy() As Double, ByRef yyyyy() As Double)
        'Dim Low As Long, High As Long
        'Dim MidValue As Long

        'Low = First
        'High = Last
        'MidValue = C((First + Last) \ 2)

        'Do
        '    While Low<High AndAlso C(Low) < MidValue
        '        ' MessageBox.Show(C(Low),MidValue)
        '        Low = Low + 1
        '    End While

        '    While Low<High AndAlso C(High) > MidValue
        '        'MessageBox.Show(C(High), MidValue)
        '        High = High - 1
        '    End While

        '    If Low <= High Then
        '        Swap(C(Low), C(High))
        '        Swap(X(Low), X(High))
        '        ' MessageBox.Show(X(Low) & " " & X(High) & " " & Low & " " & High)
        '        Swap(XX(Low), XX(High))
        '        Swap(XXX(Low), XXX(High))
        '        Swap(y(Low), y(High))
        '        Swap(yy(Low), yy(High))
        '        Swap(yyy(Low), yyy(High))
        '        Swap(yyyy(Low), yyyy(High))
        '        Swap(yyyyy(Low), yyyyy(High))
        '        Swap(XXXX(Low), XXXX(High))
        '        Swap(XXXXX(Low), XXXXX(High))

        '        Low = Low + 1
        '        High = High - 1
        '    End If
        'Loop While Low <= High

        'If First < High Then QuickSort(C, First, High, X, XX, XXX, XXXX, XXXXX, y, yy, yyy, yyyy, yyyyy)
        'If Low < Last Then QuickSort(C, Low, Last, X, XX, XXX, XXXX, XXXXX, y, yy, yyy, yyyy, yyyyy)



        'MessageBox.Show(First & "  " & Last)

        'Dim i As Int32 = First
        'Dim j As Int32 = Last
        '' MessageBox.Show((low + high) \ 2)
        'Dim xPivot As Int32 = C((i + j) \ 2)
        'Do Until i > j
        '    Do While C(i) < xPivot
        '        i += 1
        '    Loop
        '    Do While C(j) > xPivot
        '        j -= 1
        '    Loop

        '    If i <= j Then
        '        Swap(C(i), C(j))
        '        Swap(X(i), X(j))
        '        ' MessageBox.Show(X(Low) & " " & X(High) & " " & Low & " " & High)
        '        Swap(XX(i), XX(j))
        '        Swap(XXX(i), XXX(j))
        '        Swap(y(i), y(j))
        '        Swap(yy(i), yy(j))
        '        Swap(yyy(i), yyy(j))
        '        Swap(yyyy(i), yyyy(j))
        '        Swap(yyyyy(i), yyyyy(j))
        '        Swap(XXXX(i), XXXX(j))
        '        Swap(XXXXX(i), XXXXX(j))
        '        i += 1
        '        j -= 1
        '    End If
        'Loop

        'If (i < Last) Then QuickSort(C, i, Last, X, XX, XXX, XXXX, XXXXX, y, yy, yyy, yyyy, yyyyy)
        'If (j > First) Then QuickSort(C, First, j, X, XX, XXX, XXXX, XXXXX, y, yy, yyy, yyyy, yyyyy)





        ' Wird die Bereichsgrenze nicht angegeben,
        ' so wird das gesamte Array sortiert

        Dim i As Long
        Dim j As Long
        Dim mi As Double

        i = First : j = Last
        mi = C((First + Last) / 2)

        ' Array aufteilen
        Do

            While (C(i) < mi) : i = i + 1 : End While
            While (C(j) > mi) : j = j - 1 : End While

            If (i <= j) Then
                ' Wertepaare miteinander tauschen
                'MessageBox.Show(C(i) & "  " & C(j))
                Swap(C(i), C(j))
                'MessageBox.Show(C(i) & " | " & C(j))
                Swap(X(i), X(j))
                ' MessageBox.Show(X(Low) & " " & X(High) & " " & Low & " " & High)
                Swap(XX(i), XX(j))
                Swap(XXX(i), XXX(j))
                Swap(y(i), y(j))
                Swap(yy(i), yy(j))
                Swap(yyy(i), yyy(j))
                Swap(yyyy(i), yyyy(j))
                Swap(yyyyy(i), yyyyy(j))
                Swap(XXXX(i), XXXX(j))
                Swap(XXXXX(i), XXXXX(j))



                i = i + 1 : j = j - 1
            End If
        Loop Until (i > j)

        ' Rekursion (Funktion ruft sich selbst auf)
        If (First < j) Then QuickSort(C, First, j, X, XX, XXX, XXXX, XXXXX, y, yy, yyy, yyyy, yyyyy)
        If (i < Last) Then QuickSort(C, i, Last, X, XX, XXX, XXXX, XXXXX, y, yy, yyy, yyyy, yyyyy)




    End Sub

    Private Sub Swap(ByRef A As Double, ByRef B As Double)
        Dim T As Double

        T = A
        A = B
        B = T
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Form78.exportovat(DataGridView2)
    End Sub



    Private Sub zmizni()
        If (ListBox1.Visible) And ((Cursor.Position.X < ListBox1.Location.X) Or (Cursor.Position.X > (ListBox1.Location.X + ListBox1.Size.Width)) Or (Cursor.Position.Y < ListBox1.Location.Y) Or (Cursor.Position.Y > (ListBox1.Location.Y + ListBox1.Size.Height))) Then
            ListBox1.Hide()
        End If
    End Sub

    Private Sub Hchyba_Click(sender As System.Object, e As System.EventArgs) Handles MyBase.Click
        zmizni()
    End Sub

    Private Sub DataGridView2_Click(sender As System.Object, e As System.EventArgs) Handles DataGridView2.Click
        zmizni()

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListBox1.SelectedIndexChanged

        For Each s As Udaje In ds
            If ListBox1.SelectedItem = "Zadať iné" Then
                GroupBox1.Visible = True
                ListBox1.Hide()
                GroupBox1.Location = New System.Drawing.Point(550, 67 + 50 + (kks + 1) * DataGridView2.Rows(0).Height)
                TextBox2.Text = ""
                TextBox1.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                Select Case DataGridView2.Rows(kks).Cells(3).Value
                    Case "Valec"
                        RadioButton3.Checked = True
                        RadioButton2.Checked = True
                        TextBox1.Text = "-1"
                        TextBox4.Text = "-1"
                        TextBox2.Text = DataGridView2.Rows(kks).Cells(10).Value
                        TextBox2.Focus()

                    Case "Plech"
                        RadioButton3.Checked = True
                        RadioButton1.Checked = True
                        TextBox1.Focus()
                        TextBox4.Text = "-1"
                    Case "6 - hran"
                        RadioButton1.Checked = True
                        RadioButton3.Checked = True
                        TextBox2.Text = DataGridView2.Rows(kks).Cells(10).Value
                        TextBox2.Focus()
                    Case "Rurka"
                        RadioButton1.Checked = True
                        RadioButton4.Checked = True
                        TextBox1.Focus()
                    Case "L - profil"
                        RadioButton1.Checked = True
                        RadioButton5.Checked = True
                        TextBox1.Focus()
                    Case "U - profil"
                        RadioButton1.Checked = True
                        RadioButton6.Checked = True
                        TextBox1.Focus()
                    Case "Jokelt"
                        RadioButton1.Checked = True
                        RadioButton7.Checked = True
                        TextBox1.Focus()
                End Select
                Exit Sub
            End If
            If kks = s.poradie And s.slovo = ListBox1.SelectedItem Then
                DataGridView2.Rows(kks).Cells(9).Value = s.sirka
                DataGridView2.Rows(kks).Cells(10).Value = s.rozmer
                DataGridView2.Rows(kks).Cells(11).Value = s.s_rozmer
                DataGridView2.Rows(kks).Cells(12).Value = s.velkost
                DataGridView2.Rows(kks).Cells(13).Value = s.kusov
                DataGridView2.Rows(kks).Cells(14).Value = s.kusov2
                DataGridView2.Rows(kks).Cells(15).Value = s.strata
                DataGridView2.Rows(kks).Cells(17).Value = s.sirkaN
                DataGridView2.Rows(kks).Cells(18).Value = s.rozmerN
                DataGridView2.Rows(kks).Cells(19).Value = s.s_rozmerN
                DataGridView2.Rows(kks).Cells(20).Value = s.velkostN
                If s.rozmerN > 0 Or s.velkostN > 0 Or s.s_rozmerN > 0 Or s.sirkaN > 0 Then
                    DataGridView2.Rows(kks).Cells(21).Value = 1
                End If

            End If
            If ListBox1.SelectedItems.Count = 0 Then
                ListBox1.Hide()
            End If
        Next
        napln()

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            TextBox4.Text = "-1"
            TextBox1.Text = "-1"
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Visible = False
            '  ListBox2.Visible=False
            'TextBox7.Focus()
            TextBox4.Visible = False
            Label7.Hide()
            Label3.Hide()
            Label3.Text = "Šírka [mm]"
            Label4.Text = "Priemer [mm]"
            TextBox2.Focus()

        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        stuk()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        GroupBox1.Hide()

    End Sub

    'Private Sub stuk()
    '    Dim objem1, sirka1, kus1, rozmer1, velkost1 As Integer
    '    Dim objem2, sirka2, kus2, rozmer2, velkost2, jednu, rozdiel, strata, kusk As Integer
    '    kus1 = DataGridView2.Rows(kks).Cells(6).Value
    '    kus2 = 0

    '    Try
    '        sirka2 = TextBox1.Text
    '        rozmer2 = TextBox2.Text
    '        velkost2 = TextBox3.Text
    '    Catch ex As Exception
    '        Exit Sub
    '    End Try

    '    sirka1 = DataGridView2.Rows(kks).Cells(3).Value
    '    rozmer1 = DataGridView2.Rows(kks).Cells(4).Value
    '    velkost1 = DataGridView2.Rows(kks).Cells(5).Value

    '    If sirka1 = -1 Then
    '        objem1 = rozmer1 * 3.14 * rozmer1 * velkost1
    '    Else
    '        objem1 = rozmer1 * velkost1 * sirka1
    '    End If



    '    If sirka2 = -1 Then
    '        If (rozmer1 <> rozmer2) Or (sirka1 <> sirka2) Then
    '            Chyby.Show("Neda sa")
    '            Exit Sub
    '        End If

    '        For Each ss As Udaje In ds
    '            If ss.poradie = kks And velkost1 = velkost2 Then
    '                Chyby.Show("Je v zozname")
    '                Exit Sub
    '            End If
    '        Next
    '        objem2 = 3.14 * rozmer2 * rozmer2 * velkost2 / 4
    '        strata = Math.Ceiling(kus1 / Math.Floor(velkost2 / velkost1))
    '        rozdiel = Math.Floor(kus1 / Math.Floor(velkost2 / velkost1))
    '        strata = ((objem2 * rozdiel) + objem1 * (kus1 - rozdiel * Math.Floor(velkost2 / velkost1))) - (objem1 * kus1)
    '        kusk = kus2 - Math.Ceiling(kus1 / Math.Floor(velkost2 / velkost1))
    '        Dim s As String = sirka2 & "x" & rozmer2 & "x" & velkost2 & " odpad: " & strata
    '        ds.Add(New Udaje(sirka2, rozmer2, velkost2, strata, kus2, kusk, s, kks, {0, 0, 0}))

    '    Else
    '        If sirka2 < rozmer2 Then
    '            Dim pom As Integer = sirka2
    '            sirka2 = rozmer2
    '            rozmer2 = pom
    '            If velkost2 > sirka2 Then
    '                pom = rozmer2
    '                rozmer2 = sirka2
    '                sirka2 = velkost2
    '                velkost2 = pom
    '            ElseIf velkost2 > rozmer2 Then
    '                pom = rozmer2
    '                rozmer2 = velkost2
    '                velkost2 = pom
    '            End If
    '        ElseIf sirka2 < velkost2 Then
    '            Dim pom As Integer = rozmer2
    '            rozmer2 = sirka2
    '            sirka2 = velkost2
    '            velkost2 = pom

    '        End If
    '        For Each ss As Udaje In ds
    '            If ss.poradie = kks And sirka1 = sirka2 And rozmer1 = rozmer2 And velkost1 = velkost2 Then
    '                Chyby.Show("Je v zozname")
    '                Exit Sub
    '            End If
    '        Next

    '        Dim zjednej(3), rrozmer(3) As Double

    '        objem2 = rozmer2 * sirka2 * velkost2
    '        Dim k1, k2, k3, k4, k5, k6, k7, k8, k9 As Integer

    '        k1 = Math.Floor(sirka2 / sirka1)
    '        k2 = Math.Floor(rozmer2 / rozmer1)
    '        k3 = Math.Floor(velkost2 / velkost1)
    '        k4 = Math.Floor(sirka2 / rozmer1)
    '        k5 = Math.Floor(rozmer2 / velkost1)
    '        k6 = Math.Floor(velkost2 / sirka1)
    '        k7 = Math.Floor(sirka2 / velkost1)
    '        k8 = Math.Floor(rozmer2 / sirka1)
    '        k9 = Math.Floor(velkost2 / rozmer1)

    '        If k1 * k2 * k3 > k4 * k5 * k6 Then
    '            If k1 * k2 * k3 > k7 * k8 * k9 Then
    '                jednu = k1 * k2 * k3
    '                zjednej(0) = k1
    '                zjednej(1) = k2
    '                zjednej(2) = k3
    '            Else
    '                jednu = k7 * k8 * k9
    '                zjednej(0) = k7
    '                zjednej(1) = k8
    '                zjednej(2) = k9
    '            End If
    '        ElseIf k4 * k5 * k6 > k7 * k8 * k9 Then
    '            jednu = k4 * k5 * k6
    '            zjednej(0) = k4
    '            zjednej(1) = k5
    '            zjednej(2) = k6
    '        Else
    '            jednu = k7 * k8 * k9
    '            zjednej(0) = k7
    '            zjednej(1) = k8
    '            zjednej(2) = k9
    '        End If

    '        If jednu < 1 Then
    '            Chyby.Show("Neda sa")
    '            Exit Sub
    '        End If

    '        If zjednej(0) > zjednej(1) Then
    '            If zjednej(0) < zjednej(2) Then
    '                zjednej(0) = zjednej(2)
    '            End If
    '        ElseIf zjednej(1) < zjednej(2) Then
    '            zjednej(0) = zjednej(2)
    '        Else
    '            zjednej(0) = zjednej(1)
    '        End If

    '        Select Case zjednej(0)
    '            Case k1, k4, k7
    '                rrozmer(0) = sirka2
    '                rrozmer(1) = rozmer1
    '                rrozmer(2) = velkost1
    '            Case k2, k5, k8
    '                rrozmer(0) = rozmer2
    '                rrozmer(1) = sirka1
    '                rrozmer(2) = velkost1
    '            Case k3, k6, k9
    '                rrozmer(1) = rozmer1
    '                rrozmer(2) = sirka1
    '                rrozmer(0) = velkost2
    '        End Select


    '        rozdiel = kus1 - Math.Floor(kus1 / jednu)
    '        strata = Math.Ceiling(rozdiel / zjednej(0)) * rrozmer(0) * rrozmer(1) * rrozmer(2)
    '        strata = strata + Math.Floor(kus1 / jednu) * objem2 - (kus1 * objem1) + objem1 * (kus1 - Math.Floor(kus1 / jednu))
    '        kusk = kus2 - Math.Ceiling(kus1 / jednu)
    '        Dim s As String = sirka2 & "x" & rozmer2 & "x" & velkost2 & " odpad: " & strata
    '        ds.Add(New Udaje(sirka2, rozmer2, velkost2, strata, kus2, kusk, s, kks, rrozmer))
    '    End If


    '    DataGridView2.Rows(kks).Cells(7).Value = TextBox1.Text
    '    DataGridView2.Rows(kks).Cells(8).Value = TextBox2.Text
    '    DataGridView2.Rows(kks).Cells(9).Value = TextBox3.Text
    '    DataGridView2.Rows(kks).Cells(10).Value = 0
    '    DataGridView2.Rows(kks).Cells(11).Value = kusk
    '    DataGridView2.Rows(kks).Cells(12).Value = strata
    '    GroupBox1.Hide()
    '    napln()
    'End Sub


    Private Sub stuk()
        Dim sirka1, kus1, rozmer1, s_rozmer1, velkost1 As Integer
        Dim objem1, objem2, sirka2, kus2, rozmer2, s_rozmer2, velkost2, jednu, rozdiel, strata, kusk, zjednej(15), rrozmer(6) As Double
        Dim sirkaN, rozmerN, s_rozmerN, velkostN As Integer
        Dim typ As String

        kus1 = DataGridView2.Rows(kks).Cells(8).Value

        Try
            sirka2 = TextBox1.Text
            s_rozmer2 = TextBox4.Text
            rozmer2 = TextBox2.Text
            velkost2 = TextBox3.Text
        Catch ex As Exception
            Chyby.Show("Zle zadane rozmery")
            Exit Sub
        End Try
        typ = DataGridView2.Rows(kks).Cells(3).Value
        Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {4}='{5}' AND {6}='{7}' AND {8}='{9}' AND {10}='{11}' AND {12}='{13}' AND {14}='{15}'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.DruhColumn, DataGridView2.Rows(kks).Cells(1).Value, RotekDataSet.Huta.NazovColumn, DataGridView2.Rows(kks).Cells(2).Value, RotekDataSet.Huta.SirkaColumn, sirka2, RotekDataSet.Huta.VelkostColumn, velkost2, RotekDataSet.Huta.RozmerColumn, rozmer2, RotekDataSet.Huta.S_rozmerColumn, s_rozmer2, RotekDataSet.Huta.TypColumn, typ)
        If sklad.RowCount = 1 Then
            kus2 = sklad.Rows(0).Cells(6).Value
        Else
            kus2 = 0

        End If

        sirka1 = DataGridView2.Rows(kks).Cells(4).Value
        rozmer1 = DataGridView2.Rows(kks).Cells(5).Value
        s_rozmer1 = DataGridView2.Rows(kks).Cells(6).Value
        velkost1 = DataGridView2.Rows(kks).Cells(7).Value

        objem1 = Huta_SQL.objem(rozmer1, velkost1, sirka1, s_rozmer1, typ)

        Select Case typ

            Case "Valec"
                If sirka1 <> sirka2 Or rozmer1 > rozmer2 Or velkost1 > velkost2 Or s_rozmer1 <> s_rozmer2 Then
                    Chyby.Show("Zle zadane rozmery. Prosim napravit")
                    Exit Sub
                End If
            Case "Plech"
                If velkost1 > velkost2 Or rozmer1 > rozmer2 Or velkost1 > velkost2 Or s_rozmer1 <> s_rozmer2 Then
                    Chyby.Show("Zle zadane rozmery. Prosim napravit")
                    Exit Sub
                End If
            Case "6 - hran"
                If sirka1 <> sirka2 Or rozmer1 > rozmer2 Or velkost1 > velkost2 Or s_rozmer1 <> s_rozmer2 Then
                    Chyby.Show("Zle zadane rozmery. Prosim napravit")
                    Exit Sub
                End If
            Case "Rurka"
                If sirka2 > rozmer2 Then
                    Dim t As Integer = rozmer2
                    rozmer2 = sirka2
                    sirka2 = t
                End If

                If sirka1 < sirka2 Or rozmer1 > rozmer2 Or velkost1 > velkost2 Or s_rozmer1 <> s_rozmer2 Then
                    Chyby.Show("Zle zadane rozmery. Prosim napravit")
                    Exit Sub
                End If
            Case "L - profil"
                If sirka2 < rozmer2 Then
                    Dim t As Integer = rozmer2
                    rozmer2 = sirka2
                    sirka2 = t
                End If

                If sirka1 > sirka2 Or rozmer1 > rozmer2 Or velkost1 > velkost2 Or s_rozmer1 > s_rozmer2 Then
                    Chyby.Show("Zle zadane rozmery. Prosim napravit")
                    Exit Sub
                End If
            Case "U - profil"

                If sirka1 > sirka2 Or rozmer1 > rozmer2 Or velkost1 > velkost2 Or s_rozmer1 > s_rozmer2 Or sirka2 - s_rozmer2 - s_rozmer2 > sirka1 - s_rozmer1 - s_rozmer1 Then
                    Chyby.Show("Zle zadane rozmery. Prosim napravit")
                    Exit Sub
                End If
            Case "Jokelt"

                If sirka1 > sirka2 Or rozmer1 > rozmer2 Or velkost1 > velkost2 Or s_rozmer1 > s_rozmer2 Or sirka2 - s_rozmer2 - s_rozmer2 > sirka1 - s_rozmer1 - s_rozmer1 Or rozmer2 - s_rozmer2 - s_rozmer2 > rozmer2 - s_rozmer1 - s_rozmer1 Then
                    Chyby.Show("Zle zadane rozmery. Prosim napravit")
                    Exit Sub
                End If


            Case "Jokelt"

        End Select

        objem2 = Huta_SQL.objem(rozmer2, velkost2, sirka2, s_rozmer2, typ)

        'MessageBox.Show(sirka2 & "  " & rozmer2 & "  " & s_rozmer2 & "  " & velkost2 & "  " & kus2 & "  " & typ & "  " & objem2 & "  " & sirka1 & "  " & rozmer1 & "  " & s_rozmer1 & "  " & velkost1 & "  " & kus1 & "  " & sirkaN & "  " & rozmerN & "  " & s_rozmerN & "  " & velkostN & "  " & kusk & "  " & String.Format("0,0.00000", objem1))
        strata = vyhodnot(sirka2, rozmer2, s_rozmer2, velkost2, kus2, typ, objem2, sirka1, rozmer1, s_rozmer1, velkost1, kus1, sirkaN, rozmerN, s_rozmerN, velkostN, kusk, objem1)


        'strata = rozmer2 * sirka2 * velkost2 - (rrozmer(0) * rrozmer(1) * rrozmer(2))
        'strata = strata + Math.Floor(kus1 / jednu) * objem2 - (kus1 * objem1) + objem1 * (kus1 - Math.Floor(kus1 / jednu))
        'kusk = kus2 - Math.Ceiling(kus1 / jednu)


        DataGridView2.Rows(kks).Cells(9).Value = TextBox1.Text
        DataGridView2.Rows(kks).Cells(10).Value = TextBox2.Text
        DataGridView2.Rows(kks).Cells(11).Value = TextBox4.Text
        DataGridView2.Rows(kks).Cells(12).Value = TextBox3.Text
        DataGridView2.Rows(kks).Cells(13).Value = kus2
        DataGridView2.Rows(kks).Cells(14).Value = kusk
        DataGridView2.Rows(kks).Cells(15).Value = strata
        DataGridView2.Rows(kks).Cells(17).Value = sirkaN
        DataGridView2.Rows(kks).Cells(18).Value = rozmerN
        DataGridView2.Rows(kks).Cells(19).Value = s_rozmerN
        DataGridView2.Rows(kks).Cells(20).Value = velkostN
        If velkostN <> 0 Then
            DataGridView2.Rows(kks).Cells(21).Value = 1
        End If

        ' strata = Math.Round(strata) / 1000000
        Dim s As String = String.Format("{0,4}x{1,4}x{2,4}x{3,4}| Odpad:{4,10} Kusov:{5,3}", sirka2, rozmer2, s_rozmer2, velkost2, String.Format("{0:0.00000}", strata), kus2)

        ds.Add(New Udaje(sirka2, rozmer2, velkost2, strata, kus2, kusk, s, kks, {sirkaN, rozmerN, velkostN, s_rozmerN}, typ))

        GroupBox1.Hide()
        napln()
    End Sub

    Private Sub TextBox3_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then
            stuk()
        End If
    End Sub

    Public Function napln() As Boolean
        Dim druh, nazov, typ As String
        Dim sirka, rozmer, s_rozmer, dlzka, kusov, kusov2 As Integer
        Dim druh2, nazov2 As String
        Dim sirka2, rozmer2, dlzka2, s_rozmer2, kusov3, kusov4 As Integer
        Dim pravda As Boolean = True
        Try

            For i As Integer = 0 To DataGridView2.RowCount - 1
                druh = DataGridView2.Rows(i).Cells(1).Value
                nazov = DataGridView2.Rows(i).Cells(2).Value
                typ = DataGridView2.Rows(i).Cells(3).Value
                sirka = DataGridView2.Rows(i).Cells(9).Value
                rozmer = DataGridView2.Rows(i).Cells(10).Value
                s_rozmer = DataGridView2.Rows(i).Cells(11).Value
                dlzka = DataGridView2.Rows(i).Cells(12).Value
                kusov = DataGridView2.Rows(i).Cells(13).Value
                kusov2 = DataGridView2.Rows(i).Cells(14).Value
                If kusov2 < 0 Then pravda = False
                For ii As Integer = i + 1 To DataGridView2.RowCount - 1
                    druh2 = DataGridView2.Rows(ii).Cells(1).Value
                    nazov2 = DataGridView2.Rows(ii).Cells(2).Value
                    sirka2 = DataGridView2.Rows(ii).Cells(9).Value
                    rozmer2 = DataGridView2.Rows(ii).Cells(10).Value
                    s_rozmer2 = DataGridView2.Rows(ii).Cells(11).Value
                    dlzka2 = DataGridView2.Rows(ii).Cells(12).Value
                    kusov3 = DataGridView2.Rows(ii).Cells(13).Value
                    kusov4 = DataGridView2.Rows(ii).Cells(14).Value
                    If druh = druh2 And nazov = nazov2 And sirka = sirka2 And rozmer = rozmer2 And s_rozmer = s_rozmer2 And dlzka = dlzka2 Then
                        DataGridView2.Rows(ii).Cells(13).Value = kusov2
                        DataGridView2.Rows(ii).Cells(14).Value = kusov2 - kusov3 + kusov4
                    End If
                Next
            Next

        Catch ex As Exception
            Chyby.Show(ex.ToString)
            Return False
        End Try

        Return pravda
    End Function

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click

        napln()

        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Dim con As New SqlConnection
        con.ConnectionString = My.Settings.Rotek2
        Dim sql As String
        con.Open()
        Dim cmd As New SqlCommand
        'cmd = New SqlCommand(Sql, con)
        'Try
        '    cmd.ExecuteNonQuery()
        'Catch ex As Exception
        '    Chyby.Show(Sql + vbNewLine + ex.ToString)
        'End Try


        Dim zakazka, skladeNa As Udaje
        Dim hustota, cena, vaha, kuso, srot, srotcena As Double
        srot = 0
        srotcena = 0
        Dim razy As Integer = 0
        Dim over As String = ""
        Try

            For i As Integer = 0 To DataGridView2.RowCount - 1
                zakazka = New Udaje(DataGridView2.Rows(i).Cells(4).Value, DataGridView2.Rows(i).Cells(5).Value(), DataGridView2.Rows(i).Cells(6).Value(), DataGridView2.Rows(i).Cells(7).Value(), DataGridView2.Rows(i).Cells(1).Value, DataGridView2.Rows(i).Cells(2).Value, DataGridView2.Rows(i).Cells(6).Value, DataGridView2.Rows(i).Cells(3).Value())
                skladeNa = New Udaje(DataGridView2.Rows(i).Cells(9).Value, DataGridView2.Rows(i).Cells(10).Value(), DataGridView2.Rows(i).Cells(11).Value(), DataGridView2.Rows(i).Cells(12).Value(), DataGridView2.Rows(i).Cells(1).Value, DataGridView2.Rows(i).Cells(2).Value, DataGridView2.Rows(i).Cells(13).Value, DataGridView2.Rows(i).Cells(3).Value(), DataGridView2.Rows(i).Cells(17).Value(), DataGridView2.Rows(i).Cells(18).Value(), DataGridView2.Rows(i).Cells(19).Value(), DataGridView2.Rows(i).Cells(20).Value())
                zakazka.podzakazka = DataGridView2.Rows(i).Cells(0).Value

                skladeNa.kusov2 = DataGridView2.Rows(i).Cells(14).Value
                skladeNa.strata = DataGridView2.Rows(i).Cells(15).Value

                If zakazka.podzakazka <> over Then
                    over = zakazka.podzakazka
                    Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.ZakazkaColumn, Me.zakazka, RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.PodzakazkaColumn, over)
                    If DataGridView3.RowCount = 1 Then
                        razy = DataGridView3.Rows(0).Cells(4).Value
                        If sablaa Is Nothing Or razy = 0 Then
                            razy = 1
                        Else
                            For ii As Integer = 0 To sablaa.Count - 1
                                If sablaa(ii) = over Then
                                    razy = razy + iablaa(ii)
                                End If
                            Next

                            sql = "UPDATE Zakazka SET Razy=" & 2 & " WHERE Zakazka='" + Me.zakazka + "' AND pocet=1"
                            cmd = New SqlCommand(sql, con)
                            cmd.ExecuteNonQuery()

                            sql = "UPDATE Zakazka SET Rozprac=" & 0 & " WHERE Zakazka='" + Me.zakazka + "' AND pocet=2 AND Podzakazka='" + over + "'"
                            cmd = New SqlCommand(sql, con)
                            cmd.ExecuteNonQuery()

                        End If

                        sql = "UPDATE Zakazka SET Razy=" & razy & " WHERE Zakazka='" + Me.zakazka + "' AND pocet=2 AND Podzakazka='" + over + "'"
                        cmd = New SqlCommand(sql, con)
                        cmd.ExecuteNonQuery()

                    Else
                        Chyby.Show("Chyba")
                        Exit Sub
                    End If
                End If


                Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.DruhColumn, skladeNa.druh, RotekDataSet.Huta.NazovColumn, skladeNa.nazov)
                If sklad.RowCount = -1 Then
                    Chyby.Show("Nenasiel sa typ materialu")
                    Exit Sub
                End If
                hustota = sklad.Rows(0).Cells(7).Value
                cena = sklad.Rows(0).Cells(8).Value

                Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}='{7}' AND {8}='{9}' AND {10}='{11}' AND {12}='{13}' AND {14}='{15}'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.DruhColumn, skladeNa.druh, RotekDataSet.Huta.NazovColumn, skladeNa.nazov, RotekDataSet.Huta.SirkaColumn, skladeNa.sirka, RotekDataSet.Huta.VelkostColumn, skladeNa.velkost, RotekDataSet.Huta.RozmerColumn, skladeNa.rozmer, RotekDataSet.Huta.S_rozmerColumn, skladeNa.s_rozmer, RotekDataSet.Huta.TypColumn, skladeNa.typ)

                If sklad.RowCount = 0 Then
                    sql = "Insert INTO Huta (Druh,  Nazov, Sirka, Rozmer, S_rozmer, Velkost, pocet, srot, srotcena, Cena, Hustota, Vaha, Kusov, Typ) VALUES ('" + skladeNa.druh + "', '" + skladeNa.nazov + "'," & skladeNa.sirka & "," & skladeNa.rozmer & "," & skladeNa.s_rozmer & "," & skladeNa.velkost & ",'" & 0 & "','" & 0 & "','" & 0 & "','" & cena & "','" & hustota & "','" & 0 & "'," & 0 & ",'" + skladeNa.typ + "')"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                    con.Close()
                    Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
                    con.Open()
                    Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}='{7}' AND {8}='{9}' AND {10}='{11}' AND {12}='{13}' AND {14}='{15}'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.DruhColumn, skladeNa.druh, RotekDataSet.Huta.NazovColumn, skladeNa.nazov, RotekDataSet.Huta.SirkaColumn, skladeNa.sirka, RotekDataSet.Huta.VelkostColumn, skladeNa.velkost, RotekDataSet.Huta.RozmerColumn, skladeNa.rozmer, RotekDataSet.Huta.S_rozmerColumn, skladeNa.s_rozmer, RotekDataSet.Huta.TypColumn, skladeNa.typ)

                End If

                skladeNa.objem = Huta_SQL.objem(skladeNa.rozmer, skladeNa.velkost, skladeNa.sirka, skladeNa.s_rozmer, skladeNa.typ)
                skladeNa.objemN = Huta_SQL.objem(skladeNa.rozmerN, skladeNa.velkostN, skladeNa.sirkaN, skladeNa.s_rozmerN, skladeNa.typ)

                zakazka.objem = Huta_SQL.objem(zakazka.rozmer, zakazka.velkost, zakazka.sirka, zakazka.s_rozmer, zakazka.typ)

                skladeNa.srot = sklad.Rows(0).Cells(9).Value
                skladeNa.srot = skladeNa.srot + zakazka.objem * zakazka.kusov * hustota + skladeNa.strata * hustota
                skladeNa.srotcena = sklad.Rows(0).Cells(10).Value
                skladeNa.srotcena = skladeNa.srotcena + (zakazka.objem * zakazka.kusov * hustota + skladeNa.strata * hustota) * cena

                kuso = sklad.Rows(0).Cells(6).Value
                sql = "UPDATE Huta SET Kusov=" & (kuso - (skladeNa.kusov - skladeNa.kusov2)) & ", srot='" & skladeNa.srot & "', srotcena='" & skladeNa.srotcena & "' WHERE Druh='" + skladeNa.druh + "'  AND Nazov='" + skladeNa.nazov + "' AND Rozmer=" & skladeNa.rozmer & " AND S_rozmer=" & skladeNa.s_rozmer & " AND pocet='0' AND Sirka=" & skladeNa.sirka & " AND Velkost=" & skladeNa.velkost & " AND Typ='" & skladeNa.typ & "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                '     Chyby.Show(sql)

                sql = "Insert INTO Huta (Druh,  Nazov, Sirka, Rozmer, S_rozmer, Velkost, pocet,  Kusov, Typ, D_ukoncenia) VALUES ('" + skladeNa.druh + "', '" + skladeNa.nazov + "'," & skladeNa.sirka & "," & skladeNa.rozmer & "," & skladeNa.s_rozmer & "," & skladeNa.velkost & ",'" & 5 & "'," & (skladeNa.kusov - skladeNa.kusov2) & ",'" + skladeNa.typ + "','" & Now.ToString("yyyy-MM-dd") & "')"
                cmd = New SqlCommand(sql, con)
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Chyby.Show(ex.ToString)
                    Exit Sub
                End Try


                If (skladeNa.rozmerN = 0 And skladeNa.velkostN = 0 And skladeNa.sirkaN = 0 And skladeNa.s_rozmer = 0) OrElse (DataGridView2.Rows(i).Cells(21).Value = 0) Then
                Else
                    Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}='{7}' AND {8}='{9}' AND {10}='{11}' AND {12}='{13}' AND {14}='{15}'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.DruhColumn, skladeNa.druh, RotekDataSet.Huta.NazovColumn, skladeNa.nazov, RotekDataSet.Huta.SirkaColumn, skladeNa.sirkaN, RotekDataSet.Huta.VelkostColumn, skladeNa.velkostN, RotekDataSet.Huta.RozmerColumn, skladeNa.rozmerN, RotekDataSet.Huta.S_rozmerColumn, skladeNa.s_rozmerN, RotekDataSet.Huta.TypColumn, skladeNa.typ)
                    vaha = hustota * skladeNa.objemN
                    If sklad.RowCount = 0 Then
                        sql = "Insert INTO Huta (Druh,  Nazov, Sirka, Rozmer, S_rozmer, Velkost, pocet, srot, srotcena, Cena, Hustota, Vaha, Kusov, Typ) VALUES ('" + skladeNa.druh + "', '" + skladeNa.nazov + "'," & skladeNa.sirkaN & "," & skladeNa.rozmerN & "," & skladeNa.s_rozmerN & "," & skladeNa.velkostN & ",'" & 0 & "','" & 0 & "','" & 0 & "','" & cena & "','" & hustota & "','" & vaha & "'," & 1 & ",'" + skladeNa.typ + "')"
                    Else
                        kuso = sklad.Rows(0).Cells(6).Value
                        vaha = sklad.Rows(0).Cells(11).Value + vaha
                        sql = "UPDATE Huta SET Kusov=" & (kuso + 1) & ", Vaha='" & vaha & "' WHERE Druh='" + skladeNa.druh + "'  AND Nazov='" + skladeNa.nazov + "' AND Rozmer=" & skladeNa.rozmerN & " AND pocet='0' AND Sirka=" & skladeNa.sirkaN & " AND Velkost=" & skladeNa.velkostN & " AND S_rozmer=" & skladeNa.s_rozmerN & " AND Typ='" & skladeNa.typ & "'"
                    End If
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End If

                Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}='{7}' AND {8}='{9}' AND {10}='{11}' AND {12}='{13}' AND {14}='{15}' AND {16}='{17}' AND {18}='{19}'", RotekDataSet.Huta.pocetColumn, 3, RotekDataSet.Huta.DruhColumn, zakazka.druh, RotekDataSet.Huta.NazovColumn, zakazka.nazov, RotekDataSet.Huta.SirkaColumn, zakazka.sirka, RotekDataSet.Huta.VelkostColumn, zakazka.velkost, RotekDataSet.Huta.RozmerColumn, zakazka.rozmer, RotekDataSet.Huta.VahaColumn, zakazka.podzakazka, RotekDataSet.Huta.S_rozmerColumn, zakazka.s_rozmer, RotekDataSet.Huta.TypColumn, zakazka.typ, RotekDataSet.Huta.zakazkaColumn, Me.zakazka)

                zakazka.srot = sklad.Rows(0).Cells(9).Value
                zakazka.srot = zakazka.srot + zakazka.objem * zakazka.kusov * hustota
                zakazka.srotcena = sklad.Rows(0).Cells(10).Value
                zakazka.srotcena = zakazka.srotcena + (zakazka.objem * zakazka.kusov * hustota) * cena
                srot = srot + zakazka.srot
                srotcena = srotcena + zakazka.srotcena

                sql = "UPDATE Huta SET srot='" & zakazka.srot & "', srotcena='" & zakazka.srotcena & "' WHERE Druh='" + skladeNa.druh + "'  AND Nazov='" + skladeNa.nazov + "' AND Rozmer=" & zakazka.rozmer & " AND S_rozmer=" & zakazka.s_rozmer & " AND pocet='3' AND Sirka=" & zakazka.sirka & " AND Velkost=" & zakazka.velkost & " AND Vaha='" + zakazka.podzakazka + "' AND zakazka='" & Me.zakazka & "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                con.Close()
                Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
                con.Open()

            Next

            Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.zakazkaColumn, Me.zakazka, RotekDataSet.Huta.pocetColumn, 2)
            srot = srot + sklad.Rows(0).Cells(9).Value
            srotcena = srotcena + sklad.Rows(0).Cells(10).Value

            sql = "UPDATE Huta SET srot='" & srot & "', srotcena='" & srotcena & "' WHERE Zakazka='" + Me.zakazka + "' AND pocet='2'"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            con.Close()
            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
            con.Open()
            razy = -1
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.ZakazkaColumn, Me.zakazka, RotekDataSet.Zakazka.pocetColumn, 2)
            For i As Integer = 0 To DataGridView3.RowCount - 1
                razy = DataGridView3.Rows(i).Cells(4).Value
                If razy = 0 Then Exit For
            Next
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.ZakazkaColumn, Me.zakazka, RotekDataSet.Zakazka.pocetColumn, 1)

            If razy <> 0 And DataGridView3.Rows(0).Cells(4).Value <> 2 Then
                sql = "UPDATE Zakazka SET Razy=" & 1 & " WHERE Zakazka='" & Me.zakazka & "' AND pocet=1"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                '       Chyby.Show(sql)
            End If

            'If sablaa Is Nothing = False Then
            '    sql = "UPDATE Zakazka SET Razy=" & 2 & " WHERE Zakazka='" & Me.zakazka & "' AND pocet=1"
            'End If

            con.Close()
            vydajka2()
            Me.Close()

        Catch ex As Exception
            Chyby.Show(ex.ToString)
            con.Close()
        End Try

    End Sub


    Private Sub Hchyba_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged
        rozmers()
    End Sub


    Private Sub DataGridView2_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 16 Then
                kks = e.RowIndex
                ListBox1.Visible = True
                If 67 + 50 + (kks + 1) * DataGridView2.Rows(0).Height > Me.Height - ListBox1.Size.Height + 20 Then
                    ListBox1.Location = New System.Drawing.Point(666, Me.Height - ListBox1.Size.Height + 20)

                Else
                    ListBox1.Location = New System.Drawing.Point(666, 67 + 50 + (kks + 1) * DataGridView2.Rows(0).Height)
                End If
                ListBox1.Items.Clear()
                For i As Integer = 0 To ds.Count - 1
                    If ds(i).poradie = kks Then
                        ListBox1.Items.Add(ds(i).slovo)
                    End If
                Next
                'If ListBox1.Items.Count = 1 Then
                '    ListBox1.Items.Clear()
                'End If
                ListBox1.Items.Add("Zadať iné")

            ElseIf e.ColumnIndex = 21 Then
                kks = e.RowIndex
                DataGridView2.Rows(e.RowIndex).Cells(21).Value = 1 - DataGridView2.Rows(e.RowIndex).Cells(21).Value
            End If

        End If

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            TextBox4.Text = "-1"
            TextBox1.Text = "-1"
            TextBox3.Text = ""
            TextBox2.Text = ""

            TextBox1.Visible = False
            TextBox4.Visible = False

            Label7.Hide()
            Label3.Hide()

            Label4.Text = "A [mm]"
            TextBox2.Focus()

        End If

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then

            TextBox4.Text = "-1"
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Visible = True
            TextBox4.Visible = False

            Label7.Hide()
            Label3.Show()
            Label4.Text = "Výška [mm]"
            Label3.Text = "Šírka [mm]"
            'TextBox5.Focus()
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then

            TextBox4.Text = "-1"
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""

            TextBox1.Visible = True
            TextBox4.Visible = False

            Label7.Hide()
            Label3.Show()
            Label3.Text = "d priemer [mm]"
            Label4.Text = "D priemer [mm]"
            'TextBox5.Focus()
        End If

    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked = True Then

            TextBox4.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""

            TextBox1.Visible = True
            TextBox4.Visible = True

            Label7.Show()
            Label3.Show()

            Label3.Text = "A [mm]"
            Label4.Text = "B [mm]"

            'TextBox5.Focus()
        End If

    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        If RadioButton6.Checked = True Then

            TextBox4.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""

            TextBox1.Visible = True
            TextBox4.Visible = True

            Label7.Show()
            Label3.Show()

            Label3.Text = "A [mm]"
            Label4.Text = "B [mm]"
        End If

    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        If RadioButton7.Checked = True Then

            TextBox4.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""

            TextBox1.Visible = True
            TextBox4.Visible = True

            Label7.Show()
            Label3.Show()

            Label3.Text = "A [mm]"
            Label4.Text = "B [mm]"
        End If

    End Sub

    Private Sub DataGridView2_Scroll(sender As Object, e As ScrollEventArgs) Handles DataGridView2.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            Panel1.Location = New Point(Panel1.Location.X + (e.OldValue - e.NewValue), Panel1.Location.Y)
        End If
    End Sub
End Class