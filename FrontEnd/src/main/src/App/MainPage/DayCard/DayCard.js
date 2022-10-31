import React, { useState, useEffect } from "react"
import axios from "axios";


const DOMEN_SERVER = process.env.REACT_APP_BACK_ROOT ?? 'http://localhost:8080/api';
axios.defaults.withCredentials = true;

function DayCard(props){

    const [gotLessons, setLessons] = useState([])
    // const ConvertDate = () => { 

    // }

    // useEffect(() => {
    //     console.log(props.day+ 'T00:00:00.000Z')
    //     axios.post(DOMEN_SERVER + '/Days/ByDate', {
    //         dateTime: props.day + 'T19:07:08.615Z',
    //     })
    //     .then(res => {
    //         console.log(res)
    //         setLessons(res.data)
    //     })
    //     .catch(err => {
    //         console.log(err)
    //     })
    // })

    return(
        <div className="card bg-light h-100">
            <div className="card-header">{props.day}</div>
            <div className="card-body">
                <div className="card-title">Расписание</div>
                <div className="">
                    {
                        gotLessons.map(lesson => (
                            <div key={lesson.id} className="">
                                {lesson.name}
                            </div>
                        ))
                    }
                </div>
                <div className="second-card-title mt-3">Тут информация</div>
                <div className="card-text fst-italic">Some quick example text to build on the card title and make up the bulk of the card's content.</div>
            </div>
        </div>
    )
}
export default DayCard;