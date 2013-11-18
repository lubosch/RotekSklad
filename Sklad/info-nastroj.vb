Public Class info_nastroj

    Property nastr2 As String
    Property nastr As String

    Property vlast As String

    Private Sub info_nastroj_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)

        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)

        Dim crc As String = nastr & " " & nastr2 & " - " & vlast
        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(crc, Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2
        Dim rw As String = me.Width / 2 - stred
        Label1.Location = New Point(rw, 10)
        Label1.Text = crc

        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}' AND {6}='{7}'", RotekDataSet.Rotek.pocetColumn, 1, RotekDataSet.Rotek.VlastnostColumn, vlast, RotekDataSet.Rotek.NástrojColumn, nastr, RotekDataSet.Rotek.VelkostRColumn, nastr2)
        Me.RotekBindingSource.Sort = "Kolko DESC"
        Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.Firmy.pocetColumn, 1, RotekDataSet.Firmy.NástrojColumn, nastr, RotekDataSet.Firmy.VelkostRColumn, nastr2)
        Me.RotekBindingSource.Sort = "Kolko DESC"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Form78.exportovat(DataGridView1)
    End Sub
End Class