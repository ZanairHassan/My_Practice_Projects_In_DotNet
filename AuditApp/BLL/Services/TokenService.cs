using BLL.Interfaces;
using DAL.DBContext;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly AuditAppDBContext _auditAppDBContext;
        public TokenService(AuditAppDBContext auditAppDBContext)
        {
            _auditAppDBContext = auditAppDBContext;
        }
        public async Task<Token> CreateToken(TokenVM tokenVM)
        {
            Token token = new Token();
            token.CreatedDate = DateTime.Now;
            token.UserID = tokenVM.UserID;
            token.JwtToken = tokenVM.JwtToken;
            token.IsDeleted = tokenVM.IsDeleted;
            token.IsExpired = tokenVM.IsExpired;
            await _auditAppDBContext.Tokens.AddAsync(token);
            await _auditAppDBContext.SaveChangesAsync();
            return token;
        }

        public async Task<Token> DeleteToken(int tokenID)
        {
            var token = await _auditAppDBContext.Tokens.FirstOrDefaultAsync(x => x.TokenID == tokenID);
            if (token != null)
            {
                token.IsDeleted = true;
                await _auditAppDBContext.SaveChangesAsync();
            }

            return token;
        }

        public async Task<List<Token>> GetAllToken()
        {
            return await _auditAppDBContext.Tokens.Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Token> GetToken(int tokenID)
        {
            return await _auditAppDBContext.Tokens.FirstOrDefaultAsync(x => x.TokenID == tokenID);
        }

        public async Task<Token> UpdateToken(int tokenID, TokenVM tokenVM)
        {
            var token = await _auditAppDBContext.Tokens.FirstOrDefaultAsync(x => x.TokenID == tokenID);
            if (token != null)
            {
                token.UserID = tokenVM.UserID;
                token.JwtToken = tokenVM.JwtToken;
                _auditAppDBContext.Tokens.Update(token);
                await _auditAppDBContext.SaveChangesAsync();
            }

            return token;
        }


        public Task<Token> GetTokenByjwtToken(string token)
        {
            return _auditAppDBContext.Tokens
                .Where(x => x.IsExpired == false && x.JwtToken == token)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<Token>> GetTokenByUserId(int UserId)
        {
            return await _auditAppDBContext.Tokens.Where(x => x.UserID == UserId).AsNoTracking().ToListAsync();
        }

        public async Task<Token> GetTokenByUserID(int userId)
        {
            return await _auditAppDBContext.Tokens.FirstOrDefaultAsync(x => x.UserID == userId && x.IsExpired == false);

        }

        public async Task<bool> InvalidatejwtToken(string jwttoken)
        {
            var token = await _auditAppDBContext.Tokens.FirstOrDefaultAsync(x => x.JwtToken == jwttoken);
            if (token != null)
            {
                token.IsExpired = true;
                await _auditAppDBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InvalidatejwtTokenByID(int UserId)
        {
            var token = await _auditAppDBContext.Tokens.FirstOrDefaultAsync(x => x.UserID == UserId);
            if (token != null)
            {
                token.IsExpired = true;
                await _auditAppDBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Token>> ExpireTokensByUserId(int userId)
        {
            var tokens = await _auditAppDBContext.Tokens
                .Where(t => t.UserID == userId && !t.IsExpired)
                .AsNoTracking()
                .ToListAsync();

            if (tokens.Any())
            {
                foreach (var token in tokens)
                {
                    token.IsExpired = true;
                }

                _auditAppDBContext.Tokens.UpdateRange(tokens);
                await _auditAppDBContext.SaveChangesAsync(); // Persist the changes to the database
            }
            return null;
        }

        public async Task<List<Token>> BulkDeleteTokens(List<int> tokenID)
        {
            var delTokens = await _auditAppDBContext.Tokens
                .Where(f => tokenID
                .Contains(f.TokenID) && (f.IsDeleted == null || f.IsDeleted == false))
                .AsNoTracking()
                .ToListAsync();
            foreach (var token in delTokens)
            {
                token.IsDeleted = true;
            }

            _auditAppDBContext.Tokens.UpdateRange(delTokens);
            await _auditAppDBContext.SaveChangesAsync();
            return delTokens;
        }

    }
} 

