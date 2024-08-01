import { useState } from 'react'
import { useNavigate } from 'react-router-dom'

function AddClient() {
    const [name, setName] = useState("");
    const [residentialAddress, setResidentialAddress] = useState("");
    const [workAddress, setWorkAddress] = useState("");
    const [postalAddress, setPostalAddress] = useState("");
    const [cellNumber, setCellNumber] = useState("");
    const [workNumber, setWorkNumber] = useState("");

    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        fetch("http://localhost:5162/api/Clients", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                name: name,
                residentialAddress: residentialAddress,
                workAddress: workAddress,
                postalAddress: postalAddress,
                cellNumber: cellNumber,
                workNumber: workNumber
            }),
        });
       
        navigate("/");   
    };

    return (
        <>
            <h2>Add Client</h2>

            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    value={name}
                    placeholder="Name"
                    onChange={(e) => setName(e.target.value)}
                />
                <input
                    type="text"
                    value={residentialAddress}
                    placeholder="Residential Address"
                    onChange={(e) => setResidentialAddress(e.target.value)}
                />
                <input
                    type="text"
                    value={workAddress}
                    placeholder="Work Address"
                    onChange={(e) => setWorkAddress(e.target.value)}
                />
                <input
                    type="text"
                    value={postalAddress}
                    placeholder="Postal Address"
                    onChange={(e) => setPostalAddress(e.target.value)}
                />
                <input
                    type="text"
                    value={cellNumber}
                    placeholder="Cell Number"
                    onChange={(e) => setCellNumber(e.target.value)}
                />
                <input
                    type="text"
                    value={workNumber}
                    placeholder="Work Number"
                    onChange={(e) => setWorkNumber(e.target.value)}
                />

                <button type="submit">Create</button>
            </form>
        </>
    );
}

export default AddClient
