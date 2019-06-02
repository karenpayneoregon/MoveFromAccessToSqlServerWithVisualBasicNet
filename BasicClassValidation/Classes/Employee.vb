Imports BasicClassValidation.Interfaces
Imports BasicClassValidation.Rules

Namespace Classes
    Public Class Employee
        Inherits PersonBase
        Implements IBaseEntity

        ''' <summary>
        ''' Foreign key to ContactType table
        ''' </summary>
        ''' <returns></returns>
        <ContactType(SelectContactType:=0, ErrorMessage:="Must select a contact type")>
        Public Property ContactTypeIdentifier As Integer?

        Public ReadOnly Property Id As Integer Implements IBaseEntity.Id
            Get
                Return Identifier
            End Get
        End Property
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
    End Class
End Namespace