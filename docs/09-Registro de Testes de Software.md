# Registro de Testes de Software

<span style="color:red">Pré-requisitos: <a href="02-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="04-Projeto de Interface.md"> Projeto de Interface</a>,  <a href="08-Plano de Testes de Software.md"> Projeto de de Testes de Software</a>

Os registros dos testes realizados na aplicação estão descritos abaixo.

## Casos de Sucesso

## CT - 01 - Apresentação Página Home.

Objetivo do Teste:
> - Verificar se ao acessar o site, a página home está apresentando informações necessárias para que o usuário crie sua conta, faça login e descubra mais sobre o objetivo da aplicação.

Passos:
> - Acessar o site: [AgendaHub](https://agendahub20231119201019.azurewebsites.net)
> - Verificar se a página home é carregada corretamente.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/222dd46d-1f9d-4571-8cef-ed5e39442ca2)


> - Confirmar a responsividade da página em dispositivos móveis e computadores.
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/f7b6f8d1-6624-44f0-be50-92a76c161447)


> - Validar se a página é exibida corretamente nos navegadores Chrome e Edge.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/c0fb7f93-8261-42f4-a0d3-dd6751cdcbca)
Edge

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/47e7ece2-40ea-4852-a265-1c51b0d43bf2)
Google Chrome

> - Localizar as seções de criação de conta e login na página.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/5995c9d6-6ce1-4928-8ec0-32ae10b5a0ce)


> - Avaliar a presença de informações relevantes sobre o propósito da aplicação.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/dc325bf3-92f8-4548-bc22-f274e1368d63)



<b>Resultado CT - 01: </b> Todos os objetivos elencados para o Caso de Teste foram atendidos, o site apresentou responsividade, informações necessárias para que o usuário crie a sua conta, faça login e descubra mais sobre o objetivo da aplicação.


## CT - 02 - Cadastro e Login.

Objetivo do Teste:
> - Verificar se é possível efetuar um novo cadastro e, após efetuar o login utilizando as mesmas credenciais, obter êxito. Além disso, o sistema deve permitir a alteração das informações cadastrais iniciais.

Passos:
> - Acessar o site: [AgendaHub](https://agendahub20231119201019.azurewebsites.net)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/c572adb1-c554-4ccc-bf67-1a8bac5ad87f)

> - Realizar um novo cadastro inserindo informações válidas.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/52c0a5e7-53bc-4725-9a56-2b7a42d95335)

> - Efetuar o login utilizando as credenciais cadastradas.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/267ae4af-a759-45bf-aedf-89f27d13edc1)

> - Verificar se o login é bem-sucedido.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/1d388984-ca3b-491d-84ae-dac080fc0336)

> - Acessar a seção de alteração de informações cadastrais.
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/e2f29c47-48cc-43d2-819e-7aa0bf219a9e)


> - Modificar o login, senha e e-mail cadastrados.
> - Confirmar se as alterações foram salvas com sucesso.

<b>Resultado CT - 02: </b> 

## CT - 03 - CRUD de Serviço

Objetivo do Teste:
> - O objetivo deste teste é garantir que o administrador consiga realizar as operações CRUD (Create, Read, Update, Delete) de um novo serviço oferecido pelo profissional no sistema

Passos:
> - Acessar o site: [AgendaHub](https://agendahub20231119201019.azurewebsites.net)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/c572adb1-c554-4ccc-bf67-1a8bac5ad87f)

> - Efetuar o login utilizando as credenciais cadastradas.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/267ae4af-a759-45bf-aedf-89f27d13edc1)

> - Acessar a seção de novo serviço.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/a71bfdb8-5817-4575-82fa-4fecc2bcf155)
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/0a12d62b-b103-487c-9251-28fa3ddd2f1c)
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/ca2d18e9-6c42-4966-82d5-7fb335f80f8a)

> - Preencher todos os campos obrigatórios.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/cc2385d2-c69c-4fed-b969-db5bd8367725)

> - Submeter o formulário.

Consulta do Serviço Cadastrado:
> - Vá para a lista de serviços cadastrados.
> - Localizar o serviço adicionado no passo anterior.
> - Verificar se as informações do serviço estão corretas e correspondem aos dados fornecidos no cadastro.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/4fbcdc03-513b-42b0-83bc-05d3bfe7f2cd)

Atualização do Serviço:
> - Editar os detalhes do serviço cadastrado.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/a3cd391b-e395-4815-91a2-58a0ab1f1815)
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/51ac5ffb-c792-4fea-b1e3-9dcc9ab72c2c)

>- Confirmar se as informações foram atualizadas corretamente.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/ffc581c8-0075-426e-8112-ca7c014fa7db)

Exclusão do Serviço:
> - Remover o serviço cadastrado.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/e48fef6e-5614-4e2c-9213-35214f0562f2)
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/1f2f430e-8961-4680-b3bb-50e736f9445c)

> - Verificar se o serviço não está mais presente na lista de serviços cadastrados.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/ad5b6838-23f2-4796-a48a-f3128a90f616)


<b>Resultado CT - 03: </b> O teste obteve êxito em todos os critérios elencados, cadastrou um novo serviço com sucesso, sem erros ou falhas, a consulta do serviço recém-cadastrado exibiu todas as informações corretamente, a atualização do serviço refletiu as modificações feitas e a exclusão removeu efetivamente o serviço da lista. 

## CT - 04 - CRUD de Cliente.

Objetivo do Teste:
> - O objetivo deste teste é garantir que o administrador consiga realizar as operações CRUD (Create, Read, Update, Delete) de um novo cliente no sistema.

Passos:
> - Acessar o site: [AgendaHub](https://agendahub20231119201019.azurewebsites.net)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/c572adb1-c554-4ccc-bf67-1a8bac5ad87f)

> - Efetuar o login utilizando as credenciais cadastradas.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/267ae4af-a759-45bf-aedf-89f27d13edc1)

> - Acessar a seção de novo cliente.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/a71bfdb8-5817-4575-82fa-4fecc2bcf155)
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/b262bf84-0e60-410d-aa53-b3db2da4fdcd)
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/8b9e0e80-d077-4a73-ac34-3d0184fd446a)

Cadastro de Novo Cliente:
> - Selecionar a opção para cadastrar um novo cliente.
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/6630dabf-6157-4f0c-a769-522c2f5536a4)

> - Preencher todos os campos obrigatórios.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/25cf30b4-b8cc-4510-830a-e8d13773d6c4)

> - Submeter o formulário.

Consulta do Cliente Cadastrado:
> - Vá para a lista de clientes cadastrados.
> - Localizar o cliente adicionado no passo anterior.
> - Verificar se as informações do cliente estão corretas e correspondem aos dados fornecidos no cadastro.
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/ac958064-0265-4004-a959-29f454abf3cb)

Atualização do Cliente:
> - Editar os detalhes do cliente cadastrado.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/cb186673-1d8c-4a14-95ca-c7ed56fd7840)
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/6a9d777b-91c8-4bf8-9757-17510addd67a)

> - Confirmar se as informações foram atualizadas corretamente.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/8cb1e00b-da7a-4fff-8026-3d9cd802d424)

Exclusão do Cliente:
> - Remover o cliente cadastrado.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/893edabb-50ab-4584-bc28-65431dd3168e)
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/5086070c-dc1b-4187-97df-e706ab33c150)

> - Verificar se o cliente não está mais presente na lista de clientes cadastrados.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/5282a3b1-407c-4a33-b008-672f3b0c6dfd)


<b>Resultado CT - 04: </b> O teste obteve êxito, visto que atendeu a todos os critérios. O cadastro do cliente foi bem-sucedido sem erros ou falhas, a consulta do cliente recém-cadastrado exibiu todas as informações corretamente, a atualização refletiu as modificações feitas e a exclusão removeu efetivamente o cliente da lista. 

## CT - 08 - CRUD de Profissional

Objetivo do Teste:
> - O objetivo deste teste é garantir que o administrador consiga realizar as operações CRUD (Create, Read, Update, Delete) de um novo profissional no sistema.

Passos:
> - Acessar o site: [AgendaHub](https://agendahub20231119201019.azurewebsites.net)

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/c572adb1-c554-4ccc-bf67-1a8bac5ad87f)

> - Efetuar o login utilizando as credenciais cadastradas.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/267ae4af-a759-45bf-aedf-89f27d13edc1)

> - Acessar a seção de administração.

Cadastro de Novo Profissional:

> - Selecionar a opção para cadastrar um novo profissional.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/ae2a1cba-5d13-4008-8200-2a9506bed4f8)

> - Preencher todos os campos obrigatórios do formulário de cadastro de profissional.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/4474e83c-9e03-4097-b41a-0e08f16e9611)

> - Submeter o formulário.

Consulta do Profissional Cadastrado:

> - Vá para a lista de profissionais cadastrados.
> - Localizar o profissional adicionado no passo anterior.
> - Verificar se as informações do profissional estão corretas e correspondem aos dados fornecidos no cadastro.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/8a392d6f-dc0c-4c7c-96ef-cb0de9d9fba4)

Atualização do Profissional:

> - Editar os detalhes do profissional cadastrado:

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/b0375b7a-77b7-47dc-be1c-b297aeeb1a5d)
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/e33addbe-802e-4c21-9112-4886099bca43)

> - Confirmar se as informações foram atualizadas corretamente.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/7dfb4fa1-d0c7-48cc-8b95-da4c813c91ff)

> - Exclusão do Profissional:

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/ca87425c-f91e-42bc-81d4-3ebfa486a1cf)
![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/ccb4a3a9-9da5-4441-b462-394d8a13606e)

> - Verificar se o profissional não está mais presente na lista de profissionais cadastrados.

![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/b31a1c02-bbd3-4ecf-a18d-2cf2bf687bde)

<b>Resultado CT - 08: </b> O Teste obteve êxito, visto que o cadastro do novo profissional foi bem-sucedido, sem erros ou falhas. A consulta retornou o profissional recém-cadastrado e exibiu todas as informações corretamente. A atualização das informações refletiram as modificações feitas e a exclusão do profissional foi efetivada com sucesso. 

