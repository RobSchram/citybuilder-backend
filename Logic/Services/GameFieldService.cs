using Logic.Interfaces;
using Logic.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class GameFieldService : IGameFieldService
    {
        public readonly IGameFieldRepository _gameFieldRepository;
        public readonly IUserRepository _userRepository;
        public GameFieldService(IGameFieldRepository gameFieldRepository, IUserRepository userRepository)
        {
            _gameFieldRepository = gameFieldRepository;
            _userRepository = userRepository;
        }
        public async Task<GameField> GenerateGameField(int row, int column)
        {
            var gameField = new GameField(row, column);

            return gameField;
        }

        public async Task SaveGameField(GameField gameField)
        {
            await _gameFieldRepository.Insert(gameField);
        }
        public async Task<GameField> GetGameFieldById(int id)
        {
            var gamefield = await _gameFieldRepository.GetById(id);
            return gamefield;
        }
        public async Task<List<GameField>> GetAllGameFields(int userId)
        {
            List<GameField> gameFields = await _gameFieldRepository.GetAllGameFieldsForUser(userId);
            return gameFields;
        }
        public async Task AddUserToGameField(string username, int gameFieldId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user != null)
            {
                var gameField = await _gameFieldRepository.GetById(gameFieldId);
                if (gameField == null)
                {
                    throw new Exception($"GameField with ID {gameFieldId} not found.");
                }
                if (!gameField.usersId.Contains(user.Id))
                {
                    await _gameFieldRepository.AddUserToGameField(gameField, user.Id);
                }
                else
                {
                    throw new Exception($"User ID {user.Id} is already associated with GameField ID {gameFieldId}.");
                }
            }
            else
            {
                throw new Exception($"No user was found that is called {username}.");
            }
        }
    }   
}

