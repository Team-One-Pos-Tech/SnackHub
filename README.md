# SnackHub

This is the source code for SnackHub, designed to be the first step of FIAP's SOAT Tech Challenges.

It intends to apply the concepts presented so far, such as Domain Driven Design, Containerization and Hexagonal Architecture.

The API handles a few operations on a small fast food restaurant, including:

## Stack

- C# 12
- .Net 8
- mongodb
- Docker

## Running the Application


This application requires Docker to run. So, to execute the application you could just run a Docker Compose.

### Using Docker Compose

For a simpler setup, navigate to the solutions root folder in your terminal and execute:

```sh
docker compose up -d
```
This command will automatically build and start the necessary services, including the Acquiring Bank Simulator and MongoDB, based on the configuration in the `docker-compose.yml` file.

## Technical details

**ports**:

- http://localhost:7080
- https://localhost:7443

## Dependencies