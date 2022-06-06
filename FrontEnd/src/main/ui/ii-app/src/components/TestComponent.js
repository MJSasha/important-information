import React from 'react';
import TestService from '../services/TestService';

class TestComponent extends React.Component {

    constructor(props){
        super(props)
        this.state = {
            users:[]
        }
    }

    componentDidMount(){
        TestService.getUsers().then((response) => {
            this.setState({ users: response.data})
        });
    }

    render (){
        return (
            <div>
                <h1> Users List</h1>
                <table>
                    <thead>
                        <tr>
                            <td> User Name</td>
                            <td> User Login</td>
                            <td> User Password</td>
                            <td> User Role</td>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.users.map(
                                user => 
                                <tr key = {user.name}>  
                                     <td> {user.name}</td>
                                     <td> {user.login}</td>   
                                     <td> {user.password}</td>   
                                     <td> {user.role}</td>   
                                </tr>
                            )
                        }

                    </tbody>
                </table>
            </div>
        )
    }
}

export default TestComponent