Imports System.Windows.Forms

Public Module BindingSourceExtensions
    ''' <summary>
    ''' Used for selection of like condition in LikeFilter extension
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum LikeOptions
        ''' <summary>
        ''' Field starts with chars
        ''' </summary>
        ''' <remarks></remarks>
        StartsWith
        ''' <summary>
        ''' Field ends with chars
        ''' </summary>
        ''' <remarks></remarks>
        EndsWith
        ''' <summary>
        ''' File contains chars
        ''' </summary>
        ''' <remarks></remarks>
        Contains
        None
    End Enum
    <System.Runtime.CompilerServices.Extension()>
    Public Sub LikeFilter(ByVal sender As BindingSource, ByVal Value As String)
        Dim dt = CType(sender.DataSource, DataTable)
        Dim dv = dt.DefaultView
        If Value.Length > 0 Then
            dv.RowFilter = "LastName like  '" & Value & "%'"
        Else
            dv.RowFilter = ""
        End If
    End Sub
    ''' <summary>
    ''' Used to filter a BindingSource via the data source
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="FieldName">name of field to filter</param>
    ''' <param name="Value">value to filter on or an empty string to remove filter</param>
    ''' <param name="Which">direction of filter</param>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Sub LikeFilter(
        ByVal sender As BindingSource,
        ByVal FieldName As String,
        ByVal Value As String,
        ByVal Which As LikeOptions)
        Dim dt = CType(sender.DataSource, DataTable)
        Dim dv = dt.DefaultView
        If Value.Length > 0 Then
            Dim Filter As String = FieldName & " like  '"
            Select Case Which
                Case LikeOptions.StartsWith
                    Filter = FieldName & " like  '" & Value & "%'"
                Case LikeOptions.EndsWith
                    Filter = FieldName & " like  '%" & Value & "'"
                Case LikeOptions.Contains
                    Filter = FieldName & " like  '%" & Value & "%'"
            End Select
            dv.RowFilter = Filter
        Else
            dv.RowFilter = ""
        End If
    End Sub
End Module