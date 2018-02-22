Imports System.Threading
Imports RotekIS
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Public Class sklad

    Shared Property nastr As String

    Shared Property p As Integer


    Public over As Integer

    Public tex, tex2 As String

    Shared Property sku As Integer

    Shared Property reg As String

    Shared Property v As Integer

    Shared Property cenaa As String

    Shared Property nastr2 As String

    Shared Property vlast As String



    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim hladaj1 As Thread
        'hladaj1 = New Thread(Sub() Invoke(Sub() fillasync()))
        'hladaj1.IsBackground = True
        'hladaj1.Start()

        'BackgroundWorker1.RunWorkerAsync()

        'Dim taskOne = Task.Factory.StartNew(Function() Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad))

        ' taskOne.Wait()
        '       System.Threading.Thread.Sleep(1000)

        'SkladBindingSource.Filter = Nothing


        Button1.Hide()
        Button5.Hide()
        DataGridView1.Columns(8).Visible = False
        poverenie()
        rozmer()
        'napln()
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        Me.SkladBindingSource.Sort = "Nastroj, VelkostS"
        'BackgroundWorker1.RunWorkerAsync()

        TextBox2.Text = ""
        Button3.Hide()
        TextBox1.Text = ""

    End Sub
    Private Sub fillasync()
        'Thread.Sleep(5000)

        If DataGridView1.InvokeRequired Then
            Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
            DataGridView1.Invoke(Sub() fillasync())
        End If
        Me.SkladBindingSource.Sort = "Nastroj, VelkostS"

        'napln()

    End Sub


    Private Sub napln()
        Dim ii As Integer = 0
        If Label7.InvokeRequired Then
            Label7.Invoke(Sub() napln())
            Exit Sub
        End If
        If DataGridView2.Visible = False Then
            For i As Integer = 0 To DataGridView1.RowCount - 1
                If DataGridView1.RowCount < 500 And DataGridView1.Rows(i).Cells(4).Value < 5 Then
                    If Math.Floor(i / 2) = Math.Ceiling(i / 2) Then
                        DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.DeepPink
                    Else
                        DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.HotPink
                    End If
                    ii = ii + 1
                End If
            Next
        Else
            For i As Integer = 0 To DataGridView2.RowCount - 1
                If DataGridView2.RowCount < 500 And DataGridView2.Rows(i).Cells(4).Value < 5 Then
                    If Math.Floor(i / 2) = Math.Ceiling(i / 2) Then
                        DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.DeepPink
                    Else
                        DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.HotPink
                    End If
                    ii = ii + 1
                    'ElseIf Math.Floor(i / 2) = Math.Ceiling(i / 2) Then
                    'DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightGray
                End If
            Next

        End If

        Label7.Text = ii

    End Sub

    Public Sub rozmer()
        Dim rww As Integer = Me.Width / 2
        Dim sw As Integer = Me.Height

        DataGridView1.Size = New Size(rww * 2 - 10, sw - 220)
        DataGridView2.Size = New Size(rww * 2 - 10, sw - 220)
        DataGridView2.Location = DataGridView1.Location
        Dim g As System.Drawing.Graphics = Me.CreateGraphics
        Dim strSz As SizeF = g.MeasureString("Sklad", Label1.Font)
        Dim stred As Integer
        stred = strSz.Width / 2

        Dim rw As String = Me.Width / 2 - stred
        Label1.Location = New Point(rw, 10)
    End Sub

    Private Sub poverenie()
        Select Case Form78.heslo

            Case Form78.admin
                DataGridView1.Columns(8).Visible = True
                DataGridView1.Columns(9).Visible = True
                'DataGridView1.Columns(10).Visible = True
                Button1.Show()
                Button9.Show()
                Button5.Show()

            Case Form78.skladnik
                DataGridView1.Columns(8).Visible = False
                DataGridView1.Columns(9).Visible = True
                DataGridView1.Columns(10).Visible = False
                Button1.Show()
                Button9.Hide()
                Button5.Show()
            Case Else
                DataGridView1.Columns(8).Visible = False
                DataGridView1.Columns(9).Visible = False
                DataGridView1.Columns(10).Visible = False
                Button1.Hide()
                Button5.Hide()
                Button9.Hide()
        End Select
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        nastr = "fd"
        Dim f As New nastrsklad
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.nastr = nastr
        f.ShowDialog()

        '        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        Dim hladaj1 As Thread
        hladaj1 = New Thread(Sub() Invoke(Sub() napln_tabulu()))
        hladaj1.IsBackground = True
        hladaj1.Start()


        DataGridView2.Hide()
    End Sub
    Private Sub napln_tabulu()
        Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        filtruj()

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Button2.Hide()
        Button3.Show()
        Try
            Dim a As Integer
            Try
                a = TextBox1.Text
            Catch ex As Exception
                Chyby.Show("Zle zadané číslo")
            End Try
            Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
            Me.SkladBindingSource.Sort = "Pocet, Nastroj, VelkostS"

            Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%' AND {6} < '{7}'", RotekDataSet.Sklad.NastrojColumn, TextBox2.Text, RotekDataSet.Sklad.VelkostSColumn, TextBox3.Text, RotekDataSet.Sklad.VlastnostColumn, TextBox4.Text, RotekDataSet.Sklad.PocetColumn, a)
            napln()

        Catch ex As SystemException

        End Try


    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Button3.Hide()
        Button2.Show()
        filtruj()
    End Sub

    Private Sub TextBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = "10"
        TextBox1.SelectAll()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox2.Focus()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox2.MouseClick
        TextBox2.Text = ""
    End Sub
    Private Sub filtruj()
        DataGridView2.Hide()
        Me.SkladBindingSource.Sort = "Nastroj, VelkostS"
        Dim fil, slovo As String
        slovo = TextBox2.Text

        If TextBox4.Text.ToUpper = "HSS" Then
            fil = String.Format(" {2} LIKE '{3}%' AND {4} = '{5}'", RotekDataSet.Sklad.NastrojColumn, TextBox2.Text, RotekDataSet.Sklad.VelkostSColumn, TextBox3.Text, RotekDataSet.Sklad.VlastnostColumn, TextBox4.Text)
        Else
            fil = String.Format("  {2} LIKE '{3}%' AND {4} LIKE '{5}%'", RotekDataSet.Sklad.NastrojColumn, TextBox2.Text, RotekDataSet.Sklad.VelkostSColumn, TextBox3.Text, RotekDataSet.Sklad.VlastnostColumn, TextBox4.Text)
        End If

        While slovo.IndexOf(" ") > -1
            fil = fil & String.Format(" AND {0} LIKE '%{1}%'", RotekDataSet.Sklad.NastrojColumn, slovo.Substring(0, slovo.IndexOf(" ")))
            slovo = slovo.Remove(0, slovo.IndexOf(" ") + 1)
        End While
        fil = fil & String.Format(" AND {0} LIKE '%{1}%'", RotekDataSet.Sklad.NastrojColumn, slovo)

        Me.SkladBindingSource.Filter = fil
        napln()

    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        'Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        Dim hladaj1 As Thread
        hladaj1 = New Thread(Sub() DataGridView1.Invoke(Sub() filtruj()))
        hladaj1.IsBackground = True
        hladaj1.Start()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        'Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        filtruj()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try

            Dim f As New srotV
            f.TopLevel = True
            f.Dock = DockStyle.None
            f.ShowDialog()
            Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
            DataGridView2.Hide()


        Catch ex As Exception
            Chyby.Show(ex.ToString)
        End Try
    End Sub
    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick

        Dim riadok As Integer
        riadok = e.RowIndex
        If riadok < 0 Then Exit Sub
        Dim nastr As String = DataGridView1.Rows(riadok).Cells(0).Value
        Dim nastr2 As String = DataGridView1.Rows(riadok).Cells(1).Value
        vlast = DataGridView1.Rows(riadok).Cells(2).Value
        Dim f As New info_nastroj
        f.TopLevel = True
        f.Dock = DockStyle.None
        f.nastr = nastr
        f.nastr2 = nastr2
        f.vlast = vlast
        f.ShowDialog()
    End Sub

    Private Sub sklad_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        rozmer()

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Form78.exportovat(DataGridView1)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If (e.ColumnIndex = 8) And e.RowIndex >= 0 Then
            DataGridView1.Rows(e.RowIndex).Selected = True
            Dim riadok As Integer = e.RowIndex
            Dim nastr, nastr2 As String
            nastr = DataGridView1.Rows(riadok).Cells(0).Value
            nastr2 = DataGridView1.Rows(riadok).Cells(1).Value
            vlast = DataGridView1.Rows(riadok).Cells(2).Value
            Dim sql As String
            Dim con As New SqlConnection
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            Dim cmd As New SqlCommand

            sql = "DELETE FROM Sklad WHERE Nastroj='" + nastr + "' AND VelkostS='" + nastr2 + "' AND Vlastnost='" + vlast + "'"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()

            sql = "DELETE FROM Rotek WHERE Nástroj='" + nastr + "' AND VelkostR='" + nastr2 + "' AND Vlastnost='" + vlast + "'"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()

            con.Close()
            Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
            filtruj()

        ElseIf (e.ColumnIndex = 9) And e.RowIndex >= 0 Then
            Dim f As New nastrsklad(DataGridView1.Rows(e.RowIndex).Cells(0).Value, DataGridView1.Rows(e.RowIndex).Cells(1).Value, DataGridView1.Rows(e.RowIndex).Cells(2).Value)
            f.TopLevel = True
            f.Dock = DockStyle.None
            f.nastr = nastr
            f.ShowDialog()

            Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
            filtruj()
            DataGridView2.Hide()
        ElseIf (e.ColumnIndex = 10) And e.RowIndex >= 0 Then
            Dim nastr, nastr2, vlast As String
            Dim xnastr, xnastr2, xvlast As String
            Dim xpocet As Integer
            xpocet = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            xnastr = TextBox2.Text
            xnastr2 = TextBox3.Text
            xvlast = TextBox4.Text

            nastr = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            nastr2 = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            vlast = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            Dim sql As String
            Dim con As New SqlConnection
            con.ConnectionString = My.Settings.Rotek2
            con.Open()
            Dim cmd As New SqlCommand


            Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.Sklad.NastrojColumn, xnastr, RotekDataSet.Sklad.VelkostSColumn, xnastr2, RotekDataSet.Sklad.VlastnostColumn, xvlast)

            If DataGridView1.RowCount = 0 Then
                sql = "Insert INTO Sklad (Nastroj, Pocet, Regal, Cena, VelkostS, Vlastnost) VALUES ('" + nastr + "', '" & p & "', '" + reg + "', '" & cenaa & "', '" & nastr2 & "', '" & vlast & "')"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()
            Else
                sql = "UPDATE Sklad SET Pocet='" & xpocet + DataGridView1.Rows(0).Cells(4).Value & "' WHERE VelkostS='" + xnastr2 + "' AND Nastroj='" + xnastr + "' AND Vlastnost='" + xvlast + "'"
                cmd = New SqlCommand(sql, con)
                cmd.ExecuteNonQuery()

            End If

            'Me.SkladBindingSource.Filter = String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} = '{5}'", RotekDataSet.Sklad.NastrojColumn, xnastr, RotekDataSet.Sklad.VelkostSColumn, xnastr2, RotekDataSet.Sklad.VlastnostColumn, xvlast)


            sql = "DELETE FROM Sklad WHERE Nastroj='" + nastr + "' AND VelkostS='" & nastr2 & "' AND Vlastnost='" + vlast + "'"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()

            sql = "DELETE FROM Rotek WHERE Nástroj='" + nastr + "' AND VelkostR='" & nastr2 & "' AND Vlastnost='" + vlast + "'"
            cmd = New SqlCommand(sql, con)
            cmd.ExecuteNonQuery()

        End If
        If (e.RowIndex = -1) And (e.ColumnIndex = 1) Then
            Try
                DataGridView2.Rows.Clear()


                SkladBindingSource.Sort = "VelkostS"

                Dim kontrola As String = ""
                Dim s As String

                Dim adresy(DataGridView1.RowCount), velkoste(DataGridView1.RowCount) As Double
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    s = DataGridView1.Rows(i).Cells(1).Value
                    velkoste(i) = konvert(s, i)
                    adresy(i) = i
                Next
                QuickSort(velkoste, 0, DataGridView1.RowCount - 1, adresy)
                For i As Integer = 0 To DataGridView1.RowCount - 1
                    'Chyby.Show(velkoste(i))
                    DataGridView2.Rows.Add()
                    For ii As Integer = 0 To 7
                        DataGridView2.Rows(i).Cells(ii).Value = DataGridView1.Rows(adresy(i)).Cells(ii).Value
                    Next
                Next
            Catch ex As Exception
                Chyby.Show(ex.ToString)
            End Try
            DataGridView2.Show()
            napln()
        End If

    End Sub
    Private Function konvert(ByRef s As String, ByVal ix As Integer) As Double
        Dim i As Integer = 0
        While (Char.IsNumber(s(i)) Or s(i) = ",")
            i = i + 1
            If i = s.Length Then Exit While
        End While
        If i <> 0 Then
            Return s.Substring(0, i)
        Else
            Return 999999 + ix
        End If
    End Function

    Public Sub QuickSort(C() As Double, ByVal First As Long, ByVal Last As Long, ByRef X() As Double)
        Dim Low As Long, High As Long
        Low = First
        High = Last
        Dim pom, pom2 As Double
        Dim ii As Integer = 0

        For i As Integer = First + 1 To Last
            If C(i) < C(i - 1) Then
                pom = C(i)
                pom2 = X(i)
                ii = i - 1
                While pom < C(ii)
                    C(ii + 1) = C(ii)
                    X(ii + 1) = X(ii)
                    ii = ii - 1
                    If ii = -1 Then
                        Exit While
                    End If
                End While
                C(ii + 1) = pom
                X(ii + 1) = pom2

            End If
        Next

    End Sub

    Private Sub Swap(ByRef A As Double, ByRef B As Double)
        Dim T As Long

        T = A
        A = B
        B = T
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        filtruj()

    End Sub


    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        If Button2.Text = "Iba a len" Then
            Button2.Text = "Iba a viac"
        Else
            Button2.Text = "Iba a len"
        End If
        DataGridView2.Hide()
        Me.SkladBindingSource.Filter = String.Format("{0} LIKE '{1}%' AND {2} LIKE '{3}%' AND {4} LIKE '{5}%'", RotekDataSet.Sklad.NastrojColumn, TextBox2.Text, RotekDataSet.Sklad.VelkostSColumn, TextBox3.Text, RotekDataSet.Sklad.VlastnostColumn, TextBox4.Text)

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        napln()
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        DataGridView2.Rows(e.RowIndex).Selected = True
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'Me.SkladTableAdapter.Fill(Me.RotekDataSet.Sklad)
        fillasync()

    End Sub

    'Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
    '    If DataGridView1.RowCount > 500 Then
    '        Exit Sub
    '    End If
    '    If e.ColumnIndex = 4 Then
    '        If e.Value < 5 Then
    '            If Math.Floor(e.RowIndex / 2) = Math.Ceiling(e.RowIndex / 2) Then
    '                'MessageBox.Show(e.RowIndex)
    '                DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.DeepPink
    '            Else
    '                DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.HotPink
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = DataGridView1.Rows.Count
        SQL.Stock_SQL.Add("Sklad nástrojov", New String() {"Nástroj", "Priemer", "Vlastnosť"})
        Dim stockID As Integer = SQL.Stock_SQL.stock_ID("Sklad nástrojov")
        Dim i As Integer = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            Dim regal As String = Nothing

            If (DataGridView1.Rows(i).Cells(7).Value.ToString() <> "") Then
                regal = DataGridView1.Rows(i).Cells(7).Value
            End If
            Try
                SQL.Employer_Stock.AddItem(stockID, New String() {DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value}, Nothing, Nothing, regal, DataGridView1.Rows(i).Cells(4).Value, 0, DataGridView1.Rows(i).Cells(3).Value.ToString().Replace("€", "").Replace(".", ","), 1)

            Catch ex As Exception
                'dasd
                MessageBox.Show(DataGridView1.Rows(i).Cells(0).Value & " " & DataGridView1.Rows(i).Cells(1).Value & " " & DataGridView1.Rows(i).Cells(2).Value & " " & DataGridView1.Rows(i).Cells(3).Value & " " & DataGridView1.Rows(i).Cells(4).Value & " " & DataGridView1.Rows(i).Cells(5).Value & " " & DataGridView1.Rows(i).Cells(7).Value & vbCrLf & ex.ToString)
            End Try
            ProgressBar1.Value = i
        Next

    End Sub
End Class