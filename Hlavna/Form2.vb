Public Class Form2

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = vbCrLf & "Akékoľvek nejasnosti zasielajte na mail Lubomir.Vnenk@zoho.com" & vbCrLf & "S užívateľom sa narába tak, že 2x kliknete na meno v tabuľke" & vbCrLf & _
        "Akékoľvek chyby treba hneď nahlásiť na ten istý mail a opraví sa to bez poškodenia databázy" & vbCrLf & _
        "Takisto nápady či už na vizualické alebo programové zlepšováky treba poslať na mail" & vbCrLf & _
        "Nástroje sa vracajú že namiesto kladného čísla napíšete záporné do kolonky dať nástroj" & vbCrLf & _
        "Nebolo to nastavené pre iné rozlíšeni, preto sa môžu vyskytnúť komplikácie" & vbCrLf & vbCrLf &
        "Registrované pre firmu ROTEK s.r.o® "
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class