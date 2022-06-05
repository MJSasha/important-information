import axios from 'axios';


class TestService {

    getUsers(){
        return axios.get('http://localhost:8080/api/users');
    }
}

export default new TestService();