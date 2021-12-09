# Setting things up

``` batch
kubectl create ns argo
kubectl apply -n argo -f https://raw.githubusercontent.com/argoproj/argo/stable/manifests/quick-start-postgres.yaml 
```

expose port

`kubectl -n argo port-forward svc/argo-server 2746:2746`

expose argo with LoadBalancer

` kubectl patch svc argo-server -n argo -p '{"spec": {"type": "LoadBalancer"}}'`

