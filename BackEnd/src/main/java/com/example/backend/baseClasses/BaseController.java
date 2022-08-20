package com.example.backend.baseClasses;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;

public class BaseController<TEntity, TKey> {

    private final BaseCRUDService<TEntity, TKey> service;

    public BaseController(BaseCRUDService<TEntity, TKey> service) {
        this.service = service;
    }

    @GetMapping
    public ResponseEntity<List<TEntity>> readAll() {
        var result = service.read();
        if (result.isEmpty()) return ResponseEntity.noContent().build();
        result.forEach(this::RemoveUnnecessaryLinks);
        return ResponseEntity.ok(result);
    }

    @GetMapping("/{id}")
    public ResponseEntity<TEntity> readById(@PathVariable TKey id) {
        var result = service.read(id);
        if (result == null) return ResponseEntity.noContent().build();
        RemoveUnnecessaryLinks(result);
        return ResponseEntity.ok(result);
    }

    @PostMapping("/createAll")
    public ResponseEntity<String> create(@RequestBody ArrayList<TEntity> entities) {
        service.create(entities);
        return ResponseEntity.ok("Create successful");
    }

    @PostMapping
    public ResponseEntity<String> create(@RequestBody TEntity entity) {
        service.create(entity);
        return ResponseEntity.ok("Create successful");
    }

    @PatchMapping("/{id}")
    public ResponseEntity<String> update(@RequestBody TEntity entity, @PathVariable TKey id) {
        try {
            service.update(entity, id);
            return ResponseEntity.ok("Update successful");
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<String> delete(@PathVariable TKey id) {
        service.delete(id);
        return ResponseEntity.ok("Delete successful");
    }

    @DeleteMapping
    public ResponseEntity<String> delete(@RequestBody ArrayList<TKey> ids) {
        service.delete(ids);
        return ResponseEntity.ok("Delete successful");
    }

    protected void RemoveUnnecessaryLinks(TEntity entity) {
    }
}
