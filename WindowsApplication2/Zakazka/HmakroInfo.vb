Imports System.IO
Imports OfficeOpenXml
Imports System.Data.SqlClient


Public Class HmakroInfo

    Private excelSheet As ExcelWorksheet
    Property zakazka As String

    Private Sub HmakroInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Zakazka' table. You can move, or remove it, as needed.
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        Button2.Hide()
        ComboBox1.Hide()
        Button5.Hide()
        DataGridView1.Columns(11).Visible = False
        poverenie()
        'dasa()
        Dim f As New Hchyba(zakazka)
        Try
            f.Show()

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
        ' f.dasa2()
        f.napln()
        If f.dasa = True Then
            Label5.Text = "Dá sa ✓"
        Else
            Label5.Text = "Nedá sa ✘"
            Label5.ForeColor = Color.Red
        End If


        f.Dispose()
        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka)
        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)


        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(zakazka, Label1.Font)
        Dim stred As Integer
        Label4.Text = "Hlavna"
        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New System.Drawing.Point(rw - 150, 2)
        Label1.Text = zakazka

        DataGridView2.ClearSelection()


    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo
            Case Form78.admin
                'ComboBox1.Show()
                Button6.Show()
                Button7.Show()
                Button2.Show()
                DataGridView1.Columns(11).Visible = True
            Case Form78.zakazkar
                Button7.Show()
                Button2.Show()
                Button6.Show()
                'ComboBox1.Show()
                'Button5.Show()
                'DataGridView1.Columns(9).Visible = True
            Case Form78.skladnik
                Button7.Hide()
                Button2.Hide()
                Button6.Hide()
                'ComboBox1.Show()
                'Button5.Show()

            Case Else
                Button7.Hide()
                Button2.Hide()
                Button6.Hide()
                'ComboBox1.Hide()
                'Button5.Hide()

                DataGridView1.Columns(11).Visible = False
        End Select
    End Sub
    'Private Sub dasa()
    '    Label5.Text = "Dá sa ✓"

    '    Dim i, k As Integer


    '    Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
    '    Me.HutaBindingSource.Filter = String.Format("({0} = '{1}' OR {0}='{4}') AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka, 3)
    '    Me.HutaBindingSource.Sort = "Nazov"
    '    k = DataGridView1.RowCount - 1
    '    Dim sucet As Integer = 0
    '    Try

    '        For i = 0 To k
    '            Dim druh As String = ""
    '            Dim nazov As String = ""
    '            Dim rozmer As String = ""
    '            Dim druh2 As String = ""
    '            Dim nazov2 As String = ""
    '            Dim rozmer2 As String = ""
    '            Dim podzakazka As String = ""


    '            Me.HutaBindingSource.Filter = String.Format("({0} = '{1}' OR {0}='{4}') AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka, 3)
    '            Me.HutaBindingSource.Sort = "Druh,  Rozmer, Nazov"
    '            druh = DataGridView1.Rows(i).Cells(0).Value
    '            nazov = DataGridView1.Rows(i).Cells(3).Value
    '            rozmer = DataGridView1.Rows(i).Cells(4).Value
    '            podzakazka = DataGridView1.Rows(i).Cells(19).Value
    '            If String.IsNullOrEmpty(podzakazka) Then
    '                Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
    '            Else
    '                Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND  {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
    '            End If
    '            If DataGridView2.RowCount = 1 Then
    '            Else : Exit Sub
    '            End If
    '            Dim kusov As Integer = DataGridView2.Rows(0).Cells(4).Value



    '            Try
    '                druh2 = DataGridView1.Rows(i + 1).Cells(0).Value
    '                nazov2 = DataGridView1.Rows(i + 1).Cells(3).Value
    '                rozmer2 = DataGridView1.Rows(i + 1).Cells(4).Value
    '            Catch ex As Exception
    '                druh2 = ""
    '                nazov2 = ""
    '                rozmer2 = ""

    '            End Try

    '            sucet = sucet + DataGridView1.Rows(i).Cells(6).Value * kusov

    '            If (druh = druh2) And (rozmer = rozmer2) And (nazov = nazov2) Then
    '                Continue For
    '            Else
    '            End If
    '            Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
    '            Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}='{7}'", RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.pocetColumn, 0)
    '            Dim pocett As Integer = DataGridView1.Rows(0).Cells(6).Value

    '            If (pocett - sucet < 0) Then
    '                Label5.Text = "Nedá sa ✘"
    '                Label5.ForeColor = Color.Red

    '                Exit Sub
    '            End If
    '            sucet = 0
    '        Next

    '    Catch ex As Exception
    '        Chyby.Show(ex.ToString)
    '    End Try
    'End Sub


    'Public Sub stuk()
    '    If Label5.Text = "Nedá sa ✘" Then
    '        If MsgBox("Nieje dosť materiálu na sklade. Chcete aj tak vziať?", vbExclamation + vbYesNo, "Overenie") = vbYes Then  Else Exit Sub
    '    End If

    '    Dim srot, srot1, srotcena, srotcena1, Razy As Double

    '    Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
    '    If DataGridView2.RowCount = 1 Then
    '    Else : Exit Sub
    '    End If
    '    srot1 = DataGridView2.Rows(0).Cells(1).Value
    '    srotcena1 = DataGridView2.Rows(0).Cells(2).Value
    '    Razy = DataGridView2.Rows(0).Cells(3).Value + 1


    '    Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
    '    Me.HutaBindingSource.Filter = String.Format("({0} = '{1}' OR {0}='{4}') AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka, 3)
    '    Dim druh, nazov, rozmer, podzakazka As String


    '    For i = 0 To DataGridView1.RowCount - 1


    '        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
    '        Me.HutaBindingSource.Filter = String.Format("({0} = '{1}' OR {0}='{4}') AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka, 3)
    '        Me.HutaBindingSource.Sort = "Druh,  Rozmer, Nazov"

    '        druh = DataGridView1.Rows(i).Cells(0).Value
    '        nazov = DataGridView1.Rows(i).Cells(3).Value
    '        rozmer = DataGridView1.Rows(i).Cells(4).Value
    '        podzakazka = DataGridView1.Rows(i).Cells(19).Value
    '        If String.IsNullOrEmpty(podzakazka) Then
    '            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
    '        Else
    '            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND  {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
    '        End If
    '        If DataGridView2.RowCount = 1 Then
    '        Else : Exit Sub
    '        End If
    '        Dim kusov As Integer = DataGridView2.Rows(0).Cells(4).Value

    '        Dim hustota As Double
    '        Dim velkost As Double
    '        Dim con As New SqlConnection

    '        Try
    '            velkost = DataGridView1.Rows(i).Cells(6).Value * kusov

    '            Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}='{7}'", RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.pocetColumn, 0)
    '            If DataGridView1.RowCount = 1 Then
    '                Dim pocett As Integer = DataGridView1.Rows(0).Cells(4).Value 'velkost na sklade
    '                Dim cena As Double = CDec(DataGridView1.Rows(0).Cells(5).Value)
    '                hustota = DataGridView1.Rows(0).Cells(6).Value
    '                srot = DataGridView1.Rows(0).Cells(7).Value
    '                srotcena = DataGridView1.Rows(0).Cells(8).Value
    '                Dim objem As Double
    '                If rozmer.IndexOf("x") <> -1 Then
    '                    Dim pos As Integer = rozmer.IndexOf("x")
    '                    Dim dlz As Integer = rozmer.Length - pos - 1
    '                    Dim sirka As Double = CDec(rozmer.Substring(pos + 1, dlz)) / 1000
    '                    Dim dlzka As Double = CDec(rozmer.Substring(0, pos)) / 1000
    '                    objem = sirka * dlzka * velkost / 1000
    '                End If
    '                If rozmer.IndexOf("r") <> -1 Then
    '                    Dim pos As Integer = rozmer.IndexOf("r")
    '                    Dim dlzka As Double = CDec(rozmer.Substring(0, pos)) / 1000
    '                    objem = 3.14159265 * dlzka * dlzka * velkost / 1000
    '                End If
    '                If rozmer.IndexOf("d") <> -1 Then
    '                    Dim pos As Integer = rozmer.IndexOf("d")
    '                    Dim dlzka As Double = CDec(rozmer.Substring(0, pos)) / 2000
    '                    objem = 3.14159265 * dlzka * dlzka * velkost / 1000
    '                End If
    '                Dim hmota As Double = objem * hustota
    '                Dim velkostover As Integer = velkost
    '                velkost = pocett - velkost

    '                srot = srot + hmota
    '                srot1 = srot1 + hmota
    '                srotcena = srotcena + hmota * cena
    '                srotcena1 = srotcena1 + hmota * cena

    '                Dim sql As String
    '                con.ConnectionString = My.Settings.Rotek2       
    '                con.Open()
    '                Dim cmd As New SqlCommand

    '                sql = "UPDATE Huta SET Velkost='" + velkost.ToString + "', srot='" + srot.ToString + "', srotcena='" + srotcena.ToString + "' WHERE Druh='" + druh + "'  AND Nazov='" + nazov + "' AND Rozmer='" + rozmer + "' AND pocet='0'"
    '                cmd = New SqlCommand(sql, con)
    '                cmd.ExecuteNonQuery()

    '                con.Close()

    '            Else
    '                Chyby.Show("Už nie je na sklade")
    '            End If
    '            ' Me.Close()
    '        Catch ex As SystemException
    '            Chyby.Show(ex.ToString)
    '            con.Close()
    '        End Try

    '    Next

    '    Dim conn As New SqlConnection
    '    Dim sqln As String
    '    conn.ConnectionString = My.Settings.Rotek2       
    '    conn.Open()
    '    Dim cmdn As New SqlCommand

    '    sqln = "UPDATE Zakazka SET srot='" & srot1 & "', srotcena='" & srotcena1 & "', Razy=" & Razy & " WHERE Zakazka='" + zakazka + "' AND pocet=1"
    '    cmdn = New SqlCommand(sqln, conn)
    '    cmdn.ExecuteNonQuery()

    '    sqln = "UPDATE Huta SET srot='" & srot1 & "', srotcena='" & srotcena1 & "', Kusov=" & Razy & " WHERE zakazka='" + zakazka + "' AND pocet='2'"
    '    cmdn = New SqlCommand(sqln, conn)
    '    cmdn.ExecuteNonQuery()

    '    conn.Close()

    'End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim b As Boolean = True
        For i As Integer = 0 To DataGridView2.RowCount - 1

            If DataGridView2.Rows(i).Cells(4).Value() = 0 Then
            Else
                b = False
            End If

        Next
        If b Then
            Dim f As New Hchyba(zakazka)
            f.ShowDialog()
            Me.Dispose()
        Else
            Chyby.Show("Niektoré dielce už majú vybratý materiál. Ak chcete zobrať zo skladu zvyšný materiál, kliknite na | Minúť zvyšné |")
        End If

    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        'Dim kks As Integer = e.RowIndex
        'Dim f As New zmen
        'f.TopLevel = True
        'f.druh = DataGridView1.Rows(kks).Cells(0).Value
        'f.nazov = DataGridView1.Rows(kks).Cells(2).Value
        'f.rozmer = DataGridView1.Rows(kks).Cells(3).Value
        'f.velkost = DataGridView1.Rows(kks).Cells(4).Value
        'f.zakazka = zakazka
        'f.Dock = DockStyle.None
        'f.ShowDialog()
        'Me.HutaBindingSource.Filter = Nothing
        'Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        'Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka)

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex = 11 And e.RowIndex >= 0 Then
            Dim kks As Integer = e.RowIndex
            Dim druh, nazov, rozmer, velkost, podzakazka, sirka, s_rozmer As String
            druh = DataGridView1.Rows(kks).Cells(0).Value
            nazov = DataGridView1.Rows(kks).Cells(1).Value
            sirka = DataGridView1.Rows(kks).Cells(3).Value
            rozmer = DataGridView1.Rows(kks).Cells(4).Value
            s_rozmer = DataGridView1.Rows(kks).Cells(5).Value
            velkost = DataGridView1.Rows(kks).Cells(6).Value

            podzakazka = Label4.Text

            Dim con As New SqlConnection
            Dim sql As String
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            sql = "DELETE FROM Huta WHERE Druh='" + druh + "' AND Nazov='" + nazov + "' AND Rozmer=" & rozmer & " AND Velkost=" & velkost & " AND Sirka=" & sirka & " AND pocet='" & 3 & "' AND zakazka='" + zakazka + "' AND Vaha='" + podzakazka + "' AND S_rozmer=" & s_rozmer
            Dim cmd As New SqlCommand
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()

            con.Close()

            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)

            Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
            Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Huta.pocetColumn, 3, RotekDataSet.Huta.zakazkaColumn, zakazka, RotekDataSet.Huta.VahaColumn, podzakazka)
        End If
    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Dim kks As Integer
        kks = e.RowIndex
        Me.HutaBindingSource.Filter = Nothing
        If kks = -1 Then
            Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka)
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
            Label7.Text = DataGridView2.Rows(0).Cells(4).Value
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
            Label4.Text = "Hlavna"
        Else
            Dim podzakazka As String = DataGridView2.Rows(kks).Cells(0).Value
            Label4.Text = podzakazka
            Label7.Text = DataGridView2.Rows(kks).Cells(4).Value
            Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Huta.pocetColumn, 3, RotekDataSet.Huta.zakazkaColumn, zakazka, RotekDataSet.Huta.VahaColumn, podzakazka)
            If e.ColumnIndex = 5 Then
                If DataGridView2.Rows(kks).Cells(3).Value <> 0 Then
                    DataGridView2.Rows(kks).Cells(5).Value = False
                Else
                    DataGridView2.Rows(kks).Cells(5).Value = True - DataGridView2.Rows(kks).Cells(5).Value

                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New Hchyba(zakazka)

        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If (DataGridView2.Rows.Count = 0) Then
                Exit Sub
            End If


            Dim strPath As String = My.Settings.Rotek3 & "Material\" & zakazka.Replace("/", "•") & ".xlsx"
            Dim cesta As String = My.Settings.Rotek3 & "Material.xlsx"

            Try
                My.Computer.FileSystem.CopyFile(cesta, strPath, True)
            Catch ex As Exception
                Chyby.Show("Subor bol zmazany. Prosime vratit Material.xlsx do adresara " + cesta)
            End Try

            'zaciatok

            Dim fufukeh As FileInfo = New FileInfo(strPath)
            Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)


            Try
                excelSheet = excelApp.Workbook.Worksheets.First


                Dim intNewRow As Int32 = 5
                Dim vyska As Integer = 38
                Dim strana As Integer = 22

                Dim i As Integer = 0
                Dim zaciatok As Integer = i

                Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)

                Dim d As DateTime = New DateTime
                excelSheet.Cells("I1").Value = zakazka
                excelSheet.Cells("A2").Value = DataGridView2.Rows(0).Cells(6).Value
                excelSheet.Cells("F2").Value = DataGridView2.Rows(0).Cells(7).Value
                excelSheet.Cells("E2").Value = DataGridView2.Rows(0).Cells(8).Value
                excelSheet.Cells("D2").Value = DataGridView2.Rows(0).Cells(9).Value
                Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)


                For i = 0 To DataGridView2.RowCount - 1
                    Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Huta.pocetColumn, 3, RotekDataSet.Huta.zakazkaColumn, zakazka, RotekDataSet.Huta.VahaColumn, DataGridView2.Rows(i).Cells(0).Value)

                    excelSheet.Cells("A" & intNewRow + zaciatok).Value = zaciatok + 1
                    excelSheet.Cells("A" & intNewRow + zaciatok).Style.Font.Bold = True
                    excelSheet.Cells("B" & intNewRow + zaciatok).Value = DataGridView2.Rows(i).Cells(0).Value
                    excelSheet.Cells("B" & intNewRow + zaciatok & ":E" & intNewRow + zaciatok).Merge = True
                    excelSheet.Cells("B" & intNewRow + zaciatok).Style.Font.Bold = True
                    excelSheet.Cells("F" & intNewRow + zaciatok).Value = DataGridView2.Rows(i).Cells(4).Value
                    excelSheet.Cells("F" & intNewRow + zaciatok).Style.Font.Bold = True
                    zaciatok = zaciatok + 1

                    For ii As Integer = 0 To DataGridView1.RowCount - 1
                        excelSheet.Cells("A" & intNewRow + zaciatok).Value = zaciatok + 1
                        excelSheet.Cells("B" & intNewRow + zaciatok).Value = DataGridView1.Rows(ii).Cells(0).Value
                        excelSheet.Cells("C" & intNewRow + zaciatok).Value = DataGridView1.Rows(ii).Cells(1).Value
                        excelSheet.Cells("D" & intNewRow + zaciatok).Value = DataGridView1.Rows(ii).Cells(2).Value


                        excelSheet.Cells("F" & intNewRow + zaciatok).Value = DataGridView1.Rows(ii).Cells(6).Value
                        zaciatok = zaciatok + 1
                    Next

                Next



                excelApp.SaveAs(fufukeh)
                PrehladO.tlacDoPdf(strPath)
                Process.Start(strPath)
            Catch ex As Exception
                Chyby.Show(ex.ToString)
            End Try


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim f As New enfo
        f.TopLevel = True
        f.zakazka = Label1.Text

        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub



    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Dim kusov(DataGridView2.RowCount) As Integer
        Dim podzakazkaka(DataGridView2.RowCount) As String
        Dim b As Boolean = False
        For i As Integer = 0 To DataGridView2.RowCount - 1

            If DataGridView2.Rows(i).Cells(5).Value() = True Then
                kusov(i) = DataGridView2.Rows(i).Cells(4).Value()
                podzakazkaka(i) = DataGridView2.Rows(i).Cells(0).Value()
                b = True
            Else
                kusov(i) = 0
                podzakazkaka(i) = "x"
            End If

        Next
        If b Then
            Dim f As New Hchyba(zakazka, kusov, podzakazkaka)
            f.ShowDialog()
            f.Dispose()
        Else
            Chyby.Show("Nič nebolo vybraté")
        End If

    End Sub
    'Public Sub stuk(ByVal l As List(Of String), ByVal k As List(Of Integer))
    '    Dim i As Integer = 0
    '    For Each ll As String In l
    '        stuk(l(i), k(i))
    '    Next

    'End Sub
    'Private Sub stuk(ByVal podz As String, ByVal kusov As Integer)
    '    If Label5.Text = "Nedá sa ✘" Then
    '        If MsgBox("Nieje dosť materiálu na sklade. Chcete aj tak vziať?", vbExclamation + vbYesNo, "Overenie") = vbYes Then  Else Exit Sub
    '    End If

    '    Dim srot, srot1, srotcena, srotcena1, Razy As Double

    '    Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
    '    If DataGridView2.RowCount = 1 Then
    '    Else : Exit Sub
    '    End If
    '    srot1 = DataGridView2.Rows(0).Cells(1).Value
    '    srotcena1 = DataGridView2.Rows(0).Cells(2).Value
    '    Razy = DataGridView2.Rows(0).Cells(3).Value + 1


    '    Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
    '    If podz = "Hlavna" Then
    '        Me.HutaBindingSource.Filter = String.Format("({0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka)
    '    Else
    '        Me.HutaBindingSource.Filter = String.Format("({0} = '{1}' AND {4}='{5}') AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 3, RotekDataSet.Huta.zakazkaColumn, zakazka, RotekDataSet.Huta.VahaColumn, podz)
    '    End If
    '    Dim druh, nazov, rozmer, podzakazka As String


    '    For i = 0 To DataGridView1.RowCount - 1


    '        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
    '        If podz = "Hlavna" Then
    '            Me.HutaBindingSource.Filter = String.Format("({0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka)
    '        Else
    '            Me.HutaBindingSource.Filter = String.Format("({0} = '{1}' AND {4}='{5}') AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 3, RotekDataSet.Huta.zakazkaColumn, zakazka, RotekDataSet.Huta.VahaColumn, podz)
    '        End If
    '        Me.HutaBindingSource.Sort = "Druh,  Rozmer, Nazov"

    '        druh = DataGridView1.Rows(i).Cells(0).Value
    '        nazov = DataGridView1.Rows(i).Cells(2).Value
    '        rozmer = DataGridView1.Rows(i).Cells(3).Value
    '        podzakazka = DataGridView1.Rows(i).Cells(17).Value
    '        If String.IsNullOrEmpty(podzakazka) Then
    '            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
    '        Else
    '            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND  {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
    '        End If
    '        If DataGridView2.RowCount = 1 Then
    '        Else : Exit Sub
    '        End If

    '        Dim hustota As Double
    '        Dim velkost As Double
    '        Dim con As New SqlConnection

    '        Try
    '            velkost = DataGridView1.Rows(i).Cells(4).Value * kusov

    '            Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}='{7}'", RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.RozmerColumn, rozmer, RotekDataSet.Huta.pocetColumn, 0)
    '            If DataGridView1.RowCount = 1 Then
    '                Dim pocett As Integer = DataGridView1.Rows(0).Cells(4).Value 'velkost na sklade
    '                Dim cena As Double = CDec(DataGridView1.Rows(0).Cells(5).Value)
    '                hustota = DataGridView1.Rows(0).Cells(6).Value
    '                srot = DataGridView1.Rows(0).Cells(7).Value
    '                srotcena = DataGridView1.Rows(0).Cells(8).Value
    '                Dim objem As Double
    '                If rozmer.IndexOf("x") <> -1 Then
    '                    Dim pos As Integer = rozmer.IndexOf("x")
    '                    Dim dlz As Integer = rozmer.Length - pos - 1
    '                    Dim sirka As Double = CDec(rozmer.Substring(pos + 1, dlz)) / 1000
    '                    Dim dlzka As Double = CDec(rozmer.Substring(0, pos)) / 1000
    '                    objem = sirka * dlzka * velkost / 1000
    '                End If
    '                If rozmer.IndexOf("r") <> -1 Then
    '                    Dim pos As Integer = rozmer.IndexOf("r")
    '                    Dim dlzka As Double = CDec(rozmer.Substring(0, pos)) / 1000
    '                    objem = 3.14159265 * dlzka * dlzka * velkost / 1000
    '                End If
    '                If rozmer.IndexOf("d") <> -1 Then
    '                    Dim pos As Integer = rozmer.IndexOf("d")
    '                    Dim dlzka As Double = CDec(rozmer.Substring(0, pos)) / 2000
    '                    objem = 3.14159265 * dlzka * dlzka * velkost / 1000
    '                End If
    '                Dim hmota As Double = objem * hustota
    '                Dim velkostover As Integer = velkost
    '                velkost = pocett - velkost

    '                srot = srot + hmota
    '                srot1 = srot1 + hmota
    '                srotcena = srotcena + hmota * cena
    '                srotcena1 = srotcena1 + hmota * cena

    '                Dim sql As String
    '                con.ConnectionString = My.Settings.Rotek2       
    '                con.Open()
    '                Dim cmd As New SqlCommand

    '                sql = "UPDATE Huta SET Velkost='" + velkost.ToString + "', srot='" + srot.ToString + "', srotcena='" + srotcena.ToString + "' WHERE Druh='" + druh + "'  AND Nazov='" + nazov + "' AND Rozmer='" + rozmer + "' AND pocet='0'"
    '                cmd = New SqlCommand(sql, con)
    '                cmd.ExecuteNonQuery()

    '                con.Close()

    '            Else
    '                Chyby.Show("Už nie je taký typ na sklade")
    '            End If
    '            ' Me.Close()
    '        Catch ex As SystemException
    '            Chyby.Show(ex.ToString)
    '            con.Close()
    '        End Try

    '    Next

    '    Dim conn As New SqlConnection
    '    Dim sqln As String
    '    conn.ConnectionString = My.Settings.Rotek2       
    '    conn.Open()
    '    Dim cmdn As New SqlCommand

    '    sqln = "UPDATE Zakazka SET srot='" & srot1 & "', srotcena='" & srotcena1 & "', Razy=" & Razy & " WHERE Zakazka='" + zakazka + "' AND pocet=1"
    '    cmdn = New SqlCommand(sqln, conn)
    '    cmdn.ExecuteNonQuery()

    '    sqln = "UPDATE Huta SET srot='" & srot1 & "', srotcena='" & srotcena1 & "', Kusov=" & Razy & " WHERE zakazka='" + zakazka + "' AND pocet='2'"
    '    cmdn = New SqlCommand(sqln, conn)
    '    cmdn.ExecuteNonQuery()

    '    conn.Close()
    '    Chyby.Show("Odobrate")

    'End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        Dim kusov(DataGridView2.RowCount) As Integer
        Dim podzakazkaka(DataGridView2.RowCount) As String
        Dim b As Boolean = False
        For i As Integer = 0 To DataGridView2.RowCount - 1

            If DataGridView2.Rows(i).Cells(3).Value() = 0 Then
                kusov(i) = DataGridView2.Rows(i).Cells(4).Value()
                podzakazkaka(i) = DataGridView2.Rows(i).Cells(0).Value()
                b = True
            Else
                kusov(i) = 0
                podzakazkaka(i) = "x"
            End If

        Next
        If b Then
            Dim f As New Hchyba(zakazka, kusov, podzakazkaka)
            f.ShowDialog()
            f.Dispose()
        Else
            Chyby.Show("Všetky dielce majú vybratý materiál")
        End If
    End Sub
End Class