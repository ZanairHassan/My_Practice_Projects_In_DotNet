using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        Task<Token> CreateToken(TokenVM tokenVM);
        Task<List<Token>> GetAllTokens();
        Task<Token> GetToken(int tokenID);
        Task<Token> UpdateToken(int tokenId, TokenVM tokenVM);
        Task<Token> DeleteToken(int tokenId);
    }
}
