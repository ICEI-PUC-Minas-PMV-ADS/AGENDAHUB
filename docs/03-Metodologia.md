
# Metodologia

A metodologia contempla as definições de ferramental utilizado pela equipe tanto para a manutenção dos códigos e demais artefatos quanto para a organização do time na execução das tarefas do projeto.

# Relação de Ambientes de Trabalho

Os artefatos do projeto são desenvolvidos a partir de diversas plataformas e a relação dos ambientes com seu respectivo propósito é apresentada na tabela que se segue.

| Ambiente | Plataforma | Link de Acesso | 
| ------ | --------------------------------------- | ---------- |
| Repositório de Código fonte | GitHub | [Link-Repositório](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2023-2-e2-proj-int-t4-agendaHub) |
| Documentos do projeto | Google Drive | [DocsAgendaHub](https://drive.google.com/drive/folders/1SlFe3c0dFuuwu6Qu-7m3VEGTFdelQIwe?usp=sharing) |
| Projeto de interface e Wireframes | Figma | [Protótipo-Figma](https://www.figma.com/proto/2LaOQ03aPkX95NucGhKfh1/AGENDAHUB?type=design&node-id=107-118&t=TEY040VKm8H17AQM-0&scaling=scale-down&page-id=0%3A1&starting-point-node-id=107%3A118&show-proto-sidebar=1) |
| Gerenciamento do projeto | GitHub | [Quadro-Kanban](https://github.com/orgs/ICEI-PUC-Minas-PMV-ADS/projects/575) |

## Controle de Versão

A ferramenta de controle de versão adotada no projeto foi o
[Git](https://git-scm.com/), sendo que o [Github](https://github.com)
foi utilizado para hospedagem do repositório.

O projeto segue a seguinte convenção para o nome de branches:

- `main`: versão estável já testada do software
- `Develop`: Linha do tempo de desenvolvimento do próximo deploy, contendo funcionalidades não publicadas que serão posteriormente mescladas na branch "Main"
- `feature`: Uma nova funcionalidade precisa ser introduzida
- `Release`: Versão já testada do software, porém instável
- `Hotfix`: Uma funcionalidade encontra-se com problemas
- `Release`: Ambiente de homologação para mesclar as alterações da "Develop" na "Main"

As Branches mecionadas são ilustradas na figura a seguir


![Fluxo de controle do código fonte no repositório git](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2023-2-e2-proj-int-t4-agendaHub/assets/127361540/a0192673-bf5d-426d-b3ee-b4a7fe607c7b)



Fluxo de controle do código fonte no repositório git


## Gerenciamento de Projeto

A equipe utiliza metodologias ágeis, tendo escolhido o Scrum como base para definição do processo de desenvolvimento.

### Divisão de Papéis

- Scrum Master: Lucas Gabriel Duarte Enis
- Product Owner: Mariane de Oliveira Duarte
- Equipe de Desenvolvimento
  - Roger Sato
  - Álvaro Gonçalves Rodrigues
  - Josué Batista de Almeida
  - Evellyn Andrade Alves da Silva 
- Equipe de Design
  - Evellyn Andrade Alves da Silva


### Gerenciamento do Product Backlog

- Backlog: Recebe as tarefas a serem trabalhadas e representa o Product Backlog. Todas as atividades identificadas no decorrer do projeto estão incorporadas a esta lista.
- To Do: Esta lista representa o Sprint Backlog. Este é o Sprint atual que estamos trabalhando.
- In Progress: Quando uma tarefa tiver sido iniciada, ela é movida para cá para ser desenvolvida ativamente.
- Test: Checagem de Qualidade. Quando as tarefas são concluídas, eles são movidas para o “CQ”. No final da semana, eu revejo essa lista para garantir que tudo saiu como planejado.
- Done: Nesta lista são colocadas as tarefas que passaram pelos testes e controle de qualidade e estão prontos para ser entregues ao usuário. Não há mais edições ou revisões necessárias.
- Locked: Quando alguma coisa impede a conclusão da tarefa, ela é movida para esta lista juntamente com um comentário sobre o que está travando a tarefa.

O quadro kanban do grupo desenvolvido na ferramenta de gerenciamento de projetos é apresentado, no estado atual, na figura 3 e está disponível através da URL: [Quadro-Kanban](https://github.com/orgs/ICEI-PUC-Minas-PMV-ADS/projects/575/views/1)


![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/004d2864-65eb-42af-959b-91d715aa62f5)
Figura 3 - Tela do kanban utilizada pelo grupo



**Etiquetas:**
A tarefas são, ainda, etiquetadas em função da natureza da atividade 
e seguem o seguinte esquema de cores/categorias:
- Documentação
- Desenvolvimento
- Bug           


   ![image](https://github.com/ICEI-PUC-Minas-PMV-ADS/AGENDAHUB/assets/130249437/a844a293-7737-49c2-a10c-9154acbb2cac)


