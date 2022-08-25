import React from 'react';
import axios from 'axios';
import { useState, useEffect} from 'react'; // https://www.youtube.com/watch?v=pQibzAjverE&t=226s
import './SideBar.modules.css';
import 'bootstrap/dist/css/bootstrap.min.css';

const DOMEN_SERVER = process.env.REACT_APP_BACK_ROOT ?? 'http://localhost:8080/api';

function SideBar(){
    const [news, setNews] = useState([])

    useEffect(() => {
        axios.get(DOMEN_SERVER + "/?")
        .then(res => {
            // News output
        }).catch(err => console.log(err))
    })

    const postNews = (event) => {
        event.preventDefault();
        axios.post(DOMEN_SERVER + "/?", {
            text: news.text,
        }).then(res => console.log("posting news... ", res)).catch(err => console.log(err))
    }

    return(
            <div className="d-flex flex-column justify-content-between flex-shrink-0 bg-white" style={{width: 380, height: 820}}>

                <div className="username-wrapper">
                    <h1>UserName</h1>
                </div>

                <div className="news-wrapper">
                    <div className="news">
                        <div class="list-group list-group-flush border-bottom scrollarea">
                            <p class="list-group-item list-group-item py-3 lh-tight">
                                <div class="d-flex w-100 align-items-center justify-content-between">
                                <strong class="mb-1">Новость 1</strong>
                                <small class="text-muted">Mon</small>
                                </div>
                                <div class="col-10 mb-1 small">Some placeholder content in a paragraph below the heading and date.</div>
                            </p>

                            <p class="list-group-item list-group-item py-3 lh-tight" aria-current="true">
                                <div class="d-flex w-100 align-items-center justify-content-between">
                                <strong class="mb-1">Новость 2</strong>
                                <small class="text-muted">Wed</small>
                                </div>
                                <div class="col-10 mb-1 small">Some placeholder content in a paragraph below the heading and date.</div>
                            </p>
                            <p class="list-group-item list-group-item py-3 lh-tight">
                                <div class="d-flex w-100 align-items-center justify-content-between">
                                <strong class="mb-1">Новость 3</strong>
                                <small class="text-muted">Tues</small>
                                </div>
                                <div class="col-10 mb-1 small">Some placeholder content in a paragraph below the heading and date.</div>
                            </p>
                        </div>
                    </div>
                </div>

                <div class="input-group mb-3">
                <input type="text" value={news} onChange={(event) => setNews(event.target.value)} class="form-control" placeholder="Запишите новость" aria-label="Имя пользователя получателя" aria-describedby="button-addon2"/>
                <button class="btn btn-outline-secondary" onClick={postNews} type="button" id="button-addon2">Отправить</button>
                </div>

            </div>
    )
}
export default SideBar;