Imports System.IO
Imports OfficeOpenXml
Imports System.Data.SqlClient


Public Class Z_Info

    Private excelSheet As ExcelWorksheet
    Property zakazka As String

    Private Sub HmakroInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Zakazka' table. You can move, or remove it, as needed.
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT Podzakazka Dielec FROM Zakazka WHERE pocet = 2 AND Zakazka = '" & zakazka & "'", dd)
        DataGridView2.DataSource = dd

        poverenie()
        'dasa()

        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(zakazka, Label1.Font)
        Dim stred As Integer
        Label4.Text = "Hlavna"
        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New System.Drawing.Point(rw, 2)
        Label1.Text = zakazka
        Me.Text =zakazka

        DataGridView2.ClearSelection()

        Nacitaj()
        If (DataGridView2.Rows.Count) > 0 Then
            init_podzakazka(DataGridView2.Rows(0).Cells(0).Value)
        Else
            Clear_podzakazka()
        End If


    End Sub


    Private Sub Nacitaj()
        Dim zakazka_SQL As Zakazka_SQL = New Zakazka_SQL(zakazka)
        Label26.Text = zakazka_SQL.Nazov
        Label20.Text = zakazka_SQL.Zaevidoval
        Label22.Text = zakazka_SQL.Datum_Pridania.ToShortDateString
        Label36.Text = zakazka_SQL.Datum_Ukoncenia_Planovany.ToShortDateString
        Label24.Text = zakazka_SQL.Datum_Ukoncenia_Skutocny_Slovo


        Label28.Text = zakazka_SQL.Objednavka
        Label16.Text = zakazka_SQL.Firma.Nazov
        Label18.Text = zakazka_SQL.Odovzdat
        Label30.Text = zakazka_SQL.Firma.Ulica
        Label34.Text = zakazka_SQL.Firma.Mesto
        Label32.Text = zakazka_SQL.Firma.Krajina

        DataGridView2.DataSource = zakazka_SQL.Podzakazky
        poznamky()

    End Sub

    Private Sub poverenie()
        Select Case Form78.heslo
            Case Form78.admin
            Case Form78.zakazkar
            Case Form78.skladnik
            Case Else

        End Select
    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex >= 0 Then
            Dim riadok As Integer
            riadok = e.RowIndex
            Dim podzakazka As String
            podzakazka = DataGridView2.Rows(riadok).Cells(0).Value
            init_podzakazka(podzakazka)
        End If

    End Sub

    Private Sub init_podzakazka(podzakazka_nazov As String)
        Dim podzakazka As Podzakazka_SQL = New Podzakazka_SQL(podzakazka_nazov, zakazka)
        Label4.Text = podzakazka.Nazov
        Label7.Text = podzakazka.Kusov
        Label12.Text = podzakazka.Tepelna
        Label14.Text = podzakazka.Povrchova
        Label9.Text = podzakazka.Cena
        Label5.Text = podzakazka.CenaKs
    End Sub

    Private Sub Clear_podzakazka()
        Label4.Text = ""
        Label7.Text = ""
        Label12.Text = ""
        Label14.Text = ""
        Label9.Text = ""
        Label5.Text = ""
    End Sub

    Public Sub poznamky()
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT zp.Datum Datum, zp.Poznamka Poznamka, zp.Typ Typ, z.Podzakazka Podzakazka FROM ZakazkaPoznamka zp JOIN Zakazka z ON z.ID = zp.Zakazka_ID WHERE z.Zakazka = '" & zakazka & "' ORDER BY Podzakazka , Datum DESC", dd)
        For i As Integer = 0 To dd.Rows.Count - 1
            Dim datum As Date = dd.Rows(i).Item("Datum")
            Dim typ As String = dd.Rows(i).Item("Typ")
            If typ = "Podzakazka" Then
                Dim podzakazka As String = dd.Rows(i).Item("Podzakazka")
                ListBox1.Items.Add(datum.ToShortDateString & " - " & podzakazka & " |> " & dd.Rows(i).Item("Poznamka").ToString)
            Else
                ListBox1.Items.Add(datum.ToShortDateString & " |> " & dd.Rows(i).Item("Poznamka").ToString)
            End If
        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If (DataGridView2.Rows.Count = 0) Then
                Exit Sub
            End If


            Dim strPath As String = My.Settings.Rotek3 & "Pomocny\" & "zakazka.xlsx"
            Dim cesta As String = My.Settings.Rotek3 & "Zakazka.xlsx"

            Try
                My.Computer.FileSystem.CopyFile(cesta, strPath, True)
            Catch ex As Exception
                Chyby.Show("Subor bol zmazany. Prosime vratit Zakazka.xlsx do adresara " + cesta)
            End Try

            'zaciatok

            Dim fufukeh As FileInfo = New FileInfo(strPath)
            Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)


            Try
                excelSheet = excelApp.Workbook.Worksheets.First
                Dim zakazka_sql As Zakazka_SQL = New Zakazka_SQL(zakazka)

                Dim intNewRow As Int32 = 7
                Dim vyska As Integer = 38
                Dim strana As Integer = 22


                excelSheet.Cells("G1").Value = zakazka
                excelSheet.Cells("A2").Value = zakazka_sql.Nazov
                excelSheet.Cells("D2").Value = zakazka_sql.Firma.Nazov
                excelSheet.Cells("F2").Value = zakazka_sql.Odovzdat
                excelSheet.Cells("A4").Value = zakazka_sql.Objednavka
                excelSheet.Cells("C4").Value = zakazka_sql.Datum_Pridania.ToShortDateString
                excelSheet.Cells("E4").Value = zakazka_sql.Datum_Ukoncenia_Planovany.ToShortDateString
                excelSheet.Cells("F4").Value = zakazka_sql.Datum_Ukoncenia_Skutocny_Slovo
                excelSheet.Cells("G4").Value = zakazka_sql.Zaevidoval

                For i As Integer = 0 To zakazka_sql.Podzakazky.Rows.Count - 1
                    excelSheet.Cells("A" & intNewRow).Value = i + 1
                    Dim podzakazka As Podzakazka_SQL = New Podzakazka_SQL(zakazka_sql.Podzakazky.Rows(i).Item(0), zakazka)
                    excelSheet.Cells("B" & intNewRow).Value = podzakazka.Nazov
                    excelSheet.Cells("C" & intNewRow).Value = podzakazka.Kusov
                    excelSheet.Cells("D" & intNewRow).Value = podzakazka.CenaKs
                    excelSheet.Cells("E" & intNewRow).Value = podzakazka.Cena
                    excelSheet.Cells("F" & intNewRow).Value = podzakazka.Tepelna
                    excelSheet.Cells("G" & intNewRow).Value = podzakazka.Povrchova
                    intNewRow += 1
                Next


                excelApp.SaveAs(fufukeh)

                'PrehladO.tlacDoPdf(strPath)
                Process.Start(strPath)
            Catch ex As Exception
                Chyby.Show(ex.ToString)
            End Try


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub


End Class