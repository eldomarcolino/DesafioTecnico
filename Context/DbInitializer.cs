using SistemaDeRecarga.Context;
using SistemaDeRecarga.Model;

namespace DesafioTecnico.Context
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.User.Any())
            {
                return;
            }

            // Criar usuários
            var users = new User[]
            {
                new User
                {
                    Username = "Admin",
                    Email = "admin@teste.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin",
                    RegistrationNumber = "ADM001",
                    Createdate = DateTime.Now
                },
                new User
                {
                    Username = "João Silva",
                    Email = "joao@teste.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("senha123"),
                    Role = "User",
                    RegistrationNumber = "USER001",
                    Createdate = DateTime.Now
                },
                new User
                {
                    Username = "Maria Souza",
                    Email = "maria@teste.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("senha123"),
                    Role = "User",
                    RegistrationNumber = "USER002",
                    Createdate = DateTime.Now
                }
            };

            context.User.AddRange(users);
            context.SaveChanges();

            // Criar saldos
            var balances = new Balance[]
            {
                new Balance { IdUser = 1, Amount = 1000.00m },
                new Balance { IdUser = 2, Amount = 500.00m },
                new Balance { IdUser = 3, Amount = 300.00m }
            };

            context.Balance.AddRange(balances);
            context.SaveChanges();

            // Criar transações
            var transactions = new Transacao[]
            {
                new Transacao
                {
                    IdUser = 2,
                    IdDestinatario = 3,
                    Valor = 100.00m,
                    Type = "Transferência",
                    Description = "Pagamento de serviços",
                    TransactionDate = DateTime.Now.AddDays(-3)
                },
                new Transacao
                {
                    IdUser = 3,
                    IdDestinatario = 2,
                    Valor = 50.00m,
                    Type = "Transferência",
                    Description = "Reembolso",
                    TransactionDate = DateTime.Now.AddDays(-1)
                }
            };

            context.Transaction.AddRange(transactions);
            context.SaveChanges();
        }
    }
}
