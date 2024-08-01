import Clients from "./Clients"
import AddClient from "./AddClient"

import {
    BrowserRouter,
    Routes,
    Route,
} from 'react-router-dom'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
    return (
        <>
            <BrowserRouter>
                <Routes>
                    <Route
                        exact
                        path="/"
                        element={<Clients />}
                    />
                    <Route
                        exact
                        path="/addClient"
                        element={<AddClient />}
                    />
                </Routes>
            </BrowserRouter>
        </>
    );
}

export default App
