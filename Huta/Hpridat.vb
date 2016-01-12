Imports System.Data.SqlClient
Imports WindowsApplication2.RotekDataSetTableAdapters

Public Class Hpridat
    Dim vyska As Integer = 15

    Dim k, j As Integer
    Property nastr As String
    Property tex As String
    Property menko As String
    Dim crc, menoo, priezviskoo As String

    Property druhhh As String

    Property nazovvv As String

    Property rozmerrr As String

    Public Sub New(ByVal druh As String, ByVal nazov As String, ByVal sirka As Decimal, ByVal rozmer As Decimal, ByVal velkost As Integer, ByVal s_rozmer As Decimal, ByVal typ As String)
        InitializeComponent()

        typ_oznac(typ)

        TextBox4.Text = druh
        TextBox6.Text = nazov
        TextBox7.Text = rozmer
        TextBox1.Text = velkost
        TextBox5.Text = sirka
        TextBox14.Text = s_rozmer



        TextBox1.Focus()
        TextBox9.Focus()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub
    Public Sub New(ByVal prijemka As String)

        ' This call is required by the designer.
        InitializeComponent()
        Label16.Text = prijemka
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Me.DodavatelTableAdapter.Fill(Me.RotekDataSet.Dodavatel)

            If Label16.Text = "Label16" Then
                Dim prijemkyTableAdapter As PrijemkyTableAdapter = New PrijemkyTableAdapter

                Dim vydajka As String = prijemkyTableAdapter.maxNazov(Form78.rok)
                If vydajka Is Nothing Then vydajka = ""
                vydajka = vydajka.Replace("P", "")
                vydajka = vydajka.Replace("/" & Form78.rok(), "")
                Try
                    Dim i As Integer = vydajka
                    Label16.Text = "P" & Format(i + 1, "0000") & "/" & Form78.rok
                Catch ex As Exception
                    Label16.Text = "P" & Format(0, "0000") & "/" & Form78.rok
                End Try

                TextBox15.Text = Form78.uzivatel
                TextBox12.Focus()
            Else
                'Me.PrijemkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Prijemka.pocetColumn, 1, RotekDataSet.Prijemka.NazovColumn, Label16.Text)
                Dim prijemka As Prijemka_SQL = New Prijemka_SQL(Label16.Text)
                TextBox12.Text = prijemka.dodaciList
                ComboBox1.SelectedValue = prijemka.dodavatel_id
                TextBox15.Text = prijemka.vyhotovil
                DateTimePicker1.Value = prijemka.datum
                TextBox16.Text = prijemka.poznamka
                TextBox4.Focus()
            End If
            Napln_tabulku()
            hladaj()

            j = 1


            Dim druh, nazov, rozmer, sirka, srozmer, velkost, typ As String
            druh = TextBox4.Text
            nazov = TextBox6.Text
            rozmer = TextBox7.Text
            sirka = TextBox5.Text
            srozmer = TextBox14.Text
            velkost = TextBox1.Text
            typ = typ_slovom()

            If druh.Length = 0 Or nazov.Length = 0 Or rozmer.Length = 0 Then
                Exit Sub
            End If

            Dim material As Huta_SQL = New Huta_SQL(druh, nazov, sirka, rozmer, srozmer, velkost, 0, typ)

            TextBox2.Text = material.hustota
            TextBox3.Text = material.cena

            prepocet2()

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Function prijemka_databaza() As Boolean
        Try
            If TextBox12.Text.Length = 0 Or ComboBox1.Text.Length = 0 Or TextBox15.Text.Length = 0 Then
                Chyby.Show("Nevyplnené údaje príjemky. Prosím vyplniť")
                Return False
            End If

            Dim d1 As Date
            Try
                d1 = DateTimePicker1.Value
            Catch ex As Exception
                Chyby.Show("Zlý dátum")
                Return False
            End Try

            Dim pozn As String = TextBox16.Text
            If String.IsNullOrEmpty(pozn) Then
                pozn = "-"
            End If
            Dim dd As DodavatelTableAdapter = New DodavatelTableAdapter
            Dim dodavatel_id As String = ""
            dodavatel_id = dd.byNazov(ComboBox1.Text.Trim).ToString
            If String.IsNullOrEmpty(dodavatel_id) Then
                SQL_main.Odpalovac("INSERT INTO Dodavatel (Nazov) VALUES ( '" & ComboBox1.Text & "')")
                dodavatel_id = (New DodavatelTableAdapter).byNazov(ComboBox1.Text)
            End If
            If String.IsNullOrEmpty(dodavatel_id) Then
                Chyby.Show("Nepodarilo sa pridat dodavatela")
                Return False
            End If


            Dim vyhotovil As String = TextBox15.Text
            If vyhotovil.Length = 0 Then
                Chyby.Show("Nezadane kto vhotovil")
                Return False
            End If
            Dim prijemkaTableAdapter As PrijemkyTableAdapter = New PrijemkyTableAdapter

            Dim prijemka_id As String
            prijemka_id = prijemkaTableAdapter.byNazov(Label16.Text)

            Dim sql As String
            If String.IsNullOrEmpty(prijemka_id) Then
                sql = "INSERT INTO Prijemky (Nazov, DodaciList, Datum_DL, Datum, Vyhotovil, Dodavatel_ID, Poznamka) VALUES('" + Label16.Text + "','" + TextBox12.Text + "','" + d1.ToString("yyyy-MM-dd") + "','" + Date.Now.ToString("yyyy-MM-dd") + "','" + vyhotovil + "', " + dodavatel_id + " ,'" + pozn + "')"
                SQL_main.Odpalovac(sql)
            Else
                sql = "UPDATE Prijemky SET DodaciList = '" & TextBox12.Text & "' , Datum_DL = '" & d1.ToString("yyyy-MM-dd") & "' , Datum = GETDATE() , Vyhotovil = '" & vyhotovil & "' , Dodavatel_ID = " & dodavatel_id & " , Poznamka = '" & pozn & "' WHERE ID = " & prijemka_id
                SQL_main.Odpalovac(sql)
            End If

        Catch ex As Exception
            Chyby.Show(ex.ToString)
            Return False
        End Try


        Return True
    End Function


    Public Sub stuk()
        If prijemka_databaza() = False Then 'AndAlso Form78.heslo <> Form78.admin Then
            Exit Sub
        End If

        Dim druh, nazov As String
        druh = TextBox4.Text.Trim
        nazov = TextBox6.Text.Trim


        'If rozmer.IndexOf("r") = rozmer.Length - 1 Or rozmer.IndexOf("d") = rozmer.Length - 1 Or (rozmer.IndexOf("x") > 0 And rozmer.IndexOf("x") < rozmer.Length - 1) Then
        'Else
        '    Chyby.Show("Zle zadaný rozmer. Buď sa musí končiť s r/d napr. 15r, pokiaľ je okrúhly alebo ak je to doska tak 'šírka'x'výška' napr. 15x20")
        '    Exit Sub
        'End If

        If druh.Length = 0 Or nazov.Length = 0 Then
            Chyby.Show("Niečo nie je zadané druh alebo nazov")
            Exit Sub
        End If

        If (Form78.heslo <> Form78.admin) And (TextBox8.Text.Length = 0 Or TextBox2.Text.Length = 0 Or TextBox3.Text.Length = 0 Or TextBox9.Text.Length = 0 Or TextBox10.Text.Length = 0 Or TextBox11.Text.Length = 0) Then
            Chyby.Show("Niečo nie je zadané")
            Exit Sub
        End If

        Dim cenaKg, cena, hustota, velkost, kg, kskg As Double
        Dim kusov, plocha As Integer
        plocha = 0
        Try
            kusov = TextBox9.Text
        Catch ex As Exception
            Chyby.Show("Zle zadaný počet kusov")
            Exit Sub
        End Try

        Dim rozmery As Rozmery
        Try
            rozmery = Hvydat.get_rozmery(TextBox5.Text, TextBox7.Text, TextBox14.Text, TextBox1.Text, typ_slovom)


            If kusov < 0 AndAlso Form78.heslo <> Form78.admin Then
                Chyby.Show("Nemáte právo nechávať zmiznúť zo skladu materiál. To môže len administrator")
                Exit Sub
            End If

            Try
                Dim jdsfb As Double = Huta_SQL.objem(rozmery.rozmer, rozmery.velkost, rozmery.sirka, rozmery.s_rozmer, rozmery.typ)
            Catch ex As Exception
                Chyby.Show("Zle zadaný rozmer")
                Exit Sub
            End Try

            Try
                cenaKg = TextBox3.Text
            Catch ex As Exception
                cenaKg = 0
            End Try
            Try
                cena = TextBox11.Text
            Catch ex As Exception
                cena = 0
            End Try

            Try
                kg = TextBox8.Text
            Catch ex As Exception
                kg = 0
            End Try
            Try
                kskg = TextBox10.Text
            Catch ex As Exception
                kskg = 0
            End Try

            Try
                hustota = TextBox2.Text
            Catch ex As Exception
                hustota = 0
            End Try

            Dim prijemkaTableAdapter As PrijemkyTableAdapter = New PrijemkyTableAdapter
            Dim prijemka_ID As Integer = prijemkaTableAdapter.byNazov(Label16.Text)
            Dim material As Material_SQL = New Material_SQL(druh, nazov, rozmery.sirka, rozmery.rozmer, rozmery.s_rozmer, rozmery.velkost, 0, rozmery.typ)
            Dim material_id As Integer = material.save
            Dim material_plochac As Material_SQL = Nothing
            If RadioButton2.Checked Then
                material_plochac = New Material_SQL(druh, nazov, -1, rozmery.rozmer, -1, -1, 0, rozmery.typ)
                material_plochac.save()
            End If

            Me.MaterialPrijemkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' ", RotekDataSet.Material_Prijemka.Prijemka_IDColumn, prijemka_ID, RotekDataSet.Material_Prijemka.Material_IDColumn, material_id)
            If DataGridView4.RowCount = 1 Then
                SQL_main.Odpalovac("UPDATE Material_Prijemka SET Ks = " & kusov & ", KsKg = " & kskg & " , Kg = " & kg & " , CenaKg = " & cenaKg & " , Cena = " & cena & " WHERE Prijemka_ID = " & prijemka_ID & " AND Material_ID = " & material_id)
                If kusov < 0 Then
                Else
                    kusov = kusov - DataGridView4.Rows(0).Cells(3).Value
                End If
                If RadioButton2.Checked Then
                    material_plochac.add_to_stock(kusov * material.velkost * material.sirka)
                    'Chyby.Show(material.velkost & " " & material.sirka)
                Else
                    material.add_to_stock(kusov)
                End If
                If TextBox9.Text = "0" Then
                    SQL_main.Odpalovac("DELETE FROM Material_Prijemka WHERE Prijemka_ID = " & prijemka_ID & " AND Material_ID = " & material_id)
                End If
            Else
                SQL_main.Odpalovac("INSERT INTO Material_Prijemka (Material_ID, Prijemka_ID, Ks, KsKg, Kg, CenaKg, Cena) VALUES ( " & material_id & " , " & prijemka_ID & " , " & kusov & " , " & kskg & " , " & kg & " , " & cenaKg & " , " & cena & " )")
                If RadioButton2.Checked Then
                    material_plochac.add_to_stock(kusov * material.velkost * material.sirka)
                Else
                    material.add_to_stock(kusov)
                End If

            End If


            If cenaKg <> 0 Then
                Try
                    cenaKg = Math.Floor(CDec(cenaKg) * 1000) / 1000
                Catch ex As Exception
                End Try
                material.saveCena(cenaKg)
                If RadioButton2.Checked Then
                    material_plochac.saveCena(cenaKg)
                End If

            End If

            If hustota <> 0 Then
                Try
                    hustota = Math.Floor(CDec(hustota) * 1000) / 1000
                Catch ex As Exception
                End Try
                material.saveHustota(hustota)
                If RadioButton2.Checked Then
                    material_plochac.saveHustota(hustota)
                End If

            End If


            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            TextBox7.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = "1"
            TextBox10.Text = ""
            TextBox11.Text = ""
            TextBox13.Text = ""
            TextBox14.Text = ""
            RadioButton2.Checked = True
            RadioButton1.Checked = True


            Napln_tabulku()
            hladaj()

            Me.Material_PrijemkaTableAdapter.Fill(Me.RotekDataSet.Material_Prijemka)

            TextBox4.Focus()

        Catch ex As SystemException
            Chyby.Show(ex.ToString)
            Exit Sub
        End Try


    End Sub
    Private Sub typ_oznac(typ As String)

        Select Case typ
            Case "Plech"
                RadioButton2.Checked = True
            Case "Valec"
                RadioButton1.Checked = True
            Case "Rurka"
                RadioButton3.Checked = True
            Case "6 - hran"
                RadioButton4.Checked = True
            Case "L - profil"
                RadioButton5.Checked = True
            Case "U - profil"
                RadioButton6.Checked = True
            Case "Jokelt"
                RadioButton7.Checked = True
            Case "Hranol"
                RadioButton8.Checked = True

        End Select
    End Sub

    Private Function typ_slovom() As String
        If RadioButton1.Checked Then
            Return "Valec"
        ElseIf RadioButton2.Checked Then
            Return "Plech"
        ElseIf RadioButton3.Checked Then
            Return "Rurka"
        ElseIf RadioButton4.Checked Then
            Return "6 - hran"
        ElseIf RadioButton5.Checked Then
            Return "L - profil"
        ElseIf RadioButton6.Checked Then
            Return "U - profil"
        ElseIf RadioButton7.Checked Then
            Return "Jokelt"
        ElseIf RadioButton8.Checked Then
            Return "Hranol"

        Else
            Return ""
        End If

    End Function

    Private Sub poverenia()
        Dim dd As List(Of Dictionary(Of String, Boolean)) = New List(Of Dictionary(Of String, Boolean))
        Dim d As Dictionary(Of String, Boolean)

        dd = Povolenia_SQL.getRights("Príjemka")
        Select Case Form78.heslo
            Case Form78.admin
                d = dd(0)
            Case Form78.zakazkar
                d = dd(1)
            Case Form78.skladnik
                d = dd(2)
            Case Else
                d = dd(3)
        End Select

        If (Not d("Mazať položky")) Then
            DataGridView3.Columns("Zmazať").Visible = False
        End If

    End Sub

    Private Sub Napln_tabulku()
        Me.MaterialNazovTableAdapter.Fill(Me.RotekDataSet.MaterialNazov)
        Me.MaterialDruhTableAdapter.Fill(Me.RotekDataSet.MaterialDruh)
        Me.Material_PrijemkaTableAdapter.Fill(Me.RotekDataSet.Material_Prijemka)

        DataGridView3.DataSource = Nothing
        DataGridView3.Columns.Clear()
        DataGridView3.DataSource = Prijemka_SQL.materialByName(Label16.Text)
        Dim btnColumn As DataGridViewButtonColumn = New DataGridViewButtonColumn()
        btnColumn.HeaderText = "Zmazať"
        btnColumn.Name = "Zmazať"
        btnColumn.UseColumnTextForButtonValue = True
        btnColumn.Text = "Zmazať"
        btnColumn.Visible = True
        DataGridView3.Columns.Add(btnColumn)

        poverenia()


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        stuk()
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp, TextBox2.KeyUp

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                prepocet2()
            End If

        Catch ex As Exception


        End Try
    End Sub
    Private Sub prepocet2()
        If (TextBox7.Text.Length <> 0) And (TextBox5.Text.Length <> 0) And (TextBox1.Text.Length <> 0) And (TextBox2.Text.Length <> 0) Then  Else Return
        Dim hmotnost As Decimal
        Dim velkost As Decimal
        Dim rozmer As Decimal
        Dim hustota As Double
        Dim kusov As Integer
        Dim sirka As Decimal
        Dim s_rozmer As Decimal

        Try
            kusov = TextBox9.Text
            velkost = TextBox1.Text * kusov
            rozmer = TextBox7.Text
            hustota = TextBox2.Text
            sirka = TextBox5.Text
            s_rozmer = TextBox14.Text
        Catch ex As Exception

            Exit Sub
        End Try

        Try
            Dim typ As String = typ_slovom()

            Dim objem As Double = Huta_SQL.objem(rozmer, velkost, sirka, s_rozmer, typ)
            hmotnost = hustota * objem

            TextBox8.Text = hmotnost
            If TextBox9.Text = "0" Then
                TextBox10.Text = "0"
            Else
                TextBox10.Text = hmotnost / TextBox9.Text
            End If
            Try
                TextBox11.Text = hmotnost * TextBox3.Text
                If TextBox9.Text = "0" Then
                    TextBox13.Text = 0
                Else
                    TextBox13.Text = TextBox11.Text / TextBox9.Text
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception
        End Try

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
            Hpridat.stlacBound(k, sender, ListBox1, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()

            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                'Me.HutaBindingSource.Sort = "Druh"
                hladaj()
                'Dim a As Integer = tex.Length
                'TextBox4.Select(a, TextBox4.Text.Length - a)
            End If

        Catch ex As Exception


        End Try

    End Sub

    Private Sub TextBox6_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        Try
            Hpridat.stlacBound(k, sender, ListBox3, e)
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                hladaj()
            End If
        Catch ex As Exception
        End Try
    End Sub




    Private Sub hladaj()
        hladajDruh()
        hladajNazov()
    End Sub

    Private Sub hladajDruh()
        ListBox1.DataSource = MaterialDruhTableAdapter.GetDataByNazov(TextBox4.Text)

    End Sub
    Private Sub hladajNazov()
        MaterialNazovBindingSource.Filter = "Nazov LIKE '%" & TextBox6.Text & "%' AND Druh LIKE '%" & TextBox4.Text & "%'"
    End Sub


    Public Shared Sub hladaj3(ByRef listbs As List(Of ListBox), ByVal aree As List(Of String), stlp() As Integer, ByVal d As DataGridView, ByRef b As BindingSource)
        Try

            Dim i As Integer = 0
            Dim opak As String = "}|s"
            Dim listb As ListBox

            For Each listbl As ListBox In listbs
                opak = "{|gr."
                listb = listbs(i)
                b.Sort = aree(i)

                listb.Items.Clear()
                For c = 0 To d.RowCount - 1
                    If opak.ToUpper <> d.Rows(c).Cells(stlp(i)).Value.ToString.ToUpper AndAlso IsDBNull(d.Rows(c).Cells(stlp(i)).Value) = False Then

                        listb.Items.Add(d.Rows(c).Cells(stlp(i)).Value)
                        opak = d.Rows(c).Cells(stlp(i)).Value
                    End If
                Next
                i = i + 1
            Next

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Public Shared Sub stlac(ByRef i As Integer, ByRef tex As TextBox, ByRef listb As ListBox, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If i < 0 Then
                i = listb.Items.Count - 1

            ElseIf i >= listb.Items.Count Then
                i = 0
            End If

            If e.KeyCode = 40 Then
                tex.Text = (listb.Items(i).ToString)
                tex.Select(0, tex.Text.Length)
                i = i + 1
            ElseIf e.KeyCode = Keys.Up Then
                tex.Text = (listb.Items(i).ToString)
                tex.Select(0, tex.Text.Length)
                i = i - 1
            End If

        Catch ex As Exception

        End Try
    End Sub


    Public Shared Sub stlacBound(ByRef i As Integer, ByRef tex As TextBox, ByRef listb As ListBox, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If i < 0 Then
                i = listb.Items.Count - 1
            ElseIf i >= listb.Items.Count Then
                i = 0
            End If
            'listb.SelectedItem = listb.Items(i)
            If e.KeyCode = 40 Then
                listb.SelectedItem = listb.Items(i)
                tex.Text = listb.SelectedValue
                tex.Select(0, tex.Text.Length)
                i = i + 1
            ElseIf e.KeyCode = Keys.Up Then
                listb.SelectedItem = listb.Items(i)
                tex.Text = listb.SelectedValue
                tex.Select(0, tex.Text.Length)
                i = i - 1
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox7_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyUp

        Try

            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                uprav_values(TextBox7)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub uprav_values(textac As TextBox)
        Try
            textac.Text = textac.Text.Replace(",", ".")
            If (textac.Text.Chars(textac.Text.Length - 1) = "0" And textac.Text.IndexOf(".") > 0) Then
                Return
            End If

            If textac.Text.IndexOf(".") <> textac.Text.Length - 1 Then
                Dim d As Decimal = Math.Floor(CDec(textac.Text) * 100)
                d = d / 100
                textac.Text = d
            End If

            textac.SelectionStart = textac.Text.Length
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TextBox3_KeyUp_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then

                Try
                    TextBox11.Text = TextBox3.Text * TextBox8.Text
                    TextBox13.Text = TextBox3.Text * TextBox8.Text / TextBox9.Text
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub TextBox7_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.Enter
        'hladaj2(ListBox4, 4, "Rozmer")
    End Sub



    Private Sub TextBox8_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyUp
        Try

            If e.KeyCode = Keys.Escape Then
                Me.Close()

            ElseIf e.KeyCode = Keys.Enter Then
                stuk()

            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                prepocet1()
            End If

        Catch ex As Exception
        End Try
    End Sub
    Private Sub prepocet1()
        If (TextBox7.Text.Length <> 0) And (TextBox1.Text.Length <> 0) And (TextBox8.Text.Length <> 0) Then
            Try
                TextBox10.Text = TextBox8.Text / TextBox9.Text
                Dim hmotnost As Double = TextBox10.Text
                Dim velkost As Double = TextBox1.Text
                Dim rozmer As String = TextBox7.Text
                Dim sirka As String = TextBox5.Text
                Dim s_rozmer As String = TextBox14.Text
                Dim hustota As Double
                Dim typ As String
                If RadioButton1.Checked = True Then
                    typ = "Valec"
                ElseIf RadioButton2.Checked = True Then
                    typ = "Plech"
                ElseIf RadioButton3.Checked = True Then
                    typ = "Rurka"
                ElseIf RadioButton4.Checked = True Then
                    typ = "6 - hran"
                ElseIf RadioButton5.Checked = True Then
                    typ = "L - profil"
                ElseIf RadioButton6.Checked = True Then
                    typ = "U - profil"
                ElseIf RadioButton8.Checked = True Then
                    typ = "Hranol"
                Else
                    typ = "Jokelt"
                End If
                Dim objem As Double = Huta_SQL.objem(rozmer, velkost, sirka, s_rozmer, typ)

                Dim dlzka As Double = rozmer / 2000
                hustota = hmotnost / objem
                TextBox2.Text = hustota
                'Chyby.Show(hustota)
                Try
                    TextBox11.Text = TextBox8.Text * TextBox3.Text
                    TextBox13.Text = TextBox11.Text / TextBox9.Text
                Catch ex As Exception

                End Try
            Catch ex As Exception
                '    Chyby.Show(ex.ToString)
            End Try

        End If
    End Sub

    Private Sub TextBox11_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyUp

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try

                    TextBox3.Text = TextBox11.Text / TextBox8.Text
                    TextBox13.Text = TextBox11.Text / TextBox9.Text
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox10_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyUp

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try
                    TextBox8.Text = TextBox10.Text * TextBox9.Text
                    prepocet1()

                Catch ex As Exception

                End Try

            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox9_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyUp

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                prepocet2()

            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox9_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox9.Leave
        Dim i As Integer
        Try
            i = TextBox9.Text
        Catch ex As Exception
            TextBox9.Text = 1
        End Try
    End Sub

    Private Sub TextBox13_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox13.KeyUp
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try
                    TextBox11.Text = TextBox13.Text * TextBox9.Text
                    TextBox3.Text = TextBox13.Text * TextBox9.Text / TextBox8.Text
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox9_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox9.TextChanged
        Try
            prepocet2()
        Catch ex As Exception
            TextBox8.Text = ""
            TextBox10.Text = ""
        End Try
        Try

            TextBox11.Text = TextBox3.Text * TextBox8.Text
            TextBox13.Text = TextBox3.Text * TextBox8.Text / TextBox9.Text

        Catch ex As Exception
            TextBox11.Text = ""
            TextBox13.Text = ""

        End Try

    End Sub

    Private Sub TextBox10_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox10.TextChanged
        Try
            TextBox10.Text = TextBox10.Text.Replace(",", ".")

            If TextBox10.Text.IndexOf(".") <> TextBox10.Text.Length - 1 Then
                TextBox10.Text = Math.Floor(CDec(TextBox10.Text) * 1000) / 1000
            End If
            TextBox10.SelectionStart = TextBox10.Text.Length

        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox8_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox8.TextChanged
        Try
            TextBox8.Text = TextBox8.Text.Replace(",", ".")
            If TextBox8.Text.IndexOf(".") <> TextBox8.Text.Length - 1 Then
                TextBox8.Text = Math.Floor(CDec(TextBox8.Text) * 1000) / 1000
            End If
            TextBox8.SelectionStart = TextBox8.Text.Length
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        Try
            TextBox2.Text = TextBox2.Text.Replace(",", ".")
            If TextBox2.Text.IndexOf(".") <> TextBox2.Text.Length - 1 Then
                TextBox2.Text = Math.Floor(CDec(TextBox2.Text) * 1000) / 1000
            End If
            TextBox2.SelectionStart = TextBox2.Text.Length
        Catch ex As Exception
        End Try


    End Sub

    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged
        Try
            TextBox3.Text = TextBox3.Text.Replace(",", ".")
            If TextBox3.Text.IndexOf(".") <> TextBox3.Text.Length - 1 Then
                TextBox3.Text = Math.Floor(CDec(TextBox3.Text) * 1000) / 1000
            End If
            TextBox3.SelectionStart = TextBox3.Text.Length

        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox13_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox13.TextChanged
        Try
            TextBox13.Text = TextBox13.Text.Replace(",", ".")
            If TextBox13.Text.IndexOf(".") <> TextBox13.Text.Length - 1 Then
                TextBox13.Text = Math.Floor(CDec(TextBox13.Text) * 1000) / 1000
            End If
            TextBox13.SelectionStart = TextBox13.Text.Length
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox11_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox11.TextChanged
        Try
            TextBox11.Text = TextBox11.Text.Replace(",", ".")

            If TextBox11.Text.IndexOf(".") <> TextBox11.Text.Length - 1 Then
                TextBox11.Text = Math.Floor(CDec(TextBox11.Text) * 1000) / 1000
            End If
            TextBox11.SelectionStart = TextBox11.Text.Length
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox1_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox1.Leave, TextBox1.Enter
        Try

            material_hustota_cena()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub material_hustota_cena()
        Try

            Dim druh, nazov, rozmer, s_rozmer, sirka, typ As String
            druh = TextBox4.Text
            nazov = TextBox6.Text
            rozmer = TextBox7.Text
            sirka = TextBox5.Text
            s_rozmer = TextBox14.Text
            typ = typ_slovom()

            If druh.Length = 0 Or nazov.Length = 0 Or rozmer.Length = 0 Then
                Exit Sub
            End If

            Dim material As Material_SQL = New Material_SQL(druh, nazov)
            TextBox2.Text = material.hustota
            material = New Material_SQL(druh, nazov, sirka, rozmer, s_rozmer, typ)
            TextBox3.Text = material.cena
            prepocet2()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox4_Click(sender As System.Object, e As System.EventArgs) Handles TextBox4.Click
        TextBox4.Text = ""
        hladaj()

    End Sub

    Private Sub TextBox6_Click(sender As System.Object, e As System.EventArgs) Handles TextBox6.Click
        hladaj()

    End Sub


    Private Sub TextBox14_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            stuk()
        End If
    End Sub

    Private Sub TextBox15_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox15.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            stuk()
        End If
    End Sub

    Private Sub TextBox16_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox16.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            stuk()
        End If
    End Sub

    Private Sub radio_visiblity(sirka As Integer, rozmer As Integer, srozmer As Integer)
        Label5.Text = "Do dĺžky [mm]"

        If sirka = 1 Then
            Label2.Show()
            TextBox5.Text = ""
            TextBox5.Show()
        Else
            Label2.Hide()
            TextBox5.Text = "-1"
            TextBox5.Hide()
        End If
        If rozmer = 1 Then
            Label4.Show()
            TextBox7.Show()
            TextBox7.Text = ""
        Else
            Label4.Hide()
            TextBox7.Hide()
            TextBox7.Text = "-1"
        End If
        If srozmer = 1 Then
            Label23.Show()
            TextBox14.Show()
            TextBox14.Text = ""
        Else
            Label23.Hide()
            TextBox14.Text = "-1"
            TextBox14.Hide()
        End If
    End Sub
    Private Sub RadioButton1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            radio_visiblity(0, 1, 0)
            Label4.Text = "Priemer [mm]"
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox5.TextChanged

        If TextBox5.Text.IndexOf("x") > -1 Then
            TextBox5.Text = TextBox5.Text.Replace("x", "")
            TextBox7.Focus()
        End If

    End Sub

    Private Sub TextBox7_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox7.TextChanged
        If TextBox7.Text.IndexOf("x") > -1 Then
            TextBox7.Text = TextBox7.Text.Replace("x", "")
            TextBox1.Focus()
        End If
        Try
            If RadioButton3.Checked = True Then TextBox14.Text = (TextBox7.Text - TextBox5.Text) / 2
        Catch ex As Exception
        End Try
    End Sub


    Private Sub TextBox5_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyUp
        If ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
            Try
                If RadioButton3.Checked = True Then TextBox14.Text = (TextBox7.Text - TextBox5.Text) / 2
            Catch ex As Exception
            End Try
            uprav_values(TextBox5)
        End If
    End Sub

    Private Sub TextBox5_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox5.Leave
        TextBox5.Text = TextBox5.Text.Replace(",", ".")
    End Sub

    Private Sub TextBox7_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox7.Leave
        TextBox7.Text = TextBox7.Text.Replace(",", ".")

    End Sub

    Private Sub DataGridView3_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        Try

            If e.RowIndex = -1 Then
                Exit Sub
            End If
            If e.ColumnIndex = 12 Then 'zmazat

                selectRow(e.RowIndex)
                TextBox9.Text = 0
                stuk()
            Else
                selectRow(e.RowIndex)
                TextBox9.Focus()

            End If

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub selectRow(row As Integer)
        Dim druh, nazov, typ As String
        druh = DataGridView3.Rows(row).Cells(0).Value
        nazov = DataGridView3.Rows(row).Cells(1).Value
        typ = DataGridView3.Rows(row).Cells(2).Value

        Dim rozmer, sirka, srozmer As Decimal
        Dim velkost, kusov As Integer
        sirka = DataGridView3.Rows(row).Cells(3).Value
        rozmer = DataGridView3.Rows(row).Cells(4).Value
        srozmer = DataGridView3.Rows(row).Cells(5).Value
        velkost = DataGridView3.Rows(row).Cells(6).Value
        kusov = DataGridView3.Rows(row).Cells(7).Value

        TextBox4.Text = druh
        TextBox6.Text = nazov


        typ_oznac(typ)


        TextBox5.Text = sirka
        TextBox14.Text = srozmer
        TextBox7.Text = rozmer
        TextBox1.Text = velkost

        TextBox9.Text = kusov
        material_hustota_cena()


    End Sub
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            radio_visiblity(1, 1, 0)
            Label2.Text = "Šírka [mm]"
            Label5.Text = "Dĺžka [mm]"
            Label4.Text = "Hrúbka [mm]"
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            radio_visiblity(1, 1, 1)
            Label2.Text = "d priemer  (d)[mm]"
            Label4.Text = "D priemer (D)[mm]"
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            radio_visiblity(0, 1, 0)
            Label4.Text = "Šírka [mm]"
        End If
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked = True Then
            radio_visiblity(1, 1, 1)
            Label2.Text = "a [mm]"
            Label4.Text = "b [mm]"
        End If
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        If RadioButton6.Checked = True Then
            radio_visiblity(1, 1, 1)
            Label2.Text = "a [mm]"
            Label4.Text = "b [mm]"
        End If
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        If RadioButton7.Checked = True Then
            radio_visiblity(1, 1, 1)
            Label2.Text = "a [mm]"
            Label4.Text = "b [mm]"
        End If
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        If RadioButton8.Checked = True Then
            radio_visiblity(1, 1, 0)
            Label2.Text = "Šírka [mm]"
            Label4.Text = "Hrúbka [mm]"
            Label5.Text = "Dĺžka [mm]"

        End If
    End Sub



    Private Sub ListBox3_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox3.MouseClick
        If ListBox3.SelectedItems.Count > 0 Then
            TextBox6.Text = ListBox3.SelectedValue
            TextBox6.Focus()
            TextBox6.Select(TextBox6.Text.Length, 0)
        End If
    End Sub

    Private Sub ListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseClick
        If ListBox1.SelectedItems.Count > 0 Then
            TextBox4.Text = ListBox1.SelectedValue
            hladajNazov()
            TextBox4.Focus()
            TextBox4.Select(TextBox4.Text.Length, 0)
        End If
    End Sub

    Private Sub DataGridView3_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView3.CellFormatting
        If e.Value Is Nothing OrElse Not e.Value.GetType.Equals(New Decimal().GetType) OrElse e.Value <> -1 Then
        Else
            e.Value = ""
        End If

    End Sub


    Private Sub TextBox14_KeyUp_1(sender As Object, e As KeyEventArgs) Handles TextBox14.KeyUp
        If ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
            Try
                If RadioButton3.Checked = True Then TextBox5.Text = TextBox7.Text - TextBox14.Text * 2
            Catch ex As Exception
            End Try
            uprav_values(TextBox14)
        End If

    End Sub

End Class