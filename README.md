<div align="center">
  <img src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white" alt=".NET" />
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/MVC-02569B?style=for-the-badge&logo=asp.net&logoColor=white" alt="MVC" />
  
  <h1>GameStore MVC</h1>
  <p>Uma aplicação web moderna para gerenciamento de jogos e usuários, construída com o poder do ASP.NET Core MVC.</p>
</div>

<br/>

## Sobre o Projeto

O **GameStore MVC** é um sistema projetado para gerenciar o catálogo de uma loja de jogos. Ele permite o cadastro de usuários, autenticação e controle de um acervo de jogos, utilizando o padrão arquitetural MVC (Model-View-Controller) para garantir um código limpo, organizado e de fácil manutenção.

## Funcionalidades

- **Gestão de Usuários:** Sistema de cadastro seguro e login (`CadastroViewModel`, `LoginViewModel`).
- **Catálogo de Jogos:** Visualização e gerenciamento de títulos da loja.
- **Arquitetura MVC:** Separação clara entre dados (Models), interface (Views) e rotas/regras de negócios (Controllers).
- **Repositórios:** Uso do padrão Repository (`GameRepositorio`, `UsuarioRepositorio`) para abstração de acesso aos dados.

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** ASP.NET Core MVC
- **Front-end:** HTML5, CSS3, Bootstrap e JavaScript (Views Razor)

## Estrutura do Projeto

```text
GameStoreMVC/
├── Controllers/       # Controladores da aplicação (ex: GameController)
├── Models/            # Modelos de dados e ViewModels
├── Views/             # Interface do usuário (Páginas Web Razor)
├── Repositorio/       # Regras de persistência e acesso a dados
├── Interfaces/        # Contratos para os repositórios
└── wwwroot/           # Arquivos estáticos (CSS, JS, Imagens)
```

##  Como Executar o Projeto

1. **Clone o repositório (se aplicável):**
   ```bash
   git clone <url-do-repositorio>
   ```

2. **Abra o projeto:**
   Abra o arquivo de solução (`GameStoreMVC.sln`) no **Visual Studio** ou abra a pasta do projeto no **Visual Studio Code**.

3. **Execute a aplicação:**
   * No **Visual Studio**: Pressione `F5` ou clique em "Iniciar".
   * No **Terminal/CLI**: Navegue até a pasta `GameStoreMVC` e execute os comandos:
     ```bash
     dotnet restore
     dotnet run
     ```

4. **Acesse no navegador:**
   O console exibirá as URLs (geralmente `http://localhost:5xxx` ou `https://localhost:7xxx`). Clique no link para abrir a aplicação.

---

<div align="center">
  <p>Desenvolvido com dedicação.</p>
</div>