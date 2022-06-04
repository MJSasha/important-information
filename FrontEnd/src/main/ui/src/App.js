import './App.css';
import React, {Component} from 'react';

class NewComponent extends React.Component{
  constructor(props){
    super(props);
    this.state = {
      item: null
    };
  }
}

componentDidMount(){
  fetch('http://localhost:8080/hello')
  .then(
    (result) => {
      this.setState({
        item: result.item
      });
    }
  )
}

function App() {

  return (
    <div className="App">
      {item}
    </div>
  );
}

export default App;
