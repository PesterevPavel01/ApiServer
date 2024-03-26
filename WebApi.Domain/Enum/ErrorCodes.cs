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
        InternalServerError =12,
        DateIsInvalid = 13,
        NewDepartmentsNotFound = 14,
        NewExpendituresNotFound = 15,
        NewOrganizationsNotFound = 16,
        NewDocumentsNotFound = 17,
        IncorrectPeriod=18,
        IncorrectInputObject=19,
        UserNotFound=20,
        UserAlreadyExists=21,
        TargetNotFound=22,
        TargetAlreadyExists=23,
    }
}
