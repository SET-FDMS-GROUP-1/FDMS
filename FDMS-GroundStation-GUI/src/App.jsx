// NAME          : src/App.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-23
// DESCRIPTION   : Main application component, providing global context and layout.


import './App.css'
import "bootstrap/dist/css/bootstrap.min.css";
import { GlobalProvider } from './context/GlobalContext.jsx';
import Header from './components/Header/Header.jsx';
import FlightManagmentPanel from './components/FlightManagmentPanel/FlightManagmentPanel.jsx';


// FUNCTION    : App
// DESCRIPTION : Main application component, providing global context and layout.
// PARAMETERS  : None
// RETURNS     : JSX.Element - App component
function App() {
  return (
    <GlobalProvider>
      <div className="d-flex flex-column h-100 bg-panel-primary">
        <Header />
        <div className="flex-fill">
          <FlightManagmentPanel />
        </div>
      </div>
    </GlobalProvider>
  )
}

export default App
