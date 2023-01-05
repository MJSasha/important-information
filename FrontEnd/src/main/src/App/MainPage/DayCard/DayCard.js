import React, { useState, useEffect } from "react"



function DayCard(props){


    return(
        <div className="card bg-light h-100">
            <div className="card-header">{props.day}</div>
            <div className="card-body">
                <div className="card-title">Расписание</div>
                <div className="">
                    {/* {
                        gotLessons.map(data => (
                            <div key={data.lesson.id} className="">
                                {data.lesson.name}
                            </div>
                        ))
                    } */}
                </div>
                <div className="second-card-title mt-3">Тут информация</div>
                <div className="card-text fst-italic">Some quick example text to build on the card title and make up the bulk of the card's content.</div>
            </div>
        </div>
    )
}
export default DayCard;