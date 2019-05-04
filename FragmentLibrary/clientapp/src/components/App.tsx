import React from 'react';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { Container } from 'semantic-ui-react';
import Home from './Home';
import Navbar from './Navbar';
import TileDetails from './TileDetails';

const App: React.FC = () => {
  return (
    <Router>
      <div className="App">
        <Navbar />
        <Container className="page-wrapper">
          <Route path="/" exact component={Home} />
          <Route path="/tile/:id" component={TileDetails} />
        </Container>
      </div>
    </Router>
  );
}

export default App;
