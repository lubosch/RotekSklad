Imports System.Data
Imports OfficeOpenXml
Imports System.IO
Imports System.Data.SqlClient



Public Class Form78

    Public kks As Integer
    Private Declare Function InternetOpen Lib "wininet.dll" Alias "InternetOpenA" _
    (ByVal sAgent As String, ByVal lAccessType As Long, ByVal sProxyName As String, _
    ByVal sProxyBypass As String, ByVal lFlags As Long) As Long
    Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
    Shared Property heslo As String
    Shared Property admin As String
    Shared Property skladnik As String
    Shared Property zakazkar As String
    Shared Property subor As List(Of String)
    Shared Property uzivatel As String
    Shared Property mail As String
    Shared Property mail_heslo As String


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New Pridatt
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()

        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}<>'{3}'", RotekDataSet.Rotek.pocetColumn, 0, RotekDataSet.Rotek.MenoColumn, "Firma")


    End Sub
    Private Sub poverenia()
        Select Case heslo
            Case admin
                Button1.Show()
                Button2.Show()
            Case skladnik
                Button1.Show()
            Case Else
                Button1.Hide()
                Button2.Hide()
        End Select
    End Sub
    Public Shared Sub sqa(ByVal sql As String)
        Dim con As New SqlConnection
        con.ConnectionString = My.Settings.Rotek
        con.Open()
        Dim cmd As New SqlCommand
        cmd = New SqlCommand(sql, con)
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Chyby.Show(sql + vbNewLine + ex.ToString)
        End Try

        con.Close()

    End Sub
    Public Shared Sub sql(ByVal sql As String)
        Dim con As New SqlConnection
        con.ConnectionString = My.Settings.Rotek
        con.Open()
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, con)
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Chyby.Show(sql + vbNewLine + ex.ToString)
        End Try

        con.Close()

    End Sub



    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Button1.Hide()
            Button2.Hide()


            Dim rw As Integer = Me.Width / 2
            Dim sw As Integer = Me.Height

            DataGridView1.Size = New Size(rw * 2, sw - 270)
            PictureBox2.Location = New Point(rw - 100, 40)


            Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
            Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}<>'{3}'", RotekDataSet.Rotek.pocetColumn, 0, RotekDataSet.Rotek.MenoColumn, "Firma")


            poverenia()


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub rozmers()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2, sw - 220)
        Dim g As System.Drawing.Graphics = Me.CreateGraphics

        Dim stred As Integer
        stred = PictureBox2.Width / 2

        Dim rw As String = Me.Width / 2 - stred
        PictureBox2.Location = New Point(rw, 20)
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim f As New vymazať
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}<>'{3}'", RotekDataSet.Rotek.pocetColumn, 0, RotekDataSet.Rotek.MenoColumn, "Firma")

    End Sub



    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim f As New sklad
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}<>'{3}'", RotekDataSet.Rotek.pocetColumn, 0, RotekDataSet.Rotek.MenoColumn, "Firma")

    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        kks = e.RowIndex
        Dim crc As String = ""
        Dim memo As String = ""
        Dim prezvo As String = ""
        Dim spoluu As Integer
        Try
            memo = DataGridView1.Rows(kks).Cells(0).Value
            prezvo = DataGridView1.Rows(kks).Cells(1).Value
            spoluu = DataGridView1.Rows(kks).Cells(2).Value
            crc = memo & " " & prezvo
        Catch ex As SystemException
        End Try

        Dim f As New zamestnanec
        f.TopLevel = True
        f.crc = crc
        f.menko = memo
        f.prezvo = prezvo

        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}<>'{3}'", RotekDataSet.Rotek.pocetColumn, 0, RotekDataSet.Rotek.MenoColumn, "Firma")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim f As New Form2
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim f As New skladvz
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.Show()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim f As New hForm78
        f.TopLevel = True
        f.Dock = DockStyle.None
        Me.Hide()
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim f As New mForm78
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim f As New skladIne
        f.TopLevel = True
        f.crc = "Spotrebný materiál"
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim f As New skladIne
        f.TopLevel = True
        f.crc = "Zveráky a ostatné"
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim f As New skladIne
        f.TopLevel = True
        f.crc = "Upínací + špeciálne"
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim f As New skladIne
        f.TopLevel = True
        f.crc = "Elektronáradie"
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim f As New skladIne
        f.TopLevel = True
        f.crc = "Príslušenstvo"
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim f As New skladIne
        f.TopLevel = True
        f.crc = "Náradie"
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim f As New skladIne
        f.TopLevel = True
        f.crc = "Meradlá"
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Form78_ResizeEnd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ResizeEnd

        Dim rw As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rw * 2, sw - 270)
        PictureBox2.Location = New Point(rw - 100, 40)

    End Sub

    Private Sub Form78_AutoSizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.AutoSizeChanged

        Dim rw As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rw * 2, sw - 270)
        PictureBox2.Location = New Point(rw - 100, 40)

    End Sub

    Private Sub Form78_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged

        Dim rw As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rw * 2, sw - 270)
        PictureBox2.Location = New Point(rw - 100, 40)

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub


    Public Shared Sub exportovat(ByVal DataGridView1 As DataGridView)
        If ((DataGridView1.Columns.Count = 0) Or (DataGridView1.Rows.Count = 0)) Then
            Exit Sub
        End If

        Dim openFileDialog1 As New SaveFileDialog

        openFileDialog1.InitialDirectory = System.Environment.CurrentDirectory
        openFileDialog1.Title = "Open Text File"
        openFileDialog1.Filter = "Excel(*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True
        Dim cesta As String = ""

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            cesta = openFileDialog1.FileName
        Else : Exit Sub
        End If
        If File.Exists(cesta) Then
            File.Delete(cesta)
        End If
        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add column to that table
        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            If DataGridView1.Columns(i).Visible = True Then
                dset.Tables(0).Columns.Add(DataGridView1.Columns(i).HeaderText)
            End If

        Next
        'add rows to the table
        Dim dr1 As DataRow
        Dim k As Integer = 0

        For i As Integer = 0 To DataGridView1.RowCount - 1
            dr1 = dset.Tables(0).NewRow
            For j As Integer = 0 To DataGridView1.Columns.Count - 1
                If DataGridView1.Columns(j).Visible = True Then
                    dr1(k) = DataGridView1.Rows(i).Cells(j).Value
                    k = k + 1
                End If
            Next
            k = 0
            dset.Tables(0).Rows.Add(dr1)
        Next

        Dim fufukeh As FileInfo = New FileInfo(cesta)
        Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)
        Dim excelSheet As ExcelWorksheet

        excelSheet = excelApp.Workbook.Worksheets.Add("Zoznam")

        Dim dt As System.Data.DataTable = dset.Tables(0)
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        Dim pocet_stlpcov As Integer = dt.Columns.Count
        For Each dc In dt.Columns
            colIndex = colIndex + 1
            excelSheet.Cells(1, colIndex).Value = dc.ColumnName
        Next

        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                excelSheet.Cells(rowIndex + 1, colIndex).Value = dr(dc.ColumnName)
            Next
        Next
        Dim b As Integer = 1

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            excelSheet.Cells(1, b).Style.Font.Bold = True
            b = b + 1
        Next

        For i As Integer = 1 To pocet_stlpcov
            excelSheet.Column(i).AutoFit()
        Next

        Dim strFileName As String = cesta
        Dim blnFileOpen As Boolean = False
        Try
            Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
            fileTemp.Close()
        Catch ex As Exception
            blnFileOpen = False
        End Try

        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If
        excelApp.SaveAs(fufukeh)
        Process.Start(cesta)
    End Sub


    Private Sub Button15_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        exportovat(DataGridView1)
    End Sub
    Public Shared Sub premen()
        'Try
        '    For Each s As String In subor
        '        If String.IsNullOrEmpty(s) Then
        '            subor.Remove(s)
        '        End If
        '    Next
        'Catch ex As Exception
        'End Try


        'Chyby.Show(subor.IndexOf(heslo))
        Try

            If subor.IndexOf(heslo) >= 0 Then
                Dim u As String = subor(subor.IndexOf(heslo) + 1)
                uzivatel = u

                Select Case u
                    Case "Jurčo"
                        heslo = zakazkar
                    Case "Tinka"
                        heslo = skladnik
                    Case "Tekeľ"
                        heslo = admin
                    Case "Argaláš"
                        heslo = zakazkar
                    Case "Paňko"
                        heslo = zakazkar
                    Case "Tapák"
                        heslo = zakazkar
                    Case "Tomkuliak"
                        heslo = zakazkar
                    Case "Hulej"
                        heslo = zakazkar
                    Case "Matlák"
                        heslo = zakazkar
                    Case "Môj"
                        heslo = admin
                End Select
            Else
                heslo = "Neznámy užívateľ"
                uzivatel = heslo
            End If
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Public Shared Function SimpleCrypt(ByVal Text As String) As String
        ' Encrypts/decrypts the passed string using
        ' a simple ASCII value-swapping algorithm
        Dim strTempChar As String, i As Integer
        For i = 1 To Len(Text)
            If Asc(Mid$(Text, i, 1)) < 128 Then
                strTempChar = CType(Asc(Mid$(Text, i, 1)) + 128, String)
            ElseIf Asc(Mid$(Text, i, 1)) > 128 Then
                strTempChar = CType(Asc(Mid$(Text, i, 1)) - 128, String)
            Else
                Chyby.Show("Zle napisane heslo")
                Throw New Exception
            End If
            Mid$(Text, i, 1) = _
                Chr(CType(strTempChar, Integer))
        Next i
        Return Text
    End Function




    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Dim f As New skladIne
        f.TopLevel = True
        f.crc = "Spojovací materiál"
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Form78_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    My.Computer.FileSystem.CopyFile(Application.StartupPath & "\Rotek.mdb", "C:\Users\Flinston\Documents\Visual Studio 2010\Projects\WindowsApplication2\WindowsApplication2\Rotek.mdb", True)
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim f As New Evidencia
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()

    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim f As New skladIne
        f.TopLevel = True
        f.crc = "Iné"
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Panel1.Show()

    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Panel1.Hide()
        Dim f As New Majetok(1)
        f.ShowDialog()


    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Panel1.Hide()
        Dim f As New Majetok(2)
        f.ShowDialog()

    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        Panel1.Hide()
        Dim f As New Majetok(3)
        f.ShowDialog()

    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Dim f As New skladIne
        f.TopLevel = True
        f.crc = "Kvapaliny"
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button26_Click(sender As System.Object, e As System.EventArgs)
        Dim f As New Z_Main
        Me.Opacity = 0

        f.ShowDialog()
        f.Dispose()
        Me.Opacity = 100

    End Sub

    Private Sub Button27_Click(sender As System.Object, e As System.EventArgs)
        Dim f As New Prijemky
        f.ShowDialog()
        f.Dispose()
    End Sub
    Public Shared Sub hladaj2()

    End Sub

    Private Sub Button16_Click(sender As System.Object, e As System.EventArgs) Handles Button16.Click
        Dim f As New Odpadky
        f.ShowDialog()

    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        For i As Integer = 0 To DataGridView1.RowCount - 1
            Dim name As String = DataGridView1.Rows(i).Cells(0).Value
            Dim surname As String = DataGridView1.Rows(i).Cells(1).Value
            RotekIS.Stocks.Employer.Add(name, surname)
        Next
        Chyby.Show("Hotovo")
    End Sub

    Private Sub Button26_Click_1(sender As Object, e As EventArgs) Handles Button26.Click
        Dim f As New export()
        f.ShowDialog
    End Sub

    Private Sub Button27_Click_1(sender As Object, e As EventArgs) Handles Button27.Click
        Dim f As New export_sklad()
        f.ShowDialog
    End Sub

    Public Shared Function rok() As String
        Return Now.Year.ToString.Substring(2)
    End Function
End Class