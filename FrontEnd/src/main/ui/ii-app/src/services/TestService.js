import axios from 'axios';


class TestService {

    postUsers(){
        axios.post('http://localhost:8080/api/users', {
            name: 'Fred',
            login: 'Flintstone',
            password: 'asdasad',
            role: 'USER'
          })
          .then(function (response) {
            console.log(response);
          })
          .catch(function (error) {
            console.log(error);
          });
    }

    getUsers(){
        return axios.get('http://localhost:8080/api/users');
    }
}

export default new TestService();