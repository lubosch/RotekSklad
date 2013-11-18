Imports System.Threading
Imports OfficeOpenXml
Imports System.IO
Imports System.Data.SqlClient

Public Class Z_Main
    Private excelSheet As ExcelWorksheet
    Dim kukoko As Integer
    Private zakazk As String
    Private zafarbi As Integer

    Const ZAKAZKACOL = 0 '0
    Const NAZOVCOL = ZAKAZKACOL + 1 '1
    Const VEDUCICOL = NAZOVCOL + 1 '2
    Const KUSOVCOL = VEDUCICOL + 1 '3
    Const ZAEVIDOVALCOL = KUSOVCOL + 1 '4
    Const CENACOL = ZAEVIDOVALCOL + 1 '5
    Const ZAKAZNIKCOL = CENACOL + 2 '7
    Const OBJEDNAVKACISLOCOL = ZAKAZNIKCOL + 1 '8
    Const POTVRDENIECOL = OBJEDNAVKACISLOCOL + 1 '9
    Const POTVRDENIEOBJEDNAVKYBUTTON = POTVRDENIECOL + 1 '10
    Const CENOVAPONUKABUTTON = POTVRDENIEOBJEDNAVKYBUTTON + 1 '11
    Const PRIJATIACOL = CENOVAPONUKABUTTON + 1 '12
    Const POZADOVANEHOUKONCENIACOL = PRIJATIACOL + 3 '15
    Const SKUTOCNEHOUKONCENIACOL = POZADOVANEHOUKONCENIACOL + 1 '16
    Const VYDAJKACOL = SKUTOCNEHOUKONCENIACOL + 1  '17
    Const VYDAJKABUTTON = VYDAJKACOL + 1 '18
    Const VYKRESYCOL = VYDAJKABUTTON + 1 '19
    Const VYKRESYBUTTON = VYKRESYCOL + 1 '20
    Const ZODPOVEDNYKONSTRUKCIECOL = VYKRESYBUTTON + 1 '21
    Const PCNCCOL = ZODPOVEDNYKONSTRUKCIECOL + 1 '22
    Const CNCBUTTON = PCNCCOL + 1 '23 
    Const PREZCOL = CNCBUTTON + 1 '24
    Const REZACKABUTTON = PREZCOL + 3 '27
    Const ELEKTRODABUTTON = REZACKABUTTON + 1 '28
    Const UKONCENIEKONSTRUKCIECOL = ELEKTRODABUTTON + 1 '29
    Const INESUBORYBUTTON = UKONCENIEKONSTRUKCIECOL + 1 '30
    Const ROZPRACCOL = INESUBORYBUTTON + 1 '31
    Const ROZPRACOVANOSTBUTTON = ROZPRACCOL + 1 '32
    Const POVRCHUPRAVACOL = ROZPRACOVANOSTBUTTON + 1 '33
    Const POVRCHOVAUPRAVABUTTON = POVRCHUPRAVACOL + 1 '34
    Const TEPELUPRAVACOL = POVRCHOVAUPRAVABUTTON + 1 '35
    Const TEPELNAUPRAVABUTTON = TEPELUPRAVACOL + 1 '36
    Const DLCOL = TEPELNAUPRAVABUTTON + 1 '37
    Const DODACIELISTYBUTTON = DLCOL + 1 '38
    Const POZNAMKYBUTTON = DODACIELISTYBUTTON + 1 '38
    Const FAKTURACOL = POZNAMKYBUTTON + 2 '41
    Const FAKTURABUTTON = FAKTURACOL + 1 '42
    Const PODZAKAZKACOL = FAKTURABUTTON + 1 '43
    Const UPRAVITBUTTON = PODZAKAZKACOL + 1 '44
    Const ZMAZATBUTTON = UPRAVITBUTTON + 1 '45
    Const POZADOVANEHOUKONCENIA2COL = ZMAZATBUTTON + 1 '46
    Const ZMENITSTAVBUTTON = POZADOVANEHOUKONCENIA2COL + 1 '47
    Const PELEKTRODCOL = ZMENITSTAVBUTTON + 1 '48
    Const VYDAJXACOL = PELEKTRODCOL + 1 '49
    Const CENOVAPONUKACOL = VYDAJXACOL + 1 '50
    Const REKLAMACIABUTTON = CENOVAPONUKACOL + 3 '53
    Const RAZYCOL = REKLAMACIABUTTON + 1 '54
    Const POCETCOL = RAZYCOL + 1 '55



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
        For i As Integer = 0 To DataGridView1.Columns.Count - 1
            Debug.Write(DataGridView1.Columns(i).HeaderText & "|")

        Next
        ZakazkaTableAdapter.Fill(RotekDataSet1.Zakazka)
        'Me.ZakazkaTableAdapter.Prve(Me.RotekDataSet.Zakazka)
        'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
        Label7.Text = ""
        rozmers()
        'napln_thread()
        poverenie()
        NumericUpDown1.Value = Now.Year - 2000


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


        If GroupBox1.Visible Then
            GroupBox1.Hide()
            GroupBox1.Show()
        End If

    End Sub

    Private Sub Z_Main_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged
        rozmers()

    End Sub
    Private Sub napln_thread(ByVal fil As String)
        ZakazkaBindingSource.Sort = fil
        '  DataGridView1.Columns(0).Visible = False
        Label7.Visible = False
        'napln_thread()
    End Sub
    Private Sub napln_thread()
        If TextBox5.Text.Length = 0 Then
        Else
            napln2()
            Exit Sub
        End If
        If TextBox4.Text.Length = 0 Then
            'Me.ZakazkaTableAdapter.Prve(Me.RotekDataSet.Zakazka)
            Me.ZakazkaTableAdapter.Filtered(String.Format("{0} = {1} AND {2} LIKE '%{3}%' AND {4} LIKE '%{5}%' AND {6} LIKE '%{7}%' AND {8} LIKE '%{9}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, TextBox1.Text, RotekDataSet.Zakazka.MenoColumn, TextBox2.Text, RotekDataSet.Zakazka.ZakaznikColumn, TextBox3.Text, RotekDataSet.Zakazka.ZakazkaColumn, NumericUpDown1.Value), Me.RotekDataSet.Zakazka)

            'Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}%' AND {4} LIKE '%{5}%' AND {6} LIKE '%{7}%' AND {8} LIKE '%{9}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, TextBox1.Text, RotekDataSet.Zakazka.MenoColumn, TextBox2.Text, RotekDataSet.Zakazka.ZakaznikColumn, TextBox3.Text, RotekDataSet.Zakazka.ZakazkaColumn, NumericUpDown1.Value)
        Else
            Me.ZakazkaTableAdapter.Filtered(String.Format("{0} = {1} AND {2} LIKE '%{3}%' AND {4} LIKE '%{5}%' AND {6} LIKE '%{7}%' AND {8} LIKE '%{9}%'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, TextBox1.Text, RotekDataSet.Zakazka.MenoColumn, TextBox2.Text, RotekDataSet.Zakazka.ZakaznikColumn, TextBox3.Text, RotekDataSet.Zakazka.ObjednavkaColumn, TextBox4.Text), Me.RotekDataSet.Zakazka)
            'Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}%' AND {4} LIKE '%{5}%' AND {6} LIKE '%{7}%' AND {8} LIKE '%{9}%'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, TextBox1.Text, RotekDataSet.Zakazka.MenoColumn, TextBox2.Text, RotekDataSet.Zakazka.ZakaznikColumn, TextBox3.Text, RotekDataSet.Zakazka.ObjednavkaColumn, TextBox4.Text)
        End If

        If Label7.Visible = True Then
            ZakazkaBindingSource.Sort = "OrderNum, Zakazka DESC"


            'ZakazkaBindingSource.DataSource = 
        Else
            '  Label7.Visible = True 
        End If

    End Sub

    'Private Sub napln_thread()
    '    Dim pln As Thread
    '    pln = New Thread(Sub() Invoke(Sub() napln()))
    '    pln.IsBackground = True
    '    pln.Priority = ThreadPriority.AboveNormal

    '    pln.Start()

    'End Sub


    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.Rows(e.RowIndex).Cells(POCETCOL).Value <> 1 Then
            Exit Sub
        End If

        If e.ColumnIndex = POZADOVANEHOUKONCENIACOL Then
            If DataGridView1.Rows(e.RowIndex).Cells(POZADOVANEHOUKONCENIA2COL).Value = "10.10.2085" Then
                If DataGridView1.Rows(e.RowIndex).Cells(VYDAJXACOL).Value = 3 Then
                    e.Value = "Doreklamovaná"
                    DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Gainsboro
                Else
                    e.Value = "Ukončená"
                End If
            Else
                e.Value = DataGridView1.Rows(e.RowIndex).Cells(POZADOVANEHOUKONCENIA2COL).Value
                Dim datum As Date = Convert.ToDateTime(e.Value)
                If datum.CompareTo(Now.Date.AddDays(10)) < 0 And datum.CompareTo(Now.Date) > 0 Then
                    DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Yellow
                Else
                    Select Case (datum.CompareTo(Now.Date))
                        Case 0
                            DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Orange
                        Case Is < 0
                            DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Tomato
                        Case Else
                            DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Silver
                    End Select
                End If
            End If
        End If

        If e.ColumnIndex = ZMENITSTAVBUTTON Then
            If DataGridView1.Rows(e.RowIndex).Cells(POZADOVANEHOUKONCENIA2COL).Value = "10.10.2085" Then
                e.Value = "Otvoriť"
            Else
                e.Value = "Ukončiť"
            End If

            If DataGridView1.Rows(e.RowIndex).Cells(VYDAJXACOL).Value = 2 Then
                e.Value = "Doreklamovať"
            End If
        End If


        If e.ColumnIndex = REKLAMACIABUTTON Then

            If DataGridView1.Rows(e.RowIndex).Cells(VYDAJXACOL).Value = 2 Then
                DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LimeGreen
                DataGridView1.Rows(e.RowIndex).Cells(POZADOVANEHOUKONCENIACOL).Value = "Reklamácia"
            End If
        End If

        If e.ColumnIndex = VYKRESYBUTTON Then ' Vykresy
            If DataGridView1.Rows(e.RowIndex).Cells(VYKRESYCOL).Value = 1 Then
                e.CellStyle.BackColor = Color.Maroon
            ElseIf DataGridView1.Rows(e.RowIndex).Cells(VYKRESYCOL).Value = 2 Then
                e.CellStyle.BackColor = Color.Green
            End If
        End If

        If e.ColumnIndex = REZACKABUTTON Then
            If DataGridView1.Rows(e.RowIndex).Cells(PREZCOL).Value = 1 Then
                e.CellStyle.BackColor = Color.Maroon
            ElseIf DataGridView1.Rows(e.RowIndex).Cells(PREZCOL).Value = 2 Then
                e.CellStyle.BackColor = Color.Green
            End If
        End If

        If e.ColumnIndex = CNCBUTTON Then
            If DataGridView1.Rows(e.RowIndex).Cells(PCNCCOL).Value = 1 Then
                e.CellStyle.BackColor = Color.Maroon
            ElseIf DataGridView1.Rows(e.RowIndex).Cells(PCNCCOL).Value = 2 Then
                e.CellStyle.BackColor = Color.Green
            End If
        End If

        If e.ColumnIndex = ELEKTRODABUTTON Then
            If DataGridView1.Rows(e.RowIndex).Cells(PELEKTRODCOL).Value = 1 Then
                e.CellStyle.BackColor = Color.Maroon
            ElseIf DataGridView1.Rows(e.RowIndex).Cells(PELEKTRODCOL).Value = 2 Then
                e.CellStyle.BackColor = Color.Green
            End If
        End If

        If e.ColumnIndex = VYDAJKABUTTON Then

            If DataGridView1.Rows(e.RowIndex).Cells(VYDAJXACOL).Value = 0 Then
                e.CellStyle.BackColor = Color.Maroon
            ElseIf DataGridView1.Rows(e.RowIndex).Cells(VYDAJXACOL).Value = 1 Then
                e.CellStyle.BackColor = Color.Green
            ElseIf DataGridView1.Rows(e.RowIndex).Cells(VYDAJXACOL).Value = 2 Then
                e.CellStyle.BackColor = Color.Black
            Else
                e.CellStyle.BackColor = Color.DarkMagenta
            End If

        End If
    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim f As New Hmakro
        f.Owner = Me
        Me.Hide()
        f.ShowDialog()
        Me.Show()
        f.Dispose()
        'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
        ZakazkaTableAdapter.Fill(RotekDataSet1.Zakazka)
        napln_thread()

    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

        Dim kks As Integer = e.RowIndex
        '   MessageBox.Show(e.RowIndex)

        If kks = -1 Then

            If e.ColumnIndex = POZADOVANEHOUKONCENIACOL Then
                napln_thread("D_plan ASC")
            ElseIf e.ColumnIndex = ZAKAZKACOL Then
                Label7.Visible = True
                napln_thread()
            ElseIf e.ColumnIndex = VYKRESYBUTTON Then
                napln_thread(DataGridView1.Columns(VYKRESYCOL).DataPropertyName + " ASC")
            ElseIf e.ColumnIndex = CNCBUTTON Then
                napln_thread(DataGridView1.Columns(PCNCCOL).DataPropertyName + " DESC")
            ElseIf e.ColumnIndex = REZACKABUTTON Then
                napln_thread(DataGridView1.Columns(PREZCOL).DataPropertyName + " DESC")
            ElseIf e.ColumnIndex = ELEKTRODABUTTON Then
                napln_thread(DataGridView1.Columns(PELEKTRODCOL).DataPropertyName + " DESC")
            ElseIf e.ColumnIndex = VYDAJKABUTTON Then
                napln_thread(DataGridView1.Columns(VYDAJKACOL).DataPropertyName + " DESC")
            Else
                '    ZakazkaBindingSource.Sort = DataGridView1.Columns(e.ColumnIndex).DataPropertyName
                napln_thread(DataGridView1.Columns(e.ColumnIndex).DataPropertyName)
            End If

        Else
            DataGridView1.Rows(e.RowIndex).Selected = True

            Dim zakazka As String = DataGridView1.Rows(kks).Cells(ZAKAZKACOL).Value
            'MessageBox.Show(zakazka)
            If e.ColumnIndex = ZMAZATBUTTON Then 'zmazat
                zakazk = zakazka
                If MessageBox.Show("Naozajchcete zmazať zákazku """ & zakazka & """ so všetkými časťami a položkami navždy???", "Nerob to!", MessageBoxButtons.YesNo) = vbYes Then
                    zmazat(zakazka)
                End If
            ElseIf e.ColumnIndex = UPRAVITBUTTON Then 'upravit
                Dim f As New Hmakro
                f.zakazka = DataGridView1.Rows(e.RowIndex).Cells(ZAKAZKACOL).Value
                f.Owner = Me
                Me.Hide()
                f.ShowDialog()
                Me.Show()
                f.Dispose()
                'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
                napln_thread()
            ElseIf e.ColumnIndex = POTVRDENIEOBJEDNAVKYBUTTON Then 'potvrdenie
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                potvrdenie(zakazka, "Potvrdenie objednavky", 1)
            ElseIf e.ColumnIndex = CENOVAPONUKABUTTON Then 'Cenova ponuka
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                potvrdenie(zakazka, "Cenova ponuka", 10)
            ElseIf e.ColumnIndex = VYDAJKABUTTON Then 'Vydajka
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                davky(zakazka, 3, "x|}")
                Button3.Text = ("Hotovo")
            ElseIf e.ColumnIndex = VYKRESYBUTTON Then 'Vykresy
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                subory("Vykresy\", zakazka)
            ElseIf e.ColumnIndex = CNCBUTTON Then 'NC
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                subory("Programy\" + "NC\", zakazka)
            ElseIf e.ColumnIndex = REZACKABUTTON Then 'EIR
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                subory("Programy\" + "EIR\", zakazka)
            ElseIf e.ColumnIndex = ELEKTRODABUTTON Then 'Elektroda
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                subory("Programy\" + "Elektroda\", zakazka)
            ElseIf e.ColumnIndex = INESUBORYBUTTON Then 'Ine
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                subory("Ine\", zakazka)
            ElseIf e.ColumnIndex = ROZPRACOVANOSTBUTTON Then 'Rozpracovanost
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                rozpracovanost(zakazka, 3, "Rozpracovanosť")
            ElseIf e.ColumnIndex = POVRCHOVAUPRAVABUTTON Then 'Povrchová úprava
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                rozpracovanost(zakazka, 4, "Povrchová úprava")
            ElseIf e.ColumnIndex = TEPELNAUPRAVABUTTON Then 'Tepelná úprava
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                rozpracovanost(zakazka, 11, "Tepelná úprava")
            ElseIf e.ColumnIndex = DODACIELISTYBUTTON Then 'Dodacie listy
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                potvrdenie(zakazka, "Dodacie listy", 5)
            ElseIf e.ColumnIndex = POZNAMKYBUTTON Then 'Poznamky
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                potvrdenie(zakazka, "Poznamky", 18)

            ElseIf e.ColumnIndex = FAKTURABUTTON Then 'Faktury
                GroupBox1.Location = New System.Drawing.Point(Cursor.Position.X - GroupBox1.Size.Width / 2, 200 + DataGridView1.Rows(0).Height * (kks + 1))
                potvrdenie(zakazka, "Faktury", 6)
            ElseIf e.ColumnIndex = ZMENITSTAVBUTTON Then 'Dokoncit
                zakazk = zakazka
                zmenit_stav(e.RowIndex, zakazka)
            ElseIf e.ColumnIndex = REKLAMACIABUTTON Then 'Reklamacja
                zakazk = zakazka
                reklamacja(zakazka, kks)

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
        Try

            GroupBox1.Text = nazov
            GroupBox1.Show()

            If Form78.heslo = Form78.admin OrElse Form78.heslo = Form78.zakazkar Then
                Button3.Show()
                Button4.Show()
                Button10.Show()
            Else
                Button3.Hide()
                Button4.Hide()
                Button10.Hide()
            End If


            ListView1.Clear()
            zakazk = zakazka
            Dim cesta2 As String
            cesta2 = My.Settings.Rotek3 + "zakazky\"
            cesta2 = cesta2 & zakazka + "\"

            If GroupBox1.Text = "Potvrdenie objednavky" Then
                cesta2 = cesta2 + "Potvrdenie objednavky\"
            ElseIf GroupBox1.Text = "Dodacie listy" Then
                cesta2 = cesta2 + "Dodacie listy\"
            ElseIf GroupBox1.Text = "Faktury" Then
                cesta2 = cesta2 + "Faktury\"
            ElseIf GroupBox1.Text = "Cenova ponuka" Then
                cesta2 = cesta2 + "Cenova ponuka\"
            ElseIf GroupBox1.Text = "Poznamky" Then
                cesta2 = cesta2 + "Poznamky\"
            Else
                cesta2 = My.Settings.Rotek3 + "Slepy\"
            End If

            If GroupBox1.Text = "Poznamky" Then
                Dim dd As DataTable = New DataTable
                SQL_main.List("SELECT zp.Datum Datum, zp.Poznamka Poznamka, zp.Typ Typ, z.Podzakazka Podzakazka FROM ZakazkaPoznamka zp JOIN Zakazka z ON z.ID = zp.Zakazka_ID WHERE z.Zakazka = '" & zakazka & "' ORDER BY Podzakazka , Datum DESC", dd)
                ListView1.Items.Add("Papiere:")
                For i As Integer = 0 To dd.Rows.Count - 1
                    Dim datum As Date = dd.Rows(i).Item("Datum")
                    Dim typ As String = dd.Rows(i).Item("Typ")
                    If typ = "Podzakazka" Then
                        Dim podzakazka As String = dd.Rows(i).Item("Podzakazka")
                        ListView1.Items.Add(datum.ToShortDateString & " - " & podzakazka & " |> " & dd.Rows(i).Item("Poznamka").ToString)
                    Else
                        ListView1.Items.Add(datum.ToShortDateString & " |> " & dd.Rows(i).Item("Poznamka").ToString)
                    End If
                Next
            Else
                Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
                If IsDBNull(DataGridView2.Rows(0).Cells(stlpec).Value) = False Then
                    ListView1.Items.Add("Papiere:")
                    Dim potvrdenia As String = DataGridView2.Rows(0).Cells(stlpec).Value
                    While potvrdenia.IndexOf("|") >= 0
                        ListView1.Items.Add(potvrdenia.Substring(potvrdenia.LastIndexOf("|") + 1))
                        potvrdenia = potvrdenia.Substring(0, potvrdenia.LastIndexOf("|"))
                    End While
                End If
            End If


            ListView1.Items.Add("Súbory:")

            If Directory.Exists(cesta2) Then
                Try
                    For Each files In System.IO.Directory.GetFiles(cesta2)
                        ListView1.Items.Add(files.Substring(files.LastIndexOf("\") + 1), "")
                    Next
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            Chyby.Show(ex.ToString)

        End Try


    End Sub
    Private Sub subory(ByVal cesta As String, ByVal zakazka As String)
        Try
            zakazk = zakazka
            GroupBox1.Show()
            ListView1.ShowItemToolTips = True
            GroupBox1.Text = cesta
            Dim cesta2 As String
            cesta2 = My.Settings.Rotek3 + "zakazky\"
            cesta2 = cesta2 & zakazka + "\" + cesta
            ListView1.Clear()
            ListView1.Items.Add("Zobraziť priečinok", "")
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
                Case "Potvrdenie objednavky"
                    cesta2 = cesta2 + "Potvrdenie objednavky\"
                Case Else
                    cesta2 = My.Settings.Rotek3 + "Vydajky\"
            End Select

            cesta2 = cesta2 & ListView1.SelectedItems(0).Text
            cesta2 = cesta2.Replace("/", "\")

            Try
                If System.IO.File.Exists(cesta2) Then
                    Process.Start(cesta2)
                ElseIf System.IO.File.Exists(cesta2 + ".pdf") Then
                    Process.Start(cesta2 + ".pdf")
                End If
            Catch ex As Exception

            End Try
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

            Dim con As New SqlConnection
            Dim sql As String
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            Dim cmd As New SqlCommand
            sql = "UPDATE Zakazka SET " + vec + "=" & rozprac & " WHERE Zakazka='" + zakazk + "' AND pocet=" & pocet
            If podzakazka.Length <> 0 Then
                sql = sql + " AND Podzakazka='" + podzakazka + "' "
            End If
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            con.Close()

            ' Chyby.Show(sql)
            ListView1.SelectedItems(0).ImageIndex = rozprac
            'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
            napln_thread()
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

        'Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
        'If DataGridView2.Rows(0).Cells(stlpec).Value <> 0 Then
        '    ListView1.Items.Add("Hlavna", 1)
        'Else
        '    ListView1.Items.Add("Hlavna", 0)
        'End If
        ' 
        Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
        If stlpec = 3 Then
            For i As Integer = 0 To DataGridView2.RowCount - 1
                If IsDBNull(DataGridView2.Rows(i).Cells(stlpec).Value) = False Then
                    ListView1.Items.Add(DataGridView2.Rows(i).Cells(0).Value, DataGridView2.Rows(i).Cells(stlpec).Value)
                End If
            Next
        Else
            For i As Integer = 0 To DataGridView2.RowCount - 1
                If String.IsNullOrEmpty(DataGridView2.Rows(i).Cells(stlpec).Value) = False And DataGridView2.Rows(i).Cells(stlpec).Value <> "Nie" Then
                    ListView1.Items.Add(DataGridView2.Rows(i).Cells(0).Value, 1)
                    ListView1.Items.Add(DataGridView2.Rows(i).Cells(stlpec).Value, 1)
                Else
                    ListView1.Items.Add(DataGridView2.Rows(i).Cells(0).Value, 0)
                End If
            Next
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        napln_thread()
    End Sub


    Private Sub Z_Main_Click(sender As System.Object, e As System.EventArgs) Handles MyBase.Click
        zmizni()
    End Sub
    Private Sub zmizni()
        If (GroupBox1.Visible) And ((Cursor.Position.X < GroupBox1.Location.X) Or (Cursor.Position.X > (GroupBox1.Location.X + GroupBox1.Size.Width)) Or (Cursor.Position.Y < GroupBox1.Location.Y) Or (Cursor.Position.Y > (GroupBox1.Location.Y + GroupBox1.Size.Height))) Then
            GroupBox1.Hide()
            Button3.Hide()
            Button10.Hide()
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
            'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
            napln_thread()

            Exit Sub

        End If

        Dim cesta2 As String = My.Settings.Rotek3
        cesta2 = cesta2 & "\zakazky\" + zakazk + "\"
        If GroupBox1.Text = "Potvrdenie objednavky" Then
            cesta2 = cesta2 + "Potvrdenie objednavky\"
        ElseIf GroupBox1.Text = "Dodacie listy" Then
            DL_generuj()
            Exit Sub
            cesta2 = cesta2 + "Dodacie listy\"
        ElseIf GroupBox1.Text = "Faktury" Then
            cesta2 = cesta2 + "Faktury\"
        ElseIf GroupBox1.Text = "Poznamky" Then
            cesta2 = cesta2 + "Poznamky\"
        Else
            cesta2 = cesta2 + "Cenova ponuka\"

        End If


        Dim openFileDialog1 As New OpenFileDialog
        openFileDialog1.Title = "Pridať súbory"
        openFileDialog1.InitialDirectory = "\\192.168.1.150"
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
        'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
        napln_thread()

    End Sub
    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If Form78.heslo = Form78.admin Or Form78.heslo = Form78.zakazkar Then
        Else
            Exit Sub
        End If

        If GroupBox1.Text = "Poznamky" Then
            Dim slovo As String = InputBox("Text potvrdenia pre " + GroupBox1.Text, "Potvrdenie " + GroupBox1.Text)
            If slovo.Length > 0 Then
                SQL_main.Odpalovac("INSERT INTO ZakazkaPoznamka (Zakazka_ID, Poznamka, Typ) VALUES((SELECT TOP 1 ID FROM Zakazka WHERE Zakazka = '" & zakazk & "' AND pocet = 1),'" & slovo & "', 'Zakazka')")
                potvrdenie(zakazk, "Poznamky", 5)
            End If

        Else
            Dim stlpec As Integer
            Dim vec As String
            If GroupBox1.Text = "Potvrdenie objednavky" Then
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
            ListView1.Items.Add("Papiere:")
            If IsDBNull(DataGridView2.Rows(0).Cells(stlpec).Value) = False Then
                text = DataGridView2.Rows(0).Cells(stlpec).Value
            End If
            Dim slovo As String = InputBox("Text potvrdenia pre " + GroupBox1.Text, "Potvrdenie " + GroupBox1.Text)
            If slovo.Length > 0 Then
                text = text + "|" + slovo
                Dim sql As String
                sql = "UPDATE Zakazka SET " + vec + "='" + text + "' WHERE Zakazka='" + zakazk + "' AND pocet=" & 1
                Form78.sqa(sql)
                ListView1.Items.Add(slovo)
                Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet1.Zakazka)
                napln_thread()
            End If
        End If

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim kks As Integer = e.RowIndex
        If kks = -1 Then Exit Sub
        Dim f As New Z_Info
        f.zakazka = DataGridView1.Rows(kks).Cells(ZAKAZKACOL).Value
        f.Owner = Me
        f.ShowDialog()
        f.Dispose()
        'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
        napln_thread()

    End Sub
    Public Function zaokruhli_hore(ByVal b As Double) As Integer
        Return (Math.Ceiling(b))
    End Function

    Public Function zaokruhli_dole(ByVal b As Double) As Integer
        Return (Math.Floor(b))
    End Function
    Private Sub davky(ByVal zakazka As String, ByVal stlpec As Integer, ByVal pauza As String)

        GroupBox1.Text = "Vydajky"

        If Form78.heslo = Form78.admin OrElse Form78.heslo = Form78.zakazkar Then
            Button3.Show()
            Button4.Show()
            Button10.Show()
        Else
            Button3.Hide()
            Button4.Hide()
            Button10.Hide()
        End If


        GroupBox1.Show()
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

            ListView1.Items.AddRange(Vydajka_SQL.vydajky(zakazka))
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

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs)
        Dim f As New hForm78
        f.TopLevel = True
        f.Dock = DockStyle.None
        Me.Hide()
        f.ShowDialog()
        Me.Show()
        f.Dispose()

    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        napln_thread()

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
            'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
            napln_thread()
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

        napln_thread()
    End Sub

    Private Sub Button19_Click(sender As System.Object, e As System.EventArgs) Handles Button19.Click
        Dim f As New Evidencia
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub


    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged
        napln_thread()
    End Sub

    Private Sub TextBox4_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox4.TextChanged
        napln_thread()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Label7.Visible = True
        napln_thread()

        Me.ZakazkaTableAdapter.Filtered(String.Format("{0} = '{1}' AND {2} LIKE '%{3}%' AND {4} LIKE '%{5}%' AND {6} LIKE '%{7}%' AND {8} LIKE '%{9}' AND {10}<>'{11}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, TextBox1.Text, RotekDataSet.Zakazka.MenoColumn, TextBox2.Text, RotekDataSet.Zakazka.ZakaznikColumn, TextBox3.Text, RotekDataSet.Zakazka.ZakazkaColumn, NumericUpDown1.Value, RotekDataSet.Zakazka.D_planColumn, "10.10.2085"), Me.RotekDataSet.Zakazka)
        'Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}%' AND {4} LIKE '%{5}%' AND {6} LIKE '%{7}%' AND {8} LIKE '%{9}' AND {10}<>'{11}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, TextBox1.Text, RotekDataSet.Zakazka.MenoColumn, TextBox2.Text, RotekDataSet.Zakazka.ZakaznikColumn, TextBox3.Text, RotekDataSet.Zakazka.ZakazkaColumn, NumericUpDown1.Value, RotekDataSet.Zakazka.D_planColumn, "10.10.2085")
        ZakazkaBindingSource.Sort = "OrderNum, Zakazka ASC"

        Dim strPath As String = My.Settings.Rotek3 & "Pomocny\Vzor2.xlsx"
        Dim cesta As String = My.Settings.Rotek3 & "Vzor2.xlsx"

        Try
            My.Computer.FileSystem.CopyFile(cesta, strPath, True)
        Catch ex As Exception
            Chyby.Show("Subor bol zmazany. Prosime vratit Vzor2.xls do adresara " + cesta)
        End Try

        'zaciatok

        Dim fufukeh As FileInfo = New FileInfo(strPath)
        Dim excelApp As ExcelPackage = New ExcelPackage(fufukeh)

        If excelApp Is Nothing Then Chyby.Show("s")

        Try
            excelSheet = excelApp.Workbook.Worksheets.First

            Dim intNewRow As Int32 = 4
            Dim vyska As Integer = 38
            Dim strana As Integer = 13

            Dim i As Integer = 1

            Dim zaciatok As Integer = i + intNewRow
            Dim d As DateTime = New DateTime
            excelSheet.Cells("D" & 2).Value = "ROK 20" & NumericUpDown1.Value
            ' excelSheet.Cells("D2").Style.Font.

            For i = 0 To DataGridView1.RowCount - 1
                'If DataGridView1.Rows(i).Cells(46).Value = "10.10.2085" Then
                '    Continue For
                'End If
                excelSheet.Row(zaciatok).Height = excelSheet.Row(zaciatok).Height * 2.4
                excelSheet.Cells("A" & zaciatok).Value = zaciatok + 1
                excelSheet.Cells("B" & zaciatok).Value = DataGridView1.Rows(i).Cells(ZAKAZKACOL).Value
                excelSheet.Cells("C" & zaciatok).Value = DataGridView1.Rows(i).Cells(ZAKAZNIKCOL).Value
                excelSheet.Cells("D" & zaciatok).Value = DataGridView1.Rows(i).Cells(NAZOVCOL).Value
                excelSheet.Cells("E" & zaciatok).Value = DataGridView1.Rows(i).Cells(OBJEDNAVKACISLOCOL).Value
                d = DataGridView1.Rows(i).Cells(PRIJATIACOL).Value
                excelSheet.Cells("F" & zaciatok).Value = d.Day & "." & d.Month & "."
                d = DataGridView1.Rows(i).Cells(POZADOVANEHOUKONCENIA2COL).Value
                excelSheet.Cells("G" & zaciatok).Value = d.Day & "." & d.Month & "."
                'For ii As Integer = 0 To 8
                '    Dim b As String = (Chr(Asc("A") + ii) &  i)
                '    pismo(b)
                'Next
                ' 
                zaciatok = zaciatok + 1
                If (i + 1) Mod strana = 0 Then
                    excelSheet.Cells("A1:I" & intNewRow).Copy(excelSheet.Cells("A" & zaciatok & ":I" & zaciatok + intNewRow - 1))
                    zaciatok = zaciatok + intNewRow
                    '                    excelSheet.Row(zaciatok - 1).Height = excelSheet.Row(zaciatok - 1).Height * 4
                End If

            Next

            'For i = 0 To excelSheet.Drawings.Count - 1
            '    If excelSheet.Drawings(i).GetType.ToString = "OfficeOpenXml.Drawing.ExcelPicture" Then
            '        Dim pics As Drawing.ExcelPicture
            '        pics = excelSheet.Drawings(i)
            '        pics.SetSize(53)
            '        '                    Chyby.Show(excelSheet.Drawings(i).GetType.ToString)
            '    End If
            'Next
            '            excelSheet.NamedRanges = excelSheet.Cells("1:4")
            'excelSheet.NamedRanges.(excelSheet.Rows(0), 2)
            excelApp.Save()

            PrehladO.tlacDoPdf(strPath)
            'Process.Start(strPath.Substring(0, cesta.LastIndexOf(".")) & ".pdf")


            'If zaokruhli_hore((i) / strana) < 1 Then
            '    excelSheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, strPath.Replace(".xlsx", ""), XlFixedFormatQuality.xlQualityStandard, True, True, 1, 1, True)
            'Else
            '    excelSheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, strPath.Replace(".xlsx", ""), XlFixedFormatQuality.xlQualityStandard, True, True, 1, zaokruhli_hore((zaciatok) / strana), True)

            'End If
            '            Thread.Sleep(1000)

            'If File.Exists(strPath.Replace(".xlsx", ".pdf")) Then
            '    Process.Start(strPath.Replace(".xlsx", ".pdf"))
            'Else
            '    Chyby.Show("Nevyexportovalo sa do PDF")
            'End If
            napln_thread()

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Private Sub pismo(ByVal b As String)
        Dim vyska As Integer = 38
        'excelSheet.Rows(b.Substring(1)).Height = 38
        excelSheet.Cells(b).Style.Font.Name = "Times"
        excelSheet.Cells(b).Style.Font.Size = 12
    End Sub
    Private Sub napln2()
        Try
            Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} LIKE '{3}%'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.PodzakazkaColumn, TextBox5.Text)
            Me.ZakazkaBindingSource.Sort = "Zakazka"
            Dim fil, slovo As String
            slovo = "fgefd"
            fil = String.Format("{0} = '{1}' AND (", RotekDataSet.Zakazka.pocetColumn, 1)
            If DataGridView2.RowCount = 0 Then
                Me.ZakazkaTableAdapter.Filtered(String.Format("{0} = '{1}'", RotekDataSet.Zakazka.pocetColumn, -1), Me.RotekDataSet.Zakazka)
                'Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Zakazka.pocetColumn, -1)
                Exit Sub
            Else
                For i As Integer = 0 To DataGridView2.RowCount - 1
                    If slovo <> DataGridView2.Rows(i).Cells(12).Value Then
                        slovo = DataGridView2.Rows(i).Cells(12).Value
                        fil = fil & String.Format(" {0} = '{1}' OR", RotekDataSet.Zakazka.ZakazkaColumn, slovo)
                    End If
                Next
                fil = fil.Substring(0, fil.LastIndexOf("OR")) & ")"
                Me.ZakazkaTableAdapter.Filtered(fil, Me.RotekDataSet.Zakazka)
                'Me.ZakazkaBindingSource.Filter = fil
            End If
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TextBox5_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text.Length = 0 Then
            napln_thread()
        Else
            napln2()
        End If
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        TextBox5.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""

    End Sub

    Private Sub ListView1_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseUp
        If e.Button = MouseButtons.Right Then
            If Form78.heslo = Form78.admin OrElse Form78.heslo = Form78.zakazkar Then
                If Button3.Visible = True Then
                    ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
                ElseIf ListView1.ShowItemToolTips = False Then
                Else
                    ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
                End If
            End If
        End If
    End Sub

    Private Function cesta_listview_item() As String
        Dim cesta2 As String = My.Settings.Rotek3 & "zakazky\" & zakazk.Replace("/", "\") & "\" & GroupBox1.Text
        If cesta2.LastIndexOf("\") <> cesta2.Length - 1 Then
            cesta2 = cesta2 & "\"
        End If
        Dim i As Integer = ListView1.SelectedItems(0).Index
        While ListView1.Items(i).Text.LastIndexOf("\") <> ListView1.Items(i).Text.Length - 1
            i = i - 1
            If i = -1 Then Exit While
        End While
        If i <> -1 Then
            cesta2 = cesta2 & ListView1.Items(i).Text
        End If
        cesta2 = cesta2 & ListView1.SelectedItems(0).Text
        Return cesta2
    End Function

    Private Sub VymazaťToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles VymazaťToolStripMenuItem1.Click
        If ListView1.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        VymazatSuborPapier()
    End Sub
    Private Sub VymazatSuborPapier()
        Dim cesta2 As String = cesta_listview_item()

        If System.IO.File.Exists(cesta2) Then
            If MessageBox.Show("Naozaj chcete zmazať súbor """ & ListView1.SelectedItems(0).Text & """", "Overenie", MessageBoxButtons.YesNo) = vbYes Then
                VymazatSubor(cesta2)
            End If
        Else
            VymazatPapier()
            'Chyby.Show(cesta2 & " sa nenašlo")
        End If
    End Sub
    Private Sub VymazatSubor(cesta As String)
        Dim i As Integer = ListView1.SelectedItems(0).Index
        Dim vec As String = ""
        If i <> -1 And (i + 2 > ListView1.Items.Count - 1 OrElse ListView1.Items(i + 2).Text.LastIndexOf("\") = ListView1.Items(i + 2).Text.Length - 1) Then

            Select Case GroupBox1.Text
                Case "Vykresy\"
                    vec = "Vykresy"
                Case "Programy\" + "NC\"
                    vec = "P_CNC"
                Case "Programy\" + "EIR\"
                    vec = "P_REZ"
                Case "Programy\" + "Elektroda\"
                    vec = "P_ELEKTROD"
            End Select
            If vec.Length <> 0 Then
                Dim sql As String = "UPDATE Zakazka SET " & vec & "='1' WHERE pocet=1 AND Zakazka='" & zakazk & "'"
                Form78.sqa(sql)
                sql = "UPDATE Zakazka SET " & vec & "='1' WHERE pocet=2 AND Zakazka='" & zakazk & "' AND Podzakazka='" & ListView1.Items(i).Text.Replace("\", "") & "'"
                Form78.sqa(sql)
                'Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, Zakazka, RotekDataSet.Zakazka.P_ELEKTRODColumn, 1)
                'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
                napln_thread()
            End If
        End If
        System.IO.File.Delete(cesta)
        subory(GroupBox1.Text, zakazk)
    End Sub

    Private Sub VymazatPapier()
        Dim stlpec As Integer
        Dim vec As String
        If GroupBox1.Text = "Potvrdenie objednavky" Then
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
        ElseIf GroupBox1.Text = "Poznamky" Then
            vec = "Poznamky"
        Else
            stlpec = 10
            vec = "Cenovka"
        End If

        Dim slovo As String = ListView1.SelectedItems(0).Text
        If vec = "Poznamky" Then
            Dim podzakazka As String
            If slovo.IndexOf("-") > 0 Then
                podzakazka = slovo.Substring(slovo.IndexOf("-") + 2, slovo.IndexOf(" |> ") - slovo.IndexOf("-") - 2)
                System.Console.WriteLine(podzakazka)
                slovo = slovo.Substring(slovo.IndexOf(" |> ") + 4)
                SQL_main.Odpalovac("DELETE FROM ZakazkaPoznamka WHERE Zakazka_ID = (SELECT TOP 1 ID FROM Zakazka WHERE Zakazka = '" & zakazk & "' AND pocet = 2 AND Podzakazka =  '" & podzakazka & "') AND Poznamka LIKE '" & slovo & "'")
            Else
                slovo = slovo.Substring(slovo.IndexOf(" |> ") + 4)
                SQL_main.Odpalovac("DELETE FROM ZakazkaPoznamka WHERE Zakazka_ID = (SELECT TOP 1 ID FROM Zakazka WHERE Zakazka = '" & zakazk & "' AND pocet = 1) AND Poznamka LIKE '" & slovo & "'")

            End If

            potvrdenie(zakazk, "Poznamky", 5)

        Else

            Dim text As String = ""
            Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazk)
            If IsDBNull(DataGridView2.Rows(0).Cells(stlpec).Value) = False Then
                text = DataGridView2.Rows(0).Cells(stlpec).Value
            End If
            If slovo.Length > 0 Then
                text = text.Replace("|" + slovo, "")
                Dim sql As String
                sql = "UPDATE Zakazka SET " + vec + "='" + text + "' WHERE Zakazka='" + zakazk + "' AND pocet=" & 1
                Form78.sqa(sql)
                ListView1.Items.RemoveAt(ListView1.SelectedItems(0).Index)
                'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
                ZakazkaTableAdapter.Fill(RotekDataSet1.Zakazka)
                napln_thread()
            End If
        End If
    End Sub
    Private Sub PridaťToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PridaťToolStripMenuItem.Click
        Dim openFileDialog1 As New OpenFileDialog
        openFileDialog1.Title = "Vybrať súbory"
        openFileDialog1.InitialDirectory = "\\192.168.1.150"
        openFileDialog1.Filter = "Všetky súbory(*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Multiselect = True
        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            pridaj_subory_zakazka(openFileDialog1.FileNames)
        End If
    End Sub
    Private Sub pridaj_subory_zakazka(paths As String())
        Dim oznacene As String
        Dim cesta2 As String = My.Settings.Rotek3 & "zakazky\" & zakazk.Replace("/", "\") & "\" & GroupBox1.Text
        If cesta2.LastIndexOf("\") <> cesta2.Length - 1 Then
            cesta2 = cesta2 & "\"
        End If
        oznacene = ""

        If ListView1.SelectedItems.Count > 0 AndAlso ListView1.SelectedItems(0).Text.LastIndexOf("\") = ListView1.SelectedItems(0).Text.Length - 1 Then
            oznacene = ListView1.SelectedItems(0).Text
            cesta2 = cesta2 & oznacene
        End If

        For Each sx As String In paths
            If System.IO.Directory.Exists(cesta2) Then
            Else
                System.IO.Directory.CreateDirectory(cesta2)
            End If

            System.IO.File.Copy(sx, cesta2 & sx.Substring(sx.LastIndexOf("\") + 1), True)
        Next

        Dim vec As String = ""
        Select Case GroupBox1.Text
            Case "Vykresy\"
                vec = "Vykresy"
                Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazk, RotekDataSet.Zakazka.VykresyColumn, 1)
                'Chyby.Show(DataGridView2.RowCount & " " & Me.ZakazkaBindingSource1.Filter)
            Case "Programy\" + "NC\"
                vec = "P_CNC"
                Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazk, RotekDataSet.Zakazka.P_CNCColumn, 1)
            Case "Programy\" + "EIR\"
                vec = "P_REZ"
                Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazk, RotekDataSet.Zakazka.P_REZColumn, 1)
            Case "Programy\" + "Elektroda\"
                vec = "P_ELEKTROD"
                Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazk, RotekDataSet.Zakazka.P_ELEKTRODColumn, 1)
        End Select
        oznacene = oznacene.Replace("\", "")

        If vec.Length <> 0 AndAlso oznacene.Length <> 0 Then
            Dim sql As String
            sql = "UPDATE Zakazka SET " & vec & "='2' WHERE pocet=2 AND Zakazka='" & zakazk & "' AND Podzakazka='" & oznacene & "'"
            Form78.sqa(sql)
            'Chyby.Show(DataGridView2.RowCount & "  " & DataGridView2.Rows(0).Cells(0).Value & "  " & oznacene) '& " " & (DataGridView2.RowCount = 1 And DataGridView2.Rows(0).Cells(0).Value = oznacene) & (DataGridView2.RowCount = 1) & (DataGridView2.Rows(0).Cells(0).Value = oznacene))
            If DataGridView2.RowCount = 0 OrElse (DataGridView2.RowCount = 1 And DataGridView2.Rows(0).Cells(0).Value = oznacene) Then
                sql = "UPDATE Zakazka SET " & vec & "='2' WHERE pocet=1 AND Zakazka='" & zakazk & "'"
                Form78.sqa(sql)
            End If
            'Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, Zakazka, RotekDataSet.Zakazka.P_ELEKTRODColumn, 1)
            'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
            napln_thread()

        End If
        subory(GroupBox1.Text, zakazk)
    End Sub

    Private Sub UpraviťToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UpraviťToolStripMenuItem.Click
        Dim f As New Hmakro
        f.zakazka = DataGridView1.Rows(ContextMenuStrip2.Tag).Cells(ZAKAZKACOL).Value
        f.Owner = Me
        Me.Hide()
        f.ShowDialog()
        Me.Show()
        f.Dispose()
        'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
        napln_thread()
    End Sub
    Private Sub ZmeniťStavToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ZmeniťStavToolStripMenuItem.Click
        zmenit_stav(ContextMenuStrip2.Tag, DataGridView1.Rows(ContextMenuStrip2.Tag).Cells(ZAKAZKACOL).Value)
    End Sub

    Private Sub ReklamáciaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ReklamáciaToolStripMenuItem.Click

        reklamacja(DataGridView1.Rows(ContextMenuStrip2.Tag).Cells(ZAKAZKACOL).Value, ContextMenuStrip2.Tag)
    End Sub

    Private Sub ZmazaťToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ZmazaťToolStripMenuItem.Click
        If MessageBox.Show("Naozajchcete zmazať zákazku """ & DataGridView1.Rows(DataGridView1.SelectedCells(0).RowIndex).Cells(ZAKAZKACOL).Value & """ so všetkými časťami a položkami navždy???", "Nerob to!", MessageBoxButtons.YesNo) = vbYes Then
            zmazat(DataGridView1.Rows(ContextMenuStrip2.Tag).Cells(ZAKAZKACOL).Value)
        End If
    End Sub

    Private Sub zmenit_stav(ByVal riadok As Integer, ByVal zakazka As String)
        Dim awkward As String
        If DataGridView1.Rows(riadok).Cells(POZADOVANEHOUKONCENIA2COL).Value = "10.10.2085" Then
            awkward = "Otvoriť"
        Else
            awkward = "Ukončiť"
        End If

        If DataGridView1.Rows(riadok).Cells(VYDAJXACOL).Value = 2 Then
            awkward = "Doreklamovať"
        End If


        If awkward = "Otvoriť" Then
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
                    datum = f.datum.ToString("yyyy-MM-dd")
                    datum2 = f.dz.ToString("yyyy-MM-dd")
                    z = f.zakazka
                    evidol = f.evidol
                Else
                    Exit Sub
                End If
                If datum = New Date(1954, 10, 10) Then
                    Exit Sub
                End If

                Me.ZakazkaTableAdapter.Filtered(String.Format("{0} = {1} AND {2} ='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, z), Me.RotekDataSet.Zakazka)
                'Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} ='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, z)
                If DataGridView1.RowCount <> 0 Then

                    Chyby.Show("Už existuje zakazka")
                    napln_thread()
                    Exit Sub

                End If
                Me.ZakazkaTableAdapter.Filtered(String.Format("{0} = {1} AND {2} ='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka), Me.RotekDataSet.Zakazka)
                'Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} ='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)

                Dim con As New SqlConnection
                Dim cmd As New SqlCommand
                con.ConnectionString = My.Settings.Rotek2
                con.Open()

                Dim s As String = ""
                Dim firma As String = DataGridView1.Rows(0).Cells(ZAKAZNIKCOL).Value
                Dim veduci As String = DataGridView1.Rows(0).Cells(VEDUCICOL).Value
                Dim uprava1 As String = DataGridView1.Rows(0).Cells(POVRCHUPRAVACOL).Value
                Dim uprava2 As String = DataGridView1.Rows(0).Cells(TEPELUPRAVACOL).Value
                Dim kusov As Integer = DataGridView1.Rows(0).Cells(KUSOVCOL).Value
                Dim menozakazky As String = DataGridView1.Rows(0).Cells(NAZOVCOL).Value
                Dim vykresy As Integer = DataGridView1.Rows(0).Cells(VYKRESYCOL).Value
                '                Chyby.Show(vykresy)
                Dim zk As String = "" ' zodpovedny konstrukcie
                If IsDBNull(DataGridView1.Rows(0).Cells(ZODPOVEDNYKONSTRUKCIECOL).Value) = False Then
                    zk = DataGridView1.Rows(0).Cells(ZODPOVEDNYKONSTRUKCIECOL).Value
                End If
                Dim pc As Integer = DataGridView1.Rows(0).Cells(PCNCCOL).Value
                Dim pr As Integer = DataGridView1.Rows(0).Cells(PREZCOL).Value
                Dim pe As Integer = DataGridView1.Rows(0).Cells(PELEKTRODCOL).Value

                sql = "Insert INTO Zakazka (Zakazka, pocet, srot, srotcena, D_plan, D_prijatia, Zaevidoval, Zakaznik, Veduci, Povrch_uprava, Tepel_uprava, Rozprac, Kusov, Razy, Meno, Vykresy, Zodp_Konstrukcie, P_CNC, P_REZ, P_ELEKTROD, Vydajxa) VALUES ('" + z + "','" & 1 & "','" & 0 & "','" & 0 & "','" + datum.ToString("yyyy-MM-dd") + "','" + datum2.ToString("yyyy-MM-dd") + "','" + evidol + "','" + firma + "','" + veduci + "','" & uprava1 & "','" & uprava2 & "','" & 0 & "','" & kusov & "','" & 0 & "','" + menozakazky + "','" & vykresy & "','" & zk & "','" & pc & "','" & pr & "','" & pe & "','" & 0 & "')"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()

                sql = "Insert INTO Huta (zakazka, pocet, srot, srotcena, D_ukoncenia,  Kusov) VALUES ('" + z + "','" & 2 & "','" & 0 & "','" & 0 & "','" + datum.ToString("yyyy-MM-dd") + "','" & 0 & "')"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()


                Me.ZakazkaTableAdapter.Filtered(String.Format("{0} = {1} AND {2} ='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka), Me.RotekDataSet.Zakazka)
                'Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} ='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    Dim podzakazka As String = DataGridView1.Rows(i).Cells(PODZAKAZKACOL).Value
                    kusov = DataGridView1.Rows(i).Cells(KUSOVCOL).Value
                    uprava1 = DataGridView1.Rows(i).Cells(POVRCHUPRAVACOL).Value
                    uprava2 = DataGridView1.Rows(i).Cells(TEPELUPRAVACOL).Value
                    vykresy = DataGridView1.Rows(i).Cells(VYKRESYCOL).Value
                    pc = DataGridView1.Rows(i).Cells(PCNCCOL).Value
                    pr = DataGridView1.Rows(i).Cells(PREZCOL).Value
                    pe = DataGridView1.Rows(i).Cells(PELEKTRODCOL).Value
                    ' vykresy = DataGridView1.Rows(i).Cells(14).Value

                    sql = "Insert INTO Zakazka (Zakazka, pocet, srot, srotcena, Povrch_uprava, Tepel_uprava, Podzakazka, Rozprac, Kusov, Razy, P_CNC, P_REZ, P_ELEKTROD, Vykresy) VALUES ('" + z + "','" & 2 & "','" & 0 & "','" & 0 & "','" & uprava1 & "','" & uprava2 & "','" + podzakazka + "','" & 0 & "','" & kusov & "','" & 0 & "','" & pc & "','" & pr & "','" & pe & "','" & vykresy & "')"
                    '                    Chyby.Show(sql)
                    cmd = New SqlCommand(sql, con)
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
                    '      Dim pocet As Integer = DataGridView3.Rows(i).Cells(8).Value
                    Dim Podzakazka As String = DataGridView3.Rows(i).Cells(5).Value
                    Me.ZakazkaTableAdapter.Filtered(String.Format("{0} = '{1}' AND {2} ='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.PodzakazkaColumn, Podzakazka), Me.RotekDataSet.Zakazka)
                    'Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} ='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.PodzakazkaColumn, Podzakazka)
                    kusov = DataGridView1.Rows(0).Cells(KUSOVCOL).Value
                    sql = "Insert INTO Huta (Druh,  Nazov, Sirka, Rozmer, Velkost, zakazka, pocet, srot, srotcena, Vaha, Kusov) VALUES ('" + druh + "', '" + nazov + "','" & sirka & "','" & rozmer & "','" & velkost & "','" + z + "','" & 1 & "','" & 0 & "','" & 0 & "','" + Podzakazka + "','" & kusov & "')"
                    cmd = New SqlCommand(sql, con)
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

                'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
                napln_thread()
            Catch ex As Exception
                Chyby.Show(ex.ToString)
            End Try
        ElseIf awkward = "Ukončiť" Then
            Dim f As New Dokoncit(zakazka)
            f.ShowDialog()
            'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
            napln_thread()
        ElseIf awkward = "Doreklamovať" Then
            Dim sql As String
            Dim d As DateTime = New DateTime(2085, 10, 10)

            sql = "UPDATE Zakazka SET Razy=3, D_plan='" & d.ToString("yyyy-MM-dd") & "', D_real='" & Now.ToString("yyyy-MM-dd") & "' WHERE Zakazka='" + zakazka + "' AND pocet=1"
            Form78.sqa(sql)
            sql = "UPDATE Huta SET D_ukoncenia='" & d.ToString("yyyy-MM-dd") & "' WHERE zakazka='" + zakazka + "' AND pocet='2'"
            Form78.sqa(sql)
            'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
            napln_thread()
        End If
    End Sub
    Private Sub reklamacja(ByVal zakazka As String, ByVal riadok As Integer)
        GroupBox2.Location = New System.Drawing.Point(800, 175 + DataGridView1.Rows(0).Height * (riadok + 1))
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
    End Sub
    Private Sub zmazat(ByVal zakazka As String)
        Dim cmd As New SqlCommand
        SQL_main.AddCommand("DELETE FROM ZakazkaPoznamka WHERE Zakazka_ID IN (SELECT ID FROM Zakazka WHERE Zakazka ='" + zakazka + "')")
        SQL_main.AddCommand("DELETE FROM Vydajky WHERE Zakazka_ID = (SELECT TOP 1 ID FROM Zakazka WHERE pocet = 1 AND Zakazka ='" + zakazka + "')")
        SQL_main.AddCommand("DELETE FROM Zakazka WHERE Zakazka='" + zakazka + "'")
        SQL_main.Odpal
        Dim cesta As String = ""
        Try
            'Chyby.Show((My.Settings.Rotek3 & "zakazky\" & zakazka).Substring("/", "\"))
            cesta = My.Settings.Rotek3 & "zakazky\" & zakazka
            System.IO.Directory.Delete(cesta.Replace("/", "\"), True)
        Catch ex As Exception
            'Chyby.Show(ex.ToString & "  " & cesta)
        End Try
        'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
        napln_thread()

    End Sub

    Private Sub DataGridView1_CellMouseUp(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp
        If e.Button = MouseButtons.Right Then
            If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
                Me.DataGridView1.CurrentCell = Me.DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
            Else
                Exit Sub
            End If

            If Form78.heslo = Form78.admin OrElse Form78.heslo = Form78.zakazkar Then
                ContextMenuStrip2.Show(MousePosition.X, MousePosition.Y)

                ContextMenuStrip2.Tag = e.RowIndex
                If DataGridView1.Rows(e.RowIndex).Cells(POZADOVANEHOUKONCENIA2COL).Value = "10.10.2085" Then
                    ContextMenuStrip2.Items(1).Text = "Vytvoriť novú"
                Else
                    ContextMenuStrip2.Items(1).Text = "Ukončiť"
                End If
                If DataGridView1.Rows(e.RowIndex).Cells(VYDAJXACOL).Value = 2 Then
                    ContextMenuStrip2.Items(1).Text = "Doreklamovať"
                End If


                If Form78.heslo = Form78.zakazkar Then
                    ContextMenuStrip2.Items(2).Visible = False
                    ContextMenuStrip2.Items(3).Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub ZobraziťVšetkyDielceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ZobraziťVšetkyDielceToolStripMenuItem.Click
        Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazk)
        ListView1.Items.Add("")
        For i As Integer = 0 To DataGridView2.RowCount - 1
            ListView1.Items.Add(DataGridView2.Rows(i).Cells(0).Value & "\")
        Next

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        Dim f As New Prehlad
        f.Owner = Me
        Me.Hide()
        f.ShowDialog()
        Try
            If Not IsDisposed() Then
                Me.Show()
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        Dim f As New PrehladO
        Me.Hide()
        f.ShowDialog()
        Me.Show()
        f.Dispose()
    End Sub

    Private Sub GroupBox1_VisibleChanged(sender As System.Object, e As System.EventArgs) Handles GroupBox1.VisibleChanged
        If GroupBox1.Location.Y + GroupBox1.Size.Height > Me.Height Then
            GroupBox1.Location = New System.Drawing.Point(GroupBox1.Location.X, MousePosition.Y - 50 - GroupBox1.Size.Height)
            If GroupBox1.Location.Y + GroupBox1.Size.Height > Me.Height Or GroupBox1.Location.Y < Me.Location.Y Then
                GroupBox1.Location = New System.Drawing.Point(GroupBox1.Location.X, Me.Height - 50 - GroupBox1.Size.Height)
            End If
        End If

        If GroupBox1.Location.X + GroupBox1.Size.Width > Me.Width Then
            GroupBox1.Location = New System.Drawing.Point(MousePosition.X - GroupBox1.Size.Width, GroupBox1.Location.Y)
            If GroupBox1.Location.X + GroupBox1.Size.Width > Me.Width Or GroupBox1.Location.X < Me.Location.X Then
                GroupBox1.Location = New System.Drawing.Point(Me.Width - GroupBox1.Size.Width, GroupBox1.Location.Y)
            End If
        End If


    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    'Private Sub DataGridView1_SortCompare(sender As Object, e As DataGridViewSortCompareEventArgs) Handles DataGridView1.SortCompare
    '    If e.Column.Index = 0 Then
    '        Chyby.Show(e.CellValue1 & " " & e.CellValue2)
    '    Else
    '        Chyby.Show(e.Column.Index)
    '    End If
    'End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        'Chyby.Show("")
        napln_thread()


    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim m As New Mail(zakazk, "", GroupBox1.Text)
        m.ShowDialog()
        m.Dispose()
        subory(GroupBox1.Text, zakazk)

    End Sub

    Private Sub PridaťPrílohuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PridaťPrílohuToolStripMenuItem.Click

        Dim m As Mail
        Dim dielec As String = ""
        If ListView1.SelectedItems.Count > 0 AndAlso ListView1.SelectedItems(0).Text.LastIndexOf("\") = ListView1.SelectedItems(0).Text.Length - 1 Then
            dielec = ListView1.SelectedItems(0).Text
        End If

        m = New Mail(zakazk, dielec, GroupBox1.Text)
        m.ShowDialog()
        m.Dispose()
        Select Case GroupBox1.Text
            Case "Vykresy\"
                Dim sql As String
                Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazk, RotekDataSet.Zakazka.VykresyColumn, 1)

                If DataGridView2.RowCount = 0 OrElse (DataGridView2.RowCount = 1 And DataGridView2.Rows(0).Cells(0).Value = dielec.Replace("\", "")) Then
                    sql = "UPDATE Zakazka SET " & "Vykresy" & "='2' WHERE pocet=1 AND Zakazka='" & zakazk & "'"
                    Form78.sqa(sql)
                    'Me.ZakazkaTableAdapter.Zoradene(Me.RotekDataSet.Zakazka)
                    napln_thread()
                End If
        End Select

        subory(GroupBox1.Text, zakazk)

    End Sub

    Private Sub ListView1_DragDrop(sender As Object, e As DragEventArgs) Handles ListView1.DragDrop
        Dim item As ListViewItem = itemNow(e)
        If item Is Nothing Then  Else item.Selected = True
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim filePaths As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            pridaj_subory_zakazka(filePaths)
        End If
        ClearLVHighlight(ListView1)
    End Sub

    Private Sub ListView1_DragEnter(sender As Object, e As DragEventArgs) Handles ListView1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ListView1_DragOver(sender As Object, e As DragEventArgs) Handles ListView1.DragOver
        Dim item As ListViewItem = itemNow(e)
        If item Is Nothing Then Exit Sub
        ListView1.BeginUpdate()
        ClearLVHighlight(ListView1)
        item.BackColor = Color.LightBlue
        item.ForeColor = Color.White
        ListView1.EndUpdate()

        'item.Selected = True
        'item.EnsureVisible()
        'Debug.WriteLine(item.Text)
    End Sub

    Private Function itemNow(e As DragEventArgs) As ListViewItem
        Dim p As Point = ListView1.PointToClient(New Point(e.X, e.Y))
        Return ListView1.GetItemAt(p.X, p.Y)
    End Function

    Private Sub ClearLVHighlight(ByVal objLV As ListView)

        For intX As Integer = 0 To objLV.Items.Count - 1
            objLV.Items(intX).ForeColor = Color.Black
            objLV.Items(intX).BackColor = Color.White
        Next

    End Sub

    Private Sub ListView1_DragLeave(sender As Object, e As EventArgs) Handles ListView1.DragLeave
        ClearLVHighlight(ListView1)

    End Sub

    Private Sub ListView1_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles ListView1.ItemDrag
        Dim cesta2() As String
        'Debug.WriteLine(Directory.Exists(cesta_listview_item.Substring(0, cesta_listview_item.Length - 1)).ToString + " " + cesta_listview_item.Substring(0, cesta_listview_item.Length - 1))
        If File.Exists(cesta_listview_item) Then

            Dim cesta(0) As String
            cesta(0) = cesta_listview_item()
            cesta2 = cesta
        Else
            Exit Sub
        End If

        Try

            ListView1.DoDragDrop(New DataObject(DataFormats.FileDrop, cesta2), DragDropEffects.Copy)
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim f As New Firma
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim f As New infoFirma
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub
End Class

Class porovnavac
    Implements IComparer

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        If x.ToString.Substring(x.ToString.Length - 2) > y.ToString.Substring(y.ToString.Length - 2) Then
            Return 1
        ElseIf x.ToString.Substring(x.ToString.Length - 2) < y.ToString.Substring(y.ToString.Length - 2) Then
            Return -1
        Else
            Return x.ToString.CompareTo(y)
        End If
    End Function
End Class





