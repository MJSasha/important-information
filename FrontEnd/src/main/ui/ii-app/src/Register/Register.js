import React, { useState } from 'react'
import axios from 'axios';
import Cookies from 'js-cookie';
import './Register_styles.css';
import 'bootstrap/dist/css/bootstrap.min.css';

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

        axios.get(DOMEN_SERVER + "/users", {withCredentials: true})
        .then(response => {
            console.log(response.data)
        })
        .catch(err => {
            console.log(err)
        })
    }

    return (
        <div className='wrapper'>
            <div className="form">
                <h1>Register:</h1>
                <form onSubmit={submitCheckin}>
                    <div className="login-wrapper">
                        <p>Login: </p>
                        <input 
                        type="login"
                        id="login"
                        name="login"
                        value={register.login}
                        onChange={changeInputRegister}
                        />
                    </div>
                    <div className="password-wrapper">
                        <p>Password: </p>
                        <input 
                        type="password"
                        id="password"
                        name="password"
                        value={register.password}
                        onChange={changeInputRegister}
                        />
                    </div>
                    <div className='sign-in-btn'>
                    <input id='signin' type="submit" value="Sign in"/>
                    </div>
                </form>
                <form onSubmit={getAllUsers}>
                    <input id='getusers' type="submit" value="getUsers"/>
                </form>
            </div>
        </div>
    )
}



export default Register;