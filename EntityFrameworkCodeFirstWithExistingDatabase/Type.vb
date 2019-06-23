Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class Type
    Public Property id As Integer

    Public Property ProductID As Integer?

    Public Property Hight As Integer?

    Public Property Width As Integer?

    Public Property Length As Integer?

    Public Property Area As Integer?

    <Column("Type")>
    Public Property Type1 As Integer?

    Public Property kg As Integer?

    Public Overridable Property Product As Product
End Class
