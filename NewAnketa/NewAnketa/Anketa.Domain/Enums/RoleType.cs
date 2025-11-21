namespace Anketa.Domain.Enums
{
    public enum RoleType
    {
        Admin = 1,           // Can view everything
        Employee = 2,        // Can only view own answers
        MCViewAll = 3,       // Can view all reports (read-only admin)
        SectorManager = 4,   // Can view specific sector and its children
        DepartmentManager = 5, // Can view specific department and its children  
        LineManager = 6      // Can view specific line
    }
}
