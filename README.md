
`FakeOmmerce - .Net Core`
------------------------------

### `Objetivo`
Desenvolver uma API Rest utilizando .NET Core 3.1, persistindo os dados em um banco NoSql.

#### `Requisitos`
Dada a seguinte entidade

<img src="https://i.imgur.com/vdQk8qO.png">

 - [x] Cadastrar um novo produto
 - [x] Editar um produto cadastrado
 - [x] Excluir um produto
 - [x] Obter detalhes de um produto
 - [x] Listar todos os produtos existentes de forma paginada com filtros de cada propriedade que seja relevante
 - [x] Desenvolvida utilizando .Net Core 3.1
 - [x] Dados Persistidos em um banco MongoDb
 - [x] Utilizar Docker com Docker-Compose
 - [x] Testes unitários com xUnit

### `A Api e como utilizar`
A aplicação é uma API Rest construída em cima da stack de ferramentas do .NET Core. Para persistir os dados ela utiliza o banco de dados MongoDB, um banco NoSQL que armazena dados de forma não estruturada.

Para facilitar o start da aplicação construí o ambiente de desenvolvimento utilizando Docker-Compose, este ambiente consiste de um DockerFile com as instruções necessárias para buildar uma aplicação .Net Core e dois contêineres que hospedam o MongoDB e a API em .NET Core.

Para rodar a aplicação basta adicionar o arquivo .ENV ( Na raiz do projeto, existe um arquivo chamado .env.example que pode ser utilizado como boiler plate para criar seu .env ).

Obs: As seguintes variáveis de ambiente são obrigatórias
```
DB_HOST=db
DB_PORT=27017
```
Abaixo um exemplo de .env que eu utilizei:
```
MODE_ENV=development

# Server
SERVER_PORT=3001

# DB
DB_HOST=db
DB_PORT=27017
DB_NAME=fakeommerce_api
DB_USER=fakeommerceApi
DB_PASS=fakeommerceApi
```

e rodar os seguintes comandos na raiz do projeto:

```bash
make up
```
Esses comandos vão buildar e instalar as dependências do projeto dentro do contêiner, e rodar os comandos necessários para subir a aplicação. O "make up" gerará os seguintes comandos no terminal:

```bash
docker-compose build
docker-compose -f docker-compose.yml up -d
docker exec db mongoimport --host db --username ${DB_USER} --password ${DB_PASS} --authenticationDatabase admin -d ${DB_NAME} -c products --type json --file /seed.json --jsonArray
```

O primeiro comando faz o build do projeto utilizando o dotnet-cli para restaurar e buildar as dependências.

O segundo executa o arquivo docker-compose.yml e o terceiro importa os valores iniciais que estão no arquivo seed.json para o MongoDb

Exemplo de como a seed está estruturada:

```json
[
	{
		"_id": {
			"$oid": "5f42fd067ee9eb00564f9fcd"
		},
		"name": "Tênis Nike Revolution 5 Masculino - Branco e Preto",
		"images": [
			"https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom1.jpg?ts=1584658433&",
			"https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom2.jpg?ts=1584658433&",
			"https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom3.jpg?ts=1584658433&",
			"https://static.netshoes.com.br/produtos/tenis-nike-revolution-5-masculino/09/HZM-1731-309/HZM-1731-309_zoom4.jpg?ts=1584658433&"
		],
		"categories": [
			"Calçados",
			"Tênis",
			"Amortecimento"
		],
		"price": 199.99,
		"brand": "Nike",
		"description": "Com ótimo amortecimento em espuma, o Tênis Nike Masculino Revolution 5 é ideal para os corredores que buscam leveza e conforto. Confeccionado em material respirável, esse tênis para correr possui reforço no calcanhar para proteger contra possíveis torções e fechamento dinâmico, garantindo um ajuste personalizado. Aposte na Nike para te acompanhar nos seus desafios dentro do esporte!"
	},
	{
		"_id": {
			"$oid": "5f42fd0c7ee9eb00564f9fce"
		},
		"name": "Kit Elástico Extensor com 11 Itens Treino Funcional Yangfit - Preto e Amarelo",
		"images": [
			"https://static.netshoes.com.br/produtos/kit-elastico-extensor-com-11-itens-treino-funcional-yangfit/78/ISQ-0058-178/ISQ-0058-178_zoom1.jpg?ts=1599055100&?ims=544xhttps://static.netshoes.com.br/produtos/kit-elastico-extensor-com-11-itens-treino-funcional-yangfit/78/ISQ-0058-178/ISQ-0058-178_zoom1.jpg?ts=1599055100&?ims=1088x",
			"https://static.netshoes.com.br/produtos/kit-elastico-extensor-com-11-itens-treino-funcional-yangfit/78/ISQ-0058-178/ISQ-0058-178_zoom2.jpg?ts=1599055100&"
		],
		"categories": [
			"Kit",
			"Musculação",
			"Esportes"
		],
		"price": 139.99,
		"brand": "Yangfit",
		"description": ""
	},
	{
		"_id": {
			"$oid": "5f42fd137ee9eb00564f9fcf"
		},
		"name": "Bicicleta South Hunter GT - Aro 29 - 21 Marchas - Freios a Disco - Suspensão Dianteira - Preto e Vermelho",
		"images": [
			"https://static.netshoes.com.br/produtos/bicicleta-south-hunter-gt-aro-29-21-marchas-freios-a-disco-suspensao-dianteira/02/ACN-0076-002/ACN-0076-002_zoom1.jpg?ts=1597759086&",
			"https://static.netshoes.com.br/produtos/bicicleta-south-hunter-gt-aro-29-21-marchas-freios-a-disco-suspensao-dianteira/02/ACN-0076-002/ACN-0076-002_zoom2.jpg?ts=1597759086&",
			"https://static.netshoes.com.br/produtos/bicicleta-south-hunter-gt-aro-29-21-marchas-freios-a-disco-suspensao-dianteira/02/ACN-0076-002/ACN-0076-002_zoom3.jpg?ts=1597759086&"
		],
		"categories": [
			"Esportes",
			"Bicicleta",
			"Locomoção"
		],
		"price": 1059.00,
		"brand": "South",
		"description": "Distribuído por: Southbike. Bicicleta South Hunter GT - Aro 29 - Freios a Disco - 21 Marchas - Aço. Informações Técnicas:. Modelo: Hunter GT. Gênero: Unissex. Quadro Hunter GT 29 em aço Tamanho 17. Aro Aero 29 Alumínio (parede dupla). Cubo Dianteiro/Traseiro 36F para Freio a Disco preto Importado. Câmara de Ar 29 Importada. Pneu 29x1.95 de preto Importado. Selim MTB Southbike. Canote de Selim. Pedivela Aço com Engrenagem Tripla encapada. Alavanca Grip Shift 21 velocidades Importada. Suporte de Guidão Aço ahead set preto. Guidão Aço 680mm MTB. Roda Livre 7 Velocidades Index. Câmbio Dianteiro 3V. Câmbio Traseiro 7V. Corrente fina Index Importada. Pedal plataforma de nylon Importado. Garfo de suspensão 29 Over em aço preto Importado. Freio a disco dianteiro e traseiro mecânico Importado. Garantia: 03 meses contra defeito de fabricação. País de Origem Brasil."
	}
]
```

O make up não exibe o terminal de forma interativa, portanto apenas com esse comando não é possível ver os logs, para ver os logs o MakeFile também possui um comando, basta digitar no terminal
```bash
make logs
```
E os logs tanto do MongoDb quanto da API vão aparecer no terminal.

Para parar a execução da aplicação e do banco basta utilizar o comando
```bash
make down
```
Por padrão, quando um dockerfile é buildado ele cria uma imagem dentro do docker. Para desenvolver a aplicação era necessário rebuildar toda vez que o código se alterava, para gerar uma nova DLL do projeto.

Para facilitar o processo basta rodar o comando
```bash
make rst
```
O make rst chamará os comandos down, up e logs nessa sequência.

#### `Utilizando a API`
Para utilizar a API qualquer programa que suporte o procolo HTTP pode ser utilizado.
Abaixo podemos ver todos os verbos com um exemplo de requisição e as possíveis respostas que a API pode produzir.

<img src="https://i.imgur.com/29chX9D.png">

Nenhuma API Key, JWT ou qualquer outra forma de autenticação foi implementada.

#### `Arquitetura`
A arquitetura da aplicação foi inspirada na arquitetura em camadas e escolhida de forma que deixasse o projeto extensível e fácil de adicionar novos modelos e endpoints.

<img src="https://i.imgur.com/9A8LVlw.png">

Para adicionar uma nova entidade basta adicionar suas devidas camadas na arquitetura desenhada acima, de forma que as regras de negócio fiquem concentradas nos modelos e desacopladas do resto do código.

#### `Testes`
A biblioteca de testes utilizada foi o xUnit para testes unitários. Nele podemos organizar os testes em suites de testes e ter um relatório de coverage que mostra quais partes do código foram testadas e estão cobertas e quais não estão.

Para rodar os testes da aplicação e gerar o relatório de coverage basta rodar o seguinte comando:

```bash
make tests
`````

Esses comandos vai gerar os seguibnte comando
#### `tests`
```bash
dotnet test ./FakeOmmerceTests/FakeOmmerceTests.csproj /p:CollectCoverage=true
`````
O relatório de coverage vai estar dentro da pasta FakeOmmerceTests

#### `As Ferramentas`
Para desenvolver a aplicação o ambiente utilizado foi o VS Code ( por estar mais familiarizado ) com o Dotnet CLI.

Outras ferramentas para facilitar a visualização do coverage, a extensão Coverage Gutters permite visualizar todas as linhas que estão cobertas dentro do código.

Para gerar os arquivos necessários para o Coverage Gutters o comando utilizado foi:
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info ./
`````

Também utilizei o .NET Core Test Explorer para monitorar os arquivos de testes e rodar novamente sempre que um teste fosse atualizado.

 - #### `Estrutura de pastas`
```
FakeOmmerce-api
├── Dockerfile
├── FakeOmmerceAPI
│   ├── FakeOmmerce-api.csproj
│   ├── Properties
│   │   └── launchSettings.json
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   └── src
│       ├── Context
│       │   ├── IProductContext.cs
│       │   └── ProductContext.cs
│       ├── Controllers
│       │   └── ProductController.cs
│       ├── Database
│       │   ├── MongoDbConfig.cs
│       │   └── ServerConfig.cs
│       ├── Errors
│       │   ├── BadRequestException.cs
│       │   ├── CannotValidateException.cs
│       │   ├── ConflictException.cs
│       │   ├── HttpError.cs
│       │   └── NotFoundException.cs
│       ├── Models
│       │   ├── FilterParameters.cs
│       │   ├── MongoEntity.cs
│       │   ├── Product.cs
│       │   └── ProductDTO.cs
│       ├── Program.cs
│       ├── Repository
│       │   ├── IProductRepository.cs
│       │   └── ProductRepository.cs
│       └── Startup.cs
├── FakeOmmerceTests
│   ├── FakeOmmerceTests.csproj
│   ├── coverage.info
│   ├── coverage.json
│   └── unitTests
│       ├── domain
│       │   ├── FilterParameters.test.cs
│       │   ├── MongoEntity.test.cs
│       │   ├── Product.test.cs
│       │   └── ProductDTO.test.cs
│       └── repository
│           └── UnitTest1.test.cs
├── Links.txt
├── Makefile
├── README.md
├── docker-compose.yml
└── seed.json
```
---
#####  `Considerações`
O projeto foi muito desafiador, mais por ter que buscar o conhecimento das ferramentas. 

Tentei focar na implementação de todos os requisitos sem sacrificar tanto o valor do código.

A parte do projeto que dei mais atenção foi a do desacoplamento da entidade de Produto com o MongoDB.Driver e outras partes da aplicação. Resolvi esse problema utilizando um mapper do próprio mongo e um DTO que pudesse instanciar minha classe respeitando todas as regras dos Setters.

O C# (em conjunto com o .Net Core) não é minha linguagem principal, da última vez que usei o C# foi em conjunto com o .Net Framework 3.5, nem é uma linguagem que eu costumava escrever código, mas, utilizando, vejo que possui muitas ferramentas inovadoras com o novo framework. Consegui entender melhor a Injeção de dependências, desacoplamento, tive o prazer de utilizar uma linguagem fortemente tipada que dá mais garantias aos desenvolvedores e mais confiabilidade no código que está sendo produzido. Também pude utilizar coisas novas como expressões, tipos anônimos e etc.

Felizmente a comunidade é sensacional e todo esse conteúdo eu consegui achar pesquisando, abaixo seguem alguns links que me ajudaram a desenvolver esse projeto:
``````
https://balta.io/cursos/fundamentos-csharp
https://balta.io/cursos/criando-apis-data-driven-com-aspnet-core-3-e-ef-core-3
https://app.pluralsight.com/paths/skills/c-unit-testing-with-xunit
https://docs.microsoft.com/pt-br/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-3.1&tabs=visual-studio
https://gist.github.com/a3dho3yn/91dcc7e6f606eaefaf045fc193d3dcc3
https://www.dotnetperls.com/uppercase-first-letter
https://stackoverflow.com/questions/11945201/how-to-get-content-type-of-a-web-address
http://www.macoratti.net/18/03/lnq_mapred1.htm
https://stackoverflow.com/questions/5744430/c-sharp-mongodb-how-to-correctly-map-a-domain-object
https://mongodb.github.io/mongo-csharp-driver/2.10/reference/bson/mapping/
https://xunit.github.io/
https://stackoverflow.com/questions/41958510/how-to-run-all-tests-in-visual-studio-code
https://stackoverflow.com/questions/11135337/xunit-assertion-for-checking-equality-of-objects
https://medium.com/@lorranpalmeira/code-coverage-no-c-com-vscode-cfa3cb6c89d0
``````

Os desafios encontrados nesse projeto foram grandes, mas acredito que com mais familiaridade com a ferramenta esses desafios se tornariam mais fáceis.

#####  `Pontos de melhora`
Acredito que o projeto ficou bem completo em implementação. No entanto, sempre há o que melhorar:

 - Fazer mais testes
 - Refatorar alguns pontos para melhorar a legibilidade
 - Loggar a aplicação
 - Implementar uma API Key ou Token
 - Modelas os domínios de uma forma mais rica
