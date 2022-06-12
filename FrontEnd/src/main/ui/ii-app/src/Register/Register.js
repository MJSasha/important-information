import React, { useState } from 'react'
import axios from 'axios';
import Cookies from 'js-cookie';

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
                    console.log(token.data)
                    Cookies.set('token', token.data, {expires: 730});
                    alert("Success = " + token.data)
                    // next step after auth
                    // window.location.href = DOMEN_SITE + "/auth"
            })
            .catch(err => {
                alert(err)
            })
    }

    const getAllUsers = event => {
        event.preventDefault();

        axios.get(DOMEN_SERVER + "/users")
        .then(response => {
            console.log(response.data)
        })
        .catch(err => {
            console.log(err)
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
            <form onSubmit={getAllUsers}>
                <input type="submit" value="getUsers"/>
            </form>

        </div>
        
    )
}



export default Register;