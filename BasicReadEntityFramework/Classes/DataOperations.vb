Imports EntityFrameworkCodeFirstWithExistingDatabase

Namespace Classes
    ''' <summary>
    ''' Read data using Entity Framework which does the same as the SQL SELECT
    ''' in the project BasicUpdatingDataProvider with less code overall and
    ''' more functionality.
    ''' </summary>
    Public Class DataOperations
        Public Function ReadCustomers() As List(Of CustomerEntity)

            Dim customerData As List(Of CustomerEntity)

            Using context As New NorthWindEntity

                customerData =
                    (From customer In context.Customers
                     Join contactType In context.ContactTypes On customer.ContactTypeIdentifier Equals contactType.ContactTypeIdentifier
                     Join contact In context.Contacts On customer.ContactId Equals contact.ContactId
                     Select New CustomerEntity With
                         {
                         .CustomerIdentifier = customer.CustomerIdentifier,
                         .Company = customer.CompanyName,
                         .ContactIdentifier = customer.ContactId,
                         .FirstName = contact.FirstName,
                         .LastName = contact.LastName,
                         .ContactTypeIdentifier = contactType.ContactTypeIdentifier,
                         .ContactTitle = contactType.ContactTitle,
                         .Street = customer.Address,
                         .City = customer.City,
                         .PostalCode = customer.PostalCode,
                         .CountryIdentifier = customer.CountryIdentifier,
                         .CountyName = customer.Country.Name
                         }).ToList()

            End Using

            Return customerData

        End Function
    End Class
End Namespace