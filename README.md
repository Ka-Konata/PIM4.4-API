﻿# PIM4.4 API
A RestFull API to connect all PIM4.4 related systems. Made using ASP.NET Core.
> version: .NET 6


## Clonando a Solução
### Instalar Pacotes NuGet:
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.Tools
* Pomelo.EntityFrameworkCore.MySql
* Newtonsoft.Json
* Microsoft.AspNetCore.Authentication.JwtBearer (versão 6.0.*)

> Change Jwt secretKey, issuer and audience on appsettings.json!

## Rotas
* GET api/login - Importante salvar o "token", o "refreshToken" e o "id"
* GET api/login/refresh
\
&nbsp;
* GET api/analistarh
* GET api/analistarh/{id}
* POST api/analistarh
* PUT api/analistarh
* DELETE api/analistarh/{id}
\
&nbsp;
* GET api/secretario
* GET api/secretario/{id}
* POST api/secretario
* PUT api/secretario
* DELETE api/secretario/{id}
\
&nbsp;
* GET api/professor
* GET api/professor/{id}
* POST api/professor
* PUT api/professor
* DELETE api/professor/{id}
\
&nbsp;
* GET api/aluno
* GET api/aluno/{id}
* POST api/aluno
* PUT api/aluno
* DELETE api/aluno/{id}
\
&nbsp;
* GET api/disciplina
* GET api/disciplina/{id}
* POST api/disciplina
* PUT api/disciplina
* DELETE api/disciplina/{id}
\
&nbsp;
* GET api/curso
* GET api/curso/{id}
* POST api/curso
* PUT api/curso
* DELETE api/curso/{id}