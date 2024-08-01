import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'

function Clients() {
    const [clients, setClients] = useState([])

    useEffect(() => {
        fetch('http://localhost:5162/api/Clients')
            .then(response => response.json())
            .then(data => setClients(data))
            .catch(error => console.error('Unable to get items.', error));
    }, []);

    const exportToFile = () => {
        fetch('http://localhost:5162/api/Clients/export')
    };

    const navigate = useNavigate();

    return (
        <>
            <h2>Clients</h2>

            <button onClick={() => navigate("/addClient")}>
                Add Client
            </button>

            <button onClick={() => exportToFile()}>
                Export to File
            </button>

            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Residential Address</th>
                        <th>Work Address</th>
                        <th>Postal Address</th>
                        <th>Cell Number</th>
                        <th>Work Number</th>
                    </tr>
                </thead>

                <tbody>
                    {clients.map(item =>
                        <tr key={item.clientId}>
                            <td>{item.name}</td>
                            <td>{item.residentialAddress}</td>
                            <td>{item.workAddress}</td>
                            <td>{item.postalAddress}</td>
                            <td>{item.cellNumber}</td>
                            <td>{item.workNumber}</td>
                        </tr>)
                    }
                </tbody>
            </table>
        </>
    )
}

export default Clients
