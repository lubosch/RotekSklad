Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class Z_Main
    Private excelSheet As Excel.Worksheet
    Dim kukoko As Integer
    Private zakazk As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal zakazka As String)

        ' This call is required by the designer.
        InitializeComponent()
        zakazk = zakazka
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Z_Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Vydajka' table. You can move, or remove it, as needed.
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        Label7.Text = ""
        rozmers()
        napln()
        poverenie()

    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo

            Case Form78.admin
                DataGridView1.Columns(53).Visible = True
                Button1.Show()
                DataGridView1.Columns(44).Visible = True
                DataGridView1.Columns(45).Visible = True
                DataGridView1.Columns(47).Visible = True
            Case Form78.zakazkar
                DataGridView1.Columns(53).Visible = True
                Button1.Show()
                DataGridView1.Columns(45).Visible = False
                DataGridView1.Columns(44).Visible = True
                DataGridView1.Columns(47).Visible = True
            Case Form78.skladnik
                DataGridView1.Columns(45).Visible = False
                DataGridView1.Columns(51).Visible = False
                Button1.Show()
                DataGridView1.Columns(44).Visible = True
                DataGridView1.Columns(47).Visible = False
            Case Else
                DataGridView1.Columns(43).Visible = False
                DataGridView1.Columns(53).Visible = False
                Button1.Hide()
                DataGridView1.Columns(44).Visible = False
                DataGridView1.Columns(47).Visible = False

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
        Label1.Location = New System.Drawing.Point(rw, 0)
    End Sub

    Private Sub Z_Main_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged
        rozmers()

    End Sub
    Private Sub napln(ByVal fil As String)
        ZakazkaBindingSource.Sort = fil
        '  DataGridView1.Columns(0).Visible = False
        napln()
        Label7.Visible = False
    End Sub

    Private Sub napln()
        If TextBox4.Text.Length = 0 Then
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}%' AND {4} LIKE '%{5}%' AND {6} LIKE '%{7}%' ", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, TextBox1.Text, RotekDataSet.Zakazka.MenoColumn, TextBox2.Text, RotekDataSet.Zakazka.ZakaznikColumn, TextBox3.Text, RotekDataSet.Zakazka.ObjednavkaColumn, TextBox4.Text)
        Else

            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}%' AND {4} LIKE '%{5}%' AND {6} LIKE '%{7}%' AND {8} LIKE '%{9}%'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, TextBox1.Text, RotekDataSet.Zakazka.MenoColumn, TextBox2.Text, RotekDataSet.Zakazka.ZakaznikColumn, TextBox3.Text, RotekDataSet.Zakazka.ObjednavkaColumn, TextBox4.Text)
        End If

        If Label7.Visible = True Then
            ZakazkaBindingSource.Sort = "D_plan ASC"
        Else
            '  Label7.Visible = True 
        End If

        For i As Integer = 0 To DataGridView1.RowCount - 1

            Try

                If DataGridView1.Rows(i).Cells(46).Value = "10.10.2085" Then
                    DataGridView1.Rows(i).Cells(15).Value = "Ukončená"
                    DataGridView1.Rows(i).Cells(47).Value = "Otvoriť"
                    If DataGridView1.Rows(i).Cells(47).Value = 3 Then
                        DataGridView1.Rows(i).Cells(15).Value = "Doreklamovaná"
                        DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.Gainsboro
                        Continue For
                    End If

                Else
                    DataGridView1.Rows(i).Cells(47).Value = "Ukončiť"
                    DataGridView1.Rows(i).Cells(15).Value = DataGridView1.Rows(i).Cells(46).Value
                    Dim datum As Date = Convert.ToDateTime(DataGridView1.Rows(i).Cells(46).Value)
                    If datum.CompareTo(Now.Date.AddDays(10)) < 0 And datum.CompareTo(Now.Date) > 0 Then
                        DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.Yellow
                    Else
                        Select Case (datum.CompareTo(Now.Date))
                            Case 0
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.Orange
                            Case Is < 0
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                            Case Else
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.Silver
                        End Select
                    End If
                End If

                If DataGridView1.Rows(i).Cells(49).Value = 2 Then
                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LimeGreen
                    DataGridView1.Rows(i).Cells(15).Value = "Reklamácia"
                    DataGridView1.Rows(i).Cells(49).Value = "Doreklamovať"
                End If

                If DataGridView1.Rows(i).Cells(19).Value = 0 Then
                    DataGridView1.Rows(i).Cells(20).Style.BackColor = Color.Red
                ElseIf DataGridView1.Rows(i).Cells(19).Value = 1 Then
                    DataGridView1.Rows(i).Cells(20).Style.BackColor = Color.Green
                End If

                If DataGridView1.Rows(i).Cells(24).Value = 1 Then
                    DataGridView1.Rows(i).Cells(27).Style.BackColor = Color.Red
                ElseIf DataGridView1.Rows(i).Cells(24).Value = 2 Then
                    DataGridView1.Rows(i).Cells(27).Style.BackColor = Color.Green
                End If
                If DataGridView1.Rows(i).Cells(22).Value = 1 Then
                    DataGridView1.Rows(i).Cells(23).Style.BackColor = Color.Red
                ElseIf DataGridView1.Rows(i).Cells(22).Value = 2 Then
                    DataGridView1.Rows(i).Cells(23).Style.BackColor = Color.Green
                End If

                If DataGridView1.Rows(i).Cells(48).Value = 1 Then
                    DataGridView1.Rows(i).Cells(28).Style.BackColor = Color.Red
                ElseIf DataGridView1.Rows(i).Cells(48).Value = 2 Then
                    DataGridView1.Rows(i).Cells(28).Style.BackColor = Color.Green
                End If

                If DataGridView1.Rows(i).Cells(49).Value = 0 Then
                    DataGridView1.Rows(i).Cells(18).Style.BackColor = Color.Red
                ElseIf DataGridView1.Rows(i).Cells(49).Value = 1 Then
                    DataGridView1.Rows(i).Cells(18).Style.BackColor = Color.Green
                ElseIf DataGridView1.Rows(i).Cells(49).Value = 2 Then
                    DataGridView1.Rows(i).Cells(18).Style.BackColor = Color.Black
                Else
                    DataGridView1.Rows(i).Cells(18).Style.BackColor = Color.DarkMagenta
                End If



            Catch ex As Exception
                Chyby.Show(ex.ToString)
                ' DataGridView1.Rows(i).Cells(18).Value = DataGridView1.Rows(i).Cells(17).Value
            End Try
        Next
    End Sub
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim f As New Hmakro
        f.ShowDialog()
        f.Dispose()
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        napln()

    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim kks As Integer = e.RowIndex
        If kks = -1 Then
            If e.ColumnIndex = 15 Then
                Label7.Visible = True
                napln()
            Else
                '    ZakazkaBindingSource.Sort = DataGridView1.Columns(e.ColumnIndex).DataPropertyName
                napln(DataGridView1.Columns(e.ColumnIndex).DataPropertyName)
            End If

        Else

            Dim zakazka As String = DataGridView1.Rows(kks).Cells(0).Value

            If e.ColumnIndex = 45 Then 'zmazat
                zakazk = zakazka
                Dim con As New OleDb.OleDbConnection
                Dim sql As String
                con.ConnectionString = My.Settings.Rotek2      '"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & cesta & "Rotek.mdb"
                con.Open()
                Dim cmd As New OleDb.OleDbCommand
                sql = "DELETE FROM Zakazka WHERE Zakazka='" + zakazka + "'"
                cmd = New OleDb.OleDbCommand(sql, con)
                cmd.ExecuteNonQuery()
                sql = "DELETE FROM Huta WHERE zakazka='" + zakazka + "'"
                cmd = New OleDb.OleDbCommand(sql, con)
                cmd.ExecuteNonQuery()
                con.Close()
                Dim cesta As String = ""
                Try
                    'Chyby.Show((My.Settings.Rotek3 & "zakazky\" & zakazka).Substring("/", "\"))
                    cesta = My.Settings.Rotek3 & "zakazky\" & zakazka
                    System.IO.Directory.Delete(cesta.Replace("/", "\"), True)
                Catch ex As Exception
                    'Chyby.Show(ex.ToString & "  " & cesta)
                End Try
                Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
                napln()
            ElseIf e.ColumnIndex = 44 Then 'upravit
                Dim f As New Hmakro
                f.zakazka = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                f.ShowDialog()
                f.Dispose()
                Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
                napln()
            ElseIf e.ColumnIndex = 10 Then 'potvrdenie
                potvrdenie(zakazka, "Potvrdenie objednávky", 1)
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 11 Then 'Cenova ponuka
                potvrdenie(zakazka, "Cenova ponuka", 10)
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 18 Then 'Vydajka
                davky(zakazka, 3, "x|}")
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
                Button3.Text = ("Hotovo")
            ElseIf e.ColumnIndex = 20 Then 'Vykresy
                subory("Vykresy\", zakazka)
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 23 Then 'NC
                subory("Programy\" + "NC\", zakazka)
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 27 Then 'EIR
                subory("Programy\" + "EIR\", zakazka)
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 28 Then 'Elektroda
                subory("Programy\" + "Elektroda\", zakazka)
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 30 Then 'Ine
                subory("Ine\", zakazka)
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 32 Then 'Rozpracovanost
                rozpracovanost(zakazka, 3, "Rozpracovanosť")
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 34 Then 'Povrchová úprava
                rozpracovanost(zakazka, 4, "Povrchová úprava")
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 36 Then 'Tepelná úprava
                rozpracovanost(zakazka, 11, "Tepelná úprava")
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 38 Then 'Dodacie listy
                potvrdenie(zakazka, "Dodacie listy", 5)
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 42 Then 'Faktury
                potvrdenie(zakazka, "Faktury", 6)
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 175 + DataGridView1.Rows(0).Height * (kks + 1))
            ElseIf e.ColumnIndex = 57 Then 'Dokoncit
                If DataGridView1.Rows(e.RowIndex).Cells(57).Value = "Otvoriť" Then
                    Try
                        Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
                        Dim sql As String
                        Dim f As New datum_box
                        f.ShowDialog()
                        Dim datum As Date = New Date(1954, 10, 10)
                        Dim datum2 As Date = New Date(1954, 10, 10)
                        Dim z As String
                        Dim evidol As String

                        If f.DialogResult = DialogResult.OK Then
                            datum = f.datum
                            datum2 = f.dz
                            z = f.zakazka
                            evidol = f.evidol
                        Else
                            Exit Sub
                        End If
                        If datum = New Date(1954, 10, 10) Then
                            Exit Sub
                        End If

                        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} ='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, z)
                        If DataGridView1.RowCount <> 0 Then

                            Chyby.Show("Už existuje zakazka")
                            napln()
                            Exit Sub

                        End If
                        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} ='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)

                        Dim con As New OleDb.OleDbConnection
                        Dim cmd As New OleDb.OleDbCommand
                        con.ConnectionString = My.Settings.Rotek2      '"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & cesta & "Rotek.mdb"
                        con.Open()

                        Dim s As String = ""
                        Dim firma As String = DataGridView1.Rows(0).Cells(7).Value
                        Dim veduci As String = DataGridView1.Rows(0).Cells(2).Value
                        Dim uprava1 As String = DataGridView1.Rows(0).Cells(33).Value
                        Dim uprava2 As String = DataGridView1.Rows(0).Cells(35).Value
                        Dim kusov As Integer = DataGridView1.Rows(0).Cells(3).Value
                        Dim menozakazky As String = DataGridView1.Rows(0).Cells(1).Value
                        Dim vykresy As Integer = DataGridView1.Rows(0).Cells(20).Value
                        Dim zk As String = "" ' zodpovedny konstrukcie
                        If IsDBNull(DataGridView1.Rows(0).Cells(21).Value) = False Then
                            zk = DataGridView1.Rows(0).Cells(21).Value
                        End If
                        Dim pc As Integer = DataGridView1.Rows(0).Cells(22).Value
                        Dim pr As Integer = DataGridView1.Rows(0).Cells(24).Value
                        Dim pe As Integer = DataGridView1.Rows(0).Cells(48).Value

                        sql = "Insert INTO Zakazka (Zakazka, pocet, srot, srotcena, D_plan, D_prijatia, Zaevidoval, Zakaznik, Veduci, Povrch_uprava, Tepel_uprava, Rozprac, Kusov, Razy, Meno, Vykresy, Zodp_Konstrukcie, P_CNC, P_REZ, P_ELEKTROD, Vydajxa) VALUES ('" + z + "','" & 1 & "','" & 0 & "','" & 0 & "','" + datum + "','" + datum2 + "','" + evidol + "','" + firma + "','" + veduci + "','" & uprava1 & "','" & uprava2 & "','" & 0 & "','" & kusov & "','" & 0 & "','" + menozakazky + "','" & vykresy & "','" & zk & "','" & pc & "','" & pr & "','" & pe & "','" & 0 & "')"
                        cmd = New OleDb.OleDbCommand(sql, con)
                        cmd.ExecuteNonQuery()

                        sql = "Insert INTO Huta (zakazka, pocet, srot, srotcena, D_ukoncenia,  Kusov) VALUES ('" + z + "','" & 2 & "','" & 0 & "','" & 0 & "','" + datum + "','" & 0 & "')"
                        cmd = New OleDb.OleDbCommand(sql, con)
                        cmd.ExecuteNonQuery()

                        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} ='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
                        For i As Integer = 0 To DataGridView1.RowCount - 1
                            Dim podzakazka As String = DataGridView1.Rows(i).Cells(43).Value
                            kusov = DataGridView1.Rows(i).Cells(3).Value
                            uprava1 = DataGridView1.Rows(i).Cells(33).Value
                            uprava2 = DataGridView1.Rows(i).Cells(35).Value
                            pc = DataGridView1.Rows(i).Cells(22).Value
                            pr = DataGridView1.Rows(i).Cells(24).Value
                            pe = DataGridView1.Rows(i).Cells(48).Value
                            ' vykresy = DataGridView1.Rows(i).Cells(14).Value

                            sql = "Insert INTO Zakazka (Zakazka, pocet, srot, srotcena, Povrch_uprava, Tepel_uprava Podzakazka, Rozprac, Kusov, Razy, P_CNC, P_REZ, P_ELEKTROD) VALUES ('" + z + "','" & 2 & "','" & 0 & "','" & 0 & "','" & uprava1 & "','" & uprava2 & "','" + podzakazka + "','" & 0 & "','" & kusov & "','" & 0 & "','" & pc & "','" & pr & "','" & pe & "')"
                            cmd = New OleDb.OleDbCommand(sql, con)
                            cmd.ExecuteNonQuery()
                        Next

                        Me.HutaBindingSource.Filter = String.Format("({0} = '{1}' OR {0}='{4}') AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka, 3)

                        For i As Integer = 0 To DataGridView3.RowCount - 1
                            Dim druh, sirka, nazov, rozmer, velkost As String
                            druh = DataGridView3.Rows(i).Cells(0).Value
                            sirka = DataGridView3.Rows(i).Cells(1).Value
                            nazov = DataGridView3.Rows(i).Cells(2).Value
                            rozmer = DataGridView3.Rows(i).Cells(3).Value
                            velkost = DataGridView3.Rows(i).Cells(4).Value
                            Dim pocet As Integer = DataGridView3.Rows(i).Cells(8).Value
                            Dim Podzakazka As String = DataGridView3.Rows(i).Cells(5).Value
                            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} ='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.PodzakazkaColumn, Podzakazka)
                            kusov = DataGridView1.Rows(0).Cells(3).Value
                            sql = "Insert INTO Huta (Druh,  Nazov, Sirka, Rozmer, Velkost, zakazka, pocet, srot, srotcena, Vaha, Kusov) VALUES ('" + druh + "', '" + nazov + "','" & sirka & "','" & rozmer & "','" & velkost & "','" + z + "','" & pocet & "','" & 0 & "','" & 0 & "','" + Podzakazka + "','" & kusov & "')"
                            cmd = New OleDb.OleDbCommand(sql, con)
                            cmd.ExecuteNonQuery()
                        Next
                        con.Close()
                        Dim cesta As String = My.Settings.Rotek3 + "zakazky\" + zakazka + "\Vykresy"
                        Dim cesta2 As String = My.Settings.Rotek3 + "zakazky\" + z + "\Vykresy"
                        cesta = cesta.Replace("/", "\")
                        cesta2 = cesta2.Replace("/", "\")
                        If System.IO.Directory.Exists(cesta) Then
                            CopyDirectory(cesta, cesta2)
                        End If
                        cesta = My.Settings.Rotek3 + "zakazky\" + zakazka + "\Programy"
                        cesta2 = My.Settings.Rotek3 + "zakazky\" + z + "\Programy"
                        cesta = cesta.Replace("/", "\")
                        cesta2 = cesta2.Replace("/", "\")
                        If System.IO.Directory.Exists(cesta) Then
                            CopyDirectory(cesta, cesta2)
                        End If

                        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
                        napln()
                    Catch ex As Exception
                        Chyby.Show(ex.ToString)
                    End Try
                ElseIf DataGridView1.Rows(e.RowIndex).Cells(57).Value = "Ukončiť" Then
                    Dim f As New Dokoncit(zakazka)
                    f.ShowDialog()
                    Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
                    napln()
                Else
                    Dim sql As String
                    Dim d As DateTime = New DateTime(2085, 10, 10)

                    sql = "UPDATE Zakazka SET Razy=3, D_plan='" & d.ToShortDateString & "', D_real='" & Now.ToShortDateString & "' WHERE Zakazka='" + zakazka + "' AND pocet=1"
                    Form78.sqa(sql)
                    sql = "UPDATE Huta SET D_ukoncenia='" & d.ToShortDateString & "' WHERE zakazka='" + zakazka + "' AND pocet='2'"
                    Form78.sqa(sql)
                    Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
                    napln()
                End If
            ElseIf e.ColumnIndex = 53 Then 'Reklamacja
                zakazk = zakazka
                GroupBox2.Location = New System.Drawing.Point(800, 175 + DataGridView1.Rows(0).Height * (kks + 1))
                GroupBox2.Show()
                Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
                For i As Integer = 0 To DataGridView6.RowCount - 1
                    DataGridView6.Rows(i).Cells(0).Value = False
                    If DataGridView6.Rows(i).Cells(5).Value > 0 Then
                        DataGridView6.Rows(i).Cells(4).Value = DataGridView6.Rows(i).Cells(5).Value - 1
                    Else
                        DataGridView6.Rows(i).Cells(4).Value = 0
                    End If
                Next
            End If
        End If
    End Sub
    Public Sub CopyDirectory(ByVal sourcePath As String, ByVal destinationPath As String)
        Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(sourcePath)

        ' If the destination folder don't exist then create it
        If Not System.IO.Directory.Exists(destinationPath) Then
            System.IO.Directory.CreateDirectory(destinationPath)
        End If

        Dim fileSystemInfo As System.IO.FileSystemInfo
        For Each fileSystemInfo In sourceDirectoryInfo.GetFileSystemInfos
            Dim destinationFileName As String =
                System.IO.Path.Combine(destinationPath, fileSystemInfo.Name)

            ' Now check whether its a file or a folder and take action accordingly
            If TypeOf fileSystemInfo Is System.IO.FileInfo Then
                System.IO.File.Copy(fileSystemInfo.FullName, destinationFileName, True)
            Else
                ' Recursively call the mothod to copy all the neste folders
                CopyDirectory(fileSystemInfo.FullName, destinationFileName)
            End If
        Next
    End Sub



    Private Sub potvrdenie(ByVal zakazka As String, ByVal nazov As String, ByVal stlpec As Integer)
        GroupBox1.Text = nazov
        GroupBox1.Show()
        Button3.Show()
        Button4.Show()
        ListView1.Clear()
        zakazk = zakazka
        Dim cesta2 As String
        cesta2 = My.Settings.Rotek3 + "zakazky\"
        cesta2 = cesta2 & zakazka + "\"

        If GroupBox1.Text = "Potvrdenie objednávky" Then
            cesta2 = cesta2 + "Potvrdenie objednavky\"
        ElseIf GroupBox1.Text = "Dodacie listy" Then
            cesta2 = cesta2 + "Dodacie listy\"
        ElseIf GroupBox1.Text = "Faktury" Then
            cesta2 = cesta2 + "Faktury\"
        ElseIf GroupBox1.Text = "Cenova ponuka" Then
            cesta2 = cesta2 + "Cenova ponuka\"
        Else
            cesta2 = My.Settings.Rotek3 + "Slepy\"

        End If

        Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
        If IsDBNull(DataGridView2.Rows(0).Cells(stlpec).Value) = False Then
            ListView1.Items.Add("Papiere:")
            Dim potvrdenia As String = DataGridView2.Rows(0).Cells(stlpec).Value
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
    Private Sub subory(ByVal cesta As String, ByVal zakazka As String)
        zakazk = zakazka
        GroupBox1.Show()
        ListView1.ShowItemToolTips = True
        GroupBox1.Text = cesta
        Dim cesta2 As String
        cesta2 = My.Settings.Rotek3 + "zakazky\"
        cesta2 = cesta2 & zakazka + "\" + cesta
        ListView1.Clear()

        ListView1.Items.Add("Zobraziť priečinok", "")
        Try
            For Each files In System.IO.Directory.GetFiles(cesta2)
                ListView1.Items.Add(files.Substring(files.LastIndexOf("\") + 1), "")
            Next
            For Each folders In System.IO.Directory.GetDirectories(cesta2)
                Dim cesta3 As String = String.Copy(cesta2)
                cesta3 = folders + "\"

                'While (System.IO.Directory.GetDirectories(cesta3).Length <> 0)
                '    If System.IO.Directory.GetFiles(cesta3).Length <> 0 Then
                '        ListView1.Items.Add(cesta3.Replace(cesta2, ""), "") 'cesta3.Replace(cesta2, ""))
                '        For Each files In System.IO.Directory.GetFiles(cesta3)
                '            If files.Substring(files.LastIndexOf("\") + 1) <> "Thumbs.db" Then
                '                ListView1.Items.Add(files.Substring(files.LastIndexOf("\") + 1), cesta3.Replace(cesta2, ""))
                '            End If
                '        Next
                '    End If

                '    cesta3 = System.IO.Directory.GetDirectories(cesta3)(0) + "\"
                'End While
                ' Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.VykresyColumn, 0)
                '   ListView1.Items.Add(cesta3.Replace(cesta2, ""), "") 'cesta3.Replace(cesta2, ""))
                If System.IO.Directory.GetFiles(cesta3).Length <> 0 Then
                    ListView1.Items.Add(cesta3.Replace(cesta2, ""), 1) 'cesta3.Replace(cesta2, ""))
                    For Each files In System.IO.Directory.GetFiles(cesta3)
                        If files.Substring(files.LastIndexOf("\") + 1) <> "Thumbs.db" Then
                            ListView1.Items.Add(files.Substring(files.LastIndexOf("\") + 1), cesta3.Replace(cesta2, ""))
                        End If
                    Next
                Else
                    ListView1.Items.Add(cesta3.Replace(cesta2, ""), 0) 'cesta3.Replace(cesta2, ""))
                End If
            Next

        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListView1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles ListView1.DoubleClick
        Try
            If String.IsNullOrEmpty(ListView1.SelectedItems(0).Text.Length = 0) Then
                Exit Sub
            End If
        Catch ex As Exception
            Exit Sub
        End Try


        If Button3.Visible = True Then
            Dim cesta2 As String = My.Settings.Rotek3 + "zakazky\" + zakazk + "\"
            Select Case GroupBox1.Text
                Case "Dodacie listy"
                    cesta2 = cesta2 + "Dodacie listy\"
                Case "Faktury"
                    cesta2 = cesta2 + "Faktury\"
                Case "Cenova ponuka"
                    cesta2 = cesta2 + "Cenova ponuka\"
                Case "Potvrdenie objednávky"
                    cesta2 = cesta2 + "Potvrdenie objednavky\"
                Case Else
                    cesta2 = My.Settings.Rotek3 + "Vydajky\"
            End Select
            cesta2 = cesta2 & ListView1.SelectedItems(0).Text
            cesta2 = cesta2.Replace("/", "\")
            If System.IO.File.Exists(cesta2) Then
                Process.Start(cesta2)
            ElseIf System.IO.File.Exists(cesta2 + ".pdf") Then
                Process.Start(cesta2 + ".pdf")
            End If

        ElseIf ListView1.ShowItemToolTips = False Then
            If Form78.heslo = Form78.admin Or Form78.heslo = Form78.zakazkar Then
            Else
                Exit Sub
            End If

            Dim podzakazka As String = ListView1.SelectedItems(0).Text
            Dim pocet As Integer
            Dim rozprac As Integer
            Dim vec As String
            If podzakazka = "Hlavna" Then
                pocet = 1
                podzakazka = ""
            Else : pocet = 2
            End If

            If GroupBox1.Text = "Rozpracovanosť" Then
                vec = "Rozprac"
                If ListView1.SelectedItems(0).ImageIndex = 0 Then
                    rozprac = 2
                ElseIf ListView1.SelectedItems(0).ImageIndex = 1 Then
                    rozprac = 0
                Else : rozprac = 1

                End If
            Else
                Exit Sub
                vec = "Povrch_uprava"
                If ListView1.SelectedItems(0).ImageIndex = 0 Then
                    rozprac = 1
                Else : rozprac = 0

                End If
            End If

            Dim con As New OleDb.OleDbConnection
            Dim sql As String
            con.ConnectionString = My.Settings.Rotek2      '"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & cesta & "Rotek.mdb"
            con.Open()
            Dim cmd As New OleDb.OleDbCommand
            sql = "UPDATE Zakazka SET " + vec + "=" & rozprac & " WHERE Zakazka='" + zakazk + "' AND pocet=" & pocet
            If podzakazka.Length <> 0 Then
                sql = sql + " AND Podzakazka='" + podzakazka + "' "
            End If
            cmd = New OleDb.OleDbCommand(sql, con)
            cmd.ExecuteNonQuery()
            con.Close()

            ' Chyby.Show(sql)
            ListView1.SelectedItems(0).ImageIndex = rozprac
            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
            napln()
        Else
            Dim cesta2 As String = My.Settings.Rotek3 + "zakazky\" + zakazk + "\" + GroupBox1.Text + ListView1.SelectedItems(0).ImageKey + "\"
            cesta2 = cesta2 & ListView1.SelectedItems(0).Text
            cesta2 = cesta2.Replace("/", "\")
            cesta2 = cesta2.Replace("\\", "\")
            cesta2 = "\" + cesta2

            If System.IO.File.Exists(cesta2) Then
                If cesta2.LastIndexOf(".") > 20 Then
                    Process.Start(cesta2)

                End If
            ElseIf ListView1.SelectedItems(0).Text = "Zobraziť priečinok" Then
                Shell("explorer " & cesta2.Replace(ListView1.SelectedItems(0).Text, ""))
            Else
                Shell("explorer " & cesta2)
            End If
        End If

    End Sub
    Private Sub rozpracovanost(ByVal zakazka As String, ByVal stlpec As Integer, ByVal nazov As String)
        GroupBox1.Show()
        GroupBox1.Text = nazov
        ListView1.Clear()
        ListView1.ShowItemToolTips = False
        zakazk = zakazka

        Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
        'If DataGridView2.Rows(0).Cells(stlpec).Value <> 0 Then
        '    ListView1.Items.Add("Hlavna", 1)
        'Else
        '    ListView1.Items.Add("Hlavna", 0)
        'End If
        Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
        If stlpec = 3 Then
            For i As Integer = 0 To DataGridView2.RowCount - 1
                If IsDBNull(DataGridView2.Rows(i).Cells(stlpec).Value) = False Then
                    ListView1.Items.Add(DataGridView2.Rows(i).Cells(0).Value, DataGridView2.Rows(i).Cells(stlpec).Value)
                End If
            Next
        Else
            For i As Integer = 0 To DataGridView2.RowCount - 1
                If String.IsNullOrEmpty(DataGridView2.Rows(i).Cells(stlpec).Value) = False And String.Compare(DataGridView2.Rows(i).Cells(stlpec).Value, "Nie") <> 0 Then
                    ListView1.Items.Add(DataGridView2.Rows(i).Cells(0).Value, 1)
                    ListView1.Items.Add(DataGridView2.Rows(i).Cells(stlpec).Value, 1)
                Else
                    ListView1.Items.Add(DataGridView2.Rows(i).Cells(0).Value, 0)
                End If
            Next

        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        napln()
    End Sub


    Private Sub Z_Main_Click(sender As System.Object, e As System.EventArgs) Handles MyBase.Click
        zmizni()
    End Sub
    Private Sub zmizni()
        If (GroupBox1.Visible) And ((Cursor.Position.X < GroupBox1.Location.X) Or (Cursor.Position.X > (GroupBox1.Location.X + GroupBox1.Size.Width)) Or (Cursor.Position.Y < GroupBox1.Location.Y) Or (Cursor.Position.Y > (GroupBox1.Location.Y + GroupBox1.Size.Height))) Then
            GroupBox1.Hide()
            Button3.Hide()
            Button4.Hide()
            If GroupBox1.Text = "Vydajky" Then
                Button3.Text = "Pridať súbory"
            End If
        End If
        If (GroupBox2.Visible) And ((Cursor.Position.X < GroupBox2.Location.X) Or (Cursor.Position.X > (GroupBox2.Location.X + GroupBox2.Size.Width)) Or (Cursor.Position.Y < GroupBox2.Location.Y) Or (Cursor.Position.Y > (GroupBox2.Location.Y + GroupBox2.Size.Height))) Then
            GroupBox2.Hide()
            '    Button3.Hide()
            '    Button4.Hide()
            '    If GroupBox1.Text = "Vydajky" Then
            '        Button3.Text = "Pridať súbory"
            '    End If
        End If

    End Sub
    Private Sub DataGridView1_Click(sender As System.Object, e As System.EventArgs) Handles DataGridView1.Click
        zmizni()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If Form78.heslo = Form78.admin Or Form78.heslo = Form78.zakazkar Then
        Else
            Exit Sub
        End If


        If GroupBox1.Text = "Vydajky" Then
            Dim sql As String
            sql = "UPDATE Zakazka SET Vydajxa=" & 1 & " WHERE pocet=" & 1 & " AND zakazka='" + zakazk + "'"
            Form78.sqa(sql)
            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
            napln()

            Exit Sub

        End If

        Dim cesta2 As String = My.Settings.Rotek3
        cesta2 = cesta2 & "\zakazky\" + zakazk + "\"
        If GroupBox1.Text = "Potvrdenie objednávky" Then
            cesta2 = cesta2 + "Potvrdenie objednavky\"
        ElseIf GroupBox1.Text = "Dodacie listy" Then
            DL_generuj()
            Exit Sub

            cesta2 = cesta2 + "Dodacie listy\"
        ElseIf GroupBox1.Text = "Faktury" Then
            cesta2 = cesta2 + "Faktury\"
        Else
            cesta2 = cesta2 + "Cenova ponuka\"

        End If


        Dim openFileDialog1 As New OpenFileDialog
        openFileDialog1.Title = "Pridať súbory"
        openFileDialog1.InitialDirectory = "\\192.168.1.150\DataSrv"
        openFileDialog1.Filter = "Všetky súbory(*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Multiselect = True
        If openFileDialog1.ShowDialog() = DialogResult.OK Then

            For Each sl As String In openFileDialog1.FileNames

                My.Computer.FileSystem.CopyFile(sl, cesta2 + sl.Substring(sl.LastIndexOf("\") + 1), True)
                ListView1.Items.Add(sl.Substring(sl.LastIndexOf("\") + 1))
            Next
        End If
    End Sub
    Private Sub DL_generuj()
        Dim f As New DL
        f.zakazka = zakazk
        f.ShowDialog()
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        napln()

    End Sub
    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If Form78.heslo = Form78.admin Or Form78.heslo = Form78.zakazkar Then
        Else
            Exit Sub
        End If
        Dim stlpec As Integer
        Dim vec As String
        If GroupBox1.Text = "Potvrdenie objednávky" Then
            stlpec = 1
            vec = "Potvrdenie"
        ElseIf GroupBox1.Text = "Dodacie listy" Then
            stlpec = 5
            vec = "DL"
        ElseIf GroupBox1.Text = "Faktury" Then
            stlpec = 6
            vec = "Faktura"
        ElseIf GroupBox1.Text = "Vydajka" Then
            stlpec = 2
            vec = "Vydajka"
        Else
            stlpec = 10
            vec = "Cenovka"
        End If
        Dim text As String = ""
        Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazk)
        If IsDBNull(DataGridView2.Rows(0).Cells(stlpec).Value) = False Then
            ListView1.Items.Add("Papiere:")
            text = DataGridView2.Rows(0).Cells(stlpec).Value
        End If
        Dim slovo As String = InputBox("Napis identifikaciu potvrdenia objednavky", "Potvrdenie objednavky")
        If slovo.Length > 0 Then
            text = text + "|" + slovo
            Dim sql As String
            sql = "UPDATE Zakazka SET " + vec + "='" + text + "' WHERE Zakazka='" + zakazk + "' AND pocet=" & 1
            Form78.sqa(sql)
            ListView1.Items.Add(slovo)
            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
            napln()
        End If

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim kks As Integer = e.RowIndex
        If kks = -1 Then Exit Sub
        Dim f As New HmakroInfo
        f.zakazka = DataGridView1.Rows(kks).Cells(0).Value
        f.ShowDialog()
        f.Dispose()
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        napln()

    End Sub
    Public Function zaokruhli_hore(ByVal b As Double) As Integer
        Return (Math.Ceiling(b))
    End Function

    Public Function zaokruhli_dole(ByVal b As Double) As Integer
        Return (Math.Floor(b))
    End Function
    Private Sub davky(ByVal zakazka As String, ByVal stlpec As Integer, ByVal pauza As String)

        GroupBox1.Text = "Vydajky"
        GroupBox1.Show()
        Button3.Show()
        Button4.Show()
        ListView1.Clear()
        zakazk = zakazka

        Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
        If IsDBNull(DataGridView2.Rows(0).Cells(stlpec).Value) = False Then
            ListView1.Items.Add("Papiere:")
            Dim potvrdenia As String = DataGridView2.Rows(0).Cells(stlpec).Value
            While potvrdenia.IndexOf("|") >= 0
                ListView1.Items.Add(potvrdenia.Substring(potvrdenia.LastIndexOf("|") + 1))

                If potvrdenia.Substring(potvrdenia.LastIndexOf("|") + 1) = pauza Then
                    Try
                        Dim cesta2 As String
                        cesta2 = My.Settings.Rotek3 + "Vydajky\"
                        cesta2 = cesta2 & pauza
                        cesta2 = cesta2.Replace("/", "\")
                        cesta2 = (cesta2 + "\")
                        For Each file In System.IO.Directory.GetFiles(cesta2)
                            ListView1.Items.Add(file.Replace(cesta2, pauza + "\"))
                            '                            Chyby.Show(file)
                        Next

                    Catch ex As Exception
                        '                       Chyby.Show(ex.ToString)
                    End Try
                End If
                potvrdenia = potvrdenia.Substring(0, potvrdenia.LastIndexOf("|"))

            End While
        End If

    End Sub


    Private Sub ListView1_Click(sender As System.Object, e As System.EventArgs) Handles ListView1.Click
        If GroupBox1.Text = "Vydajky" Then
            Try
                Dim i As Integer = ListView1.SelectedItems(0).Index
                'Chyby.Show(i & " " & kukoko)
                Dim pauza As String = ListView1.SelectedItems(0).Text
                If i = kukoko Or pauza.LastIndexOf(".pdf") = pauza.Length - 4 Then
                    If i = kukoko Then
                        Dim cesta2 As String
                        cesta2 = My.Settings.Rotek3 + "Vydajky\"
                        cesta2 = cesta2 & ListView1.SelectedItems(0).Text
                        cesta2 = cesta2.Replace("/", "\")
                        If System.IO.File.Exists(cesta2 + ".pdf") Then
                            Process.Start(cesta2 + ".pdf")
                        End If
                    End If
                Else
                    davky(zakazk, 3, pauza)
                    kukoko = i
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Dim f As New hForm78
        f.TopLevel = True
        f.Dock = DockStyle.None
        Me.Hide()
        f.ShowDialog()
        Me.Show()
    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        napln()

    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Dim kusov(DataGridView6.RowCount) As Integer
        For i As Integer = 0 To DataGridView6.RowCount - 1
            If DataGridView6.Rows(i).Cells(0).Value() = True Then
                Try
                    kusov(0) = DataGridView6.Rows(i).Cells(3).Value()
                Catch ex As Exception
                    Chyby.Show("Nevyplnené všetky počty kusov")
                    Exit Sub
                End Try
            End If
        Next
        kusov(0) = 0
        Dim podzakazkaka(DataGridView6.RowCount) As String
        Dim b As Boolean = False

        For i As Integer = 0 To DataGridView6.RowCount - 1

            If DataGridView6.Rows(i).Cells(0).Value() = True And DataGridView6.Rows(i).Cells(5).Value() <> 0 Then
                kusov(i) = DataGridView6.Rows(i).Cells(3).Value()
                podzakazkaka(i) = DataGridView6.Rows(i).Cells(1).Value()
                b = True
            Else
                kusov(i) = 0
                podzakazkaka(i) = "x"
            End If

        Next
        If b Then
            Dim f As New Hchyba(zakazk, kusov, podzakazkaka)
            f.ShowDialog()
            f.Dispose()
            GroupBox2.Hide()
            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
            napln()
        End If

    End Sub

    Private Sub DataGridView6_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView6.CellContentClick
        If e.ColumnIndex = 0 Then
            'Chyby.Show(DataGridView6.Rows(e.RowIndex).Cells(0).Value & " " & e.RowIndex)
            DataGridView6.Rows(e.RowIndex).Cells(0).Value = 1 - DataGridView6.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            If DataGridView6.Rows(e.RowIndex).Cells(0).Value = 1 Then
                DataGridView6.Rows(e.RowIndex).Cells(3).Value = DataGridView6.Rows(e.RowIndex).Cells(2).Value
            Else
                DataGridView6.Rows(e.RowIndex).Cells(3).Value = DBNull.Value
            End If
        End If
    End Sub

    Private Sub DataGridView1_Sorted(sender As System.Object, e As System.EventArgs) Handles DataGridView1.Sorted
        napln()
    End Sub

    Private Sub Button19_Click(sender As System.Object, e As System.EventArgs) Handles Button19.Click
        Dim f As New Evidencia
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub


    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged
        napln()
    End Sub

    Private Sub TextBox4_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox4.TextChanged
        napln()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Label7.Visible = True
        napln()


        Dim strPath As String = System.Windows.Forms.Application.StartupPath & "\Vzor\Vzor2.xlsx"
        Dim cesta As String = My.Settings.Rotek3 & "Vzor2.xlsx"

        Try
            My.Computer.FileSystem.CopyFile(cesta, strPath, True)
        Catch ex As Exception
            Chyby.Show("Subor bol zmazany. Prosime vratit Vzor.xlsx do adresara " + cesta)
        End Try

        'zaciatok

        Dim excelApp As Excel.Application = New Excel.Application()
        Dim excelBook As Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet


        Try
            excelBook = excelApp.Workbooks.Open(strPath, 0, False, 5, _
            System.Reflection.Missing.Value, System.Reflection.Missing.Value, _
            False, System.Reflection.Missing.Value, System.Reflection.Missing.Value, _
            True, False, System.Reflection.Missing.Value, False)

            Dim excelSheets As Excel.Sheets = excelBook.Sheets

            excelSheet = excelSheets(1)

            Dim intNewRow As Int32 = 5
            Dim vyska As Integer = 38
            Dim strana As Integer = 12

            Dim i As Integer = 0

            Dim zaciatok As Integer = i
            Dim d As DateTime = New DateTime

            For i = 0 To DataGridView1.RowCount - 1
                If DataGridView1.Rows(i).Cells(46).Value = "10.10.2085" Then
                    Continue For
                End If
                excelSheet.Range("A" & intNewRow + i).RowHeight = vyska
                excelSheet.Range("A" & intNewRow + i).Value = i
                excelSheet.Range("B" & intNewRow + i).Value = DataGridView1.Rows(i).Cells(0).Value
                excelSheet.Range("C" & intNewRow + i).Value = DataGridView1.Rows(i).Cells(7).Value
                excelSheet.Range("D" & intNewRow + i).Value = DataGridView1.Rows(i).Cells(1).Value
                excelSheet.Range("E" & intNewRow + i).Value = DataGridView1.Rows(i).Cells(8).Value
                d = DataGridView1.Rows(i).Cells(12).Value
                excelSheet.Range("F" & intNewRow + i).Value = d.Day & "." & d.Month & "."
                d = DataGridView1.Rows(i).Cells(46).Value
                excelSheet.Range("G" & intNewRow + i).Value = d.Day & "." & d.Month & "."
                For ii As Integer = 0 To 8
                    Dim b As String = (Chr(Asc("A") + ii) & intNewRow + i)
                    pismo(b)
                Next
            Next

            wSheet = excelBook.ActiveSheet()

            excelBook.Save()
            excelSheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, strPath.Replace(".xlsx", ""), XlFixedFormatQuality.xlQualityStandard, True, True, 1, zaokruhli_hore((i) / strana), True)
            excelApp.Visible = True
            excelBook.Close()
            excelApp.Quit()

        Catch ex As Exception
            Chyby.Show(ex.ToString)
            excelApp.Quit()
            excelBook.Close()
        End Try
    End Sub
    Private Sub pismo(ByVal b As String)
        excelSheet.Range(b).Font.Name = "Times"
        excelSheet.Range(b).Font.Size = 12
        Dim vyska As Integer = 38

        While (excelSheet.Range(b).RowHeight <> vyska) And (excelSheet.Range(b).Font.Size >= 9)
            excelSheet.Range(b).Font.Size = excelSheet.Range(b).Font.Size - 1
        End While
        If (excelSheet.Range(b).Height <> vyska) Then
            excelSheet.Range(b).WrapText = False
        End If
    End Sub

End Class