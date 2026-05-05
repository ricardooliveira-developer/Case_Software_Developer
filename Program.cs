var pagamentos = new List<EventoPagamento>
{
    new(Id: "id-1", Valor: 100m),
    new(Id: "id-2", Valor: 200m),
    new(Id: "id-1", Valor: 100m),
    new(Id: "id-3", Valor: 300m),
    new(Id: "id-4", Valor: 400m),
    new(Id: "id-2", Valor: 200m),
};

var pagamentosProcessados = new HashSet<string>();

const int maxTentativas = 3;

var total = ProcessarPagamentos(pagamentos);

Console.WriteLine($"Valor total processado: {total}");

decimal ProcessarPagamentos(List<EventoPagamento> pagamentos)
{
    decimal total = 0;

    foreach (var pagamento in pagamentos)
    {
        try
        {
            if (ValidaPagamentoProcessado(pagamento))
                continue;
            
            total += Pagar(pagamento);;
        }
        catch
        {
            total += RetryPagamento(pagamento);
        }
        finally
        {
            AddPagamentoProcessado(pagamento);
        }
    }

    return total;
}

void AddPagamentoProcessado(EventoPagamento pagamento) => pagamentosProcessados.Add(pagamento.Id);

bool ValidaPagamentoProcessado(EventoPagamento pagamento) => pagamentosProcessados.Any(p => p == pagamento.Id);

decimal RetryPagamento(EventoPagamento pagamento)
{
    int tentativas = 0;
    
    while (tentativas < maxTentativas)
    {
        try
        {
            return Pagar(pagamento);
        }
        catch
        {
            tentativas++;
        }
    }
    
    Console.WriteLine($"Falha ao realizar pagamento: {pagamento.Id}. Tentativas: {tentativas}");
    return 0;
}

decimal Pagar(EventoPagamento pagamento)
{
    // NÃO alterar este método
    // Ele simula um serviço externo que pode falhar

    if (Random.Shared.Next(0, 2) == 0)
        throw new Exception("Falha ao realizar pagamento.");

    // Simula o pagamento, retornando o valor do pagamento.
    // Imagine uma chamada a um serviço externo aqui, https://api-pagamento.com/pagar, por exemplo.

    return pagamento.Valor;
}

sealed record EventoPagamento(string Id, decimal Valor);