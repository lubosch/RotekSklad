Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

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

    Partial Class PrijemkyTableAdapter
        Public Overridable Overloads Function fillFiltered(ByVal dataTable As RotekDataSet.PrijemkyDataTable, skladlist_id As Integer, ByVal typ As String, ByVal Nazov As String, ByVal Rok As Integer, ByVal Rok2 As String, ByVal druh As String, dodatok As String) As Integer
            dodatok = dodatok.Replace("Kusov", "mp.Ks")

            Dim rozkaz As String = Me.CommandCollection(3).CommandText
            rozkaz = rozkaz & " AND " & dodatok & " ORDER BY Nazov DESC"
            Dim cmd As SqlCommand = Me.CommandCollection(3).Clone
            cmd.CommandText = rozkaz
            Me.Adapter.SelectCommand = cmd

            Me.Adapter.SelectCommand.Parameters("@skladlist_id").Value = CType(skladlist_id, Integer)

            If (druh Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Druh")
            Else
                Me.Adapter.SelectCommand.Parameters("@druh").Value = CType(druh, String)
            End If
            If (typ Is Nothing) Then
                Throw New Global.System.ArgumentNullException("typ")
            Else
                Me.Adapter.SelectCommand.Parameters("@typ").Value = CType(typ, String)
            End If
            If (Nazov Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Nazov")
            Else
                Me.Adapter.SelectCommand.Parameters("@Nazov").Value = CType(Nazov, String)
            End If
            Me.Adapter.SelectCommand.Parameters("@Rok").Value = "20" + Rok.ToString("D2") + "-1-1"
            Me.Adapter.SelectCommand.Parameters("@Rok2").Value = "20" + Rok.ToString("D2") + "-12-31"

            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue

        End Function

    End Class

    Partial Class MaterialDruhTableAdapter

    End Class

    Partial Class Material_VydajkaTableAdapter

    End Class

    Partial Class OdpadyTableAdapter

    End Class

    Partial Class MaterialTableAdapter
        Public Overridable Overloads Function FillFiltered(ByVal dataTable As RotekDataSet.MaterialDataTable, ByVal skladlist_id As Integer, ByVal Druh As String, ByVal typ As String, ByVal Nazov As String, dodatok As String) As Integer
            dodatok = dodatok.Replace("Kusov", "m.Kusov")
            'dodatok = dodatok.Replace("SELECT", "SELECT TOP 500")

            Dim rozkaz As String = Me.CommandCollection(2).CommandText
            rozkaz = rozkaz & " AND " & dodatok
            Dim cmd As SqlCommand = Me.CommandCollection(2).Clone
            cmd.CommandText = rozkaz
            Me.Adapter.SelectCommand = cmd

            'Me.Adapter.SelectCommand = Me.CommandCollection(2)
            Me.Adapter.SelectCommand.Parameters("@skladlist_id").Value = CType(skladlist_id, Integer)

            If (Druh Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Druh")
            Else
                Me.Adapter.SelectCommand.Parameters("@druh").Value = CType(Druh, String)
            End If
            If (typ Is Nothing) Then
                Throw New Global.System.ArgumentNullException("typ")
            Else
                Me.Adapter.SelectCommand.Parameters("@typ").Value = CType(typ, String)
            End If
            If (Nazov Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Nazov")
            Else
                Me.Adapter.SelectCommand.Parameters("@Nazov").Value = CType(Nazov, String)
            End If
            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function
    End Class


    Partial Class VydajkyTableAdapter

        Public Overridable Overloads Function fillFiltered(ByVal dataTable As RotekDataSet.VydajkyDataTable, skladlist_id As Integer, Rok As Integer, ByVal Druh As String, ByVal typ As String, ByVal Nazov As String, ByVal dodatok As String) As Integer
            dodatok = dodatok.Replace("Kusov", "mv.Ks")
            dodatok = modify_filter(dodatok)
            Dim rozkaz As String = Me.CommandCollection(3).CommandText

            'Debug.WriteLine("Dodatok > " & dodatok)
            rozkaz = rozkaz & " AND " & dodatok & " ORDER BY Vydajky.Nazov DESC"

            Dim cmd As SqlCommand = Me.CommandCollection(3).Clone
            cmd.CommandText = rozkaz
            Me.Adapter.SelectCommand = cmd

            Me.Adapter.SelectCommand.Parameters("@skladlist_id").Value = CType(skladlist_id, Integer)

            If (Druh Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Druh")
            Else
                Me.Adapter.SelectCommand.Parameters("@druh").Value = CType(Druh, String)
            End If
            If (typ Is Nothing) Then
                Throw New Global.System.ArgumentNullException("typ")
            Else
                Me.Adapter.SelectCommand.Parameters("@Typ").Value = CType(typ, String)
            End If
            If (Nazov Is Nothing) Then
                Throw New Global.System.ArgumentNullException("Nazov")
            Else
                Me.Adapter.SelectCommand.Parameters("@Nazov").Value = CType(Nazov, String)
            End If

            Me.Adapter.SelectCommand.Parameters("@Rok").Value = CType(Rok, Integer)

            If (Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If
            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function

        Private Function modify_filter(filter As String)
            Dim strRegex As String = "m\..+?(?=AND)"
            Dim myRegex As New Regex(strRegex, RegexOptions.None)

            For Each myMatch As Match In myRegex.Matches(filter)
                If myMatch.Success Then
                    filter = filter.Replace(myMatch.Value, String.Format("({0} OR {1})", myMatch.Value, myMatch.Value.Replace("m.", "mm.")))
                End If
            Next
            Return filter
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

