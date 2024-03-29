import React from 'react';
import axios from 'axios';
import { useState, useEffect, useRef} from 'react'; // https://www.youtube.com/watch?v=pQibzAjverE&t=226s
import './SideBar.modules.css';
import 'bootstrap/dist/css/bootstrap.min.css';

const DOMEN_SERVER = process.env.REACT_APP_BACK_ROOT ?? 'http://localhost:8080/api';


axios.defaults.withCredentials = true;


function SideBar(props){
    const [news, setNews] = useState([])
    const [gotNews, getNews] = useState([])
    const divRef = useRef();
    
    const scrollToBottom = () => {
        divRef.current.scrollIntoView({
            behavior: "smooth",
            block: "end", 
            inline: "nearest",
            })
      }
    
      useEffect(() => {
        setTimeout((() => (scrollToBottom())), 1200)
      }, [news]);

    if (props.SBIsOpen){
        var getData = setInterval(() => {
                axios.post(DOMEN_SERVER + "/News/ByDates",{
                    start: getStartDate(),
                    end: getEndDate()
                })
                .then(res => {
                    clearInterval(getData)
                    getNews(res.data)
                }).catch(err => console.log('гет кэтч эрор---'+err));
        }, 1000)
    }

    const postNews = event => {
        event.preventDefault();
        axios.post(DOMEN_SERVER + "/News",{
            message: news,
            "needToSend": true,
        })
        .then(res => console.log("отправка новостей--- ", res))
        .catch(err => console.log(err))
        setNews([])
    }

    const getEndDate = () => {
        var today = new Date(),
        endDate = today.getFullYear() + '-' + ('0' + (today.getMonth()+1)).slice(-2)+ '-' +('0' + (today.getDate()+1)).slice(-2);
        return endDate;
      };
      const getStartDate = () => {
        var today = new Date(),
        startDate = today.getFullYear() + '-' + ('0' + (today.getMonth())).slice(-2)+ '-' +('0' + today.getDate()).slice(-2);
        return startDate;
      };

    return(
            <div className="d-flex flex-column justify-content-between flex-shrink-0 bg-white rounded SB-main-wrapper" style={{width: 320}}>
                <div className="username-wrapper text-light">
                    <h1>UserName</h1>
                </div>
                <div className="news">
                    <div className="list-group list-group-flush overflow-auto news-scrollarea transition">
                        {
                            gotNews.map(text => (
                                <div key={text.id} className="list-group-item list-group-item py-3 lh-tight border border-secondary my-1 ms-1 rounded" aria-current="true">
                                    <div className="d-flex w-100 align-items-center justify-content-between">
                                        <div className="mb-1 fw-weight-bold news-txt">{text.message}</div>
                                        <small className="badge bg-secondary rounded-pill">{(text.dateTimeOfCreate).substring(0, 10)}</small>
                                    </div>
                                    {/* <div className="col-10 mb-1 small">------</div> */}
                                </div>
                            ))
                        }
                        <div ref={divRef} />
                    </div>
                </div>
                <form className='postNews mx-1' onSubmit={postNews}>
                    <div className="input-group mb-3">
                        <input autoFocus type="text" value={news} onChange={(event) => {setNews(event.target.value)}} className="form-control" placeholder="Запишите новость" aria-label="Имя пользователя получателя" aria-describedby="button-addon2"/>
                        <button className="btn btn-outline-light ms-1" type="submit" id="button-addon2">Отправить</button>
                    </div>
                </form>
            </div>
    )
}
export default SideBar;