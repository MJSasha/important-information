import React, {useState , useEffect} from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import {CSSTransition} from 'react-transition-group';
import SideBar from '../SideBar/SideBar';
import DayCard from '../DayCard/DayCard';
import './Main.modules.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import ReactDatePicker from '../DatePicker/DatePicker';




function Main(){
    const [getDate, setGetDate] = useState("");
    const [showSB, setShowSB] = useState(false);
    const navigate = useNavigate();
    
    useEffect(() => {
        if (!axios.defaults.headers.common['Authorization']) {
            navigate('/')
        }
    })

    return (
        <div className="wrapper">
        
            <header className="header container-fluid d-flex flex-wrap align-items-center justify-content-center justify-content-md-between mb-1 border-bottom">
                <div className=" col-md-3 mb-md-0 text-light">ImpInfWeb</div>
                <div className="col-md-3 text-end group-btn my-1">
                    <button type="button" className="btn btn-outline-primary me-2 news-btn" onClick={()=>setShowSB(!showSB)}>Новости</button>
                    <button onClick={ () => { delete axios.defaults.headers.common["Authorization"]; navigate('/') } } className="logout-btn btn btn-primary">Logout</button>
                </div>
            </header>

            <div className="hero">
                <div className="d-flex flex-row">
                    <div className="container">

                        {/* date */}
                        <div className="parent-datepicker-wrapper">
                        <ReactDatePicker passDate={setGetDate}/>
                        </div>
                        <div>{getDate}</div>
                        {/* date */}
                        
                        <div class="overflow-hidden align-items-center">
                            <DayCard/>
                        </div>

                    </div>
                    <CSSTransition in={showSB} timeout={300} classNames='fade' unmountOnExit>
                        <div className="SideBarWrapper">
                            <SideBar SBIsOpen={showSB}/>
                        </div>
                    </CSSTransition>
                </div>
            </div>
        </div>
    )
}
export default Main