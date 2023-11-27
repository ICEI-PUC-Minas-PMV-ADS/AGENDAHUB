## Plano de Testes de Software  AgendaHub

<span style="color:red">Pré-requisitos: <a href="02-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="04-Projeto de Interface.md"> Projeto de Interface</a>

Os requisitos para a realização dos testes de software são:
 > - Aplicação hospedada em um servidor que permita realizar testagens.
 > - Navegadores – Google Chrome, Firefox, Microsoft Edge.
 > - Responsividade em diferentes dispositivos.

Os casos de testes funcionais a serem realizados na aplicação estão descritos abaixo:

## CT - 01 - Apresentação Página Home.
Requisitos Associados:
> - RNF – 01 - A aplicação deve ter uma página inicial para apresentar o sistema.
> - RNF – 04 - O sistema deve ser responsivo, com tempos de carregamento curtos para evitar frustração do usuário.
> - RNF – 07 - A interface do usuário deve ser intuitiva e de fácil utilização, mesmo para usuários não familiarizados com sistemas similares.
> - RNF - 08 - O sistema deve ser compatível com uma variedade de dispositivos e navegadores populares como Chrome e Edge.

Objetivo do Teste:
> - Verificar se ao acessar o site, a página home está apresentando informações necessárias para que o usuário crie sua conta, faça login e descubra mais sobre o objetivo da aplicação.

Passos:
> - Acessar o site:[AgendaHub](https://agendahub20231119201019.azurewebsites.net)
> - Verificar se a página home é carregada corretamente.
> - Confirmar a responsividade da página em dispositivos móveis e computadores.
> - Validar se a página é exibida corretamente nos navegadores Chrome e Edge.
> - Localizar as seções de criação de conta e login na página.
> - Avaliar a presença de informações relevantes sobre o propósito da aplicação.

Critérios de Êxito:
> - A página deve abrir em todos os navegadores dos listados no requisito.
> - A home deve ser responsiva tanto em dispositivos móveis quanto em computadores.
> - O usuário conseguir obter informações da aplicação.
> - O usuário conseguir encontrar o local para efetuar login e criar conta.

## CT - 02 - Cadastro e Login.
Requisitos Associados:
> - RF – 01 - O sistema deve permitir uma empresa/autônomo se cadastrar para receber acesso ao sistema.
> - RF - 02 - O sistema deve ser protegido por autenticação de login e senha.
> - RF - 09 - O sistema deve permitir alterar informações cadastrais ex: login, senha e e-mail.


Objetivo do Teste:
> - Verificar se é possível efetuar um novo cadastro e, após efetuar o login utilizando as mesmas credenciais, obter êxito. Além disso, o sistema deve permitir a alteração das informações cadastrais iniciais.

Passos:
> - Acessar o site:[AgendaHub](https://agendahub20231119201019.azurewebsites.net)
> - Realizar um novo cadastro inserindo informações válidas.
> - Efetuar o login utilizando as credenciais cadastradas.
> - Verificar se o login é bem-sucedido.
> - Acessar a seção de alteração de informações cadastrais.
> - Modificar o login, senha e e-mail cadastrados.
> - Confirmar se as alterações foram salvas com sucesso.

Critérios de Êxito:
> - O sistema deve permitir criar um novo cadastro com login e senha.
> - O sistema deve permitir efetuar login utilizando as credenciais do cadastro inicial.
> - Ao efetuar o login, o sistema deve permitir que sejam alterados os dados cadastrais de login, senha e e-mail.

## CT - 03 - CRUD de Serviço.
Requisitos Associados:
> - RF – 03 - O sistema deve permitir ao administrador realizar o CRUD de um novo serviço oferecido pelo profissional

Objetivo do Teste:
> - O objetivo deste teste é garantir que o administrador consiga realizar as operações CRUD (Create, Read, Update, Delete) de um novo serviço oferecido pelo profissional no sistema

Passos:
> - Acessar o site:[AgendaHub](https://agendahub20231119201019.azurewebsites.net)
> - Efetuar o login utilizando as credenciais cadastradas.
> - Acessar a seção de novo serviço.

Cadastro de Novo Serviço:
> - Selecionar a opção para cadastrar um novo serviço.
> - Preencher todos os campos obrigatórios.
> - Submeter o formulário.

Consulta do Serviço Cadastrado:
> - Vá para a lista de serviços cadastrados.
> - Localizar o serviço adicionado no passo anterior.
> - Verificar se as informações do serviço estão corretas e correspondem aos dados fornecidos no cadastro.

Atualização do Serviço:
> - Editar os detalhes do serviço cadastrado.
> - Confirmar se as informações foram atualizadas corretamente.

Exclusão do Serviço:
> - Remover o serviço cadastrado.
> - Verificar se o serviço não está mais presente na lista de serviços cadastrados.


Critérios de Êxito:
> - O cadastro de um novo serviço é bem-sucedido, sem erros ou mensagens de falha.
> - A consulta do serviço recém-cadastrado exibe todas as informações corretamente.
> - A atualização do serviço reflete as modificações feitas.
> - A exclusão do serviço remove efetivamente o serviço da lista.


## CT - 04 - CRUD de Cliente.
Requisitos Associados:
> - RF – 04 - O sistema deve permitir ao administrador realizar o CRUD de um novo cliente na base de dados

Objetivo do Teste:
> - O objetivo deste teste é garantir que o administrador consiga realizar as operações CRUD (Create, Read, Update, Delete) de um novo cliente no sistema.

Passos:
> - Acessar o site:[AgendaHub](https://agendahub20231119201019.azurewebsites.net)
> - Efetuar o login utilizando as credenciais cadastradas.
> - Acessar a seção de novo cliente.

Cadastro de Novo Cliente:
> - Selecionar a opção para cadastrar um novo cliente.
> - Preencher todos os campos obrigatórios.
> - Submeter o formulário.

Consulta do Cliente Cadastrado:
> - Vá para a lista de clientes cadastrados.
> - Localizar o cliente adicionado no passo anterior.
> - Verificar se as informações do cliente estão corretas e correspondem aos dados fornecidos no cadastro.

Atualização do Cliente:
> - Editar os detalhes do cliente cadastrado.
> - Confirmar se as informações foram atualizadas corretamente.

Exclusão do Cliente:
> - Remover o cliente cadastrado.
> - Verificar se o cliente não está mais presente na lista de clientes cadastrados.

Critérios de Êxito:
> - O cadastro de um novo cliente é bem-sucedido, sem erros ou mensagens de falha.
> - A consulta do cliente recém-cadastrado exibe todas as informações corretamente.
> - A atualização do cliente reflete as modificações feitas.
> - A exclusão do cliente remove efetivamente o cliente da lista.

## CT - 05 - CRUD de Agendamento.
Requisitos Associados:
> - RF – 05 - O sistema deve permitir ao administrador realizar o CRUD de um novo agendamento

Objetivo do Teste:
> - O objetivo deste teste é garantir que o administrador consiga realizar as operações CRUD (Create, Read, Update, Delete) de um novo agendamento no sistema.

Passos:
> - Acessar o site:[AgendaHub](https://agendahub20231119201019.azurewebsites.net)
> - Efetuar o login utilizando as credenciais cadastradas.
> - Acessar a seção de agendamento.

Cadastro de Novo Agendamento:
> - Selecionar a opção para cadastrar um novo agendamento.
> - Preencher todos os campos obrigatórios do formulário de cadastro de agendamento.
> - Submeter o formulário.

Consulta do Agendamento Cadastrado:
> - Vá para a lista de agendamentos cadastrados.
> - Localizar o agendamento adicionado no passo anterior.
> - Verificar se as informações do agendamento estão corretas e correspondem aos dados fornecidos no cadastro.

Atualização do Agendamento:
> - Editar os detalhes do agendamento cadastrado.
> - Confirmar se as informações foram atualizadas corretamente.

Exclusão do Agendamento:
> - Remover o agendamento cadastrado.
> - Verificar se o agendamento não está mais presente na lista de agendamentos cadastrados.

Critérios de Êxito:
> - O cadastro de um novo agendamento é bem-sucedido, sem erros ou mensagens de falha.
> - A consulta do agendamento recém-cadastrado exibe todas as informações corretamente.
> - A atualização do agendamento reflete as modificações feitas.
> - A exclusão do agendamento remove efetivamente o cliente da lista.

## CT - 06 - CRUD de Movimentação Financeira.
Requisitos Associados:
> - RF – 06 - O sistema deve permitir ao administrador realizar o CRUD de uma nova movimentação financeira

Objetivo do Teste:
> - O objetivo deste teste é garantir que o administrador consiga realizar as operações CRUD (Create, Read, Update, Delete) de uma nova movimentação financeira no sistema.

Passos:
> - Acessar o site:[AgendaHub](https://agendahub20231119201019.azurewebsites.net)
> - Efetuar o login utilizando as credenciais cadastradas.
> - Acessar a seção de movimentação financeira.

Cadastro de Nova Movimentação Financeira:
> - Selecionar a opção para cadastrar uma nova movimentação.
> - Preencher todos os campos obrigatórios do formulário de cadastro de movimentação financeira.
> - Submeter o formulário.

Consulta da Nova Movimentação Financeira:
> - Vá para a lista de movimentações cadastradas.
> - Localizar a movimentação adicionada no passo anterior.
> - Verificar se as informações da movimentação estão corretas e correspondem aos dados fornecidos no cadastro.

Atualização da Nova Movimentação Financeira:
> - Editar os detalhes da movimentação cadastrada.
> - Confirmar se as informações foram atualizadas corretamente.

Exclusão da Nova Movimentação Financeira:
> - Remover a movimentação cadastrada.
> - Verificar se a movimentação financeira não está mais presente na lista de movimentações cadastradas.

Critérios de Êxito:
> - O cadastro de uma nova movimentação financeira é bem-sucedido, sem erros ou mensagens de falha.
> - A consulta da movimentação financeira recém-cadastrada exibe todas as informações corretamente.
> - A atualização da movimentação financeira reflete as modificações feitas.
> - A exclusão da movimentação financeira remove efetivamente a movimentação financeira da lista.

## CT - 07 - CRUD de Colaborador.
Requisitos Associados:
> - RF – 07 - O sistema deve permitir ao administrador realizar o CRUD de um novo colaborador

Objetivo do Teste:
> - O objetivo deste teste é garantir que o administrador consiga realizar as operações CRUD (Create, Read, Update, Delete) de um novo colaborador no sistema.

Passos:
> - Acessar o site:[AgendaHub](https://agendahub20231119201019.azurewebsites.net)
> - Efetuar o login utilizando as credenciais cadastradas.
> - Acessar a seção de administração.

Cadastro de Novo Colaborador:
> - Selecionar a opção para cadastrar um novo colaborador.
> - Preencher todos os campos obrigatórios do formulário de cadastro de colaborador.
> - Submeter o formulário.

Consulta do Novo Colaborador:
> - Vá para a lista de colaboradores cadastrados.
> - Localizar o colaborador adicionado no passo anterior.
> - Verificar se as informações do colaborador estão corretas e correspondem aos dados fornecidos no cadastro.

Atualização do Colaborador:
> - Editar os detalhes do colaborador cadastrado.
> - Confirmar se as informações foram atualizadas corretamente.

Exclusão do Colaborador:
> - Remover o colaborador cadastrado.
> - Verificar se o colaborador não está mais presente na lista de colaboradores cadastrados.

Critérios de Êxito:
> - O cadastro de um novo colaborador é bem-sucedido, sem erros ou mensagens de falha.
> - A consulta do colaborador recém-cadastrado exibe todas as informações corretamente.
> - A atualização do colaborador reflete as modificações feitas.
> - A exclusão do colaborador remove efetivamente o colaborador da lista.

## CT - 08 - CRUD de Profissional.
Requisitos Associados:
> - RF – 14 - O sistema deve permitir ao administrador realizar o CRUD de um novo profissional

Objetivo do Teste:
> - O objetivo deste teste é garantir que o administrador consiga realizar as operações CRUD (Create, Read, Update, Delete) de um novo profissional no sistema.

Passos:
> - Acessar o site:[AgendaHub](https://agendahub20231119201019.azurewebsites.net)
> - Efetuar o login utilizando as credenciais cadastradas.
> - Acessar a seção de administração.

Cadastro de Novo Profissional:
> - Selecionar a opção para cadastrar um novo profissional.
> - Preencher todos os campos obrigatórios do formulário de cadastro de profissional.
> - Submeter o formulário.

Consulta do Profissional Cadastrado:
> - Vá para a lista de profissionais cadastrados.
> - Localizar o profissional adicionado no passo anterior.
> - Verificar se as informações do profissional estão corretas e correspondem aos dados fornecidos no cadastro.

Atualização do Profissional:
> - Editar os detalhes do profissional cadastrado.
> - Confirmar se as informações foram atualizadas corretamente.

Exclusão do Profissional:
> - Remover o profissional cadastrado.
> - Verificar se o profissional não está mais presente na lista de profissionais cadastrados.

Critérios de Êxito:
> - O cadastro de um novo profissional é bem-sucedido, sem erros ou mensagens de falha.
> - A consulta do profissional recém-cadastrado exibe todas as informações corretamente.
> - A atualização do profissional reflete as modificações feitas.
> - A exclusão do profissional remove efetivamente o profissional da lista.

## CT - 09 - Agendamento Cliente.
Requisitos Associados:
> - RF – 11 - O sistema deve permitir o cliente realizar, visualizar, editar e cancelar um agendamento	

Objetivo do Teste:
> - O objetivo deste teste é garantir que o cliente consiga realizar, visualizar, eitar e cancelar um agendamento efetuado por ele mesmo.

Passos:
> - Acessar o site:[AgendaHub](https://agendahub20231119201019.azurewebsites.net)


Critérios de Êxito:
> - O cadastro de um novo agendamento é bem-sucedido, sem erros ou mensagens de falha.
> - A visualização do agendamento recém-cadastrado exibte todas as informações corretamente.
> - A atualização do agendamento refelete as modificações feitas.
> - O cancelamento é bem-sucedido, sem erros ou mensagens de falha. 

