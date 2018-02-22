Public Class bum

    Property Aa As Integer

    Private Sub bum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim aks As String
        aks = "Na sklade je už len: " & Aa & " nastrojov. Chcete mu ich aj tak dať?"

        Label1.Text = aks
    End Sub
End Class