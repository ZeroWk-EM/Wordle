﻿using Wordle_Library.Enum;

namespace Wordle_Library
{
    public class Attempt
    {
        private string? _word;
        private List<Position>? _positions = new List<Position>();

        public Attempt(string? _currentWordAttempt)
        {
            Word = _currentWordAttempt;

        }

        public string? Word
        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
            }
        }

        public IEnumerable<Position> Positions
        {
            get
            {
                return _positions;
            }
        }

        public void VerifyPosition(string correctWord)
        {
            for (int i = 0; i < correctWord.Length; i++)
            {
                if (Word[i] == correctWord[i])
                {
                    _positions.Add(Position.Ok);
                    correctWord = correctWord.Replace(Word[i], ' ');
                 }
                else
                {
                    _positions.Add(Position.Missing);
                }
            }

            for (int i = 0; i < correctWord.Length; i++)
            {
                if (_positions[i] != Position.Ok && correctWord.Contains(Word[i]))
                {
                    _positions[i] = Position.Wrong;
                }
            }
        }
    }
}
