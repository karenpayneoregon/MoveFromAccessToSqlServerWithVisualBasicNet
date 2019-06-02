Imports System.ComponentModel.DataAnnotations

Namespace Classes
    Public Class DateAttribute
        Inherits RangeAttribute

        Public Sub New()
            MyBase.New(GetType(Date), Date.Now.AddDays(-1).ToShortDateString(), Date.Now.ToShortDateString())
        End Sub
    End Class
End Namespace