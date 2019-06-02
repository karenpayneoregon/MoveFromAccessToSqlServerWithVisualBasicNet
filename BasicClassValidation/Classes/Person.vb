Imports BasicClassValidation.Interfaces

Namespace Classes
    Public Class Person
        Inherits PersonBase
        Implements IBaseEntity
        ''' <summary>
        ''' Key to identifying the a specific person pointing to
        ''' the Identifier.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Id As Integer Implements IBaseEntity.Id
            Get
                Return Identifier
            End Get
        End Property

        ''' <summary>
        ''' Date the person record was modified
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property ModifiedOn As Date Implements IBaseEntity.ModifiedOn
            Get
                Return ModifiedDate
            End Get
        End Property
        ''' <summary>
        ''' Identity of person modifying the person record
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property ModifiedByUserIdentifier As Integer Implements IBaseEntity.ModifiedByUserId
            Get
                Return ModifiedByUserId
            End Get
        End Property
        ''' <summary>
        ''' Suitable for displaying in a UI control or for debugging purposes
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToString() As String
            Return $"{FirstName} {MiddleName} {LastName}"
        End Function
    End Class
End Namespace