Namespace Interfaces
    Public Interface IBaseEntity
        ReadOnly Property Id() As Integer
        ReadOnly Property ModifiedOn() As DateTime
        ReadOnly Property ModifiedByUserId() As Integer
    End Interface
End Namespace