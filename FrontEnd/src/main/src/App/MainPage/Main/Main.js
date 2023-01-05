import React, {useState , useEffect} from 'react';
import axios from 'axios';
import Cookies from "js-cookie";
import { useNavigate } from 'react-router-dom';
import {CSSTransition} from 'react-transition-group';
import SideBar from '../SideBar/SideBar';
import DayCard from '../DayCard/DayCard';
import './Main.modules.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

const DOMEN_SERVER = process.env.REACT_APP_BACK_ROOT ?? 'http://localhost:8080/api';
axios.defaults.withCredentials = true;


function Main(){
    const [showSB, setShowSB] = useState(false);
    const [startDate, setStartDate] = useState(new Date());
    const [firstDayOfWeek, setFirstDayOfWeek] = useState('');
    const [lastDayOfWeek, setLastDayOfWeek] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        if (!Cookies.get('token')) {
            navigate('/')
        }
    })

    useEffect(() => {
        setFirstDayOfWeek(new Date(startDate).setDate(startDate.getDate() - startDate.getDay() + (startDate.getDay() === 0 ? -6:1)));
        setLastDayOfWeek(new Date(startDate).setDate(startDate.getDate() - startDate.getDay()+7));
        // setArrayOfDays(Array(7).fill(new Date(firstdayofweek)).map((elem, idx) =>
        // new Date(elem.setDate(elem.getDate() - elem.getDay() + idx + 1)).toLocaleDateString().split('.').reverse().join('-')));
        
    }, [startDate])

    useEffect(() => {
        axios.post(DOMEN_SERVER + '/Days/ByDates', {
            start: firstDayOfWeek + 'T19:07:08.615Z',
            end: lastDayOfWeek + 'T19:07:08.615Z',
        })
        .then(res => {
            console.log('получить все пары в этот день---'+res)

        })
        .catch(err => {
            console.log(err)
        })
    }, [startDate])

    return (
        <div className="wrapper">
        
            <header className="header container-fluid d-flex flex-wrap align-items-center justify-content-between mb-1 border-bottom">
                <div className="col-md-3 mb-md-0 text-light">ImpInfWeb</div>
                <div className="col-md-3 text-end group-btn my-1">
                    <button type="button" className="btn btn-outline-primary news-btn" onClick={()=>setShowSB(!showSB)}>Новости</button>
                    <button onClick={ () => { Cookies.remove('token'); navigate('/') } } className="logout-btn btn btn-primary">Logout</button>
                </div>
            </header>

            <div className="hero">
                <div className="d-flex flex-row">
                    <div className="container">
                        {/* date */}
                        <div className="d-sm-flex flex-sm-row justify-content-between m-3">
                            <div className='head-text'>Расписание</div>
                            <div className='mb-3 pt-2'>
                                <div className="text-black-50">Выберите неделю</div>
                                <DatePicker
                                closeOnScroll={true}
                                todayButton="Вернуться к сегодняшнему дню"
                                dateFormat="yyyy-MM-dd"
                                calendarStartDay={1}
                                selected={startDate} 
                                onChange={(date) => setStartDate(date)} 
                                />
                            </div>
                        </div>
                        {/* date */}
                        
                        <div className="overflow-hidden align-items-center">
                            {/* {
                                arrayOfDays.map((dateOfDay, indexOfDate) =>
                                    <div key={indexOfDate} className=''> */}
                                        <DayCard day={}/>
                                    {/* </div>
                                )
                            } */}
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