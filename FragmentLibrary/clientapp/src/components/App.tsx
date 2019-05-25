import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { Container } from 'semantic-ui-react';
import Home from './Home';
import Navbar from './Navbar';
import TileDetails from './TileDetails';
import { TileAdd, TileAddAlignment, TileAddSummary } from './TileAdd'

const App: React.FC = () => {
  return (
    <Router>
      <div className="App">
        <Navbar />
        <Container className="page-wrapper">
        <Switch>
          <Route path="/" exact component={Home} />
          <Route path="/tile/add/:id/alignment" exact component={TileAddAlignment} />
          <Route path="/tile/add:id/summary" exact component={TileAddSummary} />
          <Route path="/tile/add" exact component={TileAdd} />
          <Route path="/tile/:id" exact component={TileDetails} />
        </Switch>
        </Container>
      </div>
    </Router>
  );
}

export default App;
