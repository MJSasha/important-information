FROM openjdk:18
EXPOSE 8080
ADD target/backend.jar backend.jar
ADD imp-inf.db imp-inf.db
ENTRYPOINT ["java", "-jar", "/backend.jar"]