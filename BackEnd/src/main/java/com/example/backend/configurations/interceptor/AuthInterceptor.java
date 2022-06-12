package com.example.backend.configurations.interceptor;

import com.example.backend.services.AuthService;
import org.springframework.web.servlet.HandlerInterceptor;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Objects;

public class AuthInterceptor implements HandlerInterceptor {

    AuthService authService;

    public AuthInterceptor(AuthService authService) {
        this.authService = authService;
    }

    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {
        try {
            if (inListOfAvailable(request.getServletPath())) return true;
            var cookies = request.getCookies();
            var token = Arrays.stream(cookies).filter(c -> Objects.equals(c.getName(), "token")).findFirst().get();
            authService.authenticate(token.getValue());
            return true;
        } catch (Exception e) {
            response.sendError(401);
            return false;
        }
    }

    private boolean inListOfAvailable(String path) {
        var availablePaths = new ArrayList<String>();
        availablePaths.add("/api/start");
        availablePaths.add("/api/auth");
        availablePaths.add("/api/auth/byToken");

        return availablePaths.stream().anyMatch(p -> Objects.equals(p, path));
    }
}
