import React from 'react';
import logo from './logo.svg';
import './App.css';
import { WeatherForecast } from './WeatherForecast';
import Register from './Account/Register';
import Login from './Account/Login';

function App() {
  return (
    <div>
      <Login />
      <hr />
      <Register />
      <hr />
      <WeatherForecast />
    </div>
  );
}

export default App;