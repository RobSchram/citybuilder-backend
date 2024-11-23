using Logic.Interfaces;
using Logic.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class GameFieldService : IGameFieldService
    {
        public readonly IGameFieldRepository _gameFieldRepository;
        public GameFieldService(IGameFieldRepository gameFieldRepository)
        {
            _gameFieldRepository = gameFieldRepository;
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
    }   
}

