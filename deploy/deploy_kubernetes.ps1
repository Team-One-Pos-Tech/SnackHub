Write-Host "Apply Snack Hub Database StatefulSet"
kubectl apply -f db-stateful.yaml

Write-Host "Apply metrics-server"
kubectl apply -f metrics-server.yaml

Write-Host "Apply Snack Hub App Deployment"
kubectl apply -f app-deployment.yaml
