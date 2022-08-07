import React, { useState } from 'react'
import axios from 'axios';
import Cookies from 'js-cookie';
import './Register_styles.modules.css';
import logo from './img/logo.svg';
import { Form, Button} from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

const DOMEN_SERVER = 'https://a8473-2e1f.s.d-f.pw/api';

function Register(){

    const navigate = useNavigate()

    const [register, setRegister] = useState(() => {
        return {
            login: "",
            password: "",
        }
    })
    const [errMsg, setErrMsg] = useState('')

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
                    console.log('token => '+ token.data)
                    Cookies.set('token', token.data, {expires: 730});
                    navigate('/main')
            })
            .catch(err => {
                if (!err?.response) {
                    setErrMsg('No Server Response');
                } else if (err.response?.status === 400) {
                    setErrMsg('Missing Username or Password');
                } else if (err.response?.status === 401) {
                    setErrMsg('Unauthorized');
                } else {
                    setErrMsg('Login Failed');
                }
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
                    <p className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive" style={{color:'red'}}>{errMsg}</p>
                    <Button className='btn btn-lg btn-primary btn-block' type="submit">
                    Sign in
                    </Button>
                </Form>
            </div>
        </div>
    )
}
export default Register