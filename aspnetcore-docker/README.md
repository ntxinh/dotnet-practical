# Docker

```bash
docker build -t aspnetcore-docker-image .
docker run -it --rm -p 3000:80 --name aspnetcore-docker-container aspnetcore-docker-image
docker run -d -p 3000:80 --name aspnetcore-docker-container aspnetcore-docker-image
```

- http://localhost:3000/WeatherForecast

# Kubernetes

```bash
kubectl apply -f deployment.yaml
kubectl apply -f service.yaml
```

- http://localhost:8080/WeatherForecast

```bash
# Clean
kubectl delete -f deployment.yaml
kubectl delete -f service.yaml
```

# Kubernetes Dashboard

```bash
kubectl apply -f service-account.yaml
kubectl apply -f cluster-role-binding.yaml

# Bash
kubectl -n kubernetes-dashboard describe secret $(kubectl -n kubernetes-dashboard get secret | grep admin-user | awk '{print $1}')

# Powershell
kubectl -n kubernetes-dashboard describe secret $(kubectl -n kubernetes-dashboard get secret | sls admin-user | ForEach-Object { $_ -Split '\s+' } | Select -First 1)

# Now copy the token and paste it into Enter token field on login screen.
# http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/
```