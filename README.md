### Projeto: API RESTful para um Sistema de Gerenciamento de Biblioteca

#### Requisitos Gerais
1. **API RESTful**: A API deve seguir os princípios REST.
2. **Autenticação e Autorização**: Implementar autenticação e autorização JWT.
3. **Validação**: Adicionar validação robusta dos dados de entrada.
4. **Testes Unitários**: Cobrir o código com testes unitários.

#### Funcionalidades
1. **Gerenciamento de Usuários**
   - Cadastro, edição, remoção e busca de usuários.
   - Perfis de usuário: administrador e usuário comum.

2. **Gerenciamento de Livros**
   - Cadastro, edição, remoção e busca de livros.
   - Categorias de livros para fácil classificação.

3. **Registro de Empréstimos**
   - Usuários podem emprestar livros.
   - Registro de data de empréstimo e data esperada de devolução.
   - Marcação de devolução do livro.

4. **Relatórios**
   - Gerar relatórios de livros mais emprestados.
   - Relatório de usuários com empréstimos em atraso.

#### Tecnologias
- **Framework**: .NET Core 6.
- **Banco de Dados**: Entity Framework Core com SQL Server ou outro banco de sua escolha.
- **Autenticação**: ASP.NET Core Identity com JWT.
- **Testes**: xUnit ou NUnit para testes unitários.

#### Critérios de Avaliação
1. **Qualidade do Código**: Clareza, manutenção e uso de padrões de projeto.
2. **Uso de Tecnologias**: Correto uso das tecnologias e frameworks.
3. **Implementação de Funcionalidades**: Todas as funcionalidades devem ser implementadas conforme especificado.
4. **Segurança**: Implementação de práticas de segurança, especialmente em autenticação e autorização.
5. **Testes**: Cobertura e qualidade dos testes unitários.

### Desafio Bônus
- **Dockerize** a aplicação para facilitar o deployment.
- **Documentação API**: Utilizar Swagger para documentar a API.
