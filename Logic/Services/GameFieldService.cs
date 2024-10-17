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
        public GameField GenerateGameField(int row, int column)
        {
            var gameField = new GameField(row, column);
            return gameField;
        }
        public GameField SaveGameField(GameField gameField)
        {
            _gameFieldRepository.Insert(gameField);
            var storedGameField = _gameFieldRepository.GetById(gameField.Id);
            if (storedGameField == null)
            {
                throw new Exception("GameField is niet opgeslagen in de database");
            }

            return storedGameField;
        }
    }   
}

