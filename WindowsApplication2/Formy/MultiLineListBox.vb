Public Class MultiLineListBox
    Inherits System.Windows.Forms.ListBox

    Public Sub New()
        Me.DrawMode = DrawMode.OwnerDrawVariable
        Me.ScrollAlwaysVisible = True
        tbox.Hide()
        tbox.mllb = Me
        Controls.Add(tbox)

    End Sub

    Protected Overrides Sub OnMeasureItem(e As MeasureItemEventArgs)
        If Site IsNot Nothing Then
            Return
        End If
        If e.Index > -1 Then
            Dim s As String = Items(e.Index).ToString()
            Dim sf As SizeF = e.Graphics.MeasureString(s, Font, Width)
            Dim htex As Integer = If((e.Index = 0), 15, 10)
            e.ItemHeight = CInt(sf.Height) + htex
            e.ItemWidth = Width
        End If
    End Sub

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If Site IsNot Nothing Then
            Return
        End If
        If e.Index > -1 Then
            Dim s As String = Items(e.Index).ToString()

            If (e.State And DrawItemState.Focus) = 0 Then
                e.Graphics.FillRectangle(New SolidBrush(SystemColors.Window), e.Bounds)
                e.Graphics.DrawString(s, Font, New SolidBrush(SystemColors.WindowText), e.Bounds)
                e.Graphics.DrawRectangle(New Pen(SystemColors.GrayText), e.Bounds)
            Else
                e.Graphics.FillRectangle(New SolidBrush(SystemColors.Highlight), e.Bounds)
                e.Graphics.DrawString(s, Font, New SolidBrush(SystemColors.HighlightText), e.Bounds)
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(e As System.Windows.Forms.MouseEventArgs)
        'Dim index As Integer = IndexFromPoint(e.X, e.Y)

        'If index <> ListBox.NoMatches AndAlso index <> 65535 Then


        '    If e.Button = MouseButtons.Right Then

        '        Dim s As String = Items(index).ToString()
        '        Dim rect As Rectangle = GetItemRectangle(index)

        '        tbox.Location = New Point(rect.X, rect.Y)
        '        tbox.Size = New Size(rect.Width, rect.Height)
        '        tbox.Text = s
        '        tbox.index = index
        '        tbox.SelectAll()
        '        tbox.Show()
        '        tbox.Focus()
        '    End If
        'End If

        MyBase.OnMouseUp(e)
    End Sub

    Private tbox As New NTextBox()

    Private Class NTextBox
        Inherits TextBox
        Public mllb As MultiLineListBox
        Public index As Integer = -1

        Private errshown As Boolean = False
        Private brementer As Boolean = False

        Public Sub New()
            Multiline = True
            ScrollBars = ScrollBars.Vertical
        End Sub

        Protected Overrides Sub OnKeyUp(e As KeyEventArgs)
            If brementer Then
                Text = ""
                brementer = False
            End If
            MyBase.OnKeyUp(e)
        End Sub

        Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
            MyBase.OnKeyPress(e)

            If Char.GetNumericValue(e.KeyChar) = 13 Then
                If Text.Trim() = "" Then
                    errshown = True
                    brementer = True

                    MessageBox.Show("Cannot enter NULL string as item!", "Fatal error!", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Else
                    errshown = False
                    mllb.Items(index) = Text
                    Hide()

                End If
            End If

            If Char.GetNumericValue(e.KeyChar) = 27 Then
                Text = mllb.Items(index).ToString()
                Hide()
                mllb.SelectedIndex = index
            End If

        End Sub

        Protected Overrides Sub OnLostFocus(e As System.EventArgs)

            If Text.Trim() = "" Then
                If Not errshown Then
                    MessageBox.Show("Cannot enter NULL string as item!", "Fatal error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                errshown = False
            Else
                errshown = False
                mllb.Items(index) = Text
                Hide()
            End If
            MyBase.OnLostFocus(e)
        End Sub
    End Class

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        If e.KeyData = Keys.F2 Then
            Dim index As Integer = SelectedIndex
            If index = ListBox.NoMatches OrElse index = 65535 Then
                If Items.Count > 0 Then
                    index = 0
                End If
            End If
            If index <> ListBox.NoMatches AndAlso index <> 65535 Then

                Dim s As String = Items(index).ToString()
                Dim rect As Rectangle = GetItemRectangle(index)

                tbox.Location = New Point(rect.X, rect.Y)
                tbox.Size = New Size(rect.Width, rect.Height)
                tbox.Text = s
                tbox.index = index
                tbox.SelectAll()
                tbox.Show()
                tbox.Focus()
            End If
        End If
        MyBase.OnKeyDown(e)
    End Sub

End Class
