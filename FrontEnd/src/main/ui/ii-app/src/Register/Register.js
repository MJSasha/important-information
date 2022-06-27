import React, { useState } from 'react'
import axios from 'axios';
import Cookies from 'js-cookie';
import './Register_styles.css';
import logo from './img/logo.svg';
import { Form, Button} from 'react-bootstrap';
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

    return (
        <div className="text-center">
            <div className="form-wrapper form-signin">
                <img className='mb-4' src={logo} alt="lookbook" />
                <Form onSubmit={submitCheckin}>
                    <Form.Group className="mb-3" controlId="formGroupLogin">
                        <Form.Label className='h3 mb-3 font-weight-normal text-light'>Login</Form.Label>
                        <Form.Control
                        required
                        autoFocus
                        className='form-control'
                        type="login"
                        name="login"
                        placeholder="Enter login"
                        value={register.login}
                        onChange={changeInputRegister}
                        />
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="formGroupPassword">
                        <Form.Label className='h3 mb-3 font-weight-normal text-light'>Password</Form.Label>
                        <Form.Control
                        required
                        className='form-control'
                        type="password"
                        name="password"
                        placeholder="Password"
                        value={register.password}
                        onChange={changeInputRegister}
                        />
                    </Form.Group>
                    <Button className='btn btn-lg btn-primary btn-block' type="submit">
                    Sign in
                    </Button>
                </Form>
            </div>
        </div>
    )
}
export default Register;