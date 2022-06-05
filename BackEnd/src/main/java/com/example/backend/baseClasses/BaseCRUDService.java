package com.example.backend.baseClasses;

import org.springframework.data.repository.CrudRepository;

import java.util.ArrayList;
import java.util.List;

public class BaseCRUDService<TEntity, TKey> {

    private final CrudRepository<TEntity, TKey> repository;

    public BaseCRUDService(CrudRepository<TEntity, TKey> repository) {
        this.repository = repository;
    }

    public void create(ArrayList<TEntity> entities){
        repository.saveAll(entities);
    }

    public void create(TEntity entity){
        repository.save(entity);
    }

    public List<TEntity> read(){
        return (List<TEntity>) repository.findAll();
    }

    public TEntity read(TKey key){
        return repository.findById(key).get();
    }

    public void update(TEntity entity, TKey key) throws Exception {
        TEntity dbEntity = read(key);
        if (dbEntity == null) throw new Exception("Entity not exist");
        repository.save(entity);
    }

    public void delete(TKey key){
        repository.deleteById(key);
    }

    public void delete(ArrayList<TKey> keys){
        repository.deleteAllById(keys);
    }
}
