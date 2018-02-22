Public Class mzamestnanec

    Property crc As String
    Public x As Integer
    Shared Property Pocett As Integer

    Shared Property Nastroj As String

    Property menko As String

    Property prezvo As String

    Property spoluu As Integer

    Shared Property bmp As Integer

    Shared Property Nastroj2 As String

    Shared Property spoluq As Integer

    Shared Property Cenka As Double

    Shared Property vlast As String

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
    Public Sub rozmer()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height
        DataGridView1.Location = New Point(0, 169)
        DataGridView1.Size = New Size(469, sw - 170)
        DataGridView3.Location = New Point(469, 169)
        DataGridView3.Size = New Size(rww * 2 - 469, sw - 170)

        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString(crc, Label1.Font)
        Dim stred As Integer

        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New Point(rw, 20)
    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo
            Case Form78.admin
                DataGridView1.AllowUserToDeleteRows = True

                Button1.Show()
                Button4.Show()
                Button5.Show()
                Button6.Show()
            Case Form78.zakazkar
            Case Form78.skladnik
                Button1.Show()
                Button4.Show()
                Button5.Show()
                Button6.Show()
            Case Else
                Button1.Hide()
                Button4.Hide()
                Button5.Hide()
                Button6.Hide()
        End Select
    End Sub
    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button1.Hide()
        Button4.Hide()
        Button5.Hide()
        Button6.Hide()
        poverenie()


        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)

        Me.FirmyTableAdapter.Fill(Me.RotekDataSet.Firmy)

        TextBox1.Text = "Tu napíš meno nástroja na filter:"

        DataGridView2.Hide()
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        x = DataGridView1.RowCount
        Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Firmy.pocetColumn, 1, RotekDataSet.Firmy.MenoColumn, menko)
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.pocetColumn, 2, RotekDataSet.Rotek.MenprColumn, menko)
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)

        rozmer()
        Label1.Text = menko
        Me.FirmyBindingSource.Sort = "Nástroj"

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New nastf(menko)
        Dim w As Integer = 2
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
        f.Dispose()


    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox1.Focus()
        TextBox2.Text = ""
    End Sub

    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = ""
    End Sub
    Private Sub filtruj()
        Me.FirmyBindingSource.Filter = String.Format("{0} = '{1}' AND {2}='{3}' AND AND {4}LIKE'{5}%' AND {6} LIKE '{7}%'", RotekDataSet.Firmy.pocetColumn, 1, RotekDataSet.Firmy.MenoColumn, menko,RotekDataSet.Firmy.NástrojColumn, TextBox1.Text, RotekDataSet.Firmy.VelkostRColumn, TextBox2.Text)

    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        filtruj()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        bmp = 0
        Dim f As New msrotC(menko)
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
        f.Dispose()


        x = DataGridView1.RowCount
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        filtruj()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim f As New mpozicat
        f.TopLevel = True
        f.menko = menko
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim f As New mvratit
        f.TopLevel = True
        f.menko = menko
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
    End Sub


    Private Sub mzamestnanec_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        rozmer()

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Form78.exportovat(DataGridView1)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Form78.exportovat(DataGridView3)
    End Sub
End Class