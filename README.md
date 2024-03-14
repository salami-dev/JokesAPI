
# JokeAPI Project Readme

## Overview

JokeAPI is a simple .NET 8.0 based CRUD Web API designed for managing and sharing jokes. Deployed as a containerized service in an Azure Linux App Service, it supports operations like fetching random jokes, managing categories, and voting on jokes.

## Features

- Retrieve a random joke or a random joke by category
- Fetch all categories or all jokes within a category
- Look up a joke by its ID
- Add new categories or jokes
- Associate existing jokes with additional categories
- Vote on jokes (like/dislike)

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
- [Terraform](https://www.terraform.io/downloads.html)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or compatible IDE

### Setup

1. **Clone the repository:**

   ```bash
   git clone https://github.com/your-repository/JokeAPI.git
   ```

2. **Navigate to the JokesAPI project directory:**

   ```bash
   cd src/JokesAPI
   ```

3. **Restore dependencies:**

   ```bash
   dotnet restore
   ```

4. **Configure the database connection:**

   Update the connection string in `appsettings.json` for PostgreSQL. Ensure the database exists in PostgreSQL with the corresponding name.

5. **Apply database migrations:**

   ```bash
   dotnet ef database update
   ```

6. **Build the Docker image:**

   ```bash
   docker build -t jokeapi .
   ```

7. **Run the application:**

   ```bash
   docker run -p 8080:8080 jokeapi
   ```

   The API will be accessible on `http://localhost:8080`.

### Deploying to Azure

1. **Navigate to the terraform directory:**

   ```bash
   cd infra/terraform
   ```

2. **Initialize Terraform:**

   ```bash
   terraform init
   ```

3. **Create an execution plan:**

   ```bash
   terraform plan
   ```

4. **Apply the plan to deploy to Azure:**

   ```bash
   terraform apply
   ```

For detailed instructions on SSH access to your custom container, refer to the [Azure documentation](https://learn.microsoft.com/en-us/azure/app-service/configure-custom-container?tabs=debian&pivots=container-linux#enable-ssh).


## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.