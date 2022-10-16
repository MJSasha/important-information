import React, {useState , useEffect} from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import {CSSTransition} from 'react-transition-group';
import SideBar from '../SideBar/SideBar';
import './Main.modules.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import ReactDatePicker from '../DatePicker/DatePicker';
// imgs imp
import pinkPlanet from './img/pinkPlanet.png';
import orangePlanet from './img/orangePlanet.png';
import orangeAsteroid from './img/orangeAsteroid.png';
import bluePlanet from './img/bluePlanet.png';
import astronaut from './img/astronaut.png';
// 



function Main(){
    
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
                <h1 className=" align-items-center col-md-3 mb-md-0 text-dark text-decoration-none">LOGO</h1>
                <div className="col-md-3 text-end group-btn">
                    <button type="button" className="btn btn-outline-primary me-2 news-btn" onClick={()=>setShowSB(!showSB)}>Новости</button>
                    <button onClick={ () => { delete axios.defaults.headers.common["Authorization"]; navigate('/') } } className="logout-btn btn btn-primary">Logout</button>
                </div>
            </header>

            <div className="hero">
                <div className="d-flex flex-row">
                    <img className='pinkPlanet' src={pinkPlanet} alt="pinkPlanet"/>
                    <img className='orangePlanet' src={orangePlanet} alt="orangePlanet"/>
                    <img className='orangeAsteroid' src={orangeAsteroid} alt="orangeAsteroid"/>
                    <img className='bluePlanet' src={bluePlanet} alt="bluePlanet"/>
                    <img className='astronaut' src={astronaut} alt="astronaut"/>
                    <div className="container">

                        {/* date */}
                        <div className="parent-datepicker-wrapper">
                        <ReactDatePicker />
                        </div>
                        {/* date */}
                        
                        {/* cards */}
                        <div className="row row-cols-1 row-cols-md-3 g-4 cards-wrapper">
                            <div className="col">
                                <div className="card text-dark bg-light shadow-outline">
                                    <div className="card-body">
                                        <h5 className="card-title">Заголовок карточки</h5>
                                        <p className="card-text"></p>
                                    </div>
                                </div>
                            </div>
                            <div className="col ">
                                <div className="card text-dark bg-light shadow-outline">
                                    <div className="card-body">
                                        <h5 className="card-title">Заголовок карточки</h5>
                                        <p className="card-text">Это более длинная карта С вспомогательным текстом ниже в качестве естественного перехода к дополнительному контенту. Этот контент немного длиннее.</p>
                                    </div>
                                </div>
                            </div>
                            <div className="col ">
                                <div className="card text-dark bg-light shadow-outline">
                                    <div className="card-body">
                                        <h5 className="card-title">Заголовок карточки</h5>
                                        <p className="card-text">Это более длинная карта С вспомогательным текстом ниже в качестве естественного перехода к дополнительному контенту. Этот контент немного длиннее.</p>
                                    </div>
                                </div>
                            </div>
                            <div className="col ">
                                <div className="card text-dark bg-light shadow-outline">
                                    <div className="card-body">
                                        <h5 className="card-title">Заголовок карточки</h5>
                                        <p className="card-text">Это более длинная карта С вспомогательным текстом ниже в качестве естественного перехода к дополнительному контенту. Этот контент немного длиннее.</p>
                                    </div>
                                </div>
                            </div>
                            <div className="col ">
                                <div className="card text-dark bg-light shadow-outline">
                                    <div className="card-body">
                                        <h5 className="card-title">Заголовок карточки</h5>
                                        <p className="card-text">Это более длинная карта С вспомогательным текстом ниже в качестве естественного перехода к дополнительному контенту.</p>
                                    </div>
                                </div>
                            </div>
                            <div className="col ">
                                <div className="card text-dark bg-light shadow-outline">
                                    <div className="card-body">
                                        <h5 className="card-title">Заголовок карточки</h5>
                                        <p className="card-text">Это более длинная карта С вспомогательным текстом ниже в качестве естественного перехода к дополнительному контенту. Этот контент немного длиннее.</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        {/* cards */}
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