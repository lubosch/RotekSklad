Imports System.Data.SqlClient
Imports WindowsApplication2.RotekDataSet
Public Class Hvydat
    Dim vyska As Integer = 15


    Dim k, j As Integer
    Property nastr As String
    Property tex As String
    Property menko As String
    Dim crc, menoo, priezviskoo As String

    Property druhhh As String

    Property nazovvv As String

    Property rozmerrr As String



    Public Sub New(ByVal druh As String, ByVal nazov As String, ByVal sirka As Integer, ByVal rozmer As Integer, ByVal velkost As Integer, ByVal s_rozmer As Integer, ByVal typ As String)
        InitializeComponent()

        typ_oznac(typ)

        TextBox4.Text = druh
        TextBox6.Text = nazov
        TextBox1.Text = velkost

        TextBox1.Focus()
        TextBox9.Focus()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub
    Public Sub New(ByVal vydajka As String)

        ' This call is required by the designer.
        InitializeComponent()
        TextBox12.Text = vydajka.Substring(0, vydajka.IndexOf("/"))
        NumericUpDown2.Value = vydajka.Substring(vydajka.IndexOf("/") + 1)

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Function vydajka_nazov() As String
        Return TextBox12.Text & "/" & NumericUpDown2.Value

    End Function

    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RotekDataSet.Material' table. You can move, or remove it, as needed.
        Me.MaterialTableAdapter.Fill(Me.RotekDataSet.Material)
        Me.MaterialNazovTableAdapter.Fill(Me.RotekDataSet.MaterialNazov)
        Me.MaterialDruhTableAdapter.Fill(Me.RotekDataSet.MaterialDruh)

        Dim dd As DataTable = New EmployerDataTable
        Me.EmployerTableAdapter.FillMenom(dd)
        Me.VydajkyTableAdapter.Fill(Me.RotekDataSet.Vydajky)
        Me.Material_VydajkaTableAdapter.FillByVydajkaNazov(Me.RotekDataSet2.Material_Vydajka, vydajka_nazov)


        ComboBox1.DataSource = dd
        ComboBox1.ValueMember = "ID"
        ComboBox1.DisplayMember = "Surname"

        CheckBox1.Checked = False

        NumericUpDown1.Value = Now.Year.ToString.Substring(2)

        Napln_tabulku()
        Try

            If TextBox12.Text = "" Then
                Dim vdta As RotekDataSetTableAdapters.VydajkyTableAdapter = New RotekDataSetTableAdapters.VydajkyTableAdapter

                Dim vydajka As String = vdta.maxNazov(Form78.rok)
                If vydajka Is Nothing Then vydajka = ""
                vydajka = vydajka.Replace("V ", "")
                vydajka = vydajka.Replace("/" & Form78.rok(), "")
                Try
                    Dim i As Integer = vydajka
                    TextBox12.Text = "V " & Format(i + 1, "0000")
                Catch ex As Exception
                    TextBox12.Text = "V 0000"
                End Try
                NumericUpDown2.Value = Form78.rok

            Else
                'Me.PrijemkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Prijemka.pocetColumn, 1, RotekDataSet.Prijemka.NazovColumn, Label16.Text)

                Dim vydajka As Vydajka_SQL = New Vydajka_SQL
                vydajka.find_by_nazov(vydajka_nazov)
                TextBox15.Text = vydajka.vyhotovil
                If vydajka.zakazka.IndexOf("/") > -1 Then TextBox11.Text = vydajka.zakazka.Substring(0, vydajka.zakazka.IndexOf("/"))
                DateTimePicker1.Value = vydajka.datum
                If vydajka.pokazil_ID <> "" Then
                    CheckBox1.Checked = True
                    ComboBox1.SelectedValue = vydajka.pokazil_ID
                End If
            End If

            hladaj()

            TextBox15.Text = Form78.uzivatel

            Dim druh, nazov, rozmer, sirka As String
            druh = TextBox4.Text
            nazov = TextBox6.Text




        Catch ex As Exception
            Chyby.Show(ex.ToString)

        End Try
    End Sub

    Private Sub Napln_tabulku()
        DataGridView3.DataSource = Nothing
        DataGridView3.Columns.Clear()
        DataGridView3.DataSource = Vydajka_SQL.materialByName(vydajka_nazov)
        Dim btnColumn As DataGridViewButtonColumn = New DataGridViewButtonColumn()
        btnColumn.HeaderText = "Zmazať"
        btnColumn.UseColumnTextForButtonValue = True
        btnColumn.Text = "Zmazať"
        Select Case Form78.heslo
            Case Form78.admin
                btnColumn.Visible = True
            Case Else
                btnColumn.Visible = False

        End Select

        DataGridView3.Columns.Add(btnColumn)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Public Function get_rozmery(sirka_tb As String, rozmer_tb As String, srozmer_tb As String, velkost_tb As String, typ As String, novy_rozmer As String) As Rozmery
        Select Case typ
            Case "Šírka"
                Return get_rozmery(novy_rozmer, rozmer_tb, srozmer_tb, velkost_tb, typ_slovom)
            Case "Hrúbka"
                Return get_rozmery(sirka_tb, novy_rozmer, srozmer_tb, velkost_tb, typ_slovom)
            Case "Veľkosť"
                Return get_rozmery(sirka_tb, rozmer_tb, srozmer_tb, novy_rozmer, typ_slovom)
            Case Else
                Return Nothing
        End Select
    End Function


    Public Shared Function get_rozmery(sirka_tb As String, rozmer_tb As String, srozmer_tb As String, velkost_tb As String, typ As String) As Rozmery
        Dim rozmery As Rozmery = New Rozmery
        Dim sirka, rozmer, s_rozmer, velkost As Integer

        Try
            rozmer = rozmer_tb
        Catch ex As Exception
            Throw New Exception
        End Try

        Try
            sirka = sirka_tb
        Catch ex As Exception
            Chyby.Show("Zle zadaná šírka")
            Throw New Exception
        End Try
        Try
            s_rozmer = srozmer_tb
        Catch ex As Exception
            Chyby.Show("Zle zadany s-rozmer")
            Throw New Exception
        End Try
        Try
            velkost = velkost_tb
        Catch ex As Exception
            Chyby.Show("Zle zadaná veľkosť")
            Throw New Exception
        End Try


        If typ = "Valec" Then
            sirka = -1
            s_rozmer = -1
        ElseIf typ = "Plech" Then
            s_rozmer = -1

        ElseIf typ = "Rurka" Then
            s_rozmer = -1
            If rozmer < sirka Then
                Dim t As Integer = rozmer
                rozmer = sirka
                sirka = t
                '                    Chyby.Show("D musí byť väčšie ako d")
                '                   Exit Sub
            End If
        ElseIf typ = "6 - hran" Then
            sirka = -1
            s_rozmer = -1
        ElseIf typ = "L - profil" Then

            If rozmer > sirka Then
                Dim pom As Integer = sirka
                sirka = rozmer
                rozmer = pom
            End If
        ElseIf typ = "U - profil" Then
            If s_rozmer * 2 - sirka > 0 Then
                Chyby.Show("Nefunkcne hodnoty")
                Return Nothing
            End If
        ElseIf typ = "Hranol" Then

            Dim pom As ArrayList = New ArrayList()
            pom.Add(sirka)
            pom.Add(rozmer)
            pom.Add(velkost)
            pom.Sort()
            sirka = pom(1)
            rozmer = pom(0)
            velkost = pom(2)
        Else

            If rozmer > sirka Then
                Dim pom As Integer = sirka
                sirka = rozmer
                rozmer = pom
            End If
        End If


        If rozmer = 0 Or s_rozmer = 0 Or velkost = 0 Or sirka = 0 Then
            velkost = 0
        End If

        rozmery.rozmer = rozmer
        rozmery.s_rozmer = s_rozmer
        rozmery.velkost = velkost
        rozmery.typ = typ
        rozmery.sirka = sirka
        Try
            Dim jdsfb As Double = Huta_SQL.objem(rozmery.rozmer, rozmery.velkost, rozmery.sirka, rozmery.s_rozmer, rozmery.typ)
        Catch ex As Exception
            Chyby.Show("Zle zadaný rozmer")
            Throw New Exception
        End Try


        Return rozmery

    End Function

    Private Function prijemka_databaza() As Boolean
        If TextBox15.Text.Length = 0 Then
            Chyby.Show("Nevyplnené údaje príjemky. Prosím vyplniť")
            Return False
        End If

        Dim datum As Date
        Try
            datum = DateTimePicker1.Value
        Catch ex As Exception
            Chyby.Show("Zlý dátum")
            Return False
        End Try

        Dim pozn As String = TextBox16.Text
        If String.IsNullOrEmpty(pozn) Then
            pozn = "-"
        End If

        Dim reklamacia As Boolean = CheckBox1.Checked


        Dim zakazka As String = TextBox11.Text & "/" & NumericUpDown1.Value

        Dim con As New SqlConnection

        Dim id As String = VydajkyTableAdapter.byNazov(vydajka_nazov).ToString

        Dim sql As String
        If id = "" Then

            SQL_main.AddCommand("INSERT INTO Vydajky (Nazov, Datum, Vyhotovil, Poznamka, Zakazka_ID) VALUES('" + vydajka_nazov() + "','" + datum.ToString("yyyy-MM-dd") + "','" + TextBox15.Text + "','" + pozn + "', (SELECT ID FROM Zakazka WHERE pocet = 1 AND Zakazka = '" & zakazka & "')  )")
            If reklamacia Then
                Dim zamestnanec_ID As String = ComboBox1.SelectedValue
                SQL_main.AddCommand("INSERT INTO Reklamacia_Vydajka (Vydajka_ID, Zamestnanec_ID) VALUES( (SELECT SCOPE_IDENTITY()) , " & zamestnanec_ID & ")")
            End If
            SQL_main.Odpal()
        Else
            If reklamacia Then
                Dim zamestnanec_ID As String = ComboBox1.SelectedValue
                SQL_main.AddCommand("UPDATE Reklamacia_Vydajka SET Zamestnanec_ID = " & zamestnanec_ID & " WHERE Vydajka_ID = " & id)
            End If
            SQL_main.AddCommand("UPDATE Vydajky SET Datum = '" & datum.ToString("yyyy-MM-dd") & "', Poznamka = '" & pozn & "' WHERE ID = " & id)
            SQL_main.Commit_Transaction()
        End If

        Return True
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        stuk()
    End Sub

    Public Sub stuk()
        If prijemka_databaza() = False Then 'AndAlso Form78.heslo <> Form78.admin Then
            Exit Sub
        End If

        Dim base As Integer
        If IsHranol() Then
            base = 1
        Else
            base = 0
        End If

        Try

            Dim druh, nazov As String
            druh = TextBox4.Text.Trim
            nazov = TextBox6.Text.Trim
            If TextBox8.Text.Length = 0 Then TextBox8.Text = 0
            If TextBox2.Text.Length = 0 Then TextBox2.Text = 0
            If TextBox3.Text.Length = 0 Then TextBox3.Text = 0
            If TextBox10.Text.Length = 0 Then TextBox10.Text = 0
            If TextBox17.Text.Length = 0 Then TextBox17.Text = 0


            If druh.Length = 0 Or nazov.Length = 0 Then
                Chyby.Show("Niečo nie je zadané druh alebo nazov")
                Exit Sub
            End If

            For i As Integer = 0 To DataGridView4.Rows.Count - 1

                Dim kusov, kusov_pouzil, kusov_zvysku As Integer

                Try

                    kusov = DataGridView4.Rows(i).Cells(base + 1).Value
                    kusov = 0 - kusov
                    kusov_pouzil = TextBox13.Text
                    kusov_zvysku = DataGridView4.Rows(i).Cells(base + 4).Value

                Catch ex As Exception
                    Chyby.Show("Zle zadaný počet kusov")
                    Exit Sub
                End Try

                Dim velkost As String = DataGridView4.Rows(i).Cells(base).Value
                Dim velkost_zvysok As String = DataGridView4.Rows(i).Cells(3).Value

                Dim rozmery_zvysok As Rozmery
                Dim rozmery As Rozmery
                Try
                    If (IsHranol()) Then
                        Dim typ As String = DataGridView4.Rows(i).Cells(0).Value
                        rozmery = get_rozmery(TextBox8.Text, TextBox10.Text, TextBox3.Text, TextBox2.Text, typ, velkost)
                        rozmery_zvysok = get_rozmery(TextBox8.Text, TextBox10.Text, TextBox3.Text, TextBox2.Text, typ, velkost_zvysok)
                    Else
                        rozmery = get_rozmery(TextBox8.Text, TextBox10.Text, TextBox3.Text, velkost, typ_slovom)
                        rozmery_zvysok = get_rozmery(TextBox8.Text, TextBox10.Text, TextBox3.Text, velkost_zvysok, typ_slovom)
                    End If
                    If rozmery_zvysok.velkost < 0 Then
                        Chyby.Show("Použitá dĺžka je väčšia ako na sklade")
                    End If
                Catch ex As Exception
                    Exit Sub
                End Try


                Dim vydajka_id As Integer = VydajkyTableAdapter.byNazov(vydajka_nazov)
                Dim material As Material_SQL = New Material_SQL(druh, nazov, rozmery.sirka, rozmery.rozmer, rozmery.s_rozmer, rozmery.velkost, kusov, rozmery.typ)
                Dim material_id As Integer = material.save

                Dim material_zvysok As Material_SQL = New Material_SQL(druh, nazov, rozmery_zvysok.sirka, rozmery_zvysok.rozmer, rozmery_zvysok.s_rozmer, rozmery_zvysok.velkost, kusov_zvysku, rozmery_zvysok.typ)
                Dim material_zobrat_id As String = "NULL"
                material_zobrat_id = material_zvysok.save

                Dim material_plochac_zvysok As Material_SQL = Nothing
                Dim material_plochac As Material_SQL = Nothing
                If RadioButton2.Checked Then
                    material_plochac = New Material_SQL(druh, nazov, -1, -1, -1, rozmery.velkost, rozmery.rozmer * rozmery.sirka * kusov, rozmery.typ)
                    Dim material_plochac_id As String = "NULL"
                    material_plochac_id = material_plochac.save
                    material_plochac_zvysok = New Material_SQL(druh, nazov, -1, -1, -1, rozmery_zvysok.velkost, rozmery_zvysok.sirka * rozmery_zvysok.rozmer * kusov_zvysku, rozmery_zvysok.typ)
                    Dim material_plochac_zvysok_id As String = "NULL"
                    material_plochac_zvysok_id = material_plochac_zvysok.save
                End If

                Me.MaterialVydajkaBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' ", RotekDataSet.Material_Vydajka.Vydajka_IDColumn, vydajka_id, RotekDataSet.Material_Vydajka.Material_IDColumn, material_id)
                If DataGridView1.RowCount = 1 Then
                    If DataGridView1.Rows(0).Cells("mat_zvysok").Value.ToString <> "" Then
                        Debug.WriteLine(DataGridView1.Rows(0).Cells("mat_zvysok").Value.ToString + "|||")
                        Dim material_zvysok_povodny As Material_SQL = New Material_SQL(DataGridView1.Rows(0).Cells("mat_zvysok").Value)
                        Dim material_zvysok_povodny_kusov As Integer = DataGridView1.Rows(0).Cells(3).Value
                        If RadioButton2.Checked Then
                            Dim material_zvysok_povodny_plochac As Material_SQL = material_zvysok_povodny.GetPlochac
                            material_zvysok_povodny_plochac.add_to_stock(0 - material_zvysok_povodny_kusov * material_zvysok_povodny.sirka * material_zvysok_povodny.rozmer)
                        Else
                            material_zvysok_povodny.add_to_stock(0 - material_zvysok_povodny_kusov)
                        End If
                    End If

                    SQL_main.Odpalovac("UPDATE Material_Vydajka SET Ks=" & kusov & ", Material_zvysok_ID = " & material_zobrat_id & " , Zvysok_ks = " & (kusov_zvysku) & ", Ks1Ks = " & kusov_pouzil & " WHERE Vydajka_ID = (SELECT ID FROM Vydajky WHERE Nazov='" + vydajka_nazov() + "') AND Material_ID = " & material_id)
                    If RadioButton2.Checked Then
                        material_plochac_zvysok.add_to_stock(kusov_zvysku * material_plochac.sirka * material_plochac.rozmer)
                        kusov = kusov - DataGridView1.Rows(0).Cells(3).Value
                        material_plochac.add_to_stock(kusov * material.sirka * material.rozmer)
                    Else
                        material_zvysok.add_to_stock(kusov_zvysku)
                        kusov = kusov - DataGridView1.Rows(0).Cells(3).Value
                        material.add_to_stock(kusov)
                    End If
                    If TextBox9.Text = 0 Then
                        SQL_main.Odpalovac("DELETE FROM Material_Vydajka WHERE Vydajka_ID = " & vydajka_id & " AND Material_ID = " & material_id)
                    End If
                Else

                    'If material_zvysok.velkost = 0 Then
                    'SQL_main.Odpalovac("INSERT INTO Material_Vydajka (Vydajka_ID, Material_ID, Ks) VALUES ( " & vydajka_id & " , " & material_id & " , " & kusov & " )")
                    'Else
                    SQL_main.Odpalovac("INSERT INTO Material_Vydajka (Vydajka_ID, Material_ID, Ks, Material_zvysok_ID, Zvysok_ks, Ks1Ks) VALUES ( " & vydajka_id & " , " & material_id & " , " & kusov & " , " & material_zobrat_id & " , " & (kusov_zvysku) & " , " & kusov_pouzil & " )")
                    'End If


                    If RadioButton2.Checked Then
                        material_plochac_zvysok.add_to_stock(kusov_zvysku * material.rozmer * material.sirka)
                        material_plochac.add_to_stock(kusov * material.sirka * material.rozmer)
                    Else
                        material_zvysok.add_to_stock(kusov_zvysku)
                        material.add_to_stock(kusov)
                    End If

                End If


            Next

        Catch ex As SystemException
            Chyby.Show(ex.ToString)
            Exit Sub
        End Try


        Try
            Napln_tabulku()

            TextBox4.Text = ""
            TextBox6.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = "1"
            TextBox10.Text = ""
            TextBox13.Text = "1"
            TextBox17.Text = ""
            VelkostFilter()

            hladaj()

            TextBox4.Focus()
            Me.Material_VydajkaTableAdapter.FillByVydajkaNazov(Me.RotekDataSet2.Material_Vydajka, vydajka_nazov)


        Catch ex As SystemException
            Chyby.Show(ex.ToString)
            Exit Sub
        End Try


    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                'TextBox2.Text = TextBox1.Text
                VelkostFilter()

            End If

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
                hladajDruh()
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
                hladajNazov()

                'TextBox6.Select(TextBox6.Text.Length, TextBox6.Text.Length)
            End If

        Catch ex As Exception
        End Try
    End Sub


    Private Sub TextBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.Enter
        'Me.Text = ""
        ' hladaj2(ListBox1, 0, "Druh")
    End Sub

    Private Sub hladaj()
        hladajDruh()
        hladajNazov()
    End Sub

    Private Sub hladajDruh()
        MaterialDruhBindingSource.Filter = "Nazov LIKE '%" & TextBox4.Text & "%'"
    End Sub
    Private Sub hladajNazov()
        MaterialNazovBindingSource.Filter = "Nazov LIKE '%" & TextBox6.Text & "%' AND Druh LIKE '%" & TextBox4.Text & "%'"
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

    Private Sub TextBox7_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            stuk()
        ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
            'Try
            '    TextBox9.Text = TextBox5.Text * TextBox7.Text
            'Catch ex As Exception
            'End Try
        End If
    End Sub


    Private Sub TextBox3_KeyUp_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then

                Try
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox6_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.Enter
        'hladajNazov()

    End Sub

    Private Sub TextBox5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub TextBox7_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'hladaj2(ListBox4, 4, "Rozmer")
    End Sub



    Private Sub TextBox8_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
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
        If (TextBox1.Text.Length <> 0) And (TextBox8.Text.Length <> 0) Then
            Try
                TextBox10.Text = TextBox8.Text / TextBox9.Text
                Dim hmotnost As Double = TextBox10.Text
                Dim velkost As Double = TextBox1.Text
                Dim hustota As Double
                Dim typ As String = typ_slovom()

                TextBox2.Text = hustota
                'Chyby.Show(hustota)

            Catch ex As Exception
                '    Chyby.Show(ex.ToString)
            End Try

        End If
    End Sub

    Private Sub TextBox11_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)

        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try

                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox10_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)

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


            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox9_Leave(sender As System.Object, e As System.EventArgs) Handles TextBox9.Leave
        Dim i As Integer
        Try
            i = TextBox9.Text
            TextBox18.Text = i
        Catch ex As Exception
            TextBox9.Text = 1
        End Try
    End Sub

    Private Sub TextBox13_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                Try
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception


        End Try
    End Sub

    Private Sub TextBox9_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox9.TextChanged
        Try
        Catch ex As Exception
            TextBox8.Text = ""
            TextBox10.Text = ""
        End Try
        Try


        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox10_TextChanged(sender As System.Object, e As System.EventArgs)
        Try
            TextBox10.Text = TextBox10.Text.Replace(".", ",")

            If TextBox10.Text.IndexOf(",") <> TextBox10.Text.Length - 1 Then
                TextBox10.Text = Math.Floor(CDec(TextBox10.Text) * 1000) / 1000
            End If
            TextBox10.SelectionStart = TextBox10.Text.Length

        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox8_TextChanged(sender As System.Object, e As System.EventArgs)
        Try
            TextBox8.Text = TextBox8.Text.Replace(".", ",")
            If TextBox8.Text.IndexOf(",") <> TextBox8.Text.Length - 1 Then
                TextBox8.Text = Math.Floor(CDec(TextBox8.Text) * 1000) / 1000
            End If
            TextBox8.SelectionStart = TextBox8.Text.Length
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs)
        Try
            TextBox2.Text = TextBox2.Text.Replace(".", ",")
            If TextBox2.Text.IndexOf(",") <> TextBox2.Text.Length - 1 Then
                TextBox2.Text = Math.Floor(CDec(TextBox2.Text) * 1000) / 1000
            End If
            TextBox2.SelectionStart = TextBox2.Text.Length
        Catch ex As Exception
        End Try


    End Sub

    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs)
        Try
            TextBox3.Text = TextBox3.Text.Replace(".", ",")
            If TextBox3.Text.IndexOf(",") <> TextBox3.Text.Length - 1 Then
                TextBox3.Text = Math.Floor(CDec(TextBox3.Text) * 1000) / 1000
            End If
            TextBox3.SelectionStart = TextBox3.Text.Length

        Catch ex As Exception
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


    Private Sub TextBox1_Enter(sender As System.Object, e As System.EventArgs) Handles TextBox1.Leave
        getKusov()

    End Sub
    Private Sub getKusov()
        Try

            Dim druh, nazov, rozmer, sirka, s_rozmer, typ, velkost As String
            druh = TextBox4.Text
            nazov = TextBox6.Text
            velkost = TextBox1.Text

            typ = typ_slovom()
            If druh.Length = 0 Or nazov.Length = 0 Or rozmer.Length = 0 Then
                Exit Sub
            End If

            Dim material As Material_SQL
            If RadioButton2.Checked Then
                material = New Material_SQL(druh, nazov, -1, -1, -1, velkost, typ)
                'Label7.Text = "" + material.kusov + " mm2"
            Else
                material = New Material_SQL(druh, nazov, sirka, rozmer, s_rozmer, velkost, typ)
            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextBox4_Click(sender As System.Object, e As System.EventArgs) Handles TextBox4.Click
        TextBox4.Text = ""
        hladajDruh()
    End Sub

    Private Sub TextBox6_Click(sender As System.Object, e As System.EventArgs) Handles TextBox6.Click
        hladajNazov()

    End Sub

    Private Sub TextBox7_Click(sender As System.Object, e As System.EventArgs)

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

    Private Sub RadioButton1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            radio_visiblity(0, 1, 0, 1)
            Label12.Text = "Priemer [mm]"
        End If

    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        If RadioButton8.Checked = True Then
            radio_visiblity(1, 1, 0, 1)
            Label12.Text = "Hrúbka [mm]"
            Label10.Text = "Šírka [mm]"
            Label8.Text = "Dĺžka [mm]"

        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As System.Object, e As System.EventArgs)

        Try
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TextBox7_TextChanged(sender As System.Object, e As System.EventArgs)
    End Sub


    Private Sub TextBox5_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            stuk()
        ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
            'Try
            '    If TextBox9.Text <> "" And TextBox7.Text <> "" Then
            '        TextBox9.Text = TextBox5.Text * TextBox7.Text
            '    End If
            'Catch ex As Exception
            'End Try
        End If
    End Sub

    Private Sub TextBox5_Leave(sender As System.Object, e As System.EventArgs)
    End Sub

    Private Sub TextBox7_Leave(sender As System.Object, e As System.EventArgs)
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        Try

            If e.RowIndex = -1 Then
                Exit Sub
            End If
            If e.ColumnIndex = 14 Then
                data_click(e.RowIndex)
                TextBox9.Text = 0
                stuk()
            Else
                data_click(e.RowIndex)

            End If

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub data_click(riadok As Integer)
        Dim typ As String = DataGridView3.Rows(riadok).Cells(2).Value
        TextBox4.Text = DataGridView3.Rows(riadok).Cells(0).Value
        TextBox6.Text = DataGridView3.Rows(riadok).Cells(1).Value

        typ_oznac(typ)

        TextBox1.Text = DataGridView3.Rows(riadok).Cells(6).Value
        TextBox9.Text = DataGridView3.Rows(riadok).Cells(7).Value

        If DataGridView3.Rows(riadok).Cells(8).Value.ToString <> "" Then
            TextBox8.Text = DataGridView3.Rows(riadok).Cells(8).Value
            TextBox10.Text = DataGridView3.Rows(riadok).Cells(9).Value
            TextBox3.Text = DataGridView3.Rows(riadok).Cells(10).Value
            TextBox2.Text = DataGridView3.Rows(riadok).Cells(11).Value
        Else
            TextBox8.Text = DataGridView3.Rows(riadok).Cells(3).Value
            TextBox10.Text = DataGridView3.Rows(riadok).Cells(4).Value
            TextBox3.Text = DataGridView3.Rows(riadok).Cells(5).Value
            TextBox2.Text = DataGridView3.Rows(riadok).Cells(6).Value
        End If
        TextBox13.Text = DataGridView3.Rows(riadok).Cells(12).Value
        TextBox17.Text = TextBox13.Text * TextBox2.Text
        TextBox9.Focus()

    End Sub


    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            radio_visiblity(1, 1, 0, 0)
            Label8.Text = "Hrúbka [mm]"
            Label10.Text = "Dĺžka [mm]"
            Label12.Text = "Šírka [mm]"
            Label12.Hide()
            Label10.Hide()
            TextBox10.Hide()
            TextBox8.Hide()

            TextBox13.Hide()
            Label19.Hide()
            Label21.Hide()
            TextBox17.Hide()
            TextBox13.Text = "1"


        End If
    End Sub

    Private Sub radio_visiblity(sirka As Integer, rozmer As Integer, srozmer As Integer, kusov As Integer)
        Label8.Text = "Do dĺžky [mm]"
        TextBox13.Show()
        Label19.Show()
        Label21.Show()
        TextBox17.Show()

        If sirka = 1 Then
            Label10.Show()
            TextBox8.Text = ""
            TextBox8.Show()
        Else
            Label10.Hide()
            TextBox8.Text = "-1"
            TextBox8.Hide()
        End If
        If rozmer = 1 Then
            Label12.Show()
            TextBox10.Show()
            TextBox10.Text = ""
        Else
            Label12.Hide()
            TextBox10.Hide()
            TextBox10.Text = "-1"
        End If
        If srozmer = 1 Then
            Label9.Show()
            TextBox3.Show()
            TextBox3.Text = ""
        Else
            Label9.Hide()
            TextBox3.Text = "-1"
            TextBox3.Hide()
        End If
        'If kusov = 1 Then
        '    Label11.Text = "Takýchto kusov:"
        'Else
        '    Label11.Text = "Plocha [mm2]"
        'End If

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            radio_visiblity(1, 1, 1, 1)
            Label10.Text = "d priemer  (d)[mm]"
            Label12.Text = "D priemer (D)[mm]"


        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            radio_visiblity(0, 1, 0, 1)
            Label12.Text = "Šírka [mm]"
        End If
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked = True Then
            radio_visiblity(1, 1, 1, 1)
            Label10.Text = "a [mm]"
            Label12.Text = "b [mm]"
        End If
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        If RadioButton6.Checked = True Then
            radio_visiblity(1, 1, 1, 1)
            Label10.Text = "a [mm]"
            Label12.Text = "b [mm]"
        End If
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        If RadioButton7.Checked = True Then
            radio_visiblity(1, 1, 1, 1)
            Label10.Text = "a [mm]"
            Label12.Text = "b [mm]"
        End If
    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs)
        Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        TextBox2.Text = TextBox1.Text
    End Sub


    Private Sub TextBox14_Leave(sender As Object, e As EventArgs)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)
        TextBox2.Text = TextBox1.Text
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox11_Leave(sender As Object, e As EventArgs) Handles TextBox11.Leave
        Try
            Dim x As Integer = TextBox11.Text
            TextBox11.Text = Format(x, "0000")
        Catch ex As Exception
            'Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TextBox12_Leave(sender As Object, e As EventArgs) Handles TextBox12.Leave
        Try
            Dim s As String = TextBox12.Text
            Dim x As Integer = s.Replace("V ", "")
            TextBox12.Text = "V " & Format(x, "0000")
            '   MessageBox.Show(s & "  " & s.IndexOf("/" & Year(Now)) & " " & ("/" & Year(Now)))
        Catch ex As Exception
            'Chyby.Show(ex.ToString)
        End Try
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox1.Show()
            Label16.Show()
        Else
            ComboBox1.Hide()
            Label16.Hide()
        End If

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        getKusov()

    End Sub

    Private Sub TextBox6_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TextBox6.MouseDoubleClick
        TextBox6.Text = ""
        hladajNazov()

    End Sub

    Private Sub TextBox1_Enter_1(sender As Object, e As EventArgs) Handles TextBox1.Enter
        VelkostFilter()
    End Sub
    Private Sub VelkostFilter()
        Dim dd As DataTable = New DataTable
        Dim druh, nazov As String
        Dim rozmer, sirka, srozmer, velkost As Integer

        Try
            druh = TextBox4.Text
            nazov = TextBox6.Text
        Catch ex As Exception
            Exit Sub
        End Try

        velkost = 0
        If TextBox1.Text.Length > 0 Then
            Try
                velkost = TextBox1.Text
            Catch ex As Exception
                Exit Sub
            End Try
        End If

        Dim sql As String = "SELECT DISTINCT m.Velkost FROM Material m JOIN MaterialVseobecne mv ON mv.ID = m.Material_ID JOIN MaterialNazov mn ON mn.Vseobecne_ID = mv.ID JOIN MaterialDruh md ON md.ID = mn.Druh_ID WHERE md.Nazov = '" & druh & "' AND mn.Nazov ='" & nazov & "' AND m.Typ = '" + typ_slovom() + "' AND (m.Sirka = " & sirka & " OR m.Sirka = -1) AND (m.Rozmer = " & rozmer & " OR m.Rozmer = -1) AND (m.S_rozmer = " & srozmer & " OR m.S_rozmer = -1) AND (m.Velkost >= " & velkost & " OR m.Velkost = -1) ORDER BY m.Velkost"
        System.Console.WriteLine(sql)
        SQL_main.List(sql, dd)

    End Sub


    Private Sub ListBox3_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox3.MouseClick
        If ListBox3.SelectedItems.Count > 0 Then
            TextBox6.Text = ListBox3.SelectedValue
            TextBox6.Focus()
            TextBox6.Select(TextBox6.Text.Length, 0)
        End If
    End Sub

    Private Sub ListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseClick
        Try

            Dim tex As String
            j = 1
            tex = ListBox1.Text
            TextBox4.Text = tex
            TextBox4.Focus()
            TextBox4.Select(0, TextBox4.Text.Length)
            'TextBox4.SelectionStart = TextBox4.Text.Length
            hladajNazov()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListBox2_MouseClick(sender As Object, e As MouseEventArgs)
        Try
            getKusov()
            TextBox1.Focus()
            TextBox1.Select(0, TextBox1.Text.Length)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DataGridView3_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView3.CellFormatting
        If e.Value Is Nothing OrElse e.Value.ToString <> "-1" Then
        Else
            e.Value = ""
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    End Sub

    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged
        Try
            TextBox17.Text = TextBox13.Text * TextBox2.Text

        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox2_KeyUp_1(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyUp
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                TextBox17.Text = TextBox2.Text * TextBox13.Text

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox17_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox17.KeyUp
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                stuk()
            ElseIf ((e.KeyCode > 64) And (e.KeyCode < 91)) Or ((e.KeyCode < 106) And (e.KeyCode > 95)) Or e.KeyCode = 8 Then
                TextBox2.Text = TextBox17.Text / TextBox13.Text
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        nacitaj_moznosti()

    End Sub

    Private Sub nacitaj_moznosti()
        Try

            Dim druh As String = TextBox4.Text

            Dim nazov As String = TextBox6.Text

            Dim sirka As Integer = TextBox8.Text
            Dim rozmer As Integer = TextBox10.Text
            Dim srozmer As Integer = TextBox3.Text
            Dim velkostMIN As Integer = TextBox2.Text
            Dim kusov As Integer = TextBox13.Text

            Dim material As Material_SQL = New Material_SQL(druh, nazov, sirka, rozmer, srozmer, velkostMIN, kusov, typ_slovom)

            Dim dd As DataTable = New DataTable()
            Material_SQL.getChoices(dd, material)

            DataGridView2.Rows.Clear()

            If (IsHranol()) Then
                If DataGridView2.Columns.Count = 3 Then
                    addHranolColumns()
                End If
            Else
                If DataGridView2.Columns.Count = 5 Then
                    removeHranolColumns()
                    DataGridView2.Size = New Size(308, 150)
                End If
            End If

            For i As Integer = 0 To dd.Rows.Count - 1
                DataGridView2.Rows.Add()
                If (typ_slovom() = "Hranol") Then

                    Dim sirkaGet As String = dd.Rows(i)(0).ToString()
                    Dim rozmerGet As String = dd.Rows(i)(1).ToString()
                    Dim velkostGet As String = dd.Rows(i)(2).ToString()

                    If (sirkaGet <> sirka) Then
                        DataGridView2.Rows(i).Cells(0).Value = sirkaGet
                    ElseIf (rozmerGet <> rozmer) Then
                        DataGridView2.Rows(i).Cells(1).Value = rozmerGet
                    ElseIf (velkostGet <> velkostMIN) Then
                        DataGridView2.Rows(i).Cells(2).Value = velkostGet
                    Else
                        DataGridView2.Rows(i).Cells(0).Value = sirkaGet
                        DataGridView2.Rows(i).Cells(1).Value = rozmerGet
                        DataGridView2.Rows(i).Cells(2).Value = velkostGet
                    End If

                    DataGridView2.Rows(i).Cells(3).Value = dd.Rows(i)(3).ToString()
                    DataGridView2.Rows(i).Cells(4).Value = dd.Rows(i)(3).ToString()

                Else
                    DataGridView2.Rows(i).Cells(0).Value = dd.Rows(i)(2).ToString()
                    DataGridView2.Rows(i).Cells(1).Value = dd.Rows(i)(3).ToString()
                    DataGridView2.Rows(i).Cells(2).Value = dd.Rows(i)(3).ToString()
                End If


            Next

        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub

    Private Sub addHranolColumns()
        addColumnDGV2("Strana", "Strana", DataGridView4)
        addColumnDGV2("Rozmer", "Rozmer", DataGridView2)
        addColumnDGV2("Sirka", "Šírka", DataGridView2)

    End Sub
    Private Sub removeHranolColumns()
        DataGridView2.Columns.RemoveAt(0)
        DataGridView2.Columns.RemoveAt(0)
        DataGridView4.Columns.RemoveAt(0)

    End Sub

    Private Sub addColumnDGV2(name As String, text As String, dgv As DataGridView)
        Dim dgvc As DataGridViewColumn = New DataGridViewTextBoxColumn()
        dgvc.HeaderText = text
        dgvc.Name = name
        dgvc.Width = 80
        dgv.Size = New Size(dgv.Width + 80, dgv.Height)
        dgv.Columns.Insert(0, dgvc)

        'DataGridView2.Columns.Add(name, text)


    End Sub


    Private Sub TextBox17_TextChanged(sender As Object, e As EventArgs) Handles TextBox17.TextChanged
        sum_velkost()
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If

        Dim velkost = TextBox2.Text
        Label2.Text = "Veľkosť: [mm]"

        If (typ_slovom() = "Hranol") Then
            If DataGridView2.Rows(e.RowIndex).Cells(2).Value IsNot Nothing Then
                TextBox1.Text = DataGridView2.Rows(e.RowIndex).Cells(2).Value.ToString
            ElseIf DataGridView2.Rows(e.RowIndex).Cells(1).Value IsNot Nothing Then
                Label2.Text = "Rozmer: [mm]"
                velkost = TextBox10.Text
                TextBox1.Text = DataGridView2.Rows(e.RowIndex).Cells(1).Value.ToString
            ElseIf DataGridView2.Rows(e.RowIndex).Cells(0).Value IsNot Nothing Then
                Label2.Text = "Šírka: [mm]"
                velkost = TextBox8.Text
                TextBox1.Text = DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString
            End If
        Else
            TextBox1.Text = DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString
        End If


        Dim need_ks As Integer = Math.Ceiling((TextBox13.Text) / Math.Floor(TextBox1.Text / velkost))
        TextBox9.Text = need_ks

        Dim zJedneho As Integer = (Math.Floor(TextBox1.Text / velkost))

        If (TextBox13.Text Mod zJedneho = 0) Then
            If (TextBox1.Text Mod velkost = 0) Then
                TextBox18.Text = "0"
                TextBox5.Text = "0"
            Else
                TextBox18.Text = need_ks
                TextBox5.Text = TextBox1.Text - velkost
            End If
        Else
            TextBox5.Text = TextBox1.Text - velkost * (TextBox13.Text - Math.Floor(TextBox13.Text / zJedneho) * zJedneho)
            TextBox18.Text = "1"
        End If


    End Sub
    Private Function IsHranol() As Boolean
        If (RadioButton8.Checked) Then
            Return True
        Else
            Return False
        End If

    End Function
    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim i As Integer = DataGridView4.Rows.Count
        Dim base As Integer = 0

        DataGridView4.Rows.Add()

        If IsHranol() Then
            base = 1
            DataGridView4.Rows(i).Cells(base - 1).Value = Label2.Text.Substring(0, Label2.Text.Length - 6)
        End If

        DataGridView4.Rows(i).Cells(base).Value = TextBox1.Text
        DataGridView4.Rows(i).Cells(base + 1).Value = TextBox9.Text
        DataGridView4.Rows(i).Cells(base + 3).Value = TextBox5.Text
        DataGridView4.Rows(i).Cells(base + 4).Value = TextBox18.Text

        Dim zJedneho As Integer = (Math.Floor(TextBox1.Text / TextBox2.Text))

        Dim celkovo As Integer = (TextBox9.Text - TextBox18.Text) * zJedneho * TextBox2.Text + (TextBox18.Text * Math.Floor((TextBox1.Text - TextBox5.Text) / TextBox2.Text) * TextBox2.Text)
        If celkovo > TextBox2.Text * TextBox13.Text Then
            celkovo = TextBox2.Text * TextBox13.Text
        End If
        DataGridView4.Rows(i).Cells(base + 2).Value = celkovo

        sum_velkost()
    End Sub


    Public Sub sum_velkost()
        Try
            Dim base As Integer
            If (IsHranol()) Then
                base = 1
            Else
                base = 0
            End If
            Dim sucet As Integer = 0
            For i As Integer = 0 To DataGridView4.Rows.Count - 1
                sucet += DataGridView4.Rows(i).Cells(base + 2).Value
            Next
            Label6.Text = sucet
            Label7.Text = TextBox2.Text * TextBox13.Text - sucet
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView4_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView4.CellMouseClick
        If e.Button = MouseButtons.Right Then
            DataGridView4.Rows.RemoveAt(e.RowIndex)
            If IsHranol() Then
                sum_velkost()
            Else
                sum_velkost()
            End If
        End If
    End Sub


End Class

Public Class Rozmery
    Public sirka As Integer
    Public rozmer As Integer
    Public s_rozmer As Integer
    Public velkost As Integer
    Public typ As String
    Public ok As Boolean
End Class

