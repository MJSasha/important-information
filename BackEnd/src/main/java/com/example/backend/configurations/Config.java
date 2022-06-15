package com.example.backend.configurations;

import com.example.backend.configurations.interceptor.AuthInterceptor;
import com.example.backend.services.AuthService;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.InterceptorRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurationSupport;

@Configuration
public class Config extends WebMvcConfigurationSupport {

    AuthService authService;

    public Config(AuthService authService, ApiConfig apiConfig) {
        this.authService = authService;
    }

    @Override
    public void addInterceptors(InterceptorRegistry registry) {
        registry.addInterceptor(new AuthInterceptor(authService));
    }
}