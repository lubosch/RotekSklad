Imports OfficeOpenXml
Imports System.IO

Public Class zamestnanec

    Property crc As String
    Public x As Integer
    Shared Property Pocett As Integer

    Shared Property Nastroj As String

    Property menko As String

    Property prezvo As String


    Shared Property bmp As Integer

    Shared Property Nastroj2 As String

    Shared Property spolu As Integer

    Shared Property Cenka As Double

    Shared Property Vlast As String

    Private Sub rozmer()
        Dim sw As Integer = Me.Height
        Dim rww As Integer = Me.Width / 2
        DataGridView1.Size = New Size(620, sw + 200)
        DataGridView3.Location = New Point(620, 189)
        DataGridView3.Size = New Size(rww * 2 - 640, sw + 200)


        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(crc, Label1.Font)
        Dim stred As Integer

        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New Point(rw, 0)

    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo
            Case Form78.admin
                Button2.Show()
                Button1.Show()
                Button4.Show()
                Button5.Show()
                Button6.Show()
                Button7.Show()
                Button8.Show()
                Button9.Show()
                Button10.Show()
                Button11.Show()
                Button19.Show()
                Button20.Show()
                Button21.Show()
                Button22.Show()
                Button23.Show()
                Button24.Show()
                Button25.Show()
                Button26.Show()
                Button27.Show()
                Button28.Show()
                Button29.Show()
                Button30.Show()
                Button31.Show()
                Button32.Show()
                Button37.Show()
                Button38.Show()
                Button39.Show()
                Button41.Show()
                Button42.Show()
                Button43.Show()
                Button45.Show()
                Button46.Show()
                Button47.Show()
                Label2.Show()
                Label4.Show()
                Label5.Show()

            Case Form78.zakazkar
            Case Form78.skladnik
                Button1.Show()
                Button2.Show()

                Button4.Show()
                Button5.Show()
                Button6.Show()
                Button7.Show()
                Button8.Show()
                Button9.Show()
                Button10.Show()
                Button11.Show()
                Button19.Show()
                Button20.Show()
                Button21.Show()
                Button22.Show()
                Button23.Show()
                Button24.Show()
                Button25.Show()
                Button26.Show()
                Button27.Show()
                Button28.Show()
                Button29.Show()
                Button30.Show()
                Button31.Show()
                Button32.Show()
                Button37.Show()
                Button38.Show()
                Button39.Show()
                Button41.Show()
                Button42.Show()
                Button43.Show()
                Button45.Show()
                Button46.Show()
                Button47.Show()
                Label2.Show()
                Label4.Show()
                Label5.Show()
            Case Else
                Button1.Hide()
                Button2.Hide()
                Button4.Hide()
                Button5.Hide()
                Button6.Hide()
                Button7.Hide()
                Button8.Hide()
                Button9.Hide()
                Button10.Hide()
                Button11.Hide()
                Button19.Hide()
                Button20.Hide()
                Button21.Hide()
                Button22.Hide()
                Button23.Hide()
                Button24.Hide()
                Button25.Hide()
                Button26.Hide()
                Button27.Hide()
                Button28.Hide()
                Button29.Hide()
                Button30.Hide()
                Button31.Hide()
                Button32.Hide()
                Button37.Hide()
                Button38.Hide()
                Button39.Hide()
                Button41.Hide()
                Button42.Hide()
                Button43.Hide()
                Button45.Hide()
                Button46.Hide()
                Button47.Hide()
                Label2.Hide()
                Label4.Hide()
                Label5.Hide()

        End Select
    End Sub
    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        poverenie()



        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)

        rozmer()


        x = DataGridView1.RowCount
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' and {2}>'{3}' and {4} like '{5}%' and {6} like '{7}%' and {8}='{9}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.KolkoColumn, 0, RotekDataSet.Rotek.NástrojColumn, TextBox1.Text, RotekDataSet.Rotek.VelkostRColumn, TextBox2.Text, RotekDataSet.Rotek.pocetColumn, 1)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}>'{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 2)
        ''Me.RotekBindingSource.Filter = String.Format("", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.KolkoColumn, 0, RotekDataSet.Rotek.NástrojColumn, TextBox1.Text, RotekDataSet.Rotek.VelkostRColumn, TextBox2.Text, RotekDataSet.Rotek.pocetColumn, 1)
        'Me.RotekBindingSource2.Filter = String.Format("{2}>'{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 0)

        Me.RotekBindingSource.Sort = "Nástroj"
        Me.RotekBindingSource.Sort = "pocet"
        Label1.Text = crc
        ceny()

    End Sub
    Public Sub ceny()
        For radky = 0 To DataGridView3.RowCount - 1
            Dim pe As Double
            Try
                pe = DataGridView3.Rows(radky).Cells(6).Value
            Catch ex As Exception
                pe = 0
            End Try
            DataGridView3.Rows(radky).Cells(5).Value = pe
            If DataGridView3.Rows(radky).Cells(7).Value.ToString.Length = 0 Then DataGridView3.Rows(radky).Cells(3).Value = False Else DataGridView3.Rows(radky).Cells(3).Value = True
        Next
        For radky = 0 To DataGridView1.RowCount - 1
            Dim pe As Double
            If DataGridView1.Rows(radky).Cells(5).Value.ToString.Length = 0 Then pe = 0 Else pe = DataGridView1.Rows(radky).Cells(5).Value
            DataGridView1.Rows(radky).Cells(6).Value = pe
        Next
    End Sub

    Private Sub FillBy4ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

        Catch ex As System.Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New nastrclovek
        Dim w As Integer = 2
        f.crc = crc
        f.menko = menko
        f.prezvo = prezvo

        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()

        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)

        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}>'{3}' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8}='{9}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.KolkoColumn, 0, RotekDataSet.Rotek.NástrojColumn, TextBox1.Text, RotekDataSet.Rotek.VelkostRColumn, TextBox2.Text, RotekDataSet.Rotek.pocetColumn, 1)

        x = DataGridView1.RowCount
        ceny()
    End Sub
    Public Shared Sub doexcel(ByVal menopr As String, ByVal nastr As String, ByVal nastr2 As String, ByVal vlast As String, ByVal pocet As String)

        Try

            Dim proc = Process.GetProcessesByName("EXCEL")
            For i As Integer = 0 To proc.Count - 1
                proc(i).CloseMainWindow()
            Next i

        Catch ex As Exception
            '            Chyby.Show(ex.ToString)
        End Try

        Dim strPath As String = My.Settings.Rotek3 & "Dennik\" & Now.Year & "\" & MonthName(Month(Now)) + ".xlsx"

        Dim excelSheet As ExcelWorksheet
        Dim fufukeh As FileInfo = New FileInfo(strPath)
        Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)

        If My.Computer.FileSystem.FileExists(strPath) Then
        Else
            If System.IO.Directory.Exists(strPath.Substring(0, strPath.LastIndexOf("\"))) Then
            Else
                System.IO.Directory.CreateDirectory(strPath.Substring(0, strPath.LastIndexOf("\")))
            End If
            excelSheet = excelApp.Workbook.Worksheets.Add(menopr)
            excelSheet.Cells("A" & 1).Value = "Dátum"
            excelSheet.Cells("B" & 1).Value = "Nástroj"
            excelSheet.Cells("C" & 1).Value = "Priemer"
            excelSheet.Cells("D" & 1).Value = "Vlastnosť"
            excelSheet.Cells("E" & 1).Value = "Počet"
            excelSheet.Cells("F" & 1).Value = "Pridal"
            For b As Integer = 65 To 69
                excelSheet.Cells(Chr(b) & 1).Style.Font.Bold = True
            Next
            excelApp.SaveAs(fufukeh)
        End If

        Dim WSHShell = CreateObject("WScript.Shell")
        If My.Computer.FileSystem.FileExists(WSHShell.SpecialFolders("Desktop") & "\" & "Dennik\" & Now.Year & "\" & MonthName(Month(Now)) + ".lnk") Then

        Else
            'ikona

            Dim MyShortcut, DesktopPath

            ' Read desktop path using WshSpecialFolders object
            DesktopPath = WSHShell.SpecialFolders("Desktop")
            ' Create a shortcut object on the desktop
            If System.IO.Directory.Exists(DesktopPath & "\" & "Dennik\" & Now.Year & "\") Then
            Else
                System.IO.Directory.CreateDirectory(DesktopPath & "\" & "Dennik\" & Now.Year & "\")
            End If

            MyShortcut = WSHShell.CreateShortcut(DesktopPath & "\" & "Dennik\" & Now.Year & "\" & MonthName(Month(Now)) + ".lnk")
            ' Set shortcut object properties and save it

            MyShortcut.TargetPath = _
                WSHShell.ExpandEnvironmentStrings(strPath)
            MyShortcut.WorkingDirectory = _
               WSHShell.ExpandEnvironmentStrings("%windir%")
            MyShortcut.WindowStyle = 4
            Try
                'MyShortcut.IconLocation = WSHShell.ExpandEnvironmentStrings(strPath, 0)
                MyShortcut.Save()

            Catch ex As Exception
                Chyby.Show(ex.ToString)
            End Try
        End If
        '

        fufukeh = New FileInfo(strPath)
        excelApp = New ExcelPackage(fufukeh)
        Dim intNewRow As Int32 = 1

        Try
            excelSheet = excelApp.Workbook.Worksheets(menopr)
            While excelSheet.Cells("A" & intNewRow).Text.ToString.Length <> 0
                intNewRow = intNewRow + 1
            End While
        Catch ex As Exception
            excelSheet = excelApp.Workbook.Worksheets.Add(menopr)
            excelSheet.Cells("A" & 1).Value = "Dátum"
            excelSheet.Cells("B" & 1).Value = "Nástroj"
            excelSheet.Cells("C" & 1).Value = "Priemer"
            excelSheet.Cells("D" & 1).Value = "Vlastnosť"
            excelSheet.Cells("E" & 1).Value = "Počet"
            excelSheet.Cells("F" & 1).Value = "Pridal"
            For b As Integer = 65 To 69
                excelSheet.Cells(Chr(b) & 1).Style.Font.Bold = True
            Next
            excelSheet = excelApp.Workbook.Worksheets(menopr)
            intNewRow = 2
        End Try

        'Dim excelcells As Excel.cells = excelSheet.Usedcells
        'excelcells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Activate()

     
        Dim strNewCellAddress As String = "A" & intNewRow
        excelSheet.Cells("A" & intNewRow).Value = Format$(Now, "Long Date") + " " + Format$(Now, "Long Time")
        excelSheet.Cells("B" & intNewRow).Value = nastr
        excelSheet.Cells("C" & intNewRow).Value = nastr2
        excelSheet.Cells("D" & intNewRow).Value = vlast
        excelSheet.Cells("E" & intNewRow).Value = pocet
        excelSheet.Cells("F" & intNewRow).Value = Form78.uzivatel
        excelSheet.Column(1).AutoFit()
        excelSheet.Column(2).AutoFit()
        excelSheet.Column(3).AutoFit()
        excelSheet.Column(4).AutoFit()
        excelSheet.Column(5).AutoFit()
        excelSheet.Column(6).AutoFit()

        excelApp.SaveAs(fufukeh)

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub



    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = ""
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        filtruj()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        bmp = 0
        Dim f As New srotC
        f.TopLevel = True
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()

        If bmp = 1 Then
            Try
                Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
            Catch ex As Exception
                Chyby.Show(ex.ToString)
            End Try

        End If

        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}>'{3}' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8}='{9}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.KolkoColumn, 0, RotekDataSet.Rotek.NástrojColumn, TextBox1.Text, RotekDataSet.Rotek.VelkostRColumn, TextBox2.Text, RotekDataSet.Rotek.pocetColumn, 1)
        ceny()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        filtruj()

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim f As New pridatIne
        f.TopLevel = True
        f.typ = 1
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim f As New pridatIne
        f.TopLevel = True
        f.typ = 2
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim f As New pridatIne
        f.TopLevel = True
        f.typ = 3
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim f As New pridatIne
        f.TopLevel = True
        f.typ = 4
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim f As New pridatIne
        f.TopLevel = True
        f.typ = 5
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim f As New pridatIne
        f.TopLevel = True
        f.typ = 6
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim f As New pridatIne
        f.TopLevel = True
        f.typ = 7
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 4)
        ceny()

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 5)
        ceny()

    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 6)
        ceny()

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 7)
        ceny()

    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 8)
        ceny()

    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 9)
        ceny()

    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 10)
        ceny()

    End Sub

    Private Sub Button33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button33.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}>'{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 2)
        ceny()

    End Sub

    Private Sub DataGridView3_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        Try
            Dim kks As Integer = e.RowIndex
            Dim cesta As String
            If DataGridView3.Rows(kks).Cells(7).Value.ToString.Length = 0 Then Exit Sub Else cesta = DataGridView3.Rows(kks).Cells(7).Value
            Dim f As New Fotk
            f.TopLevel = True
            f.cesta = cesta
            f.Dock = DockStyle.None
            f.ShowDialog()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim f As New VratitIne
        f.TopLevel = True
        f.typ = 1
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Dim f As New VratitIne
        f.TopLevel = True
        f.typ = 2
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Dim f As New VratitIne
        f.TopLevel = True
        f.typ = 3
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Dim f As New VratitIne
        f.TopLevel = True
        f.typ = 4
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        Dim f As New VratitIne
        f.TopLevel = True
        f.typ = 5
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
        Dim f As New VratitIne
        f.TopLevel = True
        f.typ = 6
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        Dim f As New VratitIne
        f.TopLevel = True
        f.typ = 7
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim f As New SrotZIne
        f.TopLevel = True
        f.typ = 1
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Dim f As New SrotZIne
        f.TopLevel = True
        f.typ = 2
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        Dim f As New SrotZIne
        f.TopLevel = True
        f.typ = 3
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        Dim f As New SrotZIne
        f.TopLevel = True
        f.typ = 4
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        Dim f As New SrotZIne
        f.TopLevel = True
        f.typ = 5
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click
        Dim f As New SrotZIne
        f.TopLevel = True
        f.typ = 6
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        Dim f As New SrotZIne
        f.TopLevel = True
        f.typ = 7
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub zamestnanec_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        rozmer()

    End Sub

    Private Sub Button34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button34.Click
        Form78.exportovat(DataGridView1)
    End Sub

    Private Sub Button35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button35.Click
        Form78.exportovat(DataGridView3)
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim f As New VratitNaSklad

        Dim w As Integer = 2

        f.TopLevel = True
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        If bmp = 1 Then

        End If

        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}>'{3}' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8}='{9}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.KolkoColumn, 0, RotekDataSet.Rotek.NástrojColumn, TextBox1.Text, RotekDataSet.Rotek.VelkostRColumn, TextBox2.Text, RotekDataSet.Rotek.pocetColumn, 1)

        x = DataGridView1.RowCount
        ceny()
    End Sub

    Private Sub Button36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button36.Click

    End Sub


    Private Sub Button40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button40.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 11)
        ceny()
    End Sub

    Private Sub Button39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button39.Click
        Dim f As New SrotZIne
        f.TopLevel = True
        f.typ = 8
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button38.Click
        Dim f As New VratitIne
        f.TopLevel = True
        f.typ = 8
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button37.Click
        Dim f As New pridatIne
        f.TopLevel = True
        f.typ = 8
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button41.Click
        Dim f As New pridatIne
        f.TopLevel = True
        f.typ = 9
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button42.Click
        Dim f As New VratitIne
        f.TopLevel = True
        f.typ = 9
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button43.Click
        Dim f As New SrotZIne
        f.TopLevel = True
        f.typ = 9
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button44.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 12)
        ceny()
    End Sub

    Private Sub Button45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button45.Click
        Dim f As New pridatIne
        f.TopLevel = True
        f.typ = 10
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button48.Click
        Me.RotekBindingSource2.Filter = Nothing
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.pocetColumn, 13)
        ceny()
    End Sub

    Private Sub Button47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button47.Click
        Dim f As New SrotZIne
        f.TopLevel = True
        f.typ = 10
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub

    Private Sub Button46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button46.Click
        Dim f As New VratitIne
        f.TopLevel = True
        f.typ = 10
        f.crc = crc
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        ceny()
    End Sub
    Private Sub filtruj()
        'If Button49.Text = "Všetko" Then
        '    Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}>'{3}' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8}='{9}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.KolkoColumn, 0, RotekDataSet.Rotek.NástrojColumn, TextBox1.Text, RotekDataSet.Rotek.VelkostRColumn, TextBox2.Text, RotekDataSet.Rotek.pocetColumn, 1)
        'Else
        '    Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {4} LIKE '{5}%' AND {6} LIKE '{7}%' AND {8}='{9}'", RotekDataSet.Rotek.MenprColumn, crc, RotekDataSet.Rotek.KolkoColumn, 0, RotekDataSet.Rotek.NástrojColumn, TextBox1.Text, RotekDataSet.Rotek.VelkostRColumn, TextBox2.Text, RotekDataSet.Rotek.pocetColumn, 1)
        'End If
        'ceny()

    End Sub

    Private Sub Button49_Click(sender As System.Object, e As System.EventArgs) Handles Button49.Click
        If Button49.Text = "Všetko" Then
            Button49.Text = "Menej"
        Else
            Button49.Text = "Všetko"
        End If
        filtruj()

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        DataGridView1.Rows(e.RowIndex).Selected = True

    End Sub

    Private Sub DataGridView3_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick
        DataGridView3.Rows(e.RowIndex).Selected = True

    End Sub
End Class