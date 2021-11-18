using ProgrammingQuotesApi.Models;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System.Text.Json;
using System.IO;
using System;
using ProgrammingQuotesApi.Helpers;

namespace ProgrammingQuotesApi.Services
{
    public class QuoteService
    {
        private readonly DataContext _context;

        public QuoteService(DataContext context)
        {
            _context = context;
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            string fileContent = File.ReadAllText("Data/quotes.json");
            List<Quote> quotes = JsonSerializer.Deserialize<List<Quote>>(fileContent, options);
            if (!_context.Quotes.Any())
            {
                _context.Quotes.AddRange(quotes);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Quote> GetAll(int count = 0)
        {
            return (count > 0 && count <= _context.Quotes.Count())
            ? _context.Quotes.Take(count)
            : _context.Quotes;
        }

        public Quote GetById(string id) => _context.Quotes.FirstOrDefault(p => p.Id == id);

        public Quote GetRandom() => _context.Quotes.ToList()[new Random().Next(0, _context.Quotes.Count())];

        public IEnumerable<Quote> GetByAuthor(string author) => _context.Quotes.Where(p => p.Author == author);

        public void Add(Quote quote)
        {
            quote.Id = ObjectId.GenerateNewId().ToString();
            _context.Quotes.Add(quote);
            _context.SaveChanges();
        }

        public void Delete(Quote quote)
        {
            _context.Quotes.Remove(quote);
            _context.SaveChanges();
        }

        public void Update(Quote quote)
        {
            _context.Quotes.Update(quote);
            _context.SaveChanges();
        }
    }
}