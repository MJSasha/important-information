package com.example.backend.repositories;

import com.example.backend.data.models.Password;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface PasswordsRepository extends CrudRepository<Password, Integer> {
}
