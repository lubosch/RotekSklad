Imports OfficeOpenXml
Imports System.IO
Imports NPOI.XSSF.UserModel


Public Class Evidencia
    Private excelSheet As ExcelWorksheet
    Dim vyska As Integer = 15

    Private Sub Evidencia_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        overenie()

        Try
            Me.EvidenciaTableAdapter.Fill(Me.RotekDataSet.Evidencia)
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
        Me.EvidenciaBindingSource.Filter = Nothing
        EvidenciaBindingSource.Sort = "Poradie"
    End Sub
    Private Sub overenie()
        Select Case Form78.heslo
            Case Form78.admin
                Button2.Show()
                DataGridView1.Columns(12).Visible = True

            Case Form78.zakazkar
                Button2.Show()
                DataGridView1.Columns(12).Visible = True

            Case Form78.skladnik
                DataGridView1.Columns(12).Visible = True
                Button2.Show()
            Case Else
                DataGridView1.Columns(12).Visible = False
                Button2.Hide()
        End Select
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim strPath As String = My.Settings.Rotek3 & "Pomocny\Vzor.xlsx"
        Dim cesta As String = My.Settings.Rotek3 & "Vzor.xlsx"

        Try
            My.Computer.FileSystem.CopyFile(cesta, strPath, True)
        Catch ex As Exception
            Chyby.Show("Subor bol zmazany. Prosime vratit Vzor.xlsx do adresara " + cesta)
        End Try

        'zaciatok

        Dim fufukeh As FileInfo = New FileInfo(strPath)
        Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)
        Try
            excelSheet = excelApp.Workbook.Worksheets.First
            Dim riadkov As Integer = 4
            Dim intNewRow As Int32 = 1 + riadkov
            vyska = 38
            Dim strana As Integer = 13

            Me.EvidenciaBindingSource.Sort = "Poradie"
            Dim i As Integer = 0
            Try
                While (DataGridView1.Rows(i).Cells(0).Value <> TextBox1.Text)
                    i = i + 1
                End While
            Catch ex As Exception
                i = 0
            End Try
            i = zaokruhli_dole(i / strana) * strana
            Dim zaciatok As Integer = i

            For i = 0 To DataGridView1.RowCount - 1 - zaciatok
                For ii As Integer = 0 To 11
                    Dim b As String = (Chr(Asc("A") + ii) & intNewRow + i)
                    If ii < 5 And ii > 7 Then
                        excelSheet.Cells(b).Value = DataGridView1.Rows(i + zaciatok).Cells(ii).Value
                    Else
                        excelSheet.Cells(b).Value = (DataGridView1.Rows(i + zaciatok).Cells(ii).Value.ToString.Replace("2012", ""))
                    End If
                    pismo(b)
                Next

                excelSheet.Row(intNewRow + i).Height = excelSheet.Row(intNewRow + i).Height * 2.3999999999999999
                If (i + 1) Mod strana = 0 Then
                    excelSheet.Cells("A1:L" & riadkov).Copy(excelSheet.Cells("A" & intNewRow + i + 1 & ":L" & riadkov + intNewRow + i))
                intNewRow = intNewRow + riadkov

                '                    excelSheet.Row(zaciatok - 1).Height = excelSheet.Row(zaciatok - 1).Height * 4
                End If
            Next

            For i = 0 To excelSheet.Drawings.Count - 1
                If excelSheet.Drawings(i).GetType.ToString = "OfficeOpenXml.Drawing.ExcelPicture" Then
                    Dim pics As Drawing.ExcelPicture
                    pics = excelSheet.Drawings(i)
                    pics.SetSize(100, 37)
                Else
                    Chyby.Show(excelSheet.Drawings(i).GetType.ToString)

                End If
            Next
            excelApp.SaveAs(fufukeh)

            'excelApp.Dispose()
            'hlavicka(0, 3, strPath)

            PrehladO.tlacDoPdf(strPath)
            ' Process.Start(strPath.Replace(".xlsx", ".pdf"))

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub

    'Public Sub hlavicka(odd As Integer, doo As Integer, strpath As String)
    '    Dim fss As FileStream = New FileStream(strpath, FileMode.Open, FileAccess.Read)
    '    Dim wb As XSSFWorkbook = New XSSFWorkbook()
    '    '        wb.SetRepeatingRowsAndColumns(0, 0, 0, 1, 4)
    '    wb.CreateSheet("Fonts")

    '    fss.Close()

    '    Dim fos As FileStream = New FileStream("text.xlsx", FileMode.Create, FileAccess.ReadWrite)
    '    wb.Write(fos)
    '    fos.Close()
    'End Sub

    Public Function zaokruhli_hore(ByVal b As Double) As Integer
        Dim x As Integer = b
        If (b = x) Then
            Return b
        Else
            Return (Math.Round(b + 0.5))
        End If
    End Function

    Public Function zaokruhli_dole(ByVal b As Double) As Integer
        Return (Math.Round(b - 0.5))
    End Function

    Private Sub pismo(ByVal b As String)
        excelSheet.Cells(b).Style.Font.Name = "Times"
        excelSheet.Cells(b).Style.Font.Size = 8


    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim f As New Evid_Uprav()
        f.ShowDialog()
        Me.EvidenciaTableAdapter.Fill(Me.RotekDataSet.Evidencia)
        Me.EvidenciaTableAdapter.Fill(Me.RotekDataSet.Evidencia)

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex = 12 Then

            Dim kks As Integer = e.RowIndex

            Dim f As New Evid_Uprav(DataGridView1.Rows(kks).Cells(0).Value(), _
                                    DataGridView1.Rows(kks).Cells(1).Value(), _
                                    DataGridView1.Rows(kks).Cells(2).Value(), _
                                    DataGridView1.Rows(kks).Cells(3).Value(), _
                                    DataGridView1.Rows(kks).Cells(4).Value(), _
                                    DataGridView1.Rows(kks).Cells(5).Value(), _
                                    DataGridView1.Rows(kks).Cells(6).Value(), _
                                    DataGridView1.Rows(kks).Cells(7).Value(), _
                                    DataGridView1.Rows(kks).Cells(8).Value(), _
                                    DataGridView1.Rows(kks).Cells(9).Value(), _
                                    DataGridView1.Rows(kks).Cells(10).Value(), _
                                    DataGridView1.Rows(kks).Cells(11).Value(), _
                                    Me)
            f.ShowDialog()

            Me.EvidenciaTableAdapter.Fill(Me.RotekDataSet.Evidencia)

        End If

    End Sub
End Class

