import React from 'react';
import styles from'./SideBar.modules.css';
import 'bootstrap/dist/css/bootstrap.min.css';



function SideBar(){

    return(
            <div className={`d-flex flex-column align-items-stretch flex-shrink-0 bg-white ${styles.wrapper}`} style={{width: 380}}>
                <a href="/" className="d-flex align-items-center flex-shrink-0 p-3 a-dark text-decoration-none border-bottom">
                <span className="fs-5 fw-semibold">List group</span>
                </a>
                <div className="list-group list-group-flush border-bottom scrollarea">
                    <p className="list-group-item list-group-item-action active py-3 lh-tight">
                        <div className="d-flex w-100 align-items-center justify-content-between">
                        <strong className="mb-1">List group item heading</strong>
                        <small>Wed</small>
                        </div>
                        <div className="col-10 mb-1 small">Some placeholder content in a paragraph below the heading and date.</div>
                    </p>
                    <p className="list-group-item list-group-item-action py-3 lh-tight">
                        <div className="d-flex w-100 align-items-center justify-content-between">
                        <strong className="mb-1">List group item heading</strong>
                        <small className="text-muted">Tues</small>
                        </div>
                        <div className="col-10 mb-1 small">Some placeholder content in a paragraph below the heading and date.</div>
                    </p>
                    <p className="list-group-item list-group-item-action py-3 lh-tight">
                        <div className="d-flex w-100 align-items-center justify-content-between">
                        <strong className="mb-1">List group item heading</strong>
                        <small className="text-muted">Mon</small>
                        </div>
                        <div className="col-10 mb-1 small">Some placeholder content in a paragraph below the heading and date.</div>
                    </p>
                </div>
            </div>
    )
}
export default SideBar;