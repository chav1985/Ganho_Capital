# Ganho de Capital

Para calcular impostos sobre as operações no mercado financeiro, foi desenvolvida uma aplicação em console para ler 
entradas (stdin) do tipo texto no formato JSON e retornar (stdout) texto no formato JSON com os impostos a serem pagos pela operação.

Foi utilizado o [.NET Core 5.0](https://www.microsoft.com/net/download) na solução, para atender o requisito de ser multiplataforma.

## Instruções para executar o build do projeto

Na raiz da solution, execute os comandos em um terminal para compilar:

```sh
dotnet restore
dotnet build -c Release
```

## Instruções para executar o projeto

Na raiz da solution, execute os comandos em um terminal para executar a aplicação em ambiente Linux:

```sh
cd Ganho_Capital/bin/Release/net5.0
./Ganho_Capital.exe
```

Na raiz da solution, execute os comandos em um terminal para executar a aplicação em ambiente Windows:

```sh
cd Ganho_Capital/bin/Release/net5.0
Ganho_Capital.exe
```

inserir sua(s) lista(s) no console, uma por linha e com uma linha vazia no final pressionar Enter e na saida tem o resultado.

Formato do JSON de Entrada:

```sh
[{"operation":"buy", "unit-cost":1.00, "quantity": 100},{"operation":"sell", "unit-cost":2.00, "quantity": 100}]
```

## Instruções para executar os testes do projeto

Na raiz da solution, execute os comandos em um terminal para executar os testes:

```sh
dotnet test --collect:"XPlat Code Coverage"
```