"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const path_1 = __importDefault(require("path"));
const quotes_json_1 = __importDefault(require("./quotes.json"));
const app = (0, express_1.default)();
const PORT = 80;
app.listen(PORT, () => {
    console.log(`🚀 Server Started at port ${PORT}`);
});
app.get('/', (req, res) => {
    res.sendFile(path_1.default.join(__dirname, '/index.html'));
});
app.get('/quotes', (req, res) => {
    res.json(quotes_json_1.default);
});
app.get('/quotes/random', (req, res) => {
    const randomIndex = Math.floor(Math.random() * quotes_json_1.default.length);
    const randomQuote = quotes_json_1.default[randomIndex];
    res.send(randomQuote);
});
app.get('/quotes/:id', (req, res) => {
    const id = req.params.id;
    const quote = quotes_json_1.default.find((quote) => quote.id === id);
    if (quote) {
        res.json(quote);
    }
    else {
        res.status(404).json({ error: 'quote not found!' });
    }
});
app.get('/quotes/author/:author', (req, res) => {
    const author = req.params.author;
    const authorQuotes = quotes_json_1.default.filter((quote) => quote.author.toLowerCase() === author.toLowerCase());
    if (authorQuotes) {
        res.json(authorQuotes);
    }
    else {
        res.status(404).json({ error: 'quote not found!' });
    }
});
