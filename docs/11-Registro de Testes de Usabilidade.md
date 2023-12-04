# Registro de Testes de Usabilidade

Após realizar os testes de usabilidade, obtém-se um relatório a partir das análises realizadas. O Registro de Testes de Usabilidade é um relatório que contém as evidências dos testes e relatos dos usuários participantes, baseado no Plano de Testes de Usabilidade desenvolvido para os casos de uso desta etapa.

| Nível  | Descrição                                                                                      |
|--------|----------------------------------|
| Nível 0 | Não é encarado necessariamente como um problema de usabilidade.                                     |
| Nível 1 | Problema estético que não tem necessidade de ser corrigido, a menos que haja tempo e recurso disponível. |
| Nível 2 | Pequeno problema com baixa prioridade na correção.                                               |
| Nível 3 | Problema com alta prioridade de correção.                                                       |
| Nível 4 | Catástrofe de usabilidade, ou seja, o produto só será liberado se a correção for feita.          |


# Tabela de Avaliação da Usabilidade Funcional
**Atividade: Avaliação dos botões e Section (Home Page)**


| Avaliador | Funcionalidade   | Notas dos Avaliadores | Média | Consenso | Feedback dos Avaliadores | Considerações    | Sugestão de Melhorias |
|-----------|------------------|-----------------------|-------|----------|--------------------------|------------------|-----------------------|
| Josué     | Botões Header<br>(Serviços,Sobre, Empresa)    |  Josué: 3             | 3     |  Sim     |  O botão  "Serviços" direciona corretamente para a seção correspondente | A navegação de seção para "Serviços" está funcionando conforme o esperado. Os outros botões “Sobre” e “Empresa” sem interação não estão prontos.| Finalização dos outros botões para habilitar a interação.|
| Josué     | Botões Header<br>(Login, Cadastre-se) | Josué: 3 | 3 | Sim | O botão “Login” direciona corretamente para a página de autenticação de usuário.| Habilite o direcionamento do botão "Cadastre-se" para a página de login e cadastro. | Finalizar interação |
| Josué   | Seção - Cadastre Seus Serviços | Josué: 3 | 3 | Sim | A seção apresenta um vídeo demonstrativo que deve fornecer como o usuário deve cadastrar seus serviços na aplicação, no entanto o video ainda não está pronto. | Avaliar a necessidade de adicionar informações a seção para melhorar o preechimento dos espaços e a interface gráfica da seção correspondente | Adicionar um divisor de section e melhorar o estilo/design do título correspondente |
| Josué | Seção - Exibe Para Seus Clientes | Josué: 1 | 1 | Sim | A seção apresenta uma interação dos serviços cadastrados em cards, o que fornece uma clara experiência da aplicação de forma prévia e visual para os usuários. | Essa prévia visualização ajuda os usuários a se familiarizar com o produto do sistema| Adicionar um divisor de section e melhorar o estilo/design do título correspondente |
| Josué | Footer - Rodapé | Josué: 3 | 3 | Sim | O Footer apresenta a logo da aplicação com alguns ícones de redes sociais “Instagram”, “Email”, “Whatsapp”, ainda sem interação para os perfis correspondentes que devem ser usados posteriormente como suporte ao usuário e divulgação do sistema.| A logo tem sua visibilidade prejudicada pela coloração do footer. | Trabalhar na implementação dessas interações, de modo que os usuários possam clicar nos ícones de redes sociais para acessar os perfis correspondentes. Ajustar o contraste entre a logo e o fundo do footer. |

# Tabela de Avaliação da Usabilidade Funcional
**Atividade: Seção do Painel Administrativo: Agendamento**

| Avaliador | Funcionalidade   | Notas dos Avaliadores | Média | Consenso | Feedback dos Avaliadores | Considerações    | Sugestão de Melhorias |
|-----------|------------------|-----------------------|-------|----------|--------------------------|------------------|-----------------------|
| Josué     | Botão “Novo Agendamento” | Josué: 1  | 1 | Sim | O botão corresponde ao esperado, levando ao formulário de agendamento. | O botão "Novo Agendamento" está localizado no final da tela a esquerda no canto superior, o que pode dificultar o acesso e a visibilidade. | Adicionar uma margem à esquerda para que o botão não fique muito próximo ao final da tela horizontalmente. Isso pode melhorar o preenchimento do espaço com os elementos. |
| Josué     | Formulário “Novo Agendamento” | Josué: 1 | 1 | Sim | **Título:** O título com o nome da funcionalidade no topo da página corresponde com a “Visibilidade do status”.<br> --------------------- <br> **Campo Serviço:** O campo type="text" fornece digitação livre para o usuário.<br> --------------------- <br>**Campo Cliente:** O campo apresenta tipo select(Dropdown), uma solução eficaz. <br> --------------------- <br> **Campo “Data”:** Possui um ícone de interação de calendário, o que está alinhado com o princípio de "Compatibilidade com o mundo real". <br> --------------------- <br> **Campo “Hora”:** Atende à função esperada e inclui um ícone de relógio, o que está alinhado com o princípio de "Compatibilidade com o mundo real".<br> --------------------- <br>**Campo "Status":** Corresponde com a função esperada no formulário. <br> --------------------- <br> **Campo "Profissional":**  O campo type: text fornece digitação livre para o usuário. <br> ---------------------  <br> **Botão “Voltar” e “Salvar”:** Apresenta a função esperada.| Não foi encontrado um campo que forneça o valor do serviço prestado. | **Título:** Corresponde ao esperado.<br> -------------------- <br> **Campo Serviço:** Considere trocar o tipo para Select (Dropdown), isso evita erros de digitação. <br> -------------------- <br> **Campo Cliente:** Considere trocar para um campo de pesquisa em um momento de grande volume de base de clientes, essa mudança poderá facilitar a usabilidade do usuário neste cenário. <br> -------------------- <br> **Campo Profissional:** Considere reordenar os campos no formulário, nesta ordem: Cliente, Profissional, Serviço, Data, Hora, Status. Essa mudança pode ser aplicada para melhorar e ajudar a implementar a regra de negócio. Considere trocar o type=”text” para para Select (Dropdown).|
| Josué     | Agendamentos Realizados(lista de cards) | Josué: 1 | 1 | Sim | A lista de agendamentos realizados seguem a ordem de o ultimo a entrar é o ultimo da fila, correponde com o esperado. <br> -------------------- <br> **Botão “Editar”:** Apresenta ícone de edição de acordo com a “Compatibilidade com o mundo real”. <br> -------------------- <br> Botão “Excluir”: Apresenta ícone de uma lixeira  de acordo com a “Compatibilidade com o mundo real”. | **1.** Apresenta um design moderno e limpo com informações organizadas.<br> ----------------- <br> **2.** Ao clicar no ícone de editar o sistema retorna o agendamento realizado com o formulario de edição, o que corresponde  com o esperado.<br> ----------------- <br> **3.** Ao clicar no ícone de lixeira para excluir um agendamento, o sistema retorna o card selecionado para confirmação, o que é uma boa prática para evitar erros do usuário.| |
| Josué     | Editar Agendamento |Josué: 1 | 1 | Sim | Apresenta o mesmo formulário da funcionalidade de novo agendamento, com as informações do agendamento realizado e a possibilidade de edição. | **Botão “Voltar” e “Salvar”:**  condizem com o esperado, retornando a página de agendamento e salvando as alterações realizadas. | **Título:** Corresponde ao esperado.<br> -------------------- <br> **Campo Serviço:** Considere trocar o tipo para Select (Dropdown), isso evita erros de digitação. <br> -------------------- <br> **Campo Cliente:** Considere trocar para um campo de pesquisa em um momento de grande volume de base de clientes, essa mudança poderá facilitar a usabilidade do usuário neste cenário. <br> -------------------- <br> **Campo Profissional:** Considere reordenar os campos no formulário, nesta ordem: Cliente, Profissional, Serviço, Data, Hora, Status. Essa mudança pode ser aplicada para melhorar e ajudar a implementar a regra de negócio. Considere trocar o type=”text” para para Select (Dropdown).|
| Josué     | Excluir Agendamento | Josué: 3 | 3 | Sim | **1.** Ao clicar no ícone de lixeira para excluir um agendamento, o sistema apresenta uma confirmação, o que é uma boa prática para evitar erros por parte do usuário.<br> -------------------- <br> **Botão “Voltar”:** corresponde com o resultado esperado de voltar para a página anterior “Agendamentos”. | Após confirmar a exclusão, ocorre um erro: "Nenhuma página da web foi encontrada para o endereço "https:<br>//localhost:44331/<br>Agendamentos<br>/DeleteConfirmed". | Corrigir o erro que ocorre ao confirmar a exclusão de um agendamento. |
| Josué     | Campo Pesquisa | Josué:3 | 3 | Sim | O Campo de pesquisa funciona com a busca do agendamento especificado, sendo pelo nome do serviço, cliente ou profissional.| **1.** Não foi possível voltar a todos os serviços sem usar a seta fornecida pelo navegador.<br> -------------------- <br> **2.** Não foi possível refinar a busca por data específica.<br> -------------------- <br> **3.** Não foi possível buscar pelos agendamentos pendentes.| **1.** Considere a inclusão de voltar a todos os agendamentos, ou limpar a busca realizada.<br> -------------------- <br> **2.** Considere adicionar um filtro de data para melhorar a busca do usuário.<br> -------------------- <br> **3.** Considere adicionar no filtro a opção de buscar pelo status.|

# Tabela de Avaliação da Usabilidade Funcional
**Atividade: Seção do Painel Administrativo: Clientes**

| Avaliador | Funcionalidade   | Notas dos Avaliadores | Média | Consenso | Feedback dos Avaliadores | Considerações    | Sugestão de Melhorias |
|-----------|------------------|-----------------------|-------|----------|--------------------------|------------------|-----------------------|
| Josué | Botão “Novo Cliente” | Josué: 0 | 0 | Sim | O botão corresponde ao esperado, levando ao formulário de cadastrar novo cliente.| |
| Josué | Formulário - Cadastrar Novo Cliente | Josué: 3 | 3 |  Sim | **Campo “Nome”:** Apresenta função esperada de capturar a informação do usuário.<br> ------------------ <br> **Campo “CPF”:** Apresenta função esperada de capturar a informação do usuário. (Visualizar Coluna consideração) <br> ------------------ <br> **Campo “Contato”:** Apresenta função esperada de capturar a informação do usuário. (Visualizar Coluna consideração) <br> ------------------ <br> **Campo “Email”:** Apresenta função esperada de capturar a informação do usuário. (Visualizar Coluna consideração) <br> ------------------ <br> **Campo “Observação”:** Apresenta função esperada de capturar a informação do usuário. <br> ------------------ <br> **Botão “Voltar e Salvar”:** Apresenta função esperada. | **Campo “CPF”:** Não possui regra de validação do CPF, aceitando caracteres alfabéticos, símbolos especiais. Campo com digitação ilimitada. <br> -------------------- <br> **Campo “Contato”:**  Não possui regra de validação de telefone, aceitando caracteres alfabéticos, símbolos especiais. Campo com digitação ilimitada.  <br> -------------------- <br> **Campo “Email”:** Não possui regra de validação de e-mail, podendo capturar e-mail com formato invalido. <br> -------------------- <br> O formulário aceitou dados de entrada inválidos no campos CPF, Contato e e-mail. | **Campo “CPF”:** Considere adicionar regra de validação de CPF para que a aplicação não permita mau uso da funcionalidade pelo usuário, além disso, altere o type do input para number. <br> -------------------- <br> **Campo “Contato”:** Considere adicionar regra de validação com atributo pattern e Placeholders para que a aplicação não permita mau uso da funcionalidade pelo usuário, além disso, altere o type do input para number.  <br> -------------------- <br> **Campo “Email”:** Considere adicionar placeholder no campo e trocar o type=”text” para type="email". |
| Josué | Clientes Cadastrados(lista) | Josué: 4 | 4 | Sim | A seção apresenta uma lista com os clientes cadastrados, o ultimo cliente a entrar é o ultimo da lista conforme como esperado.<br> ------------------ <br> **Coluna Observação:** Ao digitar uma observação de texto longo a página se expande com uma barra inferior scroll, empurrando as informações e a o layout para fora da tela. | Todos os campos no formulário não tem a limitação de entrada de caracteres, o mesmo pode acontecer com os outros campos o que foi observado no campo da coluna  de observações. | Considere limitar os dados de entrada do usuário a um determinado x de caracteres, ou trate os campos de exibição da lista para ocultar quando atingir determinada quantidade, essas alterações são relevantes para não comprometer o layout da aplicação e o seu uso |
| Josué | Botão editar cliente (representado por um ícone de papel e caneta.) | Josué: 0 | 0 | Sim | Ao clicar no ícone o sistema responde direcionando para o formulário de edição conforme esperado. |  | |
| Josué | Formulário - Editar Cliente | Josué: 3 | 3 | Sim | Os campos de entrada apresentam as mesmas considerações de sugestão de melhoria da funcionalidade “Cadastrar Novo Cliente”.<br> ------------------ <br> **Botões “Voltar e Salvar”:** Condizem com a função esperada. | | Faça as mesmas alterações recomendadas da funcionalidade “Cadastrar Novo Cliente” | 
|Josué | Excluir Cliente(ícone lixeira) | Josué: 0 | 0 | Sim | Apresenta resultado esperado, redirecionando para página de confirmação. <br> ------------------ <br> **Botões “Voltar e Exclui”:** Apresenta resultado esperado | | | 
| Josué | Campo pesquisa | Josué: 2 | 2 | Sim | O Campo de Pesquisa funciona com a busca do agendamento especificado, sendo pelo nome do cliente, CPF, telefone, e-mail e observação. | **1.** Não foi possível voltar à lista de clientes sem usar a seta fornecida pelo navegador. | **1.** Considere a inclusão de voltar a todos os agendamentos, ou limpar a busca realizada. |

# Tabela de Avaliação da Usabilidade Funcional
**Atividade: Seção do Painel Administrativo: Serviços**

| Avaliador | Funcionalidade   | Notas dos Avaliadores | Média | Consenso | Feedback dos Avaliadores | Considerações    | Sugestão de Melhorias |
|-----------|------------------|-----------------------|-------|----------|--------------------------|------------------|-----------------------|
| Josué     | Botão “Novo Serviço”| Josué: 0 | 0 | Sim | O botão corresponde ao esperado, levando ao formulário de cadastrar serviço.| | |
| Josué | Cadastrar Serviço(formulário) | Josué: 4 | 4 | Sim | **Campo “Nome”:** Apresenta função esperada de capturar a informação do usuário. <br> --------------- <br> **Campo “Preço”:** Apresenta função esperada de capturar a informação do usuário. <br> --------------- <br> **Campo “Tempo de execução”:** Apresenta função esperada de capturar a informação do usuário. | **Campo “Preco”:** Validação de tipo number correta, ao tentar criar um serviço com preço com caracter alfabético o sistema apresentou a mensagem “O campo Preço deve ser um número.” Ao clicar em criar, a aplicação não prosseguiu, como era esperado. <br> --------------- <br> **Campo “ Tempo de execução”:** Validação correta, ao tentar criar o serviço com o tempo definido com caracter alfabético o sistema apresentou a mensagem “O valor 'dksjd' não é válido para TempoDeExecucao.”  Ao clicar em criar, a aplicação não prosseguiu, como era esperado. No entanto, ao digitar o número 125  sem formatação de hora a aplicação não respondeu bem, parando o funcionamento do sistema. | Considere adicionar placeholder ou modelos de entrada de dados nos campos “Preço” e “Tempo de execução”, essa implementação é necessária para evitar uma catástrofe de usabilidade.|
| Josué | Lista de Serviços cadastrados | Josué: 3 | 3 | Sim | Os serviços são exibidos em cards, sendo 3 cards por linha, apresenta a visualização correta onde o último a entrar é o último da lista. <br> --------------- <br> **Imagens Serviços:** Apresenta formatação incorreta, onde a regra de dimensão distorce a imagem dependendo da largura e altura da imagem carregada pelo usuário. | | |
| Josué | Editar Serviços | Josué: 4 | 4 | Sim | Funcionalidade em desenvolvimento, ainda apresentando erros de usabilidade. | Ao editar o serviço no formulário não recupera do banco de dados a imagem correspondente, deixando o campo vazio.| |
| Josué | Excluir Serviço | Josué: 4 | 4 | Sim | Funcionalidade em desenvolvimento, ainda apresentando erros de usabilidade. | |
| Josué | Campo Pesquisa | Josué: 2 | 2 | Sim | O Campo de Pesquisa funciona com a busca do serviço especificado.| **1.** Não foi possível voltar à lista de serviços sem usar a seta fornecida pelo navegador. | **1.** Considere a inclusão de voltar a todos os serviços, ou limpar a busca realizada. |

# Tabela de Análise Heurística


A análise heurística foi conduzida para avaliar a usabilidade da aplicação AGENDAHUB. Foram considerados princípios de usabilidade e boas práticas de design, seguindo heurísticas estabelecidas. A análise abrange funcionalidades críticas para empresas/profissionais autônomos/prestadores de serviços e usuários (clientes) da empresa.


| ID | Caracteríscias | Sim | Não |N/A | Comentários |
|----|----------------|-----|-----|----|-------------|
| 1 | Visibilidade do status do sistema|
|1.1|As telas do sistema iniciam com um título que descreve seu conteúdo?| x | o | o | 
|1.2|O ícone selecionado é destacado dos demais não selecionados?| x | o | o |
|1.3|Há feedback visual do menu ou escolhas selecionadas?| x | o | o | Indicadores visuais de sucesso/falha durante o processo de login e operações CRUD.|
|1.4|O sistema provê visibilidade do estado atual e alternativas para ação?| x | o | o | Melhorar a visibilidade de feedback para ações que envolvem alterações de dados, como cadastros e atualizações.|
| 2 | Correspondência entre sistema e mundo real|
|2.1|Os ícones e ilustrações são concretos e familiares?| x | o | o | 
|2.2|As cores, quando utilizadas, correspondem aos códigos de cores comuns?| x | o | o | 
|2.3|A linguagem utilizada evita jargões técnicos?| x | o | o |  A terminologia usada nas funcionalidades reflete de forma clara as ações no mundo real.|
|2.4|Os números são devidamente separados nos milhares e nos decimais?| x | o | o | 
| 3 | Controle do usuário e liberdade |
|3.1|Se o sistema utiliza janelas que se sobrepõem, ele permite a organização e a troca simples?| o | o | x | O sistema não tem janelas que se sobrepões. |
|3.2|Quando o usuário conclui uma tarefa, o sistema aguarda uma ação antes de processar?| o | o | x | 
|3.3|O usuário é solicitado a confirmar tarefas que possuem consequências drásticas?| x | o | o | Opções de cancelamento e confirmação estão presentes durante operações críticas, proporcionando segurança ao usuário..|
|3.4|Existe uma funcionalidade para desfazer ações realizadas pelo usuário?| o | x | o | 
|3.5|O usuário pode editar, copiar e colar durante a entrada de dados?| x | o | o |
|3.6|O usuário pode pode se mover entre campos e janelas livremente?| x | o | o |
|3.7|O usuário pode configurar o sistema, a sessão, a tela conforme sua preferência?| x | o | o | É possível alterar para o modo Dark do navegador. |
| 4 | Consistência e padrões |
|4.1|O sistema evita o uso constante de letras maiúsculas?| x | o | o |
|4.2|Os números são justificados à direita e alinhados quanto aos decimais?| x | o | o |
|4.3|Os ícones e ilustrações são rotulados?| x | o | o |
|4.4|As instruções aparecem de forma consistente sempre no mesmo local?| x | o | o |
|4.5|Os objetos do sistema são nomeados de maneira consistente em todo o sistema?| x | o | o |A aplicação segue um layout consistente em várias seções.|
|4.6|Os campos obrigatórios e opcionais são corretamente sinalizados| x | o | o |
| 5 | Prevenções de erros |
|5.1|As opções de menu são lógicas, distintas e mutuamente exclusivas?| x | o | o |
|5.2|Se o sistema exibe múltiplas janelas, a navegação entre as janelas é simples e vísivel?| o | o | x |
|5.3|O sistema alerta o usuário se ele está prestes a fazer erros críticos?| x | o | o |Sinais de alerta e validações são utilizados para prevenir erros durante o preenchimento de formulários.|
| 6 | Reconhecimento ao invés de recordação |
|6.1|Há distinção clara quando é possível selecionar um item ou vários?| x | o | o |
|6.2|Quando necessário, a aplicação fornece instruções contextuais que ajudam os usuários a entenderem melhor o contexto ou as opções disponíveis, facilitando o reconhecimento das funcionalidades?| x | o | o |
|6.3|Durante a realização de ações, a aplicação fornece feedback visual imediato para indicar o status da operação, auxiliando os usuários no reconhecimento do progresso?| x | o | o |Ícones e rótulos intuitivos ajudam os usuários a reconhecerem a função das ações.|
| 7 | Flexibilidade e Eficicência do Uso |
|7.1|A aplicação oferece atalhos e comandos rápidos que permitem aos usuários experientes realizar tarefas de forma eficiente, sem depender exclusivamente de menus tradicionais.?| o | x | o | Para trocar de tela é necessário acessar o menu.|
|7.2|A aplicação é projetada para se adaptar aos diferentes níveis de habilidade dos usuários, proporcionando uma experiência eficiente tanto para novos usuários quanto para usuários experientes?| x | o | o |
|7.3|Os fluxos de trabalho são projetados de maneira eficiente, minimizando o número de etapas necessárias para completar tarefas comuns e otimizando a produtividade?| x | o | o |
| 8 | Estética e Design Minimalista |
|8.1|A interface possui clareza visual que facilita a compreensão das informações e a navegação pelos diferentes elementos da aplicação?| x | o | o |
|8.2|Os elementos visuais são, como cores, tipografia e ícones, são consistentes em toda a aplicação, proporcionando uma experiência visual unificada?| x | o | o |
|8.3|Existe uma hierarquia clara de informações, destacando elementos importantes e orientando o usuário sobre a organização da página?| x | o | o |
|8.4|O design da interface é limpo e organizado, evitando poluição visual e proporcionando uma experiência mais agradável ao usuário.?| x | o | o |O design é claro e minimamente complexo.|

# Conclusão:
A análise heurística destaca várias áreas positivas na usabilidade da aplicação AGENDAHUB. No entanto, há oportunidades para melhorias, especialmente na consistência, visibilidade de feedback e aprimoramento das mensagens de erro. Recomenda-se uma revisão iterativa com base nessas sugestões para aprimorar ainda mais a experiência do usuário.
