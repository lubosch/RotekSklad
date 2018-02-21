Public Class Forma1
    Private onazov As String
    Private Sub fodb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'TODO: This line of code loads data into the 'Ds1.faktury' table. You can move, or remove it, as needed.
        Me.FakturyTableAdapter.Fill(Me.Ds1.faktury)



    End Sub

    Private Sub cisti()
        TBcislo.Text = ""
        TBadr.Text = ""
        TBdat.Value = Date.Today
        TBpsc.Text = ""
        TBpracovnik.Text = ""
        TBphod.Text = ""
        TBcenahod.Text = ""
        TBcenapraca.Text = ""
        TBsuma.Text = ""
        TBcislo.Enabled = False
        btaddk.Enabled = False
        btadd.Enabled = True
        btupd.Enabled = False
        btdel.Enabled = False
    End Sub
    Private Sub cistimat()
        TBMjcena.Text = ""
        TBMnazov.Text = ""
        TBMpocet.Text = ""
        BTMaddk.Enabled = False
        BTMadd.Enabled = True
        BTMupd.Enabled = False
        BTMdel.Enabled = False
    End Sub
    Private Sub pocsuma()
        Dim con As New System.Data.SqlClient.SqlConnection
        Dim com As New System.Data.SqlClient.SqlCommand
        con.ConnectionString = "Data Source=BIGSPRINGFIELD\MSSMLBIZ;Initial Catalog=kdat;Integrated Security=True"
        com.Connection = con
        com.CommandText = "EXECUTE dbo.pocitajsumu '" & TBcislo.Text & "'"
        con.Open()
        com.ExecuteNonQuery()
        con.Close()
        Me.FakturyTableAdapter.Fill(Me.Ds1.faktury)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btaddk.Click
        Try

            FakturyTableAdapter.Insert(TBcislo.Text, TBdat.Value, TBadr.Text, TBpsc.Text, TBpracovnik.Text, TBphod.Text, TBcenahod.Text, TBphod.Text * TBcenahod.Text, TBphod.Text * TBcenahod.Text)

        Catch
            MsgBox("Nastala chyba pri zápise.", MsgBoxStyle.Critical, "Chyba!!!")
        End Try
        Me.FakturyTableAdapter.Fill(Me.Ds1.faktury)
        TBcislo.Enabled = False
        btaddk.Enabled = False
        btadd.Enabled = True
        btupd.Enabled = False
        btdel.Enabled = False
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick

        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        TBcislo.Text = DataGridView1.Item(0, i).Value
        TBadr.Text = DataGridView1.Item(2, i).Value
        TBpsc.Text = DataGridView1.Item(3, i).Value
        TBdat.Value = DataGridView1.Item(1, i).Value
        TBpracovnik.Text = DataGridView1.Item(4, i).Value
        TBphod.Text = DataGridView1.Item(5, i).Value
        TBcenahod.Text = DataGridView1.Item(6, i).Value
        TBcenapraca.Text = DataGridView1.Item(7, i).Value
        TBsuma.Text = DataGridView1.Item(8, i).Value

        btadd.Enabled = True
        btupd.Enabled = True
        btaddk.Enabled = False
        TBcislo.Enabled = False
        btdel.Enabled = True
        Me.MaterialTableAdapter.FillBy(Me.Ds1.material, TBcislo.Text)
        cistimat()
    End Sub

    Private Sub btadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btadd.Click
        TBcislo.Enabled = True
        btaddk.Enabled = True
        btadd.Enabled = False
        btdel.Enabled = False
        btupd.Enabled = False
        TBcislo.Text = ""
        TBadr.Text = ""
        TBdat.Value = Date.Today
        TBpsc.Text = ""
        TBpracovnik.Text = ""
        TBphod.Text = ""
        TBcenahod.Text = ""
        TBcenapraca.Text = ""
        TBsuma.Text = ""
    End Sub

    Private Sub btupd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btupd.Click
        FakturyTableAdapter.UpdateQuery(TBdat.Value, TBadr.Text, TBpsc.Text, TBpracovnik.Text, TBphod.Text, TBcenahod.Text, TBcislo.Text)
        pocsuma()
        Me.FakturyTableAdapter.Fill(Me.Ds1.faktury)
        cisti()
    End Sub

    Private Sub btdel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btdel.Click
        FakturyTableAdapter.DeleteQuery(TBcislo.Text)
        Me.FakturyTableAdapter.Fill(Me.Ds1.faktury)
        cisti()
    End Sub

    Private Sub BThl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BThl.Click
        Dim s, f As String
        If TBhl.Visible = True Then
            s = TBhl.Text



            f = ComboBox1.Text
            If f = "*like*" Then
                s = "'*" & s & "*'"
                f = "like"
            ElseIf f = "like" Then
                s = "'" & s & "'"
            ElseIf f = "like*" Then
                s = "'" & s & "*'"
                f = "like"
            End If
            Try
                If FakturyBindingSource.Filter = "" Then
                    FakturyBindingSource.Filter = "(" & Cstlpec.Text & " " & f & " " & s & ")"
                Else
                    FakturyBindingSource.Filter &= " and (" & Cstlpec.Text & " " & f & " " & s & ")"

                End If
            Catch
                MsgBox("chybne zadaný filter", MsgBoxStyle.Critical, "chyba!")
                FakturyBindingSource.Filter = ""
            End Try

        Else
            f = ComboBox1.Text
            s = DThl.Text
            If f = "*like*" Then
                s = "*" & s & "*"
                f = "like"
            ElseIf f = "like" Then
                s = "" & s & ""
            ElseIf f = "like*" Then
                s = "" & s & "*"
                f = "like"
            End If
            Try
                If FakturyBindingSource.Filter = "" Then
                    FakturyBindingSource.Filter = "(" & Cstlpec.Text & " " & f & " '" & s & "')"
                Else
                    FakturyBindingSource.Filter &= " and (" & Cstlpec.Text & " " & f & " '" & s & "')"

                End If
            Catch
                MsgBox("chybne zadaný filter", MsgBoxStyle.Critical, "chyba!")
                FakturyBindingSource.Filter = ""
            End Try
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        FakturyBindingSource.Filter = ""
    End Sub

    Private Sub Cstlpec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cstlpec.SelectedIndexChanged
        If Cstlpec.Text <> "datum" Then
            TBhl.Visible = True
            DThl.Visible = False
        Else
            TBhl.Visible = False
            DThl.Visible = True
        End If
    End Sub

    Private Sub BTMadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTMadd.Click
        cistimat()
        BTMaddk.Enabled = True
        BTMadd.Enabled = False
        BTMdel.Enabled = False
        BTMupd.Enabled = False
    End Sub

    Private Sub BTMaddk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTMaddk.Click
        Try
            MaterialTableAdapter.InsertQuery(TBcislo.Text, TBMnazov.Text, TBMjcena.Text, TBMpocet.Text)
        Catch
            MsgBox("Nastala chyba pri zápise.", MsgBoxStyle.Critical, "Chyba!!!")
        End Try
        Me.MaterialTableAdapter.FillBy(Me.Ds1.material, TBcislo.Text)
        cistimat()
        pocsuma()
    End Sub

    Private Sub DataGridView2_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.RowHeaderMouseClick
        Dim i As Integer
        i = DataGridView2.CurrentRow.Index
        TBMnazov.Text = DataGridView2.Item(1, i).Value
        TBMjcena.Text = DataGridView2.Item(2, i).Value
        onazov = DataGridView2.Item(1, i).Value
        TBMpocet.Text = DataGridView2.Item(3, i).Value
        BTMadd.Enabled = True
        BTMupd.Enabled = True
        BTMaddk.Enabled = False
        BTMdel.Enabled = True
    End Sub

    Private Sub BTMupd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTMupd.Click
        MaterialTableAdapter.UpdateQuery(TBMnazov.Text, TBMjcena.Text, TBMpocet.Text, TBcislo.Text, onazov)
        Me.MaterialTableAdapter.FillBy(Me.Ds1.material, TBcislo.Text)
        cistimat()
        pocsuma()
    End Sub


    Private Sub BTMdel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTMdel.Click
        MaterialTableAdapter.DeleteQuery(TBcislo.Text, TBMnazov.Text)
        Me.MaterialTableAdapter.FillBy(Me.Ds1.material, TBcislo.Text)
        cistimat()
        pocsuma()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim con As New System.Data.SqlClient.SqlConnection
        Dim com As New System.Data.SqlClient.SqlCommand
        con.ConnectionString = "Data Source=BIGSPRINGFIELD\MSSMLBIZ;Initial Catalog=kdat;Integrated Security=True"
        com.Connection = con
        com.CommandText = "EXECUTE dbo.pocitajsumu '0001'"
        con.Open()
        com.ExecuteNonQuery()
        con.Close()
        Me.FakturyTableAdapter.Fill(Me.Ds1.faktury)
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        reportfakt.Show()
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
