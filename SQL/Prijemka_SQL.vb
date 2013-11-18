Public Class Prijemka_SQL
    Public id as String
    Public datum As Date
    Public datumDL As Date
    Public nazov As String
    Public poznamka As String
    Public vyhotovil As String
    Public dodaciList As String
    Public dodavatel_id As String
    Public dodavatel As String

    Public Sub New()

    End Sub

    Public Sub New(nazov As String)
        find_by_nazov(nazov)
    End Sub


    Public Shared Function materialByName(nazov As String) As DataTable
        Dim dd As DataTable = New DataTable
        SQL_main.AddCommand("SELECT md.Nazov Druh, mn.Nazov Názov, m.Typ Typ, m.Sirka Šírka, m.Rozmer Rozmer, m.S_rozmer 'S-rozmer', m.Velkost Veľkosť, mp.ks Kusov, mp.KsKg 'Kg/1ks', mp.Kg Kg, mp.CenaKg 'Cena/1Kg', mp.Cena 'Cena' FROM Material_Prijemka mp")
        SQL_main.AddCommand("~JOIN Prijemky p ON p.ID = Prijemka_ID ")
        SQL_main.AddCommand("~JOIN Material m ON m.ID = mp.Material_ID")
        SQL_main.AddCommand("~JOIN MaterialVseobecne mv ON mv.ID = m.Material_ID")
        SQL_main.AddCommand("~JOIN MaterialNazov mn ON mn.ID = mv.Nazov_ID")
        SQL_main.AddCommand("~JOIN MaterialDruh md ON md.ID = mn.Druh_ID")
        SQL_main.AddCommand("~WHERE p.Nazov = '" & nazov & "'")
        SQL_main.Commit_List(dd)


        Return dd
    End Function


    Public Sub find_by_nazov(nazovv As String)
        Dim dd As DataTable = New DataTable
        SQL_main.List("SELECT TOP 1 p.*, d.Nazov Dodavatel FROM Prijemky p LEFT JOIN Dodavatel d ON d.ID = p.Dodavatel_ID WHERE p.Nazov = '" & nazovv & "' ", dd)
        If dd.Rows.Count = 0 Then
            id = ""
            Exit Sub
        End If

        Me.id = dd.Rows(0).Item("ID")
        Me.datum = dd.Rows(0).Item("Datum")
        Me.datumDL = dd.Rows(0).Item("Datum_DL")
        Me.nazov = dd.Rows(0).Item("Nazov")
        Me.poznamka = dd.Rows(0).Item("Poznamka").ToString
        Me.vyhotovil = dd.Rows(0).Item("Vyhotovil")
        Me.dodaciList = dd.Rows(0).Item("DodaciList").ToString
        Me.dodavatel_id = dd.Rows(0).Item("Dodavatel_ID").ToString
        Me.dodavatel = dd.Rows(0).Item("Dodavatel").ToString

    End Sub

    Public Shared Function prijemkyByMaterial(filter As String) As DataTable

        Dim dd As DataTable = New DataTable

        filter = filter.Replace("Nazov", "mn.Nazov")
        filter = filter.Replace("Kusov", "mp.Ks")
        filter = filter.Replace("Typ", "m.Typ")
        filter = filter.Replace("Druh", "md.Nazov")

        SQL_main.AddCommand("SELECT      DISTINCT(p.Nazov), p.DodaciList 'Dodaci list', p.Datum_DL 'Dátum DL', d.Nazov Dodavatel, p.Vyhotovil Vyrobil, p.Datum 'Datum pridania', p.Poznamka FROM ")
        SQL_main.AddCommand("~           Prijemky p LEFT OUTER JOIN")
        SQL_main.AddCommand("~           Dodavatel d ON d.ID = p.Dodavatel_ID JOIN")
        SQL_main.AddCommand("~           Material_Prijemka AS mp ON mp.Prijemka_ID = p.ID INNER JOIN")
        SQL_main.AddCommand("~           Material AS m ON m.ID = mp.Material_ID JOIN")
        SQL_main.AddCommand("~           MaterialVseobecne AS mvs ON mvs.ID = m.Material_ID JOIN")
        SQL_main.AddCommand("~           MaterialNazov AS mn ON mn.Vseobecne_ID = mvs.ID JOIN")
        SQL_main.AddCommand("~           MaterialDruh AS md ON md.ID = mn.Druh_ID ")

        If filter.Length > 0 Then
            SQL_main.AddCommand("~WHERE " & filter)
        End If
        SQL_main.AddCommand("~ORDER BY Nazov DESC")

        SQL_main.Commit_List(dd)
        Return dd

    End Function


End Class
