Imports System.IO
Imports OfficeOpenXml
Imports System.Data.SqlClient


Public Class DL
    Public zakazka As String
    Private excelSheet As ExcelWorksheet
    Public veduc As String
    Public firma As String

    Private Sub DL_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        Me.ZakazkaBindingSource.Filter = String.Format("{0} <> '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.DLColumn, "", RotekDataSet.Zakazka.pocetColumn, 1)
        TextBox8.Text = Form78.uzivatel

        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.pocetColumn, 1)
        firma = DataGridView1.Rows(0).Cells(7).Value
        veduc = DataGridView1.Rows(0).Cells(8).Value
        If DataGridView1.Rows(0).Cells(10).Value.ToString <> "" Then

            TextBox4.Text = DataGridView1.Rows(0).Cells(10).Value
        End If
        '  TextBox4.ReadOnly = True
        Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, firma)

        ZakazkaBindingSource.Sort = "DL"
        RadioButton2.Checked = True
        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Cells(11).Value.ToString <> "" Then
                DataGridView1.Rows(i).Cells(5).Value = DataGridView1.Rows(i).Cells(11).Value
            End If
        Next

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.pocetColumn, 2)
            PodzakazkaDataGridViewTextBoxColumn.DataPropertyName = "Podzakazka"
            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Cells(3).Value = 1 Then
                    DataGridView1.Rows(i).Cells(0).Value = 1
                End If
                DataGridView1.Rows(i).Cells(5).Value = DataGridView1.Rows(i).Cells(11).Value
            Next
        Else
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.pocetColumn, 1)
            PodzakazkaDataGridViewTextBoxColumn.DataPropertyName = "Meno"
            DataGridView1.Rows(0).Cells(0).Value = 1

        End If

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim dl, dl3, t1, t4, t5, x1, x2, x3 As String
        Dim t2, t3, datum As DateTime
        Dim spolu As Decimal = 0

        datum = DateTimePicker1.Value

        dl = TextBox1.Text
        dl3 = TextBox1.Text
        x1 = TextBox2.Text
        t1 = TextBox4.Text
        t2 = DateTimePicker2.Value.ToShortDateString
        t3 = DateTimePicker3.Value.ToShortDateString
        t4 = TextBox6.Text
        t5 = TextBox7.Text
        x2 = TextBox8.Text
        x3 = TextBox9.Text

        If dl.Length = 0 Then
            Chyby.Show("Nezadané číslo dodacieho listu")
            Exit Sub
        End If
        dl = dl.Replace("/", "↑")
        dl = dl.Replace("\", "↑")

        Dim strpath As String = My.Settings.Rotek3
        strpath = strpath & "\zakazky\" + zakazka + "\"
        strpath = strpath + "Dodacie listy\"

        strpath = strpath + dl + ".xlsx"
        Dim cesta As String = My.Settings.Rotek3 & "DL.xlsx"
        Try
            My.Computer.FileSystem.CopyFile(cesta, strpath, True)
        Catch ex As Exception
            Chyby.Show("Subor bol zmazany. Prosime vratit DL.xlsx do adresara " + cesta)
        End Try

        Dim sk As Decimal
        For i = 0 To DataGridView1.RowCount - 1
            If (DataGridView1.Rows(i).Cells(5).Value Is Nothing OrElse String.IsNullOrEmpty(DataGridView1.Rows(i).Cells(5).Value.ToString)) Then
                Continue For
            End If
            Try
                DataGridView1.Rows(i).Cells(4).Value = DataGridView1.Rows(i).Cells(4).Value.ToString.Replace(".", ",")
                sk = DataGridView1.Rows(i).Cells(4).Value.ToString.Replace(",", ".")

                DataGridView1.Rows(i).Cells(5).Value = DataGridView1.Rows(i).Cells(5).Value.ToString.Replace(".", ",")
                sk = DataGridView1.Rows(i).Cells(5).Value.ToString.Replace(",", ".")
            Catch ex As Exception
                Chyby.Show("Zle vyplnená tabuľka")
                Exit Sub
            End Try
        Next

        'zaciatok

        Dim fufukeh As FileInfo = New FileInfo(strpath)
        Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)

        Try

            excelSheet = excelApp.Workbook.Worksheets.First

            Dim intNewRow As Int32 = 15 'prve volne kde pisat
            Dim endNewRow As Int32 = 11 'posledne volne kde nepisat 
            'Dim vyska As Integer = 38
            Dim strana As Integer = 23 'zmesti sa na stranu


            Dim i As Integer = 0
            Dim ix As Integer = intNewRow + 1
            'Dim kusovka As Decimal
            Dim podzakazka As String = ""

            Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, firma)

            excelSheet.Cells("C3").Value = dl3
            excelSheet.Cells("C6").Value = datum.ToShortDateString
            excelSheet.Cells("C7").Value = t1
            If t2 <> New DateTime(1800, 1, 1) Then
                excelSheet.Cells("C8").Value = t2
            End If
            If t3 <> New DateTime(1800, 1, 1) Then
                excelSheet.Cells("C9").Value = t3
            End If
            excelSheet.Cells("C10").Value = t4
            excelSheet.Cells("C11").Value = t5
            excelSheet.Cells("C42").Value = x1
            excelSheet.Cells("C44").Value = x2
            excelSheet.Cells("C46").Value = x3

            excelSheet.Cells("E6").Value = veduc
            excelSheet.Cells("C39").Value = zakazka
            excelSheet.Cells("D7").Value = firma
            excelSheet.Cells("D8").Value = DataGridView2.Rows(0).Cells(0).Value
            excelSheet.Cells("D9").Value = DataGridView2.Rows(0).Cells(1).Value
            excelSheet.Cells("D10").Value = DataGridView2.Rows(0).Cells(2).Value

            If DataGridView2.Rows(0).Cells(3).Value <> "Slovensko" Then
                excelSheet.Cells("D11").Value = DataGridView2.Rows(0).Cells(3).Value
            End If


            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Cells(0).Value = 0 Then
                    Continue For
                End If

                excelSheet.Cells("A" & ix).Value = i + 1
                podzakazka = DataGridView1.Rows(i).Cells(1).Value
                excelSheet.Cells("B" & ix).Value = podzakazka
                excelSheet.Cells("D" & ix).Value = DataGridView1.Rows(i).Cells(4).Value
                If (DataGridView1.Rows(i).Cells(5).Value Is Nothing OrElse String.IsNullOrEmpty(DataGridView1.Rows(i).Cells(5).Value.ToString)) Then
                Else
                    Dim cena As Decimal = DataGridView1.Rows(i).Cells(5).Value.ToString.Replace(",", ".")
                    'kusovka = DataGridView1.Rows(i).Cells(5).Value
                    excelSheet.Cells("E" & ix).Value = cena
                    ''                excelSheet.cells("E" & ix).Value = String.Format("{0:C2}", kusovka)
                    If DataGridView1.Rows(i).Cells(4).Value * cena = 0 Then
                    Else
                        cena = DataGridView1.Rows(i).Cells(4).Value * DataGridView1.Rows(i).Cells(5).Value
                        '                    excelSheet.Cells("F" & ix).Value = DataGridView1.Rows(i).Cells(4).Value * DataGridView1.Rows(i).Cells(5).Value
                        'excelSheet.Cells("F" & ix).Value = String.Format("{0:C2}", kusovka)
                        spolu = spolu + cena
                    End If


                    SQL_main.Odpalovac("UPDATE Zakazka SET Cena = " & cena & " WHERE Zakazka='" + zakazka + "' AND Podzakazka='" & podzakazka & "' AND pocet=" & 2)
                End If

                excelSheet.Cells("F" & ix).Formula = "=IF(D" & ix & "*E" & ix & "=0,"""",D" & ix & "*E" & ix & ")"
                excelSheet.Cells("F" & ix).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                excelSheet.Cells("A" & ix).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                excelSheet.Cells("B" & ix & ":C" & ix).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                excelSheet.Cells("D" & ix).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)
                excelSheet.Cells("E" & ix).Style.Border.BorderAround(Style.ExcelBorderStyle.Thin)

                ix = ix + 1
                If (i + 1) Mod strana = 0 Then
                    ix = ix + endNewRow
                    excelSheet.Cells("A1:F" & intNewRow).Copy(excelSheet.Cells("A" & ix & ":F" & ix + intNewRow - 1))
                    ix = ix + intNewRow
                    excelSheet.Cells("A" & strana + intNewRow + 1 & ":F" & strana + intNewRow + endNewRow).Copy(excelSheet.Cells("A" & ix + strana & ":F" & ix + strana + endNewRow - 1))
                    'Dim p As Drawing.ExcelPicture = excelSheet.Drawings(0)
                    'p.Image.Clone()

                    'p.SetPosition((i + 1) / strana * 995, 460)
                End If
            Next


            'If ix <> intNewRow Then
            '    excelSheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, strpath.Replace(".xlsx", ""), XlFixedFormatQuality.xlQualityStandard, True, True, 1, 1, True)
            'End If
            ' 
            excelApp.SaveAs(fufukeh)

            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)

            Dim dl2 As String
            Try
                dl2 = DataGridView1.Rows(0).Cells(6).Value
            Catch ex As Exception
                dl2 = ""
            End Try
            dl2 = dl2 & "|" & dl3

            SQL_main.Odpalovac("UPDATE Zakazka SET DL='" + dl2 + "' , Cena = " & spolu & " WHERE Zakazka='" + zakazka + "' AND pocet = 1")

            Process.Start(strpath)
        Catch ex As Exception
            Chyby.Show(ex.ToString)
            Exit Sub
        End Try
        Me.Close()


    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 0 Then
            If e.RowIndex = -1 Then
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    DataGridView1.Rows(i).Cells(0).Value = 1 - DataGridView1.Rows(i).Cells(0).Value
                Next
            Else
                DataGridView1.Rows(e.RowIndex).Cells(0).Value = 1 - DataGridView1.Rows(e.RowIndex).Cells(0).Value
            End If
        End If
    End Sub

    Private Sub DateTimePicker2_Enter(sender As System.Object, e As System.EventArgs) Handles DateTimePicker2.Enter
        sender.value = Now
    End Sub

    Private Sub DateTimePicker3_Enter(sender As System.Object, e As System.EventArgs) Handles DateTimePicker3.Enter
        sender.value = Now
    End Sub
End Class