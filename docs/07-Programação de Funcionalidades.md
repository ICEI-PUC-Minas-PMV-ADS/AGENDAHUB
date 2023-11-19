# Programação de Funcionalidades

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>, <a href="4-Metodologia.md"> Metodologia</a>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>, <a href="5-Arquitetura da Solução.md"> Arquitetura da Solução</a>

Implementação do sistema descrita por meio dos requisitos funcionais e/ou não funcionais. Deve relacionar os requisitos atendidos com os artefatos criados (código fonte), deverão apresentadas as instruções para acesso e verificação da implementação que deve estar funcional no ambiente de hospedagem.

Por exemplo: a tabela a seguir deverá ser preenchida considerando os artefatos desenvolvidos.

|ID    | Descrição do Requisito  | Artefato(s) produzido(s) | Responsável |
|------|-----------------------------------------|----|----|
| RF-01 | O sistema deve permitir uma empresa/autônomo se cadastrar para receber acesso ao sistema |   Usuario.cs, Create.cshtml e AccountController.cs|  Álvaro  |
| RF-02 | O sistema deve ser protegido por autenticação de login e senha   |   Usuario.cs, Login.cshtml e AccountController.cs     |  Álvaro   |
| RF-03 | O sistema deve permitir ao administrador realizar o CRUD de um novo serviço oferecido pelo profissional | Servicos.cs, Servicos.cshtml e ServicosController.cs| Evellyn |
| RF-04 | O sistema deve permitir ao administrador realizar o CRUD de um novo cliente na base de dados   | Clientes.cs, Clientes.cshtmle  ClientesController.cs| Lucas |
| RF-05 | O sistema deve permitir ao administrador realizar o CRUD de um novo agendamento | Agendamentos.cs, Agendamentos.cshtml, AgendamentosController.cs| Lucas |
| RF-06 | O sistema deve permitir ao administrador realizar o CRUD de uma nova movimentação financeira   | Caixa.cs, Caixa.cshtml e CaixaController.cs | Lucas |
| RF-07 | O sistema deve permitir ao administrador realizar o CRUD de um novo colaborador | NovoUsuario.cshtml, Usuario.cs, AccountController.cs  |  Lucas     |
| RF-08 | O sistema deve permitir alterar informações sobre a empresa ex: endereço, conta de recebimento, dentre outros.   | Configuracao.cs, Edit.cshtml, ConfiguracaoController.cs |   Josué  |
| RF-09 | O sistema deve permitir alterar informações cadastrais ex: login, senha e e-mail. |  Usuario.cs, Edit.cshtml e ConfiguracaoController.cs      |   Josué  |
| RF-10 | O sistema deve oferecer a possibilidade de exigir ou não um pagamento para realizar agendamentos   |       |       |
| RF-11 | O sistema deve permitir o cliente realizar, visualizar, editar e cancelar um agendamento |        |       |
| RF-12 | O sistema deve permitir o cliente visualizar os serviços oferecidos pelo profissional |        |       |
| RF-13 | O sistema deve oferecer a possibilidade de o cliente efetuar um pagamento antecipado |        |       |

# Instruções de acesso

Link para acessar o Sistema: [AgendaHub](https://agendahub20231119201019.azurewebsites.net)
