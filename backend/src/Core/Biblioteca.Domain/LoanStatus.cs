using System.Runtime.Serialization;

namespace Biblioteca.Domain;

public enum LoanStatus
{
    [EnumMember(Value = "Prestamo Inactivo")]
    Inactivo,
    [EnumMember(Value = "Prestamo Activo")]
    Activo,
    [EnumMember(Value = "Prestamo Restrasado")]
    Retrasado,

}
