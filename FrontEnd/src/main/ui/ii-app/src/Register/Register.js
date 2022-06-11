import React, { useState } from 'react'
import axios from 'axios';

const DOMEN_SERVER = 'http://localhost:8080/api';

function Register(){
    const [register, setRegister] = useState(() => {
        return {
            login: "",
            password: "",
        }
    })

    const changeInputRegister = event => {
        event.persist()
        setRegister(prev => {
            return {
                ...prev,
                [event.target.name]: event.target.value,
            }
        })
    }
     
    const submitCheckin = event => {
        event.preventDefault();

            axios.post(DOMEN_SERVER + "/auth", {
                login: register.login,
                password: register.password,
            })
            .then(token => {
                if (token) {
                    localStorage.setItem('token', token);
                    alert("Success", token)
                    // next step after auth
                    // window.location.href = DOMEN_SITE + "/auth"
                } else { // why it's here??
                    alert("Fail", token)
                }
            })
            .catch(err => {
                alert(err)
            })
        
    }
    return (
        <div className="form">
            <h2>Register:</h2>
            <form onSubmit={submitCheckin}>
                <p>Login: <input 
                type="login"
                id="login"
                name="login"
                value={register.login}
                onChange={changeInputRegister}
                /></p>
                <p>Password: <input 
                type="password"
                id="password"
                name="password"
                value={register.password}
                onChange={changeInputRegister}
                /></p>
                <input type="submit"/>
            </form>
        </div>
    )
}



export default Register;