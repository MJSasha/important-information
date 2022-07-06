package com.example.backend.services;

import com.example.backend.baseClasses.BaseCRUDService;
import com.example.backend.data.models.User;
import com.example.backend.repositories.UsersRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class UsersService extends BaseCRUDService<User, Integer> {

    UsersRepository repository;

    public UsersService(CrudRepository<User, Integer> repository) {
        super(repository);

        this.repository = (UsersRepository) repository;
    }

    @Override
    public List<User> read() {
        var users = super.read();
        users.forEach(u->u.getPassword().setValue(null));
        return users;
    }

    @Override
    public User read(Integer integer) {
        var user = super.read(integer);
        user.getPassword().setValue(null);
        return user;
    }

    public User readByLogin(String login) {
        return repository.findByLogin(login);
    }

    public User readByToken(String token) {
        return repository.findByToken(token);
    }

    public User readByChatId(Long chatId) {
        return repository.findByChatId(chatId);
    }
}
