import Home from './components/Home';
import Navbar from './components/Navbar';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Discussion from './components/Discussion';

function App() {
  return (
    <div className="App">
      <Router>
        <Navbar />
        <Switch>
          <Route exact path="/">
            <Home />
          </Route>
          <Route path = "/discussion/:id">
            <Discussion></Discussion>
          </Route>
        </Switch>
      </Router>
    </div>
  );
}

export default App;
