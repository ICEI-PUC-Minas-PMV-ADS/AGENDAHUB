Plano de Testes de Software  AgendaHub

Introdução

O objetivo deste plano de teste é garantir que o site de agendamento esteja funcionando corretamente antes do lançamento. O site deve ser testado para garantir que ele seja seguro, confiável e fácil de usar. O plano de teste inclui testes manuais e automatizados para garantir que todas as funcionalidades do site estejam funcionando corretamente.

Escopo

O escopo deste plano de teste abrange todas as funcionalidades do site de agendamento, incluindo:

Requisitos Funcionais:

RF-01	O sistema deve permitir um usuário se cadastrar para receber acesso ao sistema.	

RF-02	O sistema deve ser protegido por autenticação de login e senha.	

RF-03	O sistema deve permitir ao administrador realizar o CRUD de um novo serviço oferecido pelo profissional.

RF-04	O sistema deve permitir ao administrador realizar o CRUD de um novo cliente na base de dados.	

RF-05	O sistema deve permitir ao administrador realizar o CRUD de um novo agendamento.	

RF-06	O sistema deve permitir ao administrador realizar o CRUD de uma nova movimentação financeira.	

RF-07	O sistema deve permitir ao administrador realizar o CRUD de um novo colaborador.	

RF-08	O sistema deve permitir alterar informações sobre a empresa ex: endereço, conta de recebimento, dentre outros.	

RF-09	O sistema deve permitir alterar informações cadastrais ex: login, senha e e-mail.	

RF-10	O sistema deve oferecer a possibilidade de exigir ou não um pagamento para realizar agendamentos.	

RF-11	O sistema deve permitir o cliente realizar, visualizar, editar e cancelar um agendamento.	

RF-12	O sistema deve permitir o cliente visualizar os serviços oferecidos pelo profissional e tirar dúvidas.	

RF-13	O sistema deve oferecer a possibilidade de o cliente efetuar um pagamento antecipado.	


Requisitos não funcionais:

RNF-01	A aplicação deve ter uma página inicial para apresentar o sistema	

RNF-02	Os dados dos usuários (incluindo informações pessoais, senhas e dados financeiros) devem ser armazenados de forma segura e 
criptografada.	

RNF-03	As transações financeiras devem ser protegidas por medidas de segurança, como SSL/TLS, para garantir a integridade e confidencialidade.	

RNF-04	O sistema deve ser responsivo, com tempos de carregamento curtos para evitar frustração do usuário.	

RNF-05	O sistema deve ser capaz de lidar com um aumento no número de usuários, serviços e agendamentos sem degradação significativa do desempenho.	

RNF-06. Deve haver medidas de backup e recuperação em caso de falha do sistema.	

RNF-07	A interface do usuário deve ser intuitiva e de fácil utilização, mesmo para usuários não familiarizados com sistemas similares.

RNF-08	O sistema deve ser compatível com uma variedade de dispositivos e navegadores populares como Chrome e Edge.

RNF-09	O código deve ser bem estruturado e modular, facilitando a manutenção e a adição de novos recursos no futuro.	

RNF-10	O sistema deve cumprir as regulamentações de proteção de dados e privacidade, como o RGPD.	

RNF-11	O sistema deve utilizar um banco de dados específico, como o SQL Server ou SQLite, para armazenar os dados do sistema.	

RNF-12	O sistema deve utilizar a tecnologia C# e a plataforma .NET para o desenvolvimento do backend.	

RNF-13	O sistema deve utilizar as tecnologias básicas de desenvolvimento web front-end.


Tipos de Teste

Os testes serão divididos em diferentes tipos, incluindo:

Teste de unidade: testes realizados nos componentes individuais do sistema.

Teste de integração: testes realizados para verificar a integração entre os componentes do sistema.

Teste funcional: testes realizados para validar as funcionalidades do sistema.

Teste de usabilidade: testes realizados para verificar a facilidade de uso e a experiência do usuário.

Teste de desempenho: testes realizados para verificar a capacidade do sistema em lidar com situações de alta demanda.

Teste de segurança: testes realizados para verificar a segurança do sistema contra ameaças externas.


A aplicação deve ter uma homepage para apresentar o sistema:

Verificar se a página inicial é carregada corretamente.

Verificar se há informações relevantes sobre o sistema na página inicial.

Verificar se há links para outras páginas no sistema a partir da página inicial.


O sistema deve permitir um usuário se cadastrar para receber acesso ao sistema:

Verificar se o formulário de cadastro é exibido corretamente.

Verificar se os campos obrigatórios estão marcados corretamente.

Verificar se a validação de entrada é realizada corretamente.

Verificar se o usuário é redirecionado para a página de login após o cadastro.


O sistema deve ser protegido por autenticação de login e senha:

Verificar se o formulário de login é exibido corretamente.

Verificar se os campos obrigatórios estão marcados corretamente.

Verificar se a validação de entrada é realizada corretamente.

Verificar se o usuário é redirecionado para a página inicial após o login bem-sucedido.

Verificar se as credenciais incorretas são rejeitadas e uma mensagem de erro é exibida.


O sistema deve permitir o administrador cadastrar, visualizar, alterar e excluir um novo serviço oferecido pelo profissional:

Verificar se o formulário de cadastro de serviço é exibido corretamente.

Verificar se os campos obrigatórios estão marcados corretamente.

Verificar se a validação de entrada é realizada corretamente.

Verificar se o serviço cadastrado é exibido corretamente na lista de serviços.

Verificar se um serviço pode ser editado e excluído corretamente.


O sistema deve permitir o administrador cadastrar, visualizar, alterar e excluir um novo cliente na base de dados:

Verificar se o formulário de cadastro de cliente é exibido corretamente.

Verificar se os campos obrigatórios estão marcados corretamente.

Verificar se a validação de entrada é realizada corretamente.

Verificar se o cliente cadastrado é exibido corretamente na lista de clientes.

Verificar se um cliente pode ser editado e excluído corretamente.


O sistema deve permitir o administrador cadastrar, visualizar, alterar e excluir um novo agendamento:

Verificar se o formulário de cadastro de agendamento é exibido corretamente.

Verificar se os campos obrigatórios estão marcados corretamente.

Verificar se a validação de entrada é realizada corretamente.

Verificar se o agendamento cadastrado é exibido corretamente na lista de agendamentos.

Verificar se um agendamento pode ser editado e excluído corretamente.


O sistema deve permitir o administrador cadastrar, visualizar, alterar e excluir uma nova movimentação financeira:

Verificar se o formulário de cadastro de movimentação financeira é exibido corretamente.

Verificar se os campos obrigatórios estão marcados corretamente.

Verificar se a validação de entrada é realizada corretamente.

Verificar se a movimentação financeira cadastrada é exibida corretamente na lista de movimentações financeiras.

Verificar se uma movimentação financeira pode ser editada e excluída corretamente.


O sistema deve permitir o administrador cadastrar, visualizar, alterar e excluir um novo colaborador:

Verificar se o formulário de cadastro de colaborador é exibido corretamente.

Verificar se os campos obrigatórios estão marcados corretamente.

Verificar se a validação de entrada é realizada corretamente.

Verificar se o colaborador cadastrado é exibido corretamente na lista de colaboradores.

Verificar se um colaborador pode ser editado e excluído corretamente.


O sistema deve permitir alterar informações sobre a empresa ex: endereço, conta de recebimento, dentre outros.

Verificar se o formulário de alteração de informações da empresa é exibido corretamente.

Verificar se os campos obrigatórios estão marcados corretamente.

Verificar se a validação de entrada é realizada corretamente.

Verificar se as informações da empresa são atualizadas corretamente após a alteração.


O sistema deve permitir alterar informações cadastrais ex: login, senha e email.

Verificar se o formulário de alteração de informações cadastrais é exibido corretamente.

Verificar se os campos obrigatórios estão marcados corretamente.

Verificar se a validação de entrada é realizada corretamente.

Verificar se as informações cadastrais são atualizadas corretamente após a alteração.


O sistema deve oferecer a possibilidade de exigir ou não um pagamento para realizar agendamentos:

Verificar se a opção de pagamento está disponível no formulário de cadastro de agendamento.

Verificar se os campos relacionados ao pagamento são exibidos ou ocultados conforme a opção selecionada.


O sistema deve permitir o cliente realizar, visualizar, editar e cancelar um agendamento:

Verificar se o cliente pode visualizar seus agendamentos na lista de agendamentos.

Verificar se o cliente pode editar e cancelar seus próprios agendamentos.


O sistema deve permitir o cliente visualizar os serviços oferecidos pelo profissional e tirar dúvidas:

Verificar se a lista de serviços está disponível para visualização pelo cliente.
Verificar se há uma opção para entrar em contato com o profissional para tirar dúvidas.


O sistema deve oferecer a possibilidade de o cliente efetuar um pagamento antecipado:

Verificar se a opção de pagamento antecipado está disponível no formulário de cadastro de agendamento.

Verificar se os campos relacionados ao pagamento antecipado são exibidos ou ocultados conforme a opção selecionada.


Os dados dos usuários (incluindo informações pessoais, senhas e dados financeiros) devem ser armazenados de forma segura e criptografada:

Verificar se as informações do usuário são armazenadas com segurança e criptografia adequadas.


As transações financeiras devem ser protegidas por medidas de segurança, como SSL/TLS, para garantir a integridade e confidencialidade:

Verificar se as transações financeiras são protegidas por SSL/TLS.


O sistema deve ser responsivo, com tempos de carregamento curtos para evitar frustração do usuário:

Testar a expansividade do sistema em diferentes dispositivos e tamanhos de tela.

Medir os tempos de carregamento das páginas e garantir que estejam dentro dos limites aceitáveis.


O sistema deve ser capaz de lidar com um aumento no número de usuários, serviços e agendamentos sem degradação significativa do desempenho:

Realizar testes de carga para avaliar o desempenho do sistema sob diferentes cargas simuladas.


Deve haver medidas de backup e recuperação em caso de falhas no sistema ou perda de dados:

Testar as medidas de backup e recuperação para garantir que funcionem conforme esperado em diferentes cenários.

Conclusão
Este plano de teste abrange todas as funcionalidades do site de agendamento e inclui testes manuais e automatizados para garantir que todas as funcionalidades estejam funcionando corretamente. O plano também inclui critérios claros para aceitação dos testes e procedimentos detalhados para cada um dos testes planejados.
