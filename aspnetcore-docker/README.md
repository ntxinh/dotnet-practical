# Docker

```
docker build -t aspnetcore-docker-image .
docker run -it --rm -p 3000:80 --name aspnetcore-docker-container aspnetcore-docker-image
docker run -d -p 3000:80 --name aspnetcore-docker-container aspnetcore-docker-image
```

- http://localhost:3000/WeatherForecast

# Kubernetes

```
kubectl apply -f deployment.yaml
kubectl apply -f service.yaml
```

- http://localhost:8080/WeatherForecast