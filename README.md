API de Carteira Digital - Sistema de Recarga
📌 Visão Geral
API RESTful para gerenciamento de carteiras digitais e transações financeiras, desenvolvida em C# com ASP.NET Core seguindo arquitetura MVC. Atende todos os requisitos do desafio incluindo autenticação JWT, transferências entre usuários e consultas com filtros.

🛠️ Tecnologias
Backend: C# / ASP.NET Core 8

Banco de Dados: MySQL (com Entity Framework Core)

Autenticação: JWT (JSON Web Tokens)

Arquitetura: MVC (Model-View-Controller) com separação clara de camadas

🔑 Endpoints Principais
Autenticação
Método	|  Endpoint	 | Descrição
POST	| /api/auth/login	| Login (retorna token JWT)

Usuários
Método |	Endpoint |	Descrição
GET	 |  /api/user |	Lista usuários (com filtros)
POST |	/api/user/CreateUser |	Cria novo usuário
PUT |	/api/user/{id} |	Atualiza usuário
DELETE |	/api/user/DeleteUser |	Remove usuário

Saldo/Carteira
Método |	Endpoint |	Descrição
GET |	/api/balance/usuario/{idUser} |	Consulta saldo
POST |	/api/balance/recarregar |	Adiciona saldo
POST |	/api/balance/debitar |	Debita saldo
POST |	/api/balance/transferir |	Transfere entre usuários

Transações
Método |	Endpoint |	Descrição
GET |	/api/transacao/transacoes |	Todas transações (apenas Admin)
GET |	/api/transacao/transacoes/usuario/{id} |	Transações por usuário
GET |	/api/transacao/usuario/{id} |	Transações com filtro por data

🚀 Como Executar
Pré-requisitos
.NET 8 SDK

MySQL Server

IDE (Visual Studio/VSCode)

Passo a Passo
Clone o repositório:


git clone [URL_DO_REPOSITORIO]
Configure a conexão com o MySQL:

// appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SistemaRecarga;Uid=root;Pwd=sua_senha;"
}
Execute as migrações:
dotnet ef database update

Inicie a aplicação:
dotnet run


🌟 Dados Iniciais
População Inicial do Banco

O banco de dados é automaticamente populado com dados de exemplo na primeira execução, incluindo:

- 1 usuário admin (email: admin@teste.com, senha: admin123)
- 2 usuários comuns
- Saldos iniciais
- Transações de exemplo

Transações de exemplo

✅ Justificativas Técnicas
Banco de Dados (MySQL)
Performance: Otimizado para operações de CRUD intensivas

Compatibilidade: Integração perfeita com Entity Framework Core

Recursos: Suporte completo a transações ACID

Ecosistema: Ferramentas robustas como MySQL Workbench

Arquitetura MVC
Separação clara de responsabilidades

Manutenibilidade facilitada por camadas isoladas

Testabilidade de cada componente

📊 Exemplo de Requisições
Login

curl -X POST 'http://localhost:5000/api/auth/login' \
-H 'Content-Type: application/json' \
-d '{
  "email": "admin@teste.com",
  "password": "admin123"
}'

Transferência entre usuários

curl -X POST 'http://localhost:5000/api/balance/transferir' \
-H 'Authorization: Bearer [TOKEN_JWT]' \
-H 'Content-Type: application/json' \
-d '{
  "IdRemetente": 1,
  "IdDestinatario": 2,
  "Valor": 50.00,
  "Description": "Pagamento de serviços"
}'
