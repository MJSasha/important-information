package com.example.backend.repositories;

import com.example.backend.models.User;
import org.springframework.data.repository.CrudRepository;

public interface UsersRepository extends CrudRepository<User, Integer> {
}
