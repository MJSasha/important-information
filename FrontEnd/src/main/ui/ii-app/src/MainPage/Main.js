import React from 'react';
import './Main.css'
import 'bootstrap/dist/css/bootstrap.min.css';




function Main(){

    return (
        <div class="shadow-box">
            <header class="header d-flex flex-wrap align-items-center justify-content-center justify-content-md-between py-3 mb-4 border-bottom">
                <h1 class="d-flex align-items-center col-md-3 mb-2 mb-md-0 text-dark text-decoration-none logo">LOGO</h1>

                <ul class="nav col-12 col-md-auto mb-2 justify-content-center mb-md-0">
                
                </ul>

                <div class="col-md-3 group-btn">
                    <button type="button" class="btn btn-outline-primary me-2">Button</button>
                    <button type="button" class="btn btn-primary">Logout</button>
                </div>
            </header>
        </div>
    )
}
export default Main