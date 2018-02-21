Imports System.Data.SqlClient

Public Class Hmakro

    Dim k, j As Integer
    Property nastr As String
    Public zakazka As String

    Private zoznam_vykresov2 As List(Of String)
    'Private zoznam_vykresov As String
    'Private zoznam_NC As String
    Private zoznam_NC2 As List(Of String)
    'Private zoznam_EIR As String
    Private zoznam_EIR2 As List(Of String)
    'Private zoznam_EL As String
    Private zoznam_EL2 As List(Of String)
    Private zoznam_ine2 As List(Of String)
    'Private zoznam_ine As String
    Private ku As Date
    Public cp As String
    Public cp_vykresy As String

    Shared Property podzakazka As String
    Property tex As String
    Dim crc, menoo, priezviskoo As String
    Public Sub New(ByVal firma As String, ByVal veduci As String, nazov As String)
        ' This call is required by the designer.
        InitializeComponent()
        ComboBox1.Text = firma
        ComboBox2.Text = veduci
        TextBox11.Text = nazov
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.CP' table. You can move, or remove it, as needed.
        Dim timer As Stopwatch = New Stopwatch
        timer.Start()
        Me.AutoSize = True
        Try

            Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)
            Debug.WriteLine(timer.ElapsedMilliseconds)
            'Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
            Debug.WriteLine(timer.ElapsedMilliseconds)
            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
            Debug.WriteLine(timer.ElapsedMilliseconds)
            If ComboBox1.Text.Length = 0 Then
                Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
                Debug.WriteLine(timer.ElapsedMilliseconds)
                Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.ZoznamF.pocetColumn, 0)
                Debug.WriteLine(timer.ElapsedMilliseconds)
            Else
                Dim s, ss As String
                s = ComboBox1.Text
                ss = ComboBox2.Text
                Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
                Debug.WriteLine(timer.ElapsedMilliseconds)
                Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.ZoznamF.pocetColumn, 0)
                Debug.WriteLine(timer.ElapsedMilliseconds)
                ComboBox1.Text = s
                ComboBox2.Text = ss

            End If


            'Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.pocetColumn, 1, RotekDataSet.Huta.zakazkaColumn, zakazka)
            Debug.WriteLine(timer.ElapsedMilliseconds)

            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.pocetColumn, 1)
            Debug.WriteLine(timer.ElapsedMilliseconds)

            Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)
            Debug.WriteLine(timer.ElapsedMilliseconds)
            hladaj2(ListBox1, 0, "Druh")
            Debug.WriteLine(timer.ElapsedMilliseconds)
            hladaj2(ListBox3, 2, "Nazov")
            Debug.WriteLine(timer.ElapsedMilliseconds)
            'hladaj2(ListBox4, 4, "Rozmer")

            Label10.Text = "Hlavna"
            j = 1

            If String.IsNullOrEmpty(zakazka) = False Then
                ComboBox1.Text = DataGridView4.Rows(0).Cells(1).Value
                ComboBox2.Text = DataGridView4.Rows(0).Cells(2).Value
                TextBox9.Text = DataGridView4.Rows(0).Cells(0).Value
                TextBox11.Text = DataGridView4.Rows(0).Cells(18).Value
                If DataGridView4.Rows(0).Cells(20).Value.ToString = "" Then
                Else
                    TextBox12.Text = DataGridView4.Rows(0).Cells(20).Value
                End If

                TextBox2.Text = zakazka
                DateTimePicker1.Value = DataGridView4.Rows(0).Cells(5).Value
                DateTimePicker2.Value = DataGridView4.Rows(0).Cells(4).Value
                'TextBox13.Text = DataGridView4.Rows(0).Cells(4).Value

                TextBox2.ReadOnly = True

                If Form78.heslo = Form78.admin Then
                    DateTimePicker1.Enabled = True
                Else
                    DateTimePicker1.Enabled = False
                End If

                DateTimePicker2.Enabled = False
                TextBox9.ReadOnly = True
                ComboBox1.Enabled = False
                ComboBox2.Enabled = False

                If IsDBNull(DataGridView4.Rows(0).Cells(10).Value) Then
                Else
                    Dim zk As String = DataGridView4.Rows(0).Cells(10).Value
                    TextBox8.Text = zk
                    TextBox8.ReadOnly = True
                End If

                Try
                    Dim d As Date = DataGridView4.Rows(0).Cells(11).Value
                    DateTimePicker3.Value = d
                    DateTimePicker3.Enabled = False
                Catch ex As Exception

                End Try
                DataGridView3.ClearSelection()

                TextBox4.Focus()

            Else
                DateTimePicker2.Text = DateValue(Now)
                Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, "/" & Year(Now).ToString.Substring(2))
                Debug.WriteLine(timer.ElapsedMilliseconds)
                ZakazkaBindingSource.Sort = "Zakazka"

                Try
                    Dim posledna As String = DataGridView4.Rows(DataGridView4.RowCount - 1).Cells(17).Value
                    Dim i As Integer = posledna.Substring(0, posledna.IndexOf("/"))
                    TextBox2.Text = Format(i + 1, "0000") & "/" & Year(Now).ToString.Substring(2)
                Catch ex As Exception
                    Dim kks As Integer = DataGridView4.RowCount + 1
                    TextBox2.Text = Format(kks, "0000") & "/" & Year(Now).ToString.Substring(2)
                End Try
                TextBox9.Text = Form78.uzivatel
                TextBox1.Focus()
                If String.IsNullOrEmpty(cp) Then
                    ComboBox2.Text = ""
                    ComboBox1.Text = ""
                Else
                    Button17.Show()
                    cp_vykresy = cp
                End If

            End If
            Debug.WriteLine(timer.ElapsedMilliseconds)

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

        zoznam_vykresov2 = New List(Of String)
        zoznam_EIR2 = New List(Of String)
        zoznam_EL2 = New List(Of String)
        zoznam_ine2 = New List(Of String)
        zoznam_NC2 = New List(Of String)



    End Sub
    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = "1"
        TextBox1.SelectAll()

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Me.Close()
    End Sub


    Private Sub Vseobecne()
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)

        zakazka = TextBox2.Text
        podzakazka = TextBox3.Text

        If zakazka.Length = 0 Then
            Chyby.Show("Nie je zadana zakazka")
            Exit Sub
        End If

        Dim d_ukoncenia As Date
        Try
            d_ukoncenia = DateTimePicker1.Value
        Catch ex As Exception
            Chyby.Show("Zle zadaný dátum ukončenia")
            Exit Sub
        End Try

        Dim kusov As Integer = 1
        Try
            kusov = TextBox10.Text
        Catch ex As Exception
            Chyby.Show("Zle zadaný počet kusov")
            Exit Sub
        End Try

        Dim d_prijatia As Date
        Try
            d_prijatia = DateTimePicker2.Value
        Catch ex As Exception
            Chyby.Show("Zle zadaný dátum ukončenia")
            Exit Sub
        End Try

        Dim menozakazky As String = TextBox11.Text
        If menozakazky.Length = 0 Then
            Chyby.Show("Názov zákazky?")
            Exit Sub
        End If
        Dim zaevidoval As String = TextBox9.Text
        If zaevidoval.Length = 0 Then
            Chyby.Show("Kto zaevidoval?")
            Exit Sub
        End If

        Dim povrch_uprava As String
        If TextBox13.Text.Length = 0 Then
            povrch_uprava = "Nie"
        Else
            povrch_uprava = TextBox13.Text
        End If

        Dim tepel_uprava As String
        If TextBox14.Text.Length = 0 Then
            tepel_uprava = "Nie"
        Else
            tepel_uprava = TextBox14.Text
        End If

        Dim firma As String = ComboBox1.Text
        If firma.Length = 0 Then
            Chyby.Show("Nezadaný názov firmy")
            Exit Sub
        End If

        Dim veduci As String = ComboBox2.Text
        If veduci.Length = 0 Then
            Chyby.Show("Nezadaný názov zodpovedného z danej firmy")
            Exit Sub
        End If

        Dim objednavka As String = TextBox12.Text
        If objednavka.Length = 0 Then
            objednavka = ""
        End If


        Me.ZoznamFBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.ZoznamF.NazovColumn, firma, RotekDataSet.ZoznamF.pocetColumn, 0)
        If DataGridView5.RowCount < 1 Then
            If MsgBox("Nenašla sa taká firma. Chcete pridať novú firmu?", vbQuestion + vbYesNo, "Neexistuje") = vbYes Then
                Dim f As New Firma()
                f.naz = firma
                f.veduci = veduci
                f.ShowDialog()

                Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)

                ComboBox1.Text = firma
                ComboBox2.Text = veduci
                Vseobecne()
                Exit Sub
            Else
                Exit Sub
            End If
        End If

        Me.ZoznamFBindingSource2.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.ZoznamF.NazovColumn, firma, RotekDataSet.ZoznamF.pocetColumn, 1, RotekDataSet.ZoznamF.VeducColumn, veduci)
        If DataGridView5.RowCount < 1 Then
            If MsgBox("Nenašiel sa taký zamestnanec. Chcete pridať nového? ", vbQuestion + vbYesNo, "Neexistuje") = vbYes Then
                SQL_main.Odpalovac("Insert INTO ZoznamF (Nazov, pocet, Veduc) VALUES ('" + firma + "', '" & 1 & "','" + veduci + "')")
                Me.ZoznamFTableAdapter.Fill(Me.RotekDataSet.ZoznamF)
                ComboBox1.Text = firma
                ComboBox2.Text = veduci
                Vseobecne()
                Exit Sub
            Else
                Exit Sub
            End If
        End If

        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 1, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)

        Try
            If DataGridView4.RowCount <> 0 And TextBox2.ReadOnly = False Then
                Chyby.Show("Už je zadaná taká zákazka")
                Exit Sub
            End If

            If DataGridView4.RowCount = 0 Then
                Dim slova As String = "Zadaná"
                Dim uprava1, uprava2 As Integer
                If povrch_uprava = "Nie" Then
                    uprava1 = 0
                Else
                    uprava1 = 1
                End If

                If tepel_uprava = "Nie" Then
                    uprava2 = 0
                Else
                    uprava2 = 1
                End If

                If String.IsNullOrEmpty(cp) Then
                    SQL_main.Odpalovac("Insert INTO Zakazka (Zakazka, pocet, srot, srotcena, D_plan, D_prijatia, Zaevidoval, Zakaznik, Veduci, Povrch_uprava, Tepel_uprava, Rozprac, Kusov, Razy, Meno, P_CNC, P_REZ, P_ELEKTROD, Vykresy, Vydajxa, Objednavka) VALUES ('" + zakazka + "','" & 1 & "','" & 0 & "','" & 0 & "','" + d_ukoncenia.ToString("yyyy-MM-dd") + "','" + d_prijatia.ToString("yyyy-MM-dd") + "','" + zaevidoval + "','" + firma + "','" + veduci + "','" & uprava1 & "','" & uprava2 & "','" & 0 & "','" & kusov & "','" & 0 & "','" + menozakazky + "','" & 0 & "','" & 0 & "','" & 0 & "','" & 2 & "','" & 1 & "', '" & objednavka & "')")
                Else
                    SQL_main.Odpalovac("Insert INTO Zakazka (Zakazka, pocet, srot, srotcena, D_plan, D_prijatia, Zaevidoval, Zakaznik, Veduci, Povrch_uprava, Tepel_uprava, Rozprac, Kusov, Razy, Meno, P_CNC, P_REZ, P_ELEKTROD, Vykresy, Vydajxa, Objednavka, Cenovka) VALUES ('" + zakazka + "','" & 1 & "','" & 0 & "','" & 0 & "','" + d_ukoncenia.ToString("yyyy-MM-dd") + "','" + d_prijatia.ToString("yyyy-MM-dd") + "','" + zaevidoval + "','" + firma + "','" + veduci + "','" & uprava1 & "','" & uprava2 & "','" & 0 & "','" & kusov & "','" & 0 & "','" + menozakazky + "','" & 0 & "','" & 0 & "','" & 0 & "','" & 1 & "','" & 1 & "', '" & objednavka & "','|" & cp & "')")
                    Try
                        Dim cesta As String = My.Settings.Rotek3 & "CP\" & (Now.Year Mod 2000) & "\" & cp.Replace("/", "•") & ".xls"
                        Dim cesta2 As String = My.Settings.Rotek3
                        cesta2 = cesta2 & "\zakazky\" + zakazka + "\"
                        cesta2 = cesta2 + "Cenova ponuka\"
                        If System.IO.File.Exists(cesta) Then
                            System.IO.Directory.CreateDirectory(cesta2)
                            cesta2 = cesta2 & cp.Replace("/", "•") & ".pdf"
                            PrehladO.tlacDoPdf(cesta)

                            Dim limit As Integer = 0
                            While limit < 3 Or Not IO.File.Exists(cesta2)

                                If IO.File.Exists(cesta.Replace(".xls", ".pdf")) Then
                                    System.IO.File.Copy(cesta.Replace(".xls", ".pdf"), cesta2, True)
                                Else
                                    Threading.Thread.Sleep(1000)
                                End If

                                limit = limit + 1
                            End While

                        End If

                    Catch ex As Exception
                        Chyby.Show(ex.ToString)

                    End Try

                End If

                Chyby.Show("Zákazka úspešne pridaná")
                TextBox2.ReadOnly = True
                DateTimePicker1.Enabled = False
                DateTimePicker2.Enabled = False
                TextBox9.ReadOnly = True
                ComboBox1.Enabled = False
                ComboBox2.Enabled = False
            Else
                If DateTimePicker1.Value.ToShortDateString <> DataGridView4.Rows(0).Cells(5).Value Then
                    SQL_main.Odpalovac("UPDATE Zakazka SET D_plan='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' WHERE pocet=" & 1 & " AND Zakazka='" + zakazka + "'")
                End If
                If TextBox12.Text <> DataGridView4.Rows(0).Cells(20).Value.ToString Or TextBox11.Text <> DataGridView4.Rows(0).Cells(18).Value.ToString Then
                    SQL_main.Odpalovac("UPDATE Zakazka SET Objednavka='" & TextBox12.Text & "', Meno='" & TextBox11.Text & "' WHERE pocet=" & 1 & " AND Zakazka='" + zakazka + "'")
                End If
            End If

            InsertPoznamka(zakazka)

            If String.IsNullOrEmpty(cp) Then
            Else
                Me.CPTableAdapter.Fill(Me.RotekDataSet.CP)
                Me.CPBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' ", RotekDataSet.CP.pocetColumn, 2, RotekDataSet.CP.NazovColumn, cp)
                Dim cena As String
                For i As Integer = 0 To DataGridView7.RowCount - 1
                    podzakazka = DataGridView7.Rows(i).Cells(0).Value.ToString.ToUpper
                    kusov = DataGridView7.Rows(i).Cells(1).Value
                    cena = DataGridView7.Rows(i).Cells(2).Value
                    SQL_main.AddCommand("Insert INTO Zakazka (Zakazka, pocet, srot, srotcena, Povrch_uprava, Tepel_uprava, Podzakazka, Rozprac, Kusov, P_CNC, P_REZ, P_ELEKTROD, Vykresy, Razy, Cena) VALUES ('" + zakazka + "','" & 2 & "','" & 0 & "','" & 0 & "','" & "Nie" & "','" & "Nie" & "','" + podzakazka + "','" & 0 & "','" & kusov & "','" & 0 & "','" & 0 & "','" & 0 & "','" & 1 & "','" & 0 & "','" & cena & "')")
                    System.IO.Directory.CreateDirectory((My.Settings.Rotek3 + "zakazky\" + zakazka + "\Vykresy\" + podzakazka + "\").Replace("/", "\"))
                Next
                aktualizuj_cenu(zakazka)

                Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
                Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)

                For i As Integer = 0 To DataGridView3.RowCount - 1
                    If DataGridView3.Rows(i).Cells(0).Value = TextBox3.Text Then
                        DataGridView3.Rows(i).Selected = True
                    Else
                        DataGridView3.Rows(i).Selected = False
                    End If
                Next
                cp = ""
                Exit Sub

            End If

            If podzakazka.Length = 0 Then Exit Sub

            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
            If DataGridView4.RowCount = 0 Then

                Dim pc As Integer = 0
                Dim pr As Integer = 0
                Dim pe As Integer = 0

                If RadioButton5.Checked = True Then
                    pc = 1
                    SQL_main.Odpalovac("UPDATE Zakazka SET P_CNC='" & 1 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'")

                    Dim cesta2 As String = My.Settings.Rotek3
                    cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Programy\NC\" + podzakazka
                    System.IO.Directory.CreateDirectory(cesta2.Replace("/", "\"))
                End If
                If RadioButton7.Checked = True Then
                    pr = 1
                    SQL_main.Odpalovac("UPDATE Zakazka SET P_REZ='" & 1 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'")

                    Dim cesta2 As String = My.Settings.Rotek3
                    cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Programy\EIR\" + podzakazka
                    System.IO.Directory.CreateDirectory(cesta2.Replace("/", "\"))

                End If
                If RadioButton9.Checked = True Then
                    pe = 1
                    SQL_main.Odpalovac("UPDATE Zakazka SET P_ELEKTROD='" & 1 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'")
                    Dim cesta2 As String = My.Settings.Rotek3
                    cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Programy\Elektroda\" + podzakazka
                    System.IO.Directory.CreateDirectory(cesta2.Replace("/", "\"))
                End If

                Dim cena As String
                Try
                    Dim tmp As Integer = TextBox16.Text
                    cena = TextBox16.Text
                Catch ex As Exception
                    cena = "NULL"
                End Try

                SQL_main.AddCommand("UPDATE Zakazka SET Vykresy='" & 1 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'")

                System.IO.Directory.CreateDirectory((My.Settings.Rotek3 + "zakazky\" + zakazka + "\Vykresy\" + podzakazka + "\").Replace("/", "\"))

                SQL_main.AddCommand("Insert INTO Zakazka (Zakazka, pocet, srot, srotcena, Povrch_uprava, Tepel_uprava, Podzakazka, Rozprac, Kusov, P_CNC, P_REZ, P_ELEKTROD, Vykresy, Razy, Cena) VALUES ('" + zakazka + "','" & 2 & "','" & 0 & "','" & 0 & "','" & povrch_uprava & "','" & tepel_uprava & "','" + podzakazka + "','" & 0 & "','" & kusov & "','" & pc & "','" & pr & "','" & pe & "','" & 1 & "','" & 0 & "', " & cena & ")")
                If (RichTextBox1.Text.Length > 0) Then
                    SQL_main.AddCommand("INSERT INTO ZakazkaPoznamka (Poznamka, Zakazka_ID, Typ) VALUES('" & RichTextBox1.Text & "', (SELECT SCOPE_IDENTITY()) , 'Podzakazka')")
                End If
                SQL_main.Odpal()
                aktualizuj_cenu(zakazka)

                Chyby.Show("Dielec úspešne pridaný")
            ElseIf DataGridView4.RowCount = 1 Then
                Dim pc As Integer = 0
                Dim pr As Integer = 0
                Dim pe As Integer = 0

                If RadioButton5.Checked = True Then
                    pc = 1
                    Dim cesta2 As String = My.Settings.Rotek3
                    cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Programy\NC\" + podzakazka
                    System.IO.Directory.CreateDirectory(cesta2.Replace("/", "\"))
                End If
                If RadioButton7.Checked = True Then
                    pr = 1
                    Dim cesta2 As String = My.Settings.Rotek3
                    cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Programy\EIR\" + podzakazka
                    System.IO.Directory.CreateDirectory(cesta2.Replace("/", "\"))
                End If
                If RadioButton9.Checked = True Then
                    pe = 1
                    Dim cesta2 As String = My.Settings.Rotek3
                    cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Programy\Elektroda\" + podzakazka
                    System.IO.Directory.CreateDirectory(cesta2.Replace("/", "\"))
                End If

                System.IO.Directory.CreateDirectory((My.Settings.Rotek3 + "zakazky\" + zakazka + "\Vykresy\" + podzakazka + "\").Replace("/", "\"))
                Dim cena As String
                Try
                    Dim tmp As Integer = TextBox16.Text
                    cena = TextBox16.Text
                Catch ex As Exception
                    cena = "Cena"
                End Try

                Try
                Catch ex As Exception

                End Try

                SQL_main.AddCommand("UPDATE Zakazka SET Povrch_uprava='" & povrch_uprava & "', Tepel_uprava='" & tepel_uprava & "', P_CNC='" & pc & "', P_REZ='" & pr & "', P_ELEKTROD='" & pe & "', Cena = " & cena & ", Kusov = " & kusov & " WHERE Zakazka='" & zakazka & "' AND pocet=2 AND Podzakazka='" & podzakazka & "'")
                If (RichTextBox1.Text.Length > 0) Then
                    SQL_main.AddCommand("INSERT INTO ZakazkaPoznamka (Poznamka, Zakazka_ID, Typ) VALUES('" & RichTextBox1.Text & "', (SELECT TOP 1 ID FROM ZAKAZKA WHERE Zakazka='" & zakazka & "' AND pocet=2 AND Podzakazka='" & podzakazka & "') , 'Podzakazka')")

                End If
                SQL_main.Odpal()
                Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)

                pc = 0
                pe = 0
                pr = 0

                subory_aktualiyuj()
                aktualizuj_cenu(zakazka)

                Chyby.Show("Dielec úspešne zmenený")
            End If

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

        poznamky(TextBox2.Text, podzakazka)

        RichTextBox1.Text = ""
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)

        For i As Integer = 0 To DataGridView3.RowCount - 1
            If DataGridView3.Rows(i).Cells(0).Value = TextBox3.Text Then
                DataGridView3.Rows(i).Selected = True
            Else
                DataGridView3.Rows(i).Selected = False
            End If
        Next
    End Sub

    Private Sub aktualizuj_cenu(zakazka As String)
        SQL_main.AddCommand("WITH toupdate as ")
        SQL_main.AddCommand("~(")
        SQL_main.AddCommand("~  select sum(cena*Kusov) sum_cena, Zakazka FROM Zakazka ")
        SQL_main.AddCommand("~  WHERE pocet = 2 AND Zakazka = '" & zakazka & "' ")
        SQL_main.AddCommand("~  GROUP BY Zakazka having sum(cena) is not null ")
        SQL_main.AddCommand("~)")
        SQL_main.AddCommand("~UPDATE Zakazka SET cena = toupdate.sum_cena FROM toupdate ")
        SQL_main.AddCommand("~WHERE zakazka.Zakazka = toupdate.Zakazka And zakazka.pocet = 1")

        SQL_main.Commit_Transaction()
    End Sub

    Private Sub InsertPoznamka(Zakazka As String)
        Dim poznamka As String = RichTextBox2.Text
        If poznamka.Length > 0 Then
            SQL_main.Odpalovac("INSERT INTO ZakazkaPoznamka (Zakazka_ID, Poznamka, Typ) VALUES((SELECT TOP 1 ID FROM Zakazka WHERE Zakazka = '" & Zakazka & "' AND pocet = 1),'" & poznamka & "', 'Zakazka')")
        End If
    End Sub

    Private Sub subory_aktualiyuj()
        Dim sql As String
        Dim con As New SqlConnection
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Dim cmd As New SqlCommand
        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.P_CNCColumn, 1)
        If DataGridView4.RowCount = 0 Then
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.P_CNCColumn, 2)
            If DataGridView4.RowCount = 0 Then
                sql = "UPDATE Zakazka SET P_CNC='" & 0 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            Else
                sql = "UPDATE Zakazka SET P_CNC='" & 2 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            End If
        Else
            sql = "UPDATE Zakazka SET P_CNC='" & 1 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()

        End If

        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.P_REZColumn, 1)
        If DataGridView4.RowCount = 0 Then
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.P_REZColumn, 2)
            If DataGridView4.RowCount = 0 Then
                sql = "UPDATE Zakazka SET P_REZ='" & 0 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            Else
                sql = "UPDATE Zakazka SET P_REZ='" & 2 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            End If
        Else
            sql = "UPDATE Zakazka SET P_REZ='" & 1 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
        End If

        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.P_ELEKTRODColumn, 1)
        If DataGridView4.RowCount = 0 Then
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.P_ELEKTRODColumn, 2)
            If DataGridView4.RowCount = 0 Then
                sql = "UPDATE Zakazka SET P_ELEKTROD='" & 0 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            Else
                sql = "UPDATE Zakazka SET P_ELEKTROD='" & 2 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            End If
        Else
            sql = "UPDATE Zakazka SET P_ELEKTROD='" & 1 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()
        End If
        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.VykresyColumn, 1)

        If DataGridView4.RowCount = 0 Then
            sql = "UPDATE Zakazka SET Vykresy='" & 2 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()

        End If
        con.Close()

    End Sub
    Private Sub material()
        'Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)

        zakazka = TextBox2.Text
        If TextBox3.Text.Length = 0 Then
            Chyby.Show("Nezadaný názov dielca")
            Exit Sub
        End If
        podzakazka = TextBox3.Text

        Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.pocetColumn, 1)
        If DataGridView4.RowCount <> 1 Then
            If MsgBox("Zákazka nenájdená. Pridať novú?", vbExclamation + vbYesNo, "Neexistuje") = vbYes Then
                Vseobecne()
                material()
            End If
            Exit Sub
        End If

        Dim druh, nazov As String
        druh = TextBox4.Text
        nazov = TextBox6.Text
        Dim sirka, rozmer, s_rozmer As Integer
        Try
            rozmer = TextBox7.Text
        Catch ex As Exception
            Chyby.Show("Zle zadanô " + Label2.Text)
            Exit Sub
        End Try


        'material
        If druh.Length = 0 Or nazov.Length = 0 Then
            Chyby.Show("Niečo nie je zadané")
            Exit Sub
        End If

        Dim hustota As Double
        Dim velkost As Double

        Try
            velkost = TextBox1.Text
        Catch ex As Exception
            Chyby.Show(" Nezadal si veľkosť")
            Exit Sub
        End Try
        Dim typ As String

        If RadioButton4.Checked = True Then
            typ = "Valec"
            sirka = -1
            s_rozmer = -1
        ElseIf RadioButton3.Checked = True Then
            typ = "Plech"
            s_rozmer = -1
            Try
                sirka = TextBox5.Text
            Catch ex As Exception
                Chyby.Show("Zle zadaná šírka")
                Exit Sub
            End Try

            If sirka < rozmer Then
                Dim pom As Integer = sirka
                sirka = rozmer
                rozmer = pom
                If velkost > sirka Then
                    pom = rozmer
                    rozmer = sirka
                    sirka = velkost
                    velkost = pom
                ElseIf velkost > rozmer Then
                    pom = rozmer
                    rozmer = velkost
                    velkost = pom
                End If
            ElseIf sirka < velkost Then
                Dim pom As Integer = rozmer
                rozmer = sirka
                sirka = velkost
                velkost = pom
            End If
        ElseIf RadioButton1.Checked = True Then
            typ = "U - profil"
            Try
                sirka = TextBox5.Text
            Catch ex As Exception
                Chyby.Show("Zle zadaná šírka")
                Exit Sub
            End Try
            Try
                s_rozmer = TextBox15.Text
            Catch ex As Exception
                Chyby.Show("Zle zadaná šírka")
                Exit Sub
            End Try

            If s_rozmer * 2 - sirka > 0 Then
                Chyby.Show("Nefunkcne hodnoty")
                Exit Sub
            End If
        ElseIf RadioButton2.Checked = True Then
            typ = "L - profil"
            Try
                sirka = TextBox5.Text
            Catch ex As Exception
                Chyby.Show("Zle zadaná šírka")
                Exit Sub
            End Try
            Try
                s_rozmer = TextBox15.Text
            Catch ex As Exception
                Chyby.Show("Zle zadany s-rozmer")
                Exit Sub
            End Try

            If rozmer > sirka Then
                Dim pom As Integer = sirka
                sirka = rozmer
                rozmer = pom
            End If

        ElseIf RadioButton11.Checked = True Then
            typ = "6 - hran"
            sirka = -1
            s_rozmer = -1
        ElseIf RadioButton12.Checked = True Then
            typ = "Rurka"
            s_rozmer = -1
            Try
                sirka = TextBox5.Text
            Catch ex As Exception
                Chyby.Show("Zle zadaná šírka")
                Exit Sub
            End Try

            If sirka > rozmer Then
                Dim t As Integer = rozmer
                rozmer = sirka
                sirka = t
            End If

        Else
            typ = "Jokelt"
            Try
                sirka = TextBox5.Text
            Catch ex As Exception
                Chyby.Show("Zle zadaná šírka")
                Exit Sub
            End Try
            Try
                s_rozmer = TextBox15.Text
            Catch ex As Exception
                Chyby.Show("Zle zadany s-rozmer")
                Exit Sub
            End Try
            If rozmer > sirka Then
                Dim pom As Integer = sirka
                sirka = rozmer
                rozmer = pom
            End If
        End If

        Dim kusov As Integer

        If podzakazka.Length <> 0 Then
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
            If DataGridView4.RowCount = 0 Then
                Vseobecne()
            Else
            End If
        End If

        Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
        kusov = DataGridView3.Rows(0).Cells(1).Value

        Try
            Dim jdsfb As Double = Huta_SQL.objem(rozmer, velkost, sirka, s_rozmer, typ)
        Catch ex As Exception
            Chyby.Show("Problem s objemom")
            Chyby.Show(ex.ToString)
            Exit Sub
        End Try


        Dim con As New SqlConnection
        Dim sql As String
        con.ConnectionString = My.Settings.Rotek2
        con.Open()
        Dim cmd As New SqlCommand

        'Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' ", RotekDataSet.Huta.DruhColumn, druh, RotekDataSet.Huta.NazovColumn, nazov, RotekDataSet.Huta.pocetColumn, 0)
        'sdfsd

        Try
            'If DataGridView2.RowCount > 0 Then
            '    Dim pocet As Integer
            '    If podzakazka.Length <> 0 Then pocet = 3 Else pocet = 1
            '    sql = "DELETE FROM Huta WHERE Druh='" & druh & "' AND Nazov='" & nazov & "' AND Sirka=" & sirka & " AND Velkost=" & velkost & " AND Rozmer=" & rozmer & " AND zakazka='" & zakazka & "' AND pocet='" & pocet & "' AND S_rozmer=" & s_rozmer
            '    If TextBox3.Text <> "" Then sql = sql & " AND Vaha='" & podzakazka & "' "
            '    cmd = New SqlCommand(sql, con)
            '    cmd.ExecuteNonQuery()

            '    sql = "Insert INTO Huta (Druh,  Nazov, Hustota, Sirka, Rozmer, Velkost, zakazka, pocet, srot, srotcena, Vaha, Kusov, Typ, S_rozmer) VALUES ('" + druh + "', '" + nazov + "','" & hustota & "'," & sirka & "," & rozmer & "," & velkost & ",'" + zakazka + "','" & pocet & "','" & 0 & "','" & 0 & "','" + podzakazka + "'," & kusov & ",'" + typ + "'," & s_rozmer & ")"
            '    cmd = New SqlCommand(sql, con)
            '    cmd.ExecuteNonQuery()

            '    sql = "UPDATE Zakazka SET Vydajxa='" & 0 & "' WHERE pocet=" & 1 & " AND Zakazka='" + zakazka + "'"
            '    cmd = New SqlCommand(sql, con)
            '    cmd.ExecuteNonQuery()

            'Else
            '    If MsgBox("Žiadne informácie o tomto materiáli. Chcete pridať nový typ materiálu?", vbQuestion + vbYesNo, "Neexistuje") = vbYes Then
            '        sql = "Insert INTO Huta (Druh,  Nazov, Sirka, Rozmer, Velkost, pocet, srot, srotcena, Cena, Hustota, Vaha, Kusov, Typ) VALUES ('" + druh + "', '" + nazov + "'," & 0 & "," & 0 & "," & 0 & ",'" & 0 & "','" & 0 & "','" & 0 & "','" & 0 & "','" & 0 & "','" & 0 & "'," & 0 & ",'" + typ + "')"
            '        cmd = New SqlCommand(sql, con)
            '        cmd.ExecuteNonQuery()
            '        con.Close()
            '        material()
            '        Exit Sub

            '    Else
            '        Exit Sub
            '    End If
            'End If


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
        con.Close()

        'Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)

        'Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4}='{5}'", RotekDataSet.Huta.pocetColumn, 3, RotekDataSet.Huta.zakazkaColumn, zakazka, RotekDataSet.Huta.VahaColumn, podzakazka)

        Me.ZakazkaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka)

        TextBox4.Text = ""
        TextBox6.Text = ""
        If TextBox7.Visible Then
            TextBox7.Text = ""
        End If
        If TextBox1.Visible Then
            TextBox1.Text = ""
        End If
        If TextBox5.Visible Then
            TextBox5.Text = ""
        End If
        If TextBox15.Visible Then
            TextBox15.Text = ""
        End If

        For i As Integer = 0 To DataGridView3.RowCount - 1
            If DataGridView3.Rows(i).Cells(0).Value = TextBox3.Text Then
                DataGridView3.Rows(i).Selected = True
            Else
                DataGridView3.Rows(i).Selected = False
            End If
        Next

        hladaj2(ListBox1, 0, "Druh")
        hladaj2(ListBox3, 2, "Nazov")
        'hladaj2(ListBox4, 4, "Rozmer")
        TextBox4.Focus()

    End Sub

    Private Sub konstrukcia()
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
        Try

            zakazka = TextBox2.Text
            If TextBox3.Text.Length = 0 Then
                Chyby.Show("Nezadaný názov dielca")
                Exit Sub
            End If
            podzakazka = TextBox3.Text

            Dim konstrukter As String = TextBox8.Text

            If podzakazka.Length <> 0 Then
                Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.pocetColumn, 2)
            Else
                Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.pocetColumn, 1)
            End If

            If DataGridView4.RowCount <> 1 Then
                If MsgBox("Zákazka alebo dielec nenájdené. Pridať novú?", vbExclamation + vbYesNo, "Neexistuje") = vbYes Then
                    Vseobecne()
                    konstrukcia()
                End If
                Exit Sub
            End If
            Dim con As New SqlConnection
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            Dim cmd As New SqlCommand
            Dim sql As String

            If ListBox5.Items.Count <> 0 Then

                Dim ss As List(Of String) = New List(Of String)
                Dim kontrl As Boolean = False

                If zoznam_vykresov2.Count > 0 Then
                    'Chyby.Show("")
                    con.Close()
                    GroupBox6.Show()
                    Exit Sub
                End If

                For Each s As String In ListBox5.Items
                    If s.IndexOf(".") < 0 Then
                        sql = "UPDATE Zakazka SET Vykresy='" & 2 & "' WHERE pocet=" & 2 & " AND zakazka='" + zakazka + "' AND Podzakazka='" + podzakazka + "'"
                        cmd = New SqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                        ss.Add(s)
                        Dim cesta2 As String = My.Settings.Rotek3
                        cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Vykresy\" + podzakazka + "\" + s
                        System.IO.File.CreateText(cesta2.Replace("/", "\"))
                        kontrl = True
                    End If
                Next
                For Each sx As String In ss
                    ListBox5.Items.Remove(sx)
                Next

                ' konstrukcia()
                'Exit Sub
                If kontrl Then
                    Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}<>'{7}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.VykresyColumn, 1, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
                Else
                    Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.VykresyColumn, 1)
                End If

                If DataGridView4.RowCount = 0 Then
                    sql = "UPDATE Zakazka SET Vykresy='" & 2 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End If

            End If
            If ListBox6.Items.Count <> 0 Then

                For Each s As String In zoznam_NC2
                    Dim cesta2 As String = My.Settings.Rotek3
                    cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Programy\NC\" + podzakazka + "\" + s.Substring(s.LastIndexOf("\") + 1)
                    My.Computer.FileSystem.CopyFile(s, cesta2, True)
                Next

                If String.IsNullOrEmpty(podzakazka) Then
                Else
                    sql = "UPDATE Zakazka SET P_CNC='" & 2 & "' WHERE pocet=" & 2 & " AND zakazka='" + zakazka + "' AND Podzakazka='" + podzakazka + "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End If

                'con.Close()
                'Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
                'con.Open()

                Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}<>'{7}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.P_CNCColumn, 1, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
                If DataGridView4.RowCount = 0 Then
                    sql = "UPDATE Zakazka SET P_CNC='" & 2 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End If

                'zoznam_NC = zoznam_NC + "|" + s

            End If
            If ListBox7.Items.Count <> 0 Then

                For Each s As String In zoznam_EIR2
                    Dim cesta2 As String = My.Settings.Rotek3
                    cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Programy\EIR\" + podzakazka + "\" + s.Substring(s.LastIndexOf("\") + 1)
                    My.Computer.FileSystem.CopyFile(s, cesta2, True)
                    'zoznam_EIR = zoznam_EIR + "|" + s
                Next

                If String.IsNullOrEmpty(podzakazka) Then
                Else
                    sql = "UPDATE Zakazka SET P_REZ='" & 2 & "' WHERE pocet=" & 2 & " AND zakazka='" + zakazka + "' AND Podzakazka='" + podzakazka + "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End If

                Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}<>'{7}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.P_REZColumn, 1, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
                If DataGridView4.RowCount = 0 Then
                    sql = "UPDATE Zakazka SET P_REZ='" & 2 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End If

            End If
            If ListBox8.Items.Count <> 0 Then

                For Each s As String In zoznam_EL2
                    Dim cesta2 As String = My.Settings.Rotek3
                    cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Programy\Elektroda\" + podzakazka + "\" + s.Substring(s.LastIndexOf("\") + 1)
                    My.Computer.FileSystem.CopyFile(s, cesta2, True)
                    'zoznam_EL = zoznam_EL + "|" + s
                Next

                If String.IsNullOrEmpty(podzakazka) Then
                Else
                    sql = "UPDATE Zakazka SET P_Elektrod='" & 2 & "' WHERE pocet=" & 2 & " AND zakazka='" + zakazka + "' AND Podzakazka='" + podzakazka + "'"
                    cmd = New SqlCommand(sql, con)
                    cmd.ExecuteNonQuery()

                    Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}' AND {6}<>'{7}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.P_ELEKTRODColumn, 1, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
                    If DataGridView4.RowCount = 0 Then
                        sql = "UPDATE Zakazka SET P_ELEKTROD='" & 2 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                        cmd = New SqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                    End If
                End If

            End If
            If ListBox9.Items.Count <> 0 Then
                For Each s As String In zoznam_ine2
                    Dim cesta2 As String = My.Settings.Rotek3
                    cesta2 = cesta2 & "\zakazky\" + zakazka + "\" + "Ine\" + podzakazka + "\" + s.Substring(s.LastIndexOf("\") + 1)
                    My.Computer.FileSystem.CopyFile(s, cesta2, True)
                    'zoznam_ine = zoznam_ine + "|" + s
                Next
            End If

            Dim pocet As Integer

            If podzakazka.Length <> 0 Then
                pocet = 2
            Else
                pocet = 1
            End If

            'sql = "UPDATE Zakazka SET Vykresy='" + zoznam_vykresov + "', P_CNC='" + zoznam_NC + "', P_REZ='" + zoznam_EIR + "', P_ELEKTROD='" + zoznam_EL + "', Subory='" + zoznam_ine + "' WHERE pocet=" & pocet & " AND zakazka='" + zakazka + "'"
            'If podzakazka.Length <> 0 Then
            '    sql = sql + " AND Podzakazka='" + podzakazka + "'"
            'End If
            'cmd = New SqlCommand(sql, con)
            'cmd.ExecuteNonQuery()

            If konstrukter.Length <> 0 Then
                sql = "UPDATE Zakazka SET Zodp_Konstrukcie='" + konstrukter + "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            End If

            Dim b As Date = New Date
            If (ku) <> b Then
                sql = "UPDATE Zakazka SET K_Ukoncenia='" + ku.ToString("yyyy-MM-dd") + "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            End If

            con.Close()
            Chyby.Show("Subory pridane")
            ListBox5.Items.Clear()
            ListBox6.Items.Clear()
            ListBox7.Items.Clear()
            ListBox8.Items.Clear()
            ListBox9.Items.Clear()

            zoznam_EIR2 = New List(Of String)
            zoznam_EL2 = New List(Of String)
            zoznam_ine2 = New List(Of String)
            zoznam_NC2 = New List(Of String)
            zoznam_vykresov2 = New List(Of String)

            For i As Integer = 0 To DataGridView3.RowCount - 1
                If DataGridView3.Rows(i).Cells(0).Value = TextBox3.Text Then
                    DataGridView3.Rows(i).Selected = True
                Else
                    DataGridView3.Rows(i).Selected = False
                End If
            Next

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Public Sub stuk()

        Try
            Vseobecne()
            material()
            konstrukcia()
        Catch ex As SystemException
            Chyby.Show(ex.ToString)
            Exit Sub
        End Try

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        stuk()
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then material()
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then stuk()
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then stuk()
    End Sub
    Private Sub nastrsklad_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Private Sub TextBox4_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox1, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                'Me.HutaBindingSource.Sort = "Druh"
                hladaj2(ListBox1, 0, "Druh")
            End If

        Catch ex As Exception


        End Try

    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            Dim tex As String
            j = 1
            tex = ListBox1.Text
            TextBox4.Text = tex
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)
            'TextBox4.SelectionStart = TextBox4.Text.Length
        Catch ex As Exception
        End Try

    End Sub
    Private Sub hladaj2(ByVal listb As ListBox, ByVal stlp As Integer, ByVal triedenie As String)
        Try

            Dim b As Integer
            Dim a, aaa, opak As String
            a = TextBox4.Text
            aaa = TextBox6.Text
            'aaaa = TextBox7.Text
            opak = ""
            '   Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
            'Me.HutaBindingSource.Sort = triedenie
            'Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%'", RotekDataSet.Huta.pocetColumn, 0, RotekDataSet.Huta.DruhColumn, a, RotekDataSet.Huta.NazovColumn, aaa)

            listb.Items.Clear()

            'For b = 0 To DataGridView2.RowCount - 1
            '    If opak.ToLower.Trim <> DataGridView2.Rows(b).Cells(stlp).Value.ToString.ToLower.Trim Then
            '        listb.Items.Add(DataGridView2.Rows(b).Cells(stlp).Value)
            '        opak = DataGridView2.Rows(b).Cells(stlp).Value
            '    End If

            'Next

            If listb.Items.Count = 1 Then
                Dim listbs As List(Of ListBox) = New List(Of ListBox)
                Dim aree As List(Of String) = New List(Of String)
                Dim stlpc() As Integer = {0, 2, 4}


                listbs.Add(ListBox1)
                listbs.Add(ListBox3)
                'listbs.Add(ListBox4)

                aree.Add("Druh")

                aree.Add("Nazov")
                'aree.Add("Rozmer")

                'Hpridat.hladaj3(listbs, aree, stlpc, DataGridView2, HutaBindingSource)
            End If
        Catch ex As Exception
            Chyby.Show("Kks nejaká trápna chyba")
        End Try
    End Sub


    Private Sub TextBox6_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        Try
            Hpridat.stlac(k, sender, ListBox3, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Up Then

                j = 1
                TextBox6.Text = (ListBox3.Items(k).ToString)
                k = k - 1
                TextBox6.Select(0, TextBox6.Text.Length)
                TextBox6.SelectionStart = TextBox6.Text.Length
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj2(ListBox3, 2, "Nazov")
                TextBox6.Select(TextBox6.Text.Length, TextBox6.Text.Length)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox3.SelectedIndexChanged
        Try

            j = 1
            TextBox6.Text = ListBox3.Text
            TextBox6.Focus()
            TextBox6.Select(0, TextBox6.Text.Length)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Enter
        Me.Text = ""
        hladaj2(ListBox1, 0, "Druh")
    End Sub

    Private Sub TextBox6_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.Enter
        Dim pom As String
        pom = TextBox4.Text

        'Me.HutaBindingSource.Filter = Nothing
        'Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        'Me.HutaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Huta.DruhColumn, pom, RotekDataSet.Huta.pocetColumn, 0)
        'Dim x As Integer = DataGridView2.RowCount
        'If x = 1 Then
        '    menoo = DataGridView2.Rows(0).Cells(2).Value
        '    TextBox6.Text = menoo
        'End If
        'Me.HutaBindingSource.Filter = Nothing
        'Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        'Me.HutaBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Huta.pocetColumn, 0)

    End Sub


    Private Sub TextBox7_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyUp
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                material()
            End If
        Catch ex As Exception
        End Try
    End Sub



    Private Sub TextBox2_KeyUp_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            Vseobecne()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick, DataGridView1.CellClick

        Dim kks As Integer = e.RowIndex
        If kks < 0 Then Exit Sub

        Dim typ As String = DataGridView1.Rows(kks).Cells(7).Value

        If typ = "Valec" Then
            RadioButton4.Checked = True
            TextBox15.Visible = False
            TextBox5.Visible = False
            Label2.Hide()
            Label24.Hide()
            Label4.Text = "Priemer [mm]"
        ElseIf typ = "Plech" Then
            RadioButton3.Checked = True
            TextBox5.Visible = True
            TextBox15.Visible = False
            Label2.Show()
            Label24.Hide()
            Label4.Text = "Výška [mm]"
            Label2.Text = "Šírka [mm]"
        ElseIf typ = "Rurka" Then
            RadioButton12.Checked = True
            TextBox5.Visible = True
            TextBox15.Visible = False
            Label2.Show()
            Label24.Hide()
            Label2.Text = "Vnutorny priemer (d) [mm]"
            Label4.Text = "Priemer (D) [mm]"
        ElseIf typ = "6 - hran" Then
            RadioButton11.Checked = True
            TextBox15.Visible = False
            TextBox5.Visible = False
            Label2.Hide()
            Label24.Hide()
            Label4.Text = "Priemer [mm]"
        ElseIf typ = "L - profil" Then
            RadioButton2.Checked = True
            TextBox5.Visible = True
            TextBox15.Visible = True
            Label2.Show()
            Label4.Text = "b [mm]"
            Label2.Text = "a [mm]"
            Label24.Show()
        ElseIf typ = "U - profil" Then
            RadioButton1.Checked = True
            TextBox5.Visible = True
            TextBox15.Visible = True
            Label2.Show()
            Label4.Text = "b [mm]"
            Label2.Text = "a [mm]"
            Label24.Show()
        ElseIf typ = "Jokelt" Then
            RadioButton13.Checked = True
            TextBox5.Visible = True
            TextBox15.Visible = True
            Label2.Show()
            Label4.Text = "b [mm]"
            Label2.Text = "a [mm]"
            Label24.Show()

        End If

        TextBox4.Text = DataGridView1.Rows(kks).Cells(0).Value
        TextBox6.Text = DataGridView1.Rows(kks).Cells(1).Value
        TextBox5.Text = DataGridView1.Rows(kks).Cells(2).Value
        TextBox7.Text = DataGridView1.Rows(kks).Cells(3).Value
        TextBox15.Text = DataGridView1.Rows(kks).Cells(4).Value
        TextBox1.Text = DataGridView1.Rows(kks).Cells(5).Value
        TextBox3.Text = Label10.Text



        TextBox5.Text = DataGridView1.Rows(kks).Cells(2).Value
        If TextBox3.Text = "Hlavna" Then TextBox3.Text = ""
        TextBox1.Focus()

    End Sub

    Private Sub DataGridView3_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        Try

            Dim kks As Integer
            kks = e.RowIndex
            If kks = -1 Then
                Exit Sub
            Else
                Dim podzakazka As String = DataGridView3.Rows(kks).Cells(0).Value
                Label10.Text = podzakazka
                TextBox3.Text = podzakazka
                'Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Huta.pocetColumn, 3, RotekDataSet.Huta.zakazkaColumn, zakazka, RotekDataSet.Huta.VahaColumn, podzakazka)
                Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.ZakazkaColumn, TextBox2.Text, RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)
                poznamky(TextBox2.Text, podzakazka)
            End If
            TextBox10.Text = DataGridView4.Rows(0).Cells(16).Value
            '678
            If DataGridView4.Rows(0).Cells(6).Value() = 1 Then
                RadioButton5.Checked = True
            Else
                RadioButton6.Checked = True
            End If
            If DataGridView4.Rows(0).Cells(7).Value() = 1 Then
                RadioButton7.Checked = True
            Else
                RadioButton8.Checked = True
            End If
            If DataGridView4.Rows(0).Cells(8).Value() = 1 Then
                RadioButton9.Checked = True
            Else
                RadioButton10.Checked = True
            End If
            TextBox13.Text = DataGridView4.Rows(0).Cells(12).Value()
            TextBox14.Text = DataGridView4.Rows(0).Cells(19).Value()

            TextBox16.Text = DataGridView4.Rows(0).Cells(21).Value().ToString
            If TextBox16.Text.Length > 0 Then
                TextBox17.Text = TextBox16.Text * TextBox10.Text
            End If


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Public Sub poznamky(zakazka As String, podzkazka As String)
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT zp.Datum Datum, zp.Poznamka Poznamka, zp.Typ Typ, z.Podzakazka Podzakazka FROM ZakazkaPoznamka zp JOIN Zakazka z ON z.ID = zp.Zakazka_ID WHERE z.Zakazka = '" & zakazka & "' AND z.Podzakazka ='" & podzkazka & "' ORDER BY Podzakazka , Datum DESC", dd)
        MultiLineListBox1.Items.Clear()
        For i As Integer = 0 To dd.Rows.Count - 1
            Dim datum As Date = dd.Rows(i).Item("Datum")
            MultiLineListBox1.Items.Add(datum.ToShortDateString & " - " & dd.Rows(i).Item("Poznamka").ToString)
        Next
    End Sub
    Public Sub zmena()
        Me.ZoznamFBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.ZoznamF.pocetColumn, 1, RotekDataSet.ZoznamF.NazovColumn, ComboBox1.Text)
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        zmena()
    End Sub


    Public Sub dialog(ByRef s As List(Of String), ByRef l As ListBox, ByVal nazov As String)

        Dim openFileDialog1 As New OpenFileDialog
        openFileDialog1.Title = nazov
        openFileDialog1.InitialDirectory = "\\192.168.1.150"
        openFileDialog1.Filter = "Všetky súbory(*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Multiselect = True

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            For Each sx As String In openFileDialog1.FileNames
                s.Add(sx)
                l.Items.Add(sx.Substring(sx.LastIndexOf("\") + 1))
            Next
        End If
    End Sub




    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        dialog(zoznam_vykresov2, ListBox5, "Vybrať výkresy")
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Vseobecne()
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        material()
    End Sub

    Private Sub TextBox5_Leave(sender As System.Object, e As System.EventArgs)
        hladaj2(ListBox3, 2, "Nazov")
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        konstrukcia()
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        dialog(zoznam_NC2, ListBox6, "Vybrať programy NC")
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        dialog(zoznam_EIR2, ListBox7, "Vybrať programy EIR")

    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        dialog(zoznam_EL2, ListBox8, "Vybrať programy Elektrody")

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        dialog(zoznam_ine2, ListBox9, "Vybrať zvyšné súbory")

    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As System.Object, e As System.EventArgs) Handles DateTimePicker3.ValueChanged
        ku = DateTimePicker3.Value

    End Sub


    Private Sub TextBox8_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyUp
        If e.KeyCode = Keys.Enter Then
            konstrukcia()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub DateTimePicker3_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker3.KeyUp
        If e.KeyCode = Keys.Enter Then
            konstrukcia()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub TextBox10_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyUp
        If e.KeyCode = Keys.Enter Then
            Vseobecne()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
            Try
                TextBox17.Text = TextBox16.Text * TextBox10.Text
            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Sub TextBox3_KeyUp_1(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp, TextBox14.KeyUp, TextBox13.KeyUp
        If e.KeyCode = Keys.Enter Then
            Vseobecne()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub TextBox9_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyUp
        If e.KeyCode = Keys.Enter Then
            Vseobecne()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub ComboBox2_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles ComboBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            Vseobecne()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub ComboBox1_KeyUp_1(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub TextBox4_Click(sender As System.Object, e As System.EventArgs) Handles TextBox4.Click
        TextBox4.Text = ""
    End Sub

    Private Sub TextBox5_Click(sender As System.Object, e As System.EventArgs)
    End Sub

    Private Sub TextBox6_Click(sender As System.Object, e As System.EventArgs) Handles TextBox6.Click
        TextBox6.Text = ""
    End Sub

    Private Sub TextBox7_Click(sender As System.Object, e As System.EventArgs) Handles TextBox7.Click
        TextBox7.Text = ""
    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        Dim slovo As String = InputBox("Napis identifikaciu výkresu", "Pridať výkres")
        If String.IsNullOrEmpty(slovo) Then  Else ListBox5.Items.Add(slovo.Replace(".", "•"))

    End Sub

    Private Sub TextBox3_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox3.Leave
        TextBox3.Text = TextBox3.Text.Replace("/", "•")
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            TextBox5.Text = "-1"
            TextBox15.Text = "-1"
            TextBox15.Visible = False
            TextBox5.Visible = False
            Label2.Hide()
            Label24.Hide()
            Label4.Text = "Priemer [mm]"

        End If
    End Sub



    Private Sub Button12_Click(sender As System.Object, e As System.EventArgs) Handles Button12.Click
        Dim sql As String
        Dim kusov As Integer
        Try
            kusov = TextBox10.Text
        Catch ex As Exception
            Chyby.Show("Zle zadaný počet kusov")
        End Try
        Dim podzakazka As String = TextBox3.Text
        If podzakazka.Length = 0 Then podzakazka = "HLAVNA"

        sql = "UPDATE Zakazka SET Kusov=" & kusov & " WHERE pocet=" & 2 & " AND Zakazka='" + zakazka + "' AND Podzakazka='" + podzakazka + "'"
        Form78.sqa(sql)
        sql = "UPDATE Huta SET Kusov=" & kusov & " WHERE pocet='" & 3 & "' AND zakazka='" + zakazka + "' AND Vaha='" + podzakazka + "'"
        Form78.sqa(sql)

        'Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
        Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)

        'Me.HutaBindingSource1.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4}='{5}'", RotekDataSet.Huta.pocetColumn, 3, RotekDataSet.Huta.zakazkaColumn, zakazka, RotekDataSet.Huta.VahaColumn, podzakazka)

        For i As Integer = 0 To DataGridView3.RowCount - 1
            If DataGridView3.Rows(i).Cells(0).Value = TextBox3.Text Then
                DataGridView3.Rows(i).Selected = True
            Else
                DataGridView3.Rows(i).Selected = False
            End If
        Next
    End Sub

    Private Sub vkres(ByRef dielce As List(Of String))
        Try

            Dim cesta2, cesta3 As String
            If dielce.Count <> 0 Then

                For Each s As String In zoznam_vykresov2
                    cesta2 = My.Settings.Rotek3 & "zakazky\" & zakazka & "\" + "Vykresy\" & s.Substring(s.LastIndexOf("\") + 1) '+ podzakazka + "\" + s.Substring(s.LastIndexOf("\") + 1)
                    My.Computer.FileSystem.CopyFile(s, cesta2, True)

                Next

            End If

            Dim con As New SqlConnection
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            Dim cmd As New SqlCommand
            Dim sql As String

            For Each podzakazka As String In dielce
                For Each s As String In zoznam_vykresov2
                    cesta2 = My.Settings.Rotek3 & "zakazky\" & zakazka & "\" + "Vykresy\" & s.Substring(s.LastIndexOf("\") + 1)
                    cesta3 = My.Settings.Rotek3 & "zakazky\" & zakazka & "\" + "Vykresy\" & podzakazka
                    Create_ShortCut(cesta2, cesta3, s.Substring(s.LastIndexOf("\") + 1))
                    '  MessageBox.Show(cesta2 & "  " & cesta3 & " " & s.Substring(s.LastIndexOf("\") + 1))
                Next

                'zoznam_vykresov = zoznam_vykresov + "|" + s

                sql = "UPDATE Zakazka SET Vykresy='" & 2 & "' WHERE pocet=" & 2 & " AND zakazka='" + zakazka + "' AND Podzakazka='" + podzakazka + "'"
                cmd = New SqlCommand(sql, con)

                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Chyby.Show(sql + vbNewLine + ex.ToString)
                End Try

            Next
            con.Close()

            zoznam_vykresov2 = New List(Of String)
            konstrukcia()

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Sub Create_ShortCut(ByVal TargetPath As String, ByVal ShortCutPath As String, ByVal ShortCutname As String)
        Dim WSHShell = CreateObject("WScript.Shell")
        Dim MyShortcut, DesktopPath

        ' Read desktop path using WshSpecialFolders object
        DesktopPath = ShortCutPath

        ' Create a shortcut object on the desktop
        If System.IO.Directory.Exists(DesktopPath) Then
        Else
            System.IO.Directory.CreateDirectory(DesktopPath)
        End If

        MyShortcut = _
           WSHShell.CreateShortcut(DesktopPath & "\" & ShortCutname.Substring(0, ShortCutname.LastIndexOf(".")) & ".lnk")
        ' Set shortcut object properties and save it

        MyShortcut.TargetPath = _
            WSHShell.ExpandEnvironmentStrings(TargetPath)
        MyShortcut.WorkingDirectory = _
           WSHShell.ExpandEnvironmentStrings("%windir%")
        MyShortcut.WindowStyle = 4
        Try
            'MyShortcut.IconLocation = WSHShell.ExpandEnvironmentStrings(strPath, 0)
            MyShortcut.Save()

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try


    End Sub

    Private Sub Button13_Click_1(sender As System.Object, e As System.EventArgs) Handles Button13.Click
        Dim dielce As List(Of String) = New List(Of String)
        For i As Integer = 0 To DataGridView6.RowCount - 1
            dielce.Add(DataGridView6.Rows(i).Cells(1).Value)
        Next
        vkres(dielce)
        GroupBox6.Hide()

    End Sub

    Private Sub Button15_Click(sender As System.Object, e As System.EventArgs) Handles Button15.Click
        Dim dielce As List(Of String) = New List(Of String)
        For i As Integer = 0 To DataGridView6.RowCount - 1
            If (DataGridView6.Rows(i).Cells(0).Value) = 1 Then
                dielce.Add(DataGridView6.Rows(i).Cells(1).Value)
            End If
        Next
        vkres(dielce)
        GroupBox6.Hide()

    End Sub

    Private Sub Button14_Click(sender As System.Object, e As System.EventArgs) Handles Button14.Click
        Dim dielce As List(Of String) = New List(Of String)
        Dim s As String = TextBox3.Text
        If s.Length < 1 Then s = "HLAVNA"
        dielce.Add(s)
        vkres(dielce)
        GroupBox6.Hide()
    End Sub

    Private Sub DataGridView6_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView6.CellContentClick
        If e.ColumnIndex = 0 And e.RowIndex > -1 Then
            DataGridView6.Rows(e.RowIndex).Cells(0).Value = 1 - (DataGridView6.Rows(e.RowIndex).Cells(0).Value)
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text.IndexOf("x") > -1 Then
            TextBox5.Text = TextBox5.Text.Replace("x", "")
            TextBox7.Focus()
        End If
        Try
            If RadioButton12.Checked = True Then TextBox15.Text = (TextBox7.Text - TextBox5.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox7_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox7.TextChanged
        If TextBox7.Text.IndexOf("x") > -1 Then
            TextBox7.Text = TextBox7.Text.Replace("x", "")
            TextBox1.Focus()
        End If
        Try
            If RadioButton12.Checked = True Then TextBox15.Text = (TextBox7.Text - TextBox5.Text)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub TextBox2_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox2.Leave
        Try
            Dim s As String = TextBox2.Text
            If s.IndexOf("/" & Year(Now).ToString.Substring(2)) > -1 Then
                Dim x As Integer = s.Substring(0, s.IndexOf("/" & Year(Now).ToString.Substring(2)))
                TextBox2.Text = Format(x, "0000") & "/" & Year(Now).ToString.Substring(2)
            End If
            '   MessageBox.Show(s & "  " & s.IndexOf("/" & Year(Now)) & " " & ("/" & Year(Now)))
        Catch ex As Exception
            'Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button16_Click(sender As System.Object, e As System.EventArgs) Handles Button16.Click
        TextBox8.Text = Form78.uzivatel
    End Sub


    Private Sub DataGridView3_CellMouseUp(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView3.CellMouseUp
        If e.Button = MouseButtons.Right Then
            If e.Button = MouseButtons.Right Then
                '  If Form78.heslo = Form78.admin Then
                ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
                ContextMenuStrip1.Tag = e.RowIndex
                ' End If
            End If
        End If
    End Sub

    Private Sub PremenovaťToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PremenovaťToolStripMenuItem.Click
        Dim podzakazka2 As String = TextBox3.Text
        If ContextMenuStrip1.Tag > -1 Then
            Dim podzakazka1 As String = DataGridView3.Rows(ContextMenuStrip1.Tag).Cells(0).Value
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.PodzakazkaColumn, podzakazka)

            If DataGridView4.RowCount = 0 Then
            Else
                Chyby.Show("Už existuje")
                Exit Sub
            End If

            Try
                premenuj(podzakazka1, podzakazka2)
            Catch ex As Exception
                Chyby.Show("Priečinok sa používa. Nemožno zmazať")
                Exit Sub
            End Try

            Dim sql As String = "UPDATE Zakazka SET Podzakazka='" & podzakazka2 & "' WHERE pocet=2 AND Zakazka='" & TextBox2.Text & "' AND Podzakazka='" & podzakazka1 & "'"
            Form78.sqa(sql)
            sql = "UPDATE Huta SET Vaha='" & podzakazka2 & "' WHERE pocet='3' AND Zakazka='" & TextBox2.Text & "' AND Vaha='" & podzakazka1 & "'"
            Form78.sqa(sql)

            'Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)

            For i As Integer = 0 To DataGridView3.RowCount - 1
                If DataGridView3.Rows(i).Cells(0).Value = TextBox3.Text Then
                    DataGridView3.Rows(i).Selected = True
                Else
                    DataGridView3.Rows(i).Selected = False
                End If
            Next
        End If
    End Sub
    Private Sub premenuj(ByVal podzakazka1 As String, ByVal podzakazka2 As String)
        Dim DirList As New ArrayList
        Dim cesta As String = My.Settings.Rotek3 & "zakazky\" & TextBox2.Text.Replace("/", "\") & "\"
        Dim cesta2 As String

        GetDirectories(cesta, DirList)


        For Each item In DirList
            ' MessageBox.Show(item)
            'add item to listbox or text etc here

            If item.ToString.IndexOf(podzakazka1) > cesta.Length - 2 Then
                cesta2 = item.ToString.Replace(podzakazka1, podzakazka2) & "\"
                FileIO.FileSystem.RenameDirectory(item, podzakazka2)
            End If
        Next

    End Sub

    Private Sub GetDirectories(ByVal StartPath As String, ByRef DirectoryList As ArrayList)
        Dim Dirs() As String = System.IO.Directory.GetDirectories(StartPath)
        DirectoryList.AddRange(Dirs)

        For Each Dir As String In Dirs
            GetDirectories(Dir, DirectoryList)
        Next
    End Sub

    Private Sub ZmazaťToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ZmazaťToolStripMenuItem.Click
        If ContextMenuStrip1.Tag > -1 Then
            Dim podzakazka1 As String = DataGridView3.Rows(ContextMenuStrip1.Tag).Cells(0).Value
            Try
                zmazuj(podzakazka1)
            Catch ex As Exception
                Chyby.Show("Nemožno zmazať. Priečinok sa používa")

            End Try
            Dim sql As String = "DELETE FROM Zakazka WHERE pocet=2 AND Zakazka='" & TextBox2.Text & "' AND Podzakazka='" & podzakazka1 & "'"
            Form78.sqa(sql)
            sql = "DELETE FROM Huta WHERE pocet='3' AND Zakazka='" & TextBox2.Text & "' AND Vaha='" & podzakazka1 & "'"
            Form78.sqa(sql)
            zakazka = TextBox2.Text

            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)
            subory_aktualiyuj()
            Me.ZakazkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND {4}='{5}'", RotekDataSet.Zakazka.pocetColumn, 2, RotekDataSet.Zakazka.ZakazkaColumn, zakazka, RotekDataSet.Zakazka.VykresyColumn, 1)

            If DataGridView4.RowCount = 0 Then
                sql = "UPDATE Zakazka SET Vykresy='" & 2 & "' WHERE pocet=" & 1 & " AND zakazka='" + zakazka + "'"

            End If

            'Me.HutaTableAdapter.Fill(Me.RotekDataSet.Huta)
            Me.ZakazkaTableAdapter.Fill(Me.RotekDataSet.Zakazka)



            For i As Integer = 0 To DataGridView3.RowCount - 1
                If DataGridView3.Rows(i).Cells(0).Value = TextBox3.Text Then
                    DataGridView3.Rows(i).Selected = True
                Else
                    DataGridView3.Rows(i).Selected = False
                End If
            Next
        End If

    End Sub
    Private Sub zmazuj(ByVal podzakazka1 As String)
        Dim DirList As New ArrayList
        Dim cesta As String = My.Settings.Rotek3 & "zakazky\" & TextBox2.Text.Replace("/", "\") & "\"

        GetDirectories(cesta, DirList)


        For Each item In DirList
            ' MessageBox.Show(item)
            'add item to listbox or text etc here

            If item.ToString.IndexOf(podzakazka1) > cesta.Length - 2 Then
                FileIO.FileSystem.DeleteDirectory(item, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
        Next
    End Sub

    Private Sub ComboBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.TextChanged
        Try

            'Dim s As String = ComboBox1.Text
            'Me.ZoznamFBindingSource.Filter = String.Format("{0} = '{1}' AND {2} LIKE '%{3}%'", RotekDataSet.ZoznamF.pocetColumn, 0, RotekDataSet.ZoznamF.NazovColumn, ComboBox1.Text)
            'ComboBox1.Text = s
            'ComboBox1.SelectionStart = s.Length
        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            TextBox5.Text = ""
            TextBox15.Text = "-1"
            TextBox5.Visible = True
            TextBox15.Visible = False
            Label2.Show()
            Label24.Hide()
            Label4.Text = "Výška [mm]"
            Label2.Text = "Šírka [mm]"
        End If

    End Sub

    Private Sub RadioButton12_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton12.CheckedChanged
        If RadioButton12.Checked = True Then
            TextBox5.Text = ""
            TextBox15.Text = ""
            TextBox5.Visible = True
            TextBox15.Visible = False
            Label2.Show()
            Label24.Show()
            Label2.Text = "Vnutorny priemer (d) [mm]"
            Label4.Text = "Priemer (D) [mm]"
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            TextBox5.Text = ""
            TextBox15.Text = ""
            TextBox5.Visible = True
            TextBox15.Visible = True
            Label2.Show()
            Label4.Text = "b [mm]"
            Label2.Text = "a [mm]"
            Label24.Show()

        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            TextBox5.Text = ""
            TextBox15.Text = ""
            TextBox5.Visible = True
            TextBox15.Visible = True
            Label2.Show()
            Label4.Text = "b [mm]"
            Label2.Text = "a [mm]"
            Label24.Show()
        End If
    End Sub

    Private Sub RadioButton11_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton11.CheckedChanged
        If RadioButton11.Checked = True Then
            TextBox5.Text = "-1"
            TextBox15.Text = "-1"
            TextBox15.Visible = False
            TextBox5.Visible = False
            Label2.Hide()
            Label24.Hide()
            Label4.Text = "Priemer [mm]"
        End If
    End Sub


    Private Sub RadioButton13_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton13.CheckedChanged
        If RadioButton13.Checked = True Then
            TextBox5.Text = ""
            TextBox15.Text = ""
            TextBox5.Visible = True
            TextBox15.Visible = True
            Label2.Show()
            Label4.Text = "b [mm]"
            Label2.Text = "a [mm]"
            Label24.Show()
        End If
    End Sub

    Private Sub TextBox15_TextChanged(sender As Object, e As EventArgs) Handles TextBox15.TextChanged
        Try
            If RadioButton12.Checked = True Then TextBox5.Text = TextBox7.Text - TextBox15.Text / 2 - TextBox15.Text / 2
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox16_TextChanged(sender As Object, e As EventArgs) Handles TextBox16.TextChanged
        Try
            TextBox16.Text = TextBox16.Text.Replace(",", ".")
            If TextBox16.Text.IndexOf(".") < TextBox16.Text.Length - 2 Then
                TextBox16.Text = Math.Floor(CDec(TextBox16.Text) * 1000) / 1000
            End If
            TextBox16.SelectionStart = TextBox16.Text.Length
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox17_TextChanged(sender As Object, e As EventArgs) Handles TextBox17.TextChanged
        Try
            TextBox17.Text = TextBox17.Text.Replace(",", ".")
            If TextBox17.Text.IndexOf(".") < TextBox17.Text.Length - 2 Then
                TextBox17.Text = Math.Floor(CDec(TextBox17.Text) * 1000) / 1000
            End If
            TextBox17.SelectionStart = TextBox17.Text.Length
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox16_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox16.KeyUp
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                Vseobecne()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try
                    TextBox17.Text = TextBox16.Text * TextBox10.Text
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox17_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox17.KeyUp
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                Vseobecne()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try
                    TextBox16.Text = TextBox17.Text / TextBox10.Text
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MultiLineListBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles MultiLineListBox1.MouseUp
        If e.Button = MouseButtons.Right Then
            MultiLineListBox1.Focus()
            Dim index As Integer = MultiLineListBox1.IndexFromPoint(e.X, e.Y)
            Dim s As String = MultiLineListBox1.Items(index).ToString()
            MultiLineListBox1.SelectedItem = MultiLineListBox1.Items(index)
            ContextMenuStrip2.Show(Cursor.Position)
        End If
    End Sub

    Private Sub ZmazaťToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ZmazaťToolStripMenuItem1.Click
        Dim podzakazka As String = TextBox3.Text
        Dim zakazka As String = TextBox2.Text
        Dim slovo As String = MultiLineListBox1.Items(MultiLineListBox1.SelectedIndex).ToString

        slovo = slovo.Substring(slovo.IndexOf("-") + 2)
        System.Console.WriteLine(podzakazka)
        SQL_main.Odpalovac("DELETE FROM ZakazkaPoznamka WHERE Zakazka_ID = (SELECT TOP 1 ID FROM Zakazka WHERE Zakazka = '" & zakazka & "' AND pocet = 2 AND Podzakazka =  '" & podzakazka & "') AND Poznamka LIKE '" & slovo & "'")
        poznamky(zakazka, podzakazka)
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim cesta2 As String = My.Settings.Rotek3 + "CP\"
        cesta2 = cesta2 & (Now.Year Mod 2000) & "\" & cp_vykresy.Replace("/", "•") & "\"
        Try
            For Each files In System.IO.Directory.GetFiles(cesta2)
                ListBox5.Items.Add(files.Substring(files.LastIndexOf("\") + 1))
                zoznam_vykresov2.Add(files)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub odober_subor(ByRef s As List(Of String), ByRef l As ListBox, ByVal name As String)
        Dim toRemove As String = ""
        For Each sx As String In s
            If sx.Substring(sx.LastIndexOf("\") + 1) = name Then
                toRemove = sx
            End If
        Next
        If toRemove <> "" Then
            s.Remove(toRemove)
            l.Items.Remove(name)
        End If
    End Sub

    Private Sub ListBox5_MouseUp(sender As Object, e As MouseEventArgs) Handles ListBox5.MouseUp
        If e.Button = MouseButtons.Right Then

            Dim item = ListBox5.IndexFromPoint(e.Location)
            MessageBox.Show(item)
            If item >= 0 Then
                ListBox5.SelectedIndex = item
                odober_subor(zoznam_vykresov2, ListBox5, ListBox5.Items(item))
            End If
        End If
    End Sub
End Class



