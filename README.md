# SecurityToken Generator

O **SecurityToken Generator** é um projeto que oferece uma WebAPI para a geração de tokens criptografados. Ele expõe um endpoint chamado `TokenGenerator/GenerateToken` que permite aos usuários gerar tokens criptografados usando diferentes tipos de algoritmos de criptografia simétrica. O tipo de criptografia é determinado através de um parâmetro no corpo da requisição.

## Funcionalidade

A API recebe uma requisição HTTP POST com o seguinte corpo:

- **typeToken**: Um inteiro que especifica o tipo de criptografia a ser utilizado. Os valores aceitos são:
  - `1`: AES (Advanced Encryption Standard)
  - `2`: RC4 (Rivest Cipher 4)
  - `3`: DES3 (Triple DES)
  - `4`: IDEA (International Data Encryption Algorithm)

Com base no valor de `typeToken`, o servidor irá gerar um token criptografado usando o algoritmo correspondente e retornará o resultado em um formato JSON.

## Endpoint

### POST `/TokenGenerator/GenerateToken`

#### Requisição

**URL**: `http://<seu-servidor>/TokenGenerator/GenerateToken`  
**Método**: POST  
**Cabeçalhos**:
- Content-Type: `application/json`

**Corpo**:

- **typeToken**: Um inteiro que especifica o tipo de criptografia a ser utilizado. Os valores aceitos são:
  - `1`: AES
  - `2`: RC4
  - `3`: DES3
  - `4`: IDEA

#### Resposta

A resposta será um objeto JSON contendo o token criptografado gerado com o algoritmo especificado. O campo `token` contém o token criptografado, que pode ser utilizado conforme a necessidade.

## Como rodar o projeto localmente

### Pré-requisitos

Certifique-se de ter os seguintes pré-requisitos instalados:

- [.NET Core SDK](https://dotnet.microsoft.com/download) ou .NET 6/7/8 (dependendo da versão utilizada no projeto)
- IDE como Visual Studio, Visual Studio Code, ou outra de sua preferência

### Passos para rodar

1. Clone o repositório.
2. Navegue até o diretório do projeto.
3. Restaure as dependências.
4. Execute o projeto.
5. A API estará disponível no endereço configurado (por padrão, `http://localhost:5000`).

## Exemplos de uso

### Exemplo de Requisição com AES (typeToken: 1)

#### bash
curl -X POST "http://localhost:5000/TokenGenerator/GenerateToken" -H "Content-Type: application/json" -d '{"typeToken": 1}'

### Exemplo de Requisição com RC4 (typeToken: 2)

#### bash
curl -X POST "http://localhost:5000/TokenGenerator/GenerateToken" -H "Content-Type: application/json" -d '{"typeToken": 2}'

## Contribuindo

Se você gostaria de contribuir para este projeto, siga as etapas abaixo:

1. Faça um fork deste repositório.
2. Crie uma nova branch para sua feature (`git checkout -b minha-feature`).
3. Faça suas alterações.
4. Envie um pull request para este repositório.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).