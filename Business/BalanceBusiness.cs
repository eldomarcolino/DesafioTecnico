using SistemaDeRecarga.Model;
using System.Runtime.CompilerServices;

namespace SistemaDeRecarga.Business
{
    public class BalanceBusiness : IBalanceBusiness
    {
        private readonly IBalanceRepository _balanceRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IUserRepository _userRepository;

        public BalanceBusiness(
            IBalanceRepository balanceRepository,
            IUserRepository userRepository,
            ITransacaoRepository transacaoRepository)
        {
            _balanceRepository = balanceRepository;
            _userRepository = userRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<Balance> GetBalanceByIdUserAsync(int idUser)
        {

            //Verificar se usuário existe
            var user = await _userRepository.GetUserByIdAsync(idUser);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            var balance = await _balanceRepository.GetBalanceByIdUserAsync(idUser);
            if(balance == null)
            {
                //Criar um saldo zerado se nao existir
                balance = new Balance
                {
                    IdUser = idUser,
                    Amount = 0,
                    LastUpdate = DateTime.Now
                };
                await _balanceRepository.CreateBalanceAsync(balance);
            }

            return balance;
        }

        public async Task<Balance> AddBalanceAsync(int idUser, decimal valor, string description)
        {
            if(valor <= 0)
            {
                throw new Exception("O vlor da recarga deve ser maior que zero");
            }

            //Obter ou criar saldo
            var balance = await GetBalanceByIdUserAsync(idUser);

            //Atualiza saldo
            balance.Amount += valor;
            balance.LastUpdate = DateTime.Now;
            await _balanceRepository.UpdateBalanceAsync(balance);

            //Registrar transaçao
            var transacao = new Transacao
            {
                IdUser = idUser,
                Valor = valor,
                Type = "Recarga",
                Description = description,
                TransactionDate = DateTime.Now
            };

            // Gerar novo ID se necessário
            if (transacao.Id == 0)
            {
                int lastId = await _transacaoRepository.GetLastIdAsync();
                transacao.Id = lastId + 1;
            }

            await _transacaoRepository.CreateTransacaoAsync(transacao);

            return balance;
        }

        public async Task<Balance> DeductBalanceAsync(int idUser, decimal valor, string description)
        {
            if(valor <= 0)
            {
                throw new Exception("O valor do débito deve ser maior que zero.");
            }

            //obter saldo
            var saldo = await GetBalanceByIdUserAsync(idUser);

            //verifica se há saldo suficiente
            if (saldo.Amount < valor)
            {
                throw new Exception("Saldo insuficiente.");
            }

            //atualizar saldo
            saldo.Amount -= valor;
            saldo.LastUpdate = DateTime.Now;
            await _balanceRepository.UpdateBalanceAsync(saldo);

            //Registrar transaçao
            var transacao = new Transacao
            {
                IdUser = idUser,
                Valor = valor,
                Type = "Recarga",
                Description = description,
                TransactionDate = DateTime.Now
            };

            // Gerar novo ID se necessário
            if (transacao.Id == 0)
            {
                int lastId = await _transacaoRepository.GetLastIdAsync();
                transacao.Id = lastId + 1;
            }

            await _transacaoRepository.CreateTransacaoAsync(transacao);

            return saldo;
        }

        public async Task<Transacao> TransferirSaldoAsync(int idRemetente, int idDestinatario, decimal valor, string description)
        {
            if (valor <= 0)
            {
                throw new Exception("Saldo insuficiente");
            }

            var remetente = await _userRepository.GetUserByIdAsync(idRemetente);
            var destinatario = await _userRepository.GetUserByIdAsync(idDestinatario);

            if (remetente == null || destinatario == null)
            {
                throw new Exception("Usuário remetene ou destinatário inválido");
            }

            //Débito do remetente
            var saldoRemetente = await GetBalanceByIdUserAsync(idRemetente);
            if (saldoRemetente.Amount < valor)
            {
                throw new Exception("Saldo insuficiente");
            }

            saldoRemetente.Amount -= valor;
            await _balanceRepository.UpdateBalanceAsync(saldoRemetente);

            //Crédito no destinatário
            var saldoDestinatario = await GetBalanceByIdUserAsync(idDestinatario);
            saldoDestinatario.Amount += valor;
            await _balanceRepository.UpdateBalanceAsync(saldoDestinatario);

            //Registrar transação
            var transacao = new Transacao
            {
                IdUser = idRemetente,
                IdDestinatario = idDestinatario,
                Valor = valor,
                Type = "Tranferência",
                Description = description,
                TransactionDate = DateTime.Now
            };

            if (transacao.Id == 0)
            {
                transacao.Id = await _transacaoRepository.GetLastIdAsync() + 1;
            }

            await _transacaoRepository.CreateTransacaoAsync(transacao);

            return transacao;
        }
    }


}
