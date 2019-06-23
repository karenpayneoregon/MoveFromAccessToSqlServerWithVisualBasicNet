﻿Imports System.ComponentModel
''' <summary>
''' Provides a generic collection that supports data binding and additionally supports sorting.
''' See http://msdn.microsoft.com/en-us/library/ms993236.aspx
''' If the elements are IComparable it uses that; otherwise compares the ToString()
''' </summary>
''' <typeparam name="T">The type of elements in the list.</typeparam>
Public Class SortableBindingList(Of T As Class)
    Inherits BindingList(Of T)

    Private _isSorted As Boolean
    Private _sortDirection As ListSortDirection = ListSortDirection.Ascending
    Private _sortProperty As PropertyDescriptor

    Public Sub New()
    End Sub

    Public Sub New(ByVal list As IList(Of T))
        MyBase.New(list)
    End Sub

    ''' <summary>
    ''' Gets a value indicating whether the list supports sorting.
    ''' </summary>
    Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether the list is sorted.
    ''' </summary>
    Protected Overrides ReadOnly Property IsSortedCore() As Boolean
        Get
            Return _isSorted
        End Get
    End Property

    ''' <summary>
    ''' Gets the direction the list is sorted.
    ''' </summary>
    Protected Overrides ReadOnly Property SortDirectionCore() As ListSortDirection
        Get
            Return _sortDirection
        End Get
    End Property

    ''' <summary>
    ''' Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns null
    ''' </summary>
    Protected Overrides ReadOnly Property SortPropertyCore() As PropertyDescriptor
        Get
            Return _sortProperty
        End Get
    End Property

    ''' <summary>
    ''' Removes any sort applied with ApplySortCore if sorting is implemented
    ''' </summary>
    Protected Overrides Sub RemoveSortCore()
        _sortDirection = ListSortDirection.Ascending
        _sortProperty = Nothing
        _isSorted = False 'thanks Luca
    End Sub

    ''' <summary>
    ''' Sorts the items if overridden in a derived class
    ''' </summary>
    ''' <param name="prop"></param>
    ''' <param name="direction"></param>
    Protected Overrides Sub ApplySortCore(ByVal prop As PropertyDescriptor, ByVal direction As ListSortDirection)
        _sortProperty = prop
        _sortDirection = direction

        Dim list As List(Of T) = TryCast(Items, List(Of T))
        If list Is Nothing Then
            Return
        End If

        list.Sort(AddressOf Compare)

        _isSorted = True
        'fire an event that the list has been changed.
        OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub

    Private Function Compare(ByVal lhs As T, ByVal rhs As T) As Integer
        Dim result = OnComparison(lhs, rhs)
        'invert if descending
        If _sortDirection = ListSortDirection.Descending Then
            result = -result
        End If
        Return result
    End Function

    Private Function OnComparison(ByVal lhs As T, ByVal rhs As T) As Integer
        Dim lhsValue As Object = If(lhs Is Nothing, Nothing, _sortProperty.GetValue(lhs))
        Dim rhsValue As Object = If(rhs Is Nothing, Nothing, _sortProperty.GetValue(rhs))
        If lhsValue Is Nothing Then
            Return If(rhsValue Is Nothing, 0, -1) 'nulls are equal
        End If
        If rhsValue Is Nothing Then
            Return 1 'first has value, second doesn't
        End If
        If TypeOf lhsValue Is IComparable Then
            Return CType(lhsValue, IComparable).CompareTo(rhsValue)
        End If
        If lhsValue.Equals(rhsValue) Then
            Return 0 'both are the same
        End If
        'not comparable, compare ToString
        Return lhsValue.ToString().CompareTo(rhsValue.ToString())
    End Function
End Class
