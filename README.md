# SnackHub


## Running the Application


This application requires Docker to run. You have two options: building the Docker image manually or using Docker Compose.


### Option 1: Building and Running Manually


To build the Docker image, navigate to the root folder of the solution in your terminal and execute the following commands:

```sh
docker build -t team-one-pos-tech/snack-hub -f ./deploy/Dockerfile .
```

Once built, you can run the container using:

```sh
docker run -p 5005:80 --name snack-hub team-one-pos-tech/snack-hub
```


### Option 2: Using Docker Compose

For a simpler setup, navigate to the `./deploy` folder in your terminal and execute:

```sh
docker-compose up -d
```
This command will automatically build and start the necessary services, including the Acquiring Bank Simulator and MongoDB, based on the configuration in the `docker-compose.yml` file.