## Contexto

Você está trabalhando em um sistema que recebe webhooks de pagamento de um provedor externo.

Cada webhook representa um evento de pagamento e contém um `Id` (único por pagamento) e um valor.

Esse tipo de integração possui dois problemas comuns:
- O provedor pode enviar o mesmo webhook mais de uma vez;
- O processamento pode falhar temporariamente.

## O que fazer

Refatore o método `ProcessarPagamentos` para:

- Ignorar eventos duplicados (mesmo `Id`), garantindo que cada pagamento seja processado apenas uma vez;
- Em caso de falha no processamento, tentar novamente até 3 vezes antes de desistir;
- Retornar o valor total dos pagamentos processados com sucesso;
- Reportar pagamentos que falharam após todas as tentativas.
  - A forma de reportar (log, retorno, exceção, etc.) é de sua escolha — explique sua decisão.

## Regras

- Não altere o método `Pagar` — ele simula um serviço externo que pode falhar;
- Você pode criar classes, métodos ou alterar o `record`, se julgar necessário — justifique;
- Considere apenas dados em memória (sem persistência).

## Diferencial (opcional)

- Processar múltiplos eventos simultaneamente;
- Caso opte por isso, considere os cuidados com thread safety.

## Resultado esperado

Existem 4 pagamentos únicos (`id-1` a `id-4`), totalizando 1000.

O valor retornado deve considerar apenas os pagamentos processados com sucesso.

Pagamentos que falharem após as 3 tentativas não devem ser incluídos no total.

> Observação: o resultado pode variar entre execuções devido às falhas simuladas.

## Entrega

- Código da solução em `.zip` ou link para repositório;
- Um `README` explicando:
  - O que estava errado no código original;
  - O que foi alterado e por quê;
  - O que faria a mais se tivesse mais tempo.

## O que avaliamos

- Correção da solução;
- Clareza e organização do código;
- Tratamento de erros;
- Qualidade das decisões e justificativas.

**Tempo estimado: 1 a 2 horas**