using Model;

namespace Crud_Api.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Person person);
    }
}