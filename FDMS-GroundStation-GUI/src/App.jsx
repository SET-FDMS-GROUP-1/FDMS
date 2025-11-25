import './App.css'
import "bootstrap/dist/css/bootstrap.min.css";
import { GlobalProvider } from './context/GlobalContext.jsx';
import Header from './components/Header/Header.jsx';

function App() {
  return (
    <GlobalProvider>
      <Header />
      <h1>FDMS Ground Station GUI. In Development...</h1>
    </GlobalProvider>
  )
}

export default App
