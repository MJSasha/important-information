import React from 'react';
import axios from 'axios';
import { useState, useEffect} from 'react'; // https://www.youtube.com/watch?v=pQibzAjverE&t=226s
import './SideBar.modules.css';
import 'bootstrap/dist/css/bootstrap.min.css';

const DOMEN_SERVER = process.env.REACT_APP_BACK_ROOT ?? 'http://localhost:8080/api';


axios.defaults.withCredentials = true;


function SideBar(){
    const [news, setNews] = useState([])

    useEffect(() => {
        axios.get(DOMEN_SERVER + "/News")
        .then(res => {

           console.log('результат гет---'+ res) // News output

        }).catch(err => console.log('гет кэтч эрор---'+err))
    })

    const postNews = event => {
        event.preventDefault();
        axios.post(DOMEN_SERVER + "/News",{
            message: news,
        })
        .then(res => console.log("отправка новостей--- ", res))
        .catch(err => console.log(err))
    }

    return(
            <div className="d-flex flex-column justify-content-between flex-shrink-0 bg-white" style={{width: 380, height: '80vh'}}>

                <div className="username-wrapper">
                    <h1>UserName</h1>
                </div>

                <div className="news-wrapper">
                    <div className="news">
                        <div className="list-group list-group-flush border-bottom scrollarea">
                            <div className="list-group-item list-group-item py-3 lh-tight">
                                <div className="d-flex w-100 align-items-center justify-content-between">
                                    <strong className="mb-1">Новость 1</strong>
                                    <small className="text-muted">Mon</small>
                                </div>
                                <div className="col-10 mb-1 small">Some placeholder content in a paragraph below the heading and date.</div>
                            </div>
                            <div className="list-group-item list-group-item py-3 lh-tight" aria-current="true">
                                <div className="d-flex w-100 align-items-center justify-content-between">
                                    <strong className="mb-1">Новость 2</strong>
                                    <small className="text-muted">Wed</small>
                                </div>
                                <div className="col-10 mb-1 small">Some placeholder content in a paragraph below the heading and date.</div>
                            </div>
                            <div className="list-group-item list-group-item py-3 lh-tight">
                                <div className="d-flex w-100 align-items-center justify-content-between">
                                    <strong className="mb-1">Новость 3</strong>
                                    <small className="text-muted">Tues</small>
                                </div>
                                <div className="col-10 mb-1 small">Some placeholder content in a paragraph below the heading and date.</div>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="input-group mb-3">
                <input type="text" value={news} onChange={(event) => setNews(event.target.value)} className="form-control" placeholder="Запишите новость" aria-label="Имя пользователя получателя" aria-describedby="button-addon2"/>
                <button className="btn btn-outline-secondary" onClick={postNews} type="button" id="button-addon2">Отправить</button>
                </div>

            </div>
    )
}
export default SideBar;