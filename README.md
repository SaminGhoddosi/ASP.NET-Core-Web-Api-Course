# NZ Walks API

[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![Entity Framework](https://img.shields.io/badge/Entity_Framework_Core-8.0-green.svg)](https://docs.microsoft.com/ef/core/)

Um projeto Web API profissional construído com ASP.NET Core 8, implementando as melhores práticas do mercado para desenvolvimento de APIs RESTful.

## Sobre o Projeto

Este projeto foi desenvolvido como parte do curso **"Build ASP.NET Core Web API - Scratch To Finish (.NET8 API)"** da Udemy. A API gerencia dados de regiões e caminhadas (walks) com autenticação JWT e autorização baseada em roles.

## Tecnologias e Conceitos Implementados

### Core Framework
- **ASP.NET Core 8** - Framework principal
- **Entity Framework Core 8** - ORM para acesso a dados
- **SQL Server** - Banco de dados relacional

### Segurança e Autenticação
- **ASP.NET Core Identity** - Sistema de gerenciamento de usuários
- **JWT (JSON Web Tokens)** - Autenticação stateless
- **Role-based Authorization** - Controle de acesso por roles
- **Identity Core** - Gerenciamento de usuários e roles

### Mapeamento e Validação
- **AutoMapper** - Mapeamento entre DTOs e Domain Models
- **Custom Action Filters** - Validação personalizada de modelos
- **Data Annotations** - Validação de dados

### Documentação e Logging
- **Swagger/OpenAPI** - Documentação interativa da API
- **Serilog** - Sistema de logging estruturado
- **Custom Middleware** - Tratamento global de exceções

### Outras Funcionalidades
- **API Versioning** - Controle de versões da API
- **CORS** - Cross-Origin Resource Sharing
- **Dependency Injection** - Injeção de dependência nativa

## Funcionalidades

### Autenticação e Autorização
- **Registro de usuários** com roles personalizadas
- **Login com JWT** - Geração de tokens de acesso
- **Autorização por roles** (Reader, Writer)
- **Password policies** configuráveis

### Gerenciamento de Regiões
- **CRUD completo** de regiões
- **Validação de modelos** com custom action filters
- **Autorização baseada em roles** para cada operação

### Gerenciamento de Caminhadas (Walks)
- **Operações CRUD** para caminhadas
- **Relacionamentos** com dificuldades e regiões
- **Repository pattern** para abstração do data access

## Arquitetura e Padrões

### Padrões de Design
- **Repository Pattern** - Abstração da camada de dados
- **Dependency Injection** - Inversão de controle
- **DTO Pattern** - Separação entre modelos de domínio e transferência
- **Clean Architecture** - Separação de responsabilidades

### Estrutura do Projeto
```
NZWalks/
├──  Controllers/                 # Controladores da API
│   ├── AuthController.cs          # Autenticação e usuários
├──  CustomActionFilters/        # Filtros personalizados de validação
├──  Data/                       # Contextos do Entity Framework
│   ├── NZWalksAuthDBContext.cs    # Contexto de autenticação
│   └── NZWalksDbContext.cs        # Contexto principal
├──  Logs/                       # Arquivos de log do Serilog
├──  Mappings/                   # Perfis do AutoMapper
│   └── AutoMapperProfiles.cs
├──  Middlewares/                # Middlewares customizados
│   └── ExceptionHandlerMiddleware.cs
├──  Migrations/                 # Migrations do Entity Framework
├──  Models/
│   ├──  Domain/                 # Modelos de domínio
│   └──  DTO/                    # Data Transfer Objects
├──  Repositories/
│   ├──  Contracts/              # Interfaces dos repositórios
├── appsettings.json               # Configurações da aplicação
├── Program.cs                     # Ponto de entrada da aplicação
└── WebApplication1.http           # Arquivo para testar requisições HTTP
```

## Configuração e Instalação

### Pré-requisitos
- .NET 8 SDK
- SQL Server
- Visual Studio 2022 ou VS Code

### Configuração
1. **Clone o repositório**
   ```bash
   git clone [url-do-repositorio]
   cd NZWalks
   ```

2. **Configure as connection strings** no `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "NZWalksConnectionString": "Server=.;Database=NZWalksDb;Trusted_Connection=true;TrustServerCertificate=true;",
       "NZWalksAuthConnectionString": "Server=.;Database=NZWalksAuthDb;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```

3. **Configure o JWT** no `appsettings.json`:
   ```json
   {
     "Jwt": {
       "Key": "your-super-secret-key-here",
       "Issuer": "NZWalks",
       "Audience": "NZWalks"
     }
   }
   ```

4. **Execute as migrations**:
   ```bash
   dotnet ef database update
   ```

5. **Execute a aplicação**:
   ```bash
   dotnet run
   ```

## Uso da API

### Autenticação

1. **Registrar usuário**:
   ```http
   POST /api/v1/Auth/Register
   Content-Type: application/json

   {
     "email": "usuario@exemplo.com",
     "password": "senha123",
     "roles": ["Reader", "Writer"]
   }
   ```

2. **Login**:
   ```http
   POST /api/v1/Auth/Login
   Content-Type: application/json

   {
     "email": "usuario@exemplo.com",
     "password": "senha123"
   }
   ```

### Exemplos de Requisições

**Listar regiões (requer autenticação)**:
```http
GET /api/v1/Regions
Authorization: Bearer {seu-jwt-token}
```

**Criar região (requer role Writer)**:
```http
POST /api/v1/Regions
Authorization: Bearer {seu-jwt-token}
Content-Type: application/json

{
  "code": "AKL",
  "name": "Auckland",
  "regionImageUrl": "https://images.akl.jpg"
}
```

## Segurança

- **JWT Authentication** com chaves simétricas
- **Role-based Authorization** para endpoints sensíveis
- **Password hashing** com ASP.NET Core Identity
- **CORS** configurado para frontend
- **Input validation** com action filters customizados

## Logging

A aplicação utiliza **Serilog** para logging estruturado:
- Logs no console
- Logs em arquivo (`Logs/NzWaks_Log.txt`)
- Diferentes níveis de log (Information, Warning, Error)

## Testing

A API inclui:
- **Custom Exception Handling** com middleware
- **Model Validation** com action filters
- **Status codes** apropriados para diferentes cenários

## Aprendizados

### Conceitos Dominados
- **ASP.NET Core 8** e suas novidades
- **Entity Framework Core** com SQL Server
- **Autenticação JWT** e ASP.NET Core Identity
- **Repository Pattern** e Dependency Injection
- **AutoMapper** para mapeamento de objetos
- **Custom Middlewares** e Action Filters
- **API Versioning** e documentação com Swagger
- **Logging estruturado** com Serilog
- **Security best practices**

### Habilidades Desenvolvidas
- Criação de APIs RESTful profissionais
- Implementação de sistemas de autenticação seguros
- Uso de padrões de arquitetura e design
- Configuração de pipelines complexos no ASP.NET Core
- Gerenciamento de dependências e injeção de dependência

---

*Projeto educativo do curso "Build ASP.NET Core Web API - Scratch To Finish"*
