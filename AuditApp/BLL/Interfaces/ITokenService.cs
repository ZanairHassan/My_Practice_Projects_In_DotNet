using DAL.Models;
using DAL.ViewModels;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        Task<Token> CreateToken(TokenVM tokenVM);
        Task<Token> GetToken(int tokenID);
        Task<Token> GetTokenByjwtToken(string token);
        Task<List<Token>> ExpireTokensByUserId(int UserId);
        Task<List<Token>> GetTokenByUserId(int UserId);
        Task<Token> GetTokenByUserID(int userId);
        Task<bool> InvalidatejwtToken(string token);
        Task<bool> InvalidatejwtTokenByID(int UserId);
        Task<List<Token>> GetAllToken();
        Task<Token> UpdateToken(int tokenID, TokenVM tokenVM);
        Task<Token> DeleteToken(int tokenID);
        Task<List<Token>> BulkDeleteTokens(List<int> tokenID);
    }
}
