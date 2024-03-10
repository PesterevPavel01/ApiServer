using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Enum
{
    public enum ErrorCodes
    {
        DocumentsNotFound=0,
        DocumentNotFound=1,
        DocumentAlreadyExists = 2,
        DepartmentsNotFound = 3,
        DepartmentNotFound = 4,
        DepartmentAlreadyExists=5,
        ExpendituresNotFound = 6,
        ExpenditureNotFound = 7,
        ExpenditureAlreadyExists = 8,
        OrganizationsNotFound = 9,
        OrganizationNotFound = 10,
        OrganizationAlreadyExists = 11,
        InternalServerError =12
    }
}
