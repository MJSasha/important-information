import React from 'react';
import styles from'./SideBar.modules.css';
import 'bootstrap/dist/css/bootstrap.min.css';



function SideBar(){

    return(
            <div className={`d-flex flex-column align-items-stretch flex-shrink-0 bg-white ${styles.wrapper}`} style={{width: 380}}>
                <div className="username-wrapper">
                    <h1>UserName</h1>
                </div>
                



            </div>
    )
}
export default SideBar;