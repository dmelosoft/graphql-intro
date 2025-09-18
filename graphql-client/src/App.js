import logo from './logo.svg';
import './App.css';
import Books from './components/Books';
import ApolloProviderWrapper from './Apollo/ApolloProvider.js';


function App() {
  return (
    <div className="App">
      <ApolloProviderWrapper>
      <div>
        <h1>📚 My Books</h1>
        <Books />
      </div>
    </ApolloProviderWrapper>
    </div>
  );
}

export default App;
