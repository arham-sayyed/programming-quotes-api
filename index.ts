import express, { Express, Request, Response } from 'express';
import path from 'path';
import quotes from './quotes.json';
const app = express();

const PORT = 80;

// interfaces

interface Quote {
  id: string;
  author: string;
  en: string;
}

app.listen(PORT, () => {
  console.log(`🚀 Server Started at port ${PORT}`);
});

app.get('/', (req: Request, res: Response) => {
  res.sendFile(path.join(__dirname, '/index.html'));
});

app.get('/quotes', (req: Request, res: Response) => {
  res.json(quotes);
});

app.get('/quotes/random', (req: Request, res: Response) => {
  const randomIndex: number = Math.floor(Math.random() * quotes.length);
  const randomQuote: Quote = quotes[randomIndex] as Quote;
  res.send(randomQuote);
});

app.get('/quotes/:id', (req: Request, res: Response) => {
  const id: string = req.params.id;

  const quote: Quote | undefined = quotes.find(
    (quote) => quote.id === id
  ) as Quote;

  if (quote) {
    res.json(quote);
  } else {
    res.status(404).json({ error: 'quote not found!' });
  }
});

app.get('/quotes/author/:author', (req: Request, res: Response) => {
  const author: string = req.params.author;
  const authorQuotes: Quote[] = quotes.filter(
    (quote) => quote.author.toLowerCase() === author.toLowerCase()
  ) as Quote[];

  if (authorQuotes) {
    res.json(authorQuotes);
  } else {
    res.status(404).json({ error: 'quote not found!' });
  }
});
