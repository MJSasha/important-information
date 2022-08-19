import React, {useState} from 'react';
import { useNavigate } from 'react-router-dom';
import Cookies from 'js-cookie';
import {CSSTransition} from 'react-transition-group';
import SideBar from '../SideBar/SideBar';
import './Main.modules.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import ReactDatePicker from '../DatePicker/DatePicker'




function Main(){
    const [showSB, setShowSB] = useState(false);
    const navigate = useNavigate();

    const UserLogout = () => {
        Cookies.remove('token');
        console.log('After logout token is => ' + Cookies.get('token'))
        navigate('/')
    }

    return (
        <div className="wrapper">
            <div className="shadow-box">
                <header className="header d-flex flex-wrap align-items-center justify-content-center justify-content-md-between py-3 mb-4 border-bottom">
                    <h1 className="d-flex align-items-center col-md-3 mb-2 mb-md-0 text-dark text-decoration-none">LOGO</h1>
                    <div className="col-md-3 group-btn">
                        <button type="button" className="btn btn-outline-primary me-2 news-btn" onClick={()=>setShowSB(!showSB)}>Новости</button>
                        <button onClick={UserLogout} className="logout-btn btn btn-primary">Logout</button>
                    </div>
                </header>
            </div>
            <div className="content-wrapper">
                {/* date */}
                <div className="parent-datepicker-wrapper">
                <ReactDatePicker />
                </div>
                {/* date */}
                <div className="container">
                    <div className="content">Lorem ipsum dolor sit amet consectetur adipisicing elit. Doloremque quaerat consequatur atque architecto illo debitis dolorem, molestias nulla porro dolor veniam totam quisquam? Sapiente odio maiores, culpa corrupti numquam dolorem?</div>

                </div>
                <CSSTransition in={showSB} timeout={300} classNames='fade' unmountOnExit>
                <div className="SideBarWrapper">
                    <SideBar/>
                </div>
                </CSSTransition>
            </div>
        </div>
    )
}
export default Main