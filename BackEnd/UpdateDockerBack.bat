mvn clean package
docker build -t backend_important-information .
docker tag backend_important-information mjsasha/myrepo:backend_important-information
docker push mjsasha/myrepo:backend_important-information