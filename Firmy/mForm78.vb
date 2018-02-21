Public Class mForm78


    Public kks As Integer
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New mPridat
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
    End Sub
    Public Sub rozmer()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height
        DataGridView1.Size = New Size(400, sw - 190)
        DataGridView2.Location = New Point(400, 189)
        DataGridView2.Size = New Size(rww * 2 - 400, sw - 190)

        PictureBox2.Location = New Point(rww - 100, 40)
    End Sub
    Private Sub poverenie()
        Select Case Form78.heslo
            Case Form78.admin
                Button1.Show()
                Button2.Show()
            Case Form78.zakazkar
            Case Form78.skladnik
                Button1.Show()

            Case Else
                Button1.Hide()
                Button2.Hide()
        End Select
    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button1.Hide()
        Button2.Hide
        poverenie

        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
        
        Me.RotekBindingSource1.Filter = String.Format("{0} = '{1}' AND {2}='{3}'", RotekDataSet.Rotek.pocetColumn, 0, RotekDataSet.Rotek.MenoColumn, "Firma")
        Me.RotekBindingSource.Filter = String.Format("{0} = '{1}'", RotekDataSet.Rotek.pocetColumn, 2)

        rozmer()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim f As New mvymazať
        f.TopLevel = True

        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim f As New sklad
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.ShowDialog()
    End Sub
    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        kks = e.RowIndex
        Dim crc As String = ""
        Dim memo As String = ""
        Dim prezvo As String = ""

        Try
            memo = DataGridView1.Rows(kks).Cells(0).Value
        Catch ex As SystemException
        End Try

        Dim f As New zamestnanec
        f.TopLevel = True
        f.crc = memo
        f.menko = "Firma"
        f.prezvo = " od "
        f.Dock = DockStyle.None
        f.ShowDialog()
        Me.RotekTableAdapter.Fill(Me.RotekDataSet.Rotek)
    End Sub

    Private Sub mForm78_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        rozmer()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form78.exportovat(DataGridView1)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Form78.exportovat(DataGridView2)
    End Sub
End Class