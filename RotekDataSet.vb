Imports System.Data.SqlClient

Partial Class RotekDataSet
    Partial Class MailyDataTable

        Private Sub MailyDataTable_MailyRowChanging(sender As Object, e As MailyRowChangeEvent) Handles Me.MailyRowChanging

        End Sub

    End Class

    Partial Class ZakazkaDataTable

    End Class

    Partial Class HutaDataTable

        Private Sub HutaDataTable_HutaRowChanging(sender As System.Object, e As HutaRowChangeEvent) Handles Me.HutaRowChanging

        End Sub

    End Class

End Class

Namespace RotekDataSetTableAdapters

    Partial Class MaterialTableAdapter
        Public Overridable Overloads Function FillFiltered(ByVal dataTable As RotekDataSet.MaterialDataTable, ByVal Druh As String, ByVal typ As String, ByVal Nazov As String, dodatok As String) As Integer
            dodatok= dodatok.Replace("Kusov", "m.Kusov")

            Dim rozkaz As String = Me.CommandCollection(2).CommandText
            rozkaz = rozkaz & " AND " & dodatok
            Dim cmd As SqlCommand = Me.CommandCollection(2).Clone
            cmd.CommandText = rozkaz
            Me.Adapter.SelectCommand = cmd

            'Me.Adapter.SelectCommand = Me.CommandCollection(2)
            If (Druh Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Druh")
            Else
                Me.Adapter.SelectCommand.Parameters(0).Value = CType(Druh, String)
            End If
            If (typ Is Nothing) Then
                Throw New Global.System.ArgumentNullException("typ")
            Else
                Me.Adapter.SelectCommand.Parameters(1).Value = CType(typ, String)
            End If
            If (Nazov Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Nazov")
            Else
                Me.Adapter.SelectCommand.Parameters(2).Value = CType(Nazov, String)
            End If
            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function
    End Class

    Partial Class PrijemkyTableAdapter
        Public Overridable Overloads Function fillFiltered(ByVal dataTable As RotekDataSet.PrijemkyDataTable, ByVal Druh As String, ByVal typ As String, ByVal Nazov As String, Dodatok As String) As Integer
            Dodatok = Dodatok.Replace("Kusov", "mp.Ks")

            Dim rozkaz As String = Me.CommandCollection(2).CommandText
            rozkaz = rozkaz & " AND " & Dodatok & " ORDER BY Nazov DESC"
            Dim cmd As SqlCommand = Me.CommandCollection(2).Clone
            cmd.CommandText = rozkaz
            Me.Adapter.SelectCommand = cmd

            If (Druh Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Druh")
            Else
                Me.Adapter.SelectCommand.Parameters(0).Value = CType(Druh, String)
            End If
            If (typ Is Nothing) Then
                Throw New Global.System.ArgumentNullException("typ")
            Else
                Me.Adapter.SelectCommand.Parameters(1).Value = CType(typ, String)
            End If
            If (Nazov Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Nazov")
            Else
                Me.Adapter.SelectCommand.Parameters(2).Value = CType(Nazov, String)
            End If
            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function
    End Class

    Partial Class VydajkyTableAdapter

        Public Overridable Overloads Function fillFiltered(ByVal dataTable As RotekDataSet.VydajkyDataTable, ByVal Druh As String, ByVal typ As String, ByVal Nazov As String, ByVal dodatok As String) As Integer
            dodatok = dodatok.Replace("Kusov", "mv.Ks")

            Dim rozkaz As String = Me.CommandCollection(2).CommandText

            'Debug.WriteLine("Dodatok > " & dodatok)
            rozkaz = rozkaz & " AND " & dodatok & " ORDER BY Vydajky.Nazov DESC"
            Dim cmd As SqlCommand = Me.CommandCollection(2).Clone
            cmd.CommandText = rozkaz
            Me.Adapter.SelectCommand = cmd
            If (Druh Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Druh")
            Else
                Me.Adapter.SelectCommand.Parameters(0).Value = CType(Druh, String)
            End If
            If (typ Is Nothing) Then
                Throw New Global.System.ArgumentNullException("typ")
            Else
                Me.Adapter.SelectCommand.Parameters(1).Value = CType(typ, String)
            End If
            If (Nazov Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Nazov")
            Else
                Me.Adapter.SelectCommand.Parameters(2).Value = CType(Nazov, String)
            End If
            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function

    End Class

    Partial Class ZakazkaTableAdapter

        Public Overridable Overloads Function Filtered(filter As String, ByVal dataTable As RotekDataSet.ZakazkaDataTable) As Integer
            Me.Adapter.SelectCommand = New SqlClient.SqlCommand(Me.CommandCollection(1).CommandText + " AND ( " + filter + " ) ORDER BY Zakazka DESC", Me.CommandCollection(1).Connection)
            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function
    End Class

    Partial Class RotekTableAdapter

    End Class

    Partial Public Class PrijemkaTableAdapter
    End Class
End Namespace
