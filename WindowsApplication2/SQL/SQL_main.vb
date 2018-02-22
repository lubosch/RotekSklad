Imports System.Data
Imports System.Data.SqlClient

Public Class SQL_main

    Shared connection As SqlConnection
    Shared command As SqlCommand

    Public Sub New()
        command = New SqlCommand()
        command.CommandType = CommandType.Text
        connection = Nothing
    End Sub

    Public Shared Sub OpenConnection()
        Dim connectionString As String = My.Settings.Rotek
        connection = New SqlConnection(connectionString)
        connection.Open()
        command = New SqlCommand()
        command.CommandType = CommandType.Text
        command.Connection = connection
    End Sub

    Public Shared Sub AddCommand(cmd As String)
        If connection Is Nothing OrElse connection.State <> ConnectionState.Open Then
            OpenConnection()
        End If
        If cmd(0) = "~"c Then
            command.CommandText += cmd.Replace("~"c, " "c)
            Console.Out.WriteLine(cmd.Replace("~"c, " "c))
        Else
            command.CommandText += ";" & vbLf & cmd
            Console.Out.WriteLine(";" & vbLf & cmd)
        End If
    End Sub


    Public Shared Sub AddParameter(parameter As String, premenna As Object)
        If premenna Is Nothing Then
            command.Parameters.Add(New SqlParameter(parameter, DBNull.Value))
        Else
            command.Parameters.Add(New SqlParameter(parameter, premenna))
        End If
    End Sub

    Public Shared Sub Odpal()
        'Try
        Debug.WriteLine(command.CommandText)
        command.ExecuteNonQuery()
        connection.Close()
        'Catch ex As Exception
        'Chyby.Show(ex.ToString)
        'End Try
    End Sub
    Public Shared Sub CloseConnection()
        command.CommandText = ""
        connection.Close()
    End Sub

    Public Shared Sub Odpalovac(cmd As String)
        Try
            Debug.WriteLine(cmd)
            System.Console.WriteLine(cmd)
            OpenConnection()
            command = New SqlCommand(cmd, connection)
            command.ExecuteNonQuery()
            CloseConnection()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Public Shared Function List(cmd As String, dataTable As DataTable) As Integer
        dataTable.Clear()
        OpenConnection()
        command = New SqlCommand(cmd, connection)
        Debug.WriteLine(cmd)
        Dim dataadapter As SqlDataAdapter = New SqlDataAdapter(command)
        Dim retutnVal As Integer = dataadapter.Fill(dataTable)
        CloseConnection()
        Return retutnVal
    End Function

    Public Shared Function Commit_List(dataTable As DataTable) As Integer
        dataTable.Clear()
        command.CommandText = "BEGIN TRANSACTION;" & vbLf & command.CommandText & vbLf & "COMMIT TRANSACTION;"
        Debug.WriteLine(command.CommandText)
        Dim dataadapter As SqlDataAdapter = New SqlDataAdapter(command)
        Dim ds As New DataSet()
        'Error.Show(command.CommandText);
        Dim retutnVal As Integer = dataadapter.Fill(dataTable)
        CloseConnection()
        Return retutnVal

    End Function

    Public Shared Function Commit_Transaction() As Object
        command.CommandText = "BEGIN TRANSACTION;" & vbLf & command.CommandText & vbLf & "COMMIT TRANSACTION;"
        Debug.WriteLine(command.CommandText)
        Dim s As Object = command.ExecuteScalar()
        If s IsNot Nothing AndAlso s.ToString().IndexOf("ERROR:") = 0 Then
            command.CommandText = ""
            command.Parameters.Clear()
            CloseConnection()
            Throw New Exception(s.ToString())
        End If

        command.CommandText = ""
        command.Parameters.Clear()
        CloseConnection()
        Return s
    End Function


    Public Shared Function OdpalList(dataTable As DataTable) As Integer
        dataTable.Clear()
        Debug.WriteLine(command.CommandText)
        Dim dataadapter As SqlDataAdapter = New SqlDataAdapter(command)
        Dim ds As New DataSet()
        'Error.Show(command.CommandText);
        Dim retutnVal As Integer = dataadapter.Fill(dataTable)
        CloseConnection()
        Return retutnVal

    End Function

End Class
