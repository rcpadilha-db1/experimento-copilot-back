# Experimento Copilot Back

## Como executar o projeto:
- Clonar o Projeto

git clone https://github.com/rcpadilha-db1/experimento-copilot-back.git

- Abrir em IDE com suporte ao [.Net8](<https://dotnet.microsoft.com/en-us/download/dotnet/8.0>)
- Ir até:
  
  ```TesteCopilot.Application```
  
- Rodar o comando:
  
  ```docker compose up -d```

-Ir até:

  ```TesteCopilot.Repository```

- Executar o comando no console nugget o seguinte comando, para gerar o banco de dados:
  
  ```Update-Database```

- Ao rodar a aplicação a documentação irá abrir com o Swagger:

  <https://localhost:7052/swagger>

## Testes Unitários:
- Ir até:
  
  ```TesteCopilot.UnitTest```

- Executar:

```dotnet test```

## Observações
Foi utilizado o Sqlite com os frameworks:

 Microsoft.EntityFrameworkCore.Sqlite Version="8.04.00"
 Microsoft.EntityFrameworkCore.SqlServer Version="8.04.00"
 Microsoft.EntityFrameworkCore.Tools
