package com.example.backend.repositories;

import com.example.backend.data.models.User;
import org.springframework.data.repository.CrudRepository;

public interface UsersRepository extends CrudRepository<User, Integer> {
    User findByLogin(String login);
    User findByToken(String token);
    User findByChatId(Long chatId);
}
