Public Class Udaje
    Public druh As String
    Public nazov As String
    Public typ As String
    Public podzakazka As String
    Public srot As Double
    Public srotcena As Double
    Public sirka As Integer
    Public s_rozmer As Integer
    Public rozmer As Integer
    Public rozmerN As Integer
    Public s_rozmerN As Integer
    Public sirkaN As Integer
    Public velkostN As Integer
    Public velkost As Integer
    Public strata As Double
    Public kusov As Integer
    Public kusov2 As Integer
    Public slovo As String
    Public poradie As Integer
    Public objem As Double
    Public objemN As Double


    Public Sub New(ByVal sirka As Integer, ByVal rozmer As Integer, ByVal velkost As Integer, ByVal strata As Double, ByVal kusov As Integer, ByVal kusov2 As Integer, ByVal slovo As String, ByVal poradie As Integer, ByVal rrozmer() As Double, ByVal typ As String)
        Me.sirka = sirka
        Me.velkost = velkost
        Me.rozmer = rozmer
        Me.strata = strata
        Me.kusov = kusov
        Me.kusov2 = kusov2
        Me.poradie = poradie
        Me.slovo = slovo
        Me.sirkaN = rrozmer(0)
        Me.rozmerN = rrozmer(1)
        Me.velkostN = rrozmer(2)
        Me.s_rozmer = rrozmer(3)
        Me.typ = typ

    End Sub

    Public Sub New(ByVal sirka As Integer, ByVal rozmer As Integer, s_rozmer As String, ByVal velkost As Integer, ByVal druh As String, ByVal nazov As String, ByVal kusov As Integer, typ As String)
        Me.sirka = sirka
        Me.velkost = velkost
        Me.rozmer = rozmer
        Me.druh = druh
        Me.nazov = nazov
        Me.kusov = kusov
        Me.s_rozmer = s_rozmer
        Me.typ = typ
    End Sub
    Public Sub New(ByVal sirka As Integer, ByVal rozmer As Integer, s_rozmer As String, ByVal velkost As Integer, ByVal druh As String, ByVal nazov As String, ByVal kusov As Integer, typ As String, sirkaN As Integer, rozmerN As Integer, s_rozmerN As Integer, velkostN As Integer)
        Me.sirka = sirka
        Me.velkost = velkost
        Me.rozmer = rozmer
        Me.druh = druh
        Me.nazov = nazov
        Me.kusov = kusov
        Me.s_rozmer = s_rozmer
        Me.typ = typ
        Me.sirkaN = sirkaN
        Me.rozmerN = rozmerN
        Me.s_rozmerN = s_rozmerN
        me.velkostN=velkostN
    End Sub

End Class
