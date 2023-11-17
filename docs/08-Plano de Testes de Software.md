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
> - Acessar o site:
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
> - Acessar o site:
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
